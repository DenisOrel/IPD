// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.NodeReaderHelper
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.LifeCycles;
using Intermech.Interfaces.Server;
using System;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal class NodeReaderHelper
{
  public static NodeReader GetNodeReader(
    XmlNode rootNode,
    IUserSession session,
    IEventLogHelper eHelper,
    string curDirectory,
    int categoryID,
    IObligatoryObjectsRegistryService obligatoryObjects,
    Guid guid)
  {
    switch (categoryID)
    {
      case 2:
        return (NodeReader) new ObjectNodeReader(rootNode, session, eHelper, curDirectory, obligatoryObjects, guid);
      case 3:
        return (NodeReader) new AttributeTypeNodeReader(rootNode, session, eHelper, curDirectory, obligatoryObjects, guid);
      case 4:
        return (NodeReader) new ObjectTypeNodeReader(rootNode, session, eHelper, curDirectory, obligatoryObjects, guid);
      case 6:
        return (NodeReader) new RelationTypeNodeReader(rootNode, session, eHelper, curDirectory, obligatoryObjects, guid);
      case 8:
        return (NodeReader) new LCLevelNodeReader(rootNode, session, eHelper, curDirectory, obligatoryObjects, guid);
      case 9:
        return (NodeReader) new LanguageNodeReader(rootNode, session, eHelper, curDirectory, obligatoryObjects, guid);
      case 11:
        return (NodeReader) new SubjectAreaNodeReader(rootNode, session, eHelper, curDirectory, obligatoryObjects, guid);
      case 12:
        return (NodeReader) new AttributeGroupNodeReader(rootNode, session, eHelper, curDirectory, obligatoryObjects, guid);
      case 16 /*0x10*/:
        return (NodeReader) new LCSchemaNodeReader(rootNode, session, eHelper, curDirectory, obligatoryObjects, guid);
      default:
        return (NodeReader) null;
    }
  }

  public static int GetLCStepID(IUserSession session, string value)
  {
    if (value != string.Empty && GuidHelper.IsGuid(value))
    {
      IDBLifecycleStep lifecycleStep = session.GetLifecycleStep(new Guid(value), true);
      if (lifecycleStep != null)
        return lifecycleStep.LCStep;
    }
    return -1;
  }

  public static long GetObjectID(IUserSession session, string value)
  {
    if (value != string.Empty && GuidHelper.IsGuid(value))
    {
      IDBObject dbObject = session.GetObject(new Guid(value), true);
      if (dbObject != null)
        return dbObject.ObjectID;
    }
    return 0;
  }

  public static long GetID(IUserSession session, string value)
  {
    if (value != string.Empty && GuidHelper.IsGuid(value))
    {
      IDBObject objectById = session.GetObjectByID(new Guid(value), true);
      if (objectById != null)
        return objectById.ID;
    }
    return 0;
  }

  public static int GetRelationTypeID(IUserSession session, string guidValue)
  {
    if (!(guidValue != string.Empty) || !GuidHelper.IsGuid(guidValue))
      return -1;
    IDBRelationType relationType = session.GetRelationType(new Guid(guidValue), true);
    return relationType == null ? -1 : relationType.RelationType;
  }

  public static int GetObjectTypeID(IUserSession session, string guidValue)
  {
    return guidValue != string.Empty && GuidHelper.IsGuid(guidValue) ? session.GetObjectType(new Guid(guidValue), true).ObjectType : -1;
  }

  public static int GetSchemaID(IUserSession session, string guidValue)
  {
    if (guidValue != string.Empty && GuidHelper.IsGuid(guidValue))
    {
      IDBLCSchema lcSchema = session.GetLCSchema(new Guid(guidValue), true);
      if (lcSchema != null)
        return lcSchema.SchemaID;
    }
    return 0;
  }

  public static int GetAttributeOption(string name, string value, ref AttributeOptions options)
  {
    int attributeOption = 0;
    string str = name.Replace("F_OPTIONS", string.Empty);
    if (str != string.Empty)
    {
      AttributeOptions int32 = (AttributeOptions) Convert.ToInt32(str);
      attributeOption = Convert.ToInt32(value);
      if (attributeOption > 0)
        options |= int32;
      else
        options &= ~int32;
    }
    return attributeOption;
  }
}
