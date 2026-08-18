// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.AttributeType4RelationTypePropertyFactory
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal class AttributeType4RelationTypePropertyFactory(Guid attributeGuid, int attributeID) : 
  AttributeType4TypePropertyFactory(attributeGuid, attributeID)
{
  public Attribute4RelationTypeProperties GetAttributeTypeProperties(IUserSession session)
  {
    IDBAttributeType attributeType = session.GetAttributeType(this.AttributeID);
    return new Attribute4RelationTypeProperties()
    {
      Options = this.GetOptions(attributeType.Options),
      SourceAttributeID = this.GetPropertyValue<int>("F_SOURCE_ID", attributeType.SourceAttributeID),
      MasterAttributeID = this.GetPropertyValue<int>("F_MASTER_ID", attributeType.MasterAttributeID),
      DefaultValue = this.GetPropertyValue<object>("F_DEFAULT_VALUE", attributeType.DefaultValue),
      Formula = this.GetPropertyValue<string>("F_FORMULA", attributeType.Formula),
      IsContent = this.GetPropertyValue<bool>("F_CONTENT", attributeType.IsContent),
      OptimizationMode = this.GetPropertyValue<OptimizationModes>("F_INVIEW", attributeType.OptimizationMode),
      Mask = this.GetPropertyValue<string>("F_MASK", attributeType.Mask),
      FieldType = this.FieldType,
      ComputeValueMode = this.GetPropertyValue<ComputeValueModes>("F_COMPUTED", attributeType.Computed),
      RequiredMode = this.GetPropertyValue<RequiredModes>("F_REQUIRED", RequiredModes.Manual),
      ValidationRule = this.GetPropertyValue<string>("F_VALIDATION_RULE", attributeType.ValidationRule),
      AttributeID = this.AttributeID
    };
  }
}
