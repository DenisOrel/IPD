// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ElectricalArticleCache
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>
/// Реализует секцию данных для хранения данных об изделий, описанном в электрической схеме.
/// Данная секция используется для кэширования объектов API приложения, а также для упрощения работы с ним.
/// </summary>
public sealed class ElectricalArticleCache
{
  /// <summary>Создает объект.</summary>
  /// <param name="article">Контейнер со свойствами, описывающими изделие</param>
  /// <param name="articleType">Тип изделия</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на аргумент метода не может быть null</exception>
  public ElectricalArticleCache(IValueBagContainer article, ArticleTypes articleType)
  {
    this.Article = article ?? throw new ArgumentNullException(nameof (article));
    this.ArticleType = articleType;
  }

  /// <summary>Возвращает компонент схемы, описывающий изделие.</summary>
  public IValueBagContainer Article { get; }

  /// <summary>Возвращает тип</summary>
  public ArticleTypes ArticleType { get; }

  /// <summary>Состав изделия</summary>
  public List<CompositionItem> Composition { get; set; }
}
