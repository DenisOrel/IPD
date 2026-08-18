// Decompiled with JetBrains decompiler
// Type: Intermech.DBClean.Client.DemoDbCleanForm
// Assembly: Intermech.DBClean.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 973F13FD-72F3-4555-9BF9-74AC5C606885
// Assembly location: D:\IPS\Client\Intermech.DBClean.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.DBClean.Client.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using DevExpress.IM.XtraEditors.Repository;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using DevExpress.IM.XtraTreeList.Nodes.Operations;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Docking;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.Controls;
using NJFLib.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.DBClean.Client;

public class DemoDbCleanForm : DockControl
{
  public static readonly Guid FormGuid = new Guid("{63D99818-9D78-47C2-B39E-7CB94C482DFC}");
  public static readonly Guid CatalogTypeAttGUID = new Guid("cad00200-306c-11d8-b4e9-00304f19f545");
  protected ICategoryTypeIconService _objtypesIcons;
  private CleanSchema activeSchema;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private System.Windows.Forms.TabControl tabControl1;
  private System.Windows.Forms.TabPage tObjects;
  private System.Windows.Forms.TabPage tImbase;
  private System.Windows.Forms.TabPage tAttrs;
  private BarManager barManager;
  private ToolBarContainer leftBarDock;
  private ToolBarContainer rightBarDock;
  private ToolBarContainer bottomBarDock;
  private ToolBarContainer topBarDock;
  private Intermech.Bars.ToolBar pageElementsToolBar;
  private ButtonItem bSelectAll;
  private ButtonItem bInvertSelection;
  private ButtonItem bUnselectAll;
  private ImageList imageListObjectsState;
  private TreeList treeObjTypes;
  private TreeListColumn treeListColumn1;
  private ImageListBoxControl imageListBoxControl1;
  private ImageList imageListObjects;
  private MenuBar menuBar1;
  private ContextMenuBarItem contextMenuBarItem1;
  private MenuButtonItem menuButtonItem1;
  private MenuButtonItem menuButtonItem2;
  private MenuButtonItem menuButtonItem3;
  private TreeList treeImBase;
  private TreeListColumn treeListColumn2;
  private RepositoryItemButtonEdit repositoryItemButtonEditDelete;
  private RepositoryItemButtonEdit repositoryItemButtonEditClean;
  private ImageList imageListImbaseObjects;
  private ImageList imageListImBaseState;
  private RepositoryItemButtonEdit repositoryClean;
  private RepositoryItemButtonEdit repositoryDelete;
  private TreeListColumn treeListColumnDelete;
  private TreeListColumn treeListColumnClean;
  private TreeList treeListAttrs;
  private TreeListColumn treeListColumn3;
  private ImageList imageListAttrs;
  private CollapsibleSplitter collapsibleSplitter1;
  private Panel panel1;
  private ButtonItem bOpen;
  private ButtonItem bSave;
  private ButtonItem bExecute;
  private ButtonItem bExecuteAll;
  private SaveFileDialog saveFileDialog1;
  private OpenFileDialog openFileDialog1;
  private TreeList treeAttrAplicability;
  private TreeListColumn colId;
  private TreeListColumn colCaption;
  private ImageList imageListAppObjects;
  private Intermech.Bars.ToolBar toolBar1;
  private ButtonItem bCard;

  public DemoDbCleanForm()
  {
    this.InitializeComponent();
    this.Guid = DemoDbCleanForm.FormGuid;
    this.PersistState = false;
  }

  private void miNewFile_Click(object sender, EventArgs e)
  {
  }

  private void miCreateFromTemplate_Click(object sender, EventArgs e)
  {
  }

  private void miOpenFile_Click(object sender, EventArgs e)
  {
  }

  private void miSaveFile_Click(object sender, EventArgs e)
  {
  }

  private void miSaveAsFile_Click(object sender, EventArgs e)
  {
  }

  private void miSaveTemplateAs_Click(object sender, EventArgs e)
  {
  }

  private void miSetTemplate_Click(object sender, EventArgs e)
  {
  }

  private void miExit_Click(object sender, EventArgs e)
  {
  }

  private void miShowTemplate_Click(object sender, EventArgs e)
  {
  }

  private void miShowDocument_Click(object sender, EventArgs e)
  {
  }

  private void miProperties__Click(object sender, EventArgs e)
  {
  }

  private void miDocumentTreeView_Click(object sender, EventArgs e)
  {
  }

  private void miConfig_Click(object sender, EventArgs e)
  {
  }

  private void miAbout_Click(object sender, EventArgs e)
  {
  }

  private void menuButtonItem1_Click(object sender, EventArgs e)
  {
  }

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    this._objtypesIcons = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    this.ShowObjects();
    this.ShowImBase();
    this.ShowAttributes();
  }

  private void ShowImBase()
  {
    if (this.treeImBase.Nodes.Count > 0)
      return;
    this.treeImBase.Nodes.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(new Guid("cad00221-306c-11d8-b4e9-00304f19f545"));
      int objectTypeId = MetaDataHelper.GetObjectTypeID(new Guid("cad00221-306c-11d8-b4e9-00304f19f545"));
      if (this.imageListImbaseObjects.Images.Count == 1)
        this.imageListImbaseObjects.Images.Add((Image) this.GetObjTypeIcon(objectTypeId, false), Color.Transparent);
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[4]
      {
        (object) new Guid("cad00020-306c-11d8-b4e9-00304f19f545"),
        (object) new Guid("cad00130-306c-11d8-b4e9-00304f19f545"),
        (object) DemoDbCleanForm.CatalogTypeAttGUID,
        (object) new Guid("cad00029-306c-11d8-b4e9-00304f19f545")
      });
      DataTable dataTable = objectCollection.Select(paramSet);
      List<ImBaseCatalog> source = new List<ImBaseCatalog>();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        Guid guid = (Guid) row[1];
        string str1 = Convert.ToString(row[0]);
        string str2 = Convert.ToString(row[2]);
        long int64 = Convert.ToInt64(row[3]);
        source.Add(new ImBaseCatalog()
        {
          ObjectId = guid,
          Name = str1,
          Type = str2,
          Id = int64
        });
      }
      foreach (var data in source.GroupBy<ImBaseCatalog, string>((System.Func<ImBaseCatalog, string>) (c => c.Type)).Select(g => new
      {
        Type = g.Key,
        Items = g.OrderBy<ImBaseCatalog, string>((System.Func<ImBaseCatalog, string>) (c => c.Name))
      }))
      {
        TreeListNode parentNode = this.treeImBase.AppendNode((object) new object[1]
        {
          (object) data.Type
        }, (TreeListNode) null);
        parentNode.ImageIndex = 0;
        parentNode.SelectImageIndex = parentNode.ImageIndex;
        foreach (ImBaseCatalog imBaseCatalog in (IEnumerable<ImBaseCatalog>) data.Items)
        {
          TreeListNode treeListNode = this.treeImBase.AppendNode((object) new object[1]
          {
            (object) imBaseCatalog.Name
          }, parentNode);
          treeListNode.Tag = (object) imBaseCatalog;
          treeListNode.ImageIndex = 1;
          treeListNode.SelectImageIndex = treeListNode.ImageIndex;
          treeListNode.StateImageIndex = 0;
        }
      }
    }
  }

  private void ShowObjects()
  {
    if (this.treeObjTypes.Nodes.Count > 0)
      return;
    this.treeObjTypes.BeginUnboundLoad();
    try
    {
      this.treeObjTypes.Nodes.Clear();
      List<Guid> objectTypesGuids = MetaDataHelper.GetTopObjectTypesGuids();
      List<IMSObjectType> imsObjectTypeList = new List<IMSObjectType>();
      foreach (Guid objTypeGuid in objectTypesGuids)
      {
        IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeGuid);
        imsObjectTypeList.Add(objectType);
      }
      imsObjectTypeList.Sort((Comparison<IMSObjectType>) ((x, y) => x.ObjectTypeName.CompareTo(y.ObjectTypeName)));
      foreach (IMSObjectType imsObjectType in imsObjectTypeList)
      {
        TreeListNode treeListNode = this.treeObjTypes.AppendNode((object) new object[1]
        {
          (object) imsObjectType.ObjectTypeName
        }, (TreeListNode) null);
        treeListNode.Tag = (object) imsObjectType;
        int num = this.imageListObjects.Images.Add((Image) this.GetObjTypeIcon(imsObjectType.ObjectTypeID, true), Color.Transparent);
        treeListNode.ImageIndex = num;
        treeListNode.SelectImageIndex = num;
        treeListNode.StateImageIndex = 0;
      }
      this.ShowObjects(this.treeObjTypes.Nodes);
    }
    finally
    {
      this.treeObjTypes.EndUnboundLoad();
    }
  }

  private void RemoveSystemObjTypes(TreeListNodes nodes)
  {
    List<TreeListNode> treeListNodeList = new List<TreeListNode>();
    foreach (TreeListNode node in nodes)
    {
      this.RemoveSystemObjTypes(node.Nodes);
      if (node.Nodes.Count == 0 && node.Tag is IMSObjectType tag && SystemGUIDs.IsSystemGUID(tag.Guid))
        treeListNodeList.Add(node);
    }
    foreach (TreeListNode node in treeListNodeList)
      nodes.Remove(node);
  }

  private void ShowObjects(TreeListNodes nodes)
  {
    foreach (TreeListNode node in nodes)
    {
      this.UpdateChilds(node);
      this.ShowObjects(node.Nodes);
    }
  }

  private void ShowObjects1()
  {
    if (this.treeObjTypes.Nodes.Count > 0)
      return;
    this.treeObjTypes.Nodes.Clear();
    List<Guid> objectTypesGuids = MetaDataHelper.GetTopObjectTypesGuids();
    List<IMSObjectType> imsObjectTypeList = new List<IMSObjectType>();
    foreach (Guid objTypeGuid in objectTypesGuids)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeGuid);
      imsObjectTypeList.Add(objectType);
    }
    imsObjectTypeList.Sort((Comparison<IMSObjectType>) ((x, y) => x.ObjectTypeName.CompareTo(y.ObjectTypeName)));
    foreach (IMSObjectType imsObjectType in imsObjectTypeList)
    {
      TreeListNode parentnode = this.treeObjTypes.AppendNode((object) new object[1]
      {
        (object) imsObjectType.ObjectTypeName
      }, (TreeListNode) null);
      parentnode.Tag = (object) imsObjectType;
      int num = this.imageListObjects.Images.Add((Image) this.GetObjTypeIcon(imsObjectType.ObjectTypeID, true), Color.Transparent);
      parentnode.ImageIndex = num;
      parentnode.SelectImageIndex = num;
      parentnode.StateImageIndex = 0;
      this.UpdateChilds(parentnode);
    }
  }

  private void UpdateChilds(TreeListNode parentnode)
  {
    if (parentnode.Nodes.Count > 0)
      return;
    List<Guid> typeChildrenGuid = MetaDataHelper.GetObjectTypeChildrenGuid((parentnode.Tag as IMSObjectType).ObjectTypeID);
    List<IMSObjectType> imsObjectTypeList = new List<IMSObjectType>();
    foreach (Guid objTypeGuid in typeChildrenGuid)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeGuid);
      imsObjectTypeList.Add(objectType);
    }
    imsObjectTypeList.Sort((Comparison<IMSObjectType>) ((x, y) => x.ObjectTypeName.CompareTo(y.ObjectTypeName)));
    foreach (IMSObjectType imsObjectType in imsObjectTypeList)
    {
      TreeListNode treeListNode = this.treeObjTypes.AppendNode((object) new object[1]
      {
        (object) imsObjectType.ObjectTypeName
      }, parentnode);
      treeListNode.Tag = (object) imsObjectType;
      int num = this.imageListObjects.Images.Add((Image) this.GetObjTypeIcon(imsObjectType.ObjectTypeID, true), Color.Transparent);
      treeListNode.ImageIndex = num;
      treeListNode.SelectImageIndex = num;
      treeListNode.StateImageIndex = parentnode.StateImageIndex;
    }
  }

  /// <summary>Вернуть значок для указанного типа объекта</summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <returns>Значок для указанного типа объекта</returns>
  protected virtual Bitmap GetAttrTypeIcon(int objTypeID)
  {
    objTypeID = Math.Max(objTypeID, -1);
    if (this._objtypesIcons.IndexOf(3, -1, (object) (FieldTypes) objTypeID) < 0)
      return (Bitmap) null;
    return ImagesResizeHelper.ResizeIconTo16x16(this._objtypesIcons.GetIcon(3, -1, (object) (FieldTypes) objTypeID), Color.Transparent)?.ToBitmap();
  }

  /// <summary>Вернуть значок для указанного типа объекта</summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <returns>Значок для указанного типа объекта</returns>
  protected virtual Bitmap GetObjTypeIcon(int objTypeID, bool is32)
  {
    objTypeID = Math.Max(objTypeID, -1);
    if (this._objtypesIcons.IndexOf(4, objTypeID) < 0)
      return (Bitmap) null;
    return (!is32 ? ImagesResizeHelper.ResizeIconTo16x16(this._objtypesIcons.GetIcon(4, objTypeID), Color.Transparent) : ImagesResizeHelper.ResizeIconTo32x16(this._objtypesIcons.GetIcon(4, objTypeID), Color.Transparent))?.ToBitmap();
  }

  private void treeObjTypes_BeforeExpand(object sender, BeforeExpandEventArgs e)
  {
  }

  private void treeObjTypes_MouseUp(object sender, MouseEventArgs e)
  {
  }

  private void treeObjTypes_StateImageClick(object sender, NodeClickEventArgs e)
  {
    if (!(e.Node.Tag is IMSObjectType))
      return;
    if (e.Node.StateImageIndex == 1)
      e.Node.StateImageIndex = 0;
    else
      ++e.Node.StateImageIndex;
    this.UpdateStates(e.Node);
  }

  private void UpdateStates(TreeListNode node)
  {
    foreach (TreeListNode node1 in node.Nodes)
    {
      node1.StateImageIndex = node.StateImageIndex;
      this.UpdateStates(node1);
    }
  }

  private void treeObjTypes_GetCustomNodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
  {
  }

  private void UpdateSelection(TreeListNodes nodes, DemoDbCleanForm.SelectEnum e)
  {
    nodes.TreeList.BeginUpdate();
    try
    {
      foreach (TreeListNode node in nodes)
      {
        if (node.Tag is IMSObjectType)
        {
          switch (e)
          {
            case DemoDbCleanForm.SelectEnum.SelectAll:
              node.StateImageIndex = 1;
              break;
            case DemoDbCleanForm.SelectEnum.UnselectAll:
              node.StateImageIndex = 0;
              break;
            case DemoDbCleanForm.SelectEnum.Invert:
              node.StateImageIndex = node.StateImageIndex == 1 ? 0 : 1;
              break;
          }
        }
        if (node.Tag is DemoDbCleanForm.Attribute)
        {
          switch (e)
          {
            case DemoDbCleanForm.SelectEnum.SelectAll:
              node.StateImageIndex = 1;
              break;
            case DemoDbCleanForm.SelectEnum.UnselectAll:
              node.StateImageIndex = 0;
              break;
            case DemoDbCleanForm.SelectEnum.Invert:
              node.StateImageIndex = node.StateImageIndex == 1 ? 0 : 1;
              break;
          }
        }
        this.UpdateSelection(node.Nodes, e);
      }
    }
    finally
    {
      nodes.TreeList.EndUpdate();
    }
  }

  private void UpdateSelectionImBase(TreeListNodes nodes, DemoDbCleanForm.SelectEnum e)
  {
    foreach (TreeListNode node in nodes)
    {
      if (node.Tag is ImBaseCatalog)
      {
        switch (e)
        {
          case DemoDbCleanForm.SelectEnum.SelectAll:
            node.StateImageIndex = 1;
            break;
          case DemoDbCleanForm.SelectEnum.UnselectAll:
            node.StateImageIndex = 0;
            break;
          case DemoDbCleanForm.SelectEnum.Invert:
            node.StateImageIndex = node.StateImageIndex == 1 ? 0 : 1;
            break;
        }
      }
      this.UpdateSelectionImBase(node.Nodes, e);
    }
  }

  private void treeObjTypes_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
  {
  }

  private void bSelectAll_Click(object sender, EventArgs e)
  {
    if (this.tabControl1.SelectedIndex == 0)
      this.UpdateSelection(this.treeObjTypes.Nodes, DemoDbCleanForm.SelectEnum.SelectAll);
    if (this.tabControl1.SelectedIndex == 1)
      this.UpdateSelectionImBase(this.treeImBase.Nodes, DemoDbCleanForm.SelectEnum.SelectAll);
    if (this.tabControl1.SelectedIndex != 2)
      return;
    this.UpdateSelection(this.treeListAttrs.Nodes, DemoDbCleanForm.SelectEnum.SelectAll);
  }

  private void bInvertSelection_Click(object sender, EventArgs e)
  {
    if (this.tabControl1.SelectedIndex == 0)
      this.UpdateSelection(this.treeObjTypes.Nodes, DemoDbCleanForm.SelectEnum.Invert);
    if (this.tabControl1.SelectedIndex == 1)
      this.UpdateSelectionImBase(this.treeImBase.Nodes, DemoDbCleanForm.SelectEnum.Invert);
    if (this.tabControl1.SelectedIndex != 2)
      return;
    this.UpdateSelection(this.treeListAttrs.Nodes, DemoDbCleanForm.SelectEnum.Invert);
  }

  private void bUnselectAll_Click(object sender, EventArgs e)
  {
    if (this.tabControl1.SelectedIndex == 0)
      this.UpdateSelection(this.treeObjTypes.Nodes, DemoDbCleanForm.SelectEnum.UnselectAll);
    if (this.tabControl1.SelectedIndex == 1)
      this.UpdateSelectionImBase(this.treeImBase.Nodes, DemoDbCleanForm.SelectEnum.UnselectAll);
    if (this.tabControl1.SelectedIndex != 2)
      return;
    this.UpdateSelection(this.treeListAttrs.Nodes, DemoDbCleanForm.SelectEnum.UnselectAll);
  }

  private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.tabControl1.SelectedIndex == 0)
      this.ShowObjects();
    if (this.tabControl1.SelectedIndex == 1)
      this.ShowImBase();
    if (this.tabControl1.SelectedIndex != 2)
      return;
    this.ShowAttributes();
  }

  private void repositoryItemButtonEditClean_ButtonPressed(object sender, ButtonPressedEventArgs e)
  {
  }

  private void repositoryItemButtonEditDelete_ButtonPressed(object sender, ButtonPressedEventArgs e)
  {
  }

  private void treeImBase_StateImageClick(object sender, NodeClickEventArgs e)
  {
    if (e.Node.StateImageIndex == 0)
      return;
    e.Node.StateImageIndex = 0;
  }

  private void repositoryItemButtonEditClean_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
  }

  private void repositoryItemButtonEditClean_Validating(object sender, CancelEventArgs e)
  {
  }

  private void repositoryClean_Click(object sender, EventArgs e)
  {
  }

  private void repositoryClean_ButtonPressed(object sender, ButtonPressedEventArgs e)
  {
    this.treeImBase.FocusedNode.StateImageIndex = 2;
  }

  private void repositoryClean_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
  }

  private void repositoryDelete_ButtonPressed(object sender, ButtonPressedEventArgs e)
  {
    this.treeImBase.FocusedNode.StateImageIndex = 1;
  }

  private void treeImBase_GetCustomNodeCellEdit(object sender, GetCustomNodeCellEditEventArgs e)
  {
    ImBaseCatalog tag = e.Node.Tag as ImBaseCatalog;
    if (e.Column == this.treeListColumnClean)
      e.RepositoryItem = tag == null ? (RepositoryItem) null : (RepositoryItem) this.repositoryClean;
    if (e.Column != this.treeListColumnDelete)
      return;
    if (tag != null)
      e.RepositoryItem = (RepositoryItem) this.repositoryDelete;
    else
      e.RepositoryItem = (RepositoryItem) null;
  }

  private void ShowAttributes()
  {
    if (this.treeListAttrs.Nodes.Count > 0)
      return;
    this.treeListAttrs.BeginUnboundLoad();
    this.treeListAttrs.Nodes.Clear();
    try
    {
      List<int> attributesInGroup = MetaDataHelper.GetAttributesInGroup(-10);
      DataTable dataTable = DataHolders.AttributesHolder.LoadData(false, (object) -1);
      int num1 = 0;
      int num2 = 0;
      DataView defaultView = dataTable.DefaultView;
      defaultView.Sort = "F_NAME";
      foreach (DataRow row in (InternalDataCollectionBase) defaultView.ToTable().Rows)
      {
        int int32_1 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
        string str = Convert.ToString(row["F_NAME"]);
        int int32_2 = Convert.ToInt32(row["F_ATTRIBUTE_TYPE"]);
        Guid aGUID = (Guid) row["F_GUID"];
        ++num1;
        if (!SystemGUIDs.IsSystemGUID(aGUID) && !attributesInGroup.Contains(int32_1))
        {
          ++num2;
          TreeListNode treeListNode = this.treeListAttrs.AppendNode((object) new object[1]
          {
            (object) str
          }, (TreeListNode) null);
          int num3 = this.imageListAttrs.Images.IndexOfKey(int32_2.ToString());
          if (num3 == -1)
          {
            Bitmap attrTypeIcon = this.GetAttrTypeIcon(int32_2);
            this.imageListAttrs.Images.Add(int32_2.ToString(), (Image) attrTypeIcon);
            num3 = this.imageListAttrs.Images.IndexOfKey(int32_2.ToString());
          }
          treeListNode.ImageIndex = num3;
          treeListNode.StateImageIndex = 0;
          treeListNode.Tag = (object) new DemoDbCleanForm.Attribute()
          {
            Id = int32_1,
            Name = str,
            Type = int32_2
          };
        }
      }
    }
    finally
    {
      this.treeListAttrs.EndUnboundLoad();
    }
  }

  private void treeListAttrs_BeforeExpand(object sender, BeforeExpandEventArgs e)
  {
  }

  private void treeListAttrs_StateImageClick(object sender, NodeClickEventArgs e)
  {
    e.Node.StateImageIndex = e.Node.StateImageIndex == 0 ? 1 : 0;
  }

  private void treeListAttrs_SelectionChanged(object sender, EventArgs e)
  {
  }

  private void treeListAttrs_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
  {
    if (!this.treeAttrAplicability.Visible)
    {
      this.treeAttrAplicability.Nodes.Clear();
    }
    else
    {
      if (this.treeListAttrs.FocusedNode == null || !(this.treeListAttrs.FocusedNode.Tag is DemoDbCleanForm.Attribute tag))
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        DataTable attributeApplicability = (sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) as IAdminUtilsService).GetAttributeApplicability(sessionKeeper.Session.SessionGUID, tag.Id);
        try
        {
          this.treeAttrAplicability.BeginUnboundLoad();
          this.treeAttrAplicability.Nodes.Clear();
          this.imageListAppObjects.Images.Clear();
          foreach (DataRow row in (InternalDataCollectionBase) attributeApplicability.Rows)
          {
            long int64 = Convert.ToInt64(row["F_OBJECT_ID"]);
            string str = Convert.ToString(row["CAPTION"]);
            int num = this.imageListAppObjects.Images.Add((Image) this.GetObjTypeIcon(Convert.ToInt32(row["F_OBJECT_TYPE"]), true), Color.Transparent);
            TreeListNode treeListNode = this.treeAttrAplicability.AppendNode((object) new object[2]
            {
              (object) int64,
              (object) str
            }, (TreeListNode) null);
            treeListNode.ImageIndex = num;
            treeListNode.SelectImageIndex = num;
            treeListNode.Tag = (object) int64;
          }
        }
        finally
        {
          this.treeAttrAplicability.EndUnboundLoad();
        }
      }
    }
  }

  private void bOpen_Click(object sender, EventArgs e)
  {
    if (this.openFileDialog1.ShowDialog() != DialogResult.OK)
      return;
    CleanSchema cleanSchema = (CleanSchema) null;
    FileStream fileStream = new FileStream(this.saveFileDialog1.FileName, FileMode.Open);
    XmlSerializer xmlSerializer = new XmlSerializer(typeof (CleanSchema));
    try
    {
      cleanSchema = xmlSerializer.Deserialize((Stream) fileStream) as CleanSchema;
    }
    catch (Exception ex)
    {
      Console.WriteLine("Failed to serialize. Reason: " + ex.Message);
      throw;
    }
    finally
    {
      fileStream.Close();
    }
    if (cleanSchema == null)
      return;
    this.treeObjTypes.NodesIterator.DoOperation((TreeListOperation) new DemoDbCleanForm.SelectFromSchema()
    {
      Cs = cleanSchema
    });
    this.treeImBase.NodesIterator.DoOperation((TreeListOperation) new DemoDbCleanForm.SelectFromSchema()
    {
      Cs = cleanSchema
    });
    this.treeListAttrs.NodesIterator.DoOperation((TreeListOperation) new DemoDbCleanForm.SelectFromSchema()
    {
      Cs = cleanSchema
    });
  }

  private void bSave_Click(object sender, EventArgs e)
  {
    if (this.saveFileDialog1.ShowDialog() != DialogResult.OK)
      return;
    CleanSchema cleanSchema = this.GetCleanSchema();
    FileStream fileStream = new FileStream(this.saveFileDialog1.FileName, FileMode.Create);
    XmlSerializer xmlSerializer = new XmlSerializer(cleanSchema.GetType());
    try
    {
      xmlSerializer.Serialize((Stream) fileStream, (object) cleanSchema);
    }
    catch (SerializationException ex)
    {
      Console.WriteLine("Failed to serialize. Reason: " + ex.Message);
      throw;
    }
    finally
    {
      fileStream.Close();
    }
  }

  private void bExecute_Click(object sender, EventArgs e)
  {
    if (this.tabControl1.SelectedIndex == 0)
      this.Execute(ExecuteMode.Objects);
    if (this.tabControl1.SelectedIndex == 1)
      this.Execute(ExecuteMode.Catalogs);
    if (this.tabControl1.SelectedIndex != 2)
      return;
    this.Execute(ExecuteMode.Attributes);
  }

  private void bExecuteAll_Click(object sender, EventArgs e) => this.Execute(ExecuteMode.All);

  private void Execute(ExecuteMode mode)
  {
    ProgressForm progressForm = new ProgressForm();
    progressForm.Argument = (object) mode;
    progressForm.DoWork += new ProgressForm.DoWorkEventHandler(this.form_DoWork);
    int num = (int) progressForm.ShowDialog();
  }

  private void form_DoWork(ProgressForm sender, DoWorkEventArgs ea)
  {
    ExecuteMode executeMode = (ExecuteMode) ea.Argument;
    CleanSchema cleanSchema = this.GetCleanSchema();
    List<string> stringList = new List<string>();
    int percent = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IAdminUtilsService customService = sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) as IAdminUtilsService;
      if (executeMode == ExecuteMode.All || executeMode == ExecuteMode.Objects)
      {
        sender.SetProgress(percent, "Очистка типов объектов");
        percent += 25;
        try
        {
          stringList.Add("-------Очистка типов объектов-------");
          if (cleanSchema.ObjectTypes.Count > 0)
          {
            string[] collection = customService.PurgeObjectsByType(sessionKeeper.Session.SessionGUID, cleanSchema.ObjectTypes.ToArray());
            stringList.AddRange((IEnumerable<string>) collection);
          }
        }
        catch (Exception ex)
        {
          stringList.Add("ОШИБКА: " + ex.Message);
        }
      }
      if (executeMode == ExecuteMode.All || executeMode == ExecuteMode.Catalogs)
      {
        sender.SetProgress(percent, "Очистка  каталогов ImBase");
        percent += 25;
        stringList.Add("-------Очистка каталогов ImBase-------");
        if (cleanSchema.Catalogs.Count > 0)
        {
          foreach (ImBaseCatalog catalog in cleanSchema.Catalogs)
          {
            try
            {
              string[] collection = customService.PurgeIMBASECatalog(sessionKeeper.Session.SessionGUID, catalog.Id, catalog.CleanMode == CleanEnum.Delete);
              stringList.AddRange((IEnumerable<string>) collection);
            }
            catch (Exception ex)
            {
              stringList.Add("ОШИБКА: " + ex.Message);
            }
          }
        }
      }
      if (executeMode != ExecuteMode.All)
      {
        if (executeMode != ExecuteMode.Attributes)
          goto label_29;
      }
      sender.SetProgress(percent, "Очистка атрибутов");
      stringList.Add("-------Очистка атрибутов-------");
      if (cleanSchema.Attributes.Count > 0)
      {
        foreach (int attribute in cleanSchema.Attributes)
        {
          try
          {
            IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(attribute);
            if (attributeType != null)
            {
              string name = attributeType.Name;
              attributeType.Delete(0L);
              stringList.Add($"Удален атрибут '{name}'");
            }
          }
          catch (Exception ex)
          {
            stringList.Add("ОШИБКА: " + ex.Message);
          }
        }
      }
    }
label_29:
    IOutputView service = ServicesManager.GetService(typeof (IOutputView)) as IOutputView;
    string category = "Очистка демонстрационной БД";
    service.ClearText(category);
    foreach (string text in stringList)
      service.WriteString(category, text);
    service.ShowView();
    service.Activate(category);
  }

  private void GetSelectedImBase(TreeListNodes nodes, List<ImBaseCatalog> types)
  {
    foreach (TreeListNode node in nodes)
    {
      if (node.Tag is ImBaseCatalog tag)
      {
        switch (node.StateImageIndex)
        {
          case 0:
            tag.CleanMode = CleanEnum.None;
            break;
          case 1:
            tag.CleanMode = CleanEnum.Delete;
            break;
          case 2:
            tag.CleanMode = CleanEnum.Clean;
            break;
        }
        if (node.StateImageIndex != 0)
          types.Add(tag);
      }
      this.GetSelectedImBase(node.Nodes, types);
    }
  }

  private void GetSelectedAttributes(TreeListNodes nodes, List<int> types)
  {
    foreach (TreeListNode node in nodes)
    {
      if (node.Tag is DemoDbCleanForm.Attribute tag && node.StateImageIndex == 1)
        types.Add(tag.Id);
      this.GetSelectedAttributes(node.Nodes, types);
    }
  }

  private void GetSelectedObjTypes(TreeListNodes nodes, List<int> types)
  {
    foreach (TreeListNode node in nodes)
    {
      if (node.Tag is IMSObjectType tag && node.StateImageIndex == 1)
        types.Add(tag.ObjectTypeID);
      this.GetSelectedObjTypes(node.Nodes, types);
    }
  }

  private CleanSchema GetCleanSchema()
  {
    CleanSchema cleanSchema = new CleanSchema();
    if (this.treeObjTypes.Nodes.Count > 0)
    {
      List<int> types = new List<int>();
      this.GetSelectedObjTypes(this.treeObjTypes.Nodes, types);
      cleanSchema.ObjectTypes = types;
    }
    else if (this.activeSchema != null)
      cleanSchema.ObjectTypes = this.activeSchema.ObjectTypes;
    if (this.treeListAttrs.Nodes.Count > 0)
    {
      List<int> types = new List<int>();
      this.GetSelectedAttributes(this.treeListAttrs.Nodes, types);
      cleanSchema.Attributes = types;
    }
    else if (this.activeSchema != null)
      cleanSchema.Attributes = this.activeSchema.Attributes;
    if (this.treeImBase.Nodes.Count > 0)
    {
      List<ImBaseCatalog> types = new List<ImBaseCatalog>();
      this.GetSelectedImBase(this.treeImBase.Nodes, types);
      cleanSchema.Catalogs = types;
    }
    else if (this.activeSchema != null)
      cleanSchema.Catalogs = this.activeSchema.Catalogs;
    return cleanSchema;
  }

  private void bCard_Click(object sender, EventArgs e)
  {
    if (this.treeAttrAplicability.FocusedNode == null)
      return;
    int num = (int) PropertiesWindow.Execute("", "", Convert.ToInt64(this.treeAttrAplicability.FocusedNode.Tag));
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DemoDbCleanForm));
    this.repositoryItemButtonEditClean = new RepositoryItemButtonEdit();
    this.repositoryItemButtonEditDelete = new RepositoryItemButtonEdit();
    this.tabControl1 = new System.Windows.Forms.TabControl();
    this.tObjects = new System.Windows.Forms.TabPage();
    this.treeObjTypes = new TreeList();
    this.treeListColumn1 = new TreeListColumn();
    this.imageListObjects = new ImageList(this.components);
    this.imageListObjectsState = new ImageList(this.components);
    this.imageListBoxControl1 = new ImageListBoxControl();
    this.tImbase = new System.Windows.Forms.TabPage();
    this.treeImBase = new TreeList();
    this.treeListColumn2 = new TreeListColumn();
    this.treeListColumnDelete = new TreeListColumn();
    this.treeListColumnClean = new TreeListColumn();
    this.repositoryClean = new RepositoryItemButtonEdit();
    this.repositoryDelete = new RepositoryItemButtonEdit();
    this.imageListImbaseObjects = new ImageList(this.components);
    this.imageListImBaseState = new ImageList(this.components);
    this.tAttrs = new System.Windows.Forms.TabPage();
    this.treeListAttrs = new TreeList();
    this.treeListColumn3 = new TreeListColumn();
    this.imageListAttrs = new ImageList(this.components);
    this.collapsibleSplitter1 = new CollapsibleSplitter();
    this.panel1 = new Panel();
    this.treeAttrAplicability = new TreeList();
    this.colId = new TreeListColumn();
    this.colCaption = new TreeListColumn();
    this.imageListAppObjects = new ImageList(this.components);
    this.toolBar1 = new Intermech.Bars.ToolBar();
    this.bCard = new ButtonItem();
    this.barManager = new BarManager();
    this.leftBarDock = new ToolBarContainer();
    this.rightBarDock = new ToolBarContainer();
    this.bottomBarDock = new ToolBarContainer();
    this.topBarDock = new ToolBarContainer();
    this.menuBar1 = new MenuBar();
    this.contextMenuBarItem1 = new ContextMenuBarItem();
    this.menuButtonItem1 = new MenuButtonItem();
    this.menuButtonItem2 = new MenuButtonItem();
    this.menuButtonItem3 = new MenuButtonItem();
    this.pageElementsToolBar = new Intermech.Bars.ToolBar();
    this.bSelectAll = new ButtonItem();
    this.bInvertSelection = new ButtonItem();
    this.bUnselectAll = new ButtonItem();
    this.bOpen = new ButtonItem();
    this.bSave = new ButtonItem();
    this.bExecute = new ButtonItem();
    this.bExecuteAll = new ButtonItem();
    this.saveFileDialog1 = new SaveFileDialog();
    this.openFileDialog1 = new OpenFileDialog();
    this.repositoryItemButtonEditClean.BeginInit();
    this.repositoryItemButtonEditDelete.BeginInit();
    this.tabControl1.SuspendLayout();
    this.tObjects.SuspendLayout();
    this.treeObjTypes.BeginInit();
    ((ISupportInitialize) this.imageListBoxControl1).BeginInit();
    this.tImbase.SuspendLayout();
    this.treeImBase.BeginInit();
    this.repositoryClean.BeginInit();
    this.repositoryDelete.BeginInit();
    this.tAttrs.SuspendLayout();
    this.treeListAttrs.BeginInit();
    this.panel1.SuspendLayout();
    this.treeAttrAplicability.BeginInit();
    this.topBarDock.SuspendLayout();
    this.SuspendLayout();
    this.repositoryItemButtonEditClean.AutoHeight = false;
    this.repositoryItemButtonEditClean.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("repositoryItemButtonEditClean.Buttons"))
    });
    this.repositoryItemButtonEditClean.Name = "repositoryItemButtonEditClean";
    this.repositoryItemButtonEditClean.ReadOnly = true;
    this.repositoryItemButtonEditClean.TextEditStyle = TextEditStyles.HideTextEditor;
    this.repositoryItemButtonEditClean.ButtonClick += new ButtonPressedEventHandler(this.repositoryItemButtonEditClean_ButtonClick);
    this.repositoryItemButtonEditClean.ButtonPressed += new ButtonPressedEventHandler(this.repositoryItemButtonEditClean_ButtonPressed);
    this.repositoryItemButtonEditClean.Validating += new System.ComponentModel.CancelEventHandler(this.repositoryItemButtonEditClean_Validating);
    this.repositoryItemButtonEditDelete.AutoHeight = false;
    this.repositoryItemButtonEditDelete.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("repositoryItemButtonEditDelete.Buttons"))
    });
    this.repositoryItemButtonEditDelete.Name = "repositoryItemButtonEditDelete";
    this.repositoryItemButtonEditDelete.ReadOnly = true;
    this.repositoryItemButtonEditDelete.TextEditStyle = TextEditStyles.HideTextEditor;
    this.repositoryItemButtonEditDelete.ButtonPressed += new ButtonPressedEventHandler(this.repositoryItemButtonEditDelete_ButtonPressed);
    this.tabControl1.Controls.Add((Control) this.tObjects);
    this.tabControl1.Controls.Add((Control) this.tImbase);
    this.tabControl1.Controls.Add((Control) this.tAttrs);
    this.tabControl1.Dock = DockStyle.Fill;
    this.tabControl1.Location = new Point(0, 54);
    this.tabControl1.Name = "tabControl1";
    this.tabControl1.SelectedIndex = 0;
    this.tabControl1.Size = new Size(698, 466);
    this.tabControl1.TabIndex = 0;
    this.tabControl1.SelectedIndexChanged += new EventHandler(this.tabControl1_SelectedIndexChanged);
    this.tObjects.Controls.Add((Control) this.treeObjTypes);
    this.tObjects.Controls.Add((Control) this.imageListBoxControl1);
    this.tObjects.Location = new Point(4, 22);
    this.tObjects.Name = "tObjects";
    this.tObjects.Padding = new Padding(3);
    this.tObjects.Size = new Size(690, 440);
    this.tObjects.TabIndex = 0;
    this.tObjects.Text = "Объекты";
    this.tObjects.UseVisualStyleBackColor = true;
    this.treeObjTypes.Columns.AddRange(new TreeListColumn[1]
    {
      this.treeListColumn1
    });
    this.treeObjTypes.Dock = DockStyle.Fill;
    this.treeObjTypes.Location = new Point(3, 3);
    this.treeObjTypes.Name = "treeObjTypes";
    this.treeObjTypes.SelectImageList = this.imageListObjects;
    this.treeObjTypes.Size = new Size(406, 434);
    this.treeObjTypes.StateImageList = this.imageListObjectsState;
    this.treeObjTypes.Styles.AddReplace("InactiveStyle", (object) new ViewStyle("InactiveStyle", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, Color.LightGray, SystemColors.WindowText));
    this.treeObjTypes.TabIndex = 1;
    this.treeObjTypes.Text = "treeList1";
    this.treeObjTypes.StateImageClick += new NodeClickEventHandler(this.treeObjTypes_StateImageClick);
    this.treeObjTypes.GetCustomNodeCellStyle += new GetCustomNodeCellStyleEventHandler(this.treeObjTypes_GetCustomNodeCellStyle);
    this.treeObjTypes.BeforeExpand += new BeforeExpandEventHandler(this.treeObjTypes_BeforeExpand);
    this.treeObjTypes.CustomDrawNodeCell += new CustomDrawNodeCellEventHandler(this.treeObjTypes_CustomDrawNodeCell);
    this.treeObjTypes.MouseUp += new MouseEventHandler(this.treeObjTypes_MouseUp);
    this.treeListColumn1.Name = "treeListColumn1";
    this.treeListColumn1.Options = ColumnOptions.ReadOnly;
    this.treeListColumn1.VisibleIndex = 0;
    this.imageListObjects.ColorDepth = ColorDepth.Depth32Bit;
    this.imageListObjects.ImageSize = new Size(32 /*0x20*/, 16 /*0x10*/);
    this.imageListObjects.TransparentColor = Color.Transparent;
    this.imageListObjectsState.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageListObjectsState.ImageStream");
    this.imageListObjectsState.TransparentColor = Color.Transparent;
    this.imageListObjectsState.Images.SetKeyName(0, "selection.png");
    this.imageListObjectsState.Images.SetKeyName(1, "selection_delete.png");
    this.imageListBoxControl1.Dock = DockStyle.Right;
    this.imageListBoxControl1.ItemHeight = 15;
    this.imageListBoxControl1.Location = new Point(409, 3);
    this.imageListBoxControl1.Name = "imageListBoxControl1";
    this.imageListBoxControl1.Size = new Size(278, 434);
    this.imageListBoxControl1.TabIndex = 2;
    this.imageListBoxControl1.Visible = false;
    this.tImbase.Controls.Add((Control) this.treeImBase);
    this.tImbase.Location = new Point(4, 22);
    this.tImbase.Name = "tImbase";
    this.tImbase.Padding = new Padding(3);
    this.tImbase.Size = new Size(690, 440);
    this.tImbase.TabIndex = 1;
    this.tImbase.Text = "Imbase";
    this.tImbase.UseVisualStyleBackColor = true;
    this.treeImBase.Columns.AddRange(new TreeListColumn[3]
    {
      this.treeListColumn2,
      this.treeListColumnDelete,
      this.treeListColumnClean
    });
    this.treeImBase.Dock = DockStyle.Fill;
    this.treeImBase.Location = new Point(3, 3);
    this.treeImBase.Name = "treeImBase";
    this.treeImBase.RepositoryItems.AddRange(new RepositoryItem[2]
    {
      (RepositoryItem) this.repositoryClean,
      (RepositoryItem) this.repositoryDelete
    });
    this.treeImBase.SelectImageList = this.imageListImbaseObjects;
    this.treeImBase.Size = new Size(684, 434);
    this.treeImBase.StateImageList = this.imageListImBaseState;
    this.treeImBase.Styles.AddReplace("InactiveStyle", (object) new ViewStyle("InactiveStyle", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, Color.LightGray, SystemColors.WindowText));
    this.treeImBase.TabIndex = 2;
    this.treeImBase.Text = "treeList1";
    this.treeImBase.StateImageClick += new NodeClickEventHandler(this.treeImBase_StateImageClick);
    this.treeImBase.GetCustomNodeCellEdit += new GetCustomNodeCellEditEventHandler(this.treeImBase_GetCustomNodeCellEdit);
    this.treeListColumn2.Name = "treeListColumn2";
    this.treeListColumn2.Options = ColumnOptions.ReadOnly;
    this.treeListColumn2.VisibleIndex = 0;
    this.treeListColumn2.Width = 350;
    this.treeListColumnDelete.Caption = "Удалить";
    this.treeListColumnDelete.FieldName = "treeListColumn5";
    this.treeListColumnDelete.Name = "treeListColumnDelete";
    this.treeListColumnDelete.Options = ColumnOptions.FixedWidth | ColumnOptions.CanFocused;
    this.treeListColumnDelete.VisibleIndex = 1;
    this.treeListColumnDelete.Width = 28;
    this.treeListColumnClean.Caption = "Очистить";
    this.treeListColumnClean.FieldName = "treeListColumn4";
    this.treeListColumnClean.Name = "treeListColumnClean";
    this.treeListColumnClean.Options = ColumnOptions.FixedWidth | ColumnOptions.CanFocused;
    this.treeListColumnClean.VisibleIndex = 2;
    this.treeListColumnClean.Width = 28;
    this.repositoryClean.AutoHeight = false;
    this.repositoryClean.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("repositoryClean.Buttons"))
    });
    this.repositoryClean.Name = "repositoryClean";
    this.repositoryClean.ReadOnly = true;
    this.repositoryClean.TextEditStyle = TextEditStyles.HideTextEditor;
    this.repositoryClean.ButtonClick += new ButtonPressedEventHandler(this.repositoryClean_ButtonClick);
    this.repositoryClean.ButtonPressed += new ButtonPressedEventHandler(this.repositoryClean_ButtonPressed);
    this.repositoryClean.Click += new EventHandler(this.repositoryClean_Click);
    this.repositoryDelete.AutoHeight = false;
    this.repositoryDelete.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, HorzAlignment.Center, (Image) componentResourceManager.GetObject("repositoryDelete.Buttons"))
    });
    this.repositoryDelete.Name = "repositoryDelete";
    this.repositoryDelete.ReadOnly = true;
    this.repositoryDelete.TextEditStyle = TextEditStyles.HideTextEditor;
    this.repositoryDelete.ButtonPressed += new ButtonPressedEventHandler(this.repositoryDelete_ButtonPressed);
    this.imageListImbaseObjects.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageListImbaseObjects.ImageStream");
    this.imageListImbaseObjects.TransparentColor = Color.Transparent;
    this.imageListImbaseObjects.Images.SetKeyName(0, "folder.png");
    this.imageListImBaseState.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageListImBaseState.ImageStream");
    this.imageListImBaseState.TransparentColor = Color.Transparent;
    this.imageListImBaseState.Images.SetKeyName(0, "selection.png");
    this.imageListImBaseState.Images.SetKeyName(1, "selection_delete.png");
    this.imageListImBaseState.Images.SetKeyName(2, "Clean1.png");
    this.tAttrs.Controls.Add((Control) this.treeListAttrs);
    this.tAttrs.Controls.Add((Control) this.collapsibleSplitter1);
    this.tAttrs.Controls.Add((Control) this.panel1);
    this.tAttrs.Location = new Point(4, 22);
    this.tAttrs.Name = "tAttrs";
    this.tAttrs.Padding = new Padding(3);
    this.tAttrs.Size = new Size(690, 440);
    this.tAttrs.TabIndex = 2;
    this.tAttrs.Text = "Атрибуты";
    this.tAttrs.UseVisualStyleBackColor = true;
    this.treeListAttrs.Columns.AddRange(new TreeListColumn[1]
    {
      this.treeListColumn3
    });
    this.treeListAttrs.Dock = DockStyle.Fill;
    this.treeListAttrs.Location = new Point(3, 3);
    this.treeListAttrs.Name = "treeListAttrs";
    this.treeListAttrs.SelectImageList = this.imageListAttrs;
    this.treeListAttrs.Size = new Size(203, 434);
    this.treeListAttrs.StateImageList = this.imageListObjectsState;
    this.treeListAttrs.Styles.AddReplace("InactiveStyle", (object) new ViewStyle("InactiveStyle", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, Color.LightGray, SystemColors.WindowText));
    this.treeListAttrs.TabIndex = 2;
    this.treeListAttrs.Text = "treeList1";
    this.treeListAttrs.StateImageClick += new NodeClickEventHandler(this.treeListAttrs_StateImageClick);
    this.treeListAttrs.BeforeExpand += new BeforeExpandEventHandler(this.treeListAttrs_BeforeExpand);
    this.treeListAttrs.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this.treeListAttrs_FocusedNodeChanged);
    this.treeListAttrs.SelectionChanged += new EventHandler(this.treeListAttrs_SelectionChanged);
    this.treeListColumn3.Name = "treeListColumn3";
    this.treeListColumn3.Options = ColumnOptions.ReadOnly;
    this.treeListColumn3.VisibleIndex = 0;
    this.imageListAttrs.ColorDepth = ColorDepth.Depth32Bit;
    this.imageListAttrs.ImageSize = new Size(16 /*0x10*/, 16 /*0x10*/);
    this.imageListAttrs.TransparentColor = Color.Transparent;
    this.collapsibleSplitter1.AnimationDelay = 20;
    this.collapsibleSplitter1.AnimationStep = 20;
    this.collapsibleSplitter1.BorderStyle3D = Border3DStyle.Flat;
    this.collapsibleSplitter1.ControlToHide = (Control) this.panel1;
    this.collapsibleSplitter1.Dock = DockStyle.Right;
    this.collapsibleSplitter1.ExpandParentForm = false;
    this.collapsibleSplitter1.Location = new Point(206, 3);
    this.collapsibleSplitter1.Name = "collapsibleSplitter1";
    this.collapsibleSplitter1.TabIndex = 4;
    this.collapsibleSplitter1.TabStop = false;
    this.collapsibleSplitter1.UseAnimations = false;
    this.collapsibleSplitter1.VisualStyle = VisualStyles.Mozilla;
    this.panel1.Controls.Add((Control) this.treeAttrAplicability);
    this.panel1.Controls.Add((Control) this.toolBar1);
    this.panel1.Dock = DockStyle.Right;
    this.panel1.Location = new Point(209, 3);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(478, 434);
    this.panel1.TabIndex = 3;
    this.treeAttrAplicability.Columns.AddRange(new TreeListColumn[2]
    {
      this.colId,
      this.colCaption
    });
    this.treeAttrAplicability.Dock = DockStyle.Fill;
    this.treeAttrAplicability.IndicatorWidth = 4;
    this.treeAttrAplicability.Location = new Point(0, 0);
    this.treeAttrAplicability.Name = "treeAttrAplicability";
    this.treeAttrAplicability.BeginUnboundLoad();
    this.treeAttrAplicability.AppendNode((object) new object[2], -1, 0, 0, -1);
    this.treeAttrAplicability.AppendNode((object) new object[2], -1, 0, 0, -1);
    this.treeAttrAplicability.AppendNode((object) new object[2], -1, 0, 0, -1);
    this.treeAttrAplicability.EndUnboundLoad();
    this.treeAttrAplicability.SelectImageList = this.imageListAppObjects;
    this.treeAttrAplicability.Size = new Size(478, 434);
    this.treeAttrAplicability.Styles.AddReplace("InactiveStyle", (object) new ViewStyle("InactiveStyle", "", new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, Color.LightGray, SystemColors.WindowText));
    this.treeAttrAplicability.TabIndex = 3;
    this.treeAttrAplicability.Text = "treeList1";
    this.treeAttrAplicability.TreeLineStyle = LineStyle.None;
    this.treeAttrAplicability.ViewOptions = ViewOptionsFlags.AutoWidth | ViewOptionsFlags.ShowButtons | ViewOptionsFlags.ShowColumns | ViewOptionsFlags.ShowHorzLines | ViewOptionsFlags.ShowRoot | ViewOptionsFlags.ShowVertLines | ViewOptionsFlags.ShowFocusedFrame;
    this.colId.Caption = "Идентификатор";
    this.colId.FieldName = "Идентификатор";
    this.colId.Name = "colId";
    this.colId.Options = ColumnOptions.CanResized | ColumnOptions.CanSorted | ColumnOptions.ReadOnly | ColumnOptions.FixedWidth;
    this.colId.VisibleIndex = 0;
    this.colId.Width = 209;
    this.colCaption.Caption = "Заголовок";
    this.colCaption.FieldName = "treeListColumn4";
    this.colCaption.Name = "colCaption";
    this.colCaption.Options = ColumnOptions.CanResized | ColumnOptions.CanSorted | ColumnOptions.ReadOnly;
    this.colCaption.VisibleIndex = 1;
    this.colCaption.Width = 142;
    this.imageListAppObjects.ColorDepth = ColorDepth.Depth32Bit;
    this.imageListAppObjects.ImageSize = new Size(32 /*0x20*/, 16 /*0x10*/);
    this.imageListAppObjects.TransparentColor = Color.Transparent;
    this.toolBar1.DockLine = 1;
    this.toolBar1.FullMenus = true;
    this.toolBar1.Guid = new Guid("6cb8f8f2-0dd1-4f8a-b642-ece847e92228");
    this.toolBar1.Hidden = true;
    this.toolBar1.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.bCard
    });
    this.toolBar1.Location = new Point(0, 0);
    this.toolBar1.Name = "toolBar1";
    this.toolBar1.Size = new Size(478, 24);
    this.toolBar1.TabIndex = 4;
    this.toolBar1.Text = "Элементы страницы";
    this.bCard.CommandName = "buttonItem1";
    this.bCard.Image = (Image) componentResourceManager.GetObject("bCard.Image");
    this.bCard.ToolTipText = "Показать карточку";
    this.bCard.Click += new EventHandler(this.bCard_Click);
    this.barManager.OwnerForm = (Form) null;
    this.leftBarDock.Dock = DockStyle.Left;
    this.leftBarDock.Guid = new Guid("c20414d5-5fcb-4834-8c5d-ac5505638bcc");
    this.leftBarDock.Location = new Point(0, 54);
    this.leftBarDock.Manager = this.barManager;
    this.leftBarDock.Name = "leftBarDock";
    this.leftBarDock.Size = new Size(0, 466);
    this.leftBarDock.TabIndex = 10;
    this.leftBarDock.Text = "BarDock";
    this.rightBarDock.Dock = DockStyle.Right;
    this.rightBarDock.Guid = new Guid("c4121ef5-a40d-4ad9-aec1-239f7aa91014");
    this.rightBarDock.Location = new Point(698, 54);
    this.rightBarDock.Manager = this.barManager;
    this.rightBarDock.Name = "rightBarDock";
    this.rightBarDock.Size = new Size(0, 466);
    this.rightBarDock.TabIndex = 11;
    this.rightBarDock.Text = "BarDock";
    this.bottomBarDock.Dock = DockStyle.Bottom;
    this.bottomBarDock.Guid = new Guid("53b5b590-67ad-4a4d-93e5-27bd3a3869c0");
    this.bottomBarDock.Location = new Point(0, 520);
    this.bottomBarDock.Manager = this.barManager;
    this.bottomBarDock.Name = "bottomBarDock";
    this.bottomBarDock.Size = new Size(698, 0);
    this.bottomBarDock.TabIndex = 12;
    this.bottomBarDock.Text = "BarDock";
    this.topBarDock.Controls.Add((Control) this.menuBar1);
    this.topBarDock.Controls.Add((Control) this.pageElementsToolBar);
    this.topBarDock.Dock = DockStyle.Top;
    this.topBarDock.Guid = new Guid("9e6c8871-749a-4dc8-a073-51a878b32ca0");
    this.topBarDock.Location = new Point(0, 0);
    this.topBarDock.Manager = this.barManager;
    this.topBarDock.Name = "topBarDock";
    this.topBarDock.Size = new Size(698, 54);
    this.topBarDock.TabIndex = 13;
    this.topBarDock.Text = "BarDock";
    this.menuBar1.Guid = new Guid("61bbb937-72bd-4ff5-a26b-88ed5cda59a5");
    this.menuBar1.Hidden = false;
    this.menuBar1.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuBarItem1
    });
    this.menuBar1.Location = new Point(2, 0);
    this.menuBar1.Name = "menuBar1";
    this.menuBar1.OwnerForm = (Form) null;
    this.menuBar1.Size = new Size(696, 28);
    this.menuBar1.TabIndex = 2;
    this.menuBar1.Text = "menuBar1";
    this.menuBar1.Visible = false;
    this.contextMenuBarItem1.CommandName = "contextMenuBarItem1";
    this.contextMenuBarItem1.Items.AddRange(new ToolbarItemBase[3]
    {
      (ToolbarItemBase) this.menuButtonItem1,
      (ToolbarItemBase) this.menuButtonItem2,
      (ToolbarItemBase) this.menuButtonItem3
    });
    this.contextMenuBarItem1.ShowText = true;
    this.menuButtonItem1.CommandName = "menuButtonItem1";
    this.menuButtonItem1.ShowText = true;
    this.menuButtonItem1.Text = "menuButtonItem1";
    this.menuButtonItem2.CommandName = "menuButtonItem2";
    this.menuButtonItem2.ShowText = true;
    this.menuButtonItem2.Text = "menuButtonItem2";
    this.menuButtonItem3.CommandName = "menuButtonItem3";
    this.menuButtonItem3.ShowText = true;
    this.menuButtonItem3.Text = "menuButtonItem3";
    this.pageElementsToolBar.DockLine = 1;
    this.pageElementsToolBar.FullMenus = true;
    this.pageElementsToolBar.Guid = new Guid("6cb8f8f2-0dd1-4f8a-b642-ece847e92228");
    this.pageElementsToolBar.Hidden = false;
    this.pageElementsToolBar.Items.AddRange(new ToolbarItemBase[7]
    {
      (ToolbarItemBase) this.bSelectAll,
      (ToolbarItemBase) this.bInvertSelection,
      (ToolbarItemBase) this.bUnselectAll,
      (ToolbarItemBase) this.bOpen,
      (ToolbarItemBase) this.bSave,
      (ToolbarItemBase) this.bExecute,
      (ToolbarItemBase) this.bExecuteAll
    });
    this.pageElementsToolBar.Location = new Point(2, 28);
    this.pageElementsToolBar.Name = "pageElementsToolBar";
    this.pageElementsToolBar.Size = new Size(199, 26);
    this.pageElementsToolBar.TabIndex = 1;
    this.pageElementsToolBar.Text = "Элементы страницы";
    this.bSelectAll.CommandName = "buttonItem1";
    this.bSelectAll.Image = (Image) componentResourceManager.GetObject("bSelectAll.Image");
    this.bSelectAll.ToolTipText = "Выделить все";
    this.bSelectAll.Click += new EventHandler(this.bSelectAll_Click);
    this.bInvertSelection.CommandName = "buttonItem2";
    this.bInvertSelection.Image = (Image) componentResourceManager.GetObject("bInvertSelection.Image");
    this.bInvertSelection.ToolTipText = "Инвертировать выделение";
    this.bInvertSelection.Click += new EventHandler(this.bInvertSelection_Click);
    this.bUnselectAll.CommandName = "selectPageElement";
    this.bUnselectAll.Image = (Image) componentResourceManager.GetObject("bUnselectAll.Image");
    this.bUnselectAll.ToolTipText = "Очистить выделение";
    this.bUnselectAll.Click += new EventHandler(this.bUnselectAll_Click);
    this.bOpen.BeginGroup = true;
    this.bOpen.CommandName = "bOpen";
    this.bOpen.Image = (Image) componentResourceManager.GetObject("bOpen.Image");
    this.bOpen.Text = "Открыть";
    this.bOpen.ToolTipText = "Открыть";
    this.bOpen.Click += new EventHandler(this.bOpen_Click);
    this.bSave.CommandName = "bSave";
    this.bSave.Image = (Image) componentResourceManager.GetObject("bSave.Image");
    this.bSave.Text = "Сохранить";
    this.bSave.ToolTipText = "Сохранить";
    this.bSave.Click += new EventHandler(this.bSave_Click);
    this.bExecute.BeginGroup = true;
    this.bExecute.CommandName = "bExecute";
    this.bExecute.Image = (Image) componentResourceManager.GetObject("bExecute.Image");
    this.bExecute.Text = "Выполнить";
    this.bExecute.ToolTipText = "Выполнить текущую страницу";
    this.bExecute.Click += new EventHandler(this.bExecute_Click);
    this.bExecuteAll.CommandName = "bExecuteAll";
    this.bExecuteAll.Image = (Image) componentResourceManager.GetObject("bExecuteAll.Image");
    this.bExecuteAll.Text = "Выполнить все";
    this.bExecuteAll.ToolTipText = "Выполнить все";
    this.bExecuteAll.Click += new EventHandler(this.bExecuteAll_Click);
    this.saveFileDialog1.Filter = "Файлы настроек | *.xml";
    this.saveFileDialog1.RestoreDirectory = true;
    this.openFileDialog1.Filter = "Файлы настроек | *.xml";
    this.openFileDialog1.RestoreDirectory = true;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.leftBarDock);
    this.Controls.Add((Control) this.rightBarDock);
    this.Controls.Add((Control) this.bottomBarDock);
    this.Controls.Add((Control) this.tabControl1);
    this.Controls.Add((Control) this.topBarDock);
    this.Name = nameof (DemoDbCleanForm);
    this.Size = new Size(698, 520);
    this.Text = "Очистка демонстрационной БД";
    this.repositoryItemButtonEditClean.EndInit();
    this.repositoryItemButtonEditDelete.EndInit();
    this.tabControl1.ResumeLayout(false);
    this.tObjects.ResumeLayout(false);
    this.treeObjTypes.EndInit();
    ((ISupportInitialize) this.imageListBoxControl1).EndInit();
    this.tImbase.ResumeLayout(false);
    this.treeImBase.EndInit();
    this.repositoryClean.EndInit();
    this.repositoryDelete.EndInit();
    this.tAttrs.ResumeLayout(false);
    this.treeListAttrs.EndInit();
    this.panel1.ResumeLayout(false);
    this.treeAttrAplicability.EndInit();
    this.topBarDock.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private enum SelectEnum
  {
    SelectAll,
    UnselectAll,
    Invert,
  }

  private class Attribute
  {
    private string name;
    private int id;
    private int type;

    public string Name
    {
      get => this.name;
      set => this.name = value;
    }

    public int Id
    {
      get => this.id;
      set => this.id = value;
    }

    public int Type
    {
      get => this.type;
      set => this.type = value;
    }
  }

  private class AttrGroup
  {
    private string name;
    private int id;

    public string Name
    {
      get => this.name;
      set => this.name = value;
    }

    public int Id
    {
      get => this.id;
      set => this.id = value;
    }
  }

  private class SelectFromSchema : TreeListOperation
  {
    private CleanSchema cs;

    public CleanSchema Cs
    {
      get => this.cs;
      set => this.cs = value;
    }

    public override void Execute(TreeListNode node)
    {
      if (node.Tag is IMSObjectType tag1)
        node.StateImageIndex = !this.cs.ObjectTypes.Contains(tag1.ObjectTypeID) ? 0 : 1;
      ImBaseCatalog cat = node.Tag as ImBaseCatalog;
      if (cat != null)
      {
        ImBaseCatalog imBaseCatalog = this.cs.Catalogs.Find((Predicate<ImBaseCatalog>) (x => x.ObjectId == cat.ObjectId));
        if (imBaseCatalog != null)
        {
          if (imBaseCatalog.CleanMode == CleanEnum.Clean)
            node.StateImageIndex = 2;
          if (imBaseCatalog.CleanMode == CleanEnum.Delete)
            node.StateImageIndex = 1;
          if (imBaseCatalog.CleanMode == CleanEnum.None)
            node.StateImageIndex = 0;
        }
        else
          node.StateImageIndex = 0;
      }
      if (!(node.Tag is DemoDbCleanForm.Attribute tag2))
        return;
      if (this.cs.Attributes.Contains(tag2.Id))
        node.StateImageIndex = 1;
      else
        node.StateImageIndex = 0;
    }
  }
}
