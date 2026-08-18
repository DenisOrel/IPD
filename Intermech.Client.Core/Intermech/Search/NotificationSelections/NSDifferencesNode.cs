
// Type: Intermech.Search.NotificationSelections.NSDifferencesNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.NotifySamples;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.NotificationSelections;

public sealed class NSDifferencesNode : CompositeNode
{
  private NSDifferences _nsDifferences;

  public NSDifferencesNode(NSDifferences nsDifferences)
  {
    this._nsDifferences = nsDifferences != null ? nsDifferences : throw new ArgumentNullException(nameof (nsDifferences));
  }

  protected override List<PartSlot> CreateFolderSlots()
  {
    DescriptorCollection descriptors = new DescriptorCollection();
    if (this._nsDifferences.IncludedObjects != null)
    {
      List<long> source = new List<long>((IEnumerable<long>) this._nsDifferences.IncludedObjects);
      source.AddRange(((IEnumerable<long>) this._nsDifferences.IncludedObjects).Select<long, long>((Func<long, long>) (o => -o)));
      descriptors.Add((IDescriptor) new IncludedObjectsDescriptor(source.Distinct<long>().ToArray<long>()));
    }
    if (this._nsDifferences.ExcludedObjects != null)
    {
      List<long> source = new List<long>((IEnumerable<long>) this._nsDifferences.ExcludedObjects);
      source.AddRange(((IEnumerable<long>) this._nsDifferences.ExcludedObjects).Select<long, long>((Func<long, long>) (o => -o)));
      descriptors.Add((IDescriptor) new ExcludedObjectsDescriptor(source.Distinct<long>().ToArray<long>()));
    }
    return this.SlotsFromSinglePart((INodePart) new DescriptorsPart(descriptors, false));
  }
}
