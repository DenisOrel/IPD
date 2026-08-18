// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.DBManagerCrossThreadAccessGuard
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using Intermech.Interfaces.Server;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Text;

#nullable disable
namespace Intermech.Server.Data;

internal sealed class DBManagerCrossThreadAccessGuard : CrossThreadAccessGuard
{
  protected override Exception CreateThreadConflictException(
    CrossThreadAccessInfo threadInfo,
    CrossThreadAccessOperation operation,
    CrossThreadConflictInfo conflictInfo)
  {
    UserSessionThreadConflictException conflictException = new UserSessionThreadConflictException($"Одновременный многопоточный доступ к объекту IUserSession недопустим!!! Сведения о конфликте: [Thread ID: {threadInfo.ThreadId}, Conflict ID: {conflictInfo.ConflictId:N}]", threadInfo.ThreadId, conflictInfo.ConflictId);
    this.ReportExceptionToServerLog(threadInfo, conflictInfo, (Exception) conflictException);
    return (Exception) conflictException;
  }

  private void ReportExceptionToServerLog(
    CrossThreadAccessInfo threadInfo,
    CrossThreadConflictInfo conflictInfo,
    Exception exception)
  {
    IEventLogHelper eventLogHelper = DbManagerConfiguration.EventLogHelper;
    if (eventLogHelper == null)
      return;
    string logMessage = this.CreateLogMessage(threadInfo, conflictInfo, exception);
    eventLogHelper.AddToTrace(logMessage, Consts.traceAlways, "session_thread_errors.log");
  }

  private string CreateLogMessage(
    CrossThreadAccessInfo threadInfo,
    CrossThreadConflictInfo conflictInfo,
    Exception exception)
  {
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(2048 /*0x0800*/))
    {
      StringBuilder stringBuilder = objectPoolScope.Object;
      stringBuilder.AppendLine("------------------------------------");
      stringBuilder.AppendLine(exception.Message);
      stringBuilder.AppendFormat("Type: {0}", (object) exception.GetType()).AppendLine();
      stringBuilder.AppendLine("Stack trace:");
      stringBuilder.AppendLine(threadInfo.StackTrace);
      return stringBuilder.ToString();
    }
  }
}
