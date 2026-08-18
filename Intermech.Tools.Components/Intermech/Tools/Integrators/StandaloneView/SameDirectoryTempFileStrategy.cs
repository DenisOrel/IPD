// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.StandaloneView.SameDirectoryTempFileStrategy
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
/// Стратегия создания временного файла, которая создает временный файл в том же каталоге диске, где находится оригинальный файл документа.
/// </summary>
public class SameDirectoryTempFileStrategy : TempFileStrategy
{
  /// <summary>Инициализирует стратегию.</summary>
  /// <param name="operation">Контейнер данных для операции</param>
  /// <returns>Кортеж из абсолютного пути к каталогу временного файла и абсолютного пути к самому временному файлу документа</returns>
  protected override Tuple<string, string> DoInitialize(
    StandaloneViewDataInjectionOperation operation)
  {
    string directoryName = Path.GetDirectoryName(operation.Parameters.FilePath);
    return Tuple.Create<string, string>(directoryName, Path.Combine(directoryName, $"{Path.GetFileNameWithoutExtension(operation.Parameters.FileName)}_{this.MakeRandomFileName()}{Path.GetExtension(operation.Parameters.FileName)}"));
  }

  protected override void DoRemoveFiles()
  {
    base.DoRemoveFiles();
    FileUtils.DeleteFileSilently(this.FilePath);
  }
}
