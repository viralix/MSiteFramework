namespace MSite
{
    partial class Main
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.ConsoleText = new System.Windows.Forms.TextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.Config = new System.Windows.Forms.Button();
            this.restart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ConsoleText
            // 
            this.ConsoleText.Location = new System.Drawing.Point(12, 50);
            this.ConsoleText.Multiline = true;
            this.ConsoleText.Name = "ConsoleText";
            this.ConsoleText.ReadOnly = true;
            this.ConsoleText.Size = new System.Drawing.Size(612, 365);
            this.ConsoleText.TabIndex = 0;
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(12, 12);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(207, 32);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // Config
            // 
            this.Config.Location = new System.Drawing.Point(438, 12);
            this.Config.Name = "Config";
            this.Config.Size = new System.Drawing.Size(186, 32);
            this.Config.TabIndex = 2;
            this.Config.Text = "Configure";
            this.Config.UseVisualStyleBackColor = true;
            // 
            // restart
            // 
            this.restart.Location = new System.Drawing.Point(225, 12);
            this.restart.Name = "restart";
            this.restart.Size = new System.Drawing.Size(207, 32);
            this.restart.TabIndex = 3;
            this.restart.Text = "Restart";
            this.restart.UseVisualStyleBackColor = true;
            this.restart.Click += new System.EventHandler(this.restart_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 427);
            this.Controls.Add(this.restart);
            this.Controls.Add(this.Config);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.ConsoleText);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MSite Control Panel";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox ConsoleText;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button Config;
        private System.Windows.Forms.Button restart;
    }
}