
// Type: Intermech.Interfaces.Compositions.CompositionObject
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>Описание объекта состава</summary>
    [DebuggerDisplay("F_OBJECT_ID: {F_OBJECT_ID}; Caption: {CAPTION}")]
    [Serializable]
    public class CompositionObject : ObjectVersionDescription, ICloneable
    {
      /// <summary>Родительская коллекция объектов состава</summary>
      protected CompositionObjects parent;
      /// <summary>Коллекция дочерних описаний объектов состава</summary>
      protected CompositionObjects items;
      /// <summary>
      /// Идентификатор связи (уникальный в пределах всей коллекции)
      /// </summary>
      protected long prjLinkID;
      /// <summary>Идентификатор версии родительского объекта</summary>
      protected long projID;
      /// <summary>Идентификатор типа связи</summary>
      protected int relTypeID = -1;
      /// <summary>Примечание</summary>
      protected string note;
      /// <summary>Был ли обработан состав объекта</summary>
      protected bool parsedComposition;

      /// <summary>Создать незаполненное описание объекта состава</summary>
      public CompositionObject()
      {
      }

      /// <summary>Создать частично описание объекта состава</summary>
      /// <param name="projID">Идентификатор версии родительского объекта</param>
      /// <param name="prjLinkID">Идентификатор связи, если родительский объект сам входит в чей-то состав</param>
      public CompositionObject(long projID, long prjLinkID)
        : this(0L, projID, -1, -1, 0L, 0L, string.Empty, 0L, 0L, 0L, ObjectVersionDescriptionOptions.None, (CompositionObjects) null, prjLinkID, projID, -1, string.Empty)
      {
      }

      /// <summary>Создать описание объекта состава</summary>
      /// <param name="_ID">Идентификатор объекта</param>
      /// <param name="_OBJECT_ID">Идентификатор версии объекта</param>
      /// <param name="_OBJECT_TYPE">Идентификатор типа объекта</param>
      /// <param name="_LCSTEP_ID">Шаг жизненного цикла</param>
      /// <param name="_OWNER_ID">Идентификатор владельца объекта</param>
      /// <param name="_CHKOUT_BY">Кем объект взят на изменение</param>
      /// <param name="_CAPTION">Заголовок объекта</param>
      /// <param name="_F_VERSION_ID">Номер версии</param>
      /// <param name="_F_MODIFICATION_ID">Номер группы изменений</param>
      /// <param name="_F_BASE_VERSION">Является ли версия базовой</param>
      /// <param name="_Options"></param>
      /// <param name="parent">Родительская коллекция объектов</param>
      /// <param name="_F_PRJLINK_ID">Идентификатор связи (уникальный в пределах всей коллекции)</param>
      /// <param name="_F_PROJ_ID">Идентификатор версии родительского объекта</param>
      /// <param name="_F_RELATION_TYPE">Идентификатор типа связи</param>
      /// <param name="note">Примечание</param>
      public CompositionObject(
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
        ObjectVersionDescriptionOptions _Options,
        CompositionObjects parent,
        long _F_PRJLINK_ID,
        long _F_PROJ_ID,
        int _F_RELATION_TYPE,
        string note)
        : base(_ID, _OBJECT_ID, _OBJECT_TYPE, _LCSTEP_ID, _OWNER_ID, _CHKOUT_BY, _CAPTION, _F_VERSION_ID, _F_MODIFICATION_ID, _F_BASE_VERSION, _Options)
      {
        this.parent = parent;
        this.prjLinkID = _F_PRJLINK_ID;
        this.projID = _F_PROJ_ID;
        this.relTypeID = _F_RELATION_TYPE;
        this.note = note;
        this.items = new CompositionObjects(parent);
        this.parsedComposition = false;
      }

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из строки таблицы
      /// </summary>
      /// <param name="row">Строка таблицы с данными</param>
      public CompositionObject(DataRow row) => this.Assign((object) row);

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из указанного объекта
      /// </summary>
      /// <param name="source">Источник информации</param>
      public CompositionObject(object source) => this.Assign(source);

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из указанного объекта
      /// </summary>
      /// <param name="source">Объект-описатель</param>
      public CompositionObject(IDBRelation source) => this.Assign((object) source);

      /// <summary>Родительская коллекция объектов состава</summary>
      public CompositionObjects Parent
      {
        [DebuggerStepThrough] get => this.parent;
        internal set => this.parent = value;
      }

      /// <summary>Коллекция дочерних описаний объектов состава</summary>
      public CompositionObjects Items
      {
        [DebuggerStepThrough] get => this.items;
      }

      /// <summary>Дочернее описание объекта с указанным индексом</summary>
      /// <param name="index">Индекс</param>
      /// <returns>Дочернее описание объекта с указанным индексом</returns>
      public CompositionObject this[int index]
      {
        [DebuggerStepThrough] get => this.items[index];
      }

      /// <summary>Количество дочерних описаний объектов</summary>
      public int Count
      {
        [DebuggerStepThrough] get => this.items.Count;
      }

      /// <summary>
      /// Идентификатор связи (уникальный в пределах всей коллекции)
      /// </summary>
      public long F_PRJLINK_ID
      {
        [DebuggerStepThrough] get => this.prjLinkID;
        set => this.prjLinkID = value;
      }

      /// <summary>Идентификатор версии родительского объекта</summary>
      public long F_PROJ_ID
      {
        [DebuggerStepThrough] get => this.projID;
        set => this.projID = value;
      }

      /// <summary>Идентификатор типа связи</summary>
      public int F_RELATION_TYPE
      {
        [DebuggerStepThrough] get => this.relTypeID;
        set => this.relTypeID = value;
      }

      /// <summary>Примечание</summary>
      public string Note
      {
        [DebuggerStepThrough] get => this.note;
        set => this.note = value;
      }

      /// <summary>Был ли обработан состав объекта</summary>
      public bool ParsedComposition
      {
        [DebuggerStepThrough] get => this.parsedComposition;
        set => this.parsedComposition = value;
      }

      /// <summary>
      /// Отыскать в коллекции описание объекта состава с указанным идентификатором связи.
      /// Поиск также будет проходить в дочерних коллекциях.
      /// </summary>
      /// <param name="F_PRJLINK_ID">Уникальный в пределах всей коллекции идентификатор версии объекта</param>
      /// <returns>null, если описание объекта состава не найдено</returns>
      public virtual CompositionObject FindRelation(long F_PRJLINK_ID)
      {
        if (F_PRJLINK_ID == 0L)
          return (CompositionObject) null;
        if (F_PRJLINK_ID == this.F_PRJLINK_ID)
          return this;
        for (int index = 0; index < this.items.Count; ++index)
        {
          CompositionObject relation = this.items[index].FindRelation(F_PRJLINK_ID);
          if (relation != null)
            return relation;
        }
        return (CompositionObject) null;
      }

      /// <summary>
      /// Получить список колонок, необходимых для получения списка объектов состава
      /// </summary>
      /// <returns>Список колонок, необходимых для получения списка объектов состава</returns>
      public override List<ColumnDescriptor> GetColumnDescriptors() => this.GetColumnDescriptors(false);

      /// <summary>
      /// Получить список колонок, необходимых для получения списка объектов состава
      /// </summary>
      /// <param name="advAttrs">Добавлять колонки дополнительных атрибутов</param>
      /// <returns>Список колонок, необходимых для получения списка объектов состава</returns>
      public virtual List<ColumnDescriptor> GetColumnDescriptors(bool advAttrs)
      {
        List<ColumnDescriptor> columnDescriptors = new List<ColumnDescriptor>((IEnumerable<ColumnDescriptor>) base.GetColumnDescriptors());
        columnDescriptors.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
        columnDescriptors.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PROJ_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
        columnDescriptors.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_RELATION_TYPE, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
        columnDescriptors.Add(new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad001c0-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
        columnDescriptors.Add(new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad001c1-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
        if (advAttrs)
        {
          columnDescriptors.Add(new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad00267-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
          columnDescriptors.Add(new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad0038f-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
          columnDescriptors.Add(new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad0058a-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
        }
        return columnDescriptors;
      }

      /// <summary>
      /// Получить список колонок, необходимых для получения списка объектов состава
      /// </summary>
      /// <param name="advAttributes">Описания дополнительных колонок (ID типа атрибута + источник атрибута)</param>
      /// <returns>Список колонок, необходимых для получения списка объектов состава</returns>
      public virtual List<ColumnDescriptor> GetColumnDescriptors(
        params Tuple<object, AttributeSourceTypes>[] advAttributes)
      {
        List<ColumnDescriptor> columnDescriptors = this.GetColumnDescriptors(false);
        if (advAttributes == null || advAttributes.Length == 0)
          return columnDescriptors;
        for (int index = 0; index < advAttributes.Length; ++index)
        {
          Tuple<object, AttributeSourceTypes> colItem = advAttributes[index];
          if (!columnDescriptors.Exists((Predicate<ColumnDescriptor>) (item => item.AttributeID == colItem.Item1 && item.AttributeSource == colItem.Item2)))
            columnDescriptors.Add(new ColumnDescriptor(colItem.Item1, colItem.Item2, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0));
        }
        return columnDescriptors;
      }

      /// <summary>Сравнить экземпляр класса с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true - объекты полностью идентичны</returns>
      public override bool Equals(object obj)
      {
        return !(obj is CompositionObject compositionObject) ? base.Equals(obj) : this.F_PRJLINK_ID == compositionObject.F_PRJLINK_ID;
      }

      /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode() => this.F_PRJLINK_ID.GetHashCode();

      /// <summary>Очистить экземпляр класса</summary>
      public override void Clear()
      {
        base.Clear();
        this.prjLinkID = 0L;
        this.projID = 0L;
        this.relTypeID = -1;
        this.note = string.Empty;
        this.items = new CompositionObjects();
        this.parsedComposition = false;
      }

      /// <summary>Скопировать информацию из указанного объекта</summary>
      /// <param name="source">Объект-источник</param>
      public override void Assign(object source)
      {
        if (this == source)
          return;
        base.Assign(source);
        switch (source)
        {
          case CompositionObject compositionObject1:
            this.prjLinkID = compositionObject1.F_PRJLINK_ID;
            this.projID = compositionObject1.F_PROJ_ID;
            this.relTypeID = compositionObject1.F_RELATION_TYPE;
            this.note = compositionObject1.Note;
            this.items.Assign(compositionObject1.Items);
            this.parsedComposition = compositionObject1.ParsedComposition;
            break;
          case SimpleCompositionObject compositionObject2:
            this.prjLinkID = compositionObject2.F_PRJLINK_ID;
            this.projID = compositionObject2.F_PROJ_ID;
            this.relTypeID = compositionObject2.F_RELATION_TYPE;
            break;
          case DataRow row:
            this.prjLinkID = DataSetProcessor.GetInt64Value(row, "F_PRJLINK_ID", 0L);
            this.projID = DataSetProcessor.GetInt64Value(row, "F_PROJ_ID", 0L);
            this.relTypeID = DataSetProcessor.GetInt32Value(row, "F_RELATION_TYPE", -1);
            this.CalcFields();
            break;
          case IDBRelation dbRelation:
            this.prjLinkID = dbRelation.RelationID;
            this.projID = dbRelation.ProjID;
            this.relTypeID = dbRelation.RelationType;
            this.CalcFields();
            break;
        }
      }
    }
}
