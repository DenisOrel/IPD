// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSSelectorFilter
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using Intermech.PropertyEditors;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

/// <summary>Фильтр типов при создании новой записи</summary>
internal class AVSSelectorFilter : TypeSelectorFilter, ISelectorFilter
{
  private List<int> _allowableTypes;
  private List<int> _filteredParentTypes;

  /// <summary>Конструктор</summary>
  /// <param name="filteredParentTypes">родительские типы для которых включена фильтрация, отображаются только те дочерние типы которые входят в allowableTypes</param>
  /// <param name="allowableTypes"></param>
  /// <param name="allowChildTypes"></param>
  /// <param name="allowAbstractTypes"></param>
  public AVSSelectorFilter(
    List<int> filteredParentTypes,
    int[] allowableTypes,
    bool allowChildTypes,
    bool allowAbstractTypes)
    : base(allowableTypes, allowChildTypes, allowAbstractTypes)
  {
    this._allowableTypes = new List<int>();
    this._allowableTypes.AddRange((IEnumerable<int>) allowableTypes);
    this._filteredParentTypes = filteredParentTypes;
  }

  bool ISelectorFilter.IsInFilter(int category, object id)
  {
    bool flag = true;
    if (id is int num1)
    {
      int objectTypeParentId = MetaDataHelper.GetObjectTypeParentID(num1);
      int num = objectTypeParentId;
      List<int> intList = new List<int>();
      if (this._allowableTypes.Contains(num1))
        intList.Add(num1);
      for (; objectTypeParentId > 0; objectTypeParentId = MetaDataHelper.GetObjectTypeParentID(objectTypeParentId))
      {
        num = objectTypeParentId;
        if (this._allowableTypes.Contains(num) && !this._filteredParentTypes.Contains(num))
          intList.Add(num);
      }
      if (this._filteredParentTypes.Contains(num) && intList.Count == 0)
      {
        flag = false;
        for (int index = 0; index < this._allowableTypes.Count; ++index)
        {
          if (MetaDataHelper.IsObjectTypeChildOf(this._allowableTypes[index], num1))
          {
            flag = true;
            break;
          }
        }
      }
    }
    return flag ? this.IsInFilter(category, id) : flag;
  }
}
