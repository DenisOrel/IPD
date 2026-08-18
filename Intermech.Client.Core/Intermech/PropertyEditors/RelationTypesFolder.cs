
// Type: Intermech.PropertyEditors.RelationTypesFolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Data;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Типы связей</summary>
public class RelationTypesFolder : CustomFolder
{
  public override bool NeedSave => true;

  public override bool NeedPageSave => true;

  public RelationTypesFolder(Guid aInstGuid, string aText, object aNodeParent)
    : base(aInstGuid, aText, aNodeParent, (object) null)
  {
    if (Statics.IconSrv == null)
      return;
    this.node.ImageIndex = Statics.IconSrv.IndexOf(Statics.CategoryRelationTypes, 0);
    this.node.SelectedImageIndex = this.node.ImageIndex;
  }

  public override object GetServerObject(IUserSession session)
  {
    return (object) session.GetRelationTypeCollection(CoreConsts.FilterRecords);
  }

  public override IFolder AddChildCallback()
  {
    return (IFolder) new RelationTypeFolder(this.instGuid, LocalizationHolder.rm.GetString("Client.Core_147"), (object) this.Node, CoreConsts.IDGeneratorNextValue, true, LocalizationHolder.rm.GetString("Client.Core_1264"), LocalizationHolder.rm.GetString("Client.Core_1265"), string.Empty, false, RelationKinds.Vertical, false, Guid.NewGuid(), string.Empty, false, string.Empty, RelationTypeOptions.None);
  }

  public override void LoadDataTable(bool reload)
  {
    this.dataTable = DataHolders.RelationTypesHolder.LoadData(reload);
  }

  public override void PopulateCallback(bool reload)
  {
    foreach (DataRow row in (InternalDataCollectionBase) this.dataTable.Rows)
    {
      if (!(this.Node.TreeView is ISelectorFilter treeView) || treeView != null && treeView.IsInFilter(this.ListCategoryValue, (object) Convert.ToInt32(row["F_RELATION_TYPE"])))
      {
        RelationTypeFolder relationTypeFolder = new RelationTypeFolder(this.instGuid, row["F_DESCRIPTION"].ToString(), (object) this.Node, Convert.ToInt32(row["F_RELATION_TYPE"]), false, row["F_TYPE_NAME"].ToString(), row["F_REVERSE_NAME"].ToString(), row["F_NOTE"].ToString(), Convert.ToInt16(row["F_CHKOUTFILE"]) == (short) 1, (RelationKinds) Convert.ToInt16(row["F_RELATION_KIND"]), Convert.ToInt16(row["F_SAVE_HISTORY"]) == (short) 1, new Guid(row["F_GUID"].ToString()), row["F_AREA_ID"].ToString(), Convert.ToInt16(row["F_ANY_ATTRIBUTES"]) == (short) 1, row["F_SHORT_NAME"].ToString(), (RelationTypeOptions) Convert.ToInt32(row["F_OPTIONS"]));
      }
    }
  }

  public override void ConstructPages(TabControl tabControl)
  {
    TabControlProcessor.AssignTabPages(tabControl, (object) TabPagesHolder.TabPages(this.instGuid).ListTabPage, (object) TabPagesHolder.TabPages(this.instGuid).SecurityTabPage);
  }

  public override int ExportCategoryValue => 6;

  public override int ListCategoryValue => 6;
}
