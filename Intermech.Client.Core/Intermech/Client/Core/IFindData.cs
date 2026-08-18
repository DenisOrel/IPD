
// Type: Intermech.Client.Core.IFindData
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core;

/// <summary>
/// Позволяет получить доступ к параметрам поиск. Доступ к объектам этого типа осуществляется через свойство IFindController.InterfaceObject
/// </summary>
public interface IFindData
{
  /// <summary>Возвращает искомый пользователем текст.</summary>
  string FindWhat { get; set; }
}
