// Decompiled with JetBrains decompiler
// Type: Intermech.Files.FileState
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Files;

/// <summary>Описывает состояние файла.</summary>
[Serializable]
public sealed class FileState : IComparable<FileState>, IComparable<DateTime>
{
  private readonly string fileName;
  private readonly DateTime lastWriteTimeUtc;
  private readonly long length;

  /// <summary>Создает объект.</summary>
  /// <param name="fileName">Имя файла</param>
  /// <param name="lastWriteTimeUtc">Дата последней модификации файла в UTC</param>
  /// <param name="length">Длина файла в байтах</param>
  public FileState(string fileName, DateTime lastWriteTimeUtc, long length)
  {
    if (string.IsNullOrEmpty(fileName))
      throw new ArgumentException("Имя файла не задано.", nameof (fileName));
    if (length < 0L)
      throw new ArgumentOutOfRangeException(nameof (length), "Длина файла должна быть неотрицательным числом.");
    this.fileName = fileName;
    this.lastWriteTimeUtc = lastWriteTimeUtc;
    this.length = length;
  }

  /// <summary>Возвращает состояние указанного файла.</summary>
  /// <param name="path">Путь к файлу</param>
  /// <returns>Состояние файла</returns>
  /// <exception cref="T:ArgumentException">path - аргумент не задан или пуст</exception>
  /// <exception cref="T:System.IO.FileNotFoundException">Указанный файл не найден на диске</exception>
  public static FileState FromFile(string path) => FileState.FromFile(path, path);

  /// <summary>
  /// Возвращает состояние указанного файла. Метод используется для получения состояний файлов, его имя файла указано в относительной форме.
  /// </summary>
  /// <param name="path">Путь к файлу</param>
  /// <param name="resultFileName">Имя файла, которое должно быть указано в объекте состояния</param>
  /// <returns>Состояние файла</returns>
  /// <exception cref="T:ArgumentException">path, resultFileName - аргумент не задан или пуст</exception>
  /// <exception cref="T:System.IO.FileNotFoundException">Указанный файл не найден на диске</exception>
  public static FileState FromFile(string path, string resultFileName)
  {
    if (string.IsNullOrEmpty(path))
      throw new ArgumentException("Путь к файлу не задан или пуст.", nameof (path));
    if (string.IsNullOrEmpty(resultFileName))
      throw new ArgumentException("Имя файла не задано или пусто.", nameof (resultFileName));
    FileInfo fileInfo = new FileInfo(path);
    return new FileState(resultFileName, fileInfo.LastWriteTimeUtc, fileInfo.Length);
  }

  /// <summary>Сравнивает текущее состояние файла с указанным.</summary>
  /// <param name="other">Другое состояние файла для сравнения</param>
  /// <returns>Результат сравнения</returns>
  /// <exception cref="T:System.ArgumentNullException">Другое состояние файла для сравнения не может быть null</exception>
  public int CompareTo(FileState other)
  {
    return other != null ? this.CompareTo(other.LastWriteTimeUtc) : throw new ArgumentNullException(nameof (other));
  }

  /// <summary>Сравнивает текущее состояние файла с указанным.</summary>
  /// <param name="otherTime">Другое состояние файла для сравнения</param>
  /// <returns>Результат сравнения</returns>
  public int CompareTo(DateTime otherTime)
  {
    return this.LastWriteTimeUtc.TruncateToSecond().CompareTo(otherTime.TruncateToSecond());
  }

  /// <summary>Возвращает имя файла.</summary>
  public string FileName
  {
    [DebuggerStepThrough] get => this.fileName;
  }

  /// <summary>Возвращает дату последней модификации файла в UTC.</summary>
  public DateTime LastWriteTimeUtc
  {
    [DebuggerStepThrough] get => this.lastWriteTimeUtc;
  }

  /// <summary>Возвращает длину файла в байтах.</summary>
  public long Length
  {
    [DebuggerStepThrough] get => this.length;
  }
}
