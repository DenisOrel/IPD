// Decompiled with JetBrains decompiler
// Type: IMLauncher.ProgrammInfo
// Assembly: IMLauncher, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DAC2135C-3212-4DE0-9552-DF99FF4FD793
// Assembly location: D:\IPS\Client\IMLauncher.exe

#nullable disable
namespace IMLauncher;

internal class ProgrammInfo
{
  private string programmName;
  private string[] programmPaths;
  private string ipsVersion;
  private string imBaseLibrary;
  private string programmArguments;
  private AdditionalInfo flags;

  public AdditionalInfo Flags
  {
    get => this.flags;
    set => this.flags = value;
  }

  public string ProgrammName
  {
    get => this.programmName;
    set => this.programmName = value;
  }

  public string[] ProgrammPaths
  {
    get => this.programmPaths;
    set => this.programmPaths = value;
  }

  public string IPSVersion
  {
    get => this.ipsVersion;
    set => this.ipsVersion = value;
  }

  public string IMBaseLibrary
  {
    get => this.imBaseLibrary;
    set => this.imBaseLibrary = value;
  }

  public string ProgrammArguments
  {
    get => this.programmArguments;
    set => this.programmArguments = value;
  }

  public ProgrammInfo(string name, string[] paths, string imLibrary, AdditionalInfo info)
  {
    this.programmName = name;
    this.programmPaths = paths;
    this.imBaseLibrary = imLibrary;
    this.programmArguments = string.Empty;
    this.flags = info;
  }

  public ProgrammInfo(string name, string[] paths, string imLibrary)
    : this(name, paths, imLibrary, AdditionalInfo.None, string.Empty)
  {
  }

  public ProgrammInfo(
    string name,
    string[] paths,
    string imLibrary,
    AdditionalInfo info,
    string arguments)
    : this(name, paths, imLibrary, info)
  {
    this.flags = info;
    this.programmArguments = arguments;
  }
}
