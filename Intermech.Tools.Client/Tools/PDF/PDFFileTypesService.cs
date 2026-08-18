// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDF.PDFFileTypesService
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.IO;
using Intermech.Tools.Integrators;

#nullable disable
namespace Intermech.Tools.PDF;

internal sealed class PDFFileTypesService(IIntegrator owner) : NameBasedFileTypesService(owner)
{
  protected override PathCollection GetFileExtensions()
  {
    PathCollection fileExtensions = base.GetFileExtensions();
    fileExtensions.Add(".pdf");
    return fileExtensions;
  }
}
