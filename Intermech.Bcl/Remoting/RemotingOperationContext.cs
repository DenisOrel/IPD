
// Type: Intermech.Remoting.RemotingOperationContext
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;


namespace Intermech.Remoting
{
    /// <summary>
    /// Контекст текущей операции remoting.
    /// Реализация не является thread safe.
    /// </summary>
    public sealed class RemotingOperationContext
    {
      private bool isStarted;
      private CancellationToken cancellationToken;
      private List<Action> completionCallbacks;
      [ThreadStatic]
      private static RemotingOperationContext currentContext;

      /// <summary>
      /// Отмечает начало операции.
      /// Если операция уже была начата, то метод завершается с исключением <see cref="T:System.InvalidOperationException" />.
      /// </summary>
      /// <exception cref="T:System.InvalidOperationException">Операция уже была начата</exception>
      public void Start()
      {
        if (this.isStarted)
          throw new InvalidOperationException("The operation is already started.");
        this.cancellationToken = new CancellationToken();
        this.isStarted = true;
      }

      /// <summary>
      /// Отмечает окончание операции.
      /// Если операция не была начата, то метод завершает выполнение без исключения.
      /// </summary>
      public void Stop()
      {
        if (!this.isStarted)
          return;
        int num = this.HasCompletionCallbacks() ? 1 : 0;
        if (num != 0)
          this.InvokeCompletionCallbacks();
        if (num != 0)
          this.completionCallbacks.Clear();
        this.cancellationToken = new CancellationToken();
        this.isStarted = false;
      }

      /// <summary>
      /// Регистрирует обработчик, который будет вызван при окончании операции
      /// </summary>
      /// <param name="action">Обработчик обратного вызова</param>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="action" /> содержит null</exception>
      public void RegisterCompletionCallback(Action action)
      {
        if (action == null)
          throw new ArgumentNullException(nameof (action));
        if (this.completionCallbacks == null)
          this.completionCallbacks = new List<Action>();
        if (this.completionCallbacks.Contains(action))
          return;
        this.completionCallbacks.Add(action);
      }

      private void InvokeCompletionCallbacks()
      {
        SilentActionInvoker silentActionInvoker = SilentActionInvoker.Default;
        foreach (Action completionCallback in this.completionCallbacks)
          silentActionInvoker.Invoke(completionCallback, "RemotingOperationContext.InvokeCompletionCallbacks");
      }

      private bool HasCompletionCallbacks()
      {
        return this.completionCallbacks != null && this.completionCallbacks.Count != 0;
      }

      /// <summary>Возвращает признак, что операция была начата.</summary>
      public bool IsStarted
      {
        [DebuggerStepThrough] get => this.isStarted;
      }

      /// <summary>
      /// Возвращает или задает токен, позволяющий прервать операцию.
      /// </summary>
      /// <exception cref="T:System.InvalidOperationException">Операция не была начата</exception>
      public CancellationToken CancellationToken
      {
        [DebuggerStepThrough] get => this.cancellationToken;
        set
        {
          if (!this.isStarted)
            throw new InvalidOperationException("The operation is not started.");
          this.cancellationToken = value;
        }
      }

      /// <summary>
      /// Возвращает контекст операции Remoting для текущего потока.
      /// </summary>
      public static RemotingOperationContext Current
      {
        [DebuggerStepThrough] get
        {
          if (RemotingOperationContext.currentContext == null)
            RemotingOperationContext.currentContext = new RemotingOperationContext();
          return RemotingOperationContext.currentContext;
        }
      }
    }
}
