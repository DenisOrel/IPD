
// Type: Intermech.Navigator.DBObjects.MultipleObjectsNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Diagnostics;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System.Collections.Generic;
using System.Drawing;


namespace Intermech.Navigator.DBObjects;

/// <summary>Нода для списка нескольких объектов. Наследовать от CustomNode.Node пришлось дабы нода поддерживала сервисы фильтрации, иначе
/// с тулбаров пропадают соотв. команды</summary>
public class MultipleObjectsNode : 
  Intermech.Navigator.CustomNode.Node,
  IObjectTypeAndRelationFiltrationSupported,
  INodeCustomUI,
  INode,
  INodeItems
{
  [CanBeNull]
  private readonly Image _mainIcon;
  [CanBeNull]
  private readonly Image _prefixIcon;

  /// <summary>Constructor</summary>
  public MultipleObjectsNode([NotNull] DescriptorCollection descriptors, [CanBeNull] Image mainIcon = null, [CanBeNull] Image prefixIcon = null)
    : base(descriptors)
  {
    this.options |= NodeOptions.CanContainsComposition;
    this._mainIcon = mainIcon;
    this._prefixIcon = prefixIcon;
  }

  /// <summary>Создать список слотов-папок</summary>
  /// <returns>Список слотов-папок</returns>
  protected override List<PartSlot> CreateFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new MultipleObjectsNodePart(this._descriptors, ContentType.Folders));
  }

  /// <summary>Создать список слотов-не-папок</summary>
  /// <returns>Список слотов-не-папок</returns>
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new MultipleObjectsNodePart(this._descriptors, ContentType.NonFolders));
  }

  /// <summary>Получение основной иконки, которая отобразится перед "стандартной иконкой" категории-типа у данной ноды. Может быть
  /// использована для визуального отображения ключевого статуса ноды в данном конкретном котексте (например для сравнения
  /// состава - результат сравнения, удалено, новое, изменено, или без изменений).</summary>
  /// <returns>Иконка, либо null, если должен использоваться стандартный механизм получения иконки по категории/типу.</returns>
  [CanBeNull]
  public Image GetMainIcon() => this._mainIcon;

  /// <summary>Получение иконки, которая отобразится перед "стандартной иконкой" категории-типа у данной ноды. Может быть использована для
  /// визуального отображения ключевого статуса ноды в данном конкретном котексте (например для сравнения состава - результат
  /// сравнения, удалено, новое, изменено, или без изменений).</summary>
  /// <returns>Иконка, либо null, если отображение не требуется.
  /// 
  /// В том случае если у некоторых нод в данном контексте статус отображается, а у других нет, для тех нод, у которых
  /// иконок не будет можно вернут стандартную иконку "пунктирная горизонтальная линия 16x16",
  /// Intermech.Navigator.NavigatorImages.HorizontalDottedTreeLine для того, чтобы у всех нод (у тех, у которых
  /// префиксная иконка есть и тех, у которой нет) в данном контексте был одинаковый отступ.</returns>
  [CanBeNull]
  public Image GetPrefixIcon() => this._prefixIcon;

  /// <summary>Получить специальный виджет для колонки.</summary>
  /// <param name="rowWidget">Виджет строки дерева</param>
  /// <param name="column">Колонка</param>
  /// <returns>Созданный виджет, либо null, если для этой колонки всё должно быть по-умолчанию</returns>
  [CanBeNull]
  public CellWidget GetCustomCellWidget(RowWidget rowWidget, NavigatorTreeColumn column)
  {
    return (CellWidget) null;
  }
}
