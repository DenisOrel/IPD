// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.AllDocumsPart
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Archives;

/// <summary>
/// Часть элемента "Архивы" из пространства навигации, отвечающая за документы, содержащиеся в любом из существующих архивов
/// </summary>
internal class AllDocumsPart : ObjectsPart
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="services"></param>
  public AllDocumsPart(IServiceProvider services)
    : base(ConstsHolder.DocTypeID, AllDocumsPart.GetObjectTypeConditions(), services)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="conditionsProvider"></param>
  /// <param name="services"></param>
  public AllDocumsPart(IConditionsProvider conditionsProvider, IServiceProvider services)
    : base(ConstsHolder.DocTypeID, AllDocumsPart.GetObjectTypeConditions(), conditionsProvider, services)
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
    Intermech.Navigator.DBObjects.Helper.AddObligatoryColumns(columns, true, true);
    Intermech.Navigator.DBObjects.Helper.AddObligatoryColumnsAdv(columns);
    Intermech.Navigator.DBObjects.Helper.AddObjectTypeColumns(columns, this.objTypeID);
    Intermech.Navigator.DBObjects.Helper.AddAllColumns(columns);
    return columns;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private static ConditionStructure[] GetObjectTypeConditions()
  {
    return new ConditionStructure[1]
    {
      new ConditionStructure(ConstsHolder.ArchiveAttrID, RelationalOperators.NotEmpty, (object) null, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID)
    };
  }
}
