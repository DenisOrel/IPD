
// Type: Intermech.Navigator.Controls.ProjectCreatorControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Client.Core.ObjectCreator;
using Intermech.Client.Core.ObjectCreator.Controls;
using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Раньше использовался только при создании проектов по шаблону.
/// Теперь может быть доступен для любого типа объектов, на который назначен атрибут Шаблон состава объекта
/// </summary>
internal class ProjectCreatorControl : ObjectCreatorControl
{
  public bool parentCheck = true;
  /// <summary>id проекта</summary>
  private long _projectObjectID;
  private BlockedMouseDBLClickTreeView tempTree;
  /// <summary>
  /// флаг для указания того что родительский узел был выделен программно,
  /// т.е. при выделение дочернего. =&gt; не надо выделять все остальные дочерние
  /// для него узлы
  /// </summary>
  private bool _isProgrammatically;
  /// <summary>id версии шаблона, по которому создаётся проект</summary>
  private long _templateID;
  /// <summary>список id объектов из шаблона, которые будут созданы</summary>
  private ArrayList _listOfCreatedObjectsID = new ArrayList();

  /// <summary>id проекта</summary>
  public long ProjectObjectID
  {
    get => this._projectObjectID;
    set => this._projectObjectID = value;
  }

  /// <summary>список id объектов из шаблона, которые будут созданы</summary>
  public ArrayList ListOfCreatedObjectsID => this._listOfCreatedObjectsID;

  /// <summary>ID шаблона по которому создаётся проект</summary>
  public long TemplateId => this._templateID;

  public ProjectCreatorControl(CreatedObjectItem createdObject)
    : base(createdObject)
  {
    this.InitializeComponent();
    this.tempTree.ImageList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
    this._projectObjectID = createdObject.ObjectID;
  }

  public ProjectCreatorControl()
  {
    this.InitializeComponent();
    this.tempTree.ImageList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
  }

  /// <summary>Загрузка дерева структуры шаблона.</summary>
  /// <param name="templateID">ИД шаблона.</param>
  public void SelectionLoad(long templateID)
  {
    this._templateID = templateID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (templateID > 0L)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(templateID);
        this.tempTree.Nodes.Clear();
        TreeNode treeNode = new TreeNode(dbObject.Caption);
        this.tempTree.Nodes.Add(treeNode);
        treeNode.Checked = true;
        treeNode.Tag = (object) dbObject.ObjectID;
        treeNode.ImageIndex = treeNode.SelectedImageIndex = Statics.IconSrv.IndexOf(4, dbObject.ObjectType);
        int relationTypeId = MetaDataHelper.GetRelationTypeID("cad00814-306c-11d8-b4e9-00304f19f545");
        IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(relationTypeId);
        DBRecordSetParams rsp = new DBRecordSetParams((ConditionStructure[]) null, new object[4]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
          (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
          (object) ObligatoryObjectAttributes.CAPTION
        }, new object[1]
        {
          (object) ObligatoryObjectAttributes.F_PRJLINK_ID
        }, new SortOrders[1]{ SortOrders.ASC });
        this.LoadTemplate(dbObject.ObjectID, treeNode, relationCollection, rsp);
        treeNode.Expand();
      }
      else
      {
        this.tempTree.Nodes.Clear();
        this._listOfCreatedObjectsID.Clear();
      }
    }
  }

  /// <summary>заполним шаблон</summary>
  /// <param name="objectID">id объекта, состав которого будем показывать</param>
  /// <param name="parent">корневой узел, представляющий объект template</param>
  /// <param name="relColl"></param>
  /// <param name="rsp"></param>
  private void LoadTemplate(
    long objectID,
    TreeNode parent,
    IDBRelationCollection relColl,
    DBRecordSetParams rsp)
  {
    DataTable dataTable = relColl.ConsistFrom(rsp, objectID);
    if (dataTable == null || dataTable.Rows.Count <= 0)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      Convert.ToInt64(row[1]);
      long int64 = Convert.ToInt64(row[0]);
      int int32 = Convert.ToInt32(row[2]);
      TreeNode treeNode = new TreeNode(row[3].ToString())
      {
        Tag = (object) int64
      };
      treeNode.ImageIndex = treeNode.SelectedImageIndex = Statics.IconSrv.IndexOf(4, int32);
      treeNode.Checked = true;
      parent.Nodes.Add(treeNode);
      this.LoadTemplate(int64, treeNode, relColl, rsp);
      treeNode.Expand();
    }
  }

  public override bool Save(PageSaveArgs args)
  {
    try
    {
      return this.SaveObjectData();
    }
    catch (Exception ex)
    {
      args.Error = ex;
      return false;
    }
  }

  public bool SaveObjectData()
  {
    if (this.tempTree.Nodes.Count > 0 && !this.tempTree.Nodes[0].Checked)
    {
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1694"), LocalizationHolder.rm.GetString("Client.Core_1695"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return false;
    }
    this._listOfCreatedObjectsID.Clear();
    this.SaveSelectedNodes(this.tempTree.Nodes);
    return true;
  }

  /// <summary>сохранить список выделенных узлов</summary>
  /// <param name="allNodes">Список выделенных узлов</param>
  public void SaveSelectedNodes(TreeNodeCollection allNodes)
  {
    foreach (TreeNode allNode in allNodes)
    {
      if (allNode.Checked)
      {
        if (allNode.Parent == null)
        {
          this.SaveSelectedNodes(allNode.Nodes);
        }
        else
        {
          this._listOfCreatedObjectsID.Add((object) new long[2]
          {
            (long) allNode.Tag,
            (long) allNode.Parent.Tag
          });
          this.SaveSelectedNodes(allNode.Nodes);
        }
      }
    }
  }

  private void tempTree_AfterCheck(object sender, TreeViewEventArgs e)
  {
    if (!e.Node.Checked)
    {
      this.CheckAllNodes(e.Node, false);
      e.Node.Collapse();
    }
    else
    {
      if (!this._isProgrammatically)
        this.CheckAllNodes(e.Node, true);
      if (e.Node.Parent == null)
        return;
      this._isProgrammatically = true;
      if (!e.Node.Parent.Checked)
        e.Node.Parent.Checked = true;
      this._isProgrammatically = false;
    }
  }

  public void CheckAllNodes(TreeNode root, bool check)
  {
    foreach (TreeNode node in root.Nodes)
    {
      node.Checked = check;
      this.CheckAllNodes(node, check);
    }
  }

  public override bool Refresh(PageRefreshArgs args)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeByGuid = sessionKeeper.Session.GetObject(this._projectObjectID).GetAttributeByGuid(new Guid("cad00815-306c-11d8-b4e9-00304f19f545"));
      long result = 0;
      if (attributeByGuid != null)
        long.TryParse(attributeByGuid.Value.ToString(), out result);
      if (result != this._templateID)
      {
        this.SelectionLoad(result);
        this.SaveSelectedNodes(this.tempTree.Nodes);
      }
    }
    return base.Refresh(args);
  }

  /// <summary>
  /// раздел справки, описывающий создание
  /// создание проекта
  /// </summary>
  public override int HelpTopicID => 1244;

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProjectCreatorControl));
    this.tempTree = new BlockedMouseDBLClickTreeView();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.tempTree, "tempTree");
    this.tempTree.CheckBoxes = true;
    this.tempTree.FullRowSelect = true;
    this.tempTree.Name = "tempTree";
    this.tempTree.AfterCheck += new TreeViewEventHandler(this.tempTree_AfterCheck);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.tempTree);
    this.Name = nameof (ProjectCreatorControl);
    this.ResumeLayout(false);
  }
}
