// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.PropertyPages.MailSettingsControl
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Bars;
using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator.PropertyPages;

public class MailSettingsControl : UserControl, IPropertyPage, IPropertyPageSearchOptionEvents
{
  private bool _changed;
  private Guid _currentServer = Guid.Empty;
  private Dictionary<Guid, List<AccauntUserInfo>> _accauntUsers;
  private bool _first = true;
  private IContainer components;
  private SplitContainer splitContainer1;
  private Intermech.Bars.ToolBar toolBar1;
  private ButtonItem biAddServer;
  private ButtonItem biDelServer;
  private ListView lvServers;
  private ColumnHeader columnHeader1;
  private TabControl tabControl1;
  private TabPage tabPage1;
  private PropertyGrid pgServerProps;
  private TabPage tabPage2;
  private ListView lvAccaunts;
  private ColumnHeader columnHeader2;
  private ColumnHeader columnHeader3;
  private ImageList imageList1;
  private Intermech.Bars.ToolBar toolBar2;
  private ButtonItem biAddAccaunt;
  private ButtonItem biEditAccaunt;
  private ButtonItem biAccauntUsers;
  private ButtonItem biDelAccaunt;
  private ButtonItem biAccauntCheck;

  public event EventHandler Changed;

  public MailSettingsControl()
  {
    this.InitializeComponent();
    Holder.NotificationService.Subscribe("EmailAccauntChanged", new NotificationEventHandler(this.OnEmailAccauntChanged));
  }

  private void OnEmailAccauntChanged(object sender, NotificationEventArgs e)
  {
    if (sender == this)
      return;
    this.ReloadData();
  }

  private void ReloadData()
  {
    this.lvServers.SelectedIndexChanged -= new EventHandler(this.Servers_SelectedIndexChanged);
    try
    {
      this.lvServers.Items.Clear();
      this.lvAccaunts.Items.Clear();
      this.pgServerProps.SelectedObject = (object) null;
      if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IEmailService)) is IEmailService customService)
      {
        EmailServer[] servers = customService.Servers;
        if (servers == null)
          return;
        for (int index = 0; index < servers.Length; ++index)
        {
          EmailServer emailServer = servers[index];
          this.AddServerItem(emailServer.Name, emailServer.Guid);
        }
      }
      if (this.lvServers.Items.Count <= 0)
        return;
      this.lvServers.Items[0].Focused = true;
      this.lvServers.Items[0].Selected = true;
      this.Servers_SelectedIndexChanged((object) this, new EventArgs());
    }
    finally
    {
      this.RefreshAccauntButtons();
      this.lvServers.SelectedIndexChanged += new EventHandler(this.Servers_SelectedIndexChanged);
    }
  }

  private ListViewItem AddServerItem(string name, Guid guid)
  {
    ListViewItem listViewItem = new ListViewItem(name)
    {
      ImageIndex = 5,
      Tag = (object) guid
    };
    this.lvServers.Items.Add(listViewItem);
    return listViewItem;
  }

  private ListViewItem AddAccauntItem(EmailAccaunt accaunt)
  {
    ListViewItem listViewItem = new ListViewItem(new string[1]
    {
      accaunt.Email
    })
    {
      ImageIndex = 4,
      Tag = (object) accaunt
    };
    this.lvAccaunts.Items.Add(listViewItem);
    return listViewItem;
  }

  public PropertyPageType Type => PropertyPageType.Control;

  public object Control => (object) this;

  public string PageName => LocalizationHolder.rm.GetString("DatabaseConfigurator_227");

  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  public void Apply()
  {
    if (!this._changed || this._currentServer == Guid.Empty)
      return;
    if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IEmailService)) is IEmailService customService)
      this.SetServer(customService);
    Holder.NotificationService.FireEvent((object) this, new NotificationEventArgs("EmailAccauntChanged"));
    this._changed = false;
  }

  public void Cancel()
  {
    this.ReloadData();
    if (this.lvServers.Items.Count > 0)
    {
      this.lvServers.Items[0].Focused = true;
      this.lvServers.Items[0].Selected = true;
    }
    this._changed = false;
  }

  public string HelpTopicID => "2493";

  public List<string> GetOptionNames()
  {
    return !(this.Control is System.Windows.Forms.Control control) ? new List<string>() : IPropertyPageHelper.GetOptionNames(control);
  }

  private void SetServer(IEmailService esService)
  {
    if (this.pgServerProps.SelectedObject == null)
      return;
    Dictionary<EmailAccaunt, List<AccauntUserInfo>> accaunts = new Dictionary<EmailAccaunt, List<AccauntUserInfo>>(this.lvAccaunts.Items.Count);
    for (int index = 0; index < this.lvAccaunts.Items.Count; ++index)
    {
      EmailAccaunt tag = (EmailAccaunt) this.lvAccaunts.Items[index].Tag;
      accaunts.Add(tag, this._accauntUsers[tag.Guid]);
    }
    esService.SetServer((EmailServer) this.pgServerProps.SelectedObject, accaunts);
  }

  private void AddServer_Click(object sender, EventArgs e)
  {
    using (NewEmailServerForm newEmailServerForm = new NewEmailServerForm())
    {
      if (newEmailServerForm.ShowDialog() != DialogResult.OK)
        return;
      EmailServer newServer = new EmailServer()
      {
        Guid = Guid.NewGuid(),
        Name = newEmailServerForm.ServerName,
        POP3Server = "pop." + newEmailServerForm.ServerName,
        SMTPServer = "smtp." + newEmailServerForm.ServerName
      };
      if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IEmailService)) is IEmailService customService)
        customService.AddServer(newServer);
      ListViewItem listViewItem = this.AddServerItem(newServer.Name, newServer.Guid);
      listViewItem.Focused = true;
      listViewItem.Selected = true;
    }
  }

  private void DelServer_Click(object sender, EventArgs e)
  {
    if (this.lvServers.SelectedItems.Count == 0 || this.lvServers.SelectedItems[0].Tag == null || IMMessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_228"), LocalizationHolder.rm.GetString("DatabaseConfigurator_229"), MessageBoxButtons.YesNo, IMMessageBoxImage.Question) != DialogResult.Yes)
      return;
    if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IEmailService)) is IEmailService customService)
      customService.DeleteServer((Guid) this.lvServers.SelectedItems[0].Tag);
    this.lvServers.Items.Remove(this.lvServers.SelectedItems[0]);
    this.ReloadData();
  }

  private void Servers_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lvServers.SelectedItems.Count == 0 || this.lvServers.SelectedItems[0].Tag == null || !((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IEmailService)) is IEmailService customService))
      return;
    if (this._changed && IMMessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_230"), LocalizationHolder.rm.GetString("DatabaseConfigurator_231"), MessageBoxButtons.YesNo, IMMessageBoxImage.Question) == DialogResult.Yes)
      this.SetServer(customService);
    EmailServer server = customService.GetServer((Guid) this.lvServers.SelectedItems[0].Tag);
    this.pgServerProps.SelectedObject = (object) server;
    this._currentServer = server.Guid;
    this.lvAccaunts.Items.Clear();
    EmailAccaunt[] accaunts = customService.GetAccaunts(server.Guid);
    this._accauntUsers = new Dictionary<Guid, List<AccauntUserInfo>>(accaunts != null ? accaunts.Length : 0);
    if (accaunts != null)
    {
      for (int index = 0; index < accaunts.Length; ++index)
      {
        this.AddAccauntItem(accaunts[index]);
        List<AccauntUserInfo> accauntUsers = customService.GetAccauntUsers(server.Guid, accaunts[index].Guid);
        this._accauntUsers.Add(accaunts[index].Guid, accauntUsers ?? new List<AccauntUserInfo>());
      }
    }
    this._changed = false;
    this.RefreshAccauntButtons();
  }

  private void RefreshAccauntButtons()
  {
    bool flag = this.lvAccaunts.Items.Count > 0 && this.lvAccaunts.FocusedItem != null && this.lvAccaunts.FocusedItem.Tag != null;
    this.biAddAccaunt.Enabled = this._currentServer != Guid.Empty;
    this.biAccauntUsers.Enabled = flag;
    this.biDelAccaunt.Enabled = flag;
    this.biEditAccaunt.Enabled = flag;
    this.biAccauntCheck.Enabled = flag;
  }

  private void ServerProps_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
  {
    if (this._currentServer == Guid.Empty)
      return;
    this._changed = true;
    this.Changed((object) this, new EventArgs());
  }

  private void MailSettingsControl_Load(object sender, EventArgs e)
  {
    if (!this._first)
      return;
    this.ReloadData();
    this._first = false;
  }

  private void AddAccaunt_Click(object sender, EventArgs e)
  {
    using (AccauntForm accauntForm = new AccauntForm(LocalizationHolder.rm.GetString("DatabaseConfigurator_232")))
    {
      if (accauntForm.ShowDialog() != DialogResult.OK)
        return;
      EmailAccaunt emailAccaunt = new EmailAccaunt()
      {
        Guid = Guid.NewGuid(),
        Email = accauntForm.Email,
        Login = accauntForm.Login,
        Password = accauntForm.Password
      };
      if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IEmailService)) is IEmailService customService)
        customService.CheckAccaunt(this._currentServer, emailAccaunt);
      ListViewItem listViewItem = this.AddAccauntItem(emailAccaunt);
      listViewItem.Focused = true;
      listViewItem.Selected = true;
      this.RefreshAccauntButtons();
      this._accauntUsers.Add(emailAccaunt.Guid, new List<AccauntUserInfo>());
      this._changed = true;
      this.Changed((object) this, new EventArgs());
    }
  }

  private void EditAccaunt_Click(object sender, EventArgs e)
  {
    if (this.lvAccaunts.FocusedItem == null || this.lvAccaunts.FocusedItem.Tag == null)
      return;
    EmailAccaunt tag = this.lvAccaunts.FocusedItem.Tag as EmailAccaunt;
    using (AccauntForm accauntForm = new AccauntForm(LocalizationHolder.rm.GetString("DatabaseConfigurator_233")))
    {
      accauntForm.Email = tag.Email;
      accauntForm.Login = tag.Login;
      accauntForm.Password = tag.Password;
      if (accauntForm.ShowDialog() != DialogResult.OK)
        return;
      tag.Email = accauntForm.Email;
      tag.Login = accauntForm.Login;
      tag.Password = accauntForm.Password;
      if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IEmailService)) is IEmailService customService)
        customService.CheckAccaunt(this._currentServer, tag);
      this.lvAccaunts.FocusedItem.Text = tag.Email;
      this._changed = true;
      this.Changed((object) this, new EventArgs());
    }
  }

  private void AccauntUsers_Click(object sender, EventArgs e)
  {
    if (this.lvAccaunts.FocusedItem == null || this.lvAccaunts.FocusedItem.Tag == null)
      return;
    EmailAccaunt tag = (EmailAccaunt) this.lvAccaunts.FocusedItem.Tag;
    using (AccauntUsersForm accauntUsersForm = new AccauntUsersForm(this._accauntUsers[tag.Guid]))
    {
      int num = (int) accauntUsersForm.ShowDialog();
      if (!accauntUsersForm.Changed)
        return;
      this._accauntUsers[tag.Guid] = accauntUsersForm.Users;
      this._changed = true;
      this.Changed((object) this, new EventArgs());
    }
  }

  private void DelAccaunt_Click(object sender, EventArgs e)
  {
    if (this.lvAccaunts.FocusedItem == null || this.lvAccaunts.FocusedItem.Tag == null)
      return;
    this._accauntUsers.Remove(((EmailAccaunt) this.lvAccaunts.FocusedItem.Tag).Guid);
    this.lvAccaunts.FocusedItem.Remove();
    if (this.lvAccaunts.Items.Count > 0)
      this.lvAccaunts.FocusedItem.Selected = true;
    this.RefreshAccauntButtons();
    this._changed = true;
    this.Changed((object) this, new EventArgs());
  }

  private void Accaunts_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.RefreshAccauntButtons();
  }

  private void AccauntCheck_Click(object sender, EventArgs e)
  {
    if (this._changed)
      this.Apply();
    using (CheckAccauntForm checkAccauntForm = new CheckAccauntForm((EmailAccaunt) this.lvAccaunts.FocusedItem.Tag))
    {
      int num = (int) checkAccauntForm.ShowDialog();
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MailSettingsControl));
    this.splitContainer1 = new SplitContainer();
    this.lvServers = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.imageList1 = new ImageList(this.components);
    this.toolBar1 = new Intermech.Bars.ToolBar();
    this.biAddServer = new ButtonItem();
    this.biDelServer = new ButtonItem();
    this.tabControl1 = new TabControl();
    this.tabPage1 = new TabPage();
    this.pgServerProps = new PropertyGrid();
    this.tabPage2 = new TabPage();
    this.lvAccaunts = new ListView();
    this.columnHeader2 = new ColumnHeader();
    this.columnHeader3 = new ColumnHeader();
    this.toolBar2 = new Intermech.Bars.ToolBar();
    this.biAddAccaunt = new ButtonItem();
    this.biEditAccaunt = new ButtonItem();
    this.biAccauntUsers = new ButtonItem();
    this.biAccauntCheck = new ButtonItem();
    this.biDelAccaunt = new ButtonItem();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.tabControl1.SuspendLayout();
    this.tabPage1.SuspendLayout();
    this.tabPage2.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((System.Windows.Forms.Control) this.lvServers);
    this.splitContainer1.Panel1.Controls.Add((System.Windows.Forms.Control) this.toolBar1);
    this.splitContainer1.Panel2.Controls.Add((System.Windows.Forms.Control) this.tabControl1);
    this.lvServers.Columns.AddRange(new ColumnHeader[1]
    {
      this.columnHeader1
    });
    componentResourceManager.ApplyResources((object) this.lvServers, "lvServers");
    this.lvServers.MultiSelect = false;
    this.lvServers.Name = "lvServers";
    this.lvServers.SmallImageList = this.imageList1;
    this.lvServers.UseCompatibleStateImageBehavior = false;
    this.lvServers.View = View.Details;
    this.lvServers.SelectedIndexChanged += new EventHandler(this.Servers_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.columnHeader1, "columnHeader1");
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "users3.png");
    this.imageList1.Images.SetKeyName(1, "add2.png");
    this.imageList1.Images.SetKeyName(2, "delete2.png");
    this.imageList1.Images.SetKeyName(3, "edit.png");
    this.imageList1.Images.SetKeyName(4, "id_card.png");
    this.imageList1.Images.SetKeyName(5, "mail_server.png");
    this.imageList1.Images.SetKeyName(6, "mail_preferences.png");
    this.toolBar1.FullMenus = true;
    this.toolBar1.Guid = new Guid("42adbd1f-cc14-49d1-8fe7-d73ad9394c8c");
    this.toolBar1.Hidden = false;
    this.toolBar1.ImageList = this.imageList1;
    this.toolBar1.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.biAddServer,
      (ToolbarItemBase) this.biDelServer
    });
    componentResourceManager.ApplyResources((object) this.toolBar1, "toolBar1");
    this.toolBar1.Name = "toolBar1";
    componentResourceManager.ApplyResources((object) this.biAddServer, "biAddServer");
    this.biAddServer.ImageIndex = 1;
    this.biAddServer.Click += new EventHandler(this.AddServer_Click);
    componentResourceManager.ApplyResources((object) this.biDelServer, "biDelServer");
    this.biDelServer.ImageIndex = 2;
    this.biDelServer.Click += new EventHandler(this.DelServer_Click);
    this.tabControl1.Controls.Add((System.Windows.Forms.Control) this.tabPage1);
    this.tabControl1.Controls.Add((System.Windows.Forms.Control) this.tabPage2);
    componentResourceManager.ApplyResources((object) this.tabControl1, "tabControl1");
    this.tabControl1.Name = "tabControl1";
    this.tabControl1.SelectedIndex = 0;
    this.tabPage1.Controls.Add((System.Windows.Forms.Control) this.pgServerProps);
    componentResourceManager.ApplyResources((object) this.tabPage1, "tabPage1");
    this.tabPage1.Name = "tabPage1";
    this.tabPage1.UseVisualStyleBackColor = true;
    this.pgServerProps.CategoryForeColor = SystemColors.InactiveCaptionText;
    componentResourceManager.ApplyResources((object) this.pgServerProps, "pgServerProps");
    this.pgServerProps.Name = "pgServerProps";
    this.pgServerProps.PropertyValueChanged += new PropertyValueChangedEventHandler(this.ServerProps_PropertyValueChanged);
    this.tabPage2.Controls.Add((System.Windows.Forms.Control) this.lvAccaunts);
    this.tabPage2.Controls.Add((System.Windows.Forms.Control) this.toolBar2);
    componentResourceManager.ApplyResources((object) this.tabPage2, "tabPage2");
    this.tabPage2.Name = "tabPage2";
    this.tabPage2.UseVisualStyleBackColor = true;
    this.lvAccaunts.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnHeader2,
      this.columnHeader3
    });
    componentResourceManager.ApplyResources((object) this.lvAccaunts, "lvAccaunts");
    this.lvAccaunts.GridLines = true;
    this.lvAccaunts.MultiSelect = false;
    this.lvAccaunts.Name = "lvAccaunts";
    this.lvAccaunts.SmallImageList = this.imageList1;
    this.lvAccaunts.UseCompatibleStateImageBehavior = false;
    this.lvAccaunts.View = View.List;
    this.lvAccaunts.SelectedIndexChanged += new EventHandler(this.Accaunts_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.columnHeader2, "columnHeader2");
    componentResourceManager.ApplyResources((object) this.columnHeader3, "columnHeader3");
    this.toolBar2.FullMenus = true;
    this.toolBar2.Guid = new Guid("423c9f9e-2ee9-485f-b711-a320f2746d28");
    this.toolBar2.Hidden = false;
    this.toolBar2.ImageList = this.imageList1;
    this.toolBar2.Items.AddRange(new ToolbarItemBase[5]
    {
      (ToolbarItemBase) this.biAddAccaunt,
      (ToolbarItemBase) this.biEditAccaunt,
      (ToolbarItemBase) this.biAccauntUsers,
      (ToolbarItemBase) this.biAccauntCheck,
      (ToolbarItemBase) this.biDelAccaunt
    });
    componentResourceManager.ApplyResources((object) this.toolBar2, "toolBar2");
    this.toolBar2.Name = "toolBar2";
    componentResourceManager.ApplyResources((object) this.biAddAccaunt, "biAddAccaunt");
    this.biAddAccaunt.ImageIndex = 1;
    this.biAddAccaunt.Click += new EventHandler(this.AddAccaunt_Click);
    this.biEditAccaunt.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.biEditAccaunt, "biEditAccaunt");
    this.biEditAccaunt.ImageIndex = 3;
    this.biEditAccaunt.Click += new EventHandler(this.EditAccaunt_Click);
    componentResourceManager.ApplyResources((object) this.biAccauntUsers, "biAccauntUsers");
    this.biAccauntUsers.ImageIndex = 0;
    this.biAccauntUsers.Click += new EventHandler(this.AccauntUsers_Click);
    componentResourceManager.ApplyResources((object) this.biAccauntCheck, "biAccauntCheck");
    this.biAccauntCheck.ImageIndex = 6;
    this.biAccauntCheck.Click += new EventHandler(this.AccauntCheck_Click);
    this.biDelAccaunt.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.biDelAccaunt, "biDelAccaunt");
    this.biDelAccaunt.ImageIndex = 2;
    this.biDelAccaunt.Click += new EventHandler(this.DelAccaunt_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((System.Windows.Forms.Control) this.splitContainer1);
    this.Name = nameof (MailSettingsControl);
    this.Load += new EventHandler(this.MailSettingsControl_Load);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.tabControl1.ResumeLayout(false);
    this.tabPage1.ResumeLayout(false);
    this.tabPage2.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
