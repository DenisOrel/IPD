// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.FileBucket
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal sealed class FileBucket : List<string>
{
  public FileBucket(IEnumerable<string> collection)
    : base(collection)
  {
  }

  public FileBucket(int capacity)
    : base(capacity)
  {
  }

  public FileBucket()
  {
  }
}
