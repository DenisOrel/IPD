// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.LogManager
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using System.IO;

#nullable disable
namespace Intermech.TwainScanner;

public class LogManager
{
  private static string directory = (string) null;
  private static StreamWriter sw = (StreamWriter) null;
  private static string fileName = "twainscanner.log";

  public static void AddLine(string text)
  {
  }
}
