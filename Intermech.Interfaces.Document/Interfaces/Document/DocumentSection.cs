// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DocumentSection
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Раздел документа</summary>
[Serializable]
public class DocumentSection : VisualNode, IDocumentElement
{
  /// <summary>Имя типа элемента</summary>
  public static string ElementTypeName = LocalizationHolder.rm.GetString("Interfaces.Document_151");

  /// <summary>Создать пустой экземпляр класса без инициализации полей</summary>
  /// <param name="element">Ссылка на новый экземпляр класса, элемент создается
  /// если на входе element равен null, иначе, считается, что он был создан
  /// в перекрытом методе наследника</param>
  public override void CreateEmptyElement(ref DocumentTreeNode element)
  {
    if (element == null)
      element = (DocumentTreeNode) new DocumentSection(false);
    base.CreateEmptyElement(ref element);
  }

  /// <summary>Создать пустой экземпляр класса с инициализацией только самых неоходимых полей.
  /// Используется в словаре кострукторов.</summary>
  public static object EmptyConstructor() => (object) new DocumentSection();

  /// <summary>Инициализировать поля объекта</summary>
  protected override void InitFields()
  {
    base.InitFields();
    this.nodes = new DocumentTreeNodeCollection((DocumentTreeNode) this);
    this.cloneByTemplateWithParent = false;
    this.SetPropertiesChangedFlag(false, false, false, false, false);
    this.TreeStructureChangedFlag = false;
  }

  /// <summary>Конструктор</summary>
  /// <param name="initFields">Вызвать метод InitFields()</param>
  public DocumentSection(bool initFields)
    : base(initFields)
  {
  }

  /// <summary>Конструктор необходимый для десериализации (ISerializable)</summary>
  /// <param name="info">Заполненный данными SerializationInfo</param>
  /// <param name="context">Контекст десериализации</param>
  protected DocumentSection(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  /// <summary>Конструктор</summary>
  public DocumentSection(DocumentTreeNode parent) => this.SetParent(parent, false, false);

  /// <summary>Конструктор</summary>
  public DocumentSection()
  {
  }

  /// <summary>Наименование типа</summary>
  [ReadOnly(true)]
  public override string NodeTypeCaption
  {
    [DebuggerStepThrough] get => DocumentSection.ElementTypeName;
    set => DocumentSection.ElementTypeName = value;
  }

  /// <summary>Получить подпись элемента по умолчанию</summary>
  public override string GetDefautCaption()
  {
    string name = this.Name;
    return name == null || name == "" ? string.Format(this.NodeTypeCaption + " {0}", (object) (this.Index + 1)) : string.Format(this.NodeTypeCaption + " {0}", (object) name);
  }

  /// <summary>Добавить и связать объекты интерфейса пользователя</summary>
  /// <param name="child">Дочерний узел</param>
  public override void AddChildUI(DocumentTreeNode child, bool createUI)
  {
    if (this.OwnerDocument != null)
      this.OwnerDocument.AddChildUI(child, createUI);
    else
      base.AddChildUI(child, createUI);
  }

  /// <summary>Сгенерировать метафайлы для страниц</summary>
  /// <param name="pages">Список номеров страниц (индекс в nodes). Если значение null, то все страницы</param>
  /// <param name="baseFilename">Базовое имени файла. К нему будет добавляться значок '#' и номер страницы</param>
  public void GeneratePageMetafiles(int[] pages, string baseFilename)
  {
    if (pages == null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        if (this.nodes[index] is PageData node2)
          node2.CreatePageMetafile($"{baseFilename}#{node2.PageNumber.ToString()}.wmf");
        else if (this.nodes[index] is DocumentSection node1)
          node1.GeneratePageMetafiles(pages, baseFilename);
      }
    }
    else
    {
      for (int index = 0; index < pages.Length; ++index)
      {
        if (this.nodes[pages[index]] is PageData node4)
          node4.CreatePageMetafile($"{baseFilename}#{pages[index].ToString()}.wmf");
        else if (this.nodes[pages[index]] is DocumentSection node3)
          node3.GeneratePageMetafiles(pages, baseFilename);
      }
    }
  }

  /// <summary>Запрет на изменение пользователем структуры узла</summary>
  public override bool ReadOnlyStructure
  {
    [DebuggerStepThrough] get
    {
      ImDocumentData ownerDocument = this.OwnerDocument;
      return ownerDocument != null && ownerDocument.ReadOnlyStructure;
    }
  }

  /// <summary>Герерирует событие ChildNodeRemoved</summary>
  public override void OnChildNodeRemoved(ChildNode_EventArgs e)
  {
    base.OnChildNodeRemoved(e);
    if (!(e.Child is PageData))
      return;
    this.OwnerDocument?.OnChildNodeRemoved(e);
  }

  /// <summary>Получить первый шаблон страницы</summary>
  /// <returns>Первый шаблон страницы</returns>
  public virtual PageData GetFirstPageTemplate()
  {
    if (!this.IsTemplate)
      return this.Template is DocumentSection template ? template.GetFirstPageTemplate() : (PageData) null;
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is PageData node1)
        return node1;
      if (this.nodes[index] is DocumentSection node2)
        node2.GetFirstPageTemplate();
    }
    return (PageData) null;
  }

  /// <summary>Создать певую страницу документа</summary>
  public PageData CreateFirstPage(bool findExistingPage)
  {
    if (this.nodes.Count == 0)
    {
      PageData firstPageTemplate = this.GetFirstPageTemplate();
      if (firstPageTemplate != null)
        this.InsertChildNode(0, firstPageTemplate.CloneFromTemplate(true, true), false, true, true, true, false);
      else if (this.OwnerDocument != null)
        return this.OwnerDocument.NewPage((DocumentTreeNode) this);
    }
    else
    {
      if (findExistingPage)
      {
        PageData firstPage = ImDocumentData.GetFirstPage((DocumentTreeNode) this);
        if (firstPage != null)
          return firstPage;
      }
      if (this.nodes[0] is DocumentSection)
        return this.CreateFirstPage(false);
    }
    return (PageData) null;
  }

  /// <summary>Документ владелец</summary>
  public override ImDocumentData OwnerDocument
  {
    [DebuggerStepThrough] get
    {
      DocumentTreeNode documentTreeNode = (DocumentTreeNode) this;
      while (documentTreeNode.Parent != null && !(documentTreeNode.Parent is ImDocumentData))
        documentTreeNode = documentTreeNode.Parent;
      return documentTreeNode.Parent as ImDocumentData;
    }
  }

  /// <summary>Документ владеющий шаблоном документа,
  /// которому принадлежит элемент.
  /// Если элемент не принадлежит шаблону, то null</summary>
  public ImDocumentData DocumentTemplateOwner
  {
    [DebuggerStepThrough] get
    {
      ImDocumentData ownerDocument = this.OwnerDocument;
      return ownerDocument != null && ownerDocument.IsTemplate ? ownerDocument.TemplateOwner : (ImDocumentData) null;
    }
  }

  /// <summary>Узел является шаблоном</summary>
  public override bool IsTemplate
  {
    get
    {
      ImDocumentData ownerDocument = this.OwnerDocument;
      return ownerDocument != null && ownerDocument.IsTemplate;
    }
  }

  /// <summary>Корень дерева в котором должен находиться шаблон этого узла</summary>
  public override DocumentTreeNode TemplateRoot => this.OwnerDocument?.TemplateRoot;

  /// <summary>Найти шаблон этого узла по идентификатору templateId</summary>
  /// <param name="templateId">Идентификатор шаблона</param>
  /// <returns>Шаблон узла</returns>
  public override DocumentTreeNode FindTemplate(string templateId)
  {
    ImDocumentData ownerDocument = this.OwnerDocument;
    return ownerDocument != null && ownerDocument.Template != null ? ownerDocument.Template.FindNode(templateId) : (DocumentTreeNode) null;
  }

  /// <summary>Получить список узлов привязки</summary>
  /// <param name="originalPoint">Оригинальная точка</param>
  /// <param name="snapSize">Размер области привязки</param>
  /// <param name="snapPointList">Список полученных точек</param>
  /// <param name="excludeNode">Узел который должен исключаться из рассмотрения</param>
  public override void GetSnapPoints(
    PointF originalPoint,
    float snapSize,
    List<SnapPoint> snapPointList,
    VisualNode excludeNode)
  {
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is VisualNode node)
        node.GetSnapPoints(originalPoint, snapSize, snapPointList, excludeNode);
    }
  }
}
