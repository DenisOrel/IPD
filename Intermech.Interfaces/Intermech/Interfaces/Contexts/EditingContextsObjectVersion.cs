
// Type: Intermech.Interfaces.Contexts.EditingContextsObjectVersion
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


namespace Intermech.Interfaces.Contexts
{
    /// <summary>
    /// Информация о версии объекта в составе контекста редактирования
    /// (строка из таблицы "IMS_VERSIONS_CONTEXT")
    /// </summary>
    [DebuggerDisplay("F_CONTEXT_ID: {F_CONTEXT_ID}, F_OBJECT_ID: {F_OBJECT_ID}, F_MODIFICATION_ID: {F_MODIFICATION_ID}")]
    [Serializable]
    public sealed class EditingContextsObjectVersion : 
      IAssignable,
      ICloneable,
      IComparable,
      IComparable<EditingContextsObjectVersion>,
      IComparer<object>,
      IComparer<EditingContextsObjectVersion>
    {
      /// <summary>
      /// Идентификатор версии контекста, в составе которого находится данный объект
      /// </summary>
      public long F_CONTEXT_ID;
      /// <summary>Идентификатор объекта</summary>
      public long F_ID;
      /// <summary>
      /// Идентификатор версии объекта, которая находится в составе контекста
      /// </summary>
      public long F_OBJECT_ID;
      /// <summary>Номер изменения</summary>
      public long F_MODIFICATION_ID;
      /// <summary>Список колонок для запроса в "ядро"</summary>
      private static List<ColumnDescriptor> columnDescriptors = new List<ColumnDescriptor>();

      /// <summary>Создать пустой экземпляр класса</summary>
      public EditingContextsObjectVersion()
      {
      }

      /// <summary>Создать заполненный экземпляр класса</summary>
      /// <param name="_F_CONTEXT_ID">Идентификатор версии контекста, в составе которого находится данный объект</param>
      /// <param name="_F_ID">Идентификатор объекта</param>
      /// <param name="_F_OBJECT_ID">Идентификатор версии объекта, которая находится в составе контекста</param>
      /// <param name="_F_MODIFICATION_ID">Номер изменения</param>
      public EditingContextsObjectVersion(
        long _F_CONTEXT_ID,
        long _F_ID,
        long _F_OBJECT_ID,
        long _F_MODIFICATION_ID)
      {
        this.F_CONTEXT_ID = _F_CONTEXT_ID;
        this.F_ID = _F_ID;
        this.F_OBJECT_ID = _F_OBJECT_ID;
        this.F_MODIFICATION_ID = _F_MODIFICATION_ID;
      }

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из строки таблицы
      /// </summary>
      /// <param name="row">Строка таблицы с данными</param>
      public EditingContextsObjectVersion(DataRow row) => this.Assign((object) row);

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из указанного объекта
      /// </summary>
      /// <param name="source">Источник информации</param>
      public EditingContextsObjectVersion(object source) => this.Assign(source);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты равны</returns>
      public override bool Equals(object obj)
      {
        return obj is EditingContextsObjectVersion contextsObjectVersion && this.F_OBJECT_ID == contextsObjectVersion.F_OBJECT_ID;
      }

      /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode() => this.F_OBJECT_ID.GetHashCode();

      /// <summary>Очистить экземпляр класса</summary>
      public void Clear()
      {
        this.F_CONTEXT_ID = 0L;
        this.F_ID = 0L;
        this.F_OBJECT_ID = 0L;
        this.F_MODIFICATION_ID = 0L;
      }

      /// <summary>Скопировать информацию из указанного объекта</summary>
      /// <param name="source">Объект-источник</param>
      public void Assign(object source)
      {
        if (this == source)
          return;
        this.Clear();
        switch (source)
        {
          case DataRow row:
            this.F_CONTEXT_ID = DataSetProcessor.GetInt64Value(row, "F_CONTEXT_ID", 0L);
            this.F_ID = DataSetProcessor.GetInt64Value(row, "F_ID", 0L);
            this.F_OBJECT_ID = DataSetProcessor.GetInt64Value(row, "F_OBJECT_ID", -1L);
            this.F_MODIFICATION_ID = DataSetProcessor.GetInt64Value(row, "F_MODIFICATION_ID", -1L);
            break;
          case EditingContextsObjectVersion contextsObjectVersion:
            this.F_CONTEXT_ID = contextsObjectVersion.F_CONTEXT_ID;
            this.F_ID = contextsObjectVersion.F_ID;
            this.F_OBJECT_ID = contextsObjectVersion.F_OBJECT_ID;
            this.F_MODIFICATION_ID = contextsObjectVersion.F_MODIFICATION_ID;
            break;
        }
      }

      /// <summary>Вернуть точную копию экземпляра класса</summary>
      /// <returns>Точная копия экземпляра класса</returns>
      public object Clone() => Activator.CreateInstance(this.GetType(), (object) this);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(object obj) => this.CompareTo(obj as EditingContextsObjectVersion);

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(EditingContextsObjectVersion other)
      {
        return other == null ? 1 : Math.Abs(this.F_OBJECT_ID).CompareTo(Math.Abs(other.F_OBJECT_ID));
      }

      /// <summary>Сравнить два объекта</summary>
      /// <param name="x">Первый объект</param>
      /// <param name="y">Второй объект</param>
      /// <returns>-1, 0, 1</returns>
      public int Compare(object x, object y)
      {
        return this.Compare(x as EditingContextsObjectVersion, y as EditingContextsObjectVersion);
      }

      /// <summary>Сравнить два объекта</summary>
      /// <param name="x">Первый объект</param>
      /// <param name="y">Второй объект</param>
      /// <returns>-1, 0, 1</returns>
      public int Compare(EditingContextsObjectVersion x, EditingContextsObjectVersion y)
      {
        return x == null || y == null ? 0 : x.CompareTo(y);
      }
    }
}
