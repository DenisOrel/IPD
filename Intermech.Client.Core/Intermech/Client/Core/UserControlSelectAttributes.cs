
// Type: Intermech.Client.Core.UserControlSelectAttributes
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Interfaces;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>
///   UserControl на котором пользователь может отметить любой из атрибутов,
///   который может присутствовать у переданного набора связей и типов объектов.
/// </summary>
public class UserControlSelectAttributes : UserControl, IAttributesSelection
{
  private bool _allignButtonAllAttributesLeft;
  private IntList _addedRelationTypeIDs = new IntList();
  private IntList _addedObjectTypeIDs = new IntList();
  private AttributeDescriptorList _attributeDescriptorList = new AttributeDescriptorList();
  private HybridDictionary _attributeIDtoAttributeDescriptorHash = new HybridDictionary();
  private int _kDif;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _buttonUncheckAll;
  private Button _buttonCheckAll;
  private CheckedListBoxControl _checkedListBoxAttributes;
  private Button _buttonAllAttributes;
  private ToolTipController _toolTipController;

  public UserControlSelectAttributes() => this.InitializeComponent();

  /// <summary> Список идентификаторов типов связей, атрибуты которых уже были добавленны в список </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IntList AddedRelationTypeIDs => this._addedRelationTypeIDs;

  /// <summary> Список идентификаторов типов объектов, атрибуты которых уже были добавленны в список </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IntList AddedObjectTypeIDs => this._addedObjectTypeIDs;

  /// <summary> Показывать ли кнопку "Все атрибуты" </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(true)]
  public bool ShowButtonAllAttributes
  {
    get => this._buttonAllAttributes.Visible;
    set
    {
      if (this._buttonAllAttributes.Visible == value)
        return;
      if (value)
      {
        this._checkedListBoxAttributes.Height -= this._kDif;
        this._buttonCheckAll.Top -= this._kDif / 2;
        this._buttonUncheckAll.Top -= this._kDif / 2;
      }
      else
      {
        if (this._kDif == 0)
          this._kDif = this._buttonAllAttributes.Height + (this.ClientSize.Height - this._buttonAllAttributes.Bottom);
        this._checkedListBoxAttributes.Height += this._kDif;
        this._buttonCheckAll.Top += this._kDif / 2;
        this._buttonUncheckAll.Top += this._kDif / 2;
      }
      this._buttonAllAttributes.Visible = value;
      this._buttonAllAttributes.Enabled = value;
    }
  }

  protected override void OnResize(EventArgs e)
  {
    base.OnResize(e);
    this.UpdatePositions();
  }

  private void UpdatePositions()
  {
    this._buttonCheckAll.Left = this.Width - this._buttonCheckAll.Width - 4;
    this._buttonUncheckAll.Left = this.Width - this._buttonCheckAll.Width - 4;
    this._checkedListBoxAttributes.Width = this._buttonUncheckAll.Left - 4 - this._checkedListBoxAttributes.Left;
  }

  /// <summary> Докать ли кнопку "Все атрибуты слева UserControl" </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(false)]
  public bool AllignButtonAllAttributesLeft
  {
    get => this._allignButtonAllAttributesLeft;
    set
    {
      if (this._buttonAllAttributes == null || this._checkedListBoxAttributes == null)
        return;
      if (value)
      {
        this._buttonAllAttributes.Anchor |= AnchorStyles.Left;
        this._buttonAllAttributes.Location = new Point(this._checkedListBoxAttributes.Location.X, this._buttonAllAttributes.Location.Y);
      }
      else
      {
        this._buttonAllAttributes.Anchor ^= AnchorStyles.Left;
        this._buttonAllAttributes.Location = new Point(this._checkedListBoxAttributes.Location.X + this._checkedListBoxAttributes.Size.Width / 2 - this._buttonAllAttributes.Size.Width / 2, this._buttonAllAttributes.Location.Y);
      }
      this._allignButtonAllAttributesLeft = value;
    }
  }

  /// <summary> Добавить в список атрибуты, которые могут принадлежать некоторым типам связей </summary>
  /// <param name="relationTypeIDs"> Идентификаторы типов связей, атрибуты которых должны быть добавлены в список </param>
  public void AddRelationAttributes(int[] relationTypeIDs)
  {
    this._checkedListBoxAttributes.Items.BeginUpdate();
    try
    {
      foreach (int relationTypeId in relationTypeIDs)
      {
        switch (relationTypeId)
        {
          case -1:
          case 0:
            continue;
          default:
            if (!this._addedRelationTypeIDs.Contains((object) relationTypeId))
            {
              IntList typeAttributeIds = DBHelper.GetRelationTypeAttributeIDs(relationTypeId);
              if (typeAttributeIds != null && typeAttributeIds.Count > 0)
              {
                foreach (int num in (ArrayList) typeAttributeIds)
                {
                  switch (num)
                  {
                    case -1:
                    case 0:
                      continue;
                    default:
                      AttributeDescriptor attributeDescriptor1 = (AttributeDescriptor) this._attributeIDtoAttributeDescriptorHash[(object) num];
                      if (attributeDescriptor1 == null)
                      {
                        AttributeDescriptor attributeDescriptor2 = new AttributeDescriptor(num, true);
                        this._attributeDescriptorList.Add((object) attributeDescriptor2);
                        this._attributeIDtoAttributeDescriptorHash.Add((object) num, (object) attributeDescriptor2);
                        this._checkedListBoxAttributes.Items.Add((object) new CheckedListBoxItem((object) attributeDescriptor2, false));
                        attributeDescriptor2.CheckedListBoxItem = this._checkedListBoxAttributes.Items[this._checkedListBoxAttributes.Items.Count - 1];
                        continue;
                      }
                      attributeDescriptor1.IsRelationAttribute = true;
                      continue;
                  }
                }
              }
              this._addedRelationTypeIDs.Add((object) relationTypeId);
              continue;
            }
            continue;
        }
      }
    }
    finally
    {
      this._checkedListBoxAttributes.Items.EndUpdate();
    }
  }

  /// <summary> Добавить в список атрибуты, которые могут принадлежать некоторым типам объектов </summary>
  /// <param name="objectTypeIDs"> Идентификаторы типов объектов, атрибуты которых должны быть добавлены в список </param>
  public void AddObjectAttributes(int[] objectTypeIDs)
  {
    this._checkedListBoxAttributes.Items.BeginUpdate();
    try
    {
      foreach (int objectTypeId in objectTypeIDs)
      {
        switch (objectTypeId)
        {
          case -1:
          case 0:
            continue;
          default:
            if (!this._addedObjectTypeIDs.Contains((object) objectTypeId))
            {
              IntList typeAttributeIds = DBHelper.GetObjTypeAttributeIDs(objectTypeId);
              if (typeAttributeIds != null && typeAttributeIds.Count > 0)
              {
                foreach (int num in (ArrayList) typeAttributeIds)
                {
                  switch (num)
                  {
                    case -1:
                    case 0:
                      continue;
                    default:
                      if ((AttributeDescriptor) this._attributeIDtoAttributeDescriptorHash[(object) num] == null)
                      {
                        AttributeDescriptor attributeDescriptor = new AttributeDescriptor(num, false);
                        this._attributeDescriptorList.Add((object) attributeDescriptor);
                        this._attributeIDtoAttributeDescriptorHash.Add((object) num, (object) attributeDescriptor);
                        this._checkedListBoxAttributes.Items.Add((object) new CheckedListBoxItem((object) attributeDescriptor, false));
                        attributeDescriptor.CheckedListBoxItem = this._checkedListBoxAttributes.Items[this._checkedListBoxAttributes.Items.Count - 1];
                        continue;
                      }
                      continue;
                  }
                }
              }
              this._addedObjectTypeIDs.Add((object) objectTypeId);
              continue;
            }
            continue;
        }
      }
    }
    finally
    {
      this._checkedListBoxAttributes.Items.EndUpdate();
    }
  }

  /// <summary>
  /// Добавить в список атрибуты.
  /// Все добавленые атрибуты считаются принадлежащими связи
  /// (всё равно, в том случае, если атрибут относиться связи при чтении значения
  /// необходимо проверять его принадлежность связи, и, если он связи не принадлежит,
  /// пытаться прочитать его из объекта)
  /// </summary>
  /// <param name="attributeIDs"> Идентификаторы атрибутов которые должны быть добавлены в список </param>
  /// <returns> Список дескрипторов добавленных атрибутов </returns>
  public AttributeDescriptorList AddAttributes(int[] attributeIDs)
  {
    return this.AddAttributes(attributeIDs, true);
  }

  /// <summary> Добавить в список атрибуты </summary>
  /// <param name="attributeIDs"> Идентификаторы атрибутов которые должны быть добавлены в список </param>
  /// <param name="isRelationAttributes"> Признак того, что дабавляемые атрибуты относятся к связи </param>
  /// <returns> Список дескрипторов добавленных атрибутов </returns>
  public AttributeDescriptorList AddAttributes(int[] attributeIDs, bool isRelationAttributes)
  {
    AttributeDescriptorList attributeDescriptorList = new AttributeDescriptorList(attributeIDs.Length);
    this._checkedListBoxAttributes.Items.BeginUpdate();
    try
    {
      foreach (int attributeId in attributeIDs)
      {
        switch (attributeId)
        {
          case -1:
          case 0:
            continue;
          default:
            AttributeDescriptor attributeDescriptor = (AttributeDescriptor) this._attributeIDtoAttributeDescriptorHash[(object) attributeId];
            if (attributeDescriptor == null)
            {
              attributeDescriptor = new AttributeDescriptor(attributeId, isRelationAttributes);
              this._attributeDescriptorList.Add((object) attributeDescriptor);
              this._attributeIDtoAttributeDescriptorHash.Add((object) attributeId, (object) attributeDescriptor);
              this._checkedListBoxAttributes.Items.Add((object) new CheckedListBoxItem((object) attributeDescriptor, false));
              attributeDescriptor.CheckedListBoxItem = this._checkedListBoxAttributes.Items[this._checkedListBoxAttributes.Items.Count - 1];
            }
            else if (isRelationAttributes)
              attributeDescriptor.IsRelationAttribute = isRelationAttributes;
            attributeDescriptorList.Add((object) attributeDescriptor);
            continue;
        }
      }
    }
    finally
    {
      this._checkedListBoxAttributes.Items.EndUpdate();
    }
    return attributeDescriptorList;
  }

  /// <summary> Добавить в список атрибуты </summary>
  /// <param name="attributeDescriptorList"> Список дескрипторов атрибутов, которые должны быть добавлены в список </param>
  public void AddAttributes(AttributeDescriptorList attributeDescriptorList)
  {
    this._checkedListBoxAttributes.Items.BeginUpdate();
    try
    {
      foreach (AttributeDescriptor attributeDescriptor in (ArrayList) attributeDescriptorList)
      {
        if (this._attributeDescriptorList.IndexOf((object) attributeDescriptor) == -1)
        {
          this._attributeDescriptorList.Add((object) attributeDescriptor);
          this._attributeIDtoAttributeDescriptorHash.Add((object) attributeDescriptor.AttributeID, (object) attributeDescriptor);
          this._checkedListBoxAttributes.Items.Add((object) new CheckedListBoxItem((object) attributeDescriptor, false));
          attributeDescriptor.CheckedListBoxItem = this._checkedListBoxAttributes.Items[this._checkedListBoxAttributes.Items.Count - 1];
        }
      }
    }
    finally
    {
      this._checkedListBoxAttributes.Items.EndUpdate();
    }
  }

  /// <summary> Выставить Checked = true у атрибутов с переданными идентификаторами </summary>
  /// <param name="attributeIDs"> Массив идентификаторов атрибутов, у которых свойство Checked должно стать = true </param>
  public void SetCheckedAttributes(int[] attributeIDs)
  {
    this.SetCheckedAttributes(attributeIDs, true);
  }

  /// <summary> Выставить Checked = true у атрибутов с переданными идентификаторами </summary>
  /// <param name="attributeIDs"> Массив идентификаторов атрибутов, у которых свойство Checked должно стать = true </param>
  /// <param name="moveToTop"> Переместить ли данные атрибуты на самый верх списка выбора </param>
  public void SetCheckedAttributes(int[] attributeIDs, bool moveToTop)
  {
    AttributeDescriptorList attributeDescriptorList = new AttributeDescriptorList(attributeIDs.Length);
    foreach (int attributeId in attributeIDs)
    {
      switch (attributeId)
      {
        case -1:
        case 0:
          continue;
        default:
          AttributeDescriptor attributeDescriptor = (AttributeDescriptor) this._attributeIDtoAttributeDescriptorHash[(object) attributeId];
          if (attributeDescriptor != null)
          {
            attributeDescriptorList.Add((object) attributeDescriptor);
            continue;
          }
          continue;
      }
    }
    if (attributeDescriptorList.Count <= 0)
      return;
    this._checkedListBoxAttributes.Items.BeginUpdate();
    try
    {
      foreach (AttributeDescriptor attributeDescriptor in (ArrayList) attributeDescriptorList)
      {
        attributeDescriptor.Checked = true;
        if (moveToTop)
          attributeDescriptor.MovedToTopInSortOrder = true;
      }
    }
    finally
    {
      this._checkedListBoxAttributes.SortOrder = SortOrder.Ascending;
      this._checkedListBoxAttributes.Items.EndUpdate();
    }
  }

  /// <summary> Получить список дескрипторов отмеченых атрибутов </summary>
  /// <returns> Список дескрипторов отмеченых атрибутов </returns>
  public AttributeDescriptorList GetCheckedAttributesList()
  {
    AttributeDescriptorList checkedAttributesList = new AttributeDescriptorList();
    foreach (AttributeDescriptor attributeDescriptor in (ArrayList) this._attributeDescriptorList)
    {
      if (attributeDescriptor != null && attributeDescriptor.Checked)
        checkedAttributesList.Add((object) attributeDescriptor);
    }
    return checkedAttributesList;
  }

  /// <summary> Очистить список атрибутов </summary>
  public void ClearAttributesList()
  {
    this._checkedListBoxAttributes.Items.BeginUpdate();
    try
    {
      this._checkedListBoxAttributes.Items.Clear();
      this._attributeDescriptorList.Clear();
      this._attributeIDtoAttributeDescriptorHash.Clear();
    }
    finally
    {
      this._checkedListBoxAttributes.Items.EndUpdate();
    }
  }

  /// <summary> Отметить все атрибуты, доступные для выбора как отмеченые </summary>
  public void CheckAllAttributes()
  {
    this._checkedListBoxAttributes.Items.BeginUpdate();
    try
    {
      foreach (AttributeDescriptor attributeDescriptor in (ArrayList) this._attributeDescriptorList)
        attributeDescriptor.Checked = true;
    }
    finally
    {
      this._checkedListBoxAttributes.Items.EndUpdate();
    }
  }

  /// <summary> Снять отметки со всех отмеченых атрибутов </summary>
  public void UncheckAllAttributes()
  {
    this._checkedListBoxAttributes.Items.BeginUpdate();
    try
    {
      foreach (AttributeDescriptor attributeDescriptor in (ArrayList) this._attributeDescriptorList)
        attributeDescriptor.Checked = false;
    }
    finally
    {
      this._checkedListBoxAttributes.Items.EndUpdate();
    }
  }

  /// <summary> Список загруженных атрибутов </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public AttributeDescriptorList LoadedAttributes
  {
    get => (AttributeDescriptorList) this._attributeDescriptorList.Clone();
    set
    {
      this._checkedListBoxAttributes.Items.BeginUpdate();
      try
      {
        this.ClearAttributesList();
        if (value == null || value.Count <= 0)
          return;
        this.AddAttributes(value);
      }
      finally
      {
        this._checkedListBoxAttributes.Items.EndUpdate();
      }
    }
  }

  /// <summary> Вызывается перед началом редактирования списка атрибутов (ускоряет работу, блокируя обновление визуальных контролов) </summary>
  public void BeginUpdate() => this._checkedListBoxAttributes.Items.BeginUpdate();

  /// <summary> Вызывается по окончании редактирования списка атрибутов (разблокирует обновление визуальных контролов, обновляет их содержимое) </summary>
  public void EndUpdate()
  {
    if (this._checkedListBoxAttributes.Items.Count > 0)
    {
      this._checkedListBoxAttributes.SelectedIndex = 0;
      this._checkedListBoxAttributes.MakeItemVisible(0);
    }
    this._checkedListBoxAttributes.Items.EndUpdate();
  }

  /// <summary> Нажата кнопка "отметить все" </summary>
  private void _buttonCheckAll_Click(object sender, EventArgs e) => this.CheckAllAttributes();

  /// <summary> Нажата кнопка "снять отметки со всех" </summary>
  private void _buttonUncheckAll_Click(object sender, EventArgs e) => this.UncheckAllAttributes();

  /// <summary> Нажата кнопка "показать список всех атрибутов" </summary>
  private void _buttonAllAttributes_Click(object sender, EventArgs e)
  {
    IntList intList = UIHelper.SelectAttributesInTotalList();
    if (intList == null || intList.Count <= 0)
      return;
    AttributeDescriptorList attributeDescriptorList = this.AddAttributes((int[]) intList.ToArray(typeof (int)));
    if (attributeDescriptorList == null || attributeDescriptorList.Count <= 0)
      return;
    foreach (AttributeDescriptor attributeDescriptor in (ArrayList) attributeDescriptorList)
    {
      if (attributeDescriptor != null && attributeDescriptor.CheckedListBoxItem != null)
      {
        this._checkedListBoxAttributes.MakeItemVisible(this._checkedListBoxAttributes.Items.IndexOf((object) attributeDescriptor.CheckedListBoxItem));
        this._checkedListBoxAttributes.SelectedItem = (object) attributeDescriptor.CheckedListBoxItem;
        attributeDescriptor.Checked = true;
      }
    }
  }

  private void _checkedListBoxAttributes_SizeChanged(object sender, EventArgs e)
  {
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UserControlSelectAttributes));
    this._buttonUncheckAll = new Button();
    this._buttonCheckAll = new Button();
    this._checkedListBoxAttributes = new CheckedListBoxControl();
    this._buttonAllAttributes = new Button();
    this._toolTipController = new ToolTipController(this.components);
    ((ISupportInitialize) this._checkedListBoxAttributes).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._buttonUncheckAll, "_buttonUncheckAll");
    this._buttonUncheckAll.Name = "_buttonUncheckAll";
    this._toolTipController.SetToolTip((Control) this._buttonUncheckAll, "Снять отметки со всех атрибутов содержащихся в данном списке");
    this._buttonUncheckAll.UseVisualStyleBackColor = true;
    this._buttonUncheckAll.Click += new EventHandler(this._buttonUncheckAll_Click);
    componentResourceManager.ApplyResources((object) this._buttonCheckAll, "_buttonCheckAll");
    this._buttonCheckAll.Name = "_buttonCheckAll";
    this._toolTipController.SetToolTip((Control) this._buttonCheckAll, "Отметить все атрибуты содержащиеся в данном списке");
    this._buttonCheckAll.UseVisualStyleBackColor = true;
    this._buttonCheckAll.Click += new EventHandler(this._buttonCheckAll_Click);
    componentResourceManager.ApplyResources((object) this._checkedListBoxAttributes, "_checkedListBoxAttributes");
    this._checkedListBoxAttributes.CheckOnClick = true;
    this._checkedListBoxAttributes.Items.AddRange(new CheckedListBoxItem[7]
    {
      new CheckedListBoxItem((object) "Зона", CheckState.Checked),
      new CheckedListBoxItem((object) "Количество", CheckState.Checked),
      new CheckedListBoxItem((object) "Наименование", CheckState.Checked),
      new CheckedListBoxItem((object) "Обозначение", CheckState.Checked),
      new CheckedListBoxItem((object) "Позиция", CheckState.Checked),
      new CheckedListBoxItem((object) "Примечание", CheckState.Checked),
      new CheckedListBoxItem((object) "Формат", CheckState.Checked)
    });
    this._checkedListBoxAttributes.Name = "_checkedListBoxAttributes";
    this._checkedListBoxAttributes.SortOrder = SortOrder.Ascending;
    this._checkedListBoxAttributes.ToolTip = "Список атрибутов";
    this._checkedListBoxAttributes.SizeChanged += new EventHandler(this._checkedListBoxAttributes_SizeChanged);
    componentResourceManager.ApplyResources((object) this._buttonAllAttributes, "_buttonAllAttributes");
    this._buttonAllAttributes.Name = "_buttonAllAttributes";
    this._toolTipController.SetToolTip((Control) this._buttonAllAttributes, "Открыть список всех атрибутов");
    this._buttonAllAttributes.Click += new EventHandler(this._buttonAllAttributes_Click);
    this._toolTipController.Style = new ViewStyle("ToolTip style");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._buttonAllAttributes);
    this.Controls.Add((Control) this._buttonUncheckAll);
    this.Controls.Add((Control) this._buttonCheckAll);
    this.Controls.Add((Control) this._checkedListBoxAttributes);
    this.Name = nameof (UserControlSelectAttributes);
    this.Tag = (object) "  ";
    ((ISupportInitialize) this._checkedListBoxAttributes).EndInit();
    this.ResumeLayout(false);
  }
}
