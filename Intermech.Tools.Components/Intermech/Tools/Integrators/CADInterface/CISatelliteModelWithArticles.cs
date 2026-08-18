// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CISatelliteModelWithArticles
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.IO;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// <para>
/// Используется для представления в БД анализатора изменений тех компонентов сборочной модели,
/// которые должны быть сохранены как дополнительный файл сборочной модели,
/// и которые требуют генерации изделия.</para>
/// <para>
/// Для обозначения таких моделей традиционно применяется PDMFlag=5.</para>
/// </summary>
internal sealed class CISatelliteModelWithArticles
{
  /// <summary>
  /// Ссылка на свойство Path для использования в запросах к БД анализатора изменений.
  /// </summary>
  public static readonly SectionPropertyReference PathRef = new SectionPropertyReference(typeof (CISatelliteModelWithArticles), nameof (Path));
  private readonly string path;

  /// <summary>Создает объект.</summary>
  /// <param name="path">Абсолютный путь к файлу модели компонента сборочной единицы</param>
  public CISatelliteModelWithArticles(string path)
  {
    this.path = path != null ? path : throw new ArgumentNullException(nameof (path));
  }

  /// <summary>
  /// Возвращает абсолютный путь к файлу модели компонента сборочной единицы.
  /// </summary>
  [Indexable(IndexType.Auto, false)]
  [Comparer(typeof (ServiceObjectAttribute.NewObject), new object[] {typeof (PathComparer)})]
  public string Path => this.path;
}
