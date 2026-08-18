// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.StructFileException
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using System;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class StructFileException : FaultException
{
  public StructFileException(string message)
    : base(message)
  {
  }

  public StructFileException(string message, Exception innerException)
    : base(message, innerException)
  {
  }
}
