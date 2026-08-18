
// Type: Intermech.Interfaces.LifeCycles.DBLCSchemaProperties
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces.LifeCycles
{
    /// <summary>Структура для передачи свойств схемы ЖЦ</summary>
    [Serializable]
    public struct DBLCSchemaProperties(
      int schemaID,
      string name,
      string note,
      Guid guid,
      bool isDefault,
      string areaID,
      LCSchemaOptions options)
    {
      /// <summary>Локальный идентификатор схемы ЖЦ</summary>
      public int SchemaID = schemaID;
      /// <summary>Уникальное наименование схемы ЖЦ</summary>
      public string Name = name;
      /// <summary>Комментарии к схеме ЖЦ</summary>
      public string Note = note;
      /// <summary>Глобальный идентификатор схемы ЖЦ</summary>
      public Guid GUID = guid;
      /// <summary>
      /// Является ли данная схема ЖЦ схемой по умолчанию.
      /// Схема по умолчанию присваивается вновь создаваемым типам объектов (которые создаются на верхнем уровне иерархии типов).
      /// </summary>
      public bool IsDefaultSchema = isDefault;
      /// <summary>Предметные области</summary>
      public string AreaID = areaID;
      /// <summary>Опции</summary>
      public LCSchemaOptions Options = options;
      /// <summary>Создавать ли пустую схему (без шагов ЖЦ по умолчанию)</summary>
      public bool CreateEmptySchema = false;

      public DBLCSchemaProperties(DataRow row)
        : this(Convert.ToInt32(row["F_SCHEMA_ID"]), row["F_NAME"].ToString(), row["F_NOTE"].ToString(), new Guid(row["F_GUID"].ToString()), Convert.ToInt32(row["F_DEFAULT"]) != 0, row["F_AREA_ID"].ToString(), (LCSchemaOptions) Convert.ToInt32(row["F_OPTIONS"]))
      {
      }
    }
}
