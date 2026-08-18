// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.RectangleEditPanel
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model.UI;

/// <summary>
/// Компонент для редактирования размеров элементов управления, порождённых от RectangleElement
/// </summary>
/// <summary>
/// Компонент для редактирования размеров элементов управления, порождённых от RectangleElement
/// </summary>
public class RectangleEditPanel : UserControl
{
  /// <summary>Редактируемый элемент</summary>
  protected RectangleElement element;
  /// <summary>
  /// Требуется ли пропускать обработку событий (если она уже где-то выполняется)
  /// </summary>
  protected bool suspendEvents;
  /// <summary>
  /// Делегат для управления состояниями кнопок "ОК", "Применить"
  /// </summary>
  public SetOkApplyEnabledDelegate SetOkApplyEnabledHandler;
  private CommonSetttings configuration;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private NumericUpDown numeric_tH;
  private NumericUpDown numeric_tBH;
  private NumericUpDown numeric_tLH;
  private NumericUpDown numeric_tRW;
  private NumericUpDown numeric_tW;
  private NumericUpDown numeric_tLW;
  private CheckBox cb_tBH;
  private CheckBox cb_tH;
  private CheckBox cb_tLH;
  private CheckBox cb_tRW;
  private CheckBox cb_tW;
  private CheckBox cb_tLW;
  private PictureBox pictureTableDimensions;

  /// <summary>Создать экземпляр редактора</summary>
  public RectangleEditPanel()
  {
    this.InitializeComponent();
    this.configuration = CommonSetttings.GetSetttings(nameof (RectangleEditPanel));
    this.SetRectangleElement((RectangleElement) null);
  }

  /// <summary>Создать экземпляр редактора, заполнить его поля</summary>
  /// <param name="element">Редактируемый прямоугольный элемент</param>
  public RectangleEditPanel(RectangleElement element)
  {
    this.InitializeComponent();
    this.configuration = CommonSetttings.GetSetttings(nameof (RectangleEditPanel));
    this.SetRectangleElement(element);
  }

  private void SaveToConfig()
  {
    this.configuration.SetProperty("cb_tLW.Checked", (object) this.cb_tLW.Checked);
    this.configuration.SetProperty("cb_tW.Checked", (object) this.cb_tW.Checked);
    this.configuration.SetProperty("cb_tRW.Checked", (object) this.cb_tRW.Checked);
    this.configuration.SetProperty("cb_tLH.Checked", (object) this.cb_tLH.Checked);
    this.configuration.SetProperty("cb_tH.Checked", (object) this.cb_tH.Checked);
    this.configuration.SetProperty("cb_tBH.Checked", (object) this.cb_tBH.Checked);
  }

  /// <summary>Очистить все поля в форме</summary>
  /// <param name="updateControls">Обновить статусы всех контролов</param>
  protected virtual void Clear(bool updateControls)
  {
    this.numeric_tLW.Value = 20M;
    object property1 = this.configuration.GetProperty("cb_tLW.Checked");
    this.cb_tLW.Checked = property1 == null || (bool) property1;
    this.numeric_tW.Value = 180M;
    object property2 = this.configuration.GetProperty("cb_tW.Checked");
    this.cb_tW.Checked = property2 != null && (bool) property2;
    this.numeric_tRW.Value = 20M;
    object property3 = this.configuration.GetProperty("cb_tRW.Checked");
    this.cb_tRW.Checked = property3 == null || (bool) property3;
    this.numeric_tLH.Value = 20M;
    object property4 = this.configuration.GetProperty("cb_tLH.Checked");
    this.cb_tLH.Checked = property4 == null || (bool) property4;
    this.numeric_tH.Value = 250M;
    object property5 = this.configuration.GetProperty("cb_tH.Checked");
    this.cb_tH.Checked = property5 != null && (bool) property5;
    this.numeric_tBH.Value = 20M;
    object property6 = this.configuration.GetProperty("cb_tBH.Checked");
    this.cb_tBH.Checked = property6 == null || (bool) property6;
    if (!updateControls)
      return;
    this.UpdateControls();
  }

  /// <summary>Обновить статусы всех контролов</summary>
  protected virtual void UpdateControls()
  {
    bool readOnly = this.element == null;
    this.SetHorizontalSizeControlsState(readOnly);
    this.SetVerticalSizeControlsState(readOnly);
  }

  /// <summary>
  /// Установить корректные цвета фона и статусы контролам, отвечающим за горизонтальные размеры
  /// прямоугольника и отступы от краёв листа справа и слева. Фон и статусы зависят от значений чек-боксов,
  /// связанных с этими контролами
  /// </summary>
  /// <param name="readOnly">Можно ли редактировать что-либо в редакторе</param>
  protected virtual void SetHorizontalSizeControlsState(bool readOnly)
  {
    int num = this.element == null ? 0 : (this.element.ParentCell != null ? 1 : 0);
    this.numeric_tLW.BackColor = this.cb_tLW.Checked ? ControlColors.colorHorizSizeActive : ControlColors.colorHorizSizeInactive;
    this.numeric_tLW.ReadOnly = !this.cb_tLW.Checked | readOnly;
    this.numeric_tLW.Enabled = !this.numeric_tLW.ReadOnly;
    this.cb_tLW.Enabled &= !readOnly;
    this.numeric_tW.BackColor = this.cb_tW.Checked ? ControlColors.colorHorizSizeActive : ControlColors.colorHorizSizeInactive;
    this.numeric_tW.ReadOnly = !this.cb_tW.Checked | readOnly;
    this.numeric_tW.Enabled = !this.numeric_tW.ReadOnly;
    this.cb_tW.Enabled &= !readOnly;
    this.numeric_tRW.BackColor = this.cb_tRW.Checked ? ControlColors.colorHorizSizeActive : ControlColors.colorHorizSizeInactive;
    this.numeric_tRW.ReadOnly = !this.cb_tRW.Checked | readOnly;
    this.numeric_tRW.Enabled = !this.numeric_tRW.ReadOnly;
    this.cb_tRW.Enabled &= !readOnly;
  }

  /// <summary>
  /// Установить корректные цвета фона и статусы контролам, отвечающим за вертикальные размеры
  /// прямоугольника и отступы от краёв листа сверху и снизу. Фон и статусы зависят от значений чек-боксов,
  /// связанных с этими контролами
  /// </summary>
  /// <param name="readOnly">Можно ли редактировать что-либо в редакторе</param>
  protected virtual void SetVerticalSizeControlsState(bool readOnly)
  {
    bool flag = this.element != null && this.element.ParentCell != null;
    this.numeric_tLH.BackColor = this.cb_tLH.Checked ? ControlColors.colorHorizSizeActive : ControlColors.colorHorizSizeInactive;
    this.numeric_tLH.ReadOnly = !this.cb_tLH.Checked | readOnly;
    this.numeric_tLH.Enabled = !flag && !this.numeric_tLH.ReadOnly;
    this.cb_tLH.Enabled &= !readOnly;
    this.numeric_tH.BackColor = this.cb_tH.Checked ? ControlColors.colorHorizSizeActive : ControlColors.colorHorizSizeInactive;
    this.numeric_tH.ReadOnly = !this.cb_tH.Checked | readOnly;
    this.numeric_tH.Enabled = !this.numeric_tH.ReadOnly;
    this.cb_tH.Enabled &= !readOnly;
    this.numeric_tBH.BackColor = this.cb_tBH.Checked ? ControlColors.colorHorizSizeActive : ControlColors.colorHorizSizeInactive;
    this.numeric_tBH.ReadOnly = !this.cb_tBH.Checked | readOnly;
    this.numeric_tBH.Enabled = !this.numeric_tBH.ReadOnly;
    this.cb_tBH.Enabled &= !readOnly;
  }

  /// <summary>
  /// Выполнить автоматический расчёт ширины прямоугольника, отступов, откорректировать значения контролов
  /// </summary>
  protected virtual void AutoCalcHorizontalSizes()
  {
    if (this.element == null)
      return;
    if (this.suspendEvents)
      return;
    try
    {
      this.suspendEvents = true;
      Decimal num1 = Convert.ToDecimal(this.element.Page.Size.Width);
      Decimal num2 = this.numeric_tLW.Value;
      Decimal num3 = this.numeric_tRW.Value;
      Decimal num4 = this.numeric_tW.Value;
      if (!this.cb_tLW.Checked)
        num2 = num1 - num4 - num3;
      else if (!this.cb_tRW.Checked)
        num3 = num1 - num4 - num2;
      else if (!this.cb_tW.Checked)
      {
        num4 = num1 - num2 - num3;
        if (num4 < 0M)
        {
          num4 = 0M;
          num3 = num1 - num2;
        }
      }
      this.numeric_tLW.Value = num2;
      this.numeric_tW.Value = num4;
      this.numeric_tRW.Value = num3;
      bool flag = num4 >= 0M;
      if (this.SetOkApplyEnabledHandler == null)
        return;
      this.SetOkApplyEnabledHandler(flag, flag);
    }
    finally
    {
      this.suspendEvents = false;
      this.UpdateControls();
    }
  }

  /// <summary>
  /// Выполнить автоматический расчёт высоты прямоугольника, отступов
  /// </summary>
  protected virtual void AutoCalcVerticalSizes()
  {
    if (this.element == null)
      return;
    if (this.suspendEvents)
      return;
    try
    {
      this.suspendEvents = true;
      Decimal num1 = Convert.ToDecimal(this.element.Page.Size.Height);
      Decimal num2 = this.numeric_tLH.Value;
      Decimal num3 = this.numeric_tBH.Value;
      Decimal num4 = this.numeric_tH.Value;
      if (!this.cb_tLH.Checked)
        num2 = num1 - num4 - num3;
      else if (!this.cb_tBH.Checked)
        num3 = num1 - num4 - num2;
      else if (!this.cb_tH.Checked)
      {
        num4 = num1 - num2 - num3;
        if (num4 < 0M)
        {
          num4 = 0M;
          num3 = num1 - num2;
        }
      }
      this.numeric_tLH.Value = num2;
      this.numeric_tH.Value = num4;
      this.numeric_tBH.Value = num3;
      bool flag = num4 >= 0M;
      if (this.SetOkApplyEnabledHandler == null)
        return;
      this.SetOkApplyEnabledHandler(flag, flag);
    }
    finally
    {
      this.suspendEvents = false;
      this.UpdateControls();
    }
  }

  /// <summary>
  /// Изменился один из горизонтальных размеров или отступов
  /// </summary>
  /// <param name="sender">Отправитель (редактор текста)</param>
  /// <param name="e">Аргументы события</param>
  private void DoChangeHorizontalSizes(object sender, EventArgs e)
  {
    if (this.element == null || this.suspendEvents)
      return;
    this.AutoCalcHorizontalSizes();
  }

  /// <summary>Изменился один из вертикальных размеров или отступов</summary>
  /// <param name="sender">Отправитель (редактор текста)</param>
  /// <param name="e">Аргументы события</param>
  private void DoChangeVerticalSizes(object sender, EventArgs e)
  {
    if (this.element == null || this.suspendEvents)
      return;
    this.AutoCalcVerticalSizes();
  }

  /// <summary>
  /// Кликнут один из чек-боксов, отвечающих за горизонтальные размеры и отступы
  /// </summary>
  /// <param name="sender">Отправитель (чек-бокс)</param>
  /// <param name="e">Аргументы события</param>
  private void DoChangeHorizontalSizeChecks(object sender, EventArgs e)
  {
    if (this.element == null || this.suspendEvents)
      return;
    CheckBox checkBox = sender as CheckBox;
    try
    {
      this.suspendEvents = true;
      if (checkBox == this.cb_tLW)
      {
        if (!checkBox.Checked)
        {
          this.cb_tW.Checked = true;
          this.cb_tRW.Checked = true;
        }
        else
        {
          this.cb_tW.Checked = true;
          this.cb_tRW.Checked = false;
        }
      }
      if (checkBox == this.cb_tW)
      {
        if (!checkBox.Checked)
        {
          this.cb_tLW.Checked = true;
          this.cb_tRW.Checked = true;
        }
        else
        {
          this.cb_tLW.Checked = true;
          this.cb_tRW.Checked = false;
        }
      }
      if (checkBox == this.cb_tRW)
      {
        if (!checkBox.Checked)
        {
          this.cb_tLW.Checked = true;
          this.cb_tW.Checked = true;
        }
        else if (this.cb_tLW.Enabled)
        {
          this.cb_tLW.Checked = false;
          this.cb_tW.Checked = true;
        }
        else
          this.cb_tW.Checked = false;
      }
      this.SetHorizontalSizeControlsState(false);
    }
    finally
    {
      this.suspendEvents = false;
      this.SaveToConfig();
      this.AutoCalcHorizontalSizes();
    }
  }

  /// <summary>
  /// Кликнут один из чек-боксов, отвечающих за вертикальные размеры и отступы
  /// </summary>
  /// <param name="sender">Отправитель (чек-бокс)</param>
  /// <param name="e">Аргументы события</param>
  private void DoChangeVerticalSizeChecks(object sender, EventArgs e)
  {
    if (this.element == null || this.suspendEvents)
      return;
    CheckBox checkBox = sender as CheckBox;
    try
    {
      this.suspendEvents = true;
      if (checkBox == this.cb_tLH)
      {
        if (!checkBox.Checked)
        {
          this.cb_tH.Checked = true;
          this.cb_tBH.Checked = true;
        }
        else
        {
          this.cb_tH.Checked = true;
          this.cb_tBH.Checked = false;
        }
      }
      if (checkBox == this.cb_tH)
      {
        if (!checkBox.Checked)
        {
          this.cb_tLH.Checked = true;
          this.cb_tBH.Checked = true;
        }
        else
        {
          this.cb_tLH.Checked = true;
          this.cb_tBH.Checked = false;
        }
      }
      if (checkBox == this.cb_tBH)
      {
        if (!checkBox.Checked)
        {
          this.cb_tLH.Checked = true;
          this.cb_tH.Checked = true;
        }
        else if (this.cb_tLH.Enabled)
        {
          this.cb_tLH.Checked = false;
          this.cb_tH.Checked = true;
        }
        else
          this.cb_tH.Checked = false;
      }
      this.SetVerticalSizeControlsState(false);
    }
    finally
    {
      this.suspendEvents = false;
      this.SaveToConfig();
      this.AutoCalcVerticalSizes();
    }
  }

  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    if (keyData != Keys.Return)
      return base.ProcessCmdKey(ref msg, keyData);
    if (this.ActiveControl is NumericUpDown)
    {
      NumericUpDown activeControl = this.ActiveControl as NumericUpDown;
      this.Focus();
      activeControl.Focus();
    }
    this.Apply();
    return true;
  }

  /// <summary>Загрузить в редактор элемент</summary>
  /// <param name="element">Редактируемый элемент</param>
  public virtual void SetRectangleElement(RectangleElement element)
  {
    this.suspendEvents = true;
    bool flag1 = true;
    if (this.element != element)
    {
      this.element = element;
      this.Clear(false);
      flag1 = false;
    }
    if (element != null)
    {
      try
      {
        Decimal num1 = Convert.ToDecimal(this.element.Page.Size.Width);
        Decimal num2 = Convert.ToDecimal(this.element.Page.Size.Height);
        TableData parentCell = element.ParentCell;
        bool flag2 = parentCell != null;
        RectangleF rectangleF = element.ProperBounds;
        if (parentCell != null && parentCell.IsFixedStructureArea)
          rectangleF = element.Bounds;
        this.numeric_tLW.Value = Convert.ToDecimal(rectangleF.Left);
        if (!flag1)
        {
          this.cb_tLW.Enabled = true;
          if (flag2)
            this.numeric_tLW.Enabled = true;
        }
        if (!flag1 & flag2)
        {
          this.numeric_tLW.Enabled = true;
          this.cb_tLW.Checked = true;
          this.cb_tRW.Checked = false;
        }
        this.numeric_tW.Value = Convert.ToDecimal(rectangleF.Width);
        this.cb_tW.Enabled = true;
        this.numeric_tRW.Value = num1 - Convert.ToDecimal(rectangleF.Right);
        this.cb_tRW.Enabled = true;
        this.numeric_tLH.Value = Convert.ToDecimal(rectangleF.Top);
        if (!flag1)
        {
          this.cb_tLH.Enabled = !flag2;
          if (flag2)
            this.numeric_tLH.Enabled = false;
        }
        if (!flag1 & flag2)
          this.cb_tLH.Checked = true;
        this.numeric_tH.Value = Convert.ToDecimal(rectangleF.Height);
        this.numeric_tH.Increment = (double) element.DefaultRowSize == 0.0 || !element.IsFixedSizeRows ? 1M : Convert.ToDecimal(element.DefaultRowSize);
        this.cb_tH.Enabled = true;
        this.numeric_tBH.Value = num2 - Convert.ToDecimal(rectangleF.Bottom);
        this.cb_tBH.Enabled = true;
        if (element.Page is Page page)
        {
          if (page.DocumentControl != null)
          {
            if (element is IPageElementWithInterface elementWithInterface)
            {
              if (elementWithInterface.PageUI != null)
                page.DocumentControl.ScrollToViewRectangle(elementWithInterface.PageUI.Bounds, true, false);
            }
          }
        }
      }
      finally
      {
        this.suspendEvents = false;
      }
    }
    this.UpdateControls();
  }

  /// <summary>Внести изменения в редактируемый элемент</summary>
  public void Apply()
  {
    Convert.ToDecimal(this.element.Page.Size.Width);
    Convert.ToDecimal(this.element.Page.Size.Height);
    float single1 = Convert.ToSingle(this.numeric_tLW.Value);
    float single2 = Convert.ToSingle(this.numeric_tW.Value);
    if ((double) single2 < 0.0)
      throw new Exception(LocalizationHolder.rm.GetString("Document.Model_90"));
    float single3 = Convert.ToSingle(this.numeric_tLH.Value);
    float single4 = Convert.ToSingle(this.numeric_tH.Value);
    if ((double) single4 < 0.0)
      throw new Exception(LocalizationHolder.rm.GetString("Document.Model_93"));
    RectangleF rect = new RectangleF(single1, single3, single2, single4);
    PageData page1 = this.element.Page;
    RectangleF rectangleF1 = this.element.ProperBounds;
    TableData parentCell1 = this.element.ParentCell;
    if (parentCell1 != null && parentCell1.IsFixedStructureArea)
      rectangleF1 = this.element.Bounds;
    RectangleF rectangleF2 = UnitsConverter.RoundPectangle(PageControl.NormalRectangle(rect), 5);
    if (rectangleF1 != rectangleF2)
    {
      if (parentCell1 != null)
      {
        if (parentCell1.IsFixedStructureArea)
        {
          this.element.AssignBounds(rectangleF2, true, false, false);
          TableData parentCell2 = parentCell1.ParentCell;
          RectangleF rectangleF3 = parentCell2 == null || !parentCell2.IsFixedStructureArea ? parentCell1.properBounds : parentCell1.bounds;
          this.element.AssignProperBounds(new RectangleF(rectangleF2.X - rectangleF3.X, rectangleF2.Y - rectangleF3.Y, rectangleF2.Width, rectangleF2.Height), true, false, false);
          RectangleF rectangleF4 = UnitsConverter.RoundPectangle(PageControl.NormalRectangle(rectangleF2), 5);
          if ((double) rectangleF1.Height != (double) rectangleF4.Height)
            this.element.AssignMinHeight(rectangleF4.Height, false, false, true);
          if ((double) rectangleF1.Width != (double) rectangleF4.Width)
            this.element.AssignMinWidth(rectangleF4.Width, false, false, true);
          this.element.SetCellSizes(this.element.Bounds, false, true, true, true);
        }
        else
        {
          if (parentCell1.IsRow && (double) rectangleF1.Left != (double) rectangleF2.Left && this.element.Index > 0 && parentCell1.Nodes[this.element.Index - 1] is RectangleElement node)
          {
            RectangleF properBounds = node.ProperBounds;
            properBounds.Width += rectangleF2.Left - rectangleF1.Left;
            node.AssignMinWidth(properBounds.Width, false, false, true);
            node.SetCellSizes(properBounds, false, true, true, true);
            node.WidthOverrided = true;
          }
          if ((double) rectangleF1.Height != (double) rectangleF2.Height)
            this.element.AssignMinHeight(rectangleF2.Height, false, false, true);
          if ((double) rectangleF1.Width != (double) rectangleF2.Width)
            this.element.AssignMinWidth(rectangleF2.Width, false, false, true);
          this.element.AssignProperBounds(rectangleF2, true, false, false);
          this.element.SetCellSizes(this.element.Bounds, false, true, true, true);
          if (parentCell1.IsColumn)
          {
            RectangleF bounds = parentCell1.Bounds;
            bounds.Size = parentCell1.CalcSizeFromProper(new SizeF(rectangleF2.Width, bounds.Height));
            parentCell1.SetCellSizes(bounds, false, true, true, true, false);
          }
          else
          {
            if ((double) rectangleF2.Width != (double) rectangleF1.Width)
              this.element.WidthOverrided = true;
            RectangleF bounds = parentCell1.Bounds;
            bounds.Size = parentCell1.CalcSizeFromProper(new SizeF(bounds.Width, rectangleF2.Height));
            parentCell1.SetCellSizes(bounds, false, true, true, true, false);
          }
        }
      }
      else if (this.element is TableData element)
      {
        if ((double) rectangleF1.Height != (double) rectangleF2.Height)
        {
          element.AssignMinHeight(rectangleF2.Height, false, false, true);
          if ((double) element.MaxHeight != 0.0)
            element.AssignMaxHeight(rectangleF2.Height, false, false, true);
        }
        rectangleF2.Location = element.CalcLocationFromProper(rectangleF2.Location);
        rectangleF2.Size = element.CalcSizeFromProper(rectangleF2.Size);
        if (rectangleF2.Location != rectangleF1.Location)
          element.RecalcCellLocations(rectangleF2.Location, 0, element.Nodes.Count, false, false, false);
        element.SetCellSizes(rectangleF2, false, true, true, true, false);
      }
      else
      {
        if ((double) rectangleF1.Height != (double) rectangleF2.Height)
          this.element.AssignMinHeight(rectangleF2.Height, false, false, true);
        this.element.AssignProperBounds(rectangleF2, true, false, false);
        this.element.RecalcRelativeSize();
      }
      this.element.UpdateLayout(true);
      rectangleF2 = this.element.ProperBounds;
      if (parentCell1 != null && parentCell1.IsFixedStructureArea)
        rectangleF2 = this.element.Bounds;
      if (rectangleF1 != rectangleF2)
        this.SetRectangleElement(this.element);
    }
    if (!(page1 is Page page2) || page2.DocumentControl == null || !(this.element is IPageElementWithInterface element1) || element1.PageUI == null)
      return;
    page2.DocumentControl.ScrollToViewRectangle(element1.PageUI.Bounds, true, false);
  }

  private void SetTableCellBounds(
    RectangleElement element,
    RectangleF oldBounds,
    RectangleF newBounds)
  {
    if ((double) oldBounds.Height != (double) newBounds.Height)
      element.AssignMinHeight(newBounds.Height, false, false, true);
    if ((double) oldBounds.Width != (double) newBounds.Width)
      element.AssignMinWidth(newBounds.Width, false, false, true);
    element.AssignProperBounds(newBounds, true, false, false);
    element.SetCellSizes(element.Bounds, false, true, true, true);
    if ((double) newBounds.Width == (double) oldBounds.Width)
      return;
    element.WidthOverrided = true;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RectangleEditPanel));
    this.numeric_tH = new NumericUpDown();
    this.numeric_tBH = new NumericUpDown();
    this.numeric_tLH = new NumericUpDown();
    this.numeric_tRW = new NumericUpDown();
    this.numeric_tW = new NumericUpDown();
    this.numeric_tLW = new NumericUpDown();
    this.cb_tBH = new CheckBox();
    this.cb_tH = new CheckBox();
    this.cb_tLH = new CheckBox();
    this.cb_tRW = new CheckBox();
    this.cb_tW = new CheckBox();
    this.cb_tLW = new CheckBox();
    this.pictureTableDimensions = new PictureBox();
    this.numeric_tH.BeginInit();
    this.numeric_tBH.BeginInit();
    this.numeric_tLH.BeginInit();
    this.numeric_tRW.BeginInit();
    this.numeric_tW.BeginInit();
    this.numeric_tLW.BeginInit();
    ((ISupportInitialize) this.pictureTableDimensions).BeginInit();
    this.SuspendLayout();
    this.numeric_tH.BackColor = Color.FromArgb(212, 225, 247);
    this.numeric_tH.BorderStyle = BorderStyle.FixedSingle;
    this.numeric_tH.DecimalPlaces = 1;
    this.numeric_tH.ForeColor = Color.Black;
    componentResourceManager.ApplyResources((object) this.numeric_tH, "numeric_tH");
    this.numeric_tH.Maximum = new Decimal(new int[4]
    {
      1000000,
      0,
      0,
      0
    });
    this.numeric_tH.MaximumSize = new Size(66, 0);
    this.numeric_tH.MinimumSize = new Size(66, 0);
    this.numeric_tH.Name = "numeric_tH";
    this.numeric_tH.ValueChanged += new EventHandler(this.DoChangeVerticalSizes);
    this.numeric_tBH.BackColor = Color.FromArgb(228, 224 /*0xE0*/, 216);
    this.numeric_tBH.BorderStyle = BorderStyle.FixedSingle;
    this.numeric_tBH.DecimalPlaces = 1;
    this.numeric_tBH.ForeColor = Color.Black;
    componentResourceManager.ApplyResources((object) this.numeric_tBH, "numeric_tBH");
    this.numeric_tBH.Maximum = new Decimal(new int[4]
    {
      1000000,
      0,
      0,
      0
    });
    this.numeric_tBH.MaximumSize = new Size(66, 0);
    this.numeric_tBH.Minimum = new Decimal(new int[4]
    {
      1000000,
      0,
      0,
      int.MinValue
    });
    this.numeric_tBH.MinimumSize = new Size(66, 0);
    this.numeric_tBH.Name = "numeric_tBH";
    this.numeric_tBH.ValueChanged += new EventHandler(this.DoChangeVerticalSizes);
    this.numeric_tLH.BackColor = Color.FromArgb(212, 225, 247);
    this.numeric_tLH.BorderStyle = BorderStyle.FixedSingle;
    this.numeric_tLH.DecimalPlaces = 1;
    this.numeric_tLH.ForeColor = Color.Black;
    componentResourceManager.ApplyResources((object) this.numeric_tLH, "numeric_tLH");
    this.numeric_tLH.Maximum = new Decimal(new int[4]
    {
      1000000,
      0,
      0,
      0
    });
    this.numeric_tLH.MaximumSize = new Size(66, 0);
    this.numeric_tLH.Minimum = new Decimal(new int[4]
    {
      1000000,
      0,
      0,
      int.MinValue
    });
    this.numeric_tLH.MinimumSize = new Size(66, 0);
    this.numeric_tLH.Name = "numeric_tLH";
    this.numeric_tLH.ValueChanged += new EventHandler(this.DoChangeVerticalSizes);
    this.numeric_tRW.BackColor = Color.FromArgb(242, 242, 242);
    this.numeric_tRW.BorderStyle = BorderStyle.FixedSingle;
    this.numeric_tRW.DecimalPlaces = 1;
    this.numeric_tRW.ForeColor = Color.Black;
    componentResourceManager.ApplyResources((object) this.numeric_tRW, "numeric_tRW");
    this.numeric_tRW.Maximum = new Decimal(new int[4]
    {
      1000000,
      0,
      0,
      0
    });
    this.numeric_tRW.MaximumSize = new Size(66, 0);
    this.numeric_tRW.Minimum = new Decimal(new int[4]
    {
      10000000,
      0,
      0,
      int.MinValue
    });
    this.numeric_tRW.MinimumSize = new Size(66, 0);
    this.numeric_tRW.Name = "numeric_tRW";
    this.numeric_tRW.ValueChanged += new EventHandler(this.DoChangeHorizontalSizes);
    this.numeric_tW.BackColor = Color.FromArgb(253, 217, 171);
    this.numeric_tW.BorderStyle = BorderStyle.FixedSingle;
    this.numeric_tW.DecimalPlaces = 1;
    this.numeric_tW.ForeColor = Color.Black;
    componentResourceManager.ApplyResources((object) this.numeric_tW, "numeric_tW");
    this.numeric_tW.Maximum = new Decimal(new int[4]
    {
      1000000,
      0,
      0,
      0
    });
    this.numeric_tW.MaximumSize = new Size(66, 0);
    this.numeric_tW.MinimumSize = new Size(66, 0);
    this.numeric_tW.Name = "numeric_tW";
    this.numeric_tW.ValueChanged += new EventHandler(this.DoChangeHorizontalSizes);
    this.numeric_tLW.BackColor = Color.FromArgb(253, 217, 171);
    this.numeric_tLW.BorderStyle = BorderStyle.FixedSingle;
    this.numeric_tLW.DecimalPlaces = 1;
    this.numeric_tLW.ForeColor = Color.Black;
    componentResourceManager.ApplyResources((object) this.numeric_tLW, "numeric_tLW");
    this.numeric_tLW.Maximum = new Decimal(new int[4]
    {
      1000000,
      0,
      0,
      0
    });
    this.numeric_tLW.MaximumSize = new Size(66, 0);
    this.numeric_tLW.Minimum = new Decimal(new int[4]
    {
      1000000,
      0,
      0,
      int.MinValue
    });
    this.numeric_tLW.MinimumSize = new Size(66, 0);
    this.numeric_tLW.Name = "numeric_tLW";
    this.numeric_tLW.ValueChanged += new EventHandler(this.DoChangeHorizontalSizes);
    this.cb_tBH.BackColor = SystemColors.Control;
    componentResourceManager.ApplyResources((object) this.cb_tBH, "cb_tBH");
    this.cb_tBH.MaximumSize = new Size(18, 18);
    this.cb_tBH.MinimumSize = new Size(18, 18);
    this.cb_tBH.Name = "cb_tBH";
    this.cb_tBH.UseVisualStyleBackColor = false;
    this.cb_tBH.CheckedChanged += new EventHandler(this.DoChangeVerticalSizeChecks);
    this.cb_tH.BackColor = SystemColors.Control;
    componentResourceManager.ApplyResources((object) this.cb_tH, "cb_tH");
    this.cb_tH.Checked = true;
    this.cb_tH.CheckState = CheckState.Checked;
    this.cb_tH.MaximumSize = new Size(18, 18);
    this.cb_tH.MinimumSize = new Size(18, 18);
    this.cb_tH.Name = "cb_tH";
    this.cb_tH.UseVisualStyleBackColor = false;
    this.cb_tH.CheckedChanged += new EventHandler(this.DoChangeVerticalSizeChecks);
    this.cb_tLH.BackColor = SystemColors.Control;
    componentResourceManager.ApplyResources((object) this.cb_tLH, "cb_tLH");
    this.cb_tLH.Checked = true;
    this.cb_tLH.CheckState = CheckState.Checked;
    this.cb_tLH.MaximumSize = new Size(18, 18);
    this.cb_tLH.MinimumSize = new Size(18, 18);
    this.cb_tLH.Name = "cb_tLH";
    this.cb_tLH.UseVisualStyleBackColor = false;
    this.cb_tLH.CheckedChanged += new EventHandler(this.DoChangeVerticalSizeChecks);
    this.cb_tRW.BackColor = Color.White;
    componentResourceManager.ApplyResources((object) this.cb_tRW, "cb_tRW");
    this.cb_tRW.MaximumSize = new Size(20, 20);
    this.cb_tRW.MinimumSize = new Size(20, 20);
    this.cb_tRW.Name = "cb_tRW";
    this.cb_tRW.UseVisualStyleBackColor = false;
    this.cb_tRW.CheckedChanged += new EventHandler(this.DoChangeHorizontalSizeChecks);
    this.cb_tW.BackColor = Color.White;
    componentResourceManager.ApplyResources((object) this.cb_tW, "cb_tW");
    this.cb_tW.Checked = true;
    this.cb_tW.CheckState = CheckState.Checked;
    this.cb_tW.MaximumSize = new Size(20, 20);
    this.cb_tW.MinimumSize = new Size(20, 20);
    this.cb_tW.Name = "cb_tW";
    this.cb_tW.UseVisualStyleBackColor = false;
    this.cb_tW.CheckedChanged += new EventHandler(this.DoChangeHorizontalSizeChecks);
    this.cb_tLW.BackColor = Color.White;
    componentResourceManager.ApplyResources((object) this.cb_tLW, "cb_tLW");
    this.cb_tLW.Checked = true;
    this.cb_tLW.CheckState = CheckState.Checked;
    this.cb_tLW.MaximumSize = new Size(20, 20);
    this.cb_tLW.MinimumSize = new Size(20, 20);
    this.cb_tLW.Name = "cb_tLW";
    this.cb_tLW.UseVisualStyleBackColor = false;
    this.cb_tLW.CheckedChanged += new EventHandler(this.DoChangeHorizontalSizeChecks);
    componentResourceManager.ApplyResources((object) this.pictureTableDimensions, "pictureTableDimensions");
    this.pictureTableDimensions.MaximumSize = new Size(272, 178);
    this.pictureTableDimensions.MinimumSize = new Size(272, 178);
    this.pictureTableDimensions.Name = "pictureTableDimensions";
    this.pictureTableDimensions.TabStop = false;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.Controls.Add((Control) this.numeric_tH);
    this.Controls.Add((Control) this.numeric_tBH);
    this.Controls.Add((Control) this.numeric_tLH);
    this.Controls.Add((Control) this.numeric_tRW);
    this.Controls.Add((Control) this.numeric_tW);
    this.Controls.Add((Control) this.numeric_tLW);
    this.Controls.Add((Control) this.cb_tBH);
    this.Controls.Add((Control) this.cb_tH);
    this.Controls.Add((Control) this.cb_tLH);
    this.Controls.Add((Control) this.cb_tRW);
    this.Controls.Add((Control) this.cb_tW);
    this.Controls.Add((Control) this.cb_tLW);
    this.Controls.Add((Control) this.pictureTableDimensions);
    this.MaximumSize = new Size(350, 190);
    this.MinimumSize = new Size(350, 190);
    this.Name = nameof (RectangleEditPanel);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.numeric_tH.EndInit();
    this.numeric_tBH.EndInit();
    this.numeric_tLH.EndInit();
    this.numeric_tRW.EndInit();
    this.numeric_tW.EndInit();
    this.numeric_tLW.EndInit();
    ((ISupportInitialize) this.pictureTableDimensions).EndInit();
    this.ResumeLayout(false);
  }
}
