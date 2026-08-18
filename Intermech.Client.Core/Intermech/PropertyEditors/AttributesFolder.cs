
// Type: Intermech.PropertyEditors.AttributesFolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Data;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Атрибуты</summary>
public class AttributesFolder : CustomFolder
{
  private bool _useFilter = true;

  public override bool NeedSave => true;

  public override bool NeedPageSave => true;

  public override bool PasteEnabled => true;

  public AttributesFolder(Guid aInstGuid, string aText, object aNodeParent)
    : base(aInstGuid, aText, aNodeParent, (object) null)
  {
    if (Statics.IconSrv == null)
      return;
    this.node.ImageIndex = Statics.IconSrv.IndexOf(Statics.CategoryAttributes, 0);
    this.node.SelectedImageIndex = this.node.ImageIndex;
  }

  public AttributesFolder(Guid aInstGuid, string aText, object aNodeParent, bool useFilter)
    : this(aInstGuid, aText, aNodeParent)
  {
    this._useFilter = useFilter;
  }

  public override object GetServerObject(IUserSession session)
  {
    return (object) session.GetAttributesGroupCollection(0, CoreConsts.FilterRecords);
  }

  public override void GetContextMenu(
    ContextMenuBarItem contextMenu,
    IEventsDispatcher iEventsDispatcher)
  {
    base.GetContextMenu(contextMenu, iEventsDispatcher);
    this.miAdd.Text = LocalizationHolder.rm.GetString("Client.Core_CreateAttrGroup");
  }

  public override void SetContextMenuItemStatus(ContextMenuBarItem contextMenu)
  {
    base.SetContextMenuItemStatus(contextMenu);
    if (!this.PasteEnabled || !(ServicesManager.GetService(typeof (IClipboard)) is IClipboard service))
      return;
    this.miPaste.Enabled = service.GetDataObject() is IDBAttributeGroupIDCollection && !this.InChange;
  }

  public override void Paste()
  {
    if (!this.CanPaste || !(ServicesManager.GetService(typeof (IClipboard)) is IClipboard service) || !(service.GetDataObject() is IDBAttributeGroupIDCollection dataObject))
      return;
    int num = 0;
    if (dataObject.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      try
      {
        for (int index = 0; index < dataObject.Count; ++index)
        {
          int attributeGroupId = dataObject.GetAttributeGroupID(index).AttributeGroupID;
          IDBAttributesGroup attributesGroup = sessionKeeper.Session.GetAttributesGroup(attributeGroupId);
          if (attributesGroup != null)
            attributesGroup.ParentID = num;
        }
      }
      catch
      {
        throw;
      }
      finally
      {
        DataHolders.AttributeGroupsHolder.ClearInfo();
        if (!ClientConsts.IsFakeNode(this.node))
          this.Populate(false);
      }
    }
  }

  public override IFolder AddChildCallback()
  {
    return (IFolder) new AttributeGroupFolder(this.instGuid, LocalizationHolder.rm.GetString("Client.Core_66"), (object) this.Node, CoreConsts.IDGeneratorNextValue, true, string.Empty, string.Empty, string.Empty, Guid.NewGuid());
  }

  public override void LoadDataTable(bool reload)
  {
    this.dataTable = DataHolders.AttributeGroupsHolder.LoadData(reload);
    this.dataTable = this.dataTable.Copy();
    for (int index = this.dataTable.Rows.Count - 1; index >= 0; --index)
    {
      if (Convert.ToInt32(this.dataTable.Rows[index]["F_PARENT_ID"]) > 0)
        this.dataTable.Rows.RemoveAt(index);
    }
  }

  public override void PopulateCallback(bool reload)
  {
    foreach (DataRow row in (InternalDataCollectionBase) this.dataTable.Rows)
    {
      ISelectorFilter treeView = this.Node.TreeView as ISelectorFilter;
      if (Convert.ToInt32(row["F_PARENT_ID"]) == 0 && (treeView == null || !this._useFilter || treeView != null && treeView.IsInFilter(this.ListCategoryValue, (object) Convert.ToInt32(row["F_GROUP_ID"]))))
      {
        AttributeGroupFolder attributeGroupFolder = new AttributeGroupFolder(this.instGuid, row["F_GROUP_NAME"].ToString(), (object) this.Node, Convert.ToInt32(row["F_GROUP_ID"]), false, row["F_NOTE"].ToString(), row["F_AREA_ID"].ToString(), Convert.ToString(row["F_LANGUAGE_ID"]), new Guid(row["F_GUID"].ToString()), this._useFilter);
      }
    }
    AttributeTypeAssignedGroupFolder assignedGroupFolder = new AttributeTypeAssignedGroupFolder(this.instGuid, (object) this.Node, this._useFilter);
  }

  public override void ConstructPages(TabControl tabControl)
  {
    TabControlProcessor.AssignTabPages(tabControl, (object) TabPagesHolder.TabPages(this.instGuid).ListTabPage, (object) TabPagesHolder.TabPages(this.instGuid).SecurityTabPage);
  }

  public override int ExportCategoryValue => 12;

  public override int ListCategoryValue => 12;
}
