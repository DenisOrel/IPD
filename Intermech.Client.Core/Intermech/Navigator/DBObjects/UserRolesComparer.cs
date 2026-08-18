
// Type: Intermech.Navigator.DBObjects.UserRolesComparer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;


namespace Intermech.Navigator.DBObjects;

internal class UserRolesComparer : IEqualityComparer<UserToRoles>
{
  public bool Equals(UserToRoles x, UserToRoles y)
  {
    if (x == y)
      return true;
    return x != null && y != null && x.ParentID == y.ParentID;
  }

  public int GetHashCode(UserToRoles role) => role == null ? 0 : role.ParentID.GetHashCode();
}
