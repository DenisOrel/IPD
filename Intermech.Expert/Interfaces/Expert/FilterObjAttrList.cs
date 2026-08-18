// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.FilterObjAttrList
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Expert;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Expert;

public class FilterObjAttrList
{
  public int taskId;
  public List<ObjChangedList> objChangedList;

  public FilterObjAttrList(int taskId, List<ObjChangedList> ocl)
  {
    this.taskId = taskId;
    this.objChangedList = ocl;
  }
}
