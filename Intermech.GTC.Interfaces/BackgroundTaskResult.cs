// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Interfaces.BackgroundTaskResult
// Assembly: Intermech.GTC.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 767EAE12-F30F-454C-81D0-2862AEDD13C4
// Assembly location: D:\IPS\Client\Intermech.GTC.Interfaces.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.GTC.Interfaces;

[Serializable]
public class BackgroundTaskResult
{
  public List<BackgroundTaskMessage> Messages { get; set; }

  public List<long> ChangedObjects { get; set; }

  public List<long> CreatedObjects { get; set; }

  public BackgroundTaskResult()
  {
    this.Messages = new List<BackgroundTaskMessage>();
    this.ChangedObjects = new List<long>();
    this.CreatedObjects = new List<long>();
  }
}
