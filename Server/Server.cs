using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjS
{
    public partial class Server : Form
    {
        public class SocketNode // to keep socket infos of each individual user
        {
            public Socket socket;
            public string name;
            public List<string> friends;
            public bool connected;
            public SocketNode(Socket socket, string name, bool connected, List<string> friends)
            {
                this.socket = socket;
                this.name = name;
                this.friends = friends;
                this.connected = connected;
            }
        }

        static Socket serverSocket; // main server socker
        delegate void StringArgReturningVoidDelegate(string text);

        private List<string> members = new List<string>(); // name of members
        SocketNode[] userSockets;
        int socketLoq = 0;

        string[] pendingNotifications;
        int pendingNotifId = 0;

        static bool terminate = false;

        public Server() // Initialize the server
        {
            InitializeComponent();
            fillTheMembers();
            userSockets = new SocketNode[members.Count()];
            pendingNotifications = new string[members.Count()];
            pendingNotifications[pendingNotifId] = "";
        }

        private void checkPendingNotif(SocketNode user) // Handle notifications through the server
        {
            for (int i = 0; i < pendingNotifId; i++)
            {
                if (pendingNotifications[i] != "")
                {
                    string sentMessage = "";
                    string sender = "";
                    string receiver = "";
                    string[] allMessages;
                    string[] users;

                    allMessages = pendingNotifications[i].Split('#');
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

                    if (receiver == user.name)
                    {
                        SendMessage(user.socket, pendingNotifications[i]);
                        if (sentMessage == "(accepted)")
                            deletePendingNotif(pendingNotifications[i]);
                        if (sentMessage == "(declined)")
                            deletePendingNotif(pendingNotifications[i]);
                        if (sentMessage == "(removed)")
                            deletePendingNotif(pendingNotifications[i]);
                        if (sentMessage.Contains('='))
                            deletePendingNotif(pendingNotifications[i]);
                    }
                }
            }
        }

        private void deletePendingNotif(string text)
        {
            for (int i = 0; i < pendingNotifId; i++)
            {
                if (pendingNotifications[i] == text)
                {
                    pendingNotifications[i] = "";
                }
            }
        }

        private bool isNotificationExist(string text)
        {
            for (int i = 0; i < pendingNotifId; i++)
            {
                if (text == pendingNotifications[i])
                {
                    return true;
                }
            }
            return false;
        }

        private void fillTheMembers()
        {
            String filename = "user_db.txt";
            StreamReader sr = new StreamReader(filename);
            string l = "";
            while (l != null)
            {
                l = sr.ReadLine();
                members.Add(l);
            }
            sr.Close();
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            int portNum;

            if (portText.Text != "")
            {
                portNum = int.Parse(portText.Text);

                try
                {
                    serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, portNum);
                    serverSocket.Bind(endPoint);
                    serverSocket.Listen(3);

                    SetText("Server is listening...");

                    connectButton.Enabled = false;
                    portText.Enabled = false;
                    disconnectButton.Enabled = true;

                    terminate = false;

                    Thread acceptThread = new Thread(Accept);
                    acceptThread.Start();
                }
                catch
                {
                    SetText("There is a problem! Check the port number and try again!");
                }
            }

            else
            {
                SetText("The port number is not entered correctly!");
            }
        }

        private bool isConnected(string name)
        {
            for (int i = 0; i < socketLoq; i++)
            {
                if (userSockets[i].name == name)
                {
                    if (userSockets[i].connected == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool isConnectedBefore(string name)
        {
            for (int i = 0; i < socketLoq; i++)
            {
                if (userSockets[i].name == name)
                {
                    return true;
                }
            }
            return false;
        }

        private bool alreadyFriend(string friend, SocketNode user) // check if already friend two users are
        {
            for (int i = 0; i < user.friends.Count(); i++)
            {
                if (user.friends[i] == friend) return true;
            }
            return false;
        }

        private void Accept() // In case of acceptence
        {
            while (!terminate)
            {
                try
                {
                    Socket newClient = serverSocket.Accept();
                    string name = ReceiveMessage(newClient);
                    bool memberFound = false;

                    foreach (string member in members)
                    {
                        if (name == member && !memberFound)
                        {
                            memberFound = true;
                            if (!isConnected(name))
                            {
                                if (!isConnectedBefore(name))
                                {
                                    SetText(name + " is connected!");
                                    SendMessage(newClient, "You are connected!");
                                    List<string> friends = new List<string>();
                                    SocketNode newSocket = new SocketNode(newClient, name, true, friends);
                                    userSockets[socketLoq] = newSocket;
                                    socketLoq++;
                                    checkPendingNotif(newSocket);
                                    Thread receiveThread = new Thread(() => Receive(newSocket));
                                    receiveThread.Start();
                                }
                                else
                                {
                                    for (int i = 0; i < socketLoq; i++)
                                    {
                                        if (userSockets[i].name == name)
                                        {
                                            string message = "You are connected!#";
                                            for (int j = 0; j < userSockets[i].friends.Count(); j++)
                                            {
                                                message += "$" + userSockets[i].friends[j];
                                            }
                                            SendMessage(newClient, message);
                                            SetText(name + " is connected again!");
                                            userSockets[i].connected = true;
                                            List<string> friends = userSockets[i].friends;
                                            userSockets[i].socket = newClient;
                                            SocketNode newSocket = new SocketNode(newClient, name, true, friends);
                                            checkPendingNotif(newSocket);
                                            Thread receiveThread2 = new Thread(() => Receive(newSocket));
                                            receiveThread2.Start();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                SetText("User with the name " + name + " tried to connect but already connected!");
                                SendMessage(newClient, "You are already connected with the same name!");
                            }
                        }
                    }

                    if (!memberFound)
                    {
                        SendMessage(newClient, "You are not a member!");
                    }
                }
                catch
                {
                    SetText("Server no longer accepting any client!");
                }
            }
        }


        private void Receive(SocketNode user) // recieve from clients
        {
            int loc = 0;
            for (int i = 0; i < socketLoq; i++) // check for the user
            {
                if (userSockets[i].name == user.name)
                {
                    loc = i;
                }
            }

            if (isConnected(user.name)) // check if the given name is connected
            {
                while (userSockets[loc].connected)
                {
                    try
                    {
                        string receivedMessages = ReceiveMessage(user.socket);
                        string sentMessage = "";
                        string inviter = "";
                        string invitee = "";
                        string[] allMessages;
                        string[] users;

                        allMessages = receivedMessages.Split('#');
                        sentMessage = allMessages[1];
                        if (allMessages[0].Contains('%'))
                        {
                            users = allMessages[0].Split('%');
                            inviter = users[0];
                            invitee = users[1];
                        }
                        else
                        {
                            inviter = allMessages[0];
                        }

                        if (sentMessage == "(accepted)") // notify the acceptence
                        {
                            SetText(inviter + " accepted " + invitee + "'s request!");
                            deletePendingNotif(invitee + "%" + inviter + "#" + "(invitation)");
                            bool sent = false;
                            for (int i = 0; i < socketLoq; i++)
                            {
                                if (userSockets[i].name == invitee && sentMessage != "")
                                {
                                    if (userSockets[i].connected)
                                    {
                                        SendMessage(userSockets[i].socket, receivedMessages);
                                        sent = true;
                                    }
                                    else
                                    {
                                        pendingNotifications[pendingNotifId] = receivedMessages;
                                        pendingNotifId++;
                                        pendingNotifications[pendingNotifId] = "";
                                        sent = true;
                                    }
                                    userSockets[i].friends.Add(inviter);
                                    userSockets[loc].friends.Add(invitee);
                                }
                            }
                            if (!sent) // if sent fails
                            {
                                bool memberFound = false;
                                foreach (string member in members)
                                {
                                    if (invitee == member && !memberFound)
                                    {
                                        pendingNotifications[pendingNotifId] = receivedMessages;
                                        pendingNotifId++;
                                        pendingNotifications[pendingNotifId] = "";
                                        sent = true;
                                        memberFound = true;
                                    }
                                }
                            }
                        }

                        else if (sentMessage == "(declined)") // notify if declined
                        {
                            SetText(inviter + " declined " + invitee + "'s request!");
                            deletePendingNotif(invitee + "%" + inviter + "#" + "(invitation)");
                            bool sent = false;
                            for (int i = 0; i < socketLoq; i++)
                            {
                                if (userSockets[i].name == invitee && sentMessage != "")
                                {
                                    if (userSockets[i].connected)
                                    {
                                        SendMessage(userSockets[i].socket, receivedMessages);
                                        sent = true;
                                    }
                                    else
                                    {
                                        pendingNotifications[pendingNotifId] = receivedMessages;
                                        pendingNotifId++;
                                        pendingNotifications[pendingNotifId] = "";
                                        sent = true;
                                    }
                                }
                            }
                            if (!sent) // if decline notification fails
                            {
                                bool memberFound = false;
                                foreach (string member in members)
                                {
                                    if (invitee == member && !memberFound)
                                    {
                                        pendingNotifications[pendingNotifId] = receivedMessages;
                                        pendingNotifId++;
                                        pendingNotifications[pendingNotifId] = "";
                                        sent = true;
                                        memberFound = true;
                                    }
                                }
                            }
                        }

                        else if (sentMessage == "(removed)") // notify if removed
                        {
                            SetText(inviter + " removed " + invitee + " from friendlist!");
                            bool sent = false;
                            for (int i = 0; i < socketLoq; i++)
                            {
                                if (userSockets[i].name == invitee && sentMessage != "")
                                {
                                    if (userSockets[i].connected)
                                    {
                                        SendMessage(userSockets[i].socket, receivedMessages);
                                        sent = true;
                                    }
                                    else
                                    {
                                        pendingNotifications[pendingNotifId] = receivedMessages;
                                        pendingNotifId++;
                                        pendingNotifications[pendingNotifId] = "";
                                        sent = true;
                                    }
                                    userSockets[i].friends.Remove(inviter);
                                    userSockets[loc].friends.Remove(invitee);
                                }
                            }
                            if (!sent) // if removed fails
                            {
                                bool memberFound = false;
                                foreach (string member in members)
                                {
                                    if (invitee == member && !memberFound)
                                    {
                                        pendingNotifications[pendingNotifId] = receivedMessages;
                                        pendingNotifId++;
                                        pendingNotifications[pendingNotifId] = "";
                                        sent = true;
                                        memberFound = true;
                                    }
                                }
                            }
                        }

                        else if (sentMessage == "(invitation)")
                        {

                            bool memberFound = false;

                            foreach (string member in members)
                            {
                                if (invitee == member && !memberFound)
                                {
                                    memberFound = true;
                                }
                            }

                            if (alreadyFriend(invitee, userSockets[loc]))
                            {
                                SetText(inviter + "tried to add already existing friend!");
                                SendMessage(user.socket, user.name + "#(alreadyFriend)");
                            }

                            else if (isNotificationExist(invitee + "%" + inviter + "#(invitation)"))
                            {
                                SetText(userSockets[loc].name + " tried to add friend when there is already a pending invite!");
                                SendMessage(userSockets[loc].socket, userSockets[loc].name + "#(alreadyPendingInvitation)");
                            }

                            else if (!isNotificationExist(receivedMessages)) // if havent notified yet
                            {
                                if (memberFound)
                                {
                                    SetText(inviter + " wants to be " + invitee + "'s friend!");
                                    bool sent = false;
                                    for (int i = 0; i < socketLoq; i++)
                                    {
                                        if (userSockets[i].name == invitee && sentMessage != "")
                                        {
                                            SendMessage(userSockets[i].socket, receivedMessages);
                                            sent = true;
                                            pendingNotifications[pendingNotifId] = receivedMessages;
                                            pendingNotifId++;
                                            pendingNotifications[pendingNotifId] = "";
                                        }
                                    }
                                    if (!sent) // if sent fails
                                    {
                                        memberFound = false;
                                        foreach (string member in members)
                                        {
                                            if (invitee == member && !memberFound)
                                            {
                                                pendingNotifications[pendingNotifId] = receivedMessages;
                                                pendingNotifId++;
                                                pendingNotifications[pendingNotifId] = "";
                                                sent = true;
                                                memberFound = true;
                                            }
                                        }
                                    }
                                }

                                else
                                {
                                    SetText(userSockets[loc].name + " tried to be friend with unknown member!");
                                    SendMessage(userSockets[loc].socket, userSockets[loc].name + "#(unknownInvitation)");
                                }
                            }
                            else
                            {
                                SetText(userSockets[loc].name + " tried to add friend more than one time!");
                                SendMessage(userSockets[loc].socket, userSockets[loc].name + "#(duplicateInvitation)");
                            }
                        }
                        else if (sentMessage == "(showfriends)") // show friends of given user
                        {
                            string friendsMessage = "";
                            foreach (string hisFriend in userSockets[loc].friends)
                            {
                                friendsMessage += hisFriend + "$";
                            }
                            SendMessage(userSockets[loc].socket, sentMessage + "#" + friendsMessage);
                        }
                        else
                        {
                            if (sentMessage != "")
                            {
                                SetText(inviter + ": " + sentMessage);
                            }
                            for (int i = 0; i < socketLoq; i++)
                            {
                                if (userSockets[i].name != inviter && sentMessage != "" && alreadyFriend(inviter, userSockets[i]))
                                {
                                    if (userSockets[i].connected)
                                        SendMessage(userSockets[i].socket, inviter + "#" + sentMessage);
                                    else
                                    {
                                        pendingNotifications[pendingNotifId] = inviter + "%" + userSockets[i].name + "#(message)=" + sentMessage;
                                        pendingNotifId++;
                                        pendingNotifications[pendingNotifId] = "";
                                    }
                                }
                            }
                        }
                    }
                    catch // handle disconnection
                    {
                        SetText(userSockets[loc].name + " has disconnected!");
                        userSockets[loc].connected = false;
                        userSockets[loc].socket.Close();
                    }
                }
            }
        }

        private void SetText(string text)
        {
            try
            {
                if (this.textBox.InvokeRequired)
                {
                    StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(SetText);
                    this.Invoke(d, new object[] { text });
                }
                else
                {
                    this.textBox.AppendText(text + "\n");
                }
            }

            catch
            {
            }
        }

        private void sendToAll(string message) // to send spesific messages to all users
        {
            for (int i = 0; i < socketLoq; i++)
            {
                if (userSockets[i].connected)
                {
                    SendMessage(userSockets[i].socket, message);
                }
            }
        }

        private string ReceiveMessage(Socket thisClient) // handle receiving messages
        {

            Byte[] sizeBuffer = new Byte[4];
            thisClient.Receive(sizeBuffer, 0, sizeBuffer.Length, 0);


            int size = BitConverter.ToInt32(sizeBuffer, 0);
            string before = "";

            MemoryStream ms = new MemoryStream();
            Byte[] buffer;

            while (size > 0)
            {
                if (size < thisClient.ReceiveBufferSize)
                    buffer = new Byte[size];
                else
                    buffer = new Byte[thisClient.ReceiveBufferSize];

                int rec = thisClient.Receive(buffer, 0, buffer.Length, 0);
                size -= rec;
                ms.Write(buffer, 0, buffer.Length);
            }
            ms.Close();

            Byte[] data = ms.ToArray();
            ms.Dispose();

            before = Encoding.Default.GetString(data);

            return before;
        }

        private void SendMessage(Socket thisClient, string message)
        {
            try
            {
                if (thisClient != null)
                {
                    Byte[] data = Encoding.Default.GetBytes(message);
                    thisClient.Send(BitConverter.GetBytes(data.Length), 0, 4, 0);
                    thisClient.Send(data);
                }
            }
            catch
            {
            }
        }

        private void disconnectButton_Click(object sender, EventArgs e) // disconnect the server 
        {
            terminate = true;
            try
            {
                sendToAll("Server is disconnected!");
                SetText("Server is closing...");

                serverSocket.Close();
                textBox.AppendText("Server has disconnected. \n");

                connectButton.Enabled = true;
                portText.Enabled = true;
                disconnectButton.Enabled = false;

                serverSocket.Close();
                return;
            }
            catch
            {
                Application.Exit();
            }

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            terminate = true;
            try
            {
                sendToAll("Server has disconnected!");
                SetText("Server is closing...");
                serverSocket.Close();                                           //We send a message to all clients in case the server closes.
                Application.Exit();
                return;
            }
            catch
            {
                Application.Exit();
            }
        }

        private void rich_Text_TextChanged(object sender, EventArgs e)
        {

        }

        private void portText_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
