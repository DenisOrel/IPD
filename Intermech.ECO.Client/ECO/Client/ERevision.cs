// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ERevision
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using System;

#nullable disable
namespace Intermech.ECO.Client;

public class ERevision : Exception
{
  public long objID;

  public ERevision(long objID, string Message)
    : base(Message)
  {
    this.objID = objID;
  }
}
