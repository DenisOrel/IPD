
// Type: Intermech.Navigator.DBObjects.FavoritesObjectsPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Часть элемента "Избранное" из пространства навигации, отвечающая непосредственно за объекты, содержащиеся в избранном
/// </summary>
public class FavoritesObjectsPart : ObjectsPart
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="services"></param>
  public FavoritesObjectsPart(IServiceProvider services)
    : base(FavoritesObjectsPart.GetObjectConditions(), services)
  {
  }

  /// <summary>Условия вхождения объекта в Избранное</summary>
  /// <returns></returns>
  private static ConditionStructure[] GetObjectConditions()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return new ConditionStructure[1]
      {
        new ConditionStructure(-2, RelationalOperators.InSelection, (object) sessionKeeper.Session.UserID, (object) true, LogicalOperators.AND, 0, false)
      };
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="conditionsProvider"></param>
  /// <param name="services"></param>
  public FavoritesObjectsPart(IConditionsProvider conditionsProvider, IServiceProvider services)
    : base(-1, FavoritesObjectsPart.GetObjectConditions(), conditionsProvider, services)
  {
  }

  /// <summary>
  /// Возвращает коллекцию всех поддерживаемых данным элементом
  /// виртуальных колонок навигатора. Этот метод используется диалогом
  /// настройки отображения грида.
  /// </summary>
  /// <param name="ColumnSetName">Название набора колонок.
  /// Intermech.Navigator.Consts.NavigatorDefaultColumnSetName - набор колонок по умолчанию</param>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  public override NodeColumnCollection GetSupportedColumns(string ColumnSetName)
  {
    NodeColumnCollection columns = new NodeColumnCollection();
    Helper.AddObligatoryColumns(columns, true, true);
    return columns;
  }
}
