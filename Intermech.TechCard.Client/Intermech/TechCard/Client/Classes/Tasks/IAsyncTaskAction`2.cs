// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Classes.Tasks.IAsyncTaskAction`2
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

#nullable disable
namespace Intermech.TechCard.Client.Classes.Tasks;

/// <summary>Интерфейс действия задачи экспорта</summary>
/// <typeparam name="TParam"></typeparam>
/// <typeparam name="TResult"></typeparam>
internal interface IAsyncTaskAction<TParam, TResult>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="asyncTask"></param>
  /// <param name="data"></param>
  /// <returns></returns>
  TResult Execute(AsyncTaskBase<TParam, TResult> asyncTask, TParam data);
}
