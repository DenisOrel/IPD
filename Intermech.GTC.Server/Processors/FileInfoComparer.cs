// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.Processors.FileInfoComparer
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.GTC.Server.Processors;

internal class FileInfoComparer : IEqualityComparer<Tuple<string, string>>
{
  public bool Equals(Tuple<string, string> x, Tuple<string, string> y)
  {
    if (x == y)
      return true;
    return x != null && y != null && x.Item1 == y.Item1;
  }

  public int GetHashCode(Tuple<string, string> fileInfo)
  {
    return fileInfo == null || fileInfo.Item1 == null ? 0 : fileInfo.Item1.GetHashCode();
  }
}
