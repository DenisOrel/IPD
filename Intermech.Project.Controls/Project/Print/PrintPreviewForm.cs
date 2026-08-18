// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Print.PrintPreviewForm
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Common;
using Intermech.Controls;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Navigator.Interfaces;
using Intermech.Paint;
using Intermech.Print;
using Intermech.Printing;
using Intermech.Project.Controls;
using Intermech.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Print;

public class PrintPreviewForm : 
  ProjectFormBase,
  IContainerControl,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IComponent,
  IDisposable,
  IContextAware,
  ISupportSaveLocks,
  INamedContext,
  ICanBeReadOnly,
  ICanBeReadOnly2,
  IClientProjectContext,
  IProjectViewContext
{
  [CanBeNull]
  private PrintSetupForm _printSetupForm;
  private readonly bool _created;
  private int _updateSettingsCounter;
  [NotNull]
  private readonly Margins _pageMargins = new Margins();
  private PrintAction _printAction;
  private float _rtfDpiY;
  private float _projectGridDpiX;
  private float _projectGridDpiY;
  private int _pageNum = 1;
  private int _startPageNum = 1;
  private int _copyNum = 1;
  private Graphics _g;
  public float _Zoom;
  private bool _isZoomed;
  private const int TextMarginLeftInMM = 2;
  private const int TextMarginRightInMM = 2;
  private const int TextMarginTopInMM = 2;
  private const int TextMarginBottomInMM = 2;
  private int _pageCountVertical;
  private int _pageCountHorizontal;
  private int _pageCountTotal;
  public float _DayWidth = 14f;
  private bool _minimumZoom;
  private bool _manualDatesUpdate;
  private bool _manualPagesUpdate;
  [ItemNotNull]
  private List<PrintPreviewForm.HPageTemplate> _hPageTemplates;
  [ItemNotNull]
  private List<PrintPreviewForm.VPageTemplate> _vPageTemplates;
  private DateTime _editDatesFrom_OldValue = DateTime.MinValue;
  private DateTime _editDatesTo_OldValue = DateTime.MinValue;
  [CanBeNull]
  private static Cursor _zoomCursor;
  private int _overPageIndex = -1;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private SmoothLabel _labelPrint;
  private SmoothLabel _labelPrinter;
  private FlatButton _buttonPrint;
  private SmoothLabel _labelCopies;
  private FlatNumericUpDown _editPagesTo;
  private LinkLabelAdv _linkPrinterProperties;
  private SmoothLabel _labelSettings;
  private ComboBoxPrintPageSettings _comboSettings;
  private FlatDateTimePicker _editDatesFrom;
  private FlatDateTimePicker _editDatesTo;
  private SmoothLabel _labelDatesFrom;
  private SmoothLabel _labelDatesTo;
  private SmoothLabel _labelPagesFrom;
  private SmoothLabel _labelPagesTo;
  private FlatNumericUpDown _editPagesCopies;
  private ComboBoxPaperOrientation _comboPaperOrientation;
  private ComboBoxPaperSizes _comboPaperSize;
  private LinkLabelAdv _linkPageSettings;
  private FlatNumericUpDown _editPagesFrom;
  private FlatButton _buttonClose;
  private Bevel _bevelPaper;
  private ComboBoxPrinters _comboBoxPrinters;
  private SmoothLabel _labelPageNum;
  private FlatRadioButton _radioButtonPagesMany;
  private FlatRadioButton _radioButtonOnePage;
  private Panel _panelShowPages;
  private FlatButton _buttonMoveToRightPage;
  private ImageList _imageListMovePages;
  private FlatButton _buttonMoveToLowerPage;
  private FlatButton _buttonMoveToUpperPage;
  private FlatButton _buttonMoveToLeftPage;
  private ProjectPrintPreviewControl _printPreviewCtrl;
  private PrintDocument _printDocument;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal SmoothLabel LabelPrint
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelPrint.CheckInitializedIn<SmoothLabel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal SmoothLabel LabelPrinter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelPrinter.CheckInitializedIn<SmoothLabel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal FlatButton ButtonPrint
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonPrint.CheckInitializedIn<FlatButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal SmoothLabel LabelCopies
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelCopies.CheckInitializedIn<SmoothLabel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal FlatNumericUpDown EditPagesTo
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editPagesTo.CheckInitializedIn<FlatNumericUpDown>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal LinkLabelAdv LinkPrinterProperties
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._linkPrinterProperties.CheckInitializedIn<LinkLabelAdv>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal SmoothLabel LabelSettings
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelSettings.CheckInitializedIn<SmoothLabel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ComboBoxPrintPageSettings ComboSettings
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._comboSettings.CheckInitializedIn<ComboBoxPrintPageSettings>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal FlatDateTimePicker EditDatesFrom
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editDatesFrom.CheckInitializedIn<FlatDateTimePicker>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal FlatDateTimePicker EditDatesTo
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editDatesTo.CheckInitializedIn<FlatDateTimePicker>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal SmoothLabel LabelDatesFrom
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelDatesFrom.CheckInitializedIn<SmoothLabel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal SmoothLabel LabelDatesTo
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelDatesTo.CheckInitializedIn<SmoothLabel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal SmoothLabel LabelPagesFrom
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelPagesFrom.CheckInitializedIn<SmoothLabel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal SmoothLabel LabelPagesTo
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelPagesTo.CheckInitializedIn<SmoothLabel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal FlatNumericUpDown EditPagesCopies
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editPagesCopies.CheckInitializedIn<FlatNumericUpDown>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ComboBoxPaperOrientation ComboPaperOrientation
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._comboPaperOrientation.CheckInitializedIn<ComboBoxPaperOrientation>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ComboBoxPaperSizes ComboPaperSize
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._comboPaperSize.CheckInitializedIn<ComboBoxPaperSizes>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal LinkLabelAdv LinkPageSettings
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._linkPageSettings.CheckInitializedIn<LinkLabelAdv>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal FlatNumericUpDown EditPagesFrom
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editPagesFrom.CheckInitializedIn<FlatNumericUpDown>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal FlatButton ButtonClose
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonClose.CheckInitializedIn<FlatButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Bevel BevelPaper
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._bevelPaper.CheckInitializedIn<Bevel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ComboBoxPrinters ComboBoxPrinters
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._comboBoxPrinters.CheckInitializedIn<ComboBoxPrinters>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal SmoothLabel LabelPageNum
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelPageNum.CheckInitializedIn<SmoothLabel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal FlatRadioButton RadioButtonPagesMany
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._radioButtonPagesMany.CheckInitializedIn<FlatRadioButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal FlatRadioButton RadioButtonOnePage
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._radioButtonOnePage.CheckInitializedIn<FlatRadioButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Panel PanelShowPages
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._panelShowPages.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal FlatButton ButtonMoveToRightPage
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonMoveToRightPage.CheckInitializedIn<FlatButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ImageList ImageListMovePages
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._imageListMovePages.CheckInitializedIn<ImageList>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal FlatButton ButtonMoveToLowerPage
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonMoveToLowerPage.CheckInitializedIn<FlatButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal FlatButton ButtonMoveToUpperPage
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonMoveToUpperPage.CheckInitializedIn<FlatButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal FlatButton ButtonMoveToLeftPage
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonMoveToLeftPage.CheckInitializedIn<FlatButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ProjectPrintPreviewControl PrintPreviewCtrl
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._printPreviewCtrl.CheckInitializedIn<ProjectPrintPreviewControl>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal PrintDocument PrintDocument
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._printDocument.CheckInitializedIn<PrintDocument>((object) this);
    }
  }

  [NotNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  private PrintSetupForm PrintSetupForm
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._printSetupForm;
  }

  public PrintPreviewForm() => this.InitializeComponent();

  public PrintPreviewForm([NotNull] System.IServiceProvider ownerServices, [CanBeNull] string contextName = null)
    : base(ownerServices, contextName ?? "ProjectPrintPreview")
  {
    this.InitializeComponent();
    this.CreatePrintSetupForm();
    this.ComboBoxPrinters.SelectedPrinter = Printers.DefaultPrinter;
    if (this.ComboBoxPrinters.SelectedPrinter != null)
      this.ComboPaperSize.SelectedPaperRawKind = this.ComboBoxPrinters.SelectedPrinter.DefaultPaperRawKind ?? 0;
    this.LinkPrinterProperties.Left = this.ComboBoxPrinters.Location.X + this.ComboBoxPrinters.Width - this.LinkPrinterProperties.Width;
    this.LinkPageSettings.Left = this.ComboBoxPrinters.Location.X + this.ComboBoxPrinters.Width - this.LinkPageSettings.Width;
    this.PrintPreviewCtrl.Services = this.Services;
    this._manualDatesUpdate = true;
    if (this.Project != null)
    {
      this.EditDatesFrom.Value = this.Project.Start;
      this.EditDatesTo.Value = this.Project.Finish;
    }
    this._manualDatesUpdate = false;
    this._created = true;
  }

  private void CreatePrintSetupForm()
  {
    this._printSetupForm = new PrintSetupForm(this, this.ContextName + ".Setup");
    this.PrintSetupForm.LoadPropertiesFromStorage();
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.components?.Dispose();
      this._printSetupForm?.Dispose();
    }
    base.Dispose(disposing);
  }

  private void _bigComboBox_MeasureItem([CanBeNull] object sender, [NotNull] MeasureItemEventArgs e)
  {
    e.ItemHeight = 44;
  }

  private void _linkPrinterProperties_LinkClicked([CanBeNull] object sender, [NotNull] LinkLabelLinkClickedEventArgs e)
  {
    this.ShowSelectedPrinterProperties();
  }

  internal void ShowSelectedPrinterProperties()
  {
    Printer selectedPrinter = this.ComboBoxPrinters.SelectedPrinter;
    if (selectedPrinter == null || !selectedPrinter.ShowPropertiesDialog(this.Handle, this.ComboPaperSize.SelectedPaperSize, this.ComboPaperOrientation.SelectedOrientation == PaperOrientation.Landscape))
      return;
    this.BeginUpdateSettings();
    try
    {
      PageSettings defaultPageSettings = selectedPrinter.DefaultPageSettings;
      this.ComboPaperOrientation.SelectedOrientation = defaultPageSettings == null || defaultPageSettings.Landscape ? PaperOrientation.Landscape : PaperOrientation.Portrait;
      this.ComboPaperSize.SelectedPaperSize = selectedPrinter.DefaultPaperSize;
      this.UpdateAllDocumentSettings();
    }
    finally
    {
      this.EndUpdateSettings();
    }
  }

  private void SyncPaperSize(PrintPreviewForm.SyncDirection syncDirection)
  {
    ComboBoxPaperSizes comboBoxPaperSizes1 = syncDirection == PrintPreviewForm.SyncDirection.FromPreviewToSetupDlg ? this.ComboPaperSize : this.PrintSetupForm.ComboPaperSize;
    ComboBoxPaperSizes comboBoxPaperSizes2 = syncDirection == PrintPreviewForm.SyncDirection.FromPreviewToSetupDlg ? Intermech.Diagnostics.Check.Optional.NotNull<PrintSetupForm>(this.PrintSetupForm, "PrintSetupForm").ComboPaperSize : this.ComboPaperSize;
    if (comboBoxPaperSizes1.SelectedIndex != -1)
      comboBoxPaperSizes2.SelectedPaperRawKind = comboBoxPaperSizes1.SelectedPaperRawKind;
    else
      comboBoxPaperSizes2.SelectedIndex = -1;
  }

  private void SyncPaperOrientation(PrintPreviewForm.SyncDirection syncDirection)
  {
    if (syncDirection == PrintPreviewForm.SyncDirection.FromPreviewToSetupDlg)
    {
      if (this.ComboPaperOrientation.SelectedIndex == -1)
        return;
      if (this.ComboPaperOrientation.SelectedOrientation == PaperOrientation.Portrait && !this.PrintSetupForm.RadioButtonPortrait.Checked)
      {
        this.PrintSetupForm.RadioButtonPortrait.Checked = true;
      }
      else
      {
        if (this.ComboPaperOrientation.SelectedOrientation != PaperOrientation.Landscape || this.PrintSetupForm.RadioButtonLandscape.Checked)
          return;
        this.PrintSetupForm.RadioButtonLandscape.Checked = true;
      }
    }
    else
      this.ComboPaperOrientation.SelectedOrientation = this.PrintSetupForm.RadioButtonPortrait.Checked ? PaperOrientation.Portrait : PaperOrientation.Landscape;
  }

  [CanBeNull]
  public Printer SelectedPrinter
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.ComboBoxPrinters.SelectedIndex != -1 ? this.ComboBoxPrinters.SelectedPrinter : (Printer) null;
    }
    set
    {
      int num = this.ComboBoxPrinters.Items.IndexOfFirst<Printer>((Predicate<Printer>) (printer => printer.Equals((object) value)));
      if (num == -1 || num == this.ComboBoxPrinters.SelectedIndex)
        return;
      this.ComboBoxPrinters.SelectedIndex = num;
    }
  }

  [CanBeNull]
  public string SelectedPrinterName
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.ComboBoxPrinters.SelectedPrinter?.ToString();
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.SelectPrinter(value);
  }

  private void SelectPrinter(string printerName)
  {
    if (string.IsNullOrEmpty(printerName) && Printers.DefaultPrinter != null)
      printerName = Printers.DefaultPrinter.Name;
    int num = this.ComboBoxPrinters.Items.IndexOfFirst<Printer>((Predicate<Printer>) (printer => printer.Name == printerName));
    if (num == -1 || num == this.ComboBoxPrinters.SelectedIndex)
      return;
    this.ComboBoxPrinters.SelectedIndex = num;
  }

  private void _linkPageSettings_LinkClicked([CanBeNull] object sender, [NotNull] LinkLabelLinkClickedEventArgs e)
  {
    this.SyncPaperOrientation(PrintPreviewForm.SyncDirection.FromPreviewToSetupDlg);
    this.SyncPaperSize(PrintPreviewForm.SyncDirection.FromPreviewToSetupDlg);
    switch (this.PrintSetupForm.ShowDialog())
    {
      case DialogResult.OK:
      case DialogResult.Yes:
        this._minimumZoom = false;
        this.BeginUpdateSettings();
        try
        {
          this.SyncPaperOrientation(PrintPreviewForm.SyncDirection.FromSetupToPreviewDlg);
          this.SyncPaperSize(PrintPreviewForm.SyncDirection.FromSetupToPreviewDlg);
          this.UpdateAllDocumentSettings();
          break;
        }
        finally
        {
          this.EndUpdateSettings();
        }
    }
    this.PrintSetupForm.Dispose();
    this.CreatePrintSetupForm();
  }

  private void _buttonPrint_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.PrintDocument.Print();
  }

  /// <summary>Загрузка свойств в словарь, который будет сохранён в FormStorage при вызове SavePropertiesToStorage</summary>
  public override void FillPropsDictionary([NotNull] Dictionary<string, object> dic)
  {
    base.FillPropsDictionary(dic);
    Printer selectedPrinter = this.SelectedPrinter;
    if (selectedPrinter != null)
    {
      dic["PrinterName"] = (object) selectedPrinter.Name;
      if (selectedPrinter.Port != null)
        dic["PrinterPort"] = (object) selectedPrinter.Port;
      if (selectedPrinter.Driver != null)
        dic["PrinterDriver"] = (object) selectedPrinter.Driver;
    }
    dic["Landscape"] = (object) (this.ComboPaperOrientation.SelectedOrientation == PaperOrientation.Landscape);
    if (this.ComboPaperSize.SelectedPaperRawKind == 0)
      return;
    dic["PaperRawKind"] = (object) this.ComboPaperSize.SelectedPaperRawKind;
  }

  /// <summary>Загрузка свойств из словаря, полученного из FormStorage при вызове LoadPropertiesFromStorage</summary>
  public override void ParseDictionaryFromFormStorage([NotNull] Dictionary<string, object> dic)
  {
    base.ParseDictionaryFromFormStorage(dic);
    this.BeginUpdateSettings();
    try
    {
      string name = (string) null;
      string port = (string) null;
      string driver = (string) null;
      object obj;
      if (dic.TryGetValue("PrinterName", out obj))
        name = (string) obj;
      if (dic.TryGetValue("PrinterPort", out obj))
        port = (string) obj;
      if (dic.TryGetValue("PrinterDriver", out obj))
        driver = (string) obj;
      this.SelectPrinter(Printers.FindActualPrinterName(name, driver, port) ?? Printers.DefaultPrinterName);
      this.ComboPaperOrientation.SelectedOrientation = !dic.TryGetValue("Landscape", out obj) || !(bool) obj ? PaperOrientation.Portrait : PaperOrientation.Landscape;
      if (!dic.TryGetValue("PaperRawKind", out obj))
        return;
      int newRawKind = (int) obj;
      if (this.ComboPaperSize.Items.Contains<PaperSize>((Predicate<PaperSize>) (paperSize => paperSize.RawKind == newRawKind)))
        this.ComboPaperSize.SelectedPaperRawKind = newRawKind;
      else if (this.ComboBoxPrinters.SelectedPrinter != null)
        this.ComboPaperSize.SelectedPaperRawKind = this.ComboBoxPrinters.SelectedPrinter.DefaultPaperRawKind ?? 0;
      else
        this.ComboPaperSize.SelectedIndex = -1;
    }
    finally
    {
      this.EndUpdateSettings();
    }
  }

  private void BeginUpdateSettings() => ++this._updateSettingsCounter;

  private void EndUpdateSettings()
  {
    --this._updateSettingsCounter;
    if (this._updateSettingsCounter != 0 || this.PrintPreviewCtrl.Document == null)
      return;
    this.InitPageTemplates();
    this.PrintPreviewCtrl.InvalidatePreview();
  }

  private void UpdatePageSettings([NotNull] PageSettings pageSettings)
  {
    pageSettings.Landscape = this.ComboPaperOrientation.SelectedOrientation != 0;
    pageSettings.PaperSize = this.ComboPaperSize.SelectedPaperSize ?? pageSettings.PrinterSettings.DefaultPageSettings.PaperSize;
    this._pageMargins.Left = (int) ((double) this.PrintSetupForm.EditMarginLeft.Value * 100.0 / 2.54);
    this._pageMargins.Top = (int) ((double) this.PrintSetupForm.EditMarginTop.Value * 100.0 / 2.54);
    this._pageMargins.Right = (int) ((double) this.PrintSetupForm.EditMarginRight.Value * 100.0 / 2.54);
    this._pageMargins.Bottom = (int) ((double) this.PrintSetupForm.EditMarginBottom.Value * 100.0 / 2.54);
    try
    {
      RectangleF printableArea = this.PrintDocument.DefaultPageSettings.PrintableArea;
      PaperSize paperSize = pageSettings.PaperSize;
      this._pageMargins.Left = Math.Max(pageSettings.Margins.Left, (int) Math.Round((double) printableArea.Left, MidpointRounding.AwayFromZero));
      this._pageMargins.Top = Math.Max(pageSettings.Margins.Top, (int) Math.Round((double) printableArea.Top, MidpointRounding.AwayFromZero));
      this._pageMargins.Right = Math.Max(pageSettings.Margins.Right, (int) Math.Round((double) paperSize.Width - (double) printableArea.Right, MidpointRounding.AwayFromZero));
      pageSettings.Margins.Bottom = Math.Max(pageSettings.Margins.Bottom, (int) Math.Round((double) paperSize.Height - (double) printableArea.Bottom, MidpointRounding.AwayFromZero));
    }
    catch
    {
    }
    pageSettings.Margins = this._pageMargins;
  }

  private Size GetPageWorkAreaSizeInHundredthsOnInch()
  {
    PaperSize paperSize = this.ComboPaperSize.SelectedPaperSize ?? this.SelectedPrinter?.DefaultPaperSize ?? new PrinterSettings().DefaultPageSettings.PaperSize;
    return new Size((this.Landscape ? paperSize.Height : paperSize.Width) - (this._pageMargins.Left + this._pageMargins.Right), (this.Landscape ? paperSize.Width : paperSize.Height) - (this._pageMargins.Top + this._pageMargins.Bottom));
  }

  private void UpdateAllDocumentSettings()
  {
    this.BeginUpdateSettings();
    try
    {
      if (this.ComboBoxPrinters.SelectedPrinter != null)
        this.PrintDocument.PrinterSettings.PrinterName = this.ComboBoxPrinters.SelectedPrinter.Name;
      this.UpdatePageSettings(this.PrintDocument.DefaultPageSettings);
    }
    finally
    {
      this.EndUpdateSettings();
    }
  }

  private void PrintPreviewForm_Shown([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.BeginUpdateSettings();
    try
    {
      this.UpdateAllDocumentSettings();
      this.PrintPreviewCtrl.Document = this.PrintDocument;
    }
    finally
    {
      this.EndUpdateSettings();
    }
  }

  private void _comboBoxPrinters_SelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.BeginUpdateSettings();
    try
    {
      this.UpdateAllDocumentSettings();
    }
    finally
    {
      this.EndUpdateSettings();
    }
  }

  private void _comboPaperOrientation_SelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.BeginUpdateSettings();
    try
    {
      this.PrintDocument.DefaultPageSettings.Landscape = this.ComboPaperOrientation.SelectedOrientation == PaperOrientation.Landscape;
    }
    finally
    {
      this.EndUpdateSettings();
    }
  }

  private void _comboPaperSize_SelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.BeginUpdateSettings();
    try
    {
      this.PrintDocument.DefaultPageSettings.PaperSize = this.ComboPaperSize.SelectedPaperSize ?? this.PrintDocument.DefaultPageSettings.PaperSize;
    }
    finally
    {
      this.EndUpdateSettings();
    }
  }

  private bool HasHeader
  {
    get
    {
      return this.PrintSetupForm.TextBoxHeaderLeft.Text.Trim() != string.Empty || this.PrintSetupForm.TextBoxHeaderCenter.Text.Trim() != string.Empty || this.PrintSetupForm.TextBoxHeaderRight.Text.Trim() != string.Empty;
    }
  }

  /// <summary>Разрешение канвы RTF контролов по вертикали (для пересчёта размеров текста в дюймы)</summary>
  private float RtfDpiY
  {
    get
    {
      if ((double) this._rtfDpiY == 0.0)
      {
        using (Graphics graphics = this.PrintSetupForm.TextOutBoxHeaderLeft.CreateGraphics())
          this._rtfDpiY = graphics.DpiY;
      }
      return this._rtfDpiY;
    }
  }

  private void CheckProjectGridDpiLoaded()
  {
    if ((double) this._projectGridDpiX != 0.0)
      return;
    using (Graphics graphics = this.ProjectView.GridView.CreateGraphics())
    {
      this._projectGridDpiX = graphics.DpiX;
      this._projectGridDpiY = graphics.DpiY;
    }
  }

  private float ProjectGridDpiX
  {
    get
    {
      this.CheckProjectGridDpiLoaded();
      return this._projectGridDpiX;
    }
  }

  private float ProjectGridDpiY
  {
    get
    {
      this.CheckProjectGridDpiLoaded();
      return this._projectGridDpiY;
    }
  }

  private int GetTitleHeight([NotNull] RichTextBox rtf1, [NotNull] RichTextBox rtf2, [NotNull] RichTextBox rtf3)
  {
    int titleHeight = Math.Max(PrintPreviewForm.GetRichTextBoxContentHeight(rtf1), Math.Max(PrintPreviewForm.GetRichTextBoxContentHeight(rtf2), PrintPreviewForm.GetRichTextBoxContentHeight(rtf3)));
    if (titleHeight > 0)
      titleHeight = (int) ((double) (titleHeight * 100) / (double) this.RtfDpiY);
    return titleHeight;
  }

  private int HeaderHeightInHundredthsOfAnInch
  {
    get
    {
      return this.GetTitleHeight((RichTextBox) this.PrintSetupForm.TextOutBoxHeaderLeft, (RichTextBox) this.PrintSetupForm.TextOutBoxHeaderCenter, (RichTextBox) this.PrintSetupForm.TextOutBoxHeaderRight);
    }
  }

  private int FooterHeightInHundredthsOfAnInch
  {
    get
    {
      return this.GetTitleHeight((RichTextBox) this.PrintSetupForm.TextOutBoxFooterLeft, (RichTextBox) this.PrintSetupForm.TextOutBoxFooterCenter, (RichTextBox) this.PrintSetupForm.TextOutBoxFooterRight);
    }
  }

  /// <summary>Получение высоты содержимого RichTextBox-а в пикселах для DPI монитора</summary>
  private static int GetRichTextBoxContentHeight([NotNull] RichTextBox richTextBox)
  {
    int richTextBoxContentHeight = 0;
    if (richTextBox.Text.Trim() != string.Empty)
    {
      string rtf = richTextBox.Rtf;
      richTextBox.Text = string.Empty;
      richTextBox.ContentsResized += new ContentsResizedEventHandler(ContentsResizedEventHandler);
      richTextBox.Rtf = rtf;
      richTextBox.ContentsResized -= new ContentsResizedEventHandler(ContentsResizedEventHandler);
    }
    return richTextBoxContentHeight;

    void ContentsResizedEventHandler(object sender, ContentsResizedEventArgs e)
    {
      richTextBoxContentHeight = e.NewRectangle.Height;
    }
  }

  private bool HasFooter
  {
    get
    {
      return this.PrintSetupForm.TextBoxFooterLeft.Text.Trim() != string.Empty || this.PrintSetupForm.TextBoxFooterCenter.Text.Trim() != string.Empty || this.PrintSetupForm.TextBoxFooterRight.Text.Trim() != string.Empty;
    }
  }

  private int RealPageNum
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._pageNum - this._startPageNum + 1;
    }
  }

  private void _printDocument_BeginPrint([CanBeNull] object sender, [NotNull] PrintEventArgs e)
  {
    this._startPageNum = (int) this.PrintSetupForm.EditFirstPageNum.Value;
    this._pageNum = this._startPageNum;
    this._printAction = e.PrintAction;
    this._copyNum = 1;
  }

  private int BeforeZoom(int value)
  {
    return !this._isZoomed ? value : (int) ((double) value / (double) this._Zoom);
  }

  private Point BeforeZoom(Point point)
  {
    return !this._isZoomed ? point : new Point((int) ((double) point.X / (double) this._Zoom), (int) ((double) point.Y / (double) this._Zoom));
  }

  private Size BeforeZoom(Size size)
  {
    return !this._isZoomed ? size : new Size((int) ((double) size.Width / (double) this._Zoom), (int) ((double) size.Height / (double) this._Zoom));
  }

  private Rectangle BeforeZoom(Rectangle rect)
  {
    return !this._isZoomed ? rect : new Rectangle((int) ((double) rect.X / (double) this._Zoom), (int) ((double) rect.Y / (double) this._Zoom), (int) ((double) rect.Width / (double) this._Zoom), (int) ((double) rect.Height / (double) this._Zoom));
  }

  private RectangleF BeforeZoom(RectangleF rect)
  {
    return !this._isZoomed ? rect : new RectangleF(rect.X / this._Zoom, rect.Y / this._Zoom, rect.Width / this._Zoom, rect.Height / this._Zoom);
  }

  private PointF BeforeZoom(PointF point)
  {
    return !this._isZoomed ? point : new PointF(point.X / this._Zoom, point.Y / this._Zoom);
  }

  private void _printDocument_EndPrint([CanBeNull] object sender, [NotNull] PrintEventArgs e)
  {
    this._pageNum = 1;
    this._startPageNum = 1;
  }

  public bool Landscape
  {
    get => this.ComboPaperOrientation.SelectedOrientation == PaperOrientation.Landscape;
  }

  private int MmToPixels(float millimeters) => (int) ((double) millimeters * 100.0 / 25.4);

  private int MmToPixelsZoomed(float millimeters)
  {
    return (int) ((double) millimeters * 100.0 * (double) this._Zoom / 25.4);
  }

  private int ApplyZoom(int coord) => (int) ((double) coord * (double) this._Zoom);

  private void _printDocument_PrintPage([CanBeNull] object sender, [NotNull] PrintPageEventArgs e)
  {
    this._g = e.Graphics;
    PageSettings pageSettings = e.PageSettings;
    Graphics g = this._g;
    Rectangle marginBounds1 = e.MarginBounds;
    double dx = (double) marginBounds1.Left - (double) pageSettings.PrintableArea.Left;
    marginBounds1 = e.MarginBounds;
    double dy = (double) marginBounds1.Top - (double) pageSettings.PrintableArea.Top;
    g.TranslateTransform((float) dx, (float) dy);
    Rectangle marginBounds2 = e.MarginBounds;
    int width = marginBounds2.Width;
    marginBounds2 = e.MarginBounds;
    int height = marginBounds2.Height;
    Rectangle pageWorkAreaRect = new Rectangle(0, 0, width, height);
    Rectangle rectangle1 = pageWorkAreaRect;
    this._g.DrawRectangle(Pens.Black, pageWorkAreaRect);
    int textMarginLeft = this.MmToPixelsZoomed(2f);
    int textMarginRight = this.MmToPixelsZoomed(2f);
    int textMarginTop = this.MmToPixelsZoomed(2f);
    int textMarginBottom = this.MmToPixelsZoomed(2f);
    Size size = this.BeforeZoom(rectangle1.Size);
    IntPtr hdc = this._g.GetHdc();
    Metafile metafile;
    try
    {
      metafile = new Metafile(hdc, new Rectangle(Point.Empty, size), MetafileFrameUnit.Point, EmfType.EmfOnly);
    }
    finally
    {
      this._g.ReleaseHdc(hdc);
    }
    Graphics zoomOutputGraphics = Graphics.FromImage((Image) metafile);
    try
    {
      if (this.HasHeader || this.HasFooter)
      {
        this.PrintSetupForm.UpdateTextOutTitleRtfs(this._pageNum, this._pageCountTotal + (int) this.PrintSetupForm.EditFirstPageNum.Value - 1);
        if (this.HasHeader)
        {
          int num = DrawTitleText(this.PrintSetupForm.TextOutBoxHeaderLeft, this.PrintSetupForm.TextOutBoxHeaderCenter, this.PrintSetupForm.TextOutBoxHeaderRight, pageWorkAreaRect.Location, false);
          if (num > 0)
          {
            this._g.DrawLine(Pens.Black, pageWorkAreaRect.Left, pageWorkAreaRect.Y + num, pageWorkAreaRect.Right, pageWorkAreaRect.Y + num);
            pageWorkAreaRect.Y += num;
            pageWorkAreaRect.Height -= num;
          }
        }
        if (this.HasFooter)
        {
          int num = DrawTitleText(this.PrintSetupForm.TextOutBoxFooterLeft, this.PrintSetupForm.TextOutBoxFooterCenter, this.PrintSetupForm.TextOutBoxFooterRight, new Point(pageWorkAreaRect.Left, pageWorkAreaRect.Bottom), true);
          if (num > 0)
          {
            this._g.DrawLine(Pens.Black, pageWorkAreaRect.Left, pageWorkAreaRect.Bottom - num, pageWorkAreaRect.Right, pageWorkAreaRect.Bottom - num);
            pageWorkAreaRect.Height -= num;
          }
        }
      }
      ProjectDataGridView gridView = this.ProjectView.GridView;
      if (this._hPageTemplates != null)
      {
        if (this._vPageTemplates != null)
        {
          PrintPreviewForm.HPageTemplate hPageTemplate = this._hPageTemplates[(this.RealPageNum - 1) % this._pageCountHorizontal];
          PrintPreviewForm.VPageTemplate vPageTemplate = this._vPageTemplates[(this.RealPageNum - 1) / this._pageCountHorizontal];
          int gridY = this.GetGridY(gridView.ColumnHeadersHeight);
          Rectangle rectangle2;
          if (hPageTemplate._GridWidth > 0)
          {
            this._g.DrawLine(Pens.Black, pageWorkAreaRect.Left, pageWorkAreaRect.Top + gridY, pageWorkAreaRect.Left + hPageTemplate._GridWidth, pageWorkAreaRect.Top + gridY);
            int left1 = pageWorkAreaRect.Left;
            new DataGridViewAdvancedBorderStyle().All = DataGridViewAdvancedCellBorderStyle.None;
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Near;
            format.Trimming = StringTrimming.None;
            foreach (PrintPreviewForm.Column column in hPageTemplate._Columns)
            {
              int x = left1;
              left1 += column._Width;
              this._g.DrawLine(Pens.Black, left1, pageWorkAreaRect.Top, left1, pageWorkAreaRect.Top + gridY + vPageTemplate._GridHeight);
              rectangle2 = this.BeforeZoom(new Rectangle(x, pageWorkAreaRect.Top, column._Width, gridY));
              zoomOutputGraphics.SetClip(rectangle2);
              if (column._Index == gridView.ImagesColumn.Index)
              {
                zoomOutputGraphics.DrawImageUnscaled((Image) Images.InfoImage, rectangle2.X + 5, rectangle2.Y + 5);
              }
              else
              {
                rectangle2.X += 2;
                rectangle2.Y += 2;
                rectangle2.Width -= 5;
                rectangle2.Height -= 5;
                zoomOutputGraphics.DrawString(column._Caption, gridView.Font, Brushes.Black, (RectangleF) rectangle2, format);
              }
              zoomOutputGraphics.ResetClip();
            }
            int num1 = pageWorkAreaRect.Top + gridY;
            foreach (PrintPreviewForm.Row row1 in vPageTemplate._Rows)
            {
              int y = num1;
              num1 += row1._Height;
              this._g.DrawLine(Pens.Black, pageWorkAreaRect.Left, num1, pageWorkAreaRect.Left + hPageTemplate._GridWidth, num1);
              DataGridViewRow row2 = gridView.Rows[row1._Index];
              int left2 = pageWorkAreaRect.Left;
              foreach (PrintPreviewForm.Column column1 in hPageTemplate._Columns)
              {
                int x = left2;
                left2 += column1._Width;
                rectangle2 = this.BeforeZoom(new Rectangle(x, y, column1._Width, row1._Height));
                DataGridViewColumn column2 = column1._Index >= 0 ? gridView.Columns[column1._Index] : (DataGridViewColumn) null;
                if (column1._Index >= 0 && column1._Index == gridView.ImagesColumn.Index)
                {
                  DataGridViewCell cell = row2.Cells[column1._Index];
                  if (!(cell.Tag is ProjectDataGridView.ImageInfos imageInfos))
                  {
                    imageInfos = new ProjectDataGridView.ImageInfos();
                    cell.Tag = (object) imageInfos;
                    Task task = gridView.GetTask(row2);
                    if (task != null)
                    {
                      if (task is Intermech.Project.Project)
                        imageInfos.Add(Images.ProjectImage, Intermech.Project.Localization.GetString("Project"));
                      if (task.Status == TaskStatus.Completed)
                        imageInfos.Add((Image) Images.CheckBitmap, $"{Intermech.Project.Localization.GetString("TaskParamStatus")}: {SimpleFuncs.GetEnumDescription((Enum) TaskStatus.Completed)}");
                      else if (task.ConstraintDate != DateTime.MinValue)
                        imageInfos.Add((Image) Images.ConstraintImage, string.Format(Intermech.Project.Localization.GetString("TaskHasConstraint"), (object) SimpleFuncs.GetEnumDescription((Enum) task.ConstraintType), (object) task.ConstraintDate));
                      if (task.Notes != string.Empty)
                        imageInfos.Add((Image) Images.NotesImage, $"{Intermech.Project.Localization.GetString("TaskParamNotes")}: '{task.Notes.Truncate(200)}'");
                    }
                  }
                  int num2 = 0;
                  int num3 = 0;
                  Point location = rectangle2.Location;
                  location.Offset(4, 4);
                  int num4 = 4;
                  foreach (ProjectDataGridView.ImageInfo imageInfo in (List<ProjectDataGridView.ImageInfo>) imageInfos)
                  {
                    Image image = imageInfo._Image;
                    if (location.X + image.Width > rectangle2.Right && num2 + num3 + image.Height < rectangle2.Bottom)
                    {
                      location.X = rectangle2.Location.X + 4;
                      num2 += num3;
                      num3 = 0;
                      location.Y = num2 + num4;
                    }
                    imageInfo._Bounds = new Rectangle(location, image.Size);
                    zoomOutputGraphics.DrawImageUnscaled(image, location);
                    if (image.Height > num3)
                      num3 = image.Height;
                    location.X += image.Width + num4;
                  }
                }
                else
                {
                  string s = column1._Index >= 0 ? row2.Cells[column1._Index].FormattedValue.ToString().Trim() : (row1._Index + 1).ToString();
                  if (!string.IsNullOrEmpty(s))
                  {
                    rectangle2.X += 2;
                    rectangle2.Y += 2;
                    rectangle2.Width -= 5;
                    rectangle2.Height -= 5;
                    if (column1._Index == gridView.NameDataGridViewColumn.Index)
                    {
                      Task task = gridView.GetTask(row2);
                      if (task != null && task.IndentLevel > 0)
                      {
                        int num5 = task.IndentLevel * 20;
                        rectangle2.X += num5;
                        rectangle2.Width -= num5;
                      }
                    }
                    Font font = gridView.Font;
                    if (column2 != null)
                    {
                      DataGridViewCell cell = row2.Cells[column1._Index];
                      font = cell.Style.Font ?? row2.DefaultCellStyle.Font ?? column2.DefaultCellStyle.Font ?? gridView.Font;
                      switch (cell.Style.Alignment)
                      {
                        case DataGridViewContentAlignment.TopLeft:
                        case DataGridViewContentAlignment.MiddleLeft:
                        case DataGridViewContentAlignment.BottomLeft:
                          format.Alignment = StringAlignment.Near;
                          break;
                        case DataGridViewContentAlignment.TopCenter:
                        case DataGridViewContentAlignment.MiddleCenter:
                        case DataGridViewContentAlignment.BottomCenter:
                          format.Alignment = StringAlignment.Center;
                          break;
                        case DataGridViewContentAlignment.TopRight:
                        case DataGridViewContentAlignment.MiddleRight:
                        case DataGridViewContentAlignment.BottomRight:
                          format.Alignment = StringAlignment.Far;
                          break;
                      }
                    }
                    else
                      format.Alignment = StringAlignment.Near;
                    zoomOutputGraphics.DrawString(s, font, Brushes.Black, (RectangleF) rectangle2, format);
                  }
                }
              }
            }
          }
          if (hPageTemplate._GanttWidth > 0)
          {
            GanttChart ganttChart = this.ProjectView.GanttChart;
            RectangleF rect = this.BeforeZoom(new RectangleF((float) (pageWorkAreaRect.Left + hPageTemplate._GridWidth) + 0.5f, (float) pageWorkAreaRect.Top + 0.5f, (float) hPageTemplate._GanttWidth - 1f, (float) (vPageTemplate._GridHeight + gridY) - 0.5f));
            GraphicsState gstate = zoomOutputGraphics.Save();
            zoomOutputGraphics.TranslateTransform(rect.Left, rect.Top);
            rect.Offset(-rect.Left, -rect.Top);
            zoomOutputGraphics.SetClip(rect);
            int spacerDaysWidth = ganttChart._SpacerDaysWidth;
            ganttChart._SpacerDaysWidth = 0;
            try
            {
              if (hPageTemplate._GanttStartDate.Hour != 0 || hPageTemplate._GanttStartDate.Minute != 0)
              {
                int num = (int) ((double) (hPageTemplate._GanttStartDate.Hour * 60 + hPageTemplate._GanttStartDate.Minute) * (double) this._DayWidth / 1440.0);
                zoomOutputGraphics.TranslateTransform((float) -num, 0.0f);
              }
              if ((double) rect.Height > 0.004999999888241291)
              {
                if ((double) rect.Width > 0.004999999888241291)
                {
                  if (!vPageTemplate._Rows.IsEmpty<PrintPreviewForm.Row>())
                    ganttChart.Draw(zoomOutputGraphics, 0, 0, this.ProjectView.Project, vPageTemplate._Rows.First<PrintPreviewForm.Row>()._Index, vPageTemplate._Rows.Count, hPageTemplate._GanttStartDate, hPageTemplate._Days, this.BeforeZoom(gridY), new GanttChart.GetRowTopYDelegate(GetRowTopY), this._DayWidth, (int) rect.Height, ganttChart.Font, Brushes.White, Brushes.Black, Pens.DarkGray, Pens.LightGray, Color.White, Color.White, Color.Gray, ganttChart.StandardTaskBrush, ganttChart.CriticalTaskBrush, ganttChart.ParentTaskBrush, ganttChart.MilestoneTaskBrush, ganttChart.PercentCompletedBrush, ganttChart.PercentNotCompletedBrush, ganttChart.StandardTaskPen, ganttChart.CriticalTaskPen, ganttChart.ParentTaskPen, ganttChart.MilestoneTaskPen, ganttChart.MetConstraintPen, ganttChart.NotMetConstraintPen, ganttChart.HighlightCriticalTasks, false, ganttChart.ScaleType, ganttChart.UseNumericScaleValues, ganttChart.NumericScaleType, Brushes.LightGray, Pens.Gray, Pens.Black, ganttChart.TaskPens, ganttChart.TaskBrushes, ganttChart.RectangleRoundnessPercent, ganttChart.RectangleHeightPercent);
                }
              }
            }
            finally
            {
              ganttChart._SpacerDaysWidth = spacerDaysWidth;
              zoomOutputGraphics.ResetClip();
              zoomOutputGraphics.Restore(gstate);
            }
            rectangle2 = new Rectangle(pageWorkAreaRect.Left + hPageTemplate._GridWidth, pageWorkAreaRect.Top, hPageTemplate._GanttWidth, vPageTemplate._GridHeight + gridY);
            this._g.DrawRectangle(Pens.Black, rectangle2);
          }

          int GetRowTopY(int rowIndex) => this.BeforeZoom(vPageTemplate._Rows[rowIndex]._TopY);
        }
      }
    }
    finally
    {
      zoomOutputGraphics.Dispose();
      GraphicsState gstate = this._g.Save();
      try
      {
        this._g.ScaleTransform(this._Zoom, this._Zoom);
        this._g.DrawImage((Image) metafile, Point.Empty);
      }
      finally
      {
        this._g.Restore(gstate);
      }
      metafile.Dispose();
    }
    this._g = (Graphics) null;
    ++this._pageNum;
    e.HasMorePages = this.RealPageNum <= this._pageCountTotal;
    if (e.HasMorePages || this._printAction != PrintAction.PrintToPrinter || !((Decimal) this._copyNum < this._editPagesCopies.Value))
      return;
    ++this._copyNum;
    this._pageNum = this._startPageNum;
    e.HasMorePages = true;

    int DrawTitleText(
      RichTextBoxAdv titleLeft,
      RichTextBoxAdv titleCenter,
      RichTextBoxAdv titleRight,
      Point textOutLocation,
      bool textOutLocationMinusTitleHeight)
    {
      int titleHeight = this.ApplyZoom(this.GetTitleHeight((RichTextBox) titleLeft, (RichTextBox) titleCenter, (RichTextBox) titleRight)) + textMarginTop + textMarginBottom;
      DrawTitleRtf(titleLeft);
      DrawTitleRtf(titleCenter);
      DrawTitleRtf(titleRight);
      return titleHeight;

      void DrawTitleRtf(RichTextBoxAdv richTextBox)
      {
        if (!(richTextBox.Text.Trim() != string.Empty))
          return;
        richTextBox.Draw(zoomOutputGraphics, this.BeforeZoom(new Rectangle(textOutLocation.X + textMarginLeft, textOutLocationMinusTitleHeight ? textOutLocation.Y + textMarginTop - titleHeight : textOutLocation.Y + textMarginTop, pageWorkAreaRect.Width - (textMarginLeft + textMarginRight), titleHeight)));
      }
    }
  }

  private int GetGridX(int xInPixels)
  {
    return (int) ((double) (110 * xInPixels) * (double) this._Zoom / (double) this.ProjectGridDpiX);
  }

  private int GetGridY(int yInPixels)
  {
    return (int) ((double) (110 * yInPixels) * (double) this._Zoom / (double) this.ProjectGridDpiX);
  }

  private int GetGanttWidth()
  {
    DateTime date1 = this.EditDatesTo.Value;
    date1 = date1.Date;
    DateTime dateTime = date1.AddDays(1.0);
    date1 = this.EditDatesFrom.Value;
    DateTime date2 = date1.Date;
    return (int) ((double) (dateTime - date2).Days * (double) this._DayWidth);
  }

  public int CurrentPage
  {
    get => this._pageNum;
    set
    {
      if (value == this._pageNum)
        return;
      if (value > 0 && value <= this._pageCountTotal)
        this._pageNum = value;
      this.PrintPreviewCtrl.StartPage = this._pageNum - 1;
      this.UpdateNavigateButtons();
    }
  }

  private void UpdateNavigateButtons()
  {
    this.ButtonMoveToLeftPage.Enabled = this.RadioButtonOnePage.Checked && this._pageCountHorizontal > 1 && (this.RealPageNum - 1) % this._pageCountHorizontal > 0;
    this.ButtonMoveToLeftPage.ImageIndex = this.ButtonMoveToLeftPage.Enabled ? 0 : 4;
    this.ButtonMoveToRightPage.Enabled = this.RadioButtonOnePage.Checked && this._pageCountHorizontal > 1 && this.RealPageNum % this._pageCountHorizontal > 0;
    this.ButtonMoveToRightPage.ImageIndex = this.ButtonMoveToRightPage.Enabled ? 3 : 7;
    this.ButtonMoveToUpperPage.Enabled = this.RadioButtonOnePage.Checked && this._pageCountVertical > 1 && this.RealPageNum > this._pageCountHorizontal;
    this.ButtonMoveToUpperPage.ImageIndex = this.ButtonMoveToUpperPage.Enabled ? 1 : 5;
    this.ButtonMoveToLowerPage.Enabled = this.RadioButtonOnePage.Checked && this._pageCountVertical > 1 && this.RealPageNum <= this._pageCountTotal - this._pageCountHorizontal;
    this.ButtonMoveToLowerPage.ImageIndex = this.ButtonMoveToLowerPage.Enabled ? 2 : 6;
    this.LabelPageNum.Text = this.RadioButtonOnePage.Checked ? $"{this.RealPageNum} из {this._pageCountTotal}" : $"{this._pageCountVertical} строк {this._pageCountHorizontal} столбцов";
  }

  private void _buttonMoveToLeftPage_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    --this.CurrentPage;
  }

  private void _buttonMoveToUpperPage_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.CurrentPage -= this._pageCountHorizontal;
  }

  private void _buttonMoveToLowerPage_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.CurrentPage += this._pageCountHorizontal;
  }

  private void _buttonMoveToRightPage_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    ++this.CurrentPage;
  }

  private void InitPageTemplates()
  {
    ProjectDataGridView projectGridView = this.ProjectView.GridView;
    int ganttWidth = 0;
    int everyPageGridWidth = 0;
    this._Zoom = this.PrintSetupForm.RadioButtonSetScalePercents.Checked ? (float) this.PrintSetupForm.EditScale.Value / 100f : 1f;
    if (this._minimumZoom)
      this._Zoom = 0.1f;
    CalcSizes();
    if (!this.PrintSetupForm.RadioButtonSetScalePercents.Checked && !this._minimumZoom)
    {
      float val1 = 1f;
      float val2 = 1f;
      Size totalPageWorkAreaSize;
      if ((Decimal) this._pageCountHorizontal > this.PrintSetupForm.EditNumScalePagesWidth.Value)
      {
        int num = this._hPageTemplates.Aggregate<PrintPreviewForm.HPageTemplate, int>(0, (Func<int, PrintPreviewForm.HPageTemplate, int>) ((oldSum, hPageTemplate) => oldSum + hPageTemplate._GridWidth - everyPageGridWidth));
        val1 = (float) ((int) this.PrintSetupForm.EditNumScalePagesWidth.Value * (totalPageWorkAreaSize.Width - everyPageGridWidth)) / (float) (num + ganttWidth);
      }
      if ((Decimal) this._pageCountVertical > this.PrintSetupForm.EditNumScalePagesHeight.Value)
      {
        int num1 = this._vPageTemplates.Aggregate<PrintPreviewForm.VPageTemplate, int>(0, (Func<int, PrintPreviewForm.VPageTemplate, int>) ((oldSum, vPageTemplate) => oldSum + vPageTemplate._GridHeight));
        int num2 = (int) this.PrintSetupForm.EditNumScalePagesHeight.Value;
        int titleTotalHeight;
        int gridHeaderHeight;
        val2 = (float) (num2 * totalPageWorkAreaSize.Height) / (float) (num1 + titleTotalHeight * num2 + gridHeaderHeight * num2);
        int totalGridRowsHeight;

        void CalcSizes()
        {
          this._isZoomed = (double) this._Zoom != 1.0;
          this._pageCountVertical = 1;
          this._pageCountTotal = 1;
          totalPageWorkAreaSize = this.GetPageWorkAreaSizeInHundredthsOnInch();
          Size size = totalPageWorkAreaSize;
          int pixelsZoomed1 = this.MmToPixelsZoomed(2f);
          int pixelsZoomed2 = this.MmToPixelsZoomed(2f);
          titleTotalHeight = 0;
          titleTotalHeight += this.ApplyZoom(this.GetTitleHeight((RichTextBox) this.PrintSetupForm.TextOutBoxHeaderLeft, (RichTextBox) this.PrintSetupForm.TextOutBoxHeaderCenter, (RichTextBox) this.PrintSetupForm.TextOutBoxHeaderRight)) + pixelsZoomed1 + pixelsZoomed2;
          titleTotalHeight += this.ApplyZoom(this.GetTitleHeight((RichTextBox) this.PrintSetupForm.TextOutBoxFooterLeft, (RichTextBox) this.PrintSetupForm.TextOutBoxFooterCenter, (RichTextBox) this.PrintSetupForm.TextOutBoxFooterRight)) + pixelsZoomed1 + pixelsZoomed2;
          size.Height -= titleTotalHeight;
          gridHeaderHeight = this.GetGridY(projectGridView.ColumnHeadersHeight);
          size.Height -= gridHeaderHeight;
          int capacity = this.PrintSetupForm.CheckBoxPrintSelectedColumns.Checked ? (int) this.PrintSetupForm.EditPrintSelectedColumnsCount.Value : 0;
          List<DataGridViewColumn> dataGridViewColumnList = new List<DataGridViewColumn>(projectGridView.Columns.Count);
          if (this.PrintSetupForm.CheckBoxPrintAllColumns.Checked)
          {
            dataGridViewColumnList.AddRange(projectGridView.Columns.Cast<DataGridViewColumn>().Where<DataGridViewColumn>((Func<DataGridViewColumn, bool>) (column => column.Visible)));
          }
          else
          {
            int num = 0;
            foreach (DataGridViewColumn dataGridViewColumn in projectGridView.Columns.Cast<DataGridViewColumn>().Where<DataGridViewColumn>((Func<DataGridViewColumn, bool>) (column => column.Visible)))
            {
              num += dataGridViewColumn.Width;
              if (num <= projectGridView.Width)
                dataGridViewColumnList.Add(dataGridViewColumn);
              else
                break;
            }
          }
          int count = dataGridViewColumnList.Count;
          int num1 = count;
          int num2 = -1;
          List<PrintPreviewForm.Column> everyPageColumns = new List<PrintPreviewForm.Column>(capacity);
          if (capacity > 0)
          {
            everyPageGridWidth = projectGridView.RowHeadersWidth;
            everyPageColumns.Add(new PrintPreviewForm.Column(projectGridView.RowHeadersWidth, -1, "Ид."));
            int index = num2 + 1;
            int num3 = capacity - 1;
            if (num3 > 0)
            {
              int num4 = projectGridView.RowHeadersWidth;
              for (; index < num3 && index < count; ++index)
              {
                DataGridViewColumn dataGridViewColumn = dataGridViewColumnList[index];
                int gridX = this.GetGridX(dataGridViewColumn.Width);
                if (everyPageGridWidth + gridX >= size.Width)
                {
                  if (everyPageColumns.Count > 0)
                    everyPageColumns.RemoveAt(everyPageColumns.Count - 1);
                  everyPageGridWidth -= num4;
                  break;
                }
                everyPageColumns.Add(new PrintPreviewForm.Column(gridX, dataGridViewColumn.Index, dataGridViewColumn.HeaderText ?? string.Empty));
                everyPageGridWidth += gridX;
                num4 = gridX;
              }
            }
          }
          this._hPageTemplates = new List<PrintPreviewForm.HPageTemplate>();
          DateTime ganttStartDate = this.EditDatesFrom.Value.Date;
          StartNewHorizontalPage();
          int index1 = everyPageColumns.Count - 1;
          if (everyPageColumns.Count > 0)
            num1 -= everyPageColumns.Count - 1;
          int num5 = 0;
          bool flag = false;
          this._DayWidth = this.ProjectView.GanttChart.DayWidth;
          PrintPreviewForm.HPageTemplate hPageTemplate;
          while (!flag || num5 > 0)
          {
            if (num1 > 0)
            {
              if (index1 == -1)
              {
                hPageTemplate._Columns.Add(new PrintPreviewForm.Column(projectGridView.RowHeadersWidth, -1, "Ид."));
                ++index1;
                hPageTemplate._GridWidth += projectGridView.RowHeadersWidth;
              }
              DataGridViewColumn dataGridViewColumn = dataGridViewColumnList[index1];
              int width = Math.Min(this.GetGridX(dataGridViewColumn.Width), size.Width);
              if (hPageTemplate._GridWidth + width > size.Width)
              {
                StartNewHorizontalPage();
              }
              else
              {
                hPageTemplate._Columns.Add(new PrintPreviewForm.Column(width, dataGridViewColumn.Index, dataGridViewColumn.HeaderText ?? string.Empty));
                hPageTemplate._GridWidth += width;
                --num1;
                ++index1;
              }
            }
            else
            {
              hPageTemplate._GanttWidth = size.Width - hPageTemplate._GridWidth;
              if (!flag)
              {
                ganttWidth = (int) ((double) this.GetGanttWidth() * (double) this._Zoom);
                num5 = ganttWidth - hPageTemplate._GanttWidth;
                if (num5 > 0)
                {
                  if (num5 % (size.Width - everyPageGridWidth) > 0)
                  {
                    int num6 = hPageTemplate._GanttWidth + (num5 / (size.Width - everyPageGridWidth) + 1) * (size.Width - everyPageGridWidth);
                    this._DayWidth *= (float) num6 / (float) (hPageTemplate._GanttWidth + num5);
                    num5 = num6 - hPageTemplate._GanttWidth;
                  }
                }
                else if (num5 < 0)
                  this._DayWidth *= (float) hPageTemplate._GanttWidth / (float) ganttWidth;
                flag = true;
              }
              else
                num5 -= hPageTemplate._GanttWidth;
              hPageTemplate._Days = (int) Math.Ceiling((double) hPageTemplate._GanttWidth / ((double) this._DayWidth * (double) this._Zoom));
              ganttStartDate = hPageTemplate.GetGanttDateByXPos((float) hPageTemplate._GanttWidth / this._Zoom);
              if (num5 > 0)
                StartNewHorizontalPage();
            }
          }
          this._pageCountHorizontal = this._hPageTemplates.Count;
          this._vPageTemplates = new List<PrintPreviewForm.VPageTemplate>();
          PrintPreviewForm.VPageTemplate vpageTemplate = new PrintPreviewForm.VPageTemplate();
          this._vPageTemplates.Add(vpageTemplate);
          int index2 = 0;
          int topY = 0;
          totalGridRowsHeight = 0;
          foreach (DataGridViewRow row in (IEnumerable) projectGridView.Rows)
          {
            if (!row.IsNewRow)
            {
              int gridY = this.GetGridY(row.Height);
              if (vpageTemplate._GridHeight + gridY > size.Height)
              {
                vpageTemplate = new PrintPreviewForm.VPageTemplate();
                this._vPageTemplates.Add(vpageTemplate);
                topY = 0;
              }
              Task task = projectGridView.GetTask(row);
              vpageTemplate._Rows.Add(new PrintPreviewForm.Row(topY, gridY, index2, task?.Name ?? string.Empty));
              topY += gridY;
              ++index2;
              totalGridRowsHeight += gridY;
              vpageTemplate._GridHeight += gridY;
            }
            else
              break;
          }
          this._pageCountVertical = this._vPageTemplates.Count;

          void StartNewHorizontalPage()
          {
            ++this._pageCountHorizontal;
            hPageTemplate = new PrintPreviewForm.HPageTemplate(this, this._pageCountHorizontal, ganttStartDate, everyPageColumns.Count > 0 ? everyPageColumns : (List<PrintPreviewForm.Column>) null);
            this._hPageTemplates.Add(hPageTemplate);
          }
        }
      }
      float num3 = Math.Min(val1, val2);
      if ((double) num3 < 0.10000000149011612)
      {
        int num4 = (int) MessageFuncs.SayOK(Intermech.Project.Localization.GetString("Cant_Set_AutoZoom"));
        this._Zoom = 0.1f;
        this._minimumZoom = true;
        CalcSizes();
      }
      else
      {
        this._Zoom = num3;
        bool flag;
        do
        {
          CalcSizes();
          if ((double) this._Zoom < 0.10000000149011612)
          {
            int num5 = (int) MessageFuncs.SayOK(Intermech.Project.Localization.GetString("Cant_Set_AutoZoom"));
            this._Zoom = 0.1f;
            this._minimumZoom = true;
            break;
          }
          if ((Decimal) this._pageCountVertical > this.PrintSetupForm.EditNumScalePagesHeight.Value || (Decimal) this._pageCountHorizontal > this.PrintSetupForm.EditNumScalePagesWidth.Value)
          {
            this._Zoom *= 0.99f;
            flag = true;
          }
          else
            flag = false;
        }
        while (flag);
      }
    }
    this._pageCountTotal = this._pageCountHorizontal * this._pageCountVertical;
    this._manualPagesUpdate = true;
    try
    {
      this.EditPagesTo.Maximum = (Decimal) this._pageCountTotal;
      if (this.ComboSettings.SelectedSettings != PrintPageSettings.AllProjectDates)
      {
        if (this.ComboSettings.SelectedSettings != PrintPageSettings.SelectedDates)
          goto label_21;
      }
      this._manualPagesUpdate = true;
      this.EditPagesFrom.Maximum = (Decimal) this._pageCountTotal;
      this.EditPagesTo.Minimum = this.EditPagesFrom.Value;
      this.EditPagesTo.Value = (Decimal) this._pageCountTotal;
      this._manualPagesUpdate = false;
    }
    finally
    {
      this._manualPagesUpdate = false;
    }
label_21:
    if (this.RadioButtonPagesMany.Checked)
    {
      this.PrintPreviewCtrl.Columns = this._pageCountHorizontal;
      this.PrintPreviewCtrl.Rows = this._pageCountVertical;
    }
    this.UpdateNavigateButtons();
  }

  private void _printDocument_QueryPageSettings([CanBeNull] object sender, [NotNull] QueryPageSettingsEventArgs e)
  {
  }

  private void radioButtonPagesMany_CheckedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.PrintPreviewCtrl.Columns = this._pageCountHorizontal;
    this.PrintPreviewCtrl.Rows = this._pageCountVertical;
    this.UpdateNavigateButtons();
  }

  private void _radioButtonOnePage_CheckedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.PrintPreviewCtrl.Columns = 1;
    this.PrintPreviewCtrl.Rows = 1;
    if (this.PrintPreviewCtrl.Cursor != Cursors.Default)
      this.PrintPreviewCtrl.Cursor = Cursors.Default;
    this.UpdateNavigateButtons();
  }

  private void _editDatesFrom_Enter([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this._editDatesFrom_OldValue = this.EditDatesFrom.Value.Date;
  }

  private void _editDatesFrom_Leave([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.CheckDatesFrom_RepaintNeeded();
  }

  private void _editDatesFrom_DropDown([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this._editDatesFrom_OldValue = this.EditDatesFrom.Value.Date;
  }

  private void _editDatesFrom_CloseUp([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.CheckDatesFrom_RepaintNeeded();
  }

  private void _editDatesFrom_KeyPress([CanBeNull] object sender, [NotNull] KeyPressEventArgs e)
  {
    if (e.KeyChar != '\r')
      return;
    this.CheckDatesFrom_RepaintNeeded();
    e.Handled = true;
  }

  private void CheckDatesFrom_RepaintNeeded()
  {
    if (!(this._editDatesFrom_OldValue != this.EditDatesFrom.Value.Date))
      return;
    if (this._editDatesFrom_OldValue != DateTime.MinValue)
    {
      if (this._created && !this._manualDatesUpdate)
        this.ComboSettings.SelectedSettings = PrintPageSettings.SelectedDates;
      if (this.EditDatesTo.Value <= this.EditDatesFrom.Value)
        this.EditDatesTo.Value = this.EditDatesFrom.Value;
      this._minimumZoom = false;
      if (this._updateSettingsCounter == 0 && this.PrintPreviewCtrl.Document != null)
      {
        this.InitPageTemplates();
        this.PrintPreviewCtrl.InvalidatePreview();
      }
    }
    this._editDatesFrom_OldValue = this.EditDatesFrom.Value.Date;
  }

  private void _editDatesTo_Enter([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this._editDatesTo_OldValue = this.EditDatesTo.Value.Date;
  }

  private void _editDatesTo_Leave([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.CheckDatesTo_RepaintNeeded();
  }

  private void _editDatesTo_DropDown([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this._editDatesTo_OldValue = this.EditDatesTo.Value.Date;
  }

  private void _editDatesTo_CloseUp([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.CheckDatesTo_RepaintNeeded();
  }

  private void _editDatesTo_KeyPress([CanBeNull] object sender, [NotNull] KeyPressEventArgs e)
  {
    if (e.KeyChar != '\r')
      return;
    this.CheckDatesTo_RepaintNeeded();
    e.Handled = true;
  }

  private void CheckDatesTo_RepaintNeeded()
  {
    if (!(this._editDatesTo_OldValue != this.EditDatesTo.Value.Date))
      return;
    if (this._editDatesTo_OldValue != DateTime.MinValue)
    {
      if (this._created && !this._manualDatesUpdate)
        this.ComboSettings.SelectedSettings = PrintPageSettings.SelectedDates;
      if (this.EditDatesTo.Value <= this.EditDatesFrom.Value)
        this.EditDatesFrom.Value = this.EditDatesTo.Value;
      this._minimumZoom = false;
      if (this._updateSettingsCounter == 0 && this.PrintPreviewCtrl.Document != null)
      {
        this.InitPageTemplates();
        this.PrintPreviewCtrl.InvalidatePreview();
      }
    }
    this._editDatesTo_OldValue = this.EditDatesTo.Value.Date;
  }

  private void _comboSettings_SelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (!this._created || this._pageCountTotal <= 0)
      return;
    switch (this.ComboSettings.SelectedSettings)
    {
      case PrintPageSettings.AllProjectDates:
        this._manualDatesUpdate = true;
        if (this.Project != null)
        {
          this.EditDatesFrom.Value = this.Project.Start;
          this.EditDatesTo.Value = this.Project.Finish;
        }
        this._manualDatesUpdate = false;
        this._manualPagesUpdate = true;
        this.EditPagesFrom.Maximum = (Decimal) this._pageCountTotal;
        this.EditPagesTo.Minimum = this.EditPagesFrom.Value;
        this.EditPagesTo.Value = (Decimal) this._pageCountTotal;
        this._manualPagesUpdate = false;
        break;
      case PrintPageSettings.SelectedDates:
        this._manualPagesUpdate = true;
        this.EditPagesFrom.Maximum = (Decimal) this._pageCountTotal;
        this.EditPagesTo.Minimum = this.EditPagesFrom.Value;
        this.EditPagesTo.Value = (Decimal) this._pageCountTotal;
        this._manualPagesUpdate = false;
        break;
      case PrintPageSettings.SelectedPages:
        this._manualDatesUpdate = true;
        if (this.Project != null)
        {
          this.EditDatesFrom.Value = this.Project.Start;
          this.EditDatesTo.Value = this.Project.Finish;
        }
        this._manualDatesUpdate = false;
        break;
    }
  }

  private void _editPagesFrom_ValueChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this._created && !this._manualPagesUpdate)
      this.ComboSettings.SelectedSettings = PrintPageSettings.SelectedPages;
    this.EditPagesTo.Minimum = this.EditPagesFrom.Value;
    this.BeginUpdateSettings();
    this.EndUpdateSettings();
  }

  private void _editPagesTo_ValueChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this._created && !this._manualPagesUpdate)
      this.ComboSettings.SelectedSettings = PrintPageSettings.SelectedPages;
    this.EditPagesFrom.Maximum = this.EditPagesTo.Value;
    this.BeginUpdateSettings();
    this.EndUpdateSettings();
  }

  [NotNull]
  private static Cursor ZoomCursor
  {
    get
    {
      if (PrintPreviewForm._zoomCursor == (Cursor) null)
      {
        using (Stream manifestResourceStream = typeof (PrintPreviewForm).Assembly.GetManifestResourceStream("Intermech.Project.Controls.Resources.ZoomIn.png"))
        {
          if (manifestResourceStream != null)
          {
            Bitmap bitmap = (Bitmap) Image.FromStream(manifestResourceStream);
            manifestResourceStream.Close();
            bitmap.MakeTransparent();
            PrintPreviewForm._zoomCursor = new Cursor(bitmap.GetHicon());
          }
          else
            PrintPreviewForm._zoomCursor = Cursors.Cross;
        }
      }
      return PrintPreviewForm._zoomCursor;
    }
  }

  private void _printPreviewCtrl_MouseMove([CanBeNull] object sender, [NotNull] MouseEventArgs e)
  {
    if (this.RadioButtonPagesMany.Checked)
    {
      if (this.PrintPreviewCtrl.PageRectangles == null)
        return;
      this._overPageIndex = ((IEnumerable<Rectangle>) this.PrintPreviewCtrl.PageRectangles).IndexOfFirst<Rectangle>((Predicate<Rectangle>) (rectangle => rectangle.Contains(e.Location)));
      if (this._overPageIndex != -1)
      {
        if (!(this.PrintPreviewCtrl.Cursor == Cursors.Default))
          return;
        this.PrintPreviewCtrl.Cursor = PrintPreviewForm.ZoomCursor;
      }
      else
      {
        if (!(this.PrintPreviewCtrl.Cursor != Cursors.Default))
          return;
        this.PrintPreviewCtrl.Cursor = Cursors.Default;
      }
    }
    else
    {
      if (!(this.PrintPreviewCtrl.Cursor != Cursors.Default))
        return;
      this.PrintPreviewCtrl.Cursor = Cursors.Default;
    }
  }

  private void _printPreviewCtrl_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (!this.RadioButtonPagesMany.Checked || !(this.PrintPreviewCtrl.Cursor == PrintPreviewForm.ZoomCursor) || this._overPageIndex == -1)
      return;
    this.RadioButtonOnePage.Checked = true;
    if (this.ActiveControl == this.RadioButtonPagesMany)
      this.ActiveControl = (Control) this.RadioButtonOnePage;
    this.CurrentPage = this._overPageIndex + 1;
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PrintPreviewForm));
    this._buttonClose = new FlatButton();
    this._bevelPaper = new Bevel();
    this._comboPaperSize = new ComboBoxPaperSizes();
    this._comboBoxPrinters = new ComboBoxPrinters();
    this._editDatesTo = new FlatDateTimePicker();
    this._editDatesFrom = new FlatDateTimePicker();
    this._comboPaperOrientation = new ComboBoxPaperOrientation();
    this._comboSettings = new ComboBoxPrintPageSettings();
    this._linkPageSettings = new LinkLabelAdv();
    this._linkPrinterProperties = new LinkLabelAdv();
    this._editPagesFrom = new FlatNumericUpDown();
    this._editPagesCopies = new FlatNumericUpDown();
    this._editPagesTo = new FlatNumericUpDown();
    this._labelDatesTo = new SmoothLabel();
    this._labelPagesTo = new SmoothLabel();
    this._labelPagesFrom = new SmoothLabel();
    this._labelDatesFrom = new SmoothLabel();
    this._labelCopies = new SmoothLabel();
    this._buttonPrint = new FlatButton();
    this._labelSettings = new SmoothLabel();
    this._labelPrinter = new SmoothLabel();
    this._labelPrint = new SmoothLabel();
    this._labelPageNum = new SmoothLabel();
    this._radioButtonPagesMany = new FlatRadioButton();
    this._radioButtonOnePage = new FlatRadioButton();
    this._panelShowPages = new Panel();
    this._buttonMoveToRightPage = new FlatButton();
    this._imageListMovePages = new ImageList(this.components);
    this._buttonMoveToLowerPage = new FlatButton();
    this._buttonMoveToUpperPage = new FlatButton();
    this._buttonMoveToLeftPage = new FlatButton();
    this._printPreviewCtrl = new ProjectPrintPreviewControl();
    this._printDocument = new PrintDocument();
    this._editPagesFrom.BeginInit();
    this._editPagesCopies.BeginInit();
    this._editPagesTo.BeginInit();
    this._panelShowPages.SuspendLayout();
    this.SuspendLayout();
    this._buttonClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this._buttonClose.BackColor = Color.FromArgb(253, 253, 253);
    this._buttonClose.DialogResult = DialogResult.Cancel;
    this._buttonClose.FlatAppearance.BorderColor = SystemColors.ControlDark;
    this._buttonClose.Font = new Font("Arial", 9f);
    this._buttonClose.ImageAlign = ContentAlignment.TopLeft;
    this._buttonClose.Location = new Point(313, 769);
    this._buttonClose.Name = "_buttonClose";
    this._buttonClose.Size = new Size(85, 32 /*0x20*/);
    this._buttonClose.TabIndex = 99;
    this._buttonClose.Text = "&Закрыть";
    this._buttonClose.UseVisualStyleBackColor = false;
    this._bevelPaper.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
    this._bevelPaper.BackColor = Color.LightGray;
    this._bevelPaper.Location = new Point(415, 68);
    this._bevelPaper.Name = "_bevelPaper";
    this._bevelPaper.Shape = BevelShape.Spacer;
    this._bevelPaper.Size = new Size(1, 745);
    this._bevelPaper.TabIndex = 13;
    this._comboPaperSize.ComboBoxPrinter = this._comboBoxPrinters;
    this._comboPaperSize.DrawMode = DrawMode.OwnerDrawVariable;
    this._comboPaperSize.DropDownHeight = 500;
    this._comboPaperSize.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboPaperSize.Font = new Font("Arial", 8.75f);
    this._comboPaperSize.GraySelection = true;
    this._comboPaperSize.ImageList = (ImageList) null;
    this._comboPaperSize.IntegralHeight = false;
    this._comboPaperSize.ItemHeight = 35;
    this._comboPaperSize.ItemsWithImages = true;
    this._comboPaperSize.Location = new Point(22, 522);
    this._comboPaperSize.Name = "_comboPaperSize";
    this._comboPaperSize.RemarksColor = SystemColors.GrayText;
    this._comboPaperSize.Size = new Size(376, 41);
    this._comboPaperSize.TabIndex = 10;
    this._comboPaperSize.MeasureItem += new MeasureItemEventHandler(this._bigComboBox_MeasureItem);
    this._comboPaperSize.SelectedIndexChanged += new EventHandler(this._comboPaperSize_SelectedIndexChanged);
    this._comboBoxPrinters.DrawMode = DrawMode.OwnerDrawVariable;
    this._comboBoxPrinters.DropDownHeight = 500;
    this._comboBoxPrinters.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboBoxPrinters.Font = new Font("Arial", 8.75f);
    this._comboBoxPrinters.GraySelection = true;
    this._comboBoxPrinters.ImageList = (ImageList) null;
    this._comboBoxPrinters.IntegralHeight = false;
    this._comboBoxPrinters.ItemHeight = 35;
    this._comboBoxPrinters.ItemsWithImages = true;
    this._comboBoxPrinters.Location = new Point(22, 225);
    this._comboBoxPrinters.Name = "_comboBoxPrinters";
    this._comboBoxPrinters.RemarksColor = SystemColors.GrayText;
    this._comboBoxPrinters.Size = new Size(376, 41);
    this._comboBoxPrinters.TabIndex = 2;
    this._comboBoxPrinters.MeasureItem += new MeasureItemEventHandler(this._bigComboBox_MeasureItem);
    this._comboBoxPrinters.SelectedIndexChanged += new EventHandler(this._comboBoxPrinters_SelectedIndexChanged);
    this._editDatesTo.Font = new Font("Arial", 10f);
    this._editDatesTo.Format = DateTimePickerFormat.Short;
    this._editDatesTo.Location = new Point(204, 392);
    this._editDatesTo.Name = "_editDatesTo";
    this._editDatesTo.Size = new Size(90, 23);
    this._editDatesTo.TabIndex = 6;
    this._editDatesTo.CloseUp += new EventHandler(this._editDatesTo_CloseUp);
    this._editDatesTo.DropDown += new EventHandler(this._editDatesTo_DropDown);
    this._editDatesTo.Enter += new EventHandler(this._editDatesTo_Enter);
    this._editDatesTo.KeyPress += new KeyPressEventHandler(this._editDatesTo_KeyPress);
    this._editDatesTo.Leave += new EventHandler(this._editDatesTo_Leave);
    this._editDatesFrom.Font = new Font("Arial", 10f);
    this._editDatesFrom.Format = DateTimePickerFormat.Short;
    this._editDatesFrom.Location = new Point(73, 392);
    this._editDatesFrom.Name = "_editDatesFrom";
    this._editDatesFrom.Size = new Size(90, 23);
    this._editDatesFrom.TabIndex = 5;
    this._editDatesFrom.CloseUp += new EventHandler(this._editDatesFrom_CloseUp);
    this._editDatesFrom.DropDown += new EventHandler(this._editDatesFrom_DropDown);
    this._editDatesFrom.Enter += new EventHandler(this._editDatesFrom_Enter);
    this._editDatesFrom.KeyPress += new KeyPressEventHandler(this._editDatesFrom_KeyPress);
    this._editDatesFrom.Leave += new EventHandler(this._editDatesFrom_Leave);
    this._comboPaperOrientation.DrawMode = DrawMode.OwnerDrawVariable;
    this._comboPaperOrientation.DropDownHeight = 500;
    this._comboPaperOrientation.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboPaperOrientation.Font = new Font("Arial", 8.75f);
    this._comboPaperOrientation.FormattingEnabled = true;
    this._comboPaperOrientation.GraySelection = true;
    this._comboPaperOrientation.ImageList = (ImageList) null;
    this._comboPaperOrientation.IntegralHeight = false;
    this._comboPaperOrientation.ItemHeight = 35;
    this._comboPaperOrientation.ItemsWithImages = true;
    this._comboPaperOrientation.Location = new Point(22, 475);
    this._comboPaperOrientation.Name = "_comboPaperOrientation";
    this._comboPaperOrientation.RemarksColor = SystemColors.GrayText;
    this._comboPaperOrientation.ShowItemRemarks = false;
    this._comboPaperOrientation.Size = new Size(376, 41);
    this._comboPaperOrientation.TabIndex = 9;
    this._comboPaperOrientation.MeasureItem += new MeasureItemEventHandler(this._bigComboBox_MeasureItem);
    this._comboPaperOrientation.SelectedIndexChanged += new EventHandler(this._comboPaperOrientation_SelectedIndexChanged);
    this._comboSettings.DrawMode = DrawMode.OwnerDrawVariable;
    this._comboSettings.DropDownHeight = 500;
    this._comboSettings.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboSettings.DropDownWidth = 382;
    this._comboSettings.Font = new Font("Arial", 8.75f);
    this._comboSettings.FormattingEnabled = true;
    this._comboSettings.GraySelection = true;
    this._comboSettings.ImageList = (ImageList) null;
    this._comboSettings.IntegralHeight = false;
    this._comboSettings.ItemHeight = 35;
    this._comboSettings.ItemsWithImages = true;
    this._comboSettings.Location = new Point(22, 334);
    this._comboSettings.Name = "_comboSettings";
    this._comboSettings.RemarksColor = SystemColors.GrayText;
    this._comboSettings.Size = new Size(376, 41);
    this._comboSettings.TabIndex = 4;
    this._comboSettings.MeasureItem += new MeasureItemEventHandler(this._bigComboBox_MeasureItem);
    this._comboSettings.SelectedIndexChanged += new EventHandler(this._comboSettings_SelectedIndexChanged);
    this._linkPageSettings.AutoSize = true;
    this._linkPageSettings.Font = new Font("Arial", 9.5f);
    this._linkPageSettings.LinkBehavior = LinkBehavior.HoverUnderline;
    this._linkPageSettings.LinkColor = Color.DarkSlateBlue;
    this._linkPageSettings.Location = new Point(263, 566);
    this._linkPageSettings.Name = "_linkPageSettings";
    this._linkPageSettings.Size = new Size(135, 16 /*0x10*/);
    this._linkPageSettings.TabIndex = 11;
    this._linkPageSettings.TabStop = true;
    this._linkPageSettings.Text = "П&араметры страницы";
    this._linkPageSettings.LinkClicked += new LinkLabelLinkClickedEventHandler(this._linkPageSettings_LinkClicked);
    this._linkPrinterProperties.AutoSize = true;
    this._linkPrinterProperties.Cursor = Cursors.Hand;
    this._linkPrinterProperties.Font = new Font("Arial", 9.5f);
    this._linkPrinterProperties.LinkBehavior = LinkBehavior.HoverUnderline;
    this._linkPrinterProperties.LinkColor = Color.DarkSlateBlue;
    this._linkPrinterProperties.Location = new Point(276, 269);
    this._linkPrinterProperties.Name = "_linkPrinterProperties";
    this._linkPrinterProperties.Size = new Size(122, 16 /*0x10*/);
    this._linkPrinterProperties.TabIndex = 3;
    this._linkPrinterProperties.TabStop = true;
    this._linkPrinterProperties.Text = "&Свойства принтера";
    this._linkPrinterProperties.LinkClicked += new LinkLabelLinkClickedEventHandler(this._linkPrinterProperties_LinkClicked);
    this._editPagesFrom.BorderStyle = BorderStyle.FixedSingle;
    this._editPagesFrom.Font = new Font("Arial", 10f);
    this._editPagesFrom.Location = new Point(103, 433);
    this._editPagesFrom.Maximum = new Decimal(new int[4]
    {
      50,
      0,
      0,
      0
    });
    this._editPagesFrom.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._editPagesFrom.Name = "_editPagesFrom";
    this._editPagesFrom.Size = new Size(63 /*0x3F*/, 23);
    this._editPagesFrom.TabIndex = 7;
    this._editPagesFrom.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._editPagesFrom.ValueChanged += new EventHandler(this._editPagesFrom_ValueChanged);
    this._editPagesCopies.BorderStyle = BorderStyle.FixedSingle;
    this._editPagesCopies.Font = new Font("Arial", 10f);
    this._editPagesCopies.Location = new Point(181, 76);
    this._editPagesCopies.Maximum = new Decimal(new int[4]
    {
      50,
      0,
      0,
      0
    });
    this._editPagesCopies.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._editPagesCopies.Name = "_editPagesCopies";
    this._editPagesCopies.Size = new Size(57, 23);
    this._editPagesCopies.TabIndex = 1;
    this._editPagesCopies.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._editPagesTo.BorderStyle = BorderStyle.FixedSingle;
    this._editPagesTo.Font = new Font("Arial", 10f);
    this._editPagesTo.Location = new Point(205, 433);
    this._editPagesTo.Maximum = new Decimal(new int[4]
    {
      50,
      0,
      0,
      0
    });
    this._editPagesTo.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._editPagesTo.Name = "_editPagesTo";
    this._editPagesTo.Size = new Size(63 /*0x3F*/, 23);
    this._editPagesTo.TabIndex = 8;
    this._editPagesTo.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._editPagesTo.ValueChanged += new EventHandler(this._editPagesTo_ValueChanged);
    this._labelDatesTo.AutoSize = true;
    this._labelDatesTo.Font = new Font("Arial", 10f);
    this._labelDatesTo.Location = new Point(173, 395);
    this._labelDatesTo.Name = "_labelDatesTo";
    this._labelDatesTo.Size = new Size(24, 16 /*0x10*/);
    this._labelDatesTo.TabIndex = 3;
    this._labelDatesTo.Text = "по";
    this._labelDatesTo.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
    this._labelPagesTo.AutoSize = true;
    this._labelPagesTo.Font = new Font("Arial", 10f);
    this._labelPagesTo.Location = new Point(177, 435);
    this._labelPagesTo.Name = "_labelPagesTo";
    this._labelPagesTo.Size = new Size(22, 16 /*0x10*/);
    this._labelPagesTo.TabIndex = 3;
    this._labelPagesTo.Text = "—";
    this._labelPagesTo.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
    this._labelPagesFrom.AutoSize = true;
    this._labelPagesFrom.Font = new Font("Arial", 10f);
    this._labelPagesFrom.Location = new Point(19, 434);
    this._labelPagesFrom.Name = "_labelPagesFrom";
    this._labelPagesFrom.Size = new Size(84, 16 /*0x10*/);
    this._labelPagesFrom.TabIndex = 3;
    this._labelPagesFrom.Text = "Страницы: ";
    this._labelPagesFrom.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
    this._labelDatesFrom.AutoSize = true;
    this._labelDatesFrom.Font = new Font("Arial", 10f);
    this._labelDatesFrom.Location = new Point(19, 395);
    this._labelDatesFrom.Name = "_labelDatesFrom";
    this._labelDatesFrom.Size = new Size(50, 16 /*0x10*/);
    this._labelDatesFrom.TabIndex = 3;
    this._labelDatesFrom.Text = "Даты: ";
    this._labelDatesFrom.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
    this._labelCopies.AutoSize = true;
    this._labelCopies.Font = new Font("Arial", 10f);
    this._labelCopies.Location = new Point(125, 78);
    this._labelCopies.Name = "_labelCopies";
    this._labelCopies.Size = new Size(56, 16 /*0x10*/);
    this._labelCopies.TabIndex = 3;
    this._labelCopies.Text = "Копии: ";
    this._labelCopies.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
    this._buttonPrint.BackColor = Color.FromArgb(253, 253, 253);
    this._buttonPrint.FlatAppearance.BorderColor = SystemColors.ControlDark;
    this._buttonPrint.Font = new Font("Arial", 9f, FontStyle.Bold);
    this._buttonPrint.ForeColor = Color.FromArgb(50, 50, 50);
    this._buttonPrint.Image = (Image) componentResourceManager.GetObject("_buttonPrint.Image");
    this._buttonPrint.ImageAlign = ContentAlignment.TopCenter;
    this._buttonPrint.Location = new Point(22, 74);
    this._buttonPrint.Name = "_buttonPrint";
    this._buttonPrint.Size = new Size(82, 86);
    this._buttonPrint.TabIndex = 0;
    this._buttonPrint.Text = "\r\n\r\n&Печать";
    this._buttonPrint.UseVisualStyleBackColor = false;
    this._buttonPrint.Click += new EventHandler(this._buttonPrint_Click);
    this._labelSettings.AutoSize = true;
    this._labelSettings.Font = new Font("Arial", 16f);
    this._labelSettings.ForeColor = Color.FromArgb(95, 95, 95);
    this._labelSettings.Location = new Point(17, 306);
    this._labelSettings.Name = "_labelSettings";
    this._labelSettings.Size = new Size(120, 25);
    this._labelSettings.TabIndex = 1;
    this._labelSettings.Text = "Настройка";
    this._labelSettings.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
    this._labelPrinter.AutoSize = true;
    this._labelPrinter.Font = new Font("Arial", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this._labelPrinter.ForeColor = Color.FromArgb(95, 95, 95);
    this._labelPrinter.Location = new Point(17, 197);
    this._labelPrinter.Name = "_labelPrinter";
    this._labelPrinter.Size = new Size(97, 25);
    this._labelPrinter.TabIndex = 1;
    this._labelPrinter.Text = "Принтер";
    this._labelPrinter.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
    this._labelPrint.AutoSize = true;
    this._labelPrint.Font = new Font("Copperplate Gothic Light", 32f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this._labelPrint.ForeColor = Color.FromArgb(90, 90, 90);
    this._labelPrint.Location = new Point(14, 16 /*0x10*/);
    this._labelPrint.Name = "_labelPrint";
    this._labelPrint.Size = new Size(145, 46);
    this._labelPrint.TabIndex = 1;
    this._labelPrint.Text = "Печать";
    this._labelPageNum.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this._labelPageNum.AutoSize = true;
    this._labelPageNum.Font = new Font("Arial", 9f);
    this._labelPageNum.Location = new Point(423, 778);
    this._labelPageNum.Name = "_labelPageNum";
    this._labelPageNum.Size = new Size(40, 15);
    this._labelPageNum.TabIndex = 3;
    this._labelPageNum.Text = "1 из 1";
    this._labelPageNum.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
    this._radioButtonPagesMany.Appearance = Appearance.Button;
    this._radioButtonPagesMany.CheckedBorderColor = SystemColors.ControlDarkDark;
    this._radioButtonPagesMany.FlatAppearance.BorderColor = SystemColors.ControlDark;
    this._radioButtonPagesMany.FlatAppearance.BorderSize = 0;
    this._radioButtonPagesMany.FlatAppearance.CheckedBackColor = Color.FromArgb(200, 200, 200);
    this._radioButtonPagesMany.FlatAppearance.MouseDownBackColor = Color.FromArgb(185, 185, 185);
    this._radioButtonPagesMany.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 230, 230);
    this._radioButtonPagesMany.HoverBorderColor = SystemColors.ControlDark;
    this._radioButtonPagesMany.Image = (Image) componentResourceManager.GetObject("_radioButtonPagesMany.Image");
    this._radioButtonPagesMany.Location = new Point(170, 0);
    this._radioButtonPagesMany.Margin = new Padding(0);
    this._radioButtonPagesMany.Name = "_radioButtonPagesMany";
    this._radioButtonPagesMany.Size = new Size(26, 26);
    this._radioButtonPagesMany.TabIndex = 5;
    this._radioButtonPagesMany.UseVisualStyleBackColor = false;
    this._radioButtonPagesMany.CheckedChanged += new EventHandler(this.radioButtonPagesMany_CheckedChanged);
    this._radioButtonOnePage.Appearance = Appearance.Button;
    this._radioButtonOnePage.Checked = true;
    this._radioButtonOnePage.CheckedBorderColor = SystemColors.ControlDarkDark;
    this._radioButtonOnePage.FlatAppearance.BorderColor = SystemColors.ControlDarkDark;
    this._radioButtonOnePage.FlatAppearance.CheckedBackColor = Color.FromArgb(200, 200, 200);
    this._radioButtonOnePage.FlatAppearance.MouseDownBackColor = Color.FromArgb(185, 185, 185);
    this._radioButtonOnePage.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 230, 230);
    this._radioButtonOnePage.HoverBorderColor = SystemColors.ControlDark;
    this._radioButtonOnePage.Image = (Image) componentResourceManager.GetObject("_radioButtonOnePage.Image");
    this._radioButtonOnePage.Location = new Point(144 /*0x90*/, 0);
    this._radioButtonOnePage.Margin = new Padding(0);
    this._radioButtonOnePage.Name = "_radioButtonOnePage";
    this._radioButtonOnePage.Size = new Size(26, 26);
    this._radioButtonOnePage.TabIndex = 4;
    this._radioButtonOnePage.TabStop = true;
    this._radioButtonOnePage.UseVisualStyleBackColor = false;
    this._radioButtonOnePage.CheckedChanged += new EventHandler(this._radioButtonOnePage_CheckedChanged);
    this._panelShowPages.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._panelShowPages.Controls.Add((Control) this._radioButtonPagesMany);
    this._panelShowPages.Controls.Add((Control) this._radioButtonOnePage);
    this._panelShowPages.Controls.Add((Control) this._buttonMoveToRightPage);
    this._panelShowPages.Controls.Add((Control) this._buttonMoveToLowerPage);
    this._panelShowPages.Controls.Add((Control) this._buttonMoveToUpperPage);
    this._panelShowPages.Controls.Add((Control) this._buttonMoveToLeftPage);
    this._panelShowPages.Location = new Point(951, 775);
    this._panelShowPages.Name = "_panelShowPages";
    this._panelShowPages.Size = new Size(196, 26);
    this._panelShowPages.TabIndex = 13;
    this._buttonMoveToRightPage.BackColor = Color.FromArgb(241, 241, 241);
    this._buttonMoveToRightPage.Enabled = false;
    this._buttonMoveToRightPage.FlatAppearance.BorderSize = 0;
    this._buttonMoveToRightPage.FlatAppearance.MouseDownBackColor = Color.FromArgb(170, 170, 170);
    this._buttonMoveToRightPage.FlatAppearance.MouseOverBackColor = Color.FromArgb(170, 170, 170);
    this._buttonMoveToRightPage.ImageIndex = 7;
    this._buttonMoveToRightPage.ImageList = this._imageListMovePages;
    this._buttonMoveToRightPage.Location = new Point(108, 0);
    this._buttonMoveToRightPage.Name = "_buttonMoveToRightPage";
    this._buttonMoveToRightPage.Size = new Size(36, 26);
    this._buttonMoveToRightPage.TabIndex = 3;
    this._buttonMoveToRightPage.UseVisualStyleBackColor = false;
    this._buttonMoveToRightPage.Click += new EventHandler(this._buttonMoveToRightPage_Click);
    this._imageListMovePages.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_imageListMovePages.ImageStream");
    this._imageListMovePages.TransparentColor = Color.Transparent;
    this._imageListMovePages.Images.SetKeyName(0, "MoveToLeftPage3.png");
    this._imageListMovePages.Images.SetKeyName(1, "MoveToUpperPage3.png");
    this._imageListMovePages.Images.SetKeyName(2, "MoveToLowerPage3.png");
    this._imageListMovePages.Images.SetKeyName(3, "MoveToRightPage3.png");
    this._imageListMovePages.Images.SetKeyName(4, "MoveToLeftPageDisabled.png");
    this._imageListMovePages.Images.SetKeyName(5, "MoveToUpperPageDisabled.png");
    this._imageListMovePages.Images.SetKeyName(6, "MoveToLowerPageDisabled.png");
    this._imageListMovePages.Images.SetKeyName(7, "MoveToRightPageDisabled.png");
    this._buttonMoveToLowerPage.BackColor = Color.FromArgb(241, 241, 241);
    this._buttonMoveToLowerPage.Enabled = false;
    this._buttonMoveToLowerPage.FlatAppearance.BorderSize = 0;
    this._buttonMoveToLowerPage.FlatAppearance.MouseDownBackColor = Color.FromArgb(170, 170, 170);
    this._buttonMoveToLowerPage.FlatAppearance.MouseOverBackColor = Color.FromArgb(170, 170, 170);
    this._buttonMoveToLowerPage.ImageIndex = 6;
    this._buttonMoveToLowerPage.ImageList = this._imageListMovePages;
    this._buttonMoveToLowerPage.Location = new Point(72, 0);
    this._buttonMoveToLowerPage.Name = "_buttonMoveToLowerPage";
    this._buttonMoveToLowerPage.Size = new Size(36, 26);
    this._buttonMoveToLowerPage.TabIndex = 2;
    this._buttonMoveToLowerPage.UseVisualStyleBackColor = false;
    this._buttonMoveToLowerPage.Click += new EventHandler(this._buttonMoveToLowerPage_Click);
    this._buttonMoveToUpperPage.BackColor = Color.FromArgb(241, 241, 241);
    this._buttonMoveToUpperPage.Enabled = false;
    this._buttonMoveToUpperPage.FlatAppearance.BorderSize = 0;
    this._buttonMoveToUpperPage.FlatAppearance.MouseDownBackColor = Color.FromArgb(170, 170, 170);
    this._buttonMoveToUpperPage.FlatAppearance.MouseOverBackColor = Color.FromArgb(170, 170, 170);
    this._buttonMoveToUpperPage.ImageIndex = 5;
    this._buttonMoveToUpperPage.ImageList = this._imageListMovePages;
    this._buttonMoveToUpperPage.Location = new Point(36, 0);
    this._buttonMoveToUpperPage.Name = "_buttonMoveToUpperPage";
    this._buttonMoveToUpperPage.Size = new Size(36, 26);
    this._buttonMoveToUpperPage.TabIndex = 1;
    this._buttonMoveToUpperPage.UseVisualStyleBackColor = false;
    this._buttonMoveToUpperPage.Click += new EventHandler(this._buttonMoveToUpperPage_Click);
    this._buttonMoveToLeftPage.BackColor = Color.FromArgb(241, 241, 241);
    this._buttonMoveToLeftPage.Enabled = false;
    this._buttonMoveToLeftPage.FlatAppearance.BorderSize = 0;
    this._buttonMoveToLeftPage.FlatAppearance.MouseDownBackColor = Color.FromArgb(170, 170, 170);
    this._buttonMoveToLeftPage.FlatAppearance.MouseOverBackColor = Color.FromArgb(170, 170, 170);
    this._buttonMoveToLeftPage.ImageIndex = 4;
    this._buttonMoveToLeftPage.ImageList = this._imageListMovePages;
    this._buttonMoveToLeftPage.Location = new Point(0, 0);
    this._buttonMoveToLeftPage.Name = "_buttonMoveToLeftPage";
    this._buttonMoveToLeftPage.Size = new Size(36, 26);
    this._buttonMoveToLeftPage.TabIndex = 0;
    this._buttonMoveToLeftPage.UseVisualStyleBackColor = false;
    this._buttonMoveToLeftPage.Click += new EventHandler(this._buttonMoveToLeftPage_Click);
    this._printPreviewCtrl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._printPreviewCtrl.AutoScroll = true;
    this._printPreviewCtrl.AutoZoom = true;
    this._printPreviewCtrl.BackColor = Color.FromArgb(241, 241, 241);
    this._printPreviewCtrl.Columns = 1;
    this._printPreviewCtrl.Document = (PrintDocument) null;
    this._printPreviewCtrl.ForeColor = Color.White;
    this._printPreviewCtrl.Location = new Point(426, 12);
    this._printPreviewCtrl.Name = "_printPreviewCtrl";
    this._printPreviewCtrl.Rows = 1;
    this._printPreviewCtrl.Selectable = true;
    this._printPreviewCtrl.ShowOnlyPrintableArea = false;
    this._printPreviewCtrl.Size = new Size(721, 757);
    this._printPreviewCtrl.StartPage = 0;
    this._printPreviewCtrl.TabIndex = 12;
    this._printPreviewCtrl.UseAntiAlias = false;
    this._printPreviewCtrl.Click += new EventHandler(this._printPreviewCtrl_Click);
    this._printPreviewCtrl.MouseMove += new MouseEventHandler(this._printPreviewCtrl_MouseMove);
    this._printDocument.DocumentName = "Project";
    this._printDocument.BeginPrint += new PrintEventHandler(this._printDocument_BeginPrint);
    this._printDocument.EndPrint += new PrintEventHandler(this._printDocument_EndPrint);
    this._printDocument.PrintPage += new PrintPageEventHandler(this._printDocument_PrintPage);
    this._printDocument.QueryPageSettings += new QueryPageSettingsEventHandler(this._printDocument_QueryPageSettings);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = Color.FromArgb(241, 241, 241);
    this.CancelButton = (IButtonControl) this._buttonClose;
    this.ClientSize = new Size(1162, 813);
    this.Controls.Add((Control) this._printPreviewCtrl);
    this.Controls.Add((Control) this._panelShowPages);
    this.Controls.Add((Control) this._bevelPaper);
    this.Controls.Add((Control) this._comboPaperSize);
    this.Controls.Add((Control) this._editDatesTo);
    this.Controls.Add((Control) this._editDatesFrom);
    this.Controls.Add((Control) this._comboPaperOrientation);
    this.Controls.Add((Control) this._comboSettings);
    this.Controls.Add((Control) this._linkPageSettings);
    this.Controls.Add((Control) this._linkPrinterProperties);
    this.Controls.Add((Control) this._comboBoxPrinters);
    this.Controls.Add((Control) this._editPagesFrom);
    this.Controls.Add((Control) this._editPagesCopies);
    this.Controls.Add((Control) this._editPagesTo);
    this.Controls.Add((Control) this._labelDatesTo);
    this.Controls.Add((Control) this._labelPagesTo);
    this.Controls.Add((Control) this._labelPagesFrom);
    this.Controls.Add((Control) this._labelDatesFrom);
    this.Controls.Add((Control) this._labelPageNum);
    this.Controls.Add((Control) this._labelCopies);
    this.Controls.Add((Control) this._buttonClose);
    this.Controls.Add((Control) this._buttonPrint);
    this.Controls.Add((Control) this._labelSettings);
    this.Controls.Add((Control) this._labelPrinter);
    this.Controls.Add((Control) this._labelPrint);
    this.DoubleBuffered = true;
    this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
    this.MinimizeBox = false;
    this.MinimumSize = new Size(892, 665);
    this.Name = nameof (PrintPreviewForm);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Печать проекта";
    this.WindowState = FormWindowState.Maximized;
    this.Shown += new EventHandler(this.PrintPreviewForm_Shown);
    this._editPagesFrom.EndInit();
    this._editPagesCopies.EndInit();
    this._editPagesTo.EndInit();
    this._panelShowPages.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private enum SyncDirection
  {
    FromPreviewToSetupDlg,
    FromSetupToPreviewDlg,
  }

  private class HPageTemplate
  {
    public int _GridWidth;
    [NotNull]
    [ItemNotNull]
    public readonly List<PrintPreviewForm.Column> _Columns;
    public readonly DateTime _GanttStartDate;
    public int _GanttWidth;
    public int _Days = 50;
    [NotNull]
    private readonly PrintPreviewForm _owner;

    public HPageTemplate(
      [NotNull] PrintPreviewForm owner,
      int hPageNum,
      DateTime ganttStartDate,
      [CanBeNull, ItemNotNull] List<PrintPreviewForm.Column> everyPageColumns = null)
    {
      this._owner = owner;
      this._GanttStartDate = ganttStartDate;
      if (everyPageColumns != null)
      {
        foreach (PrintPreviewForm.Column everyPageColumn in everyPageColumns)
          this._GridWidth += everyPageColumn._Width;
      }
      this._Columns = everyPageColumns != null ? new List<PrintPreviewForm.Column>((IEnumerable<PrintPreviewForm.Column>) everyPageColumns) : new List<PrintPreviewForm.Column>();
    }

    public DateTime GetGanttDateByXPos(float xPos)
    {
      return this._GanttStartDate.Add(TimeSpan.FromHours((double) xPos * 24.0 / (double) this._owner._DayWidth));
    }

    public int GetGanttXPosByDate(DateTime dateTime)
    {
      return (int) ((double) (dateTime - this._GanttStartDate).Days * (double) this._owner._DayWidth);
    }
  }

  private class Column
  {
    public readonly int _Index;
    public readonly int _Width;
    [NotNull]
    public readonly string _Caption;

    public Column(int width, int index, [NotNull] string caption)
    {
      this._Index = index;
      this._Width = width;
      this._Caption = caption;
    }

    public override string ToString() => this._Caption;
  }

  private class VPageTemplate
  {
    public int _GridHeight;
    [NotNull]
    [ItemNotNull]
    public readonly List<PrintPreviewForm.Row> _Rows = new List<PrintPreviewForm.Row>();
  }

  private class Row
  {
    public readonly int _Index;
    public readonly int _TopY;
    public readonly int _Height;
    [NotNull]
    private readonly string _caption;

    public Row(int topY, int height, int index, [NotNull] string caption)
    {
      this._TopY = topY;
      this._Index = index;
      this._Height = height;
      this._caption = caption;
    }

    public override string ToString() => this._caption;
  }
}
