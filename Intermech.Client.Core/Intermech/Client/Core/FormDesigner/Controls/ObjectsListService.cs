
// Type: Intermech.Client.Core.FormDesigner.Controls.ObjectsListService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
public class ObjectsListService
{
  /// <summary>Набор колонок.</summary>
  public NodeColumnCollection Columns { get; set; }

  /// <summary>Идентификатор выбранного объекта.</summary>
  public long ObjectID { get; set; }

  /// <summary>Идентификатор типа объектов.</summary>
  public int ObjectsTypeID { get; private set; }

  /// <summary>
  /// 
  /// </summary>
  public RelatedObjectsRole RelatedRole { get; private set; }

  /// <summary>Идентификатор типа связи.</summary>
  public int RelationTypeID { get; private set; }

  /// <summary>Идентификатор выборки.</summary>
  public long SelectionID { get; private set; }

  /// <summary>Конструктор.</summary>
  /// <param name="selectionID">Идентификатор выборки</param>
  /// <param name="objID">Идентификатор выбранного объекта</param>
  /// <param name="objTypeID">Идентификатор типа объектов</param>
  /// <param name="relTypeID">Идентификатор типа связи</param>
  /// <param name="relObjsRole"></param>
  /// <param name="columns">Набор колонок</param>
  public ObjectsListService(
    long selectionID,
    long objID,
    int objTypeID,
    int relTypeID,
    RelatedObjectsRole relObjsRole,
    NodeColumnCollection columns)
  {
    this.SelectionID = selectionID;
    this.ObjectID = objID;
    this.ObjectsTypeID = objTypeID;
    this.RelationTypeID = relTypeID;
    this.RelatedRole = relObjsRole;
    this.Columns = columns;
  }
}
