// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.WorkflowMapText
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Map;
using System;
using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace Intermech.Workflow.Design;

[Serializable]
internal class WorkflowMapText : MapText
{
  public override RectangleF SetRectangleSpotLocation(RectangleF r, int spot, PointF p)
  {
    RectangleF rectangleF = base.SetRectangleSpotLocation(r, spot, p);
    rectangleF.X = (float) Math.Round((double) rectangleF.X);
    rectangleF.Y = (float) Math.Round((double) rectangleF.Y);
    return rectangleF;
  }

  public string DoWordWrap(string s, int length)
  {
    List<string> stringList = new List<string>();
    string str1 = "";
    int startIndex = 0;
    do
    {
      int num = s.IndexOfAny(new char[4]
      {
        ' ',
        '=',
        ')',
        '>'
      }, startIndex);
      if (num > 0)
        stringList.Add(s.Substring(startIndex, num - startIndex + 1));
      else
        stringList.Add(s.Substring(startIndex, s.Length - startIndex));
      startIndex = num;
      if (startIndex > 0)
        ++startIndex;
    }
    while (startIndex > 0);
    int num1 = 0;
    foreach (string str2 in stringList)
    {
      num1 += str2.Length;
      if (num1 > length)
      {
        if (str1 != "")
          str1 += "\r\n";
        num1 = str2.Length;
      }
      str1 += str2;
    }
    return str1;
  }

  public bool HasBorder
  {
    get => this.Bordered;
    set
    {
      this.TransparentBackground = !value;
      this.Bordered = value;
      this.Shadowed = value;
    }
  }

  public WorkflowMapText(bool hasBorder)
  {
    this.Alignment = 1;
    this.Selectable = false;
    this.BackgroundColor = Color.White;
    this.Multiline = true;
    this.Wrapping = true;
    this.FamilyName = "MS Sans Serif";
    this.FontSize = 10f;
    this.HasBorder = hasBorder;
  }
}
