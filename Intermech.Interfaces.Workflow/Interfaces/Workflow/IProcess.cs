// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.IProcess
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Workflow;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Workflow;

public interface IProcess : 
  IScheme,
  IMailObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData,
  IDBSecurityCollection,
  IDBSecurity,
  ISchemeActivityCreator
{
  /// <summary>Заменить исполнителя</summary>
  /// <param name="userID"></param>
  /// <param name="toUserID"></param>
  /// <returns></returns>
  bool ReplaceParticipant(long userID, long toUserID);

  /// <summary>Отозвать выполняющиеся</summary>
  Dictionary<long, string> Recall();

  /// <summary>Приоритет</summary>
  ProcessPriority Priority { get; set; }

  /// <summary>Запустить процесс на выполнение</summary>
  void StartProcess();

  /// <summary>Остановить процесс</summary>
  /// <param name="sender">действие с которого выполняется остановка процесса</param>
  /// <param name="isAbort">флаг указывающий аварийно или нет останавливается процесс</param>
  void StopProcess(IActivity sender, bool isAbort);

  /// <summary>Текущий статус процесса</summary>
  ActivityStatus ProcessStatus { get; set; }

  /// <summary>Родитель процесса, в случае отсутствия 0</summary>
  long PrototypeSchemeID { get; }
}
