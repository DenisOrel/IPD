
// Type: Intermech.PropertyEditors.Attr4TypeList
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Controls;
using Intermech.Expressions;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class Attr4TypeList : ArrayList
{
  protected EventsHolder.GetListDelegate getMasterList;
  private Attr4TypeList sortedList;
  private List<int> queueList;
  private int startIndex;
  private int finishIndex;
  private bool ascending;

  public EventsHolder.GetListDelegate GetMasterList => this.getMasterList;

  public Attr4TypeList()
  {
  }

  public Attr4TypeList(EventsHolder.GetListDelegate aGetMasterList)
  {
    this.getMasterList = aGetMasterList;
  }

  public static int IndexOfByAttributeId(int attributeId, Attr4TypeList list)
  {
    int num = -1;
    for (int index = 0; index < list.Count; ++index)
    {
      if (((Attr4TypeClass) list[index]).AttributeID == attributeId)
      {
        num = index;
        break;
      }
    }
    return num;
  }

  public static int IndexOfByAttributeName(string attributeName, Attr4TypeList list)
  {
    int num = -1;
    for (int index = 0; index < list.Count; ++index)
    {
      if (((Attr4TypeClass) list[index]).AttributeName == attributeName)
      {
        num = index;
        break;
      }
    }
    return num;
  }

  /// <summary>
  /// сортировать по использованию атрибутов в формулах списка
  /// </summary>
  /// <param name="startIndex">индекс первого сортируемого элемента</param>
  /// <param name="finishIndex">индекс последнего сортируемого элемента</param>
  /// <param name="ascending">true - сначала располагаются те, которые в формуле не ссылаются на другие атрибуты; в конце те, которые ссылаются на другие атрибуты.
  /// false - сначала располагаются те, которые в формуле ссылаются на другие атрибуты; в конце списка те, которые в формуле не ссылаются на другие атрибуты</param>
  public void SortByAttrAtFormula(int startIndex, int finishIndex, bool ascending)
  {
    this.startIndex = startIndex;
    this.finishIndex = finishIndex;
    this.ascending = ascending;
    this.SortArray();
  }

  /// <summary>вызывается сугубо изнутри SortByAttrAtFormula</summary>
  private void SortArray()
  {
    this.sortedList = new Attr4TypeList();
    this.queueList = new List<int>();
    using (Parser parser = new Parser())
    {
      parser.AutoDetectVariables = true;
      parser.Validate = false;
      for (int startIndex = this.startIndex; startIndex <= this.finishIndex; ++startIndex)
        this.ParseFormulaFor(parser, (Attr4TypeClass) this[startIndex]);
    }
    for (int index = 0; index < this.sortedList.Count; ++index)
      this[this.ascending ? this.startIndex + index : this.finishIndex - index] = this.sortedList[index];
  }

  private bool ParseFormulaFor(Parser parser, Attr4TypeClass c)
  {
    if (this.queueList.IndexOf(c.AttributeID) != -1)
      throw new Exception($"{LocalizationHolder.rm.GetString("Client.Core_58")}{c.AttributeName}\"");
    if (Attr4TypeList.IndexOfByAttributeId(c.AttributeID, this.sortedList) != -1)
      return true;
    ExpressionVariablesCollection variablesCollection = (ExpressionVariablesCollection) null;
    ExpressionTree expressionTree = parser.Parse(c.Formula);
    if (expressionTree != null)
      variablesCollection = expressionTree.Variables;
    if (expressionTree == null || variablesCollection.Count == 0)
    {
      this.sortedList.Add((object) c);
      return true;
    }
    this.queueList.Add(c.AttributeID);
    try
    {
      for (int index1 = 0; index1 < variablesCollection.Count; ++index1)
      {
        int index2 = Attr4TypeList.IndexOfByAttributeName(variablesCollection[index1].Name, this);
        if (index2 == -1 && !ObligatoryObjectAttributesHelper.IsObligatoryAttribute(variablesCollection[index1].Name))
        {
          if (IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_61") + c.Formula + LocalizationHolder.rm.GetString("Client.Core_62"), LocalizationHolder.rm.GetString("Client.Core_59") + c.AttributeName + LocalizationHolder.rm.GetString("Client.Core_60") + variablesCollection[index1].Name + MessageDialogs.msgWarning, MessageBoxButtons.YesNo, IMMessageBoxImage.Question) != DialogResult.Yes)
            throw new Exception(LocalizationHolder.rm.GetString("Client.Core_63") + variablesCollection[index1].Name + LocalizationHolder.rm.GetString("Client.Core_64"));
        }
        if (index2 >= this.startIndex && index2 <= this.finishIndex)
          this.ParseFormulaFor(parser, (Attr4TypeClass) this[index2]);
      }
      this.sortedList.Add((object) c);
    }
    finally
    {
      this.queueList.RemoveAt(this.queueList.Count - 1);
    }
    return true;
  }
}
