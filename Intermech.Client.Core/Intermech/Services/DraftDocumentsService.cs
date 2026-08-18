
// Type: Intermech.Services.DraftDocumentsService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


namespace Intermech.Services;

/// <summary>
/// Сервис черновиков документов. Реализация является thread safe.
/// </summary>
internal sealed class DraftDocumentsService : IDraftDocumentsService
{
  private IDraftDocumentsIdCache idCache;

  /// <summary>Создает объект.</summary>
  /// <param name="idCache">Контейнер с метаданными, относящимися к черновикам документов</param>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="idCache" /> не должен быть равен null</exception>
  public DraftDocumentsService(IDraftDocumentsIdCache idCache)
  {
    this.idCache = idCache != null ? idCache : throw new ArgumentNullException(nameof (idCache));
  }

  /// <summary>
  /// Возвращает контейнер метаданных, относящихся к черновикам документов.
  /// </summary>
  public IDraftDocumentsIdCache IdCache
  {
    [DebuggerStepThrough] get => this.idCache;
  }

  /// <summary>
  /// Находит в базе данных IPS черновик документа по имени внешнего файла черновика документа.
  /// </summary>
  /// <param name="relativeFilename">Имя внешнего файла черновика документа в относительной форме</param>
  /// <returns>Идентификатор версии для найденного черновика документа или null</returns>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="relativeFilename" /> не должен быть равен null</exception>
  public long? FindDraftDocumentByFilename(string relativeFilename)
  {
    if (relativeFilename == null)
      throw new ArgumentNullException(nameof (relativeFilename));
    DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
    dbRecordSetParams.RecordCount = 1;
    dbRecordSetParams.Columns = new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    dbRecordSetParams.Conditions = new ConditionStructure[1]
    {
      new ConditionStructure(this.IdCache.ExternalFilePath.Id, RelationalOperators.Equal, (object) relativeFilename, LogicalOperators.NONE, 0, false)
    };
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      dataTable = sessionKeeper.Session.ObjectsSelect(this.IdCache.DraftDocuments.Id, dbRecordSetParams);
    return dataTable.Rows.Count == 0 ? new long?() : new long?(Convert.ToInt64(dataTable.Rows[0][0]));
  }

  /// <summary>
  /// Находит в базе данных IPS все черновики документов, принадлежащие текущему пользователю.
  /// </summary>
  /// <returns>Список пар вида (идентификатор версии черновика, имя файла черновика)</returns>
  public List<Tuple<long, string>> GetCurrentUserDraftDocuments()
  {
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      dataTable = sessionKeeper.Session.ObjectsSelect(this.IdCache.DraftDocuments.Id, new DBRecordSetParams()
      {
        RecordCount = -1,
        Columns = new object[2]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) this.IdCache.ExternalFilePath.Id
        },
        Conditions = new ConditionStructure[1]
        {
          new ConditionStructure(-8, RelationalOperators.Equal, (object) sessionKeeper.Session.UserID, LogicalOperators.NONE, 0, true)
        }
      });
    if (dataTable.Rows.Count == 0)
      return new List<Tuple<long, string>>(0);
    List<Tuple<long, string>> userDraftDocuments = new List<Tuple<long, string>>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      userDraftDocuments.Add(Tuple.Create<long, string>(Convert.ToInt64(row[0]), Convert.ToString(row[1])));
    return userDraftDocuments;
  }
}
