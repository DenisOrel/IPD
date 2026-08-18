// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.ZoneManagerProxy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.Data;
using Intermech.Tools.Data;
using Interop.CADInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.CADInterface.Proxies;

public sealed class ZoneManagerProxy(CADSystemProxy appProxy) : StructureManagerProxyBase(appProxy)
{
  public List<ZoneRecord> GetZones(CADDocumentProxy appDrawing)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<CADDocumentProxy>("ZoneManagerProxy.GetZones()", appDrawing);
    if (appDrawing == null)
      throw new ArgumentNullException(nameof (appDrawing));
    StructureElement[] structureForAvs = this.sm.GetStructureForAVS(appDrawing.RawObject);
    if (structureForAvs.Length != 0 && CADInterfaceTracing.Proxies.TraceVerbose)
      Trace.WriteLine($"Document: {appDrawing.FullName}");
    List<ZoneRecord> zones = new List<ZoneRecord>(structureForAvs.Length);
    foreach (IStructureElement structureElement in structureForAvs)
    {
      if (!structureElement.ExcludedFromSpec)
      {
        Guid structureElementGuid = this.ParseStructureElementGuid(structureElement);
        foreach (ZoneData decodeZone in (IEnumerable<ZoneData>) this.DecodeZones(structureElement, appDrawing))
          zones.Add(new ZoneRecord()
          {
            OccurenceGuid = structureElementGuid,
            Zone = decodeZone.Zone
          });
      }
    }
    return zones;
  }

  private ICollection<ZoneData> DecodeZones(
    IStructureElement structElem,
    CADDocumentProxy appDrawing)
  {
    LinkedList<ZoneData> linkedList = new LinkedList<ZoneData>();
    if (structElem.SameForAllConfigurations)
    {
      IStructureModification commonVariant = (IStructureModification) structElem.GetCommonVariant();
      linkedList.AddLast(this.DecodeZones((ModelConfigurationProxy) null, commonVariant.GetNotes()));
    }
    else
    {
      foreach (StructureModification modification in structElem.GetModifications())
      {
        ModelConfigurationProxy configurationWrapper = this.CreateAssemblyConfigurationWrapper(modification.GetAssemblyConfiguration(), appDrawing);
        linkedList.AddLast(this.DecodeZones(configurationWrapper, modification.GetNotes()));
      }
    }
    return (ICollection<ZoneData>) linkedList;
  }

  private ZoneData DecodeZones(ModelConfigurationProxy project, PositionNote[] positionNotes)
  {
    if (positionNotes != null)
    {
      OrderedList<string> values = new OrderedList<string>(positionNotes.Length, (IComparer<string>) StringComparer.CurrentCultureIgnoreCase);
      foreach (PositionNote positionNote in positionNotes)
      {
        if (positionNote != null)
        {
          string str = this.DecodeZone((IPositionNote) positionNote);
          if (!string.IsNullOrEmpty(str))
            values.Add(str);
        }
      }
      if (values.Count > 0)
      {
        string zone = string.Join(",", (IEnumerable<string>) values);
        return new ZoneData(project, zone);
      }
    }
    return new ZoneData(project, "");
  }

  private string DecodeZone(IPositionNote posNote)
  {
    return this.propFormatter.Read((IValueBagContainer) new CADInterfaceValueBagContainer((IParametersContainerProxy) this.CreateParametersWrapper((IParametersContainer) posNote)), (ICollection<StringKey>) new StringKey[1]
    {
      (StringKey) IDCache.Default.Zone.Text
    }).Bag.Read<string>((StringKey) IDCache.Default.Zone.Text, (string) null);
  }
}
