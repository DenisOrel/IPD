// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.CustomDescription
// Assembly: Intermech.Expert.Test, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 494A2DB2-0ED6-480D-BF40-DFD41733278B
// Assembly location: D:\IPS\Client\Intermech.Expert.Test.dll

using System.ComponentModel;

#nullable disable
namespace Intermech.Localization;

internal class CustomDescription : DescriptionAttribute
{
  public CustomDescription(string description)
  {
    object obj = (object) LocalizationHolder.rma.GetString(description);
    this.DescriptionValue = obj != null ? (string) obj : string.Empty;
  }
}
