
// Type: IMClient.UserSessions.LoginHelper




using Intermech;
using Intermech.ApplicationModel;
using Intermech.Client.Core;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Configuration;
using Intermech.Net;
using Intermech.Protection;
using Intermech.Remoting.Sponsors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;


namespace IMClient.UserSessions
{
    internal sealed class LoginHelper
    {
      private IApplicationEventLogService eventLogService;
      private IConfigurationManager loginConfigurationManager;

      public LoginHelper(
        IApplicationEventLogService eventLogService = null,
        IConfigurationManager loginConfigurationManager = null)
      {
        this.eventLogService = eventLogService;
        this.loginConfigurationManager = loginConfigurationManager;
      }

      public bool GetPassword(
        SessionLoginWithPasswordInfo loginInfo,
        string[] servers,
        IMServer server,
        IUserSession session)
      {
        if (loginInfo.IsValid)
        {
          try
          {
            session.Login(loginInfo.LoginName, new PswPackage(loginInfo.UserPassword, server.CryptMethod), SystemInformation.ComputerName, this.CalcTimeZoneOffset(), loginInfo.RoleId, loginInfo.AccessLevel);
          }
          catch (PasswordExpiredException ex)
          {
            int num = (int) MessageBox.Show("Срок действия вашего пароля истёк.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            string newPassword;
            if (ChangeCurrentUserPasswordForm.Execute(loginInfo.UserPassword, true, out newPassword) != DialogResult.OK)
              throw ex;
            char cryptMethod = server.CryptMethod;
            PswPackage pswPackage = new PswPackage(newPassword, cryptMethod);
            CryptHelper.ValidatePswRules(session, newPassword, pswPackage.GetHash(cryptMethod), session.UserID);
            session.NewPassword = pswPackage;
            session.Login(loginInfo.LoginName, new PswPackage(loginInfo.UserPassword, server.CryptMethod), SystemInformation.ComputerName, this.CalcTimeZoneOffset(), loginInfo.RoleId, loginInfo.AccessLevel);
            loginInfo.UserPassword = newPassword;
          }
          return true;
        }
        ActingUserInfo actingUserInfo = new ActingUserHelper().TryGetActingUserInfo();
        bool flag1 = actingUserInfo != null;
        if (server.LoginMode != IMServerLoginMode.Normal && !flag1)
        {
          long aRoleID = -1;
          int accessLevel = loginInfo.AccessLevel;
          string str = string.Empty;
          IMServerLoginMode loginMode = server.LoginMode;
          string userName;
          switch (loginMode)
          {
            case IMServerLoginMode.DomainLogin:
            case IMServerLoginMode.DomainOnlyLogin:
              userName = WindowsIdentity.GetCurrent().User.Value;
              break;
            default:
              userName = SystemInformation.UserName;
              break;
          }
          LoginInformation loginInformation = session.GetLoginInformation(userName);
          RoleProperties[] roles = loginInformation.Roles;
          Dictionary<int, string> accessLevels = loginInformation.AccessLevels;
          if (roles != null && roles.Length != 0)
          {
            if (roles.Length == 1 && accessLevels != null && accessLevels.Count == 1)
            {
              aRoleID = roles[0].RoleID;
              str = roles[0].RoleName;
              using (Dictionary<int, string>.Enumerator enumerator = accessLevels.GetEnumerator())
              {
                if (enumerator.MoveNext())
                  accessLevel = enumerator.Current.Key;
              }
            }
            else
            {
              aRoleID = SelectRoleForm.SelectRole(roles, accessLevels, ref accessLevel);
              if (aRoleID != -1L)
              {
                foreach (RoleProperties roleProperties in roles)
                {
                  if (aRoleID == roleProperties.RoleID)
                    str = roleProperties.RoleName;
                }
              }
              else if (loginMode == IMServerLoginMode.DomainOnlyLogin)
                return false;
            }
          }
          try
          {
            if (loginMode == IMServerLoginMode.WindowsLogin)
              loginInfo.LoginName = SystemInformation.UserName;
            else
              loginInfo.LoginName = WindowsIdentity.GetCurrent().User.Value;
            session.Login(loginInfo.LoginName, new PswPackage("WindowsLoginMode", server.CryptMethod), SystemInformation.ComputerName, this.CalcTimeZoneOffset(), aRoleID, accessLevel);
            loginInfo.UserName = session.UserName;
            loginInfo.UserPassword = "WindowsLoginMode";
            loginInfo.RoleId = aRoleID;
            loginInfo.AccessLevel = accessLevel;
            loginInfo.RoleName = str;
            loginInfo.IsValid = true;
            return true;
          }
          catch (Exception ex)
          {
            if (loginMode == IMServerLoginMode.DomainOnlyLogin)
              throw new DomainOnlyLoginException(ex);
          }
        }
        LoginForm loginForm = new LoginForm(servers, session);
        loginForm.LoginConfigurationManager = this.loginConfigurationManager;
        for (int index1 = 0; index1 < 3; ++index1)
        {
          while (true)
          {
            switch (loginForm.ShowDialog())
            {
              case DialogResult.OK:
                if (loginForm.RoleID == -1L)
                {
                  int num = (int) MessageBox.Show("Роль не выбрана.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                  continue;
                }
                goto label_36;
              case DialogResult.Cancel:
                goto label_33;
              default:
                goto label_82;
            }
          }
    label_33:
          return false;
    label_36:
          try
          {
            try
            {
              loginInfo.RoleId = loginForm.RoleID;
              loginInfo.AccessLevel = loginForm.AccessLevel;
              loginInfo.LoginName = loginForm.UserName;
              if (loginForm.ActingUserMode)
              {
                session.LoginAsActingUser(new ActingUserLoginParameters(loginForm.UserID, loginForm.RoleID, loginInfo.AccessLevel, actingUserInfo));
                loginInfo.ActingUserName = session.ActingUserName;
              }
              else
              {
                loginInfo.LoginName = loginForm.UserName;
                if (this.loginConfigurationManager != null)
                {
                  IConfiguration configuration = this.loginConfigurationManager.Open("Logging") ?? this.loginConfigurationManager.Create("Logging");
                  configuration.SetProperty("UserName", loginForm.UserName);
                  configuration.SetProperty("RoleName", loginForm.RoleName);
                  configuration.SetProperty("AccessLevel", loginForm.AccessLevel.ToString());
                  configuration.SetProperty("Location", (string) TypeDescriptor.GetConverter(typeof (Point)).ConvertTo((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) loginForm.Location, typeof (string)));
                }
                bool flag2 = true;
                session.Login(loginForm.UserName, new PswPackage(loginForm.Password, server.CryptMethod), SystemInformation.ComputerName, this.CalcTimeZoneOffset(), loginForm.RoleID, loginForm.AccessLevel);
                IDBObject dbObject = session.GetObject(session.UserID);
                IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cadd9558-306c-11d8-b4e9-00304f19f545"), false);
                using (new RemoteLock(new object[2]
                {
                  (object) dbObject,
                  (object) attributeByGuid
                }))
                {
                  if (attributeByGuid != null)
                  {
                    if (attributeByGuid.AsBoolean)
                    {
                      string newPassword;
                      if (ChangeCurrentUserPasswordForm.Execute(loginForm.Password, false, out newPassword) != DialogResult.OK)
                      {
                        ProtectionService.Stop();
                        Process.GetCurrentProcess().Kill();
                      }
                      IDBAttribute attributeById = dbObject.GetAttributeByID(session.IdentHelper.PasswordID);
                      if (attributeById != null)
                        attributeById.AsString = newPassword;
                      attributeByGuid.AsBoolean = false;
                      flag2 = false;
                    }
                  }
                }
                if (flag2)
                {
                  int expirationDays = session.GetExpirationDays();
                  if (expirationDays > 0)
                  {
                    if (expirationDays < 6)
                    {
                      string str1 = $"Срок действия вашего пароля истекает через {expirationDays} ";
                      string empty = string.Empty;
                      string str2;
                      switch (expirationDays)
                      {
                        case 1:
                          str2 = "день";
                          break;
                        case 2:
                        case 3:
                        case 4:
                          str2 = "дня";
                          break;
                        default:
                          str2 = "дней";
                          break;
                      }
                      string message = str1 + str2;
                      if (MessageBox.Show(message + ". Сменить пароль?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        throw new PasswordExpiredException(message, false);
                    }
                  }
                }
              }
            }
            catch (PasswordExpiredException ex)
            {
              if (ex.ShowDialog)
              {
                int num = (int) MessageBox.Show(ex.Message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
              }
              if (!loginForm.ActingUserMode)
              {
                string newPassword;
                if (ChangeCurrentUserPasswordForm.Execute(loginForm.Password, true, out newPassword) != DialogResult.OK)
                  throw ex;
                char cryptMethod = server.CryptMethod;
                PswPackage pswPackage = new PswPackage(newPassword, cryptMethod);
                CryptHelper.ValidatePswRules(session, newPassword, pswPackage.GetHash(cryptMethod), session.UserID);
                session.NewPassword = pswPackage;
                session.Login(loginForm.UserName, new PswPackage(loginForm.Password, server.CryptMethod), SystemInformation.ComputerName, this.CalcTimeZoneOffset(), loginForm.RoleID, loginForm.AccessLevel);
                loginForm.Password = newPassword;
              }
            }
            loginInfo.LoginName = loginForm.UserName;
            loginInfo.UserName = session.UserName;
            loginInfo.UserPassword = loginForm.Password;
            loginInfo.RoleId = loginForm.RoleID;
            loginInfo.AccessLevel = loginForm.AccessLevel;
            loginInfo.RoleName = loginForm.RoleName;
            loginInfo.IsValid = true;
            ClientTimeDelay instantClientTimeDelay = TimePatrol.GetInstantClientTimeDelay(session);
            if (instantClientTimeDelay.Value.Duration() > TimePatrol.MinimalLimit)
            {
              try
              {
                TimePatrol.SetSystemTime(DateTime.UtcNow + TimePatrol.GetMeanClientTimeDelay(session).Value);
              }
              catch (Win32Exception ex)
              {
                if (this.eventLogService != null)
                {
                  StringBuilder stringBuilder = new StringBuilder(512 /*0x0200*/);
                  stringBuilder.AppendLine($"Разница времени на сервере и клиенте превышает установленный предел в {TimePatrol.MinimalLimit.TotalMilliseconds:0.0}мс. Результаты замера: {instantClientTimeDelay.ToMillisecondsText()}.");
                  stringBuilder.AppendLine($"Выполнить синхронизацию системного времени клиента с временем сервера приложений не удалось из-за ошибки (код: 0x{ex.NativeErrorCode:X8}, {ex.Message}).");
                  this.eventLogService.DefaultLog.Write(stringBuilder.ToString(), EventLogItemType.Warning);
                }
              }
            }
            if (!loginForm.ActingUserMode && this.loginConfigurationManager != null)
            {
              IConfiguration configuration = this.loginConfigurationManager.Create("Logging");
              configuration.SetProperty("UserName", loginForm.UserName);
              configuration.SetProperty("RoleName", loginForm.RoleName);
              configuration.SetProperty("AccessLevel", loginForm.AccessLevel.ToString());
              configuration.SetProperty("Location", (string) TypeDescriptor.GetConverter(typeof (Point)).ConvertTo((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) loginForm.Location, typeof (string)));
            }
            return true;
          }
          catch (KernelException ex)
          {
            switch (ex)
            {
              case InvalidLoginInfoException _:
                if (MessageBox.Show(ex.Message, "Ошибка подключения к серверу приложений IPS.", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand) != DialogResult.OK)
                  return false;
                continue;
              case AccessDeniedException _:
                AccessDeniedException accessDeniedException = ex as AccessDeniedException;
                StringBuilder stringBuilder = new StringBuilder($"{ex.Message}:{Environment.NewLine}");
                for (int index2 = 0; index2 < accessDeniedException.LogList.Length && accessDeniedException.LogList[accessDeniedException.LogList.Length - index2 - 1] != "------------------------------------"; ++index2)
                  stringBuilder.Append(accessDeniedException.LogList[accessDeniedException.LogList.Length - index2 - 1] + Environment.NewLine);
                int num1 = (int) MessageBox.Show(stringBuilder.ToString(), "Ошибка подключения к серверу приложений IPS.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                continue;
              default:
                int num2 = (int) MessageBox.Show(ex.Message, "Ошибка подключения к серверу приложений IPS.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                --index1;
                continue;
            }
          }
    label_82:;
        }
        return false;
      }

      private TimeSpan CalcTimeZoneOffset() => TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
    }
}
