
// Type: Intermech.Interfaces.MyElement
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс для хранения значения элемента и его текстового описания
    /// </summary>
    [Serializable]
    public class MyElement : IAssignable, ICloneable, IComparable<MyElement>
    {
      /// <summary>Значение элемента</summary>
      public object Value;
      /// <summary>Текстовое описание элемента</summary>
      public string Caption = string.Empty;
      /// <summary>Какие-то пользовательские данные</summary>
      public object Tag;

      /// <summary>Создать пустой экземпляр класса</summary>
      public MyElement()
      {
      }

      /// <summary>
      /// Создать экземпляр класса, заполнить его информацией из указанного объекта
      /// </summary>
      /// <param name="source">Объект-источник</param>
      public MyElement(object source) => this.Assign(source);

      /// <summary>Создать заполненный экземпляр класса</summary>
      /// <param name="value">Значение элемента</param>
      /// <param name="caption">Текстовое описание элемента</param>
      /// <param name="tag">Пользовательские данные</param>
      public MyElement(object value, string caption, object tag)
      {
        this.Value = value;
        this.Caption = caption;
        this.Tag = tag;
      }

      /// <summary>Перекрытый метод для возвращения заголовка</summary>
      /// <returns></returns>
      public override string ToString()
      {
        if (this.Caption != null)
          return this.Caption;
        try
        {
          return Convert.ToString(this.Value);
        }
        catch
        {
        }
        return string.Empty;
      }

      /// <summary>Очистить поля класса</summary>
      public virtual void Clear()
      {
        this.Value = (object) 0L;
        this.Caption = string.Empty;
        this.Tag = (object) null;
      }

      /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
      /// <param name="source">Объект-источник</param>
      public virtual void Assign(object source)
      {
        if (this == source)
          return;
        this.Clear();
        if (!(source is MyElement myElement))
          return;
        this.Caption = myElement.Caption;
        this.Value = myElement.Value;
        this.Tag = myElement.Tag;
      }

      /// <summary>Сделать клон объекта</summary>
      /// <returns>Вернёт 100% копию объекта</returns>
      public virtual object Clone() => Activator.CreateInstance(this.GetType(), (object) this);

      /// <summary>Сравнение с объектом класса MyElement по названию.</summary>
      /// <param name="obj">The object.</param>
      /// <returns></returns>
      public int CompareTo(MyElement obj)
      {
        return string.Compare(this.Caption, obj.Caption, StringComparison.Ordinal);
      }

      /// <summary>Сравнить экземпляр класса с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты равны</returns>
      public override bool Equals(object obj)
      {
        if (!(obj is MyElement myElement))
          return base.Equals(obj);
        return this.Caption == myElement.Caption && object.Equals(this.Value, myElement.Value) && object.Equals(this.Tag, myElement.Tag);
      }

      /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode()
      {
        return this.Value == null || this.Tag == null ? this.Caption.GetHashCode() : this.Caption.GetHashCode() << 24 ^ this.Value.GetHashCode() << 8 ^ this.Tag.GetHashCode();
      }
    }
}
