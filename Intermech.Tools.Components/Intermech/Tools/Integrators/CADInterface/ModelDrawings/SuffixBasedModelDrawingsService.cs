// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ModelDrawings.SuffixBasedModelDrawingsService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.IO;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface.ModelDrawings;

/// <summary>
/// Реализует сервиса интегратора для определения файлов чертежей, а также поиска чертежей, связанных с 3D-моделями по имени файла.
/// Работа сервиса основана на знании специального суффикса в имени файла, имеющегося только у файлов чертежей.
/// Расширение файлов чертежей не существенно.
/// </summary>
/// <remarks>Реализация является thread safe.</remarks>
/// <summary>Создает объект.</summary>
/// <param name="owner">Владелец компонента</param>
/// <param name="drawingExtension">Расширение файлов чертежей, должно начинаться с символа '.' (точка)</param>
/// <param name="modelExtensions">Расширения файлов моделей</param>
/// <exception cref="T:System.ArgumentNullException">Ссылкы на владельца компонента и на расширения файлов моделей не могут быть null</exception>
/// <exception cref="T:System.ArgumentException">Расширение файлов чертежей пусто, либо начинается не с символа '.'</exception>
public class SuffixBasedModelDrawingsService(
  IIntegrator owner,
  string drawingExtension,
  params string[] modelExtensions) : AbstractModelDrawingsService(owner, drawingExtension, modelExtensions)
{
  /// <summary>Определяет по имени файла, является ли он чертежом.</summary>
  /// <param name="fileName">Имя файла, путь может быть относительным или отсутствовать</param>
  /// <returns>true, если это файл чертежа</returns>
  protected override bool DoIsDrawingFileName(string fileName)
  {
    if (PathUtils.IsSamePath(Path.GetExtension(fileName), this.DrawingExtension))
    {
      string withoutExtension = Path.GetFileNameWithoutExtension(fileName);
      foreach (string possibleSuffix in (IEnumerable<string>) this.GetPossibleSuffixes())
      {
        if (withoutExtension.EndsWith(possibleSuffix, StringComparison.CurrentCultureIgnoreCase))
          return true;
      }
    }
    return false;
  }

  /// <summary>
  /// Проверяет, соответствуют ли друг другу указанные имена файлов чертежа и 3D-модели.
  /// </summary>
  /// <param name="modelFileName">Имя файла 3D-модели</param>
  /// <param name="drawingFileName">Имя файла чертежа</param>
  /// <returns>true, если имена файлов соответствуют друг другу</returns>
  /// <exception cref="T:System.ArgumentException">Имена файлов чертежа и 3D-модели не могут быть пустыми</exception>
  protected override bool DoIsSourceModelFile(string modelFileName, string drawingFileName)
  {
    if (!PathUtils.IsSamePath(Path.GetExtension(drawingFileName), this.DrawingExtension) || !PathUtils.IsSamePath(Path.GetDirectoryName(drawingFileName), Path.GetDirectoryName(modelFileName)))
      return false;
    string withoutExtension1 = Path.GetFileNameWithoutExtension(drawingFileName);
    string withoutExtension2 = Path.GetFileNameWithoutExtension(modelFileName);
    ICollection<string> possibleSuffixes = this.GetPossibleSuffixes();
    if (possibleSuffixes.Count != 0)
    {
      foreach (string str in (IEnumerable<string>) possibleSuffixes)
      {
        if (PathUtils.IsSamePath(withoutExtension1, withoutExtension2 + str))
          return true;
      }
    }
    return false;
  }

  /// <summary>Находит все файлы чертежей по имени файла 3D-модели.</summary>
  /// <param name="modelFileName">Имя файла 3D-модели, может содержать абсолютный или относительный путь</param>
  /// <param name="fileExists">Функция для тестирования существования файла с указанным именем файла и путем</param>
  /// <returns>Коллекция имен файлов найденных чертежей</returns>
  protected override IEnumerable<string> DoEnumerateDrawingFiles(
    string modelFileName,
    Func<string, bool> fileExists)
  {
    ICollection<string> possibleSuffixes = this.GetPossibleSuffixes();
    if (possibleSuffixes.Count != 0)
    {
      string modelDirectory = Path.GetDirectoryName(modelFileName);
      string modelNameOnly = Path.GetFileNameWithoutExtension(modelFileName);
      foreach (string str1 in (IEnumerable<string>) possibleSuffixes)
      {
        string str2 = Path.Combine(modelDirectory, modelNameOnly + str1 + this.DrawingExtension);
        if (fileExists(str2))
          yield return str2;
      }
      modelDirectory = (string) null;
      modelNameOnly = (string) null;
    }
  }
}
