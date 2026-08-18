
// Type: Intermech.Controls.OleContainer.SRCategoryAttribute
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;


namespace Intermech.Controls.OleContainer;

[AttributeUsage(AttributeTargets.All)]
internal sealed class SRCategoryAttribute(string category) : CategoryAttribute(category)
{
  protected override string GetLocalizedString(string value) => LangStrings.GetString(value);
}
