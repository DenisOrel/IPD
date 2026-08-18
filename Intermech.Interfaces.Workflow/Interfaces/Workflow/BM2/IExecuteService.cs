// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.BM2.IExecuteService
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

#nullable disable
namespace Intermech.Interfaces.Workflow.BM2;

public interface IExecuteService
{
  /// <summary>Метода запуска аварийного процесса</summary>
  /// <param name="processID">ид процесса для запуска</param>
  /// <param name="activityID">ид действия для запуска</param>
  /// <param name="userID">ид пользователя кто запускает</param>
  void Execute(long processID, long activityID, long userID);

  /// <summary>
  /// Метода запуска аварийного процесса с действия которого ещё не выполнялось
  /// </summary>
  /// <param name="processID">ид процесса для запуска</param>
  /// <param name="activityID">ид действия для запуска</param>
  /// <param name="senderActivityID">ид действия откуда идёт запуск, с данного действия будут взяты вложения и значения переменных</param>
  /// <param name="userID">ид пользователя кто запускает</param>
  void ExecuteCustomSender(long processID, long activityID, long senderActivityID, long userID);
}
