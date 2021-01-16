namespace ProjC
{
    partial class Client
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chatText = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.messageText = new System.Windows.Forms.TextBox();
            this.nameText = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.portBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ipBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DisconnectBox = new System.Windows.Forms.Button();
            this.acceptButton = new System.Windows.Forms.Button();
            this.declineButton = new System.Windows.Forms.Button();
            this.friendship = new System.Windows.Forms.Button();
            this.friendsBox = new System.Windows.Forms.ListBox();
            this.friendReqBox = new System.Windows.Forms.ListBox();
            this.removeButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chatText
            // 
            this.chatText.Location = new System.Drawing.Point(10, 13);
            this.chatText.Name = "chatText";
            this.chatText.ReadOnly = true;
            this.chatText.Size = new System.Drawing.Size(230, 200);
            this.chatText.TabIndex = 1;
            this.chatText.Text = "";
            this.chatText.TextChanged += new System.EventHandler(this.rb_chat_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chatText);
            this.groupBox1.Location = new System.Drawing.Point(-2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(245, 221);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chat";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // SendButton
            // 
            this.SendButton.Enabled = false;
            this.SendButton.Location = new System.Drawing.Point(12, 230);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(230, 23);
            this.SendButton.TabIndex = 3;
            this.SendButton.Text = "Send message to your friends";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // messageText
            // 
            this.messageText.Enabled = false;
            this.messageText.HideSelection = false;
            this.messageText.Location = new System.Drawing.Point(8, 288);
            this.messageText.Name = "messageText";
            this.messageText.Size = new System.Drawing.Size(235, 20);
            this.messageText.TabIndex = 5;
            this.messageText.TextChanged += new System.EventHandler(this.messageText_TextChanged);
            // 
            // nameText
            // 
            this.nameText.Location = new System.Drawing.Point(301, 12);
            this.nameText.Name = "nameText";
            this.nameText.Size = new System.Drawing.Size(133, 20);
            this.nameText.TabIndex = 6;
            this.nameText.TextChanged += new System.EventHandler(this.nameText_TextChanged);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(257, 19);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(38, 13);
            this.nameLabel.TabIndex = 7;
            this.nameLabel.Text = "Name:";
            this.nameLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(359, 90);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 8;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // portBox
            // 
            this.portBox.Location = new System.Drawing.Point(301, 38);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(133, 20);
            this.portBox.TabIndex = 9;
            this.portBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(257, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Port:";
            // 
            // ipBox
            // 
            this.ipBox.Location = new System.Drawing.Point(301, 64);
            this.ipBox.Name = "ipBox";
            this.ipBox.Size = new System.Drawing.Size(133, 20);
            this.ipBox.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(257, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "IP:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // DisconnectBox
            // 
            this.DisconnectBox.Enabled = false;
            this.DisconnectBox.Location = new System.Drawing.Point(260, 90);
            this.DisconnectBox.Name = "DisconnectBox";
            this.DisconnectBox.Size = new System.Drawing.Size(75, 23);
            this.DisconnectBox.TabIndex = 13;
            this.DisconnectBox.Text = "Disconnect";
            this.DisconnectBox.UseVisualStyleBackColor = true;
            this.DisconnectBox.Click += new System.EventHandler(this.DisconnectBox_Click);
            // 
            // acceptButton
            // 
            this.acceptButton.Enabled = false;
            this.acceptButton.Location = new System.Drawing.Point(260, 259);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(75, 23);
            this.acceptButton.TabIndex = 14;
            this.acceptButton.Text = "Accept";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButtonClick);
            // 
            // declineButton
            // 
            this.declineButton.Enabled = false;
            this.declineButton.Location = new System.Drawing.Point(260, 285);
            this.declineButton.Name = "declineButton";
            this.declineButton.Size = new System.Drawing.Size(75, 23);
            this.declineButton.TabIndex = 15;
            this.declineButton.Text = "Decline";
            this.declineButton.UseVisualStyleBackColor = true;
            this.declineButton.Click += new System.EventHandler(this.declineButtonClick);
            // 
            // friendship
            // 
            this.friendship.Location = new System.Drawing.Point(12, 259);
            this.friendship.Name = "friendship";
            this.friendship.Size = new System.Drawing.Size(230, 23);
            this.friendship.TabIndex = 16;
            this.friendship.Text = "Send Friendship Request";
            this.friendship.UseVisualStyleBackColor = true;
            this.friendship.Click += new System.EventHandler(this.friendshipClick);
            // 
            // friendsBox
            // 
            this.friendsBox.FormattingEnabled = true;
            this.friendsBox.Location = new System.Drawing.Point(359, 145);
            this.friendsBox.Name = "friendsBox";
            this.friendsBox.Size = new System.Drawing.Size(75, 134);
            this.friendsBox.TabIndex = 20;
            // 
            // friendReqBox
            // 
            this.friendReqBox.FormattingEnabled = true;
            this.friendReqBox.Location = new System.Drawing.Point(260, 145);
            this.friendReqBox.Name = "friendReqBox";
            this.friendReqBox.Size = new System.Drawing.Size(75, 108);
            this.friendReqBox.TabIndex = 21;
            // 
            // removeButton
            // 
            this.removeButton.Enabled = false;
            this.removeButton.Location = new System.Drawing.Point(359, 285);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 22;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButtonClick);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(248, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 3);
            this.label3.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(248, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(3, 314);
            this.label4.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(345, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(3, 192);
            this.label5.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(255, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Friend Requests";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(374, 129);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Friends";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 324);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.friendReqBox);
            this.Controls.Add(this.friendsBox);
            this.Controls.Add(this.friendship);
            this.Controls.Add(this.declineButton);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.DisconnectBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ipBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.portBox);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.nameText);
            this.Controls.Add(this.messageText);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.groupBox1);
            this.Name = "Client";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Client";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox chatText;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.TextBox messageText;
        private System.Windows.Forms.TextBox nameText;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TextBox portBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ipBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button DisconnectBox;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Button declineButton;
        private System.Windows.Forms.Button friendship;
        private System.Windows.Forms.ListBox friendsBox;
        private System.Windows.Forms.ListBox friendReqBox;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}

