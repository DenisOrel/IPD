// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.UserSessionClientConnectionService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;


namespace Intermech.Kernel;

internal sealed class UserSessionClientConnectionService : IUserSessionClientConnections
{
  private object syncRoot;
  private RNGCryptoServiceProvider rnd;
  private byte[] rndBuffer;
  private HashSet<long> knownValues;

  public UserSessionClientConnectionService()
  {
    this.syncRoot = new object();
    this.rnd = new RNGCryptoServiceProvider();
    this.rndBuffer = new byte[8];
    this.knownValues = new HashSet<long>();
  }

  public long CreateConnectionID()
  {
    lock (this.syncRoot)
    {
      for (int index = 100000; index != 0; --index)
      {
        long randomValue = this.CreateRandomValue();
        if (!this.knownValues.Contains(randomValue))
        {
          this.knownValues.Add(randomValue);
          return randomValue;
        }
      }
      throw new KernelException("Unable to generate a new user session handle.");
    }
  }

  private long CreateRandomValue()
  {
    this.rnd.GetNonZeroBytes(this.rndBuffer);
    long randomValue = BitConverter.ToInt64(this.rndBuffer, 0);
    if (randomValue < 0L)
      randomValue = -randomValue;
    Array.Clear((Array) this.rndBuffer, 0, this.rndBuffer.Length);
    return randomValue;
  }
}
