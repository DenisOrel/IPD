// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Attribute4Props
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll


namespace Intermech.Kernel;

public class Attribute4Props
{
  public string[] Tables;
  public OptimizationModes Mode;
  public AttributeOptions Options;

  public Attribute4Props(OptimizationModes mode, AttributeOptions options)
  {
    this.Mode = mode;
    this.Options = options;
  }

  public Attribute4Props(OptimizationModes mode, string[] tables, AttributeOptions options)
  {
    this.Mode = mode;
    this.Tables = tables;
    this.Options = options;
  }
}
