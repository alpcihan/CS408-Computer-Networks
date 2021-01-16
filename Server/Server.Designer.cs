namespace ProjS
{
    partial class Server
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
            this.textBox = new System.Windows.Forms.RichTextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.portText = new System.Windows.Forms.TextBox();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.nameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(42, 114);
            this.textBox.Margin = new System.Windows.Forms.Padding(6);
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.Size = new System.Drawing.Size(817, 400);
            this.textBox.TabIndex = 5;
            this.textBox.Text = "";
            this.textBox.TextChanged += new System.EventHandler(this.rich_Text_TextChanged);
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(412, 33);
            this.connectButton.Margin = new System.Windows.Forms.Padding(6);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(153, 45);
            this.connectButton.TabIndex = 9;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // portText
            // 
            this.portText.Location = new System.Drawing.Point(42, 40);
            this.portText.Margin = new System.Windows.Forms.Padding(6);
            this.portText.Name = "portText";
            this.portText.Size = new System.Drawing.Size(340, 31);
            this.portText.TabIndex = 10;
            this.portText.TextChanged += new System.EventHandler(this.portText_TextChanged);
            // 
            // disconnectButton
            // 
            this.disconnectButton.Enabled = false;
            this.disconnectButton.Location = new System.Drawing.Point(590, 33);
            this.disconnectButton.Margin = new System.Windows.Forms.Padding(6);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(153, 45);
            this.disconnectButton.TabIndex = 11;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(37, 9);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(57, 25);
            this.nameLabel.TabIndex = 12;
            this.nameLabel.Text = "Port:";
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 529);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.portText);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.textBox);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Server";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox textBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TextBox portText;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.Label nameLabel;
    }
}

