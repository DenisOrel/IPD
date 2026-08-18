
// Type: Intermech.Client.Core.FormDesigner.TabPages.Forms4TypeForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Controls;
using Intermech.Expert;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Expert;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using Intermech.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.TabPages;

/// <summary>
/// 
/// </summary>
public class Forms4TypeForm : TabPageForm
{
  private bool clipboardActivated;
  private TempFormula clipboardCondition;
  /// <summary>
  /// Глобальный идентификатор атрибута "Глобальный идентификатор типа объекта"/"Глобальный идентификатор типа связи"
  /// </summary>
  private Guid _attrGuid = Guid.Empty;
  /// <summary>
  /// Глобальный идентификатор текущего типа объектов/связей
  /// </summary>
  private Guid _typeGuid = Guid.Empty;
  /// <summary>
  /// Формы, которые были взяты на изменение и их надо зачекинить обратно
  /// </summary>
  private List<long> _needFromsCheckIn = new List<long>();
  /// <summary>Действия, производимые на форме</summary>
  private List<FormAction> _actions = new List<FormAction>();
  /// <summary>"Client.Core_196" = Нет условия</summary>
  private string _strNullConditions = LocalizationHolder.rm.GetString("Client.Core_196");
  /// <summary>Флаг доступности экспертной системы</summary>
  private bool _expertEditorAvailable;
  /// <summary>
  /// Флаг наличия собственных форм (у типа объектов/связей собственные формы или родительские)
  /// </summary>
  private bool _hasOwnForms = true;
  private bool _compareFolders = true;
  private int sysFormsObjectTypeId;
  private int sysGlobals4objtypeAttrId;
  private int sysGlobals4reltypeAttrId;
  internal TempFormula curFormula;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ContextMenuStrip _contextMenu;
  private ToolStripMenuItem _miAddForm;
  private ToolStripMenuItem _miExcludeForm;
  private ToolStripSeparator tsS1;
  private ToolStripMenuItem _miIncludeUser;
  private ToolStripMenuItem _miExclude;
  private ToolStripSeparator tsS2;
  private ToolStripMenuItem _miCondition;
  private ToolStripMenuItem _miEdit;
  private ToolStripMenuItem _miOpenInNewWindow;
  private RichTextBox _txtCondition;
  private TreeView _trvForms;
  private Splitter splitter1;
  private ToolTip toolTipFE;
  private TableLayoutPanel _tlpSelecetdFields;
  private Button _btnTop;
  private Button _btnUp;
  private Button _btnBottom;
  private Button _btnDown;
  private ImageList _imgList;
  private ToolStripMenuItem _miIncludeRole;
  private ToolStripMenuItem _miCheckInForm;
  private ToolStripMenuItem _miCancelChangesForm;
  private ToolStripSeparator toolStripSeparator1;
  private ContextMenuStrip _contextMenuCondition;
  private ToolStripMenuItem _miCondEdit;
  private ToolStripMenuItem _miCondCopy;
  private ToolStripMenuItem _miCondPaste;
  private ToolStripMenuItem _miCondDelete;

  /// <summary>Конструктор.</summary>
  /// <param name="instanceGuid"></param>
  public Forms4TypeForm(Guid instanceGuid)
    : base(instanceGuid)
  {
    this.InitializeComponent();
    this.Dock = DockStyle.Fill;
    this._trvForms.ImageList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
    this._txtCondition.Enabled = this._expertEditorAvailable = ServicesManager.GetService(typeof (IExpertEditor)) != null;
    this.sysFormsObjectTypeId = MetaDataHelper.GetObjectTypeID("cad0011b-306c-11d8-b4e9-00304f19f545");
    this.sysGlobals4objtypeAttrId = MetaDataHelper.GetAttributeID((object) "cad00149-306c-11d8-b4e9-00304f19f545");
    this.sysGlobals4reltypeAttrId = MetaDataHelper.GetAttributeID((object) "cad0014a-306c-11d8-b4e9-00304f19f545");
  }

  /// <summary>Заполнение формы.</summary>
  /// <param name="folder"></param>
  public override void FillForm(IFolder folder)
  {
    if (this._compareFolders && folder == this._folder)
      return;
    this._folder = folder as CustomFolder;
    this._compareFolders = true;
    int int32 = Convert.ToInt32(folder.Id);
    this._actions.Clear();
    this.curFormula = (TempFormula) null;
    this._hasOwnForms = true;
    this._txtCondition.Text = string.Empty;
    Dictionary<int, FormInformation> forms = (Dictionary<int, FormInformation>) null;
    switch (folder)
    {
      case ObjectTypeFolder _:
        this._attrGuid = GuidHolder.GlobalObjGuid;
        this._typeGuid = MetaDataHelper.GetObjectTypeGuid(int32);
        forms = this.GetForms(int32, AttributableElements.Object);
        break;
      case RelationTypeFolder _:
        this._attrGuid = GuidHolder.GlobalRelGuid;
        this._typeGuid = MetaDataHelper.GetRelationTypeGuid(int32);
        forms = this.GetForms(int32, AttributableElements.Relation);
        break;
    }
    this.BuildTree(forms);
    this._trvForms.ForeColor = this._txtCondition.ForeColor = this._hasOwnForms ? SystemColors.WindowText : SystemColors.GrayText;
    if (!this._expertEditorAvailable)
    {
      this._txtCondition.ForeColor = Color.Red;
      this._txtCondition.Text = LocalizationHolder.rm.GetString("Forms4TypeForm.ExpertModul.LoadError.Msg");
    }
    this.SetEnabledButtons();
  }

  /// <summary>Обновление формы.</summary>
  /// <returns></returns>
  public override bool RefreshAfterCanceling()
  {
    this._compareFolders = false;
    if (this._needFromsCheckIn.Count > 0)
    {
      List<long> objectIDs = new List<long>(this._needFromsCheckIn.Count);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (long objectID in this._needFromsCheckIn)
        {
          IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objectID, false);
          if (objectActualCopy.ObjectID <= -1L)
          {
            long objectId = objectActualCopy.ObjectID;
            objectActualCopy.CancelChanges();
            objectIDs.Add(objectId);
          }
        }
      }
      (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChangesCancelled", (IList<long>) objectIDs));
      this._needFromsCheckIn.Clear();
    }
    this.SetEnabledButtons();
    return true;
  }

  /// <summary>Сохранение изменений.</summary>
  /// <param name="folder"></param>
  /// <returns></returns>
  public override bool SaveForm(IFolder folder)
  {
    if (this._folder != null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IFormDesignerService customService = sessionKeeper.Session.GetCustomService(typeof (IFormDesignerService)) as IFormDesignerService;
        INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
        if (this._folder.InChange && this._actions.Count > 0)
        {
          foreach (FormAction action in this._actions)
          {
            IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(action.FormID, false);
            if (objectActualCopy != null)
            {
              IDBObject dbObject = this.CheckOut(objectActualCopy, sessionKeeper.Session, action.ActionType);
              switch (action.ActionType)
              {
                case Forms4ActionType.AddForm:
                  this.CommitAddForm(dbObject, customService, service);
                  continue;
                case Forms4ActionType.DeleteForm:
                  IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(this._attrGuid);
                  if (attributeByGuid != null)
                  {
                    string str = Convert.ToString((object) this._typeGuid);
                    int num = 0;
                    while (num < attributeByGuid.ValuesCount)
                    {
                      attributeByGuid.Index = num;
                      if (attributeByGuid.AsString == str)
                      {
                        if (attributeByGuid.ValuesCount > 1)
                        {
                          attributeByGuid.DeleteValue();
                        }
                        else
                        {
                          attributeByGuid.Clear();
                          ++num;
                        }
                      }
                      else
                        ++num;
                    }
                    customService?.ChangeFormsVisibleForUserCache(dbObject, Convert.ToInt32(folder.Id), false);
                  }
                  service.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", dbObject.ObjectID));
                  continue;
                case Forms4ActionType.Include:
                  this.CommitInclude(sessionKeeper.Session, dbObject, action, customService, service);
                  continue;
                case Forms4ActionType.Exclude:
                  this.CommitExclude(sessionKeeper.Session, dbObject, action, customService, service);
                  continue;
                case Forms4ActionType.SetCondition:
                  this.CommitSetCondition(sessionKeeper.Session, dbObject, action, customService, service);
                  continue;
                default:
                  continue;
              }
            }
          }
        }
        this.SaveDisplayIndexesInfo(customService);
        List<long> objectIDs = new List<long>(this._needFromsCheckIn.Count);
        foreach (long objectID in this._needFromsCheckIn)
        {
          IDBObject objectActualCopy1 = sessionKeeper.Session.GetObjectActualCopy(objectID, false);
          if (objectActualCopy1 != null && objectActualCopy1.ObjectID <= 0L)
          {
            long objectId1 = objectActualCopy1.ObjectID;
            service.FireEvent((object) this, (NotificationEventArgs) new BeforeFormObjectCheckinEventArgs(objectId1));
            IDBObject objectActualCopy2 = sessionKeeper.Session.GetObjectActualCopy(objectID, false);
            if (objectActualCopy2 != null && objectActualCopy2.ObjectID <= 0L)
            {
              long objectId2 = objectActualCopy2.ObjectID;
              objectActualCopy2.CheckIn();
              objectIDs.Add(objectId2);
            }
          }
        }
        service.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCheckedIn", (IList<long>) objectIDs));
        this._needFromsCheckIn.Clear();
      }
      this._actions.Clear();
    }
    return true;
  }

  /// <summary>Перемещение элементов по позициям в списке.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnUpDown_Click(object sender, EventArgs e)
  {
    TreeNode selectedNode = this._trvForms.SelectedNode;
    if (selectedNode == null)
      return;
    int index = selectedNode.Index;
    this._trvForms.BeginUpdate();
    try
    {
      this._trvForms.Nodes.RemoveAt(index);
      switch (Convert.ToInt16((sender as Button).Tag))
      {
        case 0:
          this._trvForms.Nodes.Insert(0, selectedNode);
          break;
        case 1:
          this._trvForms.Nodes.Insert(index > 0 ? index - 1 : 0, selectedNode);
          break;
        case 2:
          this._trvForms.Nodes.Insert(index + 1, selectedNode);
          break;
        case 3:
          this._trvForms.Nodes.Insert(this._trvForms.Nodes.Count, selectedNode);
          break;
      }
      this._trvForms.SelectedNode = selectedNode;
    }
    finally
    {
      this._trvForms.EndUpdate();
    }
    (sender as Button).Focus();
    if (this._folder.InChange)
      return;
    EventsHolder.FireWasChanged((object) this, this.instGuid, EventArgs.Empty);
    this._folder.InChange = true;
  }

  /// <summary>Открытие контекстного меню.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_contextMenu_Opened(object sender, EventArgs e)
  {
    if (this._trvForms.SelectedNode != null && this._trvForms.SelectedNode.Level == 0 && this._hasOwnForms)
    {
      this._miEdit.Enabled = this._miExcludeForm.Enabled = this._miIncludeUser.Enabled = this._miIncludeRole.Enabled = true;
      this._miExclude.Enabled = false;
      this._miCheckInForm.Enabled = this._miCancelChangesForm.Enabled = true;
    }
    else
    {
      this._miEdit.Enabled = this._miExcludeForm.Enabled = this._miIncludeUser.Enabled = this._miIncludeRole.Enabled = this._miExclude.Enabled = false;
      this._miCheckInForm.Enabled = this._miCancelChangesForm.Enabled = false;
    }
    if (this._trvForms.SelectedNode != null && this._trvForms.SelectedNode.Level > 0 && this._hasOwnForms)
      this._miExclude.Enabled = true;
    this._miCondition.Enabled = this._miExcludeForm.Enabled && this._expertEditorAvailable;
    this._miOpenInNewWindow.Enabled = this._trvForms.SelectedNode != null;
    this._miCondEdit.Enabled = this._miCondition.Enabled;
    this._miCondCopy.Enabled = this._miCondDelete.Enabled = this._miExcludeForm.Enabled;
    this._miCondPaste.Enabled = this._miExcludeForm.Enabled && this.clipboardActivated;
  }

  /// <summary>Выбор одного из пунктов меню.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_miItem_Click(object sender, EventArgs e)
  {
    if (sender == null)
      return;
    if (sender == this._miAddForm)
      this.AddForm();
    else if (sender == this._miEdit || sender == this._miOpenInNewWindow)
      this.OpenInNewWindow(this._trvForms.SelectedNode, sender == this._miEdit);
    else if (sender == this._miExcludeForm)
      this.DeleteForm(this._trvForms.SelectedNode);
    else if (sender == this._miCheckInForm)
      this.CheckInForm(this._trvForms.SelectedNode);
    else if (sender == this._miCancelChangesForm)
      this.CancelChangesForm(this._trvForms.SelectedNode);
    else if (sender == this._miIncludeUser || sender == this._miIncludeRole)
      this.Include(this._trvForms.SelectedNode, sender == this._miIncludeRole);
    else if (sender == this._miExclude)
      this.Exclude(this._trvForms.SelectedNode);
    else if (sender == this._miCondition)
      this.SetCondition(this._trvForms.SelectedNode, Forms4TypeForm.ConditionOperation.Edit);
    if (this._actions.Count > 0 && !this._folder.InChange)
    {
      EventsHolder.FireWasChanged((object) this, this.instGuid, EventArgs.Empty);
      this._folder.InChange = true;
    }
    this.SetEnabledButtons();
  }

  private void On_miCondItem_Click(object sender, EventArgs e)
  {
    Forms4TypeForm.ConditionOperation operation = Forms4TypeForm.ConditionOperation.None;
    if (sender == this._miCondEdit)
      operation = Forms4TypeForm.ConditionOperation.Edit;
    else if (sender == this._miCondCopy)
      operation = Forms4TypeForm.ConditionOperation.Copy;
    else if (sender == this._miCondPaste)
      operation = Forms4TypeForm.ConditionOperation.Paste;
    else if (sender == this._miCondDelete)
      operation = Forms4TypeForm.ConditionOperation.Delete;
    if (operation == Forms4TypeForm.ConditionOperation.None || this._trvForms.SelectedNode == null)
      return;
    this.SetCondition(this._trvForms.SelectedNode, operation);
    if (this._actions.Count > 0 && !this._folder.InChange)
    {
      EventsHolder.FireWasChanged((object) this, this.instGuid, EventArgs.Empty);
      this._folder.InChange = true;
    }
    this.SetEnabledButtons();
  }

  /// <summary>Выделения узла в дереве.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_trvForms_AfterSelect(object sender, TreeViewEventArgs e)
  {
    TreeNode selectedNode = this._trvForms.SelectedNode;
    if (selectedNode == null)
      return;
    if (selectedNode.Level == 0)
    {
      Forms4TypeForm.FormInfo tag = selectedNode.Tag as Forms4TypeForm.FormInfo;
      if (!tag.CheckedCondition)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(tag.FormID, false);
          if (objectActualCopy != null)
          {
            tag.Condition = CondHelper.LoadObjectCond(sessionKeeper.Session, objectActualCopy.ObjectID);
            tag.SetCheckedCondition();
          }
        }
      }
      this.curFormula = tag.Condition;
      if (this.curFormula != null)
        this.ShowFormula();
      else
        this._txtCondition.Text = this._strNullConditions;
    }
    else
    {
      this._txtCondition.Text = string.Empty;
      this.curFormula = (TempFormula) null;
    }
    this.SetEnabledButtons();
  }

  /// <summary>Клик по дереву.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_trvForms_MouseDown(object sender, MouseEventArgs e)
  {
    this._trvForms.SelectedNode = this._trvForms.GetNodeAt(new Point(e.X, e.Y));
    if (this._trvForms.SelectedNode != null)
      return;
    this._txtCondition.Text = string.Empty;
    this.curFormula = (TempFormula) null;
    this.SetEnabledButtons();
  }

  /// <summary>Двойной клик мышкой по полю условие.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txtCondition_DoubleClick(object sender, EventArgs e)
  {
    if (this._trvForms.SelectedNode == null || this._trvForms.SelectedNode.Level != 0 || !this._hasOwnForms)
      return;
    this.On_miItem_Click((object) this._miCondition, new EventArgs());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="forms"></param>
  private void BuildTree(Dictionary<int, FormInformation> forms)
  {
    this._trvForms.BeginUpdate();
    try
    {
      this._trvForms.Nodes.Clear();
      if (forms == null)
        return;
      DataTable actualFormInfo = this.GetActualFormInfo((IEnumerable<FormInformation>) forms.Values);
      if (actualFormInfo == null)
        return;
      Tuple<long, int, string>[] source = new Tuple<long, int, string>[forms.Count];
      int i = 0;
      forms = forms.OrderBy<KeyValuePair<int, FormInformation>, int>((System.Func<KeyValuePair<int, FormInformation>, int>) (x => x.Key)).ToDictionary<KeyValuePair<int, FormInformation>, int, FormInformation>((System.Func<KeyValuePair<int, FormInformation>, int>) (x => i++), (System.Func<KeyValuePair<int, FormInformation>, FormInformation>) (y => y.Value));
      List<ObjInfoItem> formInfoList = new List<ObjInfoItem>(actualFormInfo.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) actualFormInfo.Rows)
      {
        long formID = Convert.ToInt64(row[0]);
        long absFormID = Math.Abs(formID);
        int int32 = Convert.ToInt32(row[1]);
        KeyValuePair<int, FormInformation> keyValuePair = forms.FirstOrDefault<KeyValuePair<int, FormInformation>>((System.Func<KeyValuePair<int, FormInformation>, bool>) (x => x.Value.ID == absFormID));
        if (keyValuePair.Value == null)
          keyValuePair = forms.FirstOrDefault<KeyValuePair<int, FormInformation>>((System.Func<KeyValuePair<int, FormInformation>, bool>) (x => x.Value.ID == formID));
        source[keyValuePair.Key] = new Tuple<long, int, string>(formID, int32, Convert.ToString(row[2]));
        formInfoList.Add(new ObjInfoItem(formID, int32));
        forms.Remove(keyValuePair.Key);
      }
      Tuple<long, int, string>[] array1 = ((IEnumerable<Tuple<long, int, string>>) source).Where<Tuple<long, int, string>>((System.Func<Tuple<long, int, string>, bool>) (x => x != null)).ToArray<Tuple<long, int, string>>();
      DataTable users = this.GetUsers(formInfoList);
      TreeNode[] array2;
      if (users != null)
      {
        EnumerableRowCollection<DataRow> userRows = users.AsEnumerable();
        array2 = ((IEnumerable<Tuple<long, int, string>>) array1).Select<Tuple<long, int, string>, TreeNode>((System.Func<Tuple<long, int, string>, TreeNode>) (x => this.CreateFormNode(x.Item1, x.Item2, x.Item3, userRows.Where<DataRow>((System.Func<DataRow, bool>) (y => Convert.ToInt64(y[0]) == x.Item1)).ToList<DataRow>()))).ToArray<TreeNode>();
      }
      else
        array2 = ((IEnumerable<Tuple<long, int, string>>) array1).Select<Tuple<long, int, string>, TreeNode>((System.Func<Tuple<long, int, string>, TreeNode>) (x => this.CreateFormNode(x.Item1, x.Item2, x.Item3, (List<DataRow>) null))).ToArray<TreeNode>();
      this._trvForms.Nodes.AddRange(array2);
    }
    finally
    {
      this._trvForms.EndUpdate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="id"></param>
  /// <param name="typeID"></param>
  /// <param name="caption"></param>
  /// <param name="userRows"></param>
  /// <returns></returns>
  private TreeNode CreateFormNode(long id, int typeID, string caption, List<DataRow> userRows)
  {
    if (string.IsNullOrEmpty(caption.Trim()))
      caption = string.Format(LocalizationHolder.rm.GetString("Client.Core_191"), (object) id);
    Forms4TypeForm.FormInfo formInfo = new Forms4TypeForm.FormInfo(Math.Abs(id));
    TreeNode formNode = new TreeNode(caption)
    {
      Tag = (object) formInfo
    };
    if (this._trvForms.ImageList != null)
      formNode.SelectedImageIndex = formNode.ImageIndex = Statics.IconSrv.IndexOf(4, typeID);
    if (userRows != null)
    {
      List<long> longList = new List<long>(userRows.Count);
      foreach (DataRow userRow in userRows)
      {
        long int64 = Convert.ToInt64(userRow[1]);
        TreeNode userNode = this.CreateUserNode(int64, Convert.ToInt32(userRow[2]), Convert.ToString(userRow[3]));
        formNode.Nodes.Add(userNode);
        longList.Add(Math.Abs(int64));
      }
      formInfo.UserIDs = longList;
    }
    return formNode;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="id"></param>
  /// <param name="typeID"></param>
  /// <param name="caption"></param>
  /// <returns></returns>
  private TreeNode CreateUserNode(long id, int typeID, string caption)
  {
    if (string.IsNullOrEmpty(caption))
      caption = string.Format(LocalizationHolder.rm.GetString("Client.Core_192"), (object) MetaDataHelper.GetObjectType(typeID).ObjectName, (object) id);
    TreeNode userNode = new TreeNode(caption)
    {
      Tag = (object) Math.Abs(id)
    };
    if (this._trvForms.ImageList != null)
      userNode.SelectedImageIndex = userNode.ImageIndex = Statics.IconSrv.IndexOf(4, typeID);
    return userNode;
  }

  /// <summary>
  /// Получить формы, назначенные типу объектов/связи, при отсутствии получить формы родителя.
  /// </summary>
  /// <param name="typeID">Идентификатор типа объектов/связи</param>
  /// <param name="kind">Тип элемента</param>
  /// <returns>Список форм</returns>
  private Dictionary<int, FormInformation> GetForms(int typeID, AttributableElements kind)
  {
    Dictionary<int, FormInformation> forms = this.GetFormsForObjectsType(typeID, kind);
    if (forms == null)
    {
      this._hasOwnForms = false;
      forms = this.GetParentsForms(typeID, kind);
    }
    return forms;
  }

  /// <summary>Получить формы формы родителя.</summary>
  /// <param name="typeID">Идентификатор типа объектов/связей</param>
  /// <param name="kind">Тип элемента</param>
  /// <returns>Список форм</returns>
  private Dictionary<int, FormInformation> GetParentsForms(int typeID, AttributableElements kind)
  {
    Dictionary<int, FormInformation> parentsForms = (Dictionary<int, FormInformation>) null;
    List<int> objectTypeParentsId = MetaDataHelper.GetObjectTypeParentsID(typeID);
    if (objectTypeParentsId.Count == 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        MetaDataHelper.SyncMetadata((sessionKeeper.Session as IUserSessionCacheDataSet).CacheDataSet);
      objectTypeParentsId = MetaDataHelper.GetObjectTypeParentsID(typeID);
    }
    foreach (int typeID1 in objectTypeParentsId)
    {
      parentsForms = this.GetFormsForObjectsType(typeID1, kind);
      if (parentsForms != null)
        break;
    }
    return parentsForms;
  }

  /// <summary>Получить формы, назначенные типу объектов/связей.</summary>
  /// <param name="typeID">Идентификатор типа объектов/связей</param>
  /// <param name="kind">Тип элемента</param>
  /// <returns>Список форм</returns>
  private Dictionary<int, FormInformation> GetFormsForObjectsType(
    int typeID,
    AttributableElements kind)
  {
    Dictionary<int, FormInformation> formsForObjectsType1 = (Dictionary<int, FormInformation>) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long userId = sessionKeeper.Session.UserID;
      if (sessionKeeper.Session.GetCustomService(typeof (IFormDesignerService)) is IFormDesignerService customService)
      {
        if (userId != 0L)
        {
          Dictionary<FormInformation, bool[]> formsForObjectsType2 = customService.GetFormsForObjectsType(typeID, kind);
          if (formsForObjectsType2.Count > 0)
          {
            Dictionary<FormInformation, int> source = new Dictionary<FormInformation, int>(formsForObjectsType2.Count);
            Dictionary<Guid, int> dictionary = customService.GetFormDisplayOrderForType(this._typeGuid) ?? new Dictionary<Guid, int>(0);
            IObjectsInfoCache service = ApplicationServices.Container.GetService<IObjectsInfoCache>();
            foreach (KeyValuePair<FormInformation, bool[]> keyValuePair in formsForObjectsType2)
            {
              if (keyValuePair.Value[0] && keyValuePair.Key.CheckOutBy != userId || keyValuePair.Value[1] && keyValuePair.Key.CheckOutBy == userId)
              {
                QuickObjectInfo objectInfo = service.GetObjectInfo(keyValuePair.Key.ID);
                if (!objectInfo.Empty)
                {
                  int num = dictionary.ContainsKey(objectInfo.VersionGuid) ? dictionary[objectInfo.VersionGuid] : -1;
                  source.Add(keyValuePair.Key, num);
                }
              }
            }
            if (source.Count > 0)
            {
              int maxIndex = source.Values.Max();
              formsForObjectsType1 = source.ToDictionary<KeyValuePair<FormInformation, int>, int, FormInformation>((System.Func<KeyValuePair<FormInformation, int>, int>) (x => x.Value <= -1 ? ++maxIndex : x.Value), (System.Func<KeyValuePair<FormInformation, int>, FormInformation>) (y => y.Key));
            }
          }
        }
      }
    }
    return formsForObjectsType1;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objForm"></param>
  /// <param name="formDesignerSrv"></param>
  /// <param name="notify"></param>
  private void CommitAddForm(
    IDBObject objForm,
    IFormDesignerService formDesignerSrv,
    INotificationService notify)
  {
    IDBAttribute attributeByGuid = objForm.GetAttributeByGuid(this._attrGuid);
    if (attributeByGuid == null)
      return;
    if (attributeByGuid.ValuesCount == 1 && attributeByGuid.Value == DBNull.Value)
      attributeByGuid.Value = (object) this._typeGuid;
    else
      attributeByGuid.AddValue((object) this._typeGuid);
    notify.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", objForm.ObjectID));
    formDesignerSrv?.ChangeFormsVisibleForUserCache(objForm, Convert.ToInt32(this._folder.Id), true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="objForm"></param>
  /// <param name="action"></param>
  /// <param name="formDesignerSrv"></param>
  /// <param name="notify"></param>
  private void CommitInclude(
    IUserSession session,
    IDBObject objForm,
    FormAction action,
    IFormDesignerService formDesignerSrv,
    INotificationService notify)
  {
    IDBRelation dbRelation = session.GetRelationCollection(session.IdentHelper.SimpleRelationTypeID).Create(objForm.ObjectID, action.UserID);
    notify.FireEvent((object) this, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", dbRelation.RelationID, dbRelation.ProjID, dbRelation.RelationType));
    formDesignerSrv?.ChangeFormsVisible(objForm);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="objForm"></param>
  /// <param name="action"></param>
  /// <param name="formDesignerSrv"></param>
  /// <param name="notify"></param>
  private void CommitExclude(
    IUserSession session,
    IDBObject objForm,
    FormAction action,
    IFormDesignerService formDesignerSrv,
    INotificationService notify)
  {
    IDBRelation relation = session.GetRelation(objForm.ObjectID, action.UserID, true);
    if (relation != null)
    {
      relation.Delete(0L);
      notify.FireEvent((object) this, (NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", relation.RelationID, relation.ProjID, relation.RelationType));
    }
    formDesignerSrv?.ChangeFormsVisible(objForm);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="objForm"></param>
  /// <param name="action"></param>
  /// <param name="formDesignerSrv"></param>
  /// <param name="notify"></param>
  private void CommitSetCondition(
    IUserSession session,
    IDBObject objForm,
    FormAction action,
    IFormDesignerService formDesignerSrv,
    INotificationService notify)
  {
    if (action.Condition != null)
      CondHelper.SaveObjectCond(session, objForm.ObjectID, action.Condition);
    else
      objForm.GetAttributeByGuid(GuidHolder.ConditionAttrGuid, false)?.Delete(0L);
    notify.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChanged", objForm.ObjectID));
    formDesignerSrv?.ChangeFormsCondition(objForm, (object) action.Condition);
  }

  /// <summary>Зачитать актуальный список форм</summary>
  /// <returns></returns>
  private Dictionary<int, FormInformation> GetFormInformationList()
  {
    Dictionary<int, FormInformation> dictionary = (Dictionary<int, FormInformation>) null;
    int int32 = Convert.ToInt32(this._folder.Id);
    if (this._folder is ObjectTypeFolder)
      dictionary = this.GetForms(int32, AttributableElements.Object);
    else if (this._folder is RelationTypeFolder)
      dictionary = this.GetForms(int32, AttributableElements.Relation);
    return dictionary ?? new Dictionary<int, FormInformation>();
  }

  /// <summary>
  /// 
  /// </summary>
  private void AddForm()
  {
    int objectTypeId = MetaDataHelper.GetObjectTypeID("cad0011c-306c-11d8-b4e9-00304f19f545");
    long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Client.Core_193"), string.Empty, objectTypeId, SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return;
    Dictionary<int, FormInformation> formInformationList = this.GetFormInformationList();
    List<Tuple<long, int, string>> source = new List<Tuple<long, int, string>>();
    List<ObjInfoItem> formInfoList = new List<ObjInfoItem>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long objectID in numArray)
      {
        FormInformation formInformation = (FormInformation) null;
        foreach (KeyValuePair<int, FormInformation> keyValuePair in formInformationList)
        {
          if (keyValuePair.Value.ID.Equals(Math.Abs(objectID)))
          {
            formInformation = keyValuePair.Value;
            break;
          }
        }
        if (formInformation != null)
        {
          int num = (int) IMMessageBox.Show("Внимание", $"Форма \"{formInformation.Caption}\" уже назначена типу", MessageBoxButtons.OK, IMMessageBoxImage.Warning);
        }
        else
        {
          IDBObject dbObject = this.CheckOut(sessionKeeper.Session.GetObjectActualCopy(objectID, false), sessionKeeper.Session, Forms4ActionType.AddForm);
          source.Add(new Tuple<long, int, string>(dbObject.ObjectID, dbObject.TypeID, dbObject.Caption));
          formInfoList.Add(new ObjInfoItem(dbObject.ObjectID, dbObject.TypeID));
        }
      }
    }
    DataTable users = this.GetUsers(formInfoList);
    TreeNode[] array;
    if (users != null)
    {
      EnumerableRowCollection<DataRow> userRows = users.AsEnumerable();
      array = source.Select<Tuple<long, int, string>, TreeNode>((System.Func<Tuple<long, int, string>, TreeNode>) (x => this.CreateFormNode(x.Item1, x.Item2, x.Item3, userRows.Where<DataRow>((System.Func<DataRow, bool>) (y => Convert.ToInt64(y[0]) == x.Item1)).ToList<DataRow>()))).ToArray<TreeNode>();
    }
    else
      array = source.Select<Tuple<long, int, string>, TreeNode>((System.Func<Tuple<long, int, string>, TreeNode>) (x => this.CreateFormNode(x.Item1, x.Item2, x.Item3, (List<DataRow>) null))).ToArray<TreeNode>();
    this._trvForms.BeginUpdate();
    try
    {
      if (!this._hasOwnForms)
      {
        this._trvForms.Nodes.Clear();
        this._trvForms.ForeColor = this._txtCondition.ForeColor = SystemColors.WindowText;
      }
      this._trvForms.Nodes.AddRange(array);
      this._hasOwnForms = true;
    }
    finally
    {
      this._trvForms.EndUpdate();
    }
    foreach (Tuple<long, int, string> tuple in source)
    {
      long absID = Math.Abs(tuple.Item1);
      FormAction formAction = this._actions.FirstOrDefault<FormAction>((System.Func<FormAction, bool>) (x => x.FormID == absID));
      if (formAction != null)
        this._actions.Remove(formAction);
      else
        this._actions.Add(new FormAction(Forms4ActionType.AddForm, (IFolder) this._folder, absID));
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="selectedNode"></param>
  /// <param name="isEdit"></param>
  private void OpenInNewWindow(TreeNode selectedNode, bool isEdit)
  {
    long objectID;
    if (selectedNode.Tag is Forms4TypeForm.FormInfo)
    {
      objectID = (selectedNode.Tag as Forms4TypeForm.FormInfo).FormID;
    }
    else
    {
      if (!(selectedNode.Tag is long))
        return;
      objectID = Convert.ToInt64(selectedNode.Tag);
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject formObj = sessionKeeper.Session.GetObjectActualCopy(objectID, false);
      if (isEdit)
        formObj = this.CheckOut(formObj, sessionKeeper.Session, Forms4ActionType.EditForm);
      objectID = formObj.ObjectID;
    }
    ServiceContainer viewServices = new ServiceContainer();
    viewServices.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.ReadOnly));
    ISelectedItems items1 = Intermech.Navigator.ContextMenu.Services.GetItems(objectID);
    if (items1.Count == 0)
    {
      ISelectedItems items2 = Intermech.Navigator.ContextMenu.Services.GetItems(-objectID);
      items1 = items2.Count > 0 ? items2 : items1;
    }
    Intermech.Navigator.ContextMenu.Services.InvokeCommand(nameof (OpenInNewWindow), Intermech.Navigator.ContextMenu.Services.GetCommandsTable(items1, (System.IServiceProvider) viewServices, false), (System.IServiceProvider) viewServices);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="selectedNode"></param>
  private void DeleteForm(TreeNode selectedNode)
  {
    if (MessageBox.Show((IWin32Window) this, string.Format(LocalizationHolder.rm.GetString("Client.Core.ExcludeForm.Question"), (object) selectedNode.Text), LocalizationHolder.rm.GetString("Client.Core.ExcludeForm.Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
      return;
    Forms4TypeForm.FormInfo fi = selectedNode.Tag as Forms4TypeForm.FormInfo;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.CheckOut(sessionKeeper.Session.GetObjectActualCopy(fi.FormID, false), sessionKeeper.Session, Forms4ActionType.DeleteForm);
    this._trvForms.BeginUpdate();
    try
    {
      selectedNode.Remove();
    }
    finally
    {
      this._trvForms.EndUpdate();
    }
    this._trvForms.SelectedNode = (TreeNode) null;
    this._txtCondition.Text = string.Empty;
    this.curFormula = (TempFormula) null;
    List<FormAction> list = this._actions.Where<FormAction>((System.Func<FormAction, bool>) (x => x.FormID == fi.FormID)).ToList<FormAction>();
    bool flag = false;
    if (list.Count > 0)
    {
      foreach (FormAction formAction in list)
      {
        flag = flag || formAction.ActionType == Forms4ActionType.AddForm;
        this._actions.Remove(formAction);
      }
    }
    if (!flag)
      this._actions.Add(new FormAction(Forms4ActionType.DeleteForm, (IFolder) this._folder, fi.FormID));
    if (this._trvForms.Nodes.Count != 0)
      return;
    this._trvForms.ForeColor = this._txtCondition.ForeColor = SystemColors.GrayText;
    this._hasOwnForms = false;
    Dictionary<int, FormInformation> forms = (Dictionary<int, FormInformation>) null;
    if (this._folder is ObjectTypeFolder)
      forms = this.GetParentsForms(Convert.ToInt32(this._folder.Id), AttributableElements.Object);
    else if (this._folder is RelationTypeFolder)
      forms = this.GetParentsForms(Convert.ToInt32(this._folder.Id), AttributableElements.Relation);
    this.BuildTree(forms);
  }

  /// <summary>Завершение редактирования формы.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void CheckInForm(TreeNode selectedNode)
  {
    if (selectedNode == null)
      return;
    Forms4TypeForm.FormInfo fi = selectedNode.Tag as Forms4TypeForm.FormInfo;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(fi.FormID, false);
      if (objectActualCopy != null && objectActualCopy.ObjectID < 0L)
      {
        long objectId = objectActualCopy.ObjectID;
        INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
        List<FormAction> list = this._actions.Where<FormAction>((System.Func<FormAction, bool>) (x => x.FormID == fi.FormID)).ToList<FormAction>();
        if (list.Count > 0)
        {
          IFormDesignerService customService = sessionKeeper.Session.GetCustomService(typeof (IFormDesignerService)) as IFormDesignerService;
          foreach (FormAction action in list)
          {
            switch (action.ActionType)
            {
              case Forms4ActionType.AddForm:
                this.CommitAddForm(objectActualCopy, customService, service);
                break;
              case Forms4ActionType.Include:
                this.CommitInclude(sessionKeeper.Session, objectActualCopy, action, customService, service);
                break;
              case Forms4ActionType.Exclude:
                this.CommitExclude(sessionKeeper.Session, objectActualCopy, action, customService, service);
                break;
              case Forms4ActionType.SetCondition:
                this.CommitSetCondition(sessionKeeper.Session, objectActualCopy, action, customService, service);
                break;
            }
            this._actions.Remove(action);
          }
        }
        objectActualCopy.CheckIn();
        service.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCheckedIn", objectId));
      }
      this._needFromsCheckIn.Remove(fi.FormID);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void CancelChangesForm(TreeNode selectedNode)
  {
    if (selectedNode == null)
      return;
    long formID = (selectedNode.Tag as Forms4TypeForm.FormInfo).FormID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy1 = sessionKeeper.Session.GetObjectActualCopy(formID, false);
      if (objectActualCopy1 == null || objectActualCopy1.ObjectID >= 0L)
        return;
      string caption = LocalizationHolder.rm.GetString("Client.Core_132");
      if (MessageBox.Show((IWin32Window) this, LocalizationHolder.rm.GetString("Client.Core_1227"), caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      long objectId = objectActualCopy1.ObjectID;
      objectActualCopy1.CancelChanges();
      this._needFromsCheckIn.Remove(formID);
      (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChangesCancelled", objectId));
      List<FormAction> list = this._actions.Where<FormAction>((System.Func<FormAction, bool>) (x => x.FormID == formID)).ToList<FormAction>();
      bool flag = true;
      if (list.Count > 0)
      {
        foreach (FormAction formAction in list)
        {
          this._actions.Remove(formAction);
          flag = flag && formAction.ActionType != Forms4ActionType.AddForm;
        }
      }
      this._trvForms.BeginUpdate();
      try
      {
        int index = selectedNode.Index;
        this._trvForms.Nodes.RemoveAt(index);
        TreeNode node = (TreeNode) null;
        if (flag)
        {
          IDBObject objectActualCopy2 = sessionKeeper.Session.GetObjectActualCopy(formID, false);
          DataTable users = this.GetUsers(new List<ObjInfoItem>()
          {
            new ObjInfoItem(formID, objectActualCopy2.ObjectType)
          });
          node = this.CreateFormNode(formID, objectActualCopy2.ObjectType, objectActualCopy2.Caption, users != null ? users.AsEnumerable().ToList<DataRow>() : (List<DataRow>) null);
        }
        if (node != null)
        {
          this._trvForms.Nodes.Insert(index, node);
          this._trvForms.SelectedNode = node;
        }
        else
        {
          if (this._trvForms.SelectedNode != null)
            return;
          this._txtCondition.Text = string.Empty;
          this.curFormula = (TempFormula) null;
        }
      }
      finally
      {
        this._trvForms.EndUpdate();
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="selectedNode"></param>
  /// <param name="includeRole"></param>
  private void Include(TreeNode selectedNode, bool includeRole)
  {
    Forms4TypeForm.FormInfo fi = selectedNode.Tag as Forms4TypeForm.FormInfo;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.CheckOut(sessionKeeper.Session.GetObjectActualCopy(fi.FormID, false), sessionKeeper.Session, Forms4ActionType.Include);
    long[] numArray = !includeRole ? Intermech.Navigator.SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Client.Core_194"), string.Empty, (IDescriptor) new UsersGroupsDescriptor(), SelectionOptions.Default) : Intermech.Navigator.SelectionWindow.SelectObjects("Выберите роль для добавления", string.Empty, MetaDataHelper.GetObjectTypeID("cad00007-306c-11d8-b4e9-00304f19f545"), SelectionOptions.Default);
    if (numArray == null)
      return;
    string empty = string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long objectID in numArray)
      {
        long absID = Math.Abs(objectID);
        if (!fi.UserIDs.Contains(absID))
        {
          IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objectID, false);
          TreeNode userNode = this.CreateUserNode(absID, objectActualCopy.ObjectType, objectActualCopy.Caption);
          selectedNode.Nodes.Add(userNode);
          fi.UserIDs.Add(absID);
          FormAction formAction = this._actions.FirstOrDefault<FormAction>((System.Func<FormAction, bool>) (x => x.FormID == fi.FormID && x.UserID == absID));
          if (formAction != null)
            this._actions.Remove(formAction);
          else
            this._actions.Add(new FormAction(Forms4ActionType.Include, (IFolder) this._folder, fi.FormID)
            {
              UserID = absID
            });
        }
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="selectedNode"></param>
  private void Exclude(TreeNode selectedNode)
  {
    long ID = Convert.ToInt64(selectedNode.Tag);
    Forms4TypeForm.FormInfo fi = selectedNode.Parent.Tag as Forms4TypeForm.FormInfo;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.CheckOut(sessionKeeper.Session.GetObjectActualCopy(fi.FormID, false), sessionKeeper.Session, Forms4ActionType.Exclude);
    selectedNode.Remove();
    fi.UserIDs.Remove(ID);
    FormAction formAction = this._actions.FirstOrDefault<FormAction>((System.Func<FormAction, bool>) (x => x.FormID == fi.FormID && x.UserID == ID));
    if (formAction != null)
      this._actions.Remove(formAction);
    else
      this._actions.Add(new FormAction(Forms4ActionType.Exclude, (IFolder) this._folder, fi.FormID)
      {
        UserID = ID
      });
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="selectedNode"></param>
  private void SetCondition(TreeNode selectedNode, Forms4TypeForm.ConditionOperation operation)
  {
    bool flag = false;
    Forms4TypeForm.FormInfo fi = selectedNode.Tag as Forms4TypeForm.FormInfo;
    if (operation != Forms4TypeForm.ConditionOperation.Copy)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.CheckOut(sessionKeeper.Session.GetObjectActualCopy(fi.FormID, false), sessionKeeper.Session, Forms4ActionType.SetCondition);
    }
    object condition = (object) fi.Condition;
    switch (operation - 1)
    {
      case Forms4TypeForm.ConditionOperation.None:
        if ((ServicesManager.GetService(typeof (IExpertEditor)) as IExpertEditor).EditCondition(ref condition, string.Format(LocalizationHolder.rm.GetString("Client.Core_195"), (object) selectedNode.Text)) && condition != null)
        {
          if (!string.IsNullOrEmpty(Convert.ToString(condition)))
          {
            fi.Condition = this.curFormula = condition as TempFormula;
            this.curFormula.UpdateTokenBegs();
            this.ShowFormula();
          }
          else
          {
            fi.Condition = this.curFormula = (TempFormula) null;
            this._txtCondition.Text = LocalizationHolder.rm.GetString("Client.Core_196");
          }
          flag = true;
          break;
        }
        break;
      case Forms4TypeForm.ConditionOperation.Edit:
        fi.Condition = this.curFormula = (TempFormula) null;
        this._txtCondition.Text = LocalizationHolder.rm.GetString("Client.Core_196");
        flag = true;
        break;
      case Forms4TypeForm.ConditionOperation.Delete:
        if (condition != null)
        {
          this.clipboardActivated = true;
          this.clipboardCondition = (condition as ICloneable).Clone() as TempFormula;
          break;
        }
        break;
      case Forms4TypeForm.ConditionOperation.Copy:
        if (this.clipboardActivated)
        {
          fi.Condition = this.curFormula = this.clipboardCondition.Clone() as TempFormula;
          this.curFormula.UpdateTokenBegs();
          this.ShowFormula();
          flag = true;
          break;
        }
        break;
    }
    if (!flag)
      return;
    FormAction formAction = this._actions.FirstOrDefault<FormAction>((System.Func<FormAction, bool>) (x => x.FormID == fi.FormID && x.ActionType == Forms4ActionType.SetCondition));
    if (formAction != null)
      this._actions.Remove(formAction);
    this._actions.Add(new FormAction(Forms4ActionType.SetCondition, (IFolder) this._folder, fi.FormID)
    {
      Condition = fi.Condition
    });
  }

  /// <summary>Взять форму на изменение.</summary>
  /// <param name="formObj">Форма</param>
  /// <param name="session">Сессия</param>
  /// <param name="actType">Действие</param>
  /// <returns>Форма</returns>
  private IDBObject CheckOut(IDBObject formObj, IUserSession session, Forms4ActionType actType)
  {
    IDBObject dbObject1 = formObj;
    if (formObj == null)
      throw new ArgumentException("Отсутствует форма редактирования");
    if (session == null)
      throw new ArgumentException("Не удалось получить сессию пользователя");
    switch (formObj.ObjectModifyMode)
    {
      case ObjectModifyModes.Checkout:
      case ObjectModifyModes.CreateVersion:
        if (formObj.CheckoutBy == 0L)
        {
          long objectId = formObj.ObjectID;
          dbObject1 = formObj.CheckOut();
          long num = Math.Abs(dbObject1.ObjectID);
          if (!this._needFromsCheckIn.Contains(num))
            this._needFromsCheckIn.Add(num);
          (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCheckedOut", objectId));
          break;
        }
        if (formObj.CheckoutBy != session.UserID)
        {
          string str = string.Empty;
          switch (actType)
          {
            case Forms4ActionType.AddForm:
              str = string.Format(LocalizationHolder.rm.GetString("Client.Core.Impossible.Add"), (object) formObj.Caption);
              break;
            case Forms4ActionType.DeleteForm:
              str = string.Format(LocalizationHolder.rm.GetString("Client.Core.Impossible.Exclude"), (object) formObj.Caption);
              break;
            case Forms4ActionType.EditForm:
              str = string.Format(LocalizationHolder.rm.GetString("Client.Core.Impossible.Edit"), (object) formObj.Caption);
              break;
            case Forms4ActionType.Include:
              str = LocalizationHolder.rm.GetString("Client.Core.Impossible.IncludeUser");
              break;
            case Forms4ActionType.Exclude:
              str = LocalizationHolder.rm.GetString("Client.Core.Impossible.ExcludeUser");
              break;
            case Forms4ActionType.SetCondition:
              str = LocalizationHolder.rm.GetString("Client.Core.Impossible.AddCondition");
              break;
          }
          IDBObject dbObject2 = session.GetObject(formObj.CheckoutBy, false);
          throw new ArgumentException($"{str}{LocalizationHolder.rm.GetString("Client.Core_197")}'{dbObject2.Caption}'");
        }
        break;
      case ObjectModifyModes.CantModify:
        throw new ArgumentException(string.Format(LocalizationHolder.rm.GetString("Client.Core_198"), (object) formObj.Caption));
    }
    return dbObject1;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="forms"></param>
  /// <returns></returns>
  private DataTable GetActualFormInfo(IEnumerable<FormInformation> forms)
  {
    DataTable dataTable = new DataTable();
    ColumnDescriptor[] columns = new ColumnDescriptor[3]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long userID = sessionKeeper.Session.UserID;
      foreach (IGrouping<int, long> source in (Lookup<int, long>) forms.ToLookup<FormInformation, int, long>((System.Func<FormInformation, int>) (x => x.TypeID), (System.Func<FormInformation, long>) (x => x.CheckOutBy == userID ? -x.ID : x.ID)))
      {
        ConditionStructure conditionStructure = new ConditionStructure(-2, RelationalOperators.In, (object) source.ToArray<long>(), LogicalOperators.NONE, 0, false);
        DataTable table = sessionKeeper.Session.ObjectsSelect(source.Key, new DBRecordSetParams(new ConditionStructure[1]
        {
          conditionStructure
        }, columns));
        dataTable.Merge(table);
      }
    }
    return dataTable.Rows.Count <= 0 ? (DataTable) null : dataTable;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="formInfoList"></param>
  /// <returns></returns>
  private DataTable GetUsers(List<ObjInfoItem> formInfoList)
  {
    DataTable dataTable = (DataTable) null;
    List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>()
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PROJ_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      List<ObjInfoItem> projObjList = formInfoList;
      IUserSession session = sessionKeeper.Session;
      List<int> relations = new List<int>();
      relations.Add(sessionKeeper.Session.IdentHelper.SimpleRelationTypeID);
      List<ColumnDescriptor> columns = columnDescriptorList;
      dataTable = DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) projObjList, session, (IEnumerable<int>) relations, false, (IEnumerable<ConditionStructure>) null, (IEnumerable<ColumnDescriptor>) columns);
    }
    return dataTable == null || dataTable.Rows.Count <= 0 ? (DataTable) null : dataTable;
  }

  /// <summary>
  /// Сохранить настройки индексов отображения форм редактирования для типа объектов/связи.
  /// </summary>
  private void SaveDisplayIndexesInfo(IFormDesignerService iFDSrv)
  {
    if (iFDSrv == null)
      return;
    if (this._trvForms.Nodes.Count > 0)
    {
      Dictionary<Guid, int> dict = new Dictionary<Guid, int>();
      IObjectsInfoCache service = ApplicationServices.Container.GetService<IObjectsInfoCache>();
      foreach (TreeNode node in this._trvForms.Nodes)
      {
        QuickObjectInfo objectInfo = service.GetObjectInfo((node.Tag as Forms4TypeForm.FormInfo).FormID);
        if (!objectInfo.Empty && !dict.ContainsKey(objectInfo.VersionGuid))
          dict.Add(objectInfo.VersionGuid, node.Index);
      }
      if (dict.Count > 0)
        iFDSrv.SetFormDisplayOrderForType(this._typeGuid, dict);
      else
        iFDSrv.ClearFormDisplayOrderForType(this._typeGuid);
    }
    else
      iFDSrv.ClearFormDisplayOrderForType(this._typeGuid);
  }

  /// <summary>
  /// 
  /// </summary>
  private void SetEnabledButtons()
  {
    TreeNode selectedNode = this._trvForms.SelectedNode;
    if (selectedNode != null && selectedNode.Level == 0 && this._trvForms.Nodes.Count > 1)
    {
      if (selectedNode.Index == 0)
      {
        this._btnTop.Enabled = this._btnUp.Enabled = false;
        this._btnDown.Enabled = this._btnBottom.Enabled = true;
      }
      else if (selectedNode.Index == this._trvForms.Nodes.Count - 1)
      {
        this._btnTop.Enabled = this._btnUp.Enabled = true;
        this._btnDown.Enabled = this._btnBottom.Enabled = false;
      }
      else
        this._btnTop.Enabled = this._btnUp.Enabled = this._btnDown.Enabled = this._btnBottom.Enabled = true;
    }
    else
      this._btnTop.Enabled = this._btnUp.Enabled = this._btnDown.Enabled = this._btnBottom.Enabled = false;
  }

  /// <summary>Идентификатор раздела справки.</summary>
  public override string HelpTopicID
  {
    get
    {
      if (this._folder == null)
        return base.HelpTopicID;
      return !(this._folder is ObjectTypeFolder) ? "1035" : "1027";
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OntxtCondition_MouseMove(object sender, MouseEventArgs e)
  {
    if (this.curFormula == null)
      return;
    int tokenByPos = this.curFormula.GetTokenByPos(this._txtCondition.GetCharIndexFromPosition(new Point(e.X, e.Y)));
    string caption = string.Empty;
    if (tokenByPos >= 0)
    {
      Token token = this.curFormula[tokenByPos];
      if (token.type == Intermech.Expert.TokenType.Integer && token.text != token.trueText)
        caption = token.trueText;
    }
    if (!(caption != this.toolTipFE.GetToolTip((Control) this._txtCondition)))
      return;
    this.toolTipFE.SetToolTip((Control) this._txtCondition, caption);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="t"></param>
  /// <param name="memoForm"></param>
  private void PaintCurToken(Token t, RichTextBox memoForm)
  {
    if (t.type != Intermech.Expert.TokenType.FuncCall)
      memoForm.Select(t.StartPos, t.text.Length);
    switch (t.type)
    {
      case Intermech.Expert.TokenType.UnaryOper:
      case Intermech.Expert.TokenType.BinaryOper:
        memoForm.SelectionColor = Color.DarkRed;
        break;
      case Intermech.Expert.TokenType.OpeningBrace:
      case Intermech.Expert.TokenType.ClosingBrace:
        memoForm.SelectionColor = Color.Blue;
        break;
      case Intermech.Expert.TokenType.FuncCall:
        memoForm.Select(t.StartPos, t.text.Length - 1);
        memoForm.SelectionColor = Color.Black;
        memoForm.Select(t.StartPos + t.text.Length - 1, 1);
        memoForm.SelectionColor = Color.Blue;
        break;
      case Intermech.Expert.TokenType.Integer:
        memoForm.SelectionColor = Color.Indigo;
        break;
      case Intermech.Expert.TokenType.Float:
        memoForm.SelectionColor = Color.DarkOliveGreen;
        break;
      case Intermech.Expert.TokenType.String:
        memoForm.SelectionColor = Color.DarkMagenta;
        break;
      case Intermech.Expert.TokenType.Date:
        memoForm.SelectionColor = Color.DarkOrchid;
        break;
      case Intermech.Expert.TokenType.ObjectLink:
        memoForm.SelectionColor = Color.Red;
        break;
      default:
        memoForm.SelectionColor = Color.Black;
        break;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public void ShowFormula()
  {
    if (this.curFormula == null)
      return;
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < this.curFormula.Count; ++index)
      stringBuilder.Append(this.curFormula[index].text);
    this._txtCondition.Text = stringBuilder.ToString();
    for (int index = 0; index < this.curFormula.Count; ++index)
      this.PaintCurToken(this.curFormula[index], this._txtCondition);
  }

  private void Forms4TypeForm_Load(object sender, EventArgs e)
  {
    (ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService).AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(this.svc_AfterObjectCreatedEvent);
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    service.Subscribe("ObjectsChanged", new NotificationEventHandler(this.ObjectsWasChangedHandler));
    service.Subscribe("ObjectsRemoved", new NotificationEventHandler(this.ObjectsWasChangedHandler));
  }

  /// <summary>Обработка события обновления объектов </summary>
  private void ObjectsWasChangedHandler(object sender, NotificationEventArgs e)
  {
    if (!(e is DBObjectsEventArgs objectsEventArgs) || objectsEventArgs.ObjectIDs == null || objectsEventArgs.ObjectIDs.Count == 0)
      return;
    Forms4TypeForm.ObjectOperations operationFlag = Forms4TypeForm.ObjectOperations.None;
    if (e.EventName == "ObjectsChanged")
      operationFlag = Forms4TypeForm.ObjectOperations.Changed;
    else if (e.EventName == "ObjectsRemoved")
      operationFlag = Forms4TypeForm.ObjectOperations.Removed;
    if (operationFlag == Forms4TypeForm.ObjectOperations.None)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < objectsEventArgs.ObjectIDs.Count; ++index)
      {
        int lObjectTypeID = -1;
        if (operationFlag == Forms4TypeForm.ObjectOperations.Changed)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectsEventArgs.ObjectIDs[index], false);
          if (dbObject != null)
            lObjectTypeID = dbObject.ObjectType;
          else
            continue;
        }
        if (this.ProcessPossibleFormModification(objectsEventArgs.ObjectIDs[index], lObjectTypeID, operationFlag))
          break;
      }
    }
  }

  private void svc_AfterObjectCreatedEvent(object sender, AfterObjectCreatedEventArgs e)
  {
    this.ProcessPossibleFormModification(e.ObjectID, e.ObjectTypeID, Forms4TypeForm.ObjectOperations.Created);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="lObjectID"></param>
  /// <param name="lObjectTypeID"></param>
  /// <param name="createFormFlag">когда форма была создана, а не изменена или удалена</param>
  /// <returns></returns>
  private bool ProcessPossibleFormModification(
    long lObjectID,
    int lObjectTypeID,
    Forms4TypeForm.ObjectOperations operationFlag)
  {
    bool flag1 = false;
    if (this._folder == null)
      return flag1;
    bool flag2 = this._folder is ObjectTypeFolder;
    if (this._folder.InChange)
    {
      bool flag3 = this._actions.Count > 0;
      bool flag4;
      if (this._folder is ObjectTypeFolder)
      {
        flag4 = flag3 || StatesController.GetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).Forms4ObjectTypePage);
      }
      else
      {
        if (!(this._folder is RelationTypeFolder))
          return flag1;
        flag4 = flag3 || StatesController.GetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).Forms4RelationTypePage);
      }
      if (flag4)
        return flag1;
    }
    if (operationFlag == Forms4TypeForm.ObjectOperations.Removed)
    {
      bool flag5 = false;
      for (int index = 0; index < this._trvForms.Nodes.Count; ++index)
      {
        if (this._trvForms.Nodes[index].Tag != null && this._trvForms.Nodes[index].Tag is Forms4TypeForm.FormInfo && ((Forms4TypeForm.FormInfo) this._trvForms.Nodes[index].Tag).FormID.Equals(Math.Abs(lObjectID)))
        {
          flag5 = true;
          break;
        }
      }
      if (!flag5)
        return flag1;
    }
    else
    {
      bool flag6 = lObjectTypeID == this.sysFormsObjectTypeId;
      if (!flag6)
      {
        List<int> objectTypeParentsId = MetaDataHelper.GetObjectTypeParentsID(lObjectTypeID);
        for (int index = 0; index < objectTypeParentsId.Count; ++index)
        {
          if (objectTypeParentsId[index] == this.sysFormsObjectTypeId)
          {
            flag6 = true;
            break;
          }
        }
        if (!flag6)
          return flag1;
      }
      if (operationFlag == Forms4TypeForm.ObjectOperations.Created)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(lObjectID, false);
          if (dbObject == null)
            return flag1;
          IDBAttribute dbAttribute = flag2 ? dbObject.GetAttributeByID(this.sysGlobals4objtypeAttrId) : dbObject.GetAttributeByID(this.sysGlobals4reltypeAttrId);
          if (dbAttribute == null || dbAttribute.ValuesCount == 0 || dbAttribute.ValuesCount == 1 && dbAttribute.Values[0] is DBNull)
            return flag1;
          List<object> list = ((IEnumerable<object>) dbAttribute.Values).ToList<object>();
          int id = (int) this._folder.Id;
          List<Guid> guidList;
          if (flag2)
          {
            Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(id);
            guidList = MetaDataHelper.GetObjectTypeParentsGuid(objectTypeGuid);
            guidList.Insert(0, objectTypeGuid);
          }
          else
          {
            guidList = new List<Guid>();
            guidList.Add(MetaDataHelper.GetRelationTypeGuid(id));
          }
          bool flag7 = false;
          for (int index1 = 0; index1 < guidList.Count; ++index1)
          {
            for (int index2 = 0; index2 < list.Count; ++index2)
            {
              if (guidList[index1].Equals(new Guid(list[index2].ToString())))
              {
                flag7 = true;
                break;
              }
            }
            if (flag7)
              break;
          }
          if (!flag7)
            return flag1;
        }
      }
    }
    this._compareFolders = false;
    this.FillForm((IFolder) this._folder);
    return true;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
    {
      (ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService).AfterObjectCreatedEvent -= new AfterObjectCreatedEventHandler(this.svc_AfterObjectCreatedEvent);
      if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
      {
        service.Unsubscribe("ObjectsChanged", new NotificationEventHandler(this.ObjectsWasChangedHandler));
        service.Unsubscribe("ObjectsRemoved", new NotificationEventHandler(this.ObjectsWasChangedHandler));
      }
      this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Forms4TypeForm));
    this._contextMenu = new ContextMenuStrip(this.components);
    this._miAddForm = new ToolStripMenuItem();
    this._miOpenInNewWindow = new ToolStripMenuItem();
    this._miEdit = new ToolStripMenuItem();
    this._miExcludeForm = new ToolStripMenuItem();
    this.tsS1 = new ToolStripSeparator();
    this._miCheckInForm = new ToolStripMenuItem();
    this._miCancelChangesForm = new ToolStripMenuItem();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this._miIncludeUser = new ToolStripMenuItem();
    this._miIncludeRole = new ToolStripMenuItem();
    this._miExclude = new ToolStripMenuItem();
    this.tsS2 = new ToolStripSeparator();
    this._miCondition = new ToolStripMenuItem();
    this._contextMenuCondition = new ContextMenuStrip(this.components);
    this._miCondEdit = new ToolStripMenuItem();
    this._miCondCopy = new ToolStripMenuItem();
    this._miCondPaste = new ToolStripMenuItem();
    this._miCondDelete = new ToolStripMenuItem();
    this.toolTipFE = new ToolTip(this.components);
    this._btnTop = new Button();
    this._imgList = new ImageList(this.components);
    this._btnUp = new Button();
    this._btnBottom = new Button();
    this._btnDown = new Button();
    this._trvForms = new TreeView();
    this.splitter1 = new Splitter();
    this._txtCondition = new RichTextBox();
    this._tlpSelecetdFields = new TableLayoutPanel();
    this._contextMenu.SuspendLayout();
    this._contextMenuCondition.SuspendLayout();
    this._tlpSelecetdFields.SuspendLayout();
    this.SuspendLayout();
    this._contextMenu.Items.AddRange(new ToolStripItem[13]
    {
      (ToolStripItem) this._miAddForm,
      (ToolStripItem) this._miOpenInNewWindow,
      (ToolStripItem) this._miEdit,
      (ToolStripItem) this._miExcludeForm,
      (ToolStripItem) this.tsS1,
      (ToolStripItem) this._miCheckInForm,
      (ToolStripItem) this._miCancelChangesForm,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this._miIncludeUser,
      (ToolStripItem) this._miIncludeRole,
      (ToolStripItem) this._miExclude,
      (ToolStripItem) this.tsS2,
      (ToolStripItem) this._miCondition
    });
    this._contextMenu.Name = "contextMenuStrip1";
    componentResourceManager.ApplyResources((object) this._contextMenu, "_contextMenu");
    this._contextMenu.Opened += new EventHandler(this.On_contextMenu_Opened);
    this._miAddForm.Name = "_miAddForm";
    componentResourceManager.ApplyResources((object) this._miAddForm, "_miAddForm");
    this._miAddForm.Click += new EventHandler(this.On_miItem_Click);
    this._miOpenInNewWindow.Name = "_miOpenInNewWindow";
    componentResourceManager.ApplyResources((object) this._miOpenInNewWindow, "_miOpenInNewWindow");
    this._miOpenInNewWindow.Click += new EventHandler(this.On_miItem_Click);
    this._miEdit.Name = "_miEdit";
    componentResourceManager.ApplyResources((object) this._miEdit, "_miEdit");
    this._miEdit.Click += new EventHandler(this.On_miItem_Click);
    this._miExcludeForm.Name = "_miExcludeForm";
    componentResourceManager.ApplyResources((object) this._miExcludeForm, "_miExcludeForm");
    this._miExcludeForm.Click += new EventHandler(this.On_miItem_Click);
    this.tsS1.Name = "tsS1";
    componentResourceManager.ApplyResources((object) this.tsS1, "tsS1");
    this._miCheckInForm.Name = "_miCheckInForm";
    componentResourceManager.ApplyResources((object) this._miCheckInForm, "_miCheckInForm");
    this._miCheckInForm.Click += new EventHandler(this.On_miItem_Click);
    this._miCancelChangesForm.Name = "_miCancelChangesForm";
    componentResourceManager.ApplyResources((object) this._miCancelChangesForm, "_miCancelChangesForm");
    this._miCancelChangesForm.Click += new EventHandler(this.On_miItem_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator1, "toolStripSeparator1");
    this._miIncludeUser.Name = "_miIncludeUser";
    componentResourceManager.ApplyResources((object) this._miIncludeUser, "_miIncludeUser");
    this._miIncludeUser.Click += new EventHandler(this.On_miItem_Click);
    this._miIncludeRole.Name = "_miIncludeRole";
    componentResourceManager.ApplyResources((object) this._miIncludeRole, "_miIncludeRole");
    this._miIncludeRole.Click += new EventHandler(this.On_miItem_Click);
    this._miExclude.Name = "_miExclude";
    componentResourceManager.ApplyResources((object) this._miExclude, "_miExclude");
    this._miExclude.Click += new EventHandler(this.On_miItem_Click);
    this.tsS2.Name = "tsS2";
    componentResourceManager.ApplyResources((object) this.tsS2, "tsS2");
    this._miCondition.Name = "_miCondition";
    componentResourceManager.ApplyResources((object) this._miCondition, "_miCondition");
    this._miCondition.Click += new EventHandler(this.On_miItem_Click);
    this._contextMenuCondition.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this._miCondEdit,
      (ToolStripItem) this._miCondCopy,
      (ToolStripItem) this._miCondPaste,
      (ToolStripItem) this._miCondDelete
    });
    this._contextMenuCondition.Name = "_contextMenuCondition";
    componentResourceManager.ApplyResources((object) this._contextMenuCondition, "_contextMenuCondition");
    this._contextMenuCondition.Opened += new EventHandler(this.On_contextMenu_Opened);
    this._miCondEdit.Name = "_miCondEdit";
    componentResourceManager.ApplyResources((object) this._miCondEdit, "_miCondEdit");
    this._miCondEdit.Click += new EventHandler(this.On_miCondItem_Click);
    this._miCondCopy.Name = "_miCondCopy";
    componentResourceManager.ApplyResources((object) this._miCondCopy, "_miCondCopy");
    this._miCondCopy.Click += new EventHandler(this.On_miCondItem_Click);
    this._miCondPaste.Name = "_miCondPaste";
    componentResourceManager.ApplyResources((object) this._miCondPaste, "_miCondPaste");
    this._miCondPaste.Click += new EventHandler(this.On_miCondItem_Click);
    this._miCondDelete.Name = "_miCondDelete";
    componentResourceManager.ApplyResources((object) this._miCondDelete, "_miCondDelete");
    this._miCondDelete.Click += new EventHandler(this.On_miCondItem_Click);
    componentResourceManager.ApplyResources((object) this._btnTop, "_btnTop");
    this._btnTop.ImageList = this._imgList;
    this._btnTop.Name = "_btnTop";
    this._btnTop.Tag = (object) "0";
    this.toolTipFE.SetToolTip((Control) this._btnTop, componentResourceManager.GetString("_btnTop.ToolTip"));
    this._btnTop.UseVisualStyleBackColor = true;
    this._btnTop.Click += new EventHandler(this.On_btnUpDown_Click);
    this._imgList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_imgList.ImageStream");
    this._imgList.TransparentColor = Color.Transparent;
    this._imgList.Images.SetKeyName(0, "Top.ico");
    this._imgList.Images.SetKeyName(1, "Up.ico");
    this._imgList.Images.SetKeyName(2, "Down.ico");
    this._imgList.Images.SetKeyName(3, "Bottom.ico");
    componentResourceManager.ApplyResources((object) this._btnUp, "_btnUp");
    this._btnUp.ImageList = this._imgList;
    this._btnUp.Name = "_btnUp";
    this._btnUp.Tag = (object) "1";
    this.toolTipFE.SetToolTip((Control) this._btnUp, componentResourceManager.GetString("_btnUp.ToolTip"));
    this._btnUp.UseVisualStyleBackColor = true;
    this._btnUp.Click += new EventHandler(this.On_btnUpDown_Click);
    componentResourceManager.ApplyResources((object) this._btnBottom, "_btnBottom");
    this._btnBottom.ImageList = this._imgList;
    this._btnBottom.Name = "_btnBottom";
    this._btnBottom.Tag = (object) "3";
    this.toolTipFE.SetToolTip((Control) this._btnBottom, componentResourceManager.GetString("_btnBottom.ToolTip"));
    this._btnBottom.UseVisualStyleBackColor = true;
    this._btnBottom.Click += new EventHandler(this.On_btnUpDown_Click);
    componentResourceManager.ApplyResources((object) this._btnDown, "_btnDown");
    this._btnDown.ImageList = this._imgList;
    this._btnDown.Name = "_btnDown";
    this._btnDown.Tag = (object) "2";
    this.toolTipFE.SetToolTip((Control) this._btnDown, componentResourceManager.GetString("_btnDown.ToolTip"));
    this._btnDown.UseVisualStyleBackColor = true;
    this._btnDown.Click += new EventHandler(this.On_btnUpDown_Click);
    this._trvForms.ContextMenuStrip = this._contextMenu;
    componentResourceManager.ApplyResources((object) this._trvForms, "_trvForms");
    this._trvForms.HideSelection = false;
    this._trvForms.Name = "_trvForms";
    this._trvForms.AfterSelect += new TreeViewEventHandler(this.On_trvForms_AfterSelect);
    this._trvForms.MouseDown += new MouseEventHandler(this.On_trvForms_MouseDown);
    componentResourceManager.ApplyResources((object) this.splitter1, "splitter1");
    this.splitter1.Name = "splitter1";
    this.splitter1.TabStop = false;
    this._txtCondition.BackColor = SystemColors.Window;
    this._txtCondition.ContextMenuStrip = this._contextMenuCondition;
    componentResourceManager.ApplyResources((object) this._txtCondition, "_txtCondition");
    this._txtCondition.Name = "_txtCondition";
    this._txtCondition.ReadOnly = true;
    this._txtCondition.DoubleClick += new EventHandler(this.On_txtCondition_DoubleClick);
    this._txtCondition.MouseMove += new MouseEventHandler(this.OntxtCondition_MouseMove);
    componentResourceManager.ApplyResources((object) this._tlpSelecetdFields, "_tlpSelecetdFields");
    this._tlpSelecetdFields.Controls.Add((Control) this._btnTop, 0, 1);
    this._tlpSelecetdFields.Controls.Add((Control) this._btnUp, 0, 2);
    this._tlpSelecetdFields.Controls.Add((Control) this._btnBottom, 0, 4);
    this._tlpSelecetdFields.Controls.Add((Control) this._btnDown, 0, 3);
    this._tlpSelecetdFields.Name = "_tlpSelecetdFields";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._trvForms);
    this.Controls.Add((Control) this.splitter1);
    this.Controls.Add((Control) this._txtCondition);
    this.Controls.Add((Control) this._tlpSelecetdFields);
    this.Name = nameof (Forms4TypeForm);
    this.Tag = (object) "";
    this.Load += new EventHandler(this.Forms4TypeForm_Load);
    this._contextMenu.ResumeLayout(false);
    this._contextMenuCondition.ResumeLayout(false);
    this._tlpSelecetdFields.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>Операции с условием</summary>
  private enum ConditionOperation
  {
    None,
    /// <summary>Изменение условия</summary>
    Edit,
    /// <summary>Удаление условия</summary>
    Delete,
    /// <summary>Копирование в буфер условия</summary>
    Copy,
    /// <summary>Вставка из буфера условия</summary>
    Paste,
  }

  /// <summary>
  /// 
  /// </summary>
  private class FormInfo
  {
    /// <summary>Идентификатор формы.</summary>
    internal long FormID { get; private set; }

    /// <summary>Условие.</summary>
    internal TempFormula Condition { get; set; }

    /// <summary>Признак того, что условие для формы уже проверялось.</summary>
    internal bool CheckedCondition { get; private set; }

    /// <summary>
    /// Список идентификаторов пользователей и ролей, для которых назначено отображение формы.
    /// </summary>
    internal List<long> UserIDs { get; set; }

    /// <summary>Конструктор.</summary>
    /// <param name="formID">Идентификатор формы</param>
    internal FormInfo(long formID)
    {
      this.FormID = formID;
      this.Condition = (TempFormula) null;
      this.UserIDs = new List<long>(0);
    }

    /// <summary>
    /// Установка признака, что наличие условия для формы уже проверялось.
    /// </summary>
    internal void SetCheckedCondition() => this.CheckedCondition = true;
  }

  /// <summary>Класс для сортировки нодов.</summary>
  public class TreeNodeComparer : IComparer<TreeNode>, IComparer
  {
    /// <summary>Сравнивает два нода.</summary>
    /// <param name="x">Первый нод</param>
    /// <param name="y">Второй нод</param>
    /// <returns>Результат сравнения</returns>
    public int Compare(TreeNode x, TreeNode y) => string.Compare(x.Text, y.Text);

    /// <summary>Сравнивает два нода.</summary>
    /// <param name="x">Первый нод</param>
    /// <param name="y">Второй нод</param>
    /// <returns>Результат сравнения</returns>
    public int Compare(object x, object y)
    {
      TreeNode x1 = x as TreeNode;
      TreeNode y1 = y as TreeNode;
      return x1 == null || y1 == null ? 0 : this.Compare(x1, y1);
    }
  }

  private enum ObjectOperations
  {
    None,
    Created,
    Changed,
    Removed,
  }
}
