
// Type: Intermech.Interfaces.UserSessionExtensions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using System;


namespace Intermech.Interfaces
{
    public static class UserSessionExtensions
    {
      [NotNull]
      [MustUseReturnValue]
      public static ISessionGuarantee Guarantee([CanBeNull] this IUserSession maybeSession)
      {
        return maybeSession == null ? (ISessionGuarantee) new UserSessionExtensions.Keeper() : (ISessionGuarantee) new UserSessionExtensions.SessionContainer(maybeSession);
      }

      private sealed class SessionContainer : ISessionGuarantee, IDBSessionable, IDisposable
      {
        public SessionContainer([NotNull] IUserSession session) => this.Session = session;

        public IUserSession Session { get; private set; }

        public void Dispose() => this.Session = (IUserSession) null;
      }

      private sealed class Keeper : ISessionGuarantee, IDBSessionable, IDisposable
      {
        [NotNull]
        private readonly SessionKeeper _sk;

        public Keeper() => this._sk = new SessionKeeper();

        public IUserSession Session => this._sk.Session;

        public void Dispose() => this._sk.Dispose();
      }
    }
}
