// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.InstancesAndParties.InstancePartyObjectType4ObjectTypeHelper
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Pdm.InstancesAndParties;

public static class InstancePartyObjectType4ObjectTypeHelper
{
  public static int GetInstanceObjectTypeID4ObjectTypeID(IUserSession userSession, int objectTypeID)
  {
    if (userSession == null)
      throw new ArgumentNullException();
    if (MaterialHelper.IsMaterialOrMaterialSubtype(userSession, objectTypeID))
      return Constants.InstanceMaterialObjectTypeID;
    if (MaterialHelper.IsCompositeMaterialOrCompositeMaterialSubtype(userSession, objectTypeID))
      return Constants.InstanceCompositeMaterialObjectTypeID;
    return MaterialHelper.IsMaterialMarkOrMaterialMarkSubtype(userSession, objectTypeID) ? Constants.InstanceMaterialMarkObjectTypeID : PdmMrpHelper.GetInstanceObjectType(objectTypeID);
  }

  public static int GetPartyObjectTypeID4ObjectTypeID(IUserSession userSession, int objectTypeID)
  {
    if (userSession == null)
      throw new ArgumentNullException();
    if (MaterialHelper.IsMaterialOrMaterialSubtype(userSession, objectTypeID))
      return Constants.PartyMaterialObjectTypeID;
    if (MaterialHelper.IsCompositeMaterialOrCompositeMaterialSubtype(userSession, objectTypeID))
      return Constants.PartyCompositeMaterialObjectTypeID;
    return MaterialHelper.IsMaterialMarkOrMaterialMarkSubtype(userSession, objectTypeID) ? Constants.PartyMaterialMarkObjectTypeID : PdmMrpHelper.GetPartyObjectType(objectTypeID);
  }
}
