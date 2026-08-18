
// Type: Intermech.Interfaces.RolePropertiesComparer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;


namespace Intermech.Interfaces
{
    public class RolePropertiesComparer : IEqualityComparer<RoleProperties>
    {
      public bool Equals(RoleProperties obj1, RoleProperties obj2)
      {
        if (obj1 == null && obj2 == null)
          return true;
        return obj1 != null && obj2 != null && obj1.RoleName.Equals(obj2.RoleName);
      }

      public int GetHashCode(RoleProperties obj)
      {
        return ((int) obj.RoleID ^ obj.RoleName.Length).GetHashCode();
      }
    }
}
