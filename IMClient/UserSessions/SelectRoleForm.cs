
// Type: IMClient.UserSessions.SelectRoleForm




using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace IMClient.UserSessions
{
    internal class SelectRoleForm : Form
    {
      private List<KeyValuePair<int, string>> _levels = new List<KeyValuePair<int, string>>();
      private IContainer components;
      private Button btCancel;
      private Button btOk;
      private ListBox lbRoles;
      private ComboBox _cbSecLevel;
      private Label label5;
      private Label label1;
      private Panel panel1;
      private Panel panel2;

      public SelectRoleForm() => this.InitializeComponent();

      internal static long SelectRole(
        RoleProperties[] roles,
        Dictionary<int, string> accessLevels,
        ref int accessLevel)
      {
        long num = -1;
        using (SelectRoleForm selectRoleForm = new SelectRoleForm())
        {
          selectRoleForm.SetData(roles, accessLevels, accessLevel);
          if (selectRoleForm.ShowDialog() == DialogResult.OK)
          {
            num = selectRoleForm.RoleID;
            accessLevel = selectRoleForm.AccessLevel;
          }
        }
        return num;
      }

      internal long RoleID
      {
        get
        {
          int selectedIndex = this.lbRoles.SelectedIndex;
          return selectedIndex >= 0 ? ((RoleProperties) this.lbRoles.Items[selectedIndex]).RoleID : -1L;
        }
      }

      public int AccessLevel
      {
        get
        {
          int selectedIndex = this._cbSecLevel.SelectedIndex;
          return selectedIndex >= 0 ? ((KeyValuePair<int, string>) this._cbSecLevel.Items[selectedIndex]).Key : -1;
        }
        set
        {
          int count = this._cbSecLevel.Items.Count;
          for (int index = 0; index < count; ++index)
          {
            if (((KeyValuePair<int, string>) this._cbSecLevel.Items[index]).Key == value)
            {
              this._cbSecLevel.SelectedIndex = index;
              break;
            }
          }
        }
      }

      private void SetData(RoleProperties[] roles, Dictionary<int, string> levels, int levelId)
      {
        this.lbRoles.DataSource = (object) roles;
        this.lbRoles.DisplayMember = "RoleName";
        this.ActiveControl = (Control) this.btOk;
        int val1 = -1;
        if (levels == null)
          return;
        foreach (KeyValuePair<int, string> level in levels)
        {
          this._levels.Add(level);
          val1 = Math.Max(val1, level.Key);
        }
        this._cbSecLevel.DisplayMember = "Value";
        this._cbSecLevel.DataSource = (object) new BindingList<KeyValuePair<int, string>>((IList<KeyValuePair<int, string>>) this._levels);
        this._cbSecLevel.DisplayMember = "Value";
        this.AccessLevel = levelId;
        if (this.AccessLevel != -1 || this._levels.Count <= 0)
          return;
        this.AccessLevel = val1;
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        this.btCancel = new Button();
        this.btOk = new Button();
        this.lbRoles = new ListBox();
        this._cbSecLevel = new ComboBox();
        this.label5 = new Label();
        this.label1 = new Label();
        this.panel1 = new Panel();
        this.panel2 = new Panel();
        this.panel1.SuspendLayout();
        this.panel2.SuspendLayout();
        this.SuspendLayout();
        this.btCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.btCancel.DialogResult = DialogResult.Cancel;
        this.btCancel.Location = new Point(264, 3);
        this.btCancel.Name = "btCancel";
        this.btCancel.Size = new Size(75, 23);
        this.btCancel.TabIndex = 0;
        this.btCancel.Text = "Отмена";
        this.btCancel.UseVisualStyleBackColor = true;
        this.btOk.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.btOk.DialogResult = DialogResult.OK;
        this.btOk.Location = new Point(183, 3);
        this.btOk.Name = "btOk";
        this.btOk.Size = new Size(75, 23);
        this.btOk.TabIndex = 1;
        this.btOk.Text = "OK";
        this.btOk.UseVisualStyleBackColor = true;
        this.lbRoles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        this.lbRoles.FormattingEnabled = true;
        this.lbRoles.Location = new Point(12, 28);
        this.lbRoles.Name = "lbRoles";
        this.lbRoles.Size = new Size(327, 121);
        this.lbRoles.TabIndex = 2;
        this._cbSecLevel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        this._cbSecLevel.DropDownStyle = ComboBoxStyle.DropDownList;
        this._cbSecLevel.ItemHeight = 13;
        this._cbSecLevel.Location = new Point(12, 184);
        this._cbSecLevel.Name = "_cbSecLevel";
        this._cbSecLevel.Size = new Size(327, 21);
        this._cbSecLevel.TabIndex = 4;
        this.label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        this.label5.ImeMode = ImeMode.NoControl;
        this.label5.Location = new Point(9, 165);
        this.label5.Name = "label5";
        this.label5.RightToLeft = RightToLeft.No;
        this.label5.Size = new Size(101, 16 /*0x10*/);
        this.label5.TabIndex = 17;
        this.label5.Text = "Уровень доступа:";
        this.label5.TextAlign = ContentAlignment.MiddleLeft;
        this.label1.ImeMode = ImeMode.NoControl;
        this.label1.Location = new Point(9, 9);
        this.label1.Name = "label1";
        this.label1.RightToLeft = RightToLeft.No;
        this.label1.Size = new Size(101, 16 /*0x10*/);
        this.label1.TabIndex = 18;
        this.label1.Text = "Роль:";
        this.label1.TextAlign = ContentAlignment.MiddleLeft;
        this.panel1.Controls.Add((Control) this.label1);
        this.panel1.Controls.Add((Control) this.lbRoles);
        this.panel1.Controls.Add((Control) this.label5);
        this.panel1.Controls.Add((Control) this._cbSecLevel);
        this.panel1.Dock = DockStyle.Fill;
        this.panel1.Location = new Point(0, 0);
        this.panel1.Name = "panel1";
        this.panel1.Size = new Size(354, 260);
        this.panel1.TabIndex = 19;
        this.panel2.Controls.Add((Control) this.btOk);
        this.panel2.Controls.Add((Control) this.btCancel);
        this.panel2.Dock = DockStyle.Bottom;
        this.panel2.Location = new Point(0, 225);
        this.panel2.Name = "panel2";
        this.panel2.Size = new Size(354, 35);
        this.panel2.TabIndex = 20;
        this.AcceptButton = (IButtonControl) this.btOk;
        this.AutoScaleDimensions = new SizeF(6f, 13f);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.CancelButton = (IButtonControl) this.btCancel;
        this.ClientSize = new Size(354, 260);
        this.ControlBox = false;
        this.Controls.Add((Control) this.panel2);
        this.Controls.Add((Control) this.panel1);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.Name = nameof (SelectRoleForm);
        this.ShowIcon = false;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "Выберите роль и уровень доступа для входа в систему";
        this.panel1.ResumeLayout(false);
        this.panel2.ResumeLayout(false);
        this.ResumeLayout(false);
      }
    }
}
