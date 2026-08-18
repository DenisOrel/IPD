// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.Params.CommonParams.SourceDBParams
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Imbase.Params.CommonParams;

[Serializable]
public class SourceDBParams
{
  public BaseType BaseType { get; set; }

  public string UserName { get; set; } = string.Empty;

  public string Password { get; set; } = string.Empty;

  public string ServerName { get; set; } = string.Empty;

  public string DataBaseName { get; set; } = string.Empty;

  public override string ToString() => string.Empty;
}
