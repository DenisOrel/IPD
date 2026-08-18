// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ResourceFuncs
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System;
using System.IO;
using System.Reflection;

#nullable disable
namespace Intermech.Workflow.Design;

public class ResourceFuncs
{
  /// <summary>Распаковывает файлы из ресурсов в указанную папку</summary>
  /// <param name="ResourceAssembly">Сборка, в которой находятся загружаемые ресурсы</param>
  /// <param name="files">Список путей к файлам в сборке (с указанием папок). Папки разделяюся символом '.', '/' или '\'!</param>
  /// <param name="OutDir">Папка, в которую распаковывать</param>
  public static void ExtractResourceFiles(Assembly ResourceAssembly, string[] files, string OutDir)
  {
    char[] anyOf = new char[2]{ '\\', '/' };
    for (int index = 0; index < files.Length; ++index)
    {
      string str = files[index];
      string path2 = str;
      int num1 = str.LastIndexOfAny(anyOf);
      if (num1 != -1)
      {
        path2 = str.Substring(num1 + 1);
      }
      else
      {
        int num2 = str.LastIndexOf('.');
        if (num2 != -1)
        {
          int num3 = str.LastIndexOf('.', num2 - 1, num2 - 1);
          if (num3 != -1)
            num2 = num3;
          path2 = str.Substring(num2 + 1);
        }
      }
      if (!Directory.Exists(OutDir))
        Directory.CreateDirectory(OutDir);
      string path = Path.Combine(OutDir, path2);
      if (!File.Exists(path))
      {
        foreach (char oldChar in anyOf)
          str = str.Replace(oldChar, '.');
        using (Stream manifestResourceStream = ResourceAssembly.GetManifestResourceStream($"{ResourceAssembly.GetName().Name}.{str}"))
        {
          if (manifestResourceStream != null)
          {
            try
            {
              using (FileStream fileStream = new FileStream(path, FileMode.Create))
              {
                byte[] buffer = new byte[manifestResourceStream.Length];
                manifestResourceStream.Read(buffer, 0, (int) manifestResourceStream.Length);
                fileStream.Write(buffer, 0, (int) manifestResourceStream.Length);
              }
            }
            catch (IOException ex)
            {
            }
          }
        }
      }
    }
  }

  public static void ExtractResourcesFolder(
    Assembly ResourceAssembly,
    string ResPath,
    string OutPath)
  {
    string[] manifestResourceNames = ResourceAssembly.GetManifestResourceNames();
    string name = ResourceAssembly.GetName().Name;
    string str1 = $"{name}.{ResPath}.";
    foreach (string str2 in manifestResourceNames)
    {
      if (str2.StartsWith(str1))
      {
        string str3 = str2.Replace(name + ".", "");
        string[] array = str3.Split('.');
        int length = array.Length;
        if (length > 0)
        {
          string str4 = array[length - 1];
          if (length > 1 && array[length - 1].Length <= 3)
          {
            string str5 = $"{array[length - 2]}.{array[length - 1]}";
            Array.Resize<string>(ref array, length - 2);
          }
          string path2 = string.Join("\\", array);
          string OutDir = Path.Combine(OutPath, path2);
          ResourceFuncs.ExtractResourceFiles(ResourceAssembly, new string[1]
          {
            str3
          }, OutDir);
        }
      }
    }
  }
}
