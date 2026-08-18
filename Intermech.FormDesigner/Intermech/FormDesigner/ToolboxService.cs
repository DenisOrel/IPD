// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.ToolboxService
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.FormDesigner.Properties;
using Intermech.Interfaces.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner;

/// <summary>Класс, описывающий ToolboxService.</summary>
internal class ToolboxService : ScrollableControl, IToolboxService
{
  internal IDesignerHost _host;
  private System.Type _localType = typeof (IMToolBoxItem);
  private List<DesignerToolBoxTab> _tabCollection = new List<DesignerToolBoxTab>();
  private IMToolBoxItem _selectedItem;
  private Bitmap _bmpPointer;

  /// <summary>Конструктор.</summary>
  public ToolboxService()
  {
    this.AutoScroll = true;
    this.DoubleBuffered = true;
    this._bmpPointer = Resources.Pointer;
    this._bmpPointer.MakeTransparent(Color.Magenta);
  }

  /// <summary>
  /// Получает имена всех категорий имеющихся в данный момент на панели инструментов.
  /// </summary>
  public CategoryNameCollection CategoryNames
  {
    get
    {
      return new CategoryNameCollection(this._tabCollection.Select<DesignerToolBoxTab, string>((Func<DesignerToolBoxTab, string>) (x => x.Category)).ToArray<string>());
    }
  }

  /// <summary>
  /// Получает или задает имя выбранной в данный момент категории с панели инструментов.
  /// </summary>
  public string SelectedCategory { get; set; }

  /// <summary>
  /// Добавляет разработчика нового элемента панели инструментов для форматирования указанных данных.
  /// </summary>
  /// <param name="creator">ToolboxItemCreatorCallback, который создает компонент при вызове элемента панели инструментов</param>
  /// <param name="format">Формат данных, которым управляет разработчик</param>
  public void AddCreator(ToolboxItemCreatorCallback creator, string format)
  {
  }

  /// <summary>
  /// Добавляет разработчика нового элемента панели инструментов для форматирования указанных данных.
  /// </summary>
  /// <param name="creator">ToolboxItemCreatorCallback, который создает компонент при вызове элемента панели инструментов</param>
  /// <param name="format">Формат данных, которым управляет разработчик</param>
  /// <param name="host">IDesignerHost представляет узел конструктора, связанный с разработчиком.</param>
  public void AddCreator(ToolboxItemCreatorCallback creator, string format, IDesignerHost host)
  {
  }

  /// <summary>
  /// Добавляет определенный, связанный с проектом элемент, на панель инструментов.
  /// </summary>
  /// <param name="toolboxItem">Связанный ToolboxItem, который требуется добавить на панель инструментов</param>
  /// <param name="host">IDesignerHost для текущего документа проекта</param>
  public void AddLinkedToolboxItem(ToolboxItem toolboxItem, IDesignerHost host)
  {
  }

  /// <summary>
  /// Добавляет определенный, связанный с проектом элемент, на панель инструментов.
  /// </summary>
  /// <param name="toolboxItem">Связанный ToolboxItem, который требуется добавить на панель инструментов</param>
  /// <param name="category">Категория элемента, которую необходимо добавить на панель инструментов</param>
  /// <param name="host">IDesignerHost для текущего документа проекта</param>
  public void AddLinkedToolboxItem(ToolboxItem toolboxItem, string category, IDesignerHost host)
  {
  }

  /// <summary>
  /// Добавляет указанный элемент определенной категории на панель инструментов.
  /// </summary>
  /// <param name="item">ToolboxItem, который требуется добавить на панель инструментов</param>
  public void AddToolboxItem(ToolboxItem item)
  {
    this.AddToolboxItem(item, (item as IMToolBoxItem).ItemCategory);
  }

  /// <summary>
  /// Добавляет указанный элемент определенной категории на панель инструментов.
  /// </summary>
  /// <param name="item">ToolboxItem, который требуется добавить на панель инструментов</param>
  /// <param name="category">Категория элемента, которую необходимо добавить в ToolboxItem</param>
  public void AddToolboxItem(ToolboxItem item, string category)
  {
    DesignerToolBoxTab designerToolBoxTab = this._tabCollection.FirstOrDefault<DesignerToolBoxTab>((Func<DesignerToolBoxTab, bool>) (x => x.Category == category));
    if (designerToolBoxTab == null)
    {
      designerToolBoxTab = new DesignerToolBoxTab(this, category);
      designerToolBoxTab.QueryContinueDrag += new QueryContinueDragEventHandler(this.OnTab_QueryContinueDrag);
      this._tabCollection.Add(designerToolBoxTab);
    }
    designerToolBoxTab.AddItem(item as IMToolBoxItem);
  }

  /// <summary>
  /// Получает элемент панели инструментов из указанного объекта, который представляет элемент панели в сериализованной форме.
  /// </summary>
  /// <param name="serializedObject">Объект, содержащий ToolboxItem, который требуется извлечь</param>
  /// <returns>ToolboxItem, созданный десериализацией</returns>
  public ToolboxItem DeserializeToolboxItem(object serializedObject)
  {
    return this.DeserializeToolboxItem(serializedObject, this._host);
  }

  /// <summary>
  /// Получает элемент панели инструментов из указанного объекта, который представляет элемент панели в сериализованной форме.
  /// </summary>
  /// <param name="serializedObject">Объект, содержащий ToolboxItem, который требуется извлечь</param>
  /// <param name="host">IDesignerHost для связи с данным ToolboxItem</param>
  /// <returns>ToolboxItem, созданный десериализацией</returns>
  public ToolboxItem DeserializeToolboxItem(object serializedObject, IDesignerHost host)
  {
    return (serializedObject as DataObject).GetData(this._localType) as ToolboxItem;
  }

  /// <summary>
  /// Получает выбранный в данный момент элемент панели инструментов.
  /// </summary>
  /// <returns>ToolboxItem, который выбран в данный момент или пустая ссылка, если элемент панели инструментов не выбран</returns>
  public ToolboxItem GetSelectedToolboxItem() => (ToolboxItem) this._selectedItem;

  /// <summary>
  /// Получает выбранный в данный момент элемент панели инструментов.
  /// </summary>
  /// <param name="host">IDesignerHost, с которым должно быть связано выбранное средство, для обеспечения возврата</param>
  /// <returns>ToolboxItem, который выбран в данный момент или пустая ссылка, если элемент панели инструментов не выбран</returns>
  public ToolboxItem GetSelectedToolboxItem(IDesignerHost host) => this.GetSelectedToolboxItem();

  /// <summary>
  /// Получает набор элементов панели инструментов, которые связаны с указанными узлом конструктора и категорией, с панели инструментов.
  /// </summary>
  /// <returns>Набор ToolboxItemCollection, содержащий текущие элементы панели инструментов, которые связаны с указанными узлом конструктора и категорией</returns>
  public ToolboxItemCollection GetToolboxItems()
  {
    List<IMToolBoxItem> retValue = new List<IMToolBoxItem>();
    this._tabCollection.ForEach((Action<DesignerToolBoxTab>) (x => retValue.AddRange((IEnumerable<IMToolBoxItem>) x.Items)));
    return new ToolboxItemCollection((ToolboxItem[]) retValue.ToArray());
  }

  /// <summary>
  /// Получает набор элементов панели инструментов, которые связаны с указанными узлом конструктора и категорией, с панели инструментов.
  /// </summary>
  /// <param name="host">IDesignerHost, который связан с извлекаемыми элементами панели инструментов</param>
  /// <returns>Набор ToolboxItemCollection, содержащий текущие элементы панели инструментов, которые связаны с указанными узлом конструктора и категорией</returns>
  public ToolboxItemCollection GetToolboxItems(IDesignerHost host) => this.GetToolboxItems();

  /// <summary>
  /// Получает набор элементов панели инструментов, которые связаны с указанными узлом конструктора и категорией, с панели инструментов.
  /// </summary>
  /// <param name="category">Категория элемента, по которой необходимо извлечь элементы с панели инструментов</param>
  /// <returns>Набор ToolboxItemCollection, содержащий текущие элементы панели инструментов, которые связаны с указанными узлом конструктора и категорией</returns>
  public ToolboxItemCollection GetToolboxItems(string category)
  {
    List<IMToolBoxItem> imToolBoxItemList = new List<IMToolBoxItem>();
    DesignerToolBoxTab designerToolBoxTab = this._tabCollection.FirstOrDefault<DesignerToolBoxTab>((Func<DesignerToolBoxTab, bool>) (x => x.Category == category));
    if (designerToolBoxTab != null)
      imToolBoxItemList = designerToolBoxTab.Items;
    return new ToolboxItemCollection((ToolboxItem[]) imToolBoxItemList.ToArray());
  }

  /// <summary>
  /// Получает набор элементов панели инструментов, которые связаны с указанными узлом конструктора и категорией, с панели инструментов.
  /// </summary>
  /// <param name="category">Категория элемента, по которой необходимо извлечь элементы с панели инструментов</param>
  /// <param name="host">IDesignerHost, который связан с извлекаемыми элементами панели инструментов</param>
  /// <returns>Набор ToolboxItemCollection, содержащий текущие элементы панели инструментов, которые связаны с указанными узлом конструктора и категорией</returns>
  public ToolboxItemCollection GetToolboxItems(string category, IDesignerHost host)
  {
    return this.GetToolboxItems(category);
  }

  /// <summary>
  /// Получает значение, показывающее соответствует ли указанный объект, который представляет сериализованный элемент панели инструментов, указанным атрибутам.
  /// </summary>
  /// <param name="serializedObject">Объект, содержащий ToolboxItem, который требуется извлечь</param>
  /// <param name="filterAttributes">ICollection, содержащий атрибуты для проверки сериализованных объектов</param>
  /// <returns>true, если указанный объект совместим с указанным узлом конструктора; в противном случае — false</returns>
  public bool IsSupported(object serializedObject, ICollection filterAttributes) => false;

  /// <summary>
  /// Получает значение, показывающее соответствует ли указанный объект, который представляет сериализованный элемент панели инструментов, указанным атрибутам.
  /// </summary>
  /// <param name="serializedObject">Объект, содержащий ToolboxItem, который требуется извлечь</param>
  /// <param name="host">IDesignerHost, который проверяется на предмет поддержки ToolboxItem</param>
  /// <returns>true, если указанный объект совместим с указанным узлом конструктора; в противном случае — false</returns>
  public bool IsSupported(object serializedObject, IDesignerHost host) => false;

  /// <summary>
  /// Получает значение, показывающее является ли указанный объект сериализованным элементом панели инструментов, используя указанный узел конструктора.
  /// </summary>
  /// <param name="serializedObject">Проверяемый объект</param>
  /// <returns>true, если объект содержит объект элемента панели инструментов; в противном случае — false</returns>
  public bool IsToolboxItem(object serializedObject)
  {
    return serializedObject is IDataObject dataObject && dataObject.GetDataPresent(this._localType);
  }

  /// <summary>
  /// Получает значение, показывающее является ли указанный объект сериализованным элементом панели инструментов, используя указанный узел конструктора.
  /// </summary>
  /// <param name="serializedObject">Проверяемый объект</param>
  /// <param name="host">IDesignerHost, создающий этот запрос</param>
  /// <returns>true, если объект содержит объект элемента панели инструментов; в противном случае — false</returns>
  public bool IsToolboxItem(object serializedObject, IDesignerHost host)
  {
    bool flag = false;
    if (host != null && serializedObject is IDataObject dataObject && dataObject.GetDataPresent(this._localType))
      flag = dataObject.GetData(this._localType) is ToolboxItem data && this.GetToolboxItems().Contains(data);
    return flag;
  }

  /// <summary>
  /// Удаляет ранее добавленный разработчик, который связан с указанным форматированием данных и определенным узлом конструктора.
  /// </summary>
  /// <param name="format">Формат данных разработчика, которые требуется удалить</param>
  public void RemoveCreator(string format)
  {
  }

  /// <summary>
  /// Удаляет ранее добавленный разработчик, который связан с указанным форматированием данных и определенным узлом конструктора.
  /// </summary>
  /// <param name="format">Формат данных разработчика, которые требуется удалить</param>
  /// <param name="host">IDesignerHost, который связан с удаляемым разработчиком</param>
  public void RemoveCreator(string format, IDesignerHost host)
  {
  }

  /// <summary>Удаляет определенный элемент с панели инструментов.</summary>
  /// <param name="toolboxItem">ToolboxItem, который нужно удалить с панели инструментов</param>
  public void RemoveToolboxItem(ToolboxItem toolboxItem)
  {
    this.RemoveToolboxItem(toolboxItem, (toolboxItem as IMToolBoxItem).ItemCategory);
  }

  /// <summary>Удаляет определенный элемент с панели инструментов.</summary>
  /// <param name="toolboxItem">ToolboxItem, который нужно удалить с панели инструментов</param>
  /// <param name="category">Категория элемента панели инструментов, из которой необходимо удалить ToolboxItem</param>
  public void RemoveToolboxItem(ToolboxItem toolboxItem, string category)
  {
    if (!(toolboxItem is IMToolBoxItem imToolBoxItem))
      return;
    this._tabCollection.FirstOrDefault<DesignerToolBoxTab>((Func<DesignerToolBoxTab, bool>) (x => x.Category == category))?.RemoveItem(imToolBoxItem);
  }

  /// <summary>
  /// Сообщает службе обслуживания панели инструментов, что выбранное средство используется.
  /// </summary>
  public void SelectedToolboxItemUsed() => this.OnKeyDown(new KeyEventArgs(Keys.Escape));

  /// <summary>
  /// Получает сериализованный объект, который представляет выбранный элемент панели инструментов.
  /// </summary>
  /// <param name="toolboxItem">ToolboxItem, который нужно сериализовать</param>
  /// <returns>Объект, представляющий определенный ToolboxItem</returns>
  public object SerializeToolboxItem(ToolboxItem toolboxItem)
  {
    return (object) new DataObject((object) toolboxItem);
  }

  /// <summary>
  /// Устанавливает курсор текущего приложения в курсор, представляющий текущее выбранное средство.
  /// </summary>
  /// <returns>true, если курсор установлен с помощью текущего выбранного средства, false если средство не выбрано и курсор является стандартным курсором windows</returns>
  public bool SetCursor()
  {
    bool flag = false;
    if (this._selectedItem != null)
    {
      Cursor.Current = Cursors.Cross;
      flag = true;
    }
    return flag;
  }

  /// <summary>Выбирает определенный элемент панели инструментов.</summary>
  /// <param name="toolboxItem">ToolboxItem, который нужно выбрать</param>
  public void SetSelectedToolboxItem(ToolboxItem toolboxItem)
  {
  }

  /// <summary>
  /// Освобождает неуправляемые ресурсы, используемые объектом Component, а также может дополнительно освободить управляемые ресурсы.
  /// </summary>
  /// <param name="disposing">Если этот параметр равен true, освобождаются как управляемые, так и неуправляемые ресурсы; если он равен false, освобождаются только неуправляемые ресурсы.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._host = (IDesignerHost) null;
      this.RemoveAll();
      this._bmpPointer.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="keyData"></param>
  /// <returns></returns>
  protected override bool IsInputKey(Keys keyData)
  {
    bool flag = true;
    if (keyData != Keys.Down && keyData != Keys.Up && keyData != Keys.Right && keyData != Keys.Left && keyData != Keys.Tab && keyData != (Keys.Tab | Keys.Shift))
      flag = base.IsInputKey(keyData);
    return flag;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnDoubleClick(EventArgs e)
  {
    if (this._selectedItem == null)
      return;
    base.OnDoubleClick(e);
    if (this._host.GetDesigner(this._host.RootComponent) is IToolboxUser designer)
      designer.ToolPicked((ToolboxItem) this._selectedItem);
    DesignerToolBoxTab designerToolBoxTab = this._tabCollection.FirstOrDefault<DesignerToolBoxTab>((Func<DesignerToolBoxTab, bool>) (x => x.Category == this.SelectedCategory));
    designerToolBoxTab.SetPointerSelected();
    this._selectedItem = designerToolBoxTab.SelectedItem;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    int tabsHeight = 0;
    this._tabCollection.ForEach((Action<DesignerToolBoxTab>) (x => tabsHeight += x.TabBounds.Height));
    this.AutoScrollMinSize = new Size(1, tabsHeight);
    Rectangle clipRect = new Rectangle(0, this.VScroll ? this.AutoScrollPosition.Y : 0, this.ClientSize.Width, this.Height);
    e = new PaintEventArgs(e.Graphics, clipRect);
    foreach (DesignerToolBoxTab tab in this._tabCollection)
    {
      tab.TabPaint(e);
      tab.DrawPointer(e.Graphics, this._bmpPointer);
      int height1 = this.Height;
      Rectangle tabBounds = tab.TabBounds;
      int bottom1 = tabBounds.Bottom;
      if (height1 <= bottom1)
        break;
      ref Rectangle local1 = ref clipRect;
      tabBounds = tab.TabBounds;
      int bottom2 = tabBounds.Bottom;
      local1.Y = bottom2;
      ref Rectangle local2 = ref clipRect;
      int height2 = this.Height;
      tabBounds = tab.TabBounds;
      int bottom3 = tabBounds.Bottom;
      int num = height2 - bottom3;
      local2.Height = num;
      e = new PaintEventArgs(e.Graphics, clipRect);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnKeyDown(KeyEventArgs e)
  {
    base.OnKeyDown(e);
    if (e.KeyData == Keys.Return)
      this.OnDoubleClick(new EventArgs());
    else if (e.KeyData == Keys.Divide)
      this._tabCollection.ForEach((Action<DesignerToolBoxTab>) (x => x.Collaps()));
    else if (e.KeyData == Keys.Multiply)
      this._tabCollection.ForEach((Action<DesignerToolBoxTab>) (x => x.Expand()));
    else if (!string.IsNullOrEmpty(this.SelectedCategory))
    {
      DesignerToolBoxTab designerToolBoxTab = this._tabCollection.FirstOrDefault<DesignerToolBoxTab>((Func<DesignerToolBoxTab, bool>) (x => x.Category == this.SelectedCategory));
      if (e.KeyData == Keys.Down)
      {
        int num1 = this._tabCollection.IndexOf(designerToolBoxTab);
        if (!designerToolBoxTab.TabKeyDown(num1 < this._tabCollection.Count - 1) && num1 < this._tabCollection.Count)
        {
          int num2;
          designerToolBoxTab = this._tabCollection[num2 = num1 + 1];
          designerToolBoxTab.TabKeyDown(true);
          this.SelectedCategory = designerToolBoxTab.Category;
        }
      }
      else if (e.KeyData == Keys.Up)
      {
        int num3 = this._tabCollection.IndexOf(designerToolBoxTab);
        if (!designerToolBoxTab.TabKeyUp(num3 > 0) && num3 > 0)
        {
          int num4;
          designerToolBoxTab = this._tabCollection[num4 = num3 - 1];
          designerToolBoxTab.TabKeyUp(true);
          this.SelectedCategory = designerToolBoxTab.Category;
        }
      }
      else if (e.KeyData == Keys.Left)
        designerToolBoxTab.TabKeyLeft();
      else if (e.KeyData == Keys.Right)
        designerToolBoxTab.TabKeyRight();
      else if (e.KeyData == Keys.Escape)
        designerToolBoxTab.CancelSelected();
      else if (e.KeyData == Keys.Tab)
        designerToolBoxTab.Tab();
      else if (e.KeyData == (Keys.Tab | Keys.Shift))
        designerToolBoxTab.ShiftTab();
      this._selectedItem = designerToolBoxTab.SelectedItem;
    }
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseDown(MouseEventArgs e)
  {
    base.OnMouseDown(e);
    DesignerToolBoxTab designerToolBoxTab = this._tabCollection.FirstOrDefault<DesignerToolBoxTab>((Func<DesignerToolBoxTab, bool>) (x => x.TabBounds.Contains(e.Location)));
    if (designerToolBoxTab != null)
    {
      if (!string.IsNullOrEmpty(this.SelectedCategory))
        this._tabCollection.FirstOrDefault<DesignerToolBoxTab>((Func<DesignerToolBoxTab, bool>) (x => x.Category == this.SelectedCategory))?.ClearSelected();
      designerToolBoxTab.TabMouseClick(e);
      this.SelectedCategory = designerToolBoxTab.Category;
      this._selectedItem = designerToolBoxTab.SelectedItem;
    }
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseLeave(EventArgs e)
  {
    base.OnMouseLeave(e);
    this._tabCollection.ForEach((Action<DesignerToolBoxTab>) (x => x.ClearHovered()));
    this.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseMove(MouseEventArgs e)
  {
    base.OnMouseMove(e);
    if (e.Button == MouseButtons.Left)
    {
      if (this._selectedItem == null)
        return;
      DataObject data = this.SerializeToolboxItem((ToolboxItem) this._selectedItem) as DataObject;
      try
      {
        int num = (int) this._tabCollection.FirstOrDefault<DesignerToolBoxTab>((Func<DesignerToolBoxTab, bool>) (x => x.Category == this.SelectedCategory)).DoDragDrop((object) data, DragDropEffects.Copy);
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
      }
    }
    else
    {
      foreach (DesignerToolBoxTab tab in this._tabCollection)
      {
        if (tab.TabBounds.Contains(e.Location))
          tab.TabMouseMove(e);
        else
          tab.ClearHovered();
      }
      this.Invalidate();
    }
  }

  /// <summary>Удаляет все закладки.</summary>
  public void RemoveAll()
  {
    this.SelectedCategory = string.Empty;
    this._selectedItem = (IMToolBoxItem) null;
    if (this._tabCollection == null)
      return;
    foreach (Control tab in this._tabCollection)
      tab.QueryContinueDrag -= new QueryContinueDragEventHandler(this.OnTab_QueryContinueDrag);
    this._tabCollection.Clear();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnTab_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
  {
    if (!e.EscapePressed)
      return;
    e.Action = DragAction.Cancel;
    this.OnKeyDown(new KeyEventArgs(Keys.Escape));
  }
}
