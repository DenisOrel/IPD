
// Type: Intermech.Localization.CustomDisplayName
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Diagnostics;
using System.ComponentModel;


namespace Intermech.Localization;

internal class CustomDisplayName : DisplayNameAttribute
{
  public CustomDisplayName([NotNull] string displayName)
  {
    this.DisplayNameValue = LocalizationHolder.GetAttributeString(displayName);
  }
}
