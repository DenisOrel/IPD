
// Type: Intermech.Controls.ComboBoxPaperSizes
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Controls.Properties;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Printing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;


namespace Intermech.Controls;

/// <summary>Комбобокс для выбора типа бумаги, поддерживаемого принтером</summary>
public class ComboBoxPaperSizes : TypedComboBox<PaperSize>
{
  [NonSerialized]
  private Printer _printer;
  [NonSerialized]
  private ComboBoxPrinters _comboBoxPrinter;

  protected override bool GetItemCaption(PaperSize item, out string caption)
  {
    caption = item.PaperName;
    return true;
  }

  [Bindable(false)]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [CanBeNull]
  public PaperSize SelectedPaperSize
  {
    get => this.SelectedItem;
    set
    {
      if (this.SelectedItem == value)
        return;
      this.SelectedIndex = value != null ? this.Items.IndexOfFirst<PaperSize>((Predicate<PaperSize>) (paperSize => paperSize.RawKind == value.RawKind)) : -1;
    }
  }

  [Bindable(false)]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public PaperKind SelectedPaperKind
  {
    get
    {
      PaperSize selectedItem = this.SelectedItem;
      return selectedItem == null ? PaperKind.Custom : selectedItem.Kind;
    }
    set
    {
      if (this.SelectedItem != null && this.SelectedPaperKind == value)
        return;
      this.SelectedPaperSize = this.Items.FirstOrDefault<PaperSize>((Func<PaperSize, bool>) (paperSize => paperSize.Kind == value)) ?? this.Printer?.DefaultPaperSize;
    }
  }

  [Bindable(false)]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int SelectedPaperRawKind
  {
    get
    {
      PaperSize selectedItem = this.SelectedItem;
      return selectedItem == null ? 0 : selectedItem.RawKind;
    }
    set
    {
      if (this.SelectedItem != null && this.SelectedPaperRawKind == value)
        return;
      this.SelectedPaperSize = this.Items.FirstOrDefault<PaperSize>((Func<PaperSize, bool>) (paperSize => paperSize.RawKind == value)) ?? this.Printer?.DefaultPaperSize;
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [CanBeNull]
  public ComboBoxPrinters ComboBoxPrinter
  {
    get => this._comboBoxPrinter;
    set
    {
      if (this._comboBoxPrinter == value)
        return;
      if (this._comboBoxPrinter != null)
        this._comboBoxPrinter.SelectedIndexChanged -= new EventHandler(this._comboBoxPrinter_SelectedIndexChanged);
      this._comboBoxPrinter = value;
      if (this._comboBoxPrinter != null)
        this._comboBoxPrinter.SelectedIndexChanged += new EventHandler(this._comboBoxPrinter_SelectedIndexChanged);
      this.InitFromPrinter(this._comboBoxPrinter?.SelectedPrinter);
    }
  }

  private void _comboBoxPrinter_SelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.InitFromPrinter(this._comboBoxPrinter?.SelectedPrinter);
  }

  [Bindable(false)]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [CanBeNull]
  public Printer Printer
  {
    get => this._comboBoxPrinter == null ? this._printer : this._comboBoxPrinter.SelectedPrinter;
    set
    {
      if (this._comboBoxPrinter != null)
        throw new Exception("Not allowed with linked ComboBoxPrinter");
      this.InitFromPrinter(value);
    }
  }

  private void InitFromPrinter([CanBeNull] Printer value)
  {
    if (this._printer == value)
      return;
    PaperSize selectedItem = this._printer != null ? this.SelectedItem : (PaperSize) null;
    this._printer = value;
    this.BeginUpdate();
    try
    {
      if (this.Items.Count > 0)
        this.Items.Clear();
      if (this._printer != null && this._printer.PaperSizes.Count > 0)
        this.Items.AddRange((IEnumerable<PaperSize>) this._printer.PaperSizes.OrderBy<PaperSize, PaperKind>((Func<PaperSize, PaperKind>) (paperSize => paperSize.Kind)).ToArray<PaperSize>(this._printer.PaperSizes.Count));
      if (this.Items.Count <= 0 || selectedItem == null)
        return;
      this.SelectedPaperRawKind = selectedItem.RawKind;
    }
    finally
    {
      this.EndUpdate();
    }
  }

  protected override bool GetItemImage(PaperSize paperSize, out Image image)
  {
    switch (paperSize.Kind)
    {
      case PaperKind.Letter:
        image = (Image) Resources.PaperLetter;
        return true;
      case PaperKind.Tabloid:
        image = (Image) Resources.PaperTabloid;
        return true;
      case PaperKind.Legal:
        image = (Image) Resources.PaperLegal;
        return true;
      case PaperKind.Executive:
        image = (Image) Resources.PaperExecutive;
        return true;
      case PaperKind.A3:
        image = (Image) Resources.PaperA3;
        return true;
      case PaperKind.A4:
        image = (Image) Resources.PaperA4;
        return true;
      case PaperKind.A5:
        image = (Image) Resources.PaperA5;
        return true;
      case PaperKind.Standard11x17:
        image = (Image) Resources.Paper11x17;
        return true;
      default:
        image = (Image) null;
        return false;
    }
  }

  protected override bool GetItemRemarks(PaperSize paperSize, out string remarks)
  {
    remarks = $"{Math.Round((double) paperSize.Width * 0.0254, 1, MidpointRounding.AwayFromZero)} см X " + $"{Math.Round((double) paperSize.Height * 0.0254, 1, MidpointRounding.AwayFromZero)} см";
    return true;
  }
}
