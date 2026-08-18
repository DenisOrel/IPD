
// Type: Intermech.Navigator.Conditions.MultiValueControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.SelectionView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions;

internal sealed class MultiValueControl : ValueControl
{
  private IEditControl _control;
  private SelectionParameterTypes _paramType;
  private int _attributeID;
  private Dictionary<object, string> _possibleValues;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel pControl;
  private CheckBox checkBox1;
  private ListBox listBox1;
  private Button bAdd;
  private Button bEdit;
  private Button bDelete;
  private Button bClear;
  private Label label2;
  private Button bImport;

  public MultiValueControl() => this.InitializeComponent();

  public MultiValueControl(IConditionDataProvider dataProvider)
    : base(dataProvider)
  {
    this.InitializeComponent();
  }

  public override void Initialize(
    int attributeID,
    SelectionParameterTypes paramType,
    ShowValueMode valueMode,
    Dictionary<object, string> pValues,
    ConditionStructure conditionStructure,
    int[] objectTypeIDs,
    object tag)
  {
    base.Initialize(attributeID, paramType, valueMode, pValues, conditionStructure, objectTypeIDs, tag);
    this.bImport.Visible = paramType == SelectionParameterTypes.sptString || paramType == SelectionParameterTypes.sptNumber || paramType == SelectionParameterTypes.sptFloat;
    this._control = this.GetControl(paramType, attributeID, objectTypeIDs, valueMode, pValues, (object) null, true, conditionStructure.RelationalOperator);
    this._paramType = paramType;
    this._possibleValues = pValues;
    this._attributeID = attributeID;
    this.pControl.Controls.Add(this._control.Control);
    if (valueMode == ShowValueMode.svmString)
    {
      this.checkBox1.Visible = true;
      this.checkBox1.Text = LocalizationHolder.rm.GetString("Client.Core_1498");
      this.checkBox1.Checked = conditionStructure.CaseSensitive;
      this.checkBox1.CheckedChanged += new EventHandler(this.Register_CheckedChanged);
      this.Register_CheckedChanged((object) this.checkBox1, new EventArgs());
    }
    else
      this.checkBox1.Visible = false;
    if (conditionStructure.Value != null)
    {
      if (conditionStructure.Value is IList)
      {
        this.value1 = (object) new List<object>();
        foreach (object obj in (IEnumerable) conditionStructure.Value)
          ((List<object>) this.value1).Add(obj);
      }
      else if (conditionStructure.Value.GetType() == typeof (List<object>))
      {
        this.value1 = conditionStructure.Value;
      }
      else
      {
        this.value1 = (object) new List<object>();
        ((List<object>) this.value1).Add(conditionStructure.Value);
      }
    }
    if (this.value1 != null && !(this.value1 is List<object>))
      this.value1 = this._control.Value;
    this.value2 = (object) null;
    this.OnValueChanged();
    object selectedValue = (object) null;
    if (this.value1 != null && ((List<object>) this.value1).Count > 0)
      selectedValue = ((List<object>) this.value1)[0];
    this.RefreshListBox(selectedValue);
    this.RefreshButtons();
  }

  protected override void Control_ValueChangedEvent(object sender, ValueChangedEventArgs e)
  {
    this.RefreshButtons();
  }

  private void RefreshListBox(object selectedValue)
  {
    int num1 = 0;
    this.listBox1.Items.Clear();
    if (this.value1 == null)
      return;
    this.listBox1.BeginUpdate();
    if (this.value1 is List<object> && ((List<object>) this.value1).Count == 1)
    {
      object obj = ((List<object>) this.value1)[0];
      switch (obj)
      {
        case int _:
        case long _:
          if (Convert.ToInt64(obj) == -1L)
            return;
          break;
      }
    }
    int num2 = 0;
    foreach (object obj in (List<object>) this.value1)
    {
      if (selectedValue != null && obj == selectedValue)
        num1 = num2;
      string str = this.dataProvider.ConvertToString((object) this._attributeID, this.conditionStructure.RelationalOperator, this._paramType, obj, this._possibleValues, (object) null);
      this.listBox1.Items.Add((object) new MultiValueControl.ListBoxItem(obj, string.IsNullOrEmpty(str) ? "<Пусто>" : str));
      ++num2;
    }
    this.listBox1.SelectedIndex = selectedValue == null ? this.listBox1.Items.Count - 1 : num1;
    this.listBox1.EndUpdate();
  }

  private void RefreshButtons()
  {
    this.bClear.Enabled = this.listBox1.Items.Count > 0;
    this.bDelete.Enabled = this.pControl.Enabled = this.listBox1.SelectedItem != null && this.listBox1.SelectedItem is MultiValueControl.ListBoxItem;
    object objB = this._control.Value;
    this.bEdit.Enabled = objB != null && this.listBox1.SelectedItem != null && !object.Equals(((MultiValueControl.ListBoxItem) this.listBox1.SelectedItem).Value, objB);
  }

  private void Register_CheckedChanged(object sender, EventArgs e)
  {
    this.OnCaseSensitiveChanged(((CheckBox) sender).Checked);
  }

  private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
  {
    this._control.Value = ((MultiValueControl.ListBoxItem) this.listBox1.SelectedItem).Value;
    this.RefreshButtons();
  }

  private void Add_Click(object sender, EventArgs e)
  {
    if (this.value1 == null)
      this.value1 = (object) new List<object>();
    OnOpenDialogEventArgs e1 = new OnOpenDialogEventArgs()
    {
      Multiselect = true
    };
    if (!this._control.OnAddNewValue(e1))
      return;
    object selectedValue = this._control.Value;
    List<object> objectList = (List<object>) this.value1;
    if (e1.SelectedValues != null && e1.SelectedValues is IList selectedValues)
    {
      foreach (object obj in (IEnumerable) selectedValues)
      {
        if (!objectList.Contains(obj))
          objectList.Add(obj);
      }
    }
    else if (!objectList.Contains(selectedValue))
      objectList.Add(selectedValue);
    this.RefreshListBox(selectedValue);
    this.OnValueChanged();
    this.RefreshButtons();
  }

  private void Edit_Click(object sender, EventArgs e)
  {
    object selectedValue = this._control.Value;
    int num = ((List<object>) this.value1).IndexOf(selectedValue);
    if (num < 0)
    {
      ((List<object>) this.value1)[this.listBox1.SelectedIndex] = selectedValue;
      this.RefreshListBox(selectedValue);
      this.OnValueChanged();
      this.RefreshButtons();
    }
    else
      this.listBox1.SelectedIndex = num;
  }

  private void Delete_Click(object sender, EventArgs e)
  {
    ((List<object>) this.value1).RemoveAt(this.listBox1.SelectedIndex);
    object selectedValue = (object) null;
    if (this.value1 != null && ((List<object>) this.value1).Count > 0)
    {
      selectedValue = ((List<object>) this.value1)[0];
    }
    else
    {
      this.value1 = (object) null;
      this._control.Value = (object) null;
    }
    this.RefreshListBox(selectedValue);
    this.OnValueChanged();
    this.RefreshButtons();
  }

  private void Clear_Click(object sender, EventArgs e)
  {
    this.value1 = (object) null;
    this.RefreshListBox((object) null);
    this._control.Value = (object) null;
    this.OnValueChanged();
    this.RefreshButtons();
  }

  private void Import_Click(object sender, EventArgs e)
  {
    using (ImportValuesForm importValuesForm = new ImportValuesForm())
    {
      if (importValuesForm.ShowDialog() != DialogResult.OK)
        return;
      string[] values = importValuesForm.Values;
      if (values == null)
        return;
      if (this.value1 == null)
        this.value1 = (object) new List<object>();
      List<object> objectList = (List<object>) this.value1;
      object selectedValue = (object) null;
      foreach (string s in values)
      {
        switch (this._paramType)
        {
          case SelectionParameterTypes.sptNumber:
            long result1;
            if (long.TryParse(s, out result1))
            {
              selectedValue = (object) result1;
              break;
            }
            continue;
          case SelectionParameterTypes.sptFloat:
            double result2;
            if (double.TryParse(s, NumberStyles.Any, (IFormatProvider) CultureInfo.CurrentCulture, out result2))
            {
              selectedValue = (object) result2;
              break;
            }
            continue;
          default:
            selectedValue = (object) s;
            break;
        }
        if (selectedValue != null && !objectList.Contains(selectedValue))
          objectList.Add(selectedValue);
      }
      if (selectedValue == null)
        return;
      this.RefreshListBox(selectedValue);
      this.OnValueChanged();
      this.RefreshButtons();
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MultiValueControl));
    this.pControl = new Panel();
    this.checkBox1 = new CheckBox();
    this.listBox1 = new ListBox();
    this.bClear = new Button();
    this.bDelete = new Button();
    this.bEdit = new Button();
    this.bAdd = new Button();
    this.label2 = new Label();
    this.bImport = new Button();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.pControl, "pControl");
    this.pControl.Name = "pControl";
    componentResourceManager.ApplyResources((object) this.checkBox1, "checkBox1");
    this.checkBox1.Name = "checkBox1";
    this.checkBox1.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.listBox1, "listBox1");
    this.listBox1.FormattingEnabled = true;
    this.listBox1.Name = "listBox1";
    this.listBox1.SelectedIndexChanged += new EventHandler(this.ListBox1_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.bClear, "bClear");
    this.bClear.AutoEllipsis = true;
    this.bClear.Name = "bClear";
    this.bClear.UseVisualStyleBackColor = true;
    this.bClear.Click += new EventHandler(this.Clear_Click);
    componentResourceManager.ApplyResources((object) this.bDelete, "bDelete");
    this.bDelete.Name = "bDelete";
    this.bDelete.UseVisualStyleBackColor = true;
    this.bDelete.Click += new EventHandler(this.Delete_Click);
    this.bEdit.AccessibleRole = AccessibleRole.None;
    componentResourceManager.ApplyResources((object) this.bEdit, "bEdit");
    this.bEdit.Name = "bEdit";
    this.bEdit.UseVisualStyleBackColor = true;
    this.bEdit.Click += new EventHandler(this.Edit_Click);
    componentResourceManager.ApplyResources((object) this.bAdd, "bAdd");
    this.bAdd.Name = "bAdd";
    this.bAdd.UseVisualStyleBackColor = true;
    this.bAdd.Click += new EventHandler(this.Add_Click);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.bImport, "bImport");
    this.bImport.Name = "bImport";
    this.bImport.UseVisualStyleBackColor = true;
    this.bImport.Click += new EventHandler(this.Import_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.bImport);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.bClear);
    this.Controls.Add((Control) this.bDelete);
    this.Controls.Add((Control) this.bEdit);
    this.Controls.Add((Control) this.bAdd);
    this.Controls.Add((Control) this.listBox1);
    this.Controls.Add((Control) this.checkBox1);
    this.Controls.Add((Control) this.pControl);
    this.Name = nameof (MultiValueControl);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private class ListBoxItem
  {
    public object Value;
    public string Text;

    public ListBoxItem(object val, string text)
    {
      this.Value = val;
      this.Text = text;
    }

    public override bool Equals(object obj) => base.Equals(obj);

    public override int GetHashCode() => base.GetHashCode();

    public override string ToString() => this.Text;
  }
}
