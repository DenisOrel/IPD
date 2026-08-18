
// Type: Intermech.Interfaces.QuickObjectInfoHolder
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Обёртка для хранения описаний объектов</summary>
    public class QuickObjectInfoHolder : IComparable<QuickObjectInfoHolder>
    {
      /// <summary>Краткое описание объекта</summary>
      public QuickObjectInfo Value;

      /// <summary>Создать обёртку для краткого описания объекта</summary>
      /// <param name="value">Краткое описание объекта</param>
      public QuickObjectInfoHolder(QuickObjectInfo value) => this.Value = value;

      /// <summary>Выполнить сравнение</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns></returns>
      public override bool Equals(object obj) => this.CompareTo(obj as QuickObjectInfoHolder) == 0;

      /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode() => this.Value.ObjectID.GetHashCode();

      /// <summary>Получить строковое представление экземпляра класса</summary>
      /// <returns>Строковое представление экземпляра класса</returns>
      public override string ToString() => this.Value.Caption;

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(QuickObjectInfoHolder other)
      {
        return other == null ? -1 : this.Value.ObjectID.CompareTo(other.Value.ObjectID);
      }
    }
}
