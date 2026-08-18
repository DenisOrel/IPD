
// Type: Intermech.Navigator.Selections.Implementation.SelectionNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System.Diagnostics;


namespace Intermech.Navigator.Selections.Implementation;

/// <summary>
/// Реализует унифицированный идентификатор, предназначенный для обозначения
/// элементов "Объект базы данных" из пространства навигации.
/// </summary>
public class SelectionNodeID : NodeID
{
  /// <summary>
  /// Создать описание узла на основании указанных параметров
  /// </summary>
  /// <param name="e">Параметры для создания описания узла</param>
  public SelectionNodeID(CreateObjectNodeParams e)
    : base(e)
  {
    this.pars = (CreateObjectNodeParams) new CreateSelectionNodeParams((object) e);
  }

  /// <summary>Является ли выборка ручной</summary>
  public bool HandSelection
  {
    [DebuggerStepThrough] get => (this.pars as CreateSelectionNodeParams).HandSelection;
  }

  /// <summary>Принадлежность выборки</summary>
  public SelectionType SelectionType
  {
    [DebuggerStepThrough] get => (this.pars as CreateSelectionNodeParams).SelectionType;
  }

  /// <summary>Идентификатор типа объекта, с которым связана выборка</summary>
  public int BindedObjectTypeID
  {
    [DebuggerStepThrough] get => (this.pars as CreateSelectionNodeParams).BindedObjectTypeID;
  }

  /// <summary>Назначение выборки</summary>
  public int SampleFunction
  {
    [DebuggerStepThrough] get => (this.pars as CreateSelectionNodeParams).SampleFunction;
  }

  /// <summary>Искать среди объектов локальных и глобальных типов</summary>
  public bool SearchInLocalTypes
  {
    [DebuggerStepThrough] get => (this.pars as CreateSelectionNodeParams).SearchInLocalTypes;
  }

  public override bool Equals(object obj)
  {
    return !(obj is SelectionNodeID selectionNodeId) ? base.Equals(obj) : selectionNodeId.ObjectID == this.ObjectID;
  }

  public override int GetHashCode() => this.ObjectID.GetHashCode();
}
