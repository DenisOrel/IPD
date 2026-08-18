
// Type: Intermech.Navigator.ContextNamesCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;


namespace Intermech.Navigator;

/// <summary>
/// Кэш названий контекстов редактирования. При отсутствии в нем имени для указанного идентификатора кэш лезет в базу.
/// </summary>
internal class ContextNamesCache : ICache, IContextNamesCache
{
  /// <summary>
  /// Коллекция пар значений [(Int64)Идентификатор контекста] = [(string)Название контекста]
  /// </summary>
  private Dictionary<long, string> _names = new Dictionary<long, string>();

  /// <summary>Сбросить содержимое кэша</summary>
  public void Reset() => this._names.Clear();

  /// <summary>
  /// Вернуть название контекста редактирования по его идентификатору
  /// </summary>
  /// <param name="contextObjectID">Идентификатор контекста редактирования</param>
  /// <returns>Название контекста редактирования</returns>
  public string GetContextName(long contextObjectID)
  {
    return contextObjectID == 0L ? "" : contextObjectID.ToString();
  }
}
