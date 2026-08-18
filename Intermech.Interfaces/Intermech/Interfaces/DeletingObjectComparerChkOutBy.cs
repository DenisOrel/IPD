
// Type: Intermech.Interfaces.DeletingObjectComparerChkOutBy
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Сравнить два описания удаляемых объектов по пользователям, взявшим объекты на редактирование
    /// </summary>
    public class DeletingObjectComparerChkOutBy : DeletingObjectComparer
    {
      /// <summary>
      /// Кэш имён пользователей
      /// [(Int64)Идентификатор пользователя] = [(object)Имя пользователя (string)]
      /// </summary>
      private Dictionary<long, object> users;

      /// <summary>Создать экземпляр класса</summary>
      /// <param name="ascending">true - сортировать по возрастанию</param>
      /// <param name="users">
      /// Кэш имён пользователей
      /// [(Int64)Идентификатор пользователя] = [(object)Имя пользователя (string)]
      /// </param>
      public DeletingObjectComparerChkOutBy(bool ascending, Dictionary<long, object> users)
        : base(ascending)
      {
        this.users = users;
      }

      /// <summary>
      /// Сравнить два описания удаляемых объектов по пользователям, взявшим объекты на редактирование
      /// </summary>
      /// <param name="x">Описание первого удаляемого объекта</param>
      /// <param name="y">Описание второго удаляемого объекта</param>
      /// <returns>Результаты сравнения</returns>
      public override int Compare(DeletingObject x, DeletingObject y)
      {
        object obj1 = x == null || this.users == null || !this.users.ContainsKey(x.ChkOutByID) ? (object) string.Empty : this.users[x.ChkOutByID];
        object obj2 = y == null || this.users == null || !this.users.ContainsKey(y.ChkOutByID) ? (object) string.Empty : this.users[y.ChkOutByID];
        int num = obj1.ToString().CompareTo(obj2.ToString());
        if (num == 0)
          num = x.Caption.CompareTo(y.Caption);
        return this.InvertResult(num);
      }
    }
}
