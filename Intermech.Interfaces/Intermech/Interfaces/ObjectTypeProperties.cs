
// Type: Intermech.Interfaces.ObjectTypeProperties
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Структура, содержащая свойства типа объектов</summary>
    [Serializable]
    public struct ObjectTypeProperties(
      int _objectType,
      string _objectTypeName,
      string _objectInstanceName,
      string _note,
      ObjectVersionModes _versionable,
      int _defaultRelation,
      string _areaID,
      Guid _objectTypeGuid,
      int _captionAttribute,
      bool _anyAttributes,
      InheritModes _publicLCSchema,
      string _objectTypeShortName,
      int lifetimeReserve,
      ObjectTypeOptions options,
      int schemaID)
    {
      /// <summary>Идентификатор типа объекта (только для чтения)</summary>
      public int ObjectType = _objectType;
      /// <summary>Наименование типа объектов</summary>
      public string ObjectTypeName = _objectTypeName;
      /// <summary>Наименование объекта данного типа (например, Деталь)</summary>
      public string ObjectInstanceName = _objectInstanceName;
      /// <summary>Комментарии</summary>
      public string Note = _note;
      /// <summary>
      /// 0 - абстрактный тип объекта (контейнер для группировки других типов объектов);
      /// 1 - объекты данного типа не могут иметь версий;
      /// 2 - объекты данного типа могут иметь версии.
      /// </summary>
      public ObjectVersionModes Versionable = _versionable;
      /// <summary>
      /// Ид. типа связи, который показывается по умолчанию в дереве универсального
      /// клиента для объектов данного типа.
      /// </summary>
      public int DefaultRelation = _defaultRelation;
      /// <summary>Идентификатор предметной области</summary>
      public string AreaID = _areaID;
      /// <summary>Глобальный идентификатор типа объектов</summary>
      public Guid ObjectTypeGuid = _objectTypeGuid;
      /// <summary>
      /// Ид. атрибута, который используется для отображения данного типа объектов
      /// в списках.
      /// </summary>
      public int CaptionAttribute = _captionAttribute;
      /// <summary>
      /// Контроль набора атрибутов
      /// false - допускается добавлять к объектам данного типа только разрешенные атрибуты.
      /// true - допускается добавлять любые атрибуты.
      /// </summary>
      public bool AnyAttributes = _anyAttributes;
      /// <summary>
      /// Наследует ли тип объектов схему жизненного цикла от родительского объекта или схема собственная
      /// </summary>
      public InheritModes PublicLCSchema = _publicLCSchema;
      /// <summary>Краткое наименование типа объектов</summary>
      public string ObjectTypeShortName = _objectTypeShortName;
      /// <summary>Опции</summary>
      public ObjectTypeOptions Options = options;
      /// <summary>
      /// Количество дней, в течение которых нельзя физически уничтожать удаленные объекты данного типа
      /// (время жизни удаленных объектов).
      /// </summary>
      public int LifetimeReserve = lifetimeReserve;
      /// <summary>Идентификатор схемы ЖЦ для объектов данного типа</summary>
      public int SchemaID = schemaID;
      /// <summary>
      /// Пытаться ли переводить уже существующие объекты на шаги новой схемы ЖЦ
      /// </summary>
      public bool ChangeObjectsSchema = false;

      public ObjectTypeProperties(DataRow row)
        : this(Convert.ToInt32(row["F_OBJECT_TYPE"]), row["F_OBJ_TYPE_NAME"].ToString(), row["F_OBJ_NAME"].ToString(), row["F_NOTE"].ToString(), (ObjectVersionModes) Convert.ToInt32(row["F_VERSIONABLE"]), Convert.ToInt32(row["F_DEFAULT_RELATION"]), row["F_AREA_ID"].ToString(), new Guid(row["F_GUID"].ToString()), Convert.ToInt32(row["F_CAPTION_ATTRIBUTE"]), Convert.ToBoolean(row["F_ANY_ATTRIBUTES"]), (InheritModes) Convert.ToInt32(row["F_PUBLIC_LC"]), row["F_SHORT_NAME"].ToString(), Convert.ToInt32(row["F_DEL_TIME"]), (ObjectTypeOptions) Convert.ToInt32(row["F_OPTIONS"]), Convert.ToInt32(row["F_SCHEMA_ID"]))
      {
      }

      public static ObjectTypeProperties Empty()
      {
        return new ObjectTypeProperties(0, string.Empty, string.Empty, string.Empty, ObjectVersionModes.Abstract, 0, string.Empty, Guid.Empty, 0, true, InheritModes.Inherited, string.Empty, 0, ObjectTypeOptions.None, 0);
      }
    }
}
