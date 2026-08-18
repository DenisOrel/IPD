
// Type: Intermech.Interfaces.DeletingObjectComparerBaseVersion
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Сравнить два описания удаляемых объектов по их принадлежности к базовым версиям
    /// </summary>
    /// <summary>Создать экземпляр класса</summary>
    /// <param name="ascending">true - сортировать по возрастанию</param>
    public class DeletingObjectComparerBaseVersion(bool ascending) : DeletingObjectComparer(ascending)
    {
      /// <summary>
      /// Сравнить два описания удаляемых объектов по принадлежности к базовым версиям
      /// </summary>
      /// <param name="x">Описание первого удаляемого объекта</param>
      /// <param name="y">Описание второго удаляемого объекта</param>
      /// <returns>Результаты сравнения</returns>
      public override int Compare(DeletingObject x, DeletingObject y)
      {
        return x == null || y == null || (x == null || y == null ? 0 : x.ID.CompareTo(y.ID)) != 0 ? 0 : this.InvertResult(x == null || y == null ? 0 : x.BaseVersion.CompareTo(y.BaseVersion));
      }
    }
}
