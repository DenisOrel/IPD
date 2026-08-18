// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.CustomCategory
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Workflow;

/// <summary>
/// Класс позволяет категорию (Category) из ресурсов текущей сборки
/// </summary>
public class CustomCategory(string сategory) : CategoryAttribute(сategory)
{
  /// <summary>
  /// Загрузить атрибут с указанным именем из ресурсов [CustomAttributesResources] текущей сборки
  /// </summary>
  /// <param name="value">Имя атрибута в ресурсах [CustomAttributesResources] текущей сборки</param>
  protected override string GetLocalizedString(string value)
  {
    return LocalizationHolder.rma.GetString(value) == null ? string.Empty : LocalizationHolder.rma.GetString(value);
  }
}
