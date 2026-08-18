// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Serialization.Load.DocumentConfigElementLoader
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Structure;
using System.Xml.Linq;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Serialization.Load;

internal abstract class DocumentConfigElementLoader
{
  protected abstract void LoadConfig([NotNull] IDocumentConfigElement configElement, [NotNull] XElement configNode);

  [CanBeNull]
  public virtual IDocumentConfigElement Load([NotNull] XElement source)
  {
    DocumentConfigElementType configType = source.GetConfigType();
    if (configType == DocumentConfigElementType.Unknown)
      return (IDocumentConfigElement) null;
    DocumentConfigElement documentElementConfig = DocumentConfigElementFactory.CreateDocumentElementConfig(configType) as DocumentConfigElement;
    XAttribute xattribute = source.Attribute((XName) "id");
    documentElementConfig.Id = xattribute != null ? xattribute.Value : string.Empty;
    this.LoadConfig((IDocumentConfigElement) documentElementConfig, source);
    return (IDocumentConfigElement) documentElementConfig;
  }
}
