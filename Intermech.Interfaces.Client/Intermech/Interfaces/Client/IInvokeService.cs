// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IInvokeService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Сервис, позволяющий выполнить произвольный метод на основном потоке приложения. Этот сервис используется
/// фоновыми потоками приложения в случаях, когда им нужно взаимодействовать с пользователем посредством UI.
/// </summary>
public interface IInvokeService
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
  object Invoke(int timeout, Delegate method, params object[] args);

  /// <summary>
  /// Позволяет выполнить анонимный метод на основном потоке приложения, в котором работает UI.
  /// </summary>
  /// <param name="timeout">Таймаут, в течение которого следует ожидать готовности основного потока приложения</param>
  /// <param name="method">Выполняемый анонимный метод</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на делегат выполняемого метода не может быть null</exception>
  /// <exception cref="T:System.TimeoutException">Время ожидания готовности основного потока вышло</exception>
  [Obsolete("Use the IInvokeService.InvokeAction method instead of this method.", true)]
  void InvokeCode(int timeout, InvokeServiceMethod method);

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
  T InvokeCode<T>(int timeout, InvokeServiceMethod<T> method);

  /// <summary>
  /// Позволяет выполнить анонимный метод на основном потоке приложения, в котором работает UI.
  /// </summary>
  /// <param name="timeout">Таймаут, в течение которого следует ожидать готовности основного потока приложения</param>
  /// <param name="method">Выполняемый анонимный метод</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на делегат выполняемого метода не может быть null</exception>
  /// <exception cref="T:System.TimeoutException">Время ожидания готовности основного потока вышло</exception>
  void InvokeAction(int timeout, Action method);

  /// <summary>
  /// Позволяет выполнить и получить результат выполнения анонимного метода на основном потоке приложения, в котором работает UI.
  /// </summary>
  /// <typeparam name="T">Тип значения, возвращаемого анонимным методом</typeparam>
  /// <param name="timeout">Таймаут, в течение которого следует ожидать готовности основного потока приложения</param>
  /// <param name="method">Выполняемый анонимный метод</param>
  /// <returns>Результат выполнения анонимного метода</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на делегат выполняемого метода не может быть null</exception>
  /// <exception cref="T:System.TimeoutException">Время ожидания готовности основного потока вышло</exception>
  T InvokeFunc<T>(int timeout, Func<T> method);
}
