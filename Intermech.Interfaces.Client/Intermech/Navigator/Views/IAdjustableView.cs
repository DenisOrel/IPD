// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.IAdjustableView
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Views;

/// <summary>Описание настраиваемой закладки (вьюшки) "Навигатора"</summary>
public interface IAdjustableView
{
  /// <summary>Уникальное в пределах всей системы имя закладки</summary>
  string Name { get; set; }

  /// <summary>Краткое текстовое название заладки</summary>
  string Caption { get; set; }

  /// <summary>Более подробное текстовое описание закладки</summary>
  string Hint { get; set; }

  /// <summary>Название модуля (плагина), который создаёт закладку</summary>
  string Module { get; set; }

  /// <summary>
  /// Название значка закладки (из коллекции именованных значков)
  /// </summary>
  string ImageName { get; set; }

  /// <summary>
  /// Флажок позволяет прятать или показывать данную закладку на панелях "Навигатора"
  /// </summary>
  bool Visible { get; set; }

  /// <summary>
  /// Порядковый номер закладки на менеджере закладок "Навигатора"
  /// </summary>
  int OrderID { get; set; }
}
