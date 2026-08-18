
// Type: Intermech.PropertyEditors.BaseTabPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for BaseTabPage.</summary>
public class BaseTabPage : TabPage, IBaseTabPage
{
  protected Guid instGuid = Guid.Empty;

  protected BaseTabPage()
    : this(Guid.Empty)
  {
  }

  public BaseTabPage(Guid aInstGuid) => this.instGuid = aInstGuid;

  public BaseTabPage(Guid aInstGuid, string s)
    : base(s)
  {
    this.instGuid = aInstGuid;
  }

  public virtual void DockToPanel(Panel panel)
  {
  }

  public virtual ITabPageForm TabPageProcessingForm => (ITabPageForm) null;
}
