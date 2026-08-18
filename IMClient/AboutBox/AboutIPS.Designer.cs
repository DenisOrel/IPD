using System.Drawing;
using System.Windows.Forms;

namespace IMClient.AboutBox
{
    partial class AboutIPS
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
            System.ComponentModel.ComponentResourceManager componentResourceManager = new System.ComponentModel.ComponentResourceManager(typeof(AboutIPS));
            this.labelTitle = new Label();
            this.labelVersion = new Label();
            this.labelCopyright = new Label();
            this.labelDateTimeHint = new Label();
            this.labelPlugins = new Label();
            this.listPlugins = new ListView();
            this.columnPlugin = new ColumnHeader();
            this.columnVersion = new ColumnHeader();
            this.okButton = new Button();
            this.labelDateTime = new Label();
            this.SuspendLayout();
            
            // 
            // labelTitle
            // 
            this.labelTitle.BackColor = Color.Transparent;
            componentResourceManager.ApplyResources((object) this.labelTitle, "labelTitle");
            this.labelTitle.ForeColor = Color.MediumBlue;
            this.labelTitle.Name = "labelTitle";
            
            // 
            // labelVersion
            // 
            componentResourceManager.ApplyResources((object) this.labelVersion, "labelVersion");
            this.labelVersion.BackColor = Color.Transparent;
            this.labelVersion.ForeColor = Color.Black;
            this.labelVersion.Name = "labelVersion";
            
            // 
            // labelCopyright
            // 
            componentResourceManager.ApplyResources((object) this.labelCopyright, "labelCopyright");
            this.labelCopyright.BackColor = Color.Transparent;
            this.labelCopyright.ForeColor = Color.Black;
            this.labelCopyright.Name = "labelCopyright";
            
            // 
            // labelDateTimeHint
            // 
            componentResourceManager.ApplyResources((object) this.labelDateTimeHint, "labelDateTimeHint");
            this.labelDateTimeHint.BackColor = Color.Transparent;
            this.labelDateTimeHint.ForeColor = Color.Black;
            this.labelDateTimeHint.Name = "labelDateTimeHint";
            
            // 
            // labelPlugins
            // 
            componentResourceManager.ApplyResources((object) this.labelPlugins, "labelPlugins");
            this.labelPlugins.BackColor = Color.Transparent;
            this.labelPlugins.ForeColor = Color.Black;
            this.labelPlugins.Name = "labelPlugins";
            
            // 
            // listPlugins
            // 
            this.listPlugins.Columns.AddRange(new ColumnHeader[2]
            {
                this.columnPlugin,
                this.columnVersion
            });
            this.listPlugins.FullRowSelect = true;
            componentResourceManager.ApplyResources((object) this.listPlugins, "listPlugins");
            this.listPlugins.Name = "listPlugins";
            this.listPlugins.UseCompatibleStateImageBehavior = false;
            this.listPlugins.View = View.Details;
            
            // 
            // columnPlugin
            // 
            componentResourceManager.ApplyResources((object) this.columnPlugin, "columnPlugin");
            
            // 
            // columnVersion
            // 
            componentResourceManager.ApplyResources((object) this.columnVersion, "columnVersion");
            
            // 
            // okButton
            // 
            this.okButton.DialogResult = DialogResult.Cancel;
            componentResourceManager.ApplyResources((object) this.okButton, "okButton");
            this.okButton.Name = "okButton";
            
            // 
            // labelDateTime
            // 
            componentResourceManager.ApplyResources((object) this.labelDateTime, "labelDateTime");
            this.labelDateTime.BackColor = Color.Transparent;
            this.labelDateTime.ForeColor = Color.White;
            this.labelDateTime.Name = "labelDateTime";
            
            // 
            // AboutIPS
            // 
            this.AcceptButton = (IButtonControl) this.okButton;
            componentResourceManager.ApplyResources((object) this, "$this");
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = (IButtonControl) this.okButton;
            this.Controls.Add((Control) this.labelDateTime);
            this.Controls.Add((Control) this.okButton);
            this.Controls.Add((Control) this.listPlugins);
            this.Controls.Add((Control) this.labelPlugins);
            this.Controls.Add((Control) this.labelDateTimeHint);
            this.Controls.Add((Control) this.labelCopyright);
            this.Controls.Add((Control) this.labelVersion);
            this.Controls.Add((Control) this.labelTitle);
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = nameof(AboutIPS);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = SizeGripStyle.Hide;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Label labelTitle;
        private Label labelVersion;
        private Label labelCopyright;
        private Label labelDateTimeHint;
        private Label labelPlugins;
        private ListView listPlugins;
        private ColumnHeader columnPlugin;
        private ColumnHeader columnVersion;
        private Button okButton;
        private Label labelDateTime;
    }
}