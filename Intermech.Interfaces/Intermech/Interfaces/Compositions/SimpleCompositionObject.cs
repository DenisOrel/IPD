
// Type: Intermech.Interfaces.Compositions.SimpleCompositionObject
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
    /// <summary>Упрощённый объект состава</summary>
    [DebuggerDisplay("F_OBJECT_ID: {F_OBJECT_ID}; Caption: {CAPTION}")]
    [Serializable]
    public class SimpleCompositionObject : ObjectVersionDescription, ICloneable
    {
      /// <summary>Уникальный глобальный идентификатор связи</summary>
      protected Guid linkGuid = Guid.Empty;
      /// <summary>
      /// Идентификатор связи (уникальный в пределах всей коллекции)
      /// </summary>
      protected long prjLinkID;
      /// <summary>Идентификатор версии родительского объекта</summary>
      protected long projID;
      /// <summary>Идентификатор типа связи</summary>
      protected int relTypeID = -1;
      /// <summary>Список колонок для запроса в "ядро"</summary>
      protected static List<ColumnDescriptor> columnDescriptorsComposition;

      /// <summary>Создать незаполненное описание объекта состава</summary>
      public SimpleCompositionObject()
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
      /// <param name="_Options">Опции</param>
      /// <param name="_F_LINK_GUID">Уникальный глобальный идентификатор связи</param>
      /// <param name="_F_PRJLINK_ID">Идентификатор связи (уникальный в пределах всей коллекции)</param>
      /// <param name="_F_PROJ_ID">Идентификатор версии родительского объекта</param>
      /// <param name="_F_RELATION_TYPE">Идентификатор типа связи</param>
      public SimpleCompositionObject(
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
        Guid _F_LINK_GUID,
        long _F_PRJLINK_ID,
        long _F_PROJ_ID,
        int _F_RELATION_TYPE)
        : base(_ID, _OBJECT_ID, _OBJECT_TYPE, _LCSTEP_ID, _OWNER_ID, _CHKOUT_BY, _CAPTION, _F_VERSION_ID, _F_MODIFICATION_ID, _F_BASE_VERSION, _Options)
      {
        this.linkGuid = _F_LINK_GUID;
        this.prjLinkID = _F_PRJLINK_ID;
        this.projID = _F_PROJ_ID;
        this.relTypeID = _F_RELATION_TYPE;
      }

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из строки таблицы
      /// </summary>
      /// <param name="row">Строка таблицы с данными</param>
      public SimpleCompositionObject(DataRow row) => this.Assign((object) row);

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из указанного объекта
      /// </summary>
      /// <param name="source">Источник информации</param>
      public SimpleCompositionObject(object source) => this.Assign(source);

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из указанного объекта
      /// </summary>
      /// <param name="source">Объект-описатель</param>
      public SimpleCompositionObject(IDBRelation source) => this.Assign((object) source);

      /// <summary>Уникальный глобальный идентификатор связи</summary>
      public Guid LINK_GUID
      {
        [DebuggerStepThrough] get => this.linkGuid;
        set => this.linkGuid = value;
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

      /// <summary>
      /// Получить список колонок, необходимых для получения списка объектов состава
      /// </summary>
      /// <returns>Список колонок, необходимых для получения списка объектов состава</returns>
      public override List<ColumnDescriptor> GetColumnDescriptors()
      {
        if (SimpleCompositionObject.columnDescriptorsComposition == null)
        {
          SimpleCompositionObject.columnDescriptorsComposition = new List<ColumnDescriptor>((IEnumerable<ColumnDescriptor>) base.GetColumnDescriptors());
          SimpleCompositionObject.columnDescriptorsComposition.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
          SimpleCompositionObject.columnDescriptorsComposition.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PROJ_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
          SimpleCompositionObject.columnDescriptorsComposition.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_RELATION_TYPE, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
          SimpleCompositionObject.columnDescriptorsComposition.Add(new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJ_GUID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
        }
        return SimpleCompositionObject.columnDescriptorsComposition;
      }

      /// <summary>Сравнить экземпляр класса с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true - объекты полностью идентичны</returns>
      public override bool Equals(object obj)
      {
        return !(obj is SimpleCompositionObject compositionObject) ? base.Equals(obj) : this.F_PRJLINK_ID == compositionObject.F_PRJLINK_ID;
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
        this.linkGuid = Guid.Empty;
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
          case SimpleCompositionObject compositionObject1:
            this.prjLinkID = compositionObject1.F_PRJLINK_ID;
            this.projID = compositionObject1.F_PROJ_ID;
            this.relTypeID = compositionObject1.F_RELATION_TYPE;
            this.linkGuid = compositionObject1.LINK_GUID;
            break;
          case CompositionObject compositionObject2:
            this.prjLinkID = compositionObject2.F_PRJLINK_ID;
            this.projID = compositionObject2.F_PROJ_ID;
            this.relTypeID = compositionObject2.F_RELATION_TYPE;
            break;
          case DataRow row:
            this.prjLinkID = DataSetProcessor.GetInt64Value(row, "F_PRJLINK_ID", 0L);
            this.projID = DataSetProcessor.GetInt64Value(row, "F_PROJ_ID", 0L);
            this.relTypeID = DataSetProcessor.GetInt32Value(row, "F_RELATION_TYPE", -1);
            this.linkGuid = DataSetProcessor.GetGuidValue(row, "F_PRJ_GUID", Guid.Empty);
            this.CalcFields();
            break;
          case IDBRelation dbRelation:
            this.prjLinkID = dbRelation.RelationID;
            this.projID = dbRelation.ProjID;
            this.relTypeID = dbRelation.RelationType;
            this.linkGuid = dbRelation.GUID;
            this.CalcFields();
            break;
        }
      }
    }
}
