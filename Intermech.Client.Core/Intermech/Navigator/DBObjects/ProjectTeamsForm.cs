
// Type: Intermech.Navigator.DBObjects.ProjectTeamsForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Infralution.Controls.VirtualTree;
using Intermech.Client.Core;
using Intermech.Client.Core.Navigator.Classes.ObjectNode;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Projects;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.DBObjects;

/// <summary>Форма для управления участниками проектов</summary>
public class ProjectTeamsForm : Form
{
  /// <summary>Права доступа к объекту в закладке</summary>
  internal ProjectTeamsForm.EditorMode _editorMode;
  /// <summary>Были ли изменения в закладке</summary>
  internal bool _isChanged;
  /// <summary>Текущий пользователь</summary>
  internal ICurrentUserAndRole _userAndRole;
  /// <summary>Список менеджеров проекта (оригинальный)</summary>
  internal List<MyElementEx> _orgManagers = new List<MyElementEx>();
  /// <summary>Список участников проекта (оригинальный)</summary>
  internal List<MyElementEx> _orgUsers = new List<MyElementEx>();
  /// <summary>Список менеджеров проекта</summary>
  internal List<MyElementEx> _managers = new List<MyElementEx>();
  /// <summary>Список участников проекта</summary>
  internal List<MyElementEx> _users = new List<MyElementEx>();
  /// <summary>Невидимый корневой элемент в дереве</summary>
  internal List<object> _rootItem = new List<object>();
  /// <summary>Список групп в дереве</summary>
  internal List<int> _groups = new List<int>();
  /// <summary>Значок для группы</summary>
  private static Icon _iconGroup;
  /// <summary>Значок для менеджера проекта</summary>
  private static Icon _iconManager;
  /// <summary>Значок для участника проекта</summary>
  private static Icon _iconUser;
  /// <summary>список участников проекта</summary>
  public List<ProjectParticipantInfo> Participant = new List<ProjectParticipantInfo>();
  /// <summary>Запрет на обработку событий от дерева</summary>
  private bool _disableTreeEvents;
  /// <summary>Есть ли хотя бы одна выделенная группа</summary>
  private bool hasSelGroup;
  /// <summary>Есть ли хотя бы один выделенный участник в группе</summary>
  private bool hasSelUser;
  /// <summary>Есть ли хотя бы один выделенный менеджер в группе</summary>
  private bool hasSelManager;
  /// <summary>Список отмеченных менеджеров проекта</summary>
  private List<MyElementEx> _selManagers = new List<MyElementEx>();
  /// <summary>Список отмеченных участников проекта</summary>
  private List<MyElementEx> _selUsers = new List<MyElementEx>();
  /// <summary>Прямоугольник, в котором "всё началось"</summary>
  private Rectangle dragBoxFromMouseDown;
  /// <summary>Смещение</summary>
  private Point screenOffset;
  /// <summary>
  /// Строка, на которую "сваливаются" перетаскиваемые объекты
  /// </summary>
  private Row _dropTargetRow;
  /// <summary>id версии создаваемого объекта</summary>
  private long objectID;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Column columnTeam;
  private Column columnLevel;
  private Button btnCancel;
  private ToolTip toolTip;
  private Panel panelBottom;
  private Button btnApply;
  private ImageList imageList;
  private Button btnAdd;
  private Button btnDel;
  private Panel panelControls;
  private Intermech.VirtualTreeView.VirtualTreeView projectTeam;

  public long ObjectID
  {
    set
    {
      this.objectID = value;
      if (this.objectID >= 0L)
        return;
      this.Participant.Clear();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this.objectID, false);
        if (dbObject == null)
          return;
        this.Participant.Add(new ProjectParticipantInfo(dbObject.OwnerID, true));
      }
    }
  }

  public ProjectTeamsForm()
  {
    this.InitializeComponent();
    this.InitViewResources();
    this.projectTeam.DisableHeaderContextMenu = true;
  }

  /// <summary>Инициализация ресурсов закладки</summary>
  public void InitViewResources()
  {
    this._userAndRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    this._rootItem.Add((object) this._groups);
    this._groups.Add(0);
    this._groups.Add(1);
    if (ProjectTeamsForm._iconGroup == null)
    {
      ProjectTeamsForm._iconGroup = ImageHelper.BitmapToIcon(this.imageList.Images[0] as Bitmap);
      ProjectTeamsForm._iconManager = ImageHelper.BitmapToIcon(this.imageList.Images[1] as Bitmap);
      ProjectTeamsForm._iconUser = ImageHelper.BitmapToIcon(this.imageList.Images[2] as Bitmap);
    }
    this.projectTeam.DataSource = (object) this._rootItem;
  }

  /// <summary>Освобождение ресурсов закладки</summary>
  public void DisposeViewResources()
  {
  }

  public void SetParent(Control aParent)
  {
    if (aParent == null)
    {
      this.AutoScaleMode = AutoScaleMode.None;
      this.TopLevel = true;
      this.Dock = DockStyle.None;
      this.FormBorderStyle = FormBorderStyle.Sizable;
      this.Visible = false;
    }
    else
    {
      this.AutoScaleMode = AutoScaleMode.None;
      this.TopLevel = false;
      this.Dock = DockStyle.Fill;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Visible = true;
    }
    this.Parent = aParent;
  }

  /// <summary>
  /// Заполнить элементы управления закладки данными, полученными в методе Initialize
  /// </summary>
  internal void LoadViewData()
  {
    this.Clear();
    if (this.objectID == 0L)
      return;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        string empty = string.Empty;
        int AttrID = 0;
        FieldTypes AttrType = FieldTypes.ftUnknown;
        bool IsSystemType = false;
        bool IsAttrList = false;
        ArrayList AttrPossibleValues = (ArrayList) null;
        MyAttributeHelper.GetAttrInfo("cad00816-306c-11d8-b4e9-00304f19f545", ref empty, ref AttrID, ref AttrType, ref IsSystemType, ref IsAttrList, ref AttrPossibleValues);
        IDBObject dbObject1 = sessionKeeper.Session.GetObject(this.objectID, false);
        if (!(dbObject1 is IDBProjectObject dbProjectObject) || !dbProjectObject.IsProjectParticipant(sessionKeeper.Session.UserID))
          return;
        this._editorMode = dbProjectObject.IsProjectManager(sessionKeeper.Session.UserID) ? ProjectTeamsForm.EditorMode.EditorMode : ProjectTeamsForm.EditorMode.ReadOnly;
        ProjectParticipantInfo[] projectParticipantInfoArray = !dbObject1.IsCreationMode ? (ProjectParticipantInfo[]) dbProjectObject.GetParticipantsInfo() : this.Participant.ToArray();
        if (projectParticipantInfoArray == null)
          return;
        for (int index1 = 0; index1 < projectParticipantInfoArray.Length; ++index1)
        {
          ProjectParticipantInfo projectParticipantInfo = projectParticipantInfoArray[index1];
          long result = 0;
          bool flag = false;
          IDBObject dbObject2 = sessionKeeper.Session.GetObject(projectParticipantInfo.ParticipantID);
          if (dbObject2.ObjectType == sessionKeeper.Session.IdentHelper.UsersTypeID)
          {
            flag = true;
            IDBAttribute attributeById = dbObject2.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cad00816-306c-11d8-b4e9-00304f19f545"));
            if (attributeById.Value == null || attributeById.Value == DBNull.Value || !long.TryParse(attributeById.Value.ToString(), out result))
              result = 0L;
          }
          MyElement myElement1 = (MyElement) null;
          for (int index2 = 0; index2 < AttrPossibleValues.Count; ++index2)
          {
            if (AttrPossibleValues[index2] is MyElement myElement2 && Convert.ToInt64(myElement2.Value).Equals(result))
            {
              myElement1 = myElement2;
              break;
            }
          }
          string str = myElement1 != null ? myElement1.Caption : string.Empty;
          string ACaption = projectParticipantInfo is ProjectParticipantInfoEx ? (projectParticipantInfo as ProjectParticipantInfoEx).Caption : sessionKeeper.Session.GetObjectInfo(projectParticipantInfo.ParticipantID).Caption;
          MyElementEx myElementEx = new MyElementEx((object) projectParticipantInfo.ParticipantID, ACaption, (projectParticipantInfo.ProjectManager ? 1 : 0) != 0, (flag ? 1 : 0) != 0, false, result, 0, Guid.Empty, new object[1]
          {
            (object) str
          });
          if (projectParticipantInfo.ProjectManager)
            this._managers.Add(myElementEx);
          else
            this._users.Add(myElementEx);
          if (!this._isChanged)
          {
            if (projectParticipantInfo.ProjectManager)
              this._orgManagers.Add(myElementEx.Clone() as MyElementEx);
            else
              this._orgUsers.Add(myElementEx.Clone() as MyElementEx);
          }
        }
      }
    }
    finally
    {
      this.projectTeam.UpdateRows(true);
      this.projectTeam.RootRow.ExpandChildren(true);
      this.UpdateControls();
    }
  }

  /// <summary>Выполнить очистку элементов управления в закладке</summary>
  internal void Clear()
  {
    this._managers.Clear();
    this._users.Clear();
    if (!this._isChanged)
    {
      this._orgManagers.Clear();
      this._orgUsers.Clear();
    }
    this.projectTeam.UpdateRows(true);
    this.UpdateControls();
  }

  /// <summary>
  /// Пробежаться по дереву допустимых замен, подсчитать и собрать информацию по выделенным записям
  /// </summary>
  protected virtual void GatherSelectedInfo()
  {
    this.hasSelGroup = false;
    this.hasSelManager = false;
    this.hasSelUser = false;
    this._selManagers.Clear();
    this._selUsers.Clear();
    if (this.projectTeam.SelectedRows == null || this.projectTeam.SelectedRows.Count <= 0)
      return;
    for (int index = 0; index < this.projectTeam.SelectedRows.Count; ++index)
    {
      if (this.projectTeam.SelectedRows[index].Level == 1)
        this.hasSelGroup = true;
      if (this.projectTeam.SelectedRows[index].Level == 2)
      {
        if ((int) this.projectTeam.SelectedRows[index].ParentRow.Item == 0)
        {
          this.hasSelGroup = true;
          MyElementEx myElementEx = this.projectTeam.SelectedRows[index].Item as MyElementEx;
          if ((long) myElementEx.Value != this._userAndRole.UserID || this._editorMode != ProjectTeamsForm.EditorMode.EditorMode)
          {
            this.hasSelManager = true;
            this._selManagers.Add(myElementEx);
          }
          else
            continue;
        }
        if ((int) this.projectTeam.SelectedRows[index].ParentRow.Item == 1)
        {
          this.hasSelGroup = true;
          this.hasSelUser = true;
          this._selUsers.Add(this.projectTeam.SelectedRows[index].Item as MyElementEx);
        }
      }
    }
  }

  /// <summary>Управление контролами на закладке</summary>
  internal void UpdateControls()
  {
    this.GatherSelectedInfo();
    this.projectTeam.Enabled = this._editorMode != 0;
    this.panelControls.Visible = this._editorMode == ProjectTeamsForm.EditorMode.EditorMode;
    this.panelBottom.Visible = this._editorMode == ProjectTeamsForm.EditorMode.EditorMode;
    this.AllowDrop = this._editorMode == ProjectTeamsForm.EditorMode.EditorMode;
    this.btnApply.Enabled = false;
    this.btnApply.Visible = false;
    this.btnCancel.Enabled = false;
    this.btnCancel.Visible = false;
    this.panelBottom.Visible = false;
    this.btnAdd.Enabled = this._editorMode == ProjectTeamsForm.EditorMode.EditorMode && (this.hasSelGroup || this.hasSelManager || this.hasSelUser);
    this.btnDel.Enabled = this._editorMode == ProjectTeamsForm.EditorMode.EditorMode && (this._selManagers.Count > 0 && this._selManagers.Count < this._managers.Count || this._selUsers.Count > 0);
  }

  /// <summary>Изменилась выделенная строка в дереве</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void projectTeam_SelectionChanged(object sender, EventArgs e) => this.UpdateControls();

  /// <summary>Нажата кнопка мыши в дереве</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void projectTeam_MouseDown(object sender, MouseEventArgs e)
  {
    if (this._disableTreeEvents)
      return;
    if (this.projectTeam.SelectedRows.Count > 0)
    {
      Size dragSize = SystemInformation.DragSize;
      dragSize.Width += 4;
      dragSize.Height += 4;
      this.dragBoxFromMouseDown = new Rectangle(new Point(e.X - dragSize.Width / 2, e.Y - dragSize.Height / 2), dragSize);
    }
    else
      this.dragBoxFromMouseDown = Rectangle.Empty;
  }

  /// <summary>Перемещена мышь в дереве</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void projectTeam_MouseMove(object sender, MouseEventArgs e)
  {
    if (this._disableTreeEvents || (e.Button & MouseButtons.Left) != MouseButtons.Left)
      return;
    this.TreeStartDragDrop(e.Location);
  }

  /// <summary>Попробовать начать drag'n'drop с указанной точки</summary>
  /// <param name="location">Точка начала drag'n'drop</param>
  protected virtual void TreeStartDragDrop(Point location)
  {
    if (this._disableTreeEvents || this._editorMode != ProjectTeamsForm.EditorMode.EditorMode || !(this.dragBoxFromMouseDown != Rectangle.Empty) || this.dragBoxFromMouseDown.Contains(location.X, location.Y) || location.Y <= this.projectTeam.HeaderHeight)
      return;
    if (this.projectTeam.SelectedRow == null || this._selManagers.Count == 0 && this._selUsers.Count == 0)
    {
      this.dragBoxFromMouseDown = Rectangle.Empty;
    }
    else
    {
      this.screenOffset = SystemInformation.WorkingArea.Location;
      int num = (int) this.projectTeam.DoDragDrop((object) this.projectTeam.SelectedRows, DragDropEffects.Copy | DragDropEffects.Scroll);
    }
  }

  /// <summary>В дерево пришло событие drag'n'drop</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void projectTeam_DragEnter(object sender, DragEventArgs e)
  {
    if (this._disableTreeEvents)
      return;
    this._dropTargetRow = (Row) null;
    e.Effect = DragDropEffects.None;
    if (!this.projectTeam.AllowDrop || !e.Data.GetDataPresent(typeof (RowSelectionList)) && !e.Data.GetDataPresent(typeof (IOSource)))
      return;
    e.Effect = DragDropEffects.All;
  }

  /// <summary>Над деревом перетаскиваются объекты</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void projectTeam_DragOver(object sender, DragEventArgs e)
  {
    if (this._disableTreeEvents)
      return;
    e.Effect = DragDropEffects.None;
    if (!this.projectTeam.AllowDrop || !e.Data.GetDataPresent(typeof (RowSelectionList)) && !e.Data.GetDataPresent(typeof (IOSource)))
      return;
    e.Effect = DragDropEffects.All;
  }

  /// <summary>В дереве завершён drag'n'drop</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void projectTeam_DragDrop(object sender, DragEventArgs e)
  {
    if (this._disableTreeEvents || this._editorMode != ProjectTeamsForm.EditorMode.EditorMode || this._dropTargetRow == null || !e.Data.GetDataPresent(typeof (RowSelectionList)) && !e.Data.GetDataPresent(typeof (IOSource)))
      return;
    RowSelectionList data1 = e.Data.GetData(typeof (RowSelectionList)) as RowSelectionList;
    IOSource data2 = e.Data.GetData(typeof (IOSource)) as IOSource;
    if ((data1 == null || data1.Count == 0) && (data2 == null || data2.SelectedItems == null || data2.SelectedItems.Count == 0))
      return;
    bool projectManager = (int) (this._dropTargetRow.Level == 2 ? this._dropTargetRow.ParentRow : this._dropTargetRow).Item == 0;
    List<MyElementEx> myElementExList = new List<MyElementEx>();
    if (data2 != null)
    {
      int objectTypeId1 = MetaDataHelper.GetObjectTypeID(new Guid("cad00002-306c-11d8-b4e9-00304f19f545"));
      int objectTypeId2 = MetaDataHelper.GetObjectTypeID(new Guid("cad00003-306c-11d8-b4e9-00304f19f545"));
      for (int index = 0; index < data2.SelectedItems.Count; ++index)
      {
        if (data2.SelectedItems.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData && (itemData.ObjectType == objectTypeId1 || itemData.ObjectType == objectTypeId2) && (!projectManager || itemData.ObjectType != objectTypeId2))
        {
          MyElementEx myElementEx = new MyElementEx((object) itemData.ObjectID, itemData.Caption, (projectManager ? 1 : 0) != 0, (itemData.ObjectType == objectTypeId1 ? 1 : 0) != 0, false, 0L, 0, Guid.Empty, new object[1]
          {
            (object) string.Empty
          });
          myElementExList.Add(myElementEx);
        }
      }
    }
    else
    {
      for (int index = 0; index < data1.Count; ++index)
      {
        if (data1[index].Item is MyElementEx myElementEx && myElementEx.ElementBool != projectManager && (long) myElementEx.Value != this._userAndRole.UserID)
          myElementExList.Add(myElementEx);
      }
    }
    if (myElementExList.Count == 0 || this.objectID == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string empty = string.Empty;
      int AttrID = 0;
      FieldTypes AttrType = FieldTypes.ftUnknown;
      bool IsSystemType = false;
      bool IsAttrList = false;
      ArrayList AttrPossibleValues = (ArrayList) null;
      MyAttributeHelper.GetAttrInfo("cad00816-306c-11d8-b4e9-00304f19f545", ref empty, ref AttrID, ref AttrType, ref IsSystemType, ref IsAttrList, ref AttrPossibleValues);
      int securityLevel = sessionKeeper.Session.SecurityLevel;
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(this.objectID, false);
      if (!(dbObject1 is IDBProjectObject dbProjectObject) || !dbProjectObject.IsProjectManager(sessionKeeper.Session.UserID))
        return;
      ProjectParticipantInfo[] projectParticipantInfoArray1 = !dbObject1.IsCreationMode ? dbProjectObject.GetParticipants() : this.Participant.ToArray();
      List<long> longList1 = new List<long>(projectParticipantInfoArray1.Length);
      for (int index = 0; index < projectParticipantInfoArray1.Length; ++index)
        longList1.Add(projectParticipantInfoArray1[index].ParticipantID);
      List<long> longList2 = new List<long>();
      for (int index = myElementExList.Count - 1; index >= 0; --index)
      {
        if (longList1.Contains((long) myElementExList[index].Value))
          longList2.Add((long) myElementExList[index].Value);
      }
      if (longList2.Count > 0)
      {
        if (dbObject1.IsCreationMode)
        {
          foreach (long participantID in longList2)
            this.Participant.Remove(new ProjectParticipantInfo(participantID, false));
        }
        else
          dbProjectObject.ExcludeParticipants(longList2.ToArray());
      }
      ProjectParticipantInfo[] projectParticipantInfoArray2 = new ProjectParticipantInfo[myElementExList.Count];
      for (int index1 = 0; index1 < myElementExList.Count; ++index1)
      {
        projectParticipantInfoArray2[index1] = new ProjectParticipantInfo((long) myElementExList[index1].Value, projectManager);
        long result = 0;
        IDBObject dbObject2 = sessionKeeper.Session.GetObject((long) myElementExList[index1].Value);
        if (dbObject2.ObjectType == sessionKeeper.Session.IdentHelper.UsersTypeID)
        {
          IDBAttribute attributeById = dbObject2.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cad00816-306c-11d8-b4e9-00304f19f545"));
          if (attributeById.Value == null || attributeById.Value == DBNull.Value || !long.TryParse(attributeById.Value.ToString(), out result))
            result = 0L;
        }
        MyElement myElement1 = (MyElement) null;
        for (int index2 = 0; index2 < AttrPossibleValues.Count; ++index2)
        {
          if (AttrPossibleValues[index2] is MyElement myElement2 && Convert.ToInt64(myElement2.Value).Equals(result))
          {
            myElement1 = myElement2;
            break;
          }
        }
        string str = myElement1 != null ? myElement1.Caption : string.Empty;
        myElementExList[index1].ElementID64 = result;
        myElementExList[index1].Tags[0] = (object) str;
      }
      if (dbObject1.IsCreationMode)
        this.Participant.AddRange((IEnumerable<ProjectParticipantInfo>) projectParticipantInfoArray2);
      else
        dbProjectObject.IncludeParticipants(projectParticipantInfoArray2);
    }
    this._isChanged = true;
    this.LoadViewData();
  }

  /// <summary>Определить, куда сваливать перетаскиваемую строку</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void projectTeam_GetAllowedRowDropLocations(
    object sender,
    GetAllowedRowDropLocationsEventArgs e)
  {
    if (this._disableTreeEvents)
      return;
    this._dropTargetRow = e.Row;
    e.AllowedDropLocations = this._dropTargetRow != null ? RowDropLocation.OnRow : RowDropLocation.BelowRow;
  }

  /// <summary>
  /// Определить условия "сброса" перетаскиваемых объектов на указанную строку
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void projectTeam_GetRowDropEffect(object sender, GetRowDropEffectEventArgs e)
  {
    if (this._disableTreeEvents || !e.Data.GetDataPresent(typeof (RowSelectionList)) && !e.Data.GetDataPresent(typeof (IOSource)))
      return;
    this._dropTargetRow = e.Row;
    e.DropEffect = DragDropEffects.All;
  }

  /// <summary>Добавить участников в проект</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btnAdd_Click(object sender, EventArgs e)
  {
    if (this.objectID == 0L)
      return;
    this.UpdateControls();
    if (this._editorMode != ProjectTeamsForm.EditorMode.EditorMode || !this.hasSelGroup && !this.hasSelManager && !this.hasSelUser)
      return;
    Row row = this.projectTeam.SelectedRow;
    if (row == null)
      return;
    if (row.Level == 2)
      row = row.ParentRow;
    bool projectManager = (int) row.Item == 0;
    string str = projectManager ? LocalizationHolder.rm.GetString("Client.Core_629") : LocalizationHolder.rm.GetString("Client.Core_630");
    DescriptorCollection descriptors = new DescriptorCollection();
    descriptors.Add((IDescriptor) new UsersGroupsDescriptor());
    descriptors.Add((IDescriptor) new UsersRolesDescriptor());
    if (projectManager)
      Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new ObjectTypesSelectedItemsAnalyzer(MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545"), true), true);
    else
      Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new ObjectTypesSelectedItemsAnalyzer(new List<int>((IEnumerable<int>) new int[2]
      {
        MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545"),
        MetaDataHelper.GetObjectTypeID("cad00003-306c-11d8-b4e9-00304f19f545")
      }), true), true);
    Intermech.Navigator.CustomNode.Descriptor rootDescriptor = new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("Client.Core_275"), descriptors);
    long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(str, str, (IDescriptor) rootDescriptor, SelectionOptions.Default | SelectionOptions.ForceRebuildNavTree);
    if (numArray == null || numArray.Length == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.objectID, false);
      if (!(dbObject is IDBProjectObject dbProjectObject) || !dbProjectObject.IsProjectManager(sessionKeeper.Session.UserID))
        return;
      List<ProjectParticipantInfo> collection = new List<ProjectParticipantInfo>();
      for (int index = 0; index < numArray.Length; ++index)
      {
        if ((dbObject.IsCreationMode || !dbProjectObject.IsProjectParticipant(numArray[index])) && (!dbObject.IsCreationMode || !this.Participant.Contains(new ProjectParticipantInfo(numArray[index], false))))
          collection.Add(new ProjectParticipantInfo(numArray[index], projectManager));
      }
      if (collection != null)
      {
        if (collection.Count > 0)
        {
          if (dbObject.IsCreationMode)
            this.Participant.AddRange((IEnumerable<ProjectParticipantInfo>) collection);
          else
            dbProjectObject.IncludeParticipants(collection.ToArray());
        }
      }
    }
    this._isChanged = true;
    this.LoadViewData();
  }

  /// <summary>Удалить участников из проекта</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btnDel_Click(object sender, EventArgs e)
  {
    if (this.objectID == 0L)
      return;
    this.UpdateControls();
    if (this._editorMode != ProjectTeamsForm.EditorMode.EditorMode || !this.hasSelManager && !this.hasSelUser || MessageBox.Show(LocalizationHolder.rm.GetString(sc_4289.ssp_imclient_4290()), LocalizationHolder.rm.GetString("Client.Core_132"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.objectID, false);
      if (!(dbObject is IDBProjectObject dbProjectObject) || !dbProjectObject.IsProjectManager(sessionKeeper.Session.UserID))
        return;
      List<long> longList = new List<long>();
      for (int index = 0; index < this._selUsers.Count; ++index)
        longList.Add((long) this._selUsers[index].Value);
      for (int index = 0; index < this._selManagers.Count; ++index)
        longList.Add((long) this._selManagers[index].Value);
      if (longList != null)
      {
        if (longList.Count > 0)
        {
          if (dbObject.IsCreationMode)
          {
            foreach (long participantID in longList)
              this.Participant.Remove(new ProjectParticipantInfo(participantID, false));
          }
          else
            dbProjectObject.ExcludeParticipants(longList.ToArray());
        }
      }
    }
    this._isChanged = true;
    this.LoadViewData();
  }

  /// <summary>Получить дочерние элементы</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void projectTeam_GetChildren(object sender, GetChildrenEventArgs e)
  {
    if (e.Row.Level == 0)
      e.Children = (IList) this._groups;
    if (e.Row.Level != 1)
      return;
    switch (e.Row.ChildIndex)
    {
      case 0:
        this._managers.Sort();
        e.Children = (IList) this._managers;
        break;
      case 1:
        this._users.Sort();
        e.Children = (IList) this._users;
        break;
    }
  }

  /// <summary>Получить данные для строки</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void projectTeam_GetRowData(object sender, GetRowDataEventArgs e)
  {
    if (e.Row.Level == 1)
    {
      e.RowData.IconSize = 21;
      e.RowData.Icon = ProjectTeamsForm._iconGroup;
    }
    if (e.Row.Level != 2)
      return;
    MyElementEx myElementEx = (MyElementEx) e.Row.Item;
    e.RowData.IconSize = 21;
    e.RowData.Icon = e.Row.ParentRow.ChildIndex == 0 ? ProjectTeamsForm._iconManager : (myElementEx.ElementBool2 ? ProjectTeamsForm._iconUser : ProjectTeamsForm._iconGroup);
  }

  /// <summary>Получить данные для ячейки</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void projectTeam_GetCellData(object sender, GetCellDataEventArgs e)
  {
    if (e.Row.Level == 1)
    {
      int num = (int) e.Row.Item;
      if (e.Column == this.columnTeam)
      {
        switch (num)
        {
          case 0:
            e.CellData.Value = (object) LocalizationHolder.rm.GetString("Client.Core_628");
            break;
          case 1:
            e.CellData.Value = (object) LocalizationHolder.rm.GetString("Client.Core_627");
            break;
        }
      }
    }
    if (e.Row.Level != 2)
      return;
    MyElementEx myElementEx = (MyElementEx) e.Row.Item;
    if (e.Column == this.columnTeam)
      e.CellData.Value = (object) myElementEx.Caption;
    if (e.Column != this.columnLevel)
      return;
    e.CellData.Value = (object) (string) myElementEx.Tags[0];
  }

  /// <summary>Нажата кнопка "Применить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btnApply_Click(object sender, EventArgs e)
  {
    if (!this._isChanged || this._editorMode != ProjectTeamsForm.EditorMode.EditorMode)
      return;
    this._orgManagers.Clear();
    this._orgUsers.Clear();
    for (int index = 0; index < this._managers.Count; ++index)
      this._orgManagers.Add(this._managers[index].Clone() as MyElementEx);
    for (int index = 0; index < this._users.Count; ++index)
      this._orgUsers.Add(this._users[index].Clone() as MyElementEx);
    this._isChanged = false;
    this.UpdateControls();
  }

  /// <summary>Нажата кнопка "Отменить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btnCancel_Click(object sender, EventArgs e)
  {
    if (this.objectID == 0L || !this._isChanged)
      return;
    this.UpdateControls();
    if (this._editorMode != ProjectTeamsForm.EditorMode.EditorMode || MessageBox.Show(LocalizationHolder.rm.GetString(sc_4289.ssp_imclient_4291()), LocalizationHolder.rm.GetString("Client.Core_132"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.objectID, false);
      if (!(dbObject is IDBProjectObject dbProjectObject) || !dbProjectObject.IsProjectManager(sessionKeeper.Session.UserID))
        return;
      ProjectParticipantInfo[] participants = dbProjectObject.GetParticipants();
      long[] users = new long[participants.Length - 1];
      int index1 = 0;
      for (int index2 = 0; index2 < participants.Length; ++index2)
      {
        if (participants[index2].ParticipantID != this._userAndRole.UserID)
        {
          users[index1] = participants[index2].ParticipantID;
          ++index1;
        }
      }
      dbProjectObject.ExcludeParticipants(users);
      List<ProjectParticipantInfo> collection = new List<ProjectParticipantInfo>();
      for (int index3 = 0; index3 < this._orgManagers.Count; ++index3)
      {
        if ((long) this._orgManagers[index3].Value != this._userAndRole.UserID)
          collection.Add(new ProjectParticipantInfo((long) this._orgManagers[index3].Value, true));
      }
      for (int index4 = 0; index4 < this._orgUsers.Count; ++index4)
        collection.Add(new ProjectParticipantInfo((long) this._orgUsers[index4].Value, false));
      if (collection.Count > 0)
      {
        if (dbObject.IsCreationMode)
          this.Participant.AddRange((IEnumerable<ProjectParticipantInfo>) collection);
        else
          dbProjectObject.IncludeParticipants(collection.ToArray());
      }
    }
    this._isChanged = false;
    this.LoadViewData();
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProjectTeamsForm));
    this.columnTeam = new Column();
    this.columnLevel = new Column();
    this.btnCancel = new Button();
    this.panelBottom = new Panel();
    this.btnApply = new Button();
    this.imageList = new ImageList(this.components);
    this.toolTip = new ToolTip(this.components);
    this.btnAdd = new Button();
    this.btnDel = new Button();
    this.panelControls = new Panel();
    this.projectTeam = new Intermech.VirtualTreeView.VirtualTreeView();
    this.panelBottom.SuspendLayout();
    this.panelControls.SuspendLayout();
    this.projectTeam.BeginInit();
    this.SuspendLayout();
    this.columnTeam.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    componentResourceManager.ApplyResources((object) this.columnTeam, "columnTeam");
    this.columnTeam.CellStyle.BorderColor = SystemColors.ControlDark;
    this.columnTeam.CellStyle.BorderStyle = Border3DStyle.Adjust;
    this.columnTeam.CellStyle.BorderWidth = 0;
    this.columnTeam.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnTeam.HeaderStyle.HorzAlignment");
    this.columnTeam.Movable = false;
    this.columnTeam.Name = "columnTeam";
    this.columnTeam.Sortable = false;
    this.columnLevel.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    componentResourceManager.ApplyResources((object) this.columnLevel, "columnLevel");
    this.columnLevel.CellStyle.BorderColor = SystemColors.Control;
    this.columnLevel.CellStyle.BorderStyle = Border3DStyle.Adjust;
    this.columnLevel.CellStyle.BorderWidth = 1;
    this.columnLevel.Movable = false;
    this.columnLevel.Name = "columnLevel";
    this.columnLevel.Sortable = false;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Default;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.panelBottom.Controls.Add((Control) this.btnCancel);
    this.panelBottom.Controls.Add((Control) this.btnApply);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Cursor = Cursors.Default;
    this.btnApply.Name = "btnApply";
    this.btnApply.Click += new EventHandler(this.btnApply_Click);
    this.imageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList.ImageStream");
    this.imageList.TransparentColor = Color.Transparent;
    this.imageList.Images.SetKeyName(0, "users.ico");
    this.imageList.Images.SetKeyName(1, "adim.ico");
    this.imageList.Images.SetKeyName(2, "user.ico");
    this.btnAdd.Cursor = Cursors.Default;
    componentResourceManager.ApplyResources((object) this.btnAdd, "btnAdd");
    this.btnAdd.Name = "btnAdd";
    this.toolTip.SetToolTip((Control) this.btnAdd, componentResourceManager.GetString("btnAdd.ToolTip"));
    this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
    this.btnDel.Cursor = Cursors.Default;
    componentResourceManager.ApplyResources((object) this.btnDel, "btnDel");
    this.btnDel.Name = "btnDel";
    this.toolTip.SetToolTip((Control) this.btnDel, componentResourceManager.GetString("btnDel.ToolTip"));
    this.btnDel.Click += new EventHandler(this.btnDel_Click);
    this.panelControls.Controls.Add((Control) this.btnDel);
    this.panelControls.Controls.Add((Control) this.btnAdd);
    componentResourceManager.ApplyResources((object) this.panelControls, "panelControls");
    this.panelControls.Name = "panelControls";
    this.projectTeam.AllowDrop = true;
    this.projectTeam.AllowUserPinnedColumns = false;
    this.projectTeam.AutoFitColumns = true;
    this.projectTeam.Columns.Add(this.columnTeam);
    this.projectTeam.Columns.Add(this.columnLevel);
    this.projectTeam.DisableHeaderContextMenu = true;
    componentResourceManager.ApplyResources((object) this.projectTeam, "projectTeam");
    this.projectTeam.ImageList = (ImageList) null;
    this.projectTeam.LineStyle = LineStyle.Dot;
    this.projectTeam.MainColumn = this.columnTeam;
    this.projectTeam.Name = "projectTeam";
    this.projectTeam.RowStyle.BorderColor = SystemColors.Control;
    this.projectTeam.RowStyle.BorderWidth = 1;
    this.projectTeam.SelectBeforeEdit = true;
    this.projectTeam.ShowRootRow = false;
    this.projectTeam.SuppressErrorMessages = true;
    this.projectTeam.GetAllowedRowDropLocations += new GetAllowedRowDropLocationsHandler(this.projectTeam_GetAllowedRowDropLocations);
    this.projectTeam.GetCellData += new GetCellDataHandler(this.projectTeam_GetCellData);
    this.projectTeam.GetChildren += new GetChildrenHandler(this.projectTeam_GetChildren);
    this.projectTeam.GetRowData += new GetRowDataHandler(this.projectTeam_GetRowData);
    this.projectTeam.GetRowDropEffect += new GetRowDropEffectHandler(this.projectTeam_GetRowDropEffect);
    this.projectTeam.SelectionChanged += new EventHandler(this.projectTeam_SelectionChanged);
    this.projectTeam.DragDrop += new DragEventHandler(this.projectTeam_DragDrop);
    this.projectTeam.DragEnter += new DragEventHandler(this.projectTeam_DragEnter);
    this.projectTeam.DragOver += new DragEventHandler(this.projectTeam_DragOver);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.projectTeam);
    this.Controls.Add((Control) this.panelControls);
    this.Controls.Add((Control) this.panelBottom);
    this.Name = nameof (ProjectTeamsForm);
    this.panelBottom.ResumeLayout(false);
    this.panelControls.ResumeLayout(false);
    this.projectTeam.EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>Права доступа к объекту в закладке</summary>
  internal enum EditorMode
  {
    /// <summary>Объект некорректен, закладка пуста</summary>
    None,
    /// <summary>Только просмотр</summary>
    ReadOnly,
    /// <summary>Режим администратора</summary>
    EditorMode,
  }
}
