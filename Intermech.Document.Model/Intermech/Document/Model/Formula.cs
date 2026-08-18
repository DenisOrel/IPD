// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.Formula
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

#nullable disable
namespace Intermech.Document.Model;

public class Formula
{
  public static readonly string SpecStart = "<<";
  public static readonly string SpecEnd = ">>";
  public static readonly string SpecStartOld = "«";
  public static readonly string SpecEndOld = "»";
  public static readonly char NameDivider = ':';
  public static readonly char ParmDivider = ';';
  public static readonly char FormDivider = '|';
  public static readonly string FormulaType_Index = "ИНДЕКСЫ";
  public static readonly string FormulaType_Material = "МАТЕРИАЛ";
  public static readonly float FormulaImageZoomFactor_Index = 1.6f;
  public static readonly float FormulaImageZoomFactor_Other = 1f;
  public string Id;
  public List<string> Parms;
  private Page _page;
  public PointF topLeft;
  public PointF bottomRight;
  public List<FormSearch> fields;
  public Dictionary<string, FormSearch> fieldsDic;
  public static Dictionary<TextData, CharFormat> CellsCharFormat = new Dictionary<TextData, CharFormat>();

  public Page page
  {
    get => this._page;
    set
    {
      Page page = (Page) null;
      if (value != null)
      {
        page = value.Clone() as Page;
        page.AssignIsFormulaLib(true);
      }
      this._page = page;
    }
  }

  public bool IsIndexFormula => this.Parms.Count == 2 && this.Id.Equals(Formula.FormulaType_Index);

  public bool IsMaterialFormula
  {
    get => this.Parms.Count == 2 && this.Id.Equals(Formula.FormulaType_Material);
  }

  public Formula()
  {
    this.Id = "";
    this.Parms = new List<string>();
    this.page = (Page) null;
    this.fields = new List<FormSearch>();
    this.fieldsDic = new Dictionary<string, FormSearch>();
  }

  public Formula(string aId, List<string> aParms)
  {
    this.Id = aId.ToUpper();
    this.Parms = new List<string>((IEnumerable<string>) aParms);
    this.page = (Page) null;
    this.fields = new List<FormSearch>();
    this.fieldsDic = new Dictionary<string, FormSearch>();
  }

  public Formula(Page p)
  {
    this.Id = p.Id.ToUpper();
    this.Parms = new List<string>();
    this.page = p;
    this.fields = new List<FormSearch>();
    this.fieldsDic = new Dictionary<string, FormSearch>();
    this.AccumulateEditFields((DocumentTreeNode) this.page);
    for (int index = 0; index < this.fields.Count; ++index)
    {
      if (this.fields[index].node is TextData node)
        this.Parms.Add(node.Text);
    }
  }

  public Formula(Formula other)
  {
    this.Id = other.Id;
    this.page = other.page;
    this.topLeft = other.topLeft;
    this.bottomRight = other.bottomRight;
    this.fields = new List<FormSearch>();
    this.fieldsDic = new Dictionary<string, FormSearch>();
    this.Parms = new List<string>();
    this.Parms.AddRange((IEnumerable<string>) other.Parms);
  }

  protected void InitFormula(string s)
  {
    this.Parms = new List<string>();
    this.fields = new List<FormSearch>();
    this.fieldsDic = new Dictionary<string, FormSearch>();
    int length1 = s.IndexOf(Formula.NameDivider);
    if (length1 < 0)
      length1 = s.Length;
    this.Id = s.Substring(0, length1).ToUpper();
    if (length1 >= s.Length)
      return;
    s = s.Remove(0, length1 + 1);
    while (s != "")
    {
      int length2 = s.IndexOf(Formula.ParmDivider);
      if (length2 >= 0)
      {
        this.Parms.Add(s.Substring(0, length2));
        s = s.Remove(0, length2 + 1);
        if (s == "")
          this.Parms.Add(s);
      }
      else
      {
        this.Parms.Add(s);
        break;
      }
    }
  }

  public Formula(string s)
  {
    s = s.Trim();
    this.InitFormula(s);
  }

  public Formula(string s, bool throwException)
  {
    s = s.Trim();
    int num1 = s.IndexOf(Formula.SpecStart);
    int startIndex = s.IndexOf(Formula.SpecEnd);
    int length = Formula.SpecStart.Length;
    if (num1 == -1 || startIndex == -1)
    {
      int num2 = s.IndexOf(Formula.SpecStartOld);
      if (num2 != -1)
      {
        int num3 = s.IndexOf(Formula.SpecEndOld);
        if (num3 != -1)
        {
          num1 = num2;
          startIndex = num3;
          length = Formula.SpecStartOld.Length;
        }
      }
    }
    if (num1 >= 0 && startIndex < 0 || num1 < 0 && startIndex >= 0 || startIndex < num1)
    {
      if (throwException)
        throw new Exception(LocalizationHolder.rm.GetString("Document.Model_637"));
    }
    else
    {
      if (num1 >= 0 && startIndex >= 0)
      {
        s = s.Remove(startIndex);
        s = s.Remove(0, num1 + length);
      }
      this.InitFormula(s);
    }
  }

  public override bool Equals(object obj)
  {
    if (obj == null || !(obj is Formula formula) || this.Id != formula.Id || this.Parms.Count != formula.Parms.Count)
      return false;
    for (int index = 0; index < this.Parms.Count; ++index)
    {
      if (this.Parms[index] != formula.Parms[index])
        return false;
    }
    return true;
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder(this.Id);
    if (this.Parms.Count > 0)
    {
      stringBuilder.Append(Formula.NameDivider);
      for (int index = 0; index < this.Parms.Count; ++index)
      {
        stringBuilder.Append(this.Parms[index]);
        if (index < this.Parms.Count - 1)
          stringBuilder.Append(Formula.ParmDivider);
      }
    }
    return stringBuilder.ToString();
  }

  public override int GetHashCode() => this.Id.GetHashCode();

  public bool UpdatePage(Hashtable templates)
  {
    return templates.ContainsKey((object) this.Id) && (templates[(object) this.Id] as FormSearch).node is Page node && this.page != node;
  }

  protected void AccumulateEditFields(DocumentTreeNode p)
  {
    if (p.Nodes == null)
      return;
    foreach (DocumentTreeNode node in p.Nodes)
    {
      if (node is TextData aCode && !aCode.ReadOnly && aCode.Name != "" && aCode.Name != null && !this.fieldsDic.ContainsKey(aCode.Name.ToUpper()))
      {
        FormSearch formSearch = new FormSearch(aCode.Name.ToUpper(), (DocumentTreeNode) aCode, (DocumentSection) null);
        this.fields.Add(formSearch);
        this.fieldsDic.Add(aCode.Name.ToUpper(), formSearch);
      }
      this.AccumulateEditFields(node);
    }
  }

  public virtual void GetEditFields()
  {
    if (this.Parms.Count == 0 || this.fields.Count > 0)
      return;
    this.UpdatePage(TemplateHolderBase.Instance.templates);
    if (this.page == null)
      return;
    this.fields.Clear();
    this.fieldsDic.Clear();
    this.AccumulateEditFields((DocumentTreeNode) this.page);
  }

  public void SetFormulaParms()
  {
    this.GetEditFields();
    for (int index = 0; index < this.fields.Count; ++index)
    {
      if (this.fields[index].node is TextData node)
      {
        string parm = index < this.Parms.Count ? this.Parms[index] : "";
        node.AssignText(parm, false, true, true, false, false);
        node.SetNeedUpdateLayoutFlag(true, false, false, false);
      }
    }
    if (this.page == null)
      return;
    this.page.UpdateLayout(false);
  }

  internal string ApplyFormulaFieldsFormat(string fieldFormat)
  {
    if (string.IsNullOrEmpty(fieldFormat))
      return "";
    this.GetEditFields();
    string[] source = fieldFormat.Split(';');
    for (int index = 0; index < this.fields.Count; ++index)
    {
      if (this.fields[index].node is TextData node)
      {
        string s = index < source.Length ? source[index] : "";
        int result;
        if (!string.IsNullOrEmpty(s) && int.TryParse(s, out result))
        {
          if (!Formula.CellsCharFormat.ContainsKey(node))
            Formula.CellsCharFormat.Add(node, node.CharFormat);
          CharFormat charFormat = node.CharFormat.Clone();
          charFormat.CharStyle = (CharStyle) result;
          node.SetCharFormat(charFormat, false, false);
          node.SetNeedUpdateLayoutFlag(true, false, false, false);
        }
        else
        {
          CharFormat charFormat;
          if (Formula.CellsCharFormat.TryGetValue(node, out charFormat) && node.CharFormat != charFormat)
          {
            node.SetCharFormat(charFormat, false, false);
            node.SetNeedUpdateLayoutFlag(true, false, false, false);
          }
        }
      }
    }
    if (this.page != null)
      this.page.UpdateLayout(false);
    return source.Length > this.fields.Count ? string.Join(";", ((IEnumerable<string>) source).Skip<string>(this.fields.Count)) : "";
  }

  internal string GetFormulaFieldsFormat()
  {
    this.GetEditFields();
    List<string> values = new List<string>();
    for (int index = 0; index < this.fields.Count; ++index)
    {
      if (this.fields[index].node is TextData node)
      {
        string str = "";
        CharFormat other;
        if (node.CharFormat != null && Formula.CellsCharFormat.TryGetValue(node, out other) && !node.CharFormat.Equals(other))
          str = ((int) node.CharFormat.CharStyle).ToString();
        values.Add(str);
      }
    }
    return values.Count > 0 ? string.Join(";", (IEnumerable<string>) values) : "";
  }

  public void SetCharFormatForAllFields(CharFormat charFormat)
  {
    this.GetEditFields();
    for (int index = 0; index < this.fields.Count; ++index)
    {
      if (this.fields[index].node is TextData node)
        node.SetCharFormat(charFormat.Clone(), false, false);
    }
  }

  /// <summary>Calculate topLeft and bottomRight for the formula</summary>
  public void CalcCoords()
  {
    float num1 = 1E+38f;
    float num2 = -1E+38f;
    float num3 = 1E+38f;
    float num4 = -1E+38f;
    if (this.page?.Nodes == null)
      return;
    foreach (DocumentTreeNode node in this.page.Nodes)
    {
      if (node is RectangleElement rectangleElement)
      {
        PointF location = rectangleElement.Location;
        if ((double) location.X < (double) num1)
        {
          location = rectangleElement.Location;
          num1 = location.X;
        }
        location = rectangleElement.Location;
        if ((double) location.Y < (double) num3)
        {
          location = rectangleElement.Location;
          num3 = location.Y;
        }
        location = rectangleElement.Location;
        double x1 = (double) location.X;
        SizeF size = rectangleElement.Size;
        double width1 = (double) size.Width;
        if (x1 + width1 > (double) num2)
        {
          location = rectangleElement.Location;
          double x2 = (double) location.X;
          size = rectangleElement.Size;
          double width2 = (double) size.Width;
          num2 = (float) (x2 + width2);
        }
        location = rectangleElement.Location;
        double y1 = (double) location.Y;
        size = rectangleElement.Size;
        double height1 = (double) size.Height;
        if (y1 + height1 > (double) num4)
        {
          location = rectangleElement.Location;
          double y2 = (double) location.Y;
          size = rectangleElement.Size;
          double height2 = (double) size.Height;
          num4 = (float) (y2 + height2);
        }
      }
      else if (node is Polyline polyline)
      {
        foreach (PointF pathPoint in polyline.PathPoints)
        {
          if ((double) pathPoint.X < (double) num1)
            num1 = pathPoint.X;
          if ((double) pathPoint.X > (double) num2)
            num2 = pathPoint.X;
          if ((double) pathPoint.Y < (double) num3)
            num3 = pathPoint.Y;
          if ((double) pathPoint.Y > (double) num4)
            num4 = pathPoint.Y;
        }
      }
    }
    this.topLeft.X = num1;
    this.topLeft.Y = num3;
    this.bottomRight.X = num2;
    this.bottomRight.Y = num4;
    SizeF size1 = this.page.Size;
    if ((double) size1.Width >= (double) num2 && (double) size1.Height >= (double) this.Height)
      return;
    if ((double) size1.Width < (double) num2)
      size1.Width = num2;
    if ((double) size1.Height < (double) num4)
      size1.Height = num4;
    this.page.Size = size1;
  }

  public float Width => this.bottomRight.X - this.topLeft.X;

  public float Height => this.bottomRight.Y - this.topLeft.Y;

  public void AdjustCoordsTo(PointF org)
  {
    float DiffX = org.X - this.topLeft.X;
    float DiffY = org.Y - this.topLeft.Y;
    this.topLeft = org;
    this.bottomRight.X += DiffX;
    this.bottomRight.Y += DiffY;
    if (this.page?.Nodes == null)
      return;
    foreach (DocumentTreeNode node in this.page.Nodes)
      this.MoveNode(node, DiffX, DiffY);
  }

  /// <summary>
  /// Передвинуть элемент на MoveX по горизонтали и MoveY по вертикали
  /// </summary>
  /// <param name="dtn">Передвигаемый элемент</param>
  /// <param name="DiffX"></param>
  /// <param name="DiffY"></param>
  internal void MoveNode(DocumentTreeNode dtn, float DiffX, float DiffY)
  {
    if (dtn is RectangleElement rectangleElement)
    {
      PointF location1;
      ref PointF local = ref location1;
      PointF location2 = rectangleElement.Location;
      double x = (double) location2.X + (double) DiffX;
      location2 = rectangleElement.Location;
      double y = (double) location2.Y + (double) DiffY;
      local = new PointF((float) x, (float) y);
      rectangleElement.AssignBounds(location1, rectangleElement.Size, false, false, false);
    }
    else if (dtn is Polyline polyline)
    {
      for (int index = 0; index < polyline.PathPoints.Length; ++index)
      {
        polyline.PathPoints[index].X += DiffX;
        polyline.PathPoints[index].Y += DiffY;
      }
    }
    if (dtn.Nodes == null)
      return;
    foreach (DocumentTreeNode node in dtn.Nodes)
      this.MoveNode(node, DiffX, DiffY);
  }

  public void PerformHorzAligns(DocumentTreeNode root)
  {
    if (root == null || root.Nodes == null)
      return;
    Dictionary<string, RectangleF> newBoundsDict = new Dictionary<string, RectangleF>();
    foreach (DocumentTreeNode node in root.Nodes)
    {
      if (node is RectangleElement el && el.HorzAlign != ElementHorizontalAlign.None)
        this.HorzAlign(root, el, el.HorzAlign == ElementHorizontalAlign.Left, newBoundsDict);
      if (node.Nodes != null && node.NodesCount > 0)
        this.PerformHorzAligns(node);
    }
    foreach (DocumentTreeNode node in root.Nodes)
    {
      if (node is RectangleElement rectangleElement && newBoundsDict.ContainsKey(rectangleElement.Id))
        rectangleElement.AssignBounds(newBoundsDict[rectangleElement.Id], false, false, false);
    }
    this.page.UpdateLayout(false);
  }

  /// <summary>Returns true if something was changed</summary>
  /// <param name="root"></param>
  /// <param name="el"></param>
  /// <param name="Left"></param>
  /// <returns></returns>
  protected bool HorzAlign(
    DocumentTreeNode root,
    RectangleElement el,
    bool Left,
    Dictionary<string, RectangleF> newBoundsDict)
  {
    float num1 = Left ? -1E+38f : 1E+38f;
    float num2 = num1;
    foreach (DocumentTreeNode node in root.Nodes)
    {
      if (node != el && node is RectangleElement rectangleElement)
      {
        PointF location = rectangleElement.Location;
        double y1 = (double) location.Y;
        RectangleF bounds1 = el.Bounds;
        double bottom1 = (double) bounds1.Bottom;
        if (y1 < bottom1)
        {
          bounds1 = rectangleElement.Bounds;
          double bottom2 = (double) bounds1.Bottom;
          location = el.Location;
          double y2 = (double) location.Y;
          if (bottom2 > y2)
          {
            RectangleF bounds2 = rectangleElement.Bounds;
            if (newBoundsDict.ContainsKey(rectangleElement.Id))
              bounds2 = newBoundsDict[rectangleElement.Id];
            if (Left)
            {
              if (rectangleElement.HorzAlign != ElementHorizontalAlign.Right && rectangleElement.HorzAlign != ElementHorizontalAlign.Center && (double) bounds2.Right > (double) num1)
              {
                bounds1 = rectangleElement.Bounds;
                double x1 = (double) bounds1.X;
                bounds1 = el.Bounds;
                double x2 = (double) bounds1.X;
                if (x1 < x2)
                  num1 = bounds2.Right;
              }
            }
            else
            {
              location = rectangleElement.Location;
              double x3 = (double) location.X;
              SizeF size = rectangleElement.Size;
              double width1 = (double) size.Width;
              double num3 = x3 + width1;
              location = el.Location;
              double x4 = (double) location.X;
              size = el.Size;
              double width2 = (double) size.Width;
              double num4 = x4 + width2;
              if (num3 > num4)
              {
                location = rectangleElement.Location;
                if ((double) location.X < (double) num1)
                {
                  location = rectangleElement.Location;
                  num1 = location.X;
                }
              }
            }
          }
        }
      }
    }
    if ((double) num1 == (double) num2)
      return false;
    float num5 = Left ? num1 : num1 - el.Size.Width;
    double num6 = (double) num5;
    PointF location1 = el.Location;
    double x5 = (double) location1.X;
    int num7 = num6 != x5 ? 1 : 0;
    Dictionary<string, RectangleF> dictionary = newBoundsDict;
    string id = el.Id;
    double x6 = (double) num5;
    location1 = el.Location;
    double y = (double) location1.Y;
    RectangleF rectangleF = new RectangleF(new PointF((float) x6, (float) y), el.Size);
    dictionary[id] = rectangleF;
    return num7 != 0;
  }
}
