// Decompiled with JetBrains decompiler
// Type: Intermech.Files.IFileArea
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Базовый интерфейс файловой области в файловом хранилище пользователя.
/// </summary>
public interface IFileArea
{
  /// <summary>
  /// Возвращает понятное пользователю название файловой области.
  /// </summary>
  string DisplayName { get; }

  /// <summary>
  /// Возврашает абсолютный путь к каталогу файловой области.
  /// </summary>
  string AreaPath { get; }
}
