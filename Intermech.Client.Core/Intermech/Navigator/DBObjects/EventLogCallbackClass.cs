
// Type: Intermech.Navigator.DBObjects.EventLogCallbackClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Security;
using System;


namespace Intermech.Navigator.DBObjects;

internal class EventLogCallbackClass : ISecurityCallback
{
  public IDBSecurity GetSecurity(IUserSession session, object id)
  {
    return session.EventLog as IDBSecurity;
  }

  public int MaintainedCategory => 10;

  public Tuple<int, object> Applicability => (Tuple<int, object>) null;
}
