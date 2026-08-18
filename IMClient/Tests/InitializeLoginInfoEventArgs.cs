
// Type: IMClient.Tests.InitializeLoginInfoEventArgs




using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Diagnostics;


namespace IMClient.Tests
{
    internal sealed class InitializeLoginInfoEventArgs : EventArgs
    {
      private IMServer server;
      private IUserSession session;
      private UserSessionLoginInfo loginInfo;
      private string password;

      public InitializeLoginInfoEventArgs(
        IMServer server,
        IUserSession session,
        UserSessionLoginInfo loginInfo)
      {
        if (server == null)
          throw new ArgumentNullException(nameof (server));
        if (session == null)
          throw new ArgumentNullException(nameof (session));
        if (loginInfo == null)
          throw new ArgumentNullException(nameof (loginInfo));
        this.server = server;
        this.session = session;
        this.loginInfo = loginInfo;
      }

      public IMServer Server
      {
        [DebuggerStepThrough] get => this.server;
      }

      public IUserSession Session
      {
        [DebuggerStepThrough] get => this.session;
      }

      public UserSessionLoginInfo LoginInfo
      {
        [DebuggerStepThrough] get => this.loginInfo;
      }

      public string Password
      {
        [DebuggerStepThrough] get => this.password;
        [DebuggerStepThrough] set => this.password = value;
      }
    }
}
