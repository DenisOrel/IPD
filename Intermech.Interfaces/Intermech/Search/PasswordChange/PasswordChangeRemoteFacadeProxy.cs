
// Type: Intermech.Search.PasswordChange.PasswordChangeRemoteFacadeProxy
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;


namespace Intermech.Search.PasswordChange
{
    public sealed class PasswordChangeRemoteFacadeProxy : IPasswordChangeRemoteFacade
    {
      public ChangePasswordResult ChangePassword(string oldPassword, string newPassword)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          return ((IPasswordChangeRemoteFacadeServerService) sessionKeeper.Session.GetCustomService(typeof (IPasswordChangeRemoteFacadeServerService))).ChangePassword(sessionKeeper.Session.SessionGUID, oldPassword, newPassword);
      }

      public bool GetPasswordChangeNeed()
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          return ((IPasswordChangeRemoteFacadeServerService) sessionKeeper.Session.GetCustomService(typeof (IPasswordChangeRemoteFacadeServerService))).GetPasswordChangeNeed(sessionKeeper.Session.SessionGUID);
      }
    }
}
