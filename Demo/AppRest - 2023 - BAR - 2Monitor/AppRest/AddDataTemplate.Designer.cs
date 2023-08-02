namespace AppRest
{
    partial class AddDataTemplate
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
            this.FooterPN = new System.Windows.Forms.Panel();
            this.TxtFooter = new System.Windows.Forms.Label();
            this.buttonBackToManage = new System.Windows.Forms.Button();
            this.HeaderPN = new System.Windows.Forms.Panel();
            this.FooterPN.SuspendLayout();
            this.SuspendLayout();
            // 
            // FooterPN
            // 
            this.FooterPN.BackColor = System.Drawing.Color.RosyBrown;
            this.FooterPN.Controls.Add(this.TxtFooter);
            this.FooterPN.Location = new System.Drawing.Point(3, 864);
            this.FooterPN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FooterPN.Name = "FooterPN";
            this.FooterPN.Size = new System.Drawing.Size(1344, 30);
            this.FooterPN.TabIndex = 3;
            // 
            // TxtFooter
            // 
            this.TxtFooter.AutoSize = true;
            this.TxtFooter.Font = new System.Drawing.Font("Century Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.TxtFooter.Location = new System.Drawing.Point(583, 4);
            this.TxtFooter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TxtFooter.Name = "TxtFooter";
            this.TxtFooter.Size = new System.Drawing.Size(32, 24);
            this.TxtFooter.TabIndex = 0;
            this.TxtFooter.Text = "xx";
            // 
            // buttonBackToManage
            // 
            this.buttonBackToManage.BackColor = System.Drawing.Color.Transparent;
            this.buttonBackToManage.BackgroundImage = global::AppRest.Properties.Resources.Close_Window;
            this.buttonBackToManage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonBackToManage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonBackToManage.Font = new System.Drawing.Font("Century Gothic", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.buttonBackToManage.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.buttonBackToManage.Location = new System.Drawing.Point(1213, 1);
            this.buttonBackToManage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonBackToManage.Name = "buttonBackToManage";
            this.buttonBackToManage.Size = new System.Drawing.Size(135, 118);
            this.buttonBackToManage.TabIndex = 85;
            this.buttonBackToManage.TabStop = false;
            this.buttonBackToManage.UseVisualStyleBackColor = false;
            this.buttonBackToManage.Click += new System.EventHandler(this.buttonBackToManage_Click);
            // 
            // HeaderPN
            // 
            this.HeaderPN.BackColor = System.Drawing.Color.RosyBrown;
            this.HeaderPN.BackgroundImage = global::AppRest.Properties.Resources.Header_Login;
            this.HeaderPN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.HeaderPN.Location = new System.Drawing.Point(3, 1);
            this.HeaderPN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.HeaderPN.Name = "HeaderPN";
            this.HeaderPN.Size = new System.Drawing.Size(1212, 118);
            this.HeaderPN.TabIndex = 4;
            // 
            // AddDataTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1349, 894);
            this.Controls.Add(this.buttonBackToManage);
            this.Controls.Add(this.HeaderPN);
            this.Controls.Add(this.FooterPN);
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AddDataTemplate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddDataTemplate";
            this.FooterPN.ResumeLayout(false);
            this.FooterPN.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel FooterPN;
        private System.Windows.Forms.Label TxtFooter;
        private System.Windows.Forms.Button buttonBackToManage;
        private System.Windows.Forms.Panel HeaderPN;
    }
}