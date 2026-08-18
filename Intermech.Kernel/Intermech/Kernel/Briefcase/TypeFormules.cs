// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.TypeFormules
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Briefcase;

internal sealed class TypeFormules
{
  public Dictionary<int, string> Formules;

  public Guid TypeGuid { get; private set; }

  public TypeFormules(Guid objectTypeGuid)
  {
    this.TypeGuid = objectTypeGuid;
    this.Formules = new Dictionary<int, string>();
  }
}
