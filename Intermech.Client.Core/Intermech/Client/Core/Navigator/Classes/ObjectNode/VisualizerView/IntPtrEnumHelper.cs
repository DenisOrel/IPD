
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.IntPtrEnumHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView;

internal static class IntPtrEnumHelper
{
  public static bool HasFlags(IntPtr val, object flag)
  {
    return EnumHelper.HasFlag(val.ToInt64(), (long) flag);
  }

  public static IntPtr SetFlag(IntPtr val, object flag)
  {
    return new IntPtr(EnumHelper.SetFlag(val.ToInt64(), (long) flag));
  }

  public static IntPtr UnsetFlag(IntPtr val, object flag)
  {
    return new IntPtr(EnumHelper.UnsetFlag(val.ToInt64(), (long) flag));
  }
}
