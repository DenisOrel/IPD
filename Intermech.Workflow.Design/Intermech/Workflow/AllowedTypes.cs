// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.AllowedTypes
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces;
using Intermech.Workflow.Design;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow;

public class AllowedTypes
{
  public readonly long ProcessID;
  public const int RootID = 3;
  private List<int> _allAttachTypes;
  public string PrevAsString;
  private HashSet<int> _IDs;
  private List<int> _checkedSubTypes = new List<int>();

  public AllowedTypes(long processID) => this.ProcessID = processID;

  public List<int> AllAttachTypes
  {
    get
    {
      if (this._allAttachTypes == null)
        this._allAttachTypes = wfFunx.GetApplicableAttachmentTypes(wfConsts.StartTypeID, wfConsts.AttachmentRelationTypeID);
      return this._allAttachTypes;
    }
  }

  public void Load()
  {
    if (this.ProcessID == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(this.ProcessID, wfConsts.AttrAllowedAttachTypesID);
      this.AsString = objectAttributeById != null ? objectAttributeById.Value.ToString() : "";
    }
  }

  public string AsString
  {
    get => this._IDs == null ? "" : string.Join<int>(",", (IEnumerable<int>) this._IDs);
    set
    {
      this.PrevAsString = value;
      this._IDs = new HashSet<int>();
      string str1 = value;
      char[] chArray = new char[1]{ ',' };
      foreach (string str2 in str1.Split(chArray))
      {
        if (str2 != "")
          this._IDs.Add(Convert.ToInt32(str2));
      }
      if (this._IDs.Count != 0)
        return;
      this._IDs.Add(3);
    }
  }

  public HashSet<int> IDs
  {
    get
    {
      if (this._IDs == null)
        this.Load();
      return this._IDs;
    }
  }

  public List<int> GetTypeChildren(int atype, bool recursive = false)
  {
    List<int> typeChildren;
    if (atype == 3)
    {
      typeChildren = this._allAttachTypes;
      if (recursive)
      {
        int count = typeChildren.Count;
        for (int index = 0; index < count; ++index)
        {
          List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(typeChildren[index]);
          childrenIdRecursive.Remove(typeChildren[index]);
          typeChildren.AddRange((IEnumerable<int>) childrenIdRecursive);
        }
      }
    }
    else if (recursive)
    {
      typeChildren = MetaDataHelper.GetObjectTypeChildrenIDRecursive(atype);
      typeChildren.Remove(atype);
    }
    else
      typeChildren = MetaDataHelper.GetObjectTypeChildrenID(atype);
    return typeChildren;
  }

  /// <summary>
  /// Вычисляет состояние отметку для типа atype. Если результат == CheckState.Indeterminate, то значит в подтипах есть как включенные, так и выключенные типы
  /// </summary>
  public CheckState CalcCheckState(
    int atype,
    bool defaultCheck = false,
    bool checkParents = true,
    bool checkChildren = true)
  {
    if (checkParents)
      this._checkedSubTypes.Clear();
    if (this.IDs.Count == 1 && this.IDs.Contains(3))
      return CheckState.Checked;
    bool defaultCheck1 = defaultCheck;
    List<int> intList;
    if (checkParents)
    {
      intList = MetaDataHelper.GetObjectTypeParentsID(atype);
      intList.Add(3);
    }
    else
      intList = new List<int>();
    intList.Insert(0, atype);
    foreach (int num in intList)
    {
      if (this.IDs.Contains(-num))
      {
        defaultCheck1 = false;
        break;
      }
      if (this.IDs.Contains(num))
      {
        defaultCheck1 = true;
        break;
      }
    }
    if (checkChildren)
    {
      List<int> typeChildren = this.GetTypeChildren(atype);
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = true;
      bool flag4 = true;
      List<int> collection = new List<int>();
      foreach (int atype1 in typeChildren)
      {
        CheckState checkState = this.CalcCheckState(atype1, defaultCheck1, false);
        if (checkState != CheckState.Checked)
        {
          flag3 = false;
          flag2 = true;
        }
        if (checkState != CheckState.Unchecked)
        {
          flag4 = false;
          flag1 = true;
          if (checkState == CheckState.Checked)
            collection.Add(atype1);
        }
      }
      if (flag4 && !flag3)
        return CheckState.Unchecked;
      if (flag1 & flag2)
      {
        this._checkedSubTypes.AddRange((IEnumerable<int>) collection);
        return CheckState.Indeterminate;
      }
    }
    return !defaultCheck1 ? CheckState.Unchecked : CheckState.Checked;
  }

  /// <summary>
  /// Фильтрует типы, оставляя среди них только разрешенные.
  /// </summary>
  /// <param name="attTypes"></param>
  public void Filter(List<int> attTypes)
  {
    int count = attTypes.Count;
    int index = 0;
    while (index < count)
    {
      int num = (int) this.CalcCheckState(attTypes[index]);
      if (num != 1)
        attTypes[index] = 0;
      ++index;
      if (num == 2)
      {
        foreach (int checkedSubType in this._checkedSubTypes)
        {
          if (!attTypes.Contains(checkedSubType))
          {
            attTypes.Insert(index, checkedSubType);
            ++index;
            ++count;
          }
        }
      }
    }
    attTypes.RemoveAll((Predicate<int>) (t => t == 0));
  }
}
