// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.LocalizationHolder
// Assembly: Intermech.PdmConfigurator.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 80F94CD1-7E39-423C-8BC4-966315C23D3C
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.PdmConfigurator.Server.dll

using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.Localization;

internal class LocalizationHolder
{
  public static ResourceManager rm = new ResourceManager("Intermech.PdmConfigurator.Server.Resources.PdmConfiguratorResources", Assembly.GetExecutingAssembly());
  public static ResourceManager rma = new ResourceManager("Intermech.PdmConfigurator.Server.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());
}
