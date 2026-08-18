// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SpecifNumberingForRazdelControl
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.Utils;
using System.ComponentModel;
using System.Drawing;

#nullable disable
namespace Intermech.AVS;

public class SpecifNumberingForRazdelControl : SpecifNumberingControl
{
  private IContainer components;

  public SpecifNumberingForRazdelControl() => this.InitializeComponent();

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
    this.groupBox1.SuspendLayout();
    this.StartNumberUpDown.Properties.BeginInit();
    this.BetweenDifferentDesignationsUpDown.Properties.BeginInit();
    this.BetweenIspolnsUpDown.Properties.BeginInit();
    this.BeforeNewPartUpDown.Properties.BeginInit();
    this.BeforeNewRazdelUpDown.Properties.BeginInit();
    this.BetweenSameDesignationsUpDown.Properties.BeginInit();
    this.BeforeVariableDataUpDown.Properties.BeginInit();
    this.BeforeNewObjTypeUpDown.Properties.BeginInit();
    this.BeforeNewIspolnUpDown.Properties.BeginInit();
    this.SuspendLayout();
    this.groupBox1.Size = new Size(503, 109);
    this.label5.Visible = false;
    this.label6.Text = "Перед следующим разделом";
    this.label7.Visible = false;
    this.label8.Visible = false;
    this.StartNumberUpDown.EditValue = (object) "";
    this.StartNumberUpDown.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.StartNumberUpDown.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.StartNumberUpDown.Properties.Style = new ViewStyle("ControlStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.WindowText);
    this.BetweenDifferentDesignationsUpDown.EditValue = (object) "";
    this.BetweenDifferentDesignationsUpDown.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.BetweenDifferentDesignationsUpDown.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.BetweenDifferentDesignationsUpDown.Properties.Style = new ViewStyle("ControlStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.WindowText);
    this.BetweenIspolnsUpDown.EditValue = (object) "";
    this.BetweenIspolnsUpDown.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.BetweenIspolnsUpDown.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.BetweenIspolnsUpDown.Properties.Style = new ViewStyle("ControlStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.WindowText);
    this.BeforeNewPartUpDown.EditValue = (object) "";
    this.BeforeNewPartUpDown.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.BeforeNewPartUpDown.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.BeforeNewPartUpDown.Properties.Style = new ViewStyle("ControlStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.WindowText);
    this.BeforeNewPartUpDown.Visible = false;
    this.BeforeNewRazdelUpDown.EditValue = (object) "";
    this.BeforeNewRazdelUpDown.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.BeforeNewRazdelUpDown.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.BeforeNewRazdelUpDown.Properties.Style = new ViewStyle("ControlStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.WindowText);
    this.BeforeNewRazdelUpDown.ToolTip = "Шаг нумерации перед следующим разделом спецификации";
    this.BetweenSameDesignationsUpDown.EditValue = (object) "";
    this.BetweenSameDesignationsUpDown.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.BetweenSameDesignationsUpDown.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.BetweenSameDesignationsUpDown.Properties.Style = new ViewStyle("ControlStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.WindowText);
    this.BeforeVariableDataUpDown.EditValue = (object) "";
    this.BeforeVariableDataUpDown.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.BeforeVariableDataUpDown.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.BeforeVariableDataUpDown.Properties.Style = new ViewStyle("ControlStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.WindowText);
    this.BeforeVariableDataUpDown.Visible = false;
    this.BeforeNewObjTypeUpDown.EditValue = (object) "";
    this.BeforeNewObjTypeUpDown.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.BeforeNewObjTypeUpDown.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.BeforeNewObjTypeUpDown.Properties.Style = new ViewStyle("ControlStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.WindowText);
    this.BeforeNewIspolnUpDown.EditValue = (object) "";
    this.BeforeNewIspolnUpDown.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.BeforeNewIspolnUpDown.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.BeforeNewIspolnUpDown.Properties.Style = new ViewStyle("ControlStyle", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Control, SystemColors.WindowText);
    this.BeforeNewIspolnUpDown.Visible = false;
    this.Name = nameof (SpecifNumberingForRazdelControl);
    this.Size = new Size(513, 149);
    this.groupBox1.ResumeLayout(false);
    this.StartNumberUpDown.Properties.EndInit();
    this.BetweenDifferentDesignationsUpDown.Properties.EndInit();
    this.BetweenIspolnsUpDown.Properties.EndInit();
    this.BeforeNewPartUpDown.Properties.EndInit();
    this.BeforeNewRazdelUpDown.Properties.EndInit();
    this.BetweenSameDesignationsUpDown.Properties.EndInit();
    this.BeforeVariableDataUpDown.Properties.EndInit();
    this.BeforeNewObjTypeUpDown.Properties.EndInit();
    this.BeforeNewIspolnUpDown.Properties.EndInit();
    this.ResumeLayout(false);
  }
}
