// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DwgTasks.StmFile
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Cadmech.Integrator.DwgTasks;

internal static class StmFile
{
  private static readonly Regex fieldDecoder = new Regex("\\d+#(?<field>.+)#(.*#){7}", RegexOptions.IgnoreCase | RegexOptions.Singleline);

  public static string Locate(DrawingTypeSettings dwgTypeSettings)
  {
    if (dwgTypeSettings == null)
      throw new ArgumentNullException();
    if (!string.IsNullOrEmpty(dwgTypeSettings.StmName))
    {
      foreach (string stmFilesLocation in StmFile.GetStmFilesLocations())
      {
        string path = StmFile.CombineStmFilePath(stmFilesLocation, dwgTypeSettings.StmName);
        if (File.Exists(path))
          return path;
      }
    }
    return (string) null;
  }

  private static List<string> GetStmFilesLocations()
  {
    List<string> stmFilesLocations = new List<string>();
    string appSetting = ConfigurationManager.AppSettings["StmFilesPath"];
    if (!string.IsNullOrEmpty(appSetting))
    {
      string str = appSetting;
      char[] separator = new char[1]{ ';' };
      foreach (string stmLocation in str.Split(separator, StringSplitOptions.RemoveEmptyEntries))
        stmFilesLocations.Add(StmFile.ExpandStmLocation(stmLocation));
    }
    if (stmFilesLocations.Count == 0)
    {
      string environmentVariable = Environment.GetEnvironmentVariable("IMTMP", EnvironmentVariableTarget.User);
      if (!string.IsNullOrEmpty(environmentVariable))
        stmFilesLocations.Add(StmFile.ExpandStmLocation(environmentVariable));
    }
    stmFilesLocations.RemoveAll((Predicate<string>) (location => location.IndexOfAny(Path.GetInvalidPathChars()) >= 0 || !Directory.Exists(location)));
    return stmFilesLocations;
  }

  private static string CombineStmFilePath(string stmLocation, string stmFileName)
  {
    return Path.Combine(Path.IsPathRooted(stmLocation) ? stmLocation : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, stmLocation), stmFileName);
  }

  private static string ExpandStmLocation(string stmLocation)
  {
    return Environment.ExpandEnvironmentVariables(stmLocation.Trim());
  }

  public static List<string> ReadFields(string stmFilePath)
  {
    string[] strArray = !string.IsNullOrEmpty(stmFilePath) ? File.ReadAllLines(stmFilePath, Encoding.Default) : throw new ArgumentException();
    List<string> stringList = new List<string>(strArray.Length);
    foreach (string input in strArray)
    {
      Match match = StmFile.fieldDecoder.Match(input);
      if (match.Success)
      {
        string str = match.Groups["field"].Value;
        if (!stringList.Contains(str))
          stringList.Add(str);
      }
    }
    return stringList;
  }
}
