// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADDocument
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using Intermech.Data;
using Intermech.IO;
using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADDocument : IOpenDocument
{
  private readonly AddInProxy _adProxy;

  public ADDocument(AddInProxy adProxy, string fullPath)
  {
    this._adProxy = adProxy;
    this.FullPath = fullPath;
    string firstPath = Path.GetExtension(this.FullPath);
    this.DocumentType = ADDocumentType.Unknown;
    if (string.IsNullOrEmpty(firstPath))
      return;
    if (PathUtils.IsSamePath(firstPath, ".PrjPcb"))
      this.DocumentType = ADDocumentType.Project;
    else if (PathUtils.IsSamePath(firstPath, ".SchDoc"))
    {
      this.DocumentType = ADDocumentType.SCH;
    }
    else
    {
      if (!PathUtils.IsSamePath(firstPath, ".PcbDoc"))
        return;
      this.DocumentType = ADDocumentType.PCB;
    }
  }

  public string FullPath { get; }

  public IValueBagContainer Properties
  {
    get
    {
      IParametrable parametrable = (IParametrable) null;
      switch (this.DocumentType)
      {
        case ADDocumentType.Project:
          parametrable = (IParametrable) ApiHelper.GetProject(this._adProxy.AddIn, this.FullPath);
          break;
        case ADDocumentType.SCH:
          parametrable = (IParametrable) ApiHelper.GetSchDocument(this._adProxy.AddIn, this.FullPath, false);
          break;
        case ADDocumentType.PCB:
          parametrable = (IParametrable) ApiHelper.GetPCBDocument(this._adProxy.AddIn, this.FullPath);
          break;
      }
      return parametrable != null ? (IValueBagContainer) new ParametersContainer(parametrable) : throw new Exception($"Файл {this.FullPath} интегратором не обрабатывается");
    }
  }

  public ADDocumentType DocumentType { get; }

  public List<ADDocument> AdditionalDocuments { get; set; }
}
