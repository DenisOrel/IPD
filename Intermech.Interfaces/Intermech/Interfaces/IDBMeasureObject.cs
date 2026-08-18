
// Type: Intermech.Interfaces.IDBMeasureObject
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    public interface IDBMeasureObject
    {
      /// <summary>Краткое наименование единицы измерения</summary>
      string ShortMUName { get; }

      /// <summary>Полное наименование единицы измерения</summary>
      string MUName { get; }

      /// <summary>true, если это базовая единица измерения</summary>
      bool IsBaseUnit { get; }

      /// <summary>Возвращает идентификатор базовой единицы измерения</summary>
      long BaseUnitID { get; }

      /// <summary>
      /// Метод возвращает описатель единицы измерения для данного объекта
      /// </summary>
      MeasureDescriptor GetMeasureDescriptor();
    }
}
