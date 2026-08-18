
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.EnumHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView;

internal static class EnumHelper
{
  public static bool HasFlag(long val, long flag) => (val & flag) == flag;

  public static long SetFlag(long val, long flag) => val | flag;

  public static long UnsetFlag(long val, long flag) => val & ~flag;
}
