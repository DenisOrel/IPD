// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Nodes.TechObjectListNode
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Navigator.Parts;
using System;
using System.Collections;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Nodes;

/// <summary>Custom root node for TechObjectDescriptor</summary>
public class TechObjectListNode : ObjectsListNode
{
  /// <summary>Признак раскрытия состава дочерних элементов</summary>
  protected bool _expandNode;
  /// <summary>
  /// 
  /// </summary>
  protected IDescriptor _descriptor;

  /// <summary>Constructor</summary>
  /// <param name="descriptor"></param>
  /// <param name="objectIDs"></param>
  /// <param name="objectTypeID"></param>
  /// <param name="expandNode"></param>
  public TechObjectListNode(
    IDescriptor descriptor,
    IList objectIDs,
    int objectTypeID,
    bool expandNode)
    : base(objectIDs, objectTypeID)
  {
    this._descriptor = descriptor;
    this._expandNode = expandNode;
  }

  /// <summary>
  /// 
  /// </summary>
  public IList ObjectIDs => this.objectIDs;

  /// <summary>
  /// 
  /// </summary>
  public IDescriptor Descriptor => this._descriptor;

  /// <summary>
  /// Создает и возвращает части, которые отвечают за элементы-папки.
  /// </summary>
  /// <returns></returns>
  protected override ObjectsListPart GetObjectsListPart(
    IList objectVersionIds,
    IServiceProvider serviceProvider,
    int aObjectTypeID)
  {
    return (ObjectsListPart) new TechObjectListPart(objectVersionIds, (IConditionsProvider) null, serviceProvider, aObjectTypeID, this._expandNode);
  }

  /// <summary>
  /// Возвращает интерфейс объекта-запроса, с помощью которого можно
  /// прочитать список дочерних элементов. Если у данного элемента нет
  /// дочерних, то метод вернет null.
  /// </summary>
  /// <param name="content">Набор флагов, описывающих тип читаемых дочерних элементов</param>
  /// <returns>Интерфейс запроса</returns>
  public override INodeQuery GetQuery(ContentType content) => base.GetQuery(content);

  /// <summary>
  /// Возвращает данные дочернего элемента в указанном формате. Если
  /// формат не поддерживается, то результатом будет null.
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента.</param>
  /// <param name="dataFormat">Тип формата данных.</param>
  /// <returns>Результирующий объект указанного типа.</returns>
  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    return base.GetData(nodeID, dataFormat);
  }
}
