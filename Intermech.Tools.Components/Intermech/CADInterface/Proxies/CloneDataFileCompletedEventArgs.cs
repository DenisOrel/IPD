// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.CloneDataFileCompletedEventArgs
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Intermech.CADInterface.Proxies;

public class CloneDataFileCompletedEventArgs : EventArgs
{
  public CloneDataFileCompletedEventArgs(CloneDataFileProxy file)
  {
    this.File = file != null ? file : throw new ArgumentNullException(nameof (file));
    this.Result = true;
  }

  public CloneDataFileProxy File { get; private set; }

  public bool Result { get; set; }
}
