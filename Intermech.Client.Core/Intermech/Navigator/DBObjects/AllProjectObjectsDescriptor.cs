
// Type: Intermech.Navigator.DBObjects.AllProjectObjectsDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Дескриптор для узла, описывающего список всех объектов проекта
/// </summary>
public class AllProjectObjectsDescriptor : HiveDescriptor
{
  /// <summary>Идентификатор версии проекта</summary>
  private long _projectID;

  /// <summary>Создает дескриптор.</summary>
  /// <param name="projectID">Идентификатор версии проекта</param>
  public AllProjectObjectsDescriptor(long projectID)
    : base(Intermech.Navigator.Consts.CategoryAllProjectObjectsNode, 0, AllProjectObjectsNode.AllProjectObjectsNodeName)
  {
    this._projectID = projectID;
  }

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора.
  /// </summary>
  /// <param name="state">Состояние</param>
  protected AllProjectObjectsDescriptor(PersistentState state)
    : this(0L)
  {
  }

  /// <summary>
  /// Отобразить колонку "Навигатора" на идентификатор или название атрибута
  /// </summary>
  /// <param name="column">Колонка "Навигатора"</param>
  /// <returns>Идентификатор или название атрибута, либо null</returns>
  public override object MapColumnToField(NodeColumn column)
  {
    object field = base.MapColumnToField(column);
    if (field != null)
      return field;
    return (column.SchemeGuid == Intermech.Navigator.Consts.ObjectColumnSchemeGuid || column.SchemeGuid == Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid || column.SchemeGuid == Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid) && column.ID.Equals((object) ObligatoryObjectAttributes.CAPTION) ? (object) "F_CAPTION" : (object) null;
  }

  public override INodeID GetRecordNodeID()
  {
    return (INodeID) new AllProjectObjectsNodeID(this._projectID);
  }

  public override INode GetChild(INodeID nodeID)
  {
    return nodeID is AllProjectObjectsNodeID projectObjectsNodeId ? (INode) new AllProjectObjectsNode(projectObjectsNodeId.projectID) : base.GetChild(nodeID);
  }

  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    if (dataFormat == typeof (ICanOpenInNewWindow))
      return (object) null;
    return dataFormat == typeof (IDescriptor) ? (object) new AllProjectObjectsDescriptor(this._projectID) : (object) null;
  }
}
