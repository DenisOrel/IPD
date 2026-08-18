// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.GuidHelper
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

public class GuidHelper
{
  private static Regex _guidRegex;

  public static Regex GuidRegex
  {
    get
    {
      if (GuidHelper._guidRegex == null)
        GuidHelper._guidRegex = new Regex("[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}", RegexOptions.Compiled);
      return GuidHelper._guidRegex;
    }
  }

  public static bool IsGuid(string text)
  {
    if (text == null || text.Length == 0)
      return false;
    if (text.IndexOf('{', 0, 1) == 0 && text.LastIndexOf('}', text.Length - 1, 1) == text.Length - 1)
      text = text.Substring(1, text.Length - 2);
    return text.Length == 36 && GuidHelper.GuidRegex.IsMatch(text);
  }
}
