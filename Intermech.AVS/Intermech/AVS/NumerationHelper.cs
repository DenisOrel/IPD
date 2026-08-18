// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.NumerationHelper
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.HelperClasses;
using Intermech.Document.UI;
using Intermech.Interfaces.AVS;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

/// <summary> Класс-нумератор позиций в спецификации </summary>
public class NumerationHelper
{
  public SpecifNumberingFull SpecifNumberingFull;
  private int currentIndex = -1;
  private ArrayList RazdelIDs = new ArrayList();
  internal AVSRow OldSpecificationRow;
  private long OldSectionID = -1;
  private int OldTypeID = -1;
  /// <summary>
  /// Раздел, если не равен null смотрятся пропуски перед разделом
  /// </summary>
  public Chapter Chapter;
  private SpecifNumbering specifNumbering;
  private List<int> existNumbers = new List<int>();
  public Dictionary<Guid, Dictionary<Guid, RowItem>> rows = new Dictionary<Guid, Dictionary<Guid, RowItem>>();

  public RowItem GetRowItem(AVSRow row)
  {
    Chapter rootChapter = row.GetRootChapter();
    Guid key = Guid.Empty;
    if (rootChapter != null && rootChapter is AdditionalChapter)
      key = rootChapter.ChapterGuid;
    if (!this.rows.ContainsKey(key))
      this.rows[key] = new Dictionary<Guid, RowItem>();
    if (!this.rows[key].ContainsKey(row.ObjGuid))
      this.rows[key][row.ObjGuid] = new RowItem();
    return this.rows[key][row.ObjGuid];
  }

  public int CurrentIndex
  {
    get => this.currentIndex;
    set => this.currentIndex = value;
  }

  /// <summary>Список установленных номеров позиций</summary>
  public List<int> ExistNumbers
  {
    get => this.existNumbers;
    set => this.existNumbers = value;
  }

  public NumerationHelper(SpecifNumberingFull specifNumberingFull)
  {
    this.SpecifNumberingFull = specifNumberingFull;
  }

  public void TrySetCurrentPosition(int foundPosition, AVSRow specificationRow)
  {
    this.OldSpecificationRow = specificationRow;
    this.OldSectionID = specificationRow.SectionID;
    this.OldTypeID = specificationRow.ObjType;
    this.CurrentIndex = this.CurrentIndex > foundPosition ? this.CurrentIndex : foundPosition;
  }

  public int GetNextAvailablePosition(AVSRow specificationRow)
  {
    int nextPosition1 = this.GetNextPosition1(specificationRow);
    for (int index = 0; this.ExistNumbers.Contains(nextPosition1) && index < 10000; nextPosition1 = this.GetNextPosition1(specificationRow))
      ++index;
    return nextPosition1;
  }

  public int GetNextPosition(AVSRow specificationRow)
  {
    int currentIndex = this.CurrentIndex;
    int nextPosition = this.GetNextAvailablePosition(specificationRow);
    if (nextPosition != -1)
    {
      int num1 = 0;
      int num2 = -1;
      bool flag1 = true;
      AVSRow row = specificationRow;
      while (flag1 && num1 < 2000000)
      {
        AvsRowAttributeInfo fieldPosition = row.Field_Position;
        row = row.GetNextRow(false);
        if (row != null && (specificationRow.avsDocument.AvsDocumentForm != AVSDocumentForm.A || specificationRow.ProductID == row.ProductID))
        {
          object fieldValue = row.GetFieldValue(fieldPosition, 0, -1, (List<RelationAttributeValuesCache>) null, false, true);
          ++num1;
          if (fieldValue != null && !(fieldValue is DBNull) && (!(fieldValue is string) || !(((string) fieldValue).Trim() == string.Empty)))
          {
            if (fieldValue is string)
            {
              int result = 0;
              if (int.TryParse((string) fieldValue, out result))
              {
                num2 = result;
                flag1 = false;
              }
            }
            else
            {
              try
              {
                num2 = Convert.ToInt32(fieldValue);
                flag1 = false;
              }
              catch
              {
              }
            }
          }
        }
        else
          break;
      }
      if (num2 != -1)
      {
        if (num2 > currentIndex && num2 < nextPosition + num1)
        {
          int num3 = (num2 - currentIndex) / (num1 + 1);
          int num4 = nextPosition;
          nextPosition = currentIndex + num3;
          if (nextPosition == 0 || nextPosition == currentIndex || nextPosition == num2 || this.ExistNumbers.Contains(nextPosition))
            nextPosition = num4;
          this.CurrentIndex = nextPosition;
        }
        if (nextPosition >= num2)
        {
          bool flag2 = false;
          if (row != null)
          {
            foreach (KeyValuePair<AVSRow, AVSRow[]> formBnumber in this.GetRowItem(row).FormBNumbers)
            {
              if (formBnumber.Key != row)
                flag2 = true;
            }
          }
          if (!flag2)
            specificationRow.avsDocument.AVSWindow.ErrorsUserControl.AddError((ImErrorMessage) new AVSRowErrorMessage(specificationRow, new SpecRowCheckMessage(AVSCheckType.All, "Позиция записи больше чем у следующей записи")));
        }
      }
    }
    return nextPosition;
  }

  private int GetNextPosition1(AVSRow specificationRow)
  {
    try
    {
      if (Array.IndexOf<long>(this.SpecifNumberingFull._NonNumneringRazdels, specificationRow.SectionID) != -1)
        return -1;
      if (!this.RazdelIDs.Contains((object) specificationRow.SectionID))
      {
        this.RazdelIDs.Add((object) specificationRow.SectionID);
        if (this.SpecifNumberingFull.SpecifRazdelNumbering.RazdelIDKeySpecifNumberingValueHash.ContainsKey(specificationRow.SectionID))
        {
          this.specifNumbering = this.SpecifNumberingFull.SpecifRazdelNumbering.RazdelIDKeySpecifNumberingValueHash[specificationRow.SectionID];
          if (this.specifNumbering.StartNumber > this.CurrentIndex)
          {
            this.CurrentIndex = this.specifNumbering.StartNumber;
            return this.CurrentIndex;
          }
        }
        else
          this.specifNumbering = (SpecifNumbering) this.SpecifNumberingFull;
      }
      if (this.CurrentIndex == -1)
      {
        this.CurrentIndex = this.specifNumbering.StartNumber;
        if (specificationRow.PositionStepBefore.HasValue)
          this.CurrentIndex = specificationRow.PositionStepBefore.Value;
        return this.CurrentIndex;
      }
      if (specificationRow.PositionStepBefore.HasValue)
      {
        this.CurrentIndex += specificationRow.PositionStepBefore.Value;
        if (this.OldSpecificationRow != null && this.OldSpecificationRow.PositionStepAfter.HasValue)
          this.CurrentIndex += this.OldSpecificationRow.PositionStepAfter.Value;
        return this.CurrentIndex;
      }
      if (this.OldSpecificationRow != null && this.OldSpecificationRow.PositionStepAfter.HasValue)
      {
        this.CurrentIndex += this.OldSpecificationRow.PositionStepAfter.Value;
        return this.CurrentIndex;
      }
      if (this.Chapter != null)
      {
        if (this.Chapter is AdditionalChapter)
          this.CurrentIndex += this.specifNumbering.BeforeNewPart;
        if (this.Chapter is VariableDataChapterFormA || this.Chapter is VariableDataChapterFormV)
        {
          this.CurrentIndex += this.specifNumbering.BeforeVariableData;
          return this.CurrentIndex;
        }
        if (this.Chapter is ProductVariableDataChapter)
        {
          this.CurrentIndex += this.specifNumbering.BeforeNewIspoln;
          return this.CurrentIndex;
        }
      }
      if (this.OldSectionID != specificationRow.SectionID)
      {
        this.CurrentIndex += this.specifNumbering.BeforeNewRazdel;
        return this.CurrentIndex;
      }
      int num = -1;
      if (specificationRow.Class != null && this.OldSpecificationRow != null && this.OldSpecificationRow.Class != specificationRow.Class)
        num = this.specifNumbering.BeforeNewObjType;
      if (this.OldSpecificationRow != null)
      {
        if (this.OldSpecificationRow.avsDocument.IsSameProductDesignations(this.OldSpecificationRow, specificationRow))
        {
          if (num == -1)
            this.CurrentIndex += this.specifNumbering.BetweenIspolns;
          else
            this.CurrentIndex += num;
          return this.CurrentIndex;
        }
        string designation1 = "";
        string designation2 = "";
        object fieldValue1 = this.OldSpecificationRow.GetFieldValue(new AvsRowAttributeInfo(false, AvsIDCache.Attr_Designation), 0, -1, false, false);
        switch (fieldValue1)
        {
          case null:
          case DBNull _:
            object fieldValue2 = specificationRow.GetFieldValue(new AvsRowAttributeInfo(false, AvsIDCache.Attr_Designation), 0, -1, false, false);
            switch (fieldValue2)
            {
              case null:
              case DBNull _:
                if (this.SpecifNumberingFull.CompareDesignationSchema.IsDesiagnationsAreSame(designation1, designation2))
                {
                  if (num == -1)
                    this.CurrentIndex += this.specifNumbering.BetweenSameDesignations;
                  else
                    this.CurrentIndex += num;
                  return this.CurrentIndex;
                }
                break;
              default:
                designation2 = Convert.ToString(fieldValue2);
                goto case null;
            }
            break;
          default:
            designation1 = Convert.ToString(fieldValue1);
            goto case null;
        }
      }
      this.CurrentIndex += this.specifNumbering.BetweenDifferentDesignations;
      return this.CurrentIndex;
    }
    finally
    {
      this.OldSpecificationRow = specificationRow;
      this.OldSectionID = specificationRow.SectionID;
      this.OldTypeID = specificationRow.ObjType;
    }
  }
}
