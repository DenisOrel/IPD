// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Serialization.Services.TechCardDocumentConfigSerializeService
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Serialization.Save;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Serialization.Services;

public class TechCardDocumentConfigSerializeService
{
  private Dictionary<DocumentConfigElementType, DocumentConfigElementSerializer> _serializers = new Dictionary<DocumentConfigElementType, DocumentConfigElementSerializer>();

  private void InitSerializers()
  {
    ((IEnumerable<Type>) Assembly.GetExecutingAssembly().GetTypes()).ToList<Type>().ForEach((Action<Type>) (type =>
    {
      IEnumerable<DocumentConfigElementSerializerAttribute> customAttributes = type.GetCustomAttributes<DocumentConfigElementSerializerAttribute>();
      if (customAttributes == null || customAttributes.Count<DocumentConfigElementSerializerAttribute>() == 0)
        return;
      foreach (DocumentConfigElementSerializerAttribute serializerAttribute in customAttributes)
        this._serializers[serializerAttribute.ConfigElementType] = Activator.CreateInstance(type) as DocumentConfigElementSerializer;
    }));
  }

  public TechCardDocumentConfigSerializeService() => this.InitSerializers();

  [CanBeNull]
  public XElement Serialize([NotNull] IDocumentConfigElement documentConfig)
  {
    DocumentConfigElementSerializer elementSerializer;
    return this._serializers.TryGetValue(documentConfig.ElementType, out elementSerializer) ? elementSerializer.Serialize(documentConfig) : (XElement) null;
  }
}
