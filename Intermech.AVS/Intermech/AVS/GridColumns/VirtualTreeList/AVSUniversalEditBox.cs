// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.GridColumns.VirtualTreeList.AVSUniversalEditBox
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Infralution.Controls;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.AVS.GridColumns.VirtualTreeList;

/// <summary>
/// Элемент редактирования использующий конвертеры и UITypeEditor для редактирования, аналог Infralution.UniversalEditBox
/// </summary>
[ToolboxItem(true)]
public class AVSUniversalEditBox : 
  BorderedControl,
  IWindowsFormsEditorService,
  ITypeDescriptorContext,
  System.IServiceProvider
{
  private AVSWindow aVSWindow;
  private bool _autoSize;
  private TypeConverter _converter;
  private Color _dropDownBackColor;
  private DropDownButton _dropDownButton;
  private Color _dropDownForeColor;
  private DropDownForm _dropDownForm;
  private Button _editButton;
  private UITypeEditor _editor;
  private DateTime _editTime;
  private Panel _previewPanel;
  private bool _showPreview;
  private bool _showText;
  private object[] _standardValues;
  private TextBox _textBox;
  private string _textErrorCaption;
  private string _textErrorMessage;
  private bool _useDefaultConverter;
  private bool _useDefaultEditor;
  private object _value;
  private string _valueText;
  private object _valueOwner;
  private System.Type _valueType;
  private System.ComponentModel.Container components;
  private const string DefaultTextErrorCaption = "Invalid Value";
  private const string DefaultTextErrorMessage = "{0} is not a valid {1} value";

  [Category("Behavior")]
  [Description("Event fired when validation of user entered text fails")]
  public event ValidateTextErrorHandler ValidateTextError;

  [Category("Property Changed")]
  [Description("Event fired when the Value property is changed")]
  public event EventHandler ValueChanged;

  public AVSUniversalEditBox()
  {
    this._useDefaultEditor = true;
    this._useDefaultConverter = true;
    this._showPreview = true;
    this._showText = true;
    this._autoSize = true;
    this._dropDownBackColor = Color.Empty;
    this._dropDownForeColor = Color.Empty;
    this._textErrorCaption = "Invalid Value";
    this._textErrorMessage = "{0} is not a valid {1} value";
    this.InitializeComponent();
    this.BackColor = SystemColors.Window;
    this.ForeColor = SystemColors.WindowText;
    this.TextBox.ShortcutsEnabled = false;
    this.TextBox.TextChanged += new EventHandler(this.TextBox_TextChanged);
  }

  private void TextBox_TextChanged(object sender, EventArgs e)
  {
  }

  public AVSWindow AVSWindow
  {
    get => this.aVSWindow;
    set => this.aVSWindow = value;
  }

  private void _textBox_GotFocus(object sender, EventArgs e) => base.OnGotFocus(e);

  protected virtual void CancelTextEntry() => this.UpdateText();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  protected virtual void EditValue()
  {
    if (this._editor == null || DateTime.Now.Subtract(this._editTime).TotalMilliseconds <= 200.0)
      return;
    this.Value = this._editor.EditValue((ITypeDescriptorContext) this, (System.IServiceProvider) this, (object) this.TextBox.Text);
    this._editTime = DateTime.Now;
  }

  protected virtual string GetTextForValue(object value)
  {
    try
    {
      if (this.Converter != null)
      {
        if (this.Converter.CanConvertTo((ITypeDescriptorContext) this, typeof (string)))
          return this.Converter.ConvertToString((ITypeDescriptorContext) this, value);
      }
    }
    catch
    {
    }
    return value != null ? value.ToString() : string.Empty;
  }

  protected virtual bool HandleTextConversionError(Exception e)
  {
    ValidateTextErrorEventArgs args = new ValidateTextErrorEventArgs(this.TextBox.Text, e);
    this.OnValidateTextError(args);
    if (!args.Handled && MessageBox.Show((IWin32Window) this, string.Format(this.TextErrorMessage, (object) this.TextBox.Text, (object) this.ValueType.Name), this.TextErrorCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
      args.ResetText = true;
    if (args.ResetText)
      this.CancelTextEntry();
    return args.ResetText;
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AVSUniversalEditBox));
    this._textBox = new TextBox();
    this._editButton = new Button();
    this._previewPanel = new Panel();
    this._dropDownButton = new DropDownButton();
    this.SuspendLayout();
    this._textBox.Dock = DockStyle.Fill;
    this._textBox.Location = new Point(24, 0);
    this._textBox.Name = "_textBox";
    this._textBox.Size = new Size(133, 20);
    this._textBox.TabIndex = 0;
    this._textBox.TabStop = false;
    this._textBox.DoubleClick += new EventHandler(this.OnDoubleClick);
    this._textBox.GotFocus += new EventHandler(this._textBox_GotFocus);
    this._textBox.KeyDown += new KeyEventHandler(this.OnTextKeyDown);
    this.TextBox.MouseDown += new MouseEventHandler(this.TextBox_MouseDown);
    this.TextBox.MouseUp += new MouseEventHandler(this.TextBox_MouseUp);
    this._editButton.BackColor = SystemColors.Control;
    this._editButton.Dock = DockStyle.Right;
    this._editButton.ForeColor = SystemColors.ControlText;
    this._editButton.Image = (Image) componentResourceManager.GetObject("_editButton.Image");
    this._editButton.ImageAlign = ContentAlignment.BottomCenter;
    this._editButton.Location = new Point(157, 0);
    this._editButton.Name = "_editButton";
    this._editButton.Size = new Size(17, 20);
    this._editButton.TabIndex = 1;
    this._editButton.TabStop = false;
    this._editButton.Text = "...";
    this._editButton.UseVisualStyleBackColor = false;
    this._editButton.Visible = false;
    this._editButton.Click += new EventHandler(this.OnEditButtonClick);
    this._previewPanel.BackColor = SystemColors.Window;
    this._previewPanel.Dock = DockStyle.Left;
    this._previewPanel.ForeColor = SystemColors.WindowText;
    this._previewPanel.Location = new Point(0, 0);
    this._previewPanel.Name = "_previewPanel";
    this._previewPanel.Size = new Size(24, 20);
    this._previewPanel.TabIndex = 2;
    this._previewPanel.Click += new EventHandler(this.OnClick);
    this._previewPanel.Paint += new PaintEventHandler(this.OnPreviewPaint);
    this._previewPanel.DoubleClick += new EventHandler(this.OnDoubleClick);
    this._dropDownButton.Dock = DockStyle.Right;
    this._dropDownButton.Image = (Image) componentResourceManager.GetObject("_dropDownButton.Image");
    this._dropDownButton.Location = new Point(174, 0);
    this._dropDownButton.Name = "_dropDownButton";
    this._dropDownButton.Size = new Size(17, 20);
    this._dropDownButton.TabIndex = 0;
    this._dropDownButton.TabStop = false;
    this._dropDownButton.Visible = false;
    this._dropDownButton.Click += new EventHandler(this.OnEditButtonClick);
    this.BorderStyle = BorderStyle.Fixed3D;
    this.Controls.Add((Control) this._textBox);
    this.Controls.Add((Control) this._previewPanel);
    this.Controls.Add((Control) this._editButton);
    this.Controls.Add((Control) this._dropDownButton);
    this.Size = new Size(195, 24);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void TextBox_MouseUp(object sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Right || !(sender is Control))
      return;
    this.AVSWindow.ContextMenuBarItem.Show(sender as Control, e.Location);
  }

  private void TextBox_MouseDown(object sender, MouseEventArgs e)
  {
  }

  private void OnClick(object sender, EventArgs e) => this.TextBox.Focus();

  protected override void OnDoubleClick(EventArgs e)
  {
    if (this.StandardValues != null)
      this.StepStandardValue(1, true);
    base.OnDoubleClick(e);
  }

  private void OnDoubleClick(object sender, EventArgs e) => this.OnDoubleClick(e);

  protected virtual void OnEditButtonClick(object sender, EventArgs e) => this.EditValue();

  protected override void OnFontChanged(EventArgs e)
  {
    this.PerformLayout();
    base.OnFontChanged(e);
  }

  protected override void OnGotFocus(EventArgs e)
  {
    base.OnGotFocus(e);
    this.TextBox.Focus();
  }

  protected override void OnMouseUp(MouseEventArgs e) => base.OnMouseUp(e);

  protected override void OnKeyDown(KeyEventArgs e)
  {
    e.Handled = true;
    switch (e.KeyCode)
    {
      case Keys.Return:
        this.ValidateText();
        break;
      case Keys.Escape:
        this.CancelTextEntry();
        break;
      case Keys.Next:
        this.EditValue();
        break;
      case Keys.Up:
        this.StepStandardValue(-1, false);
        break;
      case Keys.Down:
        if (this.StandardValues != null)
        {
          this.StepStandardValue(1, false);
          break;
        }
        this.EditValue();
        break;
      default:
        e.Handled = false;
        break;
    }
    base.OnKeyDown(e);
  }

  protected override void OnLayout(LayoutEventArgs levent)
  {
    if (this.AVSWindow == null || this.AVSWindow.AVSDocument == null)
      return;
    if (this.RightToLeft == RightToLeft.No)
    {
      this._editButton.Dock = DockStyle.Right;
      this._dropDownButton.Dock = DockStyle.Right;
      this._previewPanel.Dock = this.ShowText ? DockStyle.Left : DockStyle.Fill;
    }
    else
    {
      this._editButton.Dock = DockStyle.Left;
      this._dropDownButton.Dock = DockStyle.Left;
      this._previewPanel.Dock = this.ShowText ? DockStyle.Right : DockStyle.Fill;
    }
    this._textBox.Visible = this.ShowText;
    this._textBox.ReadOnly = !this.TextEditable;
    this._previewPanel.Visible = this.ShowPreview && this.PreviewSupported;
    this.UpdateButtonVisibility();
    if (this._autoSize)
      this.Height = this.PreferredHeight;
    base.OnLayout(levent);
    this._textBox.Top = (this.ClientSize.Height - this._textBox.Height) / 2;
  }

  protected override void OnParentBackColorChanged(EventArgs e)
  {
    base.OnParentBackColorChanged(e);
    this.UpdateComponentControlColors();
  }

  protected override void OnParentChanged(EventArgs e)
  {
    base.OnParentChanged(e);
    this.UpdateComponentControlColors();
  }

  protected override void OnParentForeColorChanged(EventArgs e)
  {
    base.OnParentForeColorChanged(e);
    this.UpdateComponentControlColors();
  }

  protected virtual void OnPreviewPaint(object sender, PaintEventArgs e)
  {
    if (!this.PreviewSupported)
      return;
    Rectangle bounds = new Rectangle(2, 2, this._previewPanel.Width - 4, this._previewPanel.Height - 4);
    this._editor.PaintValue(new PaintValueEventArgs((ITypeDescriptorContext) this, this._value, e.Graphics, bounds));
  }

  protected override void OnRightToLeftChanged(EventArgs e)
  {
    base.OnRightToLeftChanged(e);
    this.PerformLayout();
  }

  private void OnTextKeyDown(object sender, KeyEventArgs e) => this.OnKeyDown(e);

  protected virtual void OnValidateTextError(ValidateTextErrorEventArgs args)
  {
    ValidateTextErrorHandler validateTextError = this.ValidateTextError;
    if (validateTextError == null)
      return;
    validateTextError((object) this, args);
  }

  protected override void OnValidating(CancelEventArgs e)
  {
    if (this.TextEditable && this.ValueText != this.TextBox.Text)
      e.Cancel = !this.ValidateText();
    base.OnValidating(e);
  }

  protected virtual void OnValueChanged()
  {
    EventHandler valueChanged = this.ValueChanged;
    if (valueChanged == null)
      return;
    valueChanged((object) this, new EventArgs());
  }

  protected override void OnVisibleChanged(EventArgs e) => base.OnVisibleChanged(e);

  protected override bool ProcessDialogKey(Keys keyData)
  {
    return keyData == (Keys.Tab | Keys.Shift) ? this.Parent.SelectNextControl((Control) this, false, true, true, true) : base.ProcessDialogKey(keyData);
  }

  protected virtual void SetDefaultConverter()
  {
    if (this._valueType != (System.Type) null)
      this.Converter = TypeDescriptor.GetConverter(this._valueType);
    else
      this.Converter = (TypeConverter) null;
  }

  protected virtual void SetDefaultEditor()
  {
    UITypeEditor uiTypeEditor = (UITypeEditor) null;
    if (this._valueType != (System.Type) null)
      uiTypeEditor = (UITypeEditor) TypeDescriptor.GetEditor(this._valueType, typeof (UITypeEditor));
    if (uiTypeEditor == null && this.Converter != null && this.Converter.GetStandardValuesSupported((ITypeDescriptorContext) this))
      uiTypeEditor = (UITypeEditor) new StandardValueEditor(this.Converter);
    this.Editor = uiTypeEditor;
  }

  private bool ShouldSerializeDropDownBackColor() => this._dropDownBackColor != Color.Empty;

  private bool ShouldSerializeDropDownForeColor() => this._dropDownForeColor != Color.Empty;

  protected virtual void StepStandardValue(int step, bool circular)
  {
    object[] standardValues = this.StandardValues;
    if (standardValues == null)
      return;
    int num = Array.IndexOf<object>(standardValues, this.Value);
    int index;
    if (num < 0)
    {
      index = 0;
    }
    else
    {
      index = num + step;
      if (index < 0)
        index = circular ? standardValues.Length - 1 : 0;
      else if (index >= standardValues.Length)
        index = circular ? 0 : standardValues.Length - 1;
    }
    this.Value = standardValues[index];
    this.TextBox.SelectAll();
  }

  void ITypeDescriptorContext.OnComponentChanged()
  {
  }

  bool ITypeDescriptorContext.OnComponentChanging() => true;

  object System.IServiceProvider.GetService(System.Type serviceType)
  {
    return serviceType == typeof (IWindowsFormsEditorService) ? (object) this : (object) null;
  }

  void IWindowsFormsEditorService.CloseDropDown()
  {
    if (this._dropDownForm == null)
      return;
    this._dropDownForm.Hide();
  }

  void IWindowsFormsEditorService.DropDownControl(Control control)
  {
    DropDownForm dropDownForm = new DropDownForm();
    dropDownForm.ManageContainedControlDisposal = false;
    dropDownForm.BackColor = this.BackColor;
    dropDownForm.ForeColor = this.ForeColor;
    control.CreateControl();
    dropDownForm.FormBorderStyle = FormBorderStyle.None;
    if (control.GetType().Name == "DateTimeUI")
      dropDownForm.Width = control.Width + 4;
    else
      dropDownForm.Width = Math.Max(this.Width, control.Width + 4);
    dropDownForm.Height = control.Height + 4;
    dropDownForm.ContainedControl = control;
    this._dropDownForm = dropDownForm;
    dropDownForm.ShowModal((Control) this);
    this._dropDownForm = (DropDownForm) null;
    dropDownForm.Close();
    dropDownForm.Dispose();
  }

  DialogResult IWindowsFormsEditorService.ShowDialog(Form dialog) => dialog.ShowDialog();

  protected virtual void UpdateButtonVisibility()
  {
    UITypeEditorEditStyle typeEditorEditStyle = this._editor == null ? UITypeEditorEditStyle.None : this._editor.GetEditStyle((ITypeDescriptorContext) this);
    this._editButton.Visible = typeEditorEditStyle == UITypeEditorEditStyle.Modal;
    this._dropDownButton.Visible = typeEditorEditStyle == UITypeEditorEditStyle.DropDown;
  }

  protected virtual void UpdateComponentControlColors()
  {
    this._textBox.BackColor = this.BackColor;
    this._textBox.ForeColor = this.ForeColor;
    this._previewPanel.BackColor = this.BackColor;
    this._editButton.BackColor = this.DropDownBackColor;
    this._editButton.ForeColor = this.DropDownForeColor;
    this._dropDownButton.BackColor = this.DropDownBackColor;
    this._dropDownButton.ForeColor = this.DropDownForeColor;
    if (this._dropDownForm == null)
      return;
    this._dropDownForm.BackColor = this.BackColor;
    this._dropDownForm.ForeColor = this.ForeColor;
  }

  protected virtual void UpdateStandardValues()
  {
    if (this.Converter != null && this.Converter.GetStandardValuesSupported((ITypeDescriptorContext) this))
    {
      ICollection standardValues = this.Converter.GetStandardValues();
      this._standardValues = new object[standardValues.Count];
      standardValues.CopyTo((Array) this._standardValues, 0);
    }
    else
      this._standardValues = (object[]) null;
  }

  protected virtual void UpdateText()
  {
    this.ValueText = this.GetTextForValue(this.Value);
    this.TextBox.Text = this.ValueText;
  }

  protected virtual bool ValidateText()
  {
    try
    {
      this.Value = this.Converter.ConvertFromString((ITypeDescriptorContext) this, CultureInfo.CurrentCulture, this.TextBox.Text);
    }
    catch (Exception ex)
    {
      return this.HandleTextConversionError(ex);
    }
    return true;
  }

  [Browsable(true)]
  [Description("Set/Get whether the control height should be set automatically based on the font height")]
  [Category("Layout")]
  [DefaultValue(true)]
  public override bool AutoSize
  {
    get => this._autoSize;
    set
    {
      this._autoSize = value;
      this.SetStyle(ControlStyles.FixedHeight, value);
      this.PerformLayout();
    }
  }

  [Description("Set/Get the background color for the control")]
  [Category("Appearance")]
  [DefaultValue(typeof (Color), "Window")]
  public override Color BackColor
  {
    get => base.BackColor;
    set
    {
      if (!(value != base.BackColor))
        return;
      base.BackColor = value;
      this.UpdateComponentControlColors();
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public TypeConverter Converter
  {
    get => this._converter;
    set
    {
      if (this._converter == value)
        return;
      this._useDefaultConverter = false;
      this._converter = value;
      if (this._useDefaultEditor)
        this.SetDefaultEditor();
      this.UpdateStandardValues();
      this.PerformLayout();
    }
  }

  [Description("Set/Get the background color of the drop down button")]
  [Category("Appearance")]
  [AmbientValue(typeof (Color), "")]
  public virtual Color DropDownBackColor
  {
    get
    {
      if (this._dropDownBackColor != Color.Empty)
        return this._dropDownBackColor;
      return this.Parent != null ? this.Parent.BackColor : SystemColors.Control;
    }
    set
    {
      this._dropDownBackColor = value;
      this.UpdateComponentControlColors();
    }
  }

  protected DropDownButton DropDownButton => this._dropDownButton;

  [Description("Set/Get the foreground color of the drop down button")]
  [AmbientValue(typeof (Color), "")]
  [Category("Appearance")]
  public virtual Color DropDownForeColor
  {
    get
    {
      if (this._dropDownForeColor != Color.Empty)
        return this._dropDownForeColor;
      return this.Parent != null ? this.Parent.ForeColor : SystemColors.ControlText;
    }
    set
    {
      this._dropDownForeColor = value;
      this.UpdateComponentControlColors();
    }
  }

  protected Button EditButton => this._editButton;

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public UITypeEditor Editor
  {
    get => this._editor;
    set
    {
      this._useDefaultEditor = false;
      this._editor = value;
      this.PerformLayout();
    }
  }

  protected virtual bool EditValueSupported
  {
    get
    {
      return this._editor != null && this._editor.GetEditStyle((ITypeDescriptorContext) this) != UITypeEditorEditStyle.None;
    }
  }

  [Category("Appearance")]
  [DefaultValue(typeof (Color), "WindowText")]
  [Description("Set/Get the foreground color for the control")]
  public override Color ForeColor
  {
    get => base.ForeColor;
    set
    {
      if (!(value != base.ForeColor))
        return;
      base.ForeColor = value;
      this.UpdateComponentControlColors();
    }
  }

  protected virtual int PreferredHeight
  {
    get
    {
      int num = 0;
      switch (this.BorderStyle)
      {
        case BorderStyle.None:
          num = 0;
          break;
        case BorderStyle.FixedSingle:
        case BorderStyle.Fixed3D:
          num = 7;
          break;
      }
      return this.Font.Height + num;
    }
  }

  protected Panel PreviewPanel => this._previewPanel;

  protected virtual bool PreviewSupported
  {
    get
    {
      return this._editor != null && this._editor.GetPaintValueSupported((ITypeDescriptorContext) this);
    }
  }

  [Category("Layout")]
  [Description("Set/Get the width of the preview area")]
  [DefaultValue(24)]
  public virtual int PreviewWidth
  {
    get => this._previewPanel.Width;
    set => this._previewPanel.Width = value;
  }

  [Category("Appearance")]
  [DefaultValue(true)]
  [Description("Set/Get whether control should display a graphic preview of the current value")]
  public virtual bool ShowPreview
  {
    get => this._showPreview;
    set
    {
      this._showPreview = value;
      this.PerformLayout();
    }
  }

  [DefaultValue(true)]
  [Category("Appearance")]
  [Description("Set/Get whether control should display the text representation of the current value")]
  public virtual bool ShowText
  {
    get => this._showText;
    set
    {
      this._showText = value;
      this.PerformLayout();
    }
  }

  protected object[] StandardValues => this._standardValues;

  IContainer ITypeDescriptorContext.Container => this.Site?.Container;

  object ITypeDescriptorContext.Instance => this._valueOwner;

  PropertyDescriptor ITypeDescriptorContext.PropertyDescriptor => (PropertyDescriptor) null;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [EditorBrowsable(EditorBrowsableState.Never)]
  public override string Text
  {
    get => base.Text;
    set => base.Text = value;
  }

  internal TextBox TextBox => this._textBox;

  protected virtual bool TextEditable
  {
    get
    {
      return this.ShowText && this.Converter != null && this.Converter.CanConvertFrom((ITypeDescriptorContext) this, typeof (string)) && !this.Converter.GetStandardValuesExclusive((ITypeDescriptorContext) this);
    }
  }

  [Category("Behavior")]
  [Description("Set/Get the caption to use when displaying text validation errors")]
  [DefaultValue("Invalid Value")]
  public virtual string TextErrorCaption
  {
    get => this._textErrorCaption;
    set => this._textErrorCaption = value;
  }

  [DefaultValue("{0} is not a valid {1} value")]
  [Description("Set/Get the message to use when displaying text validation errors")]
  [Category("Behavior")]
  public virtual string TextErrorMessage
  {
    get => this._textErrorMessage;
    set => this._textErrorMessage = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool UseDefaultConverter
  {
    get => this._useDefaultConverter;
    set => this._useDefaultConverter = value;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool UseDefaultEditor
  {
    get => this._useDefaultEditor;
    set => this._useDefaultEditor = value;
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public virtual string ValueText
  {
    get => this._valueText;
    set => this._valueText = value;
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public virtual object Value
  {
    get => this._value;
    set
    {
      if ((this._value == null ? (value == null ? 1 : 0) : (this._value.Equals(value) ? 1 : 0)) == 0)
      {
        this._value = value;
        if (value != null && !Convert.IsDBNull(value))
          this.ValueType = value.GetType();
        this.OnValueChanged();
      }
      this.UpdateText();
      this.PreviewPanel.Invalidate();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual object ValueOwner
  {
    get => this._valueOwner;
    set => this._valueOwner = value;
  }

  [Category("Data")]
  [DefaultValue(null)]
  [System.ComponentModel.Editor("Infralution.Controls.Design.ObjectTypeEditor, Infralution.Controls.Design, Version=3.1.4.0, Culture=neutral, PublicKeyToken=3e7e8e3744a5c13f", typeof (UITypeEditor))]
  [TypeConverter("Infralution.Controls.Design.ObjectTypeConverter, Infralution.Controls.Design, Version=3.1.4.0, Culture=neutral, PublicKeyToken=3e7e8e3744a5c13f")]
  [Description("The type of the value to be edited")]
  public virtual System.Type ValueType
  {
    get => this._valueType;
    set
    {
      if (!(this._valueType != value))
        return;
      this._valueType = value;
      if (this._useDefaultConverter)
        this.SetDefaultConverter();
      if (!this._useDefaultEditor)
        return;
      this.SetDefaultEditor();
    }
  }
}
