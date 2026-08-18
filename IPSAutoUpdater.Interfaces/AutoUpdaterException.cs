// Decompiled with JetBrains decompiler
// Type: IPSAutoUpdater.Interfaces.AutoUpdaterException
// Assembly: IPSAutoUpdater.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 74369E9B-3C90-46D5-99C8-30597004F5A5
// Assembly location: D:\IPS\Client\IPSAutoUpdater.Interfaces.dll

using System;
using System.Runtime.Serialization;


namespace IPSAutoUpdater.Interfaces;

[Serializable]
public class AutoUpdaterException : Exception
{
  public AutoUpdaterException()
  {
  }

  public AutoUpdaterException(string msg)
    : base(msg)
  {
  }

  public AutoUpdaterException(SerializationInfo si, StreamingContext sc)
    : base(si, sc)
  {
  }

  public AutoUpdaterException(string msg, Exception e)
    : base(msg, e)
  {
  }
}
