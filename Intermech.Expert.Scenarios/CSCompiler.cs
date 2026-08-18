// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Scenarios.CSCompiler
// Assembly: Intermech.Expert.Scenarios, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 67A596D2-F145-4D6C-A4AA-0257621BF410
// Assembly location: D:\IPS\Client\Intermech.Expert.Scenarios.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Scenarios.xml

using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

#nullable disable
namespace Intermech.Expert.Scenarios;

/// <summary>C# компилятор</summary>
internal sealed class CSCompiler : ICompiler
{
  private static bool firstStart = true;

  public Assembly Compile(string code, string[] references, string name)
  {
    List<string> stringList = new List<string>((IEnumerable<string>) references);
    int endIndex = code.Length - 1;
    foreach (string rawStatement in this.GetRawStatements(code, "//css_ref", endIndex))
    {
      if (!rawStatement.EndsWith(".dll"))
        rawStatement += ".dll";
      bool flag = true;
      foreach (string reference in references)
      {
        if (reference.Contains(rawStatement))
        {
          flag = false;
          break;
        }
      }
      if (flag)
        stringList.Add(rawStatement.Trim());
    }
    references = stringList.ToArray();
    CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider();
    CompilerParameters options = new CompilerParameters(references);
    string tempPath = Path.GetTempPath();
    options.OutputAssembly = Path.Combine(tempPath, name);
    options.GenerateInMemory = true;
    options.TempFiles = new TempFileCollection(tempPath);
    bool isAttached = Debugger.IsAttached;
    options.IncludeDebugInformation = isAttached;
    CompilerResults compilerResults;
    if (isAttached)
    {
      string str = tempPath + "\\IMCSCompiler\\";
      if (CSCompiler.firstStart)
      {
        CSCompiler.firstStart = false;
        if (Directory.Exists(str))
        {
          DirectoryInfo directoryInfo = new DirectoryInfo(str);
          foreach (FileSystemInfo file in directoryInfo.GetFiles())
            file.Delete();
          foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
            directory.Delete(true);
        }
      }
      if (!Directory.Exists(str))
        Directory.CreateDirectory(str);
      string path1 = Path.GetTempFileName() + ".cs";
      string path2 = Path.Combine(str, Path.GetFileName(path1));
      StreamWriter streamWriter = new StreamWriter((Stream) new FileStream(path2, FileMode.Create));
      streamWriter.Write(code);
      streamWriter.Close();
      compilerResults = csharpCodeProvider.CompileAssemblyFromFile(options, path2);
    }
    else
      compilerResults = csharpCodeProvider.CompileAssemblyFromSource(options, code);
    if (compilerResults.Errors.HasErrors)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("Ошибки выполнения сценария:");
      foreach (CompilerError error in (CollectionBase) compilerResults.Errors)
        stringBuilder.AppendLine($"Line:{error.Line:d}, Error:{error.ErrorText}\n");
      throw new Exception(stringBuilder.ToString());
    }
    Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
    return assemblies[assemblies.Length - 1];
  }

  private bool IsToken(string code, int startPos, int length)
  {
    if (code.Length < startPos + length)
      return false;
    int startIndex = startPos != 0 ? startPos - 1 : 0;
    int num = code.Length == startPos + length ? startPos + length : startPos + length + 1;
    string str = code.Substring(startPos, length);
    return code.Substring(startIndex, num - startIndex).Replace(";", "").Replace("(", "").Replace(")", "").Replace("{", "").Trim().Length == str.Length;
  }

  private string[] GetRawStatements(string code, string pattern, int endIndex)
  {
    ArrayList arrayList = new ArrayList();
    int num1 = code.IndexOf(pattern);
    for (; num1 != -1 && num1 <= endIndex; num1 = code.IndexOf(pattern, num1 + 1))
    {
      if (this.IsToken(code, num1, pattern.Length))
      {
        num1 += pattern.Length;
        int num2 = code.IndexOf(";", num1);
        if (num2 != -1)
          arrayList.Add((object) code.Substring(num1, num2 - num1).Trim());
      }
    }
    return (string[]) arrayList.ToArray(typeof (string));
  }
}
