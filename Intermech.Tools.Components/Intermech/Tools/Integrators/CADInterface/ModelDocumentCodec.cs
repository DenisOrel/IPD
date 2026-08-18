// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ModelDocumentCodec
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Tools.Data;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public sealed class ModelDocumentCodec : DocumentAttributesCodec
{
  public ModelDocumentCodec()
    : base((IValueBagFormatter) new ModelDocumentFormatter())
  {
    this.SaveDesignationSuffix = false;
    this.Formatter.WriteTargetStrategy = (ModelParametersWriteTargetStrategy) new DefaultModelParametersWriteTargetStrategy();
  }

  private ModelDocumentFormatter Formatter => (ModelDocumentFormatter) base.Formatter;

  protected override IAttributeLayout GetContainerAttributeLayout(StringKey attributeKey)
  {
    if (attributeKey == (StringKey) IDCache.Default.Designation.Text)
      return (IAttributeLayout) new ModelDesignationLayout(this.GetContainerValueKey(attributeKey));
    return attributeKey == (StringKey) IDCache.Default.Name.Text ? (IAttributeLayout) new ModelNameLayout(this.GetContainerValueKey(attributeKey)) : base.GetContainerAttributeLayout(attributeKey);
  }
}
