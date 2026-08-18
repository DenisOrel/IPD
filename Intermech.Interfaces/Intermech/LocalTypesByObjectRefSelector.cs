
// Type: Intermech.LocalTypesByObjectRefSelector
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech
{
    /// <summary>
    /// Указывает, что нужно выбирать объекты, у которых в атрибуте AttributeID есть ссылка на объекты из списка ObjectIDs
    /// </summary>
    [Serializable]
    public class LocalTypesByObjectRefSelector : LocalTypesSelector
    {
      private int _attributeID;
      private List<long> _objectIDs = new List<long>();

      public int AttributeID => this._attributeID;

      public List<long> ObjectIDs => this._objectIDs;

      public LocalTypesByObjectRefSelector(int AttributeID, long ObjectID)
      {
        this._attributeID = AttributeID;
        this._objectIDs.Add(ObjectID);
      }

      public LocalTypesByObjectRefSelector(int AttributeID, List<long> ObjectIDs)
      {
        this._attributeID = AttributeID;
        this._objectIDs = ObjectIDs;
      }
    }
}
