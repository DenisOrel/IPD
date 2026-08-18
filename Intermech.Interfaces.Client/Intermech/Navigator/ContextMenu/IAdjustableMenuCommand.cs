// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.ContextMenu.IAdjustableMenuCommand
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.ContextMenu;

/// <summary>Описание настраиваемой команды меню</summary>
public interface IAdjustableMenuCommand
{
  /// <summary>Родительская коллекция</summary>
  AdjustableMenuCommands Parent { get; }

  /// <summary>
  /// Уникальная в пределах всей системы команда контекстного меню
  /// </summary>
  string Command { get; }

  /// <summary>Краткое текстовое описание команды</summary>
  string Caption { get; }

  /// <summary>Более подробное текстовое описание команды</summary>
  string Hint { get; }

  /// <summary>
  /// Индекс значка команды (из коллекции именованных значков)
  /// </summary>
  int ImageIndex { get; }

  /// <summary>
  /// Флажок позволяет прятать или показывать данную команду в контекстных меню "Навигатора"
  /// </summary>
  bool Visible { get; set; }

  /// <summary>Номер группы команды</summary>
  int Group { get; }

  /// <summary>Порядковый номер внутри группы</summary>
  int OrderBy { get; }

  /// <summary>Коллекция дочерних команд</summary>
  AdjustableMenuCommands Items { get; }
}
