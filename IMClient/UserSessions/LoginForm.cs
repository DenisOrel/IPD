
// Type: IMClient.UserSessions.LoginForm




using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace IMClient.UserSessions
{
    internal class LoginForm : Form
    {
      private PictureBox pictureBox1;
      private Label _lbUser;
      private Label lbPassword;
      private Label label3;
      private Label label4;
      private TextBox _edUser;
      private TextBox _edPassword;
      private Button _btOK;
      private Button _btCancel;
      private ComboBox _cbServer;
      private ComboBox _cbRole;
      private Panel panel1;
      private ComboBox _cbUsers;
      private IContainer components;
      private bool _reReadRoles = true;
      private bool _reReadLevels = true;
      private List<KeyValuePair<int, string>> _levels = new List<KeyValuePair<int, string>>();
      private IUserSession _session;
      private ActingUserInfo _actingUserInfo;
      private bool _actingUserMode;
      private const string USER_ID = "userID";
      private const string USER_NAME = "userName";
      private const string ROLE_ID = "roleID";
      private const string ROLE_NAME = "roleName";
      private ComboBox _cbSecLevel;
      private Label label5;
      private Label lbLang;
      private Timer keyboardTimer;
      private Timer _timer;

      public LoginForm(string[] servers, IUserSession session)
        : this()
      {
        this._session = session;
        this._actingUserInfo = new ActingUserHelper().TryGetActingUserInfo();
        this._actingUserMode = this._actingUserInfo != null;
        if (this._actingUserMode)
          this.LoadActingUserInformation(this._actingUserInfo.UserID);
        else
          this._cbRole.DataSource = (object) new RoleProperties[0];
        this._cbServer.DataSource = (object) servers;
        this._cbRole.DisplayMember = nameof (RoleName);
        this._edPassword.PasswordChar = ClientConsts.PasswordChar;
      }

      public LoginForm()
      {
        this.InitializeComponent();
        this.FormClosed += new FormClosedEventHandler(this.LoginForm_FormClosed);
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        this.components = (IContainer) new System.ComponentModel.Container();
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (LoginForm));
        this.pictureBox1 = new PictureBox();
        this._lbUser = new Label();
        this._edUser = new TextBox();
        this._edPassword = new TextBox();
        this.lbPassword = new Label();
        this.label3 = new Label();
        this.label4 = new Label();
        this._btOK = new Button();
        this._btCancel = new Button();
        this._cbServer = new ComboBox();
        this._cbRole = new ComboBox();
        this.panel1 = new Panel();
        this._cbUsers = new ComboBox();
        this._timer = new Timer(this.components);
        this._cbSecLevel = new ComboBox();
        this.label5 = new Label();
        this.lbLang = new Label();
        this.keyboardTimer = new Timer(this.components);
        ((ISupportInitialize) this.pictureBox1).BeginInit();
        this.SuspendLayout();
        componentResourceManager.ApplyResources((object) this.pictureBox1, "pictureBox1");
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.TabStop = false;
        componentResourceManager.ApplyResources((object) this._lbUser, "_lbUser");
        this._lbUser.Name = "_lbUser";
        componentResourceManager.ApplyResources((object) this._edUser, "_edUser");
        this._edUser.Name = "_edUser";
        this._edUser.TextChanged += new EventHandler(this._edUser_TextChanged);
        this._edUser.Leave += new EventHandler(this._edUser_Leave);
        componentResourceManager.ApplyResources((object) this._edPassword, "_edPassword");
        this._edPassword.Name = "_edPassword";
        this._edPassword.TextChanged += new EventHandler(this._edPassword_TextChanged);
        componentResourceManager.ApplyResources((object) this.lbPassword, "lbPassword");
        this.lbPassword.Name = "lbPassword";
        componentResourceManager.ApplyResources((object) this.label3, "label3");
        this.label3.Name = "label3";
        componentResourceManager.ApplyResources((object) this.label4, "label4");
        this.label4.Name = "label4";
        componentResourceManager.ApplyResources((object) this._btOK, "_btOK");
        this._btOK.DialogResult = DialogResult.OK;
        this._btOK.Name = "_btOK";
        componentResourceManager.ApplyResources((object) this._btCancel, "_btCancel");
        this._btCancel.DialogResult = DialogResult.Cancel;
        this._btCancel.Name = "_btCancel";
        componentResourceManager.ApplyResources((object) this._cbServer, "_cbServer");
        this._cbServer.DropDownStyle = ComboBoxStyle.DropDownList;
        this._cbServer.Items.AddRange(new object[2]
        {
          (object) componentResourceManager.GetString("_cbServer.Items"),
          (object) componentResourceManager.GetString("_cbServer.Items1")
        });
        this._cbServer.Name = "_cbServer";
        this._cbServer.SelectedIndexChanged += new EventHandler(this._cbServer_SelectedIndexChanged);
        componentResourceManager.ApplyResources((object) this._cbRole, "_cbRole");
        this._cbRole.DropDownStyle = ComboBoxStyle.DropDownList;
        this._cbRole.Name = "_cbRole";
        this._cbRole.DropDown += new EventHandler(this._cbRole_DropDown);
        this._cbRole.SelectedIndexChanged += new EventHandler(this._cbRole_SelectedIndexChanged);
        this._cbRole.Enter += new EventHandler(this._cbRole_Enter);
        componentResourceManager.ApplyResources((object) this.panel1, "panel1");
        this.panel1.Name = "panel1";
        this.panel1.Paint += new PaintEventHandler(this.panel1_Paint);
        this._cbUsers.DropDownStyle = ComboBoxStyle.DropDownList;
        componentResourceManager.ApplyResources((object) this._cbUsers, "_cbUsers");
        this._cbUsers.Name = "_cbUsers";
        this._cbUsers.Sorted = true;
        this._cbUsers.SelectedIndexChanged += new EventHandler(this.cbUsers_SelectedIndexChanged);
        this._timer.Enabled = true;
        this._timer.Interval = 300000;
        this._timer.Tick += new EventHandler(this._timer_Tick);
        componentResourceManager.ApplyResources((object) this._cbSecLevel, "_cbSecLevel");
        this._cbSecLevel.DropDownStyle = ComboBoxStyle.DropDownList;
        this._cbSecLevel.Name = "_cbSecLevel";
        this._cbSecLevel.DropDown += new EventHandler(this._cbSecLevel_DropDown);
        this._cbSecLevel.SelectedIndexChanged += new EventHandler(this._cbRole_SelectedIndexChanged);
        this._cbSecLevel.Enter += new EventHandler(this._cbSecLevel_Enter);
        componentResourceManager.ApplyResources((object) this.label5, "label5");
        this.label5.Name = "label5";
        componentResourceManager.ApplyResources((object) this.lbLang, "lbLang");
        this.lbLang.BackColor = SystemColors.ActiveCaption;
        this.lbLang.ForeColor = Color.White;
        this.lbLang.Name = "lbLang";
        this.keyboardTimer.Interval = 250;
        this.keyboardTimer.Tick += new EventHandler(this.keyboardTimer_Tick);
        this.AcceptButton = (IButtonControl) this._btOK;
        componentResourceManager.ApplyResources((object) this, "$this");
        this.CancelButton = (IButtonControl) this._btCancel;
        this.Controls.Add((Control) this.lbLang);
        this.Controls.Add((Control) this._cbSecLevel);
        this.Controls.Add((Control) this.label5);
        this.Controls.Add((Control) this.panel1);
        this.Controls.Add((Control) this._cbRole);
        this.Controls.Add((Control) this._cbServer);
        this.Controls.Add((Control) this._btCancel);
        this.Controls.Add((Control) this._btOK);
        this.Controls.Add((Control) this.label4);
        this.Controls.Add((Control) this.label3);
        this.Controls.Add((Control) this._edPassword);
        this.Controls.Add((Control) this.lbPassword);
        this.Controls.Add((Control) this._edUser);
        this.Controls.Add((Control) this._lbUser);
        this.Controls.Add((Control) this.pictureBox1);
        this.Controls.Add((Control) this._cbUsers);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = nameof (LoginForm);
        this.SizeGripStyle = SizeGripStyle.Hide;
        this.Load += new EventHandler(this.LoginForm_Load);
        this.Shown += new EventHandler(this.LoginForm_Shown);
        this.Layout += new LayoutEventHandler(this.LoginForm_Layout);
        this.Resize += new EventHandler(this.LoginForm_Resize);
        ((ISupportInitialize) this.pictureBox1).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
      }

      private void _edPassword_TextChanged(object sender, EventArgs e)
      {
        this.ResetTimer();
        this._btOK.Enabled = this._edUser.Text.Length > 0;
      }

      public bool ActingUserMode => this._actingUserMode;

      public string UserName
      {
        get
        {
          if (!this._actingUserMode)
            return this._edUser.Text;
          return !(this._cbUsers.SelectedItem is MyElement selectedItem) ? string.Empty : selectedItem.Caption;
        }
      }

      public long UserID
      {
        get
        {
          return this._actingUserMode ? Convert.ToInt64((this._cbUsers.SelectedItem as MyElement).Value) : 0L;
        }
      }

      public string Password
      {
        get => this._edPassword.Text;
        set => this._edPassword.Text = value;
      }

      public string RoleName => this._cbRole.Text;

      public long RoleID
      {
        get
        {
          int selectedIndex = this._cbRole.SelectedIndex;
          return selectedIndex >= 0 ? ((RoleProperties) this._cbRole.Items[selectedIndex]).RoleID : -1L;
        }
        set
        {
          int count = this._cbRole.Items.Count;
          for (int index = 0; index < count; ++index)
          {
            if (((RoleProperties) this._cbRole.Items[index]).RoleID == value)
            {
              this._cbRole.SelectedIndex = index;
              return;
            }
          }
          this._cbRole.SelectedIndex = -1;
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
              return;
            }
          }
          this._cbSecLevel.SelectedIndex = -1;
        }
      }

      public IConfigurationManager LoginConfigurationManager { get; set; }

      private void LoginForm_Load(object sender, EventArgs e)
      {
        this.BringToFront();
        if (this._actingUserMode || this.LoginConfigurationManager == null)
          return;
        IConfiguration configuration = this.LoginConfigurationManager.Open("Logging");
        if (configuration == null)
          return;
        if (configuration.HasProperty("UserName"))
        {
          this._edUser.Text = configuration.GetProperty("UserName");
          this._edPassword.Text = string.Empty;
          this.ActiveControl = (Control) this._edPassword;
          this._cbRole_DropDown((object) this._cbRole, EventArgs.Empty);
          this._edPassword.Focus();
        }
        if (configuration.HasProperty("RoleName"))
        {
          string property = configuration.GetProperty("RoleName");
          RoleProperties[] dataSource = (RoleProperties[]) this._cbRole.DataSource;
          int length = dataSource.Length;
          for (int index = 0; index < length; ++index)
          {
            if (dataSource[index].RoleName == property)
            {
              this._cbRole.SelectedIndex = index;
              break;
            }
          }
        }
        if (configuration.HasProperty("AccessLevel"))
        {
          string property = configuration.GetProperty("AccessLevel");
          int result = -1;
          if (int.TryParse(property, out result))
            this.AccessLevel = result;
        }
        if (!configuration.HasProperty("Location"))
          return;
        Point lLocation = this.Location;
        string property1 = configuration.GetProperty("Location");
        try
        {
          lLocation = (Point) TypeDescriptor.GetConverter(typeof (Point)).ConvertFrom((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) property1.Replace(";", ","));
        }
        catch
        {
        }
        Point point = FormStorage.ValidateLocation(lLocation);
        if (this.Location.Equals((object) point))
          return;
        this.Location = point;
      }

      private void panel1_Paint(object sender, PaintEventArgs e)
      {
        Rectangle displayRectangle = this.panel1.DisplayRectangle;
        Bitmap image = (Bitmap) this.pictureBox1.Image;
        ControlPaint.Dark(image.GetPixel(image.Width - 1, 1), 0.3f);
        using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(displayRectangle, Color.FromArgb(41, 71, 219), Color.FromArgb(27, 50, 160 /*0xA0*/), LinearGradientMode.Horizontal))
          e.Graphics.FillRectangle((Brush) linearGradientBrush, displayRectangle);
        e.Graphics.DrawLine(Pens.White, displayRectangle.Left, displayRectangle.Height - 2, displayRectangle.Right - 1, displayRectangle.Height - 2);
        e.Graphics.DrawLine(Pens.White, displayRectangle.Left, displayRectangle.Height - 1, displayRectangle.Right - 1, displayRectangle.Height - 1);
      }

      private void LoginForm_Layout(object sender, LayoutEventArgs e)
      {
        Rectangle displayRectangle = this.DisplayRectangle;
        this.panel1.Height = this.pictureBox1.Height;
        this.panel1.Top = this.pictureBox1.Top;
        this.panel1.Left = this.pictureBox1.Right;
        this.panel1.Width = displayRectangle.Width - this.panel1.Left - 1;
      }

      private void LoginForm_Resize(object sender, EventArgs e)
      {
        this.LoginForm_Layout((object) null, (LayoutEventArgs) null);
      }

      private void LoadActingUserInformation(long actingUserId)
      {
        List<ActingUserLoginSettings> userLoginSettings = this._session.GetActingUserLoginSettings(actingUserId);
        for (int index = 0; index < userLoginSettings.Count; ++index)
        {
          foreach (KeyValuePair<long, string> user in userLoginSettings[index].Users)
          {
            MyElement myElement = new MyElement();
            myElement.Value = (object) user.Key;
            myElement.Caption = user.Value;
            if (userLoginSettings[index].RoleID > 0L)
              myElement.Tag = (object) new RoleProperties(userLoginSettings[index].RoleID, userLoginSettings[index].RoleName);
            this._cbUsers.Items.Add((object) myElement);
          }
        }
        this._cbUsers.Visible = true;
        if (this._cbUsers.Items.Count > 0)
          this._cbUsers.SelectedIndex = 0;
        else
          this._btOK.Enabled = false;
        int num = this._cbUsers.Top - this._lbUser.Top;
        this._edPassword.Visible = this._edUser.Visible = this.lbPassword.Visible = false;
        this.Height -= this._cbSecLevel.Top - this._cbRole.Top;
        this._lbUser.Top = this._cbUsers.Top - num;
      }

      private void cbUsers_SelectedIndexChanged(object sender, EventArgs e)
      {
        MyElement selectedItem = this._cbUsers.SelectedItem as MyElement;
        long int64 = Convert.ToInt64(selectedItem.Value);
        string caption = selectedItem.Caption;
        RoleProperties roleProperties = (RoleProperties) null;
        if (selectedItem.Tag is RoleProperties)
          roleProperties = selectedItem.Tag as RoleProperties;
        if (!string.IsNullOrEmpty(caption))
        {
          if (roleProperties != null)
          {
            RoleProperties[] rolePropertiesArray = new RoleProperties[1]
            {
              roleProperties
            };
            this._cbRole.DataSource = (object) rolePropertiesArray;
            this.RoleID = rolePropertiesArray[0].RoleID;
          }
          else
          {
            try
            {
              RoleProperties[] rolesList = this._session.GetRolesList(int64);
              if (rolesList != null)
              {
                if (rolesList.Length != 0)
                {
                  long roleId = this.RoleID;
                  this._cbRole.DataSource = (object) rolesList;
                  this.RoleID = roleId;
                  if (this.RoleID == -1L)
                    this.RoleID = rolesList[0].RoleID;
                }
              }
            }
            catch
            {
            }
          }
          this._reReadLevels = true;
          this.RefreshLevelsList(int64);
        }
        if (this._cbRole.Items.Count > 0)
          this._cbRole.SelectedIndex = 0;
        this._btOK.Enabled = this._cbRole.Items.Count > 0;
      }

      private void _timer_Tick(object sender, EventArgs e) => this._edPassword.Text = string.Empty;

      private void _edUser_TextChanged(object sender, EventArgs e)
      {
        this.ResetTimer();
        this._reReadRoles = true;
        this._reReadLevels = true;
      }

      private void ResetTimer()
      {
        this._timer.Stop();
        this._timer.Start();
      }

      private void _cbRole_SelectedIndexChanged(object sender, EventArgs e) => this.ResetTimer();

      private void _cbServer_SelectedIndexChanged(object sender, EventArgs e) => this.ResetTimer();

      private void _cbRole_DropDown(object sender, EventArgs e) => this.RefreshRolesList();

      private void _cbSecLevel_DropDown(object sender, EventArgs e) => this.RefreshLevelsList(0L);

      private void RefreshRolesList()
      {
        if (this._actingUserMode || !this._reReadRoles)
          return;
        if (string.IsNullOrEmpty(this.UserName))
          return;
        try
        {
          RoleProperties[] rolesList = this._session.GetRolesList(this.UserName);
          if (rolesList != null)
          {
            long roleId = this.RoleID;
            this._cbRole.DataSource = (object) rolesList;
            this.RoleID = roleId;
            if (this.RoleID == -1L)
            {
              if (rolesList.Length != 0)
                this.RoleID = rolesList[0].RoleID;
            }
          }
        }
        catch
        {
        }
        this._reReadRoles = false;
      }

      private void RefreshLevelsList(long userID)
      {
        if (!this._reReadLevels)
          return;
        if (string.IsNullOrEmpty(this.UserName))
          return;
        try
        {
          Dictionary<int, string> dictionary = userID <= 0L ? this._session.GetSecurityLevels(this.UserName) : this._session.GetSecurityLevels(userID);
          int accessLevel = this.AccessLevel;
          this._levels.Clear();
          int val1 = -1;
          if (dictionary != null)
          {
            foreach (KeyValuePair<int, string> keyValuePair in dictionary)
            {
              this._levels.Add(keyValuePair);
              val1 = Math.Max(val1, keyValuePair.Key);
            }
            this._cbSecLevel.DisplayMember = "Value";
            this._cbSecLevel.DataSource = (object) new BindingList<KeyValuePair<int, string>>((IList<KeyValuePair<int, string>>) this._levels);
            this._cbSecLevel.DisplayMember = "Value";
            this.AccessLevel = accessLevel;
            if (this.AccessLevel == -1)
            {
              if (this._levels.Count > 0)
                this.AccessLevel = val1;
            }
          }
        }
        catch
        {
        }
        this._reReadLevels = false;
      }

      private void _cbRole_Enter(object sender, EventArgs e) => this.RefreshRolesList();

      private void _cbSecLevel_Enter(object sender, EventArgs e) => this.RefreshLevelsList(0L);

      private void _edUser_Leave(object sender, EventArgs e)
      {
        this.RefreshRolesList();
        this.RefreshLevelsList(0L);
      }

      private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
      {
        if (this.DialogResult != DialogResult.OK)
          return;
        this.RefreshRolesList();
        this.RefreshLevelsList(0L);
      }

      private void LoginForm_Shown(object sender, EventArgs e)
      {
        if (this.ActiveControl != null)
          this.ActiveControl.Select();
        this.keyboardTimer.Enabled = true;
      }

      [DllImport("user32.dll")]
      private static extern IntPtr GetForegroundWindow();

      [DllImport("user32.dll")]
      private static extern uint GetWindowThreadProcessId(IntPtr hwnd, IntPtr proccess);

      [DllImport("user32.dll")]
      private static extern IntPtr GetKeyboardLayout(uint thread);

      public CultureInfo GetCurrentKeyboardLayout()
      {
        try
        {
          return new CultureInfo(LoginForm.GetKeyboardLayout(LoginForm.GetWindowThreadProcessId(this.Handle, IntPtr.Zero)).ToInt32() & (int) ushort.MaxValue);
        }
        catch (Exception ex)
        {
          return new CultureInfo(1033);
        }
      }

      private void keyboardTimer_Tick(object sender, EventArgs e)
      {
        this.lbLang.Text = this.GetCurrentKeyboardLayout().TwoLetterISOLanguageName.ToUpper();
      }
    }
}
