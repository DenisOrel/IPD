// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.IMRPUserSessionHelper
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Интерфейс, позволяющий получить сессию по её идентификатору.
/// Внимание! Если требуется использование сессии в отдельном потоке, необходимо
/// выполнить её клонирование!
/// </summary>
public interface IMRPUserSessionHelper
{
  /// <summary>Получить сессию по её идентификатору</summary>
  /// <param name="sessionGuid">Уникальный идентификатор сессии</param>
  /// <returns>Ссылка на сессию или null</returns>
  IUserSession GetUserSession(Guid sessionGuid);
}
