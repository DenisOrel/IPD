// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Interfaces.BackgroundTaskMessage
// Assembly: Intermech.GTC.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 767EAE12-F30F-454C-81D0-2862AEDD13C4
// Assembly location: D:\IPS\Client\Intermech.GTC.Interfaces.dll

using System;

#nullable disable
namespace Intermech.GTC.Interfaces;

[Serializable]
public class BackgroundTaskMessage
{
  public string Message { get; set; }

  public Exception Exception { get; set; }

  public BackgroundTaskMessage(string message) => this.Message = message;
}
