
// Type: Intermech.Interfaces.ObjectVersionDescriptionOptions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Дополнительная информация о версии объекта из контекста редактирования
    /// </summary>
    [Flags]
    [Serializable]
    public enum ObjectVersionDescriptionOptions : long
    {
      /// <summary>
      /// Нет никакой дополнительной информации о версии объекта из контекста редактирования
      /// </summary>
      None = 0,
      /// <summary>
      /// Данное описание принадлежит непосредственно контексту редактирования, а не объекту из контекста
      /// </summary>
      IsContext = 1,
      /// <summary>
      /// Данное описание принадлежит непосредственно контексту редактирования, а не объекту из контекста,
      /// причём контекст является извещением
      /// </summary>
      IsECO = 2,
      /// <summary>
      /// Данное описание принадлежит версии объекта, которая входит в состав извещения
      /// </summary>
      FromECOComposition = 16, // 0x0000000000000010
      /// <summary>Описание неполное или некорректное</summary>
      InvalidDescription = 2305843009213693952, // 0x2000000000000000
    }
}
