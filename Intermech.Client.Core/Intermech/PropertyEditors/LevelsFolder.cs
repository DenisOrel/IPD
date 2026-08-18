
// Type: Intermech.PropertyEditors.LevelsFolder
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

/// <summary>Уровни продвижения</summary>
public class LevelsFolder : CustomFolder
{
  public override bool NeedSave => true;

  public override bool NeedPageSave => true;

  public LevelsFolder(Guid aInstGuid, string aText, object aNodeParent)
    : base(aInstGuid, aText, aNodeParent, (object) null)
  {
    if (Statics.IconSrv == null)
      return;
    this.node.ImageIndex = Statics.IconSrv.IndexOf(Statics.CategoryLCLevels, 0);
    this.node.SelectedImageIndex = this.node.ImageIndex;
  }

  public override object GetServerObject(IUserSession session)
  {
    return (object) session.GetLifecycleLevelCollection(CoreConsts.FilterRecords);
  }

  public override IFolder AddChildCallback()
  {
    return (IFolder) new LevelFolder(this.instGuid, LocalizationHolder.rm.GetString("Client.Core_1170"), (object) this.Node, CoreConsts.IDGeneratorNextValue, true, string.Empty, "", false, Guid.NewGuid(), 0L);
  }

  public override void LoadDataTable(bool reload)
  {
    this.dataTable = DataHolders.LevelsHolder.LoadData(reload);
  }

  public override void PopulateCallback(bool reload)
  {
    foreach (DataRow row in (InternalDataCollectionBase) this.dataTable.Rows)
    {
      LevelFolder levelFolder = new LevelFolder(this.instGuid, row["F_LEVEL_NAME"].ToString(), (object) this.Node, Convert.ToInt32(row["F_LEVEL_ID"]), false, row["F_LITERA"].ToString(), row["F_AREA_ID"].ToString(), Convert.ToInt16(row["F_DEFAULT"]) == (short) 1, new Guid(row["F_GUID"].ToString()), Convert.ToInt64(row["F_STORAGE_ID"]));
    }
  }

  public override void ConstructPages(TabControl tabControl)
  {
    TabControlProcessor.AssignTabPages(tabControl, (object) TabPagesHolder.TabPages(this.instGuid).ListTabPage, (object) TabPagesHolder.TabPages(this.instGuid).SecurityTabPage);
  }

  public override int ExportCategoryValue => 8;

  public override int ListCategoryValue => 8;
}
