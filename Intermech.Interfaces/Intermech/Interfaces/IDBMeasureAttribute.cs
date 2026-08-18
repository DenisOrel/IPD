
// Type: Intermech.Interfaces.IDBMeasureAttribute
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс атрибута, хранящего числовое значение вместе с его единицей измерения.
    /// В строковой составляющей атрибута хранится строковое представление хранящегося значения в
    /// той единице измерения, в которой ввел его пользователь. В double - значение, приведенное
    /// к базовой величине. В integer - ид. базовой единицы измерения, в которой записано значение
    /// атрибута.
    /// </summary>
    public interface IDBMeasureAttribute
    {
      /// <summary>Идентификатор объекта, описывающего единицу измерения. При присвоении ID другой единицы
      /// измерения переделывает строковое представление значения.
      /// </summary>
      long MeasureID { get; set; }

      /// <summary>Короткое имя БАЗОВОЙ единиц измерения (например, кг)</summary>
      string MeasureShortName { get; }

      /// <summary>Полное наименование БАЗОВОЙ единиц измерения (например, килограмм).</summary>
      string MeasureName { get; }

      /// <summary>Проверяет единицы измерения на совместимость.</summary>
      /// <param name="aMeasureID">Идентификатор объекта-единицы измерения, который
      /// проверяется на совместимость с данным параметром (например, единица массы не
      /// совместима с единицей объема и т.п.). </param>
      bool IsCompatible(long aMeasureID);

      /// <summary>Значение атрибута</summary>
      MeasuredValue Value { get; set; }
    }
}
