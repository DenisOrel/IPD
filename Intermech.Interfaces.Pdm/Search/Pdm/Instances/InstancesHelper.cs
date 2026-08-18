// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Instances.InstancesHelper
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Search.Utilities;
using System;

#nullable disable
namespace Intermech.Search.Pdm.Instances;

public static class InstancesHelper
{
  public static bool CheckObjectTypeForCreateInstances(int objectTypeID)
  {
    if (ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeID))
      throw new ArgumentException();
    return MetaDataHelper.IsObjectTypeChildOf(objectTypeID, InstancesConstants.ProductObjectTypeID) && objectTypeID != InstancesConstants.StandardProductObjectTypeID && objectTypeID != InstancesConstants.OtherProductObjectTypeID;
  }

  public static bool CheckObjectForCreateInstances(long objectVersionID)
  {
    if (ObjectHelper.IsUnknownObjectID(objectVersionID))
      throw new ArgumentException();
    return InstancesHelper.CheckObjectTypeForCreateInstances(objectVersionID) && !InstancesHelper.HasCadModels(objectVersionID);
  }

  private static bool CheckObjectTypeForCreateInstances(long objectVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return InstancesHelper.CheckObjectTypeForCreateInstances(sessionKeeper.Session.GetObject(objectVersionID).ObjectType);
  }

  private static bool HasCadModels(long objectVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return PDMHelper.Validation3DModelInComposition(sessionKeeper.Session, objectVersionID);
  }
}
