
// Type: Intermech.Navigator.DBObjects.TopObjectsPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Реализует часть элемента навигации, работающую со списком объектов
/// некоторого типа, находящихся на верхнем уровне дерева объектов, т.е. с
/// объектами, не входящими в состав других объектов этого типа или
/// производных от него.
/// </summary>
/// <remarks>
/// Части этого типа применяются при создании элементов навигации, являющихся
/// корнями иерархий однотипных объектов (например, групп пользователей,
/// архивов, катагов IMBASE и др).
/// </remarks>
public class TopObjectsPart : ObjectsPartBase
{
  /// <summary>
  /// Идентификатор типа объектов, с которыми работает эта часть.
  /// </summary>
  protected int _objTypeID;

  /// <summary>
  /// Конструктор, позволяющий создать часть, которая будет работать со
  /// всеми объектами указанного типа, находящимися на верхнем уровне
  /// иерархии.
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа объектов.</param>
  /// <param name="services">Контейнер сервисов</param>
  public TopObjectsPart(int objTypeID, IServiceProvider services)
    : base(services)
  {
    this._objTypeID = objTypeID;
  }

  /// <summary>
  /// Конструктор, позволяющий создать часть, которая будет работать только
  /// с теми объектами указанного типа, которые находятся на верхнем уровне
  /// иерархии и удовлетворяют указанному условию.
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа объектов.</param>
  /// <param name="condition">Условие, которому должны удовлетворять объекты.</param>
  /// <param name="services">Контейнер сервисов</param>
  public TopObjectsPart(int objTypeID, ConditionStructure condition, IServiceProvider services)
    : base(condition, services)
  {
    this._objTypeID = objTypeID;
  }

  /// <summary>
  /// Конструктор, позволяющий создать часть, которая будет работать только
  /// с теми объектами указанного типа, которые находятся на верхнем уровне
  /// иерархии и удовлетворяют всем указанным условиям.
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа объектов.</param>
  /// <param name="conditions">Массив условий, которым должны удовлетворять объекты.</param>
  /// <param name="services">Контейнер сервисов</param>
  public TopObjectsPart(int objTypeID, ConditionStructure[] conditions, IServiceProvider services)
    : base(conditions, services)
  {
    this._objTypeID = objTypeID;
  }

  /// <summary>
  /// Создает и возвращает объект-запрос, в результате выполнения которого
  /// будет получен список объектов, с которыми работает эта часть.
  /// </summary>
  /// <param name="conditions">Массив условий, которых должны удовлетворять объекты.</param>
  /// <returns>Ссылка на объект-запрос.</returns>
  protected override INodeQuery GetQuery(ConditionStructure[] conditions)
  {
    return (INodeQuery) new TopObjectsQuery((INodeQuerySupport) this, this._objTypeID, conditions);
  }
}
