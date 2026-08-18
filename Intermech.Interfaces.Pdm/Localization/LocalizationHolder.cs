// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.LocalizationHolder
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

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
  public static ResourceManager rm = new ResourceManager("Intermech.Interfaces.Pdm.Resources.InterfacesPdmResources", Assembly.GetExecutingAssembly());
}
