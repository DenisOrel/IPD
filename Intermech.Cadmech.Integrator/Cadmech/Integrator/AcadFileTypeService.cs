// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AcadFileTypeService
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.IO;
using Intermech.Tools.Integrators;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class AcadFileTypeService(IIntegrator owner) : ContentBasedFileTypesService(owner)
{
  private static readonly Regex acadVersionFormat = new Regex("^AC\\d\\d\\d\\d$", RegexOptions.Compiled);

  protected override PathCollection GetFileExtensions()
  {
    PathCollection fileExtensions = base.GetFileExtensions();
    fileExtensions.Add(".dwg");
    return fileExtensions;
  }

  protected override bool VerifyFileContent(FileInfo fileInfo, Stream fileContent)
  {
    if (fileInfo == null)
      throw new ArgumentNullException(nameof (fileInfo));
    string input = fileContent != null ? this.ReadAcadVersionString(fileContent) : throw new ArgumentNullException(nameof (fileContent));
    return !string.IsNullOrEmpty(input) && AcadFileTypeService.acadVersionFormat.IsMatch(input);
  }

  private string ReadAcadVersionString(Stream fileContent)
  {
    if (fileContent.Length - fileContent.Position < 6L)
      return string.Empty;
    byte[] numArray = new byte[6];
    fileContent.Read(numArray, 0, numArray.Length);
    return Encoding.ASCII.GetString(numArray);
  }
}
