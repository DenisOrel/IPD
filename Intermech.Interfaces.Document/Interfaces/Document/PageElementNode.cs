// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.PageElementNode
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Элемент страницы.
/// Базовый класс для элементов страницы.</summary>
[Serializable]
public abstract class PageElementNode : VisualNode, IDocumentElement
{
  /// <summary>Цвет переднего плана по умолчанию</summary>
  public static Color DefaultForeColor = Color.Black;
  /// <summary>Цвет фона по умолчанию</summary>
  public static Color DefaultBackColor = Color.White;
  /// <summary>Толщина линии по умолчанию</summary>
  public static readonly float DefaultLineWidth = 0.1f;
  /// <summary>Словарь методов для чтения полей из XML</summary>
  protected new static Dictionary<string, ReadFieldFromXmlDelegate> ReadFieldsDict = (Dictionary<string, ReadFieldFromXmlDelegate>) null;
  private bool geometryChangingBlocked = true;
  private bool readOnly;
  private bool transparent = true;
  private ShowOnPageOnly showOnPageOnly = ShowOnPageOnly.All;
  [NonSerialized]
  private bool inPlaceEditorActive;
  /// <summary>Страница владелец</summary>
  [NonSerialized]
  protected PageData page;

  /// <summary>Конструктор необходимый для десериализации (ISerializable)</summary>
  /// <param name="info">Заполненный данными SerializationInfo</param>
  /// <param name="context">Контекст десериализации</param>
  protected PageElementNode(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.TemplateGeometryOverrided = false;
  }

  /// <summary>Конструктор</summary>
  public PageElementNode() => this.TemplateGeometryOverrided = false;

  /// <summary>Конструктор</summary>
  /// <param name="initFields">Вызывать метод инициализации полей InitFields()</param>
  public PageElementNode(bool initFields)
    : base(initFields)
  {
  }

  /// <summary>Статический конструктор</summary>
  static PageElementNode() => PageElementNode.InitReadFieldDict();

  /// <summary>Восстановить идентификаторы потоков</summary>
  public virtual void RestoreFlowId()
  {
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is PageElementNode node)
        node.RestoreFlowId();
    }
  }

  /// <summary>Документ владеющий этим узлом</summary>
  [Browsable(false)]
  public override ImDocumentData OwnerDocument
  {
    [DebuggerStepThrough] get
    {
      return this.page != null ? this.page.OwnerDocument : (ImDocumentData) null;
    }
  }

  /// <summary>Документ, который использует данный документ как шаблон (=OwnerDocument.TemplateOwner)</summary>
  [Browsable(false)]
  public virtual ImDocumentData DocumentTemplateOwner
  {
    [DebuggerStepThrough] get
    {
      ImDocumentData ownerDocument = this.OwnerDocument;
      return ownerDocument != null && ownerDocument.IsTemplate ? ownerDocument.TemplateOwner : (ImDocumentData) null;
    }
  }

  /// <summary>Корень дерева документа в котором находится этот узел.
  /// <remarks>Документ который владеет этим узлом. Если узел не пренадлежит документу, то null</remarks>
  /// </summary>
  public override ImDocumentData GetDocTreeRoot() => this.OwnerDocument;

  /// <summary>Получить корень дерева в котором находится этот узел
  /// <remarks>Корнем считается первый узел без родителя вверх по иерархии</remarks>
  /// </summary>
  public override DocumentTreeNode GetTreeRoot()
  {
    DocumentTreeNode treeRoot = (DocumentTreeNode) this.OwnerDocument ?? (DocumentTreeNode) this;
    while (treeRoot.Parent != null)
      treeRoot = treeRoot.Parent;
    return treeRoot;
  }

  /// <summary>Проверить можно ли добавить заданный элемент в этот элемент</summary>
  /// <param name="child">Вставляемый элемент</param>
  /// <returns>Возвращает true, если заданный элемент можно добавить в этот элемент</returns>
  public override bool CanAddChildElement(DocumentTreeNode child)
  {
    return this.CanAddChildElement(child.GetType());
  }

  /// <summary>Проверить можно ли добавить элемент заданного типа в этот элемент</summary>
  /// <param name="type">Тип вставляемого элемента</param>
  /// <returns>Возвращает true, если элемент заданного типа можно добавить в этот элемент</returns>
  public override bool CanAddChildElement(Type type)
  {
    return typeof (PageElementNode).IsAssignableFrom(type);
  }

  /// <summary>Герерирует событие Changed</summary>
  public override void OnChanged(Changed_EventArgs e)
  {
    if (this.IsChanging || this.IsVirtualNode)
      return;
    ImDocumentData ownerDocument = this.OwnerDocument;
    if (ownerDocument != null)
    {
      if (e != null)
      {
        if (!ownerDocument.Modified)
          ownerDocument.SaveModificationDate = e.SaveModificationDate;
        else if (!e.SaveModificationDate)
          ownerDocument.SaveModificationDate = false;
      }
      ownerDocument.Modified = true;
    }
    base.OnChanged(e);
  }

  /// <summary>Присвоить значение свойству Parent</summary>
  /// <param name="value">Новое значение Parent</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void AssignParent(
    DocumentTreeNode value,
    bool updateUI,
    bool updateLayout,
    bool isLoading)
  {
    if (this.parent == value)
      return;
    if (this.isVirtualNode)
    {
      base.AssignParent(value, updateUI, updateLayout, isLoading);
    }
    else
    {
      int num = isLoading || !updateUI ? 1 : (!this.SuspendedUpdateUIGeometryFlag ? 0 : (this.SuspendedRefreshUIFlag ? 1 : 0));
      if (num == 0)
        this.SuspendUpdateGeometryRefreshUI();
      base.AssignParent(value, false, updateLayout, isLoading);
      PageData pageData = (PageData) null;
      if (this.parent != null)
        pageData = !(this.parent is PageElementNode parent) ? this.parent as PageData : parent.page;
      if (pageData != this.page)
        this.AssignPage(pageData, false, updateLayout);
      if (num != 0)
        return;
      this.ResumeUpdateRefreshUI(updateUI, updateUI);
    }
  }

  /// <summary>Страница, которая владеет элементом</summary>
  [Browsable(false)]
  public virtual PageData Page
  {
    [DebuggerStepThrough] get => this.page;
  }

  /// <summary>Присвоить значение свойству Page</summary>
  /// <param name="value">Новое значение Page</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public virtual void AssignPage(PageData value, bool updateUI, bool updateLayout)
  {
    if (this.page == value)
      return;
    if (this.isVirtualNode)
    {
      this.page = value;
    }
    else
    {
      bool flag = !updateUI || this.SuspendedUpdateUIGeometryFlag && this.SuspendedRefreshUIFlag;
      if (!flag)
        this.SuspendUpdateGeometryRefreshUI();
      this.page = value;
      if (this.nodes != null)
      {
        for (int index = 0; index < this.nodes.Count; ++index)
        {
          if (this.nodes[index] is PageElementNode node)
            node.AssignPage(this.page, false, false);
        }
      }
      this.needUpdateUIGeometry = true;
      if (flag)
        return;
      this.ResumeUpdateRefreshUI(updateUI, updateLayout);
    }
  }

  /// <summary>Обновить идентификаторы в ссылках на данные по установленным связям с данными</summary>
  internal virtual void UpdateDataIdCacheLinks()
  {
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is PageElementNode node)
        node.UpdateDataIdCacheLinks();
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

  /// <summary>Элемент принадлежит библиотеке формул</summary>
  public override bool IsFormulaLib
  {
    get => this.page != null ? this.page.IsFormulaLib : base.IsFormulaLib;
  }

  /// <summary>Показывать на экране, что узел выбран</summary>
  public override bool ShowSelected
  {
    [DebuggerStepThrough] get => base.ShowSelected && !this.InPlaceEditorActive;
  }

  /// <summary>Отфильтровать свойства элемента для показа в PopertyGrid</summary>
  /// <param name="properties">Список PropertyDescriptor свойств</param>
  /// <param name="attributes">Массив атрибутов элемента</param>
  protected override void FilterProperties(IDictionary properties, Attribute[] attributes)
  {
    base.FilterProperties(properties, attributes);
    if (!ImDocumentData.ShowDebugInfo)
      this.RemoveProperty(properties, "TemplateGeometryOverrided");
    if (this.TemplateId != null)
    {
      if (properties[(object) "ReadOnly"] is CustomPropertyDescriptor property1)
        property1.SetIsReadOnly(true);
      if (properties[(object) "Transparent"] is CustomPropertyDescriptor property2)
        property2.SetIsReadOnly(true);
    }
    if (!this.HasTemplate())
      return;
    this.RemoveProperty(properties, "GeometryChangingBlocked_ForUser");
  }

  /// <summary>Создать объект Graphics для контрола страницы</summary>
  /// <returns>Graphics</returns>
  public virtual Graphics CreateDefaultGraphics() => (Graphics) null;

  /// <summary>Прозрачный фон</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_189")]
  [CustomDescription("Attribute.Interfaces.Document_190")]
  [CustomCategory("Attribute.Interfaces.Document_191")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public virtual bool Transparent
  {
    get => this.transparent;
    set => this.AssignTransparent(value, true);
  }

  /// <summary>Присвоить новое значение свойству Transparent</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  public void AssignTransparent(bool value, bool updateUI)
  {
    if (this.transparent == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "Transparent", (object) this.Transparent, (object) value);
    this.transparent = value;
    this.TemplateGeometryOverrided = true;
    this.overrideFlags3 |= OverrideFlags3.Transparent;
    if (updateUI)
      this.RefreshUI();
    this.OnChanged(new Changed_EventArgs());
    this.SetPropertiesChangedFlag(true, true, false, updateUI, false);
  }

  /// <summary>Рекурсивно установить значение GeometryChangingBlocked</summary>
  /// <param name="value">Значение GeometryChangingBlocked</param>
  public virtual void SetGeometryChangingBlockedRecursive(bool value)
  {
    this.geometryChangingBlocked = value;
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is PageElementNode node)
        node.SetGeometryChangingBlockedRecursive(value);
    }
  }

  /// <summary>Обновить изображение на экране</summary>
  /// <param name="clipRegion">Область в которой нужно обновить изображение</param>
  public virtual void RefreshUI(Rectangle clipRegion)
  {
    if (this.SuspendedRefreshUIFlag || this.page == null)
      return;
    this.InvalidateUI(clipRegion);
    this.page.UpdateInvalidatedRegion();
  }

  public override bool SuspendedRefreshUIFlag
  {
    get => this.page != null ? this.page.SuspendedRefreshUIFlag : base.SuspendedRefreshUIFlag;
    set
    {
      if (this.page == null)
        return;
      this.page.SuspendedRefreshUIFlag = value;
    }
  }

  public override bool SuspendedUpdateUIGeometryFlag
  {
    get
    {
      return this.page != null ? this.page.SuspendedUpdateUIGeometryFlag : base.SuspendedUpdateUIGeometryFlag;
    }
    set
    {
      if (this.page == null)
        return;
      this.page.SuspendedUpdateUIGeometryFlag = value;
    }
  }

  /// <summary>Заблокировать изменение геометрии через интерфейс пользователя.
  /// Свойство для PropertyGrid</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_192")]
  [CustomDescription("Attribute.Interfaces.Document_193")]
  [CustomCategory("Attribute.Interfaces.Document_194")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public virtual bool GeometryChangingBlocked_ForUser
  {
    [DebuggerStepThrough] get => this.geometryChangingBlocked;
    set
    {
      if (this.geometryChangingBlocked == value)
        return;
      if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
        this.OwnerDocument.UndoManager.CreateUndo((object) this, nameof (GeometryChangingBlocked_ForUser), (object) this.GeometryChangingBlocked_ForUser, (object) value);
      this.SetGeometryChangingBlockedRecursive(value);
      this.SetPropertiesChangedFlag(true, true, false, true, true);
      this.OnChanged(new Changed_EventArgs());
    }
  }

  /// <summary>Заблокировать изменение геометрии через интерфейс пользователя. Не работает для шаблона</summary>
  [Browsable(false)]
  public virtual bool GeometryChangingBlocked
  {
    get => this.TemplateId != null && this.geometryChangingBlocked;
    set => this.geometryChangingBlocked = value;
  }

  /// <summary>Пользователь не может редактировать данные элемента</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_195")]
  [CustomDescription("Attribute.Interfaces.Document_196")]
  [CustomCategory("Attribute.Interfaces.Document_197")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public virtual bool ReadOnly
  {
    [DebuggerStepThrough] get => this.readOnly;
    set => this.SetReadOnly(value, true);
  }

  /// <summary>Установить новое значение свойства ReadOnly</summary>
  /// <param name="value">Новое значение</param>
  /// <param name="UpdateUI">Обновить интерфейс</param>
  public void SetReadOnly(bool value, bool updateUI)
  {
    if (this.readOnly == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "ReadOnly", (object) this.ReadOnly, (object) value);
    this.readOnly = value;
    this.overrideFlags |= OverrideFlags.ReadOnly;
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateUI);
    if (!updateUI)
      return;
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Присвоить новое значение свойства ReadOnly</summary>
  /// <param name="value">Новое значение</param>
  public virtual void AssignReadOnly(bool value)
  {
    if (this.readOnly == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "ReadOnly", (object) this.ReadOnly, (object) value);
    this.readOnly = value;
    if (this.InPlaceEditorActive && this.readOnly)
      this.DeactivateInPlaceEditor();
    this.SetPropertiesChangedFlag(true, true, false, true, true);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Текущее состояние только для чтения. Может меняться</summary>
  [Browsable(false)]
  [Category("Debug")]
  public virtual bool ReadOnlyNow
  {
    [DebuggerStepThrough] get => this.ReadOnly;
  }

  /// <summary>Редактор на месте</summary>
  [Browsable(false)]
  public virtual bool IsInPlaceEditor
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Можно активировать редактирование по месту</summary>
  [Browsable(false)]
  public virtual bool CanActivateInPlaceEditor
  {
    [DebuggerStepThrough] get => this.IsInPlaceEditor;
  }

  /// <summary>Активизировать редактор на месте</summary>
  public virtual void ActivateInPlaceEditor()
  {
    if (!this.IsInPlaceEditor)
      this.inPlaceEditorActive = false;
    else
      this.inPlaceEditorActive = true;
  }

  /// <summary>Деактивировать редактор на месте</summary>
  public virtual void DeactivateInPlaceEditor()
  {
    this.inPlaceEditorActive = false;
    this.RefreshUI();
  }

  /// <summary>Редактор для редактирования по месту активен</summary>
  [Browsable(false)]
  public virtual bool InPlaceEditorActive
  {
    [DebuggerStepThrough] get => this.inPlaceEditorActive;
  }

  /// <summary>
  /// Показывать на странице
  /// <remarks>
  /// Управление отображением и скрытием элемента на первой, следующей и последней страницах документа. Работает в контексте логической цепочки страниц связанных с переносом данных
  /// </remarks>
  /// </summary>
  [Browsable(false)]
  public virtual ShowOnPageOnly ShowOnPageOnly => this.showOnPageOnly;

  internal void SetShowOnPageOnly(ShowOnPageOnly value, bool update)
  {
    if (this.showOnPageOnly == value)
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "Transparent", (object) this.Transparent, (object) value);
    this.showOnPageOnly = value;
    this.OnChanged(new Changed_EventArgs());
    this.SetPropertiesChangedFlag(true, true, false, update, update);
  }

  [CustomDisplayName("Attribute.Interfaces.Document_624")]
  [CustomDescription("Attribute.Interfaces.Document_625")]
  [CustomCategory("Attribute.Interfaces.Document_615")]
  public ShowOnPageOnlyPropertyWrapper ShowOnPageOnlyVisual
  {
    get => new ShowOnPageOnlyPropertyWrapper(this);
  }

  public override bool IsVisibleNow
  {
    get
    {
      if (!base.IsVisibleNow)
        return false;
      return this.page == null || this.IsTemplate || this.ShowOnPageOnly == ShowOnPageOnly.All || ((!this.page.IsFirstPage ? 0 : ((this.ShowOnPageOnly & ShowOnPageOnly.FirstDataPage) != 0 ? 1 : 0)) | (!this.page.IsLastPage ? 0 : ((this.ShowOnPageOnly & ShowOnPageOnly.LastDataPage) != 0 ? 1 : 0)) | (this.page.IsFirstPage || this.page.IsLastPage ? 0 : ((this.ShowOnPageOnly & ShowOnPageOnly.NextDataPage) != 0 ? 1 : 0))) != 0;
    }
  }

  /// <summary>Узел является шаблоном</summary>
  public override bool IsTemplate
  {
    [DebuggerStepThrough] get => this.OwnerDocument != null && this.OwnerDocument.IsTemplate;
  }

  /// <summary>Корень дерева в котором должен находиться шаблон этого узла</summary>
  public override DocumentTreeNode TemplateRoot
  {
    [DebuggerStepThrough] get
    {
      ImDocumentData ownerDocument = this.OwnerDocument;
      if (ownerDocument != null)
        return ownerDocument.TemplateRoot;
      DocumentTreeNode template = this.Template;
      if (template != null)
      {
        DocumentTreeNode docTreeRoot = (DocumentTreeNode) template.GetDocTreeRoot();
        if (docTreeRoot != null)
          return docTreeRoot;
      }
      return template;
    }
  }

  /// <summary>Геометрия перекрыта</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_198")]
  [CustomDescription("Attribute.Interfaces.Document_199")]
  [Category("Debug")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool TemplateGeometryOverrided
  {
    [DebuggerStepThrough] get => this.IsOverridden(OverrideFlags.Geometry);
    set
    {
      if (this.TemplateGeometryOverrided == value)
        return;
      this.AssignTemplateGeometryOverrided(value);
      if (value)
        return;
      this.ApplyTemplateProperties(true, true);
    }
  }

  /// <summary>Назначить значение свойству TemplateGeometryOverrided</summary>
  /// <param name="value">Значение</param>
  protected virtual void AssignTemplateGeometryOverrided(bool value)
  {
    if (value)
      this.SetOverrideFlags(OverrideFlags.Geometry);
    else
      this.ResetOverrideFlags(OverrideFlags.Geometry);
  }

  /// <summary>Применить к элементу свойства шаблона</summary>
  /// <param name="template">Шаблон</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="isLoading">Вызов в процессе загрузки</param>
  public override void ApplyTemplateProperties(
    DocumentTreeNode template,
    bool updateUI,
    bool updateLayout,
    bool isLoading)
  {
    if (template == null)
      return;
    if (!(template is PageElementNode pageElementNode))
      throw new Exception(string.Format(ExceptionMessages.InvalideTemplateType, (object) this.Template.Id, (object) this.Id));
    bool flag = !updateUI || this.SuspendedUpdateUIGeometryFlag && this.SuspendedRefreshUIFlag;
    if (!flag)
      this.SuspendUpdateGeometryRefreshUI();
    try
    {
      base.ApplyTemplateProperties(template, updateUI, updateLayout, isLoading);
      if ((this.overrideFlags & OverrideFlags.ReadOnly) == OverrideFlags.None)
        this.readOnly = pageElementNode.ReadOnly;
      this.geometryChangingBlocked = pageElementNode.geometryChangingBlocked;
      this.showOnPageOnly = pageElementNode.showOnPageOnly;
      if ((this.overrideFlags3 & OverrideFlags3.Transparent) != OverrideFlags3.None)
        return;
      this.transparent = pageElementNode.Transparent;
    }
    finally
    {
      if (!flag)
        this.ResumeUpdateRefreshUI(true, true);
    }
  }

  /// <summary>Можно ли использовать заданный узел как шаблон</summary>
  /// <param name="node">Узел</param>
  /// <returns></returns>
  public override bool CanUseNodeAsTemplate(DocumentTreeNode node)
  {
    return node != null && node is PageElementNode;
  }

  /// <summary>Найти шаблон этого узла по идентификатору templateId</summary>
  /// <param name="templateId">Идентификатор шаблона</param>
  /// <returns>Шаблон узла</returns>
  public override DocumentTreeNode FindTemplate(string templateId)
  {
    return this.OwnerDocument != null && this.OwnerDocument.Template != null ? this.OwnerDocument.Template.FindNode(templateId) : (DocumentTreeNode) null;
  }

  /// <summary>Нужно ли сохранять свойство GeometryChangingBlocked</summary>
  protected virtual bool NeedSaveGeometryChangingBlocked
  {
    [DebuggerStepThrough] get => !this.geometryChangingBlocked;
  }

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlAttributes(xw, objectRefId);
    bool flag = this.Template != null;
    if (!this.transparent && !flag || this.overrideFlags3.HasFlag((Enum) OverrideFlags3.Transparent) & flag)
      xw.WriteAttributeString("transparent", this.transparent ? "1" : "0");
    if (!flag && this.readOnly || flag && (this.overrideFlags & OverrideFlags.ReadOnly) != OverrideFlags.None)
      xw.WriteAttributeString("readOnly", this.readOnly ? "1" : "0");
    if (this.NeedSaveGeometryChangingBlocked && !flag)
      xw.WriteAttributeString("gmLock", this.geometryChangingBlocked ? "1" : "0");
    if (flag || this.ShowOnPageOnly == ShowOnPageOnly.All)
      return;
    xw.WriteAttributeString("showOnPage", ((int) this.ShowOnPageOnly).ToString());
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (PageElementNode.ReadFieldsDict != null)
    {
      ReadFieldFromXmlDelegate fieldFromXmlDelegate;
      PageElementNode.ReadFieldsDict.TryGetValue(readArgs.Reader.LocalName, out fieldFromXmlDelegate);
      if (fieldFromXmlDelegate != null)
      {
        fieldFromXmlDelegate((DocumentTreeNode) this, readArgs);
        return true;
      }
    }
    if (base.ReadFieldFromXml(readArgs))
      return true;
    switch (readArgs.Reader.LocalName)
    {
      case "readOnly":
        PageElementNode.ReadReadOnly((DocumentTreeNode) this, readArgs);
        return true;
      case "geometryLocked":
      case "gmLock":
        PageElementNode.ReadGeometryLocked((DocumentTreeNode) this, readArgs);
        return true;
      case "transparent":
        PageElementNode.ReadTransparent((DocumentTreeNode) this, readArgs);
        return true;
      default:
        return false;
    }
  }

  private static void InitReadFieldDict()
  {
    PageElementNode.ReadFieldsDict = new Dictionary<string, ReadFieldFromXmlDelegate>((IDictionary<string, ReadFieldFromXmlDelegate>) VisualNode.ReadFieldsDict);
    PageElementNode.ReadFieldsDict.Add("readOnly", new ReadFieldFromXmlDelegate(PageElementNode.ReadReadOnly));
    PageElementNode.ReadFieldsDict.Add("geometryLocked", new ReadFieldFromXmlDelegate(PageElementNode.ReadGeometryLocked));
    PageElementNode.ReadFieldsDict.Add("gmLocked", new ReadFieldFromXmlDelegate(PageElementNode.ReadGeometryLocked));
    PageElementNode.ReadFieldsDict.Add("transparent", new ReadFieldFromXmlDelegate(PageElementNode.ReadTransparent));
    PageElementNode.ReadFieldsDict.Add("showOnPage", new ReadFieldFromXmlDelegate(PageElementNode.ReadShowOnPage));
  }

  private static void ReadReadOnly(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (readArgs.Version < 21)
      ((PageElementNode) docNode).readOnly = bool.Parse(readArgs.Reader.Value);
    else
      ((PageElementNode) docNode).readOnly = readArgs.Reader.Value == "1";
    docNode.overrideFlags |= OverrideFlags.ReadOnly;
  }

  private static void ReadGeometryLocked(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (readArgs.Version < 21)
      ((PageElementNode) docNode).geometryChangingBlocked = bool.Parse(readArgs.Reader.Value);
    else
      ((PageElementNode) docNode).geometryChangingBlocked = readArgs.Reader.Value == "1";
  }

  private static void ReadTransparent(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    docNode.overrideFlags3 |= OverrideFlags3.Transparent;
    if (readArgs.Version < 21)
      ((PageElementNode) docNode).transparent = bool.Parse(readArgs.Reader.Value);
    else
      ((PageElementNode) docNode).transparent = readArgs.Reader.Value == "1";
  }

  private static void ReadShowOnPage(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    int num = int.Parse(readArgs.Reader.Value);
    ((PageElementNode) docNode).showOnPageOnly = (ShowOnPageOnly) num;
  }

  /// <summary>Копировать поля из src</summary>
  /// <param name="src">Источник</param>
  /// <param name="copyChildren">Копировать дочерние узлы</param>
  /// <param name="copyData">Копировать данные</param>
  /// <param name="copyDataNodes">Копировать узлы являющиеся ячейками данных для таблиц</param>
  /// <param name="templateClone">Копирование по шаблону</param>
  /// <param name="externalLink">Копировать внешние ссылки</param>
  /// <param name="links">Словарь скопированных ссылок</param>
  protected override void CopyFields(
    DocumentTreeNode src,
    bool copyChildren,
    bool copyData,
    bool copyDataNodes,
    bool templateClone,
    bool externalLink,
    IDictionary links)
  {
    base.CopyFields(src, copyChildren, copyData, copyDataNodes, templateClone, externalLink, links);
    if (!(src is PageElementNode pageElementNode))
      return;
    this.geometryChangingBlocked = pageElementNode.geometryChangingBlocked;
    this.readOnly = pageElementNode.readOnly;
    this.transparent = pageElementNode.transparent;
    this.showOnPageOnly = pageElementNode.showOnPageOnly;
  }
}
