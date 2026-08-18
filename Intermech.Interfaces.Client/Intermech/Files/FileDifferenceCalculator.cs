// Decompiled with JetBrains decompiler
// Type: Intermech.Files.FileDifferenceCalculator
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.IO;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Реализует объект для сравнения состояний как отдельных файлов, так и групп файлов.
/// </summary>
public class FileDifferenceCalculator
{
  /// <summary>
  /// Сравнивает два состояния одного и того же файла объекта.
  /// </summary>
  /// <param name="localState">Локальное состояние файла</param>
  /// <param name="remoteState">Удаленное (remote) состояние файла</param>
  /// <returns>Результат сравнения состояний файлов</returns>
  /// <exception cref="T:ArgumentException">Аргументы localState и remoteState не должны одновременно быть равны null; аргументы localState и remoteState должны описывать один и тот же файл.</exception>
  public FileDifferencePair Calculate(FileState localState, FileState remoteState)
  {
    int num1 = 0;
    if (localState != null)
      num1 += 2;
    if (remoteState != null)
      ++num1;
    switch (num1)
    {
      case 1:
        return new FileDifferencePair(FileDifferenceType.MissingFile, (FileState) null, remoteState);
      case 2:
        return new FileDifferencePair(FileDifferenceType.NewFile, localState, (FileState) null);
      case 3:
        int num2 = localState.CompareTo(remoteState);
        return new FileDifferencePair(num2 == 0 ? FileDifferenceType.UnchangedFile : (num2 < 0 ? FileDifferenceType.OutdatedFile : FileDifferenceType.UpdatedFile), localState, remoteState);
      default:
        throw new ArgumentException("Аргументы localState и remoteState не должны одновременно быть равны null.");
    }
  }

  /// <summary>
  /// Сравнивает локальные и удаленные (remote) состояния для группы файлов.
  /// </summary>
  /// <param name="localStates">Локальные состояния файлов</param>
  /// <param name="remoteStates">Удаленные (remote) состояния файлов</param>
  /// <returns>Список с результатами сравнения</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список состояний не может быть null</exception>
  public List<FileDifferencePair> Calculate(
    List<FileState> localStates,
    List<FileState> remoteStates)
  {
    if (localStates == null)
      throw new ArgumentNullException(nameof (localStates));
    if (remoteStates == null)
      throw new ArgumentNullException(nameof (remoteStates));
    List<FileDifferencePair> fileDifferencePairList = new List<FileDifferencePair>(localStates.Count + remoteStates.Count);
    foreach (FileState localState1 in localStates)
    {
      FileState localState = localState1;
      FileState remoteState = remoteStates.Find((Predicate<FileState>) (file => PathUtils.IsSamePath(file.FileName, localState.FileName)));
      fileDifferencePairList.Add(this.Calculate(localState, remoteState));
    }
    foreach (FileState remoteState1 in remoteStates)
    {
      FileState remoteState = remoteState1;
      if (localStates.Find((Predicate<FileState>) (file => PathUtils.IsSamePath(file.FileName, remoteState.FileName))) == null)
        fileDifferencePairList.Add(new FileDifferencePair(FileDifferenceType.MissingFile, (FileState) null, remoteState));
    }
    return fileDifferencePairList;
  }
}
