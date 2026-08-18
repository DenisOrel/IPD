
// Type: Intermech.Client.Specialized.SimpleSessionPool
// Assembly: Intermech.Client.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C6CEDFE2-45F7-4A85-9CFB-4D0105C0197F
:\IPS\Client\Intermech.Client.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Base.xml

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Protection;
using System;
using System.Collections.Generic;


namespace Intermech.Client.Specialized
{
    /// <summary>
    /// Пул сессий сервера приложений IPS для использования в специализированном клиенте IPS.
    /// </summary>
    public class SimpleSessionPool : ClientSessionPoolBase
    {
      private Func<SimpleSessionPoolLoginInfo> loginInfoProvider;

      /// <summary>Создает объект.</summary>
      /// <param name="imserverService">Сервис доступа к главному объекту сервера приложений IPS</param>
      /// <param name="clientCacheService">Сервис клиентского кэша метаданных для сессий сервера приложений</param>
      /// <param name="loginInfoProvider">Провайдер для параметров логина</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="imserverService" /> не должен быть равен null; параметр <paramref name="clientCacheService" /> не должен быть равен null; параметр <paramref name="loginInfoProvider" /> не должен быть равен null</exception>
      public SimpleSessionPool(
        IMServerService imserverService,
        IClientCache clientCacheService,
        Func<SimpleSessionPoolLoginInfo> loginInfoProvider)
        : base(imserverService, clientCacheService)
      {
        this.loginInfoProvider = loginInfoProvider != null ? loginInfoProvider : throw new ArgumentNullException(nameof (loginInfoProvider));
      }

      /// <summary>
      /// Создает основную сессию сервера приложений и выполняет вход пользователя.
      /// </summary>
      /// <returns>Объект сессии и информация о пользователе</returns>
      protected override Tuple<IUserSession, UserSessionLoginInfo> CreateAndLoginMainSession()
      {
        IUserSession session = this.IMServerService.ServerObject.CreateSession();
        SimpleSessionPoolLoginInfo sessionPoolLoginInfo = this.loginInfoProvider();
        this.InitializeRole(sessionPoolLoginInfo, session);
        session.Login(sessionPoolLoginInfo.LoginName, new PswPackage(sessionPoolLoginInfo.Password, this.IMServerService.ServerObject.CryptMethod), EnvironmentConsts.MachineName, TimeZoneInfo.Local.BaseUtcOffset, sessionPoolLoginInfo.RoleId, sessionPoolLoginInfo.AccessLevel);
        sessionPoolLoginInfo.UserName = session.UserName;
        sessionPoolLoginInfo.ActingUserName = session.ActingUserName;
        return Tuple.Create<IUserSession, UserSessionLoginInfo>(session, this.CreateSafeLoginInfoCopy(sessionPoolLoginInfo));
      }

      private void InitializeRole(SimpleSessionPoolLoginInfo loginInfo, IUserSession newMainSession)
      {
        RoleProperties[] rolesList = newMainSession.GetRolesList(loginInfo.LoginName);
        RoleProperties roleProperties = loginInfo.RoleId == 0L || loginInfo.RoleId == -1L ? CollectionUtils.Find<RoleProperties>((IEnumerable<RoleProperties>) rolesList, (Predicate<RoleProperties>) (item => string.Equals(item.RoleName, loginInfo.RoleName, StringComparison.CurrentCultureIgnoreCase))) : CollectionUtils.Find<RoleProperties>((IEnumerable<RoleProperties>) rolesList, (Predicate<RoleProperties>) (item => item.RoleID == loginInfo.RoleId));
        loginInfo.RoleId = roleProperties != null ? roleProperties.RoleID : throw new KernelException($"У пользователя '{loginInfo.LoginName}' отсутствует указанная роль '{loginInfo.RoleName}'.");
        loginInfo.RoleName = roleProperties.RoleName;
      }

      private UserSessionLoginInfo CreateSafeLoginInfoCopy(SimpleSessionPoolLoginInfo fullLoginInfo)
      {
        UserSessionLoginInfo safeLoginInfoCopy = new UserSessionLoginInfo();
        safeLoginInfoCopy.Assign((UserSessionLoginInfo) fullLoginInfo);
        return safeLoginInfoCopy;
      }
    }
}
