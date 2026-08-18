// Decompiled with JetBrains decompiler
// Type: Intermech.Files.FileDifferencePair
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.IO;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Описывает результат сравнения локального и удаленного (remote) состояний файла.
/// </summary>
[Serializable]
public sealed class FileDifferencePair : ICloneable
{
  private readonly FileDifferenceType differenceType;
  private readonly FileState localState;
  private readonly FileState remoteState;

  /// <summary>Создает объект.</summary>
  /// <param name="differenceType">Тип различий между состояниями файла</param>
  /// <param name="localState">Локальное состояние файла</param>
  /// <param name="remoteState">Удаленное (remote) состояние файла</param>
  /// <exception cref="T:ArgumentException">Значение localState или remoteState не соответствует типу различий между состояниями файла; имена файлов в localState и remoteState не совпадают</exception>
  public FileDifferencePair(
    FileDifferenceType differenceType,
    FileState localState,
    FileState remoteState)
  {
    FileDifferencePair.CheckDifferenceTypeMatchesFileStates(differenceType, localState, remoteState);
    if (localState != null && remoteState != null && !PathUtils.IsSamePath(localState.FileName, remoteState.FileName))
      throw new ArgumentException(string.Format($"Аргументы localState и remoteState должны описывать состояния одного и того же файла. Имена файлов '{localState.FileName}' и '{remoteState.FileName}' в указанных состояниях файла не совпадают, но они должны совпадать."));
    this.differenceType = differenceType;
    this.localState = localState;
    this.remoteState = remoteState;
  }

  /// <summary>Клонирует текущий объект.</summary>
  /// <returns>Клон текущего объекта</returns>
  public FileDifferencePair Clone()
  {
    return new FileDifferencePair(this.differenceType, this.localState, this.remoteState);
  }

  /// <summary>Клонирует текущий объект.</summary>
  /// <returns>Клон текущего объекта</returns>
  object ICloneable.Clone() => (object) this.Clone();

  /// <summary>
  /// Возвращает тип различия между локальным и удаленным (remote) состояниями файла.
  /// </summary>
  public FileDifferenceType DifferenceType
  {
    [DebuggerStepThrough] get => this.differenceType;
  }

  /// <summary>
  /// Возвращает локальное состояние файла.
  /// Значение свойства может быть равно null, если локальное состояние файла отсутствует.
  /// </summary>
  public FileState LocalState
  {
    [DebuggerStepThrough] get => this.localState;
  }

  /// <summary>
  /// Возвращает удаленное (remote) состояние файла.
  /// Значение свойства может быть равно null, если удаленное состояние файла отсутствует.
  /// </summary>
  public FileState RemoteState
  {
    [DebuggerStepThrough] get => this.remoteState;
  }

  private static void CheckDifferenceTypeMatchesFileStates(
    FileDifferenceType differenceType,
    FileState localState,
    FileState remoteState)
  {
    switch (differenceType)
    {
      case FileDifferenceType.MissingFile:
        if (localState != null)
          throw new ArgumentException("Локальное состояние файла должно быть null.", nameof (localState));
        if (remoteState != null)
          break;
        throw new ArgumentNullException(nameof (remoteState));
      case FileDifferenceType.OutdatedFile:
      case FileDifferenceType.UnchangedFile:
      case FileDifferenceType.UpdatedFile:
        if (localState == null)
          throw new ArgumentNullException(nameof (localState));
        if (remoteState != null)
          break;
        throw new ArgumentNullException(nameof (remoteState));
      case FileDifferenceType.NewFile:
        if (localState == null)
          throw new ArgumentNullException(nameof (localState));
        if (remoteState == null)
          break;
        throw new ArgumentException("Удаленное (remote) состояние файла должно быть null.", nameof (remoteState));
      default:
        throw new NotSupportedEnumException((Enum) differenceType);
    }
  }
}
