
// Type: Intermech.Interfaces.IMSAttributeTypeApplicability
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Перечислитель, позволяющий указать, где применяется тип атрибута
    /// </summary>
    [Flags]
    [Serializable]
    public enum IMSAttributeTypeApplicability
    {
      /// <summary>Тип атрибута не применяется нигде</summary>
      None = 0,
      /// <summary>
      /// Тип атрибута применяется как минимум в одном типе объектов
      /// </summary>
      ObjectType = 1,
      /// <summary>
      /// Тип атрибута применяется как минимум в одном типе связей
      /// </summary>
      RelationType = 2,
    }
}
