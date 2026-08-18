
// Type: Intermech.Interfaces.ObjectVersionDescription
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс-контейнер, в котором хранится описание версии объекта
    /// </summary>
    [DebuggerDisplay("F_OBJECT_ID: {F_OBJECT_ID}, F_MODIFICATION_ID: {F_MODIFICATION_ID}, \"{CAPTION}\"")]
    [Serializable]
    public class ObjectVersionDescription : 
      IAssignable,
      ICloneable,
      IComparable,
      IComparable<ObjectVersionDescription>,
      IComparer<object>,
      IComparer<ObjectVersionDescription>
    {
      /// <summary>Идентификатор объекта</summary>
      public long F_ID;
      /// <summary>Идентификатор версии объекта</summary>
      public long F_OBJECT_ID;
      /// <summary>Идентификатор типа объекта</summary>
      public int F_OBJECT_TYPE;
      /// <summary>Шаг ЖЦ</summary>
      public int F_LCSTEP_ID;
      /// <summary>Владелец объекта</summary>
      public long F_OWNER_ID;
      /// <summary>Кем взят на изменение</summary>
      public long F_CHKOUT_BY;
      /// <summary>Заголовок</summary>
      public string CAPTION;
      /// <summary>Номер версии</summary>
      public long F_VERSION_ID;
      /// <summary>Номер группы изменений</summary>
      public long F_MODIFICATION_ID;
      /// <summary>Признак базовой версии</summary>
      public long F_BASE_VERSION;
      /// <summary>
      /// Дополнительная информация о версии объекта из контекста редактирования
      /// </summary>
      public ObjectVersionDescriptionOptions Options;
      /// <summary>Идентификатор схемы ЖЦ для шага ЖЦ версии объекта</summary>
      public int F_LCSCHEMA_ID;
      /// <summary>
      /// Идентификатор уровня продвижения для шага ЖЦ версии объекта
      /// </summary>
      public int F_LCLEVEL_ID;
      /// <summary>
      /// Список идентификаторов версий объектов-извещений, в состав которых входит указанная версия объекта
      /// </summary>
      public List<long> ECOs;
      /// <summary>Вспомогательная информация</summary>
      public object Tag;
      /// <summary>Список колонок для запроса в "ядро"</summary>
      protected static List<ColumnDescriptor> columnDescriptors = new List<ColumnDescriptor>();

      /// <summary>Создать пустой экземпляр класса</summary>
      public ObjectVersionDescription()
      {
      }

      /// <summary>Создать частично заполненный экземпляр класса</summary>
      /// <param name="objID">Идентификатор версии объекта</param>
      public ObjectVersionDescription(long objID)
        : this(0L, objID, -1, -1, 0L, 0L, string.Empty, 0L, 0L, 0L, ObjectVersionDescriptionOptions.None)
      {
      }

      /// <summary>Создать заполненный экземпляр класса</summary>
      /// <param name="_ID">Идентификатор объекта</param>
      /// <param name="_OBJECT_ID">Идентификатор версии объекта</param>
      /// <param name="_OBJECT_TYPE">Идентификатор типа объекта</param>
      /// <param name="_LCSTEP_ID">Шаг ЖЦ</param>
      /// <param name="_OWNER_ID">Владелец объекта</param>
      /// <param name="_CHKOUT_BY">Владелец объекта</param>
      /// <param name="_CAPTION">Заголовок</param>
      /// <param name="_F_VERSION_ID">Номер версии</param>
      /// <param name="_F_MODIFICATION_ID">Номер группы изменений</param>
      /// <param name="_F_BASE_VERSION">Признак базовой версии</param>
      /// <param name="_Options">Опции</param>
      public ObjectVersionDescription(
        long _ID,
        long _OBJECT_ID,
        int _OBJECT_TYPE,
        int _LCSTEP_ID,
        long _OWNER_ID,
        long _CHKOUT_BY,
        string _CAPTION,
        long _F_VERSION_ID,
        long _F_MODIFICATION_ID,
        long _F_BASE_VERSION,
        ObjectVersionDescriptionOptions _Options)
      {
        this.F_ID = _ID;
        this.F_OBJECT_ID = _OBJECT_ID;
        this.F_OBJECT_TYPE = _OBJECT_TYPE;
        this.F_LCSTEP_ID = _LCSTEP_ID;
        this.F_OWNER_ID = _OWNER_ID;
        this.F_CHKOUT_BY = _CHKOUT_BY;
        this.CAPTION = _CAPTION;
        this.F_VERSION_ID = _F_VERSION_ID;
        this.F_MODIFICATION_ID = _F_MODIFICATION_ID;
        this.F_BASE_VERSION = _F_BASE_VERSION;
        this.Options = _Options;
      }

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из строки таблицы
      /// </summary>
      /// <param name="row">Строка таблицы с данными</param>
      public ObjectVersionDescription(DataRow row) => this.Assign((object) row);

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из указанного объекта
      /// </summary>
      /// <param name="source">Источник информации</param>
      public ObjectVersionDescription(object source) => this.Assign(source);

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из указанного объекта
      /// </summary>
      /// <param name="source">Объект-описатель</param>
      public ObjectVersionDescription(IDBObject source) => this.Assign((object) source);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты равны</returns>
      public override bool Equals(object obj)
      {
        return obj is ObjectVersionDescription versionDescription && this.F_OBJECT_ID == versionDescription.F_OBJECT_ID;
      }

      /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode() => this.F_OBJECT_ID.GetHashCode();

      /// <summary>
      /// Получить представление экземпляра класса в виде строки
      /// </summary>
      /// <returns>Строковое представление экземпляра класса</returns>
      public override string ToString()
      {
        return string.Format("[{0} ver.{3}] \"{1}\" (\"{2}\")", (object) this.F_OBJECT_ID, (object) this.CAPTION, (object) MetaDataHelper.GetObjectTypeName(this.F_OBJECT_TYPE), (object) this.F_VERSION_ID);
      }

      /// <summary>
      /// Получить список колонок, необходимых для получения списка объектов
      /// </summary>
      /// <returns>Список колонок, необходимых для получения списка объектов</returns>
      public virtual List<ColumnDescriptor> GetColumnDescriptors()
      {
        if (ObjectVersionDescription.columnDescriptors.Count != 0)
          return ObjectVersionDescription.columnDescriptors;
        ObjectVersionDescription.columnDescriptors.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
        ObjectVersionDescription.columnDescriptors.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
        ObjectVersionDescription.columnDescriptors.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
        ObjectVersionDescription.columnDescriptors.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_LC_STEP, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
        ObjectVersionDescription.columnDescriptors.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_LEVEL_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
        ObjectVersionDescription.columnDescriptors.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OWNER_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
        ObjectVersionDescription.columnDescriptors.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_CHKOUT_BY, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
        ObjectVersionDescription.columnDescriptors.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
        ObjectVersionDescription.columnDescriptors.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_VERSION_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
        ObjectVersionDescription.columnDescriptors.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_MODIFICATION_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
        ObjectVersionDescription.columnDescriptors.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_BASE_VERSION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
        return ObjectVersionDescription.columnDescriptors;
      }

      /// <summary>Определить значения рассчитываемых полей</summary>
      protected virtual void CalcFields()
      {
        IMSLifeCycleStep lcStep = MetaDataHelper.GetLCStep(this.F_LCSTEP_ID);
        if (lcStep == null)
          return;
        this.F_LCLEVEL_ID = lcStep.LevelID;
        this.F_LCSCHEMA_ID = lcStep.SchemaID;
      }

      /// <summary>Очистить экземпляр класса</summary>
      public virtual void Clear()
      {
        this.F_ID = 0L;
        this.F_OBJECT_ID = 0L;
        this.F_OBJECT_TYPE = -1;
        this.F_LCSTEP_ID = -1;
        this.F_OWNER_ID = 0L;
        this.F_CHKOUT_BY = 0L;
        this.CAPTION = string.Empty;
        this.F_VERSION_ID = 0L;
        this.F_MODIFICATION_ID = 0L;
        this.F_BASE_VERSION = 0L;
        this.Options = ObjectVersionDescriptionOptions.None;
        this.F_LCSCHEMA_ID = 0;
        this.F_LCLEVEL_ID = 0;
        this.ECOs = (List<long>) null;
        this.Tag = (object) null;
      }

      /// <summary>Скопировать информацию из указанного объекта</summary>
      /// <param name="source">Объект-источник</param>
      public virtual void Assign(object source)
      {
        if (this == source)
          return;
        this.Clear();
        switch (source)
        {
          case DataRow row:
            this.F_ID = DataSetProcessor.GetInt64Value(row, "F_ID", 0L);
            this.F_OBJECT_ID = DataSetProcessor.GetInt64Value(row, "F_OBJECT_ID", 0L);
            this.F_OBJECT_TYPE = DataSetProcessor.GetInt32Value(row, "F_OBJECT_TYPE", -1);
            this.F_LCSTEP_ID = DataSetProcessor.GetInt32Value(row, "F_LC_STEP", -1);
            this.F_OWNER_ID = DataSetProcessor.GetInt64Value(row, "F_OWNER_ID", 0L);
            this.F_CHKOUT_BY = DataSetProcessor.GetInt64Value(row, "F_CHKOUT_BY", 0L);
            this.CAPTION = DataSetProcessor.GetStringValue(row, "CAPTION", string.Empty);
            this.F_VERSION_ID = DataSetProcessor.GetInt64Value(row, "F_VERSION_ID", 0L);
            this.F_MODIFICATION_ID = DataSetProcessor.GetInt64Value(row, "F_MODIFICATION_ID", 0L);
            this.F_BASE_VERSION = DataSetProcessor.GetInt64Value(row, "F_BASE_VERSION", 0L);
            this.CalcFields();
            break;
          case ObjectVersionDescription versionDescription:
            this.F_ID = versionDescription.F_ID;
            this.F_OBJECT_ID = versionDescription.F_OBJECT_ID;
            this.F_OBJECT_TYPE = versionDescription.F_OBJECT_TYPE;
            this.F_LCSTEP_ID = versionDescription.F_LCSTEP_ID;
            this.F_OWNER_ID = versionDescription.F_OWNER_ID;
            this.F_CHKOUT_BY = versionDescription.F_CHKOUT_BY;
            this.CAPTION = versionDescription.CAPTION;
            this.F_VERSION_ID = versionDescription.F_VERSION_ID;
            this.F_MODIFICATION_ID = versionDescription.F_MODIFICATION_ID;
            this.F_BASE_VERSION = versionDescription.F_BASE_VERSION;
            this.Options = versionDescription.Options;
            this.ECOs = versionDescription.ECOs;
            this.Tag = versionDescription.Tag;
            this.CalcFields();
            break;
          case IDBObject dbObject:
            this.F_ID = dbObject.ID;
            this.F_OBJECT_ID = dbObject.ObjectID;
            this.F_OBJECT_TYPE = dbObject.ObjectType;
            this.F_LCSTEP_ID = dbObject.LCStep;
            this.F_OWNER_ID = dbObject.OwnerID;
            this.F_CHKOUT_BY = dbObject.CheckoutBy;
            this.CAPTION = dbObject.Caption;
            this.F_VERSION_ID = (long) dbObject.VersionID;
            this.F_MODIFICATION_ID = dbObject.ModificationID;
            this.F_BASE_VERSION = Convert.ToInt64(dbObject.IsBaseVersion);
            this.CalcFields();
            break;
        }
      }

      /// <summary>Вернуть точную копию экземпляра класса</summary>
      /// <returns>Точная копия экземпляра класса</returns>
      public virtual object Clone() => Activator.CreateInstance(this.GetType(), (object) this);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(object obj) => this.CompareTo(obj as ObjectVersionDescription);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(ObjectVersionDescription other)
      {
        if (other == null)
          return 1;
        int num = MetaDataHelper.GetObjectTypeName(this.F_OBJECT_TYPE).CompareTo(MetaDataHelper.GetObjectTypeName(other.F_OBJECT_TYPE));
        return num != 0 ? num : this.CAPTION.CompareTo(other.CAPTION);
      }

      /// <summary>Сравнить два объекта</summary>
      /// <param name="x">Первый объект</param>
      /// <param name="y">Второй объект</param>
      /// <returns>-1, 0, 1</returns>
      public int Compare(object x, object y)
      {
        return this.Compare(x as ObjectVersionDescription, y as ObjectVersionDescription);
      }

      /// <summary>Сравнить два объекта</summary>
      /// <param name="x">Первый объект</param>
      /// <param name="y">Второй объект</param>
      /// <returns>-1, 0, 1</returns>
      public int Compare(ObjectVersionDescription x, ObjectVersionDescription y)
      {
        return x == null || y == null ? 0 : x.CompareTo(y);
      }
    }
}
