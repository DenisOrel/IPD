// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.LocalizationHolder
// Assembly: Intermech.Cadmech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A35B043F-5773-4DBE-81D3-C3E493F8C825
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Cadmech.Interfaces.xml

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
  public static ResourceManager rm = new ResourceManager("Intermech.Cadmech.Interfaces.Resources.CadmechInterfacesResources", Assembly.GetExecutingAssembly());
  /// <summary>Статическое свойство для обращения к ресурсам</summary>
  public static ResourceManager rma = new ResourceManager("Intermech.Cadmech.Integrator.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());
}
