
// Type: Intermech.Search.Diff.ObjectDiffDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search.Diff;

public class ObjectDiffDialog : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _okButton;
  private ObjectDiffControl _objectDiffControl;

  public ObjectDiffDialog() => this.InitializeComponent();

  public void SetObjectVersionIds(long leftObjectVersionID, long rightObjectVersionID)
  {
    this._objectDiffControl.SetObjectVersionIds(leftObjectVersionID, rightObjectVersionID);
  }

  private void OKButton_Click(object sender, EventArgs e) => this.Close();

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
    this._okButton = new Button();
    this._objectDiffControl = new ObjectDiffControl();
    this.SuspendLayout();
    this._okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this._okButton.Location = new Point(852, 501);
    this._okButton.Name = "_okButton";
    this._okButton.Size = new Size(75, 23);
    this._okButton.TabIndex = 1;
    this._okButton.Text = "OK";
    this._okButton.UseVisualStyleBackColor = true;
    this._okButton.Click += new EventHandler(this.OKButton_Click);
    this._objectDiffControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._objectDiffControl.Location = new Point(12, 12);
    this._objectDiffControl.Name = "_objectDiffControl";
    this._objectDiffControl.Size = new Size(915, 483);
    this._objectDiffControl.TabIndex = 2;
    this.AcceptButton = (IButtonControl) this._okButton;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(939, 536);
    this.Controls.Add((Control) this._objectDiffControl);
    this.Controls.Add((Control) this._okButton);
    this.Name = nameof (ObjectDiffDialog);
    this.ShowIcon = false;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Сравнение объектов";
    this.ResumeLayout(false);
  }
}
