// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.IMRPCompositionsServerBrowser
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.MRP;
using System;

#nullable disable
namespace Intermech.MRP.Server;

internal interface IMRPCompositionsServerBrowser : IMRPCompositionsBrowser
{
  IMRPTasksQueue CreateTasksQueue(
    Guid sessionGuid,
    IServiceProvider services,
    CurrentEditingContext editingContext,
    int threadsCount,
    bool autoComplete);

  IMRPTasksQueue GetTasksQueue(Guid queueGuid);

  bool RemoveTasksQueue(Guid queueGuid);

  bool RemoveTasksQueue(IMRPTasksQueue queue);
}
