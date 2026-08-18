// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.RemarkAttributesFormParams
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
/// Параметры, с которыми работает форма "Редактор списка атрибутов, отображаемых в примечаниях спецификации"
/// </summary>
[Serializable]
public class RemarkAttributesFormParams : AttributesListFormParams
{
  /// <summary>Опции</summary>
  public NoteFieldOptions Options = NoteFieldOptions.ShowMeasureUnits;

  /// <summary>
  /// Создать экземпляр класса, заполненный значениями по умолчанию
  /// </summary>
  public RemarkAttributesFormParams()
    : this(NoteFieldSettings.GetDefaultAttributes(Guid.Empty), NoteFieldOptions.ShowMeasureUnits)
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="items">Список атрибутов, которые отображаются в примечаниях спецификаций</param>
  /// <param name="options">Опции</param>
  public RemarkAttributesFormParams(List<RemarkAttribute> items, NoteFieldOptions options)
    : base(items)
  {
    this.Options = options;
  }

  /// <summary>Создать экземпляр класса по прототипу</summary>
  /// <param name="template">Прототип</param>
  public RemarkAttributesFormParams(RemarkAttributesFormParams template) => this.Assign(template);

  /// <summary>Скопировать все данные из указанного источника</summary>
  /// <param name="source">Источник данных</param>
  public void Assign(RemarkAttributesFormParams source)
  {
    this.Assign((AttributesListFormParams) source);
    this.Options = NoteFieldOptions.ShowMeasureUnits;
    if (source == null)
      return;
    this.Options = source.Options;
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public new object Clone() => (object) new RemarkAttributesFormParams(this);
}
