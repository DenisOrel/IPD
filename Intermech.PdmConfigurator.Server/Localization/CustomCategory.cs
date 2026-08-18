// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.CustomCategory
// Assembly: Intermech.PdmConfigurator.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 80F94CD1-7E39-423C-8BC4-966315C23D3C
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.PdmConfigurator.Server.dll

using System.ComponentModel;

#nullable disable
namespace Intermech.Localization;

internal class CustomCategory(string сategory) : CategoryAttribute(сategory)
{
  protected override string GetLocalizedString(string value)
  {
    return LocalizationHolder.rma.GetString(value) == null ? string.Empty : LocalizationHolder.rma.GetString(value);
  }
}
