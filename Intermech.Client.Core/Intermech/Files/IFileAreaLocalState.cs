
// Type: Intermech.Files.IFileAreaLocalState
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Files;

/// <summary>
/// Необязательный интерфейс для файловой области для управления ее состоянием, связанным с конкретным компьютером.
/// </summary>
internal interface IFileAreaLocalState
{
  /// <summary>
  /// Записывает на диск часть внутреннего состояния файловой области, которая сохраняется между сеансами работы клиента IPS.
  /// Метод используется для сохранения состояния файловой области перед завершением работы клиента IPS.
  /// </summary>
  void Flush();
}
