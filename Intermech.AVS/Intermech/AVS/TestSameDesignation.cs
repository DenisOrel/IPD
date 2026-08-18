// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.TestSameDesignation
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using Intermech.Client.Core;
using Intermech.Controls.Grid;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary> Описание класса TestSameDesignation </summary>
public class TestSameDesignation : ExtForm
{
  private IContainer components;
  private Button _BtnCancel;
  private Label label1;
  private TextEdit _textEditDesignation1;
  private TextEdit _textEditDesignation2;
  private Label label2;
  private ListGrid _ListGridSubStr;
  private Label label3;
  private Label label4;
  private Label label5;
  private Label label6;
  private TextEdit _textEditDesignationResult2;
  private TextEdit _textEditDesignationResult1;
  private TextEdit _textEditResult;
  private ToolTipController _ReadModeToolTip;
  private CompareDesignationSchema _compareDesignationSchema;

  public TestSameDesignation()
  {
    this.InitializeComponent();
    this.Init((CompareDesignationSchema) null);
  }

  public TestSameDesignation(CompareDesignationSchema compareDesignationSchema)
  {
    this.InitializeComponent();
    this.Init(compareDesignationSchema);
  }

  private void Init(CompareDesignationSchema compareDesignationSchema)
  {
    this._textEditDesignation1.Text = AVSPlugin.TestSameDesignation1;
    this._textEditDesignation2.Text = AVSPlugin.TestSameDesignation2;
    this.CompareDesignationSchema = compareDesignationSchema;
  }

  /// <summary> Очистка использованных ресурсов </summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.components != null)
        this.components.Dispose();
      this._ReadModeToolTip?.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary> Обязательный метод, требуемый дизайнеру формы - не модиффицируйте данный код </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ListColumn listColumn1 = new ListColumn();
    ListColumn listColumn2 = new ListColumn();
    ListColumn listColumn3 = new ListColumn();
    ListColumn listColumn4 = new ListColumn();
    Intermech.Controls.Grid.ListItem listItem = new Intermech.Controls.Grid.ListItem();
    ListSubItem listSubItem1 = new ListSubItem();
    ListSubItem listSubItem2 = new ListSubItem();
    ListSubItem listSubItem3 = new ListSubItem();
    ListSubItem listSubItem4 = new ListSubItem();
    this._BtnCancel = new Button();
    this._ReadModeToolTip = new ToolTipController(this.components);
    this._ListGridSubStr = new ListGrid();
    this.label1 = new Label();
    this._textEditDesignation1 = new TextEdit();
    this._textEditDesignation2 = new TextEdit();
    this.label2 = new Label();
    this.label3 = new Label();
    this._textEditDesignationResult2 = new TextEdit();
    this.label4 = new Label();
    this._textEditDesignationResult1 = new TextEdit();
    this.label5 = new Label();
    this._textEditResult = new TextEdit();
    this.label6 = new Label();
    this._textEditDesignation1.Properties.BeginInit();
    this._textEditDesignation2.Properties.BeginInit();
    this._textEditDesignationResult2.Properties.BeginInit();
    this._textEditDesignationResult1.Properties.BeginInit();
    this._textEditResult.Properties.BeginInit();
    this.SuspendLayout();
    this._BtnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._BtnCancel.DialogResult = DialogResult.Cancel;
    this._BtnCancel.FlatStyle = FlatStyle.System;
    this._BtnCancel.Location = new Point(659, 375);
    this._BtnCancel.Name = "_BtnCancel";
    this._BtnCancel.Size = new Size(121, 27);
    this._BtnCancel.TabIndex = 6;
    this._BtnCancel.Text = "Закрыть";
    this._ReadModeToolTip.SetToolTip((Control) this._BtnCancel, "Закрыть диалог");
    this._ReadModeToolTip.Style = new ViewStyle("ToolTip style");
    this._ListGridSubStr.AllowColumnResize = false;
    this._ListGridSubStr.AlternateBackground = Color.DarkGreen;
    this._ListGridSubStr.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._ListGridSubStr.AutoHeight = false;
    this._ListGridSubStr.BackColor = Color.WhiteSmoke;
    listColumn1.Name = "Column1";
    listColumn1.Text = "От";
    listColumn1.TextAlignment = ContentAlignment.MiddleCenter;
    listColumn1.Width = 181;
    listColumn2.Name = "Column2";
    listColumn2.Text = "До";
    listColumn2.TextAlignment = ContentAlignment.MiddleCenter;
    listColumn2.Width = 181;
    listColumn3.Name = "Column3";
    listColumn3.Text = "Первое обозначение";
    listColumn3.TextAlignment = ContentAlignment.MiddleCenter;
    listColumn3.Width = 201;
    listColumn4.Name = "Column4";
    listColumn4.Text = "Второе обозначение";
    listColumn4.TextAlignment = ContentAlignment.MiddleCenter;
    listColumn4.Width = 201;
    this._ListGridSubStr.Columns.AddRange(new ListColumn[4]
    {
      listColumn1,
      listColumn2,
      listColumn3,
      listColumn4
    });
    this._ListGridSubStr.GridColor = Color.Gray;
    this._ListGridSubStr.GridLines = GridLines.Horizontal;
    this._ListGridSubStr.GridLineStyle = GridLineStyle.Dashed;
    this._ListGridSubStr.HeaderHeight = 18;
    this._ListGridSubStr.HeaderStyle = HeaderStyle.XP;
    this._ListGridSubStr.HotTrackingColor = Color.LightGray;
    this._ListGridSubStr.ImageList = (ImageList) null;
    this._ListGridSubStr.ItemHeight = 17;
    listItem.BackColor = Color.White;
    listItem.ForeColor = Color.Black;
    listItem.RowBorderColor = Color.Black;
    listSubItem1.BackColor = Color.Empty;
    listSubItem1.ForeColor = Color.Black;
    listSubItem1.Text = "начала обозначения";
    listSubItem2.BackColor = Color.Empty;
    listSubItem2.ForeColor = Color.Black;
    listSubItem2.Text = "количества символов = 12";
    listSubItem3.BackColor = Color.Empty;
    listSubItem3.ForeColor = Color.Black;
    listSubItem3.Text = "\"ИНТМ.123456.\"";
    listSubItem4.BackColor = Color.Empty;
    listSubItem4.ForeColor = Color.Black;
    listSubItem4.Text = "\"ИНТМ.123456.\"";
    listItem.SubItems.AddRange(new ListSubItem[4]
    {
      listSubItem1,
      listSubItem2,
      listSubItem3,
      listSubItem4
    });
    listItem.Text = "начала обозначения";
    this._ListGridSubStr.Items.AddRange(new Intermech.Controls.Grid.ListItem[1]
    {
      listItem
    });
    this._ListGridSubStr.Location = new Point(4, 122);
    this._ListGridSubStr.Name = "_ListGridSubStr";
    this._ListGridSubStr.SelectedTextColor = Color.White;
    this._ListGridSubStr.SelectionColor = Color.DarkBlue;
    this._ListGridSubStr.ShowFocusRect = true;
    this._ListGridSubStr.Size = new Size(774, 81);
    this._ListGridSubStr.SortType = SortType.None;
    this._ListGridSubStr.SuperFlatHeaderColor = Color.White;
    this._ListGridSubStr.TabIndex = 2;
    this._ReadModeToolTip.SetToolTip((Control) this._ListGridSubStr, "Список правил, по которым из обозначения вырезаются подстроки для дальнейшего сравнения с целью определения \"похожести\" обозначений");
    this.label1.Location = new Point(4, 0);
    this.label1.Name = "label1";
    this.label1.Size = new Size(168, 23);
    this.label1.TabIndex = 5;
    this.label1.Text = "Первое обозначение:";
    this.label1.TextAlign = ContentAlignment.BottomLeft;
    this._textEditDesignation1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._textEditDesignation1.EditValue = (object) "ИНТМ.123456.002";
    this._textEditDesignation1.Location = new Point(4, 25);
    this._textEditDesignation1.Name = "_textEditDesignation1";
    this._textEditDesignation1.Size = new Size(774, 20);
    this._textEditDesignation1.TabIndex = 0;
    this._textEditDesignation1.ToolTip = "Первое обозначение, которое будет сравниваться для определения \"похожести\"";
    this._textEditDesignation1.EditValueChanged += new EventHandler(this._textEditDesignation_EditValueChanged);
    this._textEditDesignation2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this._textEditDesignation2.EditValue = (object) "ИНТМ.123456.123";
    this._textEditDesignation2.Location = new Point(4, 70);
    this._textEditDesignation2.Name = "_textEditDesignation2";
    this._textEditDesignation2.Size = new Size(774, 20);
    this._textEditDesignation2.TabIndex = 1;
    this._textEditDesignation2.ToolTip = "Второе обозначение, которое будет сравниваться для определения \"похожести\"";
    this._textEditDesignation2.EditValueChanged += new EventHandler(this._textEditDesignation_EditValueChanged);
    this.label2.Location = new Point(4, 45);
    this.label2.Name = "label2";
    this.label2.Size = new Size(168, 23);
    this.label2.TabIndex = 7;
    this.label2.Text = "Второе обозначение:";
    this.label2.TextAlign = ContentAlignment.BottomLeft;
    this.label3.Location = new Point(4, 96 /*0x60*/);
    this.label3.Name = "label3";
    this.label3.Size = new Size(759, 23);
    this.label3.TabIndex = 10;
    this.label3.Text = "Правила создания подстрок из обозначений, которые должны сравниваться для определения \"похожести\" обозначений:";
    this.label3.TextAlign = ContentAlignment.BottomLeft;
    this._textEditDesignationResult2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._textEditDesignationResult2.EditValue = (object) "\"ИНТМ.123456.\"";
    this._textEditDesignationResult2.Location = new Point(4, 285);
    this._textEditDesignationResult2.Name = "_textEditDesignationResult2";
    this._textEditDesignationResult2.Properties.ReadOnly = true;
    this._textEditDesignationResult2.Properties.Style = new ViewStyle("ControlStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, Color.WhiteSmoke, SystemColors.WindowText);
    this._textEditDesignationResult2.Size = new Size(774, 20);
    this._textEditDesignationResult2.TabIndex = 4;
    this.label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.label4.Location = new Point(4, 260);
    this.label4.Name = "label4";
    this.label4.Size = new Size(469, 23);
    this.label4.TabIndex = 13;
    this.label4.Text = "Строка, получившаяся после применения правил к второму обозначению:";
    this.label4.TextAlign = ContentAlignment.BottomLeft;
    this._textEditDesignationResult1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._textEditDesignationResult1.EditValue = (object) "\"ИНТМ.123456.\"";
    this._textEditDesignationResult1.Location = new Point(4, 240 /*0xF0*/);
    this._textEditDesignationResult1.Name = "_textEditDesignationResult1";
    this._textEditDesignationResult1.Properties.ReadOnly = true;
    this._textEditDesignationResult1.Properties.Style = new ViewStyle("ControlStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, Color.WhiteSmoke, SystemColors.WindowText);
    this._textEditDesignationResult1.Size = new Size(774, 20);
    this._textEditDesignationResult1.TabIndex = 3;
    this._textEditDesignationResult1.ToolTip = "Строка, полученая путем применения правил к первому обозначению";
    this.label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.label5.Location = new Point(4, 215);
    this.label5.Name = "label5";
    this.label5.Size = new Size(470, 23);
    this.label5.TabIndex = 11;
    this.label5.Text = "Строка, получившаяся после применения правил к первому обозначению:";
    this.label5.TextAlign = ContentAlignment.BottomLeft;
    this._textEditResult.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._textEditResult.EditValue = (object) "Обозначения похожи";
    this._textEditResult.Location = new Point(4, 341);
    this._textEditResult.Name = "_textEditResult";
    this._textEditResult.Properties.ReadOnly = true;
    this._textEditResult.Properties.Style = new ViewStyle("ControlStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, Color.WhiteSmoke, SystemColors.WindowText);
    this._textEditResult.Size = new Size(774, 20);
    this._textEditResult.TabIndex = 5;
    this.label6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.label6.Location = new Point(4, 316);
    this.label6.Name = "label6";
    this.label6.Size = new Size(469, 23);
    this.label6.TabIndex = 15;
    this.label6.Text = "Результат:";
    this.label6.TextAlign = ContentAlignment.BottomLeft;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.CancelButton = (IButtonControl) this._BtnCancel;
    this.ClientSize = new Size(792, 414);
    this.Controls.Add((Control) this._textEditResult);
    this.Controls.Add((Control) this.label6);
    this.Controls.Add((Control) this._textEditDesignationResult2);
    this.Controls.Add((Control) this.label4);
    this.Controls.Add((Control) this._textEditDesignationResult1);
    this.Controls.Add((Control) this.label5);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this._ListGridSubStr);
    this.Controls.Add((Control) this._textEditDesignation2);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this._textEditDesignation1);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this._BtnCancel);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(800, 435);
    this.Name = nameof (TestSameDesignation);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Окно проверки правил сравнения обозначений";
    this.Closed += new EventHandler(this.TestSameDesignation_Closed);
    this.Load += new EventHandler(this.TestSameDesignation_Load);
    this._textEditDesignation1.Properties.EndInit();
    this._textEditDesignation2.Properties.EndInit();
    this._textEditDesignationResult2.Properties.EndInit();
    this._textEditDesignationResult1.Properties.EndInit();
    this._textEditResult.Properties.EndInit();
    this.ResumeLayout(false);
  }

  /// <summary> Схема выдёргивания подстрок для дальнейшего сравнения </summary>
  public CompareDesignationSchema CompareDesignationSchema
  {
    get => this._compareDesignationSchema;
    set
    {
      this.LockControls();
      try
      {
        this._compareDesignationSchema = value;
        this.ReloadVisualList();
        this.RecalcResults();
      }
      finally
      {
        this.UnlockControls();
      }
    }
  }

  /// <summary> Обновление отображения списка правил выдёргивания подстрок из обозначения </summary>
  private void ReloadVisualList()
  {
    this._ListGridSubStr.BeginUpdate();
    try
    {
      this._ListGridSubStr.Items.Clear();
      if (this._compareDesignationSchema != null)
      {
        foreach (CompareDesignationSubStr subStr in this._compareDesignationSchema.SubStrs)
        {
          Intermech.Controls.Grid.ListItem listItem = new Intermech.Controls.Grid.ListItem();
          ListSubItem listSubItem1 = new ListSubItem();
          ListSubItem listSubItem2 = new ListSubItem();
          ListSubItem listSubItem3 = new ListSubItem();
          ListSubItem listSubItem4 = new ListSubItem();
          listItem.SubItems.AddRange(new ListSubItem[4]
          {
            listSubItem1,
            listSubItem2,
            listSubItem3,
            listSubItem4
          });
          this.RefreshListItem(listItem, subStr);
          this._ListGridSubStr.Items.Add(listItem);
        }
      }
      this.CheckAnySelected();
    }
    finally
    {
      this._ListGridSubStr.EndUpdate();
    }
  }

  /// <summary> Обновление сфокусированого визуального отображения правила выдирания подстроки в списке всех правил (в ListGrid-е) </summary>
  private void RefreshFocusedListItem() => this.RefreshListItem(this._ListGridSubStr.FocusedItem);

  /// <summary> Обновление визуального отображения правила выдирания подстроки в списке всех правил (в ListGrid-е) </summary>
  /// <param name="listItem"> ListItem - визуальное отображение </param>
  private void RefreshListItem(Intermech.Controls.Grid.ListItem listItem)
  {
    if (listItem == null)
      return;
    this.RefreshListItem(listItem, (CompareDesignationSubStr) listItem.Tag);
  }

  /// <summary> Обновление визуального отображения правила выдирания подстроки в списке всех правил (в ListGrid-е) </summary>
  /// <param name="listItem"> ListItem - визуальное отображение </param>
  /// <param name="compareDesignationSubStr"> правило выдёргивания подстроки из обозначения </param>
  private void RefreshListItem(Intermech.Controls.Grid.ListItem listItem, CompareDesignationSubStr compareDesignationSubStr)
  {
    if (listItem == null || compareDesignationSubStr == null)
      return;
    listItem.SubItems[0].Text = compareDesignationSubStr.StartAsText;
    listItem.SubItems[1].Text = compareDesignationSubStr.FinishAsText;
    listItem.SubItems[2].Text = string.Empty;
    listItem.SubItems[3].Text = string.Empty;
    listItem.Text = listItem.SubItems[0].Text;
    listItem.Tag = (object) compareDesignationSubStr;
  }

  /// <summary> Контроль того факта, что хотя бы одно правило выдирания подстроки выбрано </summary>
  private void CheckAnySelected()
  {
    if (this._ListGridSubStr.Items.Count <= 0)
      return;
    if (this._ListGridSubStr.FocusedItem == null)
      this._ListGridSubStr.FocusedItem = this._ListGridSubStr.Items[0];
    if (this._ListGridSubStr.FocusedItem.Selected)
      return;
    this._ListGridSubStr.FocusedItem.Selected = true;
  }

  /// <summary> Пересчёт результатов применения правил к обозначениям </summary>
  private void RecalcResults()
  {
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    string empty3 = string.Empty;
    int nItemIndex = 0;
    this._ListGridSubStr.BeginUpdate();
    try
    {
      foreach (CompareDesignationSubStr subStr in this._compareDesignationSchema.SubStrs)
      {
        Intermech.Controls.Grid.ListItem listItem = this._ListGridSubStr.Items[nItemIndex];
        if (nItemIndex > 0)
          empty1 += " + ";
        string str1 = $"'{subStr.GetDesignationSubStr(this._textEditDesignation1.Text)}'";
        empty1 += str1;
        listItem.SubItems[2].Text = str1;
        if (nItemIndex > 0)
          empty2 += " + ";
        string str2 = $"'{subStr.GetDesignationSubStr(this._textEditDesignation2.Text)}'";
        empty2 += str2;
        listItem.SubItems[3].Text = str2;
        ++nItemIndex;
      }
    }
    finally
    {
      this._ListGridSubStr.EndUpdate();
    }
    this._textEditDesignationResult1.Text = empty1;
    this._textEditDesignationResult2.Text = empty2;
    this._textEditResult.Text = this._compareDesignationSchema.IsDesiagnationsAreSame(this._textEditDesignation1.Text, this._textEditDesignation2.Text) ? "Обозначения похожи" : "Обозначения непохожи";
  }

  /// <summary> Обновление визуальных контролов </summary>
  protected override void UpdateControls()
  {
  }

  /// <summary> Проверка, должно ли быть доступно редактирование </summary>
  protected override bool GetIsReadOnly() => false;

  /// <summary>Форма была загруженна</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TestSameDesignation_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>Вызывается при закрытии формы</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TestSameDesignation_Closed(object sender, EventArgs e)
  {
    AVSPlugin.TestSameDesignation1 = this._textEditDesignation1.Text;
    AVSPlugin.TestSameDesignation2 = this._textEditDesignation2.Text;
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary> Было изменено первое тестовое обозначение </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _textEditDesignation_EditValueChanged(object sender, EventArgs e)
  {
    if (this._compareDesignationSchema == null)
      return;
    this.RecalcResults();
  }
}
