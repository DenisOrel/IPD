// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Interfaces.IRequestConfigObject
// Assembly: Intermech.ExternalSystemIntegration.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: F517EC21-BF51-45B0-BFB7-5DACD58FAED0
// Assembly location: D:\IPS\Client\Intermech.ExternalSystemIntegration.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.ExternalSystemIntegration.Interfaces.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Interfaces;

public interface IRequestConfigObject : 
  IConfigObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  /// <summary>Отображать карточку после создания</summary>
  bool ShowCard { get; set; }

  /// <summary>Имя файла-запроса</summary>
  string FileName { get; set; }
}
