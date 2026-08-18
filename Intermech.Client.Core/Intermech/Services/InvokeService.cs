
// Type: Intermech.Services.InvokeService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using Intermech.Interfaces.Client;
using System;
using System.Reflection;


namespace Intermech.Services;

/// <summary>
/// Реализует основу сервиса, позволяющего выполнить произвольный метод на основном потоке приложения.
/// </summary>
public abstract class InvokeService : IInvokeService
{
  /// <summary>
  /// Позволяет выполнить указанный метод на основном потоке приложения, в котором работает UI.
  /// </summary>
  /// <param name="timeout">Таймаут, в течение которого следует ожидать готовности основного потока приложения</param>
  /// <param name="method">Делегат выполняемого метода</param>
  /// <param name="args">Аргументы вызова метода</param>
  /// <returns>Результат выполнения метода</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на делегат выполняемого метода не может быть null</exception>
  /// <exception cref="T:System.TimeoutException">Время ожидания готовности основного потока вышло</exception>
  [Obsolete("Use IInvokeService.InvokeAction or IInvokeService.InvokeFunc methods instead of this method.", true)]
  public object Invoke(int timeout, Delegate method, params object[] args)
  {
    return (object) method != null ? this.InvokeFunc<object>(-1, (Func<object>) (() => method.DynamicInvoke(args))) : throw new ArgumentNullException(nameof (method));
  }

  /// <summary>
  /// Позволяет выполнить анонимный метод на основном потоке приложения, в котором работает UI.
  /// </summary>
  /// <param name="timeout">Таймаут, в течение которого следует ожидать готовности основного потока приложения</param>
  /// <param name="method">Выполняемый анонимный метод</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на делегат выполняемого метода не может быть null</exception>
  /// <exception cref="T:System.TimeoutException">Время ожидания готовности основного потока вышло</exception>
  [Obsolete("Use the IInvokeService.InvokeAction method instead of this method.", true)]
  public void InvokeCode(int timeout, InvokeServiceMethod method)
  {
    if (method == null)
      throw new ArgumentNullException(nameof (method));
    this.InvokeAction(timeout, new Action(method.Invoke));
  }

  /// <summary>
  /// Позволяет выполнить и получить результат выполнения анонимного метода на основном потоке приложения, в котором работает UI.
  /// </summary>
  /// <typeparam name="T">Тип значения, возвращаемого анонимным методом</typeparam>
  /// <param name="timeout">Таймаут, в течение которого следует ожидать готовности основного потока приложения</param>
  /// <param name="method">Выполняемый анонимный метод</param>
  /// <returns>Результат выполнения анонимного метода</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на делегат выполняемого метода не может быть null</exception>
  /// <exception cref="T:System.TimeoutException">Время ожидания готовности основного потока вышло</exception>
  [Obsolete("Use the IInvokeService.InvokeFunc method instead of this method.", true)]
  public T InvokeCode<T>(int timeout, InvokeServiceMethod<T> method)
  {
    return method != null ? this.InvokeFunc<T>(timeout, new Func<T>(method.Invoke)) : throw new ArgumentNullException(nameof (method));
  }

  /// <summary>
  /// Позволяет выполнить анонимный метод на основном потоке приложения, в котором работает UI.
  /// </summary>
  /// <param name="timeout">Таймаут, в течение которого следует ожидать готовности основного потока приложения</param>
  /// <param name="method">Выполняемый анонимный метод</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на делегат выполняемого метода не может быть null</exception>
  /// <exception cref="T:System.TimeoutException">Время ожидания готовности основного потока вышло</exception>
  public void InvokeAction(int timeout, Action method)
  {
    if (method == null)
      throw new ArgumentNullException(nameof (method));
    this.InvokeInternal<Missing>(timeout, (Func<Missing>) (() =>
    {
      method();
      return Missing.Value;
    }));
  }

  /// <summary>
  /// Позволяет выполнить и получить результат выполнения анонимного метода на основном потоке приложения, в котором работает UI.
  /// </summary>
  /// <typeparam name="T">Тип значения, возвращаемого анонимным методом</typeparam>
  /// <param name="timeout">Таймаут, в течение которого следует ожидать готовности основного потока приложения</param>
  /// <param name="method">Выполняемый анонимный метод</param>
  /// <returns>Результат выполнения анонимного метода</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на делегат выполняемого метода не может быть null</exception>
  /// <exception cref="T:System.TimeoutException">Время ожидания готовности основного потока вышло</exception>
  public T InvokeFunc<T>(int timeout, Func<T> method)
  {
    return method != null ? this.InvokeInternal<T>(timeout, method) : throw new ArgumentNullException(nameof (method));
  }

  /// <summary>
  /// Позволяет выполнить указанный метод на основном потоке приложения, в котором работает UI.
  /// </summary>
  /// <param name="timeout">Таймаут, в течение которого следует ожидать готовности основного потока приложения</param>
  /// <param name="method">Делегат выполняемого метода</param>
  /// <returns>Результат выполнения метода</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на делегат выполняемого метода не может быть null</exception>
  /// <exception cref="T:System.TimeoutException">Время ожидания готовности основного потока вышло</exception>
  private T InvokeInternal<T>(int timeout, Func<T> method)
  {
    if (this.IsDirectInvokeAllowed((Delegate) method))
      return this.DirectInvokeInternal<T>(method);
    if (timeout < 0)
      timeout = 20000;
    return this.IndirectInvokeInternal<T>(timeout, method);
  }

  private T DirectInvokeInternal<T>(Func<T> method)
  {
    using (new DynamicScope())
    {
      this.PrepareInvoke();
      return method();
    }
  }

  private T IndirectInvokeInternal<T>(int timeout, Func<T> method)
  {
    return this.DoIndirectInvoke<T>(timeout, (Func<T>) (() => this.DirectInvokeInternal<T>(method)));
  }

  private void PrepareInvoke()
  {
    SessionPoolVars.ControlFlowId.Declare(SessionPoolVars.CreateControlFlowId());
  }

  /// <summary>
  /// Проверяет, возможен ли прямой вызов указанного метода без переключения потоков. Этот метод позволяет
  /// выявить случаи, когда обращение к сервису осуществляется из основного потока приложения.
  /// </summary>
  /// <param name="method">Делегат выполняемого метода</param>
  /// <returns>true, если обращение к сервису выполняется из основного потока приложения и переключение потоков не требуется, false - если должен использоваться непрямой вызов с переключением потоков</returns>
  protected abstract bool IsDirectInvokeAllowed(Delegate method);

  /// <summary>
  /// Реализует непрямой вызов метода с переключением потоков.
  /// </summary>
  /// <param name="timeout">Таймаут, в течение которого следует ожидать готовности основного потока приложения</param>
  /// <param name="method">Делегат выполняемого метода</param>
  /// <returns>Результат выполнения метода</returns>
  protected abstract T DoIndirectInvoke<T>(int timeout, Func<T> method);
}
