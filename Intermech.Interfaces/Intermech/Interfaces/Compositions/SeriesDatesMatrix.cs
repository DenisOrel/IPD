
// Type: Intermech.Interfaces.Compositions.SeriesDatesMatrix
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Collections;
using Intermech.Interfaces.Sets;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>
    /// Класс позволяет хранить матрицу диапазонов серий и дат изготовления всех версий указанного объекта для набора головных изделий
    /// </summary>
    [DebuggerDisplay("{Text}")]
    [Serializable]
    public sealed class SeriesDatesMatrix : IAssignable, ICloneable, Intermech.Interfaces.IDisplayable
    {
      /// <summary>Идентификатор объекта (F_ID)</summary>
      public long MainID;
      /// <summary>
      /// Идентификатор версии объекта (F_OBJECT_ID), с которым связана матрица
      /// </summary>
      public long MainObjectID;
      /// <summary>
      /// [Головное изделие, Идентификатор версии объекта, Признак применяемости] =&gt; (Список применяемостей)
      /// </summary>
      public SortedDictionary<MatrixKey, SeriesDatesApplicability> Items = new SortedDictionary<MatrixKey, SeriesDatesApplicability>();

      /// <summary>Создать пустой экземпляр класса</summary>
      public SeriesDatesMatrix()
      {
      }

      /// <summary>Создать заполненный экземпляр класса</summary>
      /// <param name="mainID">Идентификатор объекта (F_ID)</param>
      /// <param name="mainObjectID">Идентификатор версии объекта (F_OBJECT_ID), с которым связана матрица</param>
      public SeriesDatesMatrix(long mainID = 0, long mainObjectID = 0)
      {
        this.MainID = mainID;
        this.MainObjectID = mainObjectID;
      }

      /// <summary>
      /// Создать экземпляр класса, заполнить его из указанного объекта-источника
      /// </summary>
      /// <param name="source">Объект-источник</param>
      public SeriesDatesMatrix(object source) => this.Assign(source);

      /// <summary>Является ли коллекция пустой</summary>
      public bool IsEmpty => this.MainID == 0L || this.MainObjectID == 0L || this.Items.Count == 0;

      /// <summary>Текст для отображения на экране</summary>
      public string Text
      {
        get
        {
          return !(ApplicationServices.Container.GetService(typeof (IObjectsInfoCache)) is IObjectsInfoCache service) ? $"[{this.MainID}] => [{this.MainObjectID}]" : $"'{service.GetObjectCaption(this.MainID)}' => '{service.GetObjectCaption(this.MainObjectID)}'";
        }
      }

      /// <summary>Очистить поля класса</summary>
      public void Clear()
      {
        this.MainID = 0L;
        this.MainObjectID = 0L;
        this.Items.Clear();
      }

      /// <summary>Очистить поля класса</summary>
      public void Assign(object source)
      {
        if (this == source || !(source is SeriesDatesMatrix seriesDatesMatrix))
          return;
        this.MainID = seriesDatesMatrix.MainID;
        this.MainObjectID = seriesDatesMatrix.MainObjectID;
        if (!(CloneHelper.Clone((object) seriesDatesMatrix.Items) is SortedDictionary<MatrixKey, SeriesDatesApplicability> sortedDictionary))
          sortedDictionary = seriesDatesMatrix.Items;
        this.Items = sortedDictionary;
      }

      /// <summary>Создать точную копию экземпляра класса</summary>
      /// <returns>Точная копия экземпляра класса</returns>
      public object Clone() => Activator.CreateInstance(this.GetType(), (object) this);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true - объекты идентичны</returns>
      public override bool Equals(object obj)
      {
        if (this == obj)
          return true;
        return obj is SeriesDatesMatrix seriesDatesMatrix && Math.Abs(this.MainID) == Math.Abs(seriesDatesMatrix.MainID) && Math.Abs(this.MainObjectID) == Math.Abs(seriesDatesMatrix.MainObjectID);
      }

      /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode()
      {
        long num1 = Math.Abs(this.MainID);
        int num2 = num1.GetHashCode() << 16 /*0x10*/;
        num1 = Math.Abs(this.MainObjectID);
        int hashCode = num1.GetHashCode();
        return num2 ^ hashCode;
      }

      /// <summary>
      /// Отыскать применяемости для указанных головного изделия, версии объекта и признака применяемости
      /// </summary>
      /// <param name="mainArticle">Головное изделие</param>
      /// <param name="objectID">Идентификатор версии объекта</param>
      /// <param name="appl">Признак применяемости</param>
      /// <param name="autoCreate">Требуется ли создавать коллекцию и размещать её в словаре, если её нет</param>
      /// <returns>Коллекция применяемостей или null</returns>
      public SeriesDatesApplicability FindApplicability(
        long mainArticle,
        long objectID,
        ApplicabilityBy appl = ApplicabilityBy.Series,
        bool autoCreate = false)
      {
        lock (this.Items)
        {
          MatrixKey key = new MatrixKey(mainArticle, objectID, appl);
          if (this.Items.ContainsKey(key))
            return this.Items[key];
          if (!autoCreate)
            return (SeriesDatesApplicability) null;
          SeriesDatesApplicability applicability = new SeriesDatesApplicability(appl, mainArticle);
          this.Items[key] = applicability;
          return applicability;
        }
      }

      /// <summary>Загрузить информацию из базы данных</summary>
      /// <param name="session">Сессия</param>
      public void Load(IUserSession session)
      {
        if (session == null)
          return;
        DataTable dataTable = this.LoadDescriptions(session);
        if (dataTable == null)
          return;
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          long int64Value = DataSetProcessor.GetInt64Value(dataTable.Rows[index][1], 0L);
          string stringValue = DataSetProcessor.GetStringValue(dataTable.Rows[index][2], string.Empty);
          if (int64Value != 0L && !string.IsNullOrEmpty(stringValue))
            (stringValue.IndexOf("1|") != 0 ? new SeriesDatesApplicabilityCollection((object) stringValue) : new SeriesDatesApplicabilityCollection((object) session.GetObject(int64Value, false))).AlterMatrix(this, this.MainID, int64Value);
        }
      }

      /// <summary>Загрузить описания версий указанного объекта</summary>
      /// <param name="session">Сессия</param>
      /// <returns>Список описаний версий объектов</returns>
      private DataTable LoadDescriptions(IUserSession session)
      {
        if (session == null || this.MainID == 0L)
          return (DataTable) null;
        IObjectsInfoCache service = ApplicationServices.Container.GetService(typeof (IObjectsInfoCache)) as IObjectsInfoCache;
        int objectType = -1;
        if (service != null)
        {
          QuickObjectInfo objectInfo = service.GetObjectInfo(this.MainObjectID);
          if (!objectInfo.Empty)
            objectType = objectInfo.ObjectTypeID;
        }
        ColumnDescriptor[] columns = new ColumnDescriptor[3]
        {
          new ColumnDescriptor((object) -5, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.ASC, 0),
          new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
          new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cadd940c-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
        };
        List<long> longList = new List<long>();
        object[] objArray = new object[0];
        SortOrders[] sortOrdersArray = new SortOrders[0];
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(-3, RelationalOperators.Equal, (object) this.MainID, LogicalOperators.NONE, 0, true)
        }, columns);
        IDBObjectCollection objectCollection = session.GetObjectCollection(objectType);
        if (objectCollection == null)
          return (DataTable) null;
        objectCollection.ShowAllModifications = true;
        return objectCollection.Select(paramSet);
      }

      /// <summary>
      /// Отыскать пересечение применяемостей указанной версии объекта с другими версиями
      /// </summary>
      /// <param name="mainArticleID">Идентификатор головного изделия</param>
      /// <param name="objectID">Идентификатор проверяемой версии объекта</param>
      /// <param name="appls">Проверяемая применяемость</param>
      /// <returns>Ключ записи в матрице, с которой возникло пересечение</returns>
      public MatrixKey FindIntersections(
        long mainArticleID,
        long objectID,
        SeriesDatesApplicability appls)
      {
        MatrixKey intersections = (MatrixKey) null;
        mainArticleID = Math.Abs(mainArticleID);
        objectID = Math.Abs(objectID);
        if (this.IsEmpty || appls == null || appls.IsEmpty || objectID == 0L || mainArticleID == 0L)
          return intersections;
        foreach (KeyValuePair<MatrixKey, SeriesDatesApplicability> keyValuePair in this.Items)
        {
          if ((keyValuePair.Key.Item1 != mainArticleID || keyValuePair.Key.Item2 != objectID || keyValuePair.Key.Item3 != appls.Applicability) && keyValuePair.Key.Item1 == mainArticleID && keyValuePair.Key.Item3 == appls.Applicability && keyValuePair.Value.IsIntersectsWith(appls))
            return keyValuePair.Key;
        }
        return intersections;
      }
    }
}
