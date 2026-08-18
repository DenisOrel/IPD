// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AutoSelectionTableInfo
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

public class AutoSelectionTableInfo : ICloneable
{
  protected AutoSelectionNodeCondList _condList;
  protected AutoSelectionDefRowList _rowList;

  public AutoSelectionTableInfo()
  {
    this._condList = new AutoSelectionNodeCondList();
    this._rowList = new AutoSelectionDefRowList();
  }

  public virtual void Clear()
  {
    this._condList.Clear();
    this._rowList.Clear();
  }

  public XmlNode SaveToXml(XmlDocument doc)
  {
    XmlNode element1 = (XmlNode) doc.CreateElement(nameof (AutoSelectionTableInfo));
    if (this._condList != null && this._condList.Count > 0)
    {
      XmlNode element2 = (XmlNode) doc.CreateElement("AutoSelNodeConditions");
      foreach (AutoSelectionNodeCondition cond in (List<AutoSelectionNodeCondition>) this._condList)
      {
        XmlNode xml = cond.SaveToXml(doc);
        if (xml != null)
          element2.AppendChild(xml);
      }
      element1.AppendChild(element2);
    }
    if (this._rowList != null && this._rowList.Count > 0)
    {
      XmlNode element3 = (XmlNode) doc.CreateElement("AutoSelectionDefRowList");
      foreach (AutoSelectionDefRow row in (List<AutoSelectionDefRow>) this._rowList)
      {
        XmlNode xml = row.SaveToXml(doc);
        if (xml != null)
          element3.AppendChild(xml);
      }
      element1.AppendChild(element3);
    }
    return element1;
  }

  public static AutoSelectionTableInfo LoadFromXml(XmlNode node)
  {
    AutoSelectionTableInfo selectionTableInfo = new AutoSelectionTableInfo();
    foreach (XmlNode childNode in node.ChildNodes)
    {
      if (childNode.Name.Equals(nameof (AutoSelectionTableInfo)))
      {
        node = childNode;
        break;
      }
    }
    foreach (XmlNode childNode1 in node.ChildNodes)
    {
      if (childNode1.Name.Equals("AutoSelNodeConditions"))
      {
        foreach (XmlNode childNode2 in childNode1.ChildNodes)
        {
          AutoSelectionNodeCondition selectionNodeCondition = AutoSelectionNodeCondition.LoadFromXml(childNode2);
          if (selectionNodeCondition != null)
            selectionTableInfo.CondList.Add(selectionNodeCondition);
        }
      }
    }
    foreach (XmlNode childNode3 in node.ChildNodes)
    {
      if (childNode3.Name.Equals("AutoSelectionDefRowList"))
      {
        foreach (XmlNode childNode4 in childNode3.ChildNodes)
        {
          AutoSelectionDefRow autoSelectionDefRow = AutoSelectionDefRow.LoadFromXml(childNode4);
          if (autoSelectionDefRow != null)
            selectionTableInfo.RowList.Add(autoSelectionDefRow);
        }
      }
    }
    return selectionTableInfo;
  }

  public virtual AutoSelectionNodeCondList CondList => this._condList;

  public virtual AutoSelectionDefRowList RowList => this._rowList;

  public object Clone()
  {
    AutoSelectionTableInfo selectionTableInfo = new AutoSelectionTableInfo();
    foreach (AutoSelectionNodeCondition cond in (List<AutoSelectionNodeCondition>) this.CondList)
      selectionTableInfo.CondList.Add(cond.Clone() as AutoSelectionNodeCondition);
    foreach (AutoSelectionDefRow row in (List<AutoSelectionDefRow>) this.RowList)
      selectionTableInfo.RowList.Add(row.Clone() as AutoSelectionDefRow);
    return (object) selectionTableInfo;
  }
}
