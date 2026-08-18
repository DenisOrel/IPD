// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.TechObjectType.ITechObjectTypeService
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Interfaces.TechCard.TechObjectType;

/// <summary>
/// Сервис "дополнительных" настроек технологических документов
/// </summary>
public interface ITechObjectTypeService
{
  /// <summary>Вернуть настройки типа объекта.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор пользовательской сессии.</param>
  /// <param name="objectType">тип объекта.</param>
  /// <returns>The settings.</returns>
  [NotNull]
  TechObjectTypeSettings GetSettings(Guid sessionGuid, int objectType);

  /// <summary>Установить настройки типа объекта.</summary>
  /// <param name="sessionGuid">Глобальный идентификатор пользовательской сессии.</param>
  /// <param name="objectType">тип объекта.</param>
  /// <param name="settings">настройки.</param>
  void SetSettings(Guid sessionGuid, int objectType, [NotNull] TechObjectTypeSettings settings);
}
