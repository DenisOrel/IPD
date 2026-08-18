
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.ViewerFactory
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.Interfaces;
using Intermech.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost;

internal class ViewerFactory
{
  private static ViewerFactory _instance;
  private IDictionary<StyleView, Type> _viewerStyleTypes = (IDictionary<StyleView, Type>) new ConcurrentDictionary<StyleView, Type>();

  private ViewerFactory()
  {
  }

  public static ViewerFactory Instance
  {
    get => ViewerFactory._instance = ViewerFactory._instance ?? new ViewerFactory();
  }

  public void Register(StyleView styleView, Type viewerType)
  {
    this._viewerStyleTypes[styleView] = ((IEnumerable<Type>) viewerType.GetInterfaces()).Contains<Type>(typeof (IViewer)) ? viewerType : throw new ArgumentException($"{viewerType} type not implemented IViewerStrategy");
  }

  public void Unregister(StyleView styleView) => this._viewerStyleTypes.Remove(styleView);

  public IViewer Create(StyleView styleView)
  {
    Type type;
    return this._viewerStyleTypes.TryGetValue(styleView, out type) ? Activator.CreateInstance(type) as IViewer : (IViewer) null;
  }
}
