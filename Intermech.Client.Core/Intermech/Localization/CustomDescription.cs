
// Type: Intermech.Localization.CustomDescription
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using System.ComponentModel;


namespace Intermech.Localization;

internal class CustomDescription : DescriptionAttribute
{
  public CustomDescription([NotNull] string description)
  {
    this.DescriptionValue = LocalizationHolder.GetAttributeString(description);
  }
}
