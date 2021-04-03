
namespace TCPClient
{
    partial class Client
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
            this.Send_Button = new System.Windows.Forms.Button();
            this.LinkServer_Button = new System.Windows.Forms.Button();
            this.ClientIP_TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ClientPort_TextBox = new System.Windows.Forms.TextBox();
            this.Send_RichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // Receive_RichTextBox
            // 
            this.Receive_RichTextBox.Location = new System.Drawing.Point(71, 35);
            this.Receive_RichTextBox.Name = "Receive_RichTextBox";
            this.Receive_RichTextBox.Size = new System.Drawing.Size(489, 279);
            this.Receive_RichTextBox.TabIndex = 0;
            this.Receive_RichTextBox.Text = "";
            // 
            // Send_Button
            // 
            this.Send_Button.Location = new System.Drawing.Point(590, 370);
            this.Send_Button.Name = "Send_Button";
            this.Send_Button.Size = new System.Drawing.Size(100, 47);
            this.Send_Button.TabIndex = 2;
            this.Send_Button.Text = "发送消息";
            this.Send_Button.UseVisualStyleBackColor = true;
            this.Send_Button.Click += new System.EventHandler(this.Send_Button_Click);
            // 
            // LinkServer_Button
            // 
            this.LinkServer_Button.Location = new System.Drawing.Point(590, 270);
            this.LinkServer_Button.Name = "LinkServer_Button";
            this.LinkServer_Button.Size = new System.Drawing.Size(100, 52);
            this.LinkServer_Button.TabIndex = 3;
            this.LinkServer_Button.Text = "连接服务器";
            this.LinkServer_Button.UseVisualStyleBackColor = true;
            this.LinkServer_Button.Click += new System.EventHandler(this.LinkServer_Button_Click);
            // 
            // ClientIP_TextBox
            // 
            this.ClientIP_TextBox.Location = new System.Drawing.Point(668, 49);
            this.ClientIP_TextBox.Name = "ClientIP_TextBox";
            this.ClientIP_TextBox.Size = new System.Drawing.Size(100, 23);
            this.ClientIP_TextBox.TabIndex = 4;
            this.ClientIP_TextBox.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(596, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "客户端IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(594, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "客户端端口";
            // 
            // ClientPort_TextBox
            // 
            this.ClientPort_TextBox.Location = new System.Drawing.Point(668, 89);
            this.ClientPort_TextBox.Name = "ClientPort_TextBox";
            this.ClientPort_TextBox.Size = new System.Drawing.Size(100, 23);
            this.ClientPort_TextBox.TabIndex = 7;
            this.ClientPort_TextBox.Text = "9600";
            // 
            // Send_RichTextBox
            // 
            this.Send_RichTextBox.Location = new System.Drawing.Point(71, 370);
            this.Send_RichTextBox.Name = "Send_RichTextBox";
            this.Send_RichTextBox.Size = new System.Drawing.Size(489, 47);
            this.Send_RichTextBox.TabIndex = 8;
            this.Send_RichTextBox.Text = "";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Send_RichTextBox);
            this.Controls.Add(this.ClientPort_TextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ClientIP_TextBox);
            this.Controls.Add(this.LinkServer_Button);
            this.Controls.Add(this.Send_Button);
            this.Controls.Add(this.Receive_RichTextBox);
            this.Name = "Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Client_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox Receive_RichTextBox;
        private System.Windows.Forms.Button Send_Button;
        private System.Windows.Forms.Button LinkServer_Button;
        private System.Windows.Forms.TextBox ClientIP_TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ClientPort_TextBox;
        private System.Windows.Forms.RichTextBox Send_RichTextBox;
    }
}

