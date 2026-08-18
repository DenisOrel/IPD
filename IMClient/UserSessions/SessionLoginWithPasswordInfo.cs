
// Type: IMClient.UserSessions.SessionLoginWithPasswordInfo




using Intermech.Interfaces.Client;


namespace IMClient.UserSessions
{
    internal sealed class SessionLoginWithPasswordInfo : UserSessionLoginInfo
    {
      private string _userPassword;
      private bool _isValid;

      public SessionLoginWithPasswordInfo()
      {
        this._userPassword = string.Empty;
        this._isValid = false;
      }

      public void SetPassword(string password) => this._userPassword = password;

      internal string UserPassword
      {
        get => this._userPassword;
        set => this._userPassword = value;
      }

      internal bool IsValid
      {
        get => this._isValid;
        set => this._isValid = value;
      }
    }
}
