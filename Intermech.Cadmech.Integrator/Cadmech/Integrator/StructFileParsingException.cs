// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.StructFileParsingException
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using System;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class StructFileParsingException : StructFileException
{
  private static readonly string MessagePrefix = Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_24");

  public StructFileParsingException(string message)
    : base(StructFileParsingException.MessagePrefix + message)
  {
  }

  public StructFileParsingException(string message, bool keepMessageUnchanged)
    : base(keepMessageUnchanged ? message : StructFileParsingException.MessagePrefix + message)
  {
  }

  public StructFileParsingException(string message, Exception innerException)
    : base(StructFileParsingException.MessagePrefix + message, innerException)
  {
  }
}
