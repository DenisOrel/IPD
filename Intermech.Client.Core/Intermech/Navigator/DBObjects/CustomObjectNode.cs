
// Type: Intermech.Navigator.DBObjects.CustomObjectNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Diagnostics;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System.Drawing;


namespace Intermech.Navigator.DBObjects;

/// <summary>Нода, представляющая объект IPS с кастомным заголовком и иконками</summary>
public class CustomObjectNode : 
  ObjectNode,
  INodeCustomUI,
  IContextAware,
  INodeNotifications,
  INodeIDCreator,
  IObjectTypeAndRelationFiltrationSupported,
  INode,
  INodeItems
{
  /// <summary>Специальная основная предварительная иконка ноды</summary>
  [CanBeNull]
  protected Image _PrefixIcon;
  /// <summary>Специальная основная иконка ноды</summary>
  [CanBeNull]
  protected Image _MainIcon;

  /// <summary>Создать узел</summary>
  /// <param name="objTypeID">Тип</param>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="prefixIcon">Дополнительная иконка ноды. Если null, то отсутствует</param>
  /// <param name="mainIcon">Основная иконка ноды. Если null, то будет использована стандартная иконка объекта</param>
  public CustomObjectNode(int objTypeID, long objID, [CanBeNull] Image prefixIcon = null, [CanBeNull] Image mainIcon = null)
    : base(objTypeID, objID)
  {
    this._PrefixIcon = prefixIcon;
    this._MainIcon = mainIcon;
  }

  /// <summary>Получение основной иконки, которая отобразится перед "стандартной иконкой" категории-типа у данной ноды. Может быть
  /// использована для визуального отображения ключевого статуса ноды в данном конкретном котексте (например для сравнения
  /// состава - результат сравнения, удалено, новое, изменено, или без изменений).</summary>
  /// <returns>Иконка, либо null, если должен использоваться стандартный механизм получения иконки по категории/типу.</returns>
  [CanBeNull]
  public Image GetMainIcon() => this._MainIcon;

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
  public Image GetPrefixIcon() => this._PrefixIcon;

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
