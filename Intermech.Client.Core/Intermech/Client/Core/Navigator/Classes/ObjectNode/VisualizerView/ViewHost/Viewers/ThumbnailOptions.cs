
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.ThumbnailOptions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers;

[Flags]
public enum ThumbnailOptions
{
  None = 0,
  BiggerSizeOk = 1,
  InMemoryOnly = 2,
  IconOnly = 4,
  ThumbnailOnly = 8,
  InCacheOnly = 16, // 0x00000010
}
