// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ImDocumentFormatHelper
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

public class ImDocumentFormatHelper
{
  /// <summary>Вернуть расширение в виде ".pdf"</summary>
  /// <returns></returns>
  public static string GetExtension(ImDocumentFormat format)
  {
    string str = string.Empty;
    switch (format)
    {
      case ImDocumentFormat.XmlFormat:
        str = ".imdx";
        break;
      case ImDocumentFormat.WmfFormat:
        str = ".wmf";
        break;
      case ImDocumentFormat.PdfFormat:
        str = ".pdf";
        break;
    }
    return str;
  }
}
