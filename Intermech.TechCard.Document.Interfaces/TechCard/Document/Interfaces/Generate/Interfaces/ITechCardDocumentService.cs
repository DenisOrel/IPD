// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Generate.Interfaces.ITechCardDocumentService
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces.Document;
using System;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Generate.Interfaces;

public interface ITechCardDocumentService
{
  bool GenerateDocument(
    Guid sessionGuid,
    [NotNull] TechCardDocumentGenerateParameter parameter,
    out ImDocumentData documentData);

  bool GenerateDocument(
    Guid sessionGuid,
    [NotNull] TechCardDocumentGenerateParameter parameter,
    out byte[] documentData);

  bool GenerateDocument(
    Guid sessionGuid,
    [NotNull] TechCardDocumentGenerateParameter parameter,
    out long docId);
}
