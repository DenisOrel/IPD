// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.LocalizationHolder
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.Localization;

internal static class LocalizationHolder
{
  public static ResourceManager rm = new ResourceManager("Intermech.Interfaces.Reports.Resources.InterfacesReportsResources", Assembly.GetExecutingAssembly());
  public static ResourceManager rma = new ResourceManager("Intermech.Interfaces.Reports.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());
}
