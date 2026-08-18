// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.RelationTypeNodeReader
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class RelationTypeNodeReader(
  XmlNode node,
  IUserSession userSession,
  IEventLogHelper eHelper,
  string curDirectory,
  IObligatoryObjectsRegistryService obligatoryObjects,
  Guid relTypeGuid) : NodeReader(node, userSession, eHelper, curDirectory, obligatoryObjects, relTypeGuid, (IPropertyFactory) new RelationTypePropertyFactory())
{
  protected override void OnRead(out int categoryID, out object id)
  {
    IDBRelationType relationType = this.session.GetRelationType(this.GUID, false);
    RelationTypePropertyFactory propertyFactory = (RelationTypePropertyFactory) this.propertyFactory;
    byte[] propertyValue1 = this.propertyFactory.GetPropertyValue<byte[]>("F_ICON", (byte[]) null);
    Dictionary<string, List<MetadataExtension>> propertyValue2 = propertyFactory.GetPropertyValue<Dictionary<string, List<MetadataExtension>>>("F_EXTENSIONS", (Dictionary<string, List<MetadataExtension>>) null);
    List<UpdateScriptAccessRight> propertyValue3 = propertyFactory.GetPropertyValue<List<UpdateScriptAccessRight>>("F_ACCESS", (List<UpdateScriptAccessRight>) null);
    if (relationType == null)
    {
      relationType = this.session.GetRelationType(this.session.GetRelationTypeCollection().Create(propertyFactory.GetRelationTypeProperties(this.GUID)));
      if (propertyValue1 != null)
        relationType.Icon = propertyValue1;
      this.SetAccess(relationType as IDBSecurity, propertyValue3, 6, Convert.ToInt64(relationType.RelationType));
      if (propertyValue2 != null && propertyValue2.Count > 0)
        this.SetExtensions(relationType as DBMetadataExtensions, propertyValue2);
    }
    else
    {
      RelationTypeProperties propertiesStructure = relationType.PropertiesStructure;
      propertiesStructure.AnyAttributes = propertyFactory.GetObligatoryPropertyValue<bool>("F_ANY_ATTRIBUTES", propertiesStructure.AnyAttributes);
      propertiesStructure.AreaID = propertyFactory.GetObligatoryPropertyValue<string>("F_AREA_ID", propertiesStructure.AreaID);
      propertiesStructure.CheckoutFile = propertyFactory.GetObligatoryPropertyValue<bool>("F_CHKOUTFILE", propertiesStructure.CheckoutFile);
      propertiesStructure.Description = propertyFactory.GetObligatoryPropertyValue<string>("F_DESCRIPTION", propertiesStructure.Description);
      propertiesStructure.Note = propertyFactory.GetObligatoryPropertyValue<string>("F_NOTE", propertiesStructure.Note);
      propertiesStructure.ReverseName = propertyFactory.GetObligatoryPropertyValue<string>("F_REVERSE_NAME", propertiesStructure.ReverseName);
      propertiesStructure.SaveHistory = propertyFactory.GetObligatoryPropertyValue<bool>("F_SAVE_HISTORY", propertiesStructure.SaveHistory);
      propertiesStructure.ShortName = propertyFactory.GetObligatoryPropertyValue<string>("F_SHORT_NAME", propertiesStructure.ShortName);
      propertiesStructure.TypeName = propertyFactory.GetObligatoryPropertyValue<string>("F_TYPE_NAME", propertiesStructure.TypeName);
      propertiesStructure.Options = propertyFactory.GetOptions(propertiesStructure.Options, true);
      relationType.PropertiesStructure = propertiesStructure;
      if (this.propertyFactory.IsPropertyObligatory("F_ICON"))
        relationType.Icon = propertyValue1;
      if (this.propertyFactory.IsPropertyObligatory("F_EXTENSIONS"))
        this.SetExtensions(relationType as DBMetadataExtensions, propertyValue2);
    }
    this.RefreshAttributes(propertyFactory, relationType);
    categoryID = 6;
    id = (object) relationType.RelationType;
  }

  private void RefreshAttributes(RelationTypePropertyFactory factory, IDBRelationType relationType)
  {
    foreach (AttributeType4RelationTypePropertyFactory attributeFactory in factory.AttributeFactories)
    {
      if (relationType.Attributes.GetAttributeByGUID(attributeFactory.AttributeGUID, false) is IDBAttributeType4Relation attributeByGuid)
      {
        attributeByGuid.SourceAttributeID = attributeFactory.GetObligatoryPropertyValue<int>("F_SOURCE_ID", attributeByGuid.SourceAttributeID);
        attributeByGuid.MasterAttributeID = attributeFactory.GetObligatoryPropertyValue<int>("F_MASTER_ID", attributeByGuid.MasterAttributeID);
        attributeByGuid.Required = attributeFactory.GetObligatoryPropertyValue<RequiredModes>("F_REQUIRED", attributeByGuid.Required);
        attributeByGuid.ValidationRule = attributeFactory.GetObligatoryPropertyValue<string>("F_VALIDATION_RULE", attributeByGuid.ValidationRule);
        attributeByGuid.Computed = attributeFactory.GetObligatoryPropertyValue<ComputeValueModes>("F_COMPUTED", attributeByGuid.Computed);
        attributeByGuid.Formula = attributeFactory.GetObligatoryPropertyValue<string>("F_FORMULA", attributeByGuid.Formula);
        attributeByGuid.Mask = attributeFactory.GetObligatoryPropertyValue<string>("F_MASK", attributeByGuid.Mask);
        attributeByGuid.IsContent = attributeFactory.GetObligatoryPropertyValue<bool>("F_CONTENT", attributeByGuid.IsContent);
        attributeByGuid.DefaultValue = attributeFactory.GetObligatoryPropertyValue<object>("F_DEFAULT_VALUE", attributeByGuid.DefaultValue);
        attributeByGuid.OptimizationMode = attributeFactory.GetObligatoryPropertyValue<OptimizationModes>("F_INVIEW", attributeByGuid.OptimizationMode);
        attributeByGuid.Options = attributeFactory.GetOptions(attributeByGuid.Options, true);
      }
      else
        attributeByGuid = (relationType.Attributes as IDBAttribute4RelationTypeCollection).Create(attributeFactory.GetAttributeTypeProperties(this.session));
      this.obligatoryObjects.RegisterObligatoryObjectElement(6, (object) relationType.RelationType, ObligatoryElementKeys.GetKeyForAttributePresence(attributeByGuid.AttributeID));
      foreach (ObligatoryElementKey obligatoryElement in attributeFactory.ObligatoryElements)
        this.obligatoryObjects.RegisterObligatoryObjectElement(6, (object) relationType.RelationType, obligatoryElement);
    }
  }
}
