// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Classes.Tasks.AsyncTaskAction`2
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System;

#nullable disable
namespace Intermech.TechCard.Client.Classes.Tasks;

/// <summary>Общий класс действий с поддержкой делегатов</summary>
/// <typeparam name="TParam"></typeparam>
/// <typeparam name="TResult"></typeparam>
internal class AsyncTaskAction<TParam, TResult> : IAsyncTaskAction<TParam, TResult>
{
  /// <summary>
  /// 
  /// </summary>
  private readonly Func<AsyncTaskBase<TParam, TResult>, TParam, TResult> _delegateFunc;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="delegateFunc"></param>
  public AsyncTaskAction(
    Func<AsyncTaskBase<TParam, TResult>, TParam, TResult> delegateFunc)
  {
    this._delegateFunc = delegateFunc ?? throw new ArgumentNullException(nameof (delegateFunc));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="techcardAsyncTask"></param>
  /// <param name="data"></param>
  /// <returns></returns>
  public TResult Execute(AsyncTaskBase<TParam, TResult> asyncTask, TParam data)
  {
    return this._delegateFunc(asyncTask, data);
  }
}
