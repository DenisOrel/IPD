
// Type: Intermech.PropertyEditors.LanguagesFolder
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

/// <summary>Языковые варианты</summary>
public class LanguagesFolder : CustomFolder
{
  public override bool NeedSave => true;

  public override bool NeedPageSave => true;

  public LanguagesFolder(Guid aInstGuid, string aText, object aNodeParent)
    : base(aInstGuid, aText, aNodeParent, (object) null)
  {
    if (Statics.IconSrv == null)
      return;
    this.node.ImageIndex = Statics.IconSrv.IndexOf(Statics.CategoryLanguages, 0);
    this.node.SelectedImageIndex = this.node.ImageIndex;
  }

  public override object GetServerObject(IUserSession session)
  {
    return (object) session.GetLanguageCollection();
  }

  public override IFolder AddChildCallback()
  {
    return (IFolder) new LanguageFolder(this.instGuid, LocalizationHolder.rm.GetString("Client.Core_104"), (object) this.Node, ' ', true, false, Guid.NewGuid(), string.Empty);
  }

  public override void LoadDataTable(bool reload)
  {
    this.dataTable = DataHolders.LanguagesHolder.LoadData(reload);
  }

  public override void PopulateCallback(bool reload)
  {
    foreach (DataRow row in (InternalDataCollectionBase) this.dataTable.Rows)
    {
      LanguageFolder languageFolder = new LanguageFolder(this.instGuid, Convert.ToString(row["F_LANGUAGE_NAME"]), (object) this.Node, Convert.ToChar(row["F_LANGUAGE_ID"]), false, Convert.ToInt16(row["F_DEFAULT"]) == (short) 1, new Guid(row["F_GUID"].ToString()), Convert.ToString(row["F_CULTURE_ID"]));
    }
  }

  public override void ConstructPages(TabControl tabControl)
  {
    TabControlProcessor.AssignTabPages(tabControl, (object) TabPagesHolder.TabPages(this.instGuid).ListTabPage, (object) TabPagesHolder.TabPages(this.instGuid).SecurityTabPage);
  }

  public override int ExportCategoryValue => 9;

  public override int ListCategoryValue => 9;
}
