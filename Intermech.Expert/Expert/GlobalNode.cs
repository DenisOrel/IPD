// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.GlobalNode
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Expert;

public class GlobalNode : ScriptTreeNode
{
  public List<ColumnDescriptor> descs;

  /// <summary>
  /// Создать дескриптор столбца. ВНИМАНИЕ: может выдать exception!
  /// </summary>
  /// <param name="attrGUID">Guid типа атрибута - он же название столбца</param>
  /// <param name="attrforRel">True, если атрибут для связи; иначе для объекта</param>
  /// <param name="orderBy">Порядок сортировки</param>
  /// <returns>ColumnDescriptor для указанного атрибута</returns>
  public static ColumnDescriptor CreateColDescr(string attrGUID, bool attrforRel, int orderBy)
  {
    bool measured = false;
    ColumnContents columnContents = DbHelper.GetColumnContents(attrGUID, out measured);
    return new ColumnDescriptor((object) new Guid(attrGUID), attrforRel ? AttributeSourceTypes.Relation : AttributeSourceTypes.Object, columnContents, ColumnNameMapping.Guid, SortOrders.NONE, measured ? 999 : orderBy);
  }

  public void CreateColumnDescs()
  {
    if (!(this.op is IDataAttrs op) || op.DataAttrGuids == null)
      return;
    if (this.descs == null)
      this.descs = new List<ColumnDescriptor>();
    for (int index = 0; index < op.DataAttrGuids.Count; ++index)
    {
      string dataAttrGuid = op.DataAttrGuids[index];
      try
      {
        this.descs.Add(GlobalNode.CreateColDescr(dataAttrGuid, op[index], this.descs.Count + 1));
      }
      catch
      {
      }
    }
  }

  /// <summary>
  /// Создать ColumnDescriptor'ы для атрибутов, заданных для этого узла, в поле descs узла.
  /// НЕ СОЗДАВАТЬ дескрипторов для тех атрибутов, которые берутся по умолчанию
  /// </summary>
  /// <param name="allGuids">Список глобальных атрибутов</param>
  public void CreateColumnDescs(List<string> allGuids)
  {
    if (!(this.op is IDataAttrs op) || op.DataAttrGuids == null)
      return;
    if (this.descs == null)
      this.descs = new List<ColumnDescriptor>();
    for (int index = 0; index < op.DataAttrGuids.Count; ++index)
    {
      string dataAttrGuid = op.DataAttrGuids[index];
      if (!allGuids.Contains(dataAttrGuid))
      {
        try
        {
          this.descs.Add(GlobalNode.CreateColDescr(dataAttrGuid, op[index], this.descs.Count + 1));
        }
        catch
        {
        }
      }
    }
  }
}
