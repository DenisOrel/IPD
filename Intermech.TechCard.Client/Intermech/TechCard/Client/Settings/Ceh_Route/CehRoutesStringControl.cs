// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Settings.Ceh_Route.CehRoutesStringControl
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.Ceh_Route;
using Intermech.Localization;
using Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Settings.Ceh_Route;

/// <summary>
/// Контрол редактирования правила сбора строки расцеховки
/// </summary>
public class CehRoutesStringControl : UserControl
{
  /// <summary>Признак read only</summary>
  private bool _readOnly;
  /// <summary>Режим загрузки</summary>
  private bool _loadMode;
  /// <summary>
  /// 
  /// </summary>
  private bool _modified;
  /// <summary>Правила построения строки расцеховки</summary>
  private ICehRouteStringItem _cehRouteStrItem;
  private ICategoryTypeIconService _categoryTypeIconService;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private SplitContainer splitContainer1;
  private Panel pnlTemplInfo;
  private Label lblTemplCaption;
  private Panel pnlRouteStringParam;
  private TextBox tbxSeparator;
  private TextBox tbxTemplate;
  private Label lblSeparator;
  private Label lblTemplate;
  private ContextMenuStrip cmObjectTypes;
  private ContextMenuStrip cmElemAttributes;
  private ToolStripMenuItem tsmiElemAttrAdd;
  private ToolStripMenuItem tsmiElemAttrDelete;
  private ToolStripMenuItem tsObjectTypeAdd;
  private ToolStripMenuItem tsObjectTypeRemove;
  private ToolStripMenuItem tsObjectTypeEdit;
  private ToolStripSeparator toolStripMenuItem1;
  private ToolStripMenuItem tsObjectTypeMove;
  private ToolStripMenuItem tsObjectTypeMoveFirst;
  private ToolStripMenuItem tsObjectTypeMoveUp;
  private ToolStripMenuItem tsObjectTypeMoveDown;
  private ToolStripMenuItem tsObjectTypeMoveLast;
  private TabControl tbAttrMode;
  private TabPage tpRouteElem;
  private ListView lvRouteElemAttrs;
  private ColumnHeader chReAttrFullName;
  private ColumnHeader chReAttrShortName;
  private TabPage tpLink;
  private ListView lvLinkAttrs;
  private ColumnHeader chLinkAttrFullName;
  private ColumnHeader chLinkAttrShortName;
  private ContextMenuStrip cmLinkAttributes;
  private ToolStripMenuItem tsmiLinkAttrAdd;
  private ToolStripMenuItem tsmiLinkAttrDelete;
  private TreeView tvObjectTypes;

  /// <summary>Инициализация контрола</summary>
  private void InitializeCustomControls()
  {
    this.FillRouteElemAttrList();
    this.FillLinkAttrList();
    this.UpdateControls();
  }

  private void InitializeCustomServices()
  {
    this._categoryTypeIconService = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
    if (this._categoryTypeIconService == null)
      return;
    this.tvObjectTypes.ImageList = this._categoryTypeIconService.ImageList;
  }

  /// <summary>Обновление контролов</summary>
  private void UpdateControls()
  {
    this.cmElemAttributes.Enabled = this.cmObjectTypes.Enabled = this.tbxSeparator.Enabled = this.tbxTemplate.Enabled = !this._readOnly;
  }

  private bool IsNeedCehRouteObject()
  {
    return MetaDataHelper.GetApplicability(TechCardConsts.ObjectTypes.CehRouteID, TechCardConsts.ObjectTypes.ElemRouteID, TechCardConsts.RelTypes.TechRelationID) != null;
  }

  /// <summary>Заполнение списка типов объектов</summary>
  private void FillObjectTypeItems()
  {
    this._loadMode = true;
    this.tvObjectTypes.TreeViewNodeSorter = (IComparer) null;
    this.tvObjectTypes.BeginUpdate();
    try
    {
      this.tbxSeparator.Text = this._cehRouteStrItem.RouteSeparator;
      this.tvObjectTypes.Nodes.Clear();
      TreeNodeCollection nodes = this.tvObjectTypes.Nodes;
      if (this.IsNeedCehRouteObject())
      {
        ICehRouteStringTemplItem routeStringTemplItem = this._cehRouteStrItem.Items.FirstOrDefault<ICehRouteStringTemplItem>((Func<ICehRouteStringTemplItem, bool>) (item => item.ObjTypeID == TechCardConsts.ObjectTypes.CehRouteID));
        if (routeStringTemplItem == null)
        {
          routeStringTemplItem = this._cehRouteStrItem.CreateTemplItem(TechCardConsts.ObjectTypes.CehRouteID);
          this._cehRouteStrItem.Items.Add(routeStringTemplItem);
        }
        TreeNode treeNode = this.tvObjectTypes.Nodes.Add(MetaDataHelper.GetObjectTypeName(TechCardConsts.ObjectTypes.CehRouteID));
        treeNode.Tag = (object) routeStringTemplItem;
        int num1;
        int num2 = num1 = this._categoryTypeIconService != null ? this._categoryTypeIconService.IndexOf(4, TechCardConsts.ObjectTypes.CehRouteID) : -1;
        treeNode.SelectedImageIndex = num1;
        treeNode.ImageIndex = num2;
        nodes = treeNode.Nodes;
      }
      foreach (ICehRouteStringTemplItem routeStringTemplItem in (IEnumerable<ICehRouteStringTemplItem>) this._cehRouteStrItem.Items)
      {
        if (routeStringTemplItem.ObjTypeID != TechCardConsts.ObjectTypes.CehRouteID)
        {
          IMSObjectType objectType = MetaDataHelper.GetObjectType(routeStringTemplItem.ObjTypeID);
          if (objectType != null && objectType.VersionsMode != ObjectVersionModes.Abstract)
          {
            TreeNode treeNode = nodes.Add(objectType.ObjectTypeName);
            treeNode.Tag = (object) routeStringTemplItem;
            int num3;
            int num4 = num3 = this._categoryTypeIconService != null ? this._categoryTypeIconService.IndexOf(4, routeStringTemplItem.ObjTypeID) : -1;
            treeNode.SelectedImageIndex = num3;
            treeNode.ImageIndex = num4;
          }
        }
      }
    }
    finally
    {
      this.tvObjectTypes.TreeViewNodeSorter = (IComparer) new CehRoutesStringControl.CehRoutesItemComparer();
      this.tvObjectTypes.EndUpdate();
      if (this.tvObjectTypes.Nodes.Count > 0)
      {
        this.tvObjectTypes.SelectedNode = this.tvObjectTypes.Nodes[0];
        this.tvObjectTypes.ExpandAll();
      }
      this._loadMode = false;
    }
  }

  /// <summary>Заполнение списка атрибутов для РЭ</summary>
  private void FillRouteElemAttrList()
  {
    this.lvRouteElemAttrs.BeginUpdate();
    try
    {
      this.lvRouteElemAttrs.Items.Clear();
      if (MetaDataHelper.GetObjectType(TechCardConsts.ObjectTypes.ElemRouteGUID) == null)
        return;
      List<IMSAttribute4ObjectType> attribute4ObjectTypeList = MetaDataHelper.GetAttribute4ObjectTypeList(TechCardConsts.ObjectTypes.ElemRouteGUID);
      if (attribute4ObjectTypeList == null)
        return;
      foreach (IMSAttribute4ObjectType attribute4ObjectType in attribute4ObjectTypeList)
      {
        if (attribute4ObjectType != null)
        {
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attribute4ObjectType.AttributeID);
          if (attributeType != null)
            this.lvRouteElemAttrs.Items.Add(new ListViewItem(new string[2]
            {
              attributeType.Name,
              attributeType.ShortName
            })
            {
              Tag = (object) attributeType.AttributeID
            });
        }
      }
    }
    finally
    {
      this.lvRouteElemAttrs.EndUpdate();
    }
  }

  /// <summary>Заполнение списка атрибутов для связи</summary>
  private void FillLinkAttrList()
  {
    this.lvLinkAttrs.BeginUpdate();
    try
    {
      this.lvLinkAttrs.Items.Clear();
      IMSRelationType relationType = MetaDataHelper.GetRelationType(TechCardConsts.RelTypes.TechRelationGuid);
      if (relationType == null)
        return;
      List<IMSAttribute4RelationType> relationTypeList = MetaDataHelper.GetAttribute4RelationTypeList(relationType.RelationTypeID);
      if (relationTypeList == null)
        return;
      foreach (IMSAttribute4RelationType attribute4RelationType in relationTypeList)
      {
        if (attribute4RelationType != null)
        {
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attribute4RelationType.AttributeID);
          if (attributeType != null)
            this.lvLinkAttrs.Items.Add(new ListViewItem(new string[2]
            {
              attributeType.Name,
              attributeType.ShortName
            })
            {
              Tag = (object) attributeType.AttributeID
            });
        }
      }
    }
    finally
    {
      this.lvLinkAttrs.EndUpdate();
    }
  }

  /// <summary>Добавление нового шаблона</summary>
  private void ObjectTypeItemAdd()
  {
    if (this._readOnly || this._cehRouteStrItem == null)
      return;
    ICehRouteStringTemplItem templItem = this._cehRouteStrItem.CreateTemplItem(-1);
    if (templItem == null || !this.ObjectTypeItemEdit(templItem))
      return;
    templItem.OrderID = this._cehRouteStrItem.Items.Count;
    this._cehRouteStrItem.Items.Add(templItem);
  }

  /// <summary>Редактирование шаблона</summary>
  /// <param name="cehRouteStrTempItem"></param>
  private bool ObjectTypeItemEdit(ICehRouteStringTemplItem cehRouteStrTempItem)
  {
    if (this._readOnly || cehRouteStrTempItem == null)
      return false;
    CehRoutesStringTemplateSelect stringTemplateSelect = new CehRoutesStringTemplateSelect();
    stringTemplateSelect.RouteStringItem = this._cehRouteStrItem;
    stringTemplateSelect.RouteStringTemplate = cehRouteStrTempItem;
    stringTemplateSelect.LoadData();
    return stringTemplateSelect.ShowDialog() == DialogResult.OK;
  }

  /// <summary>Удаление шаблона из списка</summary>
  /// <param name="index"></param>
  private void ObjectTypeItemRemove([NotNull] TreeNode treeNode)
  {
    if (this._readOnly || this._cehRouteStrItem == null || !(treeNode.Tag is ICehRouteStringTemplItem tag))
      return;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(tag.ObjTypeID);
    if (objectType == null || MessageBox.Show(string.Format(LocalizationHolder.rm.GetString(sc_19756.ssp_techcard_19757()), (object) objectType.ObjectTypeName), LocalizationHolder.rm.GetString("TechCard.Client_104"), MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    this._cehRouteStrItem.Items.Remove(tag);
  }

  /// <summary>Подвигаем элементы</summary>
  /// <param name="oldIndex"></param>
  /// <param name="newIndex"></param>
  private void ObjectTypeItemMove([NotNull] TreeNode treeNode, int newIndex)
  {
    TreeNodeCollection treeNodeCollection = treeNode.Parent?.Nodes ?? treeNode.TreeView.Nodes;
    int index = treeNode.Index;
    if (newIndex < 0 || newIndex > treeNodeCollection.Count - 1 || index == newIndex)
      return;
    this.tvObjectTypes.BeginUpdate();
    try
    {
      this.tvObjectTypes.TreeViewNodeSorter = (IComparer) null;
      int num1 = index;
      int num2 = index < newIndex ? 1 : -1;
      for (; num1 != newIndex; num1 += num2)
      {
        ICehRouteStringTemplItem tag1 = (ICehRouteStringTemplItem) treeNodeCollection[index].Tag;
        ICehRouteStringTemplItem tag2 = (ICehRouteStringTemplItem) treeNodeCollection[num1 + num2].Tag;
        int orderId = tag1.OrderID;
        tag1.OrderID = tag2.OrderID;
        tag2.OrderID = orderId;
      }
    }
    finally
    {
      this.tvObjectTypes.TreeViewNodeSorter = (IComparer) new CehRoutesStringControl.CehRoutesItemComparer();
      this.tvObjectTypes.EndUpdate();
    }
    this.DoChanged();
  }

  /// <summary>
  /// 
  /// </summary>
  private void ObjectTypeItemMoveFirst()
  {
    if (this._readOnly)
      return;
    TreeNode selectedNode = this.tvObjectTypes.SelectedNode;
    if (selectedNode == null)
      return;
    this.ObjectTypeItemMove(selectedNode, 0);
  }

  /// <summary>
  /// 
  /// </summary>
  private void ObjectTypeItemMoveUp()
  {
    if (this._readOnly)
      return;
    TreeNode selectedNode = this.tvObjectTypes.SelectedNode;
    if (selectedNode == null)
      return;
    this.ObjectTypeItemMove(selectedNode, selectedNode.Index - 1);
  }

  /// <summary>
  /// 
  /// </summary>
  private void ObjectTypeItemMoveDown()
  {
    if (this._readOnly)
      return;
    TreeNode selectedNode = this.tvObjectTypes.SelectedNode;
    if (selectedNode == null)
      return;
    this.ObjectTypeItemMove(selectedNode, selectedNode.Index + 1);
  }

  /// <summary>
  /// 
  /// </summary>
  private void ObjectTypeItemMoveLast()
  {
    if (this._readOnly)
      return;
    TreeNode selectedNode = this.tvObjectTypes.SelectedNode;
    if (selectedNode == null)
      return;
    int newIndex = selectedNode.Parent == null ? this.tvObjectTypes.Nodes.Count - 1 : selectedNode.Parent.Nodes.Count - 1;
    this.ObjectTypeItemMove(selectedNode, newIndex);
  }

  private ICehRouteStringTemplItem GetSelectedObjectTypeItemTemplate()
  {
    return this.tvObjectTypes.SelectedNode == null ? (ICehRouteStringTemplItem) null : this.tvObjectTypes.SelectedNode.Tag as ICehRouteStringTemplItem;
  }

  /// <summary>Добавим новый атрибут в строку шаблона</summary>
  /// <param name="text"></param>
  private void AttributeAdd(string text)
  {
    if (this._readOnly)
      return;
    string str = string.Format(CehRouteStringTemplItem.AttributeTemplate, (object) text);
    if (this.tbxTemplate.Text.IndexOf(str, StringComparison.Ordinal) != -1)
      return;
    this.tbxTemplate.Text += str;
  }

  /// <summary>Удалим атрибут из шаблона</summary>
  /// <param name="text"></param>
  private void AttributeDelete(string text)
  {
    if (this._readOnly)
      return;
    this.tbxTemplate.Text = this.tbxTemplate.Text.Replace(string.Format(CehRouteStringTemplItem.AttributeTemplate, (object) text), "");
  }

  /// <summary>Конструктор</summary>
  public CehRoutesStringControl()
  {
    this.InitializeComponent();
    this.InitializeCustomControls();
    if (this.DesignMode)
      return;
    this.InitializeCustomServices();
  }

  /// <summary>Интерфейс правила нумерации</summary>
  public ICehRouteStringItem CehRouteStrItem
  {
    get => this._cehRouteStrItem;
    set
    {
      this._cehRouteStrItem = value;
      this.FillObjectTypeItems();
      this._modified = false;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public bool Modified => this._modified;

  /// <summary>
  /// 
  /// </summary>
  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      if (this._readOnly == value)
        return;
      this._readOnly = value;
      this.UpdateControls();
    }
  }

  /// <summary>Changed Event</summary>
  public event EventHandler Changed;

  /// <summary>
  /// 
  /// </summary>
  private void DoChanged()
  {
    if (this._loadMode)
      return;
    this._modified = true;
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, new EventArgs());
  }

  private void tvObjectTypes_AfterSelect(object sender, TreeViewEventArgs e)
  {
    if (this._loadMode)
      return;
    ICehRouteStringTemplItem typeItemTemplate = this.GetSelectedObjectTypeItemTemplate();
    if (typeItemTemplate == null)
      return;
    this.ObjectTypeItemUpdate(typeItemTemplate);
  }

  private void ObjectTypeItemUpdate(ICehRouteStringTemplItem selectedItemTemplate)
  {
    bool loadMode = this._loadMode;
    this._loadMode = true;
    this.tbxTemplate.TextChanged -= new EventHandler(this.tbxTemplate_TextChanged);
    try
    {
      this.tbxTemplate.Text = selectedItemTemplate?.RouteTemplate ?? string.Empty;
    }
    finally
    {
      this.tbxTemplate.TextChanged += new EventHandler(this.tbxTemplate_TextChanged);
      this._loadMode = loadMode;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tbxTemplate_TextChanged(object sender, EventArgs e)
  {
    ICehRouteStringTemplItem typeItemTemplate = this.GetSelectedObjectTypeItemTemplate();
    if (typeItemTemplate == null)
      return;
    typeItemTemplate.RouteTemplate = this.tbxTemplate.Text;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void lvRouteElemAttrs_DoubleClick(object sender, EventArgs e)
  {
    if (this._readOnly || this.GetSelectedObjectTypeItemTemplate() == null)
      return;
    ListViewItem focusedItem = this.lvRouteElemAttrs.FocusedItem;
    if (focusedItem == null)
      return;
    this.AttributeAdd(focusedItem.Text);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsObjectTypeMoveFirst_Click(object sender, EventArgs e)
  {
    this.ObjectTypeItemMoveFirst();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsObjectTypeMoveUp_Click(object sender, EventArgs e) => this.ObjectTypeItemMoveUp();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsObjectTypeMoveDown_Click(object sender, EventArgs e)
  {
    this.ObjectTypeItemMoveDown();
  }

  private void tsObjectTypeMoveLast_Click(object sender, EventArgs e)
  {
    this.ObjectTypeItemMoveLast();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cmObjectTypes_Opening(object sender, CancelEventArgs e)
  {
    if (this._readOnly)
      return;
    TreeNode selectedNode = this.tvObjectTypes.SelectedNode;
    int? objTypeId = this.GetSelectedObjectTypeItemTemplate()?.ObjTypeID;
    int cehRouteId = TechCardConsts.ObjectTypes.CehRouteID;
    bool flag1 = objTypeId.GetValueOrDefault() == cehRouteId & objTypeId.HasValue;
    ToolStripMenuItem tsObjectTypeEdit = this.tsObjectTypeEdit;
    ToolStripMenuItem objectTypeRemove = this.tsObjectTypeRemove;
    bool flag2;
    this.tsObjectTypeMove.Enabled = flag2 = selectedNode != null && !flag1;
    int num1;
    bool flag3 = (num1 = flag2 ? 1 : 0) != 0;
    objectTypeRemove.Enabled = num1 != 0;
    int num2 = flag3 ? 1 : 0;
    tsObjectTypeEdit.Enabled = num2 != 0;
    this.tsObjectTypeMoveFirst.Enabled = this.tsObjectTypeMoveUp.Enabled = !flag1 && selectedNode?.PrevNode != null;
    this.tsObjectTypeMoveLast.Enabled = this.tsObjectTypeMoveDown.Enabled = !flag1 && selectedNode?.NextNode != null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cmAttributes_Opening(object sender, CancelEventArgs e)
  {
    if (this._readOnly)
      return;
    ListViewItem focusedItem = this.lvRouteElemAttrs.FocusedItem;
    bool flag1 = focusedItem != null && this.GetSelectedObjectTypeItemTemplate() != null;
    bool flag2 = focusedItem != null && this.tbxTemplate.Text.Contains(focusedItem.Text);
    this.tsmiElemAttrAdd.Enabled = flag1 && !flag2;
    this.tsmiElemAttrDelete.Enabled = flag1 & flag2;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiAttrAdd_Click(object sender, EventArgs e)
  {
    if (this._readOnly)
      return;
    ListViewItem focusedItem = this.lvRouteElemAttrs.FocusedItem;
    if (focusedItem == null)
      return;
    this.AttributeAdd(focusedItem.Text);
    this.DoChanged();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiAttrDelete_Click(object sender, EventArgs e)
  {
    if (this._readOnly)
      return;
    ListViewItem focusedItem = this.lvRouteElemAttrs.FocusedItem;
    if (focusedItem == null)
      return;
    this.AttributeDelete(focusedItem.Text);
    this.DoChanged();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsObjectTypeAdd_Click(object sender, EventArgs e)
  {
    if (this._readOnly)
      return;
    this.ObjectTypeItemAdd();
    this.FillObjectTypeItems();
    this.DoChanged();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsObjectTypeEdit_Click(object sender, EventArgs e)
  {
    if (this._readOnly)
      return;
    ICehRouteStringTemplItem typeItemTemplate = this.GetSelectedObjectTypeItemTemplate();
    if (typeItemTemplate == null)
      return;
    this.ObjectTypeItemEdit(typeItemTemplate);
    this.FillObjectTypeItems();
    this.DoChanged();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsObjectTypeRemove_Click(object sender, EventArgs e)
  {
    if (this._readOnly)
      return;
    TreeNode selectedNode = this.tvObjectTypes.SelectedNode;
    if (selectedNode == null)
      return;
    this.ObjectTypeItemRemove(selectedNode);
    this.FillObjectTypeItems();
    this.DoChanged();
  }

  private void tbxTemplate_TextChanged_1(object sender, EventArgs e)
  {
    if (this._readOnly || this._loadMode)
      return;
    this._cehRouteStrItem.RouteSeparator = this.tbxSeparator.Text;
    this.DoChanged();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void lvLinkAttrs_DoubleClick(object sender, EventArgs e)
  {
    if (this._readOnly || this.GetSelectedObjectTypeItemTemplate() == null)
      return;
    ListViewItem focusedItem = this.lvLinkAttrs.FocusedItem;
    if (focusedItem == null)
      return;
    this.AttributeAdd(CehRouteStringTemplItem.LinkAttributePrefix + focusedItem.Text);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void lvLinkAttrs_SelectedIndexChanged(object sender, EventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cmLinkAttributes_Opening(object sender, CancelEventArgs e)
  {
    if (this._readOnly)
      return;
    ListViewItem focusedItem = this.lvLinkAttrs.FocusedItem;
    bool flag1 = focusedItem != null && this.GetSelectedObjectTypeItemTemplate() != null;
    bool flag2 = focusedItem != null && this.tbxTemplate.Text.Contains(CehRouteStringTemplItem.LinkAttributePrefix + focusedItem.Text);
    this.tsmiLinkAttrAdd.Enabled = flag1 && !flag2;
    this.tsmiLinkAttrDelete.Enabled = flag1 & flag2;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiLinkAttrAdd_Click(object sender, EventArgs e)
  {
    if (this._readOnly)
      return;
    ListViewItem focusedItem = this.lvLinkAttrs.FocusedItem;
    if (focusedItem == null)
      return;
    this.AttributeAdd(CehRouteStringTemplItem.LinkAttributePrefix + focusedItem.Text);
    this.DoChanged();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiLinkAttrDelete_Click(object sender, EventArgs e)
  {
    if (this._readOnly)
      return;
    ListViewItem focusedItem = this.lvLinkAttrs.FocusedItem;
    if (focusedItem == null)
      return;
    this.AttributeDelete(CehRouteStringTemplItem.LinkAttributePrefix + focusedItem.Text);
    this.DoChanged();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CehRoutesStringControl));
    this.splitContainer1 = new SplitContainer();
    this.tvObjectTypes = new TreeView();
    this.cmObjectTypes = new ContextMenuStrip(this.components);
    this.tsObjectTypeAdd = new ToolStripMenuItem();
    this.tsObjectTypeEdit = new ToolStripMenuItem();
    this.tsObjectTypeRemove = new ToolStripMenuItem();
    this.toolStripMenuItem1 = new ToolStripSeparator();
    this.tsObjectTypeMove = new ToolStripMenuItem();
    this.tsObjectTypeMoveFirst = new ToolStripMenuItem();
    this.tsObjectTypeMoveUp = new ToolStripMenuItem();
    this.tsObjectTypeMoveDown = new ToolStripMenuItem();
    this.tsObjectTypeMoveLast = new ToolStripMenuItem();
    this.pnlTemplInfo = new Panel();
    this.lblTemplCaption = new Label();
    this.tbAttrMode = new TabControl();
    this.tpRouteElem = new TabPage();
    this.lvRouteElemAttrs = new ListView();
    this.chReAttrFullName = new ColumnHeader();
    this.chReAttrShortName = new ColumnHeader();
    this.cmElemAttributes = new ContextMenuStrip(this.components);
    this.tsmiElemAttrAdd = new ToolStripMenuItem();
    this.tsmiElemAttrDelete = new ToolStripMenuItem();
    this.tpLink = new TabPage();
    this.lvLinkAttrs = new ListView();
    this.chLinkAttrFullName = new ColumnHeader();
    this.chLinkAttrShortName = new ColumnHeader();
    this.cmLinkAttributes = new ContextMenuStrip(this.components);
    this.tsmiLinkAttrAdd = new ToolStripMenuItem();
    this.tsmiLinkAttrDelete = new ToolStripMenuItem();
    this.pnlRouteStringParam = new Panel();
    this.tbxSeparator = new TextBox();
    this.tbxTemplate = new TextBox();
    this.lblSeparator = new Label();
    this.lblTemplate = new Label();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.cmObjectTypes.SuspendLayout();
    this.pnlTemplInfo.SuspendLayout();
    this.tbAttrMode.SuspendLayout();
    this.tpRouteElem.SuspendLayout();
    this.cmElemAttributes.SuspendLayout();
    this.tpLink.SuspendLayout();
    this.cmLinkAttributes.SuspendLayout();
    this.pnlRouteStringParam.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this.tvObjectTypes);
    this.splitContainer1.Panel1.Controls.Add((Control) this.pnlTemplInfo);
    this.splitContainer1.Panel2.Controls.Add((Control) this.tbAttrMode);
    this.splitContainer1.Panel2.Controls.Add((Control) this.pnlRouteStringParam);
    this.tvObjectTypes.ContextMenuStrip = this.cmObjectTypes;
    componentResourceManager.ApplyResources((object) this.tvObjectTypes, "tvObjectTypes");
    this.tvObjectTypes.FullRowSelect = true;
    this.tvObjectTypes.HideSelection = false;
    this.tvObjectTypes.Name = "tvObjectTypes";
    this.tvObjectTypes.AfterSelect += new TreeViewEventHandler(this.tvObjectTypes_AfterSelect);
    this.cmObjectTypes.Items.AddRange(new ToolStripItem[5]
    {
      (ToolStripItem) this.tsObjectTypeAdd,
      (ToolStripItem) this.tsObjectTypeEdit,
      (ToolStripItem) this.tsObjectTypeRemove,
      (ToolStripItem) this.toolStripMenuItem1,
      (ToolStripItem) this.tsObjectTypeMove
    });
    this.cmObjectTypes.Name = "cmTemplates";
    componentResourceManager.ApplyResources((object) this.cmObjectTypes, "cmObjectTypes");
    this.cmObjectTypes.Opening += new CancelEventHandler(this.cmObjectTypes_Opening);
    this.tsObjectTypeAdd.Name = "tsObjectTypeAdd";
    componentResourceManager.ApplyResources((object) this.tsObjectTypeAdd, "tsObjectTypeAdd");
    this.tsObjectTypeAdd.Click += new EventHandler(this.tsObjectTypeAdd_Click);
    this.tsObjectTypeEdit.Name = "tsObjectTypeEdit";
    componentResourceManager.ApplyResources((object) this.tsObjectTypeEdit, "tsObjectTypeEdit");
    this.tsObjectTypeEdit.Click += new EventHandler(this.tsObjectTypeEdit_Click);
    this.tsObjectTypeRemove.Name = "tsObjectTypeRemove";
    componentResourceManager.ApplyResources((object) this.tsObjectTypeRemove, "tsObjectTypeRemove");
    this.tsObjectTypeRemove.Click += new EventHandler(this.tsObjectTypeRemove_Click);
    this.toolStripMenuItem1.Name = "toolStripMenuItem1";
    componentResourceManager.ApplyResources((object) this.toolStripMenuItem1, "toolStripMenuItem1");
    this.tsObjectTypeMove.DropDownItems.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.tsObjectTypeMoveFirst,
      (ToolStripItem) this.tsObjectTypeMoveUp,
      (ToolStripItem) this.tsObjectTypeMoveDown,
      (ToolStripItem) this.tsObjectTypeMoveLast
    });
    this.tsObjectTypeMove.Name = "tsObjectTypeMove";
    componentResourceManager.ApplyResources((object) this.tsObjectTypeMove, "tsObjectTypeMove");
    this.tsObjectTypeMoveFirst.Name = "tsObjectTypeMoveFirst";
    componentResourceManager.ApplyResources((object) this.tsObjectTypeMoveFirst, "tsObjectTypeMoveFirst");
    this.tsObjectTypeMoveFirst.Click += new EventHandler(this.tsObjectTypeMoveFirst_Click);
    this.tsObjectTypeMoveUp.Name = "tsObjectTypeMoveUp";
    componentResourceManager.ApplyResources((object) this.tsObjectTypeMoveUp, "tsObjectTypeMoveUp");
    this.tsObjectTypeMoveUp.Click += new EventHandler(this.tsObjectTypeMoveUp_Click);
    this.tsObjectTypeMoveDown.Name = "tsObjectTypeMoveDown";
    componentResourceManager.ApplyResources((object) this.tsObjectTypeMoveDown, "tsObjectTypeMoveDown");
    this.tsObjectTypeMoveDown.Click += new EventHandler(this.tsObjectTypeMoveDown_Click);
    this.tsObjectTypeMoveLast.Name = "tsObjectTypeMoveLast";
    componentResourceManager.ApplyResources((object) this.tsObjectTypeMoveLast, "tsObjectTypeMoveLast");
    this.tsObjectTypeMoveLast.Click += new EventHandler(this.tsObjectTypeMoveLast_Click);
    this.pnlTemplInfo.Controls.Add((Control) this.lblTemplCaption);
    componentResourceManager.ApplyResources((object) this.pnlTemplInfo, "pnlTemplInfo");
    this.pnlTemplInfo.Name = "pnlTemplInfo";
    componentResourceManager.ApplyResources((object) this.lblTemplCaption, "lblTemplCaption");
    this.lblTemplCaption.Name = "lblTemplCaption";
    this.tbAttrMode.Controls.Add((Control) this.tpRouteElem);
    this.tbAttrMode.Controls.Add((Control) this.tpLink);
    componentResourceManager.ApplyResources((object) this.tbAttrMode, "tbAttrMode");
    this.tbAttrMode.Name = "tbAttrMode";
    this.tbAttrMode.SelectedIndex = 0;
    this.tpRouteElem.Controls.Add((Control) this.lvRouteElemAttrs);
    componentResourceManager.ApplyResources((object) this.tpRouteElem, "tpRouteElem");
    this.tpRouteElem.Name = "tpRouteElem";
    this.tpRouteElem.UseVisualStyleBackColor = true;
    this.lvRouteElemAttrs.Columns.AddRange(new ColumnHeader[2]
    {
      this.chReAttrFullName,
      this.chReAttrShortName
    });
    this.lvRouteElemAttrs.ContextMenuStrip = this.cmElemAttributes;
    componentResourceManager.ApplyResources((object) this.lvRouteElemAttrs, "lvRouteElemAttrs");
    this.lvRouteElemAttrs.FullRowSelect = true;
    this.lvRouteElemAttrs.GridLines = true;
    this.lvRouteElemAttrs.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.lvRouteElemAttrs.HideSelection = false;
    this.lvRouteElemAttrs.LabelEdit = true;
    this.lvRouteElemAttrs.MultiSelect = false;
    this.lvRouteElemAttrs.Name = "lvRouteElemAttrs";
    this.lvRouteElemAttrs.UseCompatibleStateImageBehavior = false;
    this.lvRouteElemAttrs.View = View.Details;
    this.lvRouteElemAttrs.DoubleClick += new EventHandler(this.lvRouteElemAttrs_DoubleClick);
    componentResourceManager.ApplyResources((object) this.chReAttrFullName, "chReAttrFullName");
    componentResourceManager.ApplyResources((object) this.chReAttrShortName, "chReAttrShortName");
    this.cmElemAttributes.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.tsmiElemAttrAdd,
      (ToolStripItem) this.tsmiElemAttrDelete
    });
    this.cmElemAttributes.Name = "cmAttributes";
    componentResourceManager.ApplyResources((object) this.cmElemAttributes, "cmElemAttributes");
    this.cmElemAttributes.Opening += new CancelEventHandler(this.cmAttributes_Opening);
    this.tsmiElemAttrAdd.Name = "tsmiElemAttrAdd";
    componentResourceManager.ApplyResources((object) this.tsmiElemAttrAdd, "tsmiElemAttrAdd");
    this.tsmiElemAttrAdd.Click += new EventHandler(this.tsmiAttrAdd_Click);
    this.tsmiElemAttrDelete.Name = "tsmiElemAttrDelete";
    componentResourceManager.ApplyResources((object) this.tsmiElemAttrDelete, "tsmiElemAttrDelete");
    this.tsmiElemAttrDelete.Click += new EventHandler(this.tsmiAttrDelete_Click);
    this.tpLink.Controls.Add((Control) this.lvLinkAttrs);
    componentResourceManager.ApplyResources((object) this.tpLink, "tpLink");
    this.tpLink.Name = "tpLink";
    this.tpLink.UseVisualStyleBackColor = true;
    this.lvLinkAttrs.Columns.AddRange(new ColumnHeader[2]
    {
      this.chLinkAttrFullName,
      this.chLinkAttrShortName
    });
    this.lvLinkAttrs.ContextMenuStrip = this.cmLinkAttributes;
    componentResourceManager.ApplyResources((object) this.lvLinkAttrs, "lvLinkAttrs");
    this.lvLinkAttrs.FullRowSelect = true;
    this.lvLinkAttrs.GridLines = true;
    this.lvLinkAttrs.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.lvLinkAttrs.HideSelection = false;
    this.lvLinkAttrs.MultiSelect = false;
    this.lvLinkAttrs.Name = "lvLinkAttrs";
    this.lvLinkAttrs.UseCompatibleStateImageBehavior = false;
    this.lvLinkAttrs.View = View.Details;
    this.lvLinkAttrs.SelectedIndexChanged += new EventHandler(this.lvLinkAttrs_SelectedIndexChanged);
    this.lvLinkAttrs.DoubleClick += new EventHandler(this.lvLinkAttrs_DoubleClick);
    componentResourceManager.ApplyResources((object) this.chLinkAttrFullName, "chLinkAttrFullName");
    componentResourceManager.ApplyResources((object) this.chLinkAttrShortName, "chLinkAttrShortName");
    this.cmLinkAttributes.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.tsmiLinkAttrAdd,
      (ToolStripItem) this.tsmiLinkAttrDelete
    });
    this.cmLinkAttributes.Name = "cmAttributes";
    componentResourceManager.ApplyResources((object) this.cmLinkAttributes, "cmLinkAttributes");
    this.cmLinkAttributes.Opening += new CancelEventHandler(this.cmLinkAttributes_Opening);
    this.tsmiLinkAttrAdd.Name = "tsmiLinkAttrAdd";
    componentResourceManager.ApplyResources((object) this.tsmiLinkAttrAdd, "tsmiLinkAttrAdd");
    this.tsmiLinkAttrAdd.Click += new EventHandler(this.tsmiLinkAttrAdd_Click);
    this.tsmiLinkAttrDelete.Name = "tsmiLinkAttrDelete";
    componentResourceManager.ApplyResources((object) this.tsmiLinkAttrDelete, "tsmiLinkAttrDelete");
    this.tsmiLinkAttrDelete.Click += new EventHandler(this.tsmiLinkAttrDelete_Click);
    this.pnlRouteStringParam.Controls.Add((Control) this.tbxSeparator);
    this.pnlRouteStringParam.Controls.Add((Control) this.tbxTemplate);
    this.pnlRouteStringParam.Controls.Add((Control) this.lblSeparator);
    this.pnlRouteStringParam.Controls.Add((Control) this.lblTemplate);
    componentResourceManager.ApplyResources((object) this.pnlRouteStringParam, "pnlRouteStringParam");
    this.pnlRouteStringParam.Name = "pnlRouteStringParam";
    componentResourceManager.ApplyResources((object) this.tbxSeparator, "tbxSeparator");
    this.tbxSeparator.Name = "tbxSeparator";
    this.tbxSeparator.TextChanged += new EventHandler(this.tbxTemplate_TextChanged_1);
    componentResourceManager.ApplyResources((object) this.tbxTemplate, "tbxTemplate");
    this.tbxTemplate.Name = "tbxTemplate";
    this.tbxTemplate.TextChanged += new EventHandler(this.tbxTemplate_TextChanged_1);
    componentResourceManager.ApplyResources((object) this.lblSeparator, "lblSeparator");
    this.lblSeparator.Name = "lblSeparator";
    componentResourceManager.ApplyResources((object) this.lblTemplate, "lblTemplate");
    this.lblTemplate.Name = "lblTemplate";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitContainer1);
    this.Name = nameof (CehRoutesStringControl);
    this.Tag = (object) " ";
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.cmObjectTypes.ResumeLayout(false);
    this.pnlTemplInfo.ResumeLayout(false);
    this.pnlTemplInfo.PerformLayout();
    this.tbAttrMode.ResumeLayout(false);
    this.tpRouteElem.ResumeLayout(false);
    this.cmElemAttributes.ResumeLayout(false);
    this.tpLink.ResumeLayout(false);
    this.cmLinkAttributes.ResumeLayout(false);
    this.pnlRouteStringParam.ResumeLayout(false);
    this.pnlRouteStringParam.PerformLayout();
    this.ResumeLayout(false);
  }

  private class CehRoutesItemComparer : IComparer
  {
    public int Compare(object x, object y)
    {
      if (x == y)
        return 0;
      if (!(x is TreeNode treeNode1))
        return -1;
      return !(y is TreeNode treeNode2) ? 1 : ((ICehRouteStringTemplItem) treeNode1.Tag).OrderID - ((ICehRouteStringTemplItem) treeNode2.Tag).OrderID;
    }
  }
}
