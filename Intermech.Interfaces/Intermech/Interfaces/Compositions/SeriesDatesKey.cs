
// Type: Intermech.Interfaces.Compositions.SeriesDatesKey
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>
    /// Ключ, в котором заданы головное изделие + серия, либо головное изделие + дата
    /// </summary>
    internal class SeriesDatesKey : IComparable<SeriesDatesKey>
    {
      /// <summary>Идентификатор версии головного изделия</summary>
      public long MainArticle;
      /// <summary>Номер серии</summary>
      public int Series;
      /// <summary>Дата изготовления</summary>
      public DateTime Date;

      /// <summary>Создать ключ</summary>
      /// <param name="mainArticle">Идентификатор версии головного изделия</param>
      /// <param name="series">Номер серии</param>
      /// <param name="date">Дата изготовления</param>
      public SeriesDatesKey(long mainArticle = 0, int series = -2147483648 /*0x80000000*/, DateTime date = default (DateTime))
      {
        this.MainArticle = mainArticle;
        this.Series = series;
        this.Date = date;
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true - объекты равны</returns>
      public override bool Equals(object obj) => this.CompareTo(obj as SeriesDatesKey) == 0;

      /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode()
      {
        return Math.Abs(this.MainArticle).GetHashCode() << 16 /*0x10*/ ^ this.Series.GetHashCode() << 8 ^ this.Date.GetHashCode();
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="other">Объект для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(SeriesDatesKey other)
      {
        if (other == null)
          return 1;
        int num1 = Math.Abs(this.MainArticle).CompareTo(Math.Abs(other.MainArticle));
        if (num1 != 0)
          return num1;
        int num2 = this.Series.CompareTo(other.Series);
        return num2 != 0 ? num2 : this.Date.CompareTo(other.Date);
      }
    }
}
