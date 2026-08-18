
// Type: Intermech.Interfaces.IDBMetadataExtensions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс для чтения/записи расширенных метаданных</summary>
    public interface IDBMetadataExtensions
    {
      /// <summary>
      /// Записывает в расширенные матаданные именованный набор значений строкового типа
      /// </summary>
      /// <param name="valueName">Имя значений</param>
      /// <param name="categoryType">Категория, к которой относятся значения (или Consts.CategoryUnknown)</param>
      /// <param name="valuesList">Список значений - будет записан в указанном порядке</param>
      void SetMDValues(string valueName, int categoryType, string[] valuesList);

      /// <summary>
      /// Ссылка на таблицу с расширениями метаданных IMS_MD_EXTENSIONS
      /// </summary>
      DataTable ExtensionsTable { get; }

      /// <summary>
      /// Записывает в расширенные матаданные именованный набор значений типа int
      /// </summary>
      void SetMDValues(string valueName, int categoryType, int[] valuesList);

      /// <summary>
      /// Записывает в расширенные матаданные именованный набор значений типа long
      /// </summary>
      void SetMDValues(string valueName, int categoryType, long[] valuesList);

      /// <summary>
      /// Записывает в расширенные матаданные именованный набор значений типа Guid
      /// </summary>
      void SetMDValues(string valueName, int categoryType, Guid[] valuesList);

      /// <summary>
      /// Записывает в расширенные матаданные именованный набор значений
      /// </summary>
      /// <param name="valueName">Имя значений</param>
      /// <param name="valuesList">Список значений - будет записан в указанном порядке</param>
      void SetMDValues(string valueName, string[] valuesList);

      void SetMDValue(string valueName, int categoryType, string value);

      void SetMDValue(string valueName, string value);

      /// <summary>
      /// Возвращает строковый список значений для параметра valueName
      /// </summary>
      string[] GetMDValues(string valueName);

      /// <summary>
      /// Возвращает целочисленный список значений для параметра valueName
      /// </summary>
      int[] GetMDValuesInt(string valueName);

      /// <summary>
      /// Возвращает список значений для параметра valueName в виде гуидов
      /// </summary>
      Guid[] GetMDValuesGuid(string valueName);

      /// <summary>
      /// Возвращает список значений для параметра valueName в виде Int64
      /// </summary>
      long[] GetMDValuesInt64(string valueName);

      /// <summary>
      /// Возвращает первое строковое значение для параметра valueName
      /// </summary>
      string GetMDValue(string valueName);
    }
}
