
// Type: Intermech.Interfaces.ApplicabilitiesKey
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Ключ для проверки применяемостей</summary>
    public class ApplicabilitiesKey : ICloneable
    {
      /// <summary>Тип родительского объекта</summary>
      public int ParType { get; internal set; }

      /// <summary>Тип дочернего объекта</summary>
      public int ChildType { get; internal set; }

      /// <summary>Тип связи</summary>
      public int RelType { get; internal set; }

      /// <summary>Создать экземпляр класса</summary>
      /// <param name="parType">Тип родительского объекта</param>
      /// <param name="childType">Тип дочернего объекта</param>
      /// <param name="relType">Тип связи</param>
      public ApplicabilitiesKey(int parType, int childType, int relType)
      {
        this.ParType = parType;
        this.ChildType = childType;
        this.RelType = relType;
      }

      /// <summary>Рассчитать 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode() => this.ParType << 24 ^ this.ChildType << 8 ^ this.RelType;

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true - объекты равны</returns>
      public override bool Equals(object obj)
      {
        return obj is ApplicabilitiesKey applicabilitiesKey && this.ParType == applicabilitiesKey.ParType && this.ChildType == applicabilitiesKey.ChildType && this.RelType == applicabilitiesKey.RelType;
      }

      /// <summary>Создать точную копию экземпляра класса</summary>
      /// <returns>Точная копия экземпляра класса</returns>
      public object Clone()
      {
        return (object) new ApplicabilitiesKey(this.ParType, this.ChildType, this.RelType);
      }
    }
}
