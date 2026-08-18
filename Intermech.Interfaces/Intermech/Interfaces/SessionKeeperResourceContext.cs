
// Type: Intermech.Interfaces.SessionKeeperResourceContext
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Контекст для ресурсов пользовательской сессии, распределенной с помощью <see cref="T:Intermech.Interfaces.SessionKeeper" />.
    /// Он позволяет сторонним подсистемам взаимодействовать с пользовательской сессией.
    /// </summary>
    public sealed class SessionKeeperResourceContext
    {
      private bool isSessionLost;
      private int depth;
      private Guid sessionGUID;
      [ThreadStatic]
      private static SessionKeeperResourceContext currentContext;

      /// <summary>
      /// Возвращает или задает глубину вложения экземпляров <see cref="T:Intermech.Interfaces.SessionKeeper" />.
      /// Значение свойства будет равно 0, если <see cref="T:Intermech.Interfaces.SessionKeeper" /> не был использован.
      /// </summary>
      internal int Depth
      {
        [DebuggerStepThrough] get => this.depth;
        set => this.depth = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof (value));
      }

      /// <summary>
      /// Возвращает или задает идентификатор сессии, распределенной с помощью <see cref="T:Intermech.Interfaces.SessionKeeper" />.
      /// Значение свойства будет равно <see cref="F:System.Guid.Empty" />, если <see cref="T:Intermech.Interfaces.SessionKeeper" /> не был использован.
      /// </summary>
      internal Guid SessionGUID
      {
        [DebuggerStepThrough] get => this.sessionGUID;
        set => this.sessionGUID = value;
      }

      /// <summary>
      /// Возвращает или задает признак, что пользовательская сессия была потеряна в результате
      /// односторонней клиентской ошибки remoting.
      /// </summary>
      /// <remarks>
      /// Если значение свойства равно true, то клиентская сторона remoting больше не ждет ответа от
      /// серверной стороны, но выполнение вызова на серверной стороне продолжается. Текущую
      /// пользовательскую сессию нельзя использовать, и к ней нельзя обращаться, так как это гарантировано
      /// приведет к ошибке многопоточного доступа. Такую сессию можно только отбросить.
      /// </remarks>
      internal bool IsSessionLost
      {
        [DebuggerStepThrough] get => this.isSessionLost;
        [DebuggerStepThrough] set => this.isSessionLost = value;
      }

      /// <summary>
      /// Возвращает контекст пользовательской сессии для текущего потока.
      /// </summary>
      public static SessionKeeperResourceContext Current
      {
        get
        {
          if (SessionKeeperResourceContext.currentContext == null)
            SessionKeeperResourceContext.currentContext = new SessionKeeperResourceContext();
          return SessionKeeperResourceContext.currentContext;
        }
      }
    }
}
