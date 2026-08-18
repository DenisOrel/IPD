// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.RelationsComparer_F_PART_ID
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using System.Diagnostics;

#nullable disable
namespace Intermech.Pdm.Server;

public class RelationsComparer_F_PART_ID : RelationsComparer
{
  public RelationsComparer_F_PART_ID()
  {
    this._supportedFieldTypes.Add(FieldTypes.ftInteger);
    this._supportedAttributes.Add(-22);
  }

  protected override object GetRelationAttrValue(IDBRelation relation, int attrID)
  {
    return (object) (relation != null ? relation.PartID : 0L);
  }

  public override RelationsAttributeComparerCaps Capabilities
  {
    [DebuggerStepThrough] get => RelationsAttributeComparerCaps.BySingleAttribute;
  }
}
