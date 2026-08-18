// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Serialization.Save.TextFieldSerializer
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Serialization.Services;
using Intermech.TechCard.Document.Interfaces.Configs.Structure;
using System.Xml.Linq;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Serialization.Save;

[DocumentConfigElementSerializer(DocumentConfigElementType.TextField)]
internal class TextFieldSerializer : DocumentConfigElementSerializer
{
  protected override void SerializeConfig([NotNull] IDocumentConfigElement configElement, [NotNull] XElement configNode)
  {
    if (!(configElement is TextFieldConfig textFieldConfig))
      return;
    configNode.Add((object) new XElement((XName) "digits", (object) textFieldConfig.Digits.ToString()), (object) new XElement((XName) "not_repeated", (object) textFieldConfig.NotRepeated.ToString()), (object) new XElement((XName) "calc_on_fill", (object) textFieldConfig.CalcOnFill.ToString()));
    TechCardDocumentConfigSerializeService service = ApplicationServices.Container.GetService<TechCardDocumentConfigSerializeService>();
    if (service == null)
      return;
    if (textFieldConfig.FieldContents is IDocumentConfigElement fieldContents)
    {
      XElement content1 = service.Serialize(fieldContents);
      if (content1 != null)
      {
        XElement content2 = new XElement((XName) "field_contents");
        content2.Add((object) content1);
        configNode.Add((object) content2);
      }
    }
    if (!(textFieldConfig.Condition is IDocumentConfigElement condition))
      return;
    XElement content3 = service.Serialize(condition);
    if (content3 == null)
      return;
    XElement content4 = new XElement((XName) "condition");
    content4.Add((object) content3);
    configNode.Add((object) content4);
  }
}
