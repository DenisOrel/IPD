
// Type: Intermech.Tools.UserSecurityData
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Tools
{
    [Serializable]
    public sealed class UserSecurityData
    {
      private long userId;
      private ToolSecurityGroup securityGroup;

      public UserSecurityData(long userId, ToolSecurityGroup securityGroup)
      {
        this.userId = userId != 0L ? userId : throw new ArgumentException();
        this.securityGroup = securityGroup;
      }

      public long UserId => this.userId;

      public ToolSecurityGroup SecurityGroup => this.securityGroup;
    }
}
