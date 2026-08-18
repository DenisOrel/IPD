
// Type: Intermech.Interfaces.MyCompositeKey
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;


namespace Intermech.Interfaces
{
    /// <summary>Класс для хранения составного ключа в коллекциях</summary>
    [Serializable]
    public sealed class MyCompositeKey : ICloneable, IComparable
    {
      /// <summary>Коллекция ключей</summary>
      public ArrayList Keys = new ArrayList(1);

      /// <summary>Создать пустой экземпляр класса</summary>
      public MyCompositeKey()
      {
      }

      /// <summary>Создать заполненный экземпляр класса</summary>
      /// <param name="AKeys">Массив ключей</param>
      public MyCompositeKey(params object[] AKeys)
      {
        this.Keys.Clear();
        if (AKeys == null || AKeys.Length == 0)
          return;
        for (int index = 0; index < AKeys.Length; ++index)
          this.Keys.Add(AKeys[index]);
      }

      /// <summary>Очистка полей</summary>
      public void Clear() => this.Keys.Clear();

      /// <summary>
      /// Сравнить текущий экземпляр класса с указанным объектом
      /// </summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если объекты равны</returns>
      public override bool Equals(object obj)
      {
        if (obj == null)
          return false;
        if (!(obj is MyCompositeKey myCompositeKey))
          return base.Equals(obj);
        int count = this.Keys.Count;
        if (count != myCompositeKey.Keys.Count)
          return false;
        if (count == 0)
          return true;
        for (int index = 0; index < count; ++index)
        {
          if (!this.Keys[index].Equals(myCompositeKey.Keys[index]))
            return false;
        }
        return true;
      }

      /// <summary>Вернуть 32-битный хэш-код</summary>
      /// <returns>32-битный хэш-код</returns>
      public override int GetHashCode()
      {
        int hashCode1 = base.GetHashCode();
        int count = this.Keys.Count;
        if (count == 0)
          return hashCode1;
        int hashCode2 = 0;
        for (int index = 0; index < count; ++index)
          hashCode2 ^= this.Keys[index].GetHashCode();
        return hashCode2;
      }

      /// <summary>Сделать клон объекта</summary>
      /// <returns>Вернёт 100% копию объекта</returns>
      public object Clone()
      {
        object[] objArray = (object[]) null;
        if (this.Keys.Count > 0)
        {
          objArray = new object[this.Keys.Count];
          this.Keys.CopyTo((Array) objArray);
        }
        return (object) new MyCompositeKey(objArray);
      }

      /// <summary>Выполнить сравнение с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>-1 - меньше, чем obj, 0 - равны, 1 - больше, чем obj</returns>
      public int CompareTo(object obj) => this.Equals(obj) ? 0 : -1;
    }
}
