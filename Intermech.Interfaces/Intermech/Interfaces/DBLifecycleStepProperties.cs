
// Type: Intermech.Interfaces.DBLifecycleStepProperties
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Структура, описывающая свойства шага ЖЦ</summary>
    [Serializable]
    public struct DBLifecycleStepProperties(
      int lcStep,
      int objectTypeID,
      string lcName,
      string note,
      LCAccessTypes accessType,
      int levelID,
      ObjectModifyModes objectModifyMode,
      Guid stepGuid,
      bool firstStep,
      LCStepOptions options)
    {
      /// <summary>Идентификтор этапа ЖЦ (только для чтения)</summary>
      public int LCStep = lcStep;
      /// <summary>Наименование этапа ЖЦ</summary>
      public string LCName = lcName;
      /// <summary>Комментарии</summary>
      public string Note = note;
      /// <summary>
      /// Идентификатор типа объекта, к которому относится данный этап ЖЦ (только для
      /// чтения)
      /// </summary>
      public int ObjectTypeID = objectTypeID;
      /// <summary>
      /// Тип доступа к объектам на данном этапе ЖЦ:
      /// 0 - контроль прав не производится,
      /// 1 - контроль только по правам ЖЦ (без возможности индивидуального назначения
      /// прав).
      /// 2 - контроль по ЖЦ и персонально объекту (но без возможности передачи прав
      /// доступа по наследству),
      /// 3 - то же, но с возможностью публиковать права подтипам.
      /// </summary>
      public LCAccessTypes AccessType = accessType;
      /// <summary>Идентификатор уровня продвижения.</summary>
      public int LevelID = levelID;
      /// <summary>Способ модификации объектов на данном шаге ЖЦ</summary>
      public ObjectModifyModes ObjectModifyMode = objectModifyMode;
      /// <summary>Глобальный идентификатор шага ЖЦ</summary>
      public Guid StepGuid = stepGuid;
      /// <summary>
      /// Признак того, что данный шаг является первым в схеме ЖЦ
      /// </summary>
      public bool FirstStep = firstStep;
      /// <summary>Опции шага ЖЦ</summary>
      public LCStepOptions Options = options;

      public DBLifecycleStepProperties(DataRow dataRow)
        : this(Convert.ToInt32(dataRow["F_LC_STEP"]), Convert.ToInt32(dataRow["F_OBJECT_TYPE"]), dataRow["F_LC_NAME"].ToString(), dataRow["F_NOTE"].ToString(), (LCAccessTypes) Convert.ToInt32(dataRow["F_ACCESS_TYPE"]), Convert.ToInt32(dataRow["F_LEVEL_ID"]), (ObjectModifyModes) Convert.ToInt32(dataRow["F_MODIFY_MODE"]), new Guid(dataRow["F_GUID"].ToString()), Convert.ToInt32(dataRow["F_FIRST"]) != 0, (LCStepOptions) Convert.ToInt32(dataRow["F_OPTIONS"]))
      {
      }

      public static void StoreToDataRow(
        DBLifecycleStepProperties stepProps,
        bool deleted,
        DataRow dataRow)
      {
        dataRow["F_LC_STEP"] = (object) stepProps.LCStep;
        dataRow["F_LEVEL_ID"] = (object) stepProps.LevelID;
        dataRow["F_LC_NAME"] = (object) stepProps.LCName;
        dataRow["F_NOTE"] = (object) stepProps.Note;
        dataRow["F_OBJECT_TYPE"] = (object) stepProps.ObjectTypeID;
        dataRow["F_ACCESS_TYPE"] = (object) Convert.ToInt32((object) stepProps.AccessType);
        dataRow["F_MODIFY_MODE"] = (object) Convert.ToInt32((object) stepProps.ObjectModifyMode);
        dataRow["F_DELETED"] = (object) (deleted ? 1 : 0);
        dataRow["F_GUID"] = (object) stepProps.StepGuid;
        dataRow["F_FIRST"] = (object) stepProps.FirstStep;
        dataRow["F_OPTIONS"] = (object) Convert.ToInt32((object) stepProps.Options);
      }
    }
}
