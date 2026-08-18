// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.GlobalTypeNode
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Expert;

public class GlobalTypeNode : GlobalNode
{
  /// <summary>
  /// Key - ObjectTypeID, Value - индекс этого узла + 100.000 * расстояние в уровнях до типа, указанного юзером
  /// Используется для поиска узла с ближайшим соответствием типов
  /// </summary>
  public Dictionary<int, int> allObjTypes;

  /// <summary>
  /// Занести дочерний узел с индексом thisNodeIndex в список fixupList для всех соответствующих типов объектов
  /// </summary>
  /// <param name="thisNodeIndex">Индекс этого узла в списке дочерних корневого узла</param>
  /// <param name="fixupList">Список соответствий (Key-тип объекта, Value-список индексов (+100.000 за каждый уровень несоответствия)</param>
  public void InitObjTypes(int thisNodeIndex, Dictionary<int, List<int>> fixupList)
  {
    if (this.allObjTypes == null)
      this.allObjTypes = new Dictionary<int, int>();
    if (this.opGT.forObjTypeGUIDs == null)
      return;
    for (int index = 0; index < this.opGT.forObjTypeGUIDs.Count; ++index)
    {
      Guid result;
      if (Guid.TryParse(this.opGT.forObjTypeGUIDs[index], out result))
      {
        int objectTypeId = MetaDataHelper.GetObjectTypeID(result);
        if (this.opGT.forObjTypeOnly[index])
        {
          if (!this.allObjTypes.ContainsKey(objectTypeId))
            this.allObjTypes.Add(objectTypeId, thisNodeIndex);
          else
            this.allObjTypes[objectTypeId] = thisNodeIndex;
        }
        else
        {
          foreach (int num1 in MetaDataHelper.GetObjectTypeChildrenIDRecursive(result))
          {
            int num2 = this.GetDistance(objectTypeId, num1) * 100000 + thisNodeIndex;
            if (!this.allObjTypes.ContainsKey(num1))
              this.allObjTypes.Add(num1, num2);
            else if (num2 < this.allObjTypes[num1])
              this.allObjTypes[num1] = num2;
          }
        }
      }
    }
    foreach (int key in this.allObjTypes.Keys)
    {
      if (fixupList.ContainsKey(key))
        fixupList[key].Add(this.allObjTypes[key]);
      else
        fixupList.Add(key, new List<int>()
        {
          this.allObjTypes[key]
        });
    }
  }

  /// <summary>
  /// Рассчитать дальность от родительского до дочернего типа объектов
  /// (чем ближе типы по иерархии, тем больше вероятность, что для типа
  /// будет применяться именно этот оператор)
  /// </summary>
  /// <param name="parentType">Тип родительского объекта</param>
  /// <param name="childType">Тип дочернего объекта</param>
  /// <returns>Количество уровней между типами объектов (или 0, если типы одинаковы)</returns>
  private int GetDistance(int parentType, int childType)
  {
    int distance = 0;
    while (childType != parentType && childType > 0)
    {
      childType = MetaDataHelper.GetObjectTypeParentID(childType);
      ++distance;
    }
    return distance;
  }

  public OpParmGlobForType opGT => (OpParmGlobForType) this.op;
}
