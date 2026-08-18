
// Type: Intermech.Interfaces.ChangingObjectComparerObjectType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Сравнить два описания изменяемых объектов по типам их объектов
    /// </summary>
    public class ChangingObjectComparerObjectType : ChangingObjectComparer
    {
      /// <summary>
      /// Кэш имён пользователей
      /// [(Int64)Идентификатор пользователя] = [(object)Имя пользователя (string)]
      /// </summary>
      private Dictionary<long, object> users;
      /// <summary>
      /// Кэш названий типов объектов
      /// [(Int32)Идентификатор типа объекта] = [(object)Имя типа объекта (string)]
      /// </summary>
      private Dictionary<int, object> types;

      /// <summary>Создать экземпляр класса</summary>
      /// <param name="ascending">true - сортировать по возрастанию</param>
      /// <param name="users">
      /// Кэш имён пользователей
      /// [(Int64)Идентификатор пользователя] = [(object)Имя пользователя (string)]
      /// </param>
      /// <param name="types">
      /// Кэш названий типов объектов
      /// [(Int32)Идентификатор типа объекта] = [(object)Имя типа объекта (string)]
      /// </param>
      public ChangingObjectComparerObjectType(
        bool ascending,
        Dictionary<long, object> users,
        Dictionary<int, object> types)
        : base(ascending)
      {
        this.users = users;
        this.types = types;
      }

      /// <summary>
      /// Сравнить два описания изменяемых объектов по типам их объектов
      /// </summary>
      /// <param name="x">Описание первого изменяемого объекта</param>
      /// <param name="y">Описание второго изменяемого объекта</param>
      /// <returns>Результаты сравнения</returns>
      public override int Compare(ChangingObject x, ChangingObject y)
      {
        object obj1 = x == null || this.types == null || !this.types.ContainsKey(x.ObjectType) ? (object) string.Empty : this.types[x.ObjectType];
        object obj2 = y == null || this.types == null || !this.types.ContainsKey(y.ObjectType) ? (object) string.Empty : this.types[y.ObjectType];
        int num = obj1.ToString().CompareTo(obj2.ToString());
        if (num == 0)
        {
          object obj3 = x == null || this.users == null || !this.users.ContainsKey(x.OwnerID) ? (object) string.Empty : this.users[x.OwnerID];
          object obj4 = y == null || this.users == null || !this.users.ContainsKey(y.OwnerID) ? (object) string.Empty : this.users[y.OwnerID];
          num = obj3.ToString().CompareTo(obj4.ToString());
        }
        if (num == 0)
          num = x.Caption.CompareTo(y.Caption);
        return this.InvertResult(num);
      }
    }
}
