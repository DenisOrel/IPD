// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Generate.Interfaces.TechCardDocumentGenerateParameter
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using System;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Generate.Interfaces;

[Serializable]
public class TechCardDocumentGenerateParameter
{
  public TechCardDocumentGenerateParameter(long configId, long rootObjectId)
  {
    this.ConfigId = configId;
    this.RootObjectId = rootObjectId;
  }

  public long ConfigId { get; }

  public long RootObjectId { get; }

  public int ExpertTaskId { get; set; }
}
