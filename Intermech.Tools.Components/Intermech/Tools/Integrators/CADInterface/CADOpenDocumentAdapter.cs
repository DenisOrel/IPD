// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADOpenDocumentAdapter
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Data;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public class CADOpenDocumentAdapter : IOpenDocument
{
  private CADDocumentProxy document;
  private IValueBagContainer propertiesContainer;

  public CADOpenDocumentAdapter(CADDocumentProxy document)
  {
    this.document = document != null ? document : throw new ArgumentNullException(nameof (document));
  }

  public CADDocumentProxy Document => this.document;

  public IValueBagContainer Properties
  {
    get
    {
      if (this.propertiesContainer == null)
        this.propertiesContainer = (IValueBagContainer) CADInterfaceAdapters.AsValueBagContainer(this.document);
      return this.propertiesContainer;
    }
  }
}
