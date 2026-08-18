// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.InstancesAndParties.MaterialHelper
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.Pdm.InstancesAndParties;

public static class MaterialHelper
{
  public static bool IsMaterialOrMaterialSubtype(IUserSession userSession, int objectTypeID)
  {
    int materialObjectTypeId = Constants.MaterialObjectTypeID;
    return materialObjectTypeId == objectTypeID || MaterialHelper.ParentTypesContains(userSession, objectTypeID, materialObjectTypeId);
  }

  public static bool IsCompositeMaterialOrCompositeMaterialSubtype(
    IUserSession userSession,
    int objectTypeID)
  {
    int materialObjectTypeId = Constants.CompositeMaterialObjectTypeID;
    return materialObjectTypeId == objectTypeID || MaterialHelper.ParentTypesContains(userSession, objectTypeID, materialObjectTypeId);
  }

  public static bool IsMaterialMarkOrMaterialMarkSubtype(IUserSession userSession, int objectTypeID)
  {
    int markObjectTypeId = Constants.MaterialMarkObjectTypeID;
    return markObjectTypeId == objectTypeID || MaterialHelper.ParentTypesContains(userSession, objectTypeID, markObjectTypeId);
  }

  public static bool IsInstanceOrPartyMaterial(IUserSession userSession, int objectTypeID)
  {
    return Constants.InstanceMaterialObjectTypeID == objectTypeID || Constants.PartyMaterialObjectTypeID == objectTypeID;
  }

  public static bool IsInstanceOrPartyCompositeMaterial(IUserSession userSession, int objectTypeID)
  {
    return Constants.InstanceCompositeMaterialObjectTypeID == objectTypeID || Constants.PartyCompositeMaterialObjectTypeID == objectTypeID;
  }

  public static bool IsInstanceOrPartyMaterialMark(IUserSession userSession, int objectTypeID)
  {
    return Constants.InstanceMaterialMarkObjectTypeID == objectTypeID || Constants.PartyMaterialMarkObjectTypeID == objectTypeID;
  }

  private static bool ParentTypesContains(
    IUserSession userSession,
    int objectTypeID,
    int requiredParentObjectTypeID)
  {
    int parentTypeId = userSession.GetObjectType(objectTypeID).ParentTypeID;
    if (parentTypeId == requiredParentObjectTypeID)
      return true;
    return parentTypeId != -1 && MaterialHelper.ParentTypesContains(userSession, parentTypeId, requiredParentObjectTypeID);
  }
}
