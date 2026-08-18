// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.SchemaComponentCodec
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Data;
using Intermech.Tools.Integrators.Mechanical;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class SchemaComponentCodec : ArticleAttributesCodec
{
  private readonly AttributeTable attrTable;

  public SchemaComponentCodec(SettingsService settingsSvc)
    : base((IValueBagFormatter) new SchemaPropertiesFormatter())
  {
    this.attrTable = new AttributeTable(settingsSvc, AttributesTableKind.ComponentAttributes);
  }

  protected override StringKey GetContainerValueKey(StringKey attributeKey)
  {
    return this.attrTable.GetFormatterValueKey(attributeKey, base.GetContainerValueKey(attributeKey));
  }
}
