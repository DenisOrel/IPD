
// Type: Intermech.Interfaces.ChangingObjectComparerCaption
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Сравнить два описания изменяемых объектов по их заголовкам
    /// </summary>
    /// <summary>Создать экземпляр класса</summary>
    /// <param name="ascending">true - сортировать по возрастанию</param>
    public class ChangingObjectComparerCaption(bool ascending) : ChangingObjectComparer(ascending)
    {
      /// <summary>
      /// Сравнить два описания изменяемых объектов по заголовкам
      /// </summary>
      /// <param name="x">Описание первого изменяемого объекта</param>
      /// <param name="y">Описание второго изменяемого объекта</param>
      /// <returns>Результаты сравнения</returns>
      public override int Compare(ChangingObject x, ChangingObject y)
      {
        return this.InvertResult(x == null || y == null ? 0 : x.Caption.CompareTo(y.Caption));
      }
    }
}
