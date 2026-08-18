// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents.IFieldContents
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;

public interface IFieldContents
{
  FieldContentsType ContentsType { get; }

  void CollectAttributeSettings(ICollection<AttributeSettings> attributeSettings);

  string ToString();
}
