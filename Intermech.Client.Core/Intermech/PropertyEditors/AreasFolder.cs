
// Type: Intermech.PropertyEditors.AreasFolder
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

/// <summary>Предметные области</summary>
public class AreasFolder : CustomFolder
{
  public override bool NeedSave => true;

  public override bool NeedPageSave => true;

  public AreasFolder(Guid aInstGuid, string aText, object aNodeParent)
    : base(aInstGuid, aText, aNodeParent, (object) null)
  {
    if (Statics.IconSrv == null)
      return;
    this.node.ImageIndex = Statics.IconSrv.IndexOf(Statics.CategorySubjectAreas, 0);
    this.node.SelectedImageIndex = this.node.ImageIndex;
  }

  public override object GetServerObject(IUserSession session)
  {
    return (object) session.GetSubjectAreaCollection();
  }

  public override IFolder AddChildCallback()
  {
    return (IFolder) new AreaFolder(this.instGuid, LocalizationHolder.rm.GetString("Client.Core_32"), (object) this.Node, ' ', true, string.Empty, Guid.NewGuid());
  }

  public override void LoadDataTable(bool reload)
  {
    this.dataTable = DataHolders.SubjectAreasHolder.LoadData(reload);
  }

  public override void PopulateCallback(bool reload)
  {
    foreach (DataRow row in (InternalDataCollectionBase) this.dataTable.Rows)
    {
      AreaFolder areaFolder = new AreaFolder(this.instGuid, row["F_AREA_NAME"].ToString(), (object) this.Node, Convert.ToChar(row["F_AREA_ID"]), false, row["F_AREA_NOTE"].ToString(), new Guid(row["F_GUID"].ToString()));
    }
  }

  public override void ConstructPages(TabControl tabControl)
  {
    TabControlProcessor.AssignTabPages(tabControl, (object) TabPagesHolder.TabPages(this.instGuid).ListTabPage, (object) TabPagesHolder.TabPages(this.instGuid).SecurityTabPage);
  }

  public override int ExportCategoryValue => 11;

  public override int ListCategoryValue => 11;
}
