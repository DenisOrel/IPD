// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Helpers.OptimizationStatistics
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll


namespace Intermech.Kernel.Helpers;

internal class OptimizationStatistics
{
  public int ReadDuration;
  public int SeekDuration;
  public int WriteDuration;
  public int ReadCounter;
  public int SeekCounter;
  public int WriteCounter;

  public OptimizationStatistics()
  {
    this.ReadDuration = 0;
    this.SeekDuration = 0;
    this.WriteDuration = 0;
    this.ReadCounter = 0;
    this.SeekCounter = 0;
    this.WriteCounter = 0;
  }
}
