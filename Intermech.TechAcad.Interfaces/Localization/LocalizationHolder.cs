// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.LocalizationHolder
// Assembly: Intermech.TechAcad.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 512FF008-192B-42A6-A8D1-B0B0A687059D
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.TechAcad.Interfaces.xml

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
  public static ResourceManager rm = new ResourceManager("Intermech.TechAcad.Interfaces.Resources.TechAcadInterfacesResources", Assembly.GetExecutingAssembly());
  /// <summary>Статическое свойство для обращения к ресурсам</summary>
  public static ResourceManager rma = new ResourceManager("Intermech.TechAcad.Interfaces.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());
}
