// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.HelperClasses.SkipLinesStruct
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Document;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.AVS.HelperClasses;

public class SkipLinesStruct
{
  private SpecificationSection specSection;
  private Chapter chapter;
  private AVSRow specRow;
  private float skipBefore = -1f;
  private float skipAfter = -1f;
  private bool beforeSetted;
  private bool afterSetted;

  public SkipLinesStruct(Chapter chapter) => this.chapter = chapter;

  public SkipLinesStruct(AVSRow specRow) => this.specRow = specRow;

  public override string ToString()
  {
    if (this.chapter != null)
      return this.Chapter.ToString();
    return this.specRow != null ? this.specRow.ToString() : base.ToString();
  }

  public SpecificationSection SpecSection
  {
    get => this.specSection;
    set => this.specSection = value;
  }

  public Chapter Chapter
  {
    get => this.chapter;
    set => this.chapter = value;
  }

  public AVSRow SpecRow
  {
    get => this.specRow;
    set => this.specRow = value;
  }

  public float SkipBefore
  {
    get => this.skipBefore;
    set => this.skipBefore = value;
  }

  public float SkipAfter
  {
    get => this.skipAfter;
    set => this.skipAfter = value;
  }

  public bool BeforeSetted
  {
    get => this.beforeSetted;
    set => this.beforeSetted = value;
  }

  public bool AfterSetted
  {
    get => this.afterSetted;
    set => this.afterSetted = value;
  }

  internal static SkipLinesStruct GetStruct(object obj)
  {
    switch (obj)
    {
      case AVSRow _:
        return SkipLinesStruct.GetStruct(obj as AVSRow);
      case Chapter _:
        return SkipLinesStruct.GetStruct(obj as Chapter);
      default:
        return (SkipLinesStruct) null;
    }
  }

  private static SkipLinesStruct GetStruct(AVSRow row)
  {
    SkipLinesStruct skipLinesStruct = new SkipLinesStruct(row);
    if (row.DocNode != null)
    {
      skipLinesStruct.SkipBefore = row.DocNode.SkipCellsBefore;
      skipLinesStruct.SkipAfter = row.DocNode.SkipCellsAfter;
      skipLinesStruct.BeforeSetted = row.SkipLinesBeforeIsOverriden;
      skipLinesStruct.AfterSetted = row.SkipLinesAfterIsOverriden;
    }
    return skipLinesStruct;
  }

  private static SkipLinesStruct GetStruct(Chapter ch)
  {
    SkipLinesStruct skipLinesStruct = new SkipLinesStruct(ch);
    if (ch.DocNode != null)
    {
      TableData tableData = ch.DocNode.NodesCount > 0 ? ch.DocNode.Nodes[0] as TableData : ch.DocNode;
      switch (ch)
      {
        case ProductVariableDataChapter _:
        case VariableDataChapterFormA _:
        case SpecificationSection _:
          skipLinesStruct.SkipBefore = tableData.SkipCellsBefore;
          skipLinesStruct.SkipAfter = tableData.SkipCellsAfter;
          break;
        default:
          if (tableData.Template is TableData template)
          {
            skipLinesStruct.SkipBefore = template.SkipCellsBefore;
            skipLinesStruct.SkipAfter = template.SkipCellsAfter;
            break;
          }
          break;
      }
      if (ch is SpecificationSection specificationSection)
      {
        skipLinesStruct.BeforeSetted = specificationSection.SkipLinesBefore.HasValue;
        skipLinesStruct.AfterSetted = specificationSection.SkipLinesAfter.HasValue;
      }
    }
    return skipLinesStruct;
  }

  public void SetBefore(int beforeSkipLines, bool updateUI, bool updateLayout)
  {
    if (this.Chapter != null)
    {
      List<TableData> docNodes = this.Chapter.DocNodes;
      for (int index = 0; index < docNodes.Count; ++index)
        (docNodes[index].NodesCount > 0 ? (RectangleElement) (docNodes[index].Nodes[0] as TableData) : (RectangleElement) docNodes[index]).SetSkipCellsBefore((float) beforeSkipLines, true, updateUI, updateLayout);
    }
    if (this.SpecRow == null)
      return;
    List<TableData> docNodes1 = this.SpecRow.DocNodes;
    for (int index = 0; index < docNodes1.Count; ++index)
      docNodes1[index].SetSkipCellsBefore((float) beforeSkipLines, true, updateUI, updateLayout);
  }

  public void SetAfter(int afterSkipLines, bool updateUI, bool updateLayout)
  {
    if (this.Chapter != null)
    {
      List<TableData> docNodes = this.Chapter.DocNodes;
      for (int index = 0; index < docNodes.Count; ++index)
        (docNodes[index].NodesCount > 0 ? (RectangleElement) (docNodes[index].Nodes[0] as TableData) : (RectangleElement) docNodes[index]).SetSkipCellsAfter((float) afterSkipLines, true, updateUI, updateLayout);
    }
    if (this.SpecRow == null)
      return;
    List<TableData> docNodes1 = this.SpecRow.DocNodes;
    for (int index = 0; index < docNodes1.Count; ++index)
      docNodes1[index].SetSkipCellsAfter((float) afterSkipLines, true, updateUI, updateLayout);
  }

  /// <summary>Обновить пропуск строки после у предыдущего элемента</summary>
  /// <param name="obj">Текущий объект</param>
  /// <param name="prevObj">Предыдущий объект</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку страниц</param>
  public static void UpdatePrevElement(
    object obj,
    object prevObj,
    bool updateUI,
    bool updateLayout)
  {
    SkipLinesStruct.CompareSkipLineSettings(SkipLinesStruct.GetStruct(prevObj), SkipLinesStruct.GetStruct(obj), updateUI, updateLayout);
  }

  /// <summary>Обновить пропуск строки после у предыдущего элемента</summary>
  /// <param name="obj">Текущий объект</param>
  /// <param name="updateUI">Обновлять интерфейс пользователя</param>
  /// <param name="updateLayout">Обновлять разбивку страниц</param>
  public static void UpdatePrevElement(object obj, bool updateUI, bool updateLayout)
  {
    SkipLinesStruct.CompareSkipLineSettings(SkipLinesStruct.GetPrevSkipLinesStruct(obj), SkipLinesStruct.GetStruct(obj), updateUI, updateLayout);
  }

  public static void UpdateNextElement(object obj, bool updateUI, bool updateLayout)
  {
    SkipLinesStruct nextSkipLinesStruct = SkipLinesStruct.GetNextSkipLinesStruct(obj);
    SkipLinesStruct.CompareSkipLineSettings(SkipLinesStruct.GetStruct(obj), nextSkipLinesStruct, updateUI, updateLayout);
  }

  public static void UpdateNextElement(
    object obj,
    object nextElement,
    bool updateUI,
    bool updateLayout)
  {
    SkipLinesStruct sl = SkipLinesStruct.GetStruct(nextElement);
    SkipLinesStruct.CompareSkipLineSettings(SkipLinesStruct.GetStruct(obj), sl, updateUI, updateLayout);
  }

  /// <summary>Сравнить настройки пропусков между двумя записями документа и расставить согласно приоритетам</summary>
  /// <param name="prevsl">Предыдущая строка</param>
  /// <param name="sl">Текущая строка</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку по страницам</param>
  internal static void CompareSkipLineSettings(
    SkipLinesStruct prevsl,
    SkipLinesStruct sl,
    bool updateUI,
    bool updateLayout)
  {
    if (sl == null || prevsl == null)
      return;
    if (sl.BeforeSetted || prevsl.AfterSetted)
    {
      if (sl.BeforeSetted && prevsl.AfterSetted)
        prevsl.SetAfter(0, updateUI, updateLayout);
      else if (sl.BeforeSetted)
        prevsl.SetAfter(0, updateUI, updateLayout);
      else
        sl.SetBefore(0, updateUI, updateLayout);
    }
    else if ((double) sl.SkipBefore > (double) prevsl.SkipAfter)
      prevsl.SetAfter(0, updateUI, updateLayout);
    else
      sl.SetBefore(0, updateUI, updateLayout);
  }

  /// <summary>Сравнить настройки пропусков между двумя записями документа и расставить согласно приоритетам</summary>
  /// <param name="prevsl">Предыдущая строка</param>
  /// <param name="sl">Текущая строка</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку по страницам</param>
  internal static void CompareSkipLineSettings(SkipLinesStruct prevsl, SkipLinesStruct sl)
  {
    if (sl == null || prevsl == null)
      return;
    if (sl.BeforeSetted || prevsl.AfterSetted)
    {
      if (sl.BeforeSetted && prevsl.AfterSetted)
        prevsl.SkipAfter = 0.0f;
      else if (sl.BeforeSetted)
        prevsl.SkipAfter = 0.0f;
      else
        sl.SkipBefore = 0.0f;
    }
    else if ((double) sl.SkipBefore > (double) prevsl.SkipAfter)
      prevsl.SkipAfter = 0.0f;
    else
      sl.SkipBefore = 0.0f;
  }

  /// <summary>Поиск предыдущего элемента</summary>
  /// <param name="obj">Подраздел или запись документа AVS</param>
  public static SkipLinesStruct GetPrevSkipLinesStruct(object obj)
  {
    switch (obj)
    {
      case AVSRow _:
        AVSRow avsRow = obj as AVSRow;
        if (avsRow.DocNode == null || Chapter.IsStartOfPage(avsRow.DocNode))
          return (SkipLinesStruct) null;
        if (avsRow.Index <= 0)
          return SkipLinesStruct.GetStruct((Chapter) avsRow.Section);
        if (avsRow.Section != null && avsRow.Section.Rows != null && avsRow.Section.Rows.Count > 0 && avsRow.Section.Rows.Count > avsRow.Index)
          return SkipLinesStruct.GetStruct(avsRow.Section.Rows[avsRow.Index - 1]);
        break;
      case Chapter _:
        Chapter chapter = obj as Chapter;
        if (chapter.DocNode == null || Chapter.IsStartOfPage(chapter.DocNode))
          return (SkipLinesStruct) null;
        if (chapter.Parent != null)
        {
          int num = chapter.Parent.Chapters.IndexOf(chapter);
          if (num <= 0)
            return SkipLinesStruct.GetStruct(chapter.Parent);
          object lastDeepElement = SkipLinesStruct.GetLastDeepElement(chapter.Parent.Chapters[num - 1]);
          switch (lastDeepElement)
          {
            case AVSRow _:
              return SkipLinesStruct.GetStruct(lastDeepElement as AVSRow);
            case Chapter _:
              return SkipLinesStruct.GetStruct(lastDeepElement as Chapter);
            default:
              return (SkipLinesStruct) null;
          }
        }
        else
          break;
    }
    return (SkipLinesStruct) null;
  }

  /// <summary>Поиск следующего элемента</summary>
  /// <param name="obj">Подраздел или запись документа AVS</param>
  public static SkipLinesStruct GetNextSkipLinesStruct(object obj)
  {
    if (obj is AVSRow avsRow && avsRow.Section != null)
    {
      if (avsRow.Index > avsRow.Section.Rows.Count - 1)
        return SkipLinesStruct.GetStruct(SkipLinesStruct.GetTopNextElement((Chapter) avsRow.Section));
      if (avsRow.Section.Rows.Count > 0)
        return SkipLinesStruct.GetStruct(avsRow.Section.Rows[avsRow.Index + 1]);
    }
    if (obj is Chapter chapter)
    {
      Chapter ch = chapter is SpecificationSection || chapter.Chapters.Count <= 0 || !chapter.Chapters[0].UseParentDocNode ? chapter : chapter.Chapters[0];
      if (ch != null)
      {
        if (ch is SpecificationSection)
        {
          if ((ch as SpecificationSection).Rows != null && (ch as SpecificationSection).Rows.Count > 0)
            return SkipLinesStruct.GetStruct((ch as SpecificationSection).Rows[0]);
        }
        else if (ch.Chapters.Count > 0)
          return SkipLinesStruct.GetStruct(ch);
        return SkipLinesStruct.GetStruct(SkipLinesStruct.GetTopNextElement(ch));
      }
    }
    return (SkipLinesStruct) null;
  }

  /// <summary>Поиск следующего элемента вверх по дереву</summary>
  /// <param name="ch"></param>
  /// <returns></returns>
  private static object GetTopNextElement(Chapter ch)
  {
    if (ch.Parent == null)
      return (object) null;
    int num = ch.Parent.Chapters.IndexOf(ch);
    return num < ch.Parent.Chapters.Count - 1 ? (object) ch.Parent.Chapters[num + 1] : SkipLinesStruct.GetTopNextElement(ch.Parent);
  }

  /// <summary>Поиск самого глубокого пердыдущего элемента</summary>
  /// <param name="ch"></param>
  /// <returns></returns>
  private static object GetLastDeepElement(Chapter ch)
  {
    while (ch.Chapters.Count > 0)
      ch = ch.Chapters.Last<Chapter>();
    if (!(ch is SpecificationSection))
      return (object) ch;
    return (ch as SpecificationSection).Rows.Count > 0 ? (object) (ch as SpecificationSection).Rows.Last<AVSRow>() : (object) ch;
  }

  public static List<SkipLinesStruct> GetSkipLinesStructs(AVSDocument spec)
  {
    List<SkipLinesStruct> list = new List<SkipLinesStruct>();
    foreach (Chapter rootChapter in spec.rootChapters)
      SkipLinesStruct.GetSkipLinesStructs1(list, rootChapter);
    return list;
  }

  private static void GetSkipLinesStructs1(List<SkipLinesStruct> list, Chapter ch)
  {
    SkipLinesStruct skipLinesStruct1 = new SkipLinesStruct(ch);
    if (ch.DocNode != null)
    {
      switch (ch)
      {
        case ProductVariableDataChapter _:
        case VariableDataChapterFormA _:
        case SpecificationSection _:
          skipLinesStruct1.SkipBefore = ch.DocNode.SkipCellsBefore;
          skipLinesStruct1.SkipAfter = ch.DocNode.SkipCellsAfter;
          break;
        default:
          if (ch.DocNode.Template is TableData template)
          {
            skipLinesStruct1.SkipBefore = template.SkipCellsBefore;
            skipLinesStruct1.SkipAfter = template.SkipCellsAfter;
            break;
          }
          break;
      }
      if (ch is SpecificationSection specificationSection)
      {
        skipLinesStruct1.BeforeSetted = specificationSection.SkipLinesBefore.HasValue;
        skipLinesStruct1.AfterSetted = specificationSection.SkipLinesAfter.HasValue;
      }
    }
    list.Add(skipLinesStruct1);
    foreach (Chapter chapter in ch.Chapters)
      SkipLinesStruct.GetSkipLinesStructs1(list, chapter);
    if (!(ch is SpecificationSection))
      return;
    foreach (AVSRow row in (ch as SpecificationSection).Rows)
    {
      SkipLinesStruct skipLinesStruct2 = new SkipLinesStruct(row);
      if (row.DocNode != null)
      {
        skipLinesStruct2.SkipBefore = row.DocNode.SkipCellsBefore;
        skipLinesStruct2.SkipAfter = row.DocNode.SkipCellsAfter;
        skipLinesStruct2.BeforeSetted = row.SkipLinesBeforeIsOverriden;
        skipLinesStruct2.AfterSetted = row.SkipLinesAfterIsOverriden;
      }
      list.Add(skipLinesStruct2);
    }
  }
}
