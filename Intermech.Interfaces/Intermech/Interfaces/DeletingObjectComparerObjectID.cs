
// Type: Intermech.Interfaces.DeletingObjectComparerObjectID
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Сравнить два описания удаляемых объектов по идентификаторов версий объектов
    /// </summary>
    /// <summary>Создать экземпляр класса</summary>
    /// <param name="ascending">true - сортировать по возрастанию</param>
    public class DeletingObjectComparerObjectID(bool ascending) : DeletingObjectComparer(ascending)
    {
      /// <summary>
      /// Сравнить два описания удаляемых объектов по идентификаторам версий объектов
      /// </summary>
      /// <param name="x">Описание первого удаляемого объекта</param>
      /// <param name="y">Описание второго удаляемого объекта</param>
      /// <returns>Результаты сравнения</returns>
      public override int Compare(DeletingObject x, DeletingObject y)
      {
        return this.InvertResult(x == null || y == null ? 0 : x.ObjectID.CompareTo(y.ObjectID));
      }
    }
}
