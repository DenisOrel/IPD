// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.ChooseSymbol
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraEditors;
using Intermech.Client.Core;
using Intermech.Expert.Table;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.SelectionView;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Форма выбора символа и ввода значения</summary>
public class ChooseSymbol : Form
{
  private Panel panel1;
  private Button bOk;
  private Button bCancel;
  private Panel panel2;
  private Panel panel3;
  private System.Windows.Forms.ComboBox comboBox1;
  private Label label1;
  private ExpertValueEditor box;
  private CommonTypeHolder _commonType;
  private ExpertValue __value;
  private DataType _valueType = DataType.String;
  private eCellSymbol _previousSymbol;
  private eCellSymbol _initSymbol;
  private Button bClear;
  private SimpleButton buttonRef;
  private IContainer components;

  private ExpertValue Value
  {
    get => this.__value;
    set => this.__value = value;
  }

  /// <summary>Конструктор (внутренний)</summary>
  protected ChooseSymbol()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1326);
    this.bClear.Visible = false;
    this.Value = (ExpertValue) null;
  }

  /// <summary>Конструктор</summary>
  /// <param name="cellSymbol">символ ячейки</param>
  /// <param name="excludeSymbols">Исключаемые символы из списка</param>
  public ChooseSymbol(eCellSymbol cellSymbol, eCellSymbol[] excludeSymbols)
    : this()
  {
    this.Text = LocalizationHolder.rm.GetString("Expert.Editor_1");
    ArrayList arrayList = new ArrayList((ICollection) excludeSymbols);
    this._initSymbol = cellSymbol;
    this.Value = (ExpertValue) null;
    this.comboBox1.BeginUpdate();
    try
    {
      this.comboBox1.Items.Clear();
      foreach (FieldInfo field in typeof (eCellSymbol).GetFields())
      {
        eCellSymbol eCellSymbol = (eCellSymbol) field.GetValue((object) eCellSymbol.None);
        string caption = EnumTypeHelper.GetCaption((Enum) eCellSymbol);
        if (!arrayList.Contains((object) eCellSymbol) && !this.comboBox1.Items.Contains((object) caption))
          this.comboBox1.Items.Add((object) caption);
      }
    }
    finally
    {
      this.comboBox1.EndUpdate();
    }
    this.panel3.Enabled = false;
  }

  /// <summary>Конструктор</summary>
  /// <param name="cellSymbol">символ ячейки</param>
  /// <param name="commonType">идентификация типа объекта и типа атрибута</param>
  /// <param name="value">значение ячейки</param>
  public ChooseSymbol(eCellSymbol cellSymbol, CommonTypeHolder commonType, ExpertValue value)
    : this(cellSymbol, new eCellSymbol[0])
  {
    this.panel3.Enabled = true;
    this.bClear.Visible = true;
    this.InitValue(commonType, value);
  }

  /// <summary>Конструктор</summary>
  /// <param name="commonType">идентификация типа объекта и типа атрибута</param>
  /// <param name="value">значение ячейки</param>
  public ChooseSymbol(CommonTypeHolder commonType, ExpertValue value)
    : this()
  {
    this.panel2.Visible = false;
    this.InitValue(commonType, value);
  }

  private void InitValue(CommonTypeHolder commonType, ExpertValue value)
  {
    this.Text = LocalizationHolder.rm.GetString("Expert.Editor_2");
    if (commonType != null)
    {
      this._commonType = commonType;
      FieldTypes attrType = commonType.AttributeType.FieldTypes;
      if (attrType == FieldTypes.ftSystem)
        attrType = ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) this._commonType.AttributeType.SourceAttributeID);
      this._valueType = DataTypeConvertor.AttrType2DataType(attrType);
      this.Text += string.Format(LocalizationHolder.rm.GetString("Expert.Editor_3"), (object) DataTypeConvertor.DataTypeName(this._valueType));
      this.Value = value == null ? ExpertValue.Empty(this._valueType) : value;
    }
    else
      this.Value = ExpertValue.Empty();
    this.UpdateBox();
    this._previousSymbol = this._initSymbol;
  }

  /// <summary>Dispose</summary>
  /// <param name="disposing"></param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void UpdateBox()
  {
    this.panel3.SuspendLayout();
    try
    {
      if (this.box != null)
      {
        this.panel3.Controls.Clear();
        this.box = (ExpertValueEditor) null;
      }
      switch (this.Value.ValueType)
      {
        case DataType.Packet:
        case DataType.Diap:
          this.box = new ExpertValueEditor(this._valueType, this._commonType);
          break;
        default:
          this.box = new ExpertValueEditor(this._commonType);
          break;
      }
      this.box.EditValue = this.Value;
      this.box.Parent = (Control) this.panel3;
      this.box.DoubleClick += new EventHandler(this.box_DoubleClick);
    }
    finally
    {
      this.panel3.ResumeLayout(false);
    }
    if (this.box == null)
      return;
    this.box.Dock = DockStyle.Fill;
    this.box.BringToFront();
  }

  private void InsDefValues(List<long> inds)
  {
    PacketValue packetValue = new PacketValue();
    foreach (long ind in inds)
      packetValue.Add(new ExpertValue(ind, true));
    this.box.EditValue = new ExpertValue(DataType.Packet, (object) packetValue);
  }

  private void box_DoubleClick(object sender, EventArgs e)
  {
    int num1 = -1;
    int num2 = -1;
    Guid guid1 = this._commonType.AttributeType.Guid;
    Guid guid2 = this._commonType.ObjectType.Guid;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(guid1, false);
      if (attributeType != null)
        num1 = attributeType.AttributeID;
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(guid2, false);
      if (objectType != null)
        num2 = objectType.ObjectType;
    }
    bool flag1 = false;
    MeasuredValue aMeasureValue = (MeasuredValue) null;
    bool AddInfo = false;
    this.box.UpdateCurrent();
    if (this.box.EditValue.Value == null)
      this.box.EditValue = new ExpertValue(DataType.Integer, (object) 0);
    List<long> refIds = new List<long>();
    this.box.UpdateCurrent();
    switch (this.box.EditValue.ValueType)
    {
      case DataType.Integer:
      case DataType.ObjectLink:
      case DataType.Attribute:
      case DataType.ObjType:
      case DataType.RelType:
        if (this.box.EditValue.Value != null)
        {
          refIds.Add(Convert.ToInt64(this.box.EditValue.Value));
          break;
        }
        break;
      case DataType.Float:
        return;
      case DataType.Measured:
        return;
      case DataType.String:
        return;
      case DataType.Date:
        return;
      case DataType.Boolean:
        return;
      case DataType.Packet:
        PacketValue packetValue = (PacketValue) this.box.EditValue.Value;
        for (int index = 0; index < packetValue.Count; ++index)
        {
          ExpertValue expertValue = packetValue[index];
          if (expertValue.ValueType == DataType.ObjectLink || expertValue.ValueType == DataType.Integer || expertValue.ValueType == DataType.ObjType || expertValue.ValueType == DataType.Attribute || expertValue.ValueType == DataType.RelType)
            refIds.Add(Convert.ToInt64(expertValue.Value));
          else if (expertValue.ValueType == DataType.Diap)
          {
            DiapValue diapValue = (DiapValue) expertValue.Value;
            if (diapValue.Low.ValueType == DataType.Integer && diapValue.High.ValueType == DataType.Integer)
            {
              for (long int64 = Convert.ToInt64(diapValue.Low.Value); int64 < Convert.ToInt64(diapValue.High.Value); ++int64)
                refIds.Add(int64);
            }
          }
        }
        break;
      case DataType.Diap:
        DiapValue diapValue1 = (DiapValue) this.box.EditValue.Value;
        if (diapValue1.Low.ValueType == DataType.Integer && diapValue1.High.ValueType == DataType.Integer)
        {
          for (long int64 = Convert.ToInt64(diapValue1.Low.Value); int64 <= Convert.ToInt64(diapValue1.High.Value); ++int64)
            refIds.Add(int64);
          break;
        }
        break;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      try
      {
        IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(num1);
        if (FormEditor.UserAttrs.ua.ContainsKey((object) num1))
        {
          FormEditor.UserAttrItem userAttrItem = (FormEditor.UserAttrItem) FormEditor.UserAttrs.ua[(object) num1];
          VListSelect vlistSelect = new VListSelect();
          flag1 = true;
          if (!vlistSelect.Execute(userAttrItem.possibleValues, refIds, attributeType.Name, userAttrItem.multiSelect))
            return;
          List<long> Indices = new List<long>();
          vlistSelect.GetResults(out Indices);
          this.InsDefValues(Indices);
        }
        else
        {
          DataRow[] possibleValuesRows = attributeType.GetPossibleValuesRows();
          if (possibleValuesRows.Length != 0)
          {
            VListSelect vlistSelect = new VListSelect();
            flag1 = true;
            DataRow[] rows = possibleValuesRows;
            string name = attributeType.Name;
            int num3 = AddInfo ? 1 : 0;
            List<long> objIds = refIds;
            if (!vlistSelect.Execute(rows, name, num3 != 0, objIds))
              return;
            this.InsDefValues(new List<long>());
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
                  bool flag2 = false;
                  if (attType == SelectionParameterTypes.sptMeasured)
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
                      flag2 = true;
                      aMeasureValue = (MeasuredValue) null;
                    }
                  }
                  if (!flag2 && !sysAttrSel(ref aObject, (object) AddInfo))
                    return;
                  if (aObject is MeasuredValue)
                  {
                    this.box.EditValue = new ExpertValue((MeasuredValue) aObject);
                    return;
                  }
                  if (!AddInfo || !(aObject is ArrayList))
                  {
                    this.box.EditValue = new ExpertValue(Convert.ToInt64(aObject), false);
                    return;
                  }
                  List<long> inds = new List<long>();
                  ArrayList arrayList = (ArrayList) aObject;
                  for (int index = 0; index < arrayList.Count; ++index)
                  {
                    long int64 = Convert.ToInt64(arrayList[index]);
                    inds.Add(int64);
                    this.InsDefValues(inds);
                  }
                  return;
                }
                finally
                {
                  flag1 = true;
                }
              }
            }
            bool flag3 = FormulaEditPlugin.IsAttrForSpravochnik(num1);
            if (!flag3 && attType == SelectionParameterTypes.sptObject && attributeType.AttributeType == FieldTypes.ftObjectLink)
            {
              SelectionOptions options = SelectionOptions.SelectObjects;
              if (!AddInfo)
                options |= SelectionOptions.DisableMultiselect;
              flag1 = true;
              long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Expert.Editor_336"), LocalizationHolder.rm.GetString("Expert.Editor_337"), (int) attributeType.SizeType, options);
              if (numArray == null || numArray.Length == 0)
                return;
              List<long> inds = new List<long>();
              foreach (long num4 in numArray)
                inds.Add(num4);
              this.InsDefValues(inds);
            }
            else if (num2 == -1)
            {
              int num5 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_181"), LocalizationHolder.rm.GetString("Expert.Editor_182"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
              flag1 = true;
            }
            else
            {
              bool flag4 = ExpertConsts.UsedIMCode(sessionKeeper.Session, num2, num1);
              if (flag3)
              {
                List<long> imbaseCatalog = FormulaEditPlugin.GetImbaseCatalog(num2, num1);
                if (imbaseCatalog == null || imbaseCatalog.Count == 0)
                {
                  int num6 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_579"), LocalizationHolder.rm.GetString("Expert.Editor_222"), MessageBoxButtons.OK);
                }
                IImbaseFilterSelector service = ServicesManager.GetService(typeof (IImbaseFilterSelector)) as IImbaseFilterSelector;
                if (imbaseCatalog != null && imbaseCatalog.Count != 0 && service != null)
                {
                  refIds = service.CheckImbaseObjects(imbaseCatalog, -1L, refIds);
                  flag4 = refIds.Count > 0 && refIds[0] != 0L;
                  flag1 = true;
                }
              }
              else
              {
                IMSelector imSelector = new IMSelector();
                flag4 = !flag4 ? imSelector.Execute4Attribute(num2, num1, ref refIds) : imSelector.Execute4Objects(num2, ref refIds);
                bool flag5 = false;
                if (!flag4 && attributeType.AttributeType == FieldTypes.ftObjectLink)
                {
                  int sizeType = (int) attributeType.SizeType;
                  long[] collection = sizeType == -1 ? (long[]) null : SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Expert.Editor_553"), LocalizationHolder.rm.GetString("Expert.Editor_554"), sizeType, SelectionOptions.SelectObjects);
                  if (collection != null)
                  {
                    refIds.Clear();
                    refIds.AddRange((IEnumerable<long>) collection);
                    flag4 = true;
                  }
                  else
                    flag5 = sizeType != -1;
                }
                flag1 = flag5;
              }
              if (!flag4 || refIds.Count <= 0)
                return;
              if (refIds.Count == 1)
                this.box.EditValue = new ExpertValue(DataType.Integer, (object) refIds[0]);
              else
                this.InsDefValues(refIds);
            }
          }
        }
      }
      finally
      {
        if (!flag1)
        {
          IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(num1);
          IDescriptor rootDescriptor = attributeType.SizeType >= 0L ? (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor((int) attributeType.SizeType) : (IDescriptor) new ObjectTypesNodeDescriptor();
          long[] Values = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Expert.Editor_409"), string.Empty, rootDescriptor, SelectionOptions.Default);
          if (Values != null && Values.Length != 0)
            this.box.EditValue = new ExpertValue(DataType.Packet, (object) new PacketValue((IEnumerable) Values, DataType.ObjectLink));
        }
      }
    }
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

  /// <summary>Возвращает символ ячейки</summary>
  public eCellSymbol CellSymbol
  {
    get
    {
      return (eCellSymbol) EnumTypeHelper.GetEnumValue(typeof (eCellSymbol), this.comboBox1.Text, (object) eCellSymbol.None);
    }
  }

  /// <summary>Возвращает значение ячейки</summary>
  public ExpertValue ResultValue => this.__value;

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ChooseSymbol));
    this.panel1 = new Panel();
    this.buttonRef = new SimpleButton();
    this.bClear = new Button();
    this.bCancel = new Button();
    this.bOk = new Button();
    this.panel2 = new Panel();
    this.comboBox1 = new System.Windows.Forms.ComboBox();
    this.label1 = new Label();
    this.panel3 = new Panel();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.buttonRef);
    this.panel1.Controls.Add((Control) this.bClear);
    this.panel1.Controls.Add((Control) this.bCancel);
    this.panel1.Controls.Add((Control) this.bOk);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.buttonRef, "buttonRef");
    this.buttonRef.ImageIndex = 20;
    this.buttonRef.Name = "buttonRef";
    this.buttonRef.Style = new ViewStyle("ControlStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, HorzAlignment.Center, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.ControlText);
    this.buttonRef.TabStop = false;
    this.buttonRef.ToolTip = "Справочник";
    this.buttonRef.Click += new EventHandler(this.box_DoubleClick);
    componentResourceManager.ApplyResources((object) this.bClear, "bClear");
    this.bClear.Name = "bClear";
    this.bClear.Click += new EventHandler(this.bClear_Click);
    componentResourceManager.ApplyResources((object) this.bCancel, "bCancel");
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Name = "bCancel";
    componentResourceManager.ApplyResources((object) this.bOk, "bOk");
    this.bOk.Name = "bOk";
    this.bOk.Click += new EventHandler(this.bOk_Click);
    this.panel2.Controls.Add((Control) this.comboBox1);
    this.panel2.Controls.Add((Control) this.label1);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.comboBox1, "comboBox1");
    this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboBox1.Name = "comboBox1";
    this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    this.AcceptButton = (IButtonControl) this.bOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.bCancel;
    this.Controls.Add((Control) this.panel3);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ChooseSymbol);
    this.ShowInTaskbar = false;
    this.Tag = (object) "";
    this.Closed += new EventHandler(this.ChooseSymbol_Closed);
    this.Load += new EventHandler(this.ChooseSymbol_Load);
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void ChooseSymbol_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
    this.comboBox1.SelectedItem = (object) EnumTypeHelper.GetCaption((Enum) this._initSymbol);
  }

  private void ChooseSymbol_Closed(object sender, EventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
  {
    eCellSymbol enumValue = (eCellSymbol) EnumTypeHelper.GetEnumValue(typeof (eCellSymbol), this.comboBox1.Text, (object) eCellSymbol.None);
    if (enumValue.Equals((object) eCellSymbol.Set))
    {
      if (!this._previousSymbol.Equals((object) eCellSymbol.Set))
      {
        this.Value = new ExpertValue(new PacketValue());
        this.UpdateBox();
        this.box.Enabled = true;
      }
    }
    else if (enumValue.Equals((object) eCellSymbol.Other))
    {
      this.Value = ExpertValue.Empty(this._valueType);
      this.UpdateBox();
      this.box.Enabled = false;
    }
    else if (this._previousSymbol.Equals((object) eCellSymbol.Set) || this._previousSymbol.Equals((object) eCellSymbol.Other))
    {
      this.Value = ExpertValue.Empty(this._valueType);
      this.UpdateBox();
      this.box.Enabled = true;
    }
    this._previousSymbol = enumValue;
  }

  private void bOk_Click(object sender, EventArgs e)
  {
    try
    {
      if (this.box != null)
      {
        this.box.UpdateCurrent();
        this.Value = this.box.EditValue;
      }
    }
    catch
    {
      if (this.CellSymbol == eCellSymbol.Other)
      {
        this.DialogResult = DialogResult.OK;
        return;
      }
      this.DialogResult = DialogResult.None;
      return;
    }
    if (this.Value.Value == null)
    {
      this.DialogResult = DialogResult.None;
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_62"), LocalizationHolder.rm.GetString("Expert.Editor_63"), MessageBoxButtons.OK);
    }
    else
      this.DialogResult = DialogResult.OK;
  }

  private void bClear_Click(object sender, EventArgs e)
  {
    this.Value = (ExpertValue) null;
    this.comboBox1.Text = EnumTypeHelper.GetCaption((Enum) eCellSymbol.None);
    this.DialogResult = DialogResult.Yes;
  }
}
