// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.CompositionTrackingSettingInheritedComparer
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using Intermech.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.CompositionTracking.Server;

internal class CompositionTrackingSettingInheritedComparer : IComparer<CompositionTrackSettingData>
{
  public static readonly IComparer<CompositionTrackSettingData> Instance = (IComparer<CompositionTrackSettingData>) new CompositionTrackingSettingInheritedComparer();

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
    {
      if (x.ObjectTypeContext.InObjectTypeId == -1 || y.ObjectTypeContext.InObjectTypeId == -1 || MetaDataHelper.IsObjectTypeChildOf(y.ObjectTypeContext.InObjectTypeId, x.ObjectTypeContext.InObjectTypeId))
        num1 = 0;
      if (num1 != 0)
        return num1;
    }
    int num2 = x.ObjectTypeContext.ObjectTypeId - y.ObjectTypeContext.ObjectTypeId;
    if (num2 != 0)
    {
      if (x.ObjectTypeContext.ObjectTypeId == -1 || y.ObjectTypeContext.ObjectTypeId == -1 || MetaDataHelper.IsObjectTypeChildOf(y.ObjectTypeContext.ObjectTypeId, x.ObjectTypeContext.ObjectTypeId))
        num2 = 0;
      if (num2 != 0)
        return num2;
    }
    int num3 = x.ObjectTypeContext.RelationTypeId - y.ObjectTypeContext.RelationTypeId;
    if (num3 != 0 && (x.ObjectTypeContext.RelationTypeId == -1 || y.ObjectTypeContext.RelationTypeId == -1))
      num3 = 0;
    return num3;
  }
}
