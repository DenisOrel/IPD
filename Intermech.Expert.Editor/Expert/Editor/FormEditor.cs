// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.FormEditor
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using DevExpress.IM.LookAndFeel;
using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.SelectionService;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.SelectionView;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Expert system formula editor!</summary>
public class FormEditor : Form
{
  private ImageList IL;
  private GroupBox errorGB;
  private Label errorLbl;
  private Button button13;
  private ToolTip toolTipFE;
  private SimpleButton buttonFirst;
  private SimpleButton buttonBackspace;
  private SimpleButton buttonTrash;
  private SimpleButton buttonDeshifr;
  private SimpleButton buttonRef;
  private SimpleButton buttonEdit;
  private SimpleButton buttonNew;
  private SimpleButton buttonLast;
  private SimpleButton buttonNext;
  private SimpleButton buttonPrev;
  private IContainer components;
  private Panel panel1;
  private GroupBox insertGB;
  private Button buttonCancel;
  private Button buttonOK;
  private Panel panelButtons;
  private bool ErrorShow;
  private PopupContainerEdit popupFunc;
  private PopupContainerControl popupContainerControl1;
  private ColumnHeader columnHeader1;
  private ColumnHeader columnHeader2;
  private ListView listFunctions;
  private ColumnHeader columnHeader3;
  private SimpleButton button16;
  private SimpleButton button17;
  private SimpleButton button18;
  private SimpleButton button19;
  private SimpleButton button20;
  private SimpleButton button21;
  private SimpleButton button28;
  private SimpleButton button29;
  private SimpleButton button31;
  private SimpleButton button30;
  private SimpleButton buttonOr;
  private SimpleButton buttonAnd;
  private SimpleButton buttonPi;
  private SimpleButton button32;
  private SimpleButton button26;
  private SimpleButton button25;
  private SimpleButton button24;
  private SimpleButton button23;
  private SimpleButton button22;
  private ImageList buttIL;
  private Size MinSize = new Size(620, 500);
  private int formHeight = 500;
  private TempFormula tf;
  private int curTokIndex = -1;
  private int curExtraFunc = -1;
  private DataTable attrData;
  private DataTable objTypeData;
  public bool sortByShort = true;
  private bool fChanged;
  private bool lockChanged;
  internal System.IServiceProvider _serviceProvider;
  internal SelFormResult selAttr;
  internal SelFormResult selObjType;
  internal FieldTypes attrType;
  internal bool multi;
  internal SelForm sForm;
  internal CalcTestForm calcForm;
  internal Intermech.Expert.Editor.ShowPostfix postForm;
  internal int[] listFunctionsTags = new int[74]
  {
    50,
    55,
    56,
    57,
    1,
    2,
    3,
    4,
    5,
    6,
    7,
    8,
    9,
    101,
    102,
    103,
    104,
    105,
    106,
    107,
    108,
    109,
    110,
    111,
    112 /*0x70*/,
    113,
    114,
    115,
    116,
    117,
    118,
    133,
    119,
    51,
    52,
    53,
    54,
    120,
    121,
    (int) sbyte.MaxValue,
    129,
    130,
    131,
    132,
    134,
    135,
    136,
    137,
    138,
    139,
    140,
    141,
    142,
    143,
    144 /*0x90*/,
    145,
    146,
    147,
    148,
    149,
    152,
    153,
    154,
    155,
    156,
    157,
    158,
    161,
    162,
    163,
    165,
    167,
    168,
    169
  };
  public bool CanReturnEmpty;
  private SimpleButton buttonUMinus;
  private SimpleButton buttonNot;
  private SimpleButton buttonDiap;
  private SimpleButton buttonComma;
  private SimpleButton buttonSet;
  private RichTextBox memoForm;
  private System.Windows.Forms.ComboBox comboAttr;
  private Label hintLabel;
  private SimpleButton buttonPLUS;
  private Panel panelDate;
  private MonthCalendar monthCalendar1;
  private SimpleButton btnCompile;
  private SimpleButton btnRun;
  private CheckBox checkObjType;
  private SimpleButton btnClearAttr;
  private Label AttrTypeLbl;
  private ButtonEdit textObjName;
  private ButtonEdit textAttName;
  private Label label1;
  private SimpleButton btnData;
  private TextBox editAll;
  private SimpleButton btnMeasure;
  private SimpleButton buttonDel;
  private bool LockEditEnable;
  private SimpleButton pasteBtn;
  private SimpleButton copyBtn;
  private Label label2;
  private ImageList newIL;
  private Panel panel2;
  private Panel panel8;
  private Label label3;
  private Label label4;
  private Button btnImport;
  private Button btnExport;
  private OpenFileDialog ofd;
  private SaveFileDialog sfd;
  private CheckBox cbAllowUnknown;
  private ImageList IL_50;
  private SimpleButton btnArrayEnd;
  private SimpleButton btnArrayStart;
  private SimpleButton btnCarriageReturn;
  private INamedImageList iNIL;
  internal FormEditor.Compare cb_Compare;
  private TempFormula saveTF;
  private int saveCurTokIndex = -1;

  public event FormEditor.SelTypeEventHandler SelObjType;

  public event FormEditor.SelTypeEventHandler SelAttrType;

  public bool Changed => this.fChanged;

  public FormEditor()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1315);
    ((Control) this.insertGB).MouseDown += new MouseEventHandler(this.errorLbl_MouseDown);
    ((Control) this.errorGB).MouseDown += new MouseEventHandler(this.errorLbl_MouseDown);
    this.SetMinSize();
    this.UpdateErrorVisible();
    this.UpdateSizes();
    this.sForm = new SelForm();
    this.calcForm = new CalcTestForm();
    this.postForm = new Intermech.Expert.Editor.ShowPostfix();
    this.textObjName.Text = "";
    this.textAttName.Text = "";
    this.AttrTypeLbl.Text = "";
    this.checkObjType.Checked = true;
    this.cb_Compare = new FormEditor.Compare(this.ArgComboCompare);
  }

  /// <summary>Clean up any resources being used.</summary>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormEditor));
    ListViewItem listViewItem1 = new ListViewItem(new string[3]
    {
      "СТР(A)",
      "С",
      "Строковое представление атрибута"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem2 = new ListViewItem(new string[3]
    {
      "НЕОБ(А)",
      "С",
      "Необязательное значение атрибута"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem3 = new ListViewItem(new string[3]
    {
      "НЕОБ0(А)",
      "П",
      "Необязательное значение (0 по умолчанию)"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem4 = new ListViewItem(new string[3]
    {
      "НЕОБ1(А)",
      "П",
      "Необязательное значение (1 по умолчанию)"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem5 = new ListViewItem(new ListViewItem.ListViewSubItem[3]
    {
      new ListViewItem.ListViewSubItem((ListViewItem) null, "sin(П)", SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold)),
      new ListViewItem.ListViewSubItem((ListViewItem) null, "П", SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f)),
      new ListViewItem.ListViewSubItem((ListViewItem) null, "Синус")
    }, -1);
    ListViewItem listViewItem6 = new ListViewItem(new string[3]
    {
      "cos(П)",
      "П",
      "Косинус"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem7 = new ListViewItem(new string[3]
    {
      "tg(П)",
      "П",
      "Тангенс"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem8 = new ListViewItem(new string[3]
    {
      "ln(П)",
      "П",
      "Натуральный логарифм"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem9 = new ListViewItem(new string[3]
    {
      "lg(П)",
      "П",
      "Десятичный логарифм"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem10 = new ListViewItem(new string[3]
    {
      "atg(П)",
      "П",
      "Арктангенс"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem11 = new ListViewItem(new string[3]
    {
      "exp(П)",
      "П",
      "Экспонента"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem12 = new ListViewItem(new string[3]
    {
      "sqrt(П)",
      "П",
      "Квадратный корень"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem13 = new ListViewItem(new string[3]
    {
      "abs(П)",
      "П",
      "Модуль"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem14 = new ListViewItem(new ListViewItem.ListViewSubItem[3]
    {
      new ListViewItem.ListViewSubItem((ListViewItem) null, "def(С)", SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold)),
      new ListViewItem.ListViewSubItem((ListViewItem) null, "Л", SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204)),
      new ListViewItem.ListViewSubItem((ListViewItem) null, "Определен ли атрибут?", SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204))
    }, -1);
    ListViewItem listViewItem15 = new ListViewItem(new string[3]
    {
      "nom(С)",
      "П",
      "Номинал размера"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem16 = new ListViewItem(new string[3]
    {
      "kv(С)",
      "Ц",
      "Квалитет точности размера"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem17 = new ListViewItem(new string[3]
    {
      "hi(С)",
      "П",
      "Верхнее отклонение размера"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem18 = new ListViewItem(new string[3]
    {
      "lo(С)",
      "П",
      "Нижнее отклонение размера"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem19 = new ListViewItem(new string[3]
    {
      "kt(С)",
      "С",
      "Код класса точности размера"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem20 = new ListViewItem(new string[3]
    {
      "st(С)",
      "П",
      "Шаг резьбы размера"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem21 = new ListViewItem(new string[3]
    {
      "ctn(Ц)",
      "С",
      "Имя объекта по коду"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem22 = new ListViewItem(new string[3]
    {
      "rnd(П)",
      "Ц",
      "К ближайшему целому"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem23 = new ListViewItem(new string[3]
    {
      "rnde(П,Ц)",
      "П",
      "Округление до точности"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem24 = new ListViewItem(new string[3]
    {
      "rndg(П,Ц)",
      "П",
      "Округление до значащих"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem25 = new ListViewItem(new string[3]
    {
      "int(П)",
      "Ц",
      "Целая часть"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem26 = new ListViewItem(new string[3]
    {
      "frac(П)",
      "П",
      "Дробная часть"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem27 = new ListViewItem(new string[3]
    {
      "has(С,С)",
      "Л",
      "Содержит ли строка подстроку?"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem28 = new ListViewItem(new string[3]
    {
      "begs(С,С)",
      "Л",
      "Начинается ли строка с подстроки?"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem29 = new ListViewItem(new string[3]
    {
      "ends(С,С)",
      "Л",
      "Заканчивается ли строка подстрокой?"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem30 = new ListViewItem(new string[3]
    {
      "upp(С)",
      "С",
      "К верхнему регистру"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem31 = new ListViewItem(new string[3]
    {
      "low(С)",
      "С",
      "К нижнему регистру"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem32 = new ListViewItem(new string[3]
    {
      "len(C)",
      "Ц",
      "Длина строки"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem33 = new ListViewItem(new ListViewItem.ListViewSubItem[3]
    {
      new ListViewItem.ListViewSubItem((ListViewItem) null, "СЕЙЧАС()", SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold)),
      new ListViewItem.ListViewSubItem((ListViewItem) null, "Д", SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f)),
      new ListViewItem.ListViewSubItem((ListViewItem) null, "Текущее время и дата")
    }, -1);
    ListViewItem listViewItem34 = new ListViewItem(new ListViewItem.ListViewSubItem[3]
    {
      new ListViewItem.ListViewSubItem((ListViewItem) null, "Дочерн(О)", SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold)),
      new ListViewItem.ListViewSubItem((ListViewItem) null, "Л", SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f)),
      new ListViewItem.ListViewSubItem((ListViewItem) null, "Есть ли дочерний объект типа О", SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f))
    }, -1);
    ListViewItem listViewItem35 = new ListViewItem(new string[3]
    {
      "ДочСВ(О,В)",
      "Л",
      "Есть ли дочерний объект типа О по связи типа В"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem36 = new ListViewItem(new string[3]
    {
      "Родитл(О)",
      "Л",
      "Есть ли родительский объект типа О"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem37 = new ListViewItem(new string[3]
    {
      "РодСВ(О,В)",
      "Л",
      "Есть ли родит. объект типа О на связи типа В"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem38 = new ListViewItem(new string[3]
    {
      "ФЛАГ(Ц,Ц)",
      "Л",
      "Установлен ли флажок"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem39 = new ListViewItem(new string[3]
    {
      "ФЛАГ_А(Ц,А)",
      "Л",
      "Установлен ли флаг N в атрибуте А"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem40 = new ListViewItem(new ListViewItem.ListViewSubItem[3]
    {
      new ListViewItem.ListViewSubItem((ListViewItem) null, "ДАТА(Д)", SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold)),
      new ListViewItem.ListViewSubItem((ListViewItem) null, "С", SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f)),
      new ListViewItem.ListViewSubItem((ListViewItem) null, "Строковое представление даты")
    }, -1);
    ListViewItem listViewItem41 = new ListViewItem(new string[3]
    {
      "ЦЕЛОЕ(С)",
      "Ц",
      "Перевод строки в целое"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem42 = new ListViewItem(new string[3]
    {
      "ПЛАВ(С)",
      "П",
      "Перевод строки в число с плав. точкой"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem43 = new ListViewItem(new string[3]
    {
      "ПЛВ_ЕД(С)",
      "М",
      "Перевод строки в плав. т. с ед. измерения"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem44 = new ListViewItem(new string[3]
    {
      "Н_ИСП(Ц)",
      "Ц",
      "Номер исполнения"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem45 = new ListViewItem(new string[3]
    {
      "поз(С,С)",
      "Ц",
      "Позиция в строке подстроки"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem46 = new ListViewItem(new string[3]
    {
      "подс(С,Ц,Ц)",
      "С",
      "Выделить подстроку"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem47 = new ListViewItem(new string[3]
    {
      "ЗНАЧ(М)",
      "П",
      "Число без единицы измерения"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem48 = new ListViewItem(new string[3]
    {
      "ЕИЗМ(М)",
      "С",
      "Единица измерения"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem49 = new ListViewItem(new string[3]
    {
      "val2(С,С)",
      "С",
      "Строка с разделителем для ведомости"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem50 = new ListViewItem(new string[3]
    {
      "val3(С,С,С)",
      "C",
      "Строка с префиксом и постфиксом"
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem51 = new ListViewItem(new string[3]
    {
      "nosht(M)",
      "C",
      "Строковое описание без \"штук\""
    }, -1, SystemColors.WindowText, SystemColors.Window, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold));
    ListViewItem listViewItem52 = new ListViewItem(new string[3]
    {
      "ДОЧ(Ц)",
      "Л",
      "Текущий объект - дочерний для заданного?"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem53 = new ListViewItem(new string[3]
    {
      "РОД(Ц)",
      "Л",
      "Текущий объект - родитель заданного?"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem54 = new ListViewItem(new string[3]
    {
      "К_ЕД(М,С)",
      "М",
      "Привести к единице измерения"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem55 = new ListViewItem(new string[3]
    {
      "РАСКРЫТ(Ц)",
      "Л",
      "Структура объекта раскрыта?"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem56 = new ListViewItem(new string[3]
    {
      "НЕРАЗ(С)",
      "С",
      "Строка с неразрывными пробелами"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem57 = new ListViewItem(new string[3]
    {
      "ОДОЧ(Ц,Ц)",
      "Л",
      "Первый объект - дочерний для второго?"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem58 = new ListViewItem(new string[3]
    {
      "ОРОД(Ц,Ц)",
      "Л",
      "Первый объект - родительский для второго?"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem59 = new ListViewItem(new string[3]
    {
      "Б_Мен(П,Н)",
      "П",
      "Ближайшее меньшее из набора"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem60 = new ListViewItem(new string[3]
    {
      "Б_Бол(П,Н)",
      "П",
      "Ближайшее большее из набора"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem61 = new ListViewItem(new string[3]
    {
      "СПИС(С,А)",
      "С",
      "Список строк через разделитель"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem62 = new ListViewItem(new string[3]
    {
      "СПИС+(С,А,А)",
      "С",
      "Список ссылок через разделитель"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem63 = new ListViewItem(new string[3]
    {
      "TDiff(Д,Д)",
      "Ц",
      "Разница между временем"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem64 = new ListViewItem(new string[3]
    {
      "ЧРазд(Н,С)",
      "С",
      "Собрать строку через разделитель"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem65 = new ListViewItem(new string[3]
    {
      "Класс(Ц,Ц)",
      "Л",
      "Классифицировать объект по папке"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem66 = new ListViewItem(new string[3]
    {
      "ra(П,Н)",
      "П",
      "Функция ra2 с коэффициентом 0.7"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem67 = new ListViewItem(new string[3]
    {
      "ra2(П,Н,П)",
      "П",
      "Функция ra2"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem68 = new ListViewItem(new string[3]
    {
      "КЕИзм(М)",
      "Ц",
      "Код единицы измерения"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem69 = new ListViewItem(new string[3]
    {
      "ИМЯ_ТД(Ц)",
      "С",
      "Имя типа документа"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem70 = new ListViewItem(new string[3]
    {
      "Минус(П)",
      "C",
      "Строковое представление со словом \"минус\""
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem71 = new ListViewItem(new string[3]
    {
      "Формт(Н,С)",
      "С",
      "Форматирование набора"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem72 = new ListViewItem(new string[3]
    {
      "ЕИ_Коэф(М)",
      "П",
      "Коэффициент приведения к базовой единице"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem73 = new ListViewItem(new string[3]
    {
      "ЗамС(С, С, С)",
      "С",
      "Заменить подстроку в строке"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    ListViewItem listViewItem74 = new ListViewItem(new string[3]
    {
      "trim(C)",
      "C",
      "Убрать пробелы с краев"
    }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204));
    this.buttonDel = new SimpleButton();
    this.newIL = new ImageList(this.components);
    this.IL = new ImageList(this.components);
    this.editAll = new TextBox();
    this.memoForm = new RichTextBox();
    this.buttonFirst = new SimpleButton();
    this.buttonBackspace = new SimpleButton();
    this.buttonTrash = new SimpleButton();
    this.IL_50 = new ImageList(this.components);
    this.buttonDeshifr = new SimpleButton();
    this.buttonRef = new SimpleButton();
    this.buttonEdit = new SimpleButton();
    this.buttonNew = new SimpleButton();
    this.buttonLast = new SimpleButton();
    this.buttonNext = new SimpleButton();
    this.buttonPrev = new SimpleButton();
    this.toolTipFE = new ToolTip(this.components);
    this.button13 = new Button();
    this.btnRun = new SimpleButton();
    this.btnCompile = new SimpleButton();
    this.copyBtn = new SimpleButton();
    this.pasteBtn = new SimpleButton();
    this.buttonPLUS = new SimpleButton();
    this.btnData = new SimpleButton();
    this.btnMeasure = new SimpleButton();
    this.errorGB = new GroupBox();
    this.popupContainerControl1 = new PopupContainerControl();
    this.listFunctions = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader3 = new ColumnHeader();
    this.columnHeader2 = new ColumnHeader();
    this.errorLbl = new Label();
    this.panelDate = new Panel();
    this.monthCalendar1 = new MonthCalendar();
    this.panel1 = new Panel();
    this.panel8 = new Panel();
    this.btnCarriageReturn = new SimpleButton();
    this.btnArrayEnd = new SimpleButton();
    this.btnArrayStart = new SimpleButton();
    this.label4 = new Label();
    this.button31 = new SimpleButton();
    this.popupFunc = new PopupContainerEdit();
    this.buttonSet = new SimpleButton();
    this.button30 = new SimpleButton();
    this.buttonNot = new SimpleButton();
    this.button29 = new SimpleButton();
    this.button32 = new SimpleButton();
    this.button28 = new SimpleButton();
    this.buttonUMinus = new SimpleButton();
    this.buttonPi = new SimpleButton();
    this.buttonDiap = new SimpleButton();
    this.buttonOr = new SimpleButton();
    this.buttonComma = new SimpleButton();
    this.button16 = new SimpleButton();
    this.buttonAnd = new SimpleButton();
    this.button26 = new SimpleButton();
    this.button21 = new SimpleButton();
    this.button17 = new SimpleButton();
    this.button20 = new SimpleButton();
    this.button25 = new SimpleButton();
    this.button19 = new SimpleButton();
    this.button23 = new SimpleButton();
    this.button24 = new SimpleButton();
    this.button18 = new SimpleButton();
    this.button22 = new SimpleButton();
    this.insertGB = new GroupBox();
    this.cbAllowUnknown = new CheckBox();
    this.label3 = new Label();
    this.label2 = new Label();
    this.checkObjType = new CheckBox();
    this.btnClearAttr = new SimpleButton();
    this.AttrTypeLbl = new Label();
    this.textObjName = new ButtonEdit();
    this.textAttName = new ButtonEdit();
    this.label1 = new Label();
    this.comboAttr = new System.Windows.Forms.ComboBox();
    this.buttIL = new ImageList(this.components);
    this.panelButtons = new Panel();
    this.btnImport = new Button();
    this.btnExport = new Button();
    this.hintLabel = new Label();
    this.buttonCancel = new Button();
    this.buttonOK = new Button();
    this.panel2 = new Panel();
    this.ofd = new OpenFileDialog();
    this.sfd = new SaveFileDialog();
    this.errorGB.SuspendLayout();
    this.popupContainerControl1.SuspendLayout();
    this.panelDate.SuspendLayout();
    this.panel1.SuspendLayout();
    this.panel8.SuspendLayout();
    this.popupFunc.Properties.BeginInit();
    this.insertGB.SuspendLayout();
    this.textObjName.Properties.BeginInit();
    this.textAttName.Properties.BeginInit();
    this.panelButtons.SuspendLayout();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    this.buttonDel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonDel.ImageIndex = 13;
    this.buttonDel.ImageList = this.newIL;
    this.buttonDel.ImeMode = ImeMode.NoControl;
    this.buttonDel.Location = new Point(1026, 82);
    this.buttonDel.Name = "buttonDel";
    this.buttonDel.Size = new Size(43, 33);
    this.buttonDel.TabIndex = 4;
    this.buttonDel.TabStop = false;
    this.buttonDel.ToolTip = "Удалить текущий (Ctrl+Del)";
    this.buttonDel.Click += new EventHandler(this.buttonDel_Click);
    this.buttonDel.Enter += new EventHandler(this.memoForm_Enter);
    this.newIL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("newIL.ImageStream");
    this.newIL.TransparentColor = Color.Magenta;
    this.newIL.Images.SetKeyName(0, "(.bmp");
    this.newIL.Images.SetKeyName(1, ").bmp");
    this.newIL.Images.SetKeyName(2, "{.bmp");
    this.newIL.Images.SetKeyName(3, "}.bmp");
    this.newIL.Images.SetKeyName(4, "and.bmp");
    this.newIL.Images.SetKeyName(5, "attrib.bmp");
    this.newIL.Images.SetKeyName(6, "back.bmp");
    this.newIL.Images.SetKeyName(7, "colon.bmp");
    this.newIL.Images.SetKeyName(8, "comma.bmp");
    this.newIL.Images.SetKeyName(9, "copy.bmp");
    this.newIL.Images.SetKeyName(10, "date.bmp");
    this.newIL.Images.SetKeyName(11, "decoding.bmp");
    this.newIL.Images.SetKeyName(12, "del_left.bmp");
    this.newIL.Images.SetKeyName(13, "del_right.bmp");
    this.newIL.Images.SetKeyName(14, "delete.bmp");
    this.newIL.Images.SetKeyName(15, "divide.bmp");
    this.newIL.Images.SetKeyName(16 /*0x10*/, "e.bmp");
    this.newIL.Images.SetKeyName(17, "egual.bmp");
    this.newIL.Images.SetKeyName(18, "end.bmp");
    this.newIL.Images.SetKeyName(19, "forward.bmp");
    this.newIL.Images.SetKeyName(20, "guide.bmp");
    this.newIL.Images.SetKeyName(21, "home.bmp");
    this.newIL.Images.SetKeyName(22, "less.bmp");
    this.newIL.Images.SetKeyName(23, "less_egual.bmp");
    this.newIL.Images.SetKeyName(24, "m2.bmp");
    this.newIL.Images.SetKeyName(25, "min.bmp");
    this.newIL.Images.SetKeyName(26, "more.bmp");
    this.newIL.Images.SetKeyName(27, "more_egual.bmp");
    this.newIL.Images.SetKeyName(28, "multiply.bmp");
    this.newIL.Images.SetKeyName(29, "not.bmp");
    this.newIL.Images.SetKeyName(30, "not_egual.bmp");
    this.newIL.Images.SetKeyName(31 /*0x1F*/, "or.bmp");
    this.newIL.Images.SetKeyName(32 /*0x20*/, "paste.bmp");
    this.newIL.Images.SetKeyName(33, "pi.bmp");
    this.newIL.Images.SetKeyName(34, "plus.bmp");
    this.newIL.Images.SetKeyName(35, "power.bmp");
    this.newIL.Images.SetKeyName(36, "quest.bmp");
    this.newIL.Images.SetKeyName(37, "replace.bmp");
    this.newIL.Images.SetKeyName(38, "yes.bmp");
    this.newIL.Images.SetKeyName(39, "[.png");
    this.newIL.Images.SetKeyName(40, "].png");
    this.newIL.Images.SetKeyName(41, "CR.png");
    this.IL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("IL.ImageStream");
    this.IL.TransparentColor = Color.Magenta;
    this.IL.Images.SetKeyName(0, "");
    this.IL.Images.SetKeyName(1, "");
    this.IL.Images.SetKeyName(2, "");
    this.IL.Images.SetKeyName(3, "");
    this.IL.Images.SetKeyName(4, "");
    this.IL.Images.SetKeyName(5, "");
    this.IL.Images.SetKeyName(6, "");
    this.IL.Images.SetKeyName(7, "");
    this.IL.Images.SetKeyName(8, "");
    this.IL.Images.SetKeyName(9, "");
    this.IL.Images.SetKeyName(10, "");
    this.IL.Images.SetKeyName(11, "");
    this.IL.Images.SetKeyName(12, "");
    this.IL.Images.SetKeyName(13, "");
    this.IL.Images.SetKeyName(14, "");
    this.IL.Images.SetKeyName(15, "");
    this.IL.Images.SetKeyName(16 /*0x10*/, "");
    this.IL.Images.SetKeyName(17, "");
    this.IL.Images.SetKeyName(18, "SSS_13.bmp");
    this.IL.Images.SetKeyName(19, "calendar.bmp");
    this.IL.Images.SetKeyName(20, "measure.bmp");
    this.IL.Images.SetKeyName(21, "");
    this.editAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.editAll.Location = new Point(13, 85);
    this.editAll.Name = "editAll";
    this.editAll.Size = new Size(565, 26);
    this.editAll.TabIndex = 3;
    this.editAll.TextChanged += new EventHandler(this.editAll_TextChanged);
    this.memoForm.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.memoForm.BackColor = SystemColors.Window;
    this.memoForm.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
    this.memoForm.HideSelection = false;
    this.memoForm.Location = new Point(13, 12);
    this.memoForm.Name = "memoForm";
    this.memoForm.ReadOnly = true;
    this.memoForm.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
    this.memoForm.Size = new Size(1151, 60);
    this.memoForm.TabIndex = 2;
    this.memoForm.Text = "";
    this.memoForm.Enter += new EventHandler(this.memoForm_Enter);
    this.memoForm.MouseMove += new MouseEventHandler(this.memoForm_MouseMove);
    this.memoForm.MouseUp += new MouseEventHandler(this.memoForm_MouseUp);
    this.buttonFirst.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonFirst.ImageIndex = 21;
    this.buttonFirst.ImageList = this.newIL;
    this.buttonFirst.ImeMode = ImeMode.NoControl;
    this.buttonFirst.Location = new Point(815, 82);
    this.buttonFirst.Name = "buttonFirst";
    this.buttonFirst.Size = new Size(43, 33);
    this.buttonFirst.TabIndex = 0;
    this.buttonFirst.TabStop = false;
    this.buttonFirst.ToolTip = "На первый элемент (Ctrl+Home)";
    this.buttonFirst.Click += new EventHandler(this.buttonFirst_Click);
    this.buttonFirst.Enter += new EventHandler(this.memoForm_Enter);
    this.buttonBackspace.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonBackspace.ImageIndex = 12;
    this.buttonBackspace.ImageList = this.newIL;
    this.buttonBackspace.ImeMode = ImeMode.NoControl;
    this.buttonBackspace.Location = new Point(1072, 82);
    this.buttonBackspace.Name = "buttonBackspace";
    this.buttonBackspace.Size = new Size(44, 33);
    this.buttonBackspace.TabIndex = 0;
    this.buttonBackspace.TabStop = false;
    this.buttonBackspace.ToolTip = "Удалить предыдущий (Ctrl+Backspace)";
    this.buttonBackspace.Click += new EventHandler(this.buttonBackspace_Click);
    this.buttonBackspace.Enter += new EventHandler(this.memoForm_Enter);
    this.buttonTrash.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonTrash.ImageIndex = 14;
    this.buttonTrash.ImageList = this.IL_50;
    this.buttonTrash.ImeMode = ImeMode.NoControl;
    this.buttonTrash.Location = new Point(1119, 82);
    this.buttonTrash.Name = "buttonTrash";
    this.buttonTrash.Size = new Size(43, 33);
    this.buttonTrash.TabIndex = 0;
    this.buttonTrash.TabStop = false;
    this.buttonTrash.ToolTip = "Очистить всю формулу";
    this.buttonTrash.Click += new EventHandler(this.buttonTrash_Click);
    this.buttonTrash.Enter += new EventHandler(this.memoForm_Enter);
    this.IL_50.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("IL_50.ImageStream");
    this.IL_50.TransparentColor = Color.Magenta;
    this.IL_50.Images.SetKeyName(0, "(.bmp");
    this.IL_50.Images.SetKeyName(1, ").bmp");
    this.IL_50.Images.SetKeyName(2, "{.bmp");
    this.IL_50.Images.SetKeyName(3, "}.bmp");
    this.IL_50.Images.SetKeyName(4, "and.bmp");
    this.IL_50.Images.SetKeyName(5, "attrib.bmp");
    this.IL_50.Images.SetKeyName(6, "back.bmp");
    this.IL_50.Images.SetKeyName(7, "colon.bmp");
    this.IL_50.Images.SetKeyName(8, "comma.bmp");
    this.IL_50.Images.SetKeyName(9, "копировать.png");
    this.IL_50.Images.SetKeyName(10, "date.bmp");
    this.IL_50.Images.SetKeyName(11, "decoding.bmp");
    this.IL_50.Images.SetKeyName(12, "del_left.bmp");
    this.IL_50.Images.SetKeyName(13, "del_right.bmp");
    this.IL_50.Images.SetKeyName(14, "удалить.png");
    this.IL_50.Images.SetKeyName(15, "divide.bmp");
    this.IL_50.Images.SetKeyName(16 /*0x10*/, "e.bmp");
    this.IL_50.Images.SetKeyName(17, "egual.bmp");
    this.IL_50.Images.SetKeyName(18, "end.bmp");
    this.IL_50.Images.SetKeyName(19, "forward.bmp");
    this.IL_50.Images.SetKeyName(20, "guide.bmp");
    this.IL_50.Images.SetKeyName(21, "home.bmp");
    this.IL_50.Images.SetKeyName(22, "less.bmp");
    this.IL_50.Images.SetKeyName(23, "less_egual.bmp");
    this.IL_50.Images.SetKeyName(24, "m2.bmp");
    this.IL_50.Images.SetKeyName(25, "min.bmp");
    this.IL_50.Images.SetKeyName(26, "more.bmp");
    this.IL_50.Images.SetKeyName(27, "more_egual.bmp");
    this.IL_50.Images.SetKeyName(28, "multiply.bmp");
    this.IL_50.Images.SetKeyName(29, "not.bmp");
    this.IL_50.Images.SetKeyName(30, "not_egual.bmp");
    this.IL_50.Images.SetKeyName(31 /*0x1F*/, "or.bmp");
    this.IL_50.Images.SetKeyName(32 /*0x20*/, "вставить.png");
    this.IL_50.Images.SetKeyName(33, "pi.bmp");
    this.IL_50.Images.SetKeyName(34, "plus.bmp");
    this.IL_50.Images.SetKeyName(35, "power.bmp");
    this.IL_50.Images.SetKeyName(36, "quest.bmp");
    this.IL_50.Images.SetKeyName(37, "replace.bmp");
    this.IL_50.Images.SetKeyName(38, "ok_19х15.bmp");
    this.buttonDeshifr.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonDeshifr.ImageIndex = 11;
    this.buttonDeshifr.ImageList = this.newIL;
    this.buttonDeshifr.ImeMode = ImeMode.NoControl;
    this.buttonDeshifr.Location = new Point(746, 82);
    this.buttonDeshifr.Name = "buttonDeshifr";
    this.buttonDeshifr.Size = new Size(43, 33);
    this.buttonDeshifr.TabIndex = 0;
    this.buttonDeshifr.TabStop = false;
    this.buttonDeshifr.ToolTip = "Расшифровка (F3)";
    this.buttonDeshifr.Click += new EventHandler(this.buttonDeshifr_Click);
    this.buttonDeshifr.Enter += new EventHandler(this.memoForm_Enter);
    this.buttonRef.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonRef.ImageIndex = 20;
    this.buttonRef.ImageList = this.newIL;
    this.buttonRef.ImeMode = ImeMode.NoControl;
    this.buttonRef.Location = new Point(700, 82);
    this.buttonRef.Name = "buttonRef";
    this.buttonRef.Size = new Size(43, 33);
    this.buttonRef.TabIndex = 0;
    this.buttonRef.TabStop = false;
    this.buttonRef.ToolTip = "Справочник";
    this.buttonRef.Click += new EventHandler(this.buttonRef_Click);
    this.buttonRef.Enter += new EventHandler(this.memoForm_Enter);
    this.buttonEdit.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonEdit.ImageIndex = 37;
    this.buttonEdit.ImageList = this.newIL;
    this.buttonEdit.ImeMode = ImeMode.NoControl;
    this.buttonEdit.Location = new Point(631, 82);
    this.buttonEdit.Name = "buttonEdit";
    this.buttonEdit.Size = new Size(43, 33);
    this.buttonEdit.TabIndex = 0;
    this.buttonEdit.TabStop = false;
    this.buttonEdit.ToolTip = "Изменить значение (Ctrl+Enter)";
    this.buttonEdit.Click += new EventHandler(this.buttonEdit_Click);
    this.buttonEdit.Enter += new EventHandler(this.memoForm_Enter);
    this.buttonNew.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonNew.ImageIndex = 38;
    this.buttonNew.ImageList = this.IL_50;
    this.buttonNew.ImeMode = ImeMode.NoControl;
    this.buttonNew.Location = new Point(584, 82);
    this.buttonNew.Name = "buttonNew";
    this.buttonNew.Size = new Size(44, 33);
    this.buttonNew.TabIndex = 0;
    this.buttonNew.TabStop = false;
    this.buttonNew.ToolTip = "Внести значение";
    this.buttonNew.Click += new EventHandler(this.buttonNew_Click);
    this.buttonNew.Enter += new EventHandler(this.memoForm_Enter);
    this.buttonLast.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonLast.ImageIndex = 18;
    this.buttonLast.ImageList = this.newIL;
    this.buttonLast.ImeMode = ImeMode.NoControl;
    this.buttonLast.Location = new Point(954, 82);
    this.buttonLast.Name = "buttonLast";
    this.buttonLast.Size = new Size(43, 33);
    this.buttonLast.TabIndex = 0;
    this.buttonLast.TabStop = false;
    this.buttonLast.ToolTip = "На последний элемент (Ctrl+End)";
    this.buttonLast.Click += new EventHandler(this.buttonLast_Click);
    this.buttonLast.Enter += new EventHandler(this.memoForm_Enter);
    this.buttonNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonNext.ImageIndex = 19;
    this.buttonNext.ImageList = this.newIL;
    this.buttonNext.ImeMode = ImeMode.NoControl;
    this.buttonNext.Location = new Point(908, 82);
    this.buttonNext.Name = "buttonNext";
    this.buttonNext.Size = new Size(43, 33);
    this.buttonNext.TabIndex = 0;
    this.buttonNext.TabStop = false;
    this.buttonNext.ToolTip = "На элемент вправо (Ctrl+Right)";
    this.buttonNext.Click += new EventHandler(this.buttonNext_Click);
    this.buttonNext.Enter += new EventHandler(this.memoForm_Enter);
    this.buttonPrev.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonPrev.ImageIndex = 6;
    this.buttonPrev.ImageList = this.newIL;
    this.buttonPrev.ImeMode = ImeMode.NoControl;
    this.buttonPrev.Location = new Point(861, 82);
    this.buttonPrev.Name = "buttonPrev";
    this.buttonPrev.Size = new Size(43, 33);
    this.buttonPrev.TabIndex = 0;
    this.buttonPrev.TabStop = false;
    this.buttonPrev.ToolTip = "На элемент влево (Ctrl+Left)";
    this.buttonPrev.Click += new EventHandler(this.buttonPrev_Click);
    this.buttonPrev.Enter += new EventHandler(this.memoForm_Enter);
    this.toolTipFE.AutomaticDelay = 400;
    this.button13.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.button13.FlatStyle = FlatStyle.Popup;
    this.button13.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
    this.button13.ForeColor = SystemColors.ActiveCaption;
    this.button13.ImeMode = ImeMode.NoControl;
    this.button13.Location = new Point(1149, 10);
    this.button13.Name = "button13";
    this.button13.Size = new Size(24, 22);
    this.button13.TabIndex = 1;
    this.button13.Text = "X";
    this.toolTipFE.SetToolTip((Control) this.button13, "Закрыть панель ошибки");
    this.button13.Click += new EventHandler(this.button13_Click);
    this.btnRun.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnRun.ImageIndex = 17;
    this.btnRun.ImageList = this.IL;
    this.btnRun.ImeMode = ImeMode.NoControl;
    this.btnRun.Location = new Point(712, 4);
    this.btnRun.Name = "btnRun";
    this.btnRun.Size = new Size(44, 40);
    this.btnRun.TabIndex = 17;
    this.btnRun.TabStop = false;
    this.btnRun.ToolTip = "Тест расчёта";
    this.toolTipFE.SetToolTip((Control) this.btnRun, "Тест расчёта");
    this.btnRun.Visible = false;
    this.btnRun.Click += new EventHandler(this.btnRun_Click);
    this.btnCompile.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCompile.Enabled = false;
    this.btnCompile.ImageIndex = 21;
    this.btnCompile.ImageList = this.IL;
    this.btnCompile.ImeMode = ImeMode.NoControl;
    this.btnCompile.Location = new Point(660, 4);
    this.btnCompile.Name = "btnCompile";
    this.btnCompile.Size = new Size(43, 40);
    this.btnCompile.TabIndex = 16 /*0x10*/;
    this.btnCompile.TabStop = false;
    this.btnCompile.ToolTip = "Тест компиляции";
    this.toolTipFE.SetToolTip((Control) this.btnCompile, "Тест компиляции");
    this.btnCompile.Visible = false;
    this.btnCompile.Click += new EventHandler(this.btnCompile_Click);
    this.copyBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.copyBtn.ImageIndex = 9;
    this.copyBtn.ImageList = this.IL_50;
    this.copyBtn.ImeMode = ImeMode.NoControl;
    this.copyBtn.Location = new Point(6, 4);
    this.copyBtn.Name = "copyBtn";
    this.copyBtn.Size = new Size(44, 40);
    this.copyBtn.TabIndex = 20;
    this.copyBtn.TabStop = false;
    this.toolTipFE.SetToolTip((Control) this.copyBtn, "Копировать формулу в буфер");
    this.copyBtn.Click += new EventHandler(this.btnCopy_Click);
    this.pasteBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.pasteBtn.ImageIndex = 32 /*0x20*/;
    this.pasteBtn.ImageList = this.IL_50;
    this.pasteBtn.ImeMode = ImeMode.NoControl;
    this.pasteBtn.Location = new Point(56, 4);
    this.pasteBtn.Name = "pasteBtn";
    this.pasteBtn.Size = new Size(43, 40);
    this.pasteBtn.TabIndex = 21;
    this.pasteBtn.TabStop = false;
    this.toolTipFE.SetToolTip((Control) this.pasteBtn, "Вставить формулу из буфера");
    this.pasteBtn.Click += new EventHandler(this.btnPaste_Click);
    this.buttonPLUS.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonPLUS.ImageIndex = 38;
    this.buttonPLUS.ImageList = this.IL_50;
    this.buttonPLUS.ImeMode = ImeMode.NoControl;
    this.buttonPLUS.Location = new Point(586, 111);
    this.buttonPLUS.Name = "buttonPLUS";
    this.buttonPLUS.Size = new Size(43, 34);
    this.buttonPLUS.TabIndex = 8;
    this.buttonPLUS.TabStop = false;
    this.buttonPLUS.ToolTip = "Вставить атрибут";
    this.buttonPLUS.Click += new EventHandler(this.btnInsertAttr_Click);
    this.buttonPLUS.Enter += new EventHandler(this.memoForm_Enter);
    this.btnData.ImageIndex = 10;
    this.btnData.ImageList = this.newIL;
    this.btnData.Location = new Point(10, 82);
    this.btnData.Name = "btnData";
    this.btnData.Size = new Size(43, 33);
    this.btnData.TabIndex = 14;
    this.btnData.Tag = (object) "35";
    this.btnData.Click += new EventHandler(this.btnData_Click);
    this.btnData.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.btnData.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.btnMeasure.ImageIndex = 24;
    this.btnMeasure.ImageList = this.newIL;
    this.btnMeasure.Location = new Point(56, 82);
    this.btnMeasure.Name = "btnMeasure";
    this.btnMeasure.Size = new Size(43, 33);
    this.btnMeasure.TabIndex = 15;
    this.btnMeasure.Tag = (object) "36";
    this.btnMeasure.Click += new EventHandler(this.btnMeasured_Click);
    this.btnMeasure.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.btnMeasure.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.errorGB.Controls.Add((Control) this.popupContainerControl1);
    this.errorGB.Controls.Add((Control) this.button13);
    this.errorGB.Controls.Add((Control) this.errorLbl);
    this.errorGB.Dock = DockStyle.Bottom;
    this.errorGB.ForeColor = Color.Red;
    this.errorGB.Location = new Point(0, 130);
    this.errorGB.Name = "errorGB";
    this.errorGB.Size = new Size(1178, 105);
    this.errorGB.TabIndex = 4;
    this.errorGB.TabStop = false;
    this.errorGB.Text = "Обнаружена ошибка!";
    this.errorGB.Visible = false;
    this.popupContainerControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.popupContainerControl1.Controls.Add((Control) this.listFunctions);
    this.popupContainerControl1.Location = new Point(445, 42);
    this.popupContainerControl1.Name = "popupContainerControl1";
    this.popupContainerControl1.Size = new Size(511 /*0x01FF*/, 307);
    this.popupContainerControl1.TabIndex = 8;
    this.listFunctions.Alignment = ListViewAlignment.Default;
    this.listFunctions.Columns.AddRange(new ColumnHeader[3]
    {
      this.columnHeader1,
      this.columnHeader3,
      this.columnHeader2
    });
    this.listFunctions.Dock = DockStyle.Fill;
    this.listFunctions.FullRowSelect = true;
    this.listFunctions.GridLines = true;
    this.listFunctions.HeaderStyle = ColumnHeaderStyle.None;
    this.listFunctions.HideSelection = false;
    listViewItem1.UseItemStyleForSubItems = false;
    listViewItem2.UseItemStyleForSubItems = false;
    listViewItem3.UseItemStyleForSubItems = false;
    listViewItem4.UseItemStyleForSubItems = false;
    listViewItem5.UseItemStyleForSubItems = false;
    listViewItem6.UseItemStyleForSubItems = false;
    listViewItem7.UseItemStyleForSubItems = false;
    listViewItem8.UseItemStyleForSubItems = false;
    listViewItem9.UseItemStyleForSubItems = false;
    listViewItem10.UseItemStyleForSubItems = false;
    listViewItem11.UseItemStyleForSubItems = false;
    listViewItem12.UseItemStyleForSubItems = false;
    listViewItem13.UseItemStyleForSubItems = false;
    listViewItem14.UseItemStyleForSubItems = false;
    listViewItem15.UseItemStyleForSubItems = false;
    listViewItem16.UseItemStyleForSubItems = false;
    listViewItem17.UseItemStyleForSubItems = false;
    listViewItem18.UseItemStyleForSubItems = false;
    listViewItem19.UseItemStyleForSubItems = false;
    listViewItem20.UseItemStyleForSubItems = false;
    listViewItem21.UseItemStyleForSubItems = false;
    listViewItem22.UseItemStyleForSubItems = false;
    listViewItem23.UseItemStyleForSubItems = false;
    listViewItem24.UseItemStyleForSubItems = false;
    listViewItem25.UseItemStyleForSubItems = false;
    listViewItem26.UseItemStyleForSubItems = false;
    listViewItem27.UseItemStyleForSubItems = false;
    listViewItem28.UseItemStyleForSubItems = false;
    listViewItem29.UseItemStyleForSubItems = false;
    listViewItem30.UseItemStyleForSubItems = false;
    listViewItem31.UseItemStyleForSubItems = false;
    listViewItem32.UseItemStyleForSubItems = false;
    listViewItem33.UseItemStyleForSubItems = false;
    listViewItem34.UseItemStyleForSubItems = false;
    listViewItem35.UseItemStyleForSubItems = false;
    listViewItem36.UseItemStyleForSubItems = false;
    listViewItem37.UseItemStyleForSubItems = false;
    listViewItem38.UseItemStyleForSubItems = false;
    listViewItem39.UseItemStyleForSubItems = false;
    listViewItem40.UseItemStyleForSubItems = false;
    listViewItem41.UseItemStyleForSubItems = false;
    listViewItem42.UseItemStyleForSubItems = false;
    listViewItem43.UseItemStyleForSubItems = false;
    listViewItem44.UseItemStyleForSubItems = false;
    listViewItem45.UseItemStyleForSubItems = false;
    listViewItem46.UseItemStyleForSubItems = false;
    listViewItem47.UseItemStyleForSubItems = false;
    listViewItem48.UseItemStyleForSubItems = false;
    listViewItem49.UseItemStyleForSubItems = false;
    listViewItem50.UseItemStyleForSubItems = false;
    listViewItem51.UseItemStyleForSubItems = false;
    listViewItem52.UseItemStyleForSubItems = false;
    listViewItem53.UseItemStyleForSubItems = false;
    listViewItem54.UseItemStyleForSubItems = false;
    listViewItem55.UseItemStyleForSubItems = false;
    listViewItem56.UseItemStyleForSubItems = false;
    listViewItem57.UseItemStyleForSubItems = false;
    listViewItem58.UseItemStyleForSubItems = false;
    listViewItem59.UseItemStyleForSubItems = false;
    listViewItem60.UseItemStyleForSubItems = false;
    listViewItem61.UseItemStyleForSubItems = false;
    listViewItem62.UseItemStyleForSubItems = false;
    listViewItem63.UseItemStyleForSubItems = false;
    listViewItem64.UseItemStyleForSubItems = false;
    listViewItem65.UseItemStyleForSubItems = false;
    listViewItem66.UseItemStyleForSubItems = false;
    listViewItem67.UseItemStyleForSubItems = false;
    listViewItem68.UseItemStyleForSubItems = false;
    listViewItem69.ToolTipText = "Имя типа документа, установленная в настройках IPS";
    listViewItem69.UseItemStyleForSubItems = false;
    listViewItem70.UseItemStyleForSubItems = false;
    listViewItem71.UseItemStyleForSubItems = false;
    listViewItem72.UseItemStyleForSubItems = false;
    listViewItem73.UseItemStyleForSubItems = false;
    listViewItem74.UseItemStyleForSubItems = false;
    this.listFunctions.Items.AddRange(new ListViewItem[74]
    {
      listViewItem1,
      listViewItem2,
      listViewItem3,
      listViewItem4,
      listViewItem5,
      listViewItem6,
      listViewItem7,
      listViewItem8,
      listViewItem9,
      listViewItem10,
      listViewItem11,
      listViewItem12,
      listViewItem13,
      listViewItem14,
      listViewItem15,
      listViewItem16,
      listViewItem17,
      listViewItem18,
      listViewItem19,
      listViewItem20,
      listViewItem21,
      listViewItem22,
      listViewItem23,
      listViewItem24,
      listViewItem25,
      listViewItem26,
      listViewItem27,
      listViewItem28,
      listViewItem29,
      listViewItem30,
      listViewItem31,
      listViewItem32,
      listViewItem33,
      listViewItem34,
      listViewItem35,
      listViewItem36,
      listViewItem37,
      listViewItem38,
      listViewItem39,
      listViewItem40,
      listViewItem41,
      listViewItem42,
      listViewItem43,
      listViewItem44,
      listViewItem45,
      listViewItem46,
      listViewItem47,
      listViewItem48,
      listViewItem49,
      listViewItem50,
      listViewItem51,
      listViewItem52,
      listViewItem53,
      listViewItem54,
      listViewItem55,
      listViewItem56,
      listViewItem57,
      listViewItem58,
      listViewItem59,
      listViewItem60,
      listViewItem61,
      listViewItem62,
      listViewItem63,
      listViewItem64,
      listViewItem65,
      listViewItem66,
      listViewItem67,
      listViewItem68,
      listViewItem69,
      listViewItem70,
      listViewItem71,
      listViewItem72,
      listViewItem73,
      listViewItem74
    });
    this.listFunctions.LabelWrap = false;
    this.listFunctions.Location = new Point(0, 0);
    this.listFunctions.MultiSelect = false;
    this.listFunctions.Name = "listFunctions";
    this.listFunctions.ShowGroups = false;
    this.listFunctions.Size = new Size(511 /*0x01FF*/, 307);
    this.listFunctions.TabIndex = 8;
    this.listFunctions.UseCompatibleStateImageBehavior = false;
    this.listFunctions.View = View.Details;
    this.listFunctions.DoubleClick += new EventHandler(this.listFunctions_DoubleClick);
    this.columnHeader1.Text = "Имя";
    this.columnHeader1.Width = 80 /*0x50*/;
    this.columnHeader3.Text = "ColumnHeader";
    this.columnHeader3.Width = 22;
    this.columnHeader2.Text = "Описание";
    this.columnHeader2.Width = (int) byte.MaxValue;
    this.errorLbl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.errorLbl.ForeColor = Color.Purple;
    this.errorLbl.ImeMode = ImeMode.NoControl;
    this.errorLbl.Location = new Point(13, 29);
    this.errorLbl.Name = "errorLbl";
    this.errorLbl.Size = new Size(1133, 65);
    this.errorLbl.TabIndex = 0;
    this.errorLbl.DoubleClick += new EventHandler(this.errorLbl_DoubleClick);
    this.errorLbl.MouseDown += new MouseEventHandler(this.errorLbl_MouseDown);
    this.panelDate.BorderStyle = BorderStyle.FixedSingle;
    this.panelDate.Controls.Add((Control) this.monthCalendar1);
    this.panelDate.Location = new Point(613, 143);
    this.panelDate.Name = "panelDate";
    this.panelDate.Size = new Size(269, 227);
    this.panelDate.TabIndex = 10;
    this.panelDate.Visible = false;
    this.panelDate.Leave += new EventHandler(this.panelDate_Leave);
    this.monthCalendar1.ImeMode = ImeMode.NoControl;
    this.monthCalendar1.Location = new Point(0, 0);
    this.monthCalendar1.MaxSelectionCount = 1;
    this.monthCalendar1.Name = "monthCalendar1";
    this.monthCalendar1.TabIndex = 1;
    this.monthCalendar1.DateSelected += new DateRangeEventHandler(this.monthCalendar1_DateSelected);
    this.monthCalendar1.KeyDown += new KeyEventHandler(this.monthCalendar1_KeyDown);
    this.panel1.Controls.Add((Control) this.panel8);
    this.panel1.Controls.Add((Control) this.insertGB);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 235);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(1178, 247);
    this.panel1.TabIndex = 8;
    this.panel1.MouseDown += new MouseEventHandler(this.errorLbl_MouseDown);
    this.panel8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.panel8.Controls.Add((Control) this.btnCarriageReturn);
    this.panel8.Controls.Add((Control) this.btnArrayEnd);
    this.panel8.Controls.Add((Control) this.btnArrayStart);
    this.panel8.Controls.Add((Control) this.label4);
    this.panel8.Controls.Add((Control) this.button31);
    this.panel8.Controls.Add((Control) this.popupFunc);
    this.panel8.Controls.Add((Control) this.buttonSet);
    this.panel8.Controls.Add((Control) this.btnData);
    this.panel8.Controls.Add((Control) this.btnMeasure);
    this.panel8.Controls.Add((Control) this.button30);
    this.panel8.Controls.Add((Control) this.buttonNot);
    this.panel8.Controls.Add((Control) this.button29);
    this.panel8.Controls.Add((Control) this.button32);
    this.panel8.Controls.Add((Control) this.button28);
    this.panel8.Controls.Add((Control) this.buttonUMinus);
    this.panel8.Controls.Add((Control) this.buttonPi);
    this.panel8.Controls.Add((Control) this.buttonDiap);
    this.panel8.Controls.Add((Control) this.buttonOr);
    this.panel8.Controls.Add((Control) this.buttonComma);
    this.panel8.Controls.Add((Control) this.button16);
    this.panel8.Controls.Add((Control) this.buttonAnd);
    this.panel8.Controls.Add((Control) this.button26);
    this.panel8.Controls.Add((Control) this.button21);
    this.panel8.Controls.Add((Control) this.button17);
    this.panel8.Controls.Add((Control) this.button20);
    this.panel8.Controls.Add((Control) this.button25);
    this.panel8.Controls.Add((Control) this.button19);
    this.panel8.Controls.Add((Control) this.button23);
    this.panel8.Controls.Add((Control) this.button24);
    this.panel8.Controls.Add((Control) this.button18);
    this.panel8.Controls.Add((Control) this.button22);
    this.panel8.Location = new Point(656, 12);
    this.panel8.Name = "panel8";
    this.panel8.Size = new Size(514, 228);
    this.panel8.TabIndex = 16 /*0x10*/;
    this.btnCarriageReturn.ImageIndex = 41;
    this.btnCarriageReturn.ImageList = this.newIL;
    this.btnCarriageReturn.ImeMode = ImeMode.NoControl;
    this.btnCarriageReturn.Location = new Point(461, 82);
    this.btnCarriageReturn.Name = "btnCarriageReturn";
    this.btnCarriageReturn.Size = new Size(43, 33);
    this.btnCarriageReturn.TabIndex = 26;
    this.btnCarriageReturn.Tag = (object) "41";
    this.btnCarriageReturn.Click += new EventHandler(this.button28_Click);
    this.btnArrayEnd.ImageIndex = 40;
    this.btnArrayEnd.ImageList = this.newIL;
    this.btnArrayEnd.ImeMode = ImeMode.NoControl;
    this.btnArrayEnd.Location = new Point(461, 172);
    this.btnArrayEnd.Name = "btnArrayEnd";
    this.btnArrayEnd.Size = new Size(43, 34);
    this.btnArrayEnd.TabIndex = 25;
    this.btnArrayEnd.Tag = (object) "38";
    this.btnArrayEnd.Click += new EventHandler(this.button28_Click);
    this.btnArrayStart.ImageIndex = 39;
    this.btnArrayStart.ImageList = this.newIL;
    this.btnArrayStart.ImeMode = ImeMode.NoControl;
    this.btnArrayStart.Location = new Point(461, 134);
    this.btnArrayStart.Name = "btnArrayStart";
    this.btnArrayStart.Size = new Size(43, 34);
    this.btnArrayStart.TabIndex = 24;
    this.btnArrayStart.Tag = (object) "37";
    this.btnArrayStart.Click += new EventHandler(this.button28_Click);
    this.label4.AutoSize = true;
    this.label4.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
    this.label4.Location = new Point(13, 12);
    this.label4.Name = "label4";
    this.label4.Size = new Size(160 /*0xA0*/, 20);
    this.label4.TabIndex = 23;
    this.label4.Text = "Выбор функции:";
    this.button31.ImageIndex = 3;
    this.button31.ImageList = this.newIL;
    this.button31.ImeMode = ImeMode.NoControl;
    this.button31.Location = new Point(358, 82);
    this.button31.Name = "button31";
    this.button31.Size = new Size(44, 33);
    this.button31.TabIndex = 20;
    this.button31.Tag = (object) "34";
    this.button31.Click += new EventHandler(this.button28_Click);
    this.button31.Enter += new EventHandler(this.memoForm_Enter);
    this.button31.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.button31.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.popupFunc.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.popupFunc.EditValue = (object) "Ввести функцию...";
    this.popupFunc.Location = new Point(10, 39);
    this.popupFunc.Name = "popupFunc";
    this.popupFunc.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo, "", 16 /*0x10*/, true, true, false, HorzAlignment.Center, (Image) null)
    });
    this.popupFunc.Properties.LookAndFeel.Style = LookAndFeelStyle.Style3D;
    this.popupFunc.Properties.PopupControl = this.popupContainerControl1;
    this.popupFunc.Properties.PopupSizeable = false;
    this.popupFunc.Properties.ShowPopupCloseButton = false;
    this.popupFunc.Size = new Size(492, 26);
    this.popupFunc.TabIndex = 5;
    this.popupFunc.QueryResultValue += new QueryResultValueEventHandler(this.popupFunc_QueryResultValue);
    this.popupFunc.QueryDisplayText += new QueryDisplayTextEventHandler(this.popupFunc_QueryDisplayText);
    this.buttonSet.ImageIndex = 36;
    this.buttonSet.ImageList = this.newIL;
    this.buttonSet.ImeMode = ImeMode.NoControl;
    this.buttonSet.Location = new Point(406, 172);
    this.buttonSet.Name = "buttonSet";
    this.buttonSet.Size = new Size(44, 34);
    this.buttonSet.TabIndex = 9;
    this.buttonSet.Tag = (object) "19";
    this.buttonSet.Click += new EventHandler(this.buttonSet_Click);
    this.buttonSet.Enter += new EventHandler(this.memoForm_Enter);
    this.buttonSet.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.buttonSet.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.button30.ImageIndex = 2;
    this.button30.ImageList = this.newIL;
    this.button30.ImeMode = ImeMode.NoControl;
    this.button30.Location = new Point(310, 82);
    this.button30.Name = "button30";
    this.button30.Size = new Size(44, 33);
    this.button30.TabIndex = 19;
    this.button30.Tag = (object) "33";
    this.button30.Click += new EventHandler(this.button28_Click);
    this.button30.Enter += new EventHandler(this.memoForm_Enter);
    this.button30.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.button30.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.buttonNot.ImageIndex = 29;
    this.buttonNot.ImageList = this.newIL;
    this.buttonNot.ImeMode = ImeMode.NoControl;
    this.buttonNot.Location = new Point(406, 134);
    this.buttonNot.Name = "buttonNot";
    this.buttonNot.Size = new Size(44, 34);
    this.buttonNot.TabIndex = 22;
    this.buttonNot.Tag = (object) "18";
    this.buttonNot.Click += new EventHandler(this.buttonNot_Click);
    this.buttonNot.Enter += new EventHandler(this.memoForm_Enter);
    this.buttonNot.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.buttonNot.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.button29.ImageIndex = 1;
    this.button29.ImageList = this.newIL;
    this.button29.ImeMode = ImeMode.NoControl;
    this.button29.Location = new Point(256 /*0x0100*/, 82);
    this.button29.Name = "button29";
    this.button29.Size = new Size(43, 33);
    this.button29.TabIndex = 14;
    this.button29.Tag = (object) "32";
    this.button29.Click += new EventHandler(this.button28_Click);
    this.button29.Enter += new EventHandler(this.memoForm_Enter);
    this.button29.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.button29.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.button32.ImageIndex = 16 /*0x10*/;
    this.button32.ImageList = this.newIL;
    this.button32.ImeMode = ImeMode.NoControl;
    this.button32.Location = new Point(160 /*0xA0*/, 82);
    this.button32.Name = "button32";
    this.button32.Size = new Size(43, 33);
    this.button32.TabIndex = 5;
    this.button32.TabStop = false;
    this.button32.Tag = (object) "23";
    this.button32.Click += new EventHandler(this.buttonPi_Click);
    this.button32.Enter += new EventHandler(this.memoForm_Enter);
    this.button32.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.button32.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.button28.ImageIndex = 0;
    this.button28.ImageList = this.newIL;
    this.button28.ImeMode = ImeMode.NoControl;
    this.button28.Location = new Point(208 /*0xD0*/, 82);
    this.button28.Name = "button28";
    this.button28.Size = new Size(43, 33);
    this.button28.TabIndex = 13;
    this.button28.Tag = (object) "31";
    this.button28.Click += new EventHandler(this.button28_Click);
    this.button28.Enter += new EventHandler(this.memoForm_Enter);
    this.button28.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.button28.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.buttonUMinus.ImageIndex = 5;
    this.buttonUMinus.ImageList = this.newIL;
    this.buttonUMinus.ImeMode = ImeMode.NoControl;
    this.buttonUMinus.Location = new Point(106, 172);
    this.buttonUMinus.Name = "buttonUMinus";
    this.buttonUMinus.Size = new Size(43, 34);
    this.buttonUMinus.TabIndex = 8;
    this.buttonUMinus.Tag = (object) "30";
    this.buttonUMinus.Click += new EventHandler(this.buttonUMinus_Click);
    this.buttonUMinus.Enter += new EventHandler(this.memoForm_Enter);
    this.buttonUMinus.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.buttonUMinus.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.buttonPi.ImageIndex = 33;
    this.buttonPi.ImageList = this.newIL;
    this.buttonPi.ImeMode = ImeMode.NoControl;
    this.buttonPi.Location = new Point(106, 82);
    this.buttonPi.Name = "buttonPi";
    this.buttonPi.Size = new Size(43, 33);
    this.buttonPi.TabIndex = 3;
    this.buttonPi.TabStop = false;
    this.buttonPi.Tag = (object) "22";
    this.buttonPi.Click += new EventHandler(this.buttonPi_Click);
    this.buttonPi.Enter += new EventHandler(this.memoForm_Enter);
    this.buttonPi.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.buttonPi.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.buttonDiap.ImageIndex = 7;
    this.buttonDiap.ImageList = this.newIL;
    this.buttonDiap.ImeMode = ImeMode.NoControl;
    this.buttonDiap.Location = new Point(358, 172);
    this.buttonDiap.Name = "buttonDiap";
    this.buttonDiap.Size = new Size(44, 34);
    this.buttonDiap.TabIndex = 11;
    this.buttonDiap.Tag = (object) "20";
    this.buttonDiap.Click += new EventHandler(this.buttonDiap_Click);
    this.buttonDiap.Enter += new EventHandler(this.memoForm_Enter);
    this.buttonDiap.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.buttonDiap.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.buttonOr.ImageIndex = 31 /*0x1F*/;
    this.buttonOr.ImageList = this.newIL;
    this.buttonOr.ImeMode = ImeMode.NoControl;
    this.buttonOr.Location = new Point(358, 134);
    this.buttonOr.Name = "buttonOr";
    this.buttonOr.Size = new Size(44, 34);
    this.buttonOr.TabIndex = 21;
    this.buttonOr.Tag = (object) "17";
    this.buttonOr.Click += new EventHandler(this.button22_Click);
    this.buttonOr.Enter += new EventHandler(this.memoForm_Enter);
    this.buttonOr.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.buttonOr.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.buttonComma.ImageIndex = 8;
    this.buttonComma.ImageList = this.newIL;
    this.buttonComma.ImeMode = ImeMode.NoControl;
    this.buttonComma.Location = new Point(310, 172);
    this.buttonComma.Name = "buttonComma";
    this.buttonComma.Size = new Size(44, 34);
    this.buttonComma.TabIndex = 10;
    this.buttonComma.Tag = (object) "21";
    this.buttonComma.Click += new EventHandler(this.buttonComma_Click);
    this.buttonComma.Enter += new EventHandler(this.memoForm_Enter);
    this.buttonComma.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.buttonComma.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.button16.ImageIndex = 27;
    this.button16.ImageList = this.newIL;
    this.button16.ImeMode = ImeMode.NoControl;
    this.button16.Location = new Point(256 /*0x0100*/, 172);
    this.button16.Name = "button16";
    this.button16.Size = new Size(43, 34);
    this.button16.TabIndex = 5;
    this.button16.Tag = (object) "15";
    this.button16.Click += new EventHandler(this.button22_Click);
    this.button16.Enter += new EventHandler(this.memoForm_Enter);
    this.button16.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.button16.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.buttonAnd.ImageIndex = 4;
    this.buttonAnd.ImageList = this.newIL;
    this.buttonAnd.ImeMode = ImeMode.NoControl;
    this.buttonAnd.Location = new Point(310, 134);
    this.buttonAnd.Name = "buttonAnd";
    this.buttonAnd.Size = new Size(44, 34);
    this.buttonAnd.TabIndex = 20;
    this.buttonAnd.Tag = (object) "16";
    this.buttonAnd.Click += new EventHandler(this.button22_Click);
    this.buttonAnd.Enter += new EventHandler(this.memoForm_Enter);
    this.buttonAnd.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.buttonAnd.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.button26.ImageIndex = 35;
    this.button26.ImageList = this.newIL;
    this.button26.ImeMode = ImeMode.NoControl;
    this.button26.Location = new Point(106, 134);
    this.button26.Name = "button26";
    this.button26.Size = new Size(43, 34);
    this.button26.TabIndex = 7;
    this.button26.Tag = (object) "27";
    this.button26.Click += new EventHandler(this.button22_Click);
    this.button26.Enter += new EventHandler(this.memoForm_Enter);
    this.button26.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.button26.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.button21.ImageIndex = 17;
    this.button21.ImageList = this.newIL;
    this.button21.ImeMode = ImeMode.NoControl;
    this.button21.Location = new Point(160 /*0xA0*/, 134);
    this.button21.Name = "button21";
    this.button21.Size = new Size(43, 34);
    this.button21.TabIndex = 0;
    this.button21.Tag = (object) "10";
    this.button21.Click += new EventHandler(this.button22_Click);
    this.button21.Enter += new EventHandler(this.memoForm_Enter);
    this.button21.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.button21.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.button17.ImageIndex = 23;
    this.button17.ImageList = this.newIL;
    this.button17.ImeMode = ImeMode.NoControl;
    this.button17.Location = new Point(256 /*0x0100*/, 134);
    this.button17.Name = "button17";
    this.button17.Size = new Size(43, 34);
    this.button17.TabIndex = 4;
    this.button17.Tag = (object) "12";
    this.button17.Click += new EventHandler(this.button22_Click);
    this.button17.Enter += new EventHandler(this.memoForm_Enter);
    this.button17.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.button17.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.button20.ImageIndex = 22;
    this.button20.ImageList = this.newIL;
    this.button20.ImeMode = ImeMode.NoControl;
    this.button20.Location = new Point(160 /*0xA0*/, 172);
    this.button20.Name = "button20";
    this.button20.Size = new Size(43, 34);
    this.button20.TabIndex = 1;
    this.button20.Tag = (object) "11";
    this.button20.Click += new EventHandler(this.button22_Click);
    this.button20.Enter += new EventHandler(this.memoForm_Enter);
    this.button20.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.button20.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.button25.ImageIndex = 15;
    this.button25.ImageList = this.newIL;
    this.button25.ImeMode = ImeMode.NoControl;
    this.button25.Location = new Point(58, 172);
    this.button25.Name = "button25";
    this.button25.Size = new Size(43, 34);
    this.button25.TabIndex = 6;
    this.button25.Tag = (object) "29";
    this.button25.Click += new EventHandler(this.button22_Click);
    this.button25.Enter += new EventHandler(this.memoForm_Enter);
    this.button25.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.button25.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.button19.ImageIndex = 26;
    this.button19.ImageList = this.newIL;
    this.button19.ImeMode = ImeMode.NoControl;
    this.button19.Location = new Point(208 /*0xD0*/, 172);
    this.button19.Name = "button19";
    this.button19.Size = new Size(43, 34);
    this.button19.TabIndex = 2;
    this.button19.Tag = (object) "14";
    this.button19.Click += new EventHandler(this.button22_Click);
    this.button19.Enter += new EventHandler(this.memoForm_Enter);
    this.button19.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.button19.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.button23.ImageIndex = 25;
    this.button23.ImageList = this.newIL;
    this.button23.ImeMode = ImeMode.NoControl;
    this.button23.Location = new Point(58, 134);
    this.button23.Name = "button23";
    this.button23.Size = new Size(43, 34);
    this.button23.TabIndex = 4;
    this.button23.Tag = (object) "26";
    this.button23.Click += new EventHandler(this.button22_Click);
    this.button23.Enter += new EventHandler(this.memoForm_Enter);
    this.button23.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.button23.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.button24.ImageIndex = 28;
    this.button24.ImageList = this.newIL;
    this.button24.ImeMode = ImeMode.NoControl;
    this.button24.Location = new Point(10, 172);
    this.button24.Name = "button24";
    this.button24.Size = new Size(43, 34);
    this.button24.TabIndex = 5;
    this.button24.Tag = (object) "28";
    this.button24.Click += new EventHandler(this.button22_Click);
    this.button24.Enter += new EventHandler(this.memoForm_Enter);
    this.button24.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.button24.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.button18.ImageIndex = 30;
    this.button18.ImageList = this.newIL;
    this.button18.ImeMode = ImeMode.NoControl;
    this.button18.Location = new Point(208 /*0xD0*/, 134);
    this.button18.Name = "button18";
    this.button18.Size = new Size(43, 34);
    this.button18.TabIndex = 3;
    this.button18.Tag = (object) "13";
    this.button18.Click += new EventHandler(this.button22_Click);
    this.button18.Enter += new EventHandler(this.memoForm_Enter);
    this.button18.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.button18.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.button22.ImageIndex = 34;
    this.button22.ImageList = this.newIL;
    this.button22.ImeMode = ImeMode.NoControl;
    this.button22.Location = new Point(10, 134);
    this.button22.Name = "button22";
    this.button22.Size = new Size(43, 34);
    this.button22.TabIndex = 3;
    this.button22.Tag = (object) "25";
    this.button22.Click += new EventHandler(this.button22_Click);
    this.button22.Enter += new EventHandler(this.memoForm_Enter);
    this.button22.MouseEnter += new EventHandler(this.buttonTHIS_MouseEnter);
    this.button22.MouseLeave += new EventHandler(this.buttonTHIS_MouseLeave);
    this.insertGB.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.insertGB.Controls.Add((Control) this.cbAllowUnknown);
    this.insertGB.Controls.Add((Control) this.label3);
    this.insertGB.Controls.Add((Control) this.label2);
    this.insertGB.Controls.Add((Control) this.checkObjType);
    this.insertGB.Controls.Add((Control) this.btnClearAttr);
    this.insertGB.Controls.Add((Control) this.AttrTypeLbl);
    this.insertGB.Controls.Add((Control) this.textObjName);
    this.insertGB.Controls.Add((Control) this.textAttName);
    this.insertGB.Controls.Add((Control) this.label1);
    this.insertGB.Controls.Add((Control) this.comboAttr);
    this.insertGB.Controls.Add((Control) this.buttonPLUS);
    this.insertGB.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
    this.insertGB.Location = new Point(13, 12);
    this.insertGB.MinimumSize = new Size(435, 150);
    this.insertGB.Name = "insertGB";
    this.insertGB.Size = new Size(643, 228);
    this.insertGB.TabIndex = 6;
    this.insertGB.TabStop = false;
    this.insertGB.Text = "Ввод атрибута";
    this.cbAllowUnknown.AutoSize = true;
    this.cbAllowUnknown.Font = new Font("Microsoft Sans Serif", 8.25f);
    this.cbAllowUnknown.Location = new Point(256 /*0x0100*/, 155);
    this.cbAllowUnknown.Name = "cbAllowUnknown";
    this.cbAllowUnknown.Size = new Size(460, 24);
    this.cbAllowUnknown.TabIndex = 25;
    this.cbAllowUnknown.Text = "Если нет значения атрибута, подставлять пустое";
    this.cbAllowUnknown.UseVisualStyleBackColor = true;
    this.cbAllowUnknown.CheckedChanged += new EventHandler(this.cbAllowUnknown_CheckedChanged);
    this.label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.label3.AutoSize = true;
    this.label3.Font = new Font("Microsoft Sans Serif", 8.25f);
    this.label3.Location = new Point(10, 156);
    this.label3.Name = "label3";
    this.label3.Size = new Size(240 /*0xF0*/, 20);
    this.label3.TabIndex = 24;
    this.label3.Text = "Использованные атрибуты:";
    this.label2.AutoSize = true;
    this.label2.Font = new Font("Microsoft Sans Serif", 8.25f);
    this.label2.Location = new Point(13, 117);
    this.label2.Name = "label2";
    this.label2.Size = new Size(122, 20);
    this.label2.TabIndex = 23;
    this.label2.Text = "Тип атрибута";
    this.checkObjType.AutoSize = true;
    this.checkObjType.Font = new Font("Microsoft Sans Serif", 8.25f);
    this.checkObjType.Location = new Point(10, 34);
    this.checkObjType.Name = "checkObjType";
    this.checkObjType.Size = new Size(140, 24);
    this.checkObjType.TabIndex = 22;
    this.checkObjType.Text = "Тип объекта";
    this.checkObjType.UseVisualStyleBackColor = true;
    this.checkObjType.CheckedChanged += new EventHandler(this.checkObjType_CheckedChanged);
    this.btnClearAttr.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnClearAttr.ImageIndex = 14;
    this.btnClearAttr.ImageList = this.IL_50;
    this.btnClearAttr.ImeMode = ImeMode.NoControl;
    this.btnClearAttr.Location = new Point(538, 111);
    this.btnClearAttr.Name = "btnClearAttr";
    this.btnClearAttr.Size = new Size(43, 34);
    this.btnClearAttr.TabIndex = 21;
    this.btnClearAttr.Click += new EventHandler(this.button2_Click);
    this.AttrTypeLbl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.AttrTypeLbl.BorderStyle = BorderStyle.Fixed3D;
    this.AttrTypeLbl.ImeMode = ImeMode.NoControl;
    this.AttrTypeLbl.Location = new Point(160 /*0xA0*/, 117);
    this.AttrTypeLbl.Name = "AttrTypeLbl";
    this.AttrTypeLbl.Size = new Size(368, 22);
    this.AttrTypeLbl.TabIndex = 20;
    this.textObjName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.textObjName.Location = new Point(160 /*0xA0*/, 29);
    this.textObjName.Name = "textObjName";
    this.textObjName.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.textObjName.Properties.ReadOnly = true;
    this.textObjName.Size = new Size(469, 26);
    this.textObjName.TabIndex = 17;
    this.textObjName.ButtonClick += new ButtonPressedEventHandler(this.textObjName_ButtonClick);
    this.textAttName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.textAttName.Location = new Point(160 /*0xA0*/, 70);
    this.textAttName.Name = "textAttName";
    this.textAttName.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.textAttName.Properties.ReadOnly = true;
    this.textAttName.Size = new Size(469, 26);
    this.textAttName.TabIndex = 16 /*0x10*/;
    this.textAttName.ButtonClick += new ButtonPressedEventHandler(this.textAttName_ButtonClick);
    this.label1.Font = new Font("Microsoft Sans Serif", 8.25f);
    this.label1.ImeMode = ImeMode.NoControl;
    this.label1.Location = new Point(13, 76);
    this.label1.Name = "label1";
    this.label1.Size = new Size(128 /*0x80*/, 18);
    this.label1.TabIndex = 15;
    this.label1.Text = "Имя атрибута";
    this.comboAttr.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.comboAttr.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboAttr.ItemHeight = 20;
    this.comboAttr.Location = new Point(16 /*0x10*/, 189);
    this.comboAttr.Name = "comboAttr";
    this.comboAttr.Size = new Size(613, 28);
    this.comboAttr.Sorted = true;
    this.comboAttr.TabIndex = 10;
    this.comboAttr.SelectedIndexChanged += new EventHandler(this.comboAttr_SelectedIndexChanged);
    this.buttIL.ColorDepth = ColorDepth.Depth8Bit;
    this.buttIL.ImageSize = new Size(16 /*0x10*/, 16 /*0x10*/);
    this.buttIL.TransparentColor = Color.White;
    this.panelButtons.Controls.Add((Control) this.btnImport);
    this.panelButtons.Controls.Add((Control) this.btnExport);
    this.panelButtons.Controls.Add((Control) this.pasteBtn);
    this.panelButtons.Controls.Add((Control) this.copyBtn);
    this.panelButtons.Controls.Add((Control) this.btnRun);
    this.panelButtons.Controls.Add((Control) this.btnCompile);
    this.panelButtons.Controls.Add((Control) this.hintLabel);
    this.panelButtons.Controls.Add((Control) this.buttonCancel);
    this.panelButtons.Controls.Add((Control) this.buttonOK);
    this.panelButtons.Dock = DockStyle.Bottom;
    this.panelButtons.Location = new Point(0, 482);
    this.panelButtons.Name = "panelButtons";
    this.panelButtons.Size = new Size(1178, 47);
    this.panelButtons.TabIndex = 9;
    this.panelButtons.MouseDown += new MouseEventHandler(this.errorLbl_MouseDown);
    this.btnImport.Location = new Point(312, 4);
    this.btnImport.Name = "btnImport";
    this.btnImport.Size = new Size(194, 40);
    this.btnImport.TabIndex = 23;
    this.btnImport.Text = "Импорт";
    this.btnImport.UseVisualStyleBackColor = true;
    this.btnImport.Click += new EventHandler(this.btnImport_Click);
    this.btnExport.Location = new Point(109, 4);
    this.btnExport.Name = "btnExport";
    this.btnExport.Size = new Size(193, 40);
    this.btnExport.TabIndex = 22;
    this.btnExport.Text = "Экспорт";
    this.btnExport.UseVisualStyleBackColor = true;
    this.btnExport.Click += new EventHandler(this.btnExport_Click);
    this.hintLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.hintLabel.BackColor = SystemColors.Control;
    this.hintLabel.ImeMode = ImeMode.NoControl;
    this.hintLabel.Location = new Point(523, 10);
    this.hintLabel.Name = "hintLabel";
    this.hintLabel.Size = new Size(122, 24);
    this.hintLabel.TabIndex = 15;
    this.hintLabel.TextAlign = ContentAlignment.MiddleCenter;
    this.hintLabel.MouseDown += new MouseEventHandler(this.errorLbl_MouseDown);
    this.buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.FlatStyle = FlatStyle.System;
    this.buttonCancel.ImeMode = ImeMode.NoControl;
    this.buttonCancel.Location = new Point(970, 4);
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.Size = new Size(194, 40);
    this.buttonCancel.TabIndex = 2;
    this.buttonCancel.TabStop = false;
    this.buttonCancel.Text = "&Отмена";
    this.buttonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonOK.DialogResult = DialogResult.OK;
    this.buttonOK.FlatStyle = FlatStyle.System;
    this.buttonOK.ImeMode = ImeMode.NoControl;
    this.buttonOK.Location = new Point(767 /*0x02FF*/, 4);
    this.buttonOK.Name = "buttonOK";
    this.buttonOK.Size = new Size(193, 40);
    this.buttonOK.TabIndex = 1;
    this.buttonOK.TabStop = false;
    this.buttonOK.Text = "&OK";
    this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
    this.panel2.Controls.Add((Control) this.memoForm);
    this.panel2.Controls.Add((Control) this.buttonDel);
    this.panel2.Controls.Add((Control) this.editAll);
    this.panel2.Controls.Add((Control) this.buttonNext);
    this.panel2.Controls.Add((Control) this.buttonFirst);
    this.panel2.Controls.Add((Control) this.buttonLast);
    this.panel2.Controls.Add((Control) this.buttonBackspace);
    this.panel2.Controls.Add((Control) this.buttonNew);
    this.panel2.Controls.Add((Control) this.buttonEdit);
    this.panel2.Controls.Add((Control) this.buttonTrash);
    this.panel2.Controls.Add((Control) this.buttonPrev);
    this.panel2.Controls.Add((Control) this.buttonRef);
    this.panel2.Controls.Add((Control) this.buttonDeshifr);
    this.panel2.Dock = DockStyle.Fill;
    this.panel2.Location = new Point(0, 0);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(1178, 130);
    this.panel2.TabIndex = 11;
    this.ofd.DefaultExt = "fml";
    this.ofd.Filter = "Файлы формул (*.fml)|*.fml|Все файлы|*.cs";
    this.ofd.RestoreDirectory = true;
    this.ofd.Title = "Имя файла для импорта формулы";
    this.sfd.DefaultExt = "fml";
    this.sfd.Filter = "Файлы формул (*.fml)|*.fml|Все файлы|*.cs";
    this.sfd.RestoreDirectory = true;
    this.sfd.Title = "Имя файла для экспорта формулы";
    this.AcceptButton = (IButtonControl) this.buttonOK;
    this.AutoScaleBaseSize = new Size(8, 19);
    this.CancelButton = (IButtonControl) this.buttonCancel;
    this.ClientSize = new Size(1178, 529);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.errorGB);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.panelButtons);
    this.Controls.Add((Control) this.panelDate);
    this.HelpButton = true;
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(1200, 585);
    this.Name = nameof (FormEditor);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Формула";
    this.Closed += new EventHandler(this.FormEditor_Closed);
    this.Load += new EventHandler(this.FormEditor_Load);
    this.KeyDown += new KeyEventHandler(this.FormEditor_KeyDown);
    this.KeyPress += new KeyPressEventHandler(this.FormEditor_KeyPress);
    this.MouseDown += new MouseEventHandler(this.errorLbl_MouseDown);
    this.Resize += new EventHandler(this.FormEditor_Resize);
    this.errorGB.ResumeLayout(false);
    this.popupContainerControl1.ResumeLayout(false);
    this.panelDate.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.panel8.ResumeLayout(false);
    this.panel8.PerformLayout();
    this.popupFunc.Properties.EndInit();
    this.insertGB.ResumeLayout(false);
    this.insertGB.PerformLayout();
    this.textObjName.Properties.EndInit();
    this.textAttName.Properties.EndInit();
    this.panelButtons.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.ResumeLayout(false);
  }

  private void editAll_ButtonPressed(object sender, ButtonPressedEventArgs e)
  {
    this.ShowCalendar();
  }

  private void ShowCalendar()
  {
    if (this.panelDate.Visible)
      return;
    this.panelDate.Visible = true;
    this.panelDate.BringToFront();
    this.monthCalendar1.Focus();
  }

  private void HideDatePanel()
  {
    if (!this.panelDate.Visible)
      return;
    this.panelDate.Visible = false;
  }

  private void panelDate_Leave(object sender, EventArgs e) => this.HideDatePanel();

  private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
  {
    string shortDateString = this.monthCalendar1.SelectionStart.ToShortDateString();
    this.HideDatePanel();
    if (!this.ValidateInput(FormEditor.ValidateType.NotAfterOperand))
      return;
    this.InsertToken(this.GetNewToken(shortDateString));
  }

  private void monthCalendar1_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode == Keys.Escape)
      this.HideDatePanel();
    if (e.KeyCode != Keys.Return)
      return;
    string shortDateString = this.monthCalendar1.SelectionStart.ToShortDateString();
    this.HideDatePanel();
    if (!this.ValidateInput(FormEditor.ValidateType.NotAfterOperand))
      return;
    this.InsertToken(this.GetNewToken(shortDateString));
  }

  private void errorLbl_MouseDown(object sender, MouseEventArgs e) => this.HideDatePanel();

  private void SetMinSize()
  {
    this.MinSize.Height = this.panelButtons.Height + this.panel1.Height + 150 + (this.ErrorShow ? this.errorGB.Height : 0);
    this.MinSize.Width = this.MinimumSize.Width;
    this.MinimumSize = this.MinSize;
  }

  private void UpdateErrorVisible()
  {
    if (!(this.ErrorShow ^ this.errorGB.Visible))
      return;
    this.SetMinSize();
    this.errorGB.Visible = this.ErrorShow;
  }

  private void UpdateSizes()
  {
    Size size = this.Size;
    int num = this.ClientSize.Height - this.panelButtons.Height - this.panel1.Height;
    if (this.ErrorShow)
      num -= this.errorGB.Height;
    this.panel2.Height = num;
    this.formHeight = this.Height;
    if (!this.ErrorShow)
      return;
    this.formHeight -= this.errorGB.Height;
  }

  private void FormEditor_Resize(object sender, EventArgs e) => this.UpdateSizes();

  private void SetErrorVisible(bool vis)
  {
    bool flag = false;
    if (this.ErrorShow != vis)
    {
      this.ErrorShow = vis;
      flag = true;
    }
    this.SetMinSize();
    int num = this.ErrorShow ? this.formHeight + this.errorGB.Height : this.formHeight;
    if (this.Height != num)
      this.Height = num;
    if (!flag)
      return;
    this.UpdateErrorVisible();
  }

  private void button13_Click(object sender, EventArgs e) => this.SetErrorVisible(false);

  /// <summary>The main executing method</summary>
  /// <param name="tF"> Formula to edit </param>
  /// <param name="title"> Window Title</param>
  /// <param name="showETO">Need to show ETO button?</param>
  /// <returns>true if user pressed OK</returns>
  public bool Execute(ref TempFormula tF, string title, bool showETO)
  {
    this.SetErrorVisible(false);
    this.SetMinSize();
    this.errorGB.Visible = this.ErrorShow;
    if (this.iNIL == null)
    {
      if (FormulaEditPlugin._serviceProvider == null)
        FormulaEditPlugin._serviceProvider = (System.IServiceProvider) ServicesManager.ServiceContainer;
      this.iNIL = (INamedImageList) FormulaEditPlugin._serviceProvider.GetService(typeof (INamedImageList));
    }
    this.fChanged = false;
    this.tf = (TempFormula) tF.Clone();
    if (this.tf.usedAttrs == null)
      this.tf.Init();
    this.Text = $"{(!(title == "") ? (!this.tf.Cond ? LocalizationHolder.rm.GetString("Expert.Editor_138") + title : LocalizationHolder.rm.GetString("Expert.Editor_137") + title) : (!this.tf.Cond ? LocalizationHolder.rm.GetString("Expert.Editor_136") : LocalizationHolder.rm.GetString("Expert.Editor_135")))} [{DataTypeConvertor.DataTypeName(this.tf.resType)}]";
    this.tf.BeautifyInfixForm();
    this.tf.UpdateTokenBegs();
    this.ShowFormula(this.tf);
    if (this.tf.Count > 0)
      this.SetCurToken(0);
    else
      this.SetCurToken(-1);
    this.curExtraFunc = -1;
    this.popupFunc.Text = "";
    if (this.SelAttrType != null)
    {
      this.checkObjType.Checked = false;
      this.UpdateOTControls(this.checkObjType.Checked);
    }
    else
      this.checkObjType.Checked = true;
    this.EnableControlButtons();
    this.FillArgCombo(this.tf);
    this.LoadSessionData();
    this.CollectUserFunctions();
    for (int index = 0; index < this.listFunctionsTags.Length; ++index)
      this.listFunctions.Items[index].Tag = (object) this.listFunctionsTags[index];
    this.copyBtn.Image = this.iNIL.ImageList.Images[this.iNIL.ImageIndex("imgCopy")];
    this.pasteBtn.Image = this.iNIL.ImageList.Images[this.iNIL.ImageIndex("imgPaste")];
    if (this.ShowDialog() != DialogResult.OK)
      return false;
    tF = (TempFormula) this.tf.Clone();
    return true;
  }

  /// <summary>See above, but without ETO</summary>
  /// <param name="tf"> Formula to edit </param>
  /// <param name="title"> Window Title </param>
  /// <returns> true if user pressed OK </returns>
  public bool Execute(ref TempFormula tf, string title) => this.Execute(ref tf, title, false);

  internal void CollectUserFunctions()
  {
    if (!((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IExpertServer)) is IExpertServer customService))
      return;
    foreach (int funcId in customService.GetFuncIds())
    {
      FuncData funcData = customService.GetFuncData(funcId);
      this.listFunctions.Items.Add(new ListViewItem(new string[3]
      {
        funcData.GetFuncTemplate(),
        new string(FuncData.DTToChar(funcData.result), 1),
        funcData.description
      }, -1, Color.Empty, Color.Empty, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204))
      {
        Tag = (object) funcId
      });
    }
  }

  internal void ShowFormula(TempFormula tf)
  {
    int curCharPos = -1;
    this.memoForm.Text = tf.TextWithCursor(this.curTokIndex, out curCharPos);
    for (int index = 0; index < tf.Count; ++index)
      this.PaintCurToken(tf[index], index > this.curTokIndex);
    if (curCharPos < 0)
      return;
    this.memoForm.SelectionStart = curCharPos;
    this.memoForm.SelectionLength = 1;
    this.memoForm.SelectionColor = Color.Red;
    this.memoForm.SelectionLength = 0;
  }

  private void LoadSessionData()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.attrData = sessionKeeper.Session.GetAttributeTypeCollection(-1).Select("");
      this.objTypeData = sessionKeeper.Session.GetObjectTypeCollection(-2).Select("");
      this.tf.FixInfixForm(sessionKeeper.Session);
    }
  }

  private void listFunctions_DoubleClick(object sender, EventArgs e)
  {
    ListViewItem selectedItem = this.listFunctions.SelectedItems[0];
    this.popupFunc.ClosePopup();
    if (this.listFunctions.SelectedIndices[0] < 0)
      return;
    this.curExtraFunc = Convert.ToInt32(this.listFunctions.SelectedItems[0].Tag);
    if (this.curExtraFunc <= 0)
      return;
    if (this.curExtraFunc > 1000)
    {
      if (!((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IExpertServer)) is IExpertServer customService))
        return;
      this.InsertToken(new Token(Intermech.Expert.TokenType.FuncCall, customService.GetFuncData(this.curExtraFunc).text + "(")
      {
        info = this.curExtraFunc
      });
    }
    else
    {
      int funcIndex = ExpertFunc.GetFuncIndex((FormulaFunc) this.curExtraFunc);
      if (funcIndex < 0)
        return;
      this.InsertToken(new Token(Intermech.Expert.TokenType.FuncCall, ExpertFunc.funcs(funcIndex).text + "(")
      {
        info = funcIndex
      });
      this.InsertToken(new Token(")"));
      this.SetCurToken(this.curTokIndex - 1);
    }
  }

  private void popupFunc_QueryDisplayText(object sender, QueryDisplayTextEventArgs e)
  {
    e.DisplayText = LocalizationHolder.rm.GetString("Expert.Editor_139");
  }

  private void popupFunc_QueryResultValue(object sender, QueryResultValueEventArgs e)
  {
    if (this.listFunctions.SelectedItems.Count > 0)
      e.Value = (object) this.listFunctions.SelectedItems[0];
    else
      e.Value = (object) null;
  }

  private void UpdateOTControls(bool enable)
  {
    this.textObjName.Enabled = enable;
    this.ReflectObjType();
  }

  private void checkObjType_CheckedChanged(object sender, EventArgs e)
  {
    if (this.lockChanged)
      return;
    this.UpdateOTControls(this.checkObjType.Checked);
  }

  private void buttonTHIS_MouseEnter(object sender, EventArgs e)
  {
    if (!(sender is Control control))
      return;
    switch (Convert.ToInt32(control.Tag))
    {
      case 1:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("Expert.Editor_140");
        break;
      case 2:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("Expert.Editor_141");
        break;
      case 3:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("Expert.Editor_142");
        break;
      case 4:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("Expert.Editor_143");
        break;
      case 5:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("Expert.Editor_144");
        break;
      case 6:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("Expert.Editor_145");
        break;
      case 7:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("Expert.Editor_146");
        break;
      case 8:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("Expert.Editor_147");
        break;
      case 9:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("Expert.Editor_148");
        break;
      case 10:
      case 11:
      case 12:
      case 13:
      case 14:
      case 15:
      case 16 /*0x10*/:
      case 17:
      case 18:
        this.hintLabel.Text = "";
        break;
      case 19:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("Expert.Editor_149");
        break;
      case 20:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("Expert.Editor_150");
        break;
      case 21:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("Expert.Editor_151");
        break;
      case 22:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("Expert.Editor_152");
        break;
      case 23:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("Expert.Editor_153");
        break;
      case 24:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("Expert.Editor_154");
        break;
      case 25:
      case 26:
        this.hintLabel.Text = "";
        break;
      case 27:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("Expert.Editor_155");
        break;
      case 28:
      case 29:
        this.hintLabel.Text = "";
        break;
      case 30:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("Expert.Editor_156");
        break;
      case 31 /*0x1F*/:
      case 32 /*0x20*/:
        this.hintLabel.Text = "";
        break;
      case 33:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("Expert.Editor_157");
        break;
      case 34:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("Expert.Editor_158");
        break;
      case 35:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("Expert.Editor_576");
        break;
      case 36:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("Expert.Editor_577");
        break;
    }
  }

  private void ClearHint() => this.hintLabel.Text = "";

  private void buttonTHIS_MouseLeave(object sender, EventArgs e) => this.ClearHint();

  private void SetCurToken(int index)
  {
    Token token = (Token) null;
    if (index >= 0 && index < this.tf.Count)
      token = this.tf[index];
    this.curTokIndex = token != null ? index : -1;
    this.ShowFormula(this.tf);
    try
    {
      this.LockEditEnable = true;
      if (token == null)
      {
        this.editAll.Text = "";
      }
      else
      {
        switch (token.type)
        {
          case Intermech.Expert.TokenType.Integer:
          case Intermech.Expert.TokenType.Date:
            this.editAll.Text = token.text;
            break;
          case Intermech.Expert.TokenType.Float:
            if (token.text != "pi" && token.text != "e" && token.text != "-pi" && token.text != "-e")
            {
              this.editAll.Text = token.text;
              break;
            }
            break;
          case Intermech.Expert.TokenType.String:
            this.editAll.Text = token.text;
            break;
          case Intermech.Expert.TokenType.Attribute:
            this.ShowAttr(token.info, Math.Abs(token.fValue - (double) Token._SIGN) < 1E-20);
            this.editAll.Text = "";
            break;
          default:
            this.editAll.Text = "";
            break;
        }
      }
      this.EnableControlButtons();
    }
    finally
    {
      this.LockEditEnable = false;
    }
  }

  private void PaintCurToken(Token t, bool needShift)
  {
    int num = needShift ? 1 : 0;
    if (t.type != Intermech.Expert.TokenType.FuncCall)
      this.memoForm.Select(t.StartPos + num, t.text.Length);
    switch (t.type)
    {
      case Intermech.Expert.TokenType.UnaryOper:
      case Intermech.Expert.TokenType.BinaryOper:
        this.memoForm.SelectionColor = Color.DarkRed;
        break;
      case Intermech.Expert.TokenType.OpeningBrace:
      case Intermech.Expert.TokenType.ClosingBrace:
        this.memoForm.SelectionColor = Color.Blue;
        break;
      case Intermech.Expert.TokenType.FuncCall:
        this.memoForm.Select(t.StartPos + num, t.text.Length - 1);
        this.memoForm.SelectionColor = Color.Black;
        this.memoForm.Select(t.StartPos + num + t.text.Length - 1, 1);
        this.memoForm.SelectionColor = Color.Blue;
        break;
      case Intermech.Expert.TokenType.Integer:
        this.memoForm.SelectionColor = Color.Indigo;
        break;
      case Intermech.Expert.TokenType.Float:
        this.memoForm.SelectionColor = Color.DarkOliveGreen;
        break;
      case Intermech.Expert.TokenType.String:
        this.memoForm.SelectionColor = Color.DarkMagenta;
        break;
      case Intermech.Expert.TokenType.Date:
        this.memoForm.SelectionColor = Color.DarkOrchid;
        break;
      case Intermech.Expert.TokenType.ObjectLink:
        this.memoForm.SelectionColor = Color.Red;
        break;
      default:
        this.memoForm.SelectionColor = Color.Black;
        break;
    }
  }

  private void InsertToken(Token t)
  {
    t.AssignStackInfo();
    int index = this.curTokIndex + 1;
    if (index < 0)
      index = 0;
    this.tf.infixForm.Insert(index, t);
    this.tf.UpdateTokenBegs();
    this.ShowFormula(this.tf);
    this.SetCurToken(index);
    this.fChanged = true;
  }

  private void DelTokenShort(int pos)
  {
    if (pos < 0 || pos >= this.tf.Count)
      return;
    this.SaveCurrent();
    this.tf.infixForm.RemoveAt(pos);
    this.tf.UpdateTokenBegs();
    this.fChanged = true;
  }

  private void DeleteToken(int pos)
  {
    if (pos < 0 || pos >= this.tf.Count)
      return;
    this.SaveCurrent();
    this.tf.infixForm.RemoveAt(pos);
    this.tf.UpdateTokenBegs();
    this.ShowFormula(this.tf);
    if (pos <= this.curTokIndex)
    {
      if (this.curTokIndex >= this.tf.Count)
        this.curTokIndex = this.tf.Count - 1;
      this.SetCurToken(this.curTokIndex);
    }
    else
      this.EnableControlButtons();
    this.fChanged = true;
  }

  private void SelTokenByPos(int pos)
  {
    List<int> intList = new List<int>();
    for (int index = 0; index < this.tf.Count; ++index)
    {
      Token token = this.tf[index];
      intList.Add(token.StartPos);
    }
    if (this.tf.Count > 0)
      intList.Add(this.tf[this.tf.Count - 1].StartPos + this.tf[this.tf.Count - 1].text.Length);
    int num1 = 10000;
    int num2 = -1;
    for (int index = 0; index < intList.Count; ++index)
    {
      int num3 = Math.Abs(pos - intList[index]);
      if (num3 < num1)
      {
        num1 = num3;
        num2 = index;
      }
    }
    if (num2 == 0)
      this.SetCurToken(-1);
    else
      this.SetCurToken(num2 - 1);
  }

  private void memoForm_Enter(object sender, EventArgs e) => this.editAll.Focus();

  private void FormEditor_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (Convert.ToInt32(e.KeyChar) != (int) sbyte.MaxValue)
      return;
    e.Handled = true;
  }

  private void memoForm_MouseUp(object sender, MouseEventArgs e)
  {
    this.SelTokenByPos(this.memoForm.SelectionStart);
  }

  private Token CurToken()
  {
    return this.curTokIndex < 0 || this.curTokIndex >= this.tf.Count ? (Token) null : this.tf[this.curTokIndex];
  }

  private void EnableControlButtons()
  {
    string str = this.editAll.Text.Trim();
    Token token = this.CurToken();
    this.buttonNew.Enabled = str != "" && (token == null || token.type != Intermech.Expert.TokenType.Attribute && token.type != Intermech.Expert.TokenType.Integer && token.type != Intermech.Expert.TokenType.Float && token.type != Intermech.Expert.TokenType.String && token.type != Intermech.Expert.TokenType.Date && token.type != Intermech.Expert.TokenType.ClosingBrace);
    this.buttonEdit.Enabled = token != null && str != "" && (token.type == Intermech.Expert.TokenType.Integer || token.type == Intermech.Expert.TokenType.Float || token.type == Intermech.Expert.TokenType.String || token.type == Intermech.Expert.TokenType.Date);
    this.buttonRef.Enabled = token != null && token.type != Intermech.Expert.TokenType.Attribute && token.type != Intermech.Expert.TokenType.Float && token.type != Intermech.Expert.TokenType.String && token.type != Intermech.Expert.TokenType.Date && token.type != Intermech.Expert.TokenType.ClosingBrace && this.GetAttrId() != -1;
    this.buttonDeshifr.Enabled = this.tf.Count > 0;
    this.buttonFirst.Enabled = this.tf.Count > 0 && this.curTokIndex >= 0;
    this.buttonPrev.Enabled = this.tf.Count > 0 && this.curTokIndex >= 0;
    this.buttonNext.Enabled = this.tf.Count > 0 && this.curTokIndex < this.tf.Count - 1;
    this.buttonLast.Enabled = this.tf.Count > 0 && this.curTokIndex < this.tf.Count - 1;
    this.buttonDel.Enabled = this.tf.Count > 0 && this.curTokIndex < this.tf.Count - 1;
    this.buttonBackspace.Enabled = this.tf.Count > 0 && this.curTokIndex >= 0;
    this.buttonTrash.Enabled = this.tf.Count > 0;
    this.buttonPLUS.Enabled = this.selAttr != null && (token == null || token.type != Intermech.Expert.TokenType.Attribute && token.type != Intermech.Expert.TokenType.Integer && token.type != Intermech.Expert.TokenType.Float && token.type != Intermech.Expert.TokenType.String && token.type != Intermech.Expert.TokenType.Date && token.type != Intermech.Expert.TokenType.ClosingBrace);
    this.buttonUMinus.Enabled = token != null && token.type == Intermech.Expert.TokenType.Attribute;
  }

  private void MoveFirst()
  {
    if (this.curTokIndex == -1)
      return;
    this.SaveCurrent();
    this.SetCurToken(-1);
  }

  private void MovePrev()
  {
    if (this.curTokIndex < 0)
      return;
    this.SaveCurrent();
    this.SetCurToken(this.curTokIndex - 1);
  }

  private void MoveNext()
  {
    if (this.curTokIndex >= this.tf.Count - 1)
      return;
    this.SaveCurrent();
    this.SetCurToken(this.curTokIndex + 1);
  }

  private void MoveLast()
  {
    if (this.curTokIndex >= this.tf.Count - 1)
      return;
    this.SaveCurrent();
    this.SetCurToken(this.tf.Count - 1);
  }

  private void DelCurrent()
  {
    if (this.curTokIndex + 1 < 0 || this.curTokIndex + 1 >= this.tf.Count)
      return;
    this.SaveCurrent();
    this.DeleteToken(this.curTokIndex + 1);
  }

  private void DelPrev()
  {
    if (this.curTokIndex < 0 || this.curTokIndex >= this.tf.Count)
      return;
    this.DelTokenShort(this.curTokIndex);
    if (this.curTokIndex >= this.tf.Count)
      this.curTokIndex = this.tf.Count - 1;
    else
      --this.curTokIndex;
    this.SetCurToken(this.curTokIndex);
    this.ShowFormula(this.tf);
    this.EnableControlButtons();
  }

  private void ClearAll()
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_159"), LocalizationHolder.rm.GetString("Expert.Editor_160"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    this.SaveCurrent();
    this.tf.infixForm.Clear();
    this.curTokIndex = -1;
    this.memoForm.Text = "";
    this.EnableControlButtons();
    this.fChanged = true;
  }

  private void buttonFirst_Click(object sender, EventArgs e) => this.MoveFirst();

  private void buttonPrev_Click(object sender, EventArgs e) => this.MovePrev();

  private void buttonNext_Click(object sender, EventArgs e) => this.MoveNext();

  private void buttonLast_Click(object sender, EventArgs e) => this.MoveLast();

  private void buttonDel_Click(object sender, EventArgs e) => this.DelCurrent();

  private void buttonBackspace_Click(object sender, EventArgs e) => this.DelPrev();

  private void buttonTrash_Click(object sender, EventArgs e) => this.ClearAll();

  private void FormEditor_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.Control)
    {
      switch (e.KeyCode)
      {
        case Keys.Back:
          this.DelPrev();
          e.Handled = true;
          break;
        case Keys.Return:
          if (!this.buttonEdit.Enabled)
            break;
          this.ReplaceLiteral();
          break;
        case Keys.End:
          this.MoveLast();
          e.Handled = true;
          break;
        case Keys.Home:
          this.MoveFirst();
          e.Handled = true;
          break;
        case Keys.Left:
          this.MovePrev();
          e.Handled = true;
          break;
        case Keys.Right:
          this.MoveNext();
          e.Handled = true;
          break;
        case Keys.Delete:
          this.DelCurrent();
          e.Handled = true;
          break;
      }
    }
    else
    {
      switch (e.KeyCode)
      {
        case Keys.Return:
          if (!this.buttonNew.Enabled)
            break;
          this.InsertLiteral();
          break;
        case Keys.F3:
          this.buttonDeshifr_Click(sender, (EventArgs) null);
          break;
      }
    }
  }

  private void editAll_TextChanged(object sender, EventArgs e)
  {
    if (this.LockEditEnable)
      return;
    this.EnableControlButtons();
  }

  private void checkString_CheckedChanged(object sender, EventArgs e)
  {
    this.EnableControlButtons();
  }

  private void FormEditor_Closed(object sender, EventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void FormEditor_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void buttonDeshifr_Click(object sender, EventArgs e)
  {
    new DeshifrForm().Execute(this.tf);
  }

  private void btnCopy_Click(object sender, EventArgs e)
  {
    if (this.tf == null)
      return;
    FormulaEditPlugin.CopyToClipboard(this.tf);
  }

  private void btnPaste_Click(object sender, EventArgs e)
  {
    if (this.tf == null || !Clipboard.ContainsData(TempFormula.FormulaFormat))
      return;
    DataType resType = this.tf.resType;
    FormulaEditPlugin.PasteFromClipboard(this.tf);
    this.tf.resType = resType;
    this.tf.UpdateTokenBegs();
    this.ShowFormula(this.tf);
    if (this.tf.Count > 0)
      this.SetCurToken(0);
    else
      this.SetCurToken(-1);
    this.curExtraFunc = -1;
    this.popupFunc.Text = "";
    this.fChanged = true;
  }

  private void buttonPi_Click(object sender, EventArgs e)
  {
    if (!this.ValidateInput(FormEditor.ValidateType.NotAfterOperand))
      return;
    int int32 = Convert.ToInt32((sender as SimpleButton).Tag);
    Token t = new Token(Intermech.Expert.TokenType.Float, int32 == 22 ? "pi" : nameof (e));
    t.fValue = int32 != 22 ? Math.E : Math.PI;
    this.SaveCurrent();
    this.InsertToken(t);
  }

  private void button22_Click(object sender, EventArgs e)
  {
    if (!this.ValidateInput(FormEditor.ValidateType.NotAfterOperator) || !this.ValidateInput(FormEditor.ValidateType.NotAfterDivider) || !this.ValidateInput(FormEditor.ValidateType.NotAfterOpenBrace) || !this.ValidateInput(FormEditor.ValidateType.NotFirst))
      return;
    int int32 = Convert.ToInt32((sender as SimpleButton).Tag);
    string text = "";
    switch (int32)
    {
      case 10:
        text = " = ";
        break;
      case 11:
        text = " < ";
        break;
      case 12:
        text = " <= ";
        break;
      case 13:
        text = " <> ";
        break;
      case 14:
        text = " > ";
        break;
      case 15:
        text = " >= ";
        break;
      case 16 /*0x10*/:
        text = $" {LocalizationHolder.rm.GetString("Expert.Editor_161")} ";
        break;
      case 17:
        text = $" {LocalizationHolder.rm.GetString("Expert.Editor_162")} ";
        break;
      case 25:
        text = " + ";
        break;
      case 26:
        text = " - ";
        break;
      case 27:
        text = " ^ ";
        break;
      case 28:
        text = " * ";
        break;
      case 29:
        text = " / ";
        break;
    }
    if (!(text != ""))
      return;
    this.SaveCurrent();
    this.InsertToken(new Token(Intermech.Expert.TokenType.BinaryOper, text));
  }

  private void buttonUMinus_Click(object sender, EventArgs e)
  {
    if (!this.ValidateInput(FormEditor.ValidateType.OnlyAfterAttribute))
      return;
    Token token = this.CurToken();
    if (token == null || token.type != Intermech.Expert.TokenType.Attribute)
      return;
    this.SaveCurrent();
    this.tf.infixForm.Insert(this.curTokIndex + 1, new Token(Intermech.Expert.TokenType.UnaryOper, "->")
    {
      info = Token.RightAssoc
    });
    this.fChanged = true;
    this.tf.UpdateTokenBegs();
    this.ShowFormula(this.tf);
    this.SetCurToken(this.curTokIndex + 1);
  }

  private void buttonNot_Click(object sender, EventArgs e)
  {
    if (!this.ValidateInput(FormEditor.ValidateType.NotAfterOperand))
      return;
    this.SaveCurrent();
    this.InsertToken(new Token(Intermech.Expert.TokenType.UnaryOper, LocalizationHolder.rm.GetString("Expert.Editor_163")));
  }

  private void buttonSet_Click(object sender, EventArgs e)
  {
    if (!this.ValidateInput(FormEditor.ValidateType.NotAfterDivider) || !this.ValidateInput(FormEditor.ValidateType.NotAfterOpenBrace) || !this.ValidateInput(FormEditor.ValidateType.NotAfterOperator))
      return;
    this.SaveCurrent();
    this.InsertToken(new Token(Intermech.Expert.TokenType.BinaryOper, "?"));
  }

  private void buttonDiap_Click(object sender, EventArgs e)
  {
    if (!this.ValidateInput(FormEditor.ValidateType.OnlyAfterOperand))
      return;
    this.SaveCurrent();
    this.InsertToken(new Token(Intermech.Expert.TokenType.BinaryOper, ":"));
  }

  private void buttonComma_Click(object sender, EventArgs e)
  {
    if (!this.ValidateInput(FormEditor.ValidateType.NotAfterDivider) || !this.ValidateInput(FormEditor.ValidateType.NotAfterOpenBrace) || !this.ValidateInput(FormEditor.ValidateType.NotAfterOperator))
      return;
    this.SaveCurrent();
    this.InsertToken(new Token(Intermech.Expert.TokenType.Divider, ", "));
  }

  private void button28_Click(object sender, EventArgs e)
  {
    int int32 = Convert.ToInt32((sender as SimpleButton).Tag);
    switch (int32)
    {
      case 32 /*0x20*/:
      case 34:
        if (!this.ValidateInput(FormEditor.ValidateType.NotFirst))
          return;
        break;
    }
    Token t = (Token) null;
    switch (int32 - 31 /*0x1F*/)
    {
      case 0:
        t = new Token(Intermech.Expert.TokenType.OpeningBrace, "(");
        break;
      case 1:
        t = new Token(Intermech.Expert.TokenType.ClosingBrace, ")");
        break;
      case 2:
        t = new Token(Intermech.Expert.TokenType.OpeningBrace, "{");
        break;
      case 3:
        t = new Token(Intermech.Expert.TokenType.ClosingBrace, "}");
        break;
      case 6:
        t = new Token(Intermech.Expert.TokenType.OpeningBrace, "[");
        break;
      case 7:
        t = new Token(Intermech.Expert.TokenType.ClosingBrace, "]");
        break;
      case 10:
        t = new Token(Intermech.Expert.TokenType.String, "<CR>");
        break;
    }
    this.SaveCurrent();
    this.InsertToken(t);
  }

  private void button3_Click(object sender, EventArgs e)
  {
    if (!this.ValidateInput(FormEditor.ValidateType.NotAfterOperand))
      return;
    int funcIndex = ExpertFunc.GetFuncIndex((FormulaFunc) Convert.ToInt32((sender as SimpleButton).Tag));
    if (funcIndex < 0)
      return;
    Token t = new Token(Intermech.Expert.TokenType.FuncCall, ExpertFunc.funcs(funcIndex).text + "(");
    t.info = funcIndex;
    this.SaveCurrent();
    this.InsertToken(t);
  }

  private void btnInsertAttr_Click(object sender, EventArgs e)
  {
    if (!this.ValidateInput(FormEditor.ValidateType.NotAfterOperand))
      return;
    if (this.selObjType == null && this.checkObjType.Checked)
    {
      int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_164"), LocalizationHolder.rm.GetString("Expert.Editor_165"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else if (this.selAttr == null)
    {
      int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_166"), LocalizationHolder.rm.GetString("Expert.Editor_167"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
    {
      int index1 = -1;
      for (int index2 = 0; index2 < this.tf.usedAttrs.Count; ++index2)
      {
        AttribPair usedAttr = this.tf.usedAttrs[index2];
        if (usedAttr.attribID == this.selAttr.ID && ((this.selObjType == null || !this.checkObjType.Checked) && usedAttr.objTypeID == 0 || this.selObjType != null && this.checkObjType.Checked && usedAttr.objTypeID == this.selObjType.ID))
        {
          index1 = index2;
          break;
        }
      }
      if (index1 < 0)
      {
        if (this.attrType == FieldTypes.ftUnknown)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            this.attrType = sessionKeeper.Session.GetAttributeType(this.selAttr.ID).AttributeType;
        }
        AttribPair attribPair;
        PairName pairName;
        if (this.selObjType != null && this.checkObjType.Checked)
        {
          attribPair = new AttribPair(this.selAttr.ID, this.selObjType.ID);
          pairName = new PairName(this.selAttr.shortName, this.selAttr.longName, this.selObjType.shortName, this.selObjType.longName, this.attrType, this.multi);
        }
        else
        {
          attribPair = new AttribPair(this.selAttr.ID);
          pairName = new PairName(this.selAttr.shortName, this.selAttr.longName, "", "", this.attrType, this.multi);
        }
        this.tf.usedAttrs.Add(attribPair);
        this.tf.pairNames.Add(pairName);
        this.tf.attrGUIDs.Add(this.selAttr.GUID);
        this.tf.objTypeGUIDs.Add(this.selObjType == null || !this.checkObjType.Checked ? "" : this.selObjType.GUID);
        index1 = this.tf.usedAttrs.Count - 1;
      }
      Token t = new Token(Intermech.Expert.TokenType.Attribute, this.tf.pairNames[index1].ShortName);
      t.info = index1;
      if (this.cbAllowUnknown.Checked)
      {
        t.fValue = (double) Token._SIGN;
        t.text = "#" + t.text;
      }
      PairName pairName1 = this.tf.pairNames[index1];
      int num3 = this.BSearch(pairName1, this.comboAttr.Items.Count, this.cb_Compare);
      if (num3 >= this.comboAttr.Items.Count || this.cb_Compare(num3, ref pairName1) != 0)
      {
        FormEditor.EditComboItem editComboItem = this.selObjType == null || !this.checkObjType.Checked ? new FormEditor.EditComboItem(this.tf.usedAttrs[index1], pairName1, this.selAttr.GUID, "") : new FormEditor.EditComboItem(this.tf.usedAttrs[index1], pairName1, this.selAttr.GUID, this.selObjType.GUID);
        this.comboAttr.BeginUpdate();
        try
        {
          this.comboAttr.Items.Insert(num3, (object) editComboItem);
        }
        finally
        {
          this.comboAttr.EndUpdate();
        }
      }
      this.SaveCurrent();
      this.InsertToken(t);
    }
  }

  private bool ValidateInput(FormEditor.ValidateType vt)
  {
    Token token = this.CurToken();
    if (token == null)
    {
      if (vt != FormEditor.ValidateType.NotFirst || this.tf.Count != 0)
        return true;
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_168"), LocalizationHolder.rm.GetString("Expert.Editor_169"), MessageBoxButtons.OK);
      return false;
    }
    switch (vt)
    {
      case FormEditor.ValidateType.NotAfterOperator:
        if (token.type != Intermech.Expert.TokenType.UnaryOper && token.type != Intermech.Expert.TokenType.BinaryOper)
          return true;
        int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_170"), LocalizationHolder.rm.GetString("Expert.Editor_171"), MessageBoxButtons.OK);
        return false;
      case FormEditor.ValidateType.NotAfterOperand:
        if (token.type != Intermech.Expert.TokenType.ClosingBrace && token.type != Intermech.Expert.TokenType.Integer && token.type != Intermech.Expert.TokenType.Float && token.type != Intermech.Expert.TokenType.String && token.type != Intermech.Expert.TokenType.Date && token.type != Intermech.Expert.TokenType.Attribute && token.type != Intermech.Expert.TokenType.ObjectLink)
          return true;
        int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_172"), LocalizationHolder.rm.GetString("Expert.Editor_173"), MessageBoxButtons.OK);
        return false;
      case FormEditor.ValidateType.NotAfterDivider:
        if (token.type != Intermech.Expert.TokenType.Divider)
          return true;
        int num3 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_174"), LocalizationHolder.rm.GetString("Expert.Editor_175"), MessageBoxButtons.OK);
        return false;
      case FormEditor.ValidateType.NotAfterOpenBrace:
        if (token.type != Intermech.Expert.TokenType.OpeningBrace && token.type != Intermech.Expert.TokenType.FuncCall)
          return true;
        int num4 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_176"), LocalizationHolder.rm.GetString("Expert.Editor_177"), MessageBoxButtons.OK);
        return false;
      case FormEditor.ValidateType.OnlyAfterOperand:
        if (token.type == Intermech.Expert.TokenType.Integer || token.type == Intermech.Expert.TokenType.Float || token.type == Intermech.Expert.TokenType.String || token.type == Intermech.Expert.TokenType.Date)
          return true;
        int num5 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_178"), LocalizationHolder.rm.GetString("Expert.Editor_179"), MessageBoxButtons.OK);
        return false;
      case FormEditor.ValidateType.OnlyAfterAttribute:
        if (token.type == Intermech.Expert.TokenType.Attribute)
          return true;
        int num6 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_178"), LocalizationHolder.rm.GetString("Expert.Editor_545"), MessageBoxButtons.OK);
        return false;
      default:
        return true;
    }
  }

  private Token GetNewToken(string s)
  {
    Token newToken = new Token(Intermech.Expert.TokenType.String, s);
    newToken.InitLiteral(s);
    return newToken;
  }

  private void InsertLiteral()
  {
    if (!this.ValidateInput(FormEditor.ValidateType.NotAfterOperand))
      return;
    this.SaveCurrent();
    this.InsertToken(this.GetNewToken(this.editAll.Text));
  }

  private void ReplaceLiteral()
  {
    this.SaveCurrent();
    Token newToken = this.GetNewToken(this.editAll.Text);
    this.tf.infixForm.RemoveAt(this.curTokIndex--);
    this.InsertToken(newToken);
  }

  private void buttonNew_Click(object sender, EventArgs e) => this.InsertLiteral();

  private void buttonEdit_Click(object sender, EventArgs e) => this.ReplaceLiteral();

  internal bool PerformObjType(int objType)
  {
    if (this.selObjType != null && this.selObjType.ID == objType)
      return false;
    if (this.selObjType == null)
      this.selObjType = new SelFormResult();
    this.selObjType.ID = objType;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(this.selObjType.ID);
      if (objectType == null)
        return false;
      this.selObjType.GUID = objectType.PropertiesStructure.ObjectTypeGuid.ToString();
      this.selObjType.longName = objectType.PropertiesStructure.ObjectTypeName;
      this.selObjType.shortName = objectType.PropertiesStructure.ObjectTypeShortName;
    }
    this.ReflectObjType();
    return true;
  }

  private void textObjName_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (this.SelObjType != null)
    {
      SelTypeEventArgs e1 = new SelTypeEventArgs(this.selObjType, this.textObjName.Text.Trim(), this.textAttName.Text.Trim());
      FormEditor.SelTypeEventHandler selObjType1 = this.SelObjType;
      FormEditor.SelTypeEventHandler selObjType2 = this.SelObjType;
      SelFormResult selFormResult = selObjType2 != null ? selObjType2((object) this, e1) : (SelFormResult) null;
      if (selFormResult == null)
        return;
      this.selObjType = selFormResult;
      this.ReflectObjType();
    }
    else
    {
      int selectID = 0;
      if (this.selObjType != null)
        selectID = this.selObjType.ID;
      AdvSelectorForm advSelectorForm = new AdvSelectorForm(AdvSelector.AttributableType, AttributableElements.Object, -1, selectID);
      if (advSelectorForm.ShowDialog() != DialogResult.OK)
        return;
      this.PerformObjType(advSelectorForm.ObjectType);
    }
  }

  private void btnObjTree_Click(object sender, EventArgs e)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Expert.Editor_180"), typeof (ObjectTypeFolder), false);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int int32 = Convert.ToInt32(selectorForm.IDList[0]);
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(int32);
      if (objectType == null)
        return;
      ObjectTypeProperties propertiesStructure = objectType.PropertiesStructure;
      if (this.selObjType == null)
        this.selObjType = new SelFormResult();
      this.selObjType.ID = int32;
      this.selObjType.GUID = propertiesStructure.ObjectTypeGuid.ToString();
      this.selObjType.longName = propertiesStructure.ObjectInstanceName;
      if (this.selObjType.longName == "")
        this.selObjType.longName = propertiesStructure.ObjectTypeName;
      this.selObjType.shortName = propertiesStructure.ObjectTypeShortName;
      this.ReflectObjType();
    }
  }

  private void ReflectObjType()
  {
    if (!this.checkObjType.Checked || this.selObjType == null)
      this.textObjName.Text = "";
    else if (this.selObjType.shortName != "")
      this.textObjName.Text = $"[{this.selObjType.shortName}] {this.selObjType.longName}";
    else
      this.textObjName.Text = this.selObjType.longName;
  }

  private void textAttName_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (this.SelAttrType != null)
    {
      string cAttrType = this.textAttName.Text.Trim();
      SelTypeEventArgs e1 = new SelTypeEventArgs(this.selAttr, this.textObjName.Text.Trim(), cAttrType);
      FormEditor.SelTypeEventHandler selAttrType = this.SelAttrType;
      SelFormResult selFormResult = selAttrType != null ? selAttrType((object) this, e1) : (SelFormResult) null;
      if (selFormResult != null)
      {
        this.selAttr = selFormResult;
        this.ReflectAttr();
      }
    }
    else
    {
      int num = -1;
      int selectID = -1;
      if (this.selAttr != null)
        num = this.selAttr.ID;
      if (this.checkObjType.Checked && this.selObjType != null)
        selectID = this.selObjType.ID;
      bool flag1 = false;
      AdvSelectorForm advSelectorForm;
      if (selectID == -1)
      {
        if (num == -1)
          advSelectorForm = new AdvSelectorForm(AdvSelector.AttributableTypeWithAttributeType, AttributableElements.Object);
        else
          advSelectorForm = new AdvSelectorForm(AttributableElements.Object, -1, -1, new int[1]
          {
            num
          });
      }
      else
      {
        if (num == -1)
          advSelectorForm = new AdvSelectorForm(AdvSelector.AttributableTypeWithAttributeType, AttributableElements.Object, -1, selectID);
        else
          advSelectorForm = new AdvSelectorForm(AttributableElements.Object, -1, selectID, new int[1]
          {
            num
          });
        flag1 = true;
      }
      if (advSelectorForm.ShowDialog() == DialogResult.OK)
      {
        bool flag2 = false;
        this.lockChanged = true;
        try
        {
          bool flag3;
          if (flag1)
          {
            flag3 = flag2 || this.PerformObjType(advSelectorForm.ObjectType);
          }
          else
          {
            flag3 = advSelectorForm.ObjectType != -1;
            if (flag3)
              this.PerformObjType(advSelectorForm.ObjectType);
            if (this.selObjType != null && advSelectorForm.ObjectType == -1)
              this.selObjType = (SelFormResult) null;
          }
          if (this.selAttr == null)
            this.selAttr = new SelFormResult();
          bool flag4 = this.PerformAttrType(advSelectorForm.AttributeTypes[0]) | flag3;
        }
        finally
        {
          this.lockChanged = false;
        }
      }
    }
    Token token = this.CurToken();
    this.buttonPLUS.Enabled = this.selAttr != null && (token == null || token.type != Intermech.Expert.TokenType.Attribute && token.type != Intermech.Expert.TokenType.Integer && token.type != Intermech.Expert.TokenType.Float && token.type != Intermech.Expert.TokenType.String && token.type != Intermech.Expert.TokenType.Date && token.type != Intermech.Expert.TokenType.ClosingBrace);
    this.cbAllowUnknown.Checked = false;
  }

  internal bool PerformAttrType(int attrId)
  {
    if (this.selAttr != null && this.selAttr.ID == attrId && this.attrType == FieldTypes.ftUnknown)
      return false;
    if (this.selAttr == null)
      this.selAttr = new SelFormResult();
    this.selAttr.ID = attrId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(this.selAttr.ID);
      if (attributeType == null)
        return false;
      this.selAttr.GUID = attributeType.PropertiesStructure.AttributeGuid.ToString();
      this.selAttr.longName = attributeType.Name;
      this.selAttr.shortName = attributeType.ShortName;
    }
    this.ReflectAttr();
    return true;
  }

  private void ReflectAttr()
  {
    if (this.selAttr == null)
    {
      this.textAttName.Text = "";
      this.AttrTypeLbl.Text = "";
    }
    else
    {
      if (this.selAttr.shortName != "")
        this.textAttName.Text = $"[{this.selAttr.shortName}] {this.selAttr.longName}";
      else
        this.textAttName.Text = this.selAttr.longName;
      if (this.selObjType == null || this.selObjType.ID == -1)
      {
        this.checkObjType.Checked = false;
        this.textObjName.Text = "";
      }
      else
        this.checkObjType.Checked = true;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        string str = "";
        if (this.selAttr != null)
        {
          IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(this.selAttr.ID);
          this.attrType = attributeType.AttributeType;
          str = PairName.GetLongFTDescr(this.attrType);
          this.multi = attributeType.MultipleValued == MultiValueModes.MultiValues || attributeType.MultipleValued == MultiValueModes.MultiValuesFromList;
          if (this.multi)
            str = $"{LocalizationHolder.rm.GetString("Expert.Editor_544")}{str}}}";
        }
        this.AttrTypeLbl.Text = str;
      }
    }
  }

  private void button2_Click(object sender, EventArgs e)
  {
    this.selAttr = (SelFormResult) null;
    this.selObjType = (SelFormResult) null;
    this.textObjName.Text = "";
    this.textAttName.Text = "";
  }

  private void FillArgCombo(TempFormula tf)
  {
    if (tf.usedAttrs == null)
      return;
    this.comboAttr.BeginUpdate();
    try
    {
      for (int index = 0; index < tf.usedAttrs.Count; ++index)
      {
        AttribPair ap = (AttribPair) tf.usedAttrs[index].Clone();
        PairName pairName = tf.pairNames[index];
        if (MetaDataHelper.GetAttributeTypeGuid(ap.attribID).Equals(Guid.Empty))
          ap.attribID = MetaDataHelper.GetAttributeByTypeNameID(pairName.attrLongName);
        int num = this.BSearch(pairName, this.comboAttr.Items.Count, this.cb_Compare);
        if (num >= this.comboAttr.Items.Count || this.cb_Compare(num, ref pairName) != 0)
        {
          FormEditor.EditComboItem editComboItem = new FormEditor.EditComboItem(ap, pairName, tf.attrGUIDs[index], tf.objTypeGUIDs[index]);
          this.comboAttr.Items.Insert(num, (object) editComboItem);
        }
      }
    }
    finally
    {
      this.comboAttr.EndUpdate();
    }
  }

  private void ShowAttr(int Index, bool allowUnknown = false)
  {
    AttribPair usedAttr = this.tf.usedAttrs[Index];
    PairName pairName = this.tf.pairNames[Index];
    if (pairName.attrShortName != "")
      this.textAttName.Text = $"[{pairName.attrShortName}] {pairName.attrLongName}";
    else
      this.textAttName.Text = pairName.attrLongName;
    if (pairName.objTypeShortName != "")
      this.textObjName.Text = $"[{pairName.objTypeShortName}] {pairName.objTypeLongName}";
    this.AttrTypeLbl.Text = pairName.GetShortTypeDescr();
    if (this.selAttr == null)
      this.selAttr = new SelFormResult();
    this.selAttr.ID = usedAttr.attribID;
    this.selAttr.GUID = this.tf.attrGUIDs[Index];
    this.selAttr.shortName = pairName.attrShortName;
    this.selAttr.longName = pairName.attrLongName;
    if (this.selObjType == null)
      this.selObjType = new SelFormResult();
    this.selObjType.ID = usedAttr.objTypeID;
    this.selObjType.GUID = this.tf.objTypeGUIDs[Index];
    this.selObjType.shortName = pairName.objTypeShortName;
    this.selObjType.longName = pairName.objTypeLongName;
    this.checkObjType.Checked = usedAttr.objTypeID != -1;
    if (this.checkObjType.Checked)
      this.textObjName.Text = $"[{this.selObjType.shortName}] {this.selObjType.longName}";
    this.cbAllowUnknown.Checked = allowUnknown;
    int Index1 = this.BSearch(pairName, this.comboAttr.Items.Count, this.cb_Compare);
    if (Index1 < 0 || Index1 >= this.comboAttr.Items.Count || this.cb_Compare(Index1, ref pairName) != 0)
      this.comboAttr.SelectedIndex = -1;
    else
      this.comboAttr.SelectedIndex = Index1;
  }

  private int ArgComboCompare(int Index, ref PairName pn)
  {
    PairName pn1 = ((FormEditor.EditComboItem) this.comboAttr.Items[Index]).pn;
    string strA1 = pn1.objTypeShortName;
    if (strA1 == "")
      strA1 = pn1.objTypeLongName;
    string strB1 = pn.objTypeShortName;
    if (strB1 == "")
      strB1 = pn.objTypeLongName;
    int num = string.Compare(strA1, strB1);
    if (num != 0)
      return num;
    string strA2 = pn1.attrShortName;
    if (strA2 == "")
      strA2 = pn1.attrLongName;
    string strB2 = pn.attrShortName;
    if (strB2 == "")
      strB2 = pn.attrLongName;
    return string.Compare(strA2, strB2);
  }

  private int BSearch(PairName pn, int Count, FormEditor.Compare compare)
  {
    if (Count == 0)
      return 0;
    int num1 = 0;
    int num2 = Count;
    while (num1 < num2)
    {
      int Index = (num1 + num2) / 2;
      int num3 = compare(Index, ref pn);
      if (num3 == 0)
        return Index;
      if (num3 < 0)
        num1 = Index + 1;
      else
        num2 = Index;
    }
    return num1;
  }

  private void AddComboItem(FormEditor.EditComboItem ecbI)
  {
    int num = this.BSearch(ecbI.pn, this.comboAttr.Items.Count, this.cb_Compare);
    if (num < this.comboAttr.Items.Count && this.cb_Compare(num, ref ecbI.pn) == 0)
      return;
    this.comboAttr.Items.Insert(num, (object) ecbI);
  }

  private void comboAttr_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.comboAttr.SelectedIndex < 0)
      return;
    FormEditor.EditComboItem editComboItem = (FormEditor.EditComboItem) this.comboAttr.Items[this.comboAttr.SelectedIndex];
    if (this.selAttr != null && this.selObjType != null && this.selAttr.ID == editComboItem.ap.attribID && this.selObjType.ID == editComboItem.ap.objTypeID)
      return;
    if (this.selAttr == null)
      this.selAttr = new SelFormResult();
    if (this.selObjType == null)
      this.selObjType = new SelFormResult();
    this.selAttr.GUID = editComboItem.attrGUID;
    this.selAttr.ID = editComboItem.ap.attribID;
    this.selAttr.shortName = editComboItem.pn.attrShortName;
    this.selAttr.longName = editComboItem.pn.attrLongName;
    this.selObjType.GUID = editComboItem.objTypeGUID;
    this.selObjType.ID = editComboItem.ap.objTypeID;
    this.selObjType.shortName = editComboItem.pn.objTypeShortName;
    this.selObjType.longName = editComboItem.pn.objTypeLongName;
    this.ReflectAttr();
    if (this.selObjType.shortName != "")
      this.textObjName.Text = $"[{this.selObjType.shortName}] {this.selObjType.longName}";
    else
      this.textObjName.Text = this.selObjType.longName;
    Token token = this.CurToken();
    this.buttonPLUS.Enabled = this.selAttr != null && (token == null || token.type != Intermech.Expert.TokenType.Attribute && token.type != Intermech.Expert.TokenType.Integer && token.type != Intermech.Expert.TokenType.Float && token.type != Intermech.Expert.TokenType.String && token.type != Intermech.Expert.TokenType.Date && token.type != Intermech.Expert.TokenType.ClosingBrace);
  }

  private void btnData_Click(object sender, EventArgs e) => this.ShowCalendar();

  private void btnMeasured_Click(object sender, EventArgs e)
  {
    if (!this.ValidateInput(FormEditor.ValidateType.NotAfterOperand))
      return;
    MeasureForm measureForm = new MeasureForm();
    MeasuredValue measuredValue = (MeasuredValue) null;
    ref MeasuredValue local = ref measuredValue;
    MeasureDescriptor[] measures = MeasureHelper.Measures;
    if (measureForm.ExecuteDialog(ref local, measures) != DialogResult.OK)
      return;
    this.InsertToken(new Token(Intermech.Expert.TokenType.Measured, measuredValue.ToString())
    {
      fValue = measuredValue.Value,
      iValue = measuredValue.MeasureID
    });
  }

  private int GetAttrId()
  {
    int attrId = -1;
    int index = 0;
    int curTokIndex = this.curTokIndex;
    if (curTokIndex < this.tf.Count - 1 && this.tf[curTokIndex + 1].type == Intermech.Expert.TokenType.OpeningBrace)
      ++curTokIndex;
    for (; curTokIndex >= 0; --curTokIndex)
    {
      Token token = this.tf[curTokIndex];
      if (token.type == Intermech.Expert.TokenType.Attribute)
      {
        attrId = this.tf.usedAttrs[token.info].attribID;
        break;
      }
      if (token.type != Intermech.Expert.TokenType.ClosingBrace)
      {
        if (token.type == Intermech.Expert.TokenType.Divider && token.text[0] == ',')
          ++index;
        if (token.type == Intermech.Expert.TokenType.FuncCall)
        {
          FuncData funcData = ExpertFunc.real_funcs(token.info);
          if (funcData.parmTypes.Length > index)
          {
            if (funcData.parmTypes[index] == DataType.ObjType)
              attrId = ExpertConsts.Consts.sysAttrObjType;
            else if (funcData.parmTypes[index] == DataType.RelType)
              attrId = ExpertConsts.Consts.sysAttrRelType;
          }
        }
      }
      else
        break;
    }
    return attrId;
  }

  private void buttonRef_Click(object sender, EventArgs e)
  {
    List<long> refIds = new List<long>();
    int num1 = -1;
    int num2 = -1;
    int num3 = -1;
    int num4 = -1;
    int rightToken = -1;
    int index1 = 0;
    Token token1 = (Token) null;
    int curTokIndex1 = this.curTokIndex;
    if (curTokIndex1 < this.tf.Count - 1 && this.tf[curTokIndex1 + 1].type == Intermech.Expert.TokenType.OpeningBrace)
      ++curTokIndex1;
    if (curTokIndex1 >= 0 && curTokIndex1 < this.tf.Count)
      token1 = this.tf[curTokIndex1];
    bool Multi = false;
    for (; curTokIndex1 >= 0; --curTokIndex1)
    {
      Token token2 = this.tf[curTokIndex1];
      if (token2.type == Intermech.Expert.TokenType.Attribute)
      {
        AttribPair usedAttr = this.tf.usedAttrs[token2.info];
        num1 = usedAttr.objTypeID;
        num2 = usedAttr.attribID;
        break;
      }
      if (token2.type == Intermech.Expert.TokenType.OpeningBrace && token2.text == "{")
      {
        num4 = curTokIndex1;
        for (int curTokIndex2 = this.curTokIndex; curTokIndex2 < this.tf.Count; ++curTokIndex2)
        {
          if (this.tf[curTokIndex2].type == Intermech.Expert.TokenType.ClosingBrace && this.tf[curTokIndex2].text == "}")
          {
            rightToken = curTokIndex2;
            for (int index2 = curTokIndex1 + 1; index2 < curTokIndex2; ++index2)
            {
              Token token3 = this.tf[index2];
              if ((token3.type == Intermech.Expert.TokenType.Integer || token3.type == Intermech.Expert.TokenType.String) && !refIds.Contains(token3.iValue))
                refIds.Add(token3.iValue);
            }
            break;
          }
        }
      }
      if (token2.type != Intermech.Expert.TokenType.ClosingBrace)
      {
        if (token2.type == Intermech.Expert.TokenType.BinaryOper && (token2.text.Trim() == "?" || token2.text.Trim() == "=" || token2.text.Trim() == "<>"))
        {
          num3 = curTokIndex1;
          Multi = token2.text.Trim() == "?";
        }
        if ((token2.type == Intermech.Expert.TokenType.Integer || token2.type == Intermech.Expert.TokenType.String) && num4 < 0)
        {
          num4 = curTokIndex1;
          rightToken = curTokIndex1;
          refIds.Add(token2.iValue);
        }
        if (token2.type == Intermech.Expert.TokenType.Divider && token2.text[0] == ',')
          ++index1;
        if (token2.type == Intermech.Expert.TokenType.FuncCall)
        {
          FuncData funcData = ExpertFunc.real_funcs(token2.info);
          if (funcData.parmTypes.Length >= index1)
          {
            if (funcData.parmTypes[index1] == DataType.ObjType)
              num2 = ExpertConsts.Consts.sysAttrObjType;
            else if (funcData.parmTypes[index1] == DataType.RelType)
              num2 = ExpertConsts.Consts.sysAttrRelType;
            if (num2 != -1 && this.curTokIndex >= 0 && token1.type == Intermech.Expert.TokenType.Integer && token1.trueText != token1.text)
            {
              refIds.Add(token1.iValue);
              break;
            }
            break;
          }
          break;
        }
      }
      else
        break;
    }
    MeasuredValue aMeasureValue = (MeasuredValue) null;
    if (refIds.Count == 0 && this.curTokIndex < this.tf.Count - 1)
    {
      Token token4 = this.tf[this.curTokIndex + 1];
      if (token4.type == Intermech.Expert.TokenType.Integer || token4.type == Intermech.Expert.TokenType.String && !refIds.Contains(token4.iValue))
      {
        refIds.Add(token4.iValue);
        num4 = rightToken = this.curTokIndex + 1;
      }
      if (token4.type == Intermech.Expert.TokenType.Measured)
        aMeasureValue = new MeasuredValue(token4.fValue, token4.iValue, token4.text);
    }
    if (num2 == -1)
    {
      int num5 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_590"), LocalizationHolder.rm.GetString("Expert.Editor_63"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    else
    {
      bool flag1 = false;
      using (SessionKeeper sessionKeeper1 = new SessionKeeper())
      {
        try
        {
          IDBAttributeType attributeType = sessionKeeper1.Session.GetAttributeType(num2);
          if (attributeType == null)
            return;
          if (FormEditor.UserAttrs.ua.ContainsKey((object) num2))
          {
            FormEditor.UserAttrItem userAttrItem = (FormEditor.UserAttrItem) FormEditor.UserAttrs.ua[(object) num2];
            VListSelect vlistSelect = new VListSelect();
            flag1 = true;
            if (!vlistSelect.Execute(userAttrItem.possibleValues, refIds, attributeType.Name, userAttrItem.multiSelect))
              return;
            List<long> Indices = new List<long>();
            List<string> results = vlistSelect.GetResults(out Indices);
            this.InsDefValues(Indices, results, (List<string>) null, Intermech.Expert.TokenType.String, SelectionParameterTypes.sptBlob, num4, rightToken, num3);
          }
          else
          {
            DataRow[] possibleValuesRows = attributeType.GetPossibleValuesRows();
            if (possibleValuesRows.Length != 0)
            {
              VListSelect vlistSelect = new VListSelect();
              flag1 = true;
              if (vlistSelect.Execute(possibleValuesRows, attributeType.Name, Multi, refIds))
              {
                List<long> Indices = new List<long>();
                List<string> results = vlistSelect.GetResults(out Indices);
                if (attributeType.AttributeType != FieldTypes.ftInteger)
                {
                  this.InsDefValues(Indices, results, (List<string>) null, Intermech.Expert.TokenType.String, SelectionParameterTypes.sptBlob, num4, rightToken, num3);
                }
                else
                {
                  if (num4 >= 0)
                  {
                    if (rightToken < 0)
                      rightToken = this.tf.Count - 1;
                    for (int index3 = rightToken - num4; index3 >= 0; --index3)
                      this.tf.infixForm.RemoveAt(num4);
                  }
                  List<long> longList = new List<long>((IEnumerable<long>) Indices);
                  if (longList.Count > 0)
                  {
                    int num6 = longList.Count != 1 ? 0 : (num3 < 0 ? 1 : (this.tf[num3].text.Trim() == "=" ? 1 : 0));
                    bool flag2 = longList.Count == 1 && (num3 < 0 || this.tf[num3].text.Trim() == "<>");
                    if (num3 >= 0 && longList.Count > 1)
                      this.tf[num3].text = "?";
                    if (num4 < 0)
                      num4 = this.curTokIndex + 1;
                    int num7 = flag2 ? 1 : 0;
                    if ((num6 | num7) != 0)
                    {
                      Token token5 = new Token(Intermech.Expert.TokenType.Integer, results[0]);
                      token5.iValue = Convert.ToInt64(longList[0]);
                      token5._Guid = "";
                      token5.spt = SelectionParameterTypes.sptNumber;
                      token5.AssignStackInfo();
                      this.tf.infixForm.Insert(num4, token5);
                    }
                    else
                    {
                      Token token6 = new Token(Intermech.Expert.TokenType.OpeningBrace, "{");
                      token6.AssignStackInfo();
                      List<Token> infixForm = this.tf.infixForm;
                      int index4 = num4;
                      int index5 = index4 + 1;
                      Token token7 = token6;
                      infixForm.Insert(index4, token7);
                      for (int index6 = 0; index6 < longList.Count; ++index6)
                      {
                        long num8 = longList[index6];
                        Token token8 = new Token(Intermech.Expert.TokenType.Integer, results[index6]);
                        token8.iValue = Convert.ToInt64(num8);
                        token8._Guid = "";
                        token8.spt = SelectionParameterTypes.sptNumber;
                        token8.AssignStackInfo();
                        this.tf.infixForm.Insert(index5++, token8);
                        if (index6 < longList.Count - 1)
                        {
                          Token token9 = new Token(Intermech.Expert.TokenType.Divider, ",");
                          token9.AssignStackInfo();
                          this.tf.infixForm.Insert(index5++, token9);
                        }
                      }
                      Token token10 = new Token(Intermech.Expert.TokenType.ClosingBrace, "}");
                      token10.AssignStackInfo();
                      this.tf.infixForm.Insert(index5, token10);
                    }
                  }
                }
              }
              this.tf.UpdateTokenBegs();
              this.ShowFormula(this.tf);
              this.EnableControlButtons();
              this.fChanged = true;
            }
            else
            {
              SystemAttributeSelect sysAttrSel = (SystemAttributeSelect) null;
              SelectionParameterTypes attType = AttributeTypeValueSelector.GetAttType(attributeType, out sysAttrSel);
              if (sysAttrSel != null)
              {
                if (attType != SelectionParameterTypes.sptHandler)
                {
                  try
                  {
                    object aObject = (object) null;
                    if (refIds.Count > 0)
                    {
                      switch (attType)
                      {
                        case SelectionParameterTypes.sptDate:
                        case SelectionParameterTypes.sptGlobalID:
                          break;
                        case SelectionParameterTypes.sptObjectType:
                        case SelectionParameterTypes.sptLinkType:
                          aObject = (object) new ArrayList((ICollection) refIds);
                          break;
                        default:
                          aObject = (object) refIds[0];
                          break;
                      }
                    }
                    bool flag3 = false;
                    if (attType == SelectionParameterTypes.sptMeasured)
                    {
                      if (aMeasureValue != null)
                      {
                        aObject = (object) aMeasureValue;
                      }
                      else
                      {
                        MeasureForm measureForm = new MeasureForm();
                        List<MeasureDescriptor> measureDescriptorList = new List<MeasureDescriptor>();
                        foreach (MeasureDescriptor measure in MeasureHelper.Measures)
                        {
                          if (measure.PhysicalQuantityID == attributeType.SizeType)
                            measureDescriptorList.Add(measure);
                        }
                        if (measureForm.ExecuteDialog(ref aMeasureValue, measureDescriptorList.ToArray()) == DialogResult.OK)
                        {
                          aObject = (object) aMeasureValue;
                          flag3 = true;
                          aMeasureValue = (MeasuredValue) null;
                        }
                      }
                    }
                    object AddInfo = (object) num2;
                    if (attType == SelectionParameterTypes.sptObjectType)
                      AddInfo = (object) Multi;
                    if (!flag3 && !sysAttrSel(ref aObject, AddInfo))
                      return;
                    if (aObject is MeasuredValue)
                    {
                      if (aMeasureValue != null)
                        this.tf.infixForm.RemoveAt(this.curTokIndex + 1);
                      MeasuredValue measuredValue = (MeasuredValue) aObject;
                      Token t = new Token(Intermech.Expert.TokenType.Measured, measuredValue.ToString());
                      t.fValue = measuredValue.Value;
                      t.iValue = measuredValue.MeasureID;
                      t.spt = attType;
                      t.AssignStackInfo();
                      this.tf.InsertToken(this.curTokIndex + 1, t);
                      ++this.curTokIndex;
                      this.tf.UpdateTokenBegs();
                      this.ShowFormula(this.tf);
                      this.EnableControlButtons();
                      this.fChanged = true;
                      return;
                    }
                    List<long> inds = new List<long>();
                    List<string> capts = new List<string>();
                    List<string> guids = new List<string>();
                    if (!Multi || !(aObject is ArrayList))
                    {
                      long int64 = Convert.ToInt64(aObject);
                      string guid = this.getGuid(sessionKeeper1.Session, aObject, attType);
                      string str = SelectionParameter.ConvertToString(sessionKeeper1.Session, aObject, attType);
                      inds.Add(int64);
                      capts.Add(str);
                      guids.Add(guid);
                    }
                    else
                    {
                      ArrayList arrayList = (ArrayList) aObject;
                      for (int index7 = 0; index7 < arrayList.Count; ++index7)
                      {
                        long int64 = Convert.ToInt64(arrayList[index7]);
                        string str = SelectionParameter.ConvertToString(sessionKeeper1.Session, (object) int64, attType);
                        inds.Add(int64);
                        capts.Add(str);
                        guids.Add(this.getGuid(sessionKeeper1.Session, ((ArrayList) aObject)[index7], attType));
                      }
                    }
                    if (num3 >= 0)
                    {
                      this.InsDefValues(inds, capts, guids, Intermech.Expert.TokenType.Integer, attType, num4, rightToken, num3);
                      return;
                    }
                    if (token1 != null && token1.type == Intermech.Expert.TokenType.Integer && token1.trueText != token1.text)
                    {
                      token1.iValue = inds[0];
                      token1.text = capts[0];
                      token1.spt = attType;
                      token1._Guid = guids[0];
                    }
                    else
                    {
                      Token t = new Token(Intermech.Expert.TokenType.Integer, capts[0]);
                      t.iValue = inds[0];
                      t.spt = attType;
                      t._Guid = guids[0];
                      t.AssignStackInfo();
                      this.tf.InsertToken(this.curTokIndex + 1, t);
                    }
                    ++this.curTokIndex;
                    this.tf.UpdateTokenBegs();
                    this.ShowFormula(this.tf);
                    this.EnableControlButtons();
                    this.fChanged = true;
                    return;
                  }
                  finally
                  {
                    flag1 = true;
                  }
                }
              }
              bool flag4 = FormulaEditPlugin.IsAttrForSpravochnik(num2);
              List<long> imbaseCatalog = FormulaEditPlugin.GetImbaseCatalog(num1, num2);
              if ((!flag4 || imbaseCatalog == null || imbaseCatalog.Count == 0) && num3 >= 0 && attType == SelectionParameterTypes.sptObject && attributeType.AttributeType == FieldTypes.ftObjectLink)
              {
                SelectionOptions options = SelectionOptions.SelectObjects;
                if (!Multi)
                  options |= SelectionOptions.DisableMultiselect;
                flag1 = true;
                long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Expert.Editor_336"), LocalizationHolder.rm.GetString("Expert.Editor_337"), (int) attributeType.SizeType, options);
                if (numArray == null || numArray.Length == 0)
                  return;
                List<long> inds = new List<long>();
                List<string> capts = new List<string>();
                List<string> guids = new List<string>();
                using (SessionKeeper sessionKeeper2 = new SessionKeeper())
                {
                  foreach (long objectID in numArray)
                  {
                    IDBObject dbObject = sessionKeeper2.Session.GetObject(objectID, false);
                    if (dbObject != null)
                    {
                      inds.Add(objectID);
                      capts.Add(dbObject.Caption);
                      guids.Add(dbObject.ObjectGUID.ToString());
                    }
                  }
                }
                this.InsDefValues(inds, capts, guids, Intermech.Expert.TokenType.Integer, attType, num4, rightToken, num3);
              }
              else if (num1 == -1)
              {
                int num9 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_181"), LocalizationHolder.rm.GetString("Expert.Editor_182"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                flag1 = true;
              }
              else
              {
                bool flag5 = ExpertConsts.UsedIMCode(sessionKeeper1.Session, num1, num2);
                if (flag4)
                {
                  if (imbaseCatalog == null || imbaseCatalog.Count == 0)
                  {
                    int num10 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_579"), LocalizationHolder.rm.GetString("Expert.Editor_222"), MessageBoxButtons.OK);
                  }
                  IImbaseFilterSelector service = ServicesManager.GetService(typeof (IImbaseFilterSelector)) as IImbaseFilterSelector;
                  if (imbaseCatalog != null && imbaseCatalog.Count != 0 && service != null)
                  {
                    refIds = service.CheckImbaseObjects(imbaseCatalog, -1L, refIds);
                    flag5 = refIds.Count > 0 && refIds[0] != 0L;
                    flag1 = true;
                  }
                }
                else
                {
                  IMSelector imSelector = new IMSelector();
                  flag5 = !flag5 ? imSelector.Execute4Attribute(num1, num2, ref refIds) : imSelector.Execute4Objects(num1, ref refIds);
                  bool flag6 = false;
                  if (!flag5 && attributeType.AttributeType == FieldTypes.ftObjectLink)
                  {
                    int sizeType = (int) attributeType.SizeType;
                    long[] collection = sizeType == -1 ? (long[]) null : SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Expert.Editor_553"), LocalizationHolder.rm.GetString("Expert.Editor_554"), sizeType, SelectionOptions.SelectObjects);
                    if (collection != null)
                    {
                      refIds.Clear();
                      refIds.AddRange((IEnumerable<long>) collection);
                      flag5 = true;
                    }
                    else
                      flag6 = sizeType != -1;
                  }
                  flag1 = flag6;
                }
                if (!flag5)
                  return;
                if (num4 >= 0)
                {
                  if (rightToken < 0)
                    rightToken = this.tf.Count - 1;
                  for (int index8 = rightToken - num4; index8 >= 0; --index8)
                    this.tf.infixForm.RemoveAt(num4);
                }
                if (refIds.Count > 0)
                {
                  int num11 = refIds.Count != 1 ? 0 : (num3 < 0 ? 1 : (this.tf[num3].text.Trim() == "=" ? 1 : 0));
                  bool flag7 = refIds.Count == 1 && (num3 < 0 || this.tf[num3].text.Trim() == "<>");
                  if (num3 >= 0 && refIds.Count > 1)
                    this.tf[num3].text = "?";
                  if (num4 < 0)
                    num4 = this.curTokIndex + 1;
                  int num12 = flag7 ? 1 : 0;
                  if ((num11 | num12) != 0)
                  {
                    string str = "";
                    IDBObject dbObject = sessionKeeper1.Session.GetObject(refIds[0], false);
                    string caption;
                    if (dbObject != null)
                    {
                      caption = dbObject.Caption;
                      str = dbObject.GUID.ToString();
                    }
                    else
                      caption = Convert.ToString(refIds[0]);
                    Token token11 = new Token(Intermech.Expert.TokenType.Integer, caption);
                    token11.iValue = Convert.ToInt64(refIds[0]);
                    token11._Guid = str;
                    token11.spt = SelectionParameterTypes.sptObject;
                    token11.AssignStackInfo();
                    this.tf.infixForm.Insert(num4, token11);
                  }
                  else
                  {
                    Token token12 = new Token(Intermech.Expert.TokenType.OpeningBrace, "{");
                    token12.AssignStackInfo();
                    List<Token> infixForm = this.tf.infixForm;
                    int index9 = num4;
                    int index10 = index9 + 1;
                    Token token13 = token12;
                    infixForm.Insert(index9, token13);
                    IUserSession session = sessionKeeper1.Session;
                    for (int index11 = 0; index11 < refIds.Count; ++index11)
                    {
                      long objectID = refIds[index11];
                      IDBObject dbObject = session.GetObject(objectID, false);
                      if (dbObject != null)
                      {
                        string caption = dbObject.Caption;
                        string str = dbObject.GUID.ToString();
                        Token token14 = new Token(Intermech.Expert.TokenType.Integer, caption);
                        token14.iValue = Convert.ToInt64(objectID);
                        token14._Guid = str;
                        token14.spt = SelectionParameterTypes.sptObject;
                        token14.AssignStackInfo();
                        this.tf.infixForm.Insert(index10++, token14);
                        if (index11 < refIds.Count - 1)
                        {
                          Token token15 = new Token(Intermech.Expert.TokenType.Divider, ",");
                          token15.AssignStackInfo();
                          this.tf.infixForm.Insert(index10++, token15);
                        }
                      }
                      else
                      {
                        int num13 = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Expert.Editor_606"), (object) objectID), LocalizationHolder.rm.GetString("Expert.Editor_552"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                      }
                    }
                    Token token16 = new Token(Intermech.Expert.TokenType.ClosingBrace, "}");
                    token16.AssignStackInfo();
                    this.tf.infixForm.Insert(index10, token16);
                  }
                }
                this.tf.UpdateTokenBegs();
                this.ShowFormula(this.tf);
                this.EnableControlButtons();
                this.fChanged = true;
              }
            }
          }
        }
        finally
        {
          if (!flag1)
          {
            int num14 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_551"), LocalizationHolder.rm.GetString("Expert.Editor_552"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
        }
      }
    }
  }

  private void InsDefValues(
    List<long> inds,
    List<string> capts,
    List<string> guids,
    Intermech.Expert.TokenType tt,
    SelectionParameterTypes spt,
    int leftToken,
    int rightToken,
    int operToken)
  {
    int num1 = 0;
    if (leftToken >= 0)
    {
      if (rightToken < 0)
        rightToken = this.tf.Count - 1;
      for (int index = rightToken - leftToken; index >= 0; --index)
        this.tf.infixForm.RemoveAt(leftToken);
    }
    if (capts.Count > 0)
    {
      int num2 = capts.Count != 1 ? 0 : (operToken < 0 ? 1 : (this.tf[operToken].text.Trim() == "=" ? 1 : 0));
      bool flag = capts.Count == 1 && (operToken < 0 || this.tf[operToken].text.Trim() == "<>");
      if (operToken >= 0 && capts.Count > 1)
        this.tf[operToken].text = "?";
      if (leftToken < 0)
        leftToken = this.curTokIndex + 1;
      int num3 = flag ? 1 : 0;
      if ((num2 | num3) != 0)
      {
        string capt = capts[0];
        Token token = new Token(tt, capt);
        if (inds.Count > num1)
          token.iValue = Convert.ToInt64(inds[0]);
        token.spt = spt;
        if (guids != null)
          token._Guid = guids[0];
        token.AssignStackInfo();
        this.tf.infixForm.Insert(leftToken, token);
      }
      else
      {
        Token token1 = new Token(Intermech.Expert.TokenType.OpeningBrace, "{");
        token1.AssignStackInfo();
        this.tf.infixForm.Insert(leftToken++, token1);
        for (int index = 0; index < capts.Count; ++index)
        {
          string capt = capts[index];
          Token token2 = new Token(tt, capt);
          if (inds.Count > index)
          {
            long ind = inds[index];
            token2.iValue = Convert.ToInt64(ind);
          }
          token2.spt = spt;
          if (guids != null)
            token2._Guid = guids[index];
          token2.AssignStackInfo();
          this.tf.infixForm.Insert(leftToken++, token2);
          if (index < capts.Count - 1)
          {
            Token token3 = new Token(Intermech.Expert.TokenType.Divider, ",");
            token3.AssignStackInfo();
            this.tf.infixForm.Insert(leftToken++, token3);
          }
        }
        Token token4 = new Token(Intermech.Expert.TokenType.ClosingBrace, "}");
        token4.AssignStackInfo();
        this.tf.infixForm.Insert(leftToken, token4);
      }
    }
    this.tf.UpdateTokenBegs();
    this.ShowFormula(this.tf);
    this.EnableControlButtons();
    this.fChanged = true;
  }

  private string getGuid(IUserSession ius, object res, SelectionParameterTypes spt)
  {
    string guid = "";
    long num = -1;
    switch (spt)
    {
      case SelectionParameterTypes.sptObject:
        long int64_1 = Convert.ToInt64(res);
        IDBObject dbObject = ius.GetObject(int64_1);
        if (dbObject != null)
        {
          guid = dbObject.GUID.ToString();
          break;
        }
        break;
      case SelectionParameterTypes.sptObjectType:
        long int64_2 = Convert.ToInt64(res);
        IDBObjectType objectType = ius.GetObjectType((int) int64_2, false);
        if (objectType != null)
        {
          guid = objectType.PropertiesStructure.ObjectTypeGuid.ToString();
          break;
        }
        break;
      case SelectionParameterTypes.sptLifecycleLevel:
        num = Convert.ToInt64(res);
        break;
      case SelectionParameterTypes.sptLinkType:
        long int64_3 = Convert.ToInt64(res);
        IDBRelationType relationType = ius.GetRelationType((int) int64_3, false);
        if (relationType != null)
        {
          guid = relationType.PropertiesStructure.RelationTypeGuid.ToString();
          break;
        }
        break;
      case SelectionParameterTypes.sptLifecycleStep:
        long int64_4 = Convert.ToInt64(res);
        IDBLifecycleStep lifecycleStep = ius.GetLifecycleStep((int) int64_4, false);
        if (lifecycleStep != null)
        {
          guid = lifecycleStep.Properties.StepGuid.ToString();
          break;
        }
        break;
      case SelectionParameterTypes.sptGlobalID:
        guid = ((Guid) res).ToString();
        break;
    }
    return guid;
  }

  private void memoForm_MouseMove(object sender, MouseEventArgs e)
  {
    int tokenByPos = this.tf.GetTokenByPos(this.memoForm.GetCharIndexFromPosition(new Point(e.X, e.Y)));
    string caption = "";
    if (tokenByPos >= 0)
    {
      Token token = this.tf[tokenByPos];
      if (token.type == Intermech.Expert.TokenType.Integer && token.text != token.trueText)
        caption = token.trueText;
    }
    if (!(caption != this.toolTipFE.GetToolTip((Control) this.memoForm)))
      return;
    this.toolTipFE.SetToolTip((Control) this.memoForm, caption);
  }

  private bool CheckError(int TokenNum, ref string errorMsg)
  {
    if (!(errorMsg != ""))
      return false;
    if (TokenNum >= 0 && TokenNum < this.tf.Count)
      this.SetCurToken(TokenNum);
    if (errorMsg[0] == '-')
    {
      errorMsg = errorMsg.Substring(1);
      if (MessageBox.Show($"{errorMsg}\n{LocalizationHolder.rm.GetString("Expert.Editor_575")}", LocalizationHolder.rm.GetString("Expert.Editor_552"), MessageBoxButtons.OKCancel) == DialogResult.OK)
      {
        errorMsg = "";
        return false;
      }
    }
    this.errorLbl.Text = errorMsg;
    this.SetErrorVisible(true);
    this.DialogResult = DialogResult.None;
    return true;
  }

  private void ShowPostfix() => this.postForm.Execute(this.tf);

  private bool Compile()
  {
    this.DeleteUnused();
    int BadToken = -1;
    string errorMsg = "";
    this.tf.Compile(out BadToken, out errorMsg);
    this.ShowFormula(this.tf);
    return !this.CheckError(BadToken, ref errorMsg) && (!this.tf.StringWasConverted || MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_574"), LocalizationHolder.rm.GetString("Expert.Editor_303"), MessageBoxButtons.YesNo) != DialogResult.No) && errorMsg == "";
  }

  private void buttonOK_Click(object sender, EventArgs e)
  {
    if (this.Compile())
      return;
    this.DialogResult = DialogResult.None;
  }

  private void errorLbl_DoubleClick(object sender, EventArgs e) => this.SetErrorVisible(false);

  private void SaveCurrent()
  {
    if (this.saveTF == null || !this.saveTF.Equals((object) this.tf))
    {
      this.saveTF = new TempFormula(this.tf.resType, true);
      this.saveTF.Copy(this.tf);
    }
    this.saveCurTokIndex = this.curTokIndex;
    this.btnCompile.Enabled = true;
  }

  private void RestoreCurrent()
  {
    if (this.saveTF == null)
      return;
    if (!this.saveTF.Equals((object) this.tf))
    {
      this.tf = new TempFormula(this.saveTF.resType, true);
      this.tf.Copy(this.saveTF);
    }
    this.curTokIndex = this.saveCurTokIndex;
    this.ShowFormula(this.tf);
    this.saveTF = (TempFormula) null;
    this.saveCurTokIndex = -1;
    this.btnCompile.Enabled = false;
    this.EnableControlButtons();
  }

  private void btnCompile_Click(object sender, EventArgs e) => this.RestoreCurrent();

  private void btnRun_Click(object sender, EventArgs e)
  {
    if (!this.Compile())
      return;
    this.calcForm.Execute(ref this.tf);
  }

  private void DeleteUnused()
  {
    ArrayList arrayList1 = new ArrayList();
    ArrayList arrayList2 = new ArrayList();
    for (int index = 0; index < this.tf.Count; ++index)
    {
      switch (this.tf[index].type)
      {
        case Intermech.Expert.TokenType.ObjectLink:
          arrayList2.Add((object) this.tf[index].info);
          break;
        case Intermech.Expert.TokenType.Attribute:
          arrayList1.Add((object) this.tf[index].info);
          break;
      }
    }
    arrayList1.Sort();
    arrayList2.Sort();
    int index1 = 0;
    while (index1 < this.tf.usedAttrs.Count)
    {
      if (arrayList1.BinarySearch((object) index1) < 0)
      {
        this.tf.usedAttrs.RemoveAt(index1);
        this.tf.pairNames.RemoveAt(index1);
        this.tf.attrGUIDs.RemoveAt(index1);
        this.tf.objTypeGUIDs.RemoveAt(index1);
        for (int index2 = 0; index2 < arrayList1.Count; ++index2)
        {
          if ((int) arrayList1[index2] > index1)
            arrayList1[index2] = (object) ((int) arrayList1[index2] - 1);
        }
        for (int index3 = 0; index3 < this.tf.Count; ++index3)
        {
          Token token = this.tf[index3];
          if (token.type == Intermech.Expert.TokenType.Attribute && token.info > index1)
            --token.info;
        }
      }
      else
        ++index1;
    }
    int index4 = 0;
    while (index4 < this.tf.objectLinks.Count)
    {
      if (arrayList2.BinarySearch((object) index4) < 0)
      {
        this.tf.objectLinks.RemoveAt(index4);
        for (int index5 = 0; index5 < arrayList2.Count; ++index5)
        {
          if ((int) arrayList2[index5] > index4)
            arrayList2[index5] = (object) ((int) arrayList2[index5] - 1);
        }
        for (int index6 = 0; index6 < this.tf.Count; ++index6)
        {
          Token token = this.tf[index6];
          if (token.type == Intermech.Expert.TokenType.ObjectLink && token.info > index4)
            --token.info;
        }
      }
      else
        ++index4;
    }
  }

  private void btnExport_Click(object sender, EventArgs e)
  {
    if (this.tf == null || this.tf.Count == 0 || this.sfd.ShowDialog() != DialogResult.OK)
      return;
    XmlTextWriter writer = new XmlTextWriter(this.sfd.FileName, Encoding.Unicode);
    this.tf.WriteToXML(ref writer);
    writer.Flush();
  }

  private void btnImport_Click(object sender, EventArgs e)
  {
    if (this.tf.Count > 0 && MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_159"), LocalizationHolder.rm.GetString("Expert.Editor_160"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes || this.ofd.ShowDialog() != DialogResult.OK)
      return;
    FileStream inStream = new FileStream(this.ofd.FileName, FileMode.Open);
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load((Stream) inStream);
    this.tf = new TempFormula((XmlNode) xmlDocument.DocumentElement);
    this.tf.UpdateTokenBegs();
    if (this.tf.Count > 0)
      this.SetCurToken(0);
    else
      this.SetCurToken(-1);
    this.SaveCurrent();
    this.EnableControlButtons();
    this.fChanged = true;
  }

  private void cbAllowUnknown_CheckedChanged(object sender, EventArgs e)
  {
    if (this.LockEditEnable)
      return;
    Token token = this.CurToken();
    if (token == null || token.type != Intermech.Expert.TokenType.Attribute)
      return;
    if (this.cbAllowUnknown.Checked)
    {
      token.fValue = (double) Token._SIGN;
      token.text = "#" + token.text.TrimStart('#');
    }
    else
    {
      token.fValue = 0.0;
      token.text = token.text.TrimStart('#');
    }
    this.SaveCurrent();
    this.ShowFormula(this.tf);
  }

  private class EditComboItem
  {
    internal AttribPair ap;
    internal PairName pn;
    internal string attrGUID;
    internal string objTypeGUID;

    internal EditComboItem(AttribPair ap, PairName pn, string aGUID, string oGUID)
    {
      this.ap = ap;
      this.pn = pn;
      this.attrGUID = aGUID;
      this.objTypeGUID = oGUID;
    }

    public override string ToString() => this.pn.ShortName;
  }

  public delegate SelFormResult SelTypeEventHandler(object sender, SelTypeEventArgs e);

  public class UserAttrItem
  {
    public int attrId;
    public string caption;
    public string[] possibleValues;
    public bool multiSelect;

    public UserAttrItem(int aId, string capt, string[] posValues, bool multi)
    {
      this.attrId = aId;
      this.caption = capt;
      this.possibleValues = (string[]) posValues.Clone();
      this.multiSelect = multi;
    }
  }

  public class UserAttrs : Hashtable
  {
    public static readonly FormEditor.UserAttrs ua;

    static UserAttrs()
    {
      if (FormEditor.UserAttrs.ua != null)
        return;
      FormEditor.UserAttrs.ua = new FormEditor.UserAttrs();
    }

    public static void RegisterUserAttr(
      int attrId,
      string caption,
      string[] possibleValues,
      bool multiSelect)
    {
      if (FormEditor.UserAttrs.ua.ContainsKey((object) attrId))
        return;
      FormEditor.UserAttrItem userAttrItem = new FormEditor.UserAttrItem(attrId, caption, possibleValues, multiSelect);
      FormEditor.UserAttrs.ua.Add((object) attrId, (object) userAttrItem);
    }

    public static void UnRegisterUserAttr(int attrId)
    {
      if (!FormEditor.UserAttrs.ua.ContainsKey((object) attrId))
        return;
      FormEditor.UserAttrs.ua.Remove((object) attrId);
    }
  }

  private enum ValidateType
  {
    NotAfterOperator,
    NotAfterOperand,
    NotAfterDivider,
    NotAfterOpenBrace,
    OnlyAfterOperand,
    NotFirst,
    NotLast,
    OnlyAfterAttribute,
  }

  public delegate int Compare(int Index, ref PairName pn);
}
