// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.VersionAttributesFormParams
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.AVS;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

/// <summary>
/// Параметры, с которыми работает форма "Редактор списка атрибутов, отображаемых в прмиечаниях спецификации"
/// </summary>
[Serializable]
public class VersionAttributesFormParams : VersionAttributesListFormParams
{
  /// <summary>Опции</summary>
  public VersionAttributesOptions Options = VersionAttributesOptions.ShowMeasureUnits;
  private string variableDataCaption;

  public string VariableDataCaption
  {
    get => this.variableDataCaption;
    set => this.variableDataCaption = value;
  }

  /// <summary>
  /// Создать экземпляр класса, заполненный значениями по умолчанию
  /// </summary>
  public VersionAttributesFormParams()
    : this(VersionAttributesHelper.GetDefaultAttributes(), VersionAttributesOptions.ShowMeasureUnits)
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="items">Список атрибутов, которые отображаются в примечаниях спецификаций</param>
  /// <param name="options">Опции</param>
  public VersionAttributesFormParams(List<VersionAttribute> items, VersionAttributesOptions options)
    : base(items)
  {
    this.Options = options;
  }

  /// <summary>Создать экземпляр класса по прототипу</summary>
  /// <param name="template">Прототип</param>
  public VersionAttributesFormParams(VersionAttributesFormParams template) => this.Assign(template);

  /// <summary>Скопировать все данные из указанного источника</summary>
  /// <param name="source">Источник данных</param>
  public virtual void Assign(VersionAttributesFormParams source)
  {
    this.Assign((VersionAttributesListFormParams) source);
    this.Options = VersionAttributesOptions.ShowMeasureUnits;
    this.VariableDataCaption = "Переменные данные для исполнений:";
    if (source == null)
      return;
    this.Options = source.Options;
    this.VariableDataCaption = source.VariableDataCaption;
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public new object Clone() => (object) new VersionAttributesFormParams(this);
}
