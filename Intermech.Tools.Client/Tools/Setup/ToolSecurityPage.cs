// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Setup.ToolSecurityPage
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Security;
using Intermech.Tools.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Setup;

internal sealed class ToolSecurityPage : UserControl, IPageControl, IDisposable
{
  private bool readWrite;
  private IContainer components;
  private Label lbDescription;
  private PictureBox pbDescription;
  private ListView lvSecurityData;
  private Label lbSecurityData;
  private Button btAdd;
  private Button btChangeGroup;
  private Button btRemove;
  private ColumnHeader chUser;
  private ColumnHeader chSecurityGroup;

  public ToolSecurityPage() => this.InitializeComponent();

  public void Initialize(IPagerControl pagerControl)
  {
    this.readWrite = (ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).IsAdmin;
    this.PopulateSecurityData();
    this.InitPageButtons();
  }

  public bool CanClose => true;

  public void Close()
  {
  }

  public event EventHandler DynamicContentChanged;

  private void PopulateSecurityData()
  {
    List<ToolSecurityPage.UserSecurityItem> userSecurityItemList;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<UserSecurityData> securityData1 = ServiceUtils.GetService<IToolSecurity>((object) sessionKeeper.Session, true).GetSecurityData();
      userSecurityItemList = new List<ToolSecurityPage.UserSecurityItem>(securityData1.Count);
      foreach (UserSecurityData securityData2 in securityData1)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(securityData2.UserId, true);
        userSecurityItemList.Add(new ToolSecurityPage.UserSecurityItem(securityData2, dbObject.Caption));
      }
    }
    this.lvSecurityData.BeginUpdate();
    try
    {
      foreach (ToolSecurityPage.UserSecurityItem secItem in userSecurityItemList)
        this.lvSecurityData.Items.Add(this.MakeItem(secItem));
      if (this.lvSecurityData.Items.Count <= 0)
        return;
      this.lvSecurityData.Items[0].Selected = true;
    }
    finally
    {
      this.lvSecurityData.EndUpdate();
    }
  }

  private void InitPageButtons() => this.btAdd.Enabled = this.readWrite;

  private void InitItemButtons(bool itemSelected)
  {
    bool flag = this.readWrite & itemSelected;
    this.btChangeGroup.Enabled = flag;
    this.btRemove.Enabled = flag;
  }

  private void listView1_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.InitItemButtons(this.lvSecurityData.SelectedItems.Count == 1);
  }

  private void btAdd_Click(object sender, EventArgs e)
  {
    int[] enableTypes = new int[1];
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    enableTypes[0] = service.UsersTypeID;
    object[] objArray = SelectionWindow.Select(LocalizationHolder.rm.GetString("Tools.Client_171"), (IDescriptor) new UsersGroupsDescriptor(), typeof (IDBObjectID), SelectionOptions.Default | SelectionOptions.DisableMultiselect, enableTypes);
    if (objArray != null && objArray.Length == 1)
    {
      IDBObjectID dbObjectId = (IDBObjectID) objArray[0];
      ToolSecurityGroup? nullable = this.SelectGroupForUser();
      if (nullable.HasValue)
      {
        UserSecurityData userSecurityData = new UserSecurityData(dbObjectId.Value, nullable.Value);
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          ServiceUtils.GetService<IToolSecurity>((object) sessionKeeper.Session, true).SaveSecurityData(userSecurityData);
        ListViewItem lvItem = this.FindItem(userSecurityData.UserId);
        if (lvItem != null)
        {
          this.UpdateItem(lvItem, userSecurityData);
        }
        else
        {
          lvItem = this.MakeItem(new ToolSecurityPage.UserSecurityItem(userSecurityData, dbObjectId.Caption));
          this.lvSecurityData.Items.Add(lvItem);
        }
        lvItem.Selected = true;
      }
    }
    this.lvSecurityData.Focus();
  }

  private void btChangeGroup_Click(object sender, EventArgs e)
  {
    ListViewItem selectedItem = this.lvSecurityData.SelectedItems[0];
    ToolSecurityPage.UserSecurityItem tag = (ToolSecurityPage.UserSecurityItem) selectedItem.Tag;
    ToolSecurityGroup? nullable = this.SelectGroupForUser();
    if (nullable.HasValue)
    {
      UserSecurityData userSecurityData = new UserSecurityData(tag.SecurityData.UserId, nullable.Value);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        ServiceUtils.GetService<IToolSecurity>((object) sessionKeeper.Session, true).SaveSecurityData(userSecurityData);
      this.UpdateItem(selectedItem, userSecurityData);
    }
    this.lvSecurityData.Focus();
  }

  private void btRemove_Click(object sender, EventArgs e)
  {
    ListViewItem selectedItem = this.lvSecurityData.SelectedItems[0];
    ToolSecurityPage.UserSecurityItem tag = (ToolSecurityPage.UserSecurityItem) selectedItem.Tag;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ServiceUtils.GetService<IToolSecurity>((object) sessionKeeper.Session, true).RemoveSecurityData(tag.SecurityData.UserId);
    int index = selectedItem.Index;
    this.lvSecurityData.Items.RemoveAt(index);
    if (this.lvSecurityData.Items.Count > 0)
    {
      if (index == this.lvSecurityData.Items.Count)
        --index;
      this.lvSecurityData.Items[index].Selected = true;
    }
    this.lvSecurityData.Focus();
  }

  private ListViewItem MakeItem(ToolSecurityPage.UserSecurityItem secItem)
  {
    return new ListViewItem()
    {
      Text = secItem.DisplayName,
      SubItems = {
        EnumTypeHelper.GetCaption((Enum) secItem.SecurityData.SecurityGroup)
      },
      Tag = (object) secItem
    };
  }

  private void UpdateItem(ListViewItem lvItem, UserSecurityData newSecurityData)
  {
    lvItem.SubItems[1].Text = EnumTypeHelper.GetCaption((Enum) newSecurityData.SecurityGroup);
    ((ToolSecurityPage.UserSecurityItem) lvItem.Tag).SecurityData = newSecurityData;
  }

  private ListViewItem FindItem(long userId)
  {
    foreach (ListViewItem listViewItem in this.lvSecurityData.Items)
    {
      if (((ToolSecurityPage.UserSecurityItem) listViewItem.Tag).SecurityData.UserId == userId)
        return listViewItem;
    }
    return (ListViewItem) null;
  }

  private ToolSecurityGroup? SelectGroupForUser()
  {
    ToolSecurityGroup[] values = (ToolSecurityGroup[]) Enum.GetValues(typeof (ToolSecurityGroup));
    List<LocalId<ToolSecurityGroup>> localIdList = new List<LocalId<ToolSecurityGroup>>(values.Length);
    foreach (ToolSecurityGroup id in values)
      localIdList.Add(new LocalId<ToolSecurityGroup>(id, EnumTypeHelper.GetCaption((Enum) id)));
    SelectItemForm currentControl = new SelectItemForm();
    currentControl.Text = LocalizationHolder.rm.GetString("Tools.Client_172");
    currentControl.Description = LocalizationHolder.rm.GetString("Tools.Client_216");
    currentControl.Items = (IEnumerable) localIdList;
    HelpProvidersClass.SetHelpOptionForControl((Control) currentControl, 1645);
    ToolSecurityGroup? nullable = new ToolSecurityGroup?();
    if (currentControl.ShowDialog() == DialogResult.OK)
      nullable = new ToolSecurityGroup?(((LocalId<ToolSecurityGroup>) currentControl.SelectedItem).Id);
    return nullable;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ToolSecurityPage));
    this.lbDescription = new Label();
    this.pbDescription = new PictureBox();
    this.lvSecurityData = new ListView();
    this.chUser = new ColumnHeader();
    this.chSecurityGroup = new ColumnHeader();
    this.lbSecurityData = new Label();
    this.btAdd = new Button();
    this.btChangeGroup = new Button();
    this.btRemove = new Button();
    ((ISupportInitialize) this.pbDescription).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.lbDescription, "lbDescription");
    this.lbDescription.Name = "lbDescription";
    this.pbDescription.BackColor = SystemColors.Info;
    componentResourceManager.ApplyResources((object) this.pbDescription, "pbDescription");
    this.pbDescription.Name = "pbDescription";
    this.pbDescription.TabStop = false;
    componentResourceManager.ApplyResources((object) this.lvSecurityData, "lvSecurityData");
    this.lvSecurityData.Columns.AddRange(new ColumnHeader[2]
    {
      this.chUser,
      this.chSecurityGroup
    });
    this.lvSecurityData.FullRowSelect = true;
    this.lvSecurityData.GridLines = true;
    this.lvSecurityData.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.lvSecurityData.HideSelection = false;
    this.lvSecurityData.MultiSelect = false;
    this.lvSecurityData.Name = "lvSecurityData";
    this.lvSecurityData.Sorting = SortOrder.Ascending;
    this.lvSecurityData.UseCompatibleStateImageBehavior = false;
    this.lvSecurityData.View = View.Details;
    this.lvSecurityData.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.chUser, "chUser");
    componentResourceManager.ApplyResources((object) this.chSecurityGroup, "chSecurityGroup");
    componentResourceManager.ApplyResources((object) this.lbSecurityData, "lbSecurityData");
    this.lbSecurityData.Name = "lbSecurityData";
    componentResourceManager.ApplyResources((object) this.btAdd, "btAdd");
    this.btAdd.Name = "btAdd";
    this.btAdd.UseVisualStyleBackColor = true;
    this.btAdd.Click += new EventHandler(this.btAdd_Click);
    componentResourceManager.ApplyResources((object) this.btChangeGroup, "btChangeGroup");
    this.btChangeGroup.Name = "btChangeGroup";
    this.btChangeGroup.UseVisualStyleBackColor = true;
    this.btChangeGroup.Click += new EventHandler(this.btChangeGroup_Click);
    componentResourceManager.ApplyResources((object) this.btRemove, "btRemove");
    this.btRemove.Name = "btRemove";
    this.btRemove.UseVisualStyleBackColor = true;
    this.btRemove.Click += new EventHandler(this.btRemove_Click);
    this.Controls.Add((Control) this.btRemove);
    this.Controls.Add((Control) this.btChangeGroup);
    this.Controls.Add((Control) this.btAdd);
    this.Controls.Add((Control) this.lbSecurityData);
    this.Controls.Add((Control) this.lvSecurityData);
    this.Controls.Add((Control) this.lbDescription);
    this.Controls.Add((Control) this.pbDescription);
    this.MinimumSize = new Size(700, 400);
    this.Name = nameof (ToolSecurityPage);
    componentResourceManager.ApplyResources((object) this, "$this");
    ((ISupportInitialize) this.pbDescription).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private class UserSecurityItem
  {
    private UserSecurityData securityData;
    private string displayName;

    public UserSecurityItem(UserSecurityData securityData, string displayName)
    {
      this.securityData = securityData;
      this.displayName = displayName;
    }

    public UserSecurityData SecurityData
    {
      get => this.securityData;
      set => this.securityData = value;
    }

    public string DisplayName => this.displayName;
  }
}
