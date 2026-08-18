
// Type: Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFiltersConstants
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;


namespace Intermech.Search.CompositionByObjectTypesFilters
{
    public static class CompositionByObjectTypesFiltersConstants
    {
      public static readonly Guid CompositionByObjectTypesFilterObjectTypeGuid = new Guid("cadd9975-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid CompositionByObjectTypesFiltersRelationTypeGuid = new Guid("cadd9976-306c-11d8-b4e9-00304f19f545");

      public static int CompositionByObjectTypesFilterObjectTypeID
      {
        get
        {
          return MetaDataHelper.GetObjectTypeID(CompositionByObjectTypesFiltersConstants.CompositionByObjectTypesFilterObjectTypeGuid);
        }
      }

      public static int CompositionByObjectTypesFiltersRelationTypeID
      {
        get
        {
          return MetaDataHelper.GetRelationTypeID(CompositionByObjectTypesFiltersConstants.CompositionByObjectTypesFiltersRelationTypeGuid);
        }
      }
    }
}
