
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrListBoxBase
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
public class AttrListBoxBase : AttrsControl, IFormDesignerControl
{
  protected IAttributePropertyDescriber _describer;
  private ControlButton _btnAdd;
  private ControlButton _btnDel;
  private ControlButton _btnEdit;
  private ControlButton _btnClear;
  private object _tmpItem;
  protected Color _lstBackColor = Color.White;
  private bool _isSubscribeOnTabPageParentChanged;
  private IFormDesignerControl _parent;
  private EventHandler _formDeactivate;
  private EventHandler _loadDataCompleted;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected ListBox _lst;
  protected ToolStripMenuItem _miAdd;
  protected ToolStripMenuItem _miDel;
  protected ToolStripMenuItem _miEdit;
  protected ToolStripMenuItem _miClear;
  protected ContextMenuStrip _menu;

  /// <summary>Цвет фона элемента управления.</summary>
  [DefaultValue(typeof (Color), "Window")]
  public new Color BackColor
  {
    get => this._lst.BackColor;
    set => this._lstBackColor = this._lst.BackColor = value;
  }

  /// <summary>Вид обрамления для элемента управления.</summary>
  [DefaultValue(BorderStyle.Fixed3D)]
  public new BorderStyle BorderStyle
  {
    get => this._lst.BorderStyle;
    set => this._lst.BorderStyle = value;
  }

  /// <summary>Шрифт текста, отображаемый элементом управления.</summary>
  public new Font Font
  {
    get => base.Font;
    set => base.Font = value;
  }

  /// <summary>
  /// Основной цвет элемента управления, который используется для отображаемого текста.
  /// </summary>
  [DefaultValue(typeof (Color), "WindowText")]
  public new Color ForeColor
  {
    get => this._lst.ForeColor;
    set => this._lst.ForeColor = value;
  }

  /// <summary>Текстовая подсказка.</summary>
  [DefaultValue("")]
  public string Hint
  {
    get => this._toolTip.GetToolTip((Control) this._lst);
    set => this._toolTip.SetToolTip((Control) this._lst, value);
  }

  /// <summary>
  /// Отображение горизонтальной линейки прокрутки в элементе управления.
  /// </summary>
  [DefaultValue(false)]
  public bool HorizontalScrollbar
  {
    get => this._lst.HorizontalScrollbar;
    set => this._lst.HorizontalScrollbar = value;
  }

  /// <summary>
  /// Возвращает или задает значение, показывающее, упорядочены ли позиции в элементе управления по алфавиту.
  /// </summary>
  [DefaultValue(false)]
  public bool Sorted
  {
    get => this._lst.Sorted;
    set => this._lst.Sorted = value;
  }

  /// <summary>Получение текущего набора значений атрибута.</summary>
  protected override object[] GetValues
  {
    get
    {
      object[] getValues;
      if (this._lst.Items.Count == 0)
      {
        getValues = new object[1]{ (object) DBNull.Value };
      }
      else
      {
        List<object> objectList = new List<object>();
        if (this._describer != null)
        {
          foreach (object propertyValue in this._lst.Items)
          {
            object attributeValue = this._describer.GetAttributeValue(this.ParentInfo, this._attrValues.AttributeID, propertyValue);
            if (attributeValue != null && attributeValue != DBNull.Value)
              objectList.Add(attributeValue);
          }
        }
        else
        {
          foreach (object obj in this._lst.Items)
          {
            object forAttributeValues = this.GetItemForAttributeValues(obj);
            if (forAttributeValues != DBNull.Value && forAttributeValues != null)
              objectList.Add(forAttributeValues);
          }
        }
        object[] objArray;
        if (objectList.Count != 0)
          objArray = objectList.ToArray();
        else
          objArray = new object[1]{ (object) DBNull.Value };
        getValues = objArray;
      }
      return getValues;
    }
  }

  /// <summary>Наличие Descriptor'а у атрибута.</summary>
  /// <remark>Необходимость в свойстве появилась в следующем случае:
  /// При связывании атрибута с контролом необходимо выставить доступнонсть редактирования атрибута.
  /// Если у атрибута свойство "Запрет редактирования в ручную" = "Да", необходимо запретить редактирование атрибута с помощью контрола.
  /// НО!!! Если значение можно не ввести с клавиатуры, а выбрать из списка, то необходимо разрешить модификацию атрибута,
  /// несмотря на запрет.
  /// С помощью Descriptor'а можно значение выбирать из списка, следовательно перед тем как присваивать значение свойству Enabled,
  /// необходимо проверить наличие Descriptor'а</remark>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  internal bool HasDescriptor => this._describer != null;

  /// <summary>
  /// 
  /// </summary>
  protected override bool ValueIsEmpty
  {
    get
    {
      bool valueIsEmpty = true;
      if (this._attrValues != null)
      {
        object[] values = this._attrValues.Values;
        if (values != null && values.Length != 0)
        {
          object obj = values[0];
          valueIsEmpty = values.Length == 1 && (obj == null || obj == DBNull.Value);
        }
      }
      return valueIsEmpty;
    }
  }

  /// <summary>Возможность контрола иметь дочерние контролы.</summary>
  public bool CanContainsChildren { get; private set; }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnTabPage_ParentChanged(object sender, EventArgs e)
  {
    if (!(sender is TabPage))
      return;
    this.Unsubscribe();
    this.SubscribeLoadData(this.Parent);
    this.SubscribeFormDeactivate(this.Parent);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnParentChanged(EventArgs e)
  {
    base.OnParentChanged(e);
    this.Unsubscribe();
    this.SubscribeLoadData(this.Parent);
    this.SubscribeFormDeactivate(this.Parent);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="parent"></param>
  private void SubscribeLoadData(Control parent)
  {
    switch (parent)
    {
      case IFormDesignerControl formDesignerControl:
        if (this._parent == null)
          this._parent = formDesignerControl;
        this._parent.LoadDataCompleted += new EventHandler(this.OnLoadDataCompleted);
        break;
      case TabPage tabPage:
        if (tabPage.Parent == null)
        {
          if (this._isSubscribeOnTabPageParentChanged)
            break;
          tabPage.ParentChanged += new EventHandler(this.OnTabPage_ParentChanged);
          this._isSubscribeOnTabPageParentChanged = true;
          break;
        }
        this.SubscribeLoadData(tabPage.Parent);
        break;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void Unsubscribe()
  {
    if (this._parent == null)
      return;
    this._parent.LoadDataCompleted -= new EventHandler(this.OnLoadDataCompleted);
    this._parent.FormDeactivate -= new EventHandler(this.OnFormDeactivate);
    this._parent = (IFormDesignerControl) null;
    this._isSubscribeOnTabPageParentChanged = false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="parent"></param>
  private void SubscribeFormDeactivate(Control parent)
  {
    switch (parent)
    {
      case IFormDesignerControl formDesignerControl:
        if (this._parent == null)
          this._parent = formDesignerControl;
        this._parent.FormDeactivate += new EventHandler(this.OnFormDeactivate);
        break;
      case TabPage tabPage:
        if (tabPage.Parent == null)
        {
          if (this._isSubscribeOnTabPageParentChanged)
            break;
          tabPage.ParentChanged += new EventHandler(this.OnTabPage_ParentChanged);
          this._isSubscribeOnTabPageParentChanged = true;
          break;
        }
        this.SubscribeFormDeactivate(tabPage.Parent);
        break;
    }
  }

  private void OnFormDeactivate(object sender, EventArgs e)
  {
  }

  /// <summary>Данные загружены.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnLoadDataCompleted(object sender, EventArgs e)
  {
  }

  /// <summary>Событие, возникающее при деактивации вьюшки.</summary>
  /// <remark>
  /// Событие исходит от формы.
  /// Но на событие должны давать возможность подписываться только контролы, которые могут быть контейнерами контролов.
  /// Необходимость возникла из-за случая, когда во время деактивации вьюшки нужно провести деактивацию контрола.
  /// Поэтому, если контрол лежит на форме, то он получает сообщение от самой формы, а если контрол лежит на другом контроле, то он получает сообщение от родителя, а родитель в итоге от формы.
  /// </remark>
  public event EventHandler FormDeactivate
  {
    add
    {
      if (!this.CanContainsChildren)
        return;
      this._formDeactivate += value;
    }
    remove
    {
      if (!this.CanContainsChildren)
        return;
      this._formDeactivate -= value;
    }
  }

  /// <summary>Загрузка данных завершена.</summary>
  public event EventHandler LoadDataCompleted
  {
    add
    {
      if (!this.CanContainsChildren)
        return;
      this._loadDataCompleted += value;
    }
    remove
    {
      if (!this.CanContainsChildren)
        return;
      this._loadDataCompleted -= value;
    }
  }

  /// <summary>Конструктор.</summary>
  public AttrListBoxBase()
  {
    this.InitializeComponent();
    this.CanContainsChildren = false;
    base.BackColor = Color.Transparent;
    this._btnAdd = new ControlButton("Add", 3)
    {
      Enabled = false,
      Tag = (object) 0
    };
    this._btnAdd.Click += new EventHandler(this.OnAddEdit_Click);
    this._btnDel = new ControlButton("Del", 4)
    {
      Enabled = false
    };
    this._btnDel.Click += new EventHandler(this.OnDel_Click);
    this._btnEdit = new ControlButton("Edit", 5)
    {
      Enabled = false,
      Tag = (object) 1
    };
    this._btnEdit.Click += new EventHandler(this.OnAddEdit_Click);
    this._btnClear = new ControlButton("Clean", 6)
    {
      Enabled = false
    };
    this._btnClear.Click += new EventHandler(this.OnClear_Click);
    this.AddTopButtons(new List<ControlButton>()
    {
      this._btnAdd,
      this._btnDel,
      this._btnEdit,
      this._btnClear
    });
  }

  /// <summary>
  /// 
  /// </summary>
  protected event EventHandler MenuItemClick;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_lst_DoubleClick(object sender, EventArgs e)
  {
    if (!this.EnabledCtrl || this._lst.SelectedIndex <= -1)
      return;
    this.OnAddEdit_Click((object) this._btnEdit, e);
  }

  /// <summary>Нажатие клавиши клавиатуры.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected void On_lst_KeyDown(object sender, KeyEventArgs e)
  {
    if (this._attrValues == null || e.KeyCode != Keys.Delete)
      return;
    this.DeleteItem();
  }

  /// <summary>Изменение индекса выделенного элементна.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_lst_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.CheckAccessibilityButtons();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnAttrListBoxBase_Enter(object sender, EventArgs e)
  {
    if (this._tmpItem == null)
      return;
    this._lst.SelectedItem = this._tmpItem;
    this._tmpItem = (object) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnAddEdit_Click(object sender, EventArgs e)
  {
    if (this.MenuItemClick == null)
      return;
    this.MenuItemClick(sender, e);
  }

  /// <summary>Удалить элемент.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnDel_Click(object sender, EventArgs e) => this.DeleteItem();

  /// <summary>Очистить список элементов.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnClear_Click(object sender, EventArgs e) => this.ClearItems();

  /// <summary>Значение атрибута.</summary>
  public override AttributeValues Values
  {
    get => base.Values;
    set
    {
      base.Values = value;
      this._lst.Items.Clear();
      this._lst.SelectedIndex = -1;
      this._describer = (IAttributePropertyDescriber) null;
      try
      {
        if (this._attrValues == null)
          return;
        this._miAdd.Enabled = this._menu.Enabled = true;
        this._describer = !(ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) is IAttributePropertyDescriberService service) || this.IsDesignMode ? (IAttributePropertyDescriber) null : service.GetDescriber(this._attrValues.AttributeID);
        if (this.ValueIsEmpty)
          return;
        if (this._describer != null && this.ParentInfo != null)
        {
          foreach (object actualValue in this._attrValues.Values)
            this._lst.Items.Add(this._describer.GetPropDescriptorValue(this.ParentInfo, this._attrValues.AttributeID, actualValue) ?? this.CreateItemForListBox(actualValue));
        }
        else
        {
          foreach (object obj in this._attrValues.Values)
            this._lst.Items.Add(this.CreateItemForListBox(obj));
        }
      }
      finally
      {
        this.CheckAccessibilityButtons();
      }
    }
  }

  /// <summary>Доступность контрола.</summary>
  public override bool EnabledCtrl
  {
    get => this._enabled;
    set
    {
      this._enabled = value;
      this.CheckAccessibilityButtons();
      if (this.IsDesignMode)
        return;
      if (!value)
      {
        Color color = this._lst.BackColor;
        int argb1 = color.ToArgb();
        color = Color.White;
        int argb2 = color.ToArgb();
        if (argb1 != argb2)
          return;
        this._lst.BackColor = SystemColors.Control;
      }
      else
      {
        if (!(this._lst.BackColor == SystemColors.Control))
          return;
        this._lst.BackColor = Color.White;
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLeaveControl(EventArgs e)
  {
    this._tmpItem = this._lst.SelectedItem;
    this._lst.SelectedItem = (object) null;
    base.OnLeaveControl(e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="text"></param>
  protected override void SetDesignText(string text)
  {
    base.SetDesignText(text);
    if (!string.IsNullOrEmpty(text))
    {
      this._lst.Items.Clear();
      this._lst.Items.Add((object) text);
    }
    else
    {
      if (this._lst.Items.Count != 1)
        return;
      this._lst.Items.RemoveAt(0);
    }
  }

  /// <summary>Проверка доступности кнопок и пунктов меню.</summary>
  protected virtual void CheckAccessibilityButtons()
  {
    if (this._enabled)
    {
      this._btnAdd.Enabled = this._miAdd.Enabled = true;
      if (!this.DesignMode)
      {
        if (this._lst.Items.Count == 0)
        {
          this._btnEdit.Enabled = this._btnDel.Enabled = this._btnClear.Enabled = false;
          this._miEdit.Enabled = this._miDel.Enabled = this._miClear.Enabled = false;
          this.Error = !this._disableNulls || !this.EnabledCtrl ? string.Empty : this._errMsg_NullValue;
        }
        else
        {
          if (this._lst.SelectedIndex == -1)
          {
            this._btnEdit.Enabled = this._btnDel.Enabled = false;
            this._btnClear.Enabled = true;
            this._miEdit.Enabled = this._miDel.Enabled = false;
            this._miClear.Enabled = true;
          }
          else
          {
            this._btnDel.Enabled = this._btnClear.Enabled = true;
            this._miDel.Enabled = this._miClear.Enabled = true;
            this._btnEdit.Enabled = this._miEdit.Enabled = this._lst.SelectedIndices.Count == 1;
          }
          this.Error = string.Empty;
        }
      }
    }
    else
    {
      this._btnAdd.Enabled = this._btnEdit.Enabled = this._btnDel.Enabled = this._btnClear.Enabled = false;
      this._miAdd.Enabled = this._miEdit.Enabled = this._miDel.Enabled = this._miClear.Enabled = false;
      this.Error = string.Empty;
    }
    this.Invalidate();
  }

  /// <summary>Создание нового элемента.</summary>
  /// <param name="value"></param>
  /// <returns></returns>
  protected virtual object CreateItemForListBox(object value) => value;

  /// <summary>Получение элемента.</summary>
  /// <param name="value">Элемент в списке элементов</param>
  /// <returns>Значение</returns>
  protected virtual object GetItemForAttributeValues(object value) => value;

  /// <summary>Удаление элемента.</summary>
  /// <returns></returns>
  protected bool DeleteItem()
  {
    bool flag = false;
    if (this._lst.SelectedIndex > -1)
    {
      string caption = LocalizationHolder.rm.GetString("AttrListBox_DeleteItem_Caption");
      if (MessageBox.Show(LocalizationHolder.rm.GetString("AttrListBox_DeleteItem_Message"), caption, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != DialogResult.No)
      {
        for (int index = this._lst.SelectedIndices.Count - 1; index >= 0; --index)
          this._lst.Items.RemoveAt(this._lst.SelectedIndices[index]);
        this.CheckAccessibilityButtons();
        this.Modified = flag = true;
      }
    }
    return flag;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected bool ClearItems()
  {
    bool flag = false;
    string caption = LocalizationHolder.rm.GetString("AttrListBox_ClearList_Caption");
    if (MessageBox.Show(LocalizationHolder.rm.GetString("AttrListBox_ClearList_Message"), caption, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != DialogResult.No)
    {
      this._lst.SelectedIndex = -1;
      this._lst.Items.Clear();
      this.CheckAccessibilityButtons();
      this.Modified = flag = true;
    }
    return flag;
  }

  /// <summary>Необходимость сериализации свойства Font.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeFont()
  {
    return this.Parent != null && !this.Parent.Font.Equals((object) this.Font);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.Enter -= new EventHandler(this.OnAttrListBoxBase_Enter);
      this._lst.SelectedIndexChanged -= new EventHandler(this.On_lst_SelectedIndexChanged);
      this._lst.DoubleClick -= new EventHandler(this.On_lst_DoubleClick);
      this._lst.KeyDown -= new KeyEventHandler(this.On_lst_KeyDown);
      this._miAdd.Click -= new EventHandler(this.OnAddEdit_Click);
      this._miDel.Click -= new EventHandler(this.OnDel_Click);
      this._miEdit.Click -= new EventHandler(this.OnAddEdit_Click);
      this._miClear.Click -= new EventHandler(this.OnClear_Click);
      if (this._btnAdd != null)
        this._btnAdd.Click -= new EventHandler(this.OnAddEdit_Click);
      if (this._btnDel != null)
        this._btnDel.Click -= new EventHandler(this.OnDel_Click);
      if (this._btnEdit != null)
        this._btnEdit.Click -= new EventHandler(this.OnAddEdit_Click);
      if (this._btnClear != null)
        this._btnClear.Click -= new EventHandler(this.OnClear_Click);
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AttrListBoxBase));
    this._menu = new ContextMenuStrip(this.components);
    this._miAdd = new ToolStripMenuItem();
    this._miDel = new ToolStripMenuItem();
    this._miEdit = new ToolStripMenuItem();
    this._miClear = new ToolStripMenuItem();
    this._lst = new ListBox();
    ((ISupportInitialize) this._err).BeginInit();
    this._menu.SuspendLayout();
    this.SuspendLayout();
    this._menu.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this._miAdd,
      (ToolStripItem) this._miDel,
      (ToolStripItem) this._miEdit,
      (ToolStripItem) this._miClear
    });
    this._menu.Name = "_menu";
    componentResourceManager.ApplyResources((object) this._menu, "_menu");
    componentResourceManager.ApplyResources((object) this._miAdd, "_miAdd");
    this._miAdd.Name = "_miAdd";
    this._miAdd.Tag = (object) "0";
    this._miAdd.Click += new EventHandler(this.OnAddEdit_Click);
    componentResourceManager.ApplyResources((object) this._miDel, "_miDel");
    this._miDel.Name = "_miDel";
    this._miDel.Click += new EventHandler(this.OnDel_Click);
    componentResourceManager.ApplyResources((object) this._miEdit, "_miEdit");
    this._miEdit.Name = "_miEdit";
    this._miEdit.Tag = (object) "1";
    this._miEdit.Click += new EventHandler(this.OnAddEdit_Click);
    componentResourceManager.ApplyResources((object) this._miClear, "_miClear");
    this._miClear.Name = "_miClear";
    this._miClear.Click += new EventHandler(this.OnClear_Click);
    this._lst.ContextMenuStrip = this._menu;
    componentResourceManager.ApplyResources((object) this._lst, "_lst");
    this._lst.FormattingEnabled = true;
    this._err.SetIconAlignment((Control) this._lst, (ErrorIconAlignment) componentResourceManager.GetObject("_lst.IconAlignment"));
    this._err.SetIconPadding((Control) this._lst, (int) componentResourceManager.GetObject("_lst.IconPadding"));
    this._lst.Name = "_lst";
    this._lst.SelectedIndexChanged += new EventHandler(this.On_lst_SelectedIndexChanged);
    this._lst.DoubleClick += new EventHandler(this.On_lst_DoubleClick);
    this._lst.KeyDown += new KeyEventHandler(this.On_lst_KeyDown);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._lst);
    this._err.SetIconAlignment((Control) this, (ErrorIconAlignment) componentResourceManager.GetObject("$this.IconAlignment"));
    this._err.SetIconPadding((Control) this, (int) componentResourceManager.GetObject("$this.IconPadding"));
    this.Name = nameof (AttrListBoxBase);
    this.Enter += new EventHandler(this.OnAttrListBoxBase_Enter);
    ((ISupportInitialize) this._err).EndInit();
    this._menu.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
