
// Type: Intermech.Navigator.StringMapper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;


namespace Intermech.Navigator;

internal class StringMapper : IStringMapper
{
  private Hashtable _stringKeys = new Hashtable();
  private Hashtable _cookieKeys = new Hashtable();
  private int _lastCookie;

  public int Register(string value)
  {
    if (this._stringKeys.Contains((object) value))
      throw new Exception($"{sc_4573.ssp_imclient_4574()}{value}\" already registered!");
    ++this._lastCookie;
    this._stringKeys.Add((object) value, (object) this._lastCookie);
    this._cookieKeys.Add((object) this._lastCookie, (object) value);
    return this._lastCookie;
  }

  public void Unregister(int cookie)
  {
    string key = this._cookieKeys.Contains((object) cookie) ? (string) this._cookieKeys[(object) cookie] : throw new Exception($"{sc_4573.ssp_imclient_4575()}{cookie.ToString()} is not registered!");
    this._cookieKeys.Remove((object) cookie);
    this._stringKeys.Remove((object) key);
  }

  public int this[string value]
  {
    get => this._stringKeys.Contains((object) value) ? (int) this._stringKeys[(object) value] : 0;
  }

  public string this[int cookie]
  {
    get
    {
      return this._cookieKeys.Contains((object) cookie) ? (string) this._cookieKeys[(object) cookie] : "";
    }
  }
}
