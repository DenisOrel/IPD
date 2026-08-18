
// Type: Intermech.Security.RBSClient
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Security.Principal;


namespace Intermech.Security
{
    /// <summary>
    /// Содержит клиентские утилиты для работы с role-based security.
    /// </summary>
    public static class RBSClient
    {
      private static object syncRoot = new object();
      private static bool isInitialized;
      private static readonly IPSPrincipal emptyPrincipal = new IPSPrincipal(IPSIdentity.UnloggedUser, Guid.Empty, IPSBuiltInRole.User);

      /// <summary>
      /// Инициализирует контекст безопасности для кода клиентской части IPS. После выполнения этого метода
      /// клиентский код будет выполняться от имени анонимного пользователя, не вошедшего в систему.
      /// </summary>
      public static void InitializeSecurityContext()
      {
        lock (RBSClient.syncRoot)
        {
          if (RBSClient.isInitialized)
            return;
          AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.UnauthenticatedPrincipal);
          RBSClient.isInitialized = true;
        }
      }

      private static void CheckInitialized()
      {
        lock (RBSClient.syncRoot)
        {
          if (!RBSClient.isInitialized)
            throw new InvalidOperationException(LocalizationHolder.rm.GetString("Interfaces_735"));
        }
      }

      /// <summary>
      /// Обновляет контекст безопасности для кода клиентской части IPS. После успешного выполнения этого метода
      /// клиентский код будет выполняться от имени залогинившегося пользователя.
      /// </summary>
      /// <param name="session">Сессия пользователя</param>
      public static void UpdateSecurityContext(IUserSession session)
      {
        if (session == null)
          throw new ArgumentNullException(nameof (session));
        RBSClient.CheckInitialized();
        IPSPrincipal.DefaultPrincipal = RBSClient.CreatePrincipalFromSession(session);
      }

      /// <summary>
      /// Очищает контекст безопасности для кода клиентской части IPS. После успешного выполнения этого метода
      /// клиентский код будет выполняться от имени анонимного пользователя.
      /// </summary>
      /// <param name="session">Сессия пользователя</param>
      public static void ClearSecurityContext()
      {
        RBSClient.CheckInitialized();
        IPSPrincipal.DefaultPrincipal = RBSClient.emptyPrincipal;
      }

      /// <summary>
      /// Создает объект principal под данным из указанной пользовательской сессии IPS.
      /// </summary>
      /// <param name="session">Пользовательская сессия IPS</param>
      /// <returns>Объект principal</returns>
      public static IPSPrincipal CreatePrincipalFromSession(IUserSession session)
      {
        long userId = session != null ? session.UserID : throw new ArgumentNullException(nameof (session));
        string userName1 = session.UserName;
        IPSBuiltInRole role = session.IsAdmin ? IPSBuiltInRole.Administrator : IPSBuiltInRole.User;
        Guid masterSessionGuid = session.MasterSessionGUID;
        string userName2 = userName1;
        return new IPSPrincipal(new IPSIdentity(userId, userName2), masterSessionGuid, role);
      }
    }
}
