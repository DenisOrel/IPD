
// Type: Intermech.Interfaces.Compositions.MatrixKey
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Sets;
using System;
using System.Diagnostics;


namespace Intermech.Interfaces.Compositions
{
    /// <summary>
    /// Ключ для матрицы диапазонов серий и дат изготовления всех версий указанного объекта для набора головных изделий
    /// [Головное изделие, Идентификатор версии объекта, Признак применяемости]
    /// </summary>
    [DebuggerDisplay("{Text}")]
    /// <summary>Создать ключ для значения в матрице</summary>
    /// <param name="mainArticleID">Идентификатор версии головного изделия</param>
    /// <param name="objectID">Идентификатор версии объекта</param>
    /// <param name="appl">Признак применяемости</param>
    public sealed class MatrixKey(long mainArticleID, long objectID, ApplicabilityBy appl) : 
      Tuple<long, long, ApplicabilityBy>(Math.Abs(mainArticleID), Math.Abs(objectID), appl),
      IComparable<MatrixKey>,
      IComparable<Tuple<long, long, ApplicabilityBy>>,
      Intermech.Interfaces.IDisplayable
    {
      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true - объекты равны</returns>
      public override bool Equals(object obj) => this.CompareTo(obj as MatrixKey) == 0;

      /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode()
      {
        long num1 = this.Item1;
        int num2 = num1.GetHashCode() << 17;
        num1 = this.Item2;
        int num3 = num1.GetHashCode() << 2;
        return num2 ^ num3 ^ this.Item3.GetHashCode();
      }

      /// <summary>Вернуть строковое представление экземпляра класса</summary>
      /// <returns>Строковое представление экземпляра класса</returns>
      public override string ToString() => this.Text;

      /// <summary>Сравнить с другим ключом</summary>
      /// <param name="other">Ключ для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(MatrixKey other)
      {
        if (other == null)
          return 1;
        int num1 = Math.Abs(this.Item1).CompareTo(Math.Abs(other.Item1));
        if (num1 != 0)
          return num1;
        int num2 = Math.Abs(this.Item2).CompareTo(Math.Abs(other.Item2));
        return num2 != 0 ? num2 : this.Item3.CompareTo((object) other.Item3);
      }

      /// <summary>Сравнить с другим ключом</summary>
      /// <param name="other">Ключ для сравнения</param>
      /// <returns>-1, 0, 1</returns>
      public int CompareTo(Tuple<long, long, ApplicabilityBy> other)
      {
        return this.CompareTo(other as MatrixKey);
      }

      /// <summary>Текст для отображения на экране</summary>
      public string Text
      {
        get
        {
          return !(ApplicationServices.Container.GetService(typeof (IObjectsInfoCache)) is IObjectsInfoCache service) ? base.ToString() : string.Format("[{2}] '{0}' => '{1}'", (object) service.GetObjectCaption(this.Item1), (object) service.GetObjectCaption(this.Item2), (object) this.Item3);
        }
      }
    }
}
