// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.AttributeTypePropertyFactory
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System.Data;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal class AttributeTypePropertyFactory : OptionizedPropertyFactory<AttributeOptions>
{
  public FieldTypes FieldType;

  public AttributeTypeProperties AttributeTypeProperties
  {
    get
    {
      return new AttributeTypeProperties()
      {
        Options = this.GetOptions(AttributeOptions.None),
        SourceAttributeID = this.GetPropertyValue<int>("F_SOURCE_ID", 0),
        MasterAttributeID = this.GetPropertyValue<int>("F_MASTER_ID", 0),
        SizeType = this.GetPropertyValue<long>("F_SIZE_TYPE", 0L),
        LevelID = this.GetPropertyValue<int>("F_LEVEL_ID", 0),
        LanguageID = this.GetPropertyValue<string>("F_LANGUAGE_ID", string.Empty),
        AreaID = this.GetPropertyValue<string>("F_AREA_ID", string.Empty),
        DefaultValue = this.GetPropertyValue<object>("F_DEFAULT_VALUE", (object) null),
        Formula = this.GetPropertyValue<string>("F_FORMULA", string.Empty),
        PossibleValues = this.GetPropertyValue<DataTable>("F_POSSIBLE_VALUES", (DataTable) null),
        IsContent = this.GetPropertyValue<bool>("F_CONTENT", false),
        Computed = this.GetPropertyValue<ComputeValueModes>("F_COMPUTED", ComputeValueModes.NotComputableValue),
        OptimizationMode = this.GetPropertyValue<OptimizationModes>("F_INVIEW", OptimizationModes.Write),
        Note = this.GetPropertyValue<string>("F_NOTE", string.Empty),
        Name = this.GetPropertyValue<string>("F_NAME", string.Empty),
        ShortName = this.GetPropertyValue<string>("F_SHORT_NAME", string.Empty),
        Mask = this.GetPropertyValue<string>("F_MASK", string.Empty),
        Alias = this.GetPropertyValue<string>("F_ALIAS", string.Empty),
        MultiValueMode = this.GetPropertyValue<MultiValueModes>("F_MULTIPLE_VALUED", MultiValueModes.SingleValue),
        FieldType = this.GetPropertyValue<FieldTypes>("F_ATTRIBUTE_TYPE", FieldTypes.ftUnknown),
        Unique = this.GetPropertyValue<UniqueValueModes>("F_UNIQUE", UniqueValueModes.NotUnique)
      };
    }
  }

  protected override IPropertyNode GetPropertyNode(
    IUserSession session,
    XmlNode node,
    string nodeID)
  {
    IPropertyNode propertyNode;
    switch (nodeID)
    {
      case "F_ACCESS":
        propertyNode = (IPropertyNode) new AccessNode(session, node);
        break;
      case "F_AREA_ID":
        propertyNode = (IPropertyNode) new AreaNode(session, node, this.Directory);
        break;
      case "F_ATTRIBUTE_TYPE":
        propertyNode = (IPropertyNode) new EnumNode<FieldTypes>(session, node, nodeID);
        break;
      case "F_COMPUTED":
        propertyNode = (IPropertyNode) new EnumNode<ComputeValueModes>(session, node, nodeID);
        break;
      case "F_CONTENT":
        propertyNode = (IPropertyNode) new BooleanNode(session, node, nodeID);
        break;
      case "F_DEFAULT_VALUE":
        propertyNode = (IPropertyNode) new DefaultValueNode(session, node, this.FieldType);
        break;
      case "F_FORMULA":
        propertyNode = (IPropertyNode) new FormulaNode(session, node);
        break;
      case "F_INVIEW":
        propertyNode = (IPropertyNode) new EnumNode<OptimizationModes>(session, node, nodeID);
        break;
      case "F_MASTER_ID":
      case "F_SOURCE_ID":
        propertyNode = (IPropertyNode) new AttributeIDNode(session, node, nodeID);
        break;
      case "F_MULTIPLE_VALUED":
        propertyNode = (IPropertyNode) new EnumNode<MultiValueModes>(session, node, nodeID);
        break;
      case "F_POSSIBLE_VALUES":
        propertyNode = (IPropertyNode) new PossibleValuesNode(session, node, this.Directory);
        break;
      case "F_SIZE_TYPE":
        propertyNode = (IPropertyNode) new SizeTypeNode(session, node, this.FieldType);
        break;
      case "F_UNIQUE":
        propertyNode = (IPropertyNode) new EnumNode<UniqueValueModes>(session, node, nodeID);
        break;
      default:
        propertyNode = base.GetPropertyNode(session, node, nodeID);
        break;
    }
    return propertyNode;
  }
}
