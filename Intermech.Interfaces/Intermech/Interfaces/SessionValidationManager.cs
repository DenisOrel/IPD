
// Type: Intermech.Interfaces.SessionValidationManager
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Реализует менеждер для валидации состояния пользовательских сессий сервера приложений. Он предоставляет точки подключения валидаторов состояния.
    /// Реализация является thread safe.
    /// </summary>
    public class SessionValidationManager
    {
      private SessionValdatorBag afterAllocateSessionFromPool;
      private SessionValdatorBag beforeReleaseSessionToPool;

      /// <summary>Создает объект.</summary>
      public SessionValidationManager()
      {
        this.afterAllocateSessionFromPool = new SessionValdatorBag();
        this.beforeReleaseSessionToPool = new SessionValdatorBag();
      }

      /// <summary>
      /// Коллекция валидаторов, используемая непосредственно после извлечения пользовательской сессии из пула сессий.
      /// </summary>
      public SessionValdatorBag AfterAllocateSessionFromPool
      {
        [DebuggerStepThrough] get => this.afterAllocateSessionFromPool;
      }

      /// <summary>
      /// Коллекция валидаторов, используемая непосредственно перед возвратом пользовательской сессии в пул сессий.
      /// </summary>
      public SessionValdatorBag BeforeReleaseSessionToPool
      {
        [DebuggerStepThrough] get => this.beforeReleaseSessionToPool;
      }

      internal SessionValidatorResult ValidateSession(
        IUserSessionDescriptor sessionDescriptor,
        SessionValdatorBag validatorBag)
      {
        if (sessionDescriptor == null)
          throw new ArgumentNullException(nameof (sessionDescriptor));
        ICollection<SessionValidator> sessionValidators = validatorBag != null ? validatorBag.GetValidators() : throw new ArgumentNullException(nameof (validatorBag));
        if (sessionValidators.Count != 0)
        {
          SessionValidatorResult sessionValidatorResult1 = (SessionValidatorResult) null;
          foreach (SessionValidator sessionValidator in (IEnumerable<SessionValidator>) sessionValidators)
          {
            SessionValidatorResult sessionValidatorResult2 = sessionValidator.Validate(sessionDescriptor.Session);
            if (!sessionValidatorResult2.IsSuccessful && (sessionValidatorResult1 == null || sessionValidatorResult1.ErrorException == null && sessionValidatorResult2.ErrorException != null))
              sessionValidatorResult1 = sessionValidatorResult2;
          }
          if (sessionValidatorResult1 != null)
            return sessionValidatorResult1;
        }
        return SessionValidatorResult.Success;
      }
    }
}
