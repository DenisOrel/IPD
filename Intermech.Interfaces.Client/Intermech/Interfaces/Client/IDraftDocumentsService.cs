// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IDraftDocumentsService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс сервиса черновиков документов. Документы специального типа "Черновики документов" используются в базе данных IPS в качестве
/// временных заменителей нормальных документов, если создание нормальных документов временно невозможно.
/// </summary>
public interface IDraftDocumentsService
{
  /// <summary>
  /// Возвращает контейнер метаданных, относящихся к черновикам документов.
  /// </summary>
  IDraftDocumentsIdCache IdCache { get; }

  /// <summary>
  /// Находит в базе данных IPS черновик документа по имени внешнего файла черновика документа.
  /// </summary>
  /// <param name="relativeFilename">Имя внешнего файла черновика документа в относительной форме</param>
  /// <returns>Идентификатор версии для найденного черновика документа или null</returns>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="relativeFilename" /> не должен быть равен null</exception>
  long? FindDraftDocumentByFilename(string relativeFilename);

  /// <summary>
  /// Находит в базе данных IPS все черновики документов, принадлежащие текущему пользователю.
  /// </summary>
  /// <returns>Список пар вида (идентификатор версии черновика, имя файла черновика)</returns>
  List<Tuple<long, string>> GetCurrentUserDraftDocuments();
}
