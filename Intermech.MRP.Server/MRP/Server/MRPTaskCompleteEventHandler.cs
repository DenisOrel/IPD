// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.MRPTaskCompleteEventHandler
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Interfaces.MRP;
using System.Collections.Generic;

#nullable disable
namespace Intermech.MRP.Server;

internal delegate void MRPTaskCompleteEventHandler(
  IMRPCompositionTask task,
  LinkedList<IMRPAction> result,
  LinkedList<IMRPCompositionTask> advancedTasks);
