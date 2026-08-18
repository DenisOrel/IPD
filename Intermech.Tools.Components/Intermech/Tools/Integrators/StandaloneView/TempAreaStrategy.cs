// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.StandaloneView.TempAreaStrategy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.IO;
using System;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators.StandaloneView;

/// <summary>
/// Стратегия создания временного файла, которая создает временный файл во временном каталоге.
/// </summary>
public class TempAreaStrategy : TempFileStrategy
{
  /// <summary>Инициализирует стратегию.</summary>
  /// <param name="operation">Контейнер данных для операции</param>
  /// <returns>Кортеж из абсолютного пути к каталогу временного файла и абсолютного пути к самому временному файлу документа</returns>
  protected override Tuple<string, string> DoInitialize(
    StandaloneViewDataInjectionOperation operation)
  {
    string str1 = Path.Combine(this.FileVault.TempArea.AreaPath, "SVIEW_" + this.MakeRandomFileName());
    string path2 = $"{Path.GetFileNameWithoutExtension(operation.Parameters.FileName)}_{this.MakeRandomFileName()}{Path.GetExtension(operation.Parameters.FileName)}";
    string str2 = Path.Combine(str1, path2);
    if (!Directory.Exists(str1))
      Directory.CreateDirectory(str1);
    return Tuple.Create<string, string>(str1, str2);
  }

  protected override void DoRemoveFiles()
  {
    base.DoRemoveFiles();
    FileUtils.DeleteDirectorySilently(this.DirectoryPath, true);
  }
}
