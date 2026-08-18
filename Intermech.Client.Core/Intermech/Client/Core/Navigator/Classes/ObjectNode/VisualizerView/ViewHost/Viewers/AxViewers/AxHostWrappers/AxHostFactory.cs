
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers.AxHostFactory
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.AxViewers.AxHostWrappers;

internal class AxHostFactory
{
  private static AxHostFactory _instance;
  private IDictionary<Guid, Type> _axHostTypes = (IDictionary<Guid, Type>) new ConcurrentDictionary<Guid, Type>();

  private AxHostFactory()
  {
  }

  public static AxHostFactory Instance
  {
    get => AxHostFactory._instance = AxHostFactory._instance ?? new AxHostFactory();
  }

  public void Register(Guid clsid, Type axHostType)
  {
    this._axHostTypes[clsid] = ((IEnumerable<Type>) axHostType.GetInterfaces()).Contains<Type>(typeof (IAxHost)) ? axHostType : throw new ArgumentException($"{typeof (IAxHost)} type implemented expected");
  }

  public void Unregister(Guid clsid) => this._axHostTypes.Remove(clsid);

  public IAxHost Create(Guid clsId)
  {
    Type type;
    if (!this._axHostTypes.TryGetValue(clsId, out type))
      type = typeof (DefaultAxHost);
    return Activator.CreateInstance(type, (object) clsId.ToString()) as IAxHost;
  }
}
