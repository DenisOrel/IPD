// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.ObjectTypeNodeReader
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class ObjectTypeNodeReader(
  XmlNode node,
  IUserSession userSession,
  IEventLogHelper eHelper,
  string curDirectory,
  IObligatoryObjectsRegistryService obligatoryObjects,
  Guid objTypeGuid) : NodeReader(node, userSession, eHelper, curDirectory, obligatoryObjects, objTypeGuid, (IPropertyFactory) new ObjectTypePropertyFactory())
{
  protected override void OnRead(out int categoryID, out object id)
  {
    IDBObjectType objectType = this.session.GetObjectType(this.GUID, false);
    ObjectTypePropertyFactory propertyFactory = (ObjectTypePropertyFactory) this.propertyFactory;
    byte[] propertyValue1 = this.propertyFactory.GetPropertyValue<byte[]>("F_ICON", (byte[]) null);
    ObjectsClassifyType propertyValue2 = this.propertyFactory.GetPropertyValue<ObjectsClassifyType>("F_CLASSIFY_TYPE", ObjectsClassifyType.None);
    Dictionary<string, List<MetadataExtension>> propertyValue3 = propertyFactory.GetPropertyValue<Dictionary<string, List<MetadataExtension>>>("F_EXTENSIONS", (Dictionary<string, List<MetadataExtension>>) null);
    List<UpdateScriptAccessRight> propertyValue4 = propertyFactory.GetPropertyValue<List<UpdateScriptAccessRight>>("F_ACCESS", (List<UpdateScriptAccessRight>) null);
    int num1;
    if (objectType == null)
    {
      IDBObjectTypeCollection objectTypeCollection = this.session.GetObjectTypeCollection(this.propertyFactory.GetPropertyValue<int>("F_PARENT_ID"));
      ObjectTypeProperties objectTypeProperties = propertyFactory.GetObjectTypeProperties(this.GUID);
      num1 = objectTypeProperties.CaptionAttribute;
      objectTypeProperties.CaptionAttribute = 0;
      ObjectTypeProperties typeProperties = objectTypeProperties;
      int num2 = objectTypeCollection.Create(typeProperties);
      objectType = this.session.GetObjectType(num2);
      if (propertyValue1 != null)
        objectType.Icon = propertyValue1;
      if (propertyValue2 != ObjectsClassifyType.None)
        ObjectsClassifyHelper.SetClassifierType(this.session, num2, propertyValue2);
      this.SetAccess(objectType as IDBSecurity, propertyValue4, 4, Convert.ToInt64(objectType.ObjectType));
      if (propertyValue3 != null && propertyValue3.Count > 0)
        this.SetExtensions(objectType as DBMetadataExtensions, propertyValue3);
    }
    else
    {
      ObjectTypeProperties propertiesStructure = objectType.PropertiesStructure;
      propertiesStructure.AnyAttributes = this.propertyFactory.GetObligatoryPropertyValue<bool>("F_ANY_ATTRIBUTES", propertiesStructure.AnyAttributes);
      num1 = this.propertyFactory.GetObligatoryPropertyValue<int>("F_CAPTION_ATTRIBUTE", propertiesStructure.CaptionAttribute);
      propertiesStructure.DefaultRelation = this.propertyFactory.GetObligatoryPropertyValue<int>("F_DEFAULT_RELATION", propertiesStructure.DefaultRelation);
      propertiesStructure.Note = this.propertyFactory.GetObligatoryPropertyValue<string>("F_NOTE", propertiesStructure.Note);
      propertiesStructure.ObjectInstanceName = this.propertyFactory.GetObligatoryPropertyValue<string>("F_OBJ_NAME", propertiesStructure.ObjectInstanceName);
      propertiesStructure.ObjectTypeName = this.propertyFactory.GetObligatoryPropertyValue<string>("F_OBJ_TYPE_NAME", propertiesStructure.ObjectTypeName);
      propertiesStructure.ObjectTypeShortName = this.propertyFactory.GetObligatoryPropertyValue<string>("F_SHORT_NAME", propertiesStructure.ObjectTypeShortName);
      propertiesStructure.PublicLCSchema = this.propertyFactory.GetObligatoryPropertyValue<InheritModes>("F_PUBLIC_LC", propertiesStructure.PublicLCSchema);
      propertiesStructure.SchemaID = this.propertyFactory.GetObligatoryPropertyValue<int>("F_SCHEMA_ID", propertiesStructure.SchemaID);
      propertiesStructure.Versionable = this.propertyFactory.GetObligatoryPropertyValue<ObjectVersionModes>("F_VERSIONABLE", propertiesStructure.Versionable);
      propertiesStructure.AreaID = this.propertyFactory.GetObligatoryPropertyValue<string>("F_AREA_ID", objectType.PropertiesStructure.AreaID);
      propertiesStructure.LifetimeReserve = this.propertyFactory.GetObligatoryPropertyValue<int>("F_DEL_TIME", propertiesStructure.LifetimeReserve);
      propertiesStructure.Options = propertyFactory.GetOptions(objectType.Options, true);
      objectType.PropertiesStructure = propertiesStructure;
      if (this.propertyFactory.IsPropertyObligatory("F_PARENT_ID"))
        objectType.ParentTypeID = this.propertyFactory.GetObligatoryPropertyValue<int>("F_PARENT_ID", objectType.ParentTypeID);
      if (this.propertyFactory.IsPropertyObligatory("F_ICON"))
        objectType.Icon = propertyValue1;
      if (this.propertyFactory.IsPropertyObligatory("F_CLASSIFY_TYPE"))
        ObjectsClassifyHelper.SetClassifierType(this.session, objectType.ObjectType, propertyValue2);
      if (this.propertyFactory.IsPropertyObligatory("F_EXTENSIONS"))
        this.SetExtensions(objectType as DBMetadataExtensions, propertyValue3);
    }
    this.RefreshAttributes(propertyFactory, objectType);
    this.RefreshApplicabilities(propertyFactory, objectType);
    objectType.CaptionAttribute = num1;
    categoryID = 4;
    id = (object) objectType.ObjectType;
  }

  private void RefreshAttributes(ObjectTypePropertyFactory factory, IDBObjectType objType)
  {
    foreach (AttributeType4ObjectTypePropertyFactory attributeFactory in factory.AttributeFactories)
    {
      if (objType.Attributes.GetAttributeByGUID(attributeFactory.AttributeGUID, false) is IDBAttributeType4Object attributeByGuid)
      {
        if (attributeFactory.IsPropertyObligatory("F_PUBLIC"))
        {
          InheritModes propertyValue = attributeFactory.GetPropertyValue<InheritModes>("F_PUBLIC");
          if (attributeByGuid.InheritMode == InheritModes.Inherited && (propertyValue == InheritModes.Private || propertyValue == InheritModes.Public))
          {
            ((IDBAttribute4ObjectTypeCollection) objType.Attributes).Create(attributeFactory.GetAttributeTypeProperties(this.session));
            continue;
          }
          if ((attributeByGuid.InheritMode == InheritModes.Public || attributeByGuid.InheritMode == InheritModes.Private) && propertyValue == InheritModes.Inherited)
          {
            attributeByGuid.Delete(0L);
            continue;
          }
          attributeByGuid.InheritMode = propertyValue;
        }
        if (attributeByGuid.InheritMode != InheritModes.Inherited)
        {
          attributeByGuid.SourceAttributeID = attributeFactory.GetObligatoryPropertyValue<int>("F_SOURCE_ID", attributeByGuid.SourceAttributeID);
          attributeByGuid.MasterAttributeID = attributeFactory.GetObligatoryPropertyValue<int>("F_MASTER_ID", attributeByGuid.MasterAttributeID);
          attributeByGuid.Required = attributeFactory.GetObligatoryPropertyValue<RequiredModes>("F_REQUIRED", attributeByGuid.Required);
          attributeByGuid.ValidationRule = attributeFactory.GetObligatoryPropertyValue<string>("F_VALIDATION_RULE", attributeByGuid.ValidationRule);
          attributeByGuid.Computed = attributeFactory.GetObligatoryPropertyValue<ComputeValueModes>("F_COMPUTED", attributeByGuid.Computed);
          attributeByGuid.Formula = attributeFactory.GetObligatoryPropertyValue<string>("F_FORMULA", attributeByGuid.Formula);
          attributeByGuid.UniqueMode = attributeFactory.GetObligatoryPropertyValue<UniqueValueModes>("F_UNIQUE", attributeByGuid.UniqueMode);
          attributeByGuid.LevelID = attributeFactory.GetObligatoryPropertyValue<int>("F_LEVEL_ID", attributeByGuid.LevelID);
          attributeByGuid.Mask = attributeFactory.GetObligatoryPropertyValue<string>("F_MASK", attributeByGuid.Mask);
          attributeByGuid.IsContent = attributeFactory.GetObligatoryPropertyValue<bool>("F_CONTENT", attributeByGuid.IsContent);
          attributeByGuid.DefaultValue = attributeFactory.GetObligatoryPropertyValue<object>("F_DEFAULT_VALUE", attributeByGuid.DefaultValue);
          attributeByGuid.OptimizationMode = attributeFactory.GetObligatoryPropertyValue<OptimizationModes>("F_INVIEW", attributeByGuid.OptimizationMode);
          attributeByGuid.Options = attributeFactory.GetOptions(attributeByGuid.Options, true);
        }
      }
      else
      {
        (objType.Attributes as DBAttribute4ObjectTypeCollection).AutoPatchMode = true;
        attributeByGuid = (objType.Attributes as IDBAttribute4ObjectTypeCollection).Create(attributeFactory.GetAttributeTypeProperties(this.session));
      }
      this.obligatoryObjects.RegisterObligatoryObjectElement(4, (object) objType.ObjectType, ObligatoryElementKeys.GetKeyForAttributePresence(attributeByGuid.AttributeID));
      foreach (ObligatoryElementKey obligatoryElement in attributeFactory.ObligatoryElements)
        this.obligatoryObjects.RegisterObligatoryObjectElement(4, (object) objType.ObjectType, obligatoryElement);
    }
  }

  private void RefreshApplicabilities(ObjectTypePropertyFactory factory, IDBObjectType objType)
  {
    IDBRelationsApplicabilityCollection applicabilityCollection = this.session.GetRelationsApplicabilityCollection();
    foreach (ApplicabilityPropertyFactory applicabilityFactory in factory.ApplicabilityFactories)
    {
      int objectType = this.session.GetObjectType(Convert.ToInt32(applicabilityFactory.GetPropertyValue<CalculateObjectTypeIDValue>("F_OBJECT_TYPE").Value), true).ObjectType;
      int relationType = this.session.GetRelationType(applicabilityFactory.RelationTypeGuid, true).RelationType;
      DataRow applicabilityRow = applicabilityCollection.GetApplicabilityRow(relationType, objType.ObjectType, objectType);
      int applicabilityId;
      if (applicabilityRow != null && Convert.ToInt32(applicabilityRow["F_PUBLIC"]) != 2)
      {
        IDBRelationsApplicability applicability = applicabilityCollection.GetApplicability(Convert.ToInt32(applicabilityRow["F_APPLICABILITY_ID"]));
        applicabilityId = applicability.ApplicabilityID;
        applicability.IsContent = applicabilityFactory.GetObligatoryPropertyValue<bool>("F_CONTENT", applicability.IsContent);
        applicability.MaximumLinks = applicabilityFactory.GetObligatoryPropertyValue<int>("F_MAX_LINKS", applicability.MaximumLinks);
        applicability.CheckoutFiles = applicabilityFactory.GetObligatoryPropertyValue<bool>("F_CHKOUTFILE", applicability.CheckoutFiles);
        applicability.ApplicabilityMode = applicabilityFactory.GetObligatoryPropertyValue<ApplicabilityModes>("F_MIN_LINKS", applicability.ApplicabilityMode);
        applicability.RelationConstraintMode = applicabilityFactory.GetObligatoryPropertyValue<RelationConstraintModes>("F_CONSTRAINT_MODE", applicability.RelationConstraintMode);
        applicability.CloneChildRelations = applicabilityFactory.GetObligatoryPropertyValue<bool>("F_CLONE_RELATIONS", applicability.CloneChildRelations);
        applicability.Options = applicabilityFactory.GetOptions(applicability.Options, true);
      }
      else
        applicabilityId = applicabilityCollection.Create(applicabilityFactory.GetApplicabilityProperties(objType.ObjectType, objectType, relationType));
      this.obligatoryObjects.RegisterObligatoryObject(19, (object) applicabilityId);
      foreach (ObligatoryElementKey obligatoryElement in applicabilityFactory.ObligatoryElements)
        this.obligatoryObjects.RegisterObligatoryObjectElement(19, (object) applicabilityId, obligatoryElement);
    }
  }
}
