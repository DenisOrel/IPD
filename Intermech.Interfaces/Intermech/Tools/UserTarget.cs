
// Type: Intermech.Tools.UserTarget
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;


namespace Intermech.Tools
{
    [Serializable]
    public sealed class UserTarget : ITarget
    {
      private long userId;
      private Guid userGuid;

      public UserTarget(long userId, Guid userGuid)
      {
        if (userId == 0L)
          throw new ArgumentException();
        if (userGuid == Guid.Empty)
          throw new ArgumentException();
        this.userId = userId;
        this.userGuid = userGuid;
      }

      public UserTarget(IUserSession session)
      {
        IDBObject dbObject = session != null ? session.GetObject(session.UserID, true) : throw new ArgumentNullException();
        this.userId = session.UserID;
        this.userGuid = dbObject.ObjectGUID;
      }

      public long UserId => this.userId;

      public Guid UserGuid => this.userGuid;

      public override int GetHashCode() => this.userGuid.GetHashCode();

      public override bool Equals(object obj)
      {
        return !(obj is UserTarget userTarget) ? base.Equals(obj) : userTarget.userGuid == this.userGuid;
      }
    }
}
