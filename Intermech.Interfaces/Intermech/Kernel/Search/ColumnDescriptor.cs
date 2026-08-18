
// Type: Intermech.Kernel.Search.ColumnDescriptor
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Kernel.Search
{
    /// <summary>
    /// Структура с информацией о колонке, которую нужно получить методом IDBRecords.Select
    /// </summary>
    [DebuggerDisplay("Attr: {AttributeID}; Source: {AttributeSource}; Name: {ColumnName}; Contents: {Contents}")]
    [Serializable]
    public struct ColumnDescriptor
    {
      /// <summary>Идентификатор атрибута (числовой ид., guid или имя)</summary>
      public object AttributeID;
      /// <summary>
      /// Чему принадлежит данный атрибут (объекту, связи или определяется автоматически)
      /// </summary>
      public AttributeSourceTypes AttributeSource;
      /// <summary>Виды информации, которую нужно получить по атрибуту.</summary>
      public ColumnContents Contents;
      /// <summary>Способ именования колонки атрибута в DataTable</summary>
      public ColumnNameMapping ColumnName;
      /// <summary>Задает порядок сортировки данных по этой колонке</summary>
      public SortOrders Sort;
      /// <summary>
      /// Задает приоритет сортировки для данной колонки (ее порядок в операторе ORDER BY)
      /// </summary>
      public int OrderByID;
      /// <summary>Пустая структура</summary>
      private static readonly ColumnDescriptor _empty = new ColumnDescriptor();
      private static readonly object[] _emptyArray = new object[0];

      public ColumnDescriptor(
        object attributeID,
        AttributeSourceTypes attributeSource,
        ColumnContents contents,
        ColumnNameMapping columnName,
        SortOrders sort,
        int orderByID)
      {
        this.AttributeID = attributeID;
        this.AttributeSource = attributeSource;
        this.Contents = contents;
        this.ColumnName = columnName;
        this.Sort = sort;
        this.OrderByID = orderByID;
      }

      public ColumnDescriptor(
        object attributeID,
        ColumnContents contents,
        ColumnNameMapping columnName,
        SortOrders sort,
        int orderByID)
      {
        this.AttributeID = attributeID;
        this.AttributeSource = AttributeSourceTypes.Auto;
        this.Contents = contents;
        this.ColumnName = columnName;
        this.Sort = sort;
        this.OrderByID = orderByID;
      }

      public ColumnDescriptor(object attributeID, SortOrders sort, int orderByID)
      {
        this.AttributeID = attributeID;
        this.AttributeSource = AttributeSourceTypes.Auto;
        this.Contents = ColumnContents.Text;
        this.ColumnName = ColumnNameMapping.Default;
        this.Sort = sort;
        this.OrderByID = orderByID;
      }

      public ColumnDescriptor(object attributeID)
      {
        this.AttributeID = attributeID;
        this.AttributeSource = AttributeSourceTypes.Auto;
        this.Contents = ColumnContents.Text;
        this.ColumnName = ColumnNameMapping.Default;
        this.Sort = SortOrders.NONE;
        this.OrderByID = 0;
      }

      /// <summary>Сравнить структуру с другим объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты равны</returns>
      public override bool Equals(object obj)
      {
        if (obj == null)
          return base.Equals(obj);
        ColumnDescriptor columnDescriptor = (ColumnDescriptor) obj;
        return this.AttributeID.Equals(columnDescriptor.AttributeID) && this.AttributeSource.Equals((object) columnDescriptor.AttributeSource) && this.ColumnName.Equals((object) columnDescriptor.ColumnName) && this.Contents.Equals((object) columnDescriptor.Contents);
      }

      /// <summary>Вернуть 32-битный хэш-код экземпляра объекта</summary>
      /// <returns>32-битный хэш-код экземпляра объекта</returns>
      public override int GetHashCode()
      {
        return this.AttributeID.GetHashCode() << 4 ^ this.AttributeSource.GetHashCode() << 2 ^ this.ColumnName.GetHashCode();
      }

      /// <summary>Пустая структура</summary>
      public static ColumnDescriptor Empty
      {
        [DebuggerStepThrough] get => ColumnDescriptor._empty;
      }

      /// <summary>В целях упрощения синтасиса создания DBRecordSetParams с единственной колонкой или вообще без них (ColumnDescriptor.Empty)</summary>
      public static implicit operator object[](ColumnDescriptor columnDescriptor)
      {
        if (columnDescriptor.AttributeID == null)
          return ColumnDescriptor._emptyArray;
        return new object[1]{ (object) columnDescriptor };
      }
    }
}
