
// Type: Intermech.Interfaces.MetadataExtensions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Вспомогательный класс для работы с расширенными метаданными (таблица Consts.IMS_MD_EXTENSIONS).
    /// Исходные тексты взяты из класса Intermech.Kernel.DBMetadataExtensions
    /// (N:\Intermech.Kernel\BaseClasses\DBMetadataExtensions.cs).
    /// 
    /// Данный класс осуществляет только чтение из указанной таблицы. Никаких изменений в таблицу класс не вносит.
    /// </summary>
    internal static class MetadataExtensions
    {
      /// <summary>
      /// Возвращает первое строковое значение для параметра valueName
      /// </summary>
      /// <param name="table">Таблица с расширениями метаданных</param>
      /// <param name="valueName">Имя параметра</param>
      /// <param name="attributeTypeID">Идентификатор типа атрибута</param>
      /// <param name="objectTypeID">Идентификатор типа объекта</param>
      /// <param name="relationTypeID">Идентификатор типа связи</param>
      /// <returns>Первое строковое значение для параметра valueName</returns>
      public static string GetMDValue(
        DataTable table,
        string valueName,
        int attributeTypeID,
        int objectTypeID,
        int relationTypeID)
      {
        if (table == null)
          throw new ArgumentNullException(nameof (table));
        lock (table)
        {
          DataRow[] dataRowArray = table.Select($"F_ATTRIBUTE_ID = {attributeTypeID} AND F_OBJECT_TYPE = {objectTypeID} AND F_RELATION_TYPE = {relationTypeID} AND F_PARAM_NAME = {DataSetProcessor.QString(valueName)}", "F_INLIST_ID ASC");
          return dataRowArray.Length != 0 ? Convert.ToString(dataRowArray[0]["F_VALUE"]) : string.Empty;
        }
      }

      /// <summary>
      /// Возвращает список строковых значений для параметра valueName
      /// </summary>
      /// <param name="table">Таблица с расширениями метаданных</param>
      /// <param name="valueName">Имя параметра</param>
      /// <param name="attributeTypeID">Идентификатор типа атрибута</param>
      /// <param name="objectTypeID">Идентификатор типа объекта</param>
      /// <param name="relationTypeID">Идентификатор типа связи</param>
      /// <returns>Список строковых значений для параметра valueName</returns>
      public static string[] GetMDValues(
        DataTable table,
        string valueName,
        int attributeTypeID,
        int objectTypeID,
        int relationTypeID)
      {
        if (table == null)
          throw new ArgumentNullException(nameof (table));
        lock (table)
        {
          DataRow[] dataRowArray = table.Select($"F_ATTRIBUTE_ID = {attributeTypeID} AND F_OBJECT_TYPE = {objectTypeID} AND F_RELATION_TYPE = {relationTypeID} AND F_PARAM_NAME = {DataSetProcessor.QString(valueName)}", "F_INLIST_ID ASC");
          string[] mdValues = new string[dataRowArray.Length];
          for (int index = 0; index < mdValues.Length; ++index)
            mdValues[index] = Convert.ToString(dataRowArray[index]["F_VALUE"]);
          return mdValues;
        }
      }

      /// <summary>
      /// Возвращает список целочисленных 32-битных значений для параметра valueName
      /// </summary>
      /// <param name="table">Таблица с расширениями метаданных</param>
      /// <param name="valueName">Имя параметра</param>
      /// <param name="attributeTypeID">Идентификатор типа атрибута</param>
      /// <param name="objectTypeID">Идентификатор типа объекта</param>
      /// <param name="relationTypeID">Идентификатор типа связи</param>
      /// <returns>Список целочисленных 32-битных значений для параметра valueName</returns>
      public static int[] GetMDValuesInt(
        DataTable table,
        string valueName,
        int attributeTypeID,
        int objectTypeID,
        int relationTypeID)
      {
        string[] mdValues = MetadataExtensions.GetMDValues(table, valueName, attributeTypeID, objectTypeID, relationTypeID);
        int[] mdValuesInt = new int[mdValues.Length];
        for (int index = 0; index < mdValuesInt.Length; ++index)
          mdValuesInt[index] = Convert.ToInt32(mdValues[index]);
        return mdValuesInt;
      }

      /// <summary>
      /// Возвращает список Guid-значений для параметра valueName
      /// </summary>
      /// <param name="table">Таблица с расширениями метаданных</param>
      /// <param name="valueName">Имя параметра</param>
      /// <param name="attributeTypeID">Идентификатор типа атрибута</param>
      /// <param name="objectTypeID">Идентификатор типа объекта</param>
      /// <param name="relationTypeID">Идентификатор типа связи</param>
      /// <returns>Список Guid-значений для параметра valueName</returns>
      public static Guid[] GetMDValuesGuid(
        DataTable table,
        string valueName,
        int attributeTypeID,
        int objectTypeID,
        int relationTypeID)
      {
        string[] mdValues = MetadataExtensions.GetMDValues(table, valueName, attributeTypeID, objectTypeID, relationTypeID);
        Guid[] mdValuesGuid = new Guid[mdValues.Length];
        for (int index = 0; index < mdValuesGuid.Length; ++index)
          mdValuesGuid[index] = !GuidHelper.IsGuid(mdValues[index]) ? Guid.Empty : new Guid(mdValues[index]);
        return mdValuesGuid;
      }

      /// <summary>
      /// Возвращает список целочисленных 64-битных значений для параметра valueName
      /// </summary>
      /// <param name="table">Таблица с расширениями метаданных</param>
      /// <param name="valueName">Имя параметра</param>
      /// <param name="attributeTypeID">Идентификатор типа атрибута</param>
      /// <param name="objectTypeID">Идентификатор типа объекта</param>
      /// <param name="relationTypeID">Идентификатор типа связи</param>
      /// <returns>Список целочисленных 64-битных значений для параметра valueName</returns>
      public static long[] GetMDValuesInt64(
        DataTable table,
        string valueName,
        int attributeTypeID,
        int objectTypeID,
        int relationTypeID)
      {
        string[] mdValues = MetadataExtensions.GetMDValues(table, valueName, attributeTypeID, objectTypeID, relationTypeID);
        long[] mdValuesInt64 = new long[mdValues.Length];
        for (int index = 0; index < mdValuesInt64.Length; ++index)
          mdValuesInt64[index] = Convert.ToInt64(mdValues[index]);
        return mdValuesInt64;
      }
    }
}
