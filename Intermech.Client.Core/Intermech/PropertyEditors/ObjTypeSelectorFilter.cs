
// Type: Intermech.PropertyEditors.ObjTypeSelectorFilter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;


namespace Intermech.PropertyEditors;

/// <summary>Object type's selection filter</summary>
public class ObjTypeSelectorFilter : ISelectorFilter
{
  private List<int> objTypeList;

  /// <summary>Constructor</summary>
  /// <param name="list"></param>
  public ObjTypeSelectorFilter(List<int> list) => this.objTypeList = list;

  /// <summary>Check item in filter</summary>
  /// <param name="category"></param>
  /// <param name="id"></param>
  /// <returns></returns>
  public bool IsInFilter(int category, object id)
  {
    return category == 4 && this.objTypeList.IndexOf(Convert.ToInt32(id)) != -1;
  }
}
