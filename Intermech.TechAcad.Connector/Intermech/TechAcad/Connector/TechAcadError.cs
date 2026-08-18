// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Connector.TechAcadError
// Assembly: Intermech.TechAcad.Connector, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5A35A651-9A96-41F3-9839-2AAB5A952CB8
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Connector.dll

using System;

#nullable disable
namespace Intermech.TechAcad.Connector;

public class TechAcadError : Exception
{
  public TechAcadError()
  {
  }

  public TechAcadError(string message)
    : base(message)
  {
  }

  public TechAcadError(string message, Exception innerExeption)
    : base(message, innerExeption)
  {
  }
}
