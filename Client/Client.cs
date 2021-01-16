using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjC
{
    public partial class Client : Form
    {
        static bool connected = false; // boolean of connection
        string name = ""; // name of the user

        delegate void StringArgReturningVoidDelegate(string text);

        private void SetText(string text) 
        {
            try
            {
                if (this.chatText.InvokeRequired)
                {
                    StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(SetText);
                    this.Invoke(d, new object[] { text });
                }
                else
                {
                    this.chatText.AppendText(text + "\n");
                }
            }
            catch
            {
            }
        }
        
        public Socket userSocket; 

        public Client()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e) // send message if connected
        {
            if (connected)
            {
                try
                {
                    string message = name + "#" + messageText.Text;
                    SendMessage(message);
                    SetText("Me: " + messageText.Text); 
                }

                catch
                {
                    SetText("Message couldn't send.\n");
                }
            }
            else
            {
                chatText.AppendText("You are not connected to the server.\n");
            }
        }



        private void btnConnect_Click(object sender, EventArgs e) // connect button
        {

            if (connectButton.Text == "Connect")
            {
                string serverIP = ipBox.Text;
                string port = portBox.Text;
                name = nameText.Text;
                int portNum;

                if (serverIP == "" || port == "" || nameText.Text == "")
                    SetText("Entered information is not correct...");

                else
                {
                    portNum = int.Parse(port);

                    try
                    {
                        userSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        userSocket.Connect(serverIP, portNum);
                        SendMessage(name);
                        
                        string allMessage = ReceiveMessage();
                        string[] dividedMessage = allMessage.Split('#');
                        string message = dividedMessage[0];
                        if (allMessage.Contains('$'))
                        {
                            string[] friendArr = dividedMessage[1].Split('$');
                            for (int i = 1; i < friendArr.Count(); i++)
                            {
                                friendsBox.Items.Add(friendArr[i]);
                            }

                        }
                        SetText(message);

                        if (message == "You are connected!")
                        {

                            connected = true;
                            messageText.Enabled = true;
                            SendButton.Enabled = true;
                            friendship.Enabled = true;
                            DisconnectBox.Enabled = true;
                            removeButton.Enabled = false;

                            if (friendReqBox.Items.Count > 0)
                            {
                                acceptButton.Enabled = true;
                                declineButton.Enabled = true;
                            }

                            if (friendsBox.Items.Count > 0)
                            {
                                removeButton.Enabled = true;
                            }

                            connectButton.Enabled = false;
                            portBox.Enabled = false;
                            nameText.Enabled = false;
                            ipBox.Enabled = false;
                            
                            Thread receiveThread = new Thread(Receive); 
                            receiveThread.Start();
                        }

                    }
                    catch 
                    {
                        SetText("Client cannot able to find any server in given adress...");
                    }
                }
            }
        }

        private void Receive() // use received messages from the server
        {
            while (connected) // while connection is not failed
            {
                try 
                {
                    string receivedMessages = ReceiveMessage();
                    string sentMessage = "";
                    string sender = "";
                    string receiver = "";
                    string[] allMessages;
                    string[] users;

                    allMessages = receivedMessages.Split('#');
                    sentMessage = allMessages[1];
                    if (allMessages[0].Contains('%'))
                    {
                        users = allMessages[0].Split('%');
                        sender = users[0];
                        receiver = users[1];
                    }
                    else
                    {
                        sender = allMessages[0];
                    }

                    if (sentMessage == "(invitation)")
                    {
                        friendReqBox.Items.Add(sender);
                        
                        acceptButton.Enabled = true;
                        declineButton.Enabled = true;
                    }

                    else if (sentMessage.Contains('='))
                    {
                        string[] temp = sentMessage.Split('=');
                        string m = temp[1];
                        SetText(sender + " sent you message when you were offline: " + m);
                    }
                    else if (sentMessage == "(accepted)")
                    {
                        SetText(sender + " accepted your friend request!");
                        bool found = false;
                        foreach (var item in friendsBox.Items)
                        {
                            if (item.ToString() == sender) found = true;
                        }
                        if (!found) friendsBox.Items.Add(sender);
                        if (friendsBox.Items.Count > 0) removeButton.Enabled = true;

                    }

                    else if (sentMessage == "(declined)")
                    {
                        SetText(sender + " declined your friend request!");
                    }

                    else if (sentMessage == "(unknownInvitation)")
                    {
                        SetText("User you tried to be friend not exists!");
                    }

                    else if (sentMessage == "(duplicateInvitation)")
                    {
                        SetText("Can not add same friend more than one time!");
                    }

                    else if (sentMessage == "(alreadyPendingInvitation)")
                    {
                        SetText("The user already has a pending notification!");
                    }

                    else if (sentMessage == "(alreadyFriend)")
                    {
                        SetText("You are already friend!");
                    }

                    else if (sentMessage == "(removed)")
                    {
                        SetText(sender + " removed you from friend list!");
                        for (int i = 0; i < friendsBox.Items.Count; i++)
                        {
                            if (friendsBox.Items[i].ToString() == sender)
                                friendsBox.Items.RemoveAt(i);

                        }

                        if (friendsBox.Items.Count == 0) removeButton.Enabled = false;
                    }

                    else
                    {
                        if (connected && sentMessage != "" && sentMessage != " ")
                        {
                            SetText(sender + ": " + sentMessage);
                        }
                    }
                }
                catch
                {
                    connected = false;
                    userSocket.Close();
                }
            }
        }

        private string ReceiveMessage() // receive message
        {
            Byte[] sizeBuffer = new Byte[4];
            userSocket.Receive(sizeBuffer, 0, sizeBuffer.Length, 0);

            int size = BitConverter.ToInt32(sizeBuffer, 0);
            string before = "";

            MemoryStream ms = new MemoryStream();
            Byte[] buffer;

            while (size > 0)
            {
                if (size < userSocket.ReceiveBufferSize)
                    buffer = new Byte[size];
                else
                    buffer = new Byte[userSocket.ReceiveBufferSize];

                int rec = userSocket.Receive(buffer, 0, buffer.Length, 0);
                size -= rec;
                ms.Write(buffer, 0, buffer.Length);
            }
            ms.Close();

            Byte[] data = ms.ToArray();
            ms.Dispose();

            before = Encoding.Default.GetString(data);
 
            return before;
        }

        private void SendMessage(string message) // send message
        {
            try  
            {
                Byte[] data = Encoding.Default.GetBytes(message);
                userSocket.Send(BitConverter.GetBytes(data.Length), 0, 4, 0);
                userSocket.Send(data);

            }
            catch 
            {
                SetText("Error in sending message!");
            }
        }

        private void DisconnectBox_Click(object sender, EventArgs e) // Disconnect button
        {
            SetText("You have been disconnected.\n");

            friendReqBox.Items.Clear();
            friendsBox.Items.Clear();

            connected = false;
            friendship.Enabled = false;
            messageText.Enabled = false;
            DisconnectBox.Enabled = false;
            acceptButton.Enabled = false;
            declineButton.Enabled = false;
            removeButton.Enabled = false;

            connectButton.Enabled = true;
            portBox.Enabled = true;
            nameText.Enabled = true;
            ipBox.Enabled = true;

            userSocket.Disconnect(true);
        }

        private void acceptButtonClick(object sender, EventArgs e)  // accept selected friendship request
        {
            if (friendReqBox.SelectedIndex != -1) // -1 for no request
            { 
                string selectedFriend = friendReqBox.Items[friendReqBox.SelectedIndex].ToString();
                SetText(selectedFriend + "'s friendship request accepted!");
                string message = name + "%" + selectedFriend + "#" + "(accepted)";

                SendMessage(message);

                friendsBox.Items.Add(selectedFriend);
                friendReqBox.Items.RemoveAt(friendReqBox.SelectedIndex);

                if(friendReqBox.Items.Count == 0)
                {
                    acceptButton.Enabled = false;
                    declineButton.Enabled = false;
                }
                if (friendsBox.Items.Count > 0)
                {
                    removeButton.Enabled = true;
                }
                else removeButton.Enabled = false;
            }

        }

        private void declineButtonClick(object sender, EventArgs e) // decline selected friendship request
        {
            if (friendReqBox.SelectedIndex != -1) // -1 for no friendship request
            {
                string selectedFriend = friendReqBox.Items[friendReqBox.SelectedIndex].ToString();
                SetText(selectedFriend + "'s friendship request declined!");
                string message = name + "%" + selectedFriend + "#" + "(declined)";

                SendMessage(message);

                friendReqBox.Items.RemoveAt(friendReqBox.SelectedIndex);

                if (friendReqBox.Items.Count == 0)
                {
                    acceptButton.Enabled = false;
                    declineButton.Enabled = false;
                }

                if (friendsBox.Items.Count == 0)
                {
                    removeButton.Enabled = false;
                }
            }
        }

        private void friendshipClick(object sender, EventArgs e) // send friendship invitation
        {
            if (messageText.Text != name) // check if invitation is to another user then itself
            {
                string message = name + "%" + messageText.Text + "#" + "(invitation)" ;
                SetText("Invitation on progress!");
                SendMessage(message);
            }
            else // else warn
            {
                SetText("Cannot send invitation to yourself!");
            }
        }

        private void removeButtonClick(object sender, EventArgs e) // remove selected friend
        {
            if (friendsBox.SelectedIndex != -1) // -1 for no friend case
            {
                string selectedFriend = friendsBox.Items[friendsBox.SelectedIndex].ToString();
                SetText(selectedFriend + " is removed from your friend list!");
                string message = name + "%" + selectedFriend + "#" + "(removed)";

                SendMessage(message);

                friendsBox.Items.RemoveAt(friendsBox.SelectedIndex);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e) // close the form
        {
            connected = false;
            try
            {
                SetText("Program is closing...");
                userSocket.Close();                                           
                Application.Exit();
                return;
            }
            catch
            {
                Application.Exit();
            }
        }

        private void rb_chat_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void nameText_TextChanged(object sender, EventArgs e)
        {

        }

        private void messageText_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
