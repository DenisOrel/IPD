
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.PreviewHandlerViewer.STGM
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.PreviewHandlerViewer;

[Flags]
public enum STGM
{
  DIRECT = 0,
  TRANSACTED = 65536, // 0x00010000
  SIMPLE = 134217728, // 0x08000000
  READ = 0,
  WRITE = 1,
  READWRITE = 2,
  SHARE_DENY_NONE = 64, // 0x00000040
  SHARE_DENY_READ = 48, // 0x00000030
  SHARE_DENY_WRITE = 32, // 0x00000020
  SHARE_EXCLUSIVE = 16, // 0x00000010
  PRIORITY = 262144, // 0x00040000
  DELETEONRELEASE = 67108864, // 0x04000000
  NOSCRATCH = 1048576, // 0x00100000
  CREATE = 4096, // 0x00001000
  CONVERT = 131072, // 0x00020000
  FAILIFTHERE = 0,
  NOSNAPSHOT = 2097152, // 0x00200000
  DIRECT_SWMR = 4194304, // 0x00400000
}
