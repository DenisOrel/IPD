
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.Interfaces.IDistanceMeasureSupport
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.Interfaces;

/// <summary>Интерфейс измерения расстояния в просмотровщике</summary>
public interface IDistanceMeasureSupport
{
  void RedDistanceMeasure();

  bool RedDistanceMeasureEnabled();

  bool RedDistanceMeasureChecked();

  event EventHandler DistanceMeasureStateChanged;
}
