
// Type: Intermech.PropertyEditors.ObjectTypesFolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.LifeCycles;
using Intermech.Localization;
using System;
using System.Data;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Типы объектов</summary>
public class ObjectTypesFolder : CustomFolder
{
  private new MenuButtonItem miPaste;

  public override bool NeedSave => true;

  public override bool NeedPageSave => true;

  public ObjectTypesFolder(Guid aInstGuid, string aText, object aNodeParent)
    : base(aInstGuid, aText, aNodeParent, (object) -1)
  {
    if (Statics.IconSrv == null)
      return;
    this.node.ImageIndex = Statics.IconSrv.IndexOf(Statics.CategoryObjectTypes, 0);
    this.node.SelectedImageIndex = this.node.ImageIndex;
  }

  public override object GetServerObject(IUserSession session)
  {
    return (object) session.GetObjectTypeCollection(Convert.ToInt32(this.Id), CoreConsts.FilterRecords);
  }

  public override IFolder AddChildCallback()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBLCSchemaCollection schemaCollection = sessionKeeper.Session.GetLCSchemaCollection();
      return (IFolder) new ObjectTypeFolder(this.instGuid, LocalizationHolder.rm.GetString("Client.Core_1171"), (object) this.Node, CoreConsts.IDGeneratorNextValue, true, string.Empty, ObjectVersionModes.Abstract, string.Empty, 0, Guid.NewGuid(), string.Empty, 0, false, string.Empty, 0, ObjectTypeOptions.None, schemaCollection.GetDefaultSchemaID())
      {
        IsNewType = true
      };
    }
  }

  public override void LoadDataTable(bool reload)
  {
    this.dataTable = DataHolders.ObjectTypesHolder.LoadData((reload ? 1 : 0) != 0, this.Id);
  }

  public override void GetContextMenu(
    ContextMenuBarItem contextMenu,
    IEventsDispatcher iEventsDispatcher)
  {
    base.GetContextMenu(contextMenu, iEventsDispatcher);
    this.miPaste = new MenuButtonItem(LocalizationHolder.rm.GetString("Client.Core_99"), new EventHandler(this.PasteObjectType));
    this.miPaste.BeginGroup = true;
    INamedImageList service = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
    if (service != null)
      this.miPaste.ImageIndex = service.ImageIndex("imgPaste");
    contextMenu.Items.AddRange((ToolbarItemBase[]) new MenuButtonItem[1]
    {
      this.miPaste
    });
  }

  public override void SetContextMenuItemStatus(ContextMenuBarItem contextMenu)
  {
    base.SetContextMenuItemStatus(contextMenu);
    this.miPaste.Enabled = !this.InChange && CoreConsts.ObjectTypeToPaste != -1;
  }

  internal static void PasteObjectTypeToCustomFolder(CustomFolder destFolder)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(CoreConsts.ObjectTypeToPaste);
      if (objectType == null)
        return;
      objectType.ParentTypeID = (int) destFolder.Id;
      DataHolders.ObjectTypesHolder.ClearInfo();
      TreeNode treeNode = destFolder.Node;
      while (treeNode.Parent != null)
        treeNode = treeNode.Parent;
      TreeNode nodeById = ClientCommons.FindNodeById(((IFolder) treeNode.Tag).Node.Nodes, (object) CoreConsts.ObjectTypeToPaste);
      if (nodeById != null)
      {
        if (ClientConsts.IsFakeNode(destFolder.Node))
        {
          nodeById.Remove();
          destFolder.Populate(false);
        }
        else
        {
          nodeById.Parent.Nodes.Remove(nodeById);
          destFolder.Node.Nodes.Add(nodeById);
          ((CustomFolder) nodeById.Tag).nodeParent = destFolder.Node;
        }
      }
      else
        destFolder.Populate(true);
    }
  }

  private void PasteObjectType(object sender, EventArgs e)
  {
    ObjectTypesFolder.PasteObjectTypeToCustomFolder((CustomFolder) this);
  }

  public override void PopulateCallback(bool reload)
  {
    ISelectorFilter treeView = this.Node.TreeView as ISelectorFilter;
    foreach (DataRow row in (InternalDataCollectionBase) this.dataTable.Rows)
    {
      if (treeView == null || treeView != null && treeView.IsInFilter(this.ListCategoryValue, (object) Convert.ToInt32(row["F_OBJECT_TYPE"])))
      {
        ObjectTypeFolder objectTypeFolder = new ObjectTypeFolder(this.instGuid, row["F_OBJ_TYPE_NAME"].ToString(), (object) this.Node, Convert.ToInt32(row["F_OBJECT_TYPE"]), false, row["F_OBJ_NAME"].ToString(), (ObjectVersionModes) Convert.ToInt32(row["F_VERSIONABLE"]), row["F_NOTE"].ToString(), Convert.ToInt32(row["F_DEFAULT_RELATION"]), new Guid(row["F_GUID"].ToString()), row["F_AREA_ID"].ToString(), Convert.ToInt32(row["F_CAPTION_ATTRIBUTE"]), Convert.ToInt16(row["F_ANY_ATTRIBUTES"]) == (short) 1, row["F_SHORT_NAME"].ToString(), Convert.ToInt32(row["F_DEL_TIME"]), (ObjectTypeOptions) Convert.ToInt32(row["F_OPTIONS"]), Convert.ToInt32(row["F_SCHEMA_ID"]));
      }
    }
    if (treeView != null && (treeView == null || !treeView.IsInFilter(this.ListCategoryValue, (object) -1)))
      return;
    AllObjectTypesFolder objectTypesFolder = new AllObjectTypesFolder(this.instGuid, (object) this.node);
  }

  public override void ConstructPages(TabControl tabControl)
  {
    TabControlProcessor.AssignTabPages(tabControl, (object) TabPagesHolder.TabPages(this.instGuid).ListTabPage, (object) TabPagesHolder.TabPages(this.instGuid).SecurityTabPage);
  }

  public override int ExportCategoryValue => 4;

  public override int ListCategoryValue => 4;
}
