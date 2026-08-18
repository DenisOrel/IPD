// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.CustomDescription
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

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
