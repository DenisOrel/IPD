
// Type: Intermech.Client.Core.ClassifySelectionForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

public class ClassifySelectionForm : Form
{
  private Button _okButton;
  private Button _cancelButton;
  private TableLayoutPanel _panel;
  private ClassifyingControl _cc;

  public ClassifySelectionForm(long[] IDs)
    : this(IDs, string.Empty)
  {
  }

  public ClassifySelectionForm(long[] IDs, string text)
  {
    this.InitializeComponents();
    this.Text = text != string.Empty ? text : LocalizationHolder.rm.GetString("Client.Core_165");
    this._cc.RootClassifiers = IDs;
  }

  private void InitializeComponents()
  {
    this.SuspendLayout();
    this.Name = "ClassifyActionForm";
    this.StartPosition = FormStartPosition.CenterParent;
    this.Size = new Size(250, 250);
    this.MinimumSize = new Size(250, 250);
    this.MaximizeBox = this.MinimizeBox = false;
    this.Load += new EventHandler(this.CForm_Load);
    this.FormClosed += new FormClosedEventHandler(this.CForm_FormClosed);
    this._okButton = new Button();
    this._okButton.Text = "OK";
    this._okButton.DialogResult = DialogResult.OK;
    this._okButton.Size = new Size(121, 27);
    this.AcceptButton = (IButtonControl) this._okButton;
    this._okButton.Enabled = false;
    this._cancelButton = new Button();
    this._cancelButton.Text = LocalizationHolder.rm.GetString("Client.Core_166");
    this._cancelButton.DialogResult = DialogResult.Cancel;
    this._cancelButton.Size = new Size(121, 27);
    this.CancelButton = (IButtonControl) this._cancelButton;
    this._panel = new TableLayoutPanel();
    this._panel.SuspendLayout();
    this._panel.Height = 32 /*0x20*/;
    this._panel.Dock = DockStyle.Bottom;
    this._panel.ColumnCount = 3;
    this._panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this._panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
    this._panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
    this._panel.Controls.Add((Control) this._okButton, 1, 0);
    this._panel.Controls.Add((Control) this._cancelButton, 2, 0);
    this._panel.RowCount = 1;
    this._panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
    this._panel.Parent = (Control) this;
    this._panel.ResumeLayout(false);
    this._cc = new ClassifyingControl();
    ((ISupportInitialize) this._cc).BeginInit();
    this._cc.Dock = DockStyle.Fill;
    this._cc.Parent = (Control) this;
    this._cc.BringToFront();
    ((ISupportInitialize) this._cc).EndInit();
    this._cc.ClassifierSelected += new ClassifierSelectedEventHandler(this.SelectedItemsChanged);
    this.ResumeLayout(false);
  }

  /// <summary>Выбранные элементы.</summary>
  public ISelectedItems SelectedItems => this._cc.SelectedItems;

  private void CForm_Load(object sender, EventArgs e) => FormStorage.LoadLayout((Control) this);

  private void CForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void SelectedItemsChanged(object sender, ClassifierSelectedEventArgs e)
  {
    this._okButton.Enabled = e.SelectionID != null && e.EnableClassify;
  }
}
