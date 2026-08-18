// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CIDocumentHelper
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

internal static class CIDocumentHelper
{
  public static DocumentFileData ReadDocumentData(
    string masterFilePath,
    CADDocumentProxy cadDocument)
  {
    if (masterFilePath == null)
      throw new ArgumentNullException(nameof (masterFilePath));
    if (cadDocument == null)
      throw new ArgumentNullException(nameof (cadDocument));
    CIDocumentData sectionObject = new CIDocumentData();
    sectionObject.Document = cadDocument;
    DocumentFileData documentFileData = new DocumentFileData(masterFilePath);
    documentFileData.CustomSections.Set((object) sectionObject);
    return documentFileData;
  }
}
