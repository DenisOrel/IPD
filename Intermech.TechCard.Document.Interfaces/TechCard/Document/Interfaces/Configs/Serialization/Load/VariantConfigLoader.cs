// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Serialization.Load.VariantConfigLoader
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

[DocumentConfigElementLoader(DocumentConfigElementType.Variant)]
internal class VariantConfigLoader : DocumentConfigElementLoader
{
  private void LoadVariants(VariantConfig targetConfig, XElement sourceNode)
  {
    foreach (XElement element in sourceNode.Elements())
    {
      string str = Convert.ToString(element.Value);
      if (string.IsNullOrEmpty(targetConfig.Id) || string.IsNullOrEmpty(str) || !(targetConfig.Id == str))
        targetConfig.ChildsList.Add(str);
    }
  }

  private IFieldContents LoadContents(XElement childNode)
  {
    TechCardDocumentConfigLoadService service = ApplicationServices.Container.GetService<TechCardDocumentConfigLoadService>();
    return service == null ? (IFieldContents) null : service.Load(childNode) as IFieldContents;
  }

  protected override void LoadConfig([NotNull] IDocumentConfigElement configElement, [NotNull] XElement configNode)
  {
    if (configNode.GetConfigType() != DocumentConfigElementType.Variant || !(configElement is VariantConfig targetConfig))
      return;
    foreach (XElement element in configNode.Elements())
    {
      string localName = element.Name.LocalName;
      string str = Convert.ToString(element.Value);
      switch (localName)
      {
        case "number":
          int result1;
          if (!int.TryParse(str, out result1))
            result1 = 1;
          targetConfig.Number = result1;
          continue;
        case "on_detail":
          bool result2;
          if (!bool.TryParse(str, out result2))
            result2 = false;
          targetConfig.OnDetail = result2;
          continue;
        case "object_type":
          if (!string.IsNullOrEmpty(str))
          {
            targetConfig.ObjType = MetaDataHelper.GetObjectType(new Guid(str));
            continue;
          }
          continue;
        case "variants":
          this.LoadVariants(targetConfig, element);
          continue;
        case "condition":
          XElement childNode = element.Elements().Where<XElement>((Func<XElement, bool>) (child => child.GetConfigType().IsFieldContentsType())).FirstOrDefault<XElement>();
          if (childNode != null)
          {
            targetConfig.Condition = this.LoadContents(childNode);
            continue;
          }
          continue;
        default:
          continue;
      }
    }
  }
}
