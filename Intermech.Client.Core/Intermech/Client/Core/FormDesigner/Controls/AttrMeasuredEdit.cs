
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrMeasuredEdit
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
[Designer(typeof (AttrMeasuredEditControlDesigner))]
[RefreshProperties(RefreshProperties.All)]
public class AttrMeasuredEdit : AttrsControl, IExpertSystemCtrl
{
  private ArrayList _measures = new ArrayList();
  private List<string> _measuresSName = new List<string>();
  /// <summary>Использование атрибута в экспертной системе</summary>
  private bool _useInExpertSystem;
  private ControlButton _btnDots;
  private ControlButton _btnCalc;
  private ControlButton _btnReCalc;
  private string _errMsg_NotValidFormat = string.Empty;
  private bool _textChanged;
  private bool _textChangedFromDlg;
  private static Dictionary<Guid, MeasureDescriptor> defMeasures = new Dictionary<Guid, MeasureDescriptor>();
  /// <summary>
  /// Флаг, выполнялся ли поиск настройки хотя бы раз для назначенного атрибута
  /// </summary>
  private bool _isAttrShortNameInString_CheckedFlag;
  /// <summary>
  /// Флаг, нужно ли добавлять единицу измерения к величине в единицах измерения
  /// </summary>
  private bool _isAttrShortNameInString = true;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TextBox _txt;

  /// <summary>Цвет фона элемента управления.</summary>
  [System.ComponentModel.DefaultValue(typeof (Color), "Window")]
  public new Color BackColor
  {
    get => this._txt.BackColor;
    set => this._txt.BackColor = value;
  }

  /// <summary>Вид обрамления для элемента управления.</summary>
  [System.ComponentModel.DefaultValue(BorderStyle.Fixed3D)]
  public new BorderStyle BorderStyle
  {
    get => this._txt.BorderStyle;
    set => this._txt.BorderStyle = value;
  }

  /// <summary>Шрифт текста, отображаемый элементом управления.</summary>
  public new Font Font
  {
    get => this._txt.Font;
    set => this._txt.Font = value;
  }

  /// <summary>
  /// Основной цвет элемента управления, который используется для отображаемого текста.
  /// </summary>
  [System.ComponentModel.DefaultValue(typeof (Color), "WindowText")]
  public new Color ForeColor
  {
    get => this._txt.ForeColor;
    set => this._txt.ForeColor = value;
  }

  /// <summary>Текстовая подсказка.</summary>
  [System.ComponentModel.DefaultValue("")]
  public string Hint
  {
    get => this._toolTip.GetToolTip((Control) this._txt);
    set => this._toolTip.SetToolTip((Control) this._txt, value);
  }

  /// <summary>Текст, связанный с элементом управления.</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public override string Text
  {
    get => this._txt.Text;
    set
    {
      this._txt.Text = string.IsNullOrEmpty(this._designText) || !string.IsNullOrEmpty(value) ? value : this._designText;
    }
  }

  /// <summary>Выравнивание текста в элементе управления.</summary>
  [System.ComponentModel.DefaultValue(HorizontalAlignment.Left)]
  public HorizontalAlignment TextAlign
  {
    get => this._txt.TextAlign;
    set => this._txt.TextAlign = value;
  }

  /// <summary>Guid единицы измерения по умолчанию</summary>
  [System.ComponentModel.DefaultValue("")]
  public string DefaultMeasured { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public MeasureDescriptor DefMeasure { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [System.ComponentModel.DefaultValue(null)]
  public MeasuredValue DefaultValue { get; set; }

  /// <summary>Отступы от краев в элементе управления.</summary>
  /// <remarks>Здесь нужно только для того, чтобы запреить сериализацию</remarks>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new Padding Padding
  {
    get => base.Padding;
    private set => base.Padding = value;
  }

  /// <summary>Разрешить использовать значение по умолчанию.</summary>
  [Browsable(false)]
  public bool AllowDefaultValue { get; set; }

  /// <summary>Получение текущего набора значений атрибута.</summary>
  protected override object[] GetValues
  {
    get
    {
      return !string.IsNullOrEmpty(this._txt.Text) ? new object[1]
      {
        (object) MeasureHelper.ConvertToMeasuredValue(this._txt.Text, this.DefMeasure, false)
      } : new object[1]{ (object) DBNull.Value };
    }
  }

  /// <summary>
  /// Флаг, который показывает, что значение было изменено при загрузке.
  /// </summary>
  /// <remarks>
  /// Необходимость возникла при добавлении свойства "Значение по умолчанию".
  /// Если значение атрибута пустое, подставляется значение по умолчанию.
  /// При этом нужно запретить форме, на этапе загрузки, сбрасыват флаг модификации, чтобы можно было сохранить подставленное значение.
  /// </remarks>
  [Browsable(false)]
  public bool ModifiedInLoad { get; private set; }

  /// <summary>Конструктор.</summary>
  public AttrMeasuredEdit()
  {
    this.InitializeComponent();
    this.Name = string.Empty;
    this._errMsg_NotValidFormat = LocalizationHolder.rm.GetString("AttrControls_FormatData_ErrorMsg");
    this._txt.GotFocus += new EventHandler(this.On_txt_GotFocus);
    this._txt.LostFocus += new EventHandler(this.On_txt_LostFocus);
    this._btnDots = new ControlButton("Dots", 0)
    {
      Enabled = false
    };
    this._btnDots.Click += new EventHandler(this.On_btn_Click);
    this.AddRightButton(this._btnDots);
  }

  /// <summary>
  /// 
  /// </summary>
  public event KeyEventHandler TxtKeyDown;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btn_Click(object sender, EventArgs e)
  {
    if (this.AttributeInfo == null)
      return;
    if (AttrMeasuredEdit.defMeasures.ContainsKey(this.AttributeInfo.AttributeGuid))
      this.DefMeasure = AttrMeasuredEdit.defMeasures[this.AttributeInfo.AttributeGuid];
    else
      this.LoadDefaultMeasure();
    string strValue = string.Empty;
    if (!this.CheckValue(out strValue))
      return;
    MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(strValue, this.DefMeasure, false);
    using (MeasureForm measureForm = new MeasureForm())
    {
      this._textChangedFromDlg = false;
      if (measureForm.ExecuteDialog(ref measuredValue, this._measures.ToArray(typeof (MeasureDescriptor)) as MeasureDescriptor[]) != DialogResult.OK)
        return;
      this._textChangedFromDlg = true;
      this.Text = measuredValue.Caption;
      this.DefMeasure = MeasureHelper.FindDescriptor(measuredValue.MeasureID);
      if (AttrMeasuredEdit.defMeasures.ContainsKey(this.AttributeInfo.AttributeGuid))
        AttrMeasuredEdit.defMeasures[this.AttributeInfo.AttributeGuid] = this.DefMeasure;
      else
        AttrMeasuredEdit.defMeasures.Add(this.AttributeInfo.AttributeGuid, this.DefMeasure);
      this._txt.Focus();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_expBtn_CalcClick(object sender, EventArgs e)
  {
    if (this.AttributeInfo == null || this.ParentInfo == null)
      return;
    ExpertSystem.Calculate(this.ParentInfo, this.AttributeInfo.AttributeGuid, this.DesForm);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_expBtn_ReCalcClick(object sender, EventArgs e)
  {
    if (this.AttributeInfo == null || this.ParentInfo == null)
      return;
    ExpertSystem.ReCalculate(this.ParentInfo.ElementIdentifier, this.AttributeInfo.AttributeGuid, this.DesForm);
  }

  /// <summary>Фокусирование текстового контрола.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_GotFocus(object sender, EventArgs e)
  {
    this.Error = string.Empty;
    this._textChanged = false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_KeyDown(object sender, KeyEventArgs e)
  {
    if (this.AttributeInfo == null || this._attrValues == null || e.KeyCode != Keys.Return && e.KeyCode != Keys.Escape || this.TxtKeyDown == null)
      return;
    this.TxtKeyDown((object) this, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_LostFocus(object sender, EventArgs e)
  {
    if (this._attrValues == null)
      return;
    string strValue = string.Empty;
    this.Error = this.CheckValue(out strValue) ? (!this._disableNulls || !this.EnabledCtrl || !string.IsNullOrEmpty(this._txt.Text.Trim()) ? string.Empty : this._errMsg_NullValue) : this._errMsg_NotValidFormat;
    if (!this._textChanged && !this._textChangedFromDlg)
      return;
    this.OnCompletionOfEditing();
    this._textChangedFromDlg = false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_SizeChanged(object sender, EventArgs e)
  {
    this.Height = this._txt == null || this._txt.Height < 20 ? 22 : this._txt.Height + 2;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_txt_TextChanged(object sender, EventArgs e)
  {
    if (this.IsDesignMode || this.AttributeInfo == null || this._attrValues == null)
      return;
    this._textChanged = true;
    this.Modified = true;
    if (!this.Modified)
      return;
    this.ModifiedInLoad = false;
  }

  /// <summary>Guid атрибута и типа объекта/связи.</summary>
  public override AttributeValues Values
  {
    get => base.Values;
    set
    {
      base.Values = value;
      if (this._attrValues != null)
      {
        bool flag = this.CanUseDefaultValue();
        if (flag)
        {
          this._attrValues.Values = new object[1]
          {
            (object) this.DefaultValue
          };
          this._txt.Text = Convert.ToString(this._attrValues.Values[0]);
          this.ModifiedInLoad = true;
        }
        else
          this._txt.Text = this._attrValues == null || this._attrValues.Values == null || this._attrValues.Values.Length == 0 ? string.Empty : Convert.ToString(this._attrValues.Values[0]);
        if ((this.EnabledCtrl | flag || !string.IsNullOrEmpty(this._txt.Text.Trim())) && this.AttributeInfo != null)
        {
          this.LoadDefaultMeasure();
          this._measures.Clear();
          this._measuresSName.Clear();
          string strValue = string.Empty;
          if (this._attrValues != null)
          {
            this._measures = MeasureEditor.GetMeasureDescriptorListByAttributeId(this._attrValues.AttributeID);
            foreach (MeasureDescriptor measure in this._measures)
            {
              strValue = measure.ShortName.Trim();
              if (!this._measuresSName.Contains(strValue))
                this._measuresSName.Add(strValue);
            }
          }
          this.Error = this.CheckValue(out strValue) ? (!this._disableNulls || !this.EnabledCtrl || !string.IsNullOrEmpty(this._txt.Text.Trim()) ? string.Empty : this._errMsg_NullValue) : this._errMsg_NotValidFormat;
        }
      }
      else
      {
        this._txt.Text = string.Empty;
        this.ModifiedInLoad = false;
      }
      this.AllowDefaultValue = false;
    }
  }

  /// <summary>
  /// Устанавливает и возвращает произошло ли изменение данных.
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public override bool Modified
  {
    get => base.Modified;
    set
    {
      if (!value && this.ModifiedInLoad)
        return;
      base.Modified = value;
    }
  }

  /// <summary>Доступность контрола.</summary>
  [System.ComponentModel.DefaultValue(true)]
  public override bool EnabledCtrl
  {
    get => this._enabled;
    set
    {
      this._enabled = value;
      if (!this.IsDesignMode)
      {
        if (this._enabled)
          this._enabled = this._attrValues == null || !(this._attrValues.AttributeGuid == Guid.Empty);
        this._txt.ReadOnly = !this._enabled;
        if (!this._enabled)
        {
          if (this._txt.BackColor.ToArgb() == SystemColors.Window.ToArgb())
            this._txt.BackColor = SystemColors.Control;
        }
        else if (this._txt.BackColor == SystemColors.Control)
          this._txt.BackColor = SystemColors.Window;
      }
      this.CheckAccessibilityButtons();
    }
  }

  /// <summary>
  /// Возможность использовать атрибут в экспертной системе.
  /// </summary>
  [System.ComponentModel.DefaultValue(false)]
  public bool UseInExpertSystem
  {
    get => this._useInExpertSystem;
    set
    {
      this._useInExpertSystem = value && ExpertSystem.IsExpertSystemExists();
      if (this._useInExpertSystem)
      {
        if (this._btnCalc == null)
        {
          this._btnCalc = new ControlButton("Calc", 1);
          this._btnReCalc = new ControlButton("ReCalc", 2);
          if (!this.IsDesignMode)
          {
            this._btnCalc.Click += new EventHandler(this.On_expBtn_CalcClick);
            this._btnReCalc.Click += new EventHandler(this.On_expBtn_ReCalcClick);
          }
        }
        this.AddRightButtons(new List<ControlButton>()
        {
          this._btnCalc,
          this._btnReCalc
        });
      }
      else if (this._btnCalc != null)
        this.RemoveRightButtons(new List<ControlButton>()
        {
          this._btnCalc,
          this._btnReCalc
        });
      this.CheckAccessibilityButtons();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLeaveControl(EventArgs e)
  {
    string strValue = string.Empty;
    if (!this.CheckValue(out strValue))
      return;
    base.OnLeaveControl(e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="text"></param>
  protected override void SetDesignText(string text)
  {
    base.SetDesignText(text);
    if (this._txt.Text == this._designText)
      this._txt.Text = text;
    this._designText = text;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private bool CanUseDefaultValue()
  {
    bool flag = false;
    if (this._attrValues != null && !this._attrValues.ReadOnly)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if ((this.GetAttributeOptions(this._attrValues.AttributeID, sessionKeeper.Session) & AttributeOptions.DisableManualEdit) == AttributeOptions.None)
          flag = this.ValueIsEmpty && this.AllowDefaultValue && this.DefaultValue != null;
      }
    }
    return flag;
  }

  /// <summary>Проверка доступности кнопок.</summary>
  private void CheckAccessibilityButtons()
  {
    this._btnDots.Enabled = this.EnabledCtrl;
    if (this._btnCalc == null)
      return;
    if (this.IsDesignMode)
      this._btnCalc.Enabled = this._btnReCalc.Enabled = this.AttributeInfo != null;
    else
      this._btnCalc.Enabled = this._btnReCalc.Enabled = this._attrValues != null;
  }

  private bool IsAttrShortNameInString
  {
    get
    {
      if (!this._isAttrShortNameInString_CheckedFlag)
      {
        this._isAttrShortNameInString_CheckedFlag = true;
        this._isAttrShortNameInString = true;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBAttributeType4 dbAttributeType4 = (IDBAttributeType4) null;
          if (this.ParentInfo != null)
          {
            if (this.ParentInfo.ElementKind == AttributableElements.Object)
            {
              int objectTypeId = sessionKeeper.Session.GetObjectInfo(this.ParentInfo.ElementIdentifier).ObjectTypeID;
              IDBObjectType objectType = sessionKeeper.Session.GetObjectType(objectTypeId, false);
              if (objectType != null)
                dbAttributeType4 = objectType.Attributes.GetAttributeByGUID(this._attrInfo.AttributeGuid);
            }
            else if (this.ParentInfo.ElementKind == AttributableElements.Relation)
            {
              IDBRelation relation = sessionKeeper.Session.GetRelation(this.ParentInfo.ElementIdentifier, false);
              if (relation != null)
              {
                IDBRelationType relationType = sessionKeeper.Session.GetRelationType(relation.RelationType, false);
                if (relationType != null)
                  dbAttributeType4 = relationType.Attributes.GetAttributeByGUID(this._attrInfo.AttributeGuid);
              }
            }
          }
          if (dbAttributeType4 != null)
          {
            if (dbAttributeType4 is IDBMeasureAttributeType)
              this._isAttrShortNameInString = ((IDBMeasureAttributeType) dbAttributeType4).ShortNameInString;
          }
        }
      }
      return this._isAttrShortNameInString;
    }
  }

  protected override void ClearAttributeInfoCachedOptions()
  {
    base.ClearAttributeInfoCachedOptions();
    this._isAttrShortNameInString_CheckedFlag = false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="strValue"></param>
  /// <returns></returns>
  private bool CheckValue(out string strValue)
  {
    bool flag = true;
    strValue = "0";
    if (!string.IsNullOrEmpty(this._txt.Text.Trim()))
    {
      double number = 0.0;
      string textBeforeNumber = string.Empty;
      string textAfterNumber = string.Empty;
      int num = NumberParserAdvanced.ParseNumber(this._txt.Text, true, out number, out textBeforeNumber, out textAfterNumber) ? 1 : 0;
      textAfterNumber = textAfterNumber.Trim();
      if (num == 0 || !string.IsNullOrEmpty(textAfterNumber) && !this._measuresSName.Contains(textAfterNumber))
      {
        flag = false;
      }
      else
      {
        string str = string.Empty;
        if (string.IsNullOrEmpty(textAfterNumber) && this.DefMeasure != null)
        {
          if (this.IsAttrShortNameInString)
            str = this.DefMeasure.ShortName.Trim();
        }
        else
          str = textAfterNumber;
        this._txt.Text = strValue = $"{number.ToString("#################0.#################")} {str}";
      }
    }
    return flag;
  }

  /// <summary>Let's find deafult measure.</summary>
  private void LoadDefaultMeasure()
  {
    if (this.DefMeasure != null)
      return;
    if (this.ParentInfo == null)
    {
      this.DefMeasure = this._measuresSName.Count > 0 ? MeasureHelper.FindDescriptor(this._measuresSName[0]) : (MeasureDescriptor) null;
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        long measureID = !string.IsNullOrEmpty(this.DefaultMeasured) ? sessionKeeper.Session.GetObjectInfo(new Guid(this.DefaultMeasured)).ObjectID : 0L;
        if (measureID == 0L)
        {
          long physicalQuantityID = MetaDataHelper.GetAttributeType(this.AttributeInfo.AttributeGuid).SizeType;
          if (this.ParentTypeID == -1)
          {
            if (this.ParentInfo.ElementKind == AttributableElements.Object)
              this.ParentTypeID = sessionKeeper.Session.GetObjectInfo(this.ParentInfo.ElementIdentifier).ObjectTypeID;
            else if (this.ParentInfo.ElementKind == AttributableElements.Relation)
            {
              IDBRelation relation = sessionKeeper.Session.GetRelation(this.ParentInfo.ElementIdentifier, false);
              this.ParentTypeID = relation != null ? relation.RelationType : -1;
            }
          }
          if (ClientCommons.GetAttributableType(this.ParentTypeID, this.ParentInfo.ElementKind).Attributes.GetAttributeByID(this._attrValues.AttributeID) is IDBMeasureAttributeType attributeById)
          {
            measureID = attributeById.DefaultMeasureID;
            if (measureID == 0L)
            {
              IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(this._attrValues.AttributeID);
              if (attributeType != null && attributeType.AttributeType == FieldTypes.ftMeasured && attributeType.PropertiesStructure.MetadataExtensions != null && attributeType.PropertiesStructure.MetadataExtensions.Contains((object) "MU_PHYSICAL_ID"))
              {
                object metadataExtension = attributeType.PropertiesStructure.MetadataExtensions[(object) "MU_PHYSICAL_ID"];
                if (metadataExtension != null)
                {
                  List<long> longList = new List<long>((IEnumerable<long>) (long[]) metadataExtension);
                  physicalQuantityID = longList.Count > 0 ? longList[0] : physicalQuantityID;
                }
              }
              measureID = MeasureHelper.GetBaseMeasureID(physicalQuantityID);
            }
          }
          else
            measureID = MeasureHelper.GetBaseMeasureID(physicalQuantityID);
        }
        this.DefMeasure = measureID > 0L || MeasureHelper.Measures.Length == 0 ? MeasureHelper.FindDescriptor(measureID) : (this._measures.Count > 0 ? this._measures[0] as MeasureDescriptor : MeasureHelper.Measures[0]);
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public void ButtonPerformClick() => this.On_btn_Click((object) this._btnDots, new EventArgs());

  /// <summary>Обновить единицу измерения по умолчанию (#1549669)</summary>
  public void UpdateDefMeasure()
  {
    if (AttrMeasuredEdit.defMeasures.ContainsKey(this.AttributeInfo.AttributeGuid))
      this.DefMeasure = AttrMeasuredEdit.defMeasures[this.AttributeInfo.AttributeGuid];
    else
      this.LoadDefaultMeasure();
  }

  /// <summary>Необходимость сериализации свойства Font.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeFont() => !base.Font.Equals((object) this._txt.Font);

  /// <summary>Необходимость сериализации свойства Text.</summary>
  /// <returns>Результат проверки</returns>
  private bool ShouldSerializeText()
  {
    return !string.IsNullOrEmpty(this._designText) ? this._txt.Text != this._designText : !string.IsNullOrEmpty(this._txt.Text);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this._txt.SizeChanged -= new EventHandler(this.On_txt_SizeChanged);
      this._txt.TextChanged -= new EventHandler(this.On_txt_TextChanged);
      this._txt.KeyDown -= new KeyEventHandler(this.On_txt_KeyDown);
      this._txt.GotFocus -= new EventHandler(this.On_txt_GotFocus);
      this._txt.LostFocus -= new EventHandler(this.On_txt_LostFocus);
      if (!this.IsDesignMode)
      {
        if (this._btnDots != null)
          this._btnDots.Click -= new EventHandler(this.On_btn_Click);
        if (this._btnCalc != null)
        {
          this._btnCalc.Click -= new EventHandler(this.On_expBtn_CalcClick);
          this._btnReCalc.Click -= new EventHandler(this.On_expBtn_ReCalcClick);
        }
      }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AttrMeasuredEdit));
    this._txt = new TextBox();
    ((ISupportInitialize) this._err).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._txt, "_txt");
    this._txt.Name = "_txt";
    this._txt.SizeChanged += new EventHandler(this.On_txt_SizeChanged);
    this._txt.TextChanged += new EventHandler(this.On_txt_TextChanged);
    this._txt.KeyDown += new KeyEventHandler(this.On_txt_KeyDown);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._txt);
    this._err.SetIconAlignment((Control) this, (ErrorIconAlignment) componentResourceManager.GetObject("$this.IconAlignment"));
    this._err.SetIconPadding((Control) this, (int) componentResourceManager.GetObject("$this.IconPadding"));
    this.Name = nameof (AttrMeasuredEdit);
    ((ISupportInitialize) this._err).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
