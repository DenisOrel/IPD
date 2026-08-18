
// Type: Intermech.PropertyEditors.AdvSelectorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.LookAndFeel;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>
/// Форма для выбора типа объекта и типа атрибута
/// либо типа атрибута (без типа объекта)
/// </summary>
public class AdvSelectorForm : Form
{
  private Panel panButtons;
  private Panel panBack;
  private Panel panObj;
  private Panel panAttr;
  private StatusBar statusBar1;
  private Button bOk;
  private Button bCancel;
  private Label label1;
  private Panel panListBox;
  private Label label2;
  private TreeView tvObj;
  private IContainer components;
  private const int NoSelect = -1;
  private static readonly string ClearSearch = LocalizationHolder.rm.GetString("Client.Core_1033");
  private AdvSelector _selectorType = AdvSelector.AttributableType;
  private ElementTypeInfo _rootID;
  private ElementTypeInfo _selectID;
  private ArrayList _selectAttrIDs = new ArrayList();
  private Splitter splitter1;
  private Button bAll;
  private Panel panTreeView;
  private Panel panTextBox1;
  private Panel panTextBox2;
  private imComboBoxEdit tbObj;
  private imComboBoxEdit tbAttr;
  private SimpleButton bFilterObj;
  private SimpleButton bCancelFilterObj;
  private SimpleButton bFilterAttr;
  private SimpleButton bCancelFilterAttr;
  private ToolTip toolTip1;
  private CheckBox cbAllAttrs;
  private CheckBox cbAttrByShortName;
  private CheckBox cbObjByShortName;
  private string _filterAttr = string.Empty;
  private ListView lvAttr;
  private ColumnHeader columnHeader1;
  private ISelectorFilter _filter;

  /// <summary>
  /// Стандартный конструктор
  /// создание диалога выбора типа объекта
  /// </summary>
  protected AdvSelectorForm()
  {
    this.InitializeComponent();
    INamedImageList service = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    this.bFilterObj.Width = this.bFilterObj.Height;
    this.bFilterObj.Text = string.Empty;
    this.bFilterObj.ImageList = service.ImageList;
    this.bFilterObj.ImageIndex = service.ImageIndex("imgGotoAddress");
    this.bCancelFilterObj.Width = this.bCancelFilterObj.Height;
    this.bCancelFilterObj.Text = string.Empty;
    this.bCancelFilterObj.ImageList = service.ImageList;
    this.bCancelFilterObj.ImageIndex = service.ImageIndex("imgDelete");
    this.bFilterAttr.Width = this.bFilterAttr.Height;
    this.bFilterAttr.Text = string.Empty;
    this.bFilterAttr.ImageList = service.ImageList;
    this.bFilterAttr.ImageIndex = service.ImageIndex("imgGotoAddress");
    this.bCancelFilterAttr.Width = this.bCancelFilterAttr.Height;
    this.bCancelFilterAttr.Text = string.Empty;
    this.bCancelFilterAttr.ImageList = service.ImageList;
    this.bCancelFilterAttr.ImageIndex = service.ImageIndex("imgDelete");
    this.tbObj.AttachControl((Control) this.tvObj);
    this.tbAttr.AttachControl((Control) this.lvAttr);
    this.lvAttr.Sorting = SortOrder.Ascending;
    this.panAttr.Visible = false;
    this.bOk.Enabled = false;
    this.statusBar1.Text = LocalizationHolder.rm.GetString("Client.Core_1034");
  }

  /// <summary>
  /// Создание диалога выбора специфического типа
  /// с значениями "по умолчанию"
  /// </summary>
  /// <param name="selectType">Тип выбираемых объектов</param>
  /// <param name="kind">Вид выбираемых объектов (только для selectType=AttributeType)</param>
  public AdvSelectorForm(AdvSelector selectType, AttributableElements kind)
    : this()
  {
    this._selectorType = selectType;
    this._rootID = new ElementTypeInfo(-1, kind);
  }

  /// <summary>
  /// Создание диалога выбора специфического типа
  /// для типов с корнем типа rootID
  /// </summary>
  /// <param name="selectType">Тип выбираемых объектов</param>
  /// <param name="rootID">Идентификатор корня дерева типов объектов</param>
  /// <param name="kind">Вид выбираемых объектов (только для selectType=AttributeType)</param>
  public AdvSelectorForm(AdvSelector selectType, AttributableElements kind, int rootID)
    : this(selectType, kind)
  {
    switch (selectType)
    {
      case AdvSelector.AttributeType:
        this._rootID = new ElementTypeInfo(rootID, kind);
        break;
      case AdvSelector.AttributableType:
      case AdvSelector.AttributableTypeWithAttributeType:
        this._rootID = new ElementTypeInfo(rootID, kind);
        break;
    }
    this._selectID = this._rootID;
  }

  /// <summary>
  /// Создание диалога выбора специфического типа
  /// для типов объеков с корнем типа объекта rootID
  /// с установкой выделения на тип (объекта/атрибута) selectID
  /// </summary>
  /// <param name="selectType">Тип выбираемых объектов</param>
  /// <param name="rootID">Идентификатор корня дерева типов объектов</param>
  /// <param name="kind">Вид выбираемых объектов (только для selectType=AttributeType)</param>
  /// <param name="selectID">Выделенный тип (объекта/атрибута)</param>
  public AdvSelectorForm(
    AdvSelector selectType,
    AttributableElements kind,
    int rootID,
    int selectID)
    : this(selectType, kind, rootID)
  {
    switch (selectType)
    {
      case AdvSelector.AttributeType:
        this._selectAttrIDs.Add((object) selectID);
        break;
      case AdvSelector.AttributableType:
      case AdvSelector.AttributableTypeWithAttributeType:
        this._selectID = new ElementTypeInfo(selectID, kind);
        break;
    }
  }

  /// <summary>
  /// Создание диалога выбора пары тип объекта - типы атрибутов
  /// для типов объеков с корнем типа объекта rootID
  /// с установкой выделения на тип объекта selectObjID
  /// и установкой выделения на типы атрибутов selectAttrIDs.
  /// Доступно мультивыделение типов атрибутов.
  /// </summary>
  /// <param name="kind">Вид типа (объект/связь)</param>
  /// <param name="rootID">Идентификатор корня дерева типов объектов</param>
  /// <param name="selectID">Выделенный тип объекта</param>
  /// <param name="selectAttrIDs">Выделенные типы атрибутов</param>
  public AdvSelectorForm(AttributableElements kind, int rootID, int selectID, int[] selectAttrIDs)
    : this(AdvSelector.AttributableTypeWithAttributeType, kind, rootID, selectID)
  {
    if (selectAttrIDs == null)
      return;
    this.AttributeTypesMultiselect = selectAttrIDs.Length > 1;
    foreach (int selectAttrId in selectAttrIDs)
    {
      if (!selectAttrId.Equals(-1))
        this._selectAttrIDs.Add((object) selectAttrId);
    }
  }

  /// <summary>
  /// Выбранный тип объекта, -1 если выбран любой тип объекта
  /// </summary>
  public int ObjectType => this._selectID.TypeID;

  /// <summary>Выбранный тип связи, -1 если выбран любой тип объекта</summary>
  public int RelationType => this._selectID.TypeID;

  /// <summary>Вид выбранного элемента</summary>
  public AttributableElements Kind => this._selectID.Kind;

  /// <summary>
  /// Способ выбора типов атрибутов
  /// "по умолчанию" false
  /// </summary>
  public bool AttributeTypesMultiselect
  {
    get => this.lvAttr.MultiSelect;
    set => this.lvAttr.MultiSelect = value;
  }

  /// <summary>Выбранные типы атрибутов</summary>
  public int[] AttributeTypes => this._selectAttrIDs.ToArray(typeof (int)) as int[];

  /// <summary>Внешний фильтр для формы</summary>
  public ISelectorFilter SelectorFilter
  {
    get => this._filter;
    set => this._filter = value;
  }

  private TreeNode LoadObjectTypes(int rootID, ref TreeNode selectedNode)
  {
    IMSObjectType objectType1 = rootID != -1 ? MetaDataHelper.GetObjectType(rootID) : (IMSObjectType) null;
    TreeNode treeNode;
    if (objectType1 == null)
    {
      treeNode = new TreeNode(LocalizationHolder.rm.GetString("Client.Core_1035"));
      treeNode.Tag = (object) new ElementTypeInfo(-1, AttributableElements.Object);
      foreach (int topObjectTypesId in MetaDataHelper.GetTopObjectTypesIDs())
      {
        IMSObjectType objectType2 = MetaDataHelper.GetObjectType(topObjectTypesId);
        if (objectType2 != null)
        {
          TreeNode node = this.LoadObjectType(objectType2, ref selectedNode);
          if (node != null)
            treeNode.Nodes.Add(node);
        }
      }
    }
    else
    {
      treeNode = this.LoadObjectType(objectType1, ref selectedNode);
      treeNode.Text = LocalizationHolder.rm.GetString("Client.Core_1035");
      treeNode.Tag = (object) new ElementTypeInfo(-1, AttributableElements.Object);
    }
    treeNode.Expand();
    return treeNode;
  }

  private TreeNode LoadObjectType(IMSObjectType imsObjType, ref TreeNode selectedNode)
  {
    if (imsObjType == null)
      throw new ArgumentNullException(nameof (imsObjType));
    if (!this.Filter(4, (object) imsObjType.ObjectTypeID))
      return (TreeNode) null;
    string text = imsObjType.ObjectTypeName;
    if (this.cbObjByShortName.Checked && !imsObjType.ShortName.Equals(string.Empty))
      text = imsObjType.ShortName;
    TreeNode treeNode = new TreeNode(text);
    treeNode.ImageIndex = treeNode.SelectedImageIndex = Statics.IconSrv != null ? Statics.IconSrv.IndexOf(4, imsObjType.ObjectTypeID) : 0;
    treeNode.Tag = (object) new ElementTypeInfo(imsObjType.ObjectTypeID, AttributableElements.Object);
    if (this._selectID != null)
    {
      int num = this._selectID.TypeID;
      if (!num.Equals(-1))
      {
        num = imsObjType.ObjectTypeID;
        if (num.Equals(this._selectID.TypeID))
          selectedNode = treeNode;
      }
    }
    IEnumerable<int> objectTypeChildrenId = (IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenID(imsObjType.ObjectTypeID);
    if (objectTypeChildrenId != null)
    {
      foreach (int objTypeID in objectTypeChildrenId)
      {
        IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeID);
        if (objectType != null)
        {
          TreeNode node = this.LoadObjectType(objectType, ref selectedNode);
          if (node != null)
            treeNode.Nodes.Add(node);
        }
      }
    }
    return treeNode;
  }

  private DataRow FindRow(int ID, DataTable data, string columnName)
  {
    string filterExpression = $"{columnName}={ID}";
    DataRow[] dataRowArray = data.Select(filterExpression);
    switch (dataRowArray.Length)
    {
      case 0:
        return (DataRow) null;
      case 1:
        return dataRowArray[0];
      default:
        throw new ArgumentException(LocalizationHolder.rm.GetString("Client.Core_1036") + filterExpression);
    }
  }

  private void PopulateObjectTypes(int rootID)
  {
    this.PopulateObjectTypes(rootID, (TreeNode) null, (TreeNode) null);
  }

  private void PopulateObjectTypes(int rootID, TreeNode root, TreeNode select)
  {
    TreeNode selectedNode = select;
    if (root == null)
      root = this.LoadObjectTypes(rootID, ref selectedNode);
    this.tvObj.BeginUpdate();
    try
    {
      this.tvObj.Nodes.Clear();
      this.tvObj.ImageList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
      if (root != null)
      {
        this.tvObj.Nodes.Add(root);
        this.tvObj.SelectedNode = selectedNode == null ? root : selectedNode;
      }
    }
    finally
    {
      this.tvObj.EndUpdate();
    }
    if (this._selectorType != AdvSelector.AttributableType)
      return;
    this.bOk.Enabled = this.tvObj.SelectedNode != null && this.tvObj.SelectedNode.Tag is ElementTypeInfo && !(this.tvObj.SelectedNode.Tag as ElementTypeInfo).TypeID.Equals(-1);
  }

  private ICollection LoadAttributeTypes(ElementTypeInfo info)
  {
    ArrayList arrayList = new ArrayList();
    if (info == null || info.TypeID.Equals(-1) || this.cbAllAttrs.Checked)
    {
      List<IMSAttributeType> imsAttributeTypeList;
      if (this.cbAllAttrs.Checked)
      {
        imsAttributeTypeList = MetaDataHelper.GetAttributeTypesList();
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          DataTable source = sessionKeeper.Session.GetAttributeTypeCollection(-1, true).Select(string.Empty);
          if (source == null)
            return (ICollection) arrayList;
          int idxColAttrId = source.Columns.IndexOf("F_ATTRIBUTE_ID");
          imsAttributeTypeList = source.AsEnumerable().Select<DataRow, IMSAttributeType>((System.Func<DataRow, IMSAttributeType>) (row => MetaDataHelper.GetAttributeType(Convert.ToInt32(row[idxColAttrId])))).ToList<IMSAttributeType>();
        }
      }
      foreach (IMSAttributeType imsAttributeType in imsAttributeTypeList)
      {
        if (this.Filter(3, (object) imsAttributeType.AttributeID))
        {
          AdvSelectorForm.ID2String4AT id2String4At = this.LoadAttributeType(imsAttributeType);
          ListViewItem listViewItem = new ListViewItem(id2String4At.ToString());
          listViewItem.ImageIndex = Statics.IconSrv.IndexOf(3, -1, (object) id2String4At.FieldType);
          listViewItem.Tag = (object) id2String4At;
          if (this._filterAttr.Equals(string.Empty))
            arrayList.Add((object) listViewItem);
          else if (id2String4At.ToString().ToUpper().IndexOf(this._filterAttr) >= 0)
            arrayList.Add((object) listViewItem);
        }
      }
    }
    else
    {
      IEnumerable<IMSAttribute4> imsAttribute4s;
      switch (info.Kind)
      {
        case AttributableElements.Object:
          imsAttribute4s = (IEnumerable<IMSAttribute4>) MetaDataHelper.GetAttribute4ObjectTypeList(info.TypeID);
          break;
        case AttributableElements.Relation:
          imsAttribute4s = (IEnumerable<IMSAttribute4>) MetaDataHelper.GetAttribute4RelationTypeList(info.TypeID);
          break;
        default:
          return (ICollection) arrayList;
      }
      if (imsAttribute4s == null)
        return (ICollection) arrayList;
      foreach (IMSAttribute4 imsAttribute4 in imsAttribute4s)
      {
        if (this.Filter(3, (object) imsAttribute4.AttributeID))
        {
          AdvSelectorForm.ID2String4AT id2String4At = this.LoadAttributeType(imsAttribute4.AttributeID);
          ListViewItem listViewItem = new ListViewItem(id2String4At.ToString());
          listViewItem.ImageIndex = Statics.IconSrv.IndexOf(3, -1, (object) id2String4At.FieldType);
          listViewItem.Tag = (object) id2String4At;
          if (this._filterAttr.Equals(string.Empty))
            arrayList.Add((object) listViewItem);
          else if (id2String4At.ToString().ToUpper().IndexOf(this._filterAttr) >= 0)
            arrayList.Add((object) listViewItem);
        }
      }
    }
    return (ICollection) arrayList;
  }

  private AdvSelectorForm.ID2String4AT LoadAttributeType(int attributeTypeId)
  {
    return this.LoadAttributeType(MetaDataHelper.GetAttributeType(attributeTypeId));
  }

  private AdvSelectorForm.ID2String4AT LoadAttributeType(IMSAttributeType imsAttributeType)
  {
    return imsAttributeType == null ? (AdvSelectorForm.ID2String4AT) null : new AdvSelectorForm.ID2String4AT(imsAttributeType.AttributeID, imsAttributeType.Name, imsAttributeType.ShortName, imsAttributeType.FieldType, this.cbAttrByShortName.Checked);
  }

  private void PopulateAttributeTypes(ElementTypeInfo info)
  {
    ArrayList arrayList = new ArrayList(this.LoadAttributeTypes(info));
    this.lvAttr.BeginUpdate();
    try
    {
      this.lvAttr.Items.Clear();
      ListViewItem[] items = new ListViewItem[arrayList.Count];
      arrayList.CopyTo((Array) items);
      this.lvAttr.Items.AddRange(items);
      this.lvAttr.SmallImageList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
      if (this._selectAttrIDs.Count > 0)
      {
        foreach (int attributeTypeId in new ArrayList((ICollection) this._selectAttrIDs))
        {
          int index = -1;
          AdvSelectorForm.ID2String4AT idItem = this.LoadAttributeType(attributeTypeId);
          if (idItem != null)
            index = this.IndexOf(idItem);
          if (index >= 0)
          {
            this.lvAttr.Items[index].Selected = true;
            if (this.lvAttr.SelectedItems.Count == 1)
              this.tbAttr.Text = idItem != null ? idItem.ToString() : string.Empty;
          }
          else
            this._selectAttrIDs.Remove((object) attributeTypeId);
        }
        if (this._selectAttrIDs.Count == 0)
          this.tbAttr.Text = string.Empty;
      }
      this.lvAttr.Columns[0].Width = this.lvAttr.Size.Width - SystemInformation.VerticalScrollBarWidth - 4;
    }
    finally
    {
      this.lvAttr.EndUpdate();
    }
    this.bOk.Enabled = this._selectAttrIDs.Count > 0;
    this.bAll.Enabled = info != null && !info.TypeID.Equals(-1);
  }

  private int IndexOf(AdvSelectorForm.ID2String4AT idItem)
  {
    int num = 0;
    bool flag = false;
    foreach (ListViewItem listViewItem in this.lvAttr.Items)
    {
      if (object.Equals((object) (listViewItem.Tag as AdvSelectorForm.ID2String4AT), (object) idItem))
      {
        flag = true;
        break;
      }
      ++num;
    }
    return !flag ? -1 : num;
  }

  private TreeNode LoadRelationTypes(int rootRelTypeID, ref TreeNode selectedNode)
  {
    IMSRelationType relationType = rootRelTypeID != -1 ? MetaDataHelper.GetRelationType(rootRelTypeID) : (IMSRelationType) null;
    TreeNode treeNode;
    if (relationType == null)
    {
      treeNode = new TreeNode(LocalizationHolder.rm.GetString("Client.Core_1037"));
      treeNode.Tag = (object) new ElementTypeInfo(-1, AttributableElements.Relation);
      foreach (IMSRelationType relationTypes in MetaDataHelper.GetRelationTypesList())
      {
        TreeNode node = this.LoadRelationType(relationTypes, ref selectedNode);
        if (node != null)
          treeNode.Nodes.Add(node);
      }
    }
    else
    {
      treeNode = this.LoadRelationType(relationType, ref selectedNode);
      treeNode.Text = LocalizationHolder.rm.GetString("Client.Core_1037");
      treeNode.Tag = (object) new ElementTypeInfo(-1, AttributableElements.Relation);
    }
    treeNode.Expand();
    return treeNode;
  }

  private TreeNode LoadRelationType(IMSRelationType imsRelType, ref TreeNode selectedNode)
  {
    if (imsRelType == null)
      throw new ArgumentNullException(nameof (imsRelType));
    if (!this.Filter(6, (object) imsRelType.RelationTypeID))
      return (TreeNode) null;
    TreeNode treeNode = new TreeNode(imsRelType.Description);
    treeNode.SelectedImageIndex = treeNode.ImageIndex = Statics.IconSrv != null ? Statics.IconSrv.IndexOf(6, imsRelType.RelationTypeID) : 0;
    treeNode.Tag = (object) new ElementTypeInfo(imsRelType.RelationTypeID, AttributableElements.Relation);
    if (this._selectID != null)
    {
      int typeId = this._selectID.TypeID;
      if (!typeId.Equals(-1))
      {
        typeId = this._selectID.TypeID;
        if (typeId.Equals(imsRelType.RelationTypeID))
          selectedNode = treeNode;
      }
    }
    return treeNode;
  }

  private void PopulateRelationTypes(int rootID)
  {
    this.PopulateRelationTypes(rootID, (TreeNode) null, (TreeNode) null);
  }

  private void PopulateRelationTypes(int rootID, TreeNode root, TreeNode select)
  {
    TreeNode selectedNode = select;
    if (root == null)
      root = this.LoadRelationTypes(rootID, ref selectedNode);
    this.tvObj.BeginUpdate();
    try
    {
      this.tvObj.Nodes.Clear();
      this.tvObj.ImageList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
      if (root != null)
      {
        this.tvObj.Nodes.Add(root);
        this.tvObj.SelectedNode = selectedNode == null ? root : selectedNode;
      }
    }
    finally
    {
      this.tvObj.EndUpdate();
    }
    if (this._selectorType != AdvSelector.AttributableType)
      return;
    this.bOk.Enabled = this.tvObj.SelectedNode != null && this.tvObj.SelectedNode.Tag is ElementTypeInfo && !(this.tvObj.SelectedNode.Tag as ElementTypeInfo).TypeID.Equals(-1);
  }

  private bool Filter(int category, object id)
  {
    return this._filter == null || this._filter.IsInFilter(category, id);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="disposing"></param>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AdvSelectorForm));
    this.panButtons = new Panel();
    this.bCancel = new Button();
    this.bOk = new Button();
    this.panBack = new Panel();
    this.splitter1 = new Splitter();
    this.panAttr = new Panel();
    this.panListBox = new Panel();
    this.lvAttr = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.panTextBox2 = new Panel();
    this.tbAttr = new imComboBoxEdit();
    this.bFilterAttr = new SimpleButton();
    this.bCancelFilterAttr = new SimpleButton();
    this.bAll = new Button();
    this.cbAllAttrs = new CheckBox();
    this.cbAttrByShortName = new CheckBox();
    this.label1 = new Label();
    this.panObj = new Panel();
    this.panTreeView = new Panel();
    this.tvObj = new TreeView();
    this.panTextBox1 = new Panel();
    this.tbObj = new imComboBoxEdit();
    this.bFilterObj = new SimpleButton();
    this.bCancelFilterObj = new SimpleButton();
    this.cbObjByShortName = new CheckBox();
    this.label2 = new Label();
    this.statusBar1 = new StatusBar();
    this.toolTip1 = new ToolTip(this.components);
    this.panButtons.SuspendLayout();
    this.panBack.SuspendLayout();
    this.panAttr.SuspendLayout();
    this.panListBox.SuspendLayout();
    this.panTextBox2.SuspendLayout();
    this.tbAttr.Properties.BeginInit();
    this.panObj.SuspendLayout();
    this.panTreeView.SuspendLayout();
    this.panTextBox1.SuspendLayout();
    this.tbObj.Properties.BeginInit();
    this.SuspendLayout();
    this.panButtons.Controls.Add((Control) this.bCancel);
    this.panButtons.Controls.Add((Control) this.bOk);
    componentResourceManager.ApplyResources((object) this.panButtons, "panButtons");
    this.panButtons.Name = "panButtons";
    componentResourceManager.ApplyResources((object) this.bCancel, "bCancel");
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Name = "bCancel";
    componentResourceManager.ApplyResources((object) this.bOk, "bOk");
    this.bOk.DialogResult = DialogResult.OK;
    this.bOk.Name = "bOk";
    this.panBack.Controls.Add((Control) this.splitter1);
    this.panBack.Controls.Add((Control) this.panAttr);
    this.panBack.Controls.Add((Control) this.panObj);
    componentResourceManager.ApplyResources((object) this.panBack, "panBack");
    this.panBack.Name = "panBack";
    componentResourceManager.ApplyResources((object) this.splitter1, "splitter1");
    this.splitter1.Name = "splitter1";
    this.splitter1.TabStop = false;
    this.panAttr.Controls.Add((Control) this.panListBox);
    this.panAttr.Controls.Add((Control) this.panTextBox2);
    this.panAttr.Controls.Add((Control) this.cbAllAttrs);
    this.panAttr.Controls.Add((Control) this.cbAttrByShortName);
    this.panAttr.Controls.Add((Control) this.label1);
    componentResourceManager.ApplyResources((object) this.panAttr, "panAttr");
    this.panAttr.MinimumSize = new Size(100, 160 /*0xA0*/);
    this.panAttr.Name = "panAttr";
    this.panListBox.Controls.Add((Control) this.lvAttr);
    componentResourceManager.ApplyResources((object) this.panListBox, "panListBox");
    this.panListBox.Name = "panListBox";
    this.lvAttr.Columns.AddRange(new ColumnHeader[1]
    {
      this.columnHeader1
    });
    componentResourceManager.ApplyResources((object) this.lvAttr, "lvAttr");
    this.lvAttr.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.lvAttr.HideSelection = false;
    this.lvAttr.MultiSelect = false;
    this.lvAttr.Name = "lvAttr";
    this.lvAttr.UseCompatibleStateImageBehavior = false;
    this.lvAttr.View = View.Details;
    this.lvAttr.SelectedIndexChanged += new EventHandler(this.lvAttr_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.columnHeader1, "columnHeader1");
    this.panTextBox2.Controls.Add((Control) this.tbAttr);
    this.panTextBox2.Controls.Add((Control) this.bFilterAttr);
    this.panTextBox2.Controls.Add((Control) this.bCancelFilterAttr);
    this.panTextBox2.Controls.Add((Control) this.bAll);
    componentResourceManager.ApplyResources((object) this.panTextBox2, "panTextBox2");
    this.panTextBox2.Name = "panTextBox2";
    componentResourceManager.ApplyResources((object) this.tbAttr, "tbAttr");
    this.tbAttr.Name = "tbAttr";
    this.tbAttr.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this.tbAttr.Properties.MaxLength = 450;
    this.tbAttr.ToolTip = "Текущий фильтр: нет";
    this.toolTip1.SetToolTip((Control) this.tbAttr, componentResourceManager.GetString("tbAttr.ToolTip"));
    this.tbAttr.TextChanged += new EventHandler(this.tbAttr_TextChanged);
    componentResourceManager.ApplyResources((object) this.bFilterAttr, "bFilterAttr");
    this.bFilterAttr.Name = "bFilterAttr";
    this.bFilterAttr.ToolTip = "Применить фильтр";
    this.toolTip1.SetToolTip((Control) this.bFilterAttr, componentResourceManager.GetString("bFilterAttr.ToolTip"));
    this.bFilterAttr.Click += new EventHandler(this.bFilterAttr_Click);
    componentResourceManager.ApplyResources((object) this.bCancelFilterAttr, "bCancelFilterAttr");
    this.bCancelFilterAttr.Name = "bCancelFilterAttr";
    this.bCancelFilterAttr.ToolTip = "Снять фильтр";
    this.toolTip1.SetToolTip((Control) this.bCancelFilterAttr, componentResourceManager.GetString("bCancelFilterAttr.ToolTip"));
    this.bCancelFilterAttr.Click += new EventHandler(this.bCancelFilterAttr_Click);
    componentResourceManager.ApplyResources((object) this.bAll, "bAll");
    this.bAll.Name = "bAll";
    this.toolTip1.SetToolTip((Control) this.bAll, componentResourceManager.GetString("bAll.ToolTip"));
    this.bAll.Click += new EventHandler(this.bAll_Click);
    componentResourceManager.ApplyResources((object) this.cbAllAttrs, "cbAllAttrs");
    this.cbAllAttrs.Name = "cbAllAttrs";
    this.cbAllAttrs.CheckedChanged += new EventHandler(this.cbAllAttrs_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbAttrByShortName, "cbAttrByShortName");
    this.cbAttrByShortName.Name = "cbAttrByShortName";
    this.toolTip1.SetToolTip((Control) this.cbAttrByShortName, componentResourceManager.GetString("cbAttrByShortName.ToolTip"));
    this.cbAttrByShortName.CheckedChanged += new EventHandler(this.cbAttrByShortName_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.panObj.Controls.Add((Control) this.panTreeView);
    this.panObj.Controls.Add((Control) this.panTextBox1);
    this.panObj.Controls.Add((Control) this.cbObjByShortName);
    this.panObj.Controls.Add((Control) this.label2);
    componentResourceManager.ApplyResources((object) this.panObj, "panObj");
    this.panObj.MinimumSize = new Size(0, 200);
    this.panObj.Name = "panObj";
    this.panTreeView.Controls.Add((Control) this.tvObj);
    componentResourceManager.ApplyResources((object) this.panTreeView, "panTreeView");
    this.panTreeView.Name = "panTreeView";
    componentResourceManager.ApplyResources((object) this.tvObj, "tvObj");
    this.tvObj.FullRowSelect = true;
    this.tvObj.HideSelection = false;
    this.tvObj.Name = "tvObj";
    this.tvObj.Sorted = true;
    this.toolTip1.SetToolTip((Control) this.tvObj, componentResourceManager.GetString("tvObj.ToolTip"));
    this.tvObj.AfterSelect += new TreeViewEventHandler(this.tvObj_AfterSelect);
    this.panTextBox1.Controls.Add((Control) this.tbObj);
    this.panTextBox1.Controls.Add((Control) this.bFilterObj);
    this.panTextBox1.Controls.Add((Control) this.bCancelFilterObj);
    componentResourceManager.ApplyResources((object) this.panTextBox1, "panTextBox1");
    this.panTextBox1.Name = "panTextBox1";
    componentResourceManager.ApplyResources((object) this.tbObj, "tbObj");
    this.tbObj.Name = "tbObj";
    this.tbObj.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this.tbObj.Properties.DropDownRows = 8;
    this.tbObj.Properties.LookAndFeel.Style = LookAndFeelStyle.Office2003;
    this.tbObj.Properties.ShowPopupShadow = false;
    this.tbObj.ToolTip = "Текущий фильтр: нет";
    this.toolTip1.SetToolTip((Control) this.tbObj, componentResourceManager.GetString("tbObj.ToolTip"));
    this.tbObj.TextChanged += new EventHandler(this.tbObj_TextChanged);
    componentResourceManager.ApplyResources((object) this.bFilterObj, "bFilterObj");
    this.bFilterObj.Name = "bFilterObj";
    this.bFilterObj.ToolTip = "Применить фильтр";
    this.toolTip1.SetToolTip((Control) this.bFilterObj, componentResourceManager.GetString("bFilterObj.ToolTip"));
    this.bFilterObj.Click += new EventHandler(this.bFilterObj_Click);
    componentResourceManager.ApplyResources((object) this.bCancelFilterObj, "bCancelFilterObj");
    this.bCancelFilterObj.Name = "bCancelFilterObj";
    this.bCancelFilterObj.ToolTip = "Снять фильтр";
    this.toolTip1.SetToolTip((Control) this.bCancelFilterObj, componentResourceManager.GetString("bCancelFilterObj.ToolTip"));
    this.bCancelFilterObj.Click += new EventHandler(this.bCancelFilterObj_Click);
    componentResourceManager.ApplyResources((object) this.cbObjByShortName, "cbObjByShortName");
    this.cbObjByShortName.Name = "cbObjByShortName";
    this.cbObjByShortName.CheckedChanged += new EventHandler(this.cbObjByShortName_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.statusBar1, "statusBar1");
    this.statusBar1.Name = "statusBar1";
    this.AcceptButton = (IButtonControl) this.bOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.bCancel;
    this.Controls.Add((Control) this.panBack);
    this.Controls.Add((Control) this.panButtons);
    this.Controls.Add((Control) this.statusBar1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AdvSelectorForm);
    this.ShowInTaskbar = false;
    this.Closed += new EventHandler(this.AdvSelectorForm_Closed);
    this.Load += new EventHandler(this.AdvSelectorForm_Load);
    this.Shown += new EventHandler(this.AdvSelectorForm_Shown);
    this.Resize += new EventHandler(this.AdvSelectorForm_Resize);
    this.panButtons.ResumeLayout(false);
    this.panBack.ResumeLayout(false);
    this.panAttr.ResumeLayout(false);
    this.panListBox.ResumeLayout(false);
    this.panTextBox2.ResumeLayout(false);
    this.tbAttr.Properties.EndInit();
    this.panObj.ResumeLayout(false);
    this.panTreeView.ResumeLayout(false);
    this.panTextBox1.ResumeLayout(false);
    this.tbObj.Properties.EndInit();
    this.ResumeLayout(false);
  }

  private void LoadData()
  {
    switch (this._selectorType)
    {
      case AdvSelector.AttributeType:
        this.Text = LocalizationHolder.rm.GetString("Client.Core_1038");
        this.PopulateAttributeTypes(this._rootID);
        this.panObj.Hide();
        this.panAttr.Visible = true;
        this.splitter1.Visible = false;
        break;
      case AdvSelector.AttributableType:
        AttributableElements kind1 = this._rootID.Kind;
        if (kind1.Equals((object) AttributableElements.Object))
        {
          this.Text = LocalizationHolder.rm.GetString("Client.Core_392");
          this.PopulateObjectTypes(this._rootID.TypeID);
        }
        else
        {
          kind1 = this._rootID.Kind;
          if (kind1.Equals((object) AttributableElements.Relation))
          {
            this.cbObjByShortName.Hide();
            this.Text = LocalizationHolder.rm.GetString("Client.Core_1039");
            this.PopulateRelationTypes(this._rootID.TypeID);
          }
        }
        this.splitter1.Visible = false;
        this.panObj.Dock = DockStyle.Fill;
        break;
      case AdvSelector.AttributableTypeWithAttributeType:
        AttributableElements kind2 = this._rootID.Kind;
        if (kind2.Equals((object) AttributableElements.Object))
        {
          this.Text = LocalizationHolder.rm.GetString("Client.Core_1040");
          this.PopulateObjectTypes(this._rootID.TypeID);
        }
        else
        {
          kind2 = this._rootID.Kind;
          if (kind2.Equals((object) AttributableElements.Relation))
          {
            this.cbObjByShortName.Hide();
            this.Text = LocalizationHolder.rm.GetString("Client.Core_1041");
            this.PopulateRelationTypes(this._rootID.TypeID);
          }
        }
        TreeNode selectedNode = this.tvObj.SelectedNode;
        if (selectedNode != null)
          this.PopulateAttributeTypes(selectedNode.Tag as ElementTypeInfo);
        else
          this.PopulateAttributeTypes(this._rootID);
        this.panAttr.Visible = true;
        break;
    }
  }

  private void AdvSelectorForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
    FormStorage.LoadLayout((Control) this.panObj);
    if (ServicesManager.GetService(typeof (IConfigurationManager)) is IConfigurationManager service)
    {
      IConfiguration configuration1 = service.Open("FormStorage");
      if (configuration1 != null)
      {
        string name = $"{this.GetType().ToString()}_{this.Name}";
        IConfiguration configuration2 = configuration1.Open(name);
        if (configuration2 != null)
        {
          if (configuration2.HasProperty("panAttr.Height"))
            this.panAttr.Height = Convert.ToInt32(configuration2.GetProperty("panAttr.Height"));
          if (configuration2.HasProperty("cbObjByShortName.Checked"))
            this.cbObjByShortName.Checked = Convert.ToBoolean(configuration2.GetProperty("cbObjByShortName.Checked"));
          if (configuration2.HasProperty("cbAttrByShortName.Checked"))
            this.cbAttrByShortName.Checked = Convert.ToBoolean(configuration2.GetProperty("cbAttrByShortName.Checked"));
          if (configuration2.HasProperty("cbAllAttrs.Checked"))
            this.cbAllAttrs.Checked = Convert.ToBoolean(configuration2.GetProperty("cbAllAttrs.Checked"));
          if (configuration2.HasProperty("hisObjectTypes"))
          {
            string property = configuration2.GetProperty("hisObjectTypes");
            if (!property.Equals(string.Empty))
              this.tbObj.Properties.Items.AddRange(new ArrayList((ICollection) property.Split(';')).ToArray());
          }
          if (configuration2.HasProperty("hisAttributeTypes"))
          {
            string property = configuration2.GetProperty("hisAttributeTypes");
            if (!property.Equals(string.Empty))
              this.tbAttr.Properties.Items.AddRange(new ArrayList((ICollection) property.Split(';')).ToArray());
          }
        }
      }
    }
    this.tbObj.Properties.Items.Add((object) AdvSelectorForm.ClearSearch);
    this.tbAttr.Properties.Items.Add((object) AdvSelectorForm.ClearSearch);
    this.LoadData();
  }

  private void AdvSelectorForm_Closed(object sender, EventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
    FormStorage.SaveLayout((Control) this.panObj);
    if (!(ServicesManager.GetService(typeof (IConfigurationManager)) is IConfigurationManager service))
      return;
    IConfiguration configuration1 = service.Open("FormStorage") ?? service.Create("FormStorage");
    string name = $"{this.GetType().ToString()}_{this.Name}";
    IConfiguration configuration2 = configuration1.Open(name) ?? configuration1.Add(name);
    if (this._selectorType.Equals((object) AdvSelector.AttributableTypeWithAttributeType))
      configuration2.SetProperty("panAttr.Height", this.panAttr.Height.ToString());
    configuration2.SetProperty("cbObjByShortName.Checked", this.cbObjByShortName.Checked.ToString());
    IConfiguration configuration3 = configuration2;
    bool flag = this.cbAttrByShortName.Checked;
    string str1 = flag.ToString();
    configuration3.SetProperty("cbAttrByShortName.Checked", str1);
    IConfiguration configuration4 = configuration2;
    flag = this.cbAllAttrs.Checked;
    string str2 = flag.ToString();
    configuration4.SetProperty("cbAllAttrs.Checked", str2);
    this.tbObj.Properties.Items.Remove((object) AdvSelectorForm.ClearSearch);
    string str3 = string.Join(";", new ArrayList((ICollection) this.tbObj.Properties.Items).ToArray(typeof (string)) as string[]);
    configuration2.SetProperty("hisObjectTypes", str3);
    this.tbAttr.Properties.Items.Remove((object) AdvSelectorForm.ClearSearch);
    string str4 = string.Join(";", new ArrayList((ICollection) this.tbAttr.Properties.Items).ToArray(typeof (string)) as string[]);
    configuration2.SetProperty("hisAttributeTypes", str4);
  }

  private void AdvSelectorForm_Shown(object sender, EventArgs e)
  {
    this.tvObj.AfterSelect -= new TreeViewEventHandler(this.tvObj_AfterSelect);
    TreeNode selectedNode = this.tvObj.SelectedNode;
    this.tvObj.SelectedNode = (TreeNode) null;
    this.tvObj.SelectedNode = selectedNode;
    this.tvObj.AfterSelect += new TreeViewEventHandler(this.tvObj_AfterSelect);
    if (this.lvAttr.SelectedItems.Count > 0)
    {
      this.tbAttr.TextChanged -= new EventHandler(this.tbAttr_TextChanged);
      this.tbAttr.Text = (this.lvAttr.SelectedItems[0].Tag as AdvSelectorForm.ID2String4AT).ToString();
      this.tbAttr.TextChanged += new EventHandler(this.tbAttr_TextChanged);
    }
    if (this._selectorType == AdvSelector.AttributeType)
    {
      this.tbAttr.Focus();
      this.tbAttr.SelectAll();
    }
    else
    {
      this.tbObj.Focus();
      this.tbObj.SelectAll();
    }
    if (this._selectorType != AdvSelector.AttributableType && this.panAttr.Height < 400)
    {
      this.panAttr.Height = 400;
      this.SplitterCorrect();
    }
    switch (this._selectorType)
    {
      case AdvSelector.AttributeType:
        this.SelectAttrType(this._selectAttrIDs);
        break;
      case AdvSelector.AttributableType:
        this.SelectObjType(this._selectID.TypeID);
        break;
      case AdvSelector.AttributableTypeWithAttributeType:
        this.SelectObjType(this._selectID.TypeID);
        this.SelectAttrType(this._selectAttrIDs);
        break;
    }
  }

  private void SelectObjType(int objTypeId)
  {
  }

  private void SelectAttrType(ArrayList selAttrId)
  {
    bool flag = false;
    foreach (int attributeTypeId in new ArrayList((ICollection) selAttrId))
    {
      int index = -1;
      AdvSelectorForm.ID2String4AT idItem = this.LoadAttributeType(attributeTypeId);
      if (idItem != null)
        index = this.IndexOf(idItem);
      if (index >= 0)
      {
        this.lvAttr.Items[index].Selected = true;
        if (!flag)
        {
          this.lvAttr.EnsureVisible(index);
          flag = true;
        }
      }
    }
  }

  private void UpdateStatusBar()
  {
    TreeNode selectedNode = this.tvObj.SelectedNode;
    string text = LocalizationHolder.rm.GetString("Client.Core_1042");
    string str1 = LocalizationHolder.rm.GetString("Client.Core_1043");
    string str2 = LocalizationHolder.rm.GetString("Client.Core_1044");
    if (selectedNode != null)
      str1 = text = selectedNode.Text;
    if (this.lvAttr.SelectedItems.Count >= 1)
    {
      ArrayList arrayList = new ArrayList();
      foreach (ListViewItem selectedItem in this.lvAttr.SelectedItems)
        arrayList.Add((object) (selectedItem.Tag as AdvSelectorForm.ID2String4AT).AttributeName);
      str2 = string.Join("; ", arrayList.ToArray(typeof (string)) as string[]);
    }
    switch (this._selectorType)
    {
      case AdvSelector.AttributeType:
        this.statusBar1.Text = string.Format(LocalizationHolder.rm.GetString("Client.Core_1045"), (object) str2);
        break;
      case AdvSelector.AttributableType:
        if (this._rootID.Kind.Equals((object) AttributableElements.Object))
        {
          this.statusBar1.Text = string.Format(LocalizationHolder.rm.GetString("Client.Core_1046"), (object) text);
          break;
        }
        if (this._rootID.Kind.Equals((object) AttributableElements.Relation))
        {
          this.statusBar1.Text = string.Format(LocalizationHolder.rm.GetString("Client.Core_1047"), (object) str1);
          break;
        }
        break;
      case AdvSelector.AttributableTypeWithAttributeType:
        if (this._rootID.Kind.Equals((object) AttributableElements.Object))
        {
          this.statusBar1.Text = string.Format(LocalizationHolder.rm.GetString("Client.Core_1048"), (object) text, (object) str2);
          break;
        }
        if (this._rootID.Kind.Equals((object) AttributableElements.Relation))
        {
          this.statusBar1.Text = string.Format(LocalizationHolder.rm.GetString("Client.Core_1048"), (object) str1, (object) str2);
          break;
        }
        break;
    }
    this.toolTip1.SetToolTip((Control) this.statusBar1, this.statusBar1.Text);
  }

  private void tvObj_AfterSelect(object sender, TreeViewEventArgs e)
  {
    TreeNode selectedNode = this.tvObj.SelectedNode;
    if (selectedNode != null)
    {
      this.tbObj.TextChanged -= new EventHandler(this.tbObj_TextChanged);
      this.tbObj.Text = selectedNode.Text;
      this.tbObj.TextChanged += new EventHandler(this.tbObj_TextChanged);
      switch (this._selectorType)
      {
        case AdvSelector.AttributableType:
          this._selectID = selectedNode.Tag as ElementTypeInfo;
          this.bOk.Enabled = !this._selectID.TypeID.Equals(-1);
          break;
        case AdvSelector.AttributableTypeWithAttributeType:
          this._selectID = selectedNode.Tag as ElementTypeInfo;
          if (!this.cbAllAttrs.Checked)
          {
            this.PopulateAttributeTypes(this._selectID);
            break;
          }
          break;
      }
    }
    this.UpdateStatusBar();
  }

  private void tbObj_TextChanged(object sender, EventArgs e)
  {
    string upper = this.tbObj.Text.ToUpper();
    if (upper.Equals(AdvSelectorForm.ClearSearch.ToUpper()))
    {
      this.tbObj.Properties.Items.Clear();
      this.tbObj.Properties.Items.Add((object) AdvSelectorForm.ClearSearch);
      this.tbObj.Text = string.Empty;
    }
    else
    {
      this.tvObj.AfterSelect -= new TreeViewEventHandler(this.tvObj_AfterSelect);
      bool flag = false;
      if (!upper.Equals(string.Empty))
      {
        TreeNode visibleNode = this.FindVisibleNode(this.tvObj.Nodes[0], upper);
        if (visibleNode != null)
          this.tvObj.SelectedNode = visibleNode;
        flag = visibleNode != null;
      }
      this.tvObj.AfterSelect += new TreeViewEventHandler(this.tvObj_AfterSelect);
      if (!flag)
      {
        this._selectID = new ElementTypeInfo(-1, AttributableElements.None);
        this.tvObj.SelectedNode = (TreeNode) null;
        switch (this._selectorType)
        {
          case AdvSelector.AttributableType:
            this.bOk.Enabled = false;
            break;
          case AdvSelector.AttributableTypeWithAttributeType:
            this._selectAttrIDs.Clear();
            this.tbAttr.Text = string.Empty;
            if (!this.cbAllAttrs.Checked)
              this.lvAttr.Items.Clear();
            this.bOk.Enabled = false;
            break;
        }
        this.UpdateStatusBar();
      }
      else
      {
        this._selectID = this.tvObj.SelectedNode.Tag as ElementTypeInfo;
        switch (this._selectorType)
        {
          case AdvSelector.AttributableType:
            this.bOk.Enabled = !this._selectID.TypeID.Equals(-1);
            break;
          case AdvSelector.AttributableTypeWithAttributeType:
            this.PopulateAttributeTypes(this._selectID);
            break;
        }
        this.UpdateStatusBar();
      }
    }
  }

  private TreeNode FindVisibleNode(TreeNode parentNode, string text)
  {
    foreach (TreeNode node in parentNode.Nodes)
    {
      if (node.Text.ToUpper().StartsWith(text))
        return node;
      if (node.Nodes.Count > 0)
      {
        TreeNode visibleNode = this.FindVisibleNode(node, text);
        if (visibleNode != null)
          return visibleNode;
      }
    }
    return (TreeNode) null;
  }

  private void bFilterObj_Click(object sender, EventArgs e)
  {
    string text = this.tbObj.Text;
    if (!this.tbObj.Properties.Items.Contains((object) text))
      this.tbObj.Properties.Items.Insert(0, (object) text);
    TreeNode selectObj = (TreeNode) null;
    TreeNode appropriateNode = this.FindAppropriateNode(this.tvObj.Nodes[0], text.ToUpper(), ref selectObj);
    this.toolTip1.SetToolTip((Control) this.tbObj, LocalizationHolder.rm.GetString("Client.Core_1049"));
    if (appropriateNode != null)
    {
      ElementTypeInfo tag = appropriateNode.Tag as ElementTypeInfo;
      if (tag.Kind.Equals((object) AttributableElements.Object))
        this.PopulateObjectTypes(tag.TypeID, appropriateNode, selectObj);
      this.toolTip1.SetToolTip((Control) this.tbObj, string.Format(LocalizationHolder.rm.GetString("Client.Core_1050"), (object) text));
    }
    else if (this._rootID.Kind.Equals((object) AttributableElements.Object))
      this.PopulateObjectTypes(this._rootID.TypeID);
    this.bCancelFilterObj.Enabled = appropriateNode != null;
  }

  private void bCancelFilterObj_Click(object sender, EventArgs e)
  {
    if (this._rootID.Kind.Equals((object) AttributableElements.Object))
      this.PopulateObjectTypes(this._rootID.TypeID);
    this.toolTip1.SetToolTip((Control) this.tbObj, LocalizationHolder.rm.GetString("Client.Core_1049"));
    this.bCancelFilterObj.Enabled = false;
  }

  private TreeNode FindAppropriateNode(TreeNode parentNode, string text, ref TreeNode selectObj)
  {
    TreeNode appropriateNode1 = new TreeNode(parentNode.Text);
    appropriateNode1.Tag = parentNode.Tag;
    appropriateNode1.ImageIndex = parentNode.ImageIndex;
    appropriateNode1.SelectedImageIndex = parentNode.SelectedImageIndex;
    bool flag = appropriateNode1.Text.ToUpper().IndexOf(text) >= 0;
    if (parentNode.IsSelected)
      selectObj = appropriateNode1;
    foreach (TreeNode node in parentNode.Nodes)
    {
      TreeNode appropriateNode2 = this.FindAppropriateNode(node, text, ref selectObj);
      if (appropriateNode2 != null)
        appropriateNode1.Nodes.Add(appropriateNode2);
    }
    if (flag || !appropriateNode1.Nodes.Count.Equals(0))
      return appropriateNode1;
    if (selectObj != null && selectObj.Equals((object) appropriateNode1))
      selectObj = (TreeNode) null;
    return (TreeNode) null;
  }

  private void cbObjByShortName_CheckedChanged(object sender, EventArgs e)
  {
    if (!this._rootID.Kind.Equals((object) AttributableElements.Object))
      return;
    this.PopulateObjectTypes(this._rootID.TypeID);
  }

  private void tbAttr_TextChanged(object sender, EventArgs e)
  {
    string upper = this.tbAttr.Text.ToUpper();
    if (upper.Equals(AdvSelectorForm.ClearSearch.ToUpper()))
    {
      this.tbAttr.Properties.Items.Clear();
      this.tbAttr.Properties.Items.Add((object) AdvSelectorForm.ClearSearch);
      this.tbAttr.Text = string.Empty;
    }
    else
    {
      this.lvAttr.SelectedIndexChanged -= new EventHandler(this.lvAttr_SelectedIndexChanged);
      bool flag = false;
      if (!upper.Equals(string.Empty))
      {
        foreach (ListViewItem selectedItem in this.lvAttr.SelectedItems)
          selectedItem.Selected = false;
        foreach (ListViewItem listViewItem in this.lvAttr.Items)
        {
          if (listViewItem.Text.ToUpper().StartsWith(upper))
          {
            flag = true;
            listViewItem.Selected = true;
            break;
          }
        }
      }
      this.lvAttr.SelectedIndexChanged += new EventHandler(this.lvAttr_SelectedIndexChanged);
      if (!flag)
      {
        this._selectAttrIDs.Clear();
        this.lvAttr.SelectedItems.Clear();
        this.bOk.Enabled = false;
        this.UpdateStatusBar();
      }
      else
      {
        this._selectAttrIDs.Clear();
        this._selectAttrIDs.Add((object) (this.lvAttr.SelectedItems[0].Tag as AdvSelectorForm.ID2String4AT).AttributeType);
        this.lvAttr.EnsureVisible(this.lvAttr.SelectedIndices[0]);
        this.bOk.Enabled = true;
        this.UpdateStatusBar();
      }
    }
  }

  private void lvAttr_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lvAttr.SelectedIndices.Count > 0)
    {
      this._selectAttrIDs.Clear();
      foreach (ListViewItem selectedItem in this.lvAttr.SelectedItems)
        this._selectAttrIDs.Add((object) (selectedItem.Tag as AdvSelectorForm.ID2String4AT).AttributeType);
    }
    else
    {
      this._selectAttrIDs.Clear();
      this.tbAttr.Text = string.Empty;
    }
    this.bOk.Enabled = this._selectAttrIDs.Count > 0;
    this.UpdateStatusBar();
  }

  private void bFilterAttr_Click(object sender, EventArgs e)
  {
    string text = this.tbAttr.Text;
    this._filterAttr = text.ToUpper();
    this.toolTip1.SetToolTip((Control) this.tbAttr, LocalizationHolder.rm.GetString("Client.Core_1049"));
    if (this._filterAttr.Equals(string.Empty))
      return;
    if (!this.tbAttr.Properties.Items.Contains((object) text))
      this.tbAttr.Properties.Items.Insert(0, (object) text);
    this.PopulateAttributeTypes(this._selectID);
    this.toolTip1.SetToolTip((Control) this.tbAttr, string.Format(LocalizationHolder.rm.GetString("Client.Core_1050"), (object) text));
    this.bCancelFilterAttr.Enabled = true;
    this.bAll.Enabled = true;
  }

  private void bCancelFilterAttr_Click(object sender, EventArgs e)
  {
    this._filterAttr = string.Empty;
    this.PopulateAttributeTypes(this._selectID);
    this.toolTip1.SetToolTip((Control) this.tbAttr, LocalizationHolder.rm.GetString("Client.Core_1049"));
    this.bCancelFilterAttr.Enabled = false;
    this.bAll.Enabled = !this._selectID.TypeID.Equals(-1);
  }

  private void bAll_Click(object sender, EventArgs e)
  {
    this.PopulateAttributeTypes(new ElementTypeInfo(-1, AttributableElements.None));
    this.bAll.Enabled = false;
  }

  private void cbAttrByShortName_CheckedChanged(object sender, EventArgs e)
  {
    this.PopulateAttributeTypes(this._selectID);
  }

  private void cbAllAttrs_CheckedChanged(object sender, EventArgs e)
  {
    this.PopulateAttributeTypes(this.cbAllAttrs.Checked ? new ElementTypeInfo(-1, AttributableElements.None) : this._selectID);
  }

  private void AdvSelectorForm_Resize(object sender, EventArgs e) => this.SplitterCorrect();

  private void SplitterCorrect()
  {
    if (this.WindowState != FormWindowState.Normal)
      return;
    this.splitter1.SplitPosition = this.splitter1.SplitPosition;
  }

  internal class ID2String4AT : IEquatable<AdvSelectorForm.ID2String4AT>
  {
    private readonly int _attributeType = -1;
    private string _attributeName = string.Empty;
    private string _attributeShortName = string.Empty;
    private bool _byShortName;
    private FieldTypes _fieldType;

    public ID2String4AT(
      int attributeType,
      string attributeName,
      string attributeShortName,
      FieldTypes fieldType)
    {
      this._attributeType = attributeType;
      this._attributeName = attributeName;
      this._attributeShortName = attributeShortName;
      this._fieldType = fieldType;
    }

    public ID2String4AT(
      int attributeType,
      string attributeName,
      string attributeShortName,
      FieldTypes fieldType,
      bool byShortName)
      : this(attributeType, attributeName, attributeShortName, fieldType)
    {
      this._byShortName = byShortName;
    }

    public int AttributeType => this._attributeType;

    public string AttributeName => this._attributeName;

    public string AttributeShortName => this._attributeShortName;

    public FieldTypes FieldType => this._fieldType;

    public override string ToString()
    {
      return this._byShortName && !this._attributeShortName.Equals(string.Empty) ? this._attributeShortName : this._attributeName;
    }

    public override int GetHashCode() => this._attributeType.GetHashCode();

    public override bool Equals(object obj)
    {
      return obj is AdvSelectorForm.ID2String4AT id2String4At ? this._attributeType.Equals(id2String4At._attributeType) : base.Equals(obj);
    }

    public bool Equals(AdvSelectorForm.ID2String4AT other) => this.Equals((object) other);
  }
}
