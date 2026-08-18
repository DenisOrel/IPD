// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.FileFuncs
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.IO;

#nullable disable
namespace Intermech.Workflow.Design;

public class FileFuncs
{
  public static string IncludeTrailingPathDelimiter(string s)
  {
    return s.Length > 0 && (int) s[s.Length - 1] != (int) Path.DirectorySeparatorChar ? s + Path.DirectorySeparatorChar.ToString() : s;
  }

  /// <summary>
  /// Removes files from the directory, non recursive. Directories are not deleted.
  /// </summary>
  /// <param name="Dir"></param>
  public static void DeleteFiles(string Dir)
  {
    foreach (string file in Directory.GetFiles(Dir))
      File.Delete(file);
  }
}
