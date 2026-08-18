// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.Substitutes.SubstitutesHelper
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Interfaces;
using Intermech.Search.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

#nullable disable
namespace Intermech.Search.Pdm.Substitutes;

public static class SubstitutesHelper
{
  public static bool IsSuitableForSubstitutesRelationType(int relationTypeID)
  {
    if (relationTypeID == -1)
      throw new ArgumentException();
    IAttributeTypeForRelationRepository relationRepository = ServiceLocator.Get<IAttributeTypeForRelationRepository>();
    return relationRepository.Find(new AttributeTypeForRelationKey(SubstitutesConstants.SubstituteGroupNumberAttributeTypeID, relationTypeID)) != null && relationRepository.Find(new AttributeTypeForRelationKey(SubstitutesConstants.SubstituteNameAttributeTypeID, relationTypeID)) != null;
  }

  public static bool IsAuxiliaryOrEqualPositionsSupported(int relationTypeID)
  {
    if (relationTypeID == -1)
      throw new ArgumentException();
    return ServiceLocator.Get<IAttributeTypeForRelationRepository>().Find(relationTypeID).Where<IMSAttribute4RelationType>((Func<IMSAttribute4RelationType, bool>) (o => o.AttributeID == SubstitutesConstants.SubstitutePositionTypeAttributeTypeID)).Count<IMSAttribute4RelationType>() > 0;
  }

  public static SubstitutePack CreatePackFromRelations(IEnumerable<Relation> relations)
  {
    SubstitutePack packFromRelations = new SubstitutePack();
    foreach (Relation relation in relations)
    {
      object attributeValue1 = relation.Attributes.GetAttributeValue(SubstitutesConstants.SubstituteGroupNumberAttributeTypeID);
      string attributeValue2 = relation.Attributes.GetAttributeValue(SubstitutesConstants.SubstituteGroupNameAttributeTypeID) as string;
      object attributeValue3 = relation.Attributes.GetAttributeValue(SubstitutesConstants.SubstituteNumberAttributeTypeID);
      string attributeValue4 = relation.Attributes.GetAttributeValue(SubstitutesConstants.SubstituteNameAttributeTypeID) as string;
      object attributeValue5 = relation.Attributes.GetAttributeValue(SubstitutesConstants.DesignActualVariantAttributeTypeID);
      object attributeValue6 = relation.Attributes.GetAttributeValue(SubstitutesConstants.SubstitutePositionTypeAttributeTypeID);
      object attributeValue7 = relation.Attributes.GetAttributeValue(SubstitutesConstants.PositionNumberAttributeTypeID);
      if (attributeValue1 != null && attributeValue3 != null)
      {
        long int64_1 = Convert.ToInt64(attributeValue1);
        long int64_2 = Convert.ToInt64(attributeValue3);
        bool flag1 = Convert.ToInt64(attributeValue5) == 1L;
        bool flag2 = Convert.ToInt64(attributeValue6) == 3L;
        bool flag3 = Convert.ToInt64(attributeValue6) == 4L;
        long int64_3 = Convert.ToInt64(attributeValue7);
        SubstituteGroup substituteGroup = packFromRelations.Groups[int64_1];
        if (substituteGroup == null)
        {
          substituteGroup = new SubstituteGroup()
          {
            Name = attributeValue2,
            Number = int64_1
          };
          packFromRelations.Groups.Add(substituteGroup);
        }
        Substitute substitute = substituteGroup.Substitutes[int64_2];
        if (substitute == null)
        {
          substitute = new Substitute()
          {
            Name = attributeValue4,
            Number = int64_2,
            IsDesignerActualVariant = flag1
          };
          substituteGroup.Substitutes.Add(substitute);
        }
        SubstitutePosition substitutePosition = new SubstitutePosition(relation.ID, relation.PartID)
        {
          IsAuxiliary = flag2,
          IsEqual = flag3,
          Number = int64_3
        };
        substitute.Positions.Add(substitutePosition);
      }
    }
    foreach (SubstituteGroup group in (Collection<SubstituteGroup>) packFromRelations.Groups)
    {
      foreach (Substitute substitute in (Collection<Substitute>) group.Substitutes)
      {
        SubstitutePosition[] array = substitute.Positions.OrderBy<SubstitutePosition, long>((Func<SubstitutePosition, long>) (o => o.Number)).ToArray<SubstitutePosition>();
        substitute.Positions.Clear();
        foreach (SubstitutePosition substitutePosition in array)
          substitute.Positions.Add(substitutePosition);
      }
    }
    return packFromRelations;
  }
}
