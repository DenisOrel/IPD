// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.SchemaDocumentCodec
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Data;
using Intermech.Tools.Integrators;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class SchemaDocumentCodec : DocumentAttributesCodec
{
  private readonly AttributeTable attrTable;

  public SchemaDocumentCodec(SettingsService settingsSvc)
    : base((IValueBagFormatter) new SchemaPropertiesFormatter())
  {
    this.attrTable = new AttributeTable(settingsSvc, AttributesTableKind.SchemaDocumentAttributes);
  }

  protected override StringKey GetContainerValueKey(StringKey attributeKey)
  {
    return this.attrTable.GetFormatterValueKey(attributeKey, base.GetContainerValueKey(attributeKey));
  }
}
