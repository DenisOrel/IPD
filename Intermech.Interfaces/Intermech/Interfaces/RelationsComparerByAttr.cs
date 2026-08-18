
// Type: Intermech.Interfaces.RelationsComparerByAttr
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс для сравнения связей по значению строкового атрибута, хранящего в себе число (в виде строки)
    /// </summary>
    public class RelationsComparerByAttr : IComparer<long>
    {
      /// <summary>Список атрибутов связей</summary>
      private RelationAttributesPackage _relAttrs;
      /// <summary>Идентификатор атрибута</summary>
      private int _attrID = -10000;

      /// <summary>Создать экземпляр сравнивателя</summary>
      /// <param name="relAttrs">Список атрибутов связей</param>
      /// <param name="attrID">Идентификатор атрибута</param>
      public RelationsComparerByAttr(RelationAttributesPackage relAttrs, int attrID)
      {
        this._relAttrs = relAttrs;
        this._attrID = attrID;
      }

      /// <summary>Сравнить две связи с указанными идентификаторами</summary>
      /// <param name="x">Идентификатор связи [1]</param>
      /// <param name="y">Идентификатор связи [2]</param>
      /// <returns>-1, если связь [1] меньше связи [2], 0, если связи равны, 1, если связь [1] больше связи [2]</returns>
      public int Compare(long x, long y)
      {
        if (this._attrID == -10000 || this._relAttrs == null)
          return 0;
        object relAttr1 = this._relAttrs[x, this._attrID];
        object relAttr2 = this._relAttrs[y, this._attrID];
        long result1;
        long result2;
        return relAttr1 == null || relAttr2 == null || !long.TryParse(relAttr1.ToString(), out result1) || !long.TryParse(relAttr2.ToString(), out result2) ? 0 : result1.CompareTo(result2);
      }
    }
}
