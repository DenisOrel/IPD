// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.INormaCSService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Сервис для работы с NormaCS</summary>
public interface INormaCSService
{
  /// <summary>Запускает NormaCS, если еще не запущена.</summary>
  void Start();

  /// <summary>
  /// Отправляет запрос в NormaCS на поиск по номеру нормативного документа
  /// </summary>
  void FindByNumber(string text);

  /// <summary>
  /// Отправляет запрос в NormaCS на поиск по имени нормативного документа
  /// </summary>
  void FindByName(string searchText);

  /// <summary>
  /// Отправляет запрос в NormaCS на поиск по тексту нормативного документа.
  /// </summary>
  void FindByText(string searchText);
}
