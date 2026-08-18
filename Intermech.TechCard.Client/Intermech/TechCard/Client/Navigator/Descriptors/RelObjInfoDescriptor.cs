// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Descriptors.RelObjInfoDescriptor
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Interfaces.Compositions;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Descriptors;

/// <summary>
/// Спец. дескриптор для работы с объектом в контексте связи с родителем
/// </summary>
internal sealed class RelObjInfoDescriptor : Descriptor
{
  /// <summary>Информация о связи</summary>
  private readonly RelObjInfoItem _relationInfoItem;

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора
  /// </summary>
  /// <param name="state"></param>
  public RelObjInfoDescriptor(PersistentState state)
    : base(state)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="relationInfoItem"></param>
  public RelObjInfoDescriptor([NotNull] RelObjInfoItem relationInfoItem)
    : base(relationInfoItem.PartInfo.ObjectID)
  {
    this._relationInfoItem = relationInfoItem;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="dbObject"></param>
  /// <param name="createObjectNodeParams"></param>
  /// <returns></returns>
  protected override INodeID CreateObjectNodeIdFromParams(
    CreateObjectNodeParams createObjectNodeParams)
  {
    createObjectNodeParams.PrjLinkID = this._relationInfoItem.RelationID;
    createObjectNodeParams.RelationTypeID = this._relationInfoItem.RelTypeID;
    if ((TypedInfoItem) this._relationInfoItem.ProjInfo != (TypedInfoItem) null)
      createObjectNodeParams.ProjID = this._relationInfoItem.ProjInfo.ObjectID;
    return base.CreateObjectNodeIdFromParams(createObjectNodeParams);
  }
}
