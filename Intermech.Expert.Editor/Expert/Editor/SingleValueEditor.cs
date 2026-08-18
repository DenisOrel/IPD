// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.SingleValueEditor
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Контрол редактор одиночного значения</summary>
internal class SingleValueEditor : UserControl
{
  private Label label1;
  private Control _editor;
  private System.ComponentModel.Container components;
  private CommonTypeHolder _commonHolder;
  private DataType _dataType = DataType.String;
  private AttributeTypeHolder _attrType;
  private TextBox textBox1;
  private ArrayList _possibleValues;
  private ArrayList _possibleDescriptions;
  internal static List<int> classifTypes = MetaDataHelper.GetObjectTypeChildrenIDRecursive(MetaDataHelper.GetObjectTypeID(new Guid("cad00157-306c-11d8-b4e9-00304f19f545")));

  public SingleValueEditor(
    CommonTypeHolder commonHolder,
    DataType dataType,
    IList possibleValues,
    IList possibleDescriptions)
  {
    if (commonHolder == null)
      throw new ArgumentNullException(nameof (commonHolder));
    this.InitializeComponent();
    this._commonHolder = commonHolder;
    this._dataType = dataType;
    this._attrType = commonHolder.AttributeType;
    this._possibleValues = new ArrayList((ICollection) possibleValues);
    this._possibleDescriptions = new ArrayList((ICollection) possibleDescriptions);
    if (this._attrType.MasterAttributeID == 0)
    {
      switch (this._dataType)
      {
        case DataType.Integer:
        case DataType.Float:
        case DataType.String:
          if (this._possibleValues.Count > 0)
          {
            this._editor = (Control) new System.Windows.Forms.ComboBox();
            System.Windows.Forms.ComboBox editor = this._editor as System.Windows.Forms.ComboBox;
            editor.DropDownStyle = ComboBoxStyle.DropDownList;
            editor.BeginUpdate();
            try
            {
              if (this._possibleDescriptions.Count > 0)
              {
                editor.Items.AddRange(this._possibleDescriptions.ToArray());
                break;
              }
              editor.Items.AddRange(this._possibleValues.ToArray());
              break;
            }
            finally
            {
              editor.EndUpdate();
            }
          }
          else
          {
            this._editor = (Control) new TextBox();
            break;
          }
        case DataType.Measured:
          this._editor = (Control) new AttrMeasuredEdit();
          (this._editor as AttrMeasuredEdit).AttributeInfo = new AttributeInfo(this._attrType.Guid, Guid.Empty);
          break;
        case DataType.Date:
          this._editor = (Control) new DateEdit();
          DateEdit editor1 = this._editor as DateEdit;
          editor1.Properties.EditFormat.FormatString = "g";
          editor1.Properties.DisplayFormat.FormatString = "g";
          break;
        case DataType.Boolean:
          this._editor = (Control) new CheckBox();
          break;
        case DataType.ObjectLink:
          this._editor = (Control) new ButtonEdit();
          ButtonEdit editor2 = this._editor as ButtonEdit;
          editor2.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
          editor2.ButtonClick += new ButtonPressedEventHandler(this.editor_ButtonClick);
          editor2.DoubleClick += new EventHandler(this.editor_DoubleClick);
          editor2.KeyDown += new KeyEventHandler(this.editor_KeyDown);
          break;
      }
    }
    else
    {
      this._editor = (Control) new ButtonEdit();
      ButtonEdit editor3 = this._editor as ButtonEdit;
      editor3.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
      editor3.ButtonClick += new ButtonPressedEventHandler(this.editor_ButtonClick);
      editor3.DoubleClick += new EventHandler(this.editor_DoubleClick);
      editor3.KeyDown += new KeyEventHandler(this.editor_KeyDown);
    }
    if (this._editor == null)
      return;
    this.SuspendLayout();
    this._editor.Parent = (Control) this;
    this._editor.Dock = DockStyle.Top;
    this.ResumeLayout(false);
    this._editor.BringToFront();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  public object Value
  {
    get
    {
      if (this._attrType.MasterAttributeID != 0)
        return (this._editor as ButtonEdit).EditValue;
      switch (this._dataType)
      {
        case DataType.Integer:
        case DataType.Float:
        case DataType.String:
          if (!(this._editor is System.Windows.Forms.ComboBox))
            return (object) this._editor.Text;
          System.Windows.Forms.ComboBox editor = (System.Windows.Forms.ComboBox) this._editor;
          if (editor.DropDownStyle != ComboBoxStyle.DropDownList)
            return (object) editor.Text;
          return editor.SelectedIndex < 0 ? (object) null : (object) this._possibleValues[editor.SelectedIndex].ToString();
        case DataType.Measured:
          (this._editor as AttrMeasuredEdit).UpdateDefMeasure();
          return (this._editor as AttrMeasuredEdit).Values.Values[0];
        case DataType.Date:
          return (this._editor as DateEdit).EditValue;
        case DataType.Boolean:
          return (object) (this._editor as CheckBox).Checked;
        case DataType.ObjectLink:
          return (object) ((this._editor as ButtonEdit).EditValue as ObjectIDToCaption).ObjectID;
        default:
          return (object) null;
      }
    }
    set
    {
      ExpertValue initValue = ExpertValue.Empty(this._dataType);
      if (this._attrType.MasterAttributeID == 0)
      {
        switch (this._dataType)
        {
          case DataType.Integer:
          case DataType.Float:
          case DataType.String:
            System.Windows.Forms.ComboBox comboBox = (System.Windows.Forms.ComboBox) null;
            if (this._editor is System.Windows.Forms.ComboBox)
            {
              int num1 = -1;
              switch (this._dataType)
              {
                case DataType.Integer:
                  long int64 = Convert.ToInt64(value);
                  for (int index = 0; index < this._possibleValues.Count; ++index)
                  {
                    if (Convert.ToInt64(this._possibleValues[index]) == int64)
                    {
                      num1 = index;
                      break;
                    }
                  }
                  break;
                case DataType.Float:
                  double num2 = Convert.ToDouble(value);
                  for (int index = 0; index < this._possibleValues.Count; ++index)
                  {
                    if (Math.Abs(Convert.ToDouble(this._possibleValues[index]) - num2) < 1E-05)
                    {
                      num1 = index;
                      break;
                    }
                  }
                  break;
                case DataType.String:
                  num1 = this._possibleValues.IndexOf(value);
                  break;
              }
              comboBox = (System.Windows.Forms.ComboBox) this._editor;
              comboBox.SelectedIndex = num1;
            }
            if (comboBox != null && comboBox.DropDownStyle == ComboBoxStyle.DropDownList)
              break;
            if (value != null)
            {
              this._editor.Text = value.ToString();
              break;
            }
            this._editor.Text = initValue.ToString();
            break;
          case DataType.Measured:
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              int attributeId = sessionKeeper.Session.IdentHelper.GetAttributeID(this._attrType.Guid.ToString());
              if (value != null)
                (this._editor as AttrMeasuredEdit).Values = new AttributeValues(attributeId, value)
                {
                  AttributeGuid = MetaDataHelper.GetAttributeTypeGuid(attributeId)
                };
              else
                (this._editor as AttrMeasuredEdit).Values = new AttributeValues(attributeId, (object) initValue)
                {
                  AttributeGuid = MetaDataHelper.GetAttributeTypeGuid(attributeId)
                };
              (this._editor as AttrMeasuredEdit).Modified = false;
              break;
            }
          case DataType.Date:
            if (value != null)
            {
              (this._editor as DateEdit).EditValue = (object) Convert.ToDateTime(value);
              break;
            }
            (this._editor as DateEdit).EditValue = (object) Convert.ToDateTime(initValue.Value);
            break;
          case DataType.Boolean:
            if (value != null)
            {
              (this._editor as CheckBox).Checked = Convert.ToBoolean(value);
              break;
            }
            (this._editor as CheckBox).Checked = Convert.ToBoolean(initValue.Value);
            break;
          case DataType.ObjectLink:
            if (value != null)
            {
              (this._editor as ButtonEdit).EditValue = (object) new ObjectIDToCaption(Convert.ToInt64(value));
              break;
            }
            (this._editor as ButtonEdit).EditValue = (object) new ObjectIDToCaption(-1L);
            break;
        }
      }
      else if (value != null)
        (this._editor as ButtonEdit).EditValue = value;
      else
        (this._editor as ButtonEdit).EditValue = (object) initValue;
    }
  }

  public Label Label => this.label1;

  public string Caption
  {
    set
    {
      if (this._editor == null)
        return;
      this._editor.Text = value;
    }
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SingleValueEditor));
    this.label1 = new Label();
    this.textBox1 = new TextBox();
    this.SuspendLayout();
    this.label1.AccessibleDescription = (string) null;
    this.label1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Font = (Font) null;
    this.label1.Name = "label1";
    this.textBox1.AccessibleDescription = (string) null;
    this.textBox1.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.textBox1, "textBox1");
    this.textBox1.BackgroundImage = (Image) null;
    this.textBox1.Font = (Font) null;
    this.textBox1.Name = "textBox1";
    this.AccessibleDescription = (string) null;
    this.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BackgroundImage = (Image) null;
    this.Controls.Add((Control) this.textBox1);
    this.Controls.Add((Control) this.label1);
    this.Font = (Font) null;
    this.Name = nameof (SingleValueEditor);
    this.Tag = (object) "";
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  internal ButtonEdit ButtonEdit => this._editor as ButtonEdit;

  internal ButtonPressedEventHandler ButtonEditClick
  {
    get => new ButtonPressedEventHandler(this.editor_ButtonClick);
  }

  private void editor_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (this._attrType.MasterAttributeID != 0)
      return;
    int attributeId = MetaDataHelper.GetAttributeID((object) this._attrType.Guid);
    IMSAttributeType attributeType1 = MetaDataHelper.GetAttributeType(attributeId);
    if (SingleValueEditor.classifTypes.Contains((int) attributeType1.SizeType))
    {
      long int64 = Convert.ToInt64(((ObjectPropertyClass) (sender as ButtonEdit).EditValue).ObjectID);
      if (!new ClassifierSel().Execute(ref int64))
        return;
      (sender as ButtonEdit).EditValue = (object) new ObjectIDToCaption(int64);
    }
    else
    {
      IAttributePropertyDescriber describer = ServiceUtils.GetService<IAttributePropertyDescriberService>((object) ServicesManager.ServiceContainer, false)?.GetDescriber(attributeId);
      if (describer != null)
      {
        ITypeDescriptorContext context = (ITypeDescriptorContext) null;
        switch (describer.GetPropDescriptorEditor(attributeId) is UITypeEditor descriptorEditor ? descriptorEditor.GetEditStyle(context) : UITypeEditorEditStyle.None)
        {
          case UITypeEditorEditStyle.Modal:
          case UITypeEditorEditStyle.DropDown:
            IElementInfo elementInfo = (IElementInfo) new TypedElementInfo(0L, AttributableElements.Object, MetaDataHelper.GetObjectTypeID(this._commonHolder.ObjectType.Guid));
            ObjectIDToCaption editValue = sender is ButtonEdit buttonEdit ? buttonEdit.EditValue as ObjectIDToCaption : (ObjectIDToCaption) null;
            object propDescriptorValue = describer.GetPropDescriptorValue(elementInfo, attributeId, (object) (editValue != null ? editValue.ObjectID : 0L));
            using (ServiceContainer provider = new ServiceContainer())
            {
              object propertyValue = descriptorEditor.EditValue(context, (System.IServiceProvider) provider, propDescriptorValue);
              if (propDescriptorValue == propertyValue)
                return;
              object attributeValue = describer.GetAttributeValue((IElementInfo) null, attributeId, propertyValue);
              buttonEdit.EditValue = (object) new ObjectIDToCaption(attributeValue != null ? Convert.ToInt64(attributeValue) : -1L);
              return;
            }
        }
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttributeType attributeType2 = sessionKeeper.Session.GetAttributeType(this._attrType.Guid);
        IDescriptor rootDescriptor = attributeType2.SizeType >= 0L ? (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor((int) attributeType2.SizeType) : (IDescriptor) new ObjectTypesNodeDescriptor();
        long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Expert.Editor_409"), string.Empty, rootDescriptor, SelectionOptions.Default);
        if (numArray == null || numArray.Length == 0)
          return;
        (sender as ButtonEdit).EditValue = (object) new ObjectIDToCaption(numArray[0]);
      }
    }
  }

  private void editor_KeyDown(object sender, KeyEventArgs e)
  {
    if (!e.KeyCode.Equals((object) Keys.Delete))
      return;
    (sender as ButtonEdit).EditValue = (object) new ObjectIDToCaption(-1L);
  }

  private void editor_DoubleClick(object sender, EventArgs e)
  {
    this.editor_ButtonClick(sender, (ButtonPressedEventArgs) null);
  }

  internal AttrMeasuredEdit AttrMeasuredEdit => this._editor as AttrMeasuredEdit;
}
