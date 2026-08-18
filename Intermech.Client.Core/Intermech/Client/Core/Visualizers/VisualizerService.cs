
// Type: Intermech.Client.Core.Visualizers.VisualizerService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Client.Core.Visualizers;

internal class VisualizerService : IVisualizerService
{
  private Hashtable _visualizers = new Hashtable();

  public void AddVisualizer(string fileExt, IVisualizer visualizer)
  {
    this._visualizers[(object) fileExt] = (object) visualizer;
  }

  public IVisualizer GetVisualizer(string fileExt)
  {
    return this._visualizers[(object) fileExt] as IVisualizer;
  }

  public List<string> SupportedExtensions()
  {
    return this._visualizers.Keys.Cast<string>().ToList<string>();
  }

  private static bool CanViewInBrowser(string fileExt)
  {
    return fileExt == "ipt" || fileExt == "iam" || fileExt == "idw" || fileExt == "dwf" || fileExt == "txt";
  }
}
