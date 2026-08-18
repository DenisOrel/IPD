// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.MailRefreshWithReloadWorkOfferEventArgs
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Design;

public class MailRefreshWithReloadWorkOfferEventArgs : MailRefreshWithoutFormPopupEventArgs
{
  private HashSet<long> _activityIDs;

  public MailRefreshWithReloadWorkOfferEventArgs(string eventName, HashSet<long> activityIDs)
    : base(eventName)
  {
    this._activityIDs = activityIDs;
  }

  public HashSet<long> ActivityIDs => this._activityIDs;
}
