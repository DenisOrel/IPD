// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.AutoPlaceInArchiveView.AutoPlaceControl
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives.AutoPlaceInArchiveView;

/// <summary>
/// Контрол, управдяющий настройками автоматического размещения в архиве
/// </summary>
public class AutoPlaceControl : UserControl
{
  /// <summary>ИД архива, для которого показана закладка</summary>
  private long _archiveID;
  /// <summary>
  /// ИД типов документов, которые автоматически размещаются в данном архиве
  /// </summary>
  private List<int> _autoPlaceDocTypesIDs = new List<int>();
  /// <summary>
  /// Ид пользователей, которые могут автоматически размещать документы в архиве
  /// </summary>
  private List<long> _usersIDs;
  /// <summary>Контрол изменен</summary>
  private bool _isModified;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel lblPanel;
  private Label label1;
  private SplitContainer splitContainer;
  private Intermech.Bars.ToolBar toolBarForDocTypes;
  private Intermech.Bars.ToolBar toolBarForUsers;
  private ListView lvDocTypes;
  private ButtonItem btnAddDocType;
  private ButtonItem btnDeleteDocType;
  private ButtonItem btnAddUser;
  private ButtonItem btnDeleteUser;
  private ListView lvUsers;
  private ColumnHeader docTypes;
  private ColumnHeader users;

  /// <summary>Событие на изменение контрола</summary>
  private event EventHandler _onModified;

  /// <summary>ИД архива, для которого показана закладка</summary>
  public long ArchiveID
  {
    get => this._archiveID;
    set => this._archiveID = value;
  }

  /// <summary>
  /// Типы документов, которые автоматически размещаются в данном архиве
  /// </summary>
  public List<int> AutoPlaceDocTypesIDs => this._autoPlaceDocTypesIDs;

  /// <summary>
  /// Ид пользователей, которые могут автоматически размещать документы в архиве
  /// </summary>
  public List<long> UsersIDs => this._usersIDs;

  /// <summary>Производились ли изменения на контроле</summary>
  public bool IsModified
  {
    get => this._isModified;
    set
    {
      this._isModified = value;
      if (this._onModified == null)
        return;
      this._onModified.DynamicInvoke((object) this, (object) new EventArgs());
    }
  }

  /// <summary>Событие на изменение содержимого. Можно подписываться</summary>
  public event EventHandler OnModified
  {
    add => this._onModified += value;
    remove => this._onModified -= value;
  }

  /// <summary>Конструктор</summary>
  public AutoPlaceControl()
  {
    this.InitializeComponent();
    if (Statics.IconSrv != null)
      this.lvDocTypes.SmallImageList = this.lvUsers.SmallImageList = Statics.IconSrv.ImageList;
    else
      this.lvDocTypes.SmallImageList = this.lvUsers.SmallImageList = (ImageList) null;
  }

  /// <summary>Обновить отображаемый список Типов документов</summary>
  private void UpdateDocsListViewItems()
  {
    this.lvDocTypes.BeginUpdate();
    this.lvDocTypes.Items.Clear();
    foreach (int autoPlaceDocTypesId in this._autoPlaceDocTypesIDs)
    {
      if (autoPlaceDocTypesId != -1)
      {
        ListViewItem listViewItem = new ListViewItem(MetaDataHelper.GetObjectTypeName(autoPlaceDocTypesId));
        listViewItem.Tag = (object) autoPlaceDocTypesId;
        if (Statics.IconSrv != null)
        {
          int num = Statics.IconSrv.IndexOf(4, autoPlaceDocTypesId);
          listViewItem.ImageIndex = num;
        }
        this.lvDocTypes.Items.Add(listViewItem);
      }
    }
    this.lvDocTypes.EndUpdate();
    this.lvDocTypes.Refresh();
    if (this._autoPlaceDocTypesIDs.Count == 0 || this.lvDocTypes.SelectedItems.Count == 0)
      this.btnDeleteDocType.Enabled = false;
    else
      this.btnDeleteDocType.Enabled = true;
  }

  /// <summary>Обновить отображаемый список Пользователей</summary>
  /// <exception cref="T:System.NotImplementedException"></exception>
  private void UpdateUsersListViewItems()
  {
    this.lvUsers.BeginUpdate();
    this.lvUsers.Items.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long usersId in this._usersIDs)
      {
        if (usersId != 0L)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(usersId);
          ListViewItem listViewItem = new ListViewItem(dbObject.Caption);
          listViewItem.Tag = (object) usersId;
          if (Statics.IconSrv != null)
          {
            int num = Statics.IconSrv.IndexOf(4, dbObject.ObjectType);
            listViewItem.ImageIndex = num;
          }
          this.lvUsers.Items.Add(listViewItem);
        }
      }
    }
    this.lvUsers.EndUpdate();
    this.lvUsers.Refresh();
    if (this._usersIDs.Count == 0 || this.lvUsers.SelectedItems.Count == 0)
      this.btnDeleteUser.Enabled = false;
    else
      this.btnDeleteUser.Enabled = true;
  }

  /// <summary>
  /// Перечитывает инфомацию о типах документов и назначенных пользователях и обновляет контрол.
  /// </summary>
  public void UpdateControl()
  {
    using (SessionKeeper sk = new SessionKeeper())
    {
      this._autoPlaceDocTypesIDs = this.GetAutoPlaceDocTypesIDsFromAttribute(sk);
      this._usersIDs = this.GetUsersFromAttribute(sk);
    }
    this.UpdateDocsListViewItems();
    this.UpdateUsersListViewItems();
  }

  /// <summary>
  /// Получает список ИД типов документов для авторазмещения в архиве.
  /// </summary>
  /// <param name="archiveObj">Архив</param>
  /// <returns>Cписок ИД типов документов для авторазмещения в архиве.</returns>
  private List<int> GetAutoPlaceDocTypesIDsFromAttribute(SessionKeeper sk)
  {
    List<int> idsFromAttribute = new List<int>();
    IDBAttribute objectAttributeById = sk.Session.GetObjectAttributeByID(this._archiveID, ConstsHolder.AutoPlaceDocTypesAttrID);
    if (objectAttributeById != null && !objectAttributeById.IsNull)
    {
      foreach (string description in objectAttributeById.Descriptions)
        idsFromAttribute.Add(MetaDataHelper.GetObjectTypeID(new Guid(description)));
    }
    return idsFromAttribute;
  }

  /// <summary>
  /// Получает пользователей, имеющих право автоматически размещать документы в архиве
  /// </summary>
  /// <param name="archiveObj">Архив</param>
  /// <returns>Пользователи, имеющие право автоматически размещать документы в архиве</returns>
  private List<long> GetUsersFromAttribute(SessionKeeper sk)
  {
    List<long> usersFromAttribute = new List<long>();
    IDBAttribute objectAttributeById = sk.Session.GetObjectAttributeByID(this._archiveID, ConstsHolder.UsersCanAutoPlaceDocsAttrID);
    if (objectAttributeById != null && !objectAttributeById.IsNull)
    {
      for (int index = 0; index < objectAttributeById.ValuesCount; ++index)
      {
        long int64 = Convert.ToInt64(objectAttributeById.Values[index]);
        usersFromAttribute.Add(int64);
      }
    }
    return usersFromAttribute;
  }

  /// <summary>Добавить документы</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void btnAddDocType_Click(object sender, EventArgs e)
  {
    List<int> fromSelectorForm = AutoPlaceControl.GetTypesIDsFromSelectorForm();
    if (fromSelectorForm.Count == 0)
      return;
    List<int> first = new List<int>();
    foreach (int parentTypeID in fromSelectorForm)
    {
      List<int> childrenIdRecursive1 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(parentTypeID);
      List<int> childrenIdRecursive2 = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(parentTypeID);
      first.AddRange(childrenIdRecursive1.Union<int>((IEnumerable<int>) childrenIdRecursive2));
    }
    List<int> intList1 = new List<int>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IArchiveService customService1 = sessionKeeper.Session.GetCustomService(typeof (IArchiveService)) as IArchiveService;
      IArchiveAutoPlaceCacheService customService2 = sessionKeeper.Session.GetCustomService(typeof (IArchiveAutoPlaceCacheService)) as IArchiveAutoPlaceCacheService;
      int asInteger = (int) sessionKeeper.Session.GetObjectAttributeByID(this._archiveID, MetaDataHelper.GetAttributeTypeID(ConstsHolder.ArchiveTypesUsingModeGuid)).AsInteger;
      long archiveId = this._archiveID;
      Guid sessionGuid = sessionKeeper.Session.SessionGUID;
      List<int> permittedTypesIds = customService1.GetArchivePermittedTypesIDs(archiveId, true, sessionGuid);
      switch (asInteger)
      {
        case 0:
          intList1.AddRange(fromSelectorForm.Union<int>((IEnumerable<int>) this._autoPlaceDocTypesIDs));
          break;
        case 1:
          List<int> second = new List<int>();
          foreach (int autoPlaceDocTypesId in this._autoPlaceDocTypesIDs)
          {
            List<int> childrenIdRecursive3 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(autoPlaceDocTypesId);
            List<int> childrenIdRecursive4 = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(autoPlaceDocTypesId);
            second.AddRange(childrenIdRecursive3.Union<int>((IEnumerable<int>) childrenIdRecursive4));
          }
          List<int> list1 = first.Union<int>((IEnumerable<int>) second).ToList<int>();
          intList1 = permittedTypesIds.Intersect<int>((IEnumerable<int>) list1).ToList<int>();
          List<int> list2 = list1.Except<int>((IEnumerable<int>) intList1).ToList<int>();
          if (list2.Any<int>())
          {
            int num = (int) new WrongIdForm(list2).ShowDialog();
            break;
          }
          break;
      }
      List<int> intList2 = new List<int>();
      intList2.AddRange((IEnumerable<int>) intList1);
      if (this._usersIDs.Count > 0)
      {
        List<int> wrongTypeIDs;
        Dictionary<long, TypesAndUsers> settingsIntersections = customService2.FindArchiveSettingsIntersections(this._archiveID, intList2, this._usersIDs, out wrongTypeIDs, out List<long> _);
        foreach (int num in wrongTypeIDs)
          intList2.Remove(num);
        if (settingsIntersections.Count > 0)
        {
          int num1 = (int) new ArchiveAutoPlaceSettingsIntersection(settingsIntersections).ShowDialog();
        }
      }
      foreach (int childTypeID in intList1)
      {
        List<int> objectTypeParentsId = MetaDataHelper.GetObjectTypeParentsID(childTypeID);
        if (intList2.Intersect<int>((IEnumerable<int>) objectTypeParentsId).ToList<int>().Count > 0)
          intList2.Remove(childTypeID);
      }
      this._autoPlaceDocTypesIDs.Clear();
      this._autoPlaceDocTypesIDs.AddRange((IEnumerable<int>) intList2);
    }
    this.UpdateDocsListViewItems();
    this.IsModified = true;
  }

  /// <summary>Кнопка "Добавить пользователей".</summary>
  private void btnAddUser_Click(object sender, EventArgs e)
  {
    List<long> fromSelectorWindow = this.GetNewUserIDsFromSelectorWindow();
    IArchiveAutoPlaceCacheService customService = (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IArchiveAutoPlaceCacheService)) as IArchiveAutoPlaceCacheService;
    if (this._autoPlaceDocTypesIDs.Count > 0)
    {
      List<long> wrongUsersIDs;
      Dictionary<long, TypesAndUsers> settingsIntersections = customService.FindArchiveSettingsIntersections(this._archiveID, this._autoPlaceDocTypesIDs, fromSelectorWindow, out List<int> _, out wrongUsersIDs);
      foreach (long num in wrongUsersIDs)
        fromSelectorWindow.Remove(num);
      if (settingsIntersections.Count > 0)
      {
        int num1 = (int) new ArchiveAutoPlaceSettingsIntersection(settingsIntersections).ShowDialog();
      }
    }
    this._usersIDs = this._usersIDs.Union<long>((IEnumerable<long>) fromSelectorWindow).ToList<long>();
    this.UpdateUsersListViewItems();
    this.IsModified = true;
  }

  /// <summary>Выбрать юзеров.</summary>
  /// <returns>Список ИД выбранных пользователей</returns>
  private List<long> GetNewUserIDsFromSelectorWindow()
  {
    List<long> fromSelectorWindow = new List<long>();
    Intermech.Navigator.CustomNode.Descriptor rootDescriptor = new Intermech.Navigator.CustomNode.Descriptor(ServiceHolder.rm.GetString("Archives_186"), new DescriptorCollection()
    {
      (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545")),
      (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad00003-306c-11d8-b4e9-00304f19f545")),
      (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cadd9232-306c-11d8-b4e9-00304f19f545"))
    });
    object[] objArray = SelectionWindow.Select(ServiceHolder.rm.GetString("Archives_187"), (IDescriptor) rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableSelectFromTree);
    if (objArray == null || objArray.Length == 0)
      return fromSelectorWindow;
    for (int index = 0; index < objArray.Length; ++index)
    {
      if (objArray[index] is IDBTypedObjectID dbTypedObjectId && !fromSelectorWindow.Contains(dbTypedObjectId.ObjectID))
        fromSelectorWindow.Add(dbTypedObjectId.ObjectID);
    }
    return fromSelectorWindow;
  }

  /// <summary>
  /// Получить c помощью СелекторФорм список ИД типов для добавления
  /// </summary>
  /// <returns>Список идентификаторов типов для добавления в список.
  /// Пустой, если ничего не выбрано.</returns>
  private static List<int> GetTypesIDsFromSelectorForm()
  {
    List<int> fromSelectorForm = new List<int>();
    SelectorForm selectorForm = new SelectorForm(ServiceHolder.rm.GetString("Archives_157"), 4, true)
    {
      SelectorFilter = (ISelectorFilter) new ObjTypeSelectorFilter(MetaDataHelper.GetObjectTypeChildrenIDRecursive(ConstsHolder.DocTypeID))
    };
    if (selectorForm.ShowDialog() == DialogResult.Cancel || selectorForm.IDList.Count == 0)
      return fromSelectorForm;
    foreach (object id in selectorForm.IDList)
      fromSelectorForm.Add(Convert.ToInt32(id));
    return fromSelectorForm;
  }

  /// <summary>Кнопка "Удалить типы документов"</summary>
  private void btnDeleteDocType_Click(object sender, EventArgs e)
  {
    if (this.lvDocTypes.SelectedItems.Count == 0)
      return;
    foreach (ListViewItem selectedItem in this.lvDocTypes.SelectedItems)
    {
      this._autoPlaceDocTypesIDs.Remove(Convert.ToInt32(selectedItem.Tag));
      this.lvDocTypes.Items.Remove(selectedItem);
    }
    this.UpdateDocsListViewItems();
    this.IsModified = true;
  }

  /// <summary>Кнопка удалить пользователя</summary>
  private void btnDeleteUser_Click(object sender, EventArgs e)
  {
    if (this.lvUsers.SelectedItems.Count == 0)
      return;
    foreach (ListViewItem selectedItem in this.lvUsers.SelectedItems)
    {
      this._usersIDs.Remove((long) Convert.ToInt32(selectedItem.Tag));
      this.lvUsers.Items.Remove(selectedItem);
    }
    this.UpdateUsersListViewItems();
    this.IsModified = true;
  }

  /// <summary>Сменился выделенный элемент в списке документов</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void lvDocTypes_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lvDocTypes.SelectedItems.Count == 0)
      this.btnDeleteDocType.Enabled = false;
    else
      this.btnDeleteDocType.Enabled = true;
  }

  /// <summary>Сменился выделенный элемент в списке пользователей</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void lvUsers_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lvUsers.SelectedItems.Count == 0)
      this.btnDeleteUser.Enabled = false;
    else
      this.btnDeleteUser.Enabled = true;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AutoPlaceControl));
    this.lblPanel = new Panel();
    this.label1 = new Label();
    this.splitContainer = new SplitContainer();
    this.lvDocTypes = new ListView();
    this.docTypes = new ColumnHeader();
    this.toolBarForDocTypes = new Intermech.Bars.ToolBar();
    this.btnAddDocType = new ButtonItem();
    this.btnDeleteDocType = new ButtonItem();
    this.lvUsers = new ListView();
    this.users = new ColumnHeader();
    this.toolBarForUsers = new Intermech.Bars.ToolBar();
    this.btnAddUser = new ButtonItem();
    this.btnDeleteUser = new ButtonItem();
    this.lblPanel.SuspendLayout();
    this.splitContainer.BeginInit();
    this.splitContainer.Panel1.SuspendLayout();
    this.splitContainer.Panel2.SuspendLayout();
    this.splitContainer.SuspendLayout();
    this.SuspendLayout();
    this.lblPanel.Controls.Add((Control) this.label1);
    this.lblPanel.Dock = DockStyle.Top;
    this.lblPanel.Location = new Point(0, 0);
    this.lblPanel.Name = "lblPanel";
    this.lblPanel.Size = new Size(684, 41);
    this.lblPanel.TabIndex = 0;
    this.label1.Dock = DockStyle.Fill;
    this.label1.Location = new Point(0, 0);
    this.label1.Name = "label1";
    this.label1.Size = new Size(684, 41);
    this.label1.TabIndex = 0;
    this.label1.Text = "Документы из списка разрешенных, создаваемые выбранными пользователями, будут автоматически размещаться в данном архиве.";
    this.label1.TextAlign = ContentAlignment.MiddleLeft;
    this.splitContainer.AccessibleRole = AccessibleRole.TitleBar;
    this.splitContainer.Dock = DockStyle.Fill;
    this.splitContainer.Location = new Point(0, 41);
    this.splitContainer.Name = "splitContainer";
    this.splitContainer.Panel1.Controls.Add((Control) this.lvDocTypes);
    this.splitContainer.Panel1.Controls.Add((Control) this.toolBarForDocTypes);
    this.splitContainer.Panel2.Controls.Add((Control) this.lvUsers);
    this.splitContainer.Panel2.Controls.Add((Control) this.toolBarForUsers);
    this.splitContainer.Size = new Size(684, 416);
    this.splitContainer.SplitterDistance = 333;
    this.splitContainer.TabIndex = 1;
    this.lvDocTypes.Columns.AddRange(new ColumnHeader[1]
    {
      this.docTypes
    });
    this.lvDocTypes.Dock = DockStyle.Fill;
    this.lvDocTypes.Location = new Point(0, 24);
    this.lvDocTypes.Name = "lvDocTypes";
    this.lvDocTypes.Size = new Size(333, 392);
    this.lvDocTypes.TabIndex = 1;
    this.lvDocTypes.UseCompatibleStateImageBehavior = false;
    this.lvDocTypes.View = View.Details;
    this.lvDocTypes.SelectedIndexChanged += new EventHandler(this.lvDocTypes_SelectedIndexChanged);
    this.docTypes.Text = "Типы документов";
    this.docTypes.Width = 327;
    this.toolBarForDocTypes.FullMenus = true;
    this.toolBarForDocTypes.Guid = new Guid("93a280a9-f4db-4ac4-9ee6-5c5e3ba516b4");
    this.toolBarForDocTypes.Hidden = false;
    this.toolBarForDocTypes.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.btnAddDocType,
      (ToolbarItemBase) this.btnDeleteDocType
    });
    this.toolBarForDocTypes.Location = new Point(0, 0);
    this.toolBarForDocTypes.Name = "toolBarForDocTypes";
    this.toolBarForDocTypes.Size = new Size(333, 24);
    this.toolBarForDocTypes.TabIndex = 0;
    this.toolBarForDocTypes.Text = "toolBar1";
    this.btnAddDocType.BeginGroup = true;
    this.btnAddDocType.CommandName = "btnAddDocType";
    this.btnAddDocType.Image = (Image) componentResourceManager.GetObject("btnAddDocType.Image");
    this.btnAddDocType.ImageIndex = 0;
    this.btnAddDocType.ToolTipText = "Добавить тип документа";
    this.btnAddDocType.Click += new EventHandler(this.btnAddDocType_Click);
    this.btnDeleteDocType.BeginGroup = true;
    this.btnDeleteDocType.CommandName = "btnDeleteDoctype";
    this.btnDeleteDocType.Image = (Image) componentResourceManager.GetObject("btnDeleteDocType.Image");
    this.btnDeleteDocType.ToolTipText = "Удалить тип документа";
    this.btnDeleteDocType.Click += new EventHandler(this.btnDeleteDocType_Click);
    this.lvUsers.Columns.AddRange(new ColumnHeader[1]
    {
      this.users
    });
    this.lvUsers.Dock = DockStyle.Fill;
    this.lvUsers.Location = new Point(0, 24);
    this.lvUsers.Name = "lvUsers";
    this.lvUsers.Size = new Size(347, 392);
    this.lvUsers.TabIndex = 1;
    this.lvUsers.UseCompatibleStateImageBehavior = false;
    this.lvUsers.View = View.Details;
    this.lvUsers.SelectedIndexChanged += new EventHandler(this.lvUsers_SelectedIndexChanged);
    this.users.Text = "Пользователи";
    this.users.Width = 339;
    this.toolBarForUsers.FullMenus = true;
    this.toolBarForUsers.Guid = new Guid("789fe93a-3b81-425b-a128-f7c0eb7be0ad");
    this.toolBarForUsers.Hidden = false;
    this.toolBarForUsers.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.btnAddUser,
      (ToolbarItemBase) this.btnDeleteUser
    });
    this.toolBarForUsers.Location = new Point(0, 0);
    this.toolBarForUsers.Name = "toolBarForUsers";
    this.toolBarForUsers.Size = new Size(347, 24);
    this.toolBarForUsers.TabIndex = 0;
    this.toolBarForUsers.Text = "toolBar1";
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
    this.Controls.Add((Control) this.splitContainer);
    this.Controls.Add((Control) this.lblPanel);
    this.Name = nameof (AutoPlaceControl);
    this.Size = new Size(684, 457);
    this.lblPanel.ResumeLayout(false);
    this.splitContainer.Panel1.ResumeLayout(false);
    this.splitContainer.Panel2.ResumeLayout(false);
    this.splitContainer.EndInit();
    this.splitContainer.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
