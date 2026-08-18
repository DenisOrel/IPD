
// Type: Intermech.Navigator.CacheManager
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using System;
using System.Collections;


namespace Intermech.Navigator;

public sealed class CacheManager
{
  private static Hashtable _caches = new Hashtable();

  public static void Register(string name, ICache cache)
  {
    if (CacheManager._caches.ContainsKey((object) name))
      throw new ApplicationException($"{sc_3282.ssp_imclient_3283()}{name} already registered!");
    CacheManager._caches[(object) name] = (object) cache;
  }

  public static ICache Cache(string name) => (ICache) CacheManager._caches[(object) name];
}
