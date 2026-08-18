
// Type: Intermech.PropertyEditors.AllObjectTypesFolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Data;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Все типы объектов</summary>
public class AllObjectTypesFolder : CustomFolder
{
  public override bool NeedSave => true;

  public override bool NeedPageSave => true;

  public AllObjectTypesFolder(Guid aInstGuid, object aNodeParent)
    : base(aInstGuid, LocalizationHolder.rm.GetString("Client.Core_118"), aNodeParent, (object) -1)
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

  public override void LoadDataTable(bool reload)
  {
    this.dataTable = DataHolders.ObjectTypesHolder.LoadData((reload ? 1 : 0) != 0, (object) -2);
  }

  public override void GetContextMenu(
    ContextMenuBarItem contextMenu,
    IEventsDispatcher iEventsDispatcher)
  {
    base.GetContextMenu(contextMenu, iEventsDispatcher);
    contextMenu.Items.Remove((ToolbarItemBase) this.miAdd);
    contextMenu.Items.Remove((ToolbarItemBase) this.miCopy);
    contextMenu.Items.Remove((ToolbarItemBase) this.miDelete);
    contextMenu.Items.Remove((ToolbarItemBase) this.miPaste);
    contextMenu.Items.Remove((ToolbarItemBase) this.miExclude);
    if (this.miSetSystemGuid == null)
      return;
    contextMenu.Items.Remove((ToolbarItemBase) this.miSetSystemGuid);
  }

  public override void SetContextMenuItemStatus(ContextMenuBarItem contextMenu)
  {
    base.SetContextMenuItemStatus(contextMenu);
    this.miOpenInNewWindow.Visible = true;
  }

  public override void PopulateCallback(bool reload)
  {
    foreach (DataRow row in (InternalDataCollectionBase) this.dataTable.Rows)
    {
      if (!(this.Node.TreeView is ISelectorFilter treeView) || treeView != null && treeView.IsInFilter(this.ListCategoryValue, (object) Convert.ToInt32(row["F_OBJECT_TYPE"])))
      {
        ObjectTypeFolder objectTypeFolder = new ObjectTypeFolder(this.instGuid, row["F_OBJ_TYPE_NAME"].ToString(), (object) this.Node, Convert.ToInt32(row["F_OBJECT_TYPE"]), false, row["F_OBJ_NAME"].ToString(), (ObjectVersionModes) Convert.ToInt32(row["F_VERSIONABLE"]), row["F_NOTE"].ToString(), Convert.ToInt32(row["F_DEFAULT_RELATION"]), new Guid(row["F_GUID"].ToString()), row["F_AREA_ID"].ToString(), Convert.ToInt32(row["F_CAPTION_ATTRIBUTE"]), Convert.ToInt16(row["F_ANY_ATTRIBUTES"]) == (short) 1, row["F_SHORT_NAME"].ToString(), Convert.ToInt32(row["F_DEL_TIME"]), (ObjectTypeOptions) Convert.ToInt32(row["F_OPTIONS"]), Convert.ToInt32(row["F_SCHEMA_ID"]), true);
      }
    }
  }

  public override void ConstructPages(TabControl tabControl)
  {
    TabControlProcessor.AssignTabPages(tabControl, (object) TabPagesHolder.TabPages(this.instGuid).ListTabPage);
  }

  public override int ExportCategoryValue => 4;

  public override int ListCategoryValue => 4;
}
