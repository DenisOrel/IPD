
// Type: Intermech.PropertyEditors.LCSchemasFolder
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

public class LCSchemasFolder : CustomFolder
{
  public override bool NeedSave => true;

  public override bool NeedPageSave => true;

  public LCSchemasFolder(Guid aInstGuid, string aText, object aNodeParent)
    : base(aInstGuid, aText, aNodeParent, (object) -1)
  {
    if (Statics.IconSrv == null)
      return;
    this.node.ImageIndex = Statics.IconSrv.IndexOf(Statics.CategoryLCSchemas, 0);
    this.node.SelectedImageIndex = this.node.ImageIndex;
  }

  public override object GetServerObject(IUserSession session)
  {
    return (object) session.GetLCSchemaCollection(CoreConsts.FilterRecords);
  }

  public override IFolder AddChildCallback()
  {
    return (IFolder) new LCSchemaFolder(this.instGuid, LocalizationHolder.rm.GetString("Client.Core_1173"), (object) this.Node, CoreConsts.IDGeneratorNextValue, true, string.Empty, "", false, Guid.NewGuid());
  }

  public override void LoadDataTable(bool reload)
  {
    this.dataTable = DataHolders.LCSchemasHolder.LoadData(reload);
  }

  public override void PopulateCallback(bool reload)
  {
    foreach (DataRow row in (InternalDataCollectionBase) this.dataTable.Rows)
    {
      LCSchemaFolder lcSchemaFolder = new LCSchemaFolder(this.instGuid, Convert.ToString(row["F_NAME"]), (object) this.Node, Convert.ToInt32(row["F_SCHEMA_ID"]), false, Convert.ToString(row["F_NOTE"].ToString()), Convert.ToString(row["F_AREA_ID"]), Convert.ToInt16(row["F_DEFAULT"]) == (short) 1, new Guid(row["F_GUID"].ToString()));
    }
  }

  public override void ConstructPages(TabControl tabControl)
  {
    TabControlProcessor.AssignTabPages(tabControl, (object) TabPagesHolder.TabPages(this.instGuid).ListTabPage, (object) TabPagesHolder.TabPages(this.instGuid).SecurityTabPage);
  }

  public override int ExportCategoryValue => 16 /*0x10*/;

  public override int ListCategoryValue => 16 /*0x10*/;
}
