// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.CustomDescription
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Localization;

internal class CustomDescription : DescriptionAttribute
{
  public CustomDescription(string description)
  {
    if (LocalizationHolder.rma.GetString(description) != null)
      this.DescriptionValue = LocalizationHolder.rma.GetString(description);
    else
      this.DescriptionValue = string.Empty;
  }
}
