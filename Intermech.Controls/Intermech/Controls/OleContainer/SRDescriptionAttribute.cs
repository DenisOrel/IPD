
// Type: Intermech.Controls.OleContainer.SRDescriptionAttribute
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;


namespace Intermech.Controls.OleContainer;

[AttributeUsage(AttributeTargets.All)]
internal sealed class SRDescriptionAttribute(string description) : DescriptionAttribute(description)
{
  private bool replaced;

  public override string Description
  {
    get
    {
      if (!this.replaced)
      {
        this.replaced = true;
        this.DescriptionValue = LangStrings.GetString(base.Description);
      }
      return base.Description;
    }
  }
}
