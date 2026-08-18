// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.RelationVisualizer.IRelVisSettings
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

#nullable disable
namespace Intermech.Interfaces.Pdm.RelationVisualizer;

/// <summary>Интерфейс настроек Визуализатора связей</summary>
public interface IRelVisSettings
{
  /// <summary>
  /// Загрузить настройки из глобальной конфигурации системы
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <returns>true, если настройки успешно загружены</returns>
  bool LoadSettings(IUserSession session);

  /// <summary>Внести изменения в глобальную конфигурацию системы</summary>
  /// <param name="session">Сессия</param>
  /// <returns>true, если изменения успешно внесены</returns>
  bool SaveSettings(IUserSession session);

  /// <summary>Пользовательски енастройки</summary>
  UserSettings Settings { set; get; }
}
