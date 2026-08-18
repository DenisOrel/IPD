// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Interfaces.IObjTypeSettingItemObject
// Assembly: Intermech.ExternalSystemIntegration.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: F517EC21-BF51-45B0-BFB7-5DACD58FAED0
// Assembly location: D:\IPS\Client\Intermech.ExternalSystemIntegration.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.ExternalSystemIntegration.Interfaces.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Interfaces;

/// <summary>Настройка трансформации для типа объекта</summary>
public interface IObjTypeSettingItemObject : IDBObject, IDBAttributable, IDBSessionable, IPluginsData
{
  /// <summary>
  /// Идентификатор типа объекта, для которого хранятся настройки
  /// </summary>
  string ObjTypeGUID { get; set; }

  /// <summary>Глобальный идентификатор связанного объекта</summary>
  string LinkObjGuid { get; }

  /// <summary>Все элементы трансформации для данной настройки</summary>
  long[] Configs { get; }

  /// <summary>Входящие элементы трансформации</summary>
  long[] ResponceConfigs { get; }

  /// <summary>Исходящие элементы трансформации</summary>
  long[] RequestConfigs { get; }
}
