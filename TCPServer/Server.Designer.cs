
namespace TCPServer
{
    partial class Server
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Receive_RichTextBox = new System.Windows.Forms.RichTextBox();
            this.Send_RichTextBox = new System.Windows.Forms.RichTextBox();
            this.Send_Button = new System.Windows.Forms.Button();
            this.ServerPort_TextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ServerIP_TextBox = new System.Windows.Forms.TextBox();
            this.IPCollection_listBox = new System.Windows.Forms.ListBox();
            this.StartServer_Button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Receive_RichTextBox
            // 
            this.Receive_RichTextBox.Location = new System.Drawing.Point(71, 50);
            this.Receive_RichTextBox.Name = "Receive_RichTextBox";
            this.Receive_RichTextBox.Size = new System.Drawing.Size(481, 248);
            this.Receive_RichTextBox.TabIndex = 0;
            this.Receive_RichTextBox.Text = "";
            // 
            // Send_RichTextBox
            // 
            this.Send_RichTextBox.Location = new System.Drawing.Point(71, 360);
            this.Send_RichTextBox.Name = "Send_RichTextBox";
            this.Send_RichTextBox.Size = new System.Drawing.Size(481, 46);
            this.Send_RichTextBox.TabIndex = 1;
            this.Send_RichTextBox.Text = "";
            // 
            // Send_Button
            // 
            this.Send_Button.Location = new System.Drawing.Point(583, 360);
            this.Send_Button.Name = "Send_Button";
            this.Send_Button.Size = new System.Drawing.Size(148, 46);
            this.Send_Button.TabIndex = 2;
            this.Send_Button.Text = "发送消息";
            this.Send_Button.UseVisualStyleBackColor = true;
            this.Send_Button.Click += new System.EventHandler(this.Send_Button_Click);
            // 
            // ServerPort_TextBox
            // 
            this.ServerPort_TextBox.Location = new System.Drawing.Point(670, 103);
            this.ServerPort_TextBox.Name = "ServerPort_TextBox";
            this.ServerPort_TextBox.Size = new System.Drawing.Size(100, 23);
            this.ServerPort_TextBox.TabIndex = 11;
            this.ServerPort_TextBox.Text = "9600";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(583, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "服务端端口";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(583, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "服务端IP";
            // 
            // ServerIP_TextBox
            // 
            this.ServerIP_TextBox.Location = new System.Drawing.Point(670, 66);
            this.ServerIP_TextBox.Name = "ServerIP_TextBox";
            this.ServerIP_TextBox.Size = new System.Drawing.Size(100, 23);
            this.ServerIP_TextBox.TabIndex = 8;
            this.ServerIP_TextBox.Text = "127.0.0.1";
            // 
            // IPCollection_listBox
            // 
            this.IPCollection_listBox.FormattingEnabled = true;
            this.IPCollection_listBox.ItemHeight = 17;
            this.IPCollection_listBox.Location = new System.Drawing.Point(583, 182);
            this.IPCollection_listBox.Name = "IPCollection_listBox";
            this.IPCollection_listBox.Size = new System.Drawing.Size(148, 106);
            this.IPCollection_listBox.TabIndex = 12;
            // 
            // StartServer_Button
            // 
            this.StartServer_Button.Location = new System.Drawing.Point(583, 294);
            this.StartServer_Button.Name = "StartServer_Button";
            this.StartServer_Button.Size = new System.Drawing.Size(148, 44);
            this.StartServer_Button.TabIndex = 13;
            this.StartServer_Button.Text = "启动服务";
            this.StartServer_Button.UseVisualStyleBackColor = true;
            this.StartServer_Button.Click += new System.EventHandler(this.StartServer_Button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(583, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "客户端IP列表";
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.StartServer_Button);
            this.Controls.Add(this.IPCollection_listBox);
            this.Controls.Add(this.ServerPort_TextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ServerIP_TextBox);
            this.Controls.Add(this.Send_Button);
            this.Controls.Add(this.Send_RichTextBox);
            this.Controls.Add(this.Receive_RichTextBox);
            this.Name = "Server";
            this.Text = "服务器端";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Server_FormClosing);
            this.Load += new System.EventHandler(this.Server_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox Receive_RichTextBox;
        private System.Windows.Forms.RichTextBox Send_RichTextBox;
        private System.Windows.Forms.Button Send_Button;
        private System.Windows.Forms.TextBox ServerPort_TextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ServerIP_TextBox;
        private System.Windows.Forms.ListBox IPCollection_listBox;
        private System.Windows.Forms.Button StartServer_Button;
        private System.Windows.Forms.Label label3;
    }
}

