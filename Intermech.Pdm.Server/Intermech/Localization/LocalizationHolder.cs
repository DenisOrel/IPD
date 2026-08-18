// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.LocalizationHolder
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.Localization;

internal class LocalizationHolder
{
  public static ResourceManager rm = new ResourceManager("Intermech.Pdm.Server.Resources.PdmServerResources", Assembly.GetExecutingAssembly());
  public static ResourceManager rma = new ResourceManager("Intermech.Pdm.Server.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());
}
