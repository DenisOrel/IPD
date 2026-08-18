
// Type: Intermech.CacheServices.CacheServices
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Specialized;


namespace Intermech.CacheServices;

public class CacheServices : ICacheServices
{
  private IDictionary services = (IDictionary) new HybridDictionary();

  public void AddService(string name, ICacheService service)
  {
    Intermech.CacheServices.CacheServices.Check(name);
    Intermech.CacheServices.CacheServices.Check(service);
    lock (this.services)
    {
      if (this.services.Contains((object) name))
        throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString(sc_2316.ssp_imclient_2317()), (object) name));
      this.services.Add((object) name, (object) service);
    }
  }

  public void RemoveService(string name)
  {
    Intermech.CacheServices.CacheServices.Check(name);
    lock (this.services)
    {
      if (!this.services.Contains((object) name))
        return;
      this.services.Remove((object) name);
    }
  }

  public ICacheService GetService(string name)
  {
    Intermech.CacheServices.CacheServices.Check(name);
    lock (this.services)
      return (ICacheService) this.services[(object) name];
  }

  public string[] Names
  {
    get
    {
      lock (this.services)
      {
        string[] names = new string[this.services.Keys.Count];
        this.services.Keys.CopyTo((Array) names, 0);
        return names;
      }
    }
  }

  private static void Check(string name)
  {
    if (name == null)
      throw new ArgumentNullException(sc_2316.ssp_imclient_2318(), LocalizationHolder.rm.GetString("Client.Core_2"));
    if (name == string.Empty)
      throw new ArgumentException(LocalizationHolder.rm.GetString(sc_2316.ssp_imclient_2319()));
  }

  private static void Check(ICacheService service)
  {
    if (service == null)
      throw new ArgumentNullException(sc_2316.ssp_imclient_2320(), LocalizationHolder.rm.GetString("Client.Core_4"));
  }
}
