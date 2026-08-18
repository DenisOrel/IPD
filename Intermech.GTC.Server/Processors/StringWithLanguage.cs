// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.Processors.StringWithLanguage
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

#nullable disable
namespace Intermech.GTC.Server.Processors;

internal class StringWithLanguage
{
  public StringWithLanguage(string alanguage, string avalue)
  {
    this.Language = alanguage;
    this.Value = avalue;
  }

  public string Language { get; set; }

  public string Value { get; set; }
}
