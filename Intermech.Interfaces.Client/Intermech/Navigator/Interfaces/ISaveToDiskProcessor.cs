// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ISaveToDiskProcessor
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс, сохраняющий дополнительные файлы на диск для указанного объекта.
/// Должен быть потокобезопасным.
/// </summary>
public interface ISaveToDiskProcessor
{
  /// <summary>Сохраняет файлы на диск</summary>
  /// <param name="iSaveToDiskClass">базовые опции обработки, здесь, например, может быть указана папка c:\docs</param>
  /// <param name="folder">конкретизированная папка для сохранения файлов объекта, напр. c:\docs\000.000.001</param>
  /// <param name="objectID">идентификатор версии объекта для дополнительного сохранения</param>
  void Save(ISaveToDiskClass iSaveToDiskClass, string folder, long objectID);
}
