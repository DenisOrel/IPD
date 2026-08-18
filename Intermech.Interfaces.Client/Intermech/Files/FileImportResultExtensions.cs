// Decompiled with JetBrains decompiler
// Type: Intermech.Files.FileImportResultExtensions
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Files;

public static class FileImportResultExtensions
{
  public static bool IsSuccessfulResult(this FileImportResult importResult)
  {
    if (importResult == null)
      throw new ArgumentNullException(nameof (importResult));
    return importResult is FileImportResult.Success;
  }

  public static FileImportResult.Success AsSuccessfulResult(this FileImportResult importResult)
  {
    if (importResult == null)
      throw new ArgumentNullException(nameof (importResult));
    return importResult is FileImportResult.Success ? (FileImportResult.Success) importResult : (FileImportResult.Success) null;
  }

  public static Exception AsErrorException(this FileImportResult importResult)
  {
    switch (importResult)
    {
      case null:
        throw new ArgumentNullException(nameof (importResult));
      case FileImportResult.Success _:
        return (Exception) null;
      case FileImportResult.Error _:
        FileImportResult.Error error = (FileImportResult.Error) importResult;
        return (Exception) new FaultException(string.Join(" ", $"Файл '{error.FilePath}' не был импортирован, так как в процессе импорта файла произошла ошибка.", error.Exception.Message), error.Exception);
      case FileImportResult.IgnoredFile _:
        FileImportResult.IgnoredFile ignoredFile = (FileImportResult.IgnoredFile) importResult;
        return (Exception) new FaultException(string.Join(" ", $"Файл '{ignoredFile.FilePath}' не может быть импортирован в IPS.", ignoredFile.Reason));
      case FileImportResult.AlreadyImportedFile _:
        FileImportResult.AlreadyImportedFile alreadyImportedFile = (FileImportResult.AlreadyImportedFile) importResult;
        return (Exception) new FaultException($"Файл '{alreadyImportedFile.FilePath}' не может быть импортирован в IPS. Файл уже принадлежит объекту IPS с ид. версии = {alreadyImportedFile.ObjectId}.");
      default:
        throw new NotSupportedException($"Объекты типа '{importResult.GetType()}' не поддерживаются.");
    }
  }

  public static long UnwrapObjectId(this FileImportResult importResult)
  {
    return (importResult.AsSuccessfulResult() ?? throw importResult.AsErrorException()).ObjectId;
  }
}
