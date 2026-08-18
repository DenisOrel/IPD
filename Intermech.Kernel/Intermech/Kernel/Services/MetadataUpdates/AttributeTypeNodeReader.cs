// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.AttributeTypeNodeReader
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

internal sealed class AttributeTypeNodeReader : NodeReader
{
  public AttributeTypeNodeReader(
    XmlNode node,
    IUserSession userSession,
    IEventLogHelper eHelper,
    string curDirectory,
    IObligatoryObjectsRegistryService obligatoryObjects,
    Guid attrGuid)
    : base(node, userSession, eHelper, curDirectory, obligatoryObjects, attrGuid, (IPropertyFactory) new AttributeTypePropertyFactory())
  {
    ((AttributeTypePropertyFactory) this.propertyFactory).FieldType = (FieldTypes) Convert.ToInt32(this.rootNode.Attributes["Tag"].Value);
  }

  protected override void OnRead(out int categoryID, out object id)
  {
    AttributeTypePropertyFactory propertyFactory = (AttributeTypePropertyFactory) this.propertyFactory;
    AttributeTypeProperties attributeTypeProperties = propertyFactory.AttributeTypeProperties with
    {
      AttributeGuid = this.GUID
    };
    IDBAttributeType attributeType = this.session.GetAttributeType(this.GUID, false);
    object obj = (object) null;
    bool flag = false;
    DataTable propertyValue1 = propertyFactory.GetPropertyValue<DataTable>("F_POSSIBLE_VALUES", (DataTable) null);
    Dictionary<string, List<MetadataExtension>> propertyValue2 = propertyFactory.GetPropertyValue<Dictionary<string, List<MetadataExtension>>>("F_EXTENSIONS", (Dictionary<string, List<MetadataExtension>>) null);
    List<UpdateScriptAccessRight> propertyValue3 = propertyFactory.GetPropertyValue<List<UpdateScriptAccessRight>>("F_ACCESS", (List<UpdateScriptAccessRight>) null);
    string propertyValue4 = propertyFactory.GetPropertyValue<string>("F_GROUP_ID", string.Empty);
    if (attributeType == null)
    {
      IDBAttributeTypeCollection attributeTypeCollection = this.session.GetAttributeTypeCollection(-1);
      if ((attributeTypeProperties.MultiValueMode == MultiValueModes.MultiValuesFromList || attributeTypeProperties.MultiValueMode == MultiValueModes.SingleValueFromList) && attributeTypeProperties.DefaultValue != null)
      {
        obj = attributeTypeProperties.DefaultValue;
        attributeTypeProperties.DefaultValue = (object) null;
      }
      AttributeTypeProperties attrProperties = attributeTypeProperties;
      attributeType = this.session.GetAttributeType(attributeTypeCollection.Create(attrProperties));
      this.SetAccess(attributeType as IDBSecurity, propertyValue3, 3, Convert.ToInt64(attributeType.AttributeID));
      this.AddAtributeToGroups(attributeType, propertyValue4);
      if (propertyValue2 != null && propertyValue2.Count > 0)
        this.SetExtensions(attributeType as DBMetadataExtensions, propertyValue2);
      if (propertyValue1 != null && propertyValue1.Rows.Count > 0)
        attributeType.SetPossibleValues(propertyValue1);
      flag = true;
    }
    else
    {
      attributeTypeProperties.AttributeID = attributeType.AttributeID;
      attributeTypeProperties.SourceAttributeID = propertyFactory.GetObligatoryPropertyValue<int>("F_SOURCE_ID", attributeType.SourceAttributeID);
      attributeTypeProperties.MasterAttributeID = propertyFactory.GetObligatoryPropertyValue<int>("F_MASTER_ID", attributeType.MasterAttributeID);
      attributeTypeProperties.FieldType = propertyFactory.GetObligatoryPropertyValue<FieldTypes>("F_ATTRIBUTE_TYPE", attributeType.AttributeType);
      if (!propertyFactory.IsPropertyObligatory("F_SIZE_TYPE") || attributeTypeProperties.FieldType == FieldTypes.ftString && attributeTypeProperties.SizeType < attributeType.SizeType)
        attributeTypeProperties.SizeType = attributeType.SizeType;
      attributeTypeProperties.LevelID = propertyFactory.GetObligatoryPropertyValue<int>("F_LEVEL_ID", attributeType.LevelID);
      attributeTypeProperties.LanguageID = propertyFactory.GetObligatoryPropertyValue<string>("F_LANGUAGE_ID", attributeType.PropertiesStructure.LanguageID);
      attributeTypeProperties.AreaID = propertyFactory.GetObligatoryPropertyValue<string>("F_AREA_ID", attributeType.PropertiesStructure.AreaID);
      if (propertyValue1 != null && propertyFactory.IsPropertyObligatory("F_POSSIBLE_VALUES"))
      {
        (attributeType as DBAttributeType).SetPossibleValuesFromScript(propertyValue1);
        attributeTypeProperties.PossibleValues = propertyValue1;
      }
      else
        attributeTypeProperties.PossibleValues = (DataTable) null;
      if (!propertyFactory.IsPropertyObligatory("F_DEFAULT_VALUE"))
      {
        if ((attributeTypeProperties.MultiValueMode == MultiValueModes.MultiValuesFromList || attributeTypeProperties.MultiValueMode == MultiValueModes.SingleValueFromList) && attributeTypeProperties.DefaultValue != null)
          obj = attributeTypeProperties.DefaultValue;
        else
          attributeTypeProperties.DefaultValue = attributeType.DefaultValue;
      }
      attributeTypeProperties.MultiValueMode = propertyFactory.GetObligatoryPropertyValue<MultiValueModes>("F_MULTIPLE_VALUED", attributeType.MultipleValued);
      attributeTypeProperties.Formula = propertyFactory.GetObligatoryPropertyValue<string>("F_FORMULA", attributeType.Formula);
      attributeTypeProperties.IsContent = propertyFactory.GetObligatoryPropertyValue<bool>("F_CONTENT", attributeType.IsContent);
      attributeTypeProperties.Computed = propertyFactory.GetObligatoryPropertyValue<ComputeValueModes>("F_COMPUTED", attributeType.Computed);
      attributeTypeProperties.OptimizationMode = propertyFactory.GetObligatoryPropertyValue<OptimizationModes>("F_INVIEW", attributeType.OptimizationMode);
      attributeTypeProperties.Note = propertyFactory.GetObligatoryPropertyValue<string>("F_NOTE", attributeType.Note);
      attributeTypeProperties.Name = propertyFactory.GetObligatoryPropertyValue<string>("F_NAME", attributeType.Name);
      attributeTypeProperties.ShortName = propertyFactory.GetObligatoryPropertyValue<string>("F_SHORT_NAME", attributeType.ShortName);
      attributeTypeProperties.Mask = propertyFactory.GetObligatoryPropertyValue<string>("F_MASK", attributeType.Mask);
      attributeTypeProperties.Alias = propertyFactory.GetObligatoryPropertyValue<string>("F_ALIAS", attributeType.Alias);
      attributeTypeProperties.Unique = propertyFactory.GetObligatoryPropertyValue<UniqueValueModes>("F_UNIQUE", attributeType.UniqueMode);
      attributeTypeProperties.Options = propertyFactory.GetOptions(attributeType.Options, true);
      attributeType.PropertiesStructure = attributeTypeProperties;
      if (propertyValue2 != null && propertyValue2.Count > 0 && propertyFactory.IsPropertyObligatory("F_EXTENSIONS"))
        this.SetExtensions(attributeType as DBMetadataExtensions, propertyValue2);
      if (propertyFactory.IsPropertyObligatory("F_GROUP_ID"))
        this.AddAtributeToGroups(attributeType, propertyValue4);
    }
    if (obj != null && (flag || propertyFactory.IsPropertyObligatory("F_DEFAULT_VALUE")))
      attributeType.DefaultValue = obj;
    categoryID = 3;
    id = (object) attributeType.AttributeID;
  }

  private void AddAtributeToGroups(IDBAttributeType attrType, string groups)
  {
    if (groups == string.Empty)
      return;
    string[] strArray = groups.Split('|');
    for (int index = 0; index < strArray.Length; ++index)
    {
      if (GuidHelper.IsGuid(strArray[index]))
      {
        IDBAttributesGroup attributesGroup = this.session.GetAttributesGroup(new Guid(strArray[index]), true);
        if (!attributesGroup.HasAttribute(attrType.AttributeID))
          attributesGroup.IncludeAttribute(attrType.AttributeID);
      }
    }
  }
}
