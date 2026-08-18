// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.Ceh_Route.ICehRouteStringService
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using System;

#nullable disable
namespace Intermech.Interfaces.TechCard.Ceh_Route;

/// <summary>Интерфейс службы</summary>
public interface ICehRouteStringService
{
  /// <summary>Вызов формирования строки расцеховки</summary>
  /// <param name="objectId">Ид. объекта расцеховки или унаследованного от него</param>
  /// <param name="sessionGuid">Сессия</param>
  bool CreateCehRouteString(long objectId, Guid sessionGuid, bool throwException = false);

  /// <summary>Сохранение настроек</summary>
  /// <param name="sessionGuid"></param>
  /// <param name="settings"></param>
  bool SaveSettings(Guid sessionGuid, ICehRouteStringItem settings);

  /// <summary>Загрузка настроек</summary>
  /// <param name="sessionGuid"></param>
  /// <param name="param"></param>
  /// <returns></returns>
  bool LoadSettings(Guid sessionGuid, out ICehRouteStringItem param);
}
