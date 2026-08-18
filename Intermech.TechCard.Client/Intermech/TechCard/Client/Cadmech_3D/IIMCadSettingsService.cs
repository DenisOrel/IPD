// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Cadmech_3D.IIMCadSettingsService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

#nullable disable
namespace Intermech.TechCard.Client.Cadmech_3D;

/// <summary>Сервис настроек интеграции с CAD-cистемой</summary>
internal interface IIMCadSettingsService
{
  /// <summary>Сохранение настроек</summary>
  /// <param name="settings"></param>
  /// <returns>Reserved</returns>
  int SaveSettings(IIMCadSettings settings);

  /// <summary>Загрузка настроек</summary>
  /// <param name="settings"></param>
  /// <returns>Reserved</returns>
  int LoadSettings(out IIMCadSettings settings);
}
