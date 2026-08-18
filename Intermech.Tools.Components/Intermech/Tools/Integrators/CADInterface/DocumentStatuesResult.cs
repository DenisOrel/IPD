// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.DocumentStatuesResult
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Interop.CADInterface;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

internal class DocumentStatuesResult
{
  public readonly EDocumentStatus[] StatusArray;
  public readonly string[] UserNamesArray;

  public DocumentStatuesResult(int length)
  {
    this.StatusArray = new EDocumentStatus[length];
    this.UserNamesArray = new string[length];
  }
}
