// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.VarTypeImageIndex
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System;

#nullable disable
namespace Intermech.Workflow.Design;

public class VarTypeImageIndex
{
  private int[] _indexes = new int[Enum.GetValues(typeof (VarType)).Length];

  public int this[VarType vt]
  {
    get
    {
      int index = (int) vt;
      if (this._indexes[index] == 0)
        this._indexes[index] = BaseHolder.IconService.IndexOf(3, -1, (object) vt);
      return this._indexes[index];
    }
  }
}
