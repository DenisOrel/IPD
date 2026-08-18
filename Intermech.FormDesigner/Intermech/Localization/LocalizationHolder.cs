// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.LocalizationHolder
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.Localization;

/// <summary>
/// 
/// </summary>
internal class LocalizationHolder
{
  public static ResourceManager rm = new ResourceManager("Intermech.FormDesigner.Resources.FormDesignerResources", Assembly.GetExecutingAssembly());
  public static ResourceManager rma = new ResourceManager("Intermech.FormDesigner.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());
}
