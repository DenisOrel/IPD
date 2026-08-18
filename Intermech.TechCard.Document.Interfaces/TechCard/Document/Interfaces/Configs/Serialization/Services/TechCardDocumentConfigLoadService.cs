// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Serialization.Services.TechCardDocumentConfigLoadService
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Serialization.Load;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Serialization.Services;

public class TechCardDocumentConfigLoadService
{
  private Dictionary<DocumentConfigElementType, DocumentConfigElementLoader> _loaders = new Dictionary<DocumentConfigElementType, DocumentConfigElementLoader>();

  private void InitSerializers()
  {
    ((IEnumerable<Type>) Assembly.GetExecutingAssembly().GetTypes()).ToList<Type>().ForEach((Action<Type>) (type =>
    {
      IEnumerable<DocumentConfigElementLoaderAttribute> customAttributes = type.GetCustomAttributes<DocumentConfigElementLoaderAttribute>();
      if (customAttributes == null || customAttributes.Count<DocumentConfigElementLoaderAttribute>() == 0)
        return;
      foreach (DocumentConfigElementLoaderAttribute elementLoaderAttribute in customAttributes)
        this._loaders[elementLoaderAttribute.ConfigElementType] = Activator.CreateInstance(type) as DocumentConfigElementLoader;
    }));
  }

  public TechCardDocumentConfigLoadService() => this.InitSerializers();

  [CanBeNull]
  public IDocumentConfigElement Load([NotNull] XElement rootElement)
  {
    DocumentConfigElementLoader configElementLoader;
    return this._loaders.TryGetValue(rootElement.GetConfigType(), out configElementLoader) ? configElementLoader.Load(rootElement) : (IDocumentConfigElement) null;
  }
}
