// Decompiled with JetBrains decompiler
// Type: Intermech.Services.WeldingJoints.UpdateWeldingSeamsResult
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Services.WeldingJoints;

/// <summary>
/// Класс результатов выполнения команды обновления сварных швов в базе данных IPS.
/// </summary>
public sealed class UpdateWeldingSeamsResult
{
  private IReadOnlyList<long> documentsWithoutArticles;

  /// <summary>Создает объект.</summary>
  /// <param name="documentsWithoutArticles">Коллекция документов IPS, у который отсутствуют изделия</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="documentsWithoutArticles" /> содержит значение null</exception>
  public UpdateWeldingSeamsResult(IReadOnlyList<long> documentsWithoutArticles)
  {
    this.documentsWithoutArticles = documentsWithoutArticles != null ? documentsWithoutArticles : throw new ArgumentNullException(nameof (documentsWithoutArticles));
  }

  /// <summary>
  /// Возвращает признак успешного или неуспешного выполнения команды.
  /// </summary>
  public bool IsSuccessful => this.documentsWithoutArticles.Count == 0;

  /// <summary>
  /// Возвращает коллекцию документов IPS, у который отсутствуют изделия.
  /// Коллекция будет пуста, если команда выполнилась успешно.
  /// </summary>
  public IReadOnlyList<long> DocumentsWithoutArticles => this.documentsWithoutArticles;
}
