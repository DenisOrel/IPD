// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.VariableDataChapterFormV
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.AVS;

/// <summary>Переменные данные исполнений группового документа формы В. Содержит данные в том же виде, что форма Б</summary>
public class VariableDataChapterFormV : Chapter
{
  /// <summary>Конструктор</summary>
  /// <param name="avsDocument">Спецификация владелец подраздела</param>
  public VariableDataChapterFormV(AVSDocument avsDocument)
    : base(avsDocument, true)
  {
    this.ChapterGuid = AVSDocument.ChapterVariableDataVGuid;
    this.Product = new ProductInfo(this.ChapterGuid, -1L, "Переменные данные");
    this.Caption = avsDocument.versionAttributesHelper.VariableDataCaption;
    this.nodeLevel = Chapter.VariableData_TypeName;
  }

  /// <summary>Пронумеровать позиции записей</summary>
  /// <param name="numerationHelper">Вспомогательный класс для нумерации позиций</param>
  public override void RenumberPositions(NumerationHelper numerationHelper)
  {
    numerationHelper.Chapter = (Chapter) this;
    base.RenumberPositions(numerationHelper);
  }

  /// <summary>Удалить пустые разделы</summary>
  /// <param name="keepWithDocNode">Сохранять разделы для которых есть узлы документов</param>
  public override void RemoveEmptySections(bool keepWithDocNode)
  {
    base.RemoveEmptySections(keepWithDocNode);
    if (!this.IsEmpty || keepWithDocNode)
      return;
    for (int index1 = 0; index1 < this.chapters.Count; ++index1)
    {
      for (int index2 = 0; index2 < this.chapters[index1].DocNodes.Count; ++index2)
      {
        this.chapters[index1].DocNodes[index2].UniteTable();
        this.chapters[index1].DocNodes[index2].Remove(false, false);
      }
      this.chapters[index1].DocNodes = new List<TableData>();
    }
  }

  protected override bool IgnoreCreateForEmptyChapters => true;

  /// <summary>Раздел пуст</summary>
  public override bool IsEmpty
  {
    [DebuggerStepThrough] get
    {
      for (int index = 0; index < this.chapters.Count; ++index)
      {
        if (!this.chapters[index].IsEmpty)
          return false;
      }
      return true;
    }
  }

  /// <summary>Получить заголовок для переменных данных без содержимого</summary>
  public string GetEmptyChapterCaption()
  {
    if (!this.IsEmpty)
      return "";
    string str = "Различия исполнений ";
    int num = 0;
    foreach (Chapter chapter in this.chapters)
    {
      if (chapter != null)
        str = str + (num == 0 ? string.Empty : (num == this.chapters.Count - 1 ? " и " : ", ")) + chapter.caption;
      ++num;
    }
    return str + " по сборочному чертежу.";
  }

  /// <summary>Получить шаблон узла документа для этого подраздела</summary>
  public override TableData GetDocNodeTemplate()
  {
    return this.avsDocument.avsDocTableFormBForV_Template != null ? this.avsDocument.avsDocTableFormBForV_Template : base.GetDocNodeTemplate();
  }

  /// <summary>Создать узел документа для этого раздела</summary>
  /// <param name="templateNode">Шаблон</param>
  /// <returns>Узел документа</returns>
  public override TableData CreateDocNode(TableData templateNode)
  {
    PageData pageData = templateNode != null ? templateNode.Page : throw new ArgumentNullException(nameof (templateNode));
    TableData docNode = (TableData) null;
    if (pageData != null)
      docNode = pageData.CloneFromTemplate(true, true).FindFirstNodeFromTemplate_Recursive((DocumentTreeNode) templateNode) as TableData;
    return docNode;
  }

  /// <summary>Вставить страницы созданные для раздела в документ</summary>
  /// <param name="prevPage">Предыдущая страница</param>
  /// <returns>Возвращает последнюю страницу</returns>
  public override PageData InsertPagesInDocument(PageData prevPage)
  {
    for (int index1 = 0; index1 < this.DocNodes.Count; ++index1)
    {
      int index2;
      if (prevPage != null)
      {
        prevPage = prevPage.FindLastPage();
        index2 = prevPage.Index + 1;
      }
      else
        index2 = this.AVSDocument.Document.Nodes.Count;
      if (this.DocNodes[index1].Page != null && this.DocNodes[index1].Page.Parent == null)
      {
        string attributeValue = this.DocNodes[index1].GetAttributeValue(AVSRow.DocAttr_ProductIndex, true);
        this.DocNodes[index1].Page.SetAttributeValue(AVSRow.DocAttr_ProductIndex, attributeValue, false, false, false);
        string str = $"Исполнения {index1 * this.AVSDocument.RowProductCount}...{(index1 + 1) * this.AVSDocument.RowProductCount - 1}";
        this.DocNodes[index1].Page.Id = str;
        this.DocNodes[index1].Page.SetName(str, false, false);
      }
      this.AVSDocument.Document.InsertChildNode(index2, (DocumentTreeNode) this.DocNodes[index1].Page, true, true, false, false, false);
      prevPage = this.DocNodes[index1].Page;
    }
    return prevPage;
  }

  /// <summary>Поля отображаемые в бумажном виде спецификации</summary>
  [Browsable(false)]
  public override List<AvsRowAttributeInfo> DocRowFields
  {
    [DebuggerStepThrough] get
    {
      return this.avsDocument != null ? this.avsDocument.docRowFields_VarFormV : new List<AvsRowAttributeInfo>();
    }
  }

  /// <summary>Структура таблицы формы Б</summary>
  [Browsable(false)]
  public override bool IsFormB
  {
    [DebuggerStepThrough] get => true;
  }

  /// <summary>Индекс сортировки</summary>
  public override long ChapterSortIndex
  {
    [DebuggerStepThrough] get => 30;
  }
}
