
// Type: Intermech.Interfaces.LocalId`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Реализует класс именованных локальных идентификаторов, которые удобно использовать при работе с базой IPS.
    /// </summary>
    /// <typeparam name="T">Тип идентификатора</typeparam>
    [Serializable]
    public class LocalId<T> : IEquatable<LocalId<T>>, ICloneable
    {
      private readonly T id;
      private readonly string name;

      /// <summary>Создает объект.</summary>
      /// <param name="id">Локальный идентификатор сущности</param>
      /// <param name="name">Имя сущности</param>
      public LocalId(T id, string name)
      {
        if (string.IsNullOrEmpty(name))
          throw new ArgumentException("A identified must be created with a name.", nameof (name));
        this.id = id;
        this.name = name;
      }

      /// <summary>Клонирует объект.</summary>
      /// <returns>Клон объекта</returns>
      public virtual object Clone() => (object) new LocalId<T>(this.id, this.name);

      /// <summary>Возвращает локальный идентификатор сущности.</summary>
      public T Id => this.id;

      /// <summary>Возвращает имя сущности.</summary>
      public string Name => this.name;

      /// <summary>Возвращает хэш-код идентификатора.</summary>
      /// <returns>Значение хэш-кода</returns>
      public override int GetHashCode() => this.id.GetHashCode();

      /// <summary>
      /// Проверяет идентичность содержимого этого объекта и указанного объекта.
      /// </summary>
      /// <param name="other">Другой объект</param>
      /// <returns>true, если содержимое объектов идентично</returns>
      public bool Equals(LocalId<T> other) => other != null && other.id.Equals((object) this.id);

      /// <summary>
      /// Проверяет идентичность содержимого этого объекта и указанного объекта.
      /// </summary>
      /// <param name="obj">Другой объект</param>
      /// <returns>true, если содержимое объектов идентично</returns>
      public override bool Equals(object obj)
      {
        return !(obj is LocalId<T> localId) ? base.Equals(obj) : localId.id.Equals((object) this.id);
      }

      /// <summary>Возвращает текстовое представление объекта.</summary>
      /// <returns>Имя сущности, задаваемой идентификатором</returns>
      public override string ToString() => this.name;
    }
}
