// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.VisualNode
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
using System.Drawing.Imaging;
using System.Runtime.Serialization;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Визуальный узел документа</summary>
[Serializable]
public abstract class VisualNode : DocumentTreeNode
{
  /// <summary>Прямоугольник охватывающий все допустимое пространство</summary>
  public static readonly RectangleF NoClipRectangle = new RectangleF(-10000f, -10000f, 30000f, 30000f);
  public const int CoorRoundDigits = 5;
  public const float CoorCalcPrecision = 1E-05f;
  private int lockCount;
  protected new static Dictionary<string, ReadFieldFromXmlDelegate> ReadFieldsDict = (Dictionary<string, ReadFieldFromXmlDelegate>) null;
  [NonSerialized]
  private VisibleChanged_EventHandler visibleChanged;
  [NonSerialized]
  private UIGeometryChanged_EventHandler uiGeometryChanged;
  private static ImageAttributes negativeImageAttributes = (ImageAttributes) null;
  private static ColorMatrix negativeColorMatrix = (ColorMatrix) null;
  private bool visible = true;
  [NonSerialized]
  protected bool needUI;
  /// <summary>Необходимо обновить геометрию. Метод UpdateUIGeometry</summary>
  [NonSerialized]
  protected bool needUpdateUIGeometry = true;

  /// <summary>Конструктор</summary>
  /// <param name="initFields">Вызвать метод InitFields()</param>
  public VisualNode(bool initFields)
    : base(initFields)
  {
  }

  /// <summary>Конструктор необходимый для десериализации (ISerializable)</summary>
  /// <param name="info">Заполненный данными SerializationInfo</param>
  /// <param name="context">Контекст десериализации</param>
  protected VisualNode(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  /// <summary>Конструктор</summary>
  public VisualNode()
  {
  }

  static VisualNode() => VisualNode.InitReadFieldDict();

  /// <summary>Получить список узлов привязки</summary>
  /// <param name="originalPoint">Оригинальная точка</param>
  /// <param name="snapSize">Размер области привязки</param>
  /// <param name="snapPointList">Список полученных точек</param>
  /// <param name="excludeNode">Узел который должен исключаться из рассмотрения</param>
  public abstract void GetSnapPoints(
    PointF originalPoint,
    float snapSize,
    List<SnapPoint> snapPointList,
    VisualNode excludeNode);

  /// <summary>Показывать на экране, что узел выбран</summary>
  [Browsable(false)]
  [Category("Debug")]
  public virtual bool ShowSelected
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Отображать фокус элемента</summary>
  [Browsable(false)]
  [Category("Debug")]
  public virtual bool ShowFocused
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Инвертировать цвет</summary>
  /// <param name="c">Исходный цвет</param>
  /// <returns>Инвертированный цвет</returns>
  public static Color InvertColor(Color c)
  {
    return Color.FromArgb((int) c.A, (int) byte.MaxValue - (int) c.R, (int) byte.MaxValue - (int) c.G, (int) byte.MaxValue - (int) c.B);
  }

  /// <summary>Атрибуты рисунка для негативного изображения</summary>
  [Browsable(false)]
  public static ImageAttributes NegativeImageAttributes
  {
    [DebuggerStepThrough] get
    {
      if (VisualNode.negativeImageAttributes == null)
      {
        VisualNode.negativeImageAttributes = new ImageAttributes();
        VisualNode.negativeImageAttributes.SetColorMatrix(VisualNode.NegativeColorMatrix);
      }
      return VisualNode.negativeImageAttributes;
    }
  }

  /// <summary>Матрица преобразования цвета для негативного изображения</summary>
  [Browsable(false)]
  public static ColorMatrix NegativeColorMatrix
  {
    [DebuggerStepThrough] get
    {
      if (VisualNode.negativeColorMatrix == null)
      {
        VisualNode.negativeColorMatrix = new ColorMatrix();
        VisualNode.negativeColorMatrix.Matrix00 = VisualNode.negativeColorMatrix.Matrix11 = VisualNode.negativeColorMatrix.Matrix22 = -1f;
        VisualNode.negativeColorMatrix.Matrix40 = VisualNode.negativeColorMatrix.Matrix41 = VisualNode.negativeColorMatrix.Matrix42 = 1f;
      }
      return VisualNode.negativeColorMatrix;
    }
  }

  /// <summary>Установить значение свойства NeedUI узлу и всем подузлам</summary>
  /// <param name="needUI">Значение свойства NeedUI</param>
  public void SetNeedUIRecursive(bool needUI, bool createUI)
  {
    this.SetNeedUI(needUI, createUI);
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is VisualNode node)
        node.SetNeedUIRecursive(needUI, createUI);
    }
  }

  public void SetNeedUI(bool value, bool createUI)
  {
    if (this.needUI == value)
      return;
    this.needUI = value;
    if (this.needUI)
    {
      if (!createUI)
        return;
      this.CreateUI();
    }
    else
      this.DestroyUI();
  }

  /// <summary>Объекты интерфейса пользователя нужны</summary>
  [Browsable(false)]
  public virtual bool NeedUI
  {
    [DebuggerStepThrough] get => this.needUI;
  }

  /// <summary>Видимый в данный момент.
  /// В некоторых условиях элемент может не отобрнажаться в текущий момент.
  /// Например, невыбранные варианты строк данных в шаблоне таблицы</summary>
  [Browsable(false)]
  public virtual bool IsVisibleNow => this.Visible;

  /// <summary>Видимый</summary>
  [CustomDisplayName("Attribute.Interfaces.Document_509")]
  [CustomDescription("Attribute.Interfaces.Document_510")]
  [CustomCategory("Attribute.Interfaces.Document_511")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public virtual bool Visible
  {
    [DebuggerStepThrough] get => this.visible;
    set => this.SetVisible(value, true, true, true, false);
  }

  /// <summary>Установить значение свойства Visible</summary>
  /// <param name="value">Значение</param>
  internal void AssingVisible(bool value)
  {
    if (this.visible == value)
      return;
    this.visible = value;
  }

  /// <summary>Установить свойство Visible</summary>
  /// <param name="value">Значение</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="recursive">Назначить рекурсивно всем нижележащим узлам</param>
  /// <param name="setOverride">Устанавливать флаг перекрытия свойства шаблона</param>
  public virtual void SetVisible(
    bool value,
    bool updateUI,
    bool needUpdateLayout,
    bool updateLayout,
    bool recursive,
    bool setOverride = true)
  {
    bool flag = this.visible != value;
    if (!(flag | recursive))
      return;
    if (this.OwnerDocument != null && !this.OwnerDocument.IsLoading && this.OwnerDocument.UndoManager != null)
      this.OwnerDocument.UndoManager.CreateUndo((object) this, "Visible", (object) this.Visible, (object) value);
    this.AssingVisible(value);
    if (setOverride)
      this.SetOverrideFlags3(OverrideFlags3.Visible);
    if (recursive && this.nodes != null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        if (this.nodes[index] is VisualNode node)
          node.SetVisible(value, false, false, false, recursive, false);
      }
    }
    if (!flag)
      return;
    if (needUpdateLayout)
    {
      this.AssignNeedUpdateLayoutFlag(false);
      this.SetNeedUpdateLayoutFlag(true, true, updateUI, updateLayout);
    }
    if (!updateLayout & updateUI)
      this.RefreshUI();
    this.OnVisibleChanged(new VisibleChanged_EventArgs());
    this.SetPropertiesChangedFlag(false, false, false, false, false);
    this.SetPropertiesChangedFlag(true, true, false, updateUI, updateLayout);
    this.OnChanged(new Changed_EventArgs());
  }

  /// <summary>Создать объекты интерфейса пользователя. Должен быть перекрыт в наследнике</summary>
  public virtual void CreateUI()
  {
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is VisualNode node)
        node.CreateUI();
    }
  }

  /// <summary>Удалить объекты интерфейса пользователя</summary>
  public virtual void DestroyUI()
  {
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is VisualNode node)
        node.DestroyUI();
    }
  }

  /// <summary>Добавить и связать объекты интерфейса пользователя</summary>
  /// <param name="child">Дочерний узел</param>
  public virtual void AddChildUI(DocumentTreeNode child, bool createUI)
  {
  }

  /// <summary>Отобразить на объекте Graphics</summary>
  /// <param name="context">Данные для отрисовки</param>
  public virtual void Draw(DrawContext context)
  {
    if (!this.IsVisibleNow || this.SuspendedRefreshUIFlag || this.nodes == null)
      return;
    bool? isSelected = context.IsSelected;
    bool? isFocused = context.IsFocused;
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is VisualNode node)
        node.Draw(context);
    }
    context.IsSelected = isSelected;
    context.IsFocused = isFocused;
  }

  /// <summary>Обновить мировые координаты элемента преобразовав экранные координаты</summary>
  public virtual void UpdateWorldCoor()
  {
    if (this.nodes == null)
      return;
    bool flag = this.SuspendedUpdateUIGeometryFlag && this.SuspendedRefreshUIFlag;
    if (!flag)
      this.SuspendUpdateGeometryRefreshUI();
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is VisualNode node)
        node.UpdateWorldCoor();
    }
    if (flag)
      return;
    this.ResumeUpdateRefreshUI(true, true);
  }

  /// <summary>Флаг отложенной необходимости обновить интерфейс</summary>
  [Category("Debug")]
  public virtual bool NeedUpdateUIGeometry
  {
    [DebuggerStepThrough] get => this.needUpdateUIGeometry;
  }

  public void SetNeedUpdateUIGeometry(bool value, bool update)
  {
    if (this.needUpdateUIGeometry == value)
      return;
    this.needUpdateUIGeometry = value;
    if (!(this.needUpdateUIGeometry & update))
      return;
    this.UpdateUIGeometry(true);
  }

  /// <summary>Установить значение NeedUpdateUIGeometry для узла и всех дочерних узлов</summary>
  /// <param name="value">Значение NeedUpdateUIGeometry</param>
  public virtual void SetNeedUpdateUIGeometryRecursive(bool value, bool update)
  {
    bool updateUiGeometryFlag = this.SuspendedUpdateUIGeometryFlag;
    if (!updateUiGeometryFlag)
      this.SuspendUpdateUIGeometry();
    this.needUpdateUIGeometry = value;
    if (this.nodes != null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        if (this.nodes[index] is VisualNode node)
          node.SetNeedUpdateUIGeometryRecursive(value, update);
      }
    }
    if (updateUiGeometryFlag)
      return;
    this.ResumeUpdateUIGeometry(update, update);
  }

  /// <summary>Заблокировать обновление геометрии интерфейса пользователя
  /// <remarks>Блокирова увеличивает значение счетчика. Разблокировка соответственно уменьшает значение счетчика. При нулевом значении счетчика обновление разрешено.</remarks>&gt;
  /// </summary>
  public virtual void SuspendUpdateUIGeometry()
  {
  }

  /// <summary>Разблокировать обновление геометрии интерфейса пользователя</summary>
  /// <param name="update">Обновить геометрию</param>
  /// <param name="refresh">Обновить изображение</param>
  public virtual void ResumeUpdateUIGeometry(bool update, bool refresh)
  {
    if (update && !this.SuspendedUpdateUIGeometryFlag)
      this.UpdateUIGeometry(false);
    if (this.nodes != null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        if (this.nodes[index] is VisualNode node)
          node.ResumeUpdateUIGeometry(update, false);
      }
    }
    if (!refresh)
      return;
    this.RefreshUI();
  }

  /// <summary>Обновление геометрии интерфейса пользователя заблокировано</summary>
  [Category("Debug")]
  public virtual bool SuspendedUpdateUIGeometryFlag
  {
    [DebuggerStepThrough] get => false;
    set
    {
    }
  }

  /// <summary>Заблокировать обновление изображения
  /// <remarks>Блокирова увеличивает значение счетчика. Разблокировка соответственно уменьшает значение счетчика. При нулевом значении счетчика обновление разрешено.</remarks>&gt;
  /// </summary>
  public virtual void SuspendRefreshUI()
  {
  }

  /// <summary>Разблокировать обновление изображения</summary>
  public virtual void ResumeRefreshUI(bool refresh)
  {
    if (!refresh)
      return;
    this.RefreshUI();
  }

  /// <summary>Обновление изображения интерфейса пользователя заблокировано</summary>
  [Category("Debug")]
  public virtual bool SuspendedRefreshUIFlag
  {
    [DebuggerStepThrough] get => false;
    set
    {
    }
  }

  public void LockUndo()
  {
    if (this.Parent != null && this.Parent is VisualNode)
      (this.Parent as VisualNode).LockUndo();
    else
      ++this.lockCount;
  }

  public void UnlockUndo()
  {
    if (this.Parent != null && this.Parent is VisualNode)
    {
      (this.Parent as VisualNode).UnlockUndo();
    }
    else
    {
      if (this.lockCount <= 0)
        return;
      --this.lockCount;
    }
  }

  public bool IsUndoLocked()
  {
    return this.Parent != null && this.Parent is VisualNode ? (this.Parent as VisualNode).IsUndoLocked() : this.lockCount > 0;
  }

  /// <summary>Обновить геометрию интерфейса пользователя</summary>
  public virtual void UpdateUIGeometry(bool refreshUI)
  {
    if (this.SuspendedUpdateUIGeometryFlag)
      return;
    bool flag = !refreshUI || this.SuspendedRefreshUIFlag;
    if (!flag)
      this.SuspendRefreshUI();
    if (this.nodes != null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        if (this.nodes[index] is VisualNode node)
          node.UpdateUIGeometry(false);
      }
    }
    this.needUpdateUIGeometry = false;
    if (flag)
      return;
    this.ResumeRefreshUI(refreshUI);
  }

  /// <summary>Обновить изображение на экране</summary>
  public virtual void RefreshUI()
  {
  }

  /// <summary>Обновить изображение на экране</summary>
  /// <param name="clipRectangle">Область которую нужно обновить</param>
  public virtual void InvalidateUI(Rectangle clipRectangle)
  {
  }

  /// <summary>Обновить изображение на экране</summary>
  /// <param name="force">Обновить даже если заблокировано обновление</param>
  public virtual void InvalidateUI(bool force)
  {
  }

  /// <summary>Обновить изображение на экране</summary>
  /// <param name="clipRectangle">Область которую нужно обновить</param>
  /// <param name="force">Обновить даже если заблокировано обновление</param>
  public virtual void InvalidateUI(Rectangle clipRectangle, bool force)
  {
  }

  /// <summary>Обновить изображение на экране</summary>
  /// <param name="region">Область которую нужно обновить</param>
  public virtual void InvalidateUI(Region region)
  {
  }

  /// <summary>Заблокировать обновление геометрии интерфейса и изображения</summary>
  public virtual void SuspendUpdateGeometryRefreshUI()
  {
  }

  /// <summary>Разблокировать и провести обновление геометрии интерфейса и изображения</summary>
  public virtual void ResumeUpdateRefreshUI(bool update, bool refresh)
  {
    if (update && !this.SuspendedUpdateUIGeometryFlag)
      this.UpdateUIGeometry(false);
    if (this.nodes != null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        if (this.nodes[index] is VisualNode node)
          node.ResumeUpdateRefreshUI(update, false);
      }
    }
    if (!refresh || this.SuspendedRefreshUIFlag)
      return;
    this.RefreshUI();
  }

  /// <summary>Отфильтровать свойства элемента для показа в PopertyGrid</summary>
  /// <param name="properties">Список PropertyDescriptor свойств</param>
  /// <param name="attributes">Массив атрибутов элемента</param>
  protected override void FilterProperties(IDictionary properties, Attribute[] attributes)
  {
    base.FilterProperties(properties, attributes);
    if (ImDocumentData.ShowDebugInfo)
      return;
    this.RemoveProperty(properties, "NeedUpdateUIGeometry");
    this.RemoveProperty(properties, "SuspendedRefreshUIFlag");
    this.RemoveProperty(properties, "SuspendedUpdateUIGeometryFlag");
  }

  /// <summary>Сохранить данные в атрибуты XML</summary>
  /// <param name="xw">XmlWriter</param>
  /// <param name="objectRefId">Генератор идентификаторов</param>
  public override void WriteXmlAttributes(XmlWriter xw, ObjectIDGenerator objectRefId)
  {
    base.WriteXmlAttributes(xw, objectRefId);
    bool flag = this.HasTemplate();
    if ((flag || this.visible) && (!flag || (this.overrideFlags3 & OverrideFlags3.Visible) == OverrideFlags3.None))
      return;
    xw.WriteAttributeString("visible", this.visible ? "1" : "0");
  }

  /// <summary>Загрузить поле из текущего узла XML</summary>
  /// <param name="readArgs">Аргументы чтения из XML</param>
  /// <returns>Возвращает true, если поле загружено</returns>
  public override bool ReadFieldFromXml(XmlReadArgs readArgs)
  {
    if (VisualNode.ReadFieldsDict != null)
    {
      ReadFieldFromXmlDelegate fieldFromXmlDelegate;
      VisualNode.ReadFieldsDict.TryGetValue(readArgs.Reader.LocalName, out fieldFromXmlDelegate);
      if (fieldFromXmlDelegate != null)
      {
        fieldFromXmlDelegate((DocumentTreeNode) this, readArgs);
        return true;
      }
    }
    if (base.ReadFieldFromXml(readArgs))
      return true;
    if (!(readArgs.Reader.LocalName == "visible"))
      return false;
    VisualNode.ReadVisible((DocumentTreeNode) this, readArgs);
    return true;
  }

  private static void InitReadFieldDict()
  {
    VisualNode.ReadFieldsDict = new Dictionary<string, ReadFieldFromXmlDelegate>((IDictionary<string, ReadFieldFromXmlDelegate>) DocumentTreeNode.ReadFieldsDict);
    VisualNode.ReadFieldsDict.Add("visible", new ReadFieldFromXmlDelegate(VisualNode.ReadVisible));
  }

  private static void ReadVisible(DocumentTreeNode docNode, XmlReadArgs readArgs)
  {
    if (readArgs.Version < 21)
    {
      ((VisualNode) docNode).AssingVisible(bool.Parse(readArgs.Reader.Value));
    }
    else
    {
      ((VisualNode) docNode).AssingVisible(readArgs.Reader.Value == "1");
      docNode.overrideFlags3 |= OverrideFlags3.Visible;
    }
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
    if (!(src is VisualNode visualNode))
      return;
    this.visible = visualNode.visible;
  }

  /// <summary>Применить к элементам дерева их шаблоны</summary>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  public override void ApplyTreeTemplates(bool updateUI, bool updateLayout)
  {
    bool flag = !updateUI || this.SuspendedUpdateUIGeometryFlag && this.SuspendedRefreshUIFlag;
    if (!flag)
      this.SuspendUpdateGeometryRefreshUI();
    try
    {
      base.ApplyTreeTemplates(false, updateLayout);
    }
    finally
    {
      if (!flag)
        this.ResumeUpdateRefreshUI(true, true);
    }
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
    base.ApplyTemplateProperties(template, updateUI, updateLayout, isLoading);
    if (!(template is VisualNode visualNode) || (this.overrideFlags3 & OverrideFlags3.Visible) != OverrideFlags3.None || this is RectangleElement)
      return;
    this.SetVisible(visualNode.Visible, updateUI, true, updateLayout, false, false);
  }

  /// <summary>Создать копию элемента используя этот узел как шаблон</summary>
  /// <param name="copyChildren">Копировать дочерние узлы</param>
  /// <param name="copyDataNodes">Копировать узлы-данные в таблицах</param>
  /// <returns>Копия узла</returns>
  public override DocumentTreeNode CloneFromTemplate(bool copyChildren, bool copyDataNodes)
  {
    VisualNode visualNode = (VisualNode) base.CloneFromTemplate(copyChildren, copyDataNodes);
    visualNode.needUpdateUIGeometry = true;
    return (DocumentTreeNode) visualNode;
  }

  /// <summary>Найти элемент страницы под данной точкой</summary>
  /// <param name="point">Точка</param>
  /// <param name="layer">Слой</param>
  /// <param name="firstOnly">Найти первый попавшийся элемент</param>
  public virtual VisualNode FindPageElementAtPoint(PointF point, ref int layer, bool firstOnly)
  {
    VisualNode pageElementAtPoint1 = (VisualNode) null;
    if (this.nodes != null)
    {
      for (int index = 0; index < this.nodes.Count && (!firstOnly || pageElementAtPoint1 == null); ++index)
      {
        if (this.nodes[index] is VisualNode node)
        {
          VisualNode pageElementAtPoint2 = node.FindPageElementAtPoint(point, ref layer, firstOnly);
          if (pageElementAtPoint2 != null)
            pageElementAtPoint1 = pageElementAtPoint2;
        }
      }
    }
    return pageElementAtPoint1;
  }

  /// <summary>Получить элементы страницы в заданном прямоугольнике</summary>
  /// <param name="rect">Прямоугольник</param>
  /// <param name="elements">Возвращает элементы</param>
  /// <param name="containsOnly">Выбирать только те элементы, которые полностью попадают в прямоугольник</param>
  /// <param name="childOnly">Не учитывать родительский элемент</param>
  public virtual void FindPageElementsInRectangle(
    RectangleF rect,
    List<VisualNode> elements,
    bool containsOnly,
    bool childOnly = false)
  {
    if (elements == null)
      throw new ArgumentNullException(nameof (elements));
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is VisualNode node)
        node.FindPageElementsInRectangle(rect, elements, containsOnly);
    }
  }

  /// <summary>Определить занимаемый размер для AutoSize родителя</summary>
  /// <param name="currSize">Текущий размер (начальное значение 0)</param>
  /// <param name="childOnly">Не учитывать родительский элемент</param>
  public virtual SizeF FindMinSize(SizeF currSize, bool childOnly = false)
  {
    if (this.nodes != null)
    {
      for (int index = 0; index < this.nodes.Count; ++index)
      {
        if (this.nodes[index] is VisualNode node && node.IsVisibleNow)
          currSize = node.FindMinSize(currSize);
      }
    }
    return currSize;
  }

  /// <param name="alignTop">Список элементов, прижимаемых вверх</param>
  /// <param name="alignVertCenter">Список центрируемых по вертикали элементов</param>
  /// <param name="alignBottom">Список элементов, прижимаемых вниз</param>
  /// <param name="sort">Сортировать списки</param>
  internal void FindAlignElements(
    ref List<RectangleElement> alignLeft,
    ref List<RectangleElement> alignHorzCenter,
    ref List<RectangleElement> alignRight,
    ref List<RectangleElement> alignTop,
    ref List<RectangleElement> alignVertCenter,
    ref List<RectangleElement> alignBottom,
    bool sort)
  {
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is RectangleElement node2 && node2.IsVisibleNow)
      {
        if (node2.HorzAlign == ElementHorizontalAlign.Left)
        {
          if (alignLeft == null)
            alignLeft = new List<RectangleElement>();
          alignLeft.Add(node2);
        }
        else if (node2.HorzAlign == ElementHorizontalAlign.Right)
        {
          if (alignRight == null)
            alignRight = new List<RectangleElement>();
          alignRight.Add(node2);
        }
        else if (node2.HorzAlign == ElementHorizontalAlign.Center)
        {
          if (alignHorzCenter == null)
            alignHorzCenter = new List<RectangleElement>();
          alignHorzCenter.Add(node2);
        }
        if (node2.VertAlign == ElementVerticalAlign.Top)
        {
          if (alignTop == null)
            alignTop = new List<RectangleElement>();
          alignTop.Add(node2);
        }
        else if (node2.VertAlign == ElementVerticalAlign.Bottom)
        {
          if (alignBottom == null)
            alignBottom = new List<RectangleElement>();
          alignBottom.Add(node2);
        }
        else if (node2.VertAlign == ElementVerticalAlign.Center)
        {
          if (alignVertCenter == null)
            alignVertCenter = new List<RectangleElement>();
          alignVertCenter.Add(node2);
        }
      }
      else if (this.nodes[index] is DocumentSection node1)
        node1.FindAlignElements(ref alignLeft, ref alignHorzCenter, ref alignRight, ref alignTop, ref alignVertCenter, ref alignBottom, false);
    }
    if (!sort)
      return;
    if (alignLeft != null)
      alignLeft.Sort((IComparer<RectangleElement>) new LeftCoorComparer());
    if (alignRight != null)
      alignRight.Sort((IComparer<RectangleElement>) new RightCoorComparer());
    if (alignTop != null)
      alignTop.Sort((IComparer<RectangleElement>) new TopCoorComparer());
    if (alignBottom == null)
      return;
    alignBottom.Sort((IComparer<RectangleElement>) new BottomCoorComparer());
  }

  /// <summary>Сравнить 2 значения с погрешностью CoorCalcPrecision</summary>
  /// <param name="value1">Значение 1</param>
  /// <param name="value2">Значение 2</param>
  /// <returns>Возвращает true, если value1 меньше или равно value2 с учётом погрешности CoorCalcPrecision</returns>
  public static bool LessOrEqualWithMiscalculation(float value1, float value2)
  {
    return (double) value1 <= (double) value2 + ((double) value2 >= 0.0 ? 9.9999997473787516E-06 : -9.9999997473787516E-06);
  }

  /// <summary>Сравнить 2 значения с погрешностью CoorCalcPrecision</summary>
  /// <param name="value1">Значение 1</param>
  /// <param name="value2">Значение 2</param>
  /// <returns>Возвращает true, если value1 меньше value2 с учётом погрешности CoorCalcPrecision</returns>
  public static bool LessWithMiscalculation(float value1, float value2)
  {
    return (double) value1 < (double) value2 - 9.9999997473787516E-06;
  }

  /// <summary>Сравнить 2 значения с погрешностью CoorCalcPrecision</summary>
  /// <param name="value1">Значение 1</param>
  /// <param name="value2">Значение 2</param>
  /// <returns>Возвращает true, если value1 больше value2 с учётом погрешности CoorCalcPrecision</returns>
  public static bool MoreWithMiscalculation(float value1, float value2)
  {
    return (double) value1 > (double) value2 + 9.9999997473787516E-06;
  }

  /// <summary>Сравнить 2 значения с погрешностью CoorCalcPrecision</summary>
  /// <param name="value1">Значение 1</param>
  /// <param name="value2">Значение 2</param>
  /// <returns>Возвращает true, если value1 больше или равно value2 с учётом погрешности CoorCalcPrecision</returns>
  public static bool MoreOrEqualWithMiscalculation(float value1, float value2)
  {
    return (double) value1 + ((double) value2 >= 0.0 ? 9.9999997473787516E-06 : -9.9999997473787516E-06) >= (double) value2;
  }

  /// <summary>Присвоить значение свойству Parent</summary>
  /// <param name="value">Новое значение Parent</param>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="isLoading">Действие в контексте загрузки документа</param>
  public override void AssignParent(
    DocumentTreeNode value,
    bool updateUI,
    bool updateLayout,
    bool isLoading)
  {
    if (this.parent == value)
      return;
    if (isLoading || this.isVirtualNode)
      base.AssignParent(value, updateUI, updateLayout, isLoading);
    else
      base.AssignParent(value, false, updateLayout, isLoading);
  }

  /// <summary>Вставить в заданную позицию дочерний узел</summary>
  /// <param name="index">Позиция в которую будет вставлен узел</param>
  /// <param name="child">Узел</param>
  /// <param name="insertByShift">Узел перемещается в пределах таблицы</param>
  /// <param name="uniteTable">Объединить распределенные ячейки перед вставкой</param>
  /// <param name="updateUI">Обновить элементы управления</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="isNew">Узел новый и не требуется это проверять</param>
  /// <returns>true, если вставка не была отменена</returns>
  public override bool InsertChildNode(
    int index,
    DocumentTreeNode child,
    bool insertByShift,
    bool uniteTable,
    bool updateUI,
    bool updateLayout,
    bool isNew = false)
  {
    int num = base.InsertChildNode(index, child, insertByShift, uniteTable, updateUI, updateLayout, isNew) ? 1 : 0;
    if ((num & (updateUI ? 1 : 0)) == 0)
      return num != 0;
    this.UpdateUIGeometry(true);
    return num != 0;
  }

  /// <summary>Выполнить предварительные действия перед окончанием изменения структуры</summary>
  /// <param name="updateUI">Обновить интерфейс пользователя</param>
  /// <param name="refreshUI">Обновить изображение в интерфейсе пользователя</param>
  /// <param name="updateTemplateLinks">Обновить ссылки на шаблоны</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  protected override void PreProcessEndChangingStructure(
    bool updateUI,
    bool refreshUI,
    bool updateTemplateLinks,
    bool updateLayout)
  {
    base.PreProcessEndChangingStructure(updateUI, refreshUI, updateTemplateLinks, updateLayout);
    if (updateUI)
    {
      this.UpdateUIGeometry(refreshUI);
    }
    else
    {
      if (!refreshUI)
        return;
      this.RefreshUI();
    }
  }

  protected override void OnRemoved(Removed_EventArgs e)
  {
    base.OnRemoved(e);
    if (e.RemovedByShift)
      return;
    this.DestroyUI();
  }

  /// <summary>Назначить значение ReadOnly для текстовых ячеек рекурсивно</summary>
  /// <param name="readOnly">Значение ReadOnly</param>
  public void SetReadOnlyForTextRecursive(bool readOnly)
  {
    if (this is TextData textData)
      textData.AssignReadOnly(readOnly);
    if (this.nodes == null)
      return;
    for (int index = 0; index < this.nodes.Count; ++index)
    {
      if (this.nodes[index] is TextData node1)
        node1.AssignReadOnly(readOnly);
      if (this.nodes[index] is VisualNode node2)
        node2.SetReadOnlyForTextRecursive(readOnly);
    }
  }

  /// <summary>Вызывает событие ChildNodeAdded</summary>
  /// <param name="e">Аргумент события</param>
  protected override void OnChildNodeAdded(ChildNode_EventArgs e)
  {
    this.AddChildUI(e.Child, e.UpdateUI);
    base.OnChildNodeAdded(e);
  }

  /// <summary>Генерируется после изменения свойства Visible</summary>
  public event VisibleChanged_EventHandler VisibleChanged
  {
    add => this.visibleChanged += value;
    remove => this.visibleChanged -= value;
  }

  /// <summary>Вызывает событие VisibleChanged</summary>
  /// <param name="e">Аргументы события</param>
  protected virtual void OnVisibleChanged(VisibleChanged_EventArgs e)
  {
    if (this.visibleChanged == null)
      return;
    this.visibleChanged((object) this, e);
  }

  /// <summary>Генерируется после обновления геометрии интерфейса</summary>
  public event UIGeometryChanged_EventHandler UIGeometryChanged
  {
    add => this.uiGeometryChanged += value;
    remove => this.uiGeometryChanged -= value;
  }

  /// <summary>Вызывает событие UIGeometryChanged</summary>
  /// <param name="e">Аргументы события</param>
  protected virtual void OnUIGeometryChanged(UIGeometryChanged_EventArgs e)
  {
    if (this.uiGeometryChanged == null)
      return;
    this.uiGeometryChanged((object) this, e);
  }
}
