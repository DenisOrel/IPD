// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.Processors.LanguageTranslation
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

#nullable disable
namespace Intermech.GTC.Server.Processors;

internal class LanguageTranslation
{
  public LanguageTranslation(string language, string value)
  {
    this.Language = language;
    this.Value = value;
  }

  public string Language { get; private set; }

  public string Value { get; private set; }
}
