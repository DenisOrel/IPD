// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AutoNotification.SpecificAdresseeCntrl
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Interfaces.Workflow.AutoNotification;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client.AutoNotification;

public class SpecificAdresseeCntrl : UserControl, ICanSaveNotifSettings
{
  private AutoNotificationSettings _notifSettings;
  private List<long> _usersID = new List<long>();
  private List<long> _groupsID = new List<long>();
  private List<long> _rolesID = new List<long>();
  private string _email = string.Empty;
  private bool _isChanged;
  private IContainer components;
  private GroupBox gbSpecAdressee;
  private CheckBox cbEmail;
  private CheckBox cbSpecificGroupUsers;
  private CheckBox cbSpecificRoleUsers;
  private CheckBox cbUsers;
  private Panel specificAdrChoosePanel;
  private GroupBox gbEMail;
  private TextBox tbEmail;
  private GroupBox gbGroup;
  private ListView lvGroups;
  private Intermech.Bars.ToolBar toolBar2;
  private ButtonItem btnAddGroup;
  private ButtonItem btnDeleteGroup;
  private GroupBox gbRoles;
  private ListView lvRoles;
  private Intermech.Bars.ToolBar toolBar1;
  private ButtonItem btnAddRole;
  private ButtonItem btnDeleteRole;
  private GroupBox gbUsers;
  private ListView lvUsers;
  private Intermech.Bars.ToolBar tbUsers;
  private ButtonItem btnAddUser;
  private ButtonItem btnDeleteUser;
  private ColumnHeader userName;
  private ColumnHeader groups;
  private ColumnHeader role;

  public event EventHandler Modified;

  public bool IsChanged
  {
    get => this._isChanged;
    private set
    {
      this._isChanged = value;
      EventHandler modified = this.Modified;
      if (!value || modified == null)
        return;
      modified((object) this, (EventArgs) null);
    }
  }

  public SpecificAdresseeCntrl(AutoNotificationSettings notifSettings)
  {
    this._notifSettings = notifSettings;
    this.InitializeComponent();
    this.lvUsers.SmallImageList = Statics.IconSrv == null ? (this.lvGroups.SmallImageList = this.lvRoles.SmallImageList = (ImageList) null) : (this.lvGroups.SmallImageList = this.lvRoles.SmallImageList = Statics.IconSrv.ImageList);
    this.LoadInfoFromSettings();
    this.UpdateControl();
  }

  private void cbUsers_CheckedChanged(object sender, EventArgs e)
  {
    if (this.cbUsers.Checked)
      this.gbUsers.Enabled = true;
    else
      this.gbUsers.Enabled = false;
    this.IsChanged = true;
  }

  private void cbSpecificRoleUsers_CheckedChanged(object sender, EventArgs e)
  {
    if (this.cbSpecificRoleUsers.Checked)
      this.gbRoles.Enabled = true;
    else
      this.gbRoles.Enabled = false;
    this.IsChanged = true;
  }

  private void cbSpecificGroupUsers_CheckedChanged(object sender, EventArgs e)
  {
    if (this.cbSpecificGroupUsers.Checked)
      this.gbGroup.Enabled = true;
    else
      this.gbGroup.Enabled = false;
    this.IsChanged = true;
  }

  private void cbEmail_CheckedChanged(object sender, EventArgs e)
  {
    if (this.cbEmail.Checked)
      this.gbEMail.Enabled = true;
    else
      this.gbEMail.Enabled = false;
    this.IsChanged = true;
  }

  private void btnAddUser_Click(object sender, EventArgs e)
  {
    foreach (long num in this.GetNewUserIDsFromSelectorWindow())
    {
      if (!this._usersID.Contains(num))
        this._usersID.Add(num);
    }
    this.UpdateUsersListView();
    this.IsChanged = true;
  }

  private void lvUsers_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lvUsers.SelectedItems.Count == 0)
      this.btnDeleteUser.Enabled = false;
    else
      this.btnDeleteUser.Enabled = true;
  }

  private void btnDeleteUser_Click(object sender, EventArgs e)
  {
    if (this.lvUsers.SelectedItems.Count == 0)
      return;
    foreach (ListViewItem selectedItem in this.lvUsers.SelectedItems)
      this._usersID.Remove(Convert.ToInt64(selectedItem.Tag));
    this.UpdateUsersListView();
    this.IsChanged = true;
  }

  private void btnAddRole_Click(object sender, EventArgs e)
  {
    foreach (long num in this.GetNewRoleIDsFromSelectorWindow())
    {
      if (!this._rolesID.Contains(num))
        this._rolesID.Add(num);
    }
    this.UpdateRolesListView();
    this.IsChanged = true;
  }

  private void btnDeleteRole_Click(object sender, EventArgs e)
  {
    if (this.lvRoles.SelectedItems.Count == 0)
      return;
    foreach (ListViewItem selectedItem in this.lvRoles.SelectedItems)
      this._rolesID.Remove(Convert.ToInt64(selectedItem.Tag));
    this.UpdateRolesListView();
    this.IsChanged = true;
  }

  private void lvRoles_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lvRoles.SelectedItems.Count == 0)
      this.btnDeleteRole.Enabled = false;
    else
      this.btnDeleteRole.Enabled = true;
  }

  private void btnAddGroup_Click(object sender, EventArgs e)
  {
    foreach (long num in this.GetNewGroupIDsFromSelectorWindow())
    {
      if (!this._groupsID.Contains(num))
        this._groupsID.Add(num);
    }
    this.UpdateGroupsListView();
    this.IsChanged = true;
  }

  private void btnDeleteGroup_Click(object sender, EventArgs e)
  {
    if (this.lvGroups.SelectedItems.Count == 0)
      return;
    foreach (ListViewItem selectedItem in this.lvGroups.SelectedItems)
      this._groupsID.Remove(Convert.ToInt64(selectedItem.Tag));
    this.UpdateGroupsListView();
    this.IsChanged = true;
  }

  private void lvGroups_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lvGroups.SelectedItems.Count == 0)
      this.btnDeleteGroup.Enabled = false;
    else
      this.btnDeleteGroup.Enabled = true;
  }

  private void tbEmail_TextChanged(object sender, EventArgs e) => this.IsChanged = true;

  private void lvUsers_Leave(object sender, EventArgs e) => this.btnDeleteUser.Enabled = false;

  private void lvRoles_Leave(object sender, EventArgs e) => this.btnDeleteRole.Enabled = false;

  private void lvGroups_Leave(object sender, EventArgs e) => this.btnDeleteGroup.Enabled = false;

  private void LoadInfoFromSettings()
  {
    if (!(this._notifSettings.Adressee is SpecificAdressee adressee))
      return;
    this._usersID = new List<long>((IEnumerable<long>) adressee.UsersIDs);
    this._groupsID = new List<long>((IEnumerable<long>) adressee.GroupsIDs);
    this._rolesID = new List<long>((IEnumerable<long>) adressee.RolesIDs);
    this._email = adressee.Emails;
  }

  private void UpdateControl()
  {
    this.UpdateGroupsListView();
    this.UpdateRolesListView();
    this.UpdateUsersListView();
    this.tbEmail.Text = this._email;
    if (this.lvUsers.Items.Count > 0)
      this.cbUsers.Checked = true;
    if (this.lvGroups.Items.Count > 0)
      this.cbSpecificGroupUsers.Checked = true;
    if (this.lvRoles.Items.Count > 0)
      this.cbSpecificRoleUsers.Checked = true;
    if (this.tbEmail.Text != string.Empty)
      this.cbEmail.Checked = true;
    if (this.lvUsers.Items.Count == 0)
      this.btnDeleteUser.Enabled = false;
    if (this.lvRoles.Items.Count == 0)
      this.btnDeleteRole.Enabled = false;
    if (this.lvGroups.Items.Count != 0)
      return;
    this.btnDeleteGroup.Enabled = false;
  }

  private List<long> GetNewUserIDsFromSelectorWindow()
  {
    List<long> fromSelectorWindow = new List<long>();
    Intermech.Navigator.DBObjectTypes.Descriptor rootDescriptor = new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID(new Guid("cad00002-306c-11d8-b4e9-00304f19f545")));
    object[] objArray = SelectionWindow.Select(LocalizationHolder.rm.GetString("Workflow.Client_76"), (IDescriptor) rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableSelectFromTree);
    if (objArray == null || objArray.Length == 0)
      return fromSelectorWindow;
    for (int index = 0; index < objArray.Length; ++index)
    {
      if (objArray[index] is IDBTypedObjectID dbTypedObjectId && !fromSelectorWindow.Contains(dbTypedObjectId.ObjectID))
        fromSelectorWindow.Add(dbTypedObjectId.ObjectID);
    }
    return fromSelectorWindow;
  }

  private List<long> GetNewRoleIDsFromSelectorWindow()
  {
    List<long> fromSelectorWindow = new List<long>();
    Intermech.Navigator.DBObjectTypes.Descriptor rootDescriptor = new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID(new Guid("cad00007-306c-11d8-b4e9-00304f19f545")));
    object[] objArray = SelectionWindow.Select(LocalizationHolder.rm.GetString("Workflow.Client_106"), (IDescriptor) rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableSelectFromTree);
    if (objArray == null || objArray.Length == 0)
      return fromSelectorWindow;
    for (int index = 0; index < objArray.Length; ++index)
    {
      if (objArray[index] is IDBTypedObjectID dbTypedObjectId && !fromSelectorWindow.Contains(dbTypedObjectId.ObjectID))
        fromSelectorWindow.Add(dbTypedObjectId.ObjectID);
    }
    return fromSelectorWindow;
  }

  private List<long> GetNewGroupIDsFromSelectorWindow()
  {
    List<long> fromSelectorWindow = new List<long>();
    Intermech.Navigator.DBObjectTypes.Descriptor rootDescriptor = new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID(new Guid("cad00003-306c-11d8-b4e9-00304f19f545")));
    object[] objArray = SelectionWindow.Select(LocalizationHolder.rm.GetString("Workflow.Client_107"), (IDescriptor) rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableSelectFromTree);
    if (objArray == null || objArray.Length == 0)
      return fromSelectorWindow;
    for (int index = 0; index < objArray.Length; ++index)
    {
      if (objArray[index] is IDBTypedObjectID dbTypedObjectId && !fromSelectorWindow.Contains(dbTypedObjectId.ObjectID))
        fromSelectorWindow.Add(dbTypedObjectId.ObjectID);
    }
    return fromSelectorWindow;
  }

  private void UpdateUsersListView()
  {
    int num = -1;
    if (Statics.IconSrv != null)
      num = Statics.IconSrv.IndexOf(4, MetaDataHelper.GetObjectTypeID(new Guid("cad00002-306c-11d8-b4e9-00304f19f545")));
    this.lvUsers.BeginUpdate();
    this.lvUsers.Items.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long objectID in this._usersID)
      {
        ListViewItem listViewItem = new ListViewItem(sessionKeeper.Session.GetObjectInfo(objectID).Caption)
        {
          Tag = (object) objectID
        };
        if (num != -1)
          listViewItem.ImageIndex = num;
        this.lvUsers.Items.Add(listViewItem);
      }
    }
    this.lvUsers.EndUpdate();
    this.lvUsers.Refresh();
    if (this._usersID.Count == 0 || this.lvUsers.SelectedItems.Count == 0)
      this.btnDeleteUser.Enabled = false;
    else
      this.btnDeleteUser.Enabled = true;
  }

  private void UpdateRolesListView()
  {
    int num = -1;
    if (Statics.IconSrv != null)
      num = Statics.IconSrv.IndexOf(4, MetaDataHelper.GetObjectTypeID(new Guid("cad00007-306c-11d8-b4e9-00304f19f545")));
    this.lvRoles.BeginUpdate();
    this.lvRoles.Items.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long objectID in this._rolesID)
      {
        ListViewItem listViewItem = new ListViewItem(sessionKeeper.Session.GetObjectInfo(objectID).Caption)
        {
          Tag = (object) objectID
        };
        if (num != -1)
          listViewItem.ImageIndex = num;
        this.lvRoles.Items.Add(listViewItem);
      }
    }
    this.lvRoles.EndUpdate();
    this.lvRoles.Refresh();
    if (this._rolesID.Count == 0 || this.lvRoles.SelectedItems.Count == 0)
      this.btnDeleteRole.Enabled = false;
    else
      this.btnDeleteRole.Enabled = true;
  }

  private void UpdateGroupsListView()
  {
    int num = -1;
    if (Statics.IconSrv != null)
      num = Statics.IconSrv.IndexOf(4, MetaDataHelper.GetObjectTypeID(new Guid("cad00003-306c-11d8-b4e9-00304f19f545")));
    this.lvGroups.BeginUpdate();
    this.lvGroups.Items.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long objectID in this._groupsID)
      {
        ListViewItem listViewItem = new ListViewItem(sessionKeeper.Session.GetObjectInfo(objectID).Caption)
        {
          Tag = (object) objectID
        };
        if (num != -1)
          listViewItem.ImageIndex = num;
        this.lvGroups.Items.Add(listViewItem);
      }
    }
    this.lvGroups.EndUpdate();
    this.lvGroups.Refresh();
    if (this._groupsID.Count == 0 || this.lvGroups.SelectedItems.Count == 0)
      this.btnDeleteGroup.Enabled = false;
    else
      this.btnDeleteGroup.Enabled = true;
  }

  public void SaveSettings()
  {
    this._email = this.tbEmail.Text;
    List<long> usersID = new List<long>();
    List<long> rolesID = new List<long>();
    List<long> groupsID = new List<long>();
    string emails = string.Empty;
    if (this.cbUsers.Checked)
      usersID = this._usersID;
    if (this.cbSpecificGroupUsers.Checked)
      groupsID = this._groupsID;
    if (this.cbSpecificRoleUsers.Checked)
      rolesID = this._rolesID;
    if (this.cbEmail.Checked)
      emails = this._email;
    this._notifSettings.Adressee = (Adressee) new SpecificAdressee(usersID, rolesID, groupsID, emails);
  }

  public override void Refresh()
  {
    base.Refresh();
    this.LoadInfoFromSettings();
    this.UpdateControl();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SpecificAdresseeCntrl));
    this.gbSpecAdressee = new GroupBox();
    this.cbEmail = new CheckBox();
    this.cbSpecificGroupUsers = new CheckBox();
    this.cbSpecificRoleUsers = new CheckBox();
    this.cbUsers = new CheckBox();
    this.specificAdrChoosePanel = new Panel();
    this.gbEMail = new GroupBox();
    this.tbEmail = new TextBox();
    this.gbGroup = new GroupBox();
    this.lvGroups = new ListView();
    this.groups = new ColumnHeader();
    this.toolBar2 = new Intermech.Bars.ToolBar();
    this.btnAddGroup = new ButtonItem();
    this.btnDeleteGroup = new ButtonItem();
    this.gbRoles = new GroupBox();
    this.lvRoles = new ListView();
    this.role = new ColumnHeader();
    this.toolBar1 = new Intermech.Bars.ToolBar();
    this.btnAddRole = new ButtonItem();
    this.btnDeleteRole = new ButtonItem();
    this.gbUsers = new GroupBox();
    this.lvUsers = new ListView();
    this.userName = new ColumnHeader();
    this.tbUsers = new Intermech.Bars.ToolBar();
    this.btnAddUser = new ButtonItem();
    this.btnDeleteUser = new ButtonItem();
    this.gbSpecAdressee.SuspendLayout();
    this.specificAdrChoosePanel.SuspendLayout();
    this.gbEMail.SuspendLayout();
    this.gbGroup.SuspendLayout();
    this.gbRoles.SuspendLayout();
    this.gbUsers.SuspendLayout();
    this.SuspendLayout();
    this.gbSpecAdressee.Controls.Add((Control) this.cbEmail);
    this.gbSpecAdressee.Controls.Add((Control) this.cbSpecificGroupUsers);
    this.gbSpecAdressee.Controls.Add((Control) this.cbSpecificRoleUsers);
    this.gbSpecAdressee.Controls.Add((Control) this.cbUsers);
    this.gbSpecAdressee.Dock = DockStyle.Top;
    this.gbSpecAdressee.Location = new Point(0, 0);
    this.gbSpecAdressee.Name = "gbSpecAdressee";
    this.gbSpecAdressee.Size = new Size(488, 118);
    this.gbSpecAdressee.TabIndex = 1;
    this.gbSpecAdressee.TabStop = false;
    this.gbSpecAdressee.Text = "Адресат";
    this.cbEmail.AutoSize = true;
    this.cbEmail.Location = new Point(7, 92);
    this.cbEmail.Name = "cbEmail";
    this.cbEmail.Size = new Size(137, 17);
    this.cbEmail.TabIndex = 3;
    this.cbEmail.Text = "Адрес внешней почты";
    this.cbEmail.UseVisualStyleBackColor = true;
    this.cbEmail.CheckedChanged += new EventHandler(this.cbEmail_CheckedChanged);
    this.cbSpecificGroupUsers.AutoSize = true;
    this.cbSpecificGroupUsers.Location = new Point(7, 67);
    this.cbSpecificGroupUsers.Name = "cbSpecificGroupUsers";
    this.cbSpecificGroupUsers.Size = new Size(194, 17);
    this.cbSpecificGroupUsers.TabIndex = 2;
    this.cbSpecificGroupUsers.Text = "Пользователи указанной группы";
    this.cbSpecificGroupUsers.UseVisualStyleBackColor = true;
    this.cbSpecificGroupUsers.CheckedChanged += new EventHandler(this.cbSpecificGroupUsers_CheckedChanged);
    this.cbSpecificRoleUsers.AutoSize = true;
    this.cbSpecificRoleUsers.Location = new Point(7, 44);
    this.cbSpecificRoleUsers.Name = "cbSpecificRoleUsers";
    this.cbSpecificRoleUsers.Size = new Size(182, 17);
    this.cbSpecificRoleUsers.TabIndex = 1;
    this.cbSpecificRoleUsers.Text = "Пользователи указанной роли";
    this.cbSpecificRoleUsers.UseVisualStyleBackColor = true;
    this.cbSpecificRoleUsers.CheckedChanged += new EventHandler(this.cbSpecificRoleUsers_CheckedChanged);
    this.cbUsers.AutoSize = true;
    this.cbUsers.Location = new Point(7, 20);
    this.cbUsers.Name = "cbUsers";
    this.cbUsers.Size = new Size(99, 17);
    this.cbUsers.TabIndex = 0;
    this.cbUsers.Text = "Пользователи";
    this.cbUsers.UseVisualStyleBackColor = true;
    this.cbUsers.CheckedChanged += new EventHandler(this.cbUsers_CheckedChanged);
    this.specificAdrChoosePanel.AutoScroll = true;
    this.specificAdrChoosePanel.Controls.Add((Control) this.gbEMail);
    this.specificAdrChoosePanel.Controls.Add((Control) this.gbGroup);
    this.specificAdrChoosePanel.Controls.Add((Control) this.gbRoles);
    this.specificAdrChoosePanel.Controls.Add((Control) this.gbUsers);
    this.specificAdrChoosePanel.Dock = DockStyle.Fill;
    this.specificAdrChoosePanel.Location = new Point(0, 118);
    this.specificAdrChoosePanel.Name = "specificAdrChoosePanel";
    this.specificAdrChoosePanel.Size = new Size(488, 515);
    this.specificAdrChoosePanel.TabIndex = 2;
    this.gbEMail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.gbEMail.Controls.Add((Control) this.tbEmail);
    this.gbEMail.Enabled = false;
    this.gbEMail.Location = new Point(16 /*0x10*/, 406);
    this.gbEMail.Name = "gbEMail";
    this.gbEMail.Size = new Size(463, 43);
    this.gbEMail.TabIndex = 5;
    this.gbEMail.TabStop = false;
    this.gbEMail.Text = "Почта";
    this.tbEmail.Dock = DockStyle.Top;
    this.tbEmail.Location = new Point(3, 16 /*0x10*/);
    this.tbEmail.Name = "tbEmail";
    this.tbEmail.Size = new Size(457, 20);
    this.tbEmail.TabIndex = 0;
    this.tbEmail.TextChanged += new EventHandler(this.tbEmail_TextChanged);
    this.gbGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.gbGroup.Controls.Add((Control) this.lvGroups);
    this.gbGroup.Controls.Add((Control) this.toolBar2);
    this.gbGroup.Enabled = false;
    this.gbGroup.Location = new Point(13, 272);
    this.gbGroup.Name = "gbGroup";
    this.gbGroup.Size = new Size(469, 128 /*0x80*/);
    this.gbGroup.TabIndex = 2;
    this.gbGroup.TabStop = false;
    this.gbGroup.Text = "Группы";
    this.lvGroups.Columns.AddRange(new ColumnHeader[1]
    {
      this.groups
    });
    this.lvGroups.Dock = DockStyle.Fill;
    this.lvGroups.FullRowSelect = true;
    this.lvGroups.HideSelection = false;
    this.lvGroups.Location = new Point(3, 40);
    this.lvGroups.Name = "lvGroups";
    this.lvGroups.Size = new Size(463, 85);
    this.lvGroups.TabIndex = 4;
    this.lvGroups.UseCompatibleStateImageBehavior = false;
    this.lvGroups.View = View.Details;
    this.lvGroups.SelectedIndexChanged += new EventHandler(this.lvGroups_SelectedIndexChanged);
    this.lvGroups.Leave += new EventHandler(this.lvGroups_Leave);
    this.groups.Text = "Наименование";
    this.groups.Width = 459;
    this.toolBar2.FullMenus = true;
    this.toolBar2.Guid = new Guid("789fe93a-3b81-425b-a128-f7c0eb7be0ad");
    this.toolBar2.Hidden = false;
    this.toolBar2.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.btnAddGroup,
      (ToolbarItemBase) this.btnDeleteGroup
    });
    this.toolBar2.Location = new Point(3, 16 /*0x10*/);
    this.toolBar2.Name = "toolBar2";
    this.toolBar2.Size = new Size(463, 24);
    this.toolBar2.TabIndex = 3;
    this.toolBar2.Text = "toolBar1";
    this.btnAddGroup.BeginGroup = true;
    this.btnAddGroup.CommandName = "btnAddGroup";
    this.btnAddGroup.Image = (Image) componentResourceManager.GetObject("btnAddGroup.Image");
    this.btnAddGroup.ImageIndex = 0;
    this.btnAddGroup.ToolTipText = "Добавить группу";
    this.btnAddGroup.Click += new EventHandler(this.btnAddGroup_Click);
    this.btnDeleteGroup.BeginGroup = true;
    this.btnDeleteGroup.CommandName = "btnDeleteGroup";
    this.btnDeleteGroup.Image = (Image) componentResourceManager.GetObject("btnDeleteGroup.Image");
    this.btnDeleteGroup.ToolTipText = "Удалить группу";
    this.btnDeleteGroup.Click += new EventHandler(this.btnDeleteGroup_Click);
    this.gbRoles.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.gbRoles.Controls.Add((Control) this.lvRoles);
    this.gbRoles.Controls.Add((Control) this.toolBar1);
    this.gbRoles.Enabled = false;
    this.gbRoles.Location = new Point(10, 139);
    this.gbRoles.Name = "gbRoles";
    this.gbRoles.Size = new Size(475, 126);
    this.gbRoles.TabIndex = 1;
    this.gbRoles.TabStop = false;
    this.gbRoles.Text = "Роли";
    this.lvRoles.Columns.AddRange(new ColumnHeader[1]
    {
      this.role
    });
    this.lvRoles.Dock = DockStyle.Fill;
    this.lvRoles.FullRowSelect = true;
    this.lvRoles.HideSelection = false;
    this.lvRoles.Location = new Point(3, 40);
    this.lvRoles.Name = "lvRoles";
    this.lvRoles.Size = new Size(469, 83);
    this.lvRoles.TabIndex = 4;
    this.lvRoles.UseCompatibleStateImageBehavior = false;
    this.lvRoles.View = View.Details;
    this.lvRoles.SelectedIndexChanged += new EventHandler(this.lvRoles_SelectedIndexChanged);
    this.lvRoles.Leave += new EventHandler(this.lvRoles_Leave);
    this.role.Text = "Наименование";
    this.role.Width = 464;
    this.toolBar1.FullMenus = true;
    this.toolBar1.Guid = new Guid("789fe93a-3b81-425b-a128-f7c0eb7be0ad");
    this.toolBar1.Hidden = false;
    this.toolBar1.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.btnAddRole,
      (ToolbarItemBase) this.btnDeleteRole
    });
    this.toolBar1.Location = new Point(3, 16 /*0x10*/);
    this.toolBar1.Name = "toolBar1";
    this.toolBar1.Size = new Size(469, 24);
    this.toolBar1.TabIndex = 3;
    this.toolBar1.Text = "toolBar1";
    this.btnAddRole.BeginGroup = true;
    this.btnAddRole.CommandName = "btnAddRole";
    this.btnAddRole.Image = (Image) componentResourceManager.GetObject("btnAddRole.Image");
    this.btnAddRole.ImageIndex = 0;
    this.btnAddRole.ToolTipText = "Добавить роль";
    this.btnAddRole.Click += new EventHandler(this.btnAddRole_Click);
    this.btnDeleteRole.BeginGroup = true;
    this.btnDeleteRole.CommandName = "btnDeleteRole";
    this.btnDeleteRole.Image = (Image) componentResourceManager.GetObject("btnDeleteRole.Image");
    this.btnDeleteRole.ToolTipText = "Удалить роль";
    this.btnDeleteRole.Click += new EventHandler(this.btnDeleteRole_Click);
    this.gbUsers.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.gbUsers.Controls.Add((Control) this.lvUsers);
    this.gbUsers.Controls.Add((Control) this.tbUsers);
    this.gbUsers.Enabled = false;
    this.gbUsers.Location = new Point(7, 6);
    this.gbUsers.Name = "gbUsers";
    this.gbUsers.Size = new Size(478, (int) sbyte.MaxValue);
    this.gbUsers.TabIndex = 0;
    this.gbUsers.TabStop = false;
    this.gbUsers.Text = "Пользователи";
    this.lvUsers.Columns.AddRange(new ColumnHeader[1]
    {
      this.userName
    });
    this.lvUsers.Dock = DockStyle.Fill;
    this.lvUsers.FullRowSelect = true;
    this.lvUsers.HideSelection = false;
    this.lvUsers.Location = new Point(3, 40);
    this.lvUsers.Name = "lvUsers";
    this.lvUsers.Size = new Size(472, 84);
    this.lvUsers.TabIndex = 3;
    this.lvUsers.UseCompatibleStateImageBehavior = false;
    this.lvUsers.View = View.Details;
    this.lvUsers.SelectedIndexChanged += new EventHandler(this.lvUsers_SelectedIndexChanged);
    this.lvUsers.Leave += new EventHandler(this.lvUsers_Leave);
    this.userName.Text = "Наименование";
    this.userName.Width = 467;
    this.tbUsers.FullMenus = true;
    this.tbUsers.Guid = new Guid("789fe93a-3b81-425b-a128-f7c0eb7be0ad");
    this.tbUsers.Hidden = false;
    this.tbUsers.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.btnAddUser,
      (ToolbarItemBase) this.btnDeleteUser
    });
    this.tbUsers.Location = new Point(3, 16 /*0x10*/);
    this.tbUsers.Name = "tbUsers";
    this.tbUsers.Size = new Size(472, 24);
    this.tbUsers.TabIndex = 2;
    this.tbUsers.Text = "toolBar1";
    this.btnAddUser.BeginGroup = true;
    this.btnAddUser.CommandName = "btnAddUser";
    this.btnAddUser.Image = (Image) componentResourceManager.GetObject("btnAddUser.Image");
    this.btnAddUser.ImageIndex = 0;
    this.btnAddUser.ToolTipText = "Добавить пользователя";
    this.btnAddUser.Click += new EventHandler(this.btnAddUser_Click);
    this.btnDeleteUser.BeginGroup = true;
    this.btnDeleteUser.CommandName = "btnDeleteUser";
    this.btnDeleteUser.Image = (Image) componentResourceManager.GetObject("btnDeleteUser.Image");
    this.btnDeleteUser.ToolTipText = "Удалить пользователя";
    this.btnDeleteUser.Click += new EventHandler(this.btnDeleteUser_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoScroll = true;
    this.Controls.Add((Control) this.specificAdrChoosePanel);
    this.Controls.Add((Control) this.gbSpecAdressee);
    this.Name = nameof (SpecificAdresseeCntrl);
    this.Size = new Size(488, 633);
    this.gbSpecAdressee.ResumeLayout(false);
    this.gbSpecAdressee.PerformLayout();
    this.specificAdrChoosePanel.ResumeLayout(false);
    this.gbEMail.ResumeLayout(false);
    this.gbEMail.PerformLayout();
    this.gbGroup.ResumeLayout(false);
    this.gbRoles.ResumeLayout(false);
    this.gbUsers.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
