
// Type: Intermech.Search.UI.NotificationContext
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;


namespace Intermech.Search.UI;

public sealed class NotificationContext : IDisposable
{
  private IUserSession _userSession;
  private object _sender;

  public static NotificationContext Create(IUserSession userSession, object sender = null)
  {
    return userSession != null ? new NotificationContext(userSession, sender) : throw new ArgumentNullException(nameof (userSession));
  }

  private NotificationContext(IUserSession userSession, object sender = null)
  {
    this._userSession = userSession;
    this._sender = sender;
    this._userSession.StartLogHistory();
  }

  public void Dispose()
  {
    NotificationHelper.Notify(this._sender, this._userSession.GetModificationsHistoryList());
    this._userSession.StopLogHistory();
  }
}
