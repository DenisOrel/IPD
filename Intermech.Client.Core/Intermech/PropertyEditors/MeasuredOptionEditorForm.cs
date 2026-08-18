
// Type: Intermech.PropertyEditors.MeasuredOptionEditorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class MeasuredOptionEditorForm : Form
{
  private int attributeID;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnOk;
  private Button btnCancel;
  private Label label1;
  private ComboBox measureCB;
  private CheckBox cbShowUnit;
  private CheckBox cbConvert;

  public MeasuredOptionEditorForm() => this.InitializeComponent();

  /// <summary>Настройка параметров атрибута типа ftMeasure.</summary>
  /// <param name="options">Набор параметров через зпт, напр. ",шт,1,0":
  /// 0. Правило проверки значения атрибута. Пока не используется, пустое значение
  /// 1. Единица измерения по-умолчанию. Содержит краткое наименование единицы измерения, которая должна использоваться в данном атрибуте по-умолчанию.
  /// 2. Отображать единицу измерения в значении атрибута (1 нужно (по умолчанию), 0 не нужно).
  /// 3. Конвертировать значение в единицу измерения по умолчанию (1 нужно , 0 не нужно (по умолчанию)). </param>
  /// <param name="attributeID">id атрибута типа ftMeasure</param>
  /// <returns></returns>
  public DialogResult ShowDialog(ref string options, int lAttributeID)
  {
    this.attributeID = lAttributeID;
    List<MeasureDescriptor> lst = new List<MeasureDescriptor>((IEnumerable<MeasureDescriptor>) MeasureEditor.GetMeasureDescriptorListByAttributeId(this.attributeID).ToArray(typeof (MeasureDescriptor)));
    MeasureDescriptorComparer descriptorComparer = new MeasureDescriptorComparer(true, true);
    lst.Sort((IComparer<MeasureDescriptor>) descriptorComparer);
    this.FillCB(lst);
    this.FillFields(options);
    int num = (int) this.ShowDialog();
    if (num != 1)
      return (DialogResult) num;
    options = this.SaveFields();
    return (DialogResult) num;
  }

  private void FillCB(List<MeasureDescriptor> lst)
  {
    this.measureCB.Items.Clear();
    for (int index = 0; index < lst.Count; ++index)
      this.measureCB.Items.Add((object) new MeasureDescriptorClass(lst[index]));
  }

  private void FillFields(string options)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBMeasureAttributeType attributeType = sessionKeeper.Session.GetAttributeType(this.attributeID) as IDBMeasureAttributeType;
      string empty1 = string.Empty;
      if (attributeType == null)
      {
        string empty2 = string.Empty;
      }
      else
      {
        string ruleFormula = attributeType.RuleFormula;
      }
      string measure = empty1;
      bool flag1 = attributeType == null || attributeType.ShortNameInString;
      bool flag2 = attributeType != null && attributeType.ConvertToDefaultMeasure;
      int num1 = options.IndexOf(',');
      int num2 = options.LastIndexOf(',');
      if (num1 != -1)
      {
        string str1 = options.Substring(num1 + 1);
        if (num2 == num1)
        {
          measure = str1;
        }
        else
        {
          int length1 = str1.IndexOf(',');
          measure = str1.Substring(0, length1);
          string str2 = str1.Substring(length1 + 1);
          int length2 = str2.IndexOf(',');
          string str3 = "0";
          string str4;
          if (length2 != -1)
          {
            str4 = str2.Substring(0, length2);
            str3 = str2.Substring(length2 + 1);
          }
          else
            str4 = str2;
          string str5 = str4.Trim();
          string str6 = str3.Trim();
          if (str5 == "0")
            flag1 = false;
          if (str5 == "1")
            flag1 = true;
          if (str6 == "0")
            flag2 = false;
          if (str6 == "1")
            flag2 = true;
        }
      }
      this.measureCB.SelectedIndex = this.FindMeasureCBIndex(measure);
      this.cbShowUnit.Checked = flag1;
      this.cbConvert.Checked = flag2;
    }
  }

  private int FindMeasureCBIndex(string measure)
  {
    int measureCbIndex = -1;
    for (int index = 0; index < this.measureCB.Items.Count; ++index)
    {
      if (this.measureCB.Items[index].ToString() == measure)
      {
        measureCbIndex = index;
        break;
      }
    }
    return measureCbIndex;
  }

  private string SaveFields()
  {
    string empty = string.Empty;
    string str = string.Empty + ",";
    if (this.measureCB.SelectedItem != null)
      str += this.measureCB.SelectedItem.ToString();
    return str + "," + (this.cbShowUnit.Checked ? "1" : "0") + "," + (this.cbConvert.Checked ? "1" : "0");
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MeasuredOptionEditorForm));
    this.btnOk = new Button();
    this.btnCancel = new Button();
    this.label1 = new Label();
    this.measureCB = new ComboBox();
    this.cbShowUnit = new CheckBox();
    this.cbConvert = new CheckBox();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Name = "btnOk";
    this.btnOk.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.measureCB, "measureCB");
    this.measureCB.DropDownStyle = ComboBoxStyle.DropDownList;
    this.measureCB.FormattingEnabled = true;
    this.measureCB.Name = "measureCB";
    componentResourceManager.ApplyResources((object) this.cbShowUnit, "cbShowUnit");
    this.cbShowUnit.Name = "cbShowUnit";
    this.cbShowUnit.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.cbConvert, "cbConvert");
    this.cbConvert.Name = "cbConvert";
    this.cbConvert.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.cbConvert);
    this.Controls.Add((Control) this.cbShowUnit);
    this.Controls.Add((Control) this.measureCB);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOk);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.Name = nameof (MeasuredOptionEditorForm);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
