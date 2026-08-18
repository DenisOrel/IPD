
// Type: Intermech.Scripting.Projects.DBScripts.DBScriptRepositoryKey
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Scripting.Projects.DBScripts;

/// <summary>
/// Класс ключей репозитория для сценариев, хранящихся в базе данных IPS.
/// Реализация является immutable и thread safe.
/// </summary>
public sealed class DBScriptRepositoryKey : IEquatable<DBScriptRepositoryKey>
{
  /// <summary>Создает объект.</summary>
  /// <param name="objectId">Идентификатор версии объекта сценария в базе данных IPS</param>
  public DBScriptRepositoryKey(long objectId) => this.ObjectId = objectId;

  /// <summary>
  /// Возвращает идентификатор версии объекта сценария в базе данных IPS.
  /// </summary>
  public long ObjectId { get; private set; }

  public bool Equals(DBScriptRepositoryKey other)
  {
    return other != null && other.ObjectId == this.ObjectId;
  }

  public override bool Equals(object obj)
  {
    return !(obj is DBScriptRepositoryKey other) ? base.Equals(obj) : this.Equals(other);
  }

  public override int GetHashCode() => this.ObjectId.GetHashCode();

  /// <summary>
  /// Возвращает типизированный идентификатор сценария по его общей форме.
  /// </summary>
  /// <param name="key">Идентификатор сценария</param>
  /// <returns>Типизированный идентификатор сценария</returns>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="key" /> не должен быть равен null</exception>
  internal static DBScriptRepositoryKey CastFrom(object key)
  {
    return key != null ? (DBScriptRepositoryKey) key : throw new ArgumentNullException(nameof (key));
  }
}
