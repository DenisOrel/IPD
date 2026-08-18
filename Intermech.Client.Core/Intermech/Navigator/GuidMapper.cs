
// Type: Intermech.Navigator.GuidMapper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;


namespace Intermech.Navigator;

public class GuidMapper : IGuidMapper
{
  private Hashtable _guidKeys = new Hashtable();
  private Hashtable _cookieKeys = new Hashtable();
  private int _lastCookie = 1000;

  public int Register(Guid Guid)
  {
    if (this._guidKeys.Contains((object) Guid))
      throw new ApplicationException($"{sc_4561.ssp_imclient_4562()}{Guid.ToString()} already registered!");
    ++this._lastCookie;
    this._guidKeys.Add((object) Guid, (object) this._lastCookie);
    this._cookieKeys.Add((object) this._lastCookie, (object) Guid);
    return this._lastCookie;
  }

  public int Register(Guid guid, int cookie)
  {
    if (this._guidKeys.Contains((object) guid))
      throw new ApplicationException($"{sc_4561.ssp_imclient_4563()}{guid.ToString()} already registered!");
    if (this._cookieKeys.Contains((object) cookie))
      throw new AbortException($"{sc_4561.ssp_imclient_4564()}{cookie.ToString()} already in use!");
    this._guidKeys.Add((object) guid, (object) cookie);
    this._cookieKeys.Add((object) cookie, (object) guid);
    if (this._lastCookie < cookie)
      this._lastCookie = cookie;
    return cookie;
  }

  public void Unregister(int Cookie)
  {
    Guid key = this._cookieKeys.Contains((object) Cookie) ? (Guid) this._cookieKeys[(object) Cookie] : throw new Exception($"{sc_4561.ssp_imclient_4565()}{Cookie.ToString()} is not registered!");
    this._cookieKeys.Remove((object) Cookie);
    this._guidKeys.Remove((object) key);
  }

  public int this[Guid Guid]
  {
    get => this._guidKeys.Contains((object) Guid) ? (int) this._guidKeys[(object) Guid] : 0;
  }

  public Guid this[int Cookie]
  {
    get
    {
      return this._cookieKeys.Contains((object) Cookie) ? (Guid) this._cookieKeys[(object) Cookie] : Guid.Empty;
    }
  }
}
