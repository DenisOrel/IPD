// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Mbom.MbomHelper
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Search.Mbom;

public static class MbomHelper
{
  public static bool IsAllowableInMbomCompositionObjectType(int objectTypeID)
  {
    if (ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeID))
      throw new ArgumentException();
    return ((IEnumerable<int>) MetaDataHelper.GetObjectTypeApplicabilities(MbomConstants.MbomObjectTypeID).Where<IMSApplicability>((Func<IMSApplicability, bool>) (o => o.RelationTypeID == MbomConstants.MbomCompositionRelationTypeID)).Select<IMSApplicability, int>((Func<IMSApplicability, int>) (o => o.ChildObjectTypeID)).Distinct<int>().ToArray<int>()).Contains<int>(objectTypeID);
  }

  public static bool IsEbomObjectType(int objectTypeID)
  {
    if (ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeID))
      throw new ArgumentException();
    return ((IEnumerable<int>) MbomHelper.GetAssemblyUnitAndDescendantsTypeIds()).Contains<int>(objectTypeID) && !MbomHelper.IsTechnologicalAssemblyUnitObjectType(objectTypeID);
  }

  public static bool IsMbomOrSimilarObjectType(int objectTypeID)
  {
    if (ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeID))
      throw new ArgumentException();
    return MbomHelper.IsMbomObjectTypeID(objectTypeID) || MbomHelper.IsTechnologicalAssemblyUnitObjectType(objectTypeID);
  }

  public static bool IsMbomObjectTypeID(int objectTypeID)
  {
    return !ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeID) ? ((IEnumerable<int>) MbomHelper.GetMbomAndDescendantsTypeIds()).Contains<int>(objectTypeID) : throw new ArgumentException();
  }

  public static bool IsTechnologicalAssemblyUnitObjectType(int objectTypeID)
  {
    return !ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeID) ? ((IEnumerable<int>) MbomHelper.GetTechnologicalAssemblyUnitAndDescendantsTypeIds()).Contains<int>(objectTypeID) : throw new ArgumentException();
  }

  public static int GetRelationTypeIDForMbomOrSimilarObjectType(int objectTypeID)
  {
    if (ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeID))
      throw new ArgumentException();
    return MbomConstants.MbomCompositionRelationTypeID;
  }

  private static int[] GetAssemblyUnitAndDescendantsTypeIds()
  {
    List<int> intList = new List<int>();
    intList.Add(MbomConstants.AssemblyUnitObjectTypeID);
    intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(MbomConstants.AssemblyUnitObjectTypeID));
    return intList.ToArray();
  }

  private static int[] GetTechnologicalAssemblyUnitAndDescendantsTypeIds()
  {
    List<int> intList = new List<int>();
    intList.Add(MbomConstants.TechnologicalAssemblyUnitObjectTypeID);
    intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(MbomConstants.TechnologicalAssemblyUnitObjectTypeID));
    return intList.ToArray();
  }

  private static int[] GetMbomAndDescendantsTypeIds()
  {
    List<int> intList = new List<int>();
    intList.Add(MbomConstants.MbomObjectTypeID);
    intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(MbomConstants.MbomObjectTypeID));
    return intList.ToArray();
  }
}
