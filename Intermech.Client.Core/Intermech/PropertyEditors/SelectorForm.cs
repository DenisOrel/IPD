
// Type: Intermech.PropertyEditors.SelectorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.CustomNode;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>
/// Диалог выбора всего чего можно
/// типы объектов, типы связей, типы атрибутов, группы атрибутов
/// </summary>
public class SelectorForm : Form, ISelectorFilter, INodeSelectorFilter
{
  private Button buttonOk;
  private Button buttonCancel;
  private System.ComponentModel.Container components;
  /// <summary>
  /// Этот флаг разрешает выбирать сфокусированный, но не помеченный птичкой элемент.
  /// Если false, то при отсутствии птичек при мультиселекте не будет возвращено ни одной папки.
  /// Только для режима мультиселекта!
  /// </summary>
  public bool SelectFocusedWhenNothingMultiselected = true;
  /// <summary>
  /// Количество уровней дерева на которое надо раскрывать после его загрузки
  /// </summary>
  public int ExpandLevelsOnLoad = -1;
  private ArrayList idList = new ArrayList();
  private ArrayList nameList = new ArrayList();
  private ArrayList typeList = new ArrayList();
  private System.Type rootType;
  private string rootName = string.Empty;
  private System.Type[] checkType = new System.Type[0];
  private bool multiselect;
  private Guid selectorInstGuid = Guid.NewGuid();
  private FilteredTreeView treeView;
  /// <summary>Фильтр для выбора отображаемых узлов</summary>
  private ISelectorFilter selectorFilter;
  /// <summary>Фильтр для разрешения выбора узлов</summary>
  private INodeSelectorFilter nodeSelectorFilter;
  private bool _additionalRoot;
  private StatusStrip statusStrip;
  private ToolStripStatusLabel labelInfo;
  private string _additionalRootName = LocalizationHolder.rm.GetString("Client.Core_984");
  private SelectorForm.CheckActions _onCheckActions = SelectorForm.CheckActions.UncheckParents | SelectorForm.CheckActions.UncheckChildren;
  private SelectorForm.CheckActions _onUncheckActions;
  private bool _loading;
  private bool _inCheck;
  private bool _expandAll;

  /// <summary>Список выбранных ID</summary>
  public ArrayList IDList => this.idList;

  /// <summary>Список выбранных наименований</summary>
  public ArrayList NameList => this.nameList;

  /// <summary>Список выбранных типов папок</summary>
  public ArrayList TypeList => this.typeList;

  /// <summary>Список выбранных категорий папок</summary>
  public ArrayList CategoryList
  {
    get
    {
      return new ArrayList((ICollection) SelectorForm.FolderTypeArrayToCategoryArray((System.Type[]) this.typeList.ToArray(typeof (System.Type))));
    }
  }

  /// <summary>Фильтр для выбора отображаемых узлов</summary>
  public ISelectorFilter SelectorFilter
  {
    get => this.selectorFilter;
    set => this.selectorFilter = value;
  }

  /// <summary>Фильтр для разрешения выбора узлов</summary>
  public INodeSelectorFilter NodeSelectorFilter
  {
    get => this.nodeSelectorFilter;
    set => this.nodeSelectorFilter = value;
  }

  /// <summary>
  /// Дополнительный Root для отображения всех типов атрибутов
  /// </summary>
  public bool AdditionalRoot
  {
    get => this._additionalRoot;
    set
    {
      this._additionalRoot = ((!this.rootType.Equals(typeof (AttributesFolder)) || !SelectorForm.InTypeList(typeof (AttributeFolder), this.checkType) ? 0 : (this.treeView.CheckBoxes ? 1 : 0)) & (value ? 1 : 0)) != 0;
    }
  }

  /// <summary>
  /// Наименование дополнительного нода,
  /// если отличное от "Все типы атрибутов"
  /// </summary>
  public string AdditionalRootName
  {
    get => this._additionalRootName;
    set => this._additionalRootName = value;
  }

  /// <summary>
  /// Определяет, как себя вести при отметке узла: снимать отметки с родительских/дочерних или наоборот отмечать
  /// </summary>
  public SelectorForm.CheckActions OnCheckActions
  {
    get => this._onCheckActions;
    set => this._onCheckActions = value;
  }

  /// <summary>
  /// Определяет, как себя вести при снятии отметки узла: снимать отметки с родительских/дочерних или наоборот отмечать
  /// </summary>
  public SelectorForm.CheckActions OnUncheckActions
  {
    get => this._onUncheckActions;
    set => this._onUncheckActions = value;
  }

  /// <summary>Коснтруктор формы</summary>
  /// <param name="aRootType">тип корня</param>
  /// <param name="aRootName">имя корня</param>
  /// <param name="aCheckType">тип выбираемых нодов</param>
  /// <param name="aMultiSelect">возможность множественного выбора</param>
  public SelectorForm(System.Type aRootType, string aRootName, System.Type aCheckType, bool aMultiSelect)
    : this(aRootType, aRootName, new System.Type[1]{ aCheckType }, (aMultiSelect ? 1 : 0) != 0)
  {
  }

  /// <summary>Коснтруктор формы</summary>
  /// <param name="aRootType">тип корня</param>
  /// <param name="aRootName">имя корня</param>
  /// <param name="aCheckType">типы выбираемых нодов</param>
  /// <param name="aMultiSelect">возможность множественного выбора</param>
  public SelectorForm(System.Type aRootType, string aRootName, System.Type[] aCheckType, bool aMultiSelect)
  {
    this.InitializeComponent();
    this.treeView.SelectorFilter = (ISelectorFilter) this;
    PropertyFormsHolder.RegisterPropertyForms(this.selectorInstGuid);
    TabPagesHolder.RegisterTabPages(this.selectorInstGuid);
    this.treeView.ImageList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
    this.rootType = aRootType;
    this.rootName = aRootName;
    this.checkType = aCheckType;
    this.multiselect = aMultiSelect;
    this.treeView.CheckBoxes = this.multiselect;
  }

  /// <summary>Коструктор формы</summary>
  /// <param name="aRootName">имя корня</param>
  /// <param name="aCheckCategory">категория выбираемого нода</param>
  /// <param name="aMultiSelect">возможность множественного выбора</param>
  public SelectorForm(string aRootName, int aCheckCategory, bool aMultiSelect)
    : this(SelectorForm.RootFolderTypeByCategory(aCheckCategory), aRootName, SelectorForm.CategoryArrayToFolderTypeArray(new int[1]
    {
      aCheckCategory
    }), (aMultiSelect ? 1 : 0) != 0)
  {
  }

  /// <summary>Конструктор формы</summary>
  /// <param name="aRootName">имя корня</param>
  /// <param name="aCheckCategory">категории выбираемых нодов</param>
  /// <param name="aMultiSelect">возможность множественного выбора</param>
  public SelectorForm(string aRootName, int[] aCheckCategory, bool aMultiSelect)
    : this(SelectorForm.RootFolderTypeByCategory(aCheckCategory[0]), aRootName, SelectorForm.CategoryArrayToFolderTypeArray(aCheckCategory), aMultiSelect)
  {
  }

  /// <summary>
  /// Коснтсруктор формы
  /// для выбора типов атрибутов с возможностью множественного выбора
  /// </summary>
  public SelectorForm()
  {
    this.InitializeComponent();
    this.treeView.SelectorFilter = (ISelectorFilter) this;
    this.treeView.ImageList = (ImageList) null;
    this.rootType = typeof (AttributesFolder);
    this.rootName = LocalizationHolder.rm.GetString("Client.Core_54");
    this.checkType = new System.Type[1]{ typeof (AttributeFolder) };
    this.treeView.CheckBoxes = true;
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.components != null)
        this.components.Dispose();
      TabPagesHolder.UnregisterTabPages(this.selectorInstGuid);
      PropertyFormsHolder.UnregisterPropertyForms(this.selectorInstGuid);
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectorForm));
    this.buttonOk = new Button();
    this.buttonCancel = new Button();
    this.treeView = new FilteredTreeView();
    this.statusStrip = new StatusStrip();
    this.labelInfo = new ToolStripStatusLabel();
    this.statusStrip.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.buttonOk, "buttonOk");
    this.buttonOk.DialogResult = DialogResult.OK;
    this.buttonOk.Name = "buttonOk";
    this.buttonOk.Click += new EventHandler(this.buttonOk_Click);
    componentResourceManager.ApplyResources((object) this.buttonCancel, "buttonCancel");
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Name = "buttonCancel";
    componentResourceManager.ApplyResources((object) this.treeView, "treeView");
    this.treeView.CheckBoxes = true;
    this.treeView.HideSelection = false;
    this.treeView.Name = "treeView";
    this.treeView.SelectorFilter = (ISelectorFilter) null;
    this.treeView.Sorted = true;
    this.treeView.BeforeCheck += new TreeViewCancelEventHandler(this.treeView_BeforeCheck);
    this.treeView.AfterCheck += new TreeViewEventHandler(this.treeView_AfterCheck);
    this.treeView.BeforeExpand += new TreeViewCancelEventHandler(this.treeView_BeforeExpand);
    this.treeView.AfterSelect += new TreeViewEventHandler(this.treeView_AfterSelect);
    this.treeView.DoubleClick += new EventHandler(this.treeView_DoubleClick);
    componentResourceManager.ApplyResources((object) this.statusStrip, "statusStrip");
    this.statusStrip.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.labelInfo
    });
    this.statusStrip.Name = "statusStrip";
    this.labelInfo.Name = "labelInfo";
    componentResourceManager.ApplyResources((object) this.labelInfo, "labelInfo");
    this.AcceptButton = (IButtonControl) this.buttonOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.buttonCancel;
    this.Controls.Add((Control) this.statusStrip);
    this.Controls.Add((Control) this.treeView);
    this.Controls.Add((Control) this.buttonCancel);
    this.Controls.Add((Control) this.buttonOk);
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SelectorForm);
    this.ShowInTaskbar = false;
    this.Closed += new EventHandler(this.SelectorForm_Closed);
    this.Load += new EventHandler(this.AttributeSelectorForm_Load);
    this.KeyDown += new KeyEventHandler(this.SelectorForm_KeyDown);
    this.statusStrip.ResumeLayout(false);
    this.statusStrip.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  internal static System.Type CategoryToFolderType(int category)
  {
    System.Type folderType = (System.Type) null;
    switch (category)
    {
      case 3:
        folderType = typeof (AttributeFolder);
        break;
      case 4:
        folderType = typeof (ObjectTypeFolder);
        break;
      case 6:
        folderType = typeof (RelationTypeFolder);
        break;
      case 8:
        folderType = typeof (LevelFolder);
        break;
      case 9:
        folderType = typeof (LanguageFolder);
        break;
      case 11:
        folderType = typeof (AreaFolder);
        break;
      case 12:
        folderType = typeof (AttributeGroupFolder);
        break;
    }
    return folderType;
  }

  internal static int FolderTypeToCategory(System.Type folderType)
  {
    int category = 0;
    if (folderType == typeof (AreaFolder))
      category = 11;
    else if (folderType == typeof (LanguageFolder))
      category = 9;
    else if (folderType == typeof (LevelFolder))
      category = 8;
    else if (folderType == typeof (AttributeGroupFolder))
      category = 12;
    else if (folderType == typeof (AttributeFolder))
      category = 3;
    else if (folderType == typeof (ObjectTypeFolder))
      category = 4;
    else if (folderType == typeof (RelationTypeFolder))
      category = 6;
    return category;
  }

  internal static int[] FolderTypeArrayToCategoryArray(System.Type[] typeArray)
  {
    int[] categoryArray = new int[typeArray.Length];
    for (int index = 0; index < typeArray.Length; ++index)
      categoryArray[index] = SelectorForm.FolderTypeToCategory(typeArray[index]);
    return categoryArray;
  }

  internal static System.Type[] CategoryArrayToFolderTypeArray(int[] categoryArray)
  {
    System.Type[] folderTypeArray = new System.Type[categoryArray.Length];
    for (int index = 0; index < categoryArray.Length; ++index)
      folderTypeArray[index] = SelectorForm.CategoryToFolderType(categoryArray[index]);
    return folderTypeArray;
  }

  internal static System.Type RootFolderTypeByCategory(int category)
  {
    System.Type type = (System.Type) null;
    switch (category)
    {
      case 3:
        type = typeof (AttributesFolder);
        break;
      case 4:
        type = typeof (ObjectTypesFolder);
        break;
      case 6:
        type = typeof (RelationTypesFolder);
        break;
      case 8:
        type = typeof (LevelsFolder);
        break;
      case 9:
        type = typeof (LanguagesFolder);
        break;
      case 11:
        type = typeof (AreasFolder);
        break;
      case 12:
        type = typeof (AttributesFolder);
        break;
    }
    return type;
  }

  /// <summary>Очистить выбор</summary>
  public void ClearSelection() => this.InitSelectionAsType((ArrayList) null, (ArrayList) null);

  /// <summary>
  /// для инициализации выбора папок при открытии окна
  /// если инициализирован IDList, но не инициализирован или недоинициализирован TypeList,
  /// то будет браться первый тип что пришел в конструктор формы
  /// </summary>
  /// <param name="idList"></param>
  /// <param name="typeList"></param>
  public void InitSelectionAsType(ArrayList idList, ArrayList typeList)
  {
    this.idList = idList != null ? (ArrayList) idList.Clone() : new ArrayList();
    this.typeList = typeList != null ? (ArrayList) typeList.Clone() : new ArrayList();
    if (this.idList.Count <= 0 || this.idList.Count <= this.typeList.Count)
      return;
    int num = this.idList.Count - this.typeList.Count;
    for (int index = 0; index < num; ++index)
    {
      if (typeList != null && typeList.Count > 0)
        this.typeList.Add(typeList[0]);
      else
        this.typeList.Add((object) this.checkType[0]);
    }
  }

  /// <summary>
  /// для инициализации выбора папок при открытии окна
  /// по категориям
  /// </summary>
  /// <param name="idList"></param>
  /// <param name="categoryList"></param>
  public void InitSelectionAsCategory(ArrayList idList, ArrayList categoryList)
  {
    this.InitSelectionAsType(idList, new ArrayList((ICollection) SelectorForm.CategoryArrayToFolderTypeArray((int[]) categoryList.ToArray(typeof (int)))));
  }

  private void AttributeSelectorForm_Load(object sender, EventArgs e)
  {
    this._loading = true;
    try
    {
      FormStorage.LoadLayout((Control) this);
      this.treeView.Nodes.Clear();
      this.Text = this.rootName;
      Activator.CreateInstance(this.rootType, (object) this.selectorInstGuid, (object) this.rootName, (object) this.treeView);
      if (this._additionalRoot)
        Activator.CreateInstance(this.rootType, (object) this.selectorInstGuid, (object) this._additionalRootName, (object) this.treeView, (object) false);
      if (this.ExpandLevelsOnLoad < 0)
        this.ExpandAndSelect();
      if (this.ExpandLevelsOnLoad >= 0)
      {
        TreeNode selectedNode = this.treeView.SelectedNode;
        this.ExpandTree(this.ExpandLevelsOnLoad);
        if (selectedNode != null)
          this.treeView.SelectedNode = selectedNode;
      }
      if (!this._expandAll)
        return;
      this.treeView.ExpandAll();
    }
    finally
    {
      this._loading = false;
    }
  }

  private void ExpandAbstractObjectType(TreeNode parentNode, int parentId)
  {
    int index = DataHolders.ObjectTypesHolder.IdPresent(parentId);
    if (index == -1)
      return;
    DataTable dataTable = (DataTable) DataHolders.ObjectTypesHolder.DataTables[index];
    if (dataTable == null)
      return;
    if (!parentNode.IsExpanded)
      parentNode.Expand();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      if (Convert.ToInt32(row["F_VERSIONABLE"]) == 0)
      {
        TreeNode nodeByIdCustom = ClientCommons.FindNodeByIdCustom(parentNode, (object) Convert.ToInt32(row["F_OBJECT_TYPE"]));
        if (nodeByIdCustom != null)
          this.ExpandAbstractObjectType(nodeByIdCustom, Convert.ToInt32(row["F_OBJECT_TYPE"]));
      }
    }
  }

  /// <summary>Развернуть дерево на заданное количество уровней</summary>
  /// <param name="levels">Глубина раскрытия. 0 - корневой уровень</param>
  public void ExpandTree(int levels)
  {
    if (levels < 0 || this.treeView == null)
      return;
    for (int index = 0; index < this.treeView.Nodes.Count; ++index)
    {
      if (!this.treeView.Nodes[index].IsExpanded)
        this.treeView.Nodes[index].Expand();
      if (levels > 0)
        this.ExpandNodes(this.treeView.Nodes[index].Nodes, levels - 1);
    }
    if (this.treeView.Nodes.Count <= 0)
      return;
    if (this.treeView.Nodes[0].Nodes.Count > 0)
      this.treeView.SelectedNode = this.treeView.Nodes[0].Nodes[0];
    else
      this.treeView.SelectedNode = this.treeView.Nodes[0];
  }

  /// <summary>Развернуть узлы на заданное количество уровней</summary>
  /// <param name="nodes">Коллекция узлов</param>
  /// <param name="levels">Глубина раскрытия. 0 - начальный уровень</param>
  private void ExpandNodes(TreeNodeCollection nodes, int levels)
  {
    if (levels < 0 || nodes == null)
      return;
    for (int index = 0; index < nodes.Count; ++index)
    {
      if (!nodes[index].IsExpanded)
        nodes[index].Expand();
      if (levels > 0)
        this.ExpandNodes(nodes[index].Nodes, levels - 1);
    }
  }

  private void ExpandAndSelect()
  {
    if (this.treeView.Nodes.Count == 0)
      return;
    TreeNode node1 = this.treeView.Nodes[0];
    node1.Expand();
    if (SelectorForm.InTypeList(this.rootType, (System.Type[]) this.typeList.ToArray(typeof (System.Type))))
      node1.Checked = true;
    if (this.rootType == typeof (AreasFolder))
      this.CheckIfNeeded(node1, this.idList, this.typeList, typeof (AreaFolder));
    else if (this.rootType == typeof (LanguagesFolder))
      this.CheckIfNeeded(node1, this.idList, this.typeList, typeof (LanguageFolder));
    else if (this.rootType == typeof (LevelsFolder))
      this.CheckIfNeeded(node1, this.idList, this.typeList, typeof (LevelFolder));
    else if (this.rootType == typeof (AttributesFolder))
    {
      this.CheckIfNeeded(node1, this.idList, this.typeList, typeof (AttributeGroupFolder));
      TreeNode node2 = (TreeNode) null;
      for (int index = 0; index < node1.Nodes.Count; ++index)
      {
        if (node1.Nodes[index].Tag is AttributeGroupFolder && Convert.ToInt32((node1.Nodes[index].Tag as IFolder).Id) == -1)
        {
          node2 = node1.Nodes[index];
          break;
        }
      }
      if (node2 != null)
      {
        node2.Expand();
        this.CheckIfNeeded(node2, this.idList, this.typeList, typeof (AttributeFolder));
      }
      else
      {
        foreach (TreeNode node3 in node1.Nodes)
        {
          if (node3.Tag is AttributeGroupFolder)
          {
            if (node3.Tag is AttributeTypeAssignedGroupFolder)
              node3.Expand();
            this.CheckIfNeeded(node3, this.idList, this.typeList, typeof (AttributeFolder));
          }
        }
      }
    }
    else if (this.rootType == typeof (ObjectTypesFolder))
    {
      IObjectTypesInheritanceCache inheritanceCache = CacheManager.Cache("ObjectTypeInheritanceCache") as IObjectTypesInheritanceCache;
      for (int index1 = 0; index1 < this.idList.Count; ++index1)
      {
        if ((System.Type) this.typeList[index1] == typeof (ObjectTypeFolder))
        {
          int objType = Convert.ToInt32(this.idList[index1]);
          ArrayList arrayList = new ArrayList();
          while (objType != -1)
          {
            objType = inheritanceCache.GetParentType(objType);
            if (objType != -1)
              arrayList.Add((object) objType);
          }
          TreeNode node4 = node1;
          for (int index2 = arrayList.Count - 1; index2 >= 0; --index2)
          {
            node4 = ClientCommons.FindNodeByIdCustom(node4, arrayList[index2]);
            if (node4 != null)
            {
              if (!node4.IsExpanded)
                node4.Expand();
            }
            else
              break;
          }
          if (node4 != null)
          {
            TreeNode nodeByIdCustom = ClientCommons.FindNodeByIdCustom(node4, (object) Convert.ToInt32(this.idList[index1]));
            if (nodeByIdCustom != null)
            {
              if (this.treeView.CheckBoxes)
              {
                nodeByIdCustom.Checked = true;
              }
              else
              {
                this.treeView.SelectedNode = nodeByIdCustom;
                break;
              }
            }
          }
        }
      }
    }
    else
    {
      if (!(this.rootType == typeof (RelationTypesFolder)))
        return;
      this.CheckIfNeeded(node1, this.idList, this.typeList, typeof (RelationTypeFolder));
    }
  }

  private void CheckIfNeeded(TreeNode node, ArrayList idList, ArrayList typeList, System.Type type)
  {
    for (int index1 = 0; index1 < node.Nodes.Count; ++index1)
    {
      if ((!this.DesignMode || node.Nodes[index1].Tag != null) && node.Nodes[index1].Tag is IFolder tag)
      {
        object id = tag.Id;
        int num = -1;
        for (int index2 = 0; index2 < idList.Count; ++index2)
        {
          if (idList[index2].Equals(id) && (System.Type) typeList[index2] == type)
          {
            num = index2;
            break;
          }
        }
        if (num != -1)
        {
          if (this.treeView.CheckBoxes)
          {
            node.Nodes[index1].Checked = true;
          }
          else
          {
            this.treeView.SelectedNode = node.Nodes[index1];
            break;
          }
        }
      }
    }
  }

  private void ProcessNodes(TreeNodeCollection tnc)
  {
    for (int index = 0; index < tnc.Count; ++index)
    {
      if (!(tnc[index].Text == ClientConsts.FakeNodeString) && tnc[index].Tag != null && SelectorForm.InTypeList(tnc[index].Tag.GetType(), this.checkType) && tnc[index].Checked && !this.idList.Contains((object) Convert.ToInt32((tnc[index].Tag as IFolder).Id)))
      {
        this.idList.Add((object) Convert.ToInt32((tnc[index].Tag as IFolder).Id));
        this.nameList.Add((object) (tnc[index].Tag as IFolder).Text);
        this.typeList.Add((object) tnc[index].Tag.GetType());
      }
      this.ProcessNodes(tnc[index].Nodes);
    }
  }

  private void buttonOk_Click(object sender, EventArgs e) => this.SelectNodes();

  public void SelectNodes()
  {
    this.idList.Clear();
    this.nameList.Clear();
    this.typeList.Clear();
    if (this.treeView.CheckBoxes)
      this.ProcessNodes(this.treeView.Nodes);
    if (this.idList.Count != 0 || this.treeView.CheckBoxes && (!this.treeView.CheckBoxes || !this.SelectFocusedWhenNothingMultiselected) || this.treeView.SelectedNode == null || !SelectorForm.InTypeList(this.treeView.SelectedNode.Tag.GetType(), this.checkType))
      return;
    this.idList.Add((object) Convert.ToInt32((this.treeView.SelectedNode.Tag as IFolder).Id));
    this.nameList.Add((object) (this.treeView.SelectedNode.Tag as IFolder).Text);
    this.typeList.Add((object) this.treeView.SelectedNode.Tag.GetType());
  }

  /// <summary>Проверка наличия Type в массиве Type[]</summary>
  /// <param name="type">тип для проверки</param>
  /// <param name="list">список типов в которых проверяется</param>
  /// <returns>true - если присутствует</returns>
  public static bool InTypeList(System.Type type, System.Type[] list)
  {
    bool flag = false;
    foreach (System.Type type1 in list)
    {
      if (type1 == type)
      {
        flag = true;
        break;
      }
    }
    return flag;
  }

  private void treeView_BeforeCheck(object sender, TreeViewCancelEventArgs e)
  {
    if (e.Node != null && e.Node.Tag != null && SelectorForm.InTypeList(e.Node.Tag.GetType(), this.checkType))
      return;
    e.Cancel = true;
  }

  private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
  {
    if (this._inCheck)
      return;
    this._inCheck = true;
    try
    {
      if (this._additionalRoot)
      {
        if (!(e.Node.Tag is IFolder tag))
          return;
        this.CheckSameIdNodes(tag, this.treeView.Nodes);
      }
      TreeNode node = e.Node;
      if (!this._loading || node == this.RootNode)
      {
        if (node.Checked)
        {
          if ((this.OnCheckActions & SelectorForm.CheckActions.UncheckParents) != SelectorForm.CheckActions.None)
            this.UncheckParentRecursive(node);
          if ((this.OnCheckActions & SelectorForm.CheckActions.UncheckChildren) != SelectorForm.CheckActions.None)
            this.CheckChildrenRecursive(node, false);
          if ((this.OnCheckActions & SelectorForm.CheckActions.CheckChildren) != SelectorForm.CheckActions.None)
            this.CheckChildrenRecursive(node, true);
        }
        else
        {
          if ((this.OnUncheckActions & SelectorForm.CheckActions.UncheckParents) != SelectorForm.CheckActions.None)
            this.UncheckParentRecursive(node);
          if ((this.OnUncheckActions & SelectorForm.CheckActions.UncheckChildren) != SelectorForm.CheckActions.None)
            this.CheckChildrenRecursive(node, false);
          if ((this.OnUncheckActions & SelectorForm.CheckActions.CheckChildren) != SelectorForm.CheckActions.None)
            this.CheckChildrenRecursive(node, true);
          if (this.AllowRootSelect && this.treeView.Nodes.Count > 0 && this.treeView.Nodes[0].Checked)
            this.treeView.Nodes[0].Checked = false;
        }
      }
      this.treeView_AfterSelect(sender, e);
    }
    finally
    {
      this._inCheck = false;
    }
  }

  private void UncheckParentRecursive(TreeNode treeNode)
  {
    if (treeNode == null)
      return;
    for (; treeNode.Parent != null; treeNode = treeNode.Parent)
      treeNode.Parent.Checked = false;
  }

  private void CheckChildrenRecursive(TreeNode treeNode, bool check)
  {
    if (treeNode == null)
      return;
    if (check && !treeNode.IsExpanded)
      treeNode.Expand();
    if (treeNode.Nodes.Count <= 0)
      return;
    for (int index = 0; index < treeNode.Nodes.Count; ++index)
    {
      treeNode.Nodes[index].Checked = check;
      this.CheckChildrenRecursive(treeNode.Nodes[index], check);
    }
  }

  private void CheckSameIdNodes(IFolder sourceFolder, TreeNodeCollection tnc)
  {
    for (int index = 0; index < tnc.Count; ++index)
    {
      if (tnc[index].Tag is IFolder tag && object.Equals((object) sourceFolder.GetType(), (object) tag.GetType()) && !object.Equals((object) sourceFolder, (object) tag) && object.Equals(sourceFolder.Id, tag.Id))
        tag.Node.Checked = sourceFolder.Node.Checked;
      if (tnc[index].Nodes.Count > 0)
        this.CheckSameIdNodes(sourceFolder, tnc[index].Nodes);
    }
  }

  private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
  {
    IFolder tag = (IFolder) e.Node.Tag;
    if (tag == null)
      return;
    List<IFolder> folders = new List<IFolder>();
    if (!this.multiselect)
    {
      folders.Add(tag);
    }
    else
    {
      if (this.SelectFocusedWhenNothingMultiselected)
        folders.Add(tag);
      this.CollectCheckedNodes(this.treeView.Nodes, folders);
    }
    bool flag = folders.Count > 0;
    string errorMessage = string.Empty;
    foreach (IFolder folder in folders)
    {
      flag = this.CanSelectNode(folder.Category, folder.Id, out errorMessage);
      if (!flag)
        break;
    }
    this.buttonOk.Enabled = flag;
    this.labelInfo.Text = errorMessage;
  }

  private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
  {
    if (this.DesignMode)
      return;
    ICategoryTypeIconService service = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
    service?.BeginUpdate();
    try
    {
      if (e.Action != TreeViewAction.Expand)
        return;
      TreeNode node = e.Node;
      if (node == null)
        return;
      IFolder tag = (IFolder) node.Tag;
      if (node.Nodes.Count == 1 && node.Nodes[0].Text == ClientConsts.FakeNodeString)
      {
        List<IFolder> folders = (List<IFolder>) null;
        if (this._additionalRoot)
        {
          folders = new List<IFolder>();
          this.CollectCheckedNodes(this.treeView.Nodes, folders);
        }
        tag.Populate(false);
        if (this._inCheck || folders == null)
          return;
        this._inCheck = true;
        try
        {
          foreach (IFolder sourceFolder in folders)
            this.CheckSameIdNodes(sourceFolder, this.treeView.Nodes);
        }
        finally
        {
          this._inCheck = false;
        }
      }
      else
      {
        for (int index = 0; index < node.Nodes.Count; ++index)
        {
          if (ClientConsts.IsFakeNode(node.Nodes[index]))
          {
            ((IFolder) node.Nodes[index].Tag).Populate(false);
            break;
          }
        }
      }
    }
    finally
    {
      service?.EndUpdate();
    }
  }

  private bool CollectCheckedNodes(TreeNodeCollection tnc, List<IFolder> folders)
  {
    for (int index = 0; index < tnc.Count; ++index)
    {
      if (tnc[index].Checked && tnc[index].Tag is IFolder)
        folders.Add(tnc[index].Tag as IFolder);
      if (tnc[index].Nodes.Count > 0)
        this.CollectCheckedNodes(tnc[index].Nodes, folders);
    }
    return folders.Count > 0;
  }

  public static IDBObjectID[] SelectObjects(int[] objTypeIdArray, bool _objectVersionProcessed = true)
  {
    return SelectorForm.SelectObjects(objTypeIdArray, (long[]) null, true, false, false, _objectVersionProcessed);
  }

  /// <summary>Выбор объектов по типу</summary>
  /// <param name="objTypeIdArray">если null или первый элемент -1 то выбор по всем типам объектов</param>
  /// <param name="objects">null или Int64[] выбранных ранее идентификаторов версий объектов</param>
  /// <param name="_objectVersionProcessed">флаг обработки версии объектов по VersionID (true) или объектов по ID (false)</param>
  /// <returns></returns>
  public static IDBObjectID[] SelectObjects(
    int[] objTypeIdArray,
    long[] objects,
    bool _objectVersionProcessed = true)
  {
    return SelectorForm.SelectObjects(objTypeIdArray, objects, true, false, false, _objectVersionProcessed);
  }

  public static IDBObjectID[] SelectObjects(
    int[] objTypeIdArray,
    bool extendUsersToGroups,
    bool onlyUsersSelected,
    bool _objectVersionProcessed = true)
  {
    return SelectorForm.SelectObjects(objTypeIdArray, (long[]) null, true, extendUsersToGroups, onlyUsersSelected, _objectVersionProcessed);
  }

  /// <summary>Выбор объектов по типу</summary>
  /// <param name="objTypeIdArray">если null или первый элемент -1 то выбор по всем типам объектов</param>
  /// <param name="objects">null или Int64[] выбранных ранее идентификаторов версий объектов</param>
  /// <param name="extendUsersToGroups">если true, то если в objTypeIdArray есть тип "пользователи", то он сводится в дерево "группы-пользователи"; при true тип "группы пользователей" игнорируется</param>
  /// <param name="onlyUsersSelected">в случае extendUsersToGroups = true выбирать только пользователей, но если в objTypeIdArray будут группы, то автоматически false</param>
  /// <param name="_objectVersionProcessed">флаг обработки версии объектов по VersionID (true) или объектов по ID (false)</param>
  /// <returns></returns>
  public static IDBObjectID[] SelectObjects(
    int[] objTypeIdArray,
    long[] objects,
    bool extendUsersToGroups,
    bool onlyUsersSelected,
    bool _objectVersionProcessed = true)
  {
    return SelectorForm.SelectObjects(objTypeIdArray, objects, true, extendUsersToGroups, onlyUsersSelected, _objectVersionProcessed);
  }

  /// <summary>Выбор объектов по типу</summary>
  /// <param name="objTypeIdArray">если null или первый элемент -1 то выбор по всем типам объектов</param>
  /// <param name="objects">null или Int64[] выбранных ранее идентификаторов версий объектов</param>
  /// <param name="multiSelect">множественный выбор</param>
  /// <param name="extendUsersToGroups">если true, то если в objTypeIdArray есть тип "пользователи", то он сводится в дерево "группы-пользователи"; при true тип "группы пользователей" игнорируется</param>
  /// <param name="onlyUsersSelected">в случае extendUsersToGroups = true выбирать только пользователей, но если в objTypeIdArray будут группы, то автоматически false</param>
  /// <param name="_objectVersionProcessed">флаг обработки версии объектов по VersionID (true) или объектов по ID (false)</param>
  /// <param name="needfulDescriptors">дескрипторы для типов элементов, которые тоже хочется добавить в корень списка объектов для выбора</param>
  /// <returns></returns>
  public static IDBObjectID[] SelectObjects(
    int[] objTypeIdArray,
    long[] objects,
    bool multiSelect,
    bool extendUsersToGroups,
    bool onlyUsersSelected,
    bool _objectVersionProcessed = true,
    params IDescriptor[] needfulDescriptors)
  {
    return SelectorForm.SelectObjects(objTypeIdArray, objects, multiSelect, extendUsersToGroups, onlyUsersSelected, (ConditionStructure[]) null, _objectVersionProcessed, needfulDescriptors);
  }

  /// <summary>Выбор объектов по типу</summary>
  /// <param name="objTypeIdArray">если null или первый элемент -1 то выбор по всем типам объектов</param>
  /// <param name="objects">null или Int64[] выбранных ранее идентификаторов версий объектов</param>
  /// <param name="multiSelect">множественный выбор</param>
  /// <param name="extendUsersToGroups">если true, то если в objTypeIdArray есть тип "пользователи", то он сводится в дерево "группы-пользователи"; при true тип "группы пользователей" игнорируется</param>
  /// <param name="onlyUsersSelected">в случае extendUsersToGroups = true выбирать только пользователей, но если в objTypeIdArray будут группы, то автоматически false</param>
  /// <param name="conditions">условия, напр от контекстной выборки</param>
  /// <param name="_objectVersionProcessed">флаг обработки версии объектов по VersionID (true) или объектов по ID (false)</param>
  /// <param name="needfulDescriptors">дескрипторы для типов элементов, которые тоже хочется добавить в корень списка объектов для выбора</param>
  /// <returns></returns>
  public static IDBObjectID[] SelectObjects(
    int[] objTypeIdArray,
    long[] objects,
    bool multiSelect,
    bool extendUsersToGroups,
    bool onlyUsersSelected,
    ConditionStructure[] conditions,
    bool _objectVersionProcessed = true,
    params IDescriptor[] needfulDescriptors)
  {
    List<int> collection = new List<int>();
    bool flag = false;
    IDescriptor rootDescriptor;
    if (objTypeIdArray == null || objTypeIdArray[0] == -1)
    {
      rootDescriptor = conditions != null ? (IDescriptor) new ObjectsSelectionDescriptor(-1, LocalizationHolder.rm.GetString("Client.Core_1099"), (IReadOnlyCollection<ConditionStructure>) conditions) : (IDescriptor) new ObjectTypesNodeDescriptor();
    }
    else
    {
      IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
      DescriptorCollection descriptors = new DescriptorCollection();
      for (int index = 0; index < objTypeIdArray.Length; ++index)
      {
        if (objTypeIdArray[index] == service.GroupsTypeID)
          flag = true;
      }
      if (flag)
        onlyUsersSelected = false;
      for (int index = 0; index < objTypeIdArray.Length; ++index)
      {
        IDescriptor descriptor = (IDescriptor) null;
        if (extendUsersToGroups && (objTypeIdArray[index] == service.UsersTypeID || objTypeIdArray[index] == service.GroupsTypeID))
        {
          if (objTypeIdArray[index] == service.UsersTypeID)
          {
            descriptor = (IDescriptor) new UsersGroupsDescriptor();
            collection.Add(objTypeIdArray[index]);
            if (!onlyUsersSelected && collection.IndexOf(service.GroupsTypeID) == -1)
              collection.Add(service.GroupsTypeID);
          }
          else
          {
            if (objTypeIdArray[index] == service.GroupsTypeID && !onlyUsersSelected && collection.IndexOf(service.GroupsTypeID) == -1)
            {
              collection.Add(objTypeIdArray[index]);
              continue;
            }
            continue;
          }
        }
        else
        {
          if (conditions == null)
          {
            descriptor = (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(objTypeIdArray[index]);
          }
          else
          {
            IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeIdArray[index]);
            if (objectType != null)
              descriptor = (IDescriptor) new ObjectsSelectionDescriptor(objTypeIdArray[index], objectType.ObjectTypeName, (IReadOnlyCollection<ConditionStructure>) conditions);
          }
          collection.Add(objTypeIdArray[index]);
        }
        descriptors.Add(descriptor);
      }
      foreach (IDescriptor needfulDescriptor in needfulDescriptors)
        descriptors.Add(needfulDescriptor);
      rootDescriptor = (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("Client.Core_283"), descriptors);
    }
    if (collection.Count > 0)
      Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new TypedObjectsSelectedItemsAnalyzer(new List<int>((IEnumerable<int>) collection), true), true);
    if (objects != null)
      Intermech.Navigator.SelectionWindow.RegisterAnalyze((IToSelectItemsAnalyzer) new ObjectsToSelectItemsAnalyzer((IList<long>) objects));
    SelectionOptions options = SelectionOptions.Default;
    if (!multiSelect)
      options |= SelectionOptions.DisableMultiselect;
    return Intermech.Navigator.SelectionWindow.Select(LocalizationHolder.rm.GetString("Client.Core_852"), rootDescriptor, typeof (IDBObjectID), options) as IDBObjectID[];
  }

  private void SelectorForm_Closed(object sender, EventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>Корректно расположить форму и назначить ей предка</summary>
  /// <param name="parent"></param>
  public void SetParent(Control parent)
  {
    if (parent == null)
      return;
    this.TopLevel = false;
    this.Dock = DockStyle.Fill;
    this.FormBorderStyle = FormBorderStyle.None;
    this.Visible = true;
    this.Parent = parent;
    this.treeView.Dock = DockStyle.Fill;
    this.statusStrip.Visible = false;
    this.buttonOk.Visible = false;
    this.buttonCancel.Visible = false;
  }

  /// <summary>Метод фильтрации нодов</summary>
  /// <param name="category">Категория нода</param>
  /// <param name="id">Идентификатор нода</param>
  /// <returns>true - если нужно отображать, false - чтобы скрыть</returns>
  public bool IsInFilter(int category, object id)
  {
    return this.selectorFilter == null || this.selectorFilter.IsInFilter(category, id);
  }

  /// <summary>Можно ли выбирать указанный узел</summary>
  /// <param name="category">Категория</param>
  /// <param name="id">Идентификатор</param>
  /// <param name="errorMessage">Если значение не равно String.Empty, то оно будет отображено в статусной строке окна</param>
  /// <returns>true, если выбор узла разрешён</returns>
  public bool CanSelectNode(int category, object id, out string errorMessage)
  {
    errorMessage = string.Empty;
    return this.nodeSelectorFilter == null || this.nodeSelectorFilter.CanSelectNode(category, id, out errorMessage);
  }

  private void SelectorForm_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Escape)
      return;
    this.DialogResult = DialogResult.Cancel;
  }

  public void ExpandAll() => this._expandAll = true;

  internal TreeNode RootNode
  {
    get => this.treeView.Nodes.Count <= 0 ? (TreeNode) null : this.treeView.Nodes[0];
  }

  public bool AllowRootSelect
  {
    get => this.checkType.Length > 1;
    set
    {
      if (value)
      {
        if (this.checkType.Length < 2)
          Array.Resize<System.Type>(ref this.checkType, 2);
        if (this.checkType.Length != 2)
          return;
        this.checkType[1] = this.rootType;
      }
      else
      {
        if (this.checkType.Length != 2)
          return;
        Array.Resize<System.Type>(ref this.checkType, 1);
      }
    }
  }

  private void treeView_DoubleClick(object sender, EventArgs e)
  {
    if (!this.buttonOk.Visible || !this.buttonOk.Enabled || this.multiselect || this.treeView.SelectedNode == null || this.treeView.SelectedNode.Nodes.Count != 0)
      return;
    this.SelectNodes();
    this.DialogResult = DialogResult.OK;
  }

  [Flags]
  public enum CheckActions
  {
    None = 0,
    UncheckParents = 1,
    UncheckChildren = 2,
    CheckChildren = 4,
  }
}
