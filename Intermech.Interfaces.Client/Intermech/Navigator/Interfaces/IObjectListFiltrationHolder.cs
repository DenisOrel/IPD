// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IObjectListFiltrationHolder
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Сервис позволяет временно сохранять выбранный фильтр списка объектов
/// </summary>
public interface IObjectListFiltrationHolder
{
  /// <summary>
  /// Идентификаторы выборок, по которым будут выполняться фильтрации
  /// в разных закладках "Навигатора"
  /// </summary>
  long SelectionID { get; set; }
}
