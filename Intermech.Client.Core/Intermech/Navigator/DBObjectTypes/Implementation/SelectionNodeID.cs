
// Type: Intermech.Navigator.DBObjectTypes.Implementation.SelectionNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.DBObjectTypes.Implementation;

/// <summary>
/// Реализует унифицированный идентификатор, предназначенный для обозначения
/// элементов "Тип объекта базы данных" из пространства навигации.
/// </summary>
internal sealed class SelectionNodeID : INodeID
{
  /// <summary>
  /// Конструктор, позволяющий создать идентификатор, описывающий тип объекта.
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <param name="handSelection">Ручная выборка</param>
  public SelectionNodeID(int objTypeID, bool handSelection)
  {
    this.TypeID = objTypeID;
    this.HandSelection = handSelection;
  }

  /// <summary>
  /// Возвращает идентификатор категории описываемого элемента
  /// </summary>
  public int CategoryID => 4;

  /// <summary>
  /// Идентификатор типа объекта, описываемого данным унифицированным идентификатором.
  /// </summary>
  public int TypeID { get; }

  public object Cookie { get; set; }

  public bool HandSelection { get; }

  public override bool Equals(object obj)
  {
    return obj is SelectionNodeID selectionNodeId && selectionNodeId.TypeID == this.TypeID && selectionNodeId.HandSelection == this.HandSelection;
  }

  public override int GetHashCode() => this.TypeID ^ this.HandSelection.GetHashCode();
}
