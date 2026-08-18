// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.DoubleValueEditor
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Localization;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>
/// Контрол редактор для редактирования двойного значения
/// в частности диапазона
/// </summary>
internal class DoubleValueEditor : UserControl
{
  private System.ComponentModel.Container components;
  private SingleValueEditor _low;
  private SingleValueEditor _high;
  private DataType _dataType = DataType.String;

  public DoubleValueEditor(CommonTypeHolder commonType, DataType dataType, IList possibleValues)
  {
    this.InitializeComponent();
    this._dataType = dataType;
    this._low = new SingleValueEditor(commonType, dataType, possibleValues, (IList) new object[0]);
    this._high = new SingleValueEditor(commonType, dataType, possibleValues, (IList) new object[0]);
    this.SuspendLayout();
    this._low.Parent = (Control) this;
    this._low.Label.Text = LocalizationHolder.rm.GetString("Expert.Editor_120");
    this._low.Dock = DockStyle.Top;
    this._high.Parent = (Control) this;
    this._high.Label.Text = LocalizationHolder.rm.GetString("Expert.Editor_121");
    this._high.Dock = DockStyle.Top;
    this.ResumeLayout(false);
    this._high.BringToFront();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  public DiapValue Value
  {
    get
    {
      return new DiapValue(new ExpertValue(this._dataType, this._low.Value), new ExpertValue(this._dataType, this._high.Value));
    }
    set
    {
      if (value != null)
      {
        this._low.Value = value.Low.Value;
        this._high.Value = value.High.Value;
      }
      else
      {
        this._low.Value = (object) null;
        this._high.Value = (object) null;
      }
    }
  }

  public string Caption
  {
    set
    {
      this._low.Caption = value;
      this._high.Caption = value;
    }
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.Name = nameof (DoubleValueEditor);
    this.Size = new Size(216, 104);
  }
}
