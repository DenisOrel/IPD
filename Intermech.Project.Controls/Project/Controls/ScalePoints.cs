// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ScalePoints
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Project.Controls;

public class ScalePoints : List<ScalePoint>
{
  [NotNull]
  private readonly List<float> _xList = new List<float>();

  public void Add(float x, float width, DateTime date)
  {
    this._xList.Add(x);
    this.Add(new ScalePoint(x, width, date));
  }

  [NotNull]
  public string SeekDate(float x)
  {
    int index1 = 0;
    int index2 = this._xList.Count;
    while (Math.Abs(index2 - index1) > 1)
    {
      int index3 = (index2 - index1) / 2 + index1;
      if ((double) x > (double) this._xList[index3])
        index1 = index3;
      else
        index2 = index3;
    }
    DateTime date;
    string empty1;
    if (index1 >= this.Count)
    {
      empty1 = string.Empty;
    }
    else
    {
      date = this[index1]._Date;
      empty1 = date.ToString("dd.MM.yy");
    }
    string str1 = empty1;
    string empty2;
    if (index2 >= this.Count)
    {
      empty2 = string.Empty;
    }
    else
    {
      date = this[index2]._Date;
      empty2 = date.ToString("dd.MM.yy");
    }
    string str2 = empty2;
    if (str2 != str1 && str2 != string.Empty)
    {
      if (str1 != string.Empty)
        str1 += " - ";
      str1 += str2;
    }
    return str1;
  }

  public new void Clear()
  {
    base.Clear();
    this._xList.Clear();
  }
}
