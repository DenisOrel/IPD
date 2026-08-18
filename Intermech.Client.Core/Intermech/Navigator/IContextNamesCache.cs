
// Type: Intermech.Navigator.IContextNamesCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator;

/// <summary>
/// Интерфейс для извлечения из кэша названий контекстов редактирования по их идентификаторам в базе данных.
/// </summary>
public interface IContextNamesCache
{
  /// <summary>
  /// Вернуть название контекста редактирования по его идентификатору
  /// </summary>
  /// <param name="projectObjectID">Идентификатор контекста редактирования</param>
  /// <returns>Название контекста редактирования</returns>
  string GetContextName(long projectObjectID);
}
