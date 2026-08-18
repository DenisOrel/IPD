// Decompiled with JetBrains decompiler
// Type: Intermech.Ldap.LdapSyncConfigForm
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Controls;
using Intermech.DatabaseConfigurator;
using Intermech.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Ldap;

public class LdapSyncConfigForm : Form
{
  private LdapHolder ldapHolder = new LdapHolder();
  private HybridDictionary dbUsers;
  private bool needApply;
  private bool needConnection = true;
  private bool dbLoaded;
  private bool ldapLoaded;
  private bool blockOnCheck;
  private string loginPostfix = string.Empty;
  private string loginPostfixUpper = string.Empty;
  private int sortColumnIps = -1;
  private int sortColumnLdap = -1;
  private bool multiCatalogEnabled;
  private string defaultCatalogName = string.Empty;
  private HybridDictionary catalogsAndExclusionUsers = new HybridDictionary();
  private IContainer components;
  private ComboBox cbDomain;
  private Button btnApply;
  private Button btnExit;
  private Label label1;
  private Button btnGetUsersList;
  private SplitContainer splitContainer;
  private GroupBox groupBox1;
  private ListView lvIpsUsers;
  private GroupBox groupBox2;
  private ListView lvLdapUsers;
  private ColumnHeader ipsUser;
  private ColumnHeader ipsDescription;
  private ColumnHeader ipsSID;
  private ColumnHeader ipsStatus;
  private ColumnHeader ldapName;
  private ColumnHeader ldapDescription;
  private ColumnHeader ldapSID;
  private ContextMenuStrip ldapContextMenu;
  private ToolStripMenuItem selectAllMenuItem;
  private ToolStripMenuItem clearSelectionMenuItem;
  private Button btnUpdate;
  private StatusBar _statusBar;

  public LdapSyncConfigForm() => this.InitializeComponent();

  private void Init()
  {
    this.needConnection = true;
    this.needApply = false;
    this.dbLoaded = false;
    this.ldapHolder.Clear();
    this.ldapLoaded = false;
    this.loginPostfix = string.Empty;
    this.loginPostfixUpper = string.Empty;
    this.multiCatalogEnabled = false;
    this.defaultCatalogName = string.Empty;
    this.catalogsAndExclusionUsers = new HybridDictionary();
    this.lvIpsUsers.Items.Clear();
    this.lvLdapUsers.Items.Clear();
  }

  private void LoadDomainList()
  {
    foreach (ActiveDirectoryPartition domain in (ReadOnlyCollectionBase) Forest.GetCurrentForest().Domains)
      this.cbDomain.Items.Add((object) domain.Name);
  }

  private void LoadConfig()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService))
        return;
      customService.SynchronizeDirectoryReadConfig(sessionKeeper.Session.SessionGUID, out this.defaultCatalogName, out this.catalogsAndExclusionUsers);
    }
  }

  private void SaveConfig(bool withSync)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService))
        return;
      customService.SynchronizeDirectoryWriteConfig(sessionKeeper.Session.SessionGUID, this.defaultCatalogName, this.catalogsAndExclusionUsers, withSync);
    }
  }

  public DialogResult ExecuteDialog()
  {
    this.Init();
    this.LoadDomainList();
    return this.ShowDialog();
  }

  private void FillForm()
  {
    this.needApply = false;
    this.LoadConfig();
    this.LoadDBUsersList();
    if (this.cbDomain.Text != string.Empty && this.LoadLdapUsersList())
    {
      this.needConnection = false;
      this.FillLdapUsersList();
    }
    this.FillDBUsersList();
    if (this.ldapLoaded)
      this.UpdateDBListStatus();
    this.UpdateStatus();
    this.CheckComponents();
  }

  private bool Apply()
  {
    bool flag = true;
    this.GetExclusions(this.cbDomain.Text, true);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService)
      {
        flag = customService.SynchronizeDirectoryWriteConfig(sessionKeeper.Session.SessionGUID, this.defaultCatalogName, this.catalogsAndExclusionUsers, false) == 0;
        if (flag)
          flag = customService.SynchronizeDirectoryProcess(sessionKeeper.Session.SessionGUID, this.cbDomain.Text) == 0;
      }
    }
    return flag;
  }

  private List<string> GetExclusions(string domainName, bool withUpdate)
  {
    if (domainName == string.Empty)
      return new List<string>();
    string empty = string.Empty;
    foreach (DictionaryEntry andExclusionUser in this.catalogsAndExclusionUsers)
    {
      if (andExclusionUser.Key.ToString().Equals(domainName, StringComparison.CurrentCultureIgnoreCase))
      {
        empty = andExclusionUser.Key.ToString();
        break;
      }
    }
    if (!withUpdate)
      return empty != string.Empty ? this.catalogsAndExclusionUsers[(object) empty] as List<string> : new List<string>();
    List<string> exclusions = new List<string>();
    for (int index = 0; index < this.lvLdapUsers.CheckedItems.Count; ++index)
      exclusions.Add(((HybridDictionary) this.lvLdapUsers.CheckedItems[index].Tag)[(object) LdapConsts.ADObjectSID].ToString());
    if (empty != string.Empty)
      this.catalogsAndExclusionUsers[(object) empty] = (object) exclusions;
    else
      this.catalogsAndExclusionUsers.Add((object) domainName, (object) exclusions);
    return exclusions;
  }

  private bool LoadLdapUsersList()
  {
    bool flag = this.ldapHolder.ReadDirectory(this.cbDomain.Text, true);
    this.AssignLoginPostfix(this.cbDomain.Text);
    this.ldapLoaded = flag;
    return flag;
  }

  private bool LoadDBUsersList()
  {
    bool flag = true;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService)
        flag = customService.ReadDBUsers(sessionKeeper.Session.SessionGUID, out this.dbUsers) == 0;
    }
    this.dbLoaded = flag;
    return flag;
  }

  private void AssignLoginPostfix(string selectedDomain)
  {
    this.loginPostfix = !this.defaultCatalogName.Equals(selectedDomain, StringComparison.InvariantCultureIgnoreCase) ? "@" + selectedDomain : string.Empty;
    this.loginPostfixUpper = this.loginPostfix.ToUpper();
  }

  private bool FillLdapUsersList()
  {
    bool flag = true;
    SortOrder sorting = this.lvLdapUsers.Sorting;
    IComparer listViewItemSorter = this.lvLdapUsers.ListViewItemSorter;
    this.lvLdapUsers.BeginUpdate();
    this.lvLdapUsers.Items.Clear();
    this.lvLdapUsers.Sorting = SortOrder.None;
    try
    {
      List<string> exclusions = this.GetExclusions(this.cbDomain.Text, false);
      foreach (DictionaryEntry hdUser in this.ldapHolder.hdUsers)
      {
        string str1 = hdUser.Key.ToString();
        string str2 = ((HybridDictionary) hdUser.Value)[(object) LdapConsts.ADObjectSID].ToString();
        if (this.dbUsers.Contains((object) (str1 + this.loginPostfixUpper)))
        {
          HybridDictionary dbUser = (HybridDictionary) this.dbUsers[(object) (str1 + this.loginPostfixUpper)];
          if (dbUser[(object) LdapConsts.ADObjectSID].ToString() == string.Empty || dbUser[(object) LdapConsts.ADObjectSID].ToString() == str2)
            continue;
        }
        this.blockOnCheck = true;
        try
        {
          ListViewItem listViewItem = this.lvLdapUsers.Items.Add(new ListViewItem(new string[3]
          {
            ((HybridDictionary) hdUser.Value)[(object) LdapConsts.ADSAMAccountName].ToString() + this.loginPostfix,
            ((HybridDictionary) hdUser.Value)[(object) LdapConsts.ADDisplayName].ToString(),
            str2
          }));
          listViewItem.Tag = hdUser.Value;
          if (exclusions.IndexOf(str2) != -1)
            listViewItem.Checked = true;
          else
            this.needApply = true;
        }
        finally
        {
          this.blockOnCheck = false;
        }
      }
    }
    finally
    {
      if (this.lvLdapUsers.Sorting != sorting)
      {
        this.lvLdapUsers.ListViewItemSorter = listViewItemSorter;
        this.lvLdapUsers.Sorting = sorting;
      }
      this.lvLdapUsers.EndUpdate();
    }
    return flag;
  }

  private bool FillDBUsersList()
  {
    bool flag = true;
    SortOrder sorting = this.lvIpsUsers.Sorting;
    IComparer listViewItemSorter = this.lvIpsUsers.ListViewItemSorter;
    this.lvIpsUsers.BeginUpdate();
    this.lvIpsUsers.Items.Clear();
    this.lvIpsUsers.Sorting = SortOrder.None;
    try
    {
      foreach (DictionaryEntry dbUser in this.dbUsers)
        this.lvIpsUsers.Items.Add(new ListViewItem(new string[4]
        {
          ((HybridDictionary) dbUser.Value)[(object) LdapConsts.ADSAMAccountName].ToString(),
          ((HybridDictionary) dbUser.Value)[(object) LdapConsts.ADDisplayName].ToString(),
          ((HybridDictionary) dbUser.Value)[(object) LdapConsts.ADObjectSID].ToString(),
          string.Empty
        })).Tag = dbUser.Value;
    }
    finally
    {
      if (this.lvIpsUsers.Sorting != sorting)
      {
        this.lvIpsUsers.ListViewItemSorter = listViewItemSorter;
        this.lvIpsUsers.Sorting = sorting;
      }
      this.lvIpsUsers.EndUpdate();
    }
    return flag;
  }

  private void btnGetUsersList_Click(object sender, EventArgs e)
  {
    if (!this.CheckDefaultDomain())
      return;
    if (this.LoadLdapUsersList())
    {
      this.needConnection = false;
      this.FillLdapUsersList();
      this.UpdateDBListStatus();
      ImportFromNTDomainForm.ChechLoginNamesLengthAndWarning(this.lvLdapUsers);
    }
    this.CheckComponents();
  }

  private bool CheckDefaultDomain()
  {
    if (this.defaultCatalogName == string.Empty && this.cbDomain.Text != string.Empty)
    {
      switch (IMMessageBox.Show("Вопрос", $"Домен по умолчанию не назначен.\nДля доменов по умолчанию имя входа пользователя не содержит постфикс @<имя домена>.\nНазначить {this.cbDomain.Text} доменом по умолчанию?", MessageBoxButtons.YesNoCancel, IMMessageBoxImage.Question))
      {
        case DialogResult.Cancel:
          return false;
        case DialogResult.Yes:
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            if (sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService)
            {
              HybridDictionary catalogsAndExclusionUsers;
              customService.SynchronizeDirectoryReadConfig(sessionKeeper.Session.SessionGUID, out this.defaultCatalogName, out catalogsAndExclusionUsers);
              this.defaultCatalogName = this.cbDomain.Text;
              customService.SynchronizeDirectoryWriteConfig(sessionKeeper.Session.SessionGUID, this.defaultCatalogName, catalogsAndExclusionUsers, false);
              this.UpdateStatus();
              break;
            }
            break;
          }
      }
    }
    return true;
  }

  private void UpdateStatus()
  {
    this._statusBar.Text = "Домен по умолчанию: " + (this.defaultCatalogName != string.Empty ? this.defaultCatalogName : "<не назначен>");
  }

  private int UpdateDBListStatus()
  {
    int num = 0;
    List<string> exclusions = this.GetExclusions(this.cbDomain.Text, false);
    for (int index = 0; index < this.lvIpsUsers.Items.Count; ++index)
    {
      string str1 = this.lvIpsUsers.Items[index].Text.ToUpper();
      string uName;
      string uDomain;
      bool flag1 = this.SplitUserName(str1, out uName, out uDomain);
      HybridDictionary tag = (HybridDictionary) this.lvIpsUsers.Items[index].Tag;
      string str2 = tag[(object) LdapConsts.ADSAMAccountName].ToString();
      string str3 = tag[(object) LdapConsts.ADDisplayName].ToString();
      string str4 = tag[(object) LdapConsts.ADObjectSID].ToString();
      if (str4 == string.Empty)
      {
        bool flag2 = false;
        if (flag1)
        {
          if (this.cbDomain.Text.Equals(uDomain, StringComparison.CurrentCultureIgnoreCase))
          {
            str1 = uName;
          }
          else
          {
            this.lvIpsUsers.Items[index].SubItems[LdapConsts.IpsStatusSubitemIndex].Text = "для иного домена";
            flag2 = true;
          }
        }
        else if (!this.cbDomain.Text.Equals(this.defaultCatalogName, StringComparison.CurrentCultureIgnoreCase))
        {
          this.lvIpsUsers.Items[index].SubItems[LdapConsts.IpsStatusSubitemIndex].Text = !(this.defaultCatalogName == string.Empty) ? "локальный или для домена по умолчанию" : "локальный";
          flag2 = true;
        }
        if (!flag2)
        {
          if (this.ldapHolder.hdUsers.Contains((object) str1) && exclusions.IndexOf(((HybridDictionary) this.ldapHolder.hdUsers[(object) str1])[(object) LdapConsts.ADObjectSID].ToString()) == -1)
          {
            this.lvIpsUsers.Items[index].SubItems[LdapConsts.IpsStatusSubitemIndex].Text = "не подтверждено";
            this.FireNeedApply();
          }
          else
            this.lvIpsUsers.Items[index].SubItems[LdapConsts.IpsStatusSubitemIndex].Text = "локальный";
        }
      }
      else
      {
        bool flag3 = false;
        foreach (DictionaryEntry hdUser in this.ldapHolder.hdUsers)
        {
          HybridDictionary hybridDictionary = (HybridDictionary) hdUser.Value;
          string str5 = hybridDictionary[(object) LdapConsts.ADSAMAccountName].ToString();
          string str6 = hybridDictionary[(object) LdapConsts.ADDisplayName].ToString();
          string str7 = hybridDictionary[(object) LdapConsts.ADObjectSID].ToString();
          if (str4 == str7)
          {
            if (exclusions.IndexOf(str7) == -1)
            {
              if (flag1 && this.cbDomain.Text.Equals(uDomain, StringComparison.CurrentCultureIgnoreCase))
                str2 = uName;
              flag3 = true;
              if (str2 == str5.ToUpper() && str3 == str6)
              {
                this.lvIpsUsers.Items[index].SubItems[LdapConsts.IpsStatusSubitemIndex].Text = "ok";
                break;
              }
              this.lvIpsUsers.Items[index].SubItems[LdapConsts.IpsStatusSubitemIndex].Text = "к синхронизации";
              this.FireNeedApply();
              break;
            }
            break;
          }
        }
        if (!flag3)
        {
          if (flag1 && this.cbDomain.Text.Equals(uDomain, StringComparison.CurrentCultureIgnoreCase) || !flag1 && this.cbDomain.Text.Equals(this.defaultCatalogName, StringComparison.CurrentCultureIgnoreCase))
          {
            this.lvIpsUsers.Items[index].SubItems[LdapConsts.IpsStatusSubitemIndex].Text = "к удалению";
            this.FireNeedApply();
          }
          else
            this.lvIpsUsers.Items[index].SubItems[LdapConsts.IpsStatusSubitemIndex].Text = !flag1 ? "для домена по умолчанию" : "для иного домена";
        }
      }
    }
    return num;
  }

  private bool SplitUserName(string bdKey, out string uName, out string uDomain)
  {
    uName = string.Empty;
    uDomain = string.Empty;
    int length = bdKey.IndexOf('@');
    if (length == -1)
    {
      uName = bdKey;
    }
    else
    {
      uName = bdKey.Substring(0, length);
      uDomain = bdKey.Substring(length + 1);
    }
    return length != -1;
  }

  private void btnApply_Click(object sender, EventArgs e)
  {
    if (!this.needApply || !this.Apply())
      return;
    this.FillForm();
  }

  private void btnExit_Click(object sender, EventArgs e)
  {
    if (this.needApply)
    {
      switch (MessageBox.Show("Сохранить изменения?", "Вопрос", MessageBoxButtons.YesNoCancel))
      {
        case DialogResult.Cancel:
          return;
        case DialogResult.Yes:
          if (!this.Apply())
            return;
          break;
      }
    }
    this.Close();
  }

  private void CheckComponents()
  {
    this.btnGetUsersList.Enabled = this.cbDomain.Text != string.Empty;
    this.lvIpsUsers.Enabled = !this.needConnection;
    this.lvLdapUsers.Enabled = !this.needConnection;
    this.btnApply.Enabled = !this.needConnection && this.needApply;
  }

  private void FireNeedApply()
  {
    this.needApply = true;
    this.CheckComponents();
  }

  private void selectAllMenuItem_Click(object sender, EventArgs e)
  {
    this.blockOnCheck = true;
    try
    {
      for (int index = 0; index < this.lvLdapUsers.Items.Count; ++index)
        this.lvLdapUsers.Items[index].Checked = true;
    }
    finally
    {
      this.blockOnCheck = false;
    }
    this.FireNeedApply();
  }

  private void clearSelectionMenuItem_Click(object sender, EventArgs e)
  {
    this.blockOnCheck = true;
    try
    {
      for (int index = 0; index < this.lvLdapUsers.Items.Count; ++index)
        this.lvLdapUsers.Items[index].Checked = false;
    }
    finally
    {
      this.blockOnCheck = false;
    }
    this.FireNeedApply();
  }

  private void btnUpdate_Click(object sender, EventArgs e) => this.FillForm();

  private void lvLdapUsers_ItemChecked(object sender, ItemCheckedEventArgs e)
  {
    if (this.blockOnCheck)
      return;
    this.FireNeedApply();
  }

  private void LdapSyncConfigForm_Load(object sender, EventArgs e)
  {
    this.cbDomain.Text = string.Empty;
    this.FillForm();
  }

  private void cbDomain_TextChanged(object sender, EventArgs e) => this.CheckComponents();

  private void lvIpsUsers_ColumnClick(object sender, ColumnClickEventArgs e)
  {
    this.ProcessColumnClick(this.lvIpsUsers, ref this.sortColumnIps, e);
  }

  private void lvLdapUsers_ColumnClick(object sender, ColumnClickEventArgs e)
  {
    this.blockOnCheck = true;
    try
    {
      this.ProcessColumnClick(this.lvLdapUsers, ref this.sortColumnLdap, e);
    }
    finally
    {
      this.blockOnCheck = false;
    }
  }

  private void ProcessColumnClick(ListView _list, ref int sortColumn, ColumnClickEventArgs e)
  {
    if (e.Column != sortColumn)
    {
      _list.Sorting = sortColumn != -1 ? SortOrder.Ascending : (_list.Sorting != SortOrder.Ascending ? SortOrder.Ascending : SortOrder.Descending);
      sortColumn = e.Column;
    }
    else
      _list.Sorting = _list.Sorting != SortOrder.Ascending ? SortOrder.Ascending : SortOrder.Descending;
    _list.ListViewItemSorter = (IComparer) new ListViewItemComparer(e.Column, _list.Sorting);
    _list.Sort();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (LdapSyncConfigForm));
    this.splitContainer = new SplitContainer();
    this.groupBox1 = new GroupBox();
    this.lvIpsUsers = new ListView();
    this.ipsUser = new ColumnHeader();
    this.ipsDescription = new ColumnHeader();
    this.ipsSID = new ColumnHeader();
    this.ipsStatus = new ColumnHeader();
    this.groupBox2 = new GroupBox();
    this.lvLdapUsers = new ListView();
    this.ldapName = new ColumnHeader();
    this.ldapDescription = new ColumnHeader();
    this.ldapSID = new ColumnHeader();
    this.ldapContextMenu = new ContextMenuStrip(this.components);
    this.selectAllMenuItem = new ToolStripMenuItem();
    this.clearSelectionMenuItem = new ToolStripMenuItem();
    this.cbDomain = new ComboBox();
    this.btnApply = new Button();
    this.btnExit = new Button();
    this.label1 = new Label();
    this.btnGetUsersList = new Button();
    this.btnUpdate = new Button();
    this._statusBar = new StatusBar();
    this.splitContainer.BeginInit();
    this.splitContainer.Panel1.SuspendLayout();
    this.splitContainer.Panel2.SuspendLayout();
    this.splitContainer.SuspendLayout();
    this.groupBox1.SuspendLayout();
    this.groupBox2.SuspendLayout();
    this.ldapContextMenu.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.splitContainer, "splitContainer");
    this.splitContainer.Name = "splitContainer";
    this.splitContainer.Panel1.Controls.Add((Control) this.groupBox1);
    this.splitContainer.Panel2.Controls.Add((Control) this.groupBox2);
    this.groupBox1.Controls.Add((Control) this.lvIpsUsers);
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    this.lvIpsUsers.Columns.AddRange(new ColumnHeader[4]
    {
      this.ipsUser,
      this.ipsDescription,
      this.ipsSID,
      this.ipsStatus
    });
    componentResourceManager.ApplyResources((object) this.lvIpsUsers, "lvIpsUsers");
    this.lvIpsUsers.FullRowSelect = true;
    this.lvIpsUsers.GridLines = true;
    this.lvIpsUsers.HideSelection = false;
    this.lvIpsUsers.Name = "lvIpsUsers";
    this.lvIpsUsers.Sorting = SortOrder.Ascending;
    this.lvIpsUsers.UseCompatibleStateImageBehavior = false;
    this.lvIpsUsers.View = View.Details;
    this.lvIpsUsers.ColumnClick += new ColumnClickEventHandler(this.lvIpsUsers_ColumnClick);
    componentResourceManager.ApplyResources((object) this.ipsUser, "ipsUser");
    componentResourceManager.ApplyResources((object) this.ipsDescription, "ipsDescription");
    componentResourceManager.ApplyResources((object) this.ipsSID, "ipsSID");
    componentResourceManager.ApplyResources((object) this.ipsStatus, "ipsStatus");
    this.groupBox2.Controls.Add((Control) this.lvLdapUsers);
    componentResourceManager.ApplyResources((object) this.groupBox2, "groupBox2");
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.TabStop = false;
    this.lvLdapUsers.CheckBoxes = true;
    this.lvLdapUsers.Columns.AddRange(new ColumnHeader[3]
    {
      this.ldapName,
      this.ldapDescription,
      this.ldapSID
    });
    this.lvLdapUsers.ContextMenuStrip = this.ldapContextMenu;
    componentResourceManager.ApplyResources((object) this.lvLdapUsers, "lvLdapUsers");
    this.lvLdapUsers.FullRowSelect = true;
    this.lvLdapUsers.GridLines = true;
    this.lvLdapUsers.HideSelection = false;
    this.lvLdapUsers.MultiSelect = false;
    this.lvLdapUsers.Name = "lvLdapUsers";
    this.lvLdapUsers.Sorting = SortOrder.Ascending;
    this.lvLdapUsers.UseCompatibleStateImageBehavior = false;
    this.lvLdapUsers.View = View.Details;
    this.lvLdapUsers.ColumnClick += new ColumnClickEventHandler(this.lvLdapUsers_ColumnClick);
    this.lvLdapUsers.ItemChecked += new ItemCheckedEventHandler(this.lvLdapUsers_ItemChecked);
    componentResourceManager.ApplyResources((object) this.ldapName, "ldapName");
    componentResourceManager.ApplyResources((object) this.ldapDescription, "ldapDescription");
    componentResourceManager.ApplyResources((object) this.ldapSID, "ldapSID");
    this.ldapContextMenu.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.selectAllMenuItem,
      (ToolStripItem) this.clearSelectionMenuItem
    });
    this.ldapContextMenu.Name = "ldapContextMenu";
    componentResourceManager.ApplyResources((object) this.ldapContextMenu, "ldapContextMenu");
    this.selectAllMenuItem.Name = "selectAllMenuItem";
    componentResourceManager.ApplyResources((object) this.selectAllMenuItem, "selectAllMenuItem");
    this.selectAllMenuItem.Click += new EventHandler(this.selectAllMenuItem_Click);
    this.clearSelectionMenuItem.Name = "clearSelectionMenuItem";
    componentResourceManager.ApplyResources((object) this.clearSelectionMenuItem, "clearSelectionMenuItem");
    this.clearSelectionMenuItem.Click += new EventHandler(this.clearSelectionMenuItem_Click);
    componentResourceManager.ApplyResources((object) this.cbDomain, "cbDomain");
    this.cbDomain.FormattingEnabled = true;
    this.cbDomain.Name = "cbDomain";
    this.cbDomain.Sorted = true;
    this.cbDomain.TextChanged += new EventHandler(this.cbDomain_TextChanged);
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Name = "btnApply";
    this.btnApply.UseVisualStyleBackColor = true;
    this.btnApply.Click += new EventHandler(this.btnApply_Click);
    componentResourceManager.ApplyResources((object) this.btnExit, "btnExit");
    this.btnExit.Name = "btnExit";
    this.btnExit.UseVisualStyleBackColor = true;
    this.btnExit.Click += new EventHandler(this.btnExit_Click);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.btnGetUsersList, "btnGetUsersList");
    this.btnGetUsersList.Name = "btnGetUsersList";
    this.btnGetUsersList.Click += new EventHandler(this.btnGetUsersList_Click);
    componentResourceManager.ApplyResources((object) this.btnUpdate, "btnUpdate");
    this.btnUpdate.Name = "btnUpdate";
    this.btnUpdate.UseVisualStyleBackColor = true;
    this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
    componentResourceManager.ApplyResources((object) this._statusBar, "_statusBar");
    this._statusBar.Name = "_statusBar";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._statusBar);
    this.Controls.Add((Control) this.splitContainer);
    this.Controls.Add((Control) this.btnGetUsersList);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btnExit);
    this.Controls.Add((Control) this.btnUpdate);
    this.Controls.Add((Control) this.cbDomain);
    this.Controls.Add((Control) this.btnApply);
    this.Name = nameof (LdapSyncConfigForm);
    this.Load += new EventHandler(this.LdapSyncConfigForm_Load);
    this.splitContainer.Panel1.ResumeLayout(false);
    this.splitContainer.Panel2.ResumeLayout(false);
    this.splitContainer.EndInit();
    this.splitContainer.ResumeLayout(false);
    this.groupBox1.ResumeLayout(false);
    this.groupBox2.ResumeLayout(false);
    this.ldapContextMenu.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
