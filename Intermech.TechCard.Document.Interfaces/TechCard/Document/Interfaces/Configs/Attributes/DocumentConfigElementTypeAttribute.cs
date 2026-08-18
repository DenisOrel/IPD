// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Attributes.DocumentConfigElementTypeAttribute
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.TechCard.Document.Interfaces.Configs.Common;
using System;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class DocumentConfigElementTypeAttribute : Attribute
{
  public readonly DocumentConfigElementType ConfigElementType;

  public DocumentConfigElementTypeAttribute(DocumentConfigElementType configType)
  {
    this.ConfigElementType = configType;
  }
}
