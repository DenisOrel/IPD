
// Type: Intermech.Search.EditingContexts.EditingContextConstants
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Search.EditingContexts
{
    public static class EditingContextConstants
    {
      public static readonly Guid EditingContextObjectTypeGuid = new Guid("cad0146b-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid EcoObjectTypeGuid = new Guid("cad00348-306c-11d8-b4e9-00304f19f545");
      public static Guid LinkedEditingContextIDAttributeTypeGuid = new Guid("cad014ff-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid DocumentObjectTypeGuid = new Guid("cad00070-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid ProductObjectTypeGuid = new Guid("cad00268-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid DocumentationRelationTypeGuid = new Guid("cad00154-306c-11d8-b4e9-00304f19f545");
      private static int[] _documentObjectTypesIds;
      private static int[] _productObjectTypesIds;

      public static int EditingContextObjectTypeID
      {
        get => MetaDataHelper.GetObjectTypeID(EditingContextConstants.EditingContextObjectTypeGuid);
      }

      public static int EcoObjectTypeID
      {
        get => MetaDataHelper.GetObjectTypeID(EditingContextConstants.EcoObjectTypeGuid);
      }

      public static int LinkedEditingContextIDAttributeTypeID
      {
        get
        {
          return MetaDataHelper.GetAttributeTypeID(EditingContextConstants.LinkedEditingContextIDAttributeTypeGuid);
        }
      }

      public static int DocumentObjectTypeID
      {
        get => MetaDataHelper.GetObjectTypeID(EditingContextConstants.DocumentObjectTypeGuid);
      }

      public static int[] DocumentObjectTypesIds
      {
        get
        {
          if (EditingContextConstants._documentObjectTypesIds == null)
            EditingContextConstants._documentObjectTypesIds = EditingContextConstants.GetDescendentObjectTypeIdsAndSelt(EditingContextConstants.DocumentObjectTypeID);
          return EditingContextConstants._documentObjectTypesIds;
        }
      }

      public static int ProductObjectTypeID
      {
        get => MetaDataHelper.GetObjectTypeID(EditingContextConstants.ProductObjectTypeGuid);
      }

      public static int[] ProductObjectTypesIds
      {
        get
        {
          if (EditingContextConstants._productObjectTypesIds == null)
            EditingContextConstants._productObjectTypesIds = EditingContextConstants.GetDescendentObjectTypeIdsAndSelt(EditingContextConstants.ProductObjectTypeID);
          return EditingContextConstants._productObjectTypesIds;
        }
      }

      public static int DocumentationRelationTypeID
      {
        get => MetaDataHelper.GetRelationTypeID(EditingContextConstants.DocumentationRelationTypeGuid);
      }

      private static int[] GetDescendentObjectTypeIdsAndSelt(int objectTypeID)
      {
        List<int> intList = new List<int>();
        intList.Add(objectTypeID);
        intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectTypeID));
        return intList.ToArray();
      }
    }
}
