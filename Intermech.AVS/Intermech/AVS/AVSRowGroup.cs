// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSRowGroup
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.GridColumns.VirtualTreeList;
using Intermech.AVS.HelperClasses;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Document;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

public class AVSRowGroup : Chapter
{
  private SpecificationSection section;
  public List<AVSRow> Rows = new List<AVSRow>();

  /// <summary>Конструктор</summary>
  public AVSRowGroup() => this.nodeLevel = Chapter.Group_TypeName;

  /// <summary>Конструктор</summary>
  /// <param name="avsDocument">Спецификация владелец подраздела</param>
  public AVSRowGroup(AVSDocument avsDocument)
    : base(avsDocument, false)
  {
    this.avsDocument = avsDocument;
  }

  /// <summary>Группа располагается до или после записей</summary>
  public virtual AVRowGroupPosition GroupPosition
  {
    get => AVRowGroupPosition.AfterRowsGroup;
    set
    {
    }
  }

  public string Name { get; set; }

  public SpecificationSection Section
  {
    get => this.section;
    set => this.section = value;
  }

  public override List<AvsRowAttributeInfo> DocRowFields => this.Section.DocRowFields;

  public override List<AvsRowAttributeInfo> DocRowFields_Exp => this.Section.DocRowFields_Exp;

  public override SkipLinesStruct GetSkipLines(
    SkipLinesSchema skipLinesSchema,
    List<SkipLinesStruct> structs)
  {
    SkipLinesStruct skipLines = base.GetSkipLines(skipLinesSchema, structs);
    if (this.Rows != null)
    {
      for (int index = 0; index < this.Rows.Count; ++index)
      {
        if (this.Rows[index] != null && this.Rows[index].DocNode != null)
          this.Rows[index].GetSkipLines(skipLinesSchema, structs);
      }
    }
    return skipLines;
  }

  /// <summary>Вставить запись в раздел</summary>
  /// <param name="index">Индекс записи</param>
  /// <param name="row">Запись</param>
  public void InsertRow(int index, AVSRow row)
  {
    if (row == null)
      return;
    row.Group = this;
    if (index >= 0)
    {
      if (index <= this.Rows.Count)
        this.Rows.Insert(index, row);
      else
        this.Rows.Add(row);
    }
    for (int index1 = index; index1 < this.Rows.Count; ++index1)
      this.Rows[index1].Index = index1;
  }

  public void RemoveRow(AVSRow row)
  {
    this.Rows.Remove(row);
    row.Index = -1;
    for (int index = 0; index < this.Rows.Count; ++index)
      this.Rows[index].Index = index;
  }

  public override List<IVirtualTreeItem> GetTreeChildren()
  {
    List<IVirtualTreeItem> treeChildren = new List<IVirtualTreeItem>();
    foreach (AVSRow row in this.Rows)
      treeChildren.Add((IVirtualTreeItem) row);
    return treeChildren;
  }

  public override IVirtualTreeItem ParentItem
  {
    get => (IVirtualTreeItem) this.Section;
    set => base.ParentItem = value;
  }

  public override TableData GetDocNodeTemplate()
  {
    return this.avsDocument == null ? (TableData) null : this.avsDocument.addiitionalComplectRowGroupTemplate;
  }

  public override TableData CreateDocNode(TableData templateNode)
  {
    return base.CreateDocNode(templateNode);
  }

  /// <summary>Получить первый индекс исполнения в строке документа</summary>
  /// <param name="node">Узел документа</param>
  /// <returns></returns>
  public int GetFirstProductIndexForDocRow(DocumentTreeNode node)
  {
    int result = 0;
    DocumentTreeNode documentTreeNode = node;
    if (documentTreeNode != null)
    {
      string attributeValue = documentTreeNode.GetAttributeValue(AVSRow.DocAttr_ProductIndex, true);
      if (attributeValue == "" || !int.TryParse(attributeValue, out result))
        result = 0;
    }
    return result;
  }

  public override void UpdateViewNodes(
    SkipLinesSchema skipLinesSchema,
    bool reCreateDocNode,
    bool reCreateListNode,
    bool updateCountB,
    bool createForEmptyChapters,
    bool updateTemplate,
    EmptyRowUpdateMode updateMode)
  {
    if (reCreateDocNode)
    {
      this.DocNodes = new List<TableData>();
      this.DocNodesExp = new List<TableData>();
      this.DocNodesBlocks = (List<DocNodesBlock>) null;
    }
    int num1 = this.avsDocument.IsGridViewMode ? 1 : 0;
    List<int> intList = new List<int>();
    int num2 = -1;
    TableData dataOwner1 = (TableData) null;
    TableData dataOwner2 = (TableData) null;
    DocNodesBlock docNodesBlock = (DocNodesBlock) null;
    List<TableData> sectionDocNodes = new List<TableData>();
    List<TableData> tableDataList1 = new List<TableData>();
    List<DocNodesBlock> docNodesBlockList = new List<DocNodesBlock>();
    int index1 = 0;
    int index2 = 0;
    List<TableData> tableDataList2 = new List<TableData>();
    TableData tableData = (TableData) null;
    int num3 = this.UseParentDocNode ? 1 : 0;
    int key = 0;
    TableData templateNode = num3 == 0 ? this.GetDocNodeTemplate() : this.parent.GetDocNodeTemplate();
    TableData docNodeExpTemplate = this.IsExportSP ? this.GetDocNodeExpTemplate() : (TableData) null;
    Dictionary<int, TableData> dictionary = new Dictionary<int, TableData>();
    if (!this.avsDocument.IsExportSP)
    {
      if (this.HasDocNodes)
      {
        for (int index3 = 0; index3 < this.docNodes.Count; ++index3)
        {
          key = Chapter.GetFirstProductIndexForDocChapter((DocumentTreeNode) this.docNodes[index3]);
          if (!dictionary.ContainsKey(key))
          {
            dictionary.Add(key, this.docNodes[index3]);
            sectionDocNodes.Add(this.docNodes[index3]);
            tableDataList2.Add((TableData) null);
            intList.Add(-1);
          }
          else
          {
            dataOwner1 = this.docNodes[index3];
            this.docNodes.RemoveAt(index3);
            --index3;
            dataOwner1.Remove(false, false);
          }
        }
      }
    }
    else
    {
      if (this.HasDocNodes)
      {
        for (int index4 = 0; index4 < this.docNodes.Count; ++index4)
        {
          sectionDocNodes.Add(this.docNodes[index4]);
          tableDataList2.Add((TableData) null);
          intList.Add(-1);
        }
      }
      if (this.HasDocNodesExp)
      {
        tableDataList1.AddRange((IEnumerable<TableData>) this.DocNodesExp);
        tableData = (TableData) null;
        num2 = -1;
      }
    }
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    List<AvsRowAttributeInfo> docRowFields = this.DocRowFields;
    for (int index5 = 0; index5 < this.Rows.Count; ++index5)
    {
      bool flag4 = false;
      AVSRow row = this.Rows[index5];
      row.UpdateDocRow((TableData) null, docRowFields, reCreateDocNode, updateCountB, updateTemplate, updateMode);
      bool hasDocNodes = row.HasDocNodes;
      bool hasDocNodeExp = row.HasDocNodeExp;
      if (this.avsDocument.IsExportSP)
      {
        if (index1 < sectionDocNodes.Count)
          dataOwner1 = sectionDocNodes[index1];
        if (dataOwner1 != null && !this.TopTableIsSuitable_Normal(dataOwner1, hasDocNodes, hasDocNodeExp))
        {
          ++index1;
          docNodesBlock = (DocNodesBlock) null;
          dataOwner1 = index1 >= sectionDocNodes.Count ? (TableData) null : sectionDocNodes[index1];
          if (dataOwner1 != null && !this.TopTableIsSuitable_Normal(dataOwner1, hasDocNodes, hasDocNodeExp))
            dataOwner1 = (TableData) null;
        }
        if (index2 < tableDataList1.Count)
          dataOwner2 = tableDataList1[index2];
        if (dataOwner2 != null && !this.TopTableIsSuitable_Export(dataOwner2, hasDocNodes, hasDocNodeExp))
        {
          ++index2;
          docNodesBlock = (DocNodesBlock) null;
          dataOwner2 = index2 >= tableDataList1.Count ? (TableData) null : tableDataList1[index2];
          if (dataOwner2 != null && !this.TopTableIsSuitable_Export(dataOwner2, hasDocNodes, hasDocNodeExp))
            dataOwner2 = (TableData) null;
        }
      }
      if (row.HasDocNodes)
      {
        for (int index6 = 0; index6 < row.DocNodes.Count; ++index6)
        {
          bool flag5 = row.DocNodes[index6].Parent == null;
          flag4 |= flag5;
          if (!this.avsDocument.IsExportSP)
          {
            key = row.GetFirstProductIndexForDocRow((DocumentTreeNode) row.DocNodes[index6]);
            dictionary.TryGetValue(key, out dataOwner1);
            if (dataOwner1 != null)
            {
              if (sectionDocNodes.Count == 1)
              {
                index1 = 0;
              }
              else
              {
                for (int index7 = 0; index7 < sectionDocNodes.Count; ++index7)
                {
                  if (sectionDocNodes[index7].FindFirstTable() == dataOwner1)
                  {
                    index1 = index7;
                    break;
                  }
                }
              }
            }
          }
          if (dataOwner1 == null)
          {
            dataOwner1 = this.CreateDocNode(templateNode);
            flag2 = true;
            if (this.avsDocument.IsExportSP)
            {
              if (index1 > 0)
              {
                this.HideChapterHeaderRow(dataOwner1);
                dataOwner1.SetFromNewPage(true, false, false);
              }
            }
            else
            {
              dictionary.Add(key, dataOwner1);
              index1 = 0;
              while (index1 < sectionDocNodes.Count && Chapter.GetFirstProductIndexForDocChapter((DocumentTreeNode) sectionDocNodes[index1]) <= key)
                ++index1;
              if (key != 0)
                dataOwner1.SetAttributeValue(AVSRow.DocAttr_ProductIndex, key.ToString(), false, false, false);
              else
                dataOwner1.RemoveAttribute(AVSRow.DocAttr_ProductIndex, false, false);
            }
            sectionDocNodes.Insert(index1, dataOwner1);
            intList.Add(-1);
            tableDataList2.Insert(index1, (TableData) null);
          }
          if (tableDataList2[index1] == null)
          {
            intList[index1] = dataOwner1.FindDataPositionInFlow(0, out dataOwner1);
          }
          else
          {
            dataOwner1 = tableDataList2[index1].Parent as TableData;
            if (intList[index1] == -1)
              intList[index1] = dataOwner1.FindNextDataPositionInFlow(tableDataList2[index1].Index, out dataOwner1);
          }
          if (intList[index1] < 0)
          {
            LogManager.AddLine($"AVS. SpecificationSection.UpdateViewNodes #1. row id \"{row.DocNodes[index6].Id}\"; {$"row index {index1}; node index {intList[index1]}; "}section node id \"{dataOwner1.Id}\"", true);
            intList[index1] = 0;
          }
          else if (intList[index1] > dataOwner1.Nodes.Count)
          {
            LogManager.AddLine($"AVS. SpecificationSection.UpdateViewNodes #2. row id \"{row.DocNodes[index6].Id}\"; {$"row index {index1}; node index {intList[index1]}; "}{$"nodes Count {dataOwner1.Nodes.Count}; section node id \"{dataOwner1.Id}\""}", true);
            intList[index1] = dataOwner1.Nodes.Count;
          }
          if (flag5 && this.AVSDocument.CollectChangeEvents)
            this.avsDocument.AvsRowEventMessageViewer.AddEvent(row, new AvsRowEventMessage(AVSEventType.AddRow));
          dataOwner1.InsertChildNode(intList[index1]++, (DocumentTreeNode) row.DocNodes[index6], row.DocNodes[index6].Parent != null, true, false, false, false);
          row.DocNodes[index6].SetVisible(true, false, false, false, true);
          sectionDocNodes[index1] = dataOwner1;
          if (!flag5)
            intList[index1] = -1;
          tableDataList2[index1] = row.DocNodes[index6];
        }
      }
      if (row.HasDocNodeExp)
      {
        bool flag6 = row.DocNodeExp.Parent == null;
        flag1 = flag4 | flag6;
        if (dataOwner2 == null)
        {
          dataOwner2 = this.CreateDocNode(docNodeExpTemplate);
          flag3 = true;
          tableDataList1.Insert(index2, dataOwner2);
          num2 = -1;
          tableData = (TableData) null;
          if (index2 > 0)
          {
            this.HideChapterHeaderRow(dataOwner2);
            dataOwner2.SetFromNewPage(true, false, false);
          }
        }
        if (tableData == null)
        {
          num2 = dataOwner2.FindDataPositionInFlow(0, out dataOwner2);
        }
        else
        {
          dataOwner2 = tableData.Parent as TableData;
          if (num2 == -1)
            num2 = dataOwner2.FindNextDataPositionInFlow(tableData.Index, out dataOwner2);
        }
        if (num2 < 0)
        {
          LogManager.AddLine($"AVS. SpecificationSection.UpdateViewNodes #3. row id \"{row.DocNodeExp.Id}\"; node index {num2}; section node id \"{dataOwner2.Id}\"", true);
          num2 = 0;
        }
        else if (num2 > dataOwner2.Nodes.Count)
        {
          LogManager.AddLine($"AVS. SpecificationSection.UpdateViewNodes #4. row id \"{row.DocNodeExp.Id}\"; node index {num2}; nodes Count {dataOwner2.Nodes.Count}; section node id \"{dataOwner2.Id}\"", true);
          num2 = dataOwner2.Nodes.Count;
        }
        dataOwner2.InsertChildNode(num2++, (DocumentTreeNode) row.DocNodeExp, row.DocNodeExp.Parent != null, true, false, false, false);
        tableDataList1[index2] = dataOwner2;
        if (!flag6)
          num2 = -1;
        tableData = row.DocNodeExp;
      }
      if (docNodesBlock == null && this.avsDocument.IsExportSP)
      {
        docNodesBlock = new DocNodesBlock(dataOwner1, dataOwner2);
        docNodesBlockList.Add(docNodesBlock);
      }
    }
    for (int index8 = 0; index8 < sectionDocNodes.Count; ++index8)
      sectionDocNodes[index8] = sectionDocNodes[index8].FindFirstTable();
    for (int index9 = 0; index9 < tableDataList1.Count; ++index9)
      tableDataList1[index9] = tableDataList1[index9].FindFirstTable();
    if (!this.IsExportSP)
      this.RemoveEmptySectionDocNodes(sectionDocNodes, updateMode);
    if (((this.avsDocument.ReadOnly ? 0 : (sectionDocNodes.Count == 0 ? 1 : 0)) & (createForEmptyChapters ? 1 : 0)) != 0)
    {
      for (int index10 = 0; index10 * this.avsDocument.RowProductCount < this.avsDocument.productsInfo.Count && (index10 <= 0 || this.IsFormB); ++index10)
      {
        flag2 = true;
        dataOwner1 = this.CreateDocNode(templateNode);
        if (index10 > 0)
          dataOwner1.SetAttributeValue(AVSRow.DocAttr_ProductIndex, (index10 * this.avsDocument.RowProductCount).ToString(), false, false, false);
        else
          dataOwner1.RemoveAttribute(AVSRow.DocAttr_ProductIndex, false, false);
        sectionDocNodes.Add(dataOwner1);
      }
    }
    if (flag2)
      this.DocNodes = sectionDocNodes;
    if (flag3)
      this.DocNodesExp = tableDataList1;
    this.DocNodesBlocks = docNodesBlockList;
  }

  public void GetAllRows(bool onlyRelations, bool onlyObjects, List<AVSRow> rowList)
  {
    if (!onlyRelations && !onlyObjects)
    {
      rowList.AddRange((IEnumerable<AVSRow>) this.Rows);
    }
    else
    {
      for (int index = 0; index < this.Rows.Count; ++index)
      {
        if (onlyRelations)
        {
          if (this.Rows[index].HasRelation)
            rowList.Add(this.Rows[index]);
        }
        else if (!onlyObjects || this.Rows[index].ObjectId != -1L)
          rowList.Add(this.Rows[index]);
      }
    }
  }

  public override void GetAllRowsList(
    bool withRelationsOnly,
    bool withObjectsOnly,
    List<AVSRow> rowList)
  {
    rowList = new List<AVSRow>(this.GetRows(withRelationsOnly, withObjectsOnly));
  }

  public override IEnumerable<AVSRow> GetRows(bool withRelationsOnly = false, bool withObjectsOnly = false)
  {
    foreach (Chapter chapter in this.chapters)
    {
      foreach (AVSRow row in chapter.GetRows(withRelationsOnly, withObjectsOnly))
        yield return row;
    }
    foreach (AVSRow row in this.Rows)
    {
      if (((!withRelationsOnly ? 1 : (row.HasRelation ? 1 : 0)) | (!withObjectsOnly ? (true ? 1 : 0) : (row.HasObject ? 1 : 0))) != 0)
        yield return row;
    }
  }

  internal bool RemoveEmptySectionDocNodes(
    List<TableData> sectionDocNodes,
    EmptyRowUpdateMode updateMode)
  {
    bool flag1 = false;
    for (int index = sectionDocNodes.Count - 1; index >= 0; --index)
    {
      int indexForDocChapter = Chapter.GetFirstProductIndexForDocChapter((DocumentTreeNode) sectionDocNodes[index]);
      bool flag2 = sectionDocNodes[index].CalcDataCellCount() == 0;
      bool flag3 = updateMode == EmptyRowUpdateMode.Delete & flag2;
      int count = this.avsDocument.productsInfo.Count;
      if (indexForDocChapter >= count | flag3)
      {
        this.DisconnectDocNode(sectionDocNodes[index], false);
        sectionDocNodes[index].UniteTable();
        sectionDocNodes[index].Remove(false, false);
        sectionDocNodes.RemoveAt(index);
        flag1 = true;
      }
    }
    return flag1;
  }
}
