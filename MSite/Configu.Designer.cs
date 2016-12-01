using System.Windows.Forms;

namespace MSite
{
    partial class Configu
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
            this.pr = new System.Windows.Forms.ComboBox();
            this.value = new System.Windows.Forms.TextBox();
            this.ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.apply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pr
            // 
            this.pr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pr.FormattingEnabled = true;
            this.pr.Location = new System.Drawing.Point(12, 14);
            this.pr.Name = "pr";
            this.pr.Size = new System.Drawing.Size(380, 21);
            this.pr.TabIndex = 0;
            this.pr.SelectedIndexChanged += new System.EventHandler(this.pr_SelectedIndexChanged);
            // 
            // value
            // 
            this.value.Location = new System.Drawing.Point(12, 41);
            this.value.Name = "value";
            this.value.Size = new System.Drawing.Size(380, 20);
            this.value.TabIndex = 1;
            this.value.TextChanged += new System.EventHandler(this.value_TextChanged);
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(155, 67);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 23);
            this.ok.TabIndex = 3;
            this.ok.Text = "OK";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(236, 67);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 4;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // apply
            // 
            this.apply.Location = new System.Drawing.Point(317, 67);
            this.apply.Name = "apply";
            this.apply.Size = new System.Drawing.Size(75, 23);
            this.apply.TabIndex = 5;
            this.apply.Text = "Apply";
            this.apply.UseVisualStyleBackColor = true;
            this.apply.Click += new System.EventHandler(this.apply_Click);
            // 
            // Configu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 104);
            this.Controls.Add(this.apply);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.value);
            this.Controls.Add(this.pr);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Configu";
            this.Text = "MSite Configuration";
            this.Load += new System.EventHandler(this.Configu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ComboBox pr;
        private System.Windows.Forms.TextBox value;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button apply;
    }
}