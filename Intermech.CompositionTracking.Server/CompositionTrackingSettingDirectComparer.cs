// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.CompositionTrackingSettingDirectComparer
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.CompositionTracking.Server;

internal class CompositionTrackingSettingDirectComparer : IComparer<CompositionTrackSettingData>
{
  public static readonly IComparer<CompositionTrackSettingData> Instance = (IComparer<CompositionTrackSettingData>) new CompositionTrackingSettingDirectComparer();

  public int Compare(CompositionTrackSettingData x, CompositionTrackSettingData y)
  {
    if (x == null || y == null)
    {
      if (x != null)
        return -1;
      return y == null ? 0 : 1;
    }
    int num1 = x.ObjectTypeContext.InObjectTypeId - y.ObjectTypeContext.InObjectTypeId;
    if (num1 != 0)
      return num1;
    int num2 = x.ObjectTypeContext.ObjectTypeId - y.ObjectTypeContext.ObjectTypeId;
    return num2 != 0 ? num2 : x.ObjectTypeContext.RelationTypeId - y.ObjectTypeContext.RelationTypeId;
  }
}
