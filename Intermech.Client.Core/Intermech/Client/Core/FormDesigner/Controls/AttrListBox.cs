
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrListBox
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
public class AttrListBox : AttrListBoxBase
{
  /// <summary>
  /// Текущее действие по добавлению/редактированию элемента
  /// </summary>
  private AttrListBox.Action _action;
  private bool _loaded;
  private AttrTextEdit _txt;
  private Color _lstForeColor = SystemColors.WindowText;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Отступы от краев в элементе управления.</summary>
  /// <remarks>Здесь нужно только для того, чтобы запреить сериализацию</remarks>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new Padding Padding
  {
    get => base.Padding;
    private set => base.Padding = value;
  }

  /// <summary>Конструктор.</summary>
  public AttrListBox()
  {
    this.InitializeComponent();
    this.Name = string.Empty;
    this.MenuItemClick += new EventHandler(this.On_btnAddEdit_Click);
  }

  /// <summary>Добавить/редактировать элемент.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnAddEdit_Click(object sender, EventArgs e)
  {
    if (this.AttributeInfo == null || this._attrValues == null)
      return;
    this.Error = string.Empty;
    int int32 = Convert.ToInt32(sender is ToolStripMenuItem toolStripMenuItem ? toolStripMenuItem.Tag : (sender as ControlButton).Tag);
    if (this._describer != null && this._describer.GetPropDescriptorEditor(this._attrValues.AttributeID) is UITypeEditor descriptorEditor)
    {
      using (ServiceContainer provider = new ServiceContainer())
      {
        using (DropDownEditorForm serviceInstance = new DropDownEditorForm())
        {
          provider.AddService(typeof (IWindowsFormsEditorService), (object) serviceInstance);
          ITypeDescriptorContext context = (ITypeDescriptorContext) new ControlsContext(this.Values, this._describer, this.ParentInfo);
          switch (descriptorEditor.GetEditStyle(context))
          {
            case UITypeEditorEditStyle.Modal:
            case UITypeEditorEditStyle.DropDown:
              object obj = descriptorEditor.EditValue(context, (System.IServiceProvider) provider, int32 == 0 ? (object) null : this._lst.SelectedItem);
              if (obj == null || object.Equals(obj, this._lst.SelectedItem))
                break;
              object attributeValue = this._describer.GetAttributeValue(this.ParentInfo, this._attrValues.AttributeID, obj);
              bool flag = false;
              if (this._attrValues.AttributeType == FieldTypes.ftGuid)
              {
                if (attributeValue != null && attributeValue != DBNull.Value && attributeValue is Guid guid && guid != Guid.Empty)
                {
                  List<Guid> list = new List<Guid>(this._lst.Items.Count);
                  this.FillElementsList<Guid>(ref list);
                  if (list.Contains(guid) && (int32 == 0 || list.IndexOf(guid) != this._lst.SelectedIndex))
                  {
                    int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("FormDesigner_ListControls_ValueExist"), (object) Convert.ToString(obj)), LocalizationHolder.rm.GetString("FormDesigner_ListControls_DublicationValue"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                  }
                  else
                    flag = true;
                }
              }
              else if (this._attrValues.AttributeType == FieldTypes.ftInteger)
              {
                if (attributeValue != null && attributeValue != DBNull.Value && attributeValue is long)
                {
                  long int64 = Convert.ToInt64(attributeValue);
                  List<long> list = new List<long>(this._lst.Items.Count);
                  this.FillElementsList<long>(ref list);
                  if (list.Contains(int64) && (int32 == 0 || list.IndexOf(int64) != this._lst.SelectedIndex))
                  {
                    int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("FormDesigner_ListControls_ValueExist"), (object) Convert.ToString(obj)), LocalizationHolder.rm.GetString("FormDesigner_ListControls_DublicationValue"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                  }
                  else
                    flag = true;
                }
              }
              else
                flag = true;
              if (!flag)
                break;
              if (int32 == 0)
                this._lst.SelectedIndex = this._lst.Items.Add(obj);
              else
                this._lst.Items[this._lst.SelectedIndex] = obj;
              this.Modified = true;
              break;
          }
        }
      }
    }
    else
    {
      if (this._txt == null)
        this.CreateTextControl();
      AttributeValues attributeValues1;
      if (!this._loaded)
      {
        this._loaded = true;
        AttributeValues attributeValues2 = this._attrValues.Clone() as AttributeValues;
        attributeValues2.AttributeID = -1;
        attributeValues2.AttributeGuid = Guid.Empty;
        this._txt.AttributeInfo = this.AttributeInfo;
        attributeValues1 = attributeValues2;
      }
      else
        attributeValues1 = this._txt.Values;
      if (int32 == 0)
      {
        attributeValues1.Values = new object[1]
        {
          (object) DBNull.Value
        };
        this._action = AttrListBox.Action.Add;
      }
      else
      {
        AttributeValues attributeValues3 = attributeValues1;
        object[] objArray;
        if (this._describer == null)
          objArray = new object[1]{ this._lst.SelectedItem };
        else
          objArray = new object[1]
          {
            this._describer.GetAttributeValue(this.ParentInfo, this._attrValues.AttributeID, this._lst.SelectedItem)
          };
        attributeValues3.Values = objArray;
        this._action = AttrListBox.Action.Edit;
      }
      this._txt.LockModify = true;
      this._txt.Values = attributeValues1;
      this._txt.EnabledCtrl = true;
      this.SuspendLayout();
      try
      {
        this.EditingControl_VisibleChanged();
        this._buttons.Enabled = false;
        this.Controls.Add((Control) this._txt);
        this._txt.Focus();
      }
      finally
      {
        this.ResumeLayout();
      }
    }
  }

  /// <summary>Нажатие клавиши клавиатуры.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_KeyDown(object sender, KeyEventArgs e)
  {
    if (this._attrValues == null)
      return;
    if (e.KeyCode == Keys.Escape)
    {
      this._action = AttrListBox.Action.None;
      this._lst.Focus();
      this.CheckAccessibilityButtons();
    }
    else
    {
      if (e.KeyCode != Keys.Return)
        return;
      this._lst.Focus();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_Leave(object sender, EventArgs e)
  {
    if (this._attrValues == null)
      return;
    object actualValue = this._txt.Values.Values[0];
    if (this._action == AttrListBox.Action.Add)
    {
      if (actualValue != null && actualValue != DBNull.Value)
      {
        object obj1 = (object) null;
        if (this._describer != null)
          obj1 = this._describer.GetPropDescriptorValue(this.ParentInfo, this._attrValues.AttributeID, actualValue);
        object obj2 = obj1 ?? actualValue;
        if (!this.IsValueExist(Convert.ToString(obj2), -1))
        {
          this._lst.SelectedIndex = this._lst.Items.Add(obj2);
          this.Modified = true;
        }
      }
      this.CheckAccessibilityButtons();
    }
    else if (this._action == AttrListBox.Action.Edit)
    {
      if (actualValue != null && actualValue != DBNull.Value)
      {
        object obj3 = (object) null;
        if (this._describer != null)
          obj3 = this._describer.GetPropDescriptorValue(this.ParentInfo, this._attrValues.AttributeID, actualValue);
        object obj4 = obj3 ?? actualValue;
        if (!this.IsValueExist(Convert.ToString(obj4), this._lst.SelectedIndex))
        {
          this._lst.Items[this._lst.SelectedIndex] = obj4;
          this.Modified = true;
        }
        this.CheckAccessibilityButtons();
      }
      else
        this.DeleteItem();
    }
    this._action = AttrListBox.Action.None;
    this._txt.LockModify = false;
    this.DisposeTextControl();
    this.EditingControl_VisibleChanged();
  }

  /// <summary>Значение атрибута.</summary>
  public override AttributeValues Values
  {
    get => base.Values;
    set
    {
      if (this._txt != null)
      {
        this._action = AttrListBox.Action.None;
        this._lst.Focus();
      }
      this._loaded = false;
      base.Values = value;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void CreateTextControl()
  {
    AttrTextEdit attrTextEdit = new AttrTextEdit();
    attrTextEdit.Dock = DockStyle.Top;
    this._txt = attrTextEdit;
    this._txt.TxtKeyDown += new KeyEventHandler(this.On_txt_KeyDown);
    this._txt.Leave += new EventHandler(this.On_txt_Leave);
  }

  /// <summary>
  /// 
  /// </summary>
  private void DisposeTextControl()
  {
    this.SuspendLayout();
    try
    {
      this.Controls.Remove((Control) this._txt);
      this._txt.TxtKeyDown -= new KeyEventHandler(this.On_txt_KeyDown);
      this._txt.Leave -= new EventHandler(this.On_txt_Leave);
      this._txt.Dispose();
      this._txt = (AttrTextEdit) null;
      this._loaded = false;
    }
    finally
    {
      this.ResumeLayout();
    }
  }

  /// <summary>Изменение видимости контрола редактирования.</summary>
  private void EditingControl_VisibleChanged()
  {
    if (this._txt != null)
    {
      this._lstBackColor = this._lst.BackColor;
      this._lstForeColor = this._lst.ForeColor;
      this._lst.BackColor = SystemColors.Control;
      this._lst.ForeColor = SystemColors.GrayText;
    }
    else
    {
      this._lst.BackColor = this._lstBackColor;
      this._lst.ForeColor = this._lstForeColor;
    }
  }

  /// <summary>
  /// Заполнить список guid'ов добавленных элементов.
  /// Список необходим для того, чтобы повторно не добавлять существующие элементы.
  /// Создавалась функция для случаев когда _describer != null
  /// </summary>
  /// <param name="list"></param>
  private void FillElementsList<T>(ref List<T> list)
  {
    if (this._describer == null)
      return;
    list = new List<T>(this._lst.Items.Count);
    foreach (object propertyValue in this._lst.Items)
    {
      object attributeValue = this._describer.GetAttributeValue(this.ParentInfo, this._attrValues.AttributeID, propertyValue);
      if (attributeValue != null && attributeValue != DBNull.Value && attributeValue is T obj && !list.Contains(obj))
        list.Add(obj);
    }
  }

  /// <summary>Проверка на существование значения.</summary>
  /// <param name="value">Проверяемое значение</param>
  /// <param name="itemIndex">Индекс добавляемого/редактируемого элемента</param>
  /// <returns>Результат проверки</returns>
  private bool IsValueExist(string value, int itemIndex)
  {
    bool flag = false;
    for (int index = 0; index < this._lst.Items.Count; ++index)
    {
      if (!(Convert.ToString(this._lst.Items[index]) != value) && index != itemIndex)
      {
        int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("FormDesigner_ListControls_ValueExist"), (object) value), LocalizationHolder.rm.GetString("FormDesigner_ListControls_DublicationValue"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        flag = true;
        break;
      }
    }
    return flag;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.MenuItemClick -= new EventHandler(this.On_btnAddEdit_Click);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AttrListBox));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (AttrListBox);
    this.Controls.SetChildIndex((Control) this._lst, 0);
    this.ResumeLayout(false);
  }

  /// <summary>
  /// Действия, выполняемые пользователем по добавлению/редактированию элементов.
  /// </summary>
  private enum Action
  {
    None,
    Add,
    Edit,
  }
}
