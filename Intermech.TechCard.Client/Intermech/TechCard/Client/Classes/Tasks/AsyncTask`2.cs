// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Classes.Tasks.AsyncTask`2
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System;
using System.Threading;

#nullable disable
namespace Intermech.TechCard.Client.Classes.Tasks;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TParam"></typeparam>
/// <typeparam name="TResult"></typeparam>
internal class AsyncTask<TParam, TResult> : AsyncTaskBase<TParam, TResult>
{
  /// <summary>
  /// 
  /// </summary>
  private readonly IAsyncTaskAction<TParam, TResult> _action;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="action"></param>
  /// <param name="synchronizationContext"></param>
  public AsyncTask(
    IAsyncTaskAction<TParam, TResult> action,
    SynchronizationContext synchronizationContext = null)
    : base(synchronizationContext)
  {
    this._action = action ?? throw new ArgumentNullException(nameof (action));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="techcardAsyncTask"></param>
  /// <param name="data"></param>
  /// <returns></returns>
  protected override TResult DoExecute(
    AsyncTaskBase<TParam, TResult> techcardAsyncTask,
    TParam data)
  {
    return this._action.Execute(techcardAsyncTask, data);
  }
}
