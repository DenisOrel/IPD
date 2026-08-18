// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Serialization.Load.TextFieldLoadercs
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
using Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;
using System;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Serialization.Load;

[DocumentConfigElementLoader(DocumentConfigElementType.TextField)]
internal class TextFieldLoadercs : DocumentConfigElementLoader
{
  private IFieldContents LoadContents(XElement childNode)
  {
    TechCardDocumentConfigLoadService service = ApplicationServices.Container.GetService<TechCardDocumentConfigLoadService>();
    return service == null ? (IFieldContents) null : service.Load(childNode) as IFieldContents;
  }

  protected override void LoadConfig([NotNull] IDocumentConfigElement configElement, [NotNull] XElement configNode)
  {
    if (configNode.GetConfigType() != DocumentConfigElementType.TextField || !(configElement is TextFieldConfig textFieldConfig))
      return;
    foreach (XElement element in configNode.Elements())
    {
      string localName = element.Name.LocalName;
      string s = Convert.ToString(element.Value);
      bool result1;
      switch (localName)
      {
        case "digits":
          int result2;
          if (!int.TryParse(s, out result2))
            result2 = 3;
          textFieldConfig.Digits = result2;
          continue;
        case "not_repeated":
          if (!bool.TryParse(s, out result1))
            result1 = false;
          textFieldConfig.NotRepeated = result1;
          continue;
        case "calc_on_fill":
          if (!bool.TryParse(s, out result1))
            result1 = false;
          textFieldConfig.CalcOnFill = result1;
          continue;
        case "field_contents":
          XElement childNode1 = element.Elements().Where<XElement>((Func<XElement, bool>) (child => child.GetConfigType().IsFieldContentsType())).FirstOrDefault<XElement>();
          if (childNode1 != null)
          {
            textFieldConfig.FieldContents = this.LoadContents(childNode1);
            continue;
          }
          continue;
        case "condition":
          XElement childNode2 = element.Elements().Where<XElement>((Func<XElement, bool>) (child => child.GetConfigType().IsFieldContentsType())).FirstOrDefault<XElement>();
          if (childNode2 != null)
          {
            textFieldConfig.Condition = this.LoadContents(childNode2);
            continue;
          }
          continue;
        default:
          continue;
      }
    }
  }
}
