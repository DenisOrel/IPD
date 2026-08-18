
// Type: Intermech.Kernel.Search.ColumnNameMapping
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Kernel.Search
{
    /// <summary>
    /// Задает способ именования колонки атрибута в DataTable, возвращаемый методом IDBRecords.Select
    /// Default - имя атрибута + слова (идентификатор) для ColumnContents.ID, (дата модификации) для ColumnContents.Date
    ///   и (место хранения) или (приведенное значение) для файлов (блобов) или единиц измерения
    /// ID - целочисленный ид. атрибута
    /// Guid - глобальный ид. атрибута
    /// Alias - алиас атрибута
    /// ShortName - краткое наименование атрибута,
    /// Name - наименование атрибута,
    /// FieldName - имя колонки в базе данных (только для обязательных атрибутов!)
    /// Index - порядковый номер колонки в DataTable
    /// </summary>
    public enum ColumnNameMapping
    {
      /// <summary>
      /// Имя атрибута + слова (идентификатор) для ColumnContents.ID, (дата модификации) для ColumnContents.Date
      /// и (место хранения) или (приведенное значение) для файлов (блобов) или единиц измерения
      /// </summary>
      Default,
      /// <summary>Целочисленный идентификатор атрибута</summary>
      ID,
      /// <summary>Глобальный идентификатор атрибута</summary>
      Guid,
      /// <summary>Псевдоним атрибута</summary>
      Alias,
      /// <summary>Краткое наименование атрибута</summary>
      ShortName,
      /// <summary>Наименование атрибута</summary>
      Name,
      /// <summary>
      /// Имя колонки в базе данных (только для обязательных атрибутов!)
      /// </summary>
      FieldName,
      /// <summary>Порядковый номер колонки в DataTable</summary>
      Index,
    }
}
