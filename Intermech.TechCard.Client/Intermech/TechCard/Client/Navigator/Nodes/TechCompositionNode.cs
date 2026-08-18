// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Nodes.TechCompositionNode
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.Navigator.Params;
using Intermech.TechCard.Client.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Nodes;

/// <summary>
/// Узел для получения состава указанного объекта по определённому типу связи
/// </summary>
public class TechCompositionNode : AdvRelationsNode
{
  /// <summary>Дескриптор</summary>
  protected IDescriptor _descriptor;

  /// <summary>Constructor</summary>
  /// <param name="descriptor">"Родительский" дескриптор</param>
  /// <param name="e">Параметры для создания описания узла</param>
  public TechCompositionNode(IDescriptor descriptor, CreateObjectNodeParams e)
    : base(e)
  {
    this._descriptor = descriptor;
    this._pars = (AdvCreateObjectNodeParams) new CreateTechNodeParams((object) e);
  }

  /// <summary>
  /// 
  /// </summary>
  public IDescriptor Descriptor => this._descriptor;

  /// <summary>
  /// 
  /// </summary>
  public List<NodeColumnID> Attributes
  {
    [DebuggerStepThrough] get
    {
      return !(this._pars is CreateTechNodeParams pars) ? (List<NodeColumnID>) null : pars.Attributes;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public override void Refresh() => this.folderSlots = (List<PartSlot>) null;

  /// <summary>Get node's compositions</summary>
  /// <returns></returns>
  protected override List<PartSlot> CreateFolderSlots()
  {
    if (!(this.Descriptor is TechCompositionBaseDescriptor descriptor))
      return (List<PartSlot>) null;
    long objId = this.ObjID;
    int objType = this.ObjType;
    if (objId != -1L && objId != 0L)
    {
      List<int> intList = new List<int>();
      if (descriptor.CompRelTypeIDs != null)
        intList.AddRange(descriptor.CompRelTypeIDs);
      if (intList.Count == 0)
      {
        ICurrentUserAndRole service = ServiceUtils.GetService<ICurrentUserAndRole>((object) ApplicationServices.Container, false);
        if (service != null)
        {
          List<Guid> visibleRelationsGuids = service.Rule.GetObjectTypeVisibleRelationsGuids(objType, true);
          intList.AddRange(visibleRelationsGuids.Select<Guid, int>(new Func<Guid, int>(MetaDataHelper.GetRelationTypeID)));
        }
      }
      if (intList.Count != 0)
      {
        List<PartSlot> folderSlots = new List<PartSlot>();
        foreach (int num in intList)
        {
          INodePart part = (INodePart) new TechCompositionPart(objType, objId, num, this.Attributes, descriptor.ObjectsRole, this.FiltrationOwnerID, this.Contexts, this.Services);
          Guid relationTypeGuid = MetaDataHelper.GetRelationTypeGuid(num);
          folderSlots.Add(new PartSlot(relationTypeGuid, part));
        }
        return folderSlots;
      }
    }
    return (List<PartSlot>) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override List<PartSlot> CreateNonFolderSlots() => this.CreateFolderSlots();

  /// <summary>
  /// Возвращает данные дочернего элемента в указанном формате. Если
  /// формат не поддерживается, то результатом будет null.
  /// </summary>
  /// <param name="nodeId">Идентификатор дочернего элемента.</param>
  /// <param name="dataFormat">Тип формата данных.</param>
  /// <returns>Результирующий объект указанного типа.</returns>
  public override object GetData(INodeID nodeId, Type dataFormat)
  {
    if (dataFormat == typeof (ICanOpenInNewWindow) || dataFormat == typeof (IDescriptor))
      return base.GetData(nodeId, dataFormat);
    if (nodeId is TechCompositionNodeID && this.Descriptor is TechCompositionBaseDescriptor descriptor)
    {
      object data = descriptor.GetData(nodeId, dataFormat);
      if (data != null)
        return data;
    }
    return base.GetData(nodeId, dataFormat);
  }
}
