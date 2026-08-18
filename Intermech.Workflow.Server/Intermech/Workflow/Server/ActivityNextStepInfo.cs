// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.ActivityNextStepInfo
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Server;

public class ActivityNextStepInfo
{
  public List<WFLink> NextStepLinks;
  public VarList VariableList;
  public AttachmentList SavedAttachmentList;
  private long _activityID;
  public bool SavedCaseIsError;

  public ActivityNextStepInfo(long activityID) => this._activityID = activityID;

  public long ActivityID => this._activityID;
}
