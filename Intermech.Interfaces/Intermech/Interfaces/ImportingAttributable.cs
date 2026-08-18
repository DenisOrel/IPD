
// Type: Intermech.Interfaces.ImportingAttributable
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    [Serializable]
    public class ImportingAttributable
    {
      /// <summary>Ее атрибуты</summary>
      public List<AttributeRecord> Attributes;

      public ImportingAttributable() => this.Attributes = new List<AttributeRecord>();

      /// <summary>Добавить атрибут</summary>
      /// <param name="attribute"></param>
      public void AddAttribute(AttributeRecord attribute)
      {
        if (this.Attributes.Find((Predicate<AttributeRecord>) (x => x.AttributeId == attribute.AttributeId && x.InlistId == attribute.InlistId)) != null)
          return;
        this.Attributes.Add(attribute);
      }
    }
}
