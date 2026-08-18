
// Type: Intermech.Search.Utilities.RelationTypeHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.Utilities
{
    public static class RelationTypeHelper
    {
      public static bool IsUnknownRelationTypeID(int relationTypeID)
      {
        return relationTypeID == -1 || relationTypeID == -1;
      }

      public static string GetRelationTypeName(int relationTypeID)
      {
        return relationTypeID != -1 ? MetaDataHelper.GetRelationTypeName(relationTypeID) : throw new ArgumentException();
      }

      public static bool IsAnyUnknownRelationTypeID(IEnumerable<int> relationTypeIds)
      {
        return relationTypeIds.Any<int>((Func<int, bool>) (o => RelationTypeHelper.IsUnknownRelationTypeID(o)));
      }

      public static bool IsObjectVersionIDInCompositionExists(int relationTypeID)
      {
        return MetaDataHelper.GetAttribute4RelationType(relationTypeID, Constants.ExplicitPartVersionIDAttributeTypeID) != null;
      }

      public static bool IsManualOrAnyAttribute(int relationTypeID, int attributeTypeID)
      {
        IMSAttribute4RelationType attribute4RelationType = MetaDataHelper.GetAttribute4RelationType(relationTypeID, attributeTypeID);
        return attribute4RelationType != null && attribute4RelationType.Required == RequiredModes.Manual || MetaDataHelper.GetRelationType(relationTypeID).AnyAttributes;
      }
    }
}
