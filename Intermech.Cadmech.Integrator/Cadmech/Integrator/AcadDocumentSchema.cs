// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AcadDocumentSchema
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Interfaces;
using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal static class AcadDocumentSchema
{
  internal static DocumentTypesGroup CreateDrawingTypeGroup(
    List<DrawingTypeSettings> dwgTypes,
    Guid groupId)
  {
    DocumentTypesGroup drawingTypeGroup = new DocumentTypesGroup(groupId);
    foreach (DrawingTypeSettings dwgType in dwgTypes)
      drawingTypeGroup.DocumentTypes.Add(new LocalId<int>(dwgType.DocumentType.Id, dwgType.DocumentType.Name));
    return drawingTypeGroup;
  }

  internal static DocumentTypesGroup CreateDocumentTypeGroup(
    List<GlobalId<int>> docTypes,
    Guid groupId)
  {
    DocumentTypesGroup documentTypeGroup = new DocumentTypesGroup(groupId);
    foreach (GlobalId<int> docType in docTypes)
      documentTypeGroup.DocumentTypes.Add(new LocalId<int>(docType.Id, docType.Name));
    return documentTypeGroup;
  }
}
