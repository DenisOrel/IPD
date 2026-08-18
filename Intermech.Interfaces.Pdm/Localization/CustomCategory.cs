// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.CustomCategory
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System.ComponentModel;
using System.Reflection;
using System.Resources;

#nullable disable
namespace Intermech.Localization;

/// <summary>
/// Класс позволяет категорию (Category) из ресурсов текущей сборки
/// </summary>
/// <summary>
/// 
/// </summary>
/// <param name="сategory"></param>
internal class CustomCategory(string сategory) : CategoryAttribute(сategory)
{
  /// <summary>Статическое свойство для обращения к ресурсам</summary>
  public static ResourceManager rma = new ResourceManager("Intermech.Interfaces.Pdm.Resources.CustomAttributesResources", Assembly.GetExecutingAssembly());

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  /// <returns></returns>
  protected override string GetLocalizedString(string value)
  {
    return CustomCategory.rma.GetString(value) == null ? string.Empty : CustomCategory.rma.GetString(value);
  }
}
