
// Type: Intermech.Search.NotificationSelections.NSDifferencesDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.NotifySamples;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.VirtualNodes;
using System;


namespace Intermech.Search.NotificationSelections;

public sealed class NSDifferencesDescriptor : HiveDescriptor
{
  private NSDifferences _nsDifferences;

  public NSDifferencesDescriptor(NSDifferences nsDifferences, string caption)
    : base(Intermech.Navigator.Consts.NotificationSelectionsCategoryID, 0, caption)
  {
    this._nsDifferences = nsDifferences != null ? nsDifferences : throw new ArgumentNullException();
  }

  public override INode GetChild(INodeID nodeID)
  {
    return (INode) new NSDifferencesNode(this._nsDifferences);
  }
}
