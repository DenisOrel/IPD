// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.MRPLocalization
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.Localization;

/// <summary>
/// Класс позволяет получать строки из ресурсов текущей сборки
/// </summary>
internal class MRPLocalization
{
  /// <summary>Статическое поле для обращения к ресурсам</summary>
  public static ResourceManager rm = new ResourceManager("Intermech.Interfaces.MRP.Resources.InterfacesMRPResources", Assembly.GetExecutingAssembly());
  /// <summary>Статическое свойство для обращения к ресурсам</summary>
  public static ResourceManager rma = new ResourceManager("Intermech.Interfaces.MRP.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());
}
