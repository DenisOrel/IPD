// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportObjectTypesStore
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportObjectTypesStore : IDisposable
{
  public List<TypeFormules> AttributeFormules = new List<TypeFormules>();
  public List<Tuple<int, int>> CaptionAttributes = new List<Tuple<int, int>>();

  public ImportObjectTypesStore()
  {
    this.AttributeFormules = new List<TypeFormules>();
    this.CaptionAttributes = new List<Tuple<int, int>>();
  }

  public void Dispose()
  {
    this.AttributeFormules = (List<TypeFormules>) null;
    this.CaptionAttributes = (List<Tuple<int, int>>) null;
  }
}
