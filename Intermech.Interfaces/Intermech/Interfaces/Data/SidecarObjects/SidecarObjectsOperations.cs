
// Type: Intermech.Interfaces.Data.SidecarObjects.SidecarObjectsOperations
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Collections;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces.Data.SidecarObjects
{
    /// <summary>
    /// Класс операций с ассоциированными объектами IPS.
    /// Ассоциированные объекты - это вспомогательные объекты, связанные с исходными объектами
    /// косвенной связью (например, через содержимое файла исходного объекта).
    /// </summary>
    /// <remarks>Реализация является thread safe.</remarks>
    public class SidecarObjectsOperations
    {
      private readonly SidecarObjectsIDCache sidecarIDCache;
      private readonly System.Func<long, long> identityFunction;

      /// <summary>Создает объект.</summary>
      /// <param name="sidecarIDCache">Кэш метаданных ассоциированных объектов</param>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="sidecarIDCache" /> содержит null</exception>
      public SidecarObjectsOperations(SidecarObjectsIDCache sidecarIDCache)
      {
        this.sidecarIDCache = sidecarIDCache != null ? sidecarIDCache : throw new ArgumentNullException(nameof (sidecarIDCache));
        this.identityFunction = new System.Func<long, long>(this.IdentityFunction);
      }

      /// <summary>
      /// Находит ассоциированный объект, связанный с указанным исходным документом.
      /// </summary>
      /// <typeparam name="TDocument">Класс исходных документов</typeparam>
      /// <param name="documentEntity">Объект исходного документа</param>
      /// <param name="objectIdFunc">Функция для получения идентификатора версии исходного документа</param>
      /// <returns>Идентификатор версии ассоциированного объекта</returns>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="objectIdFunc" /> содержит null</exception>
      public long Find<TDocument>(TDocument documentEntity, System.Func<TDocument, long> objectIdFunc)
      {
        long conditionValue = objectIdFunc != null ? Math.Abs(objectIdFunc(documentEntity)) : throw new ArgumentNullException(nameof (objectIdFunc));
        DBRecordSetParams paramSet = new DBRecordSetParams();
        paramSet.RecordCount = 1;
        paramSet.Columns = new object[1]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID
        };
        // ISSUE: explicit reference operation
        (^ref paramSet).Conditions = new ConditionStructure[1]
        {
          new ConditionStructure(this.sidecarIDCache.SourceDocumentReference.Id, RelationalOperators.Equal, (object) conditionValue, LogicalOperators.NONE, 0, true)
          {
            Content = ColumnContents.ID
          }
        };
        DataTable dataTable;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          dataTable = sessionKeeper.Session.GetObjectCollection(this.sidecarIDCache.SidecarObjectType.Id).Select(paramSet);
        return dataTable.Rows.Count != 0 ? Convert.ToInt64(dataTable.Rows[0][0]) : 0L;
      }

      /// <summary>
      /// Находит ассоциированный объект, связанный с указанным исходным документом.
      /// </summary>
      /// <param name="documentId">Идентификатор версии исходного документа</param>
      /// <returns>Идентификатор версии ассоциированного объекта</returns>
      /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentId" /> содержит некорректное значение</exception>
      public long Find(long documentId)
      {
        return !Consts.IsUndefinedObjectId(documentId) ? this.Find<long>(documentId, this.identityFunction) : throw new ArgumentException("Не задан идентификатор версии объекта IPS.", nameof (documentId));
      }

      /// <summary>
      /// Находит ассоциированные объекты, связанные с указанными исходными документами.
      /// </summary>
      /// <typeparam name="TDocument">Класс исходных документов</typeparam>
      /// <param name="documentEntities">Список объектов исходных документов</param>
      /// <param name="objectIdFunc">Функция для получения идентификатора версии исходного документа</param>
      /// <returns>Список найденных пар (исходный документ, идентификатор версии ассоциированного объекта)</returns>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="documentEntities" /> содержит null; параметр <paramref name="objectIdFunc" /> содержит null</exception>
      public List<Tuple<TDocument, long>> FindMany<TDocument>(
        IList<TDocument> documentEntities,
        System.Func<TDocument, long> objectIdFunc)
      {
        if (documentEntities == null)
          throw new ArgumentNullException(nameof (documentEntities));
        if (objectIdFunc == null)
          throw new ArgumentNullException(nameof (objectIdFunc));
        long[] array = CollectionUtils.ConvertAsArray<TDocument, long>((ICollection<TDocument>) documentEntities, (Converter<TDocument, long>) (x => Math.Abs(objectIdFunc(x))));
        long[] absDocumentIds = array;
        ColumnDescriptor[] queryColumns = new ColumnDescriptor[2];
        ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID);
        columnDescriptor.Contents = ColumnContents.Text;
        queryColumns[0] = columnDescriptor;
        columnDescriptor = new ColumnDescriptor((object) this.sidecarIDCache.SourceDocumentReference.Id);
        columnDescriptor.Contents = ColumnContents.ID;
        queryColumns[1] = columnDescriptor;
        DataTable manyAsTable = this.FindManyAsTable(absDocumentIds, queryColumns);
        List<Tuple<TDocument, long>> many = new List<Tuple<TDocument, long>>(manyAsTable.Rows.Count);
        foreach (DataRow row in (InternalDataCollectionBase) manyAsTable.Rows)
        {
          long int64_1 = Convert.ToInt64(row[0]);
          long int64_2 = Convert.ToInt64(row[1]);
          int index = Array.IndexOf<long>(array, int64_2);
          if (index >= 0)
            many.Add(Tuple.Create<TDocument, long>(documentEntities[index], int64_1));
        }
        return many;
      }

      /// <summary>
      /// Находит ассоциированные объекты, связанные с указанными исходными документами.
      /// </summary>
      /// <param name="documentIds">Список идентификаторов версий исходных документов</param>
      /// <returns>Список найденных пар (идентификатор версии исходного документа, идентификатор версии ассоциированного объекта)</returns>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="documentIds" /> содержит null</exception>
      public List<Tuple<long, long>> FindMany(IList<long> documentIds)
      {
        return documentIds != null ? this.FindMany<long>(documentIds, this.identityFunction) : throw new ArgumentNullException(nameof (documentIds));
      }

      /// <summary>
      /// Находит ассоциированные объекты, связанные с указанными исходными документами.
      /// Массив столбцов запроса должен содержать атрибут "Ссылка на исходный документ".
      /// </summary>
      /// <typeparam name="TDocument">Класс исходных документов</typeparam>
      /// <param name="documentEntities">Список объектов исходных документов</param>
      /// <param name="objectIdFunc">Функция для получения идентификатора версии исходного документа</param>
      /// <param name="queryColumns">Массив столбцов запроса</param>
      /// <returns>Список найденных пар (идентификатор версии исходного документа, DataRow с атрибутами ассоциированного объекта)</returns>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="documentEntities" /> содержит null; параметр <paramref name="objectIdFunc" /> содержит null</exception>
      /// <exception cref="T:System.ArgumentException">параметр <paramref name="queryColumns" /> содержит некорректное значение (либо null, либо нет атрибута "Ссылка на исходный документ")</exception>
      public List<Tuple<TDocument, DataRow>> FindManyAsRows<TDocument>(
        IList<TDocument> documentEntities,
        System.Func<TDocument, long> objectIdFunc,
        ColumnDescriptor[] queryColumns)
      {
        if (documentEntities == null)
          throw new ArgumentNullException(nameof (documentEntities));
        if (objectIdFunc == null)
          throw new ArgumentNullException(nameof (objectIdFunc));
        if (queryColumns == null)
          throw new ArgumentNullException(nameof (queryColumns));
        int index1 = Array.FindIndex<ColumnDescriptor>(queryColumns, (Predicate<ColumnDescriptor>) (x => object.Equals(x.AttributeID, (object) this.sidecarIDCache.SourceDocumentReference.Id)));
        if (index1 == -1)
          throw new ArgumentException($"Набор столбцов запроса не содержит атрибута '{this.sidecarIDCache.SourceDocumentReference.Text}'.", nameof (queryColumns));
        long[] numArray = CollectionUtils.ConvertAsArray<TDocument, long>((ICollection<TDocument>) documentEntities, (Converter<TDocument, long>) (x => Math.Abs(objectIdFunc(x))));
        DataTable manyAsTable = this.FindManyAsTable(numArray, queryColumns);
        List<Tuple<TDocument, DataRow>> manyAsRows = new List<Tuple<TDocument, DataRow>>(manyAsTable.Rows.Count);
        foreach (DataRow row in (InternalDataCollectionBase) manyAsTable.Rows)
        {
          long int64 = Convert.ToInt64(row[index1]);
          int index2 = Array.IndexOf<long>(numArray, int64);
          if (index2 >= 0)
            manyAsRows.Add(Tuple.Create<TDocument, DataRow>(documentEntities[index2], row));
        }
        return manyAsRows;
      }

      private DataTable FindManyAsTable(long[] absDocumentIds, ColumnDescriptor[] queryColumns)
      {
        DBRecordSetParams paramSet = new DBRecordSetParams();
        paramSet.RecordCount = -1;
        paramSet.SetColumnDescriptors(queryColumns);
        // ISSUE: explicit reference operation
        (^ref paramSet).Conditions = new ConditionStructure[1]
        {
          new ConditionStructure(this.sidecarIDCache.SourceDocumentReference.Id, RelationalOperators.In, (object) absDocumentIds, LogicalOperators.NONE, 0, true)
          {
            Content = ColumnContents.ID
          }
        };
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          return sessionKeeper.Session.GetObjectCollection(this.sidecarIDCache.SidecarObjectType.Id).Select(paramSet);
      }

      private long IdentityFunction(long value) => value;
    }
}
