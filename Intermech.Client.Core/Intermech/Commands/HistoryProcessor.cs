
// Type: Intermech.Commands.HistoryProcessor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Commands;

internal sealed class HistoryProcessor
{
  private Guid sessionGuid;

  public void Start(IUserSession session)
  {
    if (session == null)
      throw new ArgumentNullException();
    if (this.sessionGuid != Guid.Empty)
      throw new InvalidOperationException();
    session.StartLogHistory();
    this.sessionGuid = session.SessionGUID;
  }

  public List<CategoryValue> Stop(IUserSession session)
  {
    if (this.sessionGuid == Guid.Empty)
      throw new InvalidOperationException();
    if (session == null)
      throw new ArgumentNullException();
    if (session.SessionGUID != this.sessionGuid)
      throw new ArgumentException();
    session.StopLogHistory();
    this.sessionGuid = Guid.Empty;
    return session.GetModificationsHistoryList() ?? new List<CategoryValue>(0);
  }
}
