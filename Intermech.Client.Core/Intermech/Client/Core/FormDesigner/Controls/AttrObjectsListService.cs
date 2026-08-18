
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrObjectsListService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
public class AttrObjectsListService
{
  /// <summary>Набор колонок.</summary>
  public NodeColumnCollection Columns { get; set; }

  /// <summary>Идентификатор выбранного объекта.</summary>
  public long ObjectID { get; set; }

  /// <summary>Идентификатор типа объектов.</summary>
  public int ObjectsTypeID { get; private set; }

  /// <summary>Идентификатор типа связи.</summary>
  public int RelationTypeID { get; private set; }

  /// <summary>Конструктор.</summary>
  /// <param name="objID">Идентификатор выбранного объекта</param>
  /// <param name="objTypeID">Идентификатор типа объектов</param>
  /// <param name="columns">Набор колонок</param>
  public AttrObjectsListService(
    long objID,
    int objTypeID,
    int relTypeID,
    NodeColumnCollection columns)
  {
    this.ObjectID = objID;
    this.ObjectsTypeID = objTypeID;
    this.RelationTypeID = relTypeID;
    this.Columns = columns;
  }
}
