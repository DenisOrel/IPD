// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.FixEditingContext
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.Contexts;
using System;
using System.Diagnostics;
using System.Threading;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Информация, передаваемая в контексте вызова какого-то задания, для фиксации контекста редактирования в данном задании.
/// Класс сам умеет получать информацию о текущем контексте редактирования. Если требуется указать другие значения,
/// следует использовать класс <see cref="T:Intermech.Interfaces.Contexts.CurrentEditingContextScope" />.
/// </summary>
public sealed class FixEditingContext : IDisposable
{
  private CurrentEditingContext _context;
  private CurrentEditingContextScope _contextFix;
  private bool _isDisposed;

  /// <summary>
  /// Создать экземпляр класса для фиксации текущего контекста редактирования в рамках потока
  /// </summary>
  public FixEditingContext()
  {
    ICurrentUserAndRole service = (ICurrentUserAndRole) ApplicationServices.Container.GetService(typeof (ICurrentUserAndRole));
    this._context = new CurrentEditingContext(service.CachedEditingContextID, service.CachedEditingContextModificationID, service.CachedContextMode);
    this._contextFix = new CurrentEditingContextScope(this._context);
  }

  /// <summary>Освободить ресурсы</summary>
  public void Dispose()
  {
    if (this._isDisposed)
      return;
    this._isDisposed = true;
    this._contextFix.Dispose();
    this._contextFix = (CurrentEditingContextScope) null;
    this._context = (CurrentEditingContext) null;
  }

  private void CheckNotDisposed()
  {
    if (this._isDisposed)
      throw new ObjectDisposedException(this.GetType().FullName);
  }

  /// <summary>
  /// Возвращает текущий фиксированный контекст редактирования.
  /// Значение свойства может содержать объект-пустышку <see cref="P:Intermech.Interfaces.Contexts.CurrentEditingContext.Dummy" />,
  /// который обозначает, что контекст редактирования не фиксирован.
  /// </summary>
  public CurrentEditingContext EditingContext
  {
    [DebuggerStepThrough] get
    {
      this.CheckNotDisposed();
      return this._context;
    }
  }

  public ThreadStart SendEditingContextToThread(ThreadStart threadStartAction)
  {
    if (threadStartAction == null)
      throw new ArgumentNullException(nameof (threadStartAction));
    this.CheckNotDisposed();
    return this._context.SendToThread(threadStartAction);
  }

  public ParameterizedThreadStart SendEditingContextToThread(
    ParameterizedThreadStart threadStartAction)
  {
    if (threadStartAction == null)
      throw new ArgumentNullException(nameof (threadStartAction));
    this.CheckNotDisposed();
    return this._context.SendToThread(threadStartAction);
  }

  public Action SendEditingContextToTask(Action taskAction)
  {
    if (taskAction == null)
      throw new ArgumentNullException(nameof (taskAction));
    this.CheckNotDisposed();
    return this._context.SendToTask(taskAction);
  }
}
