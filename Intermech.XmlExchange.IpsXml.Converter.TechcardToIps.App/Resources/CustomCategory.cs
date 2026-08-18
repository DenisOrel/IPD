// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App.Resources.CustomCategory
// Assembly: Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EB4A0A0B-E62B-4D21-A944-3B5D877E45CE
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App.exe

using System.ComponentModel;

#nullable disable
namespace Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App.Resources;

internal class CustomCategory(string сategory) : CategoryAttribute(сategory)
{
  protected override string GetLocalizedString(string value)
  {
    return LocalizationHolder.rma.GetString(value) == null ? string.Empty : LocalizationHolder.rma.GetString(value);
  }
}
