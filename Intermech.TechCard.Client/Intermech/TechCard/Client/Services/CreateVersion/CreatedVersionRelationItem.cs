// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.CreateVersion.CreatedVersionRelationItem
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;

#nullable disable
namespace Intermech.TechCard.Client.Services.CreateVersion;

internal class CreatedVersionRelationItem : RelObjInfoItem
{
  public RelObjInfoItem PrototypeRelationItem { get; set; }

  public CreatedVersionRelationItem(IDBRelation dbRel)
    : base(dbRel)
  {
  }

  public CreatedVersionRelationItem(
    RelInfoItem relInfo,
    ObjInfoItem projInfo,
    ObjInfoItem partInfo)
    : base(relInfo, projInfo, partInfo)
  {
  }
}
