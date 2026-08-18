// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Server.CacheHelper
// Assembly: Intermech.FormDesigner.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ABD17B9B-52A2-4551-9041-386497DBE670
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.FormDesigner.Server.dll

using Intermech.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.FormDesigner.Server;

internal class CacheHelper
{
  public class CacheBaseItem<T>
  {
    protected FormInformation _formInfo;
    protected T _value;

    public FormInformation FormInfo
    {
      [DebuggerStepThrough] get => this._formInfo;
      set => this._formInfo = value;
    }

    public T Value
    {
      [DebuggerStepThrough] get => this._value;
      set => this._value = value;
    }

    public CacheBaseItem(FormInformation formInfo, T value)
    {
      this._formInfo = formInfo;
      this._value = value;
    }
  }

  public class CacheBaseItems<T> : ConcurrentDictionary<long, CacheHelper.CacheBaseItem<T>>
  {
    public CacheBaseItems()
    {
    }

    public CacheBaseItems(int capacity)
    {
    }

    public CacheBaseItems(CacheHelper.CacheBaseItems<T> dictionary)
      : base((IEnumerable<KeyValuePair<long, CacheHelper.CacheBaseItem<T>>>) dictionary)
    {
    }

    public CacheBaseItems(
      Dictionary<long, CacheHelper.CacheBaseItem<T>> dictionary)
      : base((IEnumerable<KeyValuePair<long, CacheHelper.CacheBaseItem<T>>>) dictionary)
    {
    }
  }
}
