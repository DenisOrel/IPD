
// Type: Intermech.Interfaces.GlobalId`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Реализует класс именованных глобальных идентификаторов, которые удобно использовать при работе с базой IPS.
    /// </summary>
    /// <typeparam name="T">Тип идентификатора</typeparam>
    [Serializable]
    public class GlobalId<T> : LocalId<T>
    {
      private readonly Guid guid;

      /// <summary>Создает объект.</summary>
      /// <param name="guid">Глобальный идентификатор сущности</param>
      /// <param name="id">Локальный идентификатор сущности</param>
      /// <param name="name">Имя сущности</param>
      public GlobalId(Guid guid, T id, string name)
        : base(id, name)
      {
        this.guid = guid;
      }

      /// <summary>Клонирует объект.</summary>
      /// <returns>Клон объекта</returns>
      public override object Clone() => (object) new GlobalId<T>(this.guid, this.Id, this.Name);

      /// <summary>Возвращает глобальный идентификатор сущности.</summary>
      public Guid Guid => this.guid;
    }
}
