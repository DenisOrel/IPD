
// Type: Intermech.PropertyEditors.ListTabPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraGrid;
using Intermech.Localization;
using System;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for ListTabPage.</summary>
public class ListTabPage : BaseTabPage
{
  private ListForm _ListForm;

  public GridControl GridControl
  {
    get => PropertyFormsHolder.PropertyForms(this.instGuid).ListForm.GridControl;
  }

  public ListTabPage(Guid aInstGuid)
    : base(aInstGuid, LocalizationHolder.rm.GetString("Client.Core_115"))
  {
    this._ListForm = PropertyFormsHolder.PropertyForms(this.instGuid).ListForm;
  }

  public override void DockToPanel(Panel panel) => this._ListForm.SetParent(panel);

  public override ITabPageForm TabPageProcessingForm => (ITabPageForm) this._ListForm;
}
