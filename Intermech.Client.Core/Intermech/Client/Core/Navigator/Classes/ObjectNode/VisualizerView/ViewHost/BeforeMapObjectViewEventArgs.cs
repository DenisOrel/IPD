
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.BeforeMapObjectViewEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Map;
using Intermech.Navigator.Interfaces;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost;

public class BeforeMapObjectViewEventArgs : NotificationEventArgs
{
  public static readonly string BeforeMapObjectViewEvent = nameof (BeforeMapObjectViewEvent);

  public BeforeMapObjectViewEventArgs(MapObject mapObject, ISelectedItems items)
    : base(BeforeMapObjectViewEventArgs.BeforeMapObjectViewEvent)
  {
    this.MapObject = mapObject;
    this.SelectedItems = items;
  }

  public MapObject MapObject { get; }

  public ISelectedItems SelectedItems { get; }
}
