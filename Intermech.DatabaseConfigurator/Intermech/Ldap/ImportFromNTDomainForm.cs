// Decompiled with JetBrains decompiler
// Type: Intermech.Ldap.ImportFromNTDomainForm
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.DatabaseConfigurator;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Ldap;

internal class ImportFromNTDomainForm : Form
{
  private Panel _upPanel;
  private Label label1;
  private GroupBox _groupBox1;
  private Panel _downPanel;
  private ListView _list;
  private Panel _buttonsPanel;
  private Button _bClose;
  private Button _bImport;
  private ColumnHeader _cUser;
  private ColumnHeader _cFullName;
  private ColumnHeader _cDescription;
  private Button _bGetUsersList;
  private long _objID;
  private StatusBar _statusBar;
  private DirectoryEntry _domainEntry;
  private ImportFromNTDomainForm.ProviderUsage _provider = ImportFromNTDomainForm.ProviderUsage.LDAP;
  private int sortColumn = -1;
  private string defaultCatalogName = string.Empty;
  private string LoginPostfix = string.Empty;
  private ColumnHeader _cSID;
  private ContextMenuStrip contextMenuStrip;
  private IContainer components;
  private ToolStripMenuItem cmdSelectAll;
  private ToolStripMenuItem cmdDeselectAll;
  private ColumnHeader _cEmail;
  private Panel panel1;
  private Button bSearch;
  private ComboBox cbSearch;
  private ComboBox _domain;
  private bool blockOnCheck;

  public ImportFromNTDomainForm(long objID)
  {
    this._objID = objID;
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1099);
    this._domain.Text = "";
  }

  private void UpdateControlsStatus() => this._bImport.Enabled = this._list.CheckedItems.Count > 0;

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImportFromNTDomainForm));
    this._upPanel = new Panel();
    this._domain = new ComboBox();
    this._bGetUsersList = new Button();
    this.label1 = new Label();
    this._groupBox1 = new GroupBox();
    this._list = new ListView();
    this._cUser = new ColumnHeader();
    this._cFullName = new ColumnHeader();
    this._cDescription = new ColumnHeader();
    this._cSID = new ColumnHeader();
    this._cEmail = new ColumnHeader();
    this.contextMenuStrip = new ContextMenuStrip(this.components);
    this.cmdSelectAll = new ToolStripMenuItem();
    this.cmdDeselectAll = new ToolStripMenuItem();
    this.panel1 = new Panel();
    this.bSearch = new Button();
    this.cbSearch = new ComboBox();
    this._downPanel = new Panel();
    this._buttonsPanel = new Panel();
    this._bClose = new Button();
    this._bImport = new Button();
    this._statusBar = new StatusBar();
    this._domainEntry = new DirectoryEntry();
    this._upPanel.SuspendLayout();
    this._groupBox1.SuspendLayout();
    this.contextMenuStrip.SuspendLayout();
    this.panel1.SuspendLayout();
    this._downPanel.SuspendLayout();
    this._buttonsPanel.SuspendLayout();
    this.SuspendLayout();
    this._upPanel.Controls.Add((Control) this._domain);
    this._upPanel.Controls.Add((Control) this._bGetUsersList);
    this._upPanel.Controls.Add((Control) this.label1);
    componentResourceManager.ApplyResources((object) this._upPanel, "_upPanel");
    this._upPanel.Name = "_upPanel";
    componentResourceManager.ApplyResources((object) this._domain, "_domain");
    this._domain.FormattingEnabled = true;
    this._domain.Name = "_domain";
    this._domain.Sorted = true;
    this._domain.DropDown += new EventHandler(this.domainCB_DropDown);
    this._domain.SelectedValueChanged += new EventHandler(this._domain_SelectedValueChanged);
    this._domain.TextChanged += new EventHandler(this.domainCB_TextChanged);
    componentResourceManager.ApplyResources((object) this._bGetUsersList, "_bGetUsersList");
    this._bGetUsersList.Name = "_bGetUsersList";
    this._bGetUsersList.Click += new EventHandler(this._GetUsersList_Click);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this._groupBox1.Controls.Add((Control) this._list);
    this._groupBox1.Controls.Add((Control) this.panel1);
    componentResourceManager.ApplyResources((object) this._groupBox1, "_groupBox1");
    this._groupBox1.FlatStyle = FlatStyle.System;
    this._groupBox1.Name = "_groupBox1";
    this._groupBox1.TabStop = false;
    this._list.CheckBoxes = true;
    this._list.Columns.AddRange(new ColumnHeader[5]
    {
      this._cUser,
      this._cFullName,
      this._cDescription,
      this._cSID,
      this._cEmail
    });
    this._list.ContextMenuStrip = this.contextMenuStrip;
    componentResourceManager.ApplyResources((object) this._list, "_list");
    this._list.FullRowSelect = true;
    this._list.GridLines = true;
    this._list.HideSelection = false;
    this._list.MultiSelect = false;
    this._list.Name = "_list";
    this._list.Sorting = SortOrder.Ascending;
    this._list.UseCompatibleStateImageBehavior = false;
    this._list.View = View.Details;
    this._list.ColumnClick += new ColumnClickEventHandler(this._list_ColumnClick);
    this._list.ItemChecked += new ItemCheckedEventHandler(this._list_ItemChecked);
    componentResourceManager.ApplyResources((object) this._cUser, "_cUser");
    componentResourceManager.ApplyResources((object) this._cFullName, "_cFullName");
    componentResourceManager.ApplyResources((object) this._cDescription, "_cDescription");
    componentResourceManager.ApplyResources((object) this._cSID, "_cSID");
    componentResourceManager.ApplyResources((object) this._cEmail, "_cEmail");
    this.contextMenuStrip.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.cmdSelectAll,
      (ToolStripItem) this.cmdDeselectAll
    });
    this.contextMenuStrip.Name = "contextMenuStrip";
    componentResourceManager.ApplyResources((object) this.contextMenuStrip, "contextMenuStrip");
    this.cmdSelectAll.Name = "cmdSelectAll";
    componentResourceManager.ApplyResources((object) this.cmdSelectAll, "cmdSelectAll");
    this.cmdSelectAll.Click += new EventHandler(this.cmdSelectAll_Click);
    this.cmdDeselectAll.Name = "cmdDeselectAll";
    componentResourceManager.ApplyResources((object) this.cmdDeselectAll, "cmdDeselectAll");
    this.cmdDeselectAll.Click += new EventHandler(this.cmdDeselectAll_Click);
    this.panel1.Controls.Add((Control) this.bSearch);
    this.panel1.Controls.Add((Control) this.cbSearch);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.bSearch, "bSearch");
    this.bSearch.Name = "bSearch";
    this.bSearch.UseVisualStyleBackColor = true;
    this.bSearch.Click += new EventHandler(this.bSearch_Click);
    componentResourceManager.ApplyResources((object) this.cbSearch, "cbSearch");
    this.cbSearch.FormattingEnabled = true;
    this.cbSearch.Name = "cbSearch";
    this.cbSearch.KeyPress += new KeyPressEventHandler(this.cbSearch_KeyPress);
    this._downPanel.Controls.Add((Control) this._buttonsPanel);
    componentResourceManager.ApplyResources((object) this._downPanel, "_downPanel");
    this._downPanel.Name = "_downPanel";
    this._buttonsPanel.Controls.Add((Control) this._bClose);
    this._buttonsPanel.Controls.Add((Control) this._bImport);
    componentResourceManager.ApplyResources((object) this._buttonsPanel, "_buttonsPanel");
    this._buttonsPanel.Name = "_buttonsPanel";
    this._bClose.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this._bClose, "_bClose");
    this._bClose.Name = "_bClose";
    componentResourceManager.ApplyResources((object) this._bImport, "_bImport");
    this._bImport.Name = "_bImport";
    this._bImport.Click += new EventHandler(this._bImport_Click);
    componentResourceManager.ApplyResources((object) this._statusBar, "_statusBar");
    this._statusBar.Name = "_statusBar";
    this._domainEntry.AuthenticationType = AuthenticationTypes.ReadonlyServer;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this._bClose;
    this.Controls.Add((Control) this._groupBox1);
    this.Controls.Add((Control) this._downPanel);
    this.Controls.Add((Control) this._upPanel);
    this.Controls.Add((Control) this._statusBar);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ImportFromNTDomainForm);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this.Tag = (object) "    ";
    this.Closed += new EventHandler(this.ImportFromNTDomainForm_Closed);
    this.Load += new EventHandler(this.ImportFromNTDomainForm_Load);
    this._upPanel.ResumeLayout(false);
    this._groupBox1.ResumeLayout(false);
    this.contextMenuStrip.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this._downPanel.ResumeLayout(false);
    this._buttonsPanel.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  protected override void Dispose(bool disposing) => base.Dispose(disposing);

  private string GetWinNTPath() => $"WinNT://{this._domain.Text}";

  private void GetUsersFromWinNT(DirectoryEntry entry)
  {
  }

  private AttributeValues[] AddUserAttributeValues(DirectoryEntry entry, IUserSession session)
  {
    return new List<AttributeValues>().ToArray();
  }

  private void FillUserToList(Hashtable hash)
  {
    try
    {
      string empty1 = string.Empty;
      ListViewItem listViewItem = new ListViewItem();
      string str = hash[(object) LdapConsts.ADSAMAccountName].ToString();
      listViewItem.Text = str + this.LoginPostfix;
      SearchResult searchResult = hash[(object) LdapConsts._SearchResult_] as SearchResult;
      if (searchResult.Properties.Contains(LdapConsts.ADDisplayName))
      {
        string text = searchResult.Properties[LdapConsts.ADDisplayName][0].ToString();
        listViewItem.SubItems.Add(text);
      }
      else
        listViewItem.SubItems.Add(string.Empty);
      if (searchResult.Properties.Contains(LdapConsts.ADDescription))
      {
        string text = searchResult.Properties[LdapConsts.ADDescription][0].ToString();
        listViewItem.SubItems.Add(text);
      }
      else
        listViewItem.SubItems.Add(string.Empty);
      if (searchResult.Properties.Contains(LdapConsts.ADObjectSID))
      {
        byte[] binaryForm = searchResult.Properties[LdapConsts.ADObjectSID][0] as byte[];
        listViewItem.SubItems.Add(new SecurityIdentifier(binaryForm, 0).Value);
      }
      else
        listViewItem.SubItems.Add(string.Empty);
      string empty2 = string.Empty;
      if (searchResult.Properties.Contains(LdapConsts.SearchResultMail))
      {
        if (searchResult.Properties[LdapConsts.SearchResultMail].Count > 0)
        {
          try
          {
            empty2 = Convert.ToString(searchResult.Properties[LdapConsts.SearchResultMail][0]);
          }
          catch
          {
          }
        }
      }
      listViewItem.SubItems.Add(empty2);
      this.blockOnCheck = true;
      try
      {
        this._list.Items.Add(listViewItem).Tag = (object) hash;
      }
      finally
      {
        this.blockOnCheck = false;
      }
    }
    catch
    {
    }
  }

  private AttributeValues[] GetUserAttributeValues(Hashtable hash, IUserSession session)
  {
    ArrayList arrayList = new ArrayList();
    string empty1 = string.Empty;
    SearchResult searchResult = hash[(object) LdapConsts._SearchResult_] as SearchResult;
    AttributeValues attributeValues1 = new AttributeValues(session.IdentHelper.LoginNameID);
    string str = hash[(object) LdapConsts.ADSAMAccountName].ToString();
    attributeValues1.Values = new object[1]
    {
      (object) (str + this.LoginPostfix)
    };
    arrayList.Add((object) attributeValues1);
    AttributeValues attributeValues2 = new AttributeValues(session.IdentHelper.UserNameID);
    if (searchResult.Properties.Contains(LdapConsts.ADDisplayName))
    {
      ResultPropertyValueCollection property = searchResult.Properties[LdapConsts.ADDisplayName];
      attributeValues2.Values = property.Cast<object>().ToArray<object>();
    }
    else
      attributeValues2.Values = new object[1]
      {
        (object) string.Empty
      };
    arrayList.Add((object) attributeValues2);
    IDBAttributeType attributeType1 = session.GetAttributeType(new Guid("cad0001c-306c-11d8-b4e9-00304f19f545"), false);
    if (attributeType1 != null && searchResult.Properties.Contains(LdapConsts.ADDescription))
    {
      AttributeValues attributeValues3 = new AttributeValues(attributeType1.AttributeID);
      ResultPropertyValueCollection property = searchResult.Properties[LdapConsts.ADDescription];
      attributeValues3.Values = property.Cast<object>().ToArray<object>();
      arrayList.Add((object) attributeValues3);
    }
    IDBAttributeType attributeType2 = session.GetAttributeType(new Guid("cadd93c1-306c-11d8-b4e9-00304f19f545"), false);
    if (attributeType2 != null && searchResult.Properties.Contains(LdapConsts.ADObjectSID))
    {
      AttributeValues attributeValues4 = new AttributeValues(attributeType2.AttributeID);
      ResultPropertyValueCollection property = searchResult.Properties[LdapConsts.ADObjectSID];
      attributeValues4.Values = new object[1]
      {
        (object) new SecurityIdentifier((byte[]) property[0], 0).Value
      };
      arrayList.Add((object) attributeValues4);
    }
    IDBAttributeType attributeType3 = session.GetAttributeType(new Guid("cad002de-306c-11d8-b4e9-00304f19f545"), false);
    if (attributeType3 != null)
    {
      string empty2 = string.Empty;
      if (searchResult.Properties.Contains(LdapConsts.SearchResultMail))
      {
        if (searchResult.Properties[LdapConsts.SearchResultMail].Count > 0)
        {
          try
          {
            empty2 = Convert.ToString(searchResult.Properties[LdapConsts.SearchResultMail][0]);
          }
          catch
          {
          }
        }
      }
      if (empty2 != string.Empty)
        arrayList.Add((object) new AttributeValues(attributeType3.AttributeID)
        {
          Values = new object[1]{ (object) empty2 }
        });
    }
    AttributeValues[] userAttributeValues = new AttributeValues[arrayList.Count];
    arrayList.CopyTo((Array) userAttributeValues);
    return userAttributeValues;
  }

  private void EnumerateUsers(string domainName)
  {
    string ldap = LdapProcs.DomainNameToLdap(domainName, true);
    List<string> ouList = LdapProcs.GetOUList(domainName, SearchScope.OneLevel);
    ouList.Insert(0, ldap);
    try
    {
      for (int index = 0; index < ouList.Count; ++index)
      {
        using (DirectoryEntry searchRoot = new DirectoryEntry(ouList[index]))
        {
          using (DirectorySearcher search = new DirectorySearcher(searchRoot))
          {
            LdapProcs.InitSearcher(search);
            search.Filter = "(objectCategory=person)";
            foreach (SearchResult searchResult in search.FindAll())
            {
              if (searchResult.Properties.Contains(LdapConsts.ADSAMAccountName))
                this.FillUserToList(new Hashtable()
                {
                  [(object) LdapConsts.ADSAMAccountName] = (object) searchResult.Properties[LdapConsts.ADSAMAccountName][0].ToString(),
                  [(object) LdapConsts._SearchResult_] = (object) searchResult
                });
            }
          }
        }
      }
    }
    catch (DirectoryServicesCOMException ex)
    {
      throw;
    }
  }

  private void _GetUsersList_Click(object sender, EventArgs e)
  {
    if (!this.CheckDefaultDomain())
      return;
    this._list.BeginUpdate();
    this._list.Items.Clear();
    try
    {
      this.AssignLoginPostfix(this._domain.Text);
      this._list.Sorting = SortOrder.None;
      this.EnumerateUsers(this._domain.Text);
    }
    finally
    {
      this._list.EndUpdate();
      this.UpdateControlsStatus();
    }
    ImportFromNTDomainForm.ChechLoginNamesLengthAndWarning(this._list);
  }

  private bool CheckDefaultDomain()
  {
    if (this.defaultCatalogName == string.Empty && this._domain.Text != string.Empty)
    {
      switch (IMMessageBox.Show("Вопрос", $"Домен по умолчанию не назначен.\nДля доменов по умолчанию имя входа пользователя не содержит постфикс @<имя домена>.\nНазначить {this._domain.Text} доменом по умолчанию?", MessageBoxButtons.YesNoCancel, IMMessageBoxImage.Question))
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
              this.defaultCatalogName = this._domain.Text;
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

  public static int ChechLoginNamesLengthAndWarning(ListView list, bool warning = true)
  {
    long maxDBSz;
    long maxDetectedSz;
    int namesCountByLength = ImportFromNTDomainForm.GetBadLoginNamesCountByLength(list, out maxDBSz, out maxDetectedSz);
    if (namesCountByLength > 0 & warning)
    {
      int num = (int) IMMessageBox.Show("Внимание", $"Обнаружено {namesCountByLength} импортируемых имен входа, имеющих длину, превышающую максимально разрешенную в IPS в {maxDBSz} символов.\nТребуется увеличение максимальной длины атрибута \"Имя входа пользователя\" до {maxDetectedSz} или больше.", MessageBoxButtons.OK, IMMessageBoxImage.Warning);
    }
    return namesCountByLength;
  }

  private static int GetBadLoginNamesCountByLength(
    ListView list,
    out long maxDBSz,
    out long maxDetectedSz)
  {
    int namesCountByLength = 0;
    maxDetectedSz = 0L;
    IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(new Guid("cad00018-306c-11d8-b4e9-00304f19f545"), true);
    maxDBSz = attributeType.SizeType;
    for (int index = 0; index < list.Items.Count; ++index)
    {
      int length = list.Items[index].Text.Length;
      if ((long) length > maxDBSz)
      {
        ++namesCountByLength;
        if ((long) length > maxDetectedSz)
          maxDetectedSz = (long) length;
      }
    }
    return namesCountByLength;
  }

  private void AssignLoginPostfix(string selectedDomain)
  {
    if (this.defaultCatalogName.Equals(selectedDomain, StringComparison.InvariantCultureIgnoreCase))
      this.LoginPostfix = string.Empty;
    else
      this.LoginPostfix = "@" + selectedDomain;
  }

  private void _domain_TextChanged(object sender, EventArgs e)
  {
  }

  private void _bImport_Click(object sender, EventArgs e)
  {
    bool flag1 = false;
    if (this._list.CheckedItems.Count > 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ArrayList arrayList = new ArrayList();
        IUserSession session = sessionKeeper.Session;
        bool flag2 = false;
        foreach (ListViewItem checkedItem in this._list.CheckedItems)
        {
          IDBObject dbObject = session.GetObjectCollection(session.IdentHelper.UsersTypeID).Create();
          try
          {
            switch (this._provider)
            {
              case ImportFromNTDomainForm.ProviderUsage.LDAP:
                dbObject.SetAttributesValues(this.GetUserAttributeValues(checkedItem.Tag as Hashtable, session), false, true);
                break;
              case ImportFromNTDomainForm.ProviderUsage.WinNT:
                dbObject.SetAttributesValues(this.AddUserAttributeValues(checkedItem.Tag as DirectoryEntry, session), false, true);
                break;
            }
            dbObject.CommitCreation(true);
            IDBRelation relation = sessionKeeper.Session.GetRelation(session.IdentHelper.AllUsersGroupID, dbObject.ID);
            if (relation != null)
            {
              if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
              {
                DBRelationsEventArgs e1 = new DBRelationsEventArgs("RelationsCreated", relation.RelationID, relation.ProjID, relation.RelationType);
                service.FireEvent((object) null, (NotificationEventArgs) e1);
              }
            }
          }
          catch (Exception ex)
          {
            if (!flag2)
            {
              QuestionFormResult questionFormResult = QuestionForm.Show(ex.Message, LocalizationHolder.rm.GetString("DatabaseConfigurator_190"));
              flag1 = questionFormResult.Equals((object) QuestionFormResult.Break);
              flag2 = questionFormResult.Equals((object) QuestionFormResult.SkipAll);
            }
            dbObject = (IDBObject) null;
          }
          if (dbObject != null)
          {
            IDBTypedObjectID dbTypedObjectId = (IDBTypedObjectID) new DBTypedObjectID(dbObject.ObjectType, dbObject.ObjectID, dbObject.ID, dbObject.Caption, dbObject.OwnerID, (long) dbObject.VersionID, Convert.ToInt64(dbObject.IsBaseVersion), dbObject.SiteID, dbObject.ModificationID);
            arrayList.Add((object) dbTypedObjectId);
          }
          if (flag1)
            break;
        }
        if (arrayList.Count > 0)
        {
          if (!this._objID.Equals(session.IdentHelper.AllUsersGroupID))
          {
            IDBObject dbObject = session.GetObject(this._objID);
            IDBTypedObjectID parentObject = (IDBTypedObjectID) new DBTypedObjectID(dbObject.ObjectType, dbObject.ObjectID, dbObject.ID, dbObject.Caption, dbObject.OwnerID, (long) dbObject.VersionID, Convert.ToInt64(dbObject.IsBaseVersion), dbObject.SiteID, dbObject.ModificationID);
            IDBTypedObjectID[] objectIDs = new IDBTypedObjectID[arrayList.Count];
            arrayList.CopyTo((Array) objectIDs);
            ObjectCommands.DoInsertIntoObject((NodeIDPath) null, parentObject, objectIDs, (IDBRelationID[]) null, (Hashtable) null, (System.IServiceProvider) null, NavigatorRelationCommand.Unknown);
          }
        }
      }
    }
    if (flag1)
      return;
    this.Close();
  }

  private void ImportFromNTDomainForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
    this.LoadConfig();
    this.UpdateStatus();
    this.UpdateControlsStatus();
  }

  private void LoadConfig()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService))
        return;
      customService.SynchronizeDirectoryReadConfig(sessionKeeper.Session.SessionGUID, out this.defaultCatalogName, out HybridDictionary _);
    }
  }

  private void UpdateStatus()
  {
    this._statusBar.Text = "Домен по умолчанию: " + (this.defaultCatalogName != string.Empty ? this.defaultCatalogName : "<не назначен>");
  }

  private void ImportFromNTDomainForm_Closed(object sender, EventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void _list_ColumnClick(object sender, ColumnClickEventArgs e)
  {
    if (e.Column != this.sortColumn)
    {
      this.sortColumn = e.Column;
      this._list.Sorting = SortOrder.Ascending;
    }
    else
      this._list.Sorting = this._list.Sorting != SortOrder.Ascending ? SortOrder.Ascending : SortOrder.Descending;
    this._list.ListViewItemSorter = (IComparer) new ListViewItemComparer(e.Column, this._list.Sorting);
    this._list.Sort();
    this.UpdateControlsStatus();
  }

  private void domainCB_DropDown(object sender, EventArgs e)
  {
    this._domain.Items.Clear();
    foreach (ActiveDirectoryPartition domain in (ReadOnlyCollectionBase) Forest.GetCurrentForest().Domains)
      this._domain.Items.Add((object) domain.Name);
  }

  private void domainCB_TextChanged(object sender, EventArgs e)
  {
    this._bGetUsersList.Enabled = this._domain.Text != "";
  }

  private void _domain_SelectedValueChanged(object sender, EventArgs e)
  {
    this._bGetUsersList.Enabled = this._domain.Text != "";
  }

  private void cmdSelectAll_Click(object sender, EventArgs e) => this.SelectAll(true);

  private void cmdDeselectAll_Click(object sender, EventArgs e) => this.SelectAll(false);

  private void SelectAll(bool p)
  {
    this.blockOnCheck = true;
    try
    {
      foreach (ListViewItem listViewItem in this._list.Items)
        listViewItem.Checked = p;
    }
    finally
    {
      this.blockOnCheck = false;
    }
    this.UpdateControlsStatus();
  }

  private void _list_ItemChecked(object sender, ItemCheckedEventArgs e)
  {
    if (this.blockOnCheck)
      return;
    this.UpdateControlsStatus();
  }

  private void bSearch_Click(object sender, EventArgs e) => this.DoSearch();

  private void cbSearch_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar != '\r')
      return;
    this.DoSearch();
  }

  private void DoSearch()
  {
    this.UpdateCBSearchHistory();
    int index = this.SearchNext();
    if (index != -1)
    {
      ListViewItem listViewItem = this._list.Items[index];
      listViewItem.Selected = true;
      this._list.TopItem = listViewItem;
    }
    else
    {
      int num = (int) IMMessageBox.Show("Поиск", "Строка не найдена", MessageBoxButtons.OK, IMMessageBoxImage.Information);
    }
  }

  private void UpdateCBSearchHistory()
  {
    if (this.cbSearch.Items.IndexOf((object) this.cbSearch.Text) != -1)
      return;
    this.cbSearch.Items.Add((object) this.cbSearch.Text);
  }

  private int SearchNext()
  {
    string text = this.cbSearch.Text;
    if (this._list.Items.Count == 0 || text == string.Empty)
      return -1;
    int num1 = 0;
    int num2 = this._list.Items.Count - 1;
    if (this._list.SelectedItems.Count > 0)
    {
      num1 = this._list.SelectedItems[0].Index + 1;
      if (num1 >= this._list.Items.Count)
        num1 = 0;
      else
        num2 = this._list.SelectedItems[0].Index;
    }
    int index = num1;
    while (true)
    {
      do
      {
        ListViewItem lvi = this._list.Items[index];
        if (this.CheckCondition(lvi, text))
          return lvi.Index;
        if (index != num2)
          ++index;
        else
          goto label_12;
      }
      while (index < this._list.Items.Count);
      index = 0;
    }
label_12:
    return -1;
  }

  private bool CheckCondition(ListViewItem lvi, string findString)
  {
    for (int index = 0; index < lvi.SubItems.Count; ++index)
    {
      if (lvi.SubItems[index].Text.IndexOf(findString, StringComparison.InvariantCultureIgnoreCase) != -1)
        return true;
    }
    return false;
  }

  internal enum ProviderUsage
  {
    NoProvider,
    LDAP,
    WinNT,
  }
}
