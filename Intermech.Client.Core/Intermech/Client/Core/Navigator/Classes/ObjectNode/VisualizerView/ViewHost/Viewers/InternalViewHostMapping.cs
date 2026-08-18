
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.InternalViewHostMapping
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers;

internal static class InternalViewHostMapping
{
  public static List<Tuple<InternalViewerHost, List<string>>> PreviewHostsMapping { get; set; }

  static InternalViewHostMapping()
  {
    InternalViewHostMapping.PreviewHostsMapping = new List<Tuple<InternalViewerHost, List<string>>>();
    InternalViewHostMapping.PreviewHostsMapping.Add(new Tuple<InternalViewerHost, List<string>>((InternalViewerHost) new BrowserInternalViewerHost(), new List<string>()
    {
      "xml",
      "htm",
      "html",
      "config"
    }));
    InternalViewHostMapping.PreviewHostsMapping.Add(new Tuple<InternalViewerHost, List<string>>((InternalViewerHost) new TxtInternalViewerHost(), new List<string>()
    {
      "txt",
      "reg",
      "log",
      "bat",
      "cmd",
      "ini"
    }));
  }
}
