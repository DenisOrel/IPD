// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADProject
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using Intermech.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADProject(IADProject project) : 
  TypedParametersContainer<IADProject>(project),
  IADProject,
  IParametrable,
  IValueBagContainer,
  IIdentification,
  IFileDocument,
  IDisposable
{
  public List<DocumentInfo> GeneratedDocuments => this.Instance.GeneratedDocuments;

  public int VariantsCount => this.Instance.VariantsCount;

  public string FilePath => this.Instance.FilePath;

  public List<DocumentInfo> GetDocuments(bool leaveDocsOpen)
  {
    return this.Instance.GetDocuments(leaveDocsOpen);
  }

  public IVariant GetVariant(int index)
  {
    return (IVariant) new ADVariant(this.Instance.GetVariant(index));
  }
}
