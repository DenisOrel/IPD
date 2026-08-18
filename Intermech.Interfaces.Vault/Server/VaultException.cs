// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.Server.VaultException
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Vault.Interfaces.Server;

[Serializable]
public class VaultException : Exception
{
  public VaultException()
  {
  }

  public VaultException(string message)
    : base(message)
  {
    ApplicationEventLog.Log.Error((object) message);
  }

  public VaultException(string message, Exception ex)
    : base(message)
  {
    ApplicationEventLog.Log.Error((object) message, ex);
  }

  protected VaultException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
