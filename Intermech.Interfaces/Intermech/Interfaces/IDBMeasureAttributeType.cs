
// Type: Intermech.Interfaces.IDBMeasureAttributeType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс типа для атрибутов, выраженных в единицах измерения
    /// </summary>
    public interface IDBMeasureAttributeType
    {
      /// <summary>Формула проверки значений, вводимых в атрибут</summary>
      string RuleFormula { get; }

      /// <summary>Единица измерения, используемая по умолчанию</summary>
      long DefaultMeasureID { get; }

      /// <summary>
      /// Добавлять ли в строковой составляющей атрибута краткое наименование единиц измерения если сохранение
      /// производилось в единице по умолчанию
      /// </summary>
      bool ShortNameInString { get; }

      /// <summary>
      /// Конвертировать ли в единицу измерения по умолчанию строковую составляющую значения атрибута
      /// </summary>
      bool ConvertToDefaultMeasure { get; }

      /// <summary>
      /// Проверяет допустимость присвоения данному атрибуту единицы измерения muID
      /// </summary>
      void ValidateMuID(long muID);

      /// <summary>
      /// Возвращает список идентификаторов физических величин, единицы измерения которых можно присваивать данному атрибуту.
      /// Возвращает массив нулевой длины, если атрибуту можно присвоить любую единицу измерения.
      /// </summary>
      long[] GetValidPhysicalValues();

      /// <summary>Проверяет единицы измерения на совместимость.</summary>
      /// <param name="aMeasureID">Идентификатор объекта-единицы измерения, который
      /// проверяется на совместимость с данным атрибутом (например, единица массы не
      /// совместима с единицей объема и т.п.). </param>
      bool IsCompatible(long aMeasureID);
    }
}
