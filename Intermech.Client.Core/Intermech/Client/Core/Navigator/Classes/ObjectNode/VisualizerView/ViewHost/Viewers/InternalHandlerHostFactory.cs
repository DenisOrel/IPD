
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.InternalHandlerHostFactory
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.IO;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers;

internal class InternalHandlerHostFactory
{
  public static InternalViewerHost Create(
    string fileName,
    List<Tuple<InternalViewerHost, List<string>>> internalViewerHandlerHostMapping)
  {
    InternalViewerHost internalViewerHost = (InternalViewerHost) null;
    string str1 = Path.GetExtension(fileName.ToLower());
    if (!string.IsNullOrEmpty(str1))
    {
      string str2 = str1.ToLower().Replace(".", string.Empty);
      foreach (Tuple<InternalViewerHost, List<string>> tuple in internalViewerHandlerHostMapping)
      {
        if (tuple.Item2 != null)
        {
          foreach (string str3 in tuple.Item2)
          {
            if (str2 == str3)
            {
              internalViewerHost = Activator.CreateInstance(tuple.Item1.GetType()) as InternalViewerHost;
              break;
            }
          }
        }
        if (internalViewerHost != null)
          break;
      }
    }
    return internalViewerHost;
  }
}
