// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.InventorySettingsControl
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors.Controls;
using DevExpress.IM.XtraEditors.Repository;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Archives.Common;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Copies;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>Закладка в Настройках - Настройки ОТД</summary>
public class InventorySettingsControl : UserControl, IPropertyPage, IPropertyPageSearchOptionEvents
{
  /// <summary>указывает, инициализирован ли контрол</summary>
  private bool initialized;
  /// <summary>для хранения изменённых формул для типов объектов</summary>
  private Dictionary<int, string> inventoryDictionary = new Dictionary<int, string>();
  /// <summary>
  /// для хранения формул для типов объектов, считанных из настроек
  /// </summary>
  private Dictionary<int, string> settingsDictionary = new Dictionary<int, string>();
  /// <summary>админ ли наш пользователь</summary>
  private bool isAdmin;
  /// <summary>изменился ли набор классифкаторов</summary>
  private bool isClassifierChange;
  /// <summary>флаг того, что на форме произошли изменения</summary>
  private bool _changed;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private CheckBox cbAutoGeneration;
  private CheckBox cbEmailNotify;
  private TreeList tlObjectTypes;
  private TreeListColumn colObjectTypeName;
  private TreeListColumn colObjectTypeID;
  private TreeListColumn colFormula;
  private RepositoryItemButtonEdit repositoryItemButtonEdit1;
  private TreeListColumn colInherited;
  private RepositoryItemCheckEdit repositoryItemCheckEdit1;
  private RepositoryItemCheckEdit repositoryItemCheckEdit2;
  private ToolTip toolTip1;
  private iGCellStyle iGrid1Col0CellStyle;
  private iGColHdrStyle iGrid1Col0ColHdrStyle;
  private iGCellStyle igClassifiersCol1CellStyle;
  private iGColHdrStyle igClassifiersCol1ColHdrStyle;
  private CheckBox cbAutoCreateCopy;
  private Label label2;
  private CheckBox cbSubscrNotify;
  private CheckBox cbIsRecipientReturnCopy;
  private SplitContainer splitContainer1;
  private Panel pnClassifier;
  private iGrid igClassifiers;
  private Button btnRemove;
  private CheckBox cbUseClassifiers;
  private Button btnAdd;
  private ComboBox cbLevel;
  private CheckBox cbAllowSendCopyWithoutReturning;

  /// <summary>закладка с настройками отд</summary>
  public InventorySettingsControl()
  {
    this.InitializeComponent();
    this.Visible = false;
    this.initialized = false;
  }

  /// <summary>загрузить информацию</summary>
  private void LoadSettings()
  {
    if (this.initialized)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBConfigurations service = ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
      sessionKeeper.Session.GetCustomService(typeof (ICopiesService));
      this.cbAutoGeneration.Checked = service.ReadStringNoCache(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.AUTOGENERATION, true) == "True";
      if (this.cbAutoGeneration.Checked)
        this.tlObjectTypes.BehaviorOptions |= BehaviorOptionsFlags.Editable;
      else
        this.tlObjectTypes.BehaviorOptions &= ~BehaviorOptionsFlags.Editable;
      this.cbEmailNotify.Checked = service.ReadStringNoCache(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.EMAIL_NOTIFY, true) == "True";
      this.cbIsRecipientReturnCopy.Checked = service.ReadStringNoCache(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.RECIPIENT_RETURN_COPY, true) == "True";
      this.cbSubscrNotify.Checked = service.ReadStringNoCache(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.SUBSCR_NOTIFY, true) == "True";
      this.cbUseClassifiers.Checked = service.ReadStringNoCache(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.USE_CLASSIFIERS, true) == "True";
      this.tlObjectTypes.Nodes.Clear();
      this.igClassifiers.Rows.Clear();
      this.igClassifiers.Rows.AutoHeight();
      this.isAdmin = sessionKeeper.Session.IsAdmin;
      this.cbAutoCreateCopy.Enabled = this.cbLevel.Enabled = this.cbAutoGeneration.Enabled = this.cbEmailNotify.Enabled = this.cbSubscrNotify.Enabled = this.pnClassifier.Enabled = this.isAdmin;
      this.tlObjectTypes.BehaviorOptions = this.isAdmin ? BehaviorOptionsFlags.Editable : BehaviorOptionsFlags.None;
      this.cbAutoCreateCopy.Checked = service.ReadBool(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.AUTO_CREATE_COPY, false, DBConfigMode.GlobalOnly);
      this.cbAllowSendCopyWithoutReturning.Checked = service.ReadBool(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.ALLOW_SEND_COPIES, false, DBConfigMode.GlobalOnly);
      DataTable dataTable = sessionKeeper.Session.GetLifecycleLevelCollection(true).Select("F_LEVEL_NAME");
      this.cbLevel.Items.Clear();
      int int32_1 = Convert.ToInt32(service.ReadInteger(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.LEVEL, 0L, DBConfigMode.GlobalOnly));
      int num1 = 0;
      int num2 = 0;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        int int32_2 = Convert.ToInt32(row["F_LEVEL_ID"]);
        this.cbLevel.Items.Add((object) new InventorySettingsControl.LevelItem(Convert.ToString(row["F_LEVEL_NAME"]), int32_2));
        if (int32_1 == int32_2)
          num1 = num2;
        ++num2;
      }
      this.cbLevel.SelectedIndex = num1;
    }
    this.inventoryDictionary.Clear();
    this.tlObjectTypes.BeginUpdate();
    try
    {
      this.FillDocTypeTree(ConstsHolder.DocTypeID, (TreeListNode) null, string.Empty);
      this.tlObjectTypes.Nodes[0].Expanded = true;
    }
    finally
    {
      this.tlObjectTypes.EndUpdate();
    }
    this.FillClassifierList();
    this.initialized = true;
    this._changed = false;
  }

  /// <summary>
  /// заполняем дерево типами, унаследованными от документов
  /// </summary>
  /// <param name="typeID">тип объектов</param>
  /// <param name="rootNode">родительский узел</param>
  /// <param name="rootFormula">формула для родительского типа объекта</param>
  private void FillDocTypeTree(int typeID, TreeListNode rootNode, string rootFormula)
  {
    string objectTypeName = MetaDataHelper.GetObjectTypeName(typeID);
    bool flag = true;
    string rootFormula1 = rootFormula;
    object formula = ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (ICopiesService)) as ICopiesService).GetFormula(typeID);
    if (formula != null)
    {
      flag = false;
      rootFormula1 = formula.ToString();
    }
    TreeListNode rootNode1 = this.tlObjectTypes.AppendNode((object) new object[4]
    {
      (object) objectTypeName,
      (object) typeID,
      (object) rootFormula1,
      (object) flag
    }, rootNode);
    rootNode1.ImageIndex = rootNode1.SelectImageIndex = Statics.IconSrv.IndexOf(4, typeID);
    List<int> objectTypeChildrenId = MetaDataHelper.GetObjectTypeChildrenID(typeID);
    List<IMSObjectType> imsChildrenIDs = new List<IMSObjectType>(objectTypeChildrenId.Count);
    objectTypeChildrenId.ForEach((Action<int>) (item => imsChildrenIDs.Add(MetaDataHelper.GetObjectType(item))));
    imsChildrenIDs.Sort();
    foreach (IMSObjectType imsObjectType in imsChildrenIDs)
      this.FillDocTypeTree(imsObjectType.ObjectTypeID, rootNode1, rootFormula1);
  }

  /// <summary>Событие об изменении свойств на странице</summary>
  public event EventHandler Changed;

  /// <summary>Событие об изменении свойств на странице</summary>
  private void OnChanged()
  {
    EventHandler changed = this.Changed;
    if (changed != null)
      changed((object) this, new EventArgs());
    this._changed = true;
  }

  /// <summary>Тип страницы</summary>
  public PropertyPageType Type => PropertyPageType.Control;

  /// <summary>Объект для отображения свойств</summary>
  public object Control => (object) this;

  /// <summary>Имя страницы</summary>
  public string PageName => ServiceHolder.rm.GetString("Archives_130");

  /// <summary>
  /// Текст заголовка (пустое значение - заголовок не отображается)
  /// </summary>
  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  /// <summary>Сохранение изменений</summary>
  public void Apply()
  {
    if (!this._changed)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.IsAdmin)
      {
        IDBConfigurations service = ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
        service.WriteBool(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.AUTOGENERATION, this.cbAutoGeneration.Checked, 0L);
        service.WriteBool(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.EMAIL_NOTIFY, this.cbEmailNotify.Checked, 0L);
        service.WriteBool(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.RECIPIENT_RETURN_COPY, this.cbIsRecipientReturnCopy.Checked, 0L);
        service.WriteBool(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.SUBSCR_NOTIFY, this.cbSubscrNotify.Checked, 0L);
        service.WriteBool(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.USE_CLASSIFIERS, this.cbUseClassifiers.Checked, 0L);
        ICopiesService customService = sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) as ICopiesService;
        if (this.inventoryDictionary.Count != 0)
          customService.ChangeFormula(this.inventoryDictionary, (object) sessionKeeper.Session.SessionGUID);
        if (this.isClassifierChange)
        {
          List<long> classifiersID = new List<long>();
          foreach (iGRow row in (IEnumerable) this.igClassifiers.Rows)
            classifiersID.Add(Convert.ToInt64(row.Cells[0].Value));
          customService.ChangeClassifiers(classifiersID, (object) sessionKeeper.Session.SessionGUID);
          this.isClassifierChange = false;
        }
        service.WriteBool(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.AUTO_CREATE_COPY, this.cbAutoCreateCopy.Checked, 0L);
        service.WriteBool(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.ALLOW_SEND_COPIES, this.cbAllowSendCopyWithoutReturning.Checked, 0L);
        service.WriteInteger(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.LEVEL, (long) (this.cbLevel.SelectedItem as InventorySettingsControl.LevelItem).ID, 0L);
      }
    }
    this._changed = false;
  }

  /// <summary>Отмена изменений</summary>
  public void Cancel()
  {
    this.initialized = false;
    this._changed = false;
  }

  /// <summary>id раздела справки для данного элемента управления</summary>
  public string HelpTopicID => "2646";

  /// <summary>
  /// Возвращает список имен настроек, содержащихся в контроле
  /// </summary>
  public List<string> GetOptionNames()
  {
    return !(this.Control is System.Windows.Forms.Control control) ? new List<string>() : IPropertyPageHelper.GetOptionNames(control);
  }

  /// <summary>изменение автогенерации инвентарного номера</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbAutoGeneration_CheckedChanged(object sender, EventArgs e)
  {
    if (this.cbAutoGeneration.Checked)
      this.tlObjectTypes.BehaviorOptions |= BehaviorOptionsFlags.Editable;
    else
      this.tlObjectTypes.BehaviorOptions &= ~BehaviorOptionsFlags.Editable;
    if (!this.initialized)
      return;
    this.OnChanged();
  }

  /// <summary>изменение рассылки уведомлений</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbEmailNotify_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.initialized)
      return;
    this.OnChanged();
  }

  /// <summary>изменеие формулы для типа объектов</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tlObjectTypes_CellValueChanged(object sender, CellValueChangedEventArgs e)
  {
    this.OnChanged();
    int int32 = Convert.ToInt32(e.Node[(object) "TYPE_ID"]);
    string formula = e.Node[(object) "FORMULA"].ToString();
    e.Node[(object) "INHERITED"] = (object) false;
    if (this.inventoryDictionary.ContainsKey(int32))
      this.inventoryDictionary[int32] = formula;
    else
      this.inventoryDictionary.Add(int32, formula);
    this.ChangeNodeFormula(e.Node, formula);
  }

  /// <summary>Изменить формулу у всех дочерних типов объектов</summary>
  /// <param name="parentNode"></param>
  /// <param name="formula"></param>
  private void ChangeNodeFormula(TreeListNode parentNode, string formula)
  {
    foreach (TreeListNode node in parentNode.Nodes)
    {
      if (Convert.ToBoolean(node[(object) "INHERITED"]))
      {
        node[(object) "FORMULA"] = (object) formula;
        this.ChangeNodeFormula(node, formula);
      }
    }
  }

  /// <summary>заполняем таблицу классификаторами</summary>
  private void FillClassifierList()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long classifier in (sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) as ICopiesService).Classifiers)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(classifier, false);
        if (dbObject != null)
        {
          iGRow iGrow = this.igClassifiers.Rows.Add();
          iGrow.Cells[0].Value = (object) dbObject.ObjectID;
          iGrow.Cells[1].Value = (object) dbObject.NameInMessages;
          iGrow.Cells[0].ImageIndex = Statics.IconSrv.IndexOf(4, dbObject.ObjectType);
        }
      }
      if (this.igClassifiers.Rows.Count <= 0)
        return;
      this.igClassifiers.Cells[0, 0].Selected = true;
      this.igClassifiers.SelectedCells[0].Row.EnsureVisible();
      this.igClassifiers.SetCurRow(this.igClassifiers.SelectedCells[0].Row.Index);
    }
  }

  /// <summary>Изменился параметр - использовать классификаторы</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbClassifier_CheckStateChanged(object sender, EventArgs e)
  {
    if (!this.initialized)
      return;
    this.OnChanged();
  }

  /// <summary>добавляем классификатор</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnAdd_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      Intermech.Navigator.CustomNode.Descriptor rootDescriptor = new Intermech.Navigator.CustomNode.Descriptor(ServiceHolder.rm.GetString("Archives_131"), new DescriptorCollection()
      {
        (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID(new Guid("cad0014e-306c-11d8-b4e9-00304f19f545")))
      });
      if (!(SelectionWindow.Select(ServiceHolder.rm.GetString("Archives_132"), (IDescriptor) rootDescriptor, typeof (IDBObjectID), SelectionOptions.SelectObjects) is IDBObjectID[] dbObjectIdArray) || dbObjectIdArray.Length == 0)
        return;
      foreach (IDBObjectID dbObjectId in dbObjectIdArray)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(dbObjectId.Value, false);
        if (dbObject != null)
        {
          iGRow iGrow = this.igClassifiers.Rows.Add();
          iGrow.Cells[1].Value = (object) dbObject.NameInMessages;
          iGrow.Cells[0].Value = (object) dbObject.ObjectID;
          iGrow.Cells[0].ImageIndex = Statics.IconSrv.IndexOf(4, dbObject.ObjectType);
        }
      }
      this.OnChanged();
      this.isClassifierChange = true;
    }
  }

  /// <summary>удаляем классификатор</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnRemove_Click(object sender, EventArgs e)
  {
    if (this.igClassifiers.SelectedCells.Count == 0)
      return;
    List<int> intList = new List<int>(this.igClassifiers.SelectedCells.Count);
    for (int index = 0; index < this.igClassifiers.SelectedCells.Count; ++index)
    {
      iGRow row = this.igClassifiers.SelectedCells[index].Row;
      if (!intList.Contains(row.Index))
        intList.Add(row.Index);
    }
    intList.Sort();
    for (int index = intList.Count - 1; index >= 0; --index)
      this.igClassifiers.Rows.RemoveAt(intList[index]);
    this.OnChanged();
    this.isClassifierChange = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void iGrid1_SelectionChanged(object sender, EventArgs e)
  {
    if (!this.isAdmin || this.igClassifiers.SelectedCells.Count == 0)
      return;
    this.btnRemove.Enabled = true;
  }

  private void InventorySettingsControl_VisibleChanged(object sender, EventArgs e)
  {
    if (this.Parent == null || !this.Visible)
      return;
    this.tlObjectTypes.StateImageList = this.tlObjectTypes.SelectImageList = Statics.IconSrv.ImageList;
    this.igClassifiers.ImageList = Statics.IconSrv.ImageList;
    this.tlObjectTypes.Columns["TYPE_NAME"].Options = ColumnOptions.None;
    this.LoadSettings();
  }

  private void tlObjectTypes_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
  {
    if (!Convert.ToBoolean(e.Node[(object) "INHERITED"]))
      return;
    Color color1 = this.tlObjectTypes.FocusedNode == e.Node ? SystemColors.Highlight : Color.Beige;
    Color color2 = this.tlObjectTypes.FocusedNode == e.Node ? SystemColors.Highlight : Color.White;
    using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(e.Bounds, color1, color2, LinearGradientMode.Horizontal))
    {
      e.Graphics.FillRectangle((Brush) linearGradientBrush, e.Bounds);
      this.PaintText(e.Graphics, e.Bounds, e.CellText);
      e.Handled = true;
    }
  }

  private void PaintText(Graphics g, Rectangle bounds, string text)
  {
    using (SolidBrush solidBrush = new SolidBrush(Color.Black))
      g.DrawString(text, new Font("Verdana", 8f), (Brush) solidBrush, (RectangleF) bounds, new StringFormat()
      {
        LineAlignment = StringAlignment.Center,
        FormatFlags = StringFormatFlags.NoWrap,
        Trimming = StringTrimming.EllipsisCharacter
      });
  }

  private void cbAutoCreateCopy_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.initialized)
      return;
    this.OnChanged();
  }

  private void cbLevel_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (!this.initialized)
      return;
    this.OnChanged();
  }

  private void cbSubscrNotify_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.initialized)
      return;
    this.OnChanged();
  }

  private void cbIsRecipientReturnCopy_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.initialized)
      return;
    this.OnChanged();
  }

  private void cbAllowSendCopyWithoutReturning_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.initialized)
      return;
    this.OnChanged();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (InventorySettingsControl));
    iGColPattern iGcolPattern1 = new iGColPattern();
    iGColPattern iGcolPattern2 = new iGColPattern();
    this.splitContainer1 = new SplitContainer();
    this.cbIsRecipientReturnCopy = new CheckBox();
    this.cbAutoGeneration = new CheckBox();
    this.cbSubscrNotify = new CheckBox();
    this.cbEmailNotify = new CheckBox();
    this.tlObjectTypes = new TreeList();
    this.repositoryItemCheckEdit2 = new RepositoryItemCheckEdit();
    this.pnClassifier = new Panel();
    this.cbLevel = new ComboBox();
    this.igClassifiers = new iGrid();
    this.iGrid1Col0CellStyle = new iGCellStyle(true);
    this.iGrid1Col0ColHdrStyle = new iGColHdrStyle(true);
    this.igClassifiersCol1CellStyle = new iGCellStyle(true);
    this.igClassifiersCol1ColHdrStyle = new iGColHdrStyle(true);
    this.cbAutoCreateCopy = new CheckBox();
    this.btnRemove = new Button();
    this.label2 = new Label();
    this.cbUseClassifiers = new CheckBox();
    this.btnAdd = new Button();
    this.toolTip1 = new ToolTip(this.components);
    this.cbAllowSendCopyWithoutReturning = new CheckBox();
    this.colObjectTypeName = new TreeListColumn();
    this.colObjectTypeID = new TreeListColumn();
    this.colFormula = new TreeListColumn();
    this.colInherited = new TreeListColumn();
    this.repositoryItemButtonEdit1 = new RepositoryItemButtonEdit();
    this.repositoryItemCheckEdit1 = new RepositoryItemCheckEdit();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.tlObjectTypes.BeginInit();
    this.repositoryItemCheckEdit2.BeginInit();
    this.pnClassifier.SuspendLayout();
    ((ISupportInitialize) this.igClassifiers).BeginInit();
    this.repositoryItemButtonEdit1.BeginInit();
    this.repositoryItemCheckEdit1.BeginInit();
    this.SuspendLayout();
    this.splitContainer1.BackColor = SystemColors.ActiveBorder;
    componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
    this.splitContainer1.Name = "splitContainer1";
    componentResourceManager.ApplyResources((object) this.splitContainer1.Panel1, "splitContainer1.Panel1");
    this.splitContainer1.Panel1.BackColor = SystemColors.Control;
    this.splitContainer1.Panel1.Controls.Add((System.Windows.Forms.Control) this.cbAllowSendCopyWithoutReturning);
    this.splitContainer1.Panel1.Controls.Add((System.Windows.Forms.Control) this.cbIsRecipientReturnCopy);
    this.splitContainer1.Panel1.Controls.Add((System.Windows.Forms.Control) this.cbAutoGeneration);
    this.splitContainer1.Panel1.Controls.Add((System.Windows.Forms.Control) this.cbSubscrNotify);
    this.splitContainer1.Panel1.Controls.Add((System.Windows.Forms.Control) this.cbEmailNotify);
    this.splitContainer1.Panel1.Controls.Add((System.Windows.Forms.Control) this.tlObjectTypes);
    componentResourceManager.ApplyResources((object) this.splitContainer1.Panel2, "splitContainer1.Panel2");
    this.splitContainer1.Panel2.BackColor = SystemColors.Control;
    this.splitContainer1.Panel2.Controls.Add((System.Windows.Forms.Control) this.pnClassifier);
    componentResourceManager.ApplyResources((object) this.cbIsRecipientReturnCopy, "cbIsRecipientReturnCopy");
    this.cbIsRecipientReturnCopy.Name = "cbIsRecipientReturnCopy";
    this.cbIsRecipientReturnCopy.UseVisualStyleBackColor = true;
    this.cbIsRecipientReturnCopy.CheckedChanged += new EventHandler(this.cbIsRecipientReturnCopy_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbAutoGeneration, "cbAutoGeneration");
    this.cbAutoGeneration.Name = "cbAutoGeneration";
    this.cbAutoGeneration.UseVisualStyleBackColor = true;
    this.cbAutoGeneration.CheckedChanged += new EventHandler(this.cbAutoGeneration_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbSubscrNotify, "cbSubscrNotify");
    this.cbSubscrNotify.Name = "cbSubscrNotify";
    this.cbSubscrNotify.UseVisualStyleBackColor = true;
    this.cbSubscrNotify.CheckedChanged += new EventHandler(this.cbSubscrNotify_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbEmailNotify, "cbEmailNotify");
    this.cbEmailNotify.Name = "cbEmailNotify";
    this.cbEmailNotify.UseVisualStyleBackColor = true;
    this.cbEmailNotify.CheckedChanged += new EventHandler(this.cbEmailNotify_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.tlObjectTypes, "tlObjectTypes");
    this.tlObjectTypes.Columns.AddRange(new TreeListColumn[4]
    {
      this.colObjectTypeName,
      this.colObjectTypeID,
      this.colFormula,
      this.colInherited
    });
    this.tlObjectTypes.Name = "tlObjectTypes";
    this.tlObjectTypes.RepositoryItems.AddRange(new RepositoryItem[2]
    {
      (RepositoryItem) this.repositoryItemButtonEdit1,
      (RepositoryItem) this.repositoryItemCheckEdit1
    });
    this.tlObjectTypes.Styles.AddReplace("InheritedStyle", (object) new ViewStyle("InheritedStyle", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Info, SystemColors.WindowText));
    this.toolTip1.SetToolTip((System.Windows.Forms.Control) this.tlObjectTypes, componentResourceManager.GetString("tlObjectTypes.ToolTip"));
    this.tlObjectTypes.CustomDrawNodeCell += new CustomDrawNodeCellEventHandler(this.tlObjectTypes_CustomDrawNodeCell);
    this.tlObjectTypes.CellValueChanged += new CellValueChangedEventHandler(this.tlObjectTypes_CellValueChanged);
    this.repositoryItemCheckEdit2.AutoHeight = false;
    this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
    componentResourceManager.ApplyResources((object) this.pnClassifier, "pnClassifier");
    this.pnClassifier.BackColor = SystemColors.Control;
    this.pnClassifier.Controls.Add((System.Windows.Forms.Control) this.cbLevel);
    this.pnClassifier.Controls.Add((System.Windows.Forms.Control) this.igClassifiers);
    this.pnClassifier.Controls.Add((System.Windows.Forms.Control) this.cbAutoCreateCopy);
    this.pnClassifier.Controls.Add((System.Windows.Forms.Control) this.btnRemove);
    this.pnClassifier.Controls.Add((System.Windows.Forms.Control) this.label2);
    this.pnClassifier.Controls.Add((System.Windows.Forms.Control) this.cbUseClassifiers);
    this.pnClassifier.Controls.Add((System.Windows.Forms.Control) this.btnAdd);
    this.pnClassifier.Name = "pnClassifier";
    this.cbLevel.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbLevel.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this.cbLevel, "cbLevel");
    this.cbLevel.Name = "cbLevel";
    this.cbLevel.SelectedIndexChanged += new EventHandler(this.cbLevel_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.igClassifiers, "igClassifiers");
    this.igClassifiers.AutoResizeCols = true;
    iGcolPattern1.CellStyle = this.iGrid1Col0CellStyle;
    iGcolPattern1.ColHdrStyle = this.iGrid1Col0ColHdrStyle;
    iGcolPattern1.SortOrder = iGSortOrder.None;
    componentResourceManager.ApplyResources((object) iGcolPattern1, "iGColPattern1");
    iGcolPattern2.CellStyle = this.igClassifiersCol1CellStyle;
    iGcolPattern2.ColHdrStyle = this.igClassifiersCol1ColHdrStyle;
    componentResourceManager.ApplyResources((object) iGcolPattern2, "iGColPattern2");
    this.igClassifiers.Cols.AddRange(new iGColPattern[2]
    {
      iGcolPattern1,
      iGcolPattern2
    });
    this.igClassifiers.Header.Height = (int) componentResourceManager.GetObject("igClassifiers.Header.Height");
    this.igClassifiers.Name = "igClassifiers";
    this.igClassifiers.ReadOnly = true;
    this.igClassifiers.RowMode = true;
    this.igClassifiers.SelectionMode = iGSelectionMode.MultiExtended;
    this.igClassifiers.SingleClickEdit = true;
    this.igClassifiers.SelectionChanged += new EventHandler(this.iGrid1_SelectionChanged);
    componentResourceManager.ApplyResources((object) this.cbAutoCreateCopy, "cbAutoCreateCopy");
    this.cbAutoCreateCopy.Name = "cbAutoCreateCopy";
    this.cbAutoCreateCopy.UseVisualStyleBackColor = true;
    this.cbAutoCreateCopy.CheckedChanged += new EventHandler(this.cbAutoCreateCopy_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.btnRemove, "btnRemove");
    this.btnRemove.Name = "btnRemove";
    this.toolTip1.SetToolTip((System.Windows.Forms.Control) this.btnRemove, componentResourceManager.GetString("btnRemove.ToolTip"));
    this.btnRemove.UseVisualStyleBackColor = true;
    this.btnRemove.Click += new EventHandler(this.btnRemove_Click);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.cbUseClassifiers, "cbUseClassifiers");
    this.cbUseClassifiers.Name = "cbUseClassifiers";
    this.cbUseClassifiers.UseVisualStyleBackColor = true;
    this.cbUseClassifiers.CheckStateChanged += new EventHandler(this.cbClassifier_CheckStateChanged);
    componentResourceManager.ApplyResources((object) this.btnAdd, "btnAdd");
    this.btnAdd.Name = "btnAdd";
    this.toolTip1.SetToolTip((System.Windows.Forms.Control) this.btnAdd, componentResourceManager.GetString("btnAdd.ToolTip"));
    this.btnAdd.UseVisualStyleBackColor = true;
    this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
    componentResourceManager.ApplyResources((object) this.cbAllowSendCopyWithoutReturning, "cbAllowSendCopyWithoutReturning");
    this.cbAllowSendCopyWithoutReturning.Name = "cbAllowSendCopyWithoutReturning";
    this.cbAllowSendCopyWithoutReturning.UseVisualStyleBackColor = true;
    this.cbAllowSendCopyWithoutReturning.CheckedChanged += new EventHandler(this.cbAllowSendCopyWithoutReturning_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.colObjectTypeName, "colObjectTypeName");
    this.colObjectTypeName.Name = "colObjectTypeName";
    componentResourceManager.ApplyResources((object) this.colObjectTypeID, "colObjectTypeID");
    this.colObjectTypeID.Name = "colObjectTypeID";
    componentResourceManager.ApplyResources((object) this.colFormula, "colFormula");
    this.colFormula.Name = "colFormula";
    this.colInherited.ColumnEdit = (RepositoryItem) this.repositoryItemCheckEdit2;
    componentResourceManager.ApplyResources((object) this.colInherited, "colInherited");
    this.colInherited.Name = "colInherited";
    this.repositoryItemButtonEdit1.AutoHeight = false;
    this.repositoryItemButtonEdit1.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
    this.repositoryItemCheckEdit1.AutoHeight = false;
    this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((System.Windows.Forms.Control) this.splitContainer1);
    this.Name = nameof (InventorySettingsControl);
    this.VisibleChanged += new EventHandler(this.InventorySettingsControl_VisibleChanged);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel1.PerformLayout();
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.tlObjectTypes.EndInit();
    this.repositoryItemCheckEdit2.EndInit();
    this.pnClassifier.ResumeLayout(false);
    this.pnClassifier.PerformLayout();
    ((ISupportInitialize) this.igClassifiers).EndInit();
    this.repositoryItemButtonEdit1.EndInit();
    this.repositoryItemCheckEdit1.EndInit();
    this.ResumeLayout(false);
  }

  private class LevelItem
  {
    public string Name { get; private set; }

    public int ID { get; private set; }

    public LevelItem(string name, int id)
    {
      this.Name = name;
      this.ID = id;
    }

    public override string ToString() => this.Name;
  }
}
