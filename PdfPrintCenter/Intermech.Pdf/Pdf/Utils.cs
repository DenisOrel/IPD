// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Utils
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.IO;

#nullable disable
namespace Syncfusion.Pdf;

internal class Utils
{
  private const int c_roundDecimals = 4;

  private Utils()
  {
  }

  public static string CheckFilePath(string path)
  {
    string path1 = path != null ? Path.GetFullPath(path) : throw new ArgumentNullException(nameof (path));
    return File.Exists(path1) ? path1 : throw new FileNotFoundException("File can't be found");
  }
}
