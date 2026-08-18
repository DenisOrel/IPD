// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.P21.EntityObjects
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using Intermech.GTC.Interfaces;
using Intermech.GTC.Interfaces.Entities;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.GTC.Server.P21;

public class EntityObjects : IEntityObjects
{
  private Dictionary<string, IBaseObject> _objDictionary = new Dictionary<string, IBaseObject>();

  public Dictionary<string, IBaseObject> ObjectCashe => this._objDictionary;

  public IBaseObject Get(string key)
  {
    if (key.Equals(string.Empty) || key.Length < 2 || !key[0].Equals('#'))
      return (IBaseObject) null;
    IBaseObject baseObject;
    if (!this._objDictionary.TryGetValue(key, out baseObject))
      throw new Exception($"Не найден объект с ключем '{key}'");
    return baseObject;
  }

  public void SetEntitiesData(string[] arr)
  {
    foreach (string str1 in arr)
    {
      int length1 = str1.IndexOf('=');
      string key = str1.Substring(0, length1).Trim();
      string str2 = str1.Substring(length1 + 1, str1.Length - length1 - 1);
      int length2 = str2.IndexOf('(');
      string typestr = str2.Substring(0, length2).Trim();
      string str3 = str2.Substring(length2 + 1, str2.Length - length2 - 2);
      string keyStr = key;
      string paramStr = str3;
      IBaseObject baseObject = (IBaseObject) Factory.Create(typestr, keyStr, paramStr);
      this._objDictionary.Add(key, baseObject);
    }
    foreach (KeyValuePair<string, IBaseObject> keyValuePair in this.ObjectCashe)
      keyValuePair.Value.SetParams((IEntityObjects) this);
  }
}
