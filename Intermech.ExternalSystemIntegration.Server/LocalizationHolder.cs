// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.LocalizationHolder
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server;

internal class LocalizationHolder
{
  public static ResourceManager rm = new ResourceManager("Intermech.ExternalSystemIntegration.Server.Resources.ExternalIntegrationResources", Assembly.GetExecutingAssembly());
  public static ResourceManager rma = new ResourceManager("Intermech.ExternalSystemIntegration.Server.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());
}
