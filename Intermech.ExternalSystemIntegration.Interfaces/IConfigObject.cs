// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Interfaces.IConfigObject
// Assembly: Intermech.ExternalSystemIntegration.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: F517EC21-BF51-45B0-BFB7-5DACD58FAED0
// Assembly location: D:\IPS\Client\Intermech.ExternalSystemIntegration.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.ExternalSystemIntegration.Interfaces.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Interfaces;

public interface IConfigObject : IDBObject, IDBAttributable, IDBSessionable, IPluginsData
{
  /// <summary>Имя конфигурации</summary>
  string ConfigName { get; set; }

  /// <summary>Глобальный идентификатор связанного объекта</summary>
  string LinkObjGuid { get; set; }

  /// <summary>Соответствие атрибутов</summary>
  string[] AttributeComprasion { get; set; }

  /// <summary>Ссылка на схему трансофрмации</summary>
  long SchemeTransfLink { get; set; }

  /// <summary>
  /// ID объекта настройки трансформации, которому принадлежит данная конфигурация
  /// </summary>
  long ObjTypeSettingItemObjectID { get; }
}
