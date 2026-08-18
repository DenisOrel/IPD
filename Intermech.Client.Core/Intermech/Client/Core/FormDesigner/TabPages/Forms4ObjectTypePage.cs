
// Type: Intermech.Client.Core.FormDesigner.TabPages.Forms4ObjectTypePage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.TabPages;

/// <summary>
/// 
/// </summary>
public class Forms4ObjectTypePage : BaseTabPage
{
  private Forms4TypeForm _form;

  /// <summary>Конструктор.</summary>
  /// <param name="instGuid"></param>
  public Forms4ObjectTypePage(Guid instGuid)
    : base(instGuid, LocalizationHolder.rm.GetString("Client.Core_189"))
  {
    this._form = PropertyFormsHolder.PropertyForms(instGuid).Forms4Type;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="panel"></param>
  public override void DockToPanel(Panel panel) => this._form.SetParent(panel);

  /// <summary>
  /// 
  /// </summary>
  public override ITabPageForm TabPageProcessingForm => (ITabPageForm) this._form;
}
