
// Type: Intermech.Navigator.SelectionView.SelectionForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using DevExpress.IM.XtraEditors.Repository;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.SelectionService;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Conditions;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.SelectionView;

public class SelectionForm : Form, ISelectionForm
{
  private bool _autoCheck;
  public int[] ObjectTypeForInnerSelection;
  private SelectionConditionWrapper _selectionConditionWrapper;
  /// <summary>
  /// Условия только по атрибутам указанного в ObjectTypeForInnerSelection типа объектов
  /// </summary>
  public bool ObjectAttributesOnlyConditions;
  private IConditionDataProvider _dataProvider;
  /// <summary>Принадлежность выборки</summary>
  private SelectionType _selectionType;
  private SelectionFormMode _parentMode;
  /// <summary>Идентификатор текущего объекта выборки</summary>
  private long _objectID;
  /// <summary>
  /// Храним копию первоначально загруженных условий для проверки
  /// </summary>
  private List<ConditionStructure> _loadedStructures;
  /// <summary>Признак выборки для удаленных запросов</summary>
  private SelectionDataSource _dataSourceType = SelectionDataSource.DataBase;
  private bool _readOnly;
  /// <summary>
  /// GUID атрибута в котором хранится признак - является ли выборка ручной
  /// </summary>
  private static Guid HandSelectionGuid = new Guid("cad00155-306c-11d8-b4e9-00304f19f545");
  /// <summary>GUID типа "Выборки"</summary>
  private static Guid TypeSelectionGuid = new Guid("cad00156-306c-11d8-b4e9-00304f19f545");
  /// <summary>Тип объекта Общая выборка</summary>
  private static Guid TypeSelectionCommonGuid = new Guid("cad00122-306c-11d8-b4e9-00304f19f545");
  /// <summary>Тип объекта Персональная выборка</summary>
  private static Guid TypeSelectionPersonGuid = new Guid("cad00123-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Массив типов объектов для которых предназначена выборка
  /// </summary>
  private List<int> objTypesID = new List<int>();
  /// <summary>Цепочка иерархии выборок вверх</summary>
  private List<long> _objIDList = new List<long>();
  /// <summary>поле для хранения признака модификации выборки</summary>
  private bool _isModified;
  /// <summary>Признак необходимости перечитки данных</summary>
  internal bool IsNeedRefresh;
  private List<ToolStripMenuItem> _additionalMItems;
  private List<ButtonItem> _additionalButtons;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelBottom;
  private GroupBox groupBox3;
  private Button buttonApply;
  private Button buttonCancel;
  private ContextMenuStrip contextMenuStripTree;
  private ToolStripMenuItem miAddFilter;
  private ToolStripMenuItem miAdd;
  private ToolStripMenuItem miAddChild;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripMenuItem miEdit;
  private ToolStripMenuItem miEnable;
  private ToolStripSeparator toolStripSeparator2;
  private ToolStripMenuItem miDel;
  private ToolStripMenuItem miClear;
  private ImageList imageList1;
  private ToolStripMenuItem miChangeType;
  protected Intermech.Bars.ToolBar toolStrip2;
  private ButtonItem bAddFilter;
  private ButtonItem bAdd;
  private ButtonItem bAddChild;
  private ButtonItem bChangeType;
  private ButtonItem bEdit;
  private ButtonItem bEnable;
  private ButtonItem bDel;
  private ButtonItem bClear;
  private ButtonItem bSearch;
  private Panel panel1;
  private Panel panel2;
  private TreeList treeList1;
  private TreeListColumn сolumnConditions;
  private TreeListColumn columnValues;
  private RepositoryItemDateEdit riDateTime;
  private RepositoryItemButtonEdit riButton;
  public RepositoryItemButtonEdit riEditor;
  private RepositoryItemComboBox riBool;
  private RepositoryItemCalcEdit riNumber;
  private RepositoryItemTextEdit riString;
  private RepositoryItemButtonEdit riReadOnly;
  private RepositoryItemCalcEdit riFloat;
  private RepositoryItemComboBox riPossibleValues;
  protected ButtonItem buttonHeightSet;
  private ToolStripMenuItem miInnerConditions;
  private ToolStripSeparator toolStripSeparator4;
  private ImageList imageList2;
  private ToolTipController toolTipController1;

  public void SetGoEnable(bool enable) => this.bSearch.Visible = enable;

  /// <summary>
  /// Где размещена наша форма
  /// 0 - самостоятельная форма (по дефолту)
  /// 1 - на форме-создателе новых объектов
  /// 2 - на вьюшке "Навигатора"
  /// 3 - самостоятельная форма для вложенных условий
  /// </summary>
  public SelectionFormMode ParentMode
  {
    get => this._parentMode;
    set
    {
      this._parentMode = value;
      if (value == SelectionFormMode.InObjectCreator)
      {
        this.bSearch.Visible = this.bEnable.Visible = this.miEnable.Visible = this.buttonApply.Visible = this.buttonCancel.Visible = false;
      }
      else
      {
        if (value != SelectionFormMode.InnerConditionsForm)
          return;
        this.bSearch.Visible = this.bEnable.Visible = this.miEnable.Visible = false;
        this.buttonApply.Text = "OK";
        this.buttonApply.DialogResult = DialogResult.OK;
        this.buttonCancel.Text = "Закрыть";
        this.buttonCancel.DialogResult = DialogResult.Cancel;
        this.Text = "Вложенные условия";
        this.buttonCancel.Enabled = true;
        this.AcceptButton = (IButtonControl) this.buttonApply;
        this.CancelButton = (IButtonControl) this.buttonCancel;
      }
    }
  }

  /// <summary>Признак того, что форма работает в режиме ReadOnly</summary>
  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      this.miInnerConditions.Enabled = this.bEnable.Enabled = this.bAddFilter.Enabled = this.bAdd.Enabled = this.bAddChild.Enabled = this.bChangeType.Enabled = this.bEdit.Enabled = this.bDel.Enabled = this.bClear.Enabled = this.buttonApply.Enabled = this.buttonCancel.Enabled = !value;
      this._readOnly = value;
    }
  }

  /// <summary>
  /// Признак - нужно ли показывать параметры настройки условия выборки
  /// </summary>
  private bool _showConditionSettings { get; set; }

  /// <summary>Признак - была ли модифицирована выборка</summary>
  internal bool IsModified
  {
    get => this._isModified;
    set
    {
      if (this._isModified != value)
        this._isModified = value;
      this.buttonApply.Enabled = this._isModified;
      if (this._parentMode != SelectionFormMode.InnerConditionsForm)
        this.buttonCancel.Enabled = this._isModified;
      if (!this._isModified)
        return;
      this.UpdateConditionControls(false);
    }
  }

  public SelectionForm()
  {
    this.InitializeComponent();
    this.LoadBitmaps(this.imageList1);
    this._selectionConditionWrapper = new SelectionConditionWrapper(this.treeList1);
    this.riBool.Items.Clear();
    this.riBool.Items.AddRange(new object[2]
    {
      (object) Intermech.Consts.TrueValue,
      (object) Intermech.Consts.FalseValue
    });
    this._additionalMItems = new List<ToolStripMenuItem>();
    this._additionalButtons = new List<ButtonItem>();
    EventHandler eventHandler = new EventHandler(this.Item_Click);
    foreach (ISelectionFormCustomCommandsSubscriber subscriber in (ServicesManager.GetService(typeof (ISelectionFormCustomCommandsService)) as ISelectionFormCustomCommandsService).Subscribers)
    {
      List<SelectionFormCommand> buttons = subscriber.Buttons;
      if (buttons != null)
      {
        foreach (SelectionFormCommand selectionFormCommand in buttons)
        {
          ButtonItem buttonItem = new ButtonItem();
          buttonItem.Text = buttonItem.ToolTipText = selectionFormCommand.Caption;
          if (selectionFormCommand.Image != null)
          {
            this.imageList1.Images.Add(selectionFormCommand.Image);
            buttonItem.ImageIndex = this.imageList1.Images.Count - 1;
          }
          else
            buttonItem.ImageIndex = -1;
          buttonItem.Click += eventHandler;
          buttonItem.Tag = (object) selectionFormCommand.OnClickHandler;
          if (selectionFormCommand.Index >= 0)
            this.toolStrip2.Items.Insert(selectionFormCommand.Index, (ToolbarItemBase) buttonItem);
          else
            this.toolStrip2.Items.Add((ToolbarItemBase) buttonItem);
          this._additionalButtons.Add(buttonItem);
          ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem();
          toolStripMenuItem1.Name = selectionFormCommand.Name;
          ToolStripMenuItem toolStripMenuItem2 = toolStripMenuItem1;
          toolStripMenuItem2.Text = toolStripMenuItem2.ToolTipText = selectionFormCommand.Caption;
          toolStripMenuItem2.ImageIndex = selectionFormCommand.Image != null ? this.imageList1.Images.Count - 1 : -1;
          toolStripMenuItem2.Tag = (object) selectionFormCommand.OnClickHandler;
          toolStripMenuItem2.Click += eventHandler;
          if (selectionFormCommand.Index >= 0)
            this.contextMenuStripTree.Items.Insert(selectionFormCommand.Index, (ToolStripItem) toolStripMenuItem2);
          else
            this.contextMenuStripTree.Items.Add((ToolStripItem) toolStripMenuItem2);
          this._additionalMItems.Add(toolStripMenuItem2);
        }
      }
    }
  }

  private void Item_Click(object sender, EventArgs e)
  {
    SelectionFormCommandExecHandler commandExecHandler = (SelectionFormCommandExecHandler) null;
    if (sender is ToolStripMenuItem)
      commandExecHandler = ((ToolStripItem) sender).Tag as SelectionFormCommandExecHandler;
    else if (sender is ButtonItem)
      commandExecHandler = ((ToolbarItemBase) sender).Tag as SelectionFormCommandExecHandler;
    if (commandExecHandler == null)
      return;
    ConditionStructureNode conditionStructureNode = (ConditionStructureNode) null;
    if (this.treeList1.FocusedNode != null && this.treeList1.FocusedNode.Tag is ConditionStructureNode)
      conditionStructureNode = this.treeList1.FocusedNode.Tag as ConditionStructureNode;
    commandExecHandler(sender, new SelectionFormCommandExecEventArgs((ISelectionForm) this, conditionStructureNode != null ? conditionStructureNode.ConditionStruct : ConditionStructure.Empty));
  }

  /// <summary>загрузка изображений</summary>
  private void LoadBitmaps(ImageList imageList)
  {
    this.contextMenuStripTree.ImageList = this.imageList1;
    this.miAddFilter.ImageIndex = this.bAddFilter.ImageIndex;
    this.miAdd.ImageIndex = this.bAdd.ImageIndex;
    this.miAddChild.ImageIndex = this.bAddChild.ImageIndex;
    this.miEdit.ImageIndex = this.bEdit.ImageIndex;
    this.miChangeType.ImageIndex = this.bChangeType.ImageIndex;
    this.miEnable.ImageIndex = this.bEnable.ImageIndex;
    this.miDel.ImageIndex = this.bDel.ImageIndex;
    this.miClear.ImageIndex = this.bClear.ImageIndex;
    this.miAddFilter.ToolTipText = this.bAddFilter.ToolTipText = this.bAddFilter.Text = this.miAddFilter.Text;
    this.bAdd.ToolTipText = this.miAdd.ToolTipText = this.bAdd.Text = this.miAdd.Text;
    this.bAddChild.ToolTipText = this.miAddChild.ToolTipText = this.bAddChild.Text = this.miAddChild.Text;
    this.bEdit.ToolTipText = this.miEdit.ToolTipText = this.bEdit.Text = this.miEdit.Text;
    this.bChangeType.ToolTipText = this.miChangeType.ToolTipText = this.bChangeType.Text = this.miChangeType.Text;
    this.bEnable.ToolTipText = this.miEnable.ToolTipText = this.bEnable.Text = this.miEnable.Text;
    this.bDel.ToolTipText = this.miDel.ToolTipText = this.bDel.Text = this.miDel.Text;
    this.bClear.ToolTipText = this.miClear.ToolTipText = this.bClear.Text = this.miClear.Text;
  }

  /// <summary>Чтение типов объектов к которым привязана выборка</summary>
  /// <param name="session"></param>
  /// <param name="objIDList">Список идентификаторов выборок (цепочка иерархии вверх вместе с текущей выборкой)</param>
  public void ReloadObjTypes(IUserSession session, List<long> objIDList)
  {
    this.objTypesID.Clear();
    this._objIDList = objIDList;
    if (objIDList != null && objIDList.Count != 0)
    {
      for (int index = 0; index < objIDList.Count; ++index)
      {
        IDBObject dbObject = session.GetObject(objIDList[index], false);
        if (dbObject != null)
        {
          IDBAttribute dbAttribute = (IDBAttribute) null;
          if (this._dataSourceType == SelectionDataSource.Portal)
          {
            dbAttribute = dbObject.Attributes.FindByGUID(PortalConsts.attributePortalObjectTypes);
          }
          else
          {
            IDBAttribute byGuid = dbObject.Attributes.FindByGUID(new Guid("cad00158-306c-11d8-b4e9-00304f19f545"));
            if (byGuid != null && byGuid.AsInteger == 3L)
              dbAttribute = dbObject.Attributes.FindByGUID(new Guid("cad00149-306c-11d8-b4e9-00304f19f545"));
          }
          if (dbAttribute != null && dbAttribute.Values != null)
          {
            foreach (object obj in dbAttribute.Values)
            {
              string str = Convert.ToString(obj);
              if (GuidHelper.IsGuid(str))
              {
                int objectTypeId = this._dataProvider.GetObjectTypeID(new Guid(str));
                if (objectTypeId != -1 && !this.objTypesID.Contains(objectTypeId))
                  this.objTypesID.Add(objectTypeId);
              }
            }
          }
        }
      }
    }
    this.UpdateFilterControls();
  }

  public ConditionStructure[] Conditions
  {
    get
    {
      if (this.treeList1.Nodes.Count <= 0)
        return (ConditionStructure[]) null;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return this._selectionConditionWrapper.UnpackConditionTreeList(sessionKeeper.Session, this.treeList1.Nodes).ToArray();
    }
  }

  public void SelectionLoad(long aObjectID, List<long> objIDList)
  {
    this.SelectionLoad(aObjectID, objIDList, (ConditionStructure[]) null);
  }

  /// <summary>загрузка значений атрибутов настройки выборки</summary>
  /// <param name="aObjectID"></param>
  /// <param name="objIDList"></param>
  public void SelectionLoad(long aObjectID, List<long> objIDList, ConditionStructure[] css)
  {
    if (aObjectID != 0L)
    {
      this._objectID = aObjectID;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(aObjectID);
        this._dataSourceType = dbObject.ObjectType == MetaDataHelper.GetObjectTypeID(PortalConsts.objtypePortalSelections) ? SelectionDataSource.Portal : SelectionDataSource.DataBase;
        this.ReloadSelectionType(sessionKeeper.Session, dbObject);
        this._dataProvider = ServicesManager.GetService<IConditionDataProviderService>().GetDataProvider(this._dataSourceType);
        ISelectionsService service = ServicesManager.GetService<ISelectionsService>();
        if (this._parentMode != SelectionFormMode.InnerConditionsForm)
        {
          this._selectionConditionWrapper.FromBase(sessionKeeper.Session, aObjectID);
          int conditionNumber = 0;
          List<object[]> temporaryValues = service.GetTemporaryValues(this._objectID);
          if (temporaryValues != null && temporaryValues.Count > 0)
            this.RefreshTemporaryValues(this.treeList1.Nodes, temporaryValues, ref conditionNumber);
        }
        else if (css != null)
          this._selectionConditionWrapper.FromConditionsArray(sessionKeeper.Session, css, false);
        if (this.treeList1.Nodes.Count > 0)
        {
          this._loadedStructures = this._selectionConditionWrapper.UnpackConditionTreeList(sessionKeeper.Session, this.treeList1.Nodes);
          this.treeList1.FocusedNode = this.treeList1.Nodes[0];
          if (this._parentMode != SelectionFormMode.InObjectCreator && this._parentMode != SelectionFormMode.InnerConditionsForm)
            this.RefreshNodesEnable(this.treeList1.Nodes, service);
          this.ExpandTreeListNodes(this.treeList1.Nodes);
          this.UpdateImageIndexes(this.treeList1.Nodes);
          this.UpdateGroups();
          this.ReloadPossibleValues4Node(this.treeList1.FocusedNode);
        }
        this.ReloadObjTypes(sessionKeeper.Session, objIDList);
      }
    }
    this.IsModified = false;
    this.UpdateConditionControls(false, false);
  }

  private void RefreshTemporaryValues(
    TreeListNodes nodes,
    List<object[]> tempValues,
    ref int conditionNumber)
  {
    for (int index = 0; index < nodes.Count; ++index)
    {
      TreeListNode node = nodes[index];
      ConditionStructureNode tag = (ConditionStructureNode) node.Tag;
      object[] tempValue = tempValues[conditionNumber];
      if (tag.ConditionStruct.Value != tempValue[0])
        tag.ConditionStruct.Value = tempValue[0];
      if (tag.ConditionStruct.Value2 != tempValue[1])
        tag.ConditionStruct.Value2 = tempValue[1];
      ++conditionNumber;
      if (tag.ConditionStruct.NestedConditions != null && tag.ConditionStruct.NestedConditions.Length != 0)
        this.SetTempValuesToNested(ref tag.ConditionStruct.NestedConditions, tempValues, ref conditionNumber);
      if (node.Nodes.Count > 0)
        this.RefreshTemporaryValues(node.Nodes, tempValues, ref conditionNumber);
    }
  }

  private void SetTempValuesToNested(
    ref ConditionStructure[] cs,
    List<object[]> tempValues,
    ref int conditionNumber)
  {
    for (int index = 0; index < cs.Length; ++index)
    {
      object[] tempValue = tempValues[conditionNumber];
      if (cs[index].Value != tempValue[0])
        cs[index].Value = tempValue[0];
      if (cs[index].Value2 != tempValue[1])
        cs[index].Value2 = tempValue[1];
      ++conditionNumber;
      if (cs[index].NestedConditions != null && cs[index].NestedConditions.Length != 0)
        this.SetTempValuesToNested(ref cs[index].NestedConditions, tempValues, ref conditionNumber);
    }
  }

  /// <summary>Рекурсивно бежит по нодам и раскрывает их</summary>
  /// <param name="nodes"></param>
  private void ExpandTreeListNodes(TreeListNodes nodes)
  {
    for (int index = 0; index < nodes.Count; ++index)
    {
      nodes[index].Expanded = true;
      if (nodes[index].Nodes != null && nodes[index].Nodes.Count > 0)
        this.ExpandTreeListNodes(nodes[index].Nodes);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  /// <param name="obj"></param>
  public void ReloadSelectionType(IUserSession session, IDBObject obj)
  {
    IDBAttribute attributeByGuid = obj.GetAttributeByGuid(new Guid("cad00158-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid == null || CompareValuesHelper.NormalizedValue(attributeByGuid.Value) == null)
      return;
    this._selectionType = (SelectionType) Convert.ToInt32(attributeByGuid.Value);
  }

  private void CheckEmptyValues(TreeListNodes nodes)
  {
    for (int index = 0; index < nodes.Count; ++index)
    {
      ConditionStructureNode tag = (ConditionStructureNode) nodes[index].Tag;
      if (tag.ConditionStruct.Attribute != null && tag.ConditionStruct.RelationalOperator != RelationalOperators.AttributeExists && tag.ConditionStruct.RelationalOperator != RelationalOperators.NotExistsOrEmpty && tag.ConditionStruct.RelationalOperator != RelationalOperators.Empty && tag.ConditionStruct.RelationalOperator != RelationalOperators.NotEmpty && tag.ConditionStruct.RelationalOperator != RelationalOperators.None && tag.ConditionStruct.RelationalOperator != RelationalOperators.NOP && CompareValuesHelper.NormalizedValue(tag.ConditionStruct.Value) == null)
        this.SetNodeEnabled(nodes[index], false);
      else if (nodes[index].Nodes.Count > 0)
        this.CheckEmptyValues(nodes[index].Nodes);
    }
  }

  /// <summary>Cохранение значений атрибутов настройки выборки</summary>
  public void SelectionSave()
  {
    if (this._objectID != 0L)
    {
      this.CheckNodeCorrect();
      if (this.treeList1.Nodes.Count > 0)
        this.CheckEmptyValues(this.treeList1.Nodes);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this._objectID);
        if ((dbObject as IDBSecurity).CheckAccess(ActionType.Edit, true, false))
        {
          this._selectionConditionWrapper.ToBase(sessionKeeper.Session, dbObject);
        }
        else
        {
          bool flag = false;
          if (sessionKeeper.Session.EnableEditOwnSelections)
          {
            ISelectionsService service = (ISelectionsService) ServicesManager.GetService(typeof (ISelectionsService));
            List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
            if (this.treeList1.Nodes != null && this.treeList1.Nodes.Count > 0)
              conditionStructureList = this._selectionConditionWrapper.UnpackConditionTreeList(sessionKeeper.Session, this.treeList1.Nodes);
            ConditionStructure[] cs2 = this._loadedStructures != null ? this._loadedStructures.ToArray() : new ConditionStructure[0];
            List<object[]> objArrayList = new List<object[]>();
            if (conditionStructureList.Count == cs2.Length)
            {
              if (!ConditionStructure.Equals(conditionStructureList.ToArray(), cs2))
                flag = true;
              else
                this.GetTemporaryValues(objArrayList, conditionStructureList.ToArray());
            }
            else
              flag = true;
            if (!flag)
              service.SetTemporaryValues(this._objectID, objArrayList);
          }
          else
            flag = true;
          if (flag)
            (dbObject as IDBSecurity).CheckAccess(ActionType.Edit, true, true);
        }
        this.IsModified = false;
        (ServicesManager.GetService(typeof (INotificationService)) as INotificationService).FireEvent((object) this, (NotificationEventArgs) new DBObjectsFiltrationEventArgs("ObjectsFiltrationChanged", this._objectID, dbObject.ObjectGUID));
      }
    }
    this.SetDisableConditionsCache();
  }

  private void GetTemporaryValues(List<object[]> result, ConditionStructure[] cs)
  {
    if (cs == null || cs.Length == 0)
      return;
    for (int index = 0; index < cs.Length; ++index)
    {
      result.Add(new object[2]
      {
        cs[index].Value,
        cs[index].Value2
      });
      if (cs[index].NestedConditions != null)
        this.GetTemporaryValues(result, cs[index].NestedConditions);
    }
  }

  private List<ConditionStructure> GetCurentStructures(TreeListNodes nodes)
  {
    List<ConditionStructure> curentStructures = new List<ConditionStructure>();
    for (int index = 0; index < nodes.Count; ++index)
    {
      curentStructures.Add(((ConditionStructureNode) nodes[index].Tag).ConditionStruct);
      if (nodes[index].Nodes != null && nodes[index].Nodes.Count > 0)
        curentStructures.AddRange((IEnumerable<ConditionStructure>) this.GetCurentStructures(nodes[index].Nodes));
    }
    return curentStructures;
  }

  private int[] ObjTypesInSelection()
  {
    List<int> intList = new List<int>();
    if (this._parentMode == SelectionFormMode.InnerConditionsForm && this.ObjectTypeForInnerSelection != null && this.ObjectTypeForInnerSelection.Length != 0)
    {
      intList.AddRange((IEnumerable<int>) this.ObjectTypeForInnerSelection);
    }
    else
    {
      foreach (TreeListNode node in this.treeList1.Nodes)
      {
        if (this.IsFilterNode(node))
        {
          int int32 = Convert.ToInt32(((ConditionStructureNode) node.Tag).ConditionStruct.Value);
          if (MetaDataHelper.GetObjectType(int32) != null && !this.objTypesID.Contains(int32))
            this.objTypesID.Add(int32);
        }
      }
      foreach (int num in this.objTypesID)
        intList.Add(num);
    }
    return intList.ToArray();
  }

  /// <summary>добавление фильтра по типу объектов</summary>
  private void FilterAdd()
  {
    this.treeList1.BeginUpdate();
    try
    {
      ConditionStructure conditionStructure = new ConditionStructure((string) null, RelationalOperators.ObjectTypeFilter, (object) null, (object) null, LogicalOperators.AND, 0, true);
      object objectType = conditionStructure.Value;
      if (!this._dataProvider.ChoiseObjectType(ref objectType, this._selectionType))
        return;
      conditionStructure.Value = (object) (int) objectType;
      TreeListNode node1 = this.treeList1.AppendNode((object) null, (TreeListNode) null);
      try
      {
        TreeListNode node2 = (TreeListNode) node1.Clone();
        this.treeList1.Nodes.Insert(0, node2);
        node2.Tag = (object) new ConditionStructureNode(conditionStructure);
      }
      finally
      {
        this.treeList1.Nodes.Remove(node1);
      }
      this.UpdateGroups();
      this.IsModified = true;
      this.UpdateImageIndexes(this.treeList1.Nodes);
    }
    finally
    {
      this.treeList1.EndUpdate();
    }
  }

  /// <summary>добавление нового узла параметров выборки</summary>
  private void TreeAdd()
  {
    this.treeList1.BeginUpdate();
    try
    {
      TreeListNode focusedNode = this.treeList1.FocusedNode;
      if (focusedNode == null || focusedNode.ParentNode == null)
        this.NewNodeAdd((TreeListNode) null, -1, 0, LogicalOperators.AND);
      else if (((ConditionStructureNode) focusedNode.Tag).ConditionStruct.LogicalOperator == LogicalOperators.AND)
        this.NewNodeAdd(focusedNode.ParentNode, -1, 0, LogicalOperators.AND);
      else
        this.NewNodeAdd(focusedNode, -1, 0, LogicalOperators.AND);
      this.UpdateImageIndexes(this.treeList1.Nodes);
    }
    finally
    {
      this.treeList1.EndUpdate();
    }
  }

  /// <summary>добавление нового дочернего узла параметров выборки</summary>
  private void TreeAddChild()
  {
    this.treeList1.BeginUpdate();
    try
    {
      TreeListNode focusedNode = this.treeList1.FocusedNode;
      if (focusedNode != null)
      {
        if (((ConditionStructureNode) focusedNode.Tag).ConditionStruct.LogicalOperator == LogicalOperators.AND)
          this.NewNodeAdd(focusedNode, -1, 0, LogicalOperators.OR);
        else if (focusedNode.ParentNode != null)
          this.NewNodeAdd(focusedNode.ParentNode, -1, 0, LogicalOperators.OR);
      }
      this.UpdateImageIndexes(this.treeList1.Nodes);
    }
    finally
    {
      this.treeList1.EndUpdate();
    }
  }

  private void ChangeType()
  {
    TreeListNode focusedNode = this.treeList1.FocusedNode;
    if (focusedNode == null)
      return;
    ConditionStructure conditionStruct = ((ConditionStructureNode) focusedNode.Tag).ConditionStruct;
    if (conditionStruct.LogicalOperator == LogicalOperators.AND)
    {
      conditionStruct.LogicalOperator = LogicalOperators.OR;
    }
    else
    {
      if (conditionStruct.LogicalOperator != LogicalOperators.OR)
        return;
      conditionStruct.LogicalOperator = LogicalOperators.AND;
    }
    try
    {
      this.treeList1.BeginUpdate();
      TreeListNode node1 = (TreeListNode) null;
      try
      {
        TreeListNode parentNode = focusedNode.ParentNode;
        int num = this.NodeIndex(focusedNode);
        if (parentNode != null)
          parentNode.Nodes.Remove(focusedNode);
        else
          this.treeList1.Nodes.Remove(focusedNode);
        if (parentNode != null)
        {
          if (num > 0)
            node1 = this.treeList1.AppendNode((object) null, parentNode.Nodes[num - 1]);
          else if (parentNode.ParentNode != null)
          {
            node1 = this.treeList1.AppendNode((object) null, parentNode.ParentNode);
          }
          else
          {
            TreeListNode node2 = this.treeList1.AppendNode((object) null, (TreeListNode) null);
            try
            {
              node1 = (TreeListNode) node2.Clone();
              this.treeList1.Nodes.Insert(this.NodeIndex(parentNode) + 1, node1);
            }
            finally
            {
              this.treeList1.Nodes.Remove(node2);
            }
          }
        }
        else
          node1 = this.treeList1.AppendNode((object) null, this.treeList1.Nodes[num - 1]);
      }
      finally
      {
        if (node1 != null)
        {
          node1.Tag = (object) new ConditionStructureNode(conditionStruct);
          if (node1.ParentNode != null && !node1.ParentNode.Expanded)
            node1.ParentNode.Expanded = true;
          this.treeList1.SetFocusedNode(node1.ParentNode ?? node1);
        }
      }
      this.UpdateGroups();
      this.IsModified = true;
      this.UpdateImageIndexes(this.treeList1.Nodes);
    }
    finally
    {
      this.treeList1.EndUpdate();
    }
  }

  private void ToolStripBtnChangeType_Click(object sender, EventArgs e) => this.ChangeType();

  /// <summary>редактирование условия</summary>
  private void TreeEdit()
  {
    TreeListNode focusedNode = this.treeList1.FocusedNode;
    if (focusedNode == null)
      return;
    ConditionStructureNode tag = (ConditionStructureNode) focusedNode.Tag;
    ConditionStructure locCS = tag.ConditionStruct;
    if (!tag.Enabled)
      return;
    this.treeList1.BeginUpdate();
    try
    {
      bool flag = false;
      bool handled = false;
      foreach (ISelectionFormCustomCommandsSubscriber subscriber in ServicesManager.GetService<ISelectionFormCustomCommandsService>().Subscribers)
      {
        ConditionStructure conditionStructure = subscriber.Edit(locCS, ref handled);
        if (handled)
        {
          conditionStructure.GroupID = locCS.GroupID;
          if (conditionStructure.Attribute == null)
          {
            if (conditionStructure.RelationalOperator == RelationalOperators.Empty)
              break;
          }
          focusedNode.Tag = (object) new ConditionStructureNode(conditionStructure);
          flag = true;
          break;
        }
      }
      if (!handled)
      {
        if (locCS.RelationalOperator == RelationalOperators.ObjectTypeFilter)
        {
          object objectType = locCS.Value;
          if (this._dataProvider.ChoiseObjectType(ref objectType, this._selectionType))
          {
            locCS.Value = (object) (int) objectType;
            ((ConditionStructureNode) focusedNode.Tag).ConditionStruct = locCS;
            flag = true;
          }
        }
        else
        {
          IConditionControllersService service = ServicesManager.GetService<IConditionControllersService>();
          if (service == null || service.Controllers == null)
            return;
          IConditionController last = Array.FindLast<IConditionController>(service.Controllers, (Predicate<IConditionController>) (x => x.IsHandleConditionStructure(locCS) && x.SupportedDataSource.Equals((object) this._dataSourceType)));
          if (last != null)
          {
            ConditionStructure conditionStructure = last.EditCondition(this._objectID, locCS.Clone(), this.ObjTypesInSelection());
            if (!locCS.EqualsWithValues(conditionStructure))
            {
              conditionStructure.LogicalOperator = locCS.LogicalOperator;
              focusedNode.Tag = (object) new ConditionStructureNode(conditionStructure);
              flag = true;
            }
          }
        }
      }
      if (flag)
      {
        this.SetCaptionForNode(focusedNode);
        this.IsModified = true;
      }
      this.UpdateImageIndexes(this.treeList1.Nodes);
    }
    finally
    {
      this.treeList1.EndUpdate();
    }
  }

  /// <summary>Показать/скрыть настройки условия</summary>
  private void TreeDetail() => this._showConditionSettings = !this._showConditionSettings;

  /// <summary>Включить/выключить условие</summary>
  private void TreeEnable()
  {
    if (this._autoCheck)
      return;
    this.bEnable.Checked = !this.bEnable.Checked;
    this.bEdit.Enabled = !this.bEnable.Checked && !this._readOnly;
    TreeListNode focusedNode = this.treeList1.FocusedNode;
    if (focusedNode == null)
      return;
    this.SetNodeEnabled(focusedNode, !this.bEnable.Checked);
  }

  private void SetNodeEnabled(TreeListNode node, bool enable)
  {
    this.treeList1.BeginUpdate();
    try
    {
      ConditionStructureNode tag = (ConditionStructureNode) node.Tag;
      ConditionStructure conditionStruct = tag.ConditionStruct;
      tag.Enabled = enable;
      this.UpdateChildNodesEnable(node.Nodes, tag.Enabled);
      this.UpdateImageIndex(node);
      this.UpdateConditionControls(false);
      this.SetDisableConditionsCache();
      this.IsNeedRefresh = true;
    }
    finally
    {
      this.treeList1.EndUpdate();
    }
  }

  private void UpdateChildNodesEnable(TreeListNodes aNodes, bool enable)
  {
    if (aNodes == null || aNodes.Count <= 0)
      return;
    foreach (TreeListNode aNode in aNodes)
    {
      ((ConditionStructureNode) aNode.Tag).Enabled = enable;
      this.UpdateChildNodesEnable(aNode.Nodes, enable);
    }
  }

  private void SetDisableConditionsCache()
  {
    ISelectionsService service = (ISelectionsService) ServicesManager.GetService(typeof (ISelectionsService));
    if (service == null)
      return;
    List<int> intList = new List<int>(1);
    int nextIndex = 0;
    this.CheckNodesArray(this.treeList1.Nodes, intList, ref nextIndex);
    service.DisableConditionStructures(this._objectID, intList);
  }

  private void CheckNodesArray(TreeListNodes aNodes, List<int> disableIndexes, ref int nextIndex)
  {
    if (aNodes == null || aNodes.Count <= 0)
      return;
    for (int index = 0; index < aNodes.Count; ++index)
    {
      TreeListNode aNode = aNodes[index];
      if (!((ConditionStructureNode) aNode.Tag).Enabled)
        disableIndexes.Add(nextIndex);
      ++nextIndex;
      this.CheckNodesArray(aNode.Nodes, disableIndexes, ref nextIndex);
    }
  }

  private void RefreshNodesEnable(TreeListNodes aNodes, ISelectionsService service)
  {
    if (aNodes == null || aNodes.Count <= 0)
      return;
    foreach (TreeListNode aNode in aNodes)
    {
      ((ConditionStructureNode) aNode.Tag).Enabled = service.IsEnabledConditionStructure(this._objectID, aNode.Id);
      this.RefreshNodesEnable(aNode.Nodes, service);
    }
  }

  /// <summary>
  /// Обновление значка на узлах дерева в соответствии с представляемыми ими оператороми
  /// </summary>
  /// <param name="nodes">Коллекция узлов для которых надо обновить иконки</param>
  private void UpdateImageIndexes(TreeListNodes nodes)
  {
    if (nodes == null)
      return;
    foreach (TreeListNode node in nodes)
      this.UpdateImageIndex(node);
  }

  private void UpdateImageIndex(TreeListNode node)
  {
    ConditionStructureNode tag = (ConditionStructureNode) node.Tag;
    ConditionStructure conditionStruct = tag.ConditionStruct;
    bool flag = tag.ConditionStruct.NestedConditions != null;
    node.StateImageIndex = !this.IsFilterNode(node) ? (!tag.Enabled ? (conditionStruct.LogicalOperator == LogicalOperators.AND ? (flag ? 3 : 2) : (flag ? 7 : 6)) : (conditionStruct.LogicalOperator == LogicalOperators.AND ? (flag ? 1 : 0) : (flag ? 5 : 4))) : (tag.Enabled ? 8 : 9);
    node.CheckState = conditionStruct.NestedConditions != null ? CheckState.Checked : CheckState.Unchecked;
    this.UpdateImageIndexes(node.Nodes);
  }

  /// <summary>
  /// Проверка - является ли данный узел фильтром по типу объекта
  /// </summary>
  /// <param name="node">Узел, который надо проверить</param>
  /// <returns>Если узел является фильтром по типу, то true, иначе false</returns>
  private bool IsFilterNode(TreeListNode node)
  {
    return node != null && node.Tag != null && ((ConditionStructureNode) node.Tag).ConditionStruct.RelationalOperator == RelationalOperators.ObjectTypeFilter;
  }

  /// <summary>
  /// Проверка на заполненность "Tag" у текущего выбранного узла
  /// </summary>
  private void CheckNodeCorrect()
  {
    TreeListNode focusedNode = this.treeList1.FocusedNode;
    if (focusedNode == null || focusedNode.Tag != null)
      return;
    focusedNode.Tag = (object) new ConditionStructureNode(new ConditionStructure((string) null, RelationalOperators.None, (object) null, (object) null, LogicalOperators.NONE, 0, true));
  }

  /// <summary>
  /// Обновление доступности элементов управления, связанныз с редактированием условий выборки
  /// </summary>
  /// <param name="onlyItems">Признак, указывающий на то, что нужно произвести только обновление
  /// доступности пунктов меню</param>
  private void UpdateConditionControls(bool onlyItems)
  {
    this.UpdateConditionControls(onlyItems, true);
  }

  /// <summary>
  /// Обновление доступности элементов управления, связанныз с редактированием условий выборки
  /// </summary>
  /// <param name="onlyItems">Признак, указывающий на то, что нужно произвести только обновление
  /// доступности пунктов меню</param>
  /// <param name="updateFilter">Признак, указывающий на то, что нужно произвести обновление доступности элементов управления, связанныз с фильтром по типу объектов</param>
  private void UpdateConditionControls(bool onlyItems, bool updateFilter)
  {
    TreeListNode focusedNode = this.treeList1.FocusedNode;
    ConditionStructureNode conditionStructureNode = (ConditionStructureNode) null;
    if (focusedNode != null)
      conditionStructureNode = (ConditionStructureNode) focusedNode.Tag;
    this.miDel.Enabled = focusedNode != null && !this._readOnly;
    this.miEdit.Enabled = focusedNode != null && conditionStructureNode != null && conditionStructureNode.Enabled && !this._readOnly;
    this.miAddChild.Enabled = focusedNode != null && !this.IsFilterNode(focusedNode) && conditionStructureNode != null && conditionStructureNode.Enabled && !this._readOnly;
    this.miClear.Enabled = this.treeList1.Nodes.Count > 0 && !this._readOnly;
    if (updateFilter)
      this.UpdateFilterControls();
    this.miInnerConditions.Enabled = !this.ObjectAttributesOnlyConditions && !this._readOnly && conditionStructureNode != null && (conditionStructureNode.ConditionStruct.RelationalOperator == RelationalOperators.ConsistFromType || conditionStructureNode.ConditionStruct.RelationalOperator == RelationalOperators.NotConsistFromType || conditionStructureNode.ConditionStruct.RelationalOperator == RelationalOperators.EntersInType || conditionStructureNode.ConditionStruct.RelationalOperator == RelationalOperators.NotEntersInType);
    this.miAdd.Enabled = (conditionStructureNode == null || conditionStructureNode.ConditionStruct.LogicalOperator != LogicalOperators.OR || conditionStructureNode.Enabled) && !this._readOnly;
    if (focusedNode != null && !this._readOnly)
    {
      if (conditionStructureNode != null && !conditionStructureNode.Enabled && focusedNode.ParentNode != null && !((ConditionStructureNode) focusedNode.ParentNode.Tag).Enabled)
        this.miEnable.Enabled = this.bEnable.Enabled = false;
      else
        this.miEnable.Enabled = this.bEnable.Enabled = true;
    }
    else
      this.miEnable.Enabled = this.bEnable.Enabled = false;
    this.miChangeType.Enabled = this.bChangeType.Enabled = conditionStructureNode != null && conditionStructureNode.Enabled && focusedNode != null && !this.IsFilterNode(focusedNode) && (focusedNode.Level != 0 || this.NodeIndex(focusedNode) != 0 || conditionStructureNode.ConditionStruct.LogicalOperator != LogicalOperators.AND) && focusedNode.Nodes.Count == 0 && !this._readOnly;
    if (!onlyItems)
    {
      this._autoCheck = true;
      this.bEnable.Checked = focusedNode != null && conditionStructureNode != null && !conditionStructureNode.Enabled && !this._readOnly;
      this._autoCheck = false;
      this.bDel.Enabled = this.miDel.Enabled;
      this.bEdit.Enabled = this.miEdit.Enabled;
      this.bAddChild.Enabled = this.miAddChild.Enabled;
      this.bAdd.Enabled = this.miAdd.Enabled;
      this.miEnable.Text = this.bEnable.Checked ? LocalizationHolder.rm.GetString("Client.Core_700") : LocalizationHolder.rm.GetString("Client.Core_701");
      this.bClear.Enabled = this.miClear.Enabled;
    }
    ISelectionFormCustomCommandsService service = (ISelectionFormCustomCommandsService) ServicesManager.GetService(typeof (ISelectionFormCustomCommandsService));
    List<ConditionStructure> structures = this.GetStructures(this.treeList1.Nodes);
    for (int index = 0; index < this._additionalMItems.Count; ++index)
    {
      ToolStripMenuItem additionalMitem = this._additionalMItems[index];
      additionalMitem.Enabled = !this.ObjectAttributesOnlyConditions && service.EnableButton(structures.ToArray(), conditionStructureNode != null ? conditionStructureNode.ConditionStruct : ConditionStructure.Empty, additionalMitem.Name);
      if (!onlyItems)
        this._additionalButtons[index].Enabled = additionalMitem.Enabled;
    }
  }

  private List<ConditionStructure> GetStructures(TreeListNodes nodes)
  {
    List<ConditionStructure> structures = new List<ConditionStructure>();
    for (int index = 0; index < nodes.Count; ++index)
    {
      structures.Add(((ConditionStructureNode) nodes[index].Tag).ConditionStruct);
      if (nodes[index].Nodes.Count > 0)
        structures.AddRange((IEnumerable<ConditionStructure>) this.GetStructures(nodes[index].Nodes));
    }
    return structures;
  }

  private int NodeIndex(TreeListNode node)
  {
    TreeListNodes treeListNodes = node.ParentNode != null ? node.ParentNode.Nodes : this.treeList1.Nodes;
    for (int index = 0; index < treeListNodes.Count; ++index)
    {
      if (treeListNodes[index].Id == node.Id)
        return index;
    }
    return 0;
  }

  /// <summary>
  /// Обновление доступности элементов управления, связанныз с фильтром по типу объектов
  /// </summary>
  private void UpdateFilterControls()
  {
    this.miAddFilter.Enabled = this._selectionType != SelectionType.Mail && this._parentMode != SelectionFormMode.InnerConditionsForm && this.FilterIsAvailable() && !this.IsFilterPresent(this.treeList1.Nodes) && !this._readOnly && !this.ObjectAttributesOnlyConditions;
    this.bAddFilter.Enabled = this.miAddFilter.Enabled;
  }

  /// <summary>Доступна ли фильтрация</summary>
  /// <returns></returns>
  private bool FilterIsAvailable()
  {
    return (this.objTypesID == null || this.objTypesID.Count == 0) && this._selectionType != SelectionType.Archiv && this._selectionType != SelectionType.ObjectType;
  }

  private bool IsFilterPresent(TreeListNodes nodes)
  {
    if (nodes != null)
    {
      IEnumerator enumerator = nodes.GetEnumerator();
      try
      {
        if (enumerator.MoveNext())
        {
          TreeListNode current = (TreeListNode) enumerator.Current;
          return this.IsFilterNode(current) || this.IsFilterPresent(current.Nodes);
        }
      }
      finally
      {
        if (enumerator is IDisposable disposable)
          disposable.Dispose();
      }
    }
    return false;
  }

  /// <summary>
  /// Если доступны только условия по атрибутам указанного типа объектов, то дополнительно отфильтруем
  /// </summary>
  private IConditionController[] FilterControllers(IConditionController[] conditionControllers)
  {
    if (conditionControllers.Length == 0 || !this.ObjectAttributesOnlyConditions)
      return conditionControllers;
    List<IConditionController> conditionControllerList = new List<IConditionController>();
    foreach (IConditionController conditionController in conditionControllers)
    {
      if (conditionController.AttributesCondition)
        conditionControllerList.Add(conditionController);
    }
    return conditionControllerList.ToArray();
  }

  /// <summary>локальная функция для создания нового TTreeNode</summary>
  /// <param name="parentNode">Коллекция узлов к которой производится добавление</param>
  /// <param name="aIndex">Индекс позиции в которую будет произволиться вставка узла</param>
  /// <param name="aGroupID">Идентификатор группы</param>
  /// <param name="aLogOp">Логический оператор</param>
  private void NewNodeAdd(
    TreeListNode parentNode,
    int aIndex,
    int aGroupID,
    LogicalOperators aLogOp)
  {
    this.treeList1.BeginUpdate();
    try
    {
      IConditionController[] controllersForSelection = ServicesManager.GetService<IConditionControllersService>().GetConditionControllersForSelection(this._dataSourceType, this._selectionType, this._parentMode == SelectionFormMode.InnerConditionsForm);
      IConditionController conditionController = (IConditionController) null;
      IConditionController[] controllers = this.FilterControllers(controllersForSelection);
      if (controllers.Length > 1)
      {
        ConditionTypeSelector conditionTypeSelector = new ConditionTypeSelector(this._dataSourceType);
        conditionTypeSelector.Initialize(aLogOp, controllers);
        if (conditionTypeSelector.ShowDialog() == DialogResult.OK)
          conditionController = conditionTypeSelector.SelectedController;
      }
      else
        conditionController = controllers[0];
      if (conditionController == null)
        return;
      ConditionStructure condition = conditionController.CreateCondition(this._objectID, this.ObjTypesInSelection());
      if (condition.Equals((object) ConditionStructure.Empty))
        return;
      condition.LogicalOperator = aLogOp;
      TreeListNode node = this.treeList1.AppendNode((object) null, parentNode);
      TreeListNodes treeListNodes = parentNode == null ? this.treeList1.Nodes : parentNode.Nodes;
      try
      {
        node.Tag = (object) new ConditionStructureNode(condition);
        if (aIndex == -1)
        {
          treeListNodes.Add((TreeListNode) node.Clone());
        }
        else
        {
          treeListNodes.Insert(aIndex, (TreeListNode) node.Clone());
          if (node.ParentNode != null && node.ParentNode.Tag != null)
          {
            ConditionStructureNode tag = (ConditionStructureNode) node.ParentNode.Tag;
            if (!tag.Enabled)
              ((ConditionStructureNode) node.Tag).Enabled = tag.Enabled;
          }
        }
        if (node.ParentNode != null)
        {
          if (!node.ParentNode.Expanded)
            node.ParentNode.Expanded = true;
        }
      }
      finally
      {
        treeListNodes.Remove(node);
      }
      this.UpdateGroups();
      this.IsModified = true;
    }
    finally
    {
      this.treeList1.EndUpdate();
    }
  }

  /// <summary>
  /// Для GroupID производится следующующая обработка:
  /// если GroupID больше 0, то открывается столько скобок, сколько равен GroupID,
  /// если он меньше 0, то, соответственно, столько скобок закрывается.
  /// Последним оператором закрываются все открытые скобки автоматически
  /// </summary>
  private void UpdateGroups() => this.UpdateGroupsRecurs(this.treeList1.Nodes, 0);

  /// <summary>
  /// Обновление групп логических операторов (проверка скобок для группировки операторов)
  /// </summary>
  /// <param name="nodes">Коллекция узлов для проверки</param>
  /// <param name="aBrackCounter">Количество открытых скобок для данной коллекции</param>
  private void UpdateGroupsRecurs(TreeListNodes nodes, int aBrackCounter)
  {
    for (int index = 0; index < nodes.Count; ++index)
    {
      TreeListNode node = nodes[index];
      this.SetCaptionForNode(node);
      this.ReloadPossibleValues4Node(node);
      ConditionStructure conditionStruct = ((ConditionStructureNode) node.Tag).ConditionStruct;
      if (node.Nodes.Count > 0)
      {
        conditionStruct.GroupID = 1;
        if (index == nodes.Count - 1)
          this.UpdateGroupsRecurs(node.Nodes, aBrackCounter + 1);
        else
          this.UpdateGroupsRecurs(node.Nodes, 1);
      }
      else
        conditionStruct.GroupID = index != nodes.Count - 1 ? 0 : -aBrackCounter;
      conditionStruct.LogicalOperator = Convert.ToBoolean(node.Level % 2) ? LogicalOperators.OR : LogicalOperators.AND;
      ((ConditionStructureNode) node.Tag).ConditionStruct = conditionStruct;
    }
  }

  private bool IsRelationNode(TreeListNode aNode)
  {
    RelationalOperators relationalOperator = ((ConditionStructureNode) aNode.Tag).ConditionStruct.RelationalOperator;
    switch (relationalOperator)
    {
      case RelationalOperators.EntersIn:
      case RelationalOperators.ConsistFrom:
        return true;
      default:
        return relationalOperator == RelationalOperators.ExistsInVersionContext;
    }
  }

  /// <summary>Обновление заголовка узла</summary>
  /// <param name="aNode">Узел для которого надо обновить заголовок</param>
  private void SetCaptionForNode(TreeListNode aNode)
  {
    if (aNode == null || aNode.Tag == null || !(aNode.Tag is ConditionStructureNode))
      return;
    ConditionStructure conditionStruct = ((ConditionStructureNode) aNode.Tag).ConditionStruct;
    foreach (IConditionController controller in ServicesManager.GetService<IConditionControllersService>().Controllers)
    {
      string condition;
      string val;
      if (controller.IsHandleConditionStructure(conditionStruct) && controller.HandleConditionCaption(conditionStruct, out condition, out val))
      {
        aNode.SetValue((object) 0, (object) condition);
        aNode.SetValue((object) 1, (object) val);
        return;
      }
    }
    Dictionary<object, string> possibleValues = (Dictionary<object, string>) null;
    SelectionParameterTypes selParType = !this.IsFilterNode(aNode) ? (this.IsRelationNode(aNode) ? SelectionParameterTypes.sptObject : this.GetSelectionParameterTypes(((ConditionStructureNode) aNode.Tag).ConditionStruct, ref possibleValues, true)) : SelectionParameterTypes.sptObjectType;
    string str1 = ((ConditionStructureNode) aNode.Tag).ConditionStruct.Value != null ? this._dataProvider.ConvertToString(conditionStruct.Attribute, conditionStruct.RelationalOperator, selParType, conditionStruct.Value, possibleValues, conditionStruct.TypeID) : string.Empty;
    string str2 = ((ConditionStructureNode) aNode.Tag).ConditionStruct.Value != null ? this._dataProvider.ConvertToString(conditionStruct.Attribute, conditionStruct.RelationalOperator, selParType, conditionStruct.Value2, possibleValues, conditionStruct.TypeID) : string.Empty;
    string val1 = conditionStruct.RelationalOperator == RelationalOperators.Between || conditionStruct.RelationalOperator == RelationalOperators.NotBetween ? string.Format(LocalizationHolder.rm.GetString("Client.Core_1518"), (object) str1, (object) str2) : str1;
    aNode.SetValue((object) 0, (object) this._dataProvider.GenerateConditionCaption(conditionStruct, str1, str2));
    aNode.SetValue((object) 1, (object) val1);
  }

  /// <summary>удаление выделнных узлов параметров выборки</summary>
  private void TreeDel()
  {
    TreeListNode focusedNode = this.treeList1.FocusedNode;
    if (focusedNode == null)
      return;
    if (focusedNode.Nodes.Count == 0)
    {
      this.DeleteNode(focusedNode);
    }
    else
    {
      using (ConditionDeleteForm conditionDeleteForm = new ConditionDeleteForm())
      {
        if (conditionDeleteForm.ShowDialog() != DialogResult.OK)
          return;
        this.treeList1.BeginUpdate();
        try
        {
          if (conditionDeleteForm.DeleteType == DeleteNodeType.NodeOnly)
          {
            TreeListNode node = focusedNode.Nodes[0];
            TreeListNode destinationNode1 = (TreeListNode) null;
            if (node.Nodes.Count > 0 && ((ConditionStructureNode) node.Nodes[0].Tag).ConditionStruct.LogicalOperator == LogicalOperators.AND)
            {
              if (focusedNode.ParentNode != null)
                destinationNode1 = focusedNode.ParentNode;
              List<TreeListNode> treeListNodeList = new List<TreeListNode>(node.Nodes.Count);
              for (int index = 0; index < node.Nodes.Count; ++index)
                treeListNodeList.Add(node.Nodes[index]);
              for (int index = 0; index < treeListNodeList.Count; ++index)
                this.treeList1.MoveNode(treeListNodeList[index], destinationNode1, true);
            }
            List<TreeListNode> treeListNodeList1 = new List<TreeListNode>(focusedNode.Nodes.Count);
            for (int index = 0; index < focusedNode.Nodes.Count; ++index)
              treeListNodeList1.Add(focusedNode.Nodes[index]);
            for (int index = 0; index < treeListNodeList1.Count; ++index)
            {
              TreeListNode destinationNode2 = (TreeListNode) null;
              if (index == 0)
              {
                if (focusedNode.ParentNode != null)
                  destinationNode2 = focusedNode.ParentNode;
              }
              else
                destinationNode2 = node;
              this.treeList1.MoveNode(treeListNodeList1[index], destinationNode2, true);
            }
          }
          this.DeleteNode(focusedNode);
        }
        finally
        {
          this.treeList1.EndUpdate();
        }
      }
    }
    this.treeList1.BeginUpdate();
    try
    {
      this.UpdateGroups();
      this.IsModified = true;
      this.UpdateImageIndexes(this.treeList1.Nodes);
    }
    finally
    {
      this.treeList1.EndUpdate();
    }
  }

  private void DeleteNode(TreeListNode loc_node)
  {
    if (this.IsFilterNode(loc_node))
      this.objTypesID.Remove(Convert.ToInt32(((ConditionStructureNode) loc_node.Tag).ConditionStruct.Value));
    (loc_node.ParentNode == null ? this.treeList1.Nodes : loc_node.ParentNode.Nodes).Remove(loc_node);
  }

  /// <summary>очистка (удаление всех узлов) параметров выборки</summary>
  private void TreeClear()
  {
    if (this.treeList1.Nodes.Count <= 0)
      return;
    this.treeList1.BeginUpdate();
    try
    {
      foreach (TreeListNode node in this.treeList1.Nodes)
      {
        if (this.IsFilterNode(node))
          this.objTypesID.Remove(Convert.ToInt32(((ConditionStructureNode) node.Tag).ConditionStruct.Value));
      }
      this.treeList1.Nodes.Clear();
      this.IsModified = true;
    }
    finally
    {
      this.treeList1.EndUpdate();
    }
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

  private void ToolTreeAddFilter_Click(object sender, EventArgs e) => this.FilterAdd();

  private void ToolTreeAdd_Click(object sender, EventArgs e) => this.TreeAdd();

  private void ToolTreeAddChild_Click(object sender, EventArgs e) => this.TreeAddChild();

  private void ToolTreeEdit_Click(object sender, EventArgs e) => this.TreeEdit();

  private void ToolTreeEnable_Click(object sender, EventArgs e) => this.TreeEnable();

  private void ToolTreeDel_Click(object sender, EventArgs e) => this.TreeDel();

  private void ToolTreeClear_Click(object sender, EventArgs e) => this.TreeClear();

  private void ToolStripBtnGO_Click(object sender, EventArgs e)
  {
    if (this.IsModified)
      this.SelectionSave();
    if (!(this.Parent is Intermech.Navigator.SelectionView.SelectionView))
      return;
    ((Intermech.Navigator.SelectionView.SelectionView) this.Parent).GoClick();
  }

  private void BiInnerConditions_Click(object sender, EventArgs e)
  {
    TreeListNode focusedNode = this.treeList1.FocusedNode;
    SelectionForm selectionForm = new SelectionForm()
    {
      ParentMode = SelectionFormMode.InnerConditionsForm
    };
    ConditionStructure[] css = (ConditionStructure[]) null;
    ConditionStructureNode tag = (ConditionStructureNode) focusedNode.Tag;
    if (tag != null)
    {
      if (tag.ConditionStruct.NestedConditions != null)
        css = tag.ConditionStruct.NestedConditions;
      if ((tag.ConditionStruct.RelationalOperator == RelationalOperators.ConsistFromType || tag.ConditionStruct.RelationalOperator == RelationalOperators.EntersInType) && tag.ConditionStruct.Value != null)
        selectionForm.ObjectTypeForInnerSelection = ControlsHelper.GetObjectTypeFilterForInnerForm(tag.ConditionStruct.Value);
    }
    selectionForm.ReadOnly = false;
    selectionForm.SelectionLoad(this._objectID, new List<long>(), css);
    if (selectionForm.ShowDialog() != DialogResult.OK)
      return;
    tag.ConditionStruct.NestedConditions = selectionForm.Conditions;
    this.UpdateImageIndex(focusedNode);
    this.IsModified = true;
  }

  private void ButtonApply_Click(object sender, EventArgs e)
  {
    if (this._parentMode == SelectionFormMode.InnerConditionsForm)
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }
    else
      this.SelectionSave();
  }

  private void ButtonCancel_Click(object sender, EventArgs e)
  {
    if (this._parentMode == SelectionFormMode.InnerConditionsForm)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }
    else
      this.SelectionLoad(this._objectID, this._objIDList);
  }

  private void ReloadPossibleValues4Node(TreeListNode node)
  {
    this.riPossibleValues.EditValueChanged -= new EventHandler(this.RiPossibleValues_EditValueChanged);
    try
    {
      if (node == null || node.Tag == null)
        return;
      if (((ConditionStructureNode) node.Tag).PossibleValues == null)
      {
        int attributeId = this._dataProvider.GetAttributeID(((ConditionStructureNode) node.Tag).ConditionStruct.Attribute);
        if (attributeId != 0)
        {
          Dictionary<object, string> possibleValues = this._dataProvider.GetPossibleValues((object) attributeId);
          if (possibleValues != null && possibleValues.Count > 0)
            ((ConditionStructureNode) node.Tag).PossibleValues = possibleValues;
        }
      }
      if (((ConditionStructureNode) node.Tag).PossibleValues == null || ((ConditionStructureNode) node.Tag).PossibleValues.Count <= 0)
        return;
      this.riPossibleValues.BeginUpdate();
      try
      {
        this.riPossibleValues.Items.Clear();
        foreach (KeyValuePair<object, string> possibleValue in ((ConditionStructureNode) node.Tag).PossibleValues)
          this.riPossibleValues.Items.Add((object) new SelectionForm.PossibleValueItem(possibleValue.Key, possibleValue.Value));
      }
      finally
      {
        this.riPossibleValues.EndUpdate();
      }
    }
    finally
    {
      this.riPossibleValues.EditValueChanged += new EventHandler(this.RiPossibleValues_EditValueChanged);
    }
  }

  private void TreeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
  {
    this.CheckNodeCorrect();
    this.UpdateConditionControls(false);
    if (e.Node == null)
      return;
    this.ReloadPossibleValues4Node(e.Node);
  }

  private void TreeList1_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    if (this._readOnly)
      return;
    this.TreeEdit();
  }

  private void TreeList1_MouseDown(object sender, MouseEventArgs e)
  {
  }

  private void TreeList1_GetCustomNodeCellEdit(object sender, GetCustomNodeCellEditEventArgs e)
  {
    if (e.Node.Tag == null || this.IsFilterNode(e.Node))
    {
      e.RepositoryItem = (RepositoryItem) this.riReadOnly;
    }
    else
    {
      ConditionStructureNode cs = (ConditionStructureNode) e.Node.Tag;
      if (cs == null || !cs.Enabled || e.Column.Name == LocalizationHolder.rm.GetString("Client.Core_1519"))
      {
        e.RepositoryItem = (RepositoryItem) this.riReadOnly;
      }
      else
      {
        if (!(e.Column.Name == "columnValues"))
          return;
        if (SelectionParameter.IsNoneValueOpr(cs.ConditionStruct.RelationalOperator))
          e.RepositoryItem = (RepositoryItem) this.riReadOnly;
        else if (cs.ConditionStruct.RelationalOperator == RelationalOperators.Between || cs.ConditionStruct.RelationalOperator == RelationalOperators.In || cs.ConditionStruct.RelationalOperator == RelationalOperators.NotBetween || cs.ConditionStruct.RelationalOperator == RelationalOperators.NotIn)
        {
          e.RepositoryItem = (RepositoryItem) this.riEditor;
        }
        else
        {
          SelectionParameterTypes selectionParameterTypes = this.GetSelectionParameterTypes(cs.ConditionStruct);
          if (cs.PossibleValues != null && cs.PossibleValues.Count > 0)
            e.RepositoryItem = (RepositoryItem) this.riPossibleValues;
          else if (Array.Exists<RelationalOperators>(SelectionParameter.StringOperators, (Predicate<RelationalOperators>) (x => x.Equals((object) cs.ConditionStruct.RelationalOperator))))
          {
            e.RepositoryItem = (RepositoryItem) this.riString;
          }
          else
          {
            switch (selectionParameterTypes)
            {
              case SelectionParameterTypes.sptString:
                e.RepositoryItem = (RepositoryItem) this.riString;
                break;
              case SelectionParameterTypes.sptNumber:
                e.RepositoryItem = (RepositoryItem) this.riNumber;
                break;
              case SelectionParameterTypes.sptFloat:
                e.RepositoryItem = (RepositoryItem) this.riFloat;
                break;
              case SelectionParameterTypes.sptBool:
                e.RepositoryItem = (RepositoryItem) this.riBool;
                break;
              case SelectionParameterTypes.sptDate:
                if (Convert.ToString(cs.ConditionStruct.Value) == Intermech.Consts.CurrentDateFunction)
                  e.RepositoryItem = (RepositoryItem) this.riEditor;
                if (cs.ConditionStruct.RelationalOperator == RelationalOperators.LastNDays || cs.ConditionStruct.RelationalOperator == RelationalOperators.NextNDays)
                {
                  e.RepositoryItem = (RepositoryItem) this.riNumber;
                  break;
                }
                e.RepositoryItem = (RepositoryItem) this.riDateTime;
                break;
              case SelectionParameterTypes.sptSiteID:
              case SelectionParameterTypes.sptObject:
              case SelectionParameterTypes.sptCheckOutBy:
              case SelectionParameterTypes.sptUser:
              case SelectionParameterTypes.sptObjectType:
              case SelectionParameterTypes.sptLifecycleLevel:
              case SelectionParameterTypes.sptSubjectArea:
              case SelectionParameterTypes.sptLinkType:
              case SelectionParameterTypes.sptLifecycleStep:
              case SelectionParameterTypes.sptGlobalID:
              case SelectionParameterTypes.sptMeasured:
              case SelectionParameterTypes.sptHandler:
                e.RepositoryItem = (RepositoryItem) this.riButton;
                break;
              default:
                e.RepositoryItem = (RepositoryItem) this.riEditor;
                break;
            }
          }
        }
      }
    }
  }

  private SelectionParameterTypes GetSelectionParameterTypes(ConditionStructure cs)
  {
    Dictionary<object, string> possibleValues = (Dictionary<object, string>) null;
    return this.GetSelectionParameterTypes(cs, ref possibleValues, false);
  }

  private SelectionParameterTypes GetSelectionParameterTypes(
    ConditionStructure cs,
    ref Dictionary<object, string> possibleValues,
    bool getPossibleValues)
  {
    if (cs.Value is InputObjectAttribute || cs.Value is ConditionFormula)
      return SelectionParameterTypes.sptNone;
    if (SelectionParameter.IsInRelationOpr(cs.RelationalOperator))
      return cs.RelationalOperator == RelationalOperators.EntersIn || cs.RelationalOperator == RelationalOperators.ConsistFrom ? SelectionParameterTypes.sptObject : SelectionParameterTypes.sptRelationOpValue;
    int attributeId = this._dataProvider.GetAttributeID(cs.Attribute);
    FieldTypes fieldType = this._dataProvider.GetFieldType(cs.Attribute);
    if (attributeId == 0 || fieldType == FieldTypes.ftUnknown)
      return SelectionParameterTypes.sptNone;
    if (getPossibleValues)
      possibleValues = this._dataProvider.GetPossibleValues((object) attributeId);
    if (SelectionParameter.IsLinkRelationOpr(cs.RelationalOperator))
      return SelectionParameterTypes.sptObjectType;
    if ((ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) as IAttributePropertyDescriberService).GetDescriber(attributeId) != null)
      return SelectionParameterTypes.sptHandler;
    IConditionEditorAttribute handler = ((IConditionEditorAttributeService) ServicesManager.GetService(typeof (IConditionEditorAttributeService))).GetHandler(this._dataProvider.GetAttributeGuid(cs.Attribute));
    return handler != null ? handler.NodeValueType : SelectionParameter.GetNodeValueType(attributeId, fieldType);
  }

  private void RiButton_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    ConditionStructureNode cs = (ConditionStructureNode) this.treeList1.FocusedNode.Tag;
    if (cs == null)
      return;
    IConditionControllersService service = ServicesManager.GetService<IConditionControllersService>();
    if (service != null && service.Controllers != null && Array.Exists<IConditionController>(service.Controllers, (Predicate<IConditionController>) (x => x.IsHandleConditionStructure(cs.ConditionStruct))))
    {
      this.TreeEdit();
    }
    else
    {
      object addInfo = (object) null;
      int attributeId = this._dataProvider.GetAttributeID(cs.ConditionStruct.Attribute);
      if (this.IsRelationNode(this.treeList1.FocusedNode) || SelectionParameter.IsLinkRelationOpr(cs.ConditionStruct.RelationalOperator))
      {
        if (cs.ConditionStruct.Value is IList)
        {
          this.RiEditor_ButtonClick(sender, e);
          return;
        }
        if (this._dataProvider.SelectDialog(ref cs.ConditionStruct.Value, this.GetSelectionParameterTypes(cs.ConditionStruct), addInfo, attributeId, this.objTypesID?.ToArray()))
        {
          this.SetCaptionForNode(this.treeList1.FocusedNode);
          this.IsModified = true;
          return;
        }
      }
      if (attributeId == 0)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if ((ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) as IAttributePropertyDescriberService).GetDescriber(attributeId) != null)
        {
          SystemAttributeSelect sysAttrSel;
          int attType = (int) AttributeTypeValueSelector.GetAttType(sessionKeeper.Session.GetAttributeType(attributeId), out sysAttrSel);
          if (!sysAttrSel(ref cs.ConditionStruct.Value, (object) attributeId))
            return;
          this.SetCaptionForNode(this.treeList1.FocusedNode);
          this.IsModified = true;
        }
        else
        {
          FieldTypes fieldType = this._dataProvider.GetFieldType(cs.ConditionStruct.Attribute);
          if (attributeId < 0 && fieldType == FieldTypes.ftSystem)
          {
            IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(attributeId);
            SystemAttributeSelect sysAttrSel;
            int attType = (int) AttributeTypeValueSelector.GetAttType(attributeType, out sysAttrSel);
            if ((sysAttrSel != null || !ValueRelationSelector.SelectObject(ref cs.ConditionStruct.Value, attributeId, this.objTypesID?.ToArray(), (object) attributeType.SizeType, true)) && (sysAttrSel == null || !sysAttrSel(ref cs.ConditionStruct.Value, (object) attributeType.SizeType)))
              return;
            this.SetCaptionForNode(this.treeList1.FocusedNode);
            this.IsModified = true;
          }
          else
          {
            IIDLinkTranslate customService = (IIDLinkTranslate) sessionKeeper.Session.GetCustomService(typeof (IIDLinkTranslate));
            if (fieldType == FieldTypes.ftObjectLink || customService.IsIDLink(attributeId))
            {
              int objectType4ObjectLink = this._dataProvider.GetObjectType4ObjectLink(attributeId);
              if (objectType4ObjectLink != -1)
                addInfo = (object) objectType4ObjectLink;
            }
            if (!this._dataProvider.SelectDialog(ref cs.ConditionStruct.Value, this.GetSelectionParameterTypes(cs.ConditionStruct), addInfo, attributeId, this.objTypesID?.ToArray()))
              return;
            this.SetCaptionForNode(this.treeList1.FocusedNode);
            this.IsModified = true;
          }
        }
      }
    }
  }

  private void RiEditor_ButtonClick(object sender, ButtonPressedEventArgs e) => this.TreeEdit();

  private void MouseDoubleClickOnCondition(object sender, EventArgs e)
  {
    if (this._readOnly)
      return;
    this.TreeEdit();
  }

  private void OnEditValueChanged(object newValue)
  {
    ConditionStructureNode tag = (ConditionStructureNode) this.treeList1.FocusedNode.Tag;
    if (tag.ConditionStruct.Value == newValue)
      return;
    tag.ConditionStruct.Value = newValue;
    this.SetCaptionForNode(this.treeList1.FocusedNode);
    this.IsModified = true;
  }

  private void RiDateTime_EditValueChanged(object sender, EventArgs e)
  {
    this.OnEditValueChanged((object) ((DateEdit) sender).DateTime);
  }

  private void RiNumber_EditValueChanged(object sender, EventArgs e)
  {
    this.OnEditValueChanged((object) ((CalcEdit) sender).Value);
  }

  private void RiBool_EditValueChanged(object sender, EventArgs e)
  {
    this.OnEditValueChanged((object) (((ComboBoxEdit) sender).SelectedIndex == 0));
  }

  private void RiString_EditValueChanged(object sender, EventArgs e)
  {
    this.OnEditValueChanged((object) ((Control) sender).Text);
  }

  private void RiFloat_EditValueChanged(object sender, EventArgs e)
  {
    this.OnEditValueChanged((object) ((CalcEdit) sender).Value);
  }

  private void RiPossibleValues_EditValueChanged(object sender, EventArgs e)
  {
    this.OnEditValueChanged(((SelectionForm.PossibleValueItem) ((BaseEdit) sender).EditValue).Value);
  }

  private void SelectionForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (this._parentMode != SelectionFormMode.IndependentForm && this._parentMode != SelectionFormMode.InnerConditionsForm)
      return;
    FormStorage.SaveLayout((Control) this);
  }

  private void SelectionForm_Shown(object sender, EventArgs e)
  {
    if (this._parentMode != SelectionFormMode.IndependentForm && this._parentMode != SelectionFormMode.InnerConditionsForm)
      return;
    FormStorage.LoadLayout((Control) this);
  }

  private void ToolTipController1_GetActiveObjectInfo(
    object sender,
    ToolTipControllerGetActiveObjectInfoEventArgs e)
  {
  }

  public void Replace(ConditionStructure cs)
  {
    TreeListNode focusedNode = this.treeList1.FocusedNode;
    if (focusedNode == null)
      return;
    focusedNode.Tag = (object) new ConditionStructureNode(cs);
    this.SetCaptionForNode(focusedNode);
    this.UpdateImageIndex(focusedNode);
    this.IsModified = true;
  }

  public void Add(ConditionStructure cs)
  {
    this.treeList1.BeginUpdate();
    try
    {
      TreeListNode focusedNode = this.treeList1.FocusedNode;
      TreeListNode treeListNode = focusedNode == null || focusedNode.ParentNode == null ? this.NewNode((TreeListNode) null, cs) : (((ConditionStructureNode) focusedNode.Tag).ConditionStruct.LogicalOperator != LogicalOperators.AND ? this.NewNode(focusedNode, cs) : this.NewNode(focusedNode.ParentNode, cs));
      this.SetCaptionForNode(treeListNode);
      this.UpdateImageIndex(treeListNode);
      this.IsModified = true;
    }
    finally
    {
      this.treeList1.EndUpdate();
    }
  }

  private TreeListNode NewNode(TreeListNode parentNode, ConditionStructure cs)
  {
    TreeListNode treeListNode = this.treeList1.AppendNode((object) null, parentNode);
    treeListNode.Tag = (object) new ConditionStructureNode(cs);
    if (treeListNode.ParentNode != null && !treeListNode.ParentNode.Expanded)
      treeListNode.ParentNode.Expanded = true;
    return treeListNode;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectionForm));
    this.panelBottom = new Panel();
    this.groupBox3 = new GroupBox();
    this.buttonApply = new Button();
    this.buttonCancel = new Button();
    this.contextMenuStripTree = new ContextMenuStrip(this.components);
    this.miAddFilter = new ToolStripMenuItem();
    this.miAdd = new ToolStripMenuItem();
    this.miAddChild = new ToolStripMenuItem();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.miInnerConditions = new ToolStripMenuItem();
    this.toolStripSeparator4 = new ToolStripSeparator();
    this.miChangeType = new ToolStripMenuItem();
    this.miEdit = new ToolStripMenuItem();
    this.miEnable = new ToolStripMenuItem();
    this.toolStripSeparator2 = new ToolStripSeparator();
    this.miDel = new ToolStripMenuItem();
    this.miClear = new ToolStripMenuItem();
    this.imageList1 = new ImageList(this.components);
    this.panel1 = new Panel();
    this.panel2 = new Panel();
    this.treeList1 = new TreeList();
    this.сolumnConditions = new TreeListColumn();
    this.columnValues = new TreeListColumn();
    this.riDateTime = new RepositoryItemDateEdit();
    this.riEditor = new RepositoryItemButtonEdit();
    this.riButton = new RepositoryItemButtonEdit();
    this.riBool = new RepositoryItemComboBox();
    this.riNumber = new RepositoryItemCalcEdit();
    this.riString = new RepositoryItemTextEdit();
    this.riReadOnly = new RepositoryItemButtonEdit();
    this.riFloat = new RepositoryItemCalcEdit();
    this.riPossibleValues = new RepositoryItemComboBox();
    this.imageList2 = new ImageList(this.components);
    this.toolTipController1 = new ToolTipController(this.components);
    this.toolStrip2 = new Intermech.Bars.ToolBar();
    this.bAddFilter = new ButtonItem();
    this.bAdd = new ButtonItem();
    this.bAddChild = new ButtonItem();
    this.bChangeType = new ButtonItem();
    this.bEdit = new ButtonItem();
    this.bEnable = new ButtonItem();
    this.bDel = new ButtonItem();
    this.bClear = new ButtonItem();
    this.bSearch = new ButtonItem();
    this.buttonHeightSet = new ButtonItem();
    this.panelBottom.SuspendLayout();
    this.contextMenuStripTree.SuspendLayout();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.treeList1.BeginInit();
    this.riDateTime.BeginInit();
    this.riEditor.BeginInit();
    this.riButton.BeginInit();
    this.riBool.BeginInit();
    this.riNumber.BeginInit();
    this.riString.BeginInit();
    this.riReadOnly.BeginInit();
    this.riFloat.BeginInit();
    this.riPossibleValues.BeginInit();
    this.SuspendLayout();
    this.panelBottom.Controls.Add((Control) this.groupBox3);
    this.panelBottom.Controls.Add((Control) this.buttonApply);
    this.panelBottom.Controls.Add((Control) this.buttonCancel);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.groupBox3, "groupBox3");
    this.groupBox3.Name = "groupBox3";
    this.groupBox3.TabStop = false;
    componentResourceManager.ApplyResources((object) this.buttonApply, "buttonApply");
    this.buttonApply.Name = "buttonApply";
    this.buttonApply.Click += new EventHandler(this.ButtonApply_Click);
    componentResourceManager.ApplyResources((object) this.buttonCancel, "buttonCancel");
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.Click += new EventHandler(this.ButtonCancel_Click);
    this.contextMenuStripTree.Items.AddRange(new ToolStripItem[12]
    {
      (ToolStripItem) this.miAddFilter,
      (ToolStripItem) this.miAdd,
      (ToolStripItem) this.miAddChild,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this.miInnerConditions,
      (ToolStripItem) this.toolStripSeparator4,
      (ToolStripItem) this.miChangeType,
      (ToolStripItem) this.miEdit,
      (ToolStripItem) this.miEnable,
      (ToolStripItem) this.toolStripSeparator2,
      (ToolStripItem) this.miDel,
      (ToolStripItem) this.miClear
    });
    this.contextMenuStripTree.Name = "contextMenuStripTree";
    componentResourceManager.ApplyResources((object) this.contextMenuStripTree, "contextMenuStripTree");
    this.miAddFilter.Name = "miAddFilter";
    componentResourceManager.ApplyResources((object) this.miAddFilter, "miAddFilter");
    this.miAddFilter.Click += new EventHandler(this.ToolTreeAddFilter_Click);
    this.miAdd.Name = "miAdd";
    componentResourceManager.ApplyResources((object) this.miAdd, "miAdd");
    this.miAdd.Click += new EventHandler(this.ToolTreeAdd_Click);
    this.miAddChild.Name = "miAddChild";
    componentResourceManager.ApplyResources((object) this.miAddChild, "miAddChild");
    this.miAddChild.Click += new EventHandler(this.ToolTreeAddChild_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator1, "toolStripSeparator1");
    this.miInnerConditions.Name = "miInnerConditions";
    componentResourceManager.ApplyResources((object) this.miInnerConditions, "miInnerConditions");
    this.miInnerConditions.Click += new EventHandler(this.BiInnerConditions_Click);
    this.toolStripSeparator4.Name = "toolStripSeparator4";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator4, "toolStripSeparator4");
    this.miChangeType.Name = "miChangeType";
    componentResourceManager.ApplyResources((object) this.miChangeType, "miChangeType");
    this.miChangeType.Click += new EventHandler(this.ToolStripBtnChangeType_Click);
    this.miEdit.Name = "miEdit";
    componentResourceManager.ApplyResources((object) this.miEdit, "miEdit");
    this.miEdit.Click += new EventHandler(this.ToolTreeEdit_Click);
    componentResourceManager.ApplyResources((object) this.miEnable, "miEnable");
    this.miEnable.Name = "miEnable";
    this.miEnable.Click += new EventHandler(this.ToolTreeEnable_Click);
    this.toolStripSeparator2.Name = "toolStripSeparator2";
    componentResourceManager.ApplyResources((object) this.toolStripSeparator2, "toolStripSeparator2");
    this.miDel.Name = "miDel";
    componentResourceManager.ApplyResources((object) this.miDel, "miDel");
    this.miDel.Click += new EventHandler(this.ToolTreeDel_Click);
    this.miClear.Name = "miClear";
    componentResourceManager.ApplyResources((object) this.miClear, "miClear");
    this.miClear.Click += new EventHandler(this.ToolTreeClear_Click);
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "or_and.ico");
    this.imageList1.Images.SetKeyName(1, "and.ico");
    this.imageList1.Images.SetKeyName(2, "clean.ico");
    this.imageList1.Images.SetKeyName(3, "del.ico");
    this.imageList1.Images.SetKeyName(4, "edit.ico");
    this.imageList1.Images.SetKeyName(5, "filter.ico");
    this.imageList1.Images.SetKeyName(6, "go.ico");
    this.imageList1.Images.SetKeyName(7, "off.ico");
    this.imageList1.Images.SetKeyName(8, "or.ico");
    this.imageList1.Images.SetKeyName(9, "отключить_фильтр.ico");
    this.imageList1.Images.SetKeyName(10, "отключить_и.ico");
    this.imageList1.Images.SetKeyName(11, "отключить_или.ico");
    this.panel1.Controls.Add((Control) this.panel2);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.panel2.Controls.Add((Control) this.treeList1);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    this.treeList1.Columns.AddRange(new TreeListColumn[2]
    {
      this.сolumnConditions,
      this.columnValues
    });
    this.treeList1.ContextMenuStrip = this.contextMenuStripTree;
    componentResourceManager.ApplyResources((object) this.treeList1, "treeList1");
    this.treeList1.Name = "treeList1";
    this.treeList1.RepositoryItems.AddRange(new RepositoryItem[9]
    {
      (RepositoryItem) this.riDateTime,
      (RepositoryItem) this.riEditor,
      (RepositoryItem) this.riButton,
      (RepositoryItem) this.riBool,
      (RepositoryItem) this.riNumber,
      (RepositoryItem) this.riString,
      (RepositoryItem) this.riReadOnly,
      (RepositoryItem) this.riFloat,
      (RepositoryItem) this.riPossibleValues
    });
    this.treeList1.StateImageList = this.imageList2;
    this.treeList1.Styles.AddReplace("Style1", (object) new ViewStyle("Style1", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Window, SystemColors.GrayText));
    this.treeList1.ToolTipController = this.toolTipController1;
    this.treeList1.GetCustomNodeCellEdit += new GetCustomNodeCellEditEventHandler(this.TreeList1_GetCustomNodeCellEdit);
    this.treeList1.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this.TreeList1_FocusedNodeChanged);
    this.treeList1.MouseDoubleClick += new MouseEventHandler(this.TreeList1_MouseDoubleClick);
    this.treeList1.MouseDown += new MouseEventHandler(this.TreeList1_MouseDown);
    componentResourceManager.ApplyResources((object) this.сolumnConditions, "сolumnConditions");
    this.сolumnConditions.Name = "сolumnConditions";
    componentResourceManager.ApplyResources((object) this.columnValues, "columnValues");
    this.columnValues.Name = "columnValues";
    this.riDateTime.AutoHeight = false;
    this.riDateTime.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this.riDateTime.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm:ss";
    this.riDateTime.DisplayFormat.FormatType = FormatType.DateTime;
    this.riDateTime.EditFormat.FormatString = "dd.MM.yyyy HH:mm:ss";
    this.riDateTime.EditFormat.FormatType = FormatType.DateTime;
    this.riDateTime.MaskData.EditMask = componentResourceManager.GetString("riDateTime.MaskData.EditMask");
    this.riDateTime.MaskData.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("riDateTime.MaskData.IgnoreMaskBlank");
    this.riDateTime.Name = "riDateTime";
    this.riDateTime.EditValueChanged += new EventHandler(this.RiDateTime_EditValueChanged);
    this.riEditor.AutoHeight = false;
    this.riEditor.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.riEditor.Name = "riEditor";
    this.riEditor.ReadOnly = true;
    this.riEditor.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.riEditor.ButtonClick += new ButtonPressedEventHandler(this.RiEditor_ButtonClick);
    this.riEditor.DoubleClick += new EventHandler(this.MouseDoubleClickOnCondition);
    this.riButton.AutoHeight = false;
    this.riButton.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.riButton.Name = "riButton";
    this.riButton.ReadOnly = true;
    this.riButton.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.riButton.ButtonClick += new ButtonPressedEventHandler(this.RiButton_ButtonClick);
    this.riButton.DoubleClick += new EventHandler(this.MouseDoubleClickOnCondition);
    this.riBool.AutoHeight = false;
    this.riBool.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this.riBool.Items.AddRange(new object[2]
    {
      (object) componentResourceManager.GetString("riBool.Items"),
      (object) componentResourceManager.GetString("riBool.Items1")
    });
    this.riBool.Name = "riBool";
    this.riBool.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.riBool.EditValueChanged += new EventHandler(this.RiBool_EditValueChanged);
    this.riNumber.AutoHeight = false;
    this.riNumber.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this.riNumber.Name = "riNumber";
    this.riNumber.EditValueChanged += new EventHandler(this.RiNumber_EditValueChanged);
    this.riString.AutoHeight = false;
    this.riString.Name = "riString";
    this.riString.EditValueChanged += new EventHandler(this.RiString_EditValueChanged);
    this.riReadOnly.AutoHeight = false;
    this.riReadOnly.Name = "riReadOnly";
    this.riReadOnly.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.riReadOnly.DoubleClick += new EventHandler(this.MouseDoubleClickOnCondition);
    this.riFloat.AutoHeight = false;
    this.riFloat.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this.riFloat.Name = "riFloat";
    this.riFloat.EditValueChanged += new EventHandler(this.RiFloat_EditValueChanged);
    this.riPossibleValues.AutoHeight = false;
    this.riPossibleValues.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this.riPossibleValues.Items.AddRange(new object[3]
    {
      (object) componentResourceManager.GetString("riPossibleValues.Items"),
      (object) componentResourceManager.GetString("riPossibleValues.Items1"),
      (object) componentResourceManager.GetString("riPossibleValues.Items2")
    });
    this.riPossibleValues.Name = "riPossibleValues";
    this.riPossibleValues.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.riPossibleValues.EditValueChanged += new EventHandler(this.RiPossibleValues_EditValueChanged);
    this.imageList2.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList2.ImageStream");
    this.imageList2.TransparentColor = Color.Transparent;
    this.imageList2.Images.SetKeyName(0, "add.ico");
    this.imageList2.Images.SetKeyName(1, "add-f.ico");
    this.imageList2.Images.SetKeyName(2, "add-d.ico");
    this.imageList2.Images.SetKeyName(3, "add-f-d.ico");
    this.imageList2.Images.SetKeyName(4, "or.ico");
    this.imageList2.Images.SetKeyName(5, "or-f.ico");
    this.imageList2.Images.SetKeyName(6, "or-d.ico");
    this.imageList2.Images.SetKeyName(7, "or-f-d.ico");
    this.imageList2.Images.SetKeyName(8, "f.ico");
    this.imageList2.Images.SetKeyName(9, "f-d.ico");
    this.toolTipController1.Style = new ViewStyle("ToolTip style");
    this.toolTipController1.GetActiveObjectInfo += new ToolTipControllerGetActiveObjectInfoEventHandler(this.ToolTipController1_GetActiveObjectInfo);
    this.toolStrip2.FullMenus = true;
    this.toolStrip2.Guid = new Guid("2337b74f-5d86-4565-809f-c0fa244e17e8");
    this.toolStrip2.Hidden = false;
    this.toolStrip2.ImageList = this.imageList1;
    this.toolStrip2.Items.AddRange(new ToolbarItemBase[10]
    {
      (ToolbarItemBase) this.bAddFilter,
      (ToolbarItemBase) this.bAdd,
      (ToolbarItemBase) this.bAddChild,
      (ToolbarItemBase) this.bChangeType,
      (ToolbarItemBase) this.bEdit,
      (ToolbarItemBase) this.bEnable,
      (ToolbarItemBase) this.bDel,
      (ToolbarItemBase) this.bClear,
      (ToolbarItemBase) this.bSearch,
      (ToolbarItemBase) this.buttonHeightSet
    });
    componentResourceManager.ApplyResources((object) this.toolStrip2, "toolStrip2");
    this.toolStrip2.Name = "toolStrip2";
    componentResourceManager.ApplyResources((object) this.bAddFilter, "bAddFilter");
    this.bAddFilter.ImageIndex = 5;
    this.bAddFilter.Click += new EventHandler(this.ToolTreeAddFilter_Click);
    componentResourceManager.ApplyResources((object) this.bAdd, "bAdd");
    this.bAdd.ImageIndex = 1;
    this.bAdd.Click += new EventHandler(this.ToolTreeAdd_Click);
    componentResourceManager.ApplyResources((object) this.bAddChild, "bAddChild");
    this.bAddChild.ImageIndex = 8;
    this.bAddChild.Click += new EventHandler(this.ToolTreeAddChild_Click);
    this.bChangeType.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.bChangeType, "bChangeType");
    this.bChangeType.ImageIndex = 0;
    this.bChangeType.Click += new EventHandler(this.ToolStripBtnChangeType_Click);
    componentResourceManager.ApplyResources((object) this.bEdit, "bEdit");
    this.bEdit.ImageIndex = 4;
    this.bEdit.Click += new EventHandler(this.ToolTreeEdit_Click);
    componentResourceManager.ApplyResources((object) this.bEnable, "bEnable");
    this.bEnable.ImageIndex = 7;
    this.bEnable.Click += new EventHandler(this.ToolTreeEnable_Click);
    this.bDel.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.bDel, "bDel");
    this.bDel.ImageIndex = 3;
    this.bDel.Click += new EventHandler(this.ToolTreeDel_Click);
    componentResourceManager.ApplyResources((object) this.bClear, "bClear");
    this.bClear.ImageIndex = 2;
    this.bClear.Click += new EventHandler(this.ToolTreeClear_Click);
    this.bSearch.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.bSearch, "bSearch");
    this.bSearch.ImageIndex = 6;
    this.bSearch.ShowText = true;
    this.bSearch.Visible = false;
    this.bSearch.Click += new EventHandler(this.ToolStripBtnGO_Click);
    componentResourceManager.ApplyResources((object) this.buttonHeightSet, "buttonHeightSet");
    this.buttonHeightSet.Enabled = false;
    this.buttonHeightSet.IconSize = new Size(1, 37);
    this.buttonHeightSet.Image = (Image) Intermech.Client.Core.Properties.Resources.pixel;
    this.buttonHeightSet.Padding.Bottom = 0;
    this.buttonHeightSet.Padding.Left = 0;
    this.buttonHeightSet.Padding.Right = 0;
    this.buttonHeightSet.Padding.Top = 0;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.toolStrip2);
    this.Controls.Add((Control) this.panelBottom);
    this.Name = nameof (SelectionForm);
    this.ShowInTaskbar = false;
    this.FormClosing += new FormClosingEventHandler(this.SelectionForm_FormClosing);
    this.Shown += new EventHandler(this.SelectionForm_Shown);
    this.panelBottom.ResumeLayout(false);
    this.contextMenuStripTree.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.treeList1.EndInit();
    this.riDateTime.EndInit();
    this.riEditor.EndInit();
    this.riButton.EndInit();
    this.riBool.EndInit();
    this.riNumber.EndInit();
    this.riString.EndInit();
    this.riReadOnly.EndInit();
    this.riFloat.EndInit();
    this.riPossibleValues.EndInit();
    this.ResumeLayout(false);
  }

  private class PossibleValueItem
  {
    /// <summary>Значение</summary>
    public object Value;
    /// <summary>Отображаемое значение</summary>
    public string Description;

    public PossibleValueItem(object value, string description)
    {
      this.Value = value;
      this.Description = description;
    }

    public override string ToString()
    {
      return !(this.Description != string.Empty) ? Convert.ToString(this.Value) : this.Description;
    }

    public override bool Equals(object obj) => this.Value.Equals(obj);

    public override int GetHashCode() => this.Value.GetHashCode();
  }
}
