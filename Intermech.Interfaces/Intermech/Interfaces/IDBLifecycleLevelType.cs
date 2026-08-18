
// Type: Intermech.Interfaces.IDBLifecycleLevelType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс для редактирования уровня продвижения</summary>
    public interface IDBLifecycleLevelType
    {
      /// <summary>
      /// Идентификатор уровня продвижения. В некоторых реализациях может быть только для
      /// чтения.
      /// </summary>
      int LevelID { get; }

      /// <summary>Наименование уровня продвижения.</summary>
      string LevelName { get; set; }

      /// <summary>Литера уровня продвижения (например, А)</summary>
      string Litera { get; set; }

      /// <summary>Иконка, отображающая уровень продвижения.</summary>
      byte[] LevelIcon { get; set; }

      /// <summary>Является ли уровень продвижения уровнем по умолчанию</summary>
      bool IsDefaultLevel { get; set; }

      /// <summary>
      /// Файловый шкаф, в котором будут размещаться двоичные данные объектов, перемещенных на шаг ЖЦ с данным уровнем продвижения
      /// </summary>
      long StorageID { get; set; }

      /// <summary>Глобальный идентификатор уровня продвижения</summary>
      Guid GUID { get; set; }

      /// <summary>Удалить уровень продвижения</summary>
      /// <param name="DeleteMode">Зарезервировано</param>
      /// <returns>Зарезервировано</returns>
      int Delete(long DeleteMode);

      /// <summary>
      /// Структура со свойствами вновь создаваемого шага ЖЦ данного уровня продвижения
      /// </summary>
      DBLifecycleStepProperties DefaultPropertiesForLCStep();
    }
}
