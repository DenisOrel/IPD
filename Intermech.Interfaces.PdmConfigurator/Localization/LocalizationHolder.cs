// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.LocalizationHolder
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.Localization;

internal class LocalizationHolder
{
  public static ResourceManager rm = new ResourceManager("Intermech.Interfaces.PdmConfigurator.Resources.PdmConfiguratorResources", Assembly.GetExecutingAssembly());
  public static ResourceManager rma = new ResourceManager("Intermech.Interfaces.PdmConfigurator.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());
}
