// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.PDMSystemException
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal class PDMSystemException : FaultException
{
  public PDMSystemException(string message)
    : base(message)
  {
  }

  public PDMSystemException(string message, Exception innerException)
    : base(message, innerException)
  {
  }
}
