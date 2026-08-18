
// Type: Intermech.Search.Classifiers.ClassifiersConstants
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;


namespace Intermech.Search.Classifiers
{
    public static class ClassifiersConstants
    {
      public static int ObjectTypeGuidsAttributeTypeID
      {
        get => MetaDataHelper.GetAttributeTypeID(new Guid("cad00149-306c-11d8-b4e9-00304f19f545"));
      }

      public static int ClassifierTypeAttributeTypeID
      {
        get => MetaDataHelper.GetAttributeTypeID(new Guid("cad00e8f-306c-11d8-b4e9-00304f19f545"));
      }

      public static int ArchiveAttributeTypeID
      {
        get => MetaDataHelper.GetAttributeTypeID(SystemGUIDs.attributeArchive);
      }

      public static int ArchivesAttributeTypeID
      {
        get => MetaDataHelper.GetAttributeTypeID(new Guid("cad01485-306c-11d8-b4e9-00304f19f545"));
      }

      public static int CommonClassifierObjectTypeID
      {
        get => MetaDataHelper.GetAttributeTypeID(new Guid("cad0014e-306c-11d8-b4e9-00304f19f545"));
      }

      public static int PersonalClassifierObjectTypeID
      {
        get => MetaDataHelper.GetAttributeTypeID(new Guid("cad0014f-306c-11d8-b4e9-00304f19f545"));
      }

      public static int[] RootClassifierObjectTypeIds
      {
        get
        {
          return new int[2]
          {
            ClassifiersConstants.CommonClassifierObjectTypeID,
            ClassifiersConstants.PersonalClassifierObjectTypeID
          };
        }
      }

      public static int DocumentObjectTypeID
      {
        get => MetaDataHelper.GetObjectTypeID(new Guid("cad00070-306c-11d8-b4e9-00304f19f545"));
      }

      public static int[] AllDocumentsObjectTypeIds
      {
        get
        {
          return MetaDataHelper.GetObjectTypeChildrenIDRecursive(ClassifiersConstants.DocumentObjectTypeID).ToArray();
        }
      }

      public static int SimpleRelationWithSortingRelationTypeID
      {
        get => MetaDataHelper.GetRelationTypeID("cad00151-306c-11d8-b4e9-00304f19f545");
      }

      public static int ClassifierFolderObjectTypeID
      {
        get => MetaDataHelper.GetObjectTypeID("cad00150-306c-11d8-b4e9-00304f19f545");
      }
    }
}
