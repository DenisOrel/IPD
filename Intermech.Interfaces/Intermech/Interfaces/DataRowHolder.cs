
// Type: Intermech.Interfaces.DataRowHolder
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс, в котором, помимо функционала DataRow, есть такое
    /// полезное поле, как Tag
    /// </summary>
    [DebuggerDisplay("Parsed: {Parsed}; Tag: {Tag}")]
    public sealed class DataRowHolder : IAssignable, ICloneable
    {
      /// <summary>Строка с данными</summary>
      private DataRow _row;
      /// <summary>Дополнительные данные</summary>
      private object _tag;
      /// <summary>Обработана ли информация из строки с данными</summary>
      private bool _parsed;

      /// <summary>Создать пустой экземпляр класса</summary>
      public DataRowHolder()
      {
      }

      /// <summary>Создать заполненный экземпляр класса</summary>
      /// <param name="row">Строка с данными</param>
      /// <param name="tag">Дополнительные данные</param>
      /// <param name="parsed">Обработана ли информация из строки с данными</param>
      public DataRowHolder(DataRow row, object tag, bool parsed)
      {
        this._row = row;
        this._tag = tag;
        this._parsed = parsed;
      }

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из указанного объекта
      /// </summary>
      /// <param name="source">Объект-источник</param>
      public DataRowHolder(object source) => this.Assign(source);

      /// <summary>Строка с данными</summary>
      public DataRow Row
      {
        [DebuggerStepThrough] get => this._row;
        set => this._row = value;
      }

      /// <summary>Вспомогательные данные</summary>
      public object Tag
      {
        [DebuggerStepThrough] get => this._tag;
        set => this._tag = value;
      }

      /// <summary>Обработана ли информация из строки с данными</summary>
      public bool Parsed
      {
        [DebuggerStepThrough] get => this._parsed;
        set => this._parsed = value;
      }

      /// <summary>Очистить поля класса</summary>
      public void Clear()
      {
        this._row = (DataRow) null;
        this._tag = (object) null;
      }

      /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
      /// <param name="source">Объект-источник</param>
      public void Assign(object source)
      {
        if (this == source)
          return;
        this.Clear();
        if (!(source is DataRowHolder dataRowHolder))
          return;
        this._row = dataRowHolder._row;
        this._tag = dataRowHolder._tag;
        this._parsed = dataRowHolder._parsed;
      }

      /// <summary>Создать точную копию экземпляра класса</summary>
      /// <returns>Точная копия экземпляра класса</returns>
      public object Clone() => (object) new DataRowHolder((object) this);
    }
}
