
// Type: Intermech.Interfaces.WebPortal.AttributePossibleValues
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Допустимые значения атрибута</summary>
    [Serializable]
    public class AttributePossibleValues
    {
      public AttributePossibleValues()
      {
      }

      public AttributePossibleValues(int attributeID, PossibleValue[] possibleValues)
      {
        this.AttributeID = attributeID;
        this.PossibleValues = possibleValues;
      }

      /// <summary>Идентификатор атрибута</summary>
      public int AttributeID { get; }

      /// <summary>Допустимые значения</summary>
      public PossibleValue[] PossibleValues { get; }
    }
}
