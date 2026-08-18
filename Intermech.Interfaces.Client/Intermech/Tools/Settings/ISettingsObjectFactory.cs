// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Settings.ISettingsObjectFactory
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Tools.Settings;

/// <summary>Интерфейс фабрики объектов настроек.</summary>
public interface ISettingsObjectFactory
{
  /// <summary>Создает пустой объект настроек.</summary>
  /// <returns>Пусто объект настроек</returns>
  ISettingsObject CreateSettingsObject();
}
