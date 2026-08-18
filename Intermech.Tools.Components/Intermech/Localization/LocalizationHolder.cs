// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.LocalizationHolder
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.Localization;

/// <summary>
/// Класс позволяет получать строки из ресурсов текущей сборки
/// </summary>
internal class LocalizationHolder
{
  /// <summary>Статическое поле для обращения к ресурсам</summary>
  public static ResourceManager rm = new ResourceManager("Intermech.Tools.Components.Resources.ToolsComponentsResources", Assembly.GetExecutingAssembly());
  /// <summary>Статическое свойство для обращения к ресурсам</summary>
  public static ResourceManager rma = new ResourceManager("Intermech.Tools.Components.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());
}
