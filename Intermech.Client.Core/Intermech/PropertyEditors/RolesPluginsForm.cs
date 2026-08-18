
// Type: Intermech.PropertyEditors.RolesPluginsForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Форма по настройке списка плагинов для ролей</summary>
public sealed class RolesPluginsForm : Form
{
  /// <summary>
  /// Режим работы формы
  /// 0 -	редактирование
  /// 1 - просмотр
  /// </summary>
  public int _editorMode;
  /// <summary>
  /// Где размещена наша форма
  /// 0 - самостоятельная форма (по дефолту)
  /// 1 - на форме-создателе новых объектов
  /// 2 - на вьюшке "Навигатора"
  /// </summary>
  public int ParentMode;
  /// <summary>Были ли изменения в дополнительных настройках роли</summary>
  public bool IsChanged;
  /// <summary>ID выделенных объектов</summary>
  public ArrayList RoleObjectIDs = new ArrayList();
  /// <summary>Название выделенных объектов</summary>
  public string RoleObjectName = "";
  /// <summary>
  /// Название базовой роли (если выделено несколько ролей, то первая будет базовой,
  /// а её настройки будут загружены в редактор)
  /// </summary>
  public string BaseRoleObjectName = "";
  /// <summary>
  /// Выполняется ли работа внутри обработчиков событий, меняющих структуру дерева
  /// </summary>
  private bool _inEditor;
  /// <summary>Коллекция плагинов для базовой роли</summary>
  private List<MyElementEx> _plugins = new List<MyElementEx>();
  /// <summary>
  /// Кэш для быстрого поиска описаний плагинов [ID плагина] - [Описание плагина]
  /// </summary>
  private Dictionary<long, MyElementEx> _pluginsCache = new Dictionary<long, MyElementEx>();
  /// <summary>
  /// Варианты заголовка (для режимов редактирования и просмотра)
  /// </summary>
  internal string[] Headers = new string[2]
  {
    LocalizationHolder.rm.GetString("Client.Core_649"),
    LocalizationHolder.rm.GetString("Client.Core_649")
  };
  /// <summary>Коллекция изображений для разных категорий</summary>
  private ICategoryTypeIconService objtypesIcons;
  /// <summary>Тип объекта "Загружаемый модуль"</summary>
  private int pluginTypeID = -1;
  /// <summary>Тип объекта "Роль"</summary>
  private int roleTypeID = -1;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelBottom;
  private Label lbTooltip;
  private PictureBox imgTooltip;
  private Button _cancelButton;
  private Button _acceptButton;
  private TreeListColumn columnCaption;
  private Panel panelControls;
  private Button _deleteButton;
  private Button _addButton;
  private TreeListColumn columnFileName;
  private TreeList _treeList;

  /// <summary>Создать экземпляр формы-редактора</summary>
  public RolesPluginsForm()
  {
    this.InitializeComponent();
    this.objtypesIcons = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
    this.Size = new Size(workingArea.Width / 100 * 70, workingArea.Height / 100 * 60);
    this.Location = new Point((workingArea.Width - this.Size.Width) / 2, (workingArea.Height - this.Size.Height) / 2);
    this._treeList.SelectImageList = this.objtypesIcons.ImageList;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.pluginTypeID = sessionKeeper.Session.IdentHelper.PluginTypeID;
      this.roleTypeID = sessionKeeper.Session.IdentHelper.RolesTypeID;
    }
    this.UpdateControls();
  }

  /// <summary>Корректно назначить контрол-предок для формы</summary>
  /// <param name="aParent">Родительский оконный объект</param>
  public void SetParent(Control aParent)
  {
    if (aParent == null)
    {
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.TopLevel = true;
      this.Dock = DockStyle.None;
      this.FormBorderStyle = FormBorderStyle.Sizable;
      this.Visible = false;
    }
    else
    {
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.TopLevel = false;
      this.Dock = DockStyle.Fill;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Visible = true;
    }
    this.Parent = aParent;
    this.UpdateControls();
  }

  /// <summary>Загрузить данные объектов в форму</summary>
  /// <param name="AEditorMode">Режим редактирования (0 - редактор, 1 - просмотр)</param>
  public void LoadObjectData(int AEditorMode)
  {
    this._editorMode = AEditorMode;
    bool inEditor = this._inEditor;
    try
    {
      this._inEditor = true;
      if (this._editorMode < 0)
        this._editorMode = 1;
      if (this._editorMode >= this.Headers.Length)
        this._editorMode = 1;
      this.IsChanged = false;
      long num = 0;
      if (this.RoleObjectIDs.Count > 0)
        num = Convert.ToInt64(this.RoleObjectIDs[this.RoleObjectIDs.Count - 1]);
      if (num == 0L)
      {
        this.CreatePluginsTree();
        this.UpdateControls();
      }
      else
      {
        this.LoadPluginsRelations(ref this._plugins);
        this.CreatePluginsTree();
      }
    }
    finally
    {
      this._inEditor = inEditor;
      this.UpdateControls();
    }
  }

  /// <summary>Сохранить данные в объект с ID = RoleObjectID</summary>
  public void SaveObjectData()
  {
    long partObjectID = 0;
    if (this.RoleObjectIDs.Count > 0)
      partObjectID = Convert.ToInt64(this.RoleObjectIDs[this.RoleObjectIDs.Count - 1]);
    if (partObjectID == 0L)
      return;
    List<long> relationIDs = new List<long>();
    List<long> projIDs = new List<long>();
    List<int> relTypeIDs = new List<int>();
    List<long> longList = new List<long>();
    List<MyElementEx> myElementExList = new List<MyElementEx>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(sessionKeeper.Session.IdentHelper.SimpleRelationTypeID);
      for (int index = 0; index < this._plugins.Count; ++index)
      {
        MyElementEx plugin = this._plugins[index];
        if (plugin.ElementBool || plugin.ElementBool2)
        {
          if (plugin.ElementBool && (long) plugin.Tags[0] == -1L)
          {
            IDBRelation dbRelation = relationCollection.Create(plugin.ElementID64, partObjectID);
            plugin.ElementBool = false;
            plugin.Tags[0] = (object) dbRelation.RelationID;
            relationIDs.Add(dbRelation.RelationID);
            projIDs.Add(dbRelation.ProjID);
            relTypeIDs.Add(dbRelation.RelationType);
          }
          else if (plugin.ElementBool2)
          {
            if ((long) plugin.Tags[0] > 0L)
            {
              try
              {
                if (relationCollection.Delete(new long[1]
                {
                  (long) plugin.Tags[0]
                }, true, 0L) == 1)
                {
                  longList.Add((long) plugin.Tags[0]);
                  myElementExList.Add(plugin);
                }
              }
              catch
              {
                plugin.ElementBool2 = false;
                if ((DialogResult) IMMessageBox.ShowEx(LocalizationHolder.rm.GetString("Client.Core_1317"), string.Format(LocalizationHolder.rm.GetString("Client.Core_1496"), (object) plugin.Caption), new IMMessageBoxButton[2]
                {
                  new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1374"), DialogResult.OK, (object) DialogResult.OK),
                  new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1497"), DialogResult.Retry, (object) DialogResult.Retry)
                }, IMMessageBoxImage.Information) == DialogResult.Retry)
                  throw;
              }
            }
          }
        }
      }
      for (int index = 0; index < myElementExList.Count; ++index)
      {
        this._plugins.Remove(myElementExList[index]);
        this._pluginsCache.Remove(myElementExList[index].ElementID64);
      }
    }
    this.IsChanged = false;
    this.CreatePluginsTree();
    this.UpdateControls();
    INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    if (relationIDs.Count > 0)
    {
      DBRelationsEventArgs e = new DBRelationsEventArgs("RelationsCreated", (IList<long>) relationIDs, (IList<long>) projIDs, (IList<int>) null, (IList<int>) relTypeIDs);
      service.FireEvent((object) this, (NotificationEventArgs) e);
    }
    if (longList.Count <= 0)
      return;
    DBRelationsEventArgs e1 = new DBRelationsEventArgs("RelationsRemoved", (IList<long>) longList.ToArray());
    service.FireEvent((object) this, (NotificationEventArgs) e1);
  }

  private void TreeList_GetCustomNodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
  {
    if (e.Node == null || e.Column == null || !(e.Node.Tag is MyElementEx tag) || !tag.ElementBool)
      return;
    if (this._treeList.Selection.Contains(e.Node))
      e.Style = e.Node.TreeList.Styles["ChangedCellSelected"];
    else
      e.Style = e.Node.TreeList.Styles["ChangedCell"];
  }

  private void AddButton_Click(object sender, EventArgs e)
  {
    long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Client.Core_651"), LocalizationHolder.rm.GetString("Client.Core_652"), this.pluginTypeID, SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < numArray.Length; ++index)
      {
        if (!this._pluginsCache.ContainsKey(numArray[index]))
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(numArray[index], false);
          if (dbObject != null)
          {
            IDBAttribute attributeById = dbObject.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cad00127-306c-11d8-b4e9-00304f19f545"));
            long num = -1;
            MyElementEx myElementEx = new MyElementEx((object) (attributeById != null ? DataSetProcessor.GetStringValue(attributeById.Value, string.Empty) : string.Empty), dbObject.Caption, true, false, false, numArray[index], 0, Guid.Empty, new object[1]
            {
              (object) num
            });
            this._plugins.Add(myElementEx);
            this._pluginsCache[numArray[index]] = myElementEx;
            this.IsChanged = true;
          }
        }
      }
    }
    this.CreatePluginsTree();
    this.UpdateControls();
    this._treeList.Selection.Clear();
    if (this._treeList.Nodes.LastNode == null)
      return;
    this._treeList.Selection.Add(this._treeList.Nodes.LastNode);
    this._treeList.FocusedNode = this._treeList.Nodes.LastNode;
  }

  private void DeleteButton_Click(object sender, EventArgs e)
  {
    if (this._treeList.Selection.Count == 0)
      return;
    bool flag = false;
    int index1 = this._treeList.Selection.Cast<TreeListNode>().Select<TreeListNode, int>((System.Func<TreeListNode, int>) (o => this._treeList.Nodes.IndexOf(o))).OrderBy<int, int>((System.Func<int, int>) (o => o)).FirstOrDefault<int>() - 1;
    for (int index2 = 0; index2 < this._treeList.Selection.Count; ++index2)
    {
      MyElementEx tag = this._treeList.Selection[index2].Tag as MyElementEx;
      if (tag.ElementBool)
      {
        this._plugins.Remove(tag);
        this._pluginsCache.Remove(tag.ElementID64);
      }
      else
        tag.ElementBool2 = true;
    }
    for (int index3 = 0; index3 < this._plugins.Count; ++index3)
      flag = flag | this._plugins[index3].ElementBool | this._plugins[index3].ElementBool2;
    this.IsChanged = flag;
    this.CreatePluginsTree();
    this.UpdateControls();
    this._treeList.Selection.Clear();
    if (index1 < 0 || this._treeList.Nodes[index1] == null)
      return;
    this._treeList.Selection.Add(this._treeList.Nodes[index1]);
    this._treeList.FocusedNode = this._treeList.Nodes[index1];
  }

  private void AcceptButton_Click(object sender, EventArgs e)
  {
    if (this._editorMode != 0)
    {
      this.DialogResult = DialogResult.OK;
    }
    else
    {
      if (!this.IsChanged)
        return;
      this.SaveObjectData();
    }
  }

  private void CancelButton_Click(object sender, EventArgs e)
  {
    if (this.ParentMode == 1)
      return;
    if (this._editorMode == 1 && this.ParentMode == 0)
      this.DialogResult = DialogResult.Cancel;
    else if (this._editorMode == 0 && this.ParentMode == 0)
    {
      if (!this.IsChanged)
      {
        this.DialogResult = DialogResult.Cancel;
      }
      else
      {
        if (MessageBox.Show(RolesPluginsForm.RolesPluginsFormConsts.Dialog1, RolesPluginsForm.RolesPluginsFormConsts.Dialog2, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
          return;
        this.DialogResult = DialogResult.Cancel;
      }
    }
    else
    {
      if (this._editorMode != 0 || this.ParentMode != 2 || !this.IsChanged || MessageBox.Show(RolesPluginsForm.RolesPluginsFormConsts.Dialog1, RolesPluginsForm.RolesPluginsFormConsts.Dialog2, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
        return;
      this.LoadObjectData(this._editorMode);
    }
  }

  private void UpdateControls()
  {
    this._acceptButton.Enabled = this.ParentMode != 1 && this._editorMode == 0 && this.IsChanged;
    this._acceptButton.Visible = this.ParentMode != 1 && this._editorMode == 0;
    if (this.ParentMode == 0)
      this._acceptButton.Text = RolesPluginsForm.RolesPluginsFormConsts.ApplyText2;
    if (this.ParentMode == 2)
      this._acceptButton.Text = RolesPluginsForm.RolesPluginsFormConsts.ApplyText1;
    this._cancelButton.Visible = this.ParentMode != 1;
    this._cancelButton.Enabled = this._cancelButton.Visible && this.IsChanged;
    if (this._editorMode == 0)
      this._cancelButton.Text = RolesPluginsForm.RolesPluginsFormConsts.CancelText1;
    if (this._editorMode == 1)
      this._cancelButton.Text = RolesPluginsForm.RolesPluginsFormConsts.CancelText2;
    this.imgTooltip.Visible = this.RoleObjectIDs != null && this.RoleObjectIDs.Count > 1;
    this.lbTooltip.Text = string.Format(RolesPluginsForm.RolesPluginsFormConsts.Tooltip1, (object) this.BaseRoleObjectName);
    this.lbTooltip.Visible = this.imgTooltip.Visible;
  }

  private void CreatePluginsTree()
  {
    try
    {
      this._treeList.BeginUpdate();
      try
      {
        this._treeList.BeginSort();
        this._treeList.ClearNodes();
        for (int index = 0; index < this._plugins.Count; ++index)
          this.AddPluginItem(this._plugins[index]);
      }
      finally
      {
        this._treeList.EndSort();
      }
    }
    finally
    {
      this._treeList.EndUpdate();
    }
  }

  private TreeListNode AddPluginItem(MyElementEx plugin)
  {
    if (plugin == null || plugin.ElementBool2)
      return (TreeListNode) null;
    TreeListNode treeListNode = this._treeList.AppendNode((object) new object[2]
    {
      (object) plugin.Caption,
      plugin.Value
    }, (TreeListNode) null);
    if (this.objtypesIcons != null)
      treeListNode.ImageIndex = this.objtypesIcons.IndexOf(4, this.pluginTypeID);
    treeListNode.SelectImageIndex = treeListNode.ImageIndex;
    treeListNode.Tag = (object) plugin;
    return treeListNode;
  }

  /// <summary>Загрузить список плагинов для базовой роли</summary>
  /// <param name="pluginsList"></param>
  private void LoadPluginsRelations(ref List<MyElementEx> pluginsList)
  {
    if (pluginsList == null)
      pluginsList = new List<MyElementEx>();
    pluginsList.Clear();
    this._pluginsCache.Clear();
    long objectID = 0;
    if (this.RoleObjectIDs.Count > 0)
      objectID = Convert.ToInt64(this.RoleObjectIDs[this.RoleObjectIDs.Count - 1]);
    if (objectID == 0L)
      return;
    DataTable dataTable = (DataTable) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(sessionKeeper.Session.IdentHelper.SimpleRelationTypeID, "cad001e2-306c-11d8-b4e9-00304f19f545");
      if (relationCollection != null)
      {
        relationCollection.ObjectTypeID = MetaDataHelper.GetObjectTypeID("cad0005b-306c-11d8-b4e9-00304f19f545");
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectID);
        DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new List<ColumnDescriptor>()
        {
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PROJ_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
          new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
          new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad00127-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
        }.ToArray());
        dataTable = relationCollection.EntersIn(paramSet, dbObject.ID);
      }
    }
    if (dataTable == null)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64Value1 = DataSetProcessor.GetInt64Value(row, 0, 0L);
      long int64Value2 = DataSetProcessor.GetInt64Value(row, 1, 0L);
      int int32Value = DataSetProcessor.GetInt32Value(row, 2, -1);
      DataSetProcessor.GetStringValue(row, 3, string.Empty);
      string stringValue = DataSetProcessor.GetStringValue(row, 4, string.Empty);
      int pluginTypeId = this.pluginTypeID;
      if (MetaDataHelper.IsObjectTypeChildOf(int32Value, pluginTypeId))
      {
        MyElementEx myElementEx = new MyElementEx((object) stringValue, row[3].ToString(), false, false, false, int64Value2, 0, Guid.Empty, new object[1]
        {
          (object) int64Value1
        });
        pluginsList.Add(myElementEx);
        this._pluginsCache[int64Value2] = myElementEx;
      }
    }
    dataTable.Dispose();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RolesPluginsForm));
    this.panelBottom = new Panel();
    this.lbTooltip = new Label();
    this.imgTooltip = new PictureBox();
    this._cancelButton = new Button();
    this._acceptButton = new Button();
    this._treeList = new TreeList();
    this.columnCaption = new TreeListColumn();
    this.columnFileName = new TreeListColumn();
    this.panelControls = new Panel();
    this._deleteButton = new Button();
    this._addButton = new Button();
    this.panelBottom.SuspendLayout();
    ((ISupportInitialize) this.imgTooltip).BeginInit();
    this._treeList.BeginInit();
    this.panelControls.SuspendLayout();
    this.SuspendLayout();
    this.panelBottom.BorderStyle = BorderStyle.Fixed3D;
    this.panelBottom.Controls.Add((Control) this.lbTooltip);
    this.panelBottom.Controls.Add((Control) this.imgTooltip);
    this.panelBottom.Controls.Add((Control) this._cancelButton);
    this.panelBottom.Controls.Add((Control) this._acceptButton);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.lbTooltip, "lbTooltip");
    this.lbTooltip.Name = "lbTooltip";
    componentResourceManager.ApplyResources((object) this.imgTooltip, "imgTooltip");
    this.imgTooltip.Name = "imgTooltip";
    this.imgTooltip.TabStop = false;
    componentResourceManager.ApplyResources((object) this._cancelButton, "_cancelButton");
    this._cancelButton.Cursor = Cursors.Default;
    this._cancelButton.DialogResult = DialogResult.Cancel;
    this._cancelButton.Name = "_cancelButton";
    this._cancelButton.Click += new EventHandler(this.CancelButton_Click);
    componentResourceManager.ApplyResources((object) this._acceptButton, "_acceptButton");
    this._acceptButton.Cursor = Cursors.Default;
    this._acceptButton.Name = "_acceptButton";
    this._acceptButton.Click += new EventHandler(this.AcceptButton_Click);
    componentResourceManager.ApplyResources((object) this._treeList, "_treeList");
    this._treeList.CheckBoxes = CheckBoxesStyle.ThreeState;
    this._treeList.Columns.AddRange(new TreeListColumn[2]
    {
      this.columnCaption,
      this.columnFileName
    });
    this._treeList.Name = "treePlugins";
    this._treeList.Styles.AddReplace("HideSelectionRow", (object) new ViewStyle("HideSelectionRow", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Highlight, SystemColors.HighlightText));
    this._treeList.Styles.AddReplace("OddRow", (object) new ViewStyle("OddRow", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.None, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, Color.LightGreen, SystemColors.WindowText));
    this._treeList.Styles.AddReplace("ChangedCellSelected", (object) new ViewStyle("ChangedCellSelected", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Highlight, SystemColors.WindowText));
    this._treeList.Styles.AddReplace("ChangedCell", (object) new ViewStyle("ChangedCell", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, Color.LemonChiffon, SystemColors.WindowText));
    this._treeList.Styles.AddReplace("FocusedCell", (object) new ViewStyle("FocusedCell", "TreeList", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Highlight, SystemColors.HighlightText));
    this._treeList.GetCustomNodeCellStyle += new GetCustomNodeCellStyleEventHandler(this.TreeList_GetCustomNodeCellStyle);
    componentResourceManager.ApplyResources((object) this.columnCaption, "columnCaption");
    this.columnCaption.Name = "columnCaption";
    componentResourceManager.ApplyResources((object) this.columnFileName, "columnFileName");
    this.columnFileName.Name = "columnFileName";
    this.panelControls.Controls.Add((Control) this._deleteButton);
    this.panelControls.Controls.Add((Control) this._addButton);
    componentResourceManager.ApplyResources((object) this.panelControls, "panelControls");
    this.panelControls.Name = "panelControls";
    this._deleteButton.Cursor = Cursors.Default;
    componentResourceManager.ApplyResources((object) this._deleteButton, "_deleteButton");
    this._deleteButton.Name = "_deleteButton";
    this._deleteButton.Click += new EventHandler(this.DeleteButton_Click);
    this._addButton.Cursor = Cursors.Default;
    componentResourceManager.ApplyResources((object) this._addButton, "_addButton");
    this._addButton.Name = "_addButton";
    this._addButton.Click += new EventHandler(this.AddButton_Click);
    this.AutoScaleMode = AutoScaleMode.Inherit;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this._treeList);
    this.Controls.Add((Control) this.panelControls);
    this.Controls.Add((Control) this.panelBottom);
    this.Name = nameof (RolesPluginsForm);
    this.ShowInTaskbar = false;
    this.panelBottom.ResumeLayout(false);
    ((ISupportInitialize) this.imgTooltip).EndInit();
    this._treeList.EndInit();
    this.panelControls.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>
  /// Свалка констант для формы-редактора списка загружаемых модулей
  /// </summary>
  internal abstract class RolesPluginsFormConsts
  {
    /// <summary>Применить</summary>
    internal static readonly string ApplyText1 = LocalizationHolder.rm.GetString("Client.Core_167");
    /// <summary>ОК</summary>
    internal static readonly string ApplyText2 = LocalizationHolder.rm.GetString("Client.Core_218");
    /// <summary>Отмена</summary>
    internal static readonly string CancelText1 = LocalizationHolder.rm.GetString("Client.Core_166");
    /// <summary>Закрыть</summary>
    internal static readonly string CancelText2 = LocalizationHolder.rm.GetString("Client.Core_217");
    /// <summary>Вы действительно хотите отменить все изменения?</summary>
    internal static readonly string Dialog1 = LocalizationHolder.rm.GetString("Client.Core_641");
    /// <summary>Отмена изменений в списке загружаемых плагинов</summary>
    internal static readonly string Dialog2 = LocalizationHolder.rm.GetString("Client.Core_650");
    /// <summary>Базовая роль: \"{0}\"</summary>
    internal static readonly string Tooltip1 = LocalizationHolder.rm.GetString("Client.Core_643");
  }
}
