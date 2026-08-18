// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Nodes.TechCompositionNodeID
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Navigator.Params;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Nodes;

/// <summary>TechCard compositions node's description</summary>
public class TechCompositionNodeID : AdvRelationsNodeID
{
  /// <summary>Создать новый узел</summary>
  /// <param name="e">Параметры</param>
  public TechCompositionNodeID(CreateObjectNodeParams e)
    : base(e)
  {
    this.pars = (CreateObjectNodeParams) new CreateTechNodeParams((object) e);
  }

  /// <summary>
  /// 
  /// </summary>
  internal CreateObjectNodeParams Params
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.pars;
  }

  /// <summary>
  /// Список дополнительных идентификаторов атрибутов, которые будут загружаться в узел независимо от видимых колонок
  /// </summary>
  public List<NodeColumnID> Attributes
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return !(this.pars is CreateTechNodeParams pars) ? (List<NodeColumnID>) null : pars.Attributes;
    }
  }

  /// <summary>
  /// Список дополнительных идентификаторов атрибутов, которые будут загружаться в узел независимо от видимых колонок
  /// </summary>
  public override object[] Values
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return !(this.pars is CreateTechNodeParams pars) ? (object[]) null : pars.Values;
    }
  }

  /// <summary>Значение указанного атрибута</summary>
  /// <param name="attributeId">Идентификатор атрибута</param>
  /// <returns>null, если значение атрибута не найдено</returns>
  public override object this[int attributeId]
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      if (!(this.pars is CreateTechNodeParams pars))
        return (object) null;
      for (int index = 0; index < pars.Attributes.Count; ++index)
      {
        if (pars.Attributes[index].ID.Equals((object) attributeId))
          return pars.Values[index];
      }
      return (object) null;
    }
  }

  /// <summary>Compare with object</summary>
  /// <param name="obj">Compared object</param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    if (!(obj is TechCompositionNodeID compositionNodeId))
      return base.Equals(obj);
    long prjLinkId1 = this.PrjLinkID;
    switch (prjLinkId1)
    {
      case -1:
      case 0:
label_5:
        return this.ObjectID == compositionNodeId.ObjectID;
      default:
        long prjLinkId2 = compositionNodeId.PrjLinkID;
        switch (prjLinkId2)
        {
          case -1:
          case 0:
            goto label_5;
          default:
            return prjLinkId1 == prjLinkId2;
        }
    }
  }

  /// <summary>Get object's hash code</summary>
  /// <returns></returns>
  public override int GetHashCode()
  {
    long prjLinkId = this.PrjLinkID;
    switch (prjLinkId)
    {
      case -1:
      case 0:
        return this.ObjectID.GetHashCode();
      default:
        return prjLinkId.GetHashCode() << 16 /*0x10*/ ^ prjLinkId.GetHashCode();
    }
  }
}
