
// Type: Intermech.LocalTypesByObjectIDsSelector
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech
{
    /// <summary>
    /// Указывает, что нужно выбирать среди типов перечисленных объектов
    /// </summary>
    [Serializable]
    public class LocalTypesByObjectIDsSelector : LocalTypesSelector
    {
      private long[] _objectIDs;

      public long[] ObjectIDs => this._objectIDs;

      public LocalTypesByObjectIDsSelector(long[] ObjectIDs) => this._objectIDs = ObjectIDs;
    }
}
