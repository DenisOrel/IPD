// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportStore
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportStore : IDisposable
{
  public List<AttributeTypePossibleValues> PossibleValuesAttributeType { get; set; }

  public List<SaveImportValues> DefaultValueObjectLink { get; set; }

  public List<SaveImportValues> MeasureValueObjectLink { get; set; }

  public ImportStore()
  {
    this.PossibleValuesAttributeType = new List<AttributeTypePossibleValues>();
    this.DefaultValueObjectLink = new List<SaveImportValues>();
    this.MeasureValueObjectLink = new List<SaveImportValues>();
  }

  public void Dispose()
  {
    this.DefaultValueObjectLink = (List<SaveImportValues>) null;
    this.PossibleValuesAttributeType = (List<AttributeTypePossibleValues>) null;
    this.MeasureValueObjectLink = (List<SaveImportValues>) null;
  }
}
