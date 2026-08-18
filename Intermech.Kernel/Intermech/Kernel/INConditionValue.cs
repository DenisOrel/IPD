// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.INConditionValue
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System;


namespace Intermech.Kernel;

public class INConditionValue
{
  public long SelectKey { get; internal set; }

  public bool IsInsertData { get; internal set; }

  public string TmpTableName { get; internal set; }

  public Array Values { get; set; }

  public INConditionValue() => this.IsInsertData = false;
}
