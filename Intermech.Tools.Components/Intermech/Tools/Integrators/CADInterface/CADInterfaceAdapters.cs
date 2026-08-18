// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADInterfaceAdapters
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Data;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public static class CADInterfaceAdapters
{
  private const string ValueBagContainerTag = "ValueBagContainerAdapter";
  private const string OpenDocumentTag = "OpenDocumentAdapter";

  public static CADInterfaceValueBagContainer AsValueBagContainer(CADDocumentProxy document)
  {
    CADInterfaceValueBagContainer valueBagContainer = document != null ? (CADInterfaceValueBagContainer) document.Tags.TryGet("ValueBagContainerAdapter") : throw new ArgumentNullException(nameof (document));
    if (valueBagContainer == null)
    {
      valueBagContainer = new CADInterfaceValueBagContainer((IParametersContainerProxy) document);
      document.Tags.Set("ValueBagContainerAdapter", (object) valueBagContainer);
    }
    return valueBagContainer;
  }

  public static CADOpenDocumentAdapter AsOpenDocument(CADDocumentProxy document)
  {
    CADOpenDocumentAdapter openDocumentAdapter = document != null ? (CADOpenDocumentAdapter) document.Tags.TryGet("OpenDocumentAdapter") : throw new ArgumentNullException(nameof (document));
    if (openDocumentAdapter == null)
    {
      openDocumentAdapter = new CADOpenDocumentAdapter(document);
      document.Tags.Set("OpenDocumentAdapter", (object) openDocumentAdapter);
    }
    return openDocumentAdapter;
  }

  public static IValueBagContainer AsValueBagContainer(ModelConfigurationProxy configuration)
  {
    CADInterfaceValueBagContainer valueBagContainer = configuration != null ? (CADInterfaceValueBagContainer) configuration.Tags.TryGet("ValueBagContainerAdapter") : throw new ArgumentNullException(nameof (configuration));
    if (valueBagContainer == null)
    {
      valueBagContainer = new CADInterfaceValueBagContainer((IParametersContainerProxy) configuration);
      configuration.Tags.Set("ValueBagContainerAdapter", (object) valueBagContainer);
    }
    return (IValueBagContainer) valueBagContainer;
  }
}
