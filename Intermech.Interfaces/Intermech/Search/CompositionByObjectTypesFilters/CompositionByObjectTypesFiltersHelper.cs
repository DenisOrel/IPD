
// Type: Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFiltersHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.CompositionByObjectTypesFilters
{
    public static class CompositionByObjectTypesFiltersHelper
    {
      public static CompositionByObjectTypesFilterProjectType CreateProjectType(int objectTypeID)
      {
        CompositionByObjectTypesFilterProjectType projectType = new CompositionByObjectTypesFilterProjectType(objectTypeID);
        projectType.PartTypes.AddRange((IEnumerable<CompositionByObjectTypesFilterPartType>) MetaDataHelper.GetObjectTypeApplicabilities(objectTypeID).Select<IMSApplicability, int>((Func<IMSApplicability, int>) (o => o.ChildObjectTypeID)).Distinct<int>().Select<int, IMSObjectType>((Func<int, IMSObjectType>) (o => MetaDataHelper.GetObjectType(o))).OrderBy<IMSObjectType, string>((Func<IMSObjectType, string>) (o => o.ObjectTypeName)).Select<IMSObjectType, CompositionByObjectTypesFilterPartType>((Func<IMSObjectType, CompositionByObjectTypesFilterPartType>) (o => CompositionByObjectTypesFiltersHelper.CreatePartType(o.ObjectTypeID))).ToArray<CompositionByObjectTypesFilterPartType>());
        return projectType;
      }

      private static CompositionByObjectTypesFilterPartType CreatePartType(int objectTypeID)
      {
        IMSObjectType objectType = MetaDataHelper.GetObjectType(objectTypeID);
        CompositionByObjectTypesFilterPartType partType = new CompositionByObjectTypesFilterPartType(objectTypeID, objectType.VersionsMode == ObjectVersionModes.Abstract);
        partType.Children.AddRange((IEnumerable<CompositionByObjectTypesFilterPartType>) MetaDataHelper.GetObjectTypeChildrenID(objectTypeID).Select<int, CompositionByObjectTypesFilterPartType>((Func<int, CompositionByObjectTypesFilterPartType>) (o => CompositionByObjectTypesFiltersHelper.CreatePartType(o))).ToArray<CompositionByObjectTypesFilterPartType>());
        return partType;
      }
    }
}
