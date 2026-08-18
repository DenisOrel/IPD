
// Type: Intermech.Interfaces.SessionKeeper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Реализует хранителя сессии, обеспечивающего выделение пользовательской сессии подключения к
    /// серверу приложений.
    /// </summary>
    public sealed class SessionKeeper : IDisposable
    {
      [ThreadStatic]
      private static SessionKeeper.ThreadData threadData;
      private static volatile IUserSessionAllocator currentAllocator;
      private static SessionValidationManager validators = new SessionValidationManager();
      private static volatile bool isSessionGuardEnabled;
      private IUserSessionAllocator allocator;
      private SessionKeeperResourceContext resourceContext;
      private IUserSessionDescriptor sessionDescriptor;
      private IUserSession session;
      private Guid sessionGuid;
      private bool isNewScope;

      /// <summary>
      /// Создает хранителя сессии. В процессе создания выполняется проверка подключения
      /// к серверу приложений и, при необходимости, переподключение.
      /// </summary>
      public SessionKeeper()
      {
        this.allocator = SessionKeeper.CurrentAllocator;
        if (this.allocator == null)
          throw new InvalidOperationException("Не задан объект для выделения сессий к базе данных. Для этого следует установить свойство CurrentAllocator.");
        try
        {
          this.SuspendTracker();
          try
          {
            this.sessionDescriptor = this.allocator.Allocate();
            this.session = this.sessionDescriptor.Session;
            if (this.sessionDescriptor.IsTopmost)
            {
              SessionValidatorResult sessionValidatorResult = SessionKeeper.Validators.ValidateSession(this.sessionDescriptor, SessionKeeper.Validators.AfterAllocateSessionFromPool);
              if (!sessionValidatorResult.IsSuccessful && sessionValidatorResult.ErrorException != null)
                throw sessionValidatorResult.ErrorException;
            }
            this.AttachTrackerToSession();
          }
          finally
          {
            this.ResumeTracker();
          }
          this.TrackAllocatedSession();
        }
        catch
        {
          if (this.sessionDescriptor != null)
            this.allocator.Release(this.sessionDescriptor);
          throw;
        }
        this.resourceContext = SessionKeeperResourceContext.Current;
        if (this.resourceContext.Depth == 0)
          this.resourceContext.SessionGUID = this.session.SessionGUID;
        ++this.resourceContext.Depth;
      }

      /// <summary>Освобождает удерживаемую пользовательскую сессию.</summary>
      public void Dispose()
      {
        if (this.allocator == null)
          return;
        --this.resourceContext.Depth;
        if (this.resourceContext.Depth == 0)
          this.resourceContext.SessionGUID = Guid.Empty;
        bool isTopmost = this.sessionDescriptor.IsTopmost;
        bool isSessionLost = this.resourceContext.IsSessionLost;
        if (!isSessionLost)
        {
          SessionValidatorResult success = SessionValidatorResult.Success;
          if (isTopmost)
            SessionKeeper.Validators.ValidateSession(this.sessionDescriptor, SessionKeeper.Validators.BeforeReleaseSessionToPool);
        }
        if (isTopmost & isSessionLost && this.sessionDescriptor.TrySetReleaseMode(UserSessionReleaseMode.Drop))
          this.resourceContext.IsSessionLost = false;
        this.allocator.Release(this.sessionDescriptor);
        this.allocator = (IUserSessionAllocator) null;
        this.resourceContext = (SessionKeeperResourceContext) null;
        this.sessionDescriptor = (IUserSessionDescriptor) null;
        this.session = (IUserSession) null;
        this.TrackReleasedSession();
      }

      public IUserSession Session => this.session;

      private void SuspendTracker()
      {
        if (!SessionKeeper.isSessionGuardEnabled)
          return;
        SessionGuardContext.Suspend();
      }

      private void ResumeTracker()
      {
        if (!SessionKeeper.isSessionGuardEnabled)
          return;
        SessionGuardContext.Resume();
      }

      private void AttachTrackerToSession()
      {
        if (!SessionKeeper.isSessionGuardEnabled)
          return;
        this.SetSessionGuardData();
      }

      private void TrackAllocatedSession()
      {
        if (!SessionKeeper.isSessionGuardEnabled || !this.isNewScope || this.session.IsSessionGuardActive)
          return;
        this.session.ActivateSessionGuard();
      }

      private void TrackReleasedSession()
      {
        if (!SessionKeeper.isSessionGuardEnabled)
          return;
        this.ClearSessionGuardData();
      }

      private void SetSessionGuardData()
      {
        SessionKeeper.ThreadData threadData = SessionKeeper.GetThreadData();
        this.sessionGuid = this.session.SessionGUID;
        if (threadData.SessionStack.Count == 0)
        {
          this.isNewScope = true;
          threadData.SessionStack.AddFirst(new SessionKeeper.DepthRecord(this.sessionGuid, 1));
          SessionGuardContext.SetActiveClientSession(this.sessionGuid);
        }
        else
        {
          SessionKeeper.DepthRecord depthRecord1 = threadData.SessionStack.First.Value;
          if (this.sessionGuid == depthRecord1.SessionGuid)
          {
            ++depthRecord1.Depth;
          }
          else
          {
            this.isNewScope = true;
            SessionKeeper.DepthRecord depthRecord2 = new SessionKeeper.DepthRecord(this.sessionGuid, 1);
            threadData.SessionStack.AddFirst(depthRecord2);
            SessionGuardContext.SetActiveClientSession(this.sessionGuid);
          }
        }
      }

      private void ClearSessionGuardData()
      {
        SessionKeeper.ThreadData threadData = SessionKeeper.GetThreadData();
        SessionKeeper.DepthRecord depthRecord = threadData.SessionStack.First.Value;
        if (this.sessionGuid != depthRecord.SessionGuid)
          throw new InvalidOperationException("Нарушен корректный порядок закрытия SessionKeeper!");
        --depthRecord.Depth;
        if (depthRecord.Depth != 0)
          return;
        threadData.SessionStack.RemoveFirst();
        if (threadData.SessionStack.Count == 0)
          SessionGuardContext.ResetActiveClientSession();
        else
          SessionGuardContext.SetActiveClientSession(threadData.SessionStack.First.Value.SessionGuid);
      }

      private static SessionKeeper.ThreadData GetThreadData()
      {
        if (SessionKeeper.threadData == null)
          SessionKeeper.threadData = new SessionKeeper.ThreadData();
        return SessionKeeper.threadData;
      }

      public static IUserSessionAllocator CurrentAllocator => SessionKeeper.currentAllocator;

      public static bool InitializeAllocator(IUserSessionAllocator allocator)
      {
        if (allocator == null)
          throw new ArgumentNullException(nameof (allocator));
        return Interlocked.CompareExchange<IUserSessionAllocator>(ref SessionKeeper.currentAllocator, allocator, (IUserSessionAllocator) null) == null;
      }

      public static SessionValidationManager Validators
      {
        [DebuggerStepThrough] get => SessionKeeper.validators;
      }

      public static void EnableSessionGuard() => SessionKeeper.isSessionGuardEnabled = true;

      private sealed class ThreadData
      {
        public readonly LinkedList<SessionKeeper.DepthRecord> SessionStack;

        internal ThreadData() => this.SessionStack = new LinkedList<SessionKeeper.DepthRecord>();
      }

      private sealed class DepthRecord
      {
        public readonly Guid SessionGuid;
        public int Depth;

        public DepthRecord(Guid sessionGuid, int depth)
        {
          this.SessionGuid = sessionGuid;
          this.Depth = depth;
        }
      }
    }
}
