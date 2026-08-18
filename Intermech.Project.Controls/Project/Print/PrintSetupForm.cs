// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Print.PrintSetupForm
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Client;
using Intermech.Client.Core;
using Intermech.Common;
using Intermech.Controls;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Metadata;
using Intermech.Navigator.Interfaces;
using Intermech.Paint;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Print;

/// <summary>Диалог настройки печати диаграммы Гантта</summary>
public class PrintSetupForm : 
  ProjectDialogBase,
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
  private readonly PrintPreviewForm _printPreviewForm;
  [NotNull]
  private readonly Brush _tabBkBrush = (Brush) new SolidBrush(SystemColors.Control);
  [NotNull]
  private readonly Brush _tabTextBrush = (Brush) new SolidBrush(SystemColors.ControlText);
  [NotNull]
  private readonly StringFormat _tabTextStringFormat = new StringFormat();
  [NotNull]
  private readonly Pen _dotLinePen = SystemPens.ControlDarkDark;
  [NotNull]
  private readonly Pen _selectedMarginPen = new Pen(Color.Black, 2f);
  [NotNull]
  private readonly Dictionary<string, string> _projectAttributeValues = new Dictionary<string, string>();
  /// <summary>Список список имён атрибутов, обнаруженных </summary>
  [NotNull]
  private readonly Dictionary<object, List<string>> _titlePageAttributes = new Dictionary<object, List<string>>();
  /// <summary>Словарь имя атрибута =&gt; его идентификатор</summary>
  [NotNull]
  private readonly Dictionary<string, int> _checkedAttributes = new Dictionary<string, int>();
  [NotNull]
  [ItemNotNull]
  private static readonly string[] _reservedAttributes = new string[4]
  {
    "Page",
    "Pages",
    "Date",
    "Time"
  };
  private int _pageNum = 1;
  private int _pages = 1;
  [NotNull]
  private readonly Dictionary<string, string> _stringToRtfConvertedCache = new Dictionary<string, string>();
  private Rectangle _headerTextRectangle;
  private Rectangle _footerTextRectangle;
  private bool _schemesLoaded;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _btnPrint;
  private Button _btnPrinterProps;
  private TabControlAdvanced _tabs;
  private TabPage _tabPage;
  private TabPage _tabMargins;
  private TabPage _tabHeader;
  private TabPage _tabFooter;
  private TabPage _tabScheme;
  private TabPage _tabView;
  private Label _labelOrientation;
  private Bevel _bevelOrientation;
  private PictureBox _pictureBoxPortrait;
  private PictureBox _pictureBoxLandscape;
  private Label _labelScale;
  private Bevel _bevelScale;
  private Label _labelManualScalePages2;
  private Label _labelManualScalePages1;
  private Label _labelSetScalePercents;
  private Panel _panel1;
  private Label _labelOther;
  private Bevel _bevelOther;
  private Label _labelFirstPageNum;
  private Label _labelPaperSize;
  private Panel _panelTools;
  private Label _labelMarginLeft2;
  private Label _labelMarginRight2;
  private Label _labelMarginTop2;
  private Label _labelMarginBottom2;
  private Label _labelMarginLeft;
  private Label _labelMarginRight;
  private Label _labelMarginBottom;
  private Label _labelMarginTop;
  private PictureBox _picturePortraitMargins;
  private PictureBox _pictureLandscapeMargins;
  private Label _labelHeaderPreview;
  private TabControlAdvanced _tabsHeader;
  private TabPage _tabHeaderLeft;
  private RichTextBox _textBoxHeaderLeft;
  private TabPage _tabHeaderCenter;
  private RichTextBox _textBoxHeaderCenter;
  private TabPage _tabHeaderRight;
  private Button _buttonCurrentTimeHeaderRight;
  private Button _buttonAddFieldHeaderRight;
  private RichTextBox _textBoxHeaderRight;
  private Button _buttonAddFieldHeaderLeft;
  private Button _buttonAddFieldHeaderCenter;
  private Button _buttonSetupFontHeaderRight;
  private Button _buttonPageNumberHeaderRight;
  private Button _buttonTotalPagesHeaderRight;
  private Button _buttonCurrentDateHeaderRight;
  private Button _buttonSetupFontHeaderLeft;
  private Button _buttonPageNumberHeaderLeft;
  private Button _buttonTotalPagesHeaderLeft;
  private Button _buttonCurrentDateHeaderLeft;
  private Button _buttonCurrentTimeHeaderLeft;
  private Button _buttonSetupFontHeaderCenter;
  private Button _buttonPageNumberHeaderCenter;
  private Button _buttonTotalPagesHeaderCenter;
  private Button _buttonCurrentDateHeaderCenter;
  private Button _buttonCurrentTimeHeaderCenter;
  private PictureBox _pictureHeader;
  private PictureBox _pictureFooter;
  private TabControlAdvanced _tabsFooter;
  private TabPage _tabFooterLeft;
  private Button _buttonSetupFontFooterLeft;
  private Button _buttonPageNumberFooterLeft;
  private Button _buttonTotalPagesFooterLeft;
  private Button _buttonCurrentDateFooterLeft;
  private Button _buttonCurrentTimeFooterLeft;
  private Button _buttonAddFieldFooterLeft;
  private RichTextBox _textBoxFooterLeft;
  private TabPage _tabFooterCenter;
  private Button _buttonSetupFontFooterCenter;
  private Button _buttonPageNumberFooterCenter;
  private Button _buttonTotalPagesFooterCenter;
  private Button _buttonCurrentDateFooterCenter;
  private Button _buttonCurrentTimeFooterCenter;
  private Button _buttonAddFieldFooterCenter;
  private RichTextBox _textBoxFooterCenter;
  private TabPage _tabFooterRight;
  private Button _buttonSetupFontFooterRight;
  private Button _buttonPageNumberFooterRight;
  private Button _buttonTotalPagesFooterRight;
  private Button _buttonCurrentDateFooterRight;
  private Button _buttonCurrentTimeFooterRight;
  private Button _buttonAddFieldFooterRight;
  private RichTextBox _textBoxFooterRight;
  private Label _labelFooterPreview;
  private Button _buttonSchemeDelete;
  private Button _buttonSchemeRename;
  private Button _buttonSchemeApply;
  private Button _buttonSchemeSave;
  private ListBox _listBoxSchemes;
  private Label _labelSchemes;
  private Label _labelPrintSelectedColumns;
  private RichTextBoxAdv _textOutBoxHeaderLeft;
  private RichTextBoxAdv _textOutBoxHeaderCenter;
  private RichTextBoxAdv _textOutBoxHeaderRight;
  private RichTextBoxAdv _textOutBoxFooterLeft;
  private RichTextBoxAdv _textOutBoxFooterCenter;
  private RichTextBoxAdv _textOutBoxFooterRight;
  private RichTextBoxAdv _richTextConverter;
  private RadioButton _radioButtonLandscape;
  private RadioButton _radioButtonPortrait;
  private ComboBoxPaperSizes _comboPaperSize;
  private NumericUpDown _editMarginLeft;
  private NumericUpDown _editMarginBottom;
  private NumericUpDown _editMarginRight;
  private NumericUpDown _editMarginTop;
  private NumericUpDown _editFirstPageNum;
  private NumericUpDown _editNumScalePagesHeight;
  private NumericUpDown _editNumScalePagesWidth;
  private NumericUpDown _editScale;
  private RadioButton _radioButtonManualScalePages;
  private RadioButton _radioButtonSetScalePercents;
  private NumericUpDown _editPrintSelectedColumnsCount;
  private CheckBox _checkBoxPrintSelectedColumns;
  private CheckBox _checkBoxPrintAllColumns;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button BtnPrint
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._btnPrint.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button BtnPrinterProps
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._btnPrinterProps.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabControlAdvanced Tabs
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._tabs.CheckInitializedIn<TabControlAdvanced>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabPage TabPage
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._tabPage.CheckInitializedIn<TabPage>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabPage TabMargins
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._tabMargins.CheckInitializedIn<TabPage>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabPage TabHeader
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._tabHeader.CheckInitializedIn<TabPage>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabPage TabFooter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._tabFooter.CheckInitializedIn<TabPage>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabPage TabScheme
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._tabScheme.CheckInitializedIn<TabPage>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabPage TabView
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._tabView.CheckInitializedIn<TabPage>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelOrientation
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelOrientation.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Bevel BevelOrientation
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._bevelOrientation.CheckInitializedIn<Bevel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal PictureBox PictureBoxPortrait
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._pictureBoxPortrait.CheckInitializedIn<PictureBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal PictureBox PictureBoxLandscape
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._pictureBoxLandscape.CheckInitializedIn<PictureBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelScale
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelScale.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Bevel BevelScale
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._bevelScale.CheckInitializedIn<Bevel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelManualScalePages2
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelManualScalePages2.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelManualScalePages1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelManualScalePages1.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelSetScalePercents
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelSetScalePercents.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Panel Panel1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._panel1.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelOther
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelOther.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Bevel BevelOther
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._bevelOther.CheckInitializedIn<Bevel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelFirstPageNum
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelFirstPageNum.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelPaperSize
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelPaperSize.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Panel PanelTools
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._panelTools.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelMarginLeft2
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelMarginLeft2.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelMarginRight2
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelMarginRight2.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelMarginTop2
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelMarginTop2.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelMarginBottom2
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelMarginBottom2.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelMarginLeft
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelMarginLeft.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelMarginRight
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelMarginRight.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelMarginBottom
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelMarginBottom.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelMarginTop
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelMarginTop.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal PictureBox PicturePortraitMargins
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._picturePortraitMargins.CheckInitializedIn<PictureBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal PictureBox PictureLandscapeMargins
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._pictureLandscapeMargins.CheckInitializedIn<PictureBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelHeaderPreview
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelHeaderPreview.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabControlAdvanced TabsHeader
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._tabsHeader.CheckInitializedIn<TabControlAdvanced>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabPage TabHeaderLeft
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._tabHeaderLeft.CheckInitializedIn<TabPage>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal RichTextBox TextBoxHeaderLeft
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._textBoxHeaderLeft.CheckInitializedIn<RichTextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabPage TabHeaderCenter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._tabHeaderCenter.CheckInitializedIn<TabPage>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal RichTextBox TextBoxHeaderCenter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._textBoxHeaderCenter.CheckInitializedIn<RichTextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabPage TabHeaderRight
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._tabHeaderRight.CheckInitializedIn<TabPage>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonCurrentTimeHeaderRight
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonCurrentTimeHeaderRight.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonAddFieldHeaderRight
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonAddFieldHeaderRight.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal RichTextBox TextBoxHeaderRight
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._textBoxHeaderRight.CheckInitializedIn<RichTextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonAddFieldHeaderLeft
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonAddFieldHeaderLeft.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonAddFieldHeaderCenter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonAddFieldHeaderCenter.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonSetupFontHeaderRight
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonSetupFontHeaderRight.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonPageNumberHeaderRight
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonPageNumberHeaderRight.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonTotalPagesHeaderRight
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonTotalPagesHeaderRight.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonCurrentDateHeaderRight
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonCurrentDateHeaderRight.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonSetupFontHeaderLeft
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonSetupFontHeaderLeft.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonPageNumberHeaderLeft
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonPageNumberHeaderLeft.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonTotalPagesHeaderLeft
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonTotalPagesHeaderLeft.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonCurrentDateHeaderLeft
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonCurrentDateHeaderLeft.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonCurrentTimeHeaderLeft
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonCurrentTimeHeaderLeft.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonSetupFontHeaderCenter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonSetupFontHeaderCenter.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonPageNumberHeaderCenter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonPageNumberHeaderCenter.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonTotalPagesHeaderCenter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonTotalPagesHeaderCenter.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonCurrentDateHeaderCenter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonCurrentDateHeaderCenter.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonCurrentTimeHeaderCenter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonCurrentTimeHeaderCenter.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal PictureBox PictureHeader
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._pictureHeader.CheckInitializedIn<PictureBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal PictureBox PictureFooter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._pictureFooter.CheckInitializedIn<PictureBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabControlAdvanced TabsFooter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._tabsFooter.CheckInitializedIn<TabControlAdvanced>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabPage TabFooterLeft
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._tabFooterLeft.CheckInitializedIn<TabPage>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonSetupFontFooterLeft
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonSetupFontFooterLeft.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonPageNumberFooterLeft
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonPageNumberFooterLeft.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonTotalPagesFooterLeft
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonTotalPagesFooterLeft.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonCurrentDateFooterLeft
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonCurrentDateFooterLeft.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonCurrentTimeFooterLeft
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonCurrentTimeFooterLeft.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonAddFieldFooterLeft
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonAddFieldFooterLeft.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal RichTextBox TextBoxFooterLeft
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._textBoxFooterLeft.CheckInitializedIn<RichTextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabPage TabFooterCenter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._tabFooterCenter.CheckInitializedIn<TabPage>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonSetupFontFooterCenter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonSetupFontFooterCenter.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonPageNumberFooterCenter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonPageNumberFooterCenter.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonTotalPagesFooterCenter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonTotalPagesFooterCenter.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonCurrentDateFooterCenter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonCurrentDateFooterCenter.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonCurrentTimeFooterCenter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonCurrentTimeFooterCenter.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonAddFieldFooterCenter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonAddFieldFooterCenter.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal RichTextBox TextBoxFooterCenter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._textBoxFooterCenter.CheckInitializedIn<RichTextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal TabPage TabFooterRight
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._tabFooterRight.CheckInitializedIn<TabPage>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonSetupFontFooterRight
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonSetupFontFooterRight.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonPageNumberFooterRight
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonPageNumberFooterRight.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonTotalPagesFooterRight
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonTotalPagesFooterRight.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonCurrentDateFooterRight
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonCurrentDateFooterRight.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonCurrentTimeFooterRight
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonCurrentTimeFooterRight.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonAddFieldFooterRight
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonAddFieldFooterRight.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal RichTextBox TextBoxFooterRight
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._textBoxFooterRight.CheckInitializedIn<RichTextBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelFooterPreview
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelFooterPreview.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonSchemeDelete
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonSchemeDelete.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonSchemeRename
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonSchemeRename.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonSchemeApply
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonSchemeApply.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button ButtonSchemeSave
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonSchemeSave.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ListBox ListBoxSchemes
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._listBoxSchemes.CheckInitializedIn<ListBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelSchemes
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelSchemes.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label LabelPrintSelectedColumns
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._labelPrintSelectedColumns.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal RichTextBoxAdv TextOutBoxHeaderLeft
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._textOutBoxHeaderLeft.CheckInitializedIn<RichTextBoxAdv>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal RichTextBoxAdv TextOutBoxHeaderCenter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._textOutBoxHeaderCenter.CheckInitializedIn<RichTextBoxAdv>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal RichTextBoxAdv TextOutBoxHeaderRight
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._textOutBoxHeaderRight.CheckInitializedIn<RichTextBoxAdv>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal RichTextBoxAdv TextOutBoxFooterLeft
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._textOutBoxFooterLeft.CheckInitializedIn<RichTextBoxAdv>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal RichTextBoxAdv TextOutBoxFooterCenter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._textOutBoxFooterCenter.CheckInitializedIn<RichTextBoxAdv>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal RichTextBoxAdv TextOutBoxFooterRight
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._textOutBoxFooterRight.CheckInitializedIn<RichTextBoxAdv>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal RichTextBoxAdv RichTextConverter
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._richTextConverter.CheckInitializedIn<RichTextBoxAdv>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal RadioButton RadioButtonLandscape
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._radioButtonLandscape.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal RadioButton RadioButtonPortrait
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._radioButtonPortrait.CheckInitializedIn<RadioButton>((object) this);
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
  protected internal NumericUpDown EditMarginLeft
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editMarginLeft.CheckInitializedIn<NumericUpDown>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal NumericUpDown EditMarginBottom
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editMarginBottom.CheckInitializedIn<NumericUpDown>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal NumericUpDown EditMarginRight
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editMarginRight.CheckInitializedIn<NumericUpDown>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal NumericUpDown EditMarginTop
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editMarginTop.CheckInitializedIn<NumericUpDown>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal NumericUpDown EditFirstPageNum
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editFirstPageNum.CheckInitializedIn<NumericUpDown>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal NumericUpDown EditNumScalePagesHeight
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editNumScalePagesHeight.CheckInitializedIn<NumericUpDown>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal NumericUpDown EditNumScalePagesWidth
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editNumScalePagesWidth.CheckInitializedIn<NumericUpDown>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal NumericUpDown EditScale
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editScale.CheckInitializedIn<NumericUpDown>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal RadioButton RadioButtonManualScalePages
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._radioButtonManualScalePages.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal RadioButton RadioButtonSetScalePercents
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._radioButtonSetScalePercents.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal NumericUpDown EditPrintSelectedColumnsCount
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editPrintSelectedColumnsCount.CheckInitializedIn<NumericUpDown>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal CheckBox CheckBoxPrintSelectedColumns
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._checkBoxPrintSelectedColumns.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal CheckBox CheckBoxPrintAllColumns
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._checkBoxPrintAllColumns.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  public PrintSetupForm() => this.InitializeComponent();

  public PrintSetupForm([NotNull] PrintPreviewForm printPreviewForm, [CanBeNull] string contextName = null)
    : base(printPreviewForm.Services, string.IsNullOrEmpty(contextName) ? "ProjectPrintSetup" : contextName)
  {
    this._printPreviewForm = printPreviewForm;
    this._dotLinePen = new Pen(Color.Black);
    this._dotLinePen.DashStyle = DashStyle.Dot;
    this._tabTextStringFormat.Alignment = StringAlignment.Center;
    this._tabTextStringFormat.LineAlignment = StringAlignment.Center;
    this.InitializeComponent();
    this.ComboPaperSize.ComboBoxPrinter = this.ComboBoxPrinters;
    this.InitTitlePageAttributes();
    this.Tabs.DrawMode = TabDrawMode.OwnerDrawFixed;
    this.InitTextRectangles();
    this.TextOutBoxHeaderLeft.SelectionAlignment = HorizontalAlignment.Left;
    this.TextOutBoxHeaderCenter.SelectionAlignment = HorizontalAlignment.Center;
    this.TextOutBoxHeaderRight.SelectionAlignment = HorizontalAlignment.Right;
    this.TextOutBoxFooterLeft.SelectionAlignment = HorizontalAlignment.Left;
    this.TextOutBoxFooterCenter.SelectionAlignment = HorizontalAlignment.Center;
    this.TextOutBoxFooterRight.SelectionAlignment = HorizontalAlignment.Right;
    this.TextBoxHeaderLeft.SelectionAlignment = HorizontalAlignment.Left;
    this.TextBoxHeaderCenter.SelectionAlignment = HorizontalAlignment.Center;
    this.TextBoxHeaderRight.SelectionAlignment = HorizontalAlignment.Right;
    this.TextBoxFooterLeft.SelectionAlignment = HorizontalAlignment.Left;
    this.TextBoxFooterCenter.SelectionAlignment = HorizontalAlignment.Center;
    this.TextBoxFooterRight.SelectionAlignment = HorizontalAlignment.Right;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
    {
      this.components.Dispose();
      this._tabBkBrush.Dispose();
      this._tabTextBrush.Dispose();
      this._tabTextStringFormat.Dispose();
      this._dotLinePen.Dispose();
      this._selectedMarginPen.Dispose();
    }
    base.Dispose(disposing);
  }

  private void _tabs_DrawItem([CanBeNull] object sender, [NotNull] DrawItemEventArgs e)
  {
    e.Graphics.FillRectangle(this._tabBkBrush, e.Bounds);
    Rectangle bounds = e.Bounds;
    if (e.Index != this.Tabs.SelectedIndex)
      bounds.Offset(0, 3);
    e.Graphics.DrawString(this.Tabs.TabPages[e.Index].Text, e.Font, this._tabTextBrush, (RectangleF) bounds, this._tabTextStringFormat);
  }

  private void SaveSettings([NotNull] Dictionary<string, object> dic, bool formClosing)
  {
    if (formClosing)
      dic["ActiveTabNum"] = (object) this.Tabs.SelectedIndex;
    if (!formClosing)
      dic["Landscape"] = (object) this.RadioButtonLandscape.Checked;
    dic["ScalePages"] = (object) this.RadioButtonManualScalePages.Checked;
    dic["ScalePagesValue"] = (object) this.EditScale.Value;
    dic["ScalePagesWidth"] = (object) this.EditNumScalePagesWidth.Value;
    dic["ScalePagesHeight"] = (object) this.EditNumScalePagesHeight.Value;
    if (!formClosing && this.ComboPaperSize.SelectedPaperRawKind != 0)
      dic["PaperRawKind"] = (object) this.ComboPaperSize.SelectedPaperRawKind;
    dic["FirstPageNum"] = (object) this.EditFirstPageNum.Value;
    dic["MarginLeft"] = (object) this.EditMarginLeft.Value;
    dic["MarginTop"] = (object) this.EditMarginTop.Value;
    dic["MarginRight"] = (object) this.EditMarginRight.Value;
    dic["MarginBottom"] = (object) this.EditMarginBottom.Value;
    if (this.TextBoxHeaderLeft.Text.Trim() != string.Empty)
      dic["HeaderLeft"] = (object) this.TextBoxHeaderLeft.Rtf;
    if (this.TextBoxHeaderCenter.Text.Trim() != string.Empty)
      dic["HeaderCenter"] = (object) this.TextBoxHeaderCenter.Rtf;
    if (this.TextBoxHeaderRight.Text.Trim() != string.Empty)
      dic["HeaderRight"] = (object) this.TextBoxHeaderRight.Rtf;
    if (formClosing)
      dic["HeaderPage"] = (object) this.TabsHeader.SelectedIndex;
    if (this.TextBoxFooterLeft.Text.Trim() != string.Empty)
      dic["FooterLeft"] = (object) this.TextBoxFooterLeft.Rtf;
    if (this.TextBoxFooterCenter.Text.Trim() != string.Empty)
      dic["FooterCenter"] = (object) this.TextBoxFooterCenter.Rtf;
    if (this.TextBoxFooterRight.Text.Trim() != string.Empty)
      dic["FooterRight"] = (object) this.TextBoxFooterRight.Rtf;
    if (formClosing)
      dic["FooterPage"] = (object) this.TabsFooter.SelectedIndex;
    dic["PrintAllColumns"] = (object) this.CheckBoxPrintAllColumns.Checked;
    if (!this.CheckBoxPrintSelectedColumns.Checked)
      return;
    dic["PrintSelectedColumns"] = (object) this.EditPrintSelectedColumnsCount.Value;
  }

  private void LoadSettings([NotNull] Dictionary<string, object> dic, bool formShowing)
  {
    object val2;
    if (formShowing && dic.TryGetValue("ActiveTabNum", out val2))
      this.Tabs.SelectedIndex = (int) val2;
    if (!formShowing)
    {
      if (dic.TryGetValue("Landscape", out val2) && (bool) val2)
        this.RadioButtonLandscape.Checked = true;
      else
        this.RadioButtonPortrait.Checked = true;
    }
    if (dic.TryGetValue("ScalePages", out val2) && (bool) val2)
      this.RadioButtonManualScalePages.Checked = true;
    else
      this.RadioButtonSetScalePercents.Checked = true;
    if (dic.TryGetValue("ScalePagesValue", out val2))
      this.EditScale.Value = (Decimal) val2;
    if (dic.TryGetValue("ScalePagesWidth", out val2))
      this.EditNumScalePagesWidth.Value = (Decimal) val2;
    if (dic.TryGetValue("ScalePagesHeight", out val2))
      this.EditNumScalePagesHeight.Value = (Decimal) val2;
    if (!formShowing && dic.TryGetValue("PaperRawKind", out val2))
    {
      int newRawKind = (int) val2;
      if (this.ComboPaperSize.Items.Contains<PaperSize>((Predicate<PaperSize>) (paperSize => paperSize.RawKind == newRawKind)))
        this.ComboPaperSize.SelectedPaperRawKind = newRawKind;
      else if (this.ComboBoxPrinters.SelectedPrinter != null)
        this.ComboPaperSize.SelectedPaperRawKind = this.ComboBoxPrinters.SelectedPrinter.DefaultPaperRawKind ?? 0;
      else
        this.ComboPaperSize.SelectedIndex = -1;
    }
    if (dic.TryGetValue("FirstPageNum", out val2))
      this.EditFirstPageNum.Value = (Decimal) val2;
    if (dic.TryGetValue("MarginLeft", out val2))
      this.EditMarginLeft.Value = Math.Min(this.EditMarginLeft.Maximum, (Decimal) val2);
    if (dic.TryGetValue("MarginTop", out val2))
      this.EditMarginTop.Value = Math.Min(this.EditMarginTop.Maximum, (Decimal) val2);
    if (dic.TryGetValue("MarginRight", out val2))
      this.EditMarginRight.Value = Math.Min(this.EditMarginRight.Maximum, (Decimal) val2);
    if (dic.TryGetValue("MarginBottom", out val2))
      this.EditMarginBottom.Value = Math.Min(this.EditMarginBottom.Maximum, (Decimal) val2);
    if (dic.TryGetValue("HeaderLeft", out val2))
      this.TextBoxHeaderLeft.Rtf = (string) val2;
    if (dic.TryGetValue("HeaderCenter", out val2))
      this.TextBoxHeaderCenter.Rtf = (string) val2;
    if (dic.TryGetValue("HeaderRight", out val2))
      this.TextBoxHeaderRight.Rtf = (string) val2;
    if (formShowing && dic.TryGetValue("HeaderPage", out val2))
      this.TabsHeader.SelectedIndex = (int) val2;
    if (dic.TryGetValue("FooterLeft", out val2))
      this.TextBoxFooterLeft.Rtf = (string) val2;
    if (dic.TryGetValue("FooterCenter", out val2))
      this.TextBoxFooterCenter.Rtf = (string) val2;
    if (dic.TryGetValue("FooterRight", out val2))
      this.TextBoxFooterRight.Rtf = (string) val2;
    if (formShowing && dic.TryGetValue("FooterPage", out val2))
      this.TabsFooter.SelectedIndex = (int) val2;
    if (dic.TryGetValue("PrintAllColumns", out val2))
      this.CheckBoxPrintAllColumns.Checked = (bool) val2;
    if (!dic.TryGetValue("PrintSelectedColumns", out val2))
      return;
    this.CheckBoxPrintSelectedColumns.Checked = true;
    this.EditPrintSelectedColumnsCount.Value = (Decimal) val2;
  }

  /// <summary>Загрузка свойств в словарь, который будет сохранён в FormStorage при вызове SavePropertiesToStorage</summary>
  public override void FillPropsDictionary([NotNull] Dictionary<string, object> dic)
  {
    base.FillPropsDictionary(dic);
    this.SaveSettings(dic, true);
  }

  /// <summary>Загрузка свойств из словаря, полученного из FormStorage при вызове LoadPropertiesFromStorage</summary>
  public override void ParseDictionaryFromFormStorage([NotNull] Dictionary<string, object> dic)
  {
    base.ParseDictionaryFromFormStorage(dic);
    this.LoadSettings(dic, true);
  }

  private void _radioButtonLandscape_CheckedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.PicturePortraitMargins.Visible = false;
    this.PictureLandscapeMargins.Visible = true;
    this.RecalcMaxMargins();
  }

  private void _radioButtonPortrait_CheckedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.PictureLandscapeMargins.Visible = false;
    this.PicturePortraitMargins.Visible = true;
    this.RecalcMaxMargins();
  }

  private void _radioButtonSetScalePercents_CheckedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.EditScale.Enabled = true;
    this.LabelSetScalePercents.ForeColor = SystemColors.ControlText;
    this.EditNumScalePagesWidth.Enabled = false;
    this.EditNumScalePagesHeight.Enabled = false;
    this.LabelManualScalePages1.ForeColor = SystemColors.GrayText;
    this.LabelManualScalePages2.ForeColor = SystemColors.GrayText;
  }

  private void _radioButtonManualScalePages_CheckedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.EditScale.Enabled = false;
    this.LabelSetScalePercents.ForeColor = SystemColors.GrayText;
    this.EditNumScalePagesWidth.Enabled = true;
    this.EditNumScalePagesHeight.Enabled = true;
    this.LabelManualScalePages1.ForeColor = SystemColors.ControlText;
    this.LabelManualScalePages2.ForeColor = SystemColors.ControlText;
  }

  [NotNull]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  private ComboBoxPrinters ComboBoxPrinters
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._printPreviewForm.ComboBoxPrinters;
    }
  }

  private void _buttonPrinterSettings_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this._printPreviewForm.ShowSelectedPrinterProperties();
  }

  private void _picturePortraitMargins_Paint([CanBeNull] object sender, [NotNull] PaintEventArgs e)
  {
    this.DrawMargins(e.Graphics, this.PicturePortraitMargins, true);
  }

  private void _pictureLandscapeMargins_Paint([CanBeNull] object sender, [NotNull] PaintEventArgs e)
  {
    this.DrawMargins(e.Graphics, this.PictureLandscapeMargins, false);
  }

  private void DrawMargins([NotNull] Graphics graphics, [NotNull] PictureBox pictureBox, bool portrait)
  {
    PaperSize selectedPaperSize = this.ComboPaperSize.SelectedPaperSize;
    if (selectedPaperSize == null)
      return;
    int num1 = pictureBox.Width - 2;
    int num2 = pictureBox.Height - 2;
    double num3 = 39.37 * (double) num1 / (portrait ? (double) selectedPaperSize.Width : (double) selectedPaperSize.Height);
    int num4 = 1 + (int) ((double) this.EditMarginLeft.Value * num3);
    graphics.DrawLine(this.EditMarginLeft.Focused ? this._selectedMarginPen : this._dotLinePen, num4, 1, num4, pictureBox.Height - 2);
    int num5 = num1 - (int) ((double) this.EditMarginRight.Value * num3);
    graphics.DrawLine(this.EditMarginRight.Focused ? this._selectedMarginPen : this._dotLinePen, num5, 1, num5, pictureBox.Height - 2);
    double num6 = 39.37 * (double) num2 / (portrait ? (double) selectedPaperSize.Height : (double) selectedPaperSize.Width);
    int num7 = 1 + (int) ((double) this.EditMarginTop.Value * num6);
    graphics.DrawLine(this.EditMarginTop.Focused ? this._selectedMarginPen : this._dotLinePen, 1, num7, pictureBox.Width - 2, num7);
    int num8 = num2 - (int) ((double) this.EditMarginBottom.Value * num6);
    graphics.DrawLine(this.EditMarginBottom.Focused ? this._selectedMarginPen : this._dotLinePen, 1, num8, pictureBox.Width - 2, num8);
  }

  private void _editPortraitMargin_ValueChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this.PicturePortraitMargins.Visible)
      this.PicturePortraitMargins.Invalidate();
    else
      this.PictureLandscapeMargins.Invalidate();
  }

  private void _editMargin_EnterOrLeave([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this.PicturePortraitMargins.Visible)
      this.PicturePortraitMargins.Invalidate();
    else
      this.PictureLandscapeMargins.Invalidate();
  }

  private void _comboPaperSize_SelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.RecalcMaxMargins();
  }

  private void RecalcMaxMargins()
  {
    if (this.ComboPaperSize.SelectedPaperSize == null)
      return;
    Decimal num = (Decimal) ((double) Math.Min(this.ComboPaperSize.SelectedPaperSize.Width, this.ComboPaperSize.SelectedPaperSize.Height) * 2.54 / 300.0);
    this.EditMarginLeft.Maximum = num;
    this.EditMarginRight.Maximum = num;
    this.EditMarginTop.Maximum = num;
    this.EditMarginBottom.Maximum = num;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private RichTextBox TextBoxFromTag([CanBeNull] object tag)
  {
    if (tag is string str)
    {
      switch (str)
      {
        case "0":
          return this.TextBoxHeaderLeft;
        case "1":
          return this.TextBoxHeaderCenter;
        case "2":
          return this.TextBoxHeaderRight;
        case "3":
          return this.TextBoxFooterLeft;
        case "4":
          return this.TextBoxFooterCenter;
        case "5":
          return this.TextBoxFooterRight;
      }
    }
    throw new Exception("Unknown tag");
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private PictureBox TitlePreviewPictureFromTag([CanBeNull] object tag)
  {
    if (tag is string str)
    {
      switch (str)
      {
        case "0":
        case "1":
        case "2":
          return this.PictureHeader;
        case "3":
        case "4":
        case "5":
          return this.PictureFooter;
      }
    }
    throw new Exception("Unknown tag");
  }

  private void _buttonSetupTitleFont_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    Button button = (Button) sender;
    RichTextBox richTextBox = this.TextBoxFromTag(button.Tag);
    using (FontDialog fontDialog = new FontDialog())
    {
      fontDialog.Font = richTextBox.SelectionFont;
      fontDialog.AllowScriptChange = false;
      if (fontDialog.ShowDialog((IWin32Window) this) != DialogResult.OK)
        return;
      richTextBox.SelectionFont = fontDialog.Font;
      this.TitlePreviewPictureFromTag(button.Tag).Invalidate();
    }
  }

  private void AppendTitleText([CanBeNull] object sender, [NotNull] string text)
  {
    Button button = (Button) sender;
    RichTextBox richTextBox = this.TextBoxFromTag(button.Tag);
    this.TitlePreviewPictureFromTag(button.Tag);
    richTextBox.AppendText(text);
    this.ActiveControl = (Control) richTextBox;
  }

  private void _buttonInsertPageNumberToTitle_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.AppendTitleText(sender, "&[Page]");
  }

  private void _buttonInsetTotalPagesToTitle_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.AppendTitleText(sender, "&[Pages]");
  }

  private void _buttonInsetDateToTitle_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.AppendTitleText(sender, "&[Date]");
  }

  private void _buttonInsetTimeToTitle_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.AppendTitleText(sender, "&[Time]");
  }

  private void _buttonInsetAttributeToTitle_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(false))
    {
      attributesSelectDlg.AllowedAttrsTypesFilter.RemoveRange<FieldTypes>((IEnumerable<FieldTypes>) new FieldTypes[7]
      {
        FieldTypes.ftBlob,
        FieldTypes.ftFile,
        FieldTypes.ftShortBlob,
        FieldTypes.ftUnknown,
        FieldTypes.ftPassword,
        FieldTypes.ftExternalLink,
        FieldTypes.ftSystem
      });
      attributesSelectDlg.LoadAttrDialogForObjectsTypes(Intermech.Project.ObjectTypes.Project.Guid);
      if (attributesSelectDlg.ShowDialog((IWin32Window) this) != DialogResult.OK || !attributesSelectDlg.SelectedAttributesID.Any<int>())
        return;
      int selectedAttributeTypeID = attributesSelectDlg.SelectedAttributesID.First<int>();
      string str = Session.Invoke<string>((Session.SessionHandler<string>) (session => session.GetAttributeType(selectedAttributeTypeID).Name));
      this.AppendTitleText(sender, $"&[{str}]");
    }
  }

  [CanBeNull]
  public string GetProjectAttributeValue([NotNull] string attributeName)
  {
    return this._projectAttributeValues.LazyGet<string, string>(attributeName, (Func<string>) (() => Repository.ObjectVersions.Invoke<string>(this.ProjectObjectVersionID, (ServerEntityHandler<IDBObject, string>) (project => PrintSetupForm.NullableObjectToString(((IEnumerable<object>) project.GetValuesByID(this._checkedAttributes[attributeName], false)).NotNull<object>().FirstOrDefault<object>())))));
  }

  [NotNull]
  private static string NullableObjectToString([CanBeNull] object obj)
  {
    return obj?.ToString() ?? string.Empty;
  }

  private void InitTitlePageAttributes()
  {
    for (int index = 0; index <= 5; ++index)
      this._titlePageAttributes[(object) index.ToString()] = new List<string>();
  }

  /// <summary>Перестроить список имён идентификаторов</summary>
  /// <param name="sender"></param>
  private void RebuildPageAttributes([CanBeNull] object sender)
  {
    RichTextBox richTextBox = (RichTextBox) sender;
    string text = richTextBox.Text;
    int startIndex1 = 0;
    int length = text.Length;
    List<string> titlePageAttribute = this._titlePageAttributes[richTextBox.Tag];
    titlePageAttribute.Clear();
    int num;
    for (; startIndex1 < length; startIndex1 = num + 1)
    {
      int startIndex2 = text.IndexOf("&[", startIndex1, StringComparison.Ordinal);
      if (startIndex2 == -1)
        break;
      num = text.IndexOf("]", startIndex2, StringComparison.Ordinal);
      if (startIndex2 == -1 || num <= 0)
        break;
      string key = text.Substring(startIndex2 + 2, num - (startIndex2 + 2));
      if (!string.IsNullOrEmpty(key) && !((IEnumerable<string>) PrintSetupForm._reservedAttributes).Contains<string>(key) && !titlePageAttribute.Contains(key))
      {
        string name = key;
        if (this._checkedAttributes.LazyGet<string, int>(key, (Func<int>) (() => MetaDataHelperService.Instance.GetAttributeByTypeNameID(name))) != 0)
          titlePageAttribute.Add(key);
      }
    }
  }

  [NotNull]
  private RichTextBoxAdv GetPreviewRichTextBox([CanBeNull] object tag)
  {
    if (tag is string str)
    {
      switch (str)
      {
        case "0":
          return this.TextOutBoxHeaderLeft;
        case "1":
          return this.TextOutBoxHeaderCenter;
        case "2":
          return this.TextOutBoxHeaderRight;
        case "3":
          return this.TextOutBoxFooterLeft;
        case "4":
          return this.TextOutBoxFooterCenter;
        case "5":
          return this.TextOutBoxFooterRight;
      }
    }
    throw new Exception("Unknown tag");
  }

  [NotNull]
  private RichTextBox GetTitleRichTextBox([CanBeNull] object tag)
  {
    if (tag is string str)
    {
      switch (str)
      {
        case "0":
          return this.TextBoxHeaderLeft;
        case "1":
          return this.TextBoxHeaderCenter;
        case "2":
          return this.TextBoxHeaderRight;
        case "3":
          return this.TextBoxFooterLeft;
        case "4":
          return this.TextBoxFooterCenter;
        case "5":
          return this.TextBoxFooterRight;
      }
    }
    throw new Exception("Unknown tag");
  }

  public void UpdateTextOutTitleRtfs(int pageNum, int pages)
  {
    this._pageNum = pageNum;
    this._pages = pages;
    try
    {
      for (int index = 0; index < 6; ++index)
        this.GetPreviewRichTextBox((object) index.ToString()).Rtf = this.PrepareRtf((object) index.ToString(), this.GetTitleRichTextBox((object) index.ToString()).Rtf);
    }
    finally
    {
      this._pageNum = 1;
      this._pages = 1;
    }
  }

  private void _textBoxTitle_TextChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.RebuildPageAttributes(sender);
    object tag = ((Control) sender).Tag;
    this.GetPreviewRichTextBox(tag).Rtf = this.PrepareRtf(tag, ((RichTextBox) sender).Rtf);
    if (this.Tabs.SelectedTab == this._tabHeader)
    {
      this.PictureHeader.Invalidate();
    }
    else
    {
      if (this.Tabs.SelectedTab != this._tabFooter)
        return;
      this.PictureFooter.Invalidate();
    }
  }

  [NotNull]
  private string PrepareRtf([NotNull] object tag, [NotNull] string rtf)
  {
    string str1 = rtf.Replace("&[Page]", this._pageNum.ToString()).Replace("&[Pages]", this._pages.ToString()).Replace("&[Date]", DateTime.Today.ToShortDateString()).Replace("&[Time]", DateTime.Now.ToShortTimeString());
    if (this.ProjectObjectVersionID != 0L)
    {
      foreach (string str2 in this._titlePageAttributes[tag])
      {
        string str3 = this.ConvertString2Rtf(str2);
        string projectAttributeValue = this.GetProjectAttributeValue(str2);
        string newValue = !string.IsNullOrWhiteSpace(projectAttributeValue) ? this.ConvertString2Rtf(projectAttributeValue) : string.Empty;
        int startIndex = newValue.IndexOf("\\'", StringComparison.Ordinal);
        if (startIndex != -1)
          newValue = newValue.Insert(startIndex, "\\lang1049\\f1");
        str1 = str1.Replace($"&[\\{str3}]", newValue);
        str1 = str1.Replace($"&[\\f0\\{str3}]", newValue);
        str1 = str1.Replace($"&[\\f1\\{str3}]", newValue);
      }
    }
    return str1;
  }

  [NotNull]
  private string ConvertString2Rtf([NotNull] string str)
  {
    return this._stringToRtfConvertedCache.LazyGet<string, string>(str, (Func<string>) (() =>
    {
      this.RichTextConverter.Text = str;
      string rtf = this.RichTextConverter.Rtf;
      int startIndex = rtf.IndexOf("\\f0\\fs17", StringComparison.Ordinal) + 9;
      int length = rtf.LastIndexOf("\\par", StringComparison.Ordinal) - startIndex;
      return length <= 0 ? str : this.RichTextConverter.Rtf.Substring(startIndex, length);
    }));
  }

  private void InitTextRectangles()
  {
    this._headerTextRectangle = new Rectangle(this.TextOutBoxHeaderLeft.Left - this.PictureHeader.Left, this.TextOutBoxHeaderLeft.Top - this.PictureHeader.Top, this.TextOutBoxHeaderLeft.Width, this.TextOutBoxHeaderLeft.Height);
    this._footerTextRectangle = new Rectangle(this.TextOutBoxFooterLeft.Left - this.PictureFooter.Left, this.TextOutBoxFooterLeft.Top - this.PictureFooter.Top, this.TextOutBoxFooterLeft.Width, this.TextOutBoxFooterLeft.Height);
  }

  private void _pictureHeader_Paint([CanBeNull] object sender, [NotNull] PaintEventArgs e)
  {
    this.TextOutBoxHeaderLeft.Draw(e.Graphics, this._headerTextRectangle);
    this.TextOutBoxHeaderCenter.Draw(e.Graphics, this._headerTextRectangle);
    this.TextOutBoxHeaderRight.Draw(e.Graphics, this._headerTextRectangle);
  }

  private void _pictureFooter_Paint([CanBeNull] object sender, [NotNull] PaintEventArgs e)
  {
    this.TextOutBoxFooterLeft.Draw(e.Graphics, this._footerTextRectangle);
    this.TextOutBoxFooterCenter.Draw(e.Graphics, this._footerTextRectangle);
    this.TextOutBoxFooterRight.Draw(e.Graphics, this._footerTextRectangle);
  }

  private void _tabs_SelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this.Tabs.SelectedTab != this._tabScheme || this._schemesLoaded)
      return;
    this.LoadSchemes();
  }

  private void LoadSchemes()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetObjectCollection((int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.PrintScheme).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
      {
        (object) -2,
        (object) -50
      }, new object[1]{ (object) -50 }, new SortOrders[1]
      {
        SortOrders.ASC
      }));
      if (dataTable != null)
        this.ListBoxSchemes.Items.AddRange((object[]) dataTable.Rows.Select<PrintSetupForm.Scheme>((System.Func<DataRow, PrintSetupForm.Scheme>) (row => new PrintSetupForm.Scheme((long) row.FieldAsInt(0), row.FieldAsString(1)))).ToArray<PrintSetupForm.Scheme>(dataTable.Rows.Count));
      if (this.ListBoxSchemes.Items.Count > 0)
        this.ListBoxSchemes.SelectedIndex = 0;
      this._schemesLoaded = true;
    }
  }

  [CanBeNull]
  private PrintSetupForm.Scheme SelectedScheme
  {
    get
    {
      return this.ListBoxSchemes.SelectedIndex < 0 ? (PrintSetupForm.Scheme) null : (PrintSetupForm.Scheme) this.ListBoxSchemes.Items[this.ListBoxSchemes.SelectedIndex];
    }
  }

  private void _listBoxSchemes_SelectedIndexChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.RefreshSchemeTabButtonsEnabled();
  }

  private void RefreshSchemeTabButtonsEnabled()
  {
    PrintSetupForm.Scheme selectedScheme = this.SelectedScheme;
    this.ButtonSchemeApply.Enabled = selectedScheme != null;
    this.ButtonSchemeDelete.Enabled = selectedScheme != null;
    this.ButtonSchemeRename.Enabled = selectedScheme != null;
  }

  private void _buttonSchemeSave_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    using (CreateNewPrintSchemeDlg newPrintSchemeDlg = new CreateNewPrintSchemeDlg((Form) this, this.Services, this.ContextName + ".CreateNewScheme"))
    {
      if (newPrintSchemeDlg.ShowDialog((IWin32Window) this) != DialogResult.OK)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObjectCollection((int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.PrintScheme).Create();
        IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.Data, false);
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        this.SaveSettings(dictionary, false);
        using (MemoryStream serializationStream = new MemoryStream())
        {
          try
          {
            new BinaryFormatter().Serialize((Stream) serializationStream, (object) dictionary);
            long length = serializationStream.Length;
            IBlobWriter blobWriter = Intermech.Diagnostics.Check.Optional.Is<IBlobWriter>((object) dbAttribute, "iDbAttribute");
            BlobInformation blobInfo = new BlobInformation(length, length, DateTime.Now, string.Empty, ArcMethods.NotPacked, string.Empty);
            if (blobWriter.OpenBlob(blobInfo, false))
              blobWriter.WriteDataBlock(serializationStream.ToArray());
          }
          finally
          {
            serializationStream.Close();
          }
        }
        dbObject.Caption = newPrintSchemeDlg.SchemeName;
        dbObject.CommitCreation(true);
        this.ListBoxSchemes.SelectedIndex = this.ListBoxSchemes.Items.Add((object) new PrintSetupForm.Scheme(dbObject.ObjectID, dbObject.Caption));
        this.RefreshSchemeTabButtonsEnabled();
      }
    }
  }

  private void _buttonSchemeRename_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    using (CreateNewPrintSchemeDlg newPrintSchemeDlg = new CreateNewPrintSchemeDlg((Form) this, this.Services, this.ContextName + ".CreateNewScheme"))
    {
      newPrintSchemeDlg.Text = "Переименовать схему печати";
      PrintSetupForm.Scheme selectedItem = (PrintSetupForm.Scheme) this.ListBoxSchemes.SelectedItem;
      newPrintSchemeDlg.SchemeName = selectedItem.Name;
      if (newPrintSchemeDlg.ShowDialog((IWin32Window) this) != DialogResult.OK || !(newPrintSchemeDlg.SchemeName != selectedItem.Name))
        return;
      selectedItem.Name = newPrintSchemeDlg.SchemeName;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        sessionKeeper.Session.GetObject(selectedItem.VersionID).Caption = selectedItem.Name;
        this.ListBoxSchemes.Items[this.ListBoxSchemes.SelectedIndex] = (object) selectedItem;
      }
    }
  }

  private void _buttonSchemeDelete_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (MessageBoxCentered.Show((Form) this, "Удалить выбранную схему печати?", "Удаление схемы печати", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
      return;
    PrintSetupForm.Scheme selectedItem = (PrintSetupForm.Scheme) this.ListBoxSchemes.SelectedItem;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      sessionKeeper.Session.GetObject(selectedItem.VersionID).Delete(0L);
      int selectedIndex = this.ListBoxSchemes.SelectedIndex;
      this.ListBoxSchemes.Items.RemoveAt(selectedIndex);
      if (this.ListBoxSchemes.Items.Count > 0)
        this.ListBoxSchemes.SelectedIndex = selectedIndex < this.ListBoxSchemes.Items.Count ? selectedIndex : this.ListBoxSchemes.Items.Count - 1;
      else
        this.ActiveControl = (Control) this._buttonSchemeSave;
      this.RefreshSchemeTabButtonsEnabled();
    }
  }

  private void _buttonSchemeApply_Click([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.ApplyScheme();
  }

  private void ApplyScheme()
  {
    if (MessageBoxCentered.Show((Form) this, "Применить выбранную схему печати?", "Применение схемы печати", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
      return;
    PrintSetupForm.Scheme selectedItem = (PrintSetupForm.Scheme) this.ListBoxSchemes.SelectedItem;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute byId = sessionKeeper.Session.GetObject(selectedItem.VersionID).Attributes.FindByID((int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.Data);
      Intermech.Diagnostics.Check.Optional.NotNull<IDBAttribute>(byId, "Can`t find Data attribute");
      IBlobReader blobReader = Intermech.Diagnostics.Check.Optional.Is<IBlobReader>((object) byId);
      BlobInformation blobInformation = blobReader.OpenBlob(0);
      Intermech.Diagnostics.Check.Assert(blobInformation.RealFileSize > 0L, "Data attribute is empty");
      byte[] buffer = blobReader.ReadDataBlock((int) blobInformation.RealFileSize);
      MemoryStream memoryStream = new MemoryStream(buffer);
      try
      {
        memoryStream.Seek(0L, SeekOrigin.Begin);
        memoryStream.Write(buffer, 0, buffer.Length);
        memoryStream.Seek(0L, SeekOrigin.Begin);
        if (blobInformation.ArcMethod == ArcMethods.ZLibPacked)
        {
          using (MemoryStream outStream = new MemoryStream())
          {
            ZLibStreamHelper.UnpackStream((Stream) memoryStream, (Stream) outStream);
            memoryStream.Close();
            memoryStream.Dispose();
            memoryStream = outStream;
            memoryStream.Seek(0L, SeekOrigin.Begin);
          }
        }
        this.LoadSettings((Dictionary<string, object>) new BinaryFormatter().Deserialize((Stream) memoryStream), false);
      }
      finally
      {
        memoryStream.Close();
        memoryStream.Dispose();
        blobReader.CloseBlob();
      }
    }
  }

  private void _listBoxSchemes_DoubleClick([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    if (this.ListBoxSchemes.IndexFromPoint(this.ListBoxSchemes.PointToClient(Cursor.Position)) == -1)
      return;
    this.ApplyScheme();
  }

  private void _checkBoxPrintSelectedColumns_CheckedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.EditPrintSelectedColumnsCount.Enabled = this.CheckBoxPrintSelectedColumns.Checked;
    this.LabelPrintSelectedColumns.ForeColor = this.EditPrintSelectedColumnsCount.Enabled ? SystemColors.ControlText : SystemColors.GrayText;
  }

  private void PrintSetupForm_FormClosing([CanBeNull] object sender, [NotNull] FormClosingEventArgs e)
  {
  }

  private void PrintSetupForm_FormClosed([CanBeNull] object sender, [NotNull] FormClosedEventArgs e)
  {
  }

  private void _textBoxTitle_Enter([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.AcceptButton = (IButtonControl) null;
  }

  private void _textBoxTitle_Leave([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.AcceptButton = (IButtonControl) this._okButton;
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PrintSetupForm));
    this._btnPrinterProps = new Button();
    this._btnPrint = new Button();
    this._tabs = new TabControlAdvanced();
    this._tabPage = new TabPage();
    this._editFirstPageNum = new NumericUpDown();
    this._comboPaperSize = new ComboBoxPaperSizes();
    this._labelFirstPageNum = new Label();
    this._labelPaperSize = new Label();
    this._labelManualScalePages2 = new Label();
    this._labelManualScalePages1 = new Label();
    this._labelSetScalePercents = new Label();
    this._editNumScalePagesHeight = new NumericUpDown();
    this._editNumScalePagesWidth = new NumericUpDown();
    this._editScale = new NumericUpDown();
    this._panel1 = new Panel();
    this._pictureBoxLandscape = new PictureBox();
    this._radioButtonPortrait = new RadioButton();
    this._radioButtonLandscape = new RadioButton();
    this._pictureBoxPortrait = new PictureBox();
    this._radioButtonManualScalePages = new RadioButton();
    this._radioButtonSetScalePercents = new RadioButton();
    this._labelOther = new Label();
    this._bevelOther = new Bevel();
    this._labelScale = new Label();
    this._bevelScale = new Bevel();
    this._labelOrientation = new Label();
    this._bevelOrientation = new Bevel();
    this._tabMargins = new TabPage();
    this._pictureLandscapeMargins = new PictureBox();
    this._picturePortraitMargins = new PictureBox();
    this._labelMarginLeft2 = new Label();
    this._labelMarginRight2 = new Label();
    this._labelMarginTop2 = new Label();
    this._labelMarginBottom2 = new Label();
    this._labelMarginLeft = new Label();
    this._labelMarginRight = new Label();
    this._labelMarginBottom = new Label();
    this._labelMarginTop = new Label();
    this._editMarginLeft = new NumericUpDown();
    this._editMarginBottom = new NumericUpDown();
    this._editMarginRight = new NumericUpDown();
    this._editMarginTop = new NumericUpDown();
    this._tabHeader = new TabPage();
    this._textOutBoxHeaderRight = new RichTextBoxAdv();
    this._textOutBoxHeaderCenter = new RichTextBoxAdv();
    this._textOutBoxHeaderLeft = new RichTextBoxAdv();
    this._pictureHeader = new PictureBox();
    this._tabsHeader = new TabControlAdvanced();
    this._tabHeaderLeft = new TabPage();
    this._buttonSetupFontHeaderLeft = new Button();
    this._buttonPageNumberHeaderLeft = new Button();
    this._buttonTotalPagesHeaderLeft = new Button();
    this._buttonCurrentDateHeaderLeft = new Button();
    this._buttonCurrentTimeHeaderLeft = new Button();
    this._buttonAddFieldHeaderLeft = new Button();
    this._textBoxHeaderLeft = new RichTextBox();
    this._tabHeaderCenter = new TabPage();
    this._buttonSetupFontHeaderCenter = new Button();
    this._buttonPageNumberHeaderCenter = new Button();
    this._buttonTotalPagesHeaderCenter = new Button();
    this._buttonCurrentDateHeaderCenter = new Button();
    this._buttonCurrentTimeHeaderCenter = new Button();
    this._buttonAddFieldHeaderCenter = new Button();
    this._textBoxHeaderCenter = new RichTextBox();
    this._tabHeaderRight = new TabPage();
    this._buttonSetupFontHeaderRight = new Button();
    this._buttonPageNumberHeaderRight = new Button();
    this._buttonTotalPagesHeaderRight = new Button();
    this._buttonCurrentDateHeaderRight = new Button();
    this._buttonCurrentTimeHeaderRight = new Button();
    this._buttonAddFieldHeaderRight = new Button();
    this._textBoxHeaderRight = new RichTextBox();
    this._labelHeaderPreview = new Label();
    this._tabFooter = new TabPage();
    this._richTextConverter = new RichTextBoxAdv();
    this._textOutBoxFooterRight = new RichTextBoxAdv();
    this._textOutBoxFooterCenter = new RichTextBoxAdv();
    this._textOutBoxFooterLeft = new RichTextBoxAdv();
    this._pictureFooter = new PictureBox();
    this._tabsFooter = new TabControlAdvanced();
    this._tabFooterLeft = new TabPage();
    this._buttonSetupFontFooterLeft = new Button();
    this._buttonPageNumberFooterLeft = new Button();
    this._buttonTotalPagesFooterLeft = new Button();
    this._buttonCurrentDateFooterLeft = new Button();
    this._buttonCurrentTimeFooterLeft = new Button();
    this._buttonAddFieldFooterLeft = new Button();
    this._textBoxFooterLeft = new RichTextBox();
    this._tabFooterCenter = new TabPage();
    this._buttonSetupFontFooterCenter = new Button();
    this._buttonPageNumberFooterCenter = new Button();
    this._buttonTotalPagesFooterCenter = new Button();
    this._buttonCurrentDateFooterCenter = new Button();
    this._buttonCurrentTimeFooterCenter = new Button();
    this._buttonAddFieldFooterCenter = new Button();
    this._textBoxFooterCenter = new RichTextBox();
    this._tabFooterRight = new TabPage();
    this._buttonSetupFontFooterRight = new Button();
    this._buttonPageNumberFooterRight = new Button();
    this._buttonTotalPagesFooterRight = new Button();
    this._buttonCurrentDateFooterRight = new Button();
    this._buttonCurrentTimeFooterRight = new Button();
    this._buttonAddFieldFooterRight = new Button();
    this._textBoxFooterRight = new RichTextBox();
    this._labelFooterPreview = new Label();
    this._tabScheme = new TabPage();
    this._buttonSchemeDelete = new Button();
    this._buttonSchemeRename = new Button();
    this._buttonSchemeApply = new Button();
    this._buttonSchemeSave = new Button();
    this._listBoxSchemes = new ListBox();
    this._labelSchemes = new Label();
    this._tabView = new TabPage();
    this._labelPrintSelectedColumns = new Label();
    this._editPrintSelectedColumnsCount = new NumericUpDown();
    this._checkBoxPrintSelectedColumns = new CheckBox();
    this._checkBoxPrintAllColumns = new CheckBox();
    this._panelTools = new Panel();
    this._pnlDialogButtons.SuspendLayout();
    this._panelBtns.SuspendLayout();
    this._tabs.SuspendLayout();
    this._tabPage.SuspendLayout();
    this._editFirstPageNum.BeginInit();
    this._editNumScalePagesHeight.BeginInit();
    this._editNumScalePagesWidth.BeginInit();
    this._editScale.BeginInit();
    this._panel1.SuspendLayout();
    ((ISupportInitialize) this._pictureBoxLandscape).BeginInit();
    ((ISupportInitialize) this._pictureBoxPortrait).BeginInit();
    this._tabMargins.SuspendLayout();
    ((ISupportInitialize) this._pictureLandscapeMargins).BeginInit();
    ((ISupportInitialize) this._picturePortraitMargins).BeginInit();
    this._editMarginLeft.BeginInit();
    this._editMarginBottom.BeginInit();
    this._editMarginRight.BeginInit();
    this._editMarginTop.BeginInit();
    this._tabHeader.SuspendLayout();
    ((ISupportInitialize) this._pictureHeader).BeginInit();
    this._tabsHeader.SuspendLayout();
    this._tabHeaderLeft.SuspendLayout();
    this._tabHeaderCenter.SuspendLayout();
    this._tabHeaderRight.SuspendLayout();
    this._tabFooter.SuspendLayout();
    ((ISupportInitialize) this._pictureFooter).BeginInit();
    this._tabsFooter.SuspendLayout();
    this._tabFooterLeft.SuspendLayout();
    this._tabFooterCenter.SuspendLayout();
    this._tabFooterRight.SuspendLayout();
    this._tabScheme.SuspendLayout();
    this._tabView.SuspendLayout();
    this._editPrintSelectedColumnsCount.BeginInit();
    this._panelTools.SuspendLayout();
    this.SuspendLayout();
    this._pnlDialogButtons.BackColor = SystemColors.Control;
    this._pnlDialogButtons.Dock = DockStyle.None;
    this._pnlDialogButtons.Location = new Point(331, 329);
    this._pnlDialogButtons.Size = new Size(177, 28);
    this._cancelButton.Location = new Point(94, 1);
    this._okButton.Location = new Point(10, 1);
    this._bevelDialogButtons.Location = new Point(0, 360);
    this._bevelDialogButtons.Shape = BevelShape.Box;
    this._bevelDialogButtons.Size = new Size(513, 2);
    this._bevelDialogButtons.Style = BevelStyle.Lowered;
    this._bevelDialogButtons.Visible = false;
    this._panelBtns.Location = new Point(4, 0);
    this._panelBtns.Size = new Size(173, 28);
    this._btnPrinterProps.Location = new Point(9, 1);
    this._btnPrinterProps.Name = "_btnPrinterProps";
    this._btnPrinterProps.Size = new Size(81, 23);
    this._btnPrinterProps.TabIndex = 1;
    this._btnPrinterProps.Text = "П&араметры...";
    this._btnPrinterProps.UseVisualStyleBackColor = true;
    this._btnPrinterProps.Click += new EventHandler(this._buttonPrinterSettings_Click);
    this._btnPrint.DialogResult = DialogResult.Yes;
    this._btnPrint.Location = new Point(99, 1);
    this._btnPrint.Name = "_btnPrint";
    this._btnPrint.Size = new Size(75, 23);
    this._btnPrint.TabIndex = 3;
    this._btnPrint.Text = "&Печать...";
    this._tabs.Controls.Add((Control) this._tabPage);
    this._tabs.Controls.Add((Control) this._tabMargins);
    this._tabs.Controls.Add((Control) this._tabHeader);
    this._tabs.Controls.Add((Control) this._tabFooter);
    this._tabs.Controls.Add((Control) this._tabScheme);
    this._tabs.Controls.Add((Control) this._tabView);
    this._tabs.Dock = DockStyle.Fill;
    this._tabs.ItemSize = new Size(100, 20);
    this._tabs.Location = new Point(0, 0);
    this._tabs.Name = "_tabs";
    this._tabs.Padding = new Point(8, 0);
    this._tabs.SelectedIndex = 0;
    this._tabs.ShowTabHeaders = true;
    this._tabs.Size = new Size(513, 362);
    this._tabs.TabIndex = 1;
    this._tabs.DrawItem += new DrawItemEventHandler(this._tabs_DrawItem);
    this._tabs.SelectedIndexChanged += new EventHandler(this._tabs_SelectedIndexChanged);
    this._tabPage.Controls.Add((Control) this._editFirstPageNum);
    this._tabPage.Controls.Add((Control) this._comboPaperSize);
    this._tabPage.Controls.Add((Control) this._labelFirstPageNum);
    this._tabPage.Controls.Add((Control) this._labelPaperSize);
    this._tabPage.Controls.Add((Control) this._labelManualScalePages2);
    this._tabPage.Controls.Add((Control) this._labelManualScalePages1);
    this._tabPage.Controls.Add((Control) this._labelSetScalePercents);
    this._tabPage.Controls.Add((Control) this._editNumScalePagesHeight);
    this._tabPage.Controls.Add((Control) this._editNumScalePagesWidth);
    this._tabPage.Controls.Add((Control) this._editScale);
    this._tabPage.Controls.Add((Control) this._panel1);
    this._tabPage.Controls.Add((Control) this._radioButtonManualScalePages);
    this._tabPage.Controls.Add((Control) this._radioButtonSetScalePercents);
    this._tabPage.Controls.Add((Control) this._labelOther);
    this._tabPage.Controls.Add((Control) this._bevelOther);
    this._tabPage.Controls.Add((Control) this._labelScale);
    this._tabPage.Controls.Add((Control) this._bevelScale);
    this._tabPage.Controls.Add((Control) this._labelOrientation);
    this._tabPage.Controls.Add((Control) this._bevelOrientation);
    this._tabPage.Location = new Point(4, 24);
    this._tabPage.Name = "_tabPage";
    this._tabPage.Padding = new Padding(3);
    this._tabPage.Size = new Size(505, 334);
    this._tabPage.TabIndex = 0;
    this._tabPage.Text = "Страница";
    this._editFirstPageNum.Location = new Point(151, 216);
    this._editFirstPageNum.Maximum = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._editFirstPageNum.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._editFirstPageNum.Name = "_editFirstPageNum";
    this._editFirstPageNum.Size = new Size(110, 20);
    this._editFirstPageNum.TabIndex = 7;
    this._editFirstPageNum.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._comboPaperSize.ComboBoxPrinter = (ComboBoxPrinters) null;
    this._comboPaperSize.DrawMode = DrawMode.OwnerDrawFixed;
    this._comboPaperSize.DropDownStyle = ComboBoxStyle.DropDownList;
    this._comboPaperSize.FormattingEnabled = true;
    this._comboPaperSize.ImageList = (ImageList) null;
    this._comboPaperSize.Location = new Point(107, 186);
    this._comboPaperSize.Name = "_comboPaperSize";
    this._comboPaperSize.RemarksColor = SystemColors.GrayText;
    this._comboPaperSize.ShowItemRemarks = false;
    this._comboPaperSize.Size = new Size(257, 21);
    this._comboPaperSize.TabIndex = 6;
    this._comboPaperSize.SelectedIndexChanged += new EventHandler(this._comboPaperSize_SelectedIndexChanged);
    this._labelFirstPageNum.AutoSize = true;
    this._labelFirstPageNum.Location = new Point(14, 219);
    this._labelFirstPageNum.Name = "_labelFirstPageNum";
    this._labelFirstPageNum.Size = new Size(135, 13);
    this._labelFirstPageNum.TabIndex = 12;
    this._labelFirstPageNum.Text = "&Номер первой страницы:";
    this._labelPaperSize.AutoSize = true;
    this._labelPaperSize.Location = new Point(14, 190);
    this._labelPaperSize.Name = "_labelPaperSize";
    this._labelPaperSize.Size = new Size(88, 13);
    this._labelPaperSize.TabIndex = 11;
    this._labelPaperSize.Text = "Ра&змер бумаги:";
    this._labelManualScalePages2.AutoSize = true;
    this._labelManualScalePages2.ForeColor = SystemColors.GrayText;
    this._labelManualScalePages2.Location = new Point(394, 135);
    this._labelManualScalePages2.Name = "_labelManualScalePages2";
    this._labelManualScalePages2.Size = new Size(75, 13);
    this._labelManualScalePages2.TabIndex = 10;
    this._labelManualScalePages2.Text = "стр. в высоту";
    this._labelManualScalePages1.AutoSize = true;
    this._labelManualScalePages1.ForeColor = SystemColors.GrayText;
    this._labelManualScalePages1.Location = new Point(247, 135);
    this._labelManualScalePages1.Name = "_labelManualScalePages1";
    this._labelManualScalePages1.Size = new Size(88, 13);
    this._labelManualScalePages1.TabIndex = 10;
    this._labelManualScalePages1.Text = "стр. в ширину и ";
    this._labelSetScalePercents.AutoSize = true;
    this._labelSetScalePercents.Location = new Point(170, 105);
    this._labelSetScalePercents.Name = "_labelSetScalePercents";
    this._labelSetScalePercents.Size = new Size(148, 13);
    this._labelSetScalePercents.TabIndex = 9;
    this._labelSetScalePercents.Text = "% от натуральной величины";
    this._editNumScalePagesHeight.BorderStyle = BorderStyle.FixedSingle;
    this._editNumScalePagesHeight.Enabled = false;
    this._editNumScalePagesHeight.Location = new Point(337, 133);
    this._editNumScalePagesHeight.Maximum = new Decimal(new int[4]
    {
      10,
      0,
      0,
      0
    });
    this._editNumScalePagesHeight.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._editNumScalePagesHeight.Name = "_editNumScalePagesHeight";
    this._editNumScalePagesHeight.Size = new Size(51, 20);
    this._editNumScalePagesHeight.TabIndex = 5;
    this._editNumScalePagesHeight.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._editNumScalePagesWidth.Enabled = false;
    this._editNumScalePagesWidth.Location = new Point(188, 133);
    this._editNumScalePagesWidth.Maximum = new Decimal(new int[4]
    {
      10,
      0,
      0,
      0
    });
    this._editNumScalePagesWidth.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._editNumScalePagesWidth.Name = "_editNumScalePagesWidth";
    this._editNumScalePagesWidth.Size = new Size(55, 20);
    this._editNumScalePagesWidth.TabIndex = 4;
    this._editNumScalePagesWidth.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._editScale.BorderStyle = BorderStyle.FixedSingle;
    this._editScale.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this._editScale.Location = new Point(111, 103);
    this._editScale.Margin = new Padding(10);
    this._editScale.Maximum = new Decimal(new int[4]
    {
      500,
      0,
      0,
      0
    });
    this._editScale.Minimum = new Decimal(new int[4]
    {
      10,
      0,
      0,
      0
    });
    this._editScale.Name = "_editScale";
    this._editScale.Size = new Size(55, 20);
    this._editScale.TabIndex = 3;
    this._editScale.Value = new Decimal(new int[4]
    {
      100,
      0,
      0,
      0
    });
    this._panel1.Controls.Add((Control) this._pictureBoxLandscape);
    this._panel1.Controls.Add((Control) this._radioButtonPortrait);
    this._panel1.Controls.Add((Control) this._radioButtonLandscape);
    this._panel1.Controls.Add((Control) this._pictureBoxPortrait);
    this._panel1.Location = new Point(20, 26);
    this._panel1.Name = "_panel1";
    this._panel1.Size = new Size(304, 53);
    this._panel1.TabIndex = 0;
    this._pictureBoxLandscape.Image = (Image) componentResourceManager.GetObject("_pictureBoxLandscape.Image");
    this._pictureBoxLandscape.Location = new Point(150, 10);
    this._pictureBoxLandscape.Name = "_pictureBoxLandscape";
    this._pictureBoxLandscape.Size = new Size(32 /*0x20*/, 25);
    this._pictureBoxLandscape.TabIndex = 4;
    this._pictureBoxLandscape.TabStop = false;
    this._radioButtonPortrait.AutoSize = true;
    this._radioButtonPortrait.Location = new Point(34, 14);
    this._radioButtonPortrait.Name = "_radioButtonPortrait";
    this._radioButtonPortrait.Size = new Size(69, 17);
    this._radioButtonPortrait.TabIndex = 0;
    this._radioButtonPortrait.Text = "кни&жная";
    this._radioButtonPortrait.UseVisualStyleBackColor = true;
    this._radioButtonPortrait.CheckedChanged += new EventHandler(this._radioButtonPortrait_CheckedChanged);
    this._radioButtonLandscape.AutoSize = true;
    this._radioButtonLandscape.Checked = true;
    this._radioButtonLandscape.Location = new Point(190, 14);
    this._radioButtonLandscape.Name = "_radioButtonLandscape";
    this._radioButtonLandscape.Size = new Size(81, 17);
    this._radioButtonLandscape.TabIndex = 1;
    this._radioButtonLandscape.TabStop = true;
    this._radioButtonLandscape.Text = "ал&ьбомная";
    this._radioButtonLandscape.UseVisualStyleBackColor = true;
    this._radioButtonLandscape.CheckedChanged += new EventHandler(this._radioButtonLandscape_CheckedChanged);
    this._pictureBoxPortrait.Image = (Image) componentResourceManager.GetObject("_pictureBoxPortrait.Image");
    this._pictureBoxPortrait.Location = new Point(2, 7);
    this._pictureBoxPortrait.Name = "_pictureBoxPortrait";
    this._pictureBoxPortrait.Size = new Size(25, 32 /*0x20*/);
    this._pictureBoxPortrait.TabIndex = 3;
    this._pictureBoxPortrait.TabStop = false;
    this._radioButtonManualScalePages.AutoSize = true;
    this._radioButtonManualScalePages.Location = new Point(17, 133);
    this._radioButtonManualScalePages.Name = "_radioButtonManualScalePages";
    this._radioButtonManualScalePages.Size = new Size(173, 17);
    this._radioButtonManualScalePages.TabIndex = 2;
    this._radioButtonManualScalePages.Text = "&разместить не более чем на ";
    this._radioButtonManualScalePages.UseVisualStyleBackColor = true;
    this._radioButtonManualScalePages.CheckedChanged += new EventHandler(this._radioButtonManualScalePages_CheckedChanged);
    this._radioButtonSetScalePercents.AutoSize = true;
    this._radioButtonSetScalePercents.Checked = true;
    this._radioButtonSetScalePercents.Location = new Point(17, 103);
    this._radioButtonSetScalePercents.Name = "_radioButtonSetScalePercents";
    this._radioButtonSetScalePercents.Size = new Size(94, 17);
    this._radioButtonSetScalePercents.TabIndex = 1;
    this._radioButtonSetScalePercents.TabStop = true;
    this._radioButtonSetScalePercents.Text = "&установить в ";
    this._radioButtonSetScalePercents.UseVisualStyleBackColor = true;
    this._radioButtonSetScalePercents.CheckedChanged += new EventHandler(this._radioButtonSetScalePercents_CheckedChanged);
    this._labelOther.AutoSize = true;
    this._labelOther.Location = new Point(8, 166);
    this._labelOther.Name = "_labelOther";
    this._labelOther.Size = new Size(44, 13);
    this._labelOther.TabIndex = 5;
    this._labelOther.Text = "Прочее";
    this._bevelOther.BackColor = SystemColors.ScrollBar;
    this._bevelOther.Location = new Point(27, 172);
    this._bevelOther.Name = "_bevelOther";
    this._bevelOther.Shape = BevelShape.Spacer;
    this._bevelOther.Size = new Size(469, 1);
    this._bevelOther.TabIndex = 6;
    this._bevelOther.Text = "bevel2";
    this._labelScale.AutoSize = true;
    this._labelScale.Location = new Point(8, 82);
    this._labelScale.Name = "_labelScale";
    this._labelScale.Size = new Size(53, 13);
    this._labelScale.TabIndex = 5;
    this._labelScale.Text = "Масштаб";
    this._bevelScale.BackColor = SystemColors.ScrollBar;
    this._bevelScale.ForeColor = SystemColors.ScrollBar;
    this._bevelScale.Location = new Point(27, 88);
    this._bevelScale.Name = "_bevelScale";
    this._bevelScale.Shape = BevelShape.Spacer;
    this._bevelScale.Size = new Size(469, 1);
    this._bevelScale.TabIndex = 6;
    this._bevelScale.Text = "bevel2";
    this._labelOrientation.AutoSize = true;
    this._labelOrientation.Location = new Point(9, 12);
    this._labelOrientation.Name = "_labelOrientation";
    this._labelOrientation.Size = new Size(68, 13);
    this._labelOrientation.TabIndex = 0;
    this._labelOrientation.Text = "Ориентация";
    this._bevelOrientation.BackColor = SystemColors.ScrollBar;
    this._bevelOrientation.Location = new Point(28, 18);
    this._bevelOrientation.Name = "_bevelOrientation";
    this._bevelOrientation.Shape = BevelShape.Spacer;
    this._bevelOrientation.Size = new Size(469, 1);
    this._bevelOrientation.TabIndex = 1;
    this._bevelOrientation.Text = "bevel1";
    this._tabMargins.BackColor = SystemColors.Control;
    this._tabMargins.Controls.Add((Control) this._pictureLandscapeMargins);
    this._tabMargins.Controls.Add((Control) this._picturePortraitMargins);
    this._tabMargins.Controls.Add((Control) this._labelMarginLeft2);
    this._tabMargins.Controls.Add((Control) this._labelMarginRight2);
    this._tabMargins.Controls.Add((Control) this._labelMarginTop2);
    this._tabMargins.Controls.Add((Control) this._labelMarginBottom2);
    this._tabMargins.Controls.Add((Control) this._labelMarginLeft);
    this._tabMargins.Controls.Add((Control) this._labelMarginRight);
    this._tabMargins.Controls.Add((Control) this._labelMarginBottom);
    this._tabMargins.Controls.Add((Control) this._labelMarginTop);
    this._tabMargins.Controls.Add((Control) this._editMarginLeft);
    this._tabMargins.Controls.Add((Control) this._editMarginBottom);
    this._tabMargins.Controls.Add((Control) this._editMarginRight);
    this._tabMargins.Controls.Add((Control) this._editMarginTop);
    this._tabMargins.Location = new Point(4, 24);
    this._tabMargins.Name = "_tabMargins";
    this._tabMargins.Padding = new Padding(3);
    this._tabMargins.Size = new Size(505, 334);
    this._tabMargins.TabIndex = 1;
    this._tabMargins.Text = "Поля";
    this._pictureLandscapeMargins.Image = (Image) componentResourceManager.GetObject("_pictureLandscapeMargins.Image");
    this._pictureLandscapeMargins.Location = new Point(73, 61);
    this._pictureLandscapeMargins.Name = "_pictureLandscapeMargins";
    this._pictureLandscapeMargins.Size = new Size(165, 94);
    this._pictureLandscapeMargins.TabIndex = 10;
    this._pictureLandscapeMargins.TabStop = false;
    this._pictureLandscapeMargins.Paint += new PaintEventHandler(this._pictureLandscapeMargins_Paint);
    this._picturePortraitMargins.Image = (Image) componentResourceManager.GetObject("_picturePortraitMargins.Image");
    this._picturePortraitMargins.Location = new Point(109, 39);
    this._picturePortraitMargins.Name = "_picturePortraitMargins";
    this._picturePortraitMargins.Size = new Size(119, 130);
    this._picturePortraitMargins.TabIndex = 9;
    this._picturePortraitMargins.TabStop = false;
    this._picturePortraitMargins.Visible = false;
    this._picturePortraitMargins.Paint += new PaintEventHandler(this._picturePortraitMargins_Paint);
    this._labelMarginLeft2.AutoSize = true;
    this._labelMarginLeft2.Location = new Point(28, 120);
    this._labelMarginLeft2.Name = "_labelMarginLeft2";
    this._labelMarginLeft2.Size = new Size(21, 13);
    this._labelMarginLeft2.TabIndex = 5;
    this._labelMarginLeft2.Text = "см";
    this._labelMarginRight2.AutoSize = true;
    this._labelMarginRight2.Location = new Point(263, 120);
    this._labelMarginRight2.Name = "_labelMarginRight2";
    this._labelMarginRight2.Size = new Size(21, 13);
    this._labelMarginRight2.TabIndex = 5;
    this._labelMarginRight2.Text = "см";
    this._labelMarginTop2.AutoSize = true;
    this._labelMarginTop2.Location = new Point(206, 14);
    this._labelMarginTop2.Name = "_labelMarginTop2";
    this._labelMarginTop2.Size = new Size(21, 13);
    this._labelMarginTop2.TabIndex = 5;
    this._labelMarginTop2.Text = "см";
    this._labelMarginBottom2.AutoSize = true;
    this._labelMarginBottom2.Location = new Point(206, 184);
    this._labelMarginBottom2.Name = "_labelMarginBottom2";
    this._labelMarginBottom2.Size = new Size(21, 13);
    this._labelMarginBottom2.TabIndex = 5;
    this._labelMarginBottom2.Text = "см";
    this._labelMarginLeft.AutoSize = true;
    this._labelMarginLeft.Location = new Point(17, 83);
    this._labelMarginLeft.Name = "_labelMarginLeft";
    this._labelMarginLeft.Size = new Size(42, 13);
    this._labelMarginLeft.TabIndex = 4;
    this._labelMarginLeft.Text = "&Левое:";
    this._labelMarginRight.AutoSize = true;
    this._labelMarginRight.Location = new Point(251, 83);
    this._labelMarginRight.Name = "_labelMarginRight";
    this._labelMarginRight.Size = new Size(48 /*0x30*/, 13);
    this._labelMarginRight.TabIndex = 4;
    this._labelMarginRight.Text = "Пра&вое:";
    this._labelMarginBottom.AutoSize = true;
    this._labelMarginBottom.Location = new Point(91, 184);
    this._labelMarginBottom.Name = "_labelMarginBottom";
    this._labelMarginBottom.Size = new Size(50, 13);
    this._labelMarginBottom.TabIndex = 4;
    this._labelMarginBottom.Text = "Н&ижнее:";
    this._labelMarginTop.AutoSize = true;
    this._labelMarginTop.Location = new Point(89, 14);
    this._labelMarginTop.Name = "_labelMarginTop";
    this._labelMarginTop.Size = new Size(52, 13);
    this._labelMarginTop.TabIndex = 4;
    this._labelMarginTop.Text = "&Верхнее:";
    this._editMarginLeft.DecimalPlaces = 2;
    this._editMarginLeft.Location = new Point(10, 98);
    this._editMarginLeft.Maximum = new Decimal(new int[4]
    {
      10,
      0,
      0,
      0
    });
    this._editMarginLeft.Name = "_editMarginLeft";
    this._editMarginLeft.Size = new Size(55, 20);
    this._editMarginLeft.TabIndex = 2;
    this._editMarginLeft.Value = new Decimal(new int[4]
    {
      125,
      0,
      0,
      131072 /*0x020000*/
    });
    this._editMarginLeft.ValueChanged += new EventHandler(this._editPortraitMargin_ValueChanged);
    this._editMarginLeft.Enter += new EventHandler(this._editMargin_EnterOrLeave);
    this._editMarginLeft.Leave += new EventHandler(this._editMargin_EnterOrLeave);
    this._editMarginBottom.DecimalPlaces = 2;
    this._editMarginBottom.Location = new Point(145, 183);
    this._editMarginBottom.Maximum = new Decimal(new int[4]
    {
      10,
      0,
      0,
      0
    });
    this._editMarginBottom.Name = "_editMarginBottom";
    this._editMarginBottom.Size = new Size(55, 20);
    this._editMarginBottom.TabIndex = 1;
    this._editMarginBottom.Value = new Decimal(new int[4]
    {
      125,
      0,
      0,
      131072 /*0x020000*/
    });
    this._editMarginBottom.ValueChanged += new EventHandler(this._editPortraitMargin_ValueChanged);
    this._editMarginBottom.Enter += new EventHandler(this._editMargin_EnterOrLeave);
    this._editMarginBottom.Leave += new EventHandler(this._editMargin_EnterOrLeave);
    this._editMarginRight.DecimalPlaces = 2;
    this._editMarginRight.Location = new Point(246, 98);
    this._editMarginRight.Maximum = new Decimal(new int[4]
    {
      10,
      0,
      0,
      0
    });
    this._editMarginRight.Name = "_editMarginRight";
    this._editMarginRight.Size = new Size(55, 20);
    this._editMarginRight.TabIndex = 3;
    this._editMarginRight.Value = new Decimal(new int[4]
    {
      125,
      0,
      0,
      131072 /*0x020000*/
    });
    this._editMarginRight.ValueChanged += new EventHandler(this._editPortraitMargin_ValueChanged);
    this._editMarginRight.Enter += new EventHandler(this._editMargin_EnterOrLeave);
    this._editMarginRight.Leave += new EventHandler(this._editMargin_EnterOrLeave);
    this._editMarginTop.DecimalPlaces = 2;
    this._editMarginTop.Location = new Point(145, 13);
    this._editMarginTop.Maximum = new Decimal(new int[4]
    {
      10,
      0,
      0,
      0
    });
    this._editMarginTop.Name = "_editMarginTop";
    this._editMarginTop.Size = new Size(55, 20);
    this._editMarginTop.TabIndex = 0;
    this._editMarginTop.Value = new Decimal(new int[4]
    {
      125,
      0,
      0,
      131072 /*0x020000*/
    });
    this._editMarginTop.ValueChanged += new EventHandler(this._editPortraitMargin_ValueChanged);
    this._editMarginTop.Enter += new EventHandler(this._editMargin_EnterOrLeave);
    this._editMarginTop.Leave += new EventHandler(this._editMargin_EnterOrLeave);
    this._tabHeader.BackColor = SystemColors.Control;
    this._tabHeader.Controls.Add((Control) this._textOutBoxHeaderRight);
    this._tabHeader.Controls.Add((Control) this._textOutBoxHeaderCenter);
    this._tabHeader.Controls.Add((Control) this._textOutBoxHeaderLeft);
    this._tabHeader.Controls.Add((Control) this._pictureHeader);
    this._tabHeader.Controls.Add((Control) this._tabsHeader);
    this._tabHeader.Controls.Add((Control) this._labelHeaderPreview);
    this._tabHeader.Location = new Point(4, 24);
    this._tabHeader.Name = "_tabHeader";
    this._tabHeader.Padding = new Padding(3);
    this._tabHeader.Size = new Size(505, 334);
    this._tabHeader.TabIndex = 2;
    this._tabHeader.Text = "Верхний колонтитул";
    this._textOutBoxHeaderRight.BorderStyle = BorderStyle.None;
    this._textOutBoxHeaderRight.Cursor = Cursors.Default;
    this._textOutBoxHeaderRight.Location = new Point(21, 46);
    this._textOutBoxHeaderRight.Name = "_textOutBoxHeaderRight";
    this._textOutBoxHeaderRight.ReadOnly = true;
    this._textOutBoxHeaderRight.ScrollBars = RichTextBoxScrollBars.None;
    this._textOutBoxHeaderRight.Size = new Size(373, 57);
    this._textOutBoxHeaderRight.TabIndex = 12;
    this._textOutBoxHeaderRight.TabStop = false;
    this._textOutBoxHeaderRight.Tag = (object) "2";
    this._textOutBoxHeaderRight.Text = "";
    this._textOutBoxHeaderRight.Visible = false;
    this._textOutBoxHeaderCenter.BorderStyle = BorderStyle.None;
    this._textOutBoxHeaderCenter.Cursor = Cursors.Default;
    this._textOutBoxHeaderCenter.Location = new Point(21, 46);
    this._textOutBoxHeaderCenter.Name = "_textOutBoxHeaderCenter";
    this._textOutBoxHeaderCenter.ReadOnly = true;
    this._textOutBoxHeaderCenter.ScrollBars = RichTextBoxScrollBars.None;
    this._textOutBoxHeaderCenter.Size = new Size(373, 57);
    this._textOutBoxHeaderCenter.TabIndex = 12;
    this._textOutBoxHeaderCenter.TabStop = false;
    this._textOutBoxHeaderCenter.Tag = (object) "1";
    this._textOutBoxHeaderCenter.Text = "";
    this._textOutBoxHeaderCenter.Visible = false;
    this._textOutBoxHeaderLeft.BorderStyle = BorderStyle.None;
    this._textOutBoxHeaderLeft.Cursor = Cursors.Default;
    this._textOutBoxHeaderLeft.Location = new Point(21, 46);
    this._textOutBoxHeaderLeft.Name = "_textOutBoxHeaderLeft";
    this._textOutBoxHeaderLeft.ReadOnly = true;
    this._textOutBoxHeaderLeft.ScrollBars = RichTextBoxScrollBars.None;
    this._textOutBoxHeaderLeft.Size = new Size(373, 57);
    this._textOutBoxHeaderLeft.TabIndex = 12;
    this._textOutBoxHeaderLeft.TabStop = false;
    this._textOutBoxHeaderLeft.Tag = (object) "0";
    this._textOutBoxHeaderLeft.Text = "";
    this._textOutBoxHeaderLeft.Visible = false;
    this._pictureHeader.Image = (Image) componentResourceManager.GetObject("_pictureHeader.Image");
    this._pictureHeader.Location = new Point(11, 31 /*0x1F*/);
    this._pictureHeader.Name = "_pictureHeader";
    this._pictureHeader.Size = new Size(394, 75);
    this._pictureHeader.TabIndex = 11;
    this._pictureHeader.TabStop = false;
    this._pictureHeader.Paint += new PaintEventHandler(this._pictureHeader_Paint);
    this._tabsHeader.Controls.Add((Control) this._tabHeaderLeft);
    this._tabsHeader.Controls.Add((Control) this._tabHeaderCenter);
    this._tabsHeader.Controls.Add((Control) this._tabHeaderRight);
    this._tabsHeader.Location = new Point(8, 112 /*0x70*/);
    this._tabsHeader.Name = "_tabsHeader";
    this._tabsHeader.Padding = new Point(8, 3);
    this._tabsHeader.SelectedIndex = 0;
    this._tabsHeader.ShowTabHeaders = true;
    this._tabsHeader.Size = new Size(407, 175);
    this._tabsHeader.TabIndex = 0;
    this._tabHeaderLeft.Controls.Add((Control) this._buttonSetupFontHeaderLeft);
    this._tabHeaderLeft.Controls.Add((Control) this._buttonPageNumberHeaderLeft);
    this._tabHeaderLeft.Controls.Add((Control) this._buttonTotalPagesHeaderLeft);
    this._tabHeaderLeft.Controls.Add((Control) this._buttonCurrentDateHeaderLeft);
    this._tabHeaderLeft.Controls.Add((Control) this._buttonCurrentTimeHeaderLeft);
    this._tabHeaderLeft.Controls.Add((Control) this._buttonAddFieldHeaderLeft);
    this._tabHeaderLeft.Controls.Add((Control) this._textBoxHeaderLeft);
    this._tabHeaderLeft.Location = new Point(4, 22);
    this._tabHeaderLeft.Name = "_tabHeaderLeft";
    this._tabHeaderLeft.Padding = new Padding(3);
    this._tabHeaderLeft.Size = new Size(399, 149);
    this._tabHeaderLeft.TabIndex = 0;
    this._tabHeaderLeft.Text = "влево";
    this._tabHeaderLeft.UseVisualStyleBackColor = true;
    this._buttonSetupFontHeaderLeft.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonSetupFontHeaderLeft.Image = (Image) componentResourceManager.GetObject("_buttonSetupFontHeaderLeft.Image");
    this._buttonSetupFontHeaderLeft.Location = new Point(266, 122);
    this._buttonSetupFontHeaderLeft.Name = "_buttonSetupFontHeaderLeft";
    this._buttonSetupFontHeaderLeft.Size = new Size(23, 23);
    this._buttonSetupFontHeaderLeft.TabIndex = 2;
    this._buttonSetupFontHeaderLeft.Tag = (object) "0";
    this._buttonSetupFontHeaderLeft.UseVisualStyleBackColor = true;
    this._buttonSetupFontHeaderLeft.Click += new EventHandler(this._buttonSetupTitleFont_Click);
    this._buttonPageNumberHeaderLeft.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonPageNumberHeaderLeft.Image = (Image) componentResourceManager.GetObject("_buttonPageNumberHeaderLeft.Image");
    this._buttonPageNumberHeaderLeft.Location = new Point(292, 122);
    this._buttonPageNumberHeaderLeft.Name = "_buttonPageNumberHeaderLeft";
    this._buttonPageNumberHeaderLeft.Size = new Size(23, 23);
    this._buttonPageNumberHeaderLeft.TabIndex = 3;
    this._buttonPageNumberHeaderLeft.Tag = (object) "0";
    this._buttonPageNumberHeaderLeft.UseVisualStyleBackColor = true;
    this._buttonPageNumberHeaderLeft.Click += new EventHandler(this._buttonInsertPageNumberToTitle_Click);
    this._buttonTotalPagesHeaderLeft.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonTotalPagesHeaderLeft.Image = (Image) componentResourceManager.GetObject("_buttonTotalPagesHeaderLeft.Image");
    this._buttonTotalPagesHeaderLeft.Location = new Point(318, 122);
    this._buttonTotalPagesHeaderLeft.Name = "_buttonTotalPagesHeaderLeft";
    this._buttonTotalPagesHeaderLeft.Size = new Size(23, 23);
    this._buttonTotalPagesHeaderLeft.TabIndex = 6;
    this._buttonTotalPagesHeaderLeft.Tag = (object) "0";
    this._buttonTotalPagesHeaderLeft.UseVisualStyleBackColor = true;
    this._buttonTotalPagesHeaderLeft.Click += new EventHandler(this._buttonInsetTotalPagesToTitle_Click);
    this._buttonCurrentDateHeaderLeft.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonCurrentDateHeaderLeft.Image = (Image) componentResourceManager.GetObject("_buttonCurrentDateHeaderLeft.Image");
    this._buttonCurrentDateHeaderLeft.Location = new Point(344, 122);
    this._buttonCurrentDateHeaderLeft.Name = "_buttonCurrentDateHeaderLeft";
    this._buttonCurrentDateHeaderLeft.Size = new Size(23, 23);
    this._buttonCurrentDateHeaderLeft.TabIndex = 7;
    this._buttonCurrentDateHeaderLeft.Tag = (object) "0";
    this._buttonCurrentDateHeaderLeft.UseVisualStyleBackColor = true;
    this._buttonCurrentDateHeaderLeft.Click += new EventHandler(this._buttonInsetDateToTitle_Click);
    this._buttonCurrentTimeHeaderLeft.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonCurrentTimeHeaderLeft.Image = (Image) componentResourceManager.GetObject("_buttonCurrentTimeHeaderLeft.Image");
    this._buttonCurrentTimeHeaderLeft.Location = new Point(370, 122);
    this._buttonCurrentTimeHeaderLeft.Name = "_buttonCurrentTimeHeaderLeft";
    this._buttonCurrentTimeHeaderLeft.Size = new Size(23, 23);
    this._buttonCurrentTimeHeaderLeft.TabIndex = 8;
    this._buttonCurrentTimeHeaderLeft.Tag = (object) "0";
    this._buttonCurrentTimeHeaderLeft.UseVisualStyleBackColor = true;
    this._buttonCurrentTimeHeaderLeft.Click += new EventHandler(this._buttonInsetTimeToTitle_Click);
    this._buttonAddFieldHeaderLeft.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this._buttonAddFieldHeaderLeft.Location = new Point(7, 122);
    this._buttonAddFieldHeaderLeft.Name = "_buttonAddFieldHeaderLeft";
    this._buttonAddFieldHeaderLeft.Size = new Size(105, 23);
    this._buttonAddFieldHeaderLeft.TabIndex = 1;
    this._buttonAddFieldHeaderLeft.Tag = (object) "0";
    this._buttonAddFieldHeaderLeft.Text = "Добавить поле...";
    this._buttonAddFieldHeaderLeft.UseVisualStyleBackColor = true;
    this._buttonAddFieldHeaderLeft.Click += new EventHandler(this._buttonInsetAttributeToTitle_Click);
    this._textBoxHeaderLeft.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._textBoxHeaderLeft.BorderStyle = BorderStyle.FixedSingle;
    this._textBoxHeaderLeft.HideSelection = false;
    this._textBoxHeaderLeft.Location = new Point(6, 6);
    this._textBoxHeaderLeft.MaxLength = 500;
    this._textBoxHeaderLeft.Name = "_textBoxHeaderLeft";
    this._textBoxHeaderLeft.ScrollBars = RichTextBoxScrollBars.Vertical;
    this._textBoxHeaderLeft.ShowSelectionMargin = true;
    this._textBoxHeaderLeft.Size = new Size(387, 112 /*0x70*/);
    this._textBoxHeaderLeft.TabIndex = 0;
    this._textBoxHeaderLeft.Tag = (object) "0";
    this._textBoxHeaderLeft.Text = "";
    this._textBoxHeaderLeft.TextChanged += new EventHandler(this._textBoxTitle_TextChanged);
    this._textBoxHeaderLeft.Enter += new EventHandler(this._textBoxTitle_Enter);
    this._textBoxHeaderLeft.Leave += new EventHandler(this._textBoxTitle_Leave);
    this._tabHeaderCenter.Controls.Add((Control) this._buttonSetupFontHeaderCenter);
    this._tabHeaderCenter.Controls.Add((Control) this._buttonPageNumberHeaderCenter);
    this._tabHeaderCenter.Controls.Add((Control) this._buttonTotalPagesHeaderCenter);
    this._tabHeaderCenter.Controls.Add((Control) this._buttonCurrentDateHeaderCenter);
    this._tabHeaderCenter.Controls.Add((Control) this._buttonCurrentTimeHeaderCenter);
    this._tabHeaderCenter.Controls.Add((Control) this._buttonAddFieldHeaderCenter);
    this._tabHeaderCenter.Controls.Add((Control) this._textBoxHeaderCenter);
    this._tabHeaderCenter.Location = new Point(4, 22);
    this._tabHeaderCenter.Name = "_tabHeaderCenter";
    this._tabHeaderCenter.Padding = new Padding(3);
    this._tabHeaderCenter.Size = new Size(399, 149);
    this._tabHeaderCenter.TabIndex = 1;
    this._tabHeaderCenter.Text = "по центру";
    this._tabHeaderCenter.UseVisualStyleBackColor = true;
    this._buttonSetupFontHeaderCenter.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonSetupFontHeaderCenter.Image = (Image) componentResourceManager.GetObject("_buttonSetupFontHeaderCenter.Image");
    this._buttonSetupFontHeaderCenter.Location = new Point(266, 122);
    this._buttonSetupFontHeaderCenter.Name = "_buttonSetupFontHeaderCenter";
    this._buttonSetupFontHeaderCenter.Size = new Size(23, 23);
    this._buttonSetupFontHeaderCenter.TabIndex = 4;
    this._buttonSetupFontHeaderCenter.Tag = (object) "1";
    this._buttonSetupFontHeaderCenter.UseVisualStyleBackColor = true;
    this._buttonSetupFontHeaderCenter.Click += new EventHandler(this._buttonSetupTitleFont_Click);
    this._buttonPageNumberHeaderCenter.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonPageNumberHeaderCenter.Image = (Image) componentResourceManager.GetObject("_buttonPageNumberHeaderCenter.Image");
    this._buttonPageNumberHeaderCenter.Location = new Point(292, 122);
    this._buttonPageNumberHeaderCenter.Name = "_buttonPageNumberHeaderCenter";
    this._buttonPageNumberHeaderCenter.Size = new Size(23, 23);
    this._buttonPageNumberHeaderCenter.TabIndex = 5;
    this._buttonPageNumberHeaderCenter.Tag = (object) "1";
    this._buttonPageNumberHeaderCenter.UseVisualStyleBackColor = true;
    this._buttonPageNumberHeaderCenter.Click += new EventHandler(this._buttonInsertPageNumberToTitle_Click);
    this._buttonTotalPagesHeaderCenter.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonTotalPagesHeaderCenter.Image = (Image) componentResourceManager.GetObject("_buttonTotalPagesHeaderCenter.Image");
    this._buttonTotalPagesHeaderCenter.Location = new Point(318, 122);
    this._buttonTotalPagesHeaderCenter.Name = "_buttonTotalPagesHeaderCenter";
    this._buttonTotalPagesHeaderCenter.Size = new Size(23, 23);
    this._buttonTotalPagesHeaderCenter.TabIndex = 6;
    this._buttonTotalPagesHeaderCenter.Tag = (object) "1";
    this._buttonTotalPagesHeaderCenter.UseVisualStyleBackColor = true;
    this._buttonTotalPagesHeaderCenter.Click += new EventHandler(this._buttonInsetTotalPagesToTitle_Click);
    this._buttonCurrentDateHeaderCenter.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonCurrentDateHeaderCenter.Image = (Image) componentResourceManager.GetObject("_buttonCurrentDateHeaderCenter.Image");
    this._buttonCurrentDateHeaderCenter.Location = new Point(344, 122);
    this._buttonCurrentDateHeaderCenter.Name = "_buttonCurrentDateHeaderCenter";
    this._buttonCurrentDateHeaderCenter.Size = new Size(23, 23);
    this._buttonCurrentDateHeaderCenter.TabIndex = 7;
    this._buttonCurrentDateHeaderCenter.Tag = (object) "1";
    this._buttonCurrentDateHeaderCenter.UseVisualStyleBackColor = true;
    this._buttonCurrentDateHeaderCenter.Click += new EventHandler(this._buttonInsetDateToTitle_Click);
    this._buttonCurrentTimeHeaderCenter.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonCurrentTimeHeaderCenter.Image = (Image) componentResourceManager.GetObject("_buttonCurrentTimeHeaderCenter.Image");
    this._buttonCurrentTimeHeaderCenter.Location = new Point(370, 122);
    this._buttonCurrentTimeHeaderCenter.Name = "_buttonCurrentTimeHeaderCenter";
    this._buttonCurrentTimeHeaderCenter.Size = new Size(23, 23);
    this._buttonCurrentTimeHeaderCenter.TabIndex = 8;
    this._buttonCurrentTimeHeaderCenter.Tag = (object) "1";
    this._buttonCurrentTimeHeaderCenter.UseVisualStyleBackColor = true;
    this._buttonCurrentTimeHeaderCenter.Click += new EventHandler(this._buttonInsetTimeToTitle_Click);
    this._buttonAddFieldHeaderCenter.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this._buttonAddFieldHeaderCenter.Location = new Point(7, 122);
    this._buttonAddFieldHeaderCenter.Name = "_buttonAddFieldHeaderCenter";
    this._buttonAddFieldHeaderCenter.Size = new Size(105, 23);
    this._buttonAddFieldHeaderCenter.TabIndex = 3;
    this._buttonAddFieldHeaderCenter.Tag = (object) "1";
    this._buttonAddFieldHeaderCenter.Text = "Добавить поле...";
    this._buttonAddFieldHeaderCenter.UseVisualStyleBackColor = true;
    this._buttonAddFieldHeaderCenter.Click += new EventHandler(this._buttonInsetAttributeToTitle_Click);
    this._textBoxHeaderCenter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._textBoxHeaderCenter.HideSelection = false;
    this._textBoxHeaderCenter.Location = new Point(6, 6);
    this._textBoxHeaderCenter.MaxLength = 500;
    this._textBoxHeaderCenter.Name = "_textBoxHeaderCenter";
    this._textBoxHeaderCenter.ScrollBars = RichTextBoxScrollBars.Vertical;
    this._textBoxHeaderCenter.ShowSelectionMargin = true;
    this._textBoxHeaderCenter.Size = new Size(387, 112 /*0x70*/);
    this._textBoxHeaderCenter.TabIndex = 1;
    this._textBoxHeaderCenter.Tag = (object) "1";
    this._textBoxHeaderCenter.Text = "";
    this._textBoxHeaderCenter.TextChanged += new EventHandler(this._textBoxTitle_TextChanged);
    this._textBoxHeaderCenter.Enter += new EventHandler(this._textBoxTitle_Enter);
    this._textBoxHeaderCenter.Leave += new EventHandler(this._textBoxTitle_Leave);
    this._tabHeaderRight.Controls.Add((Control) this._buttonSetupFontHeaderRight);
    this._tabHeaderRight.Controls.Add((Control) this._buttonPageNumberHeaderRight);
    this._tabHeaderRight.Controls.Add((Control) this._buttonTotalPagesHeaderRight);
    this._tabHeaderRight.Controls.Add((Control) this._buttonCurrentDateHeaderRight);
    this._tabHeaderRight.Controls.Add((Control) this._buttonCurrentTimeHeaderRight);
    this._tabHeaderRight.Controls.Add((Control) this._buttonAddFieldHeaderRight);
    this._tabHeaderRight.Controls.Add((Control) this._textBoxHeaderRight);
    this._tabHeaderRight.Location = new Point(4, 22);
    this._tabHeaderRight.Name = "_tabHeaderRight";
    this._tabHeaderRight.Padding = new Padding(3);
    this._tabHeaderRight.Size = new Size(399, 149);
    this._tabHeaderRight.TabIndex = 2;
    this._tabHeaderRight.Text = "вправо";
    this._tabHeaderRight.UseVisualStyleBackColor = true;
    this._buttonSetupFontHeaderRight.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonSetupFontHeaderRight.Image = (Image) componentResourceManager.GetObject("_buttonSetupFontHeaderRight.Image");
    this._buttonSetupFontHeaderRight.Location = new Point(266, 122);
    this._buttonSetupFontHeaderRight.Name = "_buttonSetupFontHeaderRight";
    this._buttonSetupFontHeaderRight.Size = new Size(23, 23);
    this._buttonSetupFontHeaderRight.TabIndex = 3;
    this._buttonSetupFontHeaderRight.Tag = (object) "2";
    this._buttonSetupFontHeaderRight.UseVisualStyleBackColor = true;
    this._buttonSetupFontHeaderRight.Click += new EventHandler(this._buttonSetupTitleFont_Click);
    this._buttonPageNumberHeaderRight.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonPageNumberHeaderRight.Image = (Image) componentResourceManager.GetObject("_buttonPageNumberHeaderRight.Image");
    this._buttonPageNumberHeaderRight.Location = new Point(292, 122);
    this._buttonPageNumberHeaderRight.Name = "_buttonPageNumberHeaderRight";
    this._buttonPageNumberHeaderRight.Size = new Size(23, 23);
    this._buttonPageNumberHeaderRight.TabIndex = 3;
    this._buttonPageNumberHeaderRight.Tag = (object) "2";
    this._buttonPageNumberHeaderRight.UseVisualStyleBackColor = true;
    this._buttonPageNumberHeaderRight.Click += new EventHandler(this._buttonInsertPageNumberToTitle_Click);
    this._buttonTotalPagesHeaderRight.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonTotalPagesHeaderRight.Image = (Image) componentResourceManager.GetObject("_buttonTotalPagesHeaderRight.Image");
    this._buttonTotalPagesHeaderRight.Location = new Point(318, 122);
    this._buttonTotalPagesHeaderRight.Name = "_buttonTotalPagesHeaderRight";
    this._buttonTotalPagesHeaderRight.Size = new Size(23, 23);
    this._buttonTotalPagesHeaderRight.TabIndex = 3;
    this._buttonTotalPagesHeaderRight.Tag = (object) "2";
    this._buttonTotalPagesHeaderRight.UseVisualStyleBackColor = true;
    this._buttonTotalPagesHeaderRight.Click += new EventHandler(this._buttonInsetTotalPagesToTitle_Click);
    this._buttonCurrentDateHeaderRight.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonCurrentDateHeaderRight.Image = (Image) componentResourceManager.GetObject("_buttonCurrentDateHeaderRight.Image");
    this._buttonCurrentDateHeaderRight.Location = new Point(344, 122);
    this._buttonCurrentDateHeaderRight.Name = "_buttonCurrentDateHeaderRight";
    this._buttonCurrentDateHeaderRight.Size = new Size(23, 23);
    this._buttonCurrentDateHeaderRight.TabIndex = 3;
    this._buttonCurrentDateHeaderRight.Tag = (object) "2";
    this._buttonCurrentDateHeaderRight.UseVisualStyleBackColor = true;
    this._buttonCurrentDateHeaderRight.Click += new EventHandler(this._buttonInsetDateToTitle_Click);
    this._buttonCurrentTimeHeaderRight.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonCurrentTimeHeaderRight.Image = (Image) componentResourceManager.GetObject("_buttonCurrentTimeHeaderRight.Image");
    this._buttonCurrentTimeHeaderRight.Location = new Point(370, 122);
    this._buttonCurrentTimeHeaderRight.Name = "_buttonCurrentTimeHeaderRight";
    this._buttonCurrentTimeHeaderRight.Size = new Size(23, 23);
    this._buttonCurrentTimeHeaderRight.TabIndex = 3;
    this._buttonCurrentTimeHeaderRight.Tag = (object) "2";
    this._buttonCurrentTimeHeaderRight.UseVisualStyleBackColor = true;
    this._buttonCurrentTimeHeaderRight.Click += new EventHandler(this._buttonInsetTimeToTitle_Click);
    this._buttonAddFieldHeaderRight.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this._buttonAddFieldHeaderRight.Location = new Point(7, 122);
    this._buttonAddFieldHeaderRight.Name = "_buttonAddFieldHeaderRight";
    this._buttonAddFieldHeaderRight.Size = new Size(105, 23);
    this._buttonAddFieldHeaderRight.TabIndex = 2;
    this._buttonAddFieldHeaderRight.Tag = (object) "2";
    this._buttonAddFieldHeaderRight.Text = "Добавить поле...";
    this._buttonAddFieldHeaderRight.UseVisualStyleBackColor = true;
    this._buttonAddFieldHeaderRight.Click += new EventHandler(this._buttonInsetAttributeToTitle_Click);
    this._textBoxHeaderRight.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._textBoxHeaderRight.HideSelection = false;
    this._textBoxHeaderRight.Location = new Point(6, 6);
    this._textBoxHeaderRight.MaxLength = 500;
    this._textBoxHeaderRight.Name = "_textBoxHeaderRight";
    this._textBoxHeaderRight.ScrollBars = RichTextBoxScrollBars.Vertical;
    this._textBoxHeaderRight.ShowSelectionMargin = true;
    this._textBoxHeaderRight.Size = new Size(387, 112 /*0x70*/);
    this._textBoxHeaderRight.TabIndex = 1;
    this._textBoxHeaderRight.Tag = (object) "2";
    this._textBoxHeaderRight.Text = "";
    this._textBoxHeaderRight.TextChanged += new EventHandler(this._textBoxTitle_TextChanged);
    this._textBoxHeaderRight.Enter += new EventHandler(this._textBoxTitle_Enter);
    this._textBoxHeaderRight.Leave += new EventHandler(this._textBoxTitle_Leave);
    this._labelHeaderPreview.AutoSize = true;
    this._labelHeaderPreview.Location = new Point(8, 15);
    this._labelHeaderPreview.Name = "_labelHeaderPreview";
    this._labelHeaderPreview.Size = new Size(61, 13);
    this._labelHeaderPreview.TabIndex = 0;
    this._labelHeaderPreview.Text = "Просмотр:";
    this._tabFooter.BackColor = SystemColors.Control;
    this._tabFooter.Controls.Add((Control) this._richTextConverter);
    this._tabFooter.Controls.Add((Control) this._textOutBoxFooterRight);
    this._tabFooter.Controls.Add((Control) this._textOutBoxFooterCenter);
    this._tabFooter.Controls.Add((Control) this._textOutBoxFooterLeft);
    this._tabFooter.Controls.Add((Control) this._pictureFooter);
    this._tabFooter.Controls.Add((Control) this._tabsFooter);
    this._tabFooter.Controls.Add((Control) this._labelFooterPreview);
    this._tabFooter.Location = new Point(4, 24);
    this._tabFooter.Name = "_tabFooter";
    this._tabFooter.Padding = new Padding(3);
    this._tabFooter.Size = new Size(505, 334);
    this._tabFooter.TabIndex = 3;
    this._tabFooter.Text = "Нижний колонтитул";
    this._richTextConverter.BorderStyle = BorderStyle.None;
    this._richTextConverter.Cursor = Cursors.Default;
    this._richTextConverter.Enabled = false;
    this._richTextConverter.Location = new Point(425, 112 /*0x70*/);
    this._richTextConverter.Name = "_richTextConverter";
    this._richTextConverter.ReadOnly = true;
    this._richTextConverter.Size = new Size(48 /*0x30*/, 56);
    this._richTextConverter.TabIndex = 15;
    this._richTextConverter.TabStop = false;
    this._richTextConverter.Text = "";
    this._richTextConverter.Visible = false;
    this._textOutBoxFooterRight.BorderStyle = BorderStyle.None;
    this._textOutBoxFooterRight.Cursor = Cursors.Default;
    this._textOutBoxFooterRight.Location = new Point(21, 46);
    this._textOutBoxFooterRight.Name = "_textOutBoxFooterRight";
    this._textOutBoxFooterRight.ReadOnly = true;
    this._textOutBoxFooterRight.ScrollBars = RichTextBoxScrollBars.None;
    this._textOutBoxFooterRight.Size = new Size(373, 44);
    this._textOutBoxFooterRight.TabIndex = 12;
    this._textOutBoxFooterRight.TabStop = false;
    this._textOutBoxFooterRight.Tag = (object) "5";
    this._textOutBoxFooterRight.Text = "";
    this._textOutBoxFooterRight.Visible = false;
    this._textOutBoxFooterCenter.BorderStyle = BorderStyle.None;
    this._textOutBoxFooterCenter.Cursor = Cursors.Default;
    this._textOutBoxFooterCenter.Location = new Point(21, 46);
    this._textOutBoxFooterCenter.Name = "_textOutBoxFooterCenter";
    this._textOutBoxFooterCenter.ReadOnly = true;
    this._textOutBoxFooterCenter.ScrollBars = RichTextBoxScrollBars.None;
    this._textOutBoxFooterCenter.Size = new Size(373, 44);
    this._textOutBoxFooterCenter.TabIndex = 12;
    this._textOutBoxFooterCenter.TabStop = false;
    this._textOutBoxFooterCenter.Tag = (object) "4";
    this._textOutBoxFooterCenter.Text = "";
    this._textOutBoxFooterCenter.Visible = false;
    this._textOutBoxFooterLeft.BorderStyle = BorderStyle.None;
    this._textOutBoxFooterLeft.Cursor = Cursors.Default;
    this._textOutBoxFooterLeft.Location = new Point(21, 46);
    this._textOutBoxFooterLeft.Name = "_textOutBoxFooterLeft";
    this._textOutBoxFooterLeft.ReadOnly = true;
    this._textOutBoxFooterLeft.ScrollBars = RichTextBoxScrollBars.None;
    this._textOutBoxFooterLeft.Size = new Size(373, 44);
    this._textOutBoxFooterLeft.TabIndex = 12;
    this._textOutBoxFooterLeft.TabStop = false;
    this._textOutBoxFooterLeft.Tag = (object) "3";
    this._textOutBoxFooterLeft.Text = "";
    this._textOutBoxFooterLeft.Visible = false;
    this._pictureFooter.Image = (Image) componentResourceManager.GetObject("_pictureFooter.Image");
    this._pictureFooter.Location = new Point(11, 31 /*0x1F*/);
    this._pictureFooter.Name = "_pictureFooter";
    this._pictureFooter.Size = new Size(394, 75);
    this._pictureFooter.TabIndex = 14;
    this._pictureFooter.TabStop = false;
    this._pictureFooter.Paint += new PaintEventHandler(this._pictureFooter_Paint);
    this._tabsFooter.Controls.Add((Control) this._tabFooterLeft);
    this._tabsFooter.Controls.Add((Control) this._tabFooterCenter);
    this._tabsFooter.Controls.Add((Control) this._tabFooterRight);
    this._tabsFooter.Location = new Point(8, 112 /*0x70*/);
    this._tabsFooter.Name = "_tabsFooter";
    this._tabsFooter.Padding = new Point(8, 3);
    this._tabsFooter.SelectedIndex = 0;
    this._tabsFooter.ShowTabHeaders = true;
    this._tabsFooter.Size = new Size(407, 175);
    this._tabsFooter.TabIndex = 13;
    this._tabFooterLeft.Controls.Add((Control) this._buttonSetupFontFooterLeft);
    this._tabFooterLeft.Controls.Add((Control) this._buttonPageNumberFooterLeft);
    this._tabFooterLeft.Controls.Add((Control) this._buttonTotalPagesFooterLeft);
    this._tabFooterLeft.Controls.Add((Control) this._buttonCurrentDateFooterLeft);
    this._tabFooterLeft.Controls.Add((Control) this._buttonCurrentTimeFooterLeft);
    this._tabFooterLeft.Controls.Add((Control) this._buttonAddFieldFooterLeft);
    this._tabFooterLeft.Controls.Add((Control) this._textBoxFooterLeft);
    this._tabFooterLeft.Location = new Point(4, 22);
    this._tabFooterLeft.Name = "_tabFooterLeft";
    this._tabFooterLeft.Padding = new Padding(3);
    this._tabFooterLeft.Size = new Size(399, 149);
    this._tabFooterLeft.TabIndex = 0;
    this._tabFooterLeft.Text = "влево";
    this._tabFooterLeft.UseVisualStyleBackColor = true;
    this._buttonSetupFontFooterLeft.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonSetupFontFooterLeft.Image = (Image) componentResourceManager.GetObject("_buttonSetupFontFooterLeft.Image");
    this._buttonSetupFontFooterLeft.Location = new Point(266, 122);
    this._buttonSetupFontFooterLeft.Name = "_buttonSetupFontFooterLeft";
    this._buttonSetupFontFooterLeft.Size = new Size(23, 23);
    this._buttonSetupFontFooterLeft.TabIndex = 4;
    this._buttonSetupFontFooterLeft.Tag = (object) "3";
    this._buttonSetupFontFooterLeft.UseVisualStyleBackColor = true;
    this._buttonSetupFontFooterLeft.Click += new EventHandler(this._buttonSetupTitleFont_Click);
    this._buttonPageNumberFooterLeft.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonPageNumberFooterLeft.Image = (Image) componentResourceManager.GetObject("_buttonPageNumberFooterLeft.Image");
    this._buttonPageNumberFooterLeft.Location = new Point(292, 122);
    this._buttonPageNumberFooterLeft.Name = "_buttonPageNumberFooterLeft";
    this._buttonPageNumberFooterLeft.Size = new Size(23, 23);
    this._buttonPageNumberFooterLeft.TabIndex = 5;
    this._buttonPageNumberFooterLeft.Tag = (object) "3";
    this._buttonPageNumberFooterLeft.UseVisualStyleBackColor = true;
    this._buttonPageNumberFooterLeft.Click += new EventHandler(this._buttonInsertPageNumberToTitle_Click);
    this._buttonTotalPagesFooterLeft.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonTotalPagesFooterLeft.Image = (Image) componentResourceManager.GetObject("_buttonTotalPagesFooterLeft.Image");
    this._buttonTotalPagesFooterLeft.Location = new Point(318, 122);
    this._buttonTotalPagesFooterLeft.Name = "_buttonTotalPagesFooterLeft";
    this._buttonTotalPagesFooterLeft.Size = new Size(23, 23);
    this._buttonTotalPagesFooterLeft.TabIndex = 6;
    this._buttonTotalPagesFooterLeft.Tag = (object) "3";
    this._buttonTotalPagesFooterLeft.UseVisualStyleBackColor = true;
    this._buttonTotalPagesFooterLeft.Click += new EventHandler(this._buttonInsetTotalPagesToTitle_Click);
    this._buttonCurrentDateFooterLeft.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonCurrentDateFooterLeft.Image = (Image) componentResourceManager.GetObject("_buttonCurrentDateFooterLeft.Image");
    this._buttonCurrentDateFooterLeft.Location = new Point(344, 122);
    this._buttonCurrentDateFooterLeft.Name = "_buttonCurrentDateFooterLeft";
    this._buttonCurrentDateFooterLeft.Size = new Size(23, 23);
    this._buttonCurrentDateFooterLeft.TabIndex = 7;
    this._buttonCurrentDateFooterLeft.Tag = (object) "3";
    this._buttonCurrentDateFooterLeft.UseVisualStyleBackColor = true;
    this._buttonCurrentDateFooterLeft.Click += new EventHandler(this._buttonInsetDateToTitle_Click);
    this._buttonCurrentTimeFooterLeft.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonCurrentTimeFooterLeft.Image = (Image) componentResourceManager.GetObject("_buttonCurrentTimeFooterLeft.Image");
    this._buttonCurrentTimeFooterLeft.Location = new Point(370, 122);
    this._buttonCurrentTimeFooterLeft.Name = "_buttonCurrentTimeFooterLeft";
    this._buttonCurrentTimeFooterLeft.Size = new Size(23, 23);
    this._buttonCurrentTimeFooterLeft.TabIndex = 8;
    this._buttonCurrentTimeFooterLeft.Tag = (object) "3";
    this._buttonCurrentTimeFooterLeft.UseVisualStyleBackColor = true;
    this._buttonCurrentTimeFooterLeft.Click += new EventHandler(this._buttonInsetTimeToTitle_Click);
    this._buttonAddFieldFooterLeft.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this._buttonAddFieldFooterLeft.Location = new Point(7, 122);
    this._buttonAddFieldFooterLeft.Name = "_buttonAddFieldFooterLeft";
    this._buttonAddFieldFooterLeft.Size = new Size(105, 23);
    this._buttonAddFieldFooterLeft.TabIndex = 3;
    this._buttonAddFieldFooterLeft.Tag = (object) "3";
    this._buttonAddFieldFooterLeft.Text = "Добавить поле...";
    this._buttonAddFieldFooterLeft.UseVisualStyleBackColor = true;
    this._buttonAddFieldFooterLeft.Click += new EventHandler(this._buttonInsetAttributeToTitle_Click);
    this._textBoxFooterLeft.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._textBoxFooterLeft.HideSelection = false;
    this._textBoxFooterLeft.Location = new Point(6, 6);
    this._textBoxFooterLeft.MaxLength = 500;
    this._textBoxFooterLeft.Name = "_textBoxFooterLeft";
    this._textBoxFooterLeft.ScrollBars = RichTextBoxScrollBars.Vertical;
    this._textBoxFooterLeft.ShowSelectionMargin = true;
    this._textBoxFooterLeft.Size = new Size(387, 112 /*0x70*/);
    this._textBoxFooterLeft.TabIndex = 0;
    this._textBoxFooterLeft.Tag = (object) "3";
    this._textBoxFooterLeft.Text = "";
    this._textBoxFooterLeft.TextChanged += new EventHandler(this._textBoxTitle_TextChanged);
    this._textBoxFooterLeft.Enter += new EventHandler(this._textBoxTitle_Enter);
    this._textBoxFooterLeft.Leave += new EventHandler(this._textBoxTitle_Leave);
    this._tabFooterCenter.Controls.Add((Control) this._buttonSetupFontFooterCenter);
    this._tabFooterCenter.Controls.Add((Control) this._buttonPageNumberFooterCenter);
    this._tabFooterCenter.Controls.Add((Control) this._buttonTotalPagesFooterCenter);
    this._tabFooterCenter.Controls.Add((Control) this._buttonCurrentDateFooterCenter);
    this._tabFooterCenter.Controls.Add((Control) this._buttonCurrentTimeFooterCenter);
    this._tabFooterCenter.Controls.Add((Control) this._buttonAddFieldFooterCenter);
    this._tabFooterCenter.Controls.Add((Control) this._textBoxFooterCenter);
    this._tabFooterCenter.Location = new Point(4, 22);
    this._tabFooterCenter.Name = "_tabFooterCenter";
    this._tabFooterCenter.Padding = new Padding(3);
    this._tabFooterCenter.Size = new Size(399, 149);
    this._tabFooterCenter.TabIndex = 1;
    this._tabFooterCenter.Text = "по центру";
    this._tabFooterCenter.UseVisualStyleBackColor = true;
    this._buttonSetupFontFooterCenter.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonSetupFontFooterCenter.Image = (Image) componentResourceManager.GetObject("_buttonSetupFontFooterCenter.Image");
    this._buttonSetupFontFooterCenter.Location = new Point(266, 122);
    this._buttonSetupFontFooterCenter.Name = "_buttonSetupFontFooterCenter";
    this._buttonSetupFontFooterCenter.Size = new Size(23, 23);
    this._buttonSetupFontFooterCenter.TabIndex = 4;
    this._buttonSetupFontFooterCenter.Tag = (object) "4";
    this._buttonSetupFontFooterCenter.UseVisualStyleBackColor = true;
    this._buttonSetupFontFooterCenter.Click += new EventHandler(this._buttonSetupTitleFont_Click);
    this._buttonPageNumberFooterCenter.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonPageNumberFooterCenter.Image = (Image) componentResourceManager.GetObject("_buttonPageNumberFooterCenter.Image");
    this._buttonPageNumberFooterCenter.Location = new Point(292, 122);
    this._buttonPageNumberFooterCenter.Name = "_buttonPageNumberFooterCenter";
    this._buttonPageNumberFooterCenter.Size = new Size(23, 23);
    this._buttonPageNumberFooterCenter.TabIndex = 5;
    this._buttonPageNumberFooterCenter.Tag = (object) "4";
    this._buttonPageNumberFooterCenter.UseVisualStyleBackColor = true;
    this._buttonPageNumberFooterCenter.Click += new EventHandler(this._buttonInsertPageNumberToTitle_Click);
    this._buttonTotalPagesFooterCenter.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonTotalPagesFooterCenter.Image = (Image) componentResourceManager.GetObject("_buttonTotalPagesFooterCenter.Image");
    this._buttonTotalPagesFooterCenter.Location = new Point(318, 122);
    this._buttonTotalPagesFooterCenter.Name = "_buttonTotalPagesFooterCenter";
    this._buttonTotalPagesFooterCenter.Size = new Size(23, 23);
    this._buttonTotalPagesFooterCenter.TabIndex = 6;
    this._buttonTotalPagesFooterCenter.Tag = (object) "4";
    this._buttonTotalPagesFooterCenter.UseVisualStyleBackColor = true;
    this._buttonTotalPagesFooterCenter.Click += new EventHandler(this._buttonInsetTotalPagesToTitle_Click);
    this._buttonCurrentDateFooterCenter.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonCurrentDateFooterCenter.Image = (Image) componentResourceManager.GetObject("_buttonCurrentDateFooterCenter.Image");
    this._buttonCurrentDateFooterCenter.Location = new Point(344, 122);
    this._buttonCurrentDateFooterCenter.Name = "_buttonCurrentDateFooterCenter";
    this._buttonCurrentDateFooterCenter.Size = new Size(23, 23);
    this._buttonCurrentDateFooterCenter.TabIndex = 7;
    this._buttonCurrentDateFooterCenter.Tag = (object) "4";
    this._buttonCurrentDateFooterCenter.UseVisualStyleBackColor = true;
    this._buttonCurrentDateFooterCenter.Click += new EventHandler(this._buttonInsetDateToTitle_Click);
    this._buttonCurrentTimeFooterCenter.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonCurrentTimeFooterCenter.Image = (Image) componentResourceManager.GetObject("_buttonCurrentTimeFooterCenter.Image");
    this._buttonCurrentTimeFooterCenter.Location = new Point(370, 122);
    this._buttonCurrentTimeFooterCenter.Name = "_buttonCurrentTimeFooterCenter";
    this._buttonCurrentTimeFooterCenter.Size = new Size(23, 23);
    this._buttonCurrentTimeFooterCenter.TabIndex = 8;
    this._buttonCurrentTimeFooterCenter.Tag = (object) "4";
    this._buttonCurrentTimeFooterCenter.UseVisualStyleBackColor = true;
    this._buttonCurrentTimeFooterCenter.Click += new EventHandler(this._buttonInsetTimeToTitle_Click);
    this._buttonAddFieldFooterCenter.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this._buttonAddFieldFooterCenter.Location = new Point(7, 122);
    this._buttonAddFieldFooterCenter.Name = "_buttonAddFieldFooterCenter";
    this._buttonAddFieldFooterCenter.Size = new Size(105, 23);
    this._buttonAddFieldFooterCenter.TabIndex = 3;
    this._buttonAddFieldFooterCenter.Tag = (object) "4";
    this._buttonAddFieldFooterCenter.Text = "Добавить поле...";
    this._buttonAddFieldFooterCenter.UseVisualStyleBackColor = true;
    this._buttonAddFieldFooterCenter.Click += new EventHandler(this._buttonInsetAttributeToTitle_Click);
    this._textBoxFooterCenter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._textBoxFooterCenter.HideSelection = false;
    this._textBoxFooterCenter.Location = new Point(6, 6);
    this._textBoxFooterCenter.MaxLength = 500;
    this._textBoxFooterCenter.Name = "_textBoxFooterCenter";
    this._textBoxFooterCenter.ScrollBars = RichTextBoxScrollBars.Vertical;
    this._textBoxFooterCenter.ShowSelectionMargin = true;
    this._textBoxFooterCenter.Size = new Size(387, 112 /*0x70*/);
    this._textBoxFooterCenter.TabIndex = 1;
    this._textBoxFooterCenter.Tag = (object) "4";
    this._textBoxFooterCenter.Text = "";
    this._textBoxFooterCenter.TextChanged += new EventHandler(this._textBoxTitle_TextChanged);
    this._textBoxFooterCenter.Enter += new EventHandler(this._textBoxTitle_Enter);
    this._textBoxFooterCenter.Leave += new EventHandler(this._textBoxTitle_Leave);
    this._tabFooterRight.Controls.Add((Control) this._buttonSetupFontFooterRight);
    this._tabFooterRight.Controls.Add((Control) this._buttonPageNumberFooterRight);
    this._tabFooterRight.Controls.Add((Control) this._buttonTotalPagesFooterRight);
    this._tabFooterRight.Controls.Add((Control) this._buttonCurrentDateFooterRight);
    this._tabFooterRight.Controls.Add((Control) this._buttonCurrentTimeFooterRight);
    this._tabFooterRight.Controls.Add((Control) this._buttonAddFieldFooterRight);
    this._tabFooterRight.Controls.Add((Control) this._textBoxFooterRight);
    this._tabFooterRight.Location = new Point(4, 22);
    this._tabFooterRight.Name = "_tabFooterRight";
    this._tabFooterRight.Padding = new Padding(3);
    this._tabFooterRight.Size = new Size(399, 149);
    this._tabFooterRight.TabIndex = 2;
    this._tabFooterRight.Text = "вправо";
    this._tabFooterRight.UseVisualStyleBackColor = true;
    this._buttonSetupFontFooterRight.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonSetupFontFooterRight.Image = (Image) componentResourceManager.GetObject("_buttonSetupFontFooterRight.Image");
    this._buttonSetupFontFooterRight.Location = new Point(266, 122);
    this._buttonSetupFontFooterRight.Name = "_buttonSetupFontFooterRight";
    this._buttonSetupFontFooterRight.Size = new Size(23, 23);
    this._buttonSetupFontFooterRight.TabIndex = 3;
    this._buttonSetupFontFooterRight.Tag = (object) "5";
    this._buttonSetupFontFooterRight.UseVisualStyleBackColor = true;
    this._buttonSetupFontFooterRight.Click += new EventHandler(this._buttonSetupTitleFont_Click);
    this._buttonPageNumberFooterRight.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonPageNumberFooterRight.Image = (Image) componentResourceManager.GetObject("_buttonPageNumberFooterRight.Image");
    this._buttonPageNumberFooterRight.Location = new Point(292, 122);
    this._buttonPageNumberFooterRight.Name = "_buttonPageNumberFooterRight";
    this._buttonPageNumberFooterRight.Size = new Size(23, 23);
    this._buttonPageNumberFooterRight.TabIndex = 3;
    this._buttonPageNumberFooterRight.Tag = (object) "5";
    this._buttonPageNumberFooterRight.UseVisualStyleBackColor = true;
    this._buttonPageNumberFooterRight.Click += new EventHandler(this._buttonInsertPageNumberToTitle_Click);
    this._buttonTotalPagesFooterRight.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonTotalPagesFooterRight.Image = (Image) componentResourceManager.GetObject("_buttonTotalPagesFooterRight.Image");
    this._buttonTotalPagesFooterRight.Location = new Point(318, 122);
    this._buttonTotalPagesFooterRight.Name = "_buttonTotalPagesFooterRight";
    this._buttonTotalPagesFooterRight.Size = new Size(23, 23);
    this._buttonTotalPagesFooterRight.TabIndex = 3;
    this._buttonTotalPagesFooterRight.Tag = (object) "5";
    this._buttonTotalPagesFooterRight.UseVisualStyleBackColor = true;
    this._buttonTotalPagesFooterRight.Click += new EventHandler(this._buttonInsetTotalPagesToTitle_Click);
    this._buttonCurrentDateFooterRight.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonCurrentDateFooterRight.Image = (Image) componentResourceManager.GetObject("_buttonCurrentDateFooterRight.Image");
    this._buttonCurrentDateFooterRight.Location = new Point(344, 122);
    this._buttonCurrentDateFooterRight.Name = "_buttonCurrentDateFooterRight";
    this._buttonCurrentDateFooterRight.Size = new Size(23, 23);
    this._buttonCurrentDateFooterRight.TabIndex = 3;
    this._buttonCurrentDateFooterRight.Tag = (object) "5";
    this._buttonCurrentDateFooterRight.UseVisualStyleBackColor = true;
    this._buttonCurrentDateFooterRight.Click += new EventHandler(this._buttonInsetDateToTitle_Click);
    this._buttonCurrentTimeFooterRight.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._buttonCurrentTimeFooterRight.Image = (Image) componentResourceManager.GetObject("_buttonCurrentTimeFooterRight.Image");
    this._buttonCurrentTimeFooterRight.Location = new Point(370, 122);
    this._buttonCurrentTimeFooterRight.Name = "_buttonCurrentTimeFooterRight";
    this._buttonCurrentTimeFooterRight.Size = new Size(23, 23);
    this._buttonCurrentTimeFooterRight.TabIndex = 3;
    this._buttonCurrentTimeFooterRight.Tag = (object) "5";
    this._buttonCurrentTimeFooterRight.UseVisualStyleBackColor = true;
    this._buttonCurrentTimeFooterRight.Click += new EventHandler(this._buttonInsetTimeToTitle_Click);
    this._buttonAddFieldFooterRight.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this._buttonAddFieldFooterRight.Location = new Point(7, 122);
    this._buttonAddFieldFooterRight.Name = "_buttonAddFieldFooterRight";
    this._buttonAddFieldFooterRight.Size = new Size(105, 23);
    this._buttonAddFieldFooterRight.TabIndex = 2;
    this._buttonAddFieldFooterRight.Tag = (object) "5";
    this._buttonAddFieldFooterRight.Text = "Добавить поле...";
    this._buttonAddFieldFooterRight.UseVisualStyleBackColor = true;
    this._buttonAddFieldFooterRight.Click += new EventHandler(this._buttonInsetAttributeToTitle_Click);
    this._textBoxFooterRight.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._textBoxFooterRight.HideSelection = false;
    this._textBoxFooterRight.Location = new Point(6, 6);
    this._textBoxFooterRight.MaxLength = 500;
    this._textBoxFooterRight.Name = "_textBoxFooterRight";
    this._textBoxFooterRight.ScrollBars = RichTextBoxScrollBars.Vertical;
    this._textBoxFooterRight.ShowSelectionMargin = true;
    this._textBoxFooterRight.Size = new Size(387, 112 /*0x70*/);
    this._textBoxFooterRight.TabIndex = 1;
    this._textBoxFooterRight.Tag = (object) "5";
    this._textBoxFooterRight.Text = "";
    this._textBoxFooterRight.TextChanged += new EventHandler(this._textBoxTitle_TextChanged);
    this._textBoxFooterRight.Enter += new EventHandler(this._textBoxTitle_Enter);
    this._textBoxFooterRight.Leave += new EventHandler(this._textBoxTitle_Leave);
    this._labelFooterPreview.AutoSize = true;
    this._labelFooterPreview.Location = new Point(8, 15);
    this._labelFooterPreview.Name = "_labelFooterPreview";
    this._labelFooterPreview.Size = new Size(61, 13);
    this._labelFooterPreview.TabIndex = 12;
    this._labelFooterPreview.Text = "Просмотр:";
    this._tabScheme.BackColor = SystemColors.Control;
    this._tabScheme.Controls.Add((Control) this._buttonSchemeDelete);
    this._tabScheme.Controls.Add((Control) this._buttonSchemeRename);
    this._tabScheme.Controls.Add((Control) this._buttonSchemeApply);
    this._tabScheme.Controls.Add((Control) this._buttonSchemeSave);
    this._tabScheme.Controls.Add((Control) this._listBoxSchemes);
    this._tabScheme.Controls.Add((Control) this._labelSchemes);
    this._tabScheme.Location = new Point(4, 24);
    this._tabScheme.Name = "_tabScheme";
    this._tabScheme.Padding = new Padding(3);
    this._tabScheme.Size = new Size(505, 334);
    this._tabScheme.TabIndex = 4;
    this._tabScheme.Text = "Схемы";
    this._buttonSchemeDelete.Enabled = false;
    this._buttonSchemeDelete.Location = new Point(284, 123);
    this._buttonSchemeDelete.Name = "_buttonSchemeDelete";
    this._buttonSchemeDelete.Size = new Size(146, 23);
    this._buttonSchemeDelete.TabIndex = 15;
    this._buttonSchemeDelete.Text = "Удалить";
    this._buttonSchemeDelete.UseVisualStyleBackColor = true;
    this._buttonSchemeDelete.Click += new EventHandler(this._buttonSchemeDelete_Click);
    this._buttonSchemeRename.Enabled = false;
    this._buttonSchemeRename.Location = new Point(284, 94);
    this._buttonSchemeRename.Name = "_buttonSchemeRename";
    this._buttonSchemeRename.Size = new Size(146, 23);
    this._buttonSchemeRename.TabIndex = 15;
    this._buttonSchemeRename.Text = "Переименовать";
    this._buttonSchemeRename.UseVisualStyleBackColor = true;
    this._buttonSchemeRename.Click += new EventHandler(this._buttonSchemeRename_Click);
    this._buttonSchemeApply.Enabled = false;
    this._buttonSchemeApply.Location = new Point(284, 65);
    this._buttonSchemeApply.Name = "_buttonSchemeApply";
    this._buttonSchemeApply.Size = new Size(146, 23);
    this._buttonSchemeApply.TabIndex = 15;
    this._buttonSchemeApply.Text = "Применить";
    this._buttonSchemeApply.UseVisualStyleBackColor = true;
    this._buttonSchemeApply.Click += new EventHandler(this._buttonSchemeApply_Click);
    this._buttonSchemeSave.Location = new Point(284, 36);
    this._buttonSchemeSave.Name = "_buttonSchemeSave";
    this._buttonSchemeSave.Size = new Size(146, 23);
    this._buttonSchemeSave.TabIndex = 15;
    this._buttonSchemeSave.Text = "Сохранить как новую...";
    this._buttonSchemeSave.UseVisualStyleBackColor = true;
    this._buttonSchemeSave.Click += new EventHandler(this._buttonSchemeSave_Click);
    this._listBoxSchemes.FormattingEnabled = true;
    this._listBoxSchemes.Location = new Point(13, 36);
    this._listBoxSchemes.Name = "_listBoxSchemes";
    this._listBoxSchemes.Size = new Size(264, 238);
    this._listBoxSchemes.TabIndex = 14;
    this._listBoxSchemes.SelectedIndexChanged += new EventHandler(this._listBoxSchemes_SelectedIndexChanged);
    this._listBoxSchemes.DoubleClick += new EventHandler(this._listBoxSchemes_DoubleClick);
    this._labelSchemes.AutoSize = true;
    this._labelSchemes.Location = new Point(8, 15);
    this._labelSchemes.Name = "_labelSchemes";
    this._labelSchemes.Size = new Size(94, 13);
    this._labelSchemes.TabIndex = 13;
    this._labelSchemes.Text = "Схемы настроек:";
    this._tabView.BackColor = SystemColors.Control;
    this._tabView.Controls.Add((Control) this._labelPrintSelectedColumns);
    this._tabView.Controls.Add((Control) this._editPrintSelectedColumnsCount);
    this._tabView.Controls.Add((Control) this._checkBoxPrintSelectedColumns);
    this._tabView.Controls.Add((Control) this._checkBoxPrintAllColumns);
    this._tabView.Location = new Point(4, 24);
    this._tabView.Name = "_tabView";
    this._tabView.Padding = new Padding(3);
    this._tabView.Size = new Size(505, 334);
    this._tabView.TabIndex = 5;
    this._tabView.Text = "Вид";
    this._labelPrintSelectedColumns.AutoSize = true;
    this._labelPrintSelectedColumns.ForeColor = SystemColors.GrayText;
    this._labelPrintSelectedColumns.Location = new Point(335, 46);
    this._labelPrintSelectedColumns.Name = "_labelPrintSelectedColumns";
    this._labelPrintSelectedColumns.Size = new Size(100, 13);
    this._labelPrintSelectedColumns.TabIndex = 2;
    this._labelPrintSelectedColumns.Text = "на всех страницах";
    this._editPrintSelectedColumnsCount.Enabled = false;
    this._editPrintSelectedColumnsCount.Location = new Point(269, 44);
    this._editPrintSelectedColumnsCount.Maximum = new Decimal(new int[4]
    {
      7,
      0,
      0,
      0
    });
    this._editPrintSelectedColumnsCount.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._editPrintSelectedColumnsCount.Name = "_editPrintSelectedColumnsCount";
    this._editPrintSelectedColumnsCount.Size = new Size(61, 20);
    this._editPrintSelectedColumnsCount.TabIndex = 2;
    this._editPrintSelectedColumnsCount.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._checkBoxPrintSelectedColumns.AutoSize = true;
    this._checkBoxPrintSelectedColumns.Location = new Point(11, 45);
    this._checkBoxPrintSelectedColumns.Name = "_checkBoxPrintSelectedColumns";
    this._checkBoxPrintSelectedColumns.Size = new Size(254, 17);
    this._checkBoxPrintSelectedColumns.TabIndex = 1;
    this._checkBoxPrintSelectedColumns.Text = "Печатать указанное число первых столбцов:";
    this._checkBoxPrintSelectedColumns.UseVisualStyleBackColor = true;
    this._checkBoxPrintSelectedColumns.CheckedChanged += new EventHandler(this._checkBoxPrintSelectedColumns_CheckedChanged);
    this._checkBoxPrintAllColumns.AutoSize = true;
    this._checkBoxPrintAllColumns.Location = new Point(11, 19);
    this._checkBoxPrintAllColumns.Name = "_checkBoxPrintAllColumns";
    this._checkBoxPrintAllColumns.Size = new Size(172, 17);
    this._checkBoxPrintAllColumns.TabIndex = 0;
    this._checkBoxPrintAllColumns.Text = "Печатать &все столбцы листа";
    this._checkBoxPrintAllColumns.UseVisualStyleBackColor = true;
    this._panelTools.Controls.Add((Control) this._btnPrint);
    this._panelTools.Controls.Add((Control) this._btnPrinterProps);
    this._panelTools.Location = new Point(5, 329);
    this._panelTools.Name = "_panelTools";
    this._panelTools.Size = new Size(177, 28);
    this._panelTools.TabIndex = 2;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(513, 362);
    this.Controls.Add((Control) this._panelTools);
    this.Controls.Add((Control) this._tabs);
    this.HelpButton = true;
    this.Name = nameof (PrintSetupForm);
    this.Text = "Печать диаграммы Ганта";
    this.FormClosing += new FormClosingEventHandler(this.PrintSetupForm_FormClosing);
    this.FormClosed += new FormClosedEventHandler(this.PrintSetupForm_FormClosed);
    this.Controls.SetChildIndex((Control) this._tabs, 0);
    this.Controls.SetChildIndex((Control) this._panelTools, 0);
    this.Controls.SetChildIndex((Control) this._pnlDialogButtons, 0);
    this.Controls.SetChildIndex((Control) this._bevelDialogButtons, 0);
    this._pnlDialogButtons.ResumeLayout(false);
    this._panelBtns.ResumeLayout(false);
    this._tabs.ResumeLayout(false);
    this._tabPage.ResumeLayout(false);
    this._tabPage.PerformLayout();
    this._editFirstPageNum.EndInit();
    this._editNumScalePagesHeight.EndInit();
    this._editNumScalePagesWidth.EndInit();
    this._editScale.EndInit();
    this._panel1.ResumeLayout(false);
    this._panel1.PerformLayout();
    ((ISupportInitialize) this._pictureBoxLandscape).EndInit();
    ((ISupportInitialize) this._pictureBoxPortrait).EndInit();
    this._tabMargins.ResumeLayout(false);
    this._tabMargins.PerformLayout();
    ((ISupportInitialize) this._pictureLandscapeMargins).EndInit();
    ((ISupportInitialize) this._picturePortraitMargins).EndInit();
    this._editMarginLeft.EndInit();
    this._editMarginBottom.EndInit();
    this._editMarginRight.EndInit();
    this._editMarginTop.EndInit();
    this._tabHeader.ResumeLayout(false);
    this._tabHeader.PerformLayout();
    ((ISupportInitialize) this._pictureHeader).EndInit();
    this._tabsHeader.ResumeLayout(false);
    this._tabHeaderLeft.ResumeLayout(false);
    this._tabHeaderCenter.ResumeLayout(false);
    this._tabHeaderRight.ResumeLayout(false);
    this._tabFooter.ResumeLayout(false);
    this._tabFooter.PerformLayout();
    ((ISupportInitialize) this._pictureFooter).EndInit();
    this._tabsFooter.ResumeLayout(false);
    this._tabFooterLeft.ResumeLayout(false);
    this._tabFooterCenter.ResumeLayout(false);
    this._tabFooterRight.ResumeLayout(false);
    this._tabScheme.ResumeLayout(false);
    this._tabScheme.PerformLayout();
    this._tabView.ResumeLayout(false);
    this._tabView.PerformLayout();
    this._editPrintSelectedColumnsCount.EndInit();
    this._panelTools.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private class Scheme
  {
    public readonly long VersionID;
    [NotNull]
    public string Name;

    public Scheme(long versionID, [NotNull] string name)
    {
      this.VersionID = versionID;
      this.Name = name;
    }

    public override string ToString() => this.Name;

    public override bool Equals(object obj)
    {
      return obj is PrintSetupForm.Scheme scheme && this.VersionID == scheme.VersionID;
    }

    public override int GetHashCode() => this.VersionID.GetHashCode();
  }
}
