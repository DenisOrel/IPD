
// Type: Intermech.Interfaces.RoleProperties
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Класс, содержащий свойства роли</summary>
    [Serializable]
    public class RoleProperties : IComparable
    {
      public long RoleID;
      private string _roleName;

      public string RoleName
      {
        get => this._roleName;
        set => this._roleName = value;
      }

      public RoleProperties(long roleID, string roleName)
      {
        this.RoleID = roleID;
        this._roleName = roleName;
      }

      public override string ToString() => this._roleName;

      public int CompareTo(object obj) => this._roleName.CompareTo(((RoleProperties) obj)._roleName);
    }
}
