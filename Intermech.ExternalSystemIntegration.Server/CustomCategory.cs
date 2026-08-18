// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.CustomCategory
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using System.ComponentModel;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server;

internal class CustomCategory(string сategory) : CategoryAttribute(сategory)
{
  protected override string GetLocalizedString(string value)
  {
    return LocalizationHolder.rma.GetString(value) ?? string.Empty;
  }
}
