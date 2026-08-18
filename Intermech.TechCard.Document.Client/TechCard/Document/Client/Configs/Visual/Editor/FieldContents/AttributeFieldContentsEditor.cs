// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Configs.Visual.Editor.FieldContents.AttributeFieldContentsEditor
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.PropertyEditors;
using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Structure;
using Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Document.Client.Configs.Visual.Editor.FieldContents;

[FieldContentsTypeEditor(FieldContentsType.Attribute)]
internal class AttributeFieldContentsEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    if (!(value is AttributeFieldContents attributeFieldContents))
      return value;
    using (AdvSelectorForm advSelectorForm = new AdvSelectorForm(AdvSelector.AttributableTypeWithAttributeType, AttributableElements.Object, -1, attributeFieldContents.AttributeSettings != null ? attributeFieldContents.AttributeSettings.GetItemTypeId() : TechCardConsts.ObjectTypes.TechBaseObjectID))
    {
      advSelectorForm.Text = "Выберите атрибут";
      if (advSelectorForm.ShowDialog() != DialogResult.OK)
        return value;
      return (object) new AttributeFieldContents()
      {
        AttributeSettings = new AttributeSettings(AttributableElements.Object, MetaDataHelper.GetObjectTypeGuid(advSelectorForm.ObjectType), MetaDataHelper.GetAttributeTypeGuid(advSelectorForm.AttributeTypes[0]))
      };
    }
  }
}
