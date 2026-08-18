
// Type: Intermech.Search.GlobalNodes.StandardGlobalNodeRegistry
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.GlobalNodes;

public sealed class StandardGlobalNodeRegistry : IGlobalNodeRegistry
{
  private Dictionary<Guid, Tuple<IDescriptor, int, int>> _map = new Dictionary<Guid, Tuple<IDescriptor, int, int>>();

  public DescriptorCollection CreateDescriptorCollection()
  {
    return new DescriptorCollection(this._map.OrderBy<KeyValuePair<Guid, Tuple<IDescriptor, int, int>>, int>((Func<KeyValuePair<Guid, Tuple<IDescriptor, int, int>>, int>) (o => o.Value.Item2)).ThenBy<KeyValuePair<Guid, Tuple<IDescriptor, int, int>>, int>((Func<KeyValuePair<Guid, Tuple<IDescriptor, int, int>>, int>) (o => o.Value.Item3)).Select<KeyValuePair<Guid, Tuple<IDescriptor, int, int>>, Tuple<Guid, IDescriptor>>((Func<KeyValuePair<Guid, Tuple<IDescriptor, int, int>>, Tuple<Guid, IDescriptor>>) (o => new Tuple<Guid, IDescriptor>(o.Key, o.Value.Item1))));
  }

  public void RegisterGlobalNode(Guid descriptorGuid, IDescriptor descriptor, int order)
  {
    if (descriptorGuid == Guid.Empty)
      throw new ArgumentException();
    if (descriptor == null)
      throw new ArgumentNullException(nameof (descriptor));
    lock (this._map)
      this._map.Add(descriptorGuid, new Tuple<IDescriptor, int, int>(descriptor, order, this._map.Count));
  }
}
