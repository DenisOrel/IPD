// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Common.DocumentConfigElementFactory
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Common;

public static class DocumentConfigElementFactory
{
  private static readonly Dictionary<DocumentConfigElementType, Type> _configTypesCache = new Dictionary<DocumentConfigElementType, Type>();

  private static void InitCache()
  {
    ((IEnumerable<Type>) Assembly.GetExecutingAssembly().GetTypes()).ToList<Type>().ForEach((Action<Type>) (type =>
    {
      DocumentConfigElementTypeAttribute[] array = type.GetCustomAttributes<DocumentConfigElementTypeAttribute>().ToArray<DocumentConfigElementTypeAttribute>();
      if (!((IEnumerable<DocumentConfigElementTypeAttribute>) array).Any<DocumentConfigElementTypeAttribute>())
        return;
      foreach (DocumentConfigElementTypeAttribute elementTypeAttribute in array)
        DocumentConfigElementFactory._configTypesCache[elementTypeAttribute.ConfigElementType] = type;
    }));
  }

  static DocumentConfigElementFactory() => DocumentConfigElementFactory.InitCache();

  [CanBeNull]
  public static IDocumentConfigElement CreateDocumentElementConfig(
    DocumentConfigElementType configType)
  {
    Type type;
    return DocumentConfigElementFactory._configTypesCache.TryGetValue(configType, out type) && Activator.CreateInstance(type) is IDocumentConfigElement instance ? instance : (IDocumentConfigElement) null;
  }
}
