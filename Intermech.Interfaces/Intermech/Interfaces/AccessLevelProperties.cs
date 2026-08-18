
// Type: Intermech.Interfaces.AccessLevelProperties
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Класс, содержащий свойства роли</summary>
    [Serializable]
    public class AccessLevelProperties : IComparable
    {
      public long AccessLevelID;
      private string _AccessLevelName;

      public string AccessLevelName
      {
        get => this._AccessLevelName;
        set => this._AccessLevelName = value;
      }

      public AccessLevelProperties(long accessLevelID, string accessLevelName)
      {
        this.AccessLevelID = accessLevelID;
        this._AccessLevelName = accessLevelName;
      }

      public override string ToString() => this._AccessLevelName;

      public int CompareTo(object obj)
      {
        return this._AccessLevelName.CompareTo(((AccessLevelProperties) obj)._AccessLevelName);
      }
    }
}
