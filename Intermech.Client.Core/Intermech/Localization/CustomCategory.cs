
// Type: Intermech.Localization.CustomCategory
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using System.ComponentModel;


namespace Intermech.Localization;

internal class CustomCategory([NotNull] string category) : CategoryAttribute(category)
{
  [NotNull]
  protected override string GetLocalizedString([NotNull] string value)
  {
    return LocalizationHolder.GetAttributeString(value);
  }
}
