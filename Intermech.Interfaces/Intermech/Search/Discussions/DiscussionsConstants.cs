
// Type: Intermech.Search.Discussions.DiscussionsConstants
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;


namespace Intermech.Search.Discussions
{
    public static class DiscussionsConstants
    {
      public static readonly Guid DiscussionObjectTypeGuid = new Guid("cadd92ce-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid DiscussedObjectVersionGuidAttributeTypeGuid = new Guid("cadd92de-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid DiscussedObjectGuidAttributeTypeGuid = new Guid("cadd92df-306c-11d8-b4e9-00304f19f545");
      public static readonly Guid DiscussionAttributeTypeGuid = new Guid("cadd92cf-306c-11d8-b4e9-00304f19f545");

      public static int DiscussionObjectTypeId
      {
        get => MetaDataHelper.GetObjectTypeID(DiscussionsConstants.DiscussionObjectTypeGuid);
      }

      public static int DiscussedObjectVersionGuidAttributeTypeId
      {
        get
        {
          return MetaDataHelper.GetAttributeTypeID(DiscussionsConstants.DiscussedObjectVersionGuidAttributeTypeGuid);
        }
      }

      public static int DiscussedObjectGuidAttributeTypeId
      {
        get
        {
          return MetaDataHelper.GetAttributeTypeID(DiscussionsConstants.DiscussedObjectGuidAttributeTypeGuid);
        }
      }

      public static int DiscussionAttributeTypeId
      {
        get => MetaDataHelper.GetAttributeTypeID(DiscussionsConstants.DiscussionAttributeTypeGuid);
      }
    }
}
