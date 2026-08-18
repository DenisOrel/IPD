// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IPackageExtension
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>
/// Интерфейс для дополнительный возможностей модуля расширения.
/// </summary>
public interface IPackageExtension
{
  /// <summary>
  /// Дополнительная инициализация элемента модуля расширения.
  /// </summary>
  /// <returns>Возвращает true, если дополнительная инициализация выполнена успешно.</returns>
  bool PostInit();
}
