// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.CADInterfaceValueBagContainer
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.CADInterface.Proxies;

public class CADInterfaceValueBagContainer : IValueBagContainer
{
  private IParametersContainerProxy cadInterfaceObject;

  public CADInterfaceValueBagContainer(IParametersContainerProxy cadInterfaceObject)
  {
    this.cadInterfaceObject = cadInterfaceObject != null ? cadInterfaceObject : throw new ArgumentNullException(nameof (cadInterfaceObject));
  }

  public IParametersContainerProxy CADInterfaceObject
  {
    [DebuggerStepThrough] get => this.cadInterfaceObject;
  }
}
