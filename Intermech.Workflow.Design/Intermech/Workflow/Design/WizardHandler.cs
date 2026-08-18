// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.WizardHandler
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces.Workflow;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for WizardHandler.</summary>
public class WizardHandler
{
  private TabControl tc;
  private Button prevButton;
  private Button nextButton;
  private Button cancelButton;
  private string cStrReady = LocalizationHolder.rm.GetString("Workflow.Design_122");
  private string cStrNext = LocalizationHolder.rm.GetString("Workflow.Design_123");

  public event WizardHandler.SelectionChangedHandler BeforeSelectionChanged;

  public event WizardHandler.SelectionChangedHandler AfterSelectionChanged;

  public WizardHandler(TabControl wizard, Button prev, Button next, Button cancel)
  {
    this.tc = wizard;
    this.prevButton = prev;
    this.nextButton = next;
    this.cancelButton = cancel;
    this.tc.ItemSize = new Size(1, 1);
    typeof (Control).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic).Invoke((object) this.tc, new object[2]
    {
      (object) (ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint),
      (object) true
    });
    this.tc.Paint += new PaintEventHandler(this.Wizard_Paint);
    this.tc.KeyDown += new KeyEventHandler(this.Wizard_KeyDown);
    this.nextButton.Click += new EventHandler(this.NextBtn_Click);
    this.prevButton.Click += new EventHandler(this.PrevBtn_Click);
    Form form = wizard.FindForm();
    if (form != null)
    {
      form.AcceptButton = (IButtonControl) this.nextButton;
      form.CancelButton = (IButtonControl) cancel;
    }
    this.UpdateButtons();
  }

  private void Wizard_Paint(object sender, PaintEventArgs e)
  {
    e.Graphics.FillRectangle(SystemBrushes.Control, e.ClipRectangle);
  }

  private void NextBtn_Click(object sender, EventArgs e) => this.NextPage();

  private void PrevBtn_Click(object sender, EventArgs e) => this.PrevPage();

  public void NextPage() => this.IncPage(1);

  public void PrevPage() => this.IncPage(-1);

  private void IncPage(int dx)
  {
    this.tc.Parent.Cursor = Cursors.WaitCursor;
    try
    {
      int NewIndex = this.tc.SelectedIndex + dx;
      WizardHandler.SelectionChangedHandler selectionChanged1 = this.BeforeSelectionChanged;
      if (selectionChanged1 != null && !selectionChanged1(ref NewIndex))
        return;
      if (NewIndex >= 0 && NewIndex < this.tc.TabCount)
        this.tc.SelectedIndex = NewIndex;
      this.UpdateButtons();
      WizardHandler.SelectionChangedHandler selectionChanged2 = this.AfterSelectionChanged;
      if (selectionChanged2 == null)
        return;
      int num = selectionChanged2(ref NewIndex) ? 1 : 0;
    }
    finally
    {
      this.tc.Parent.Cursor = Cursors.Default;
    }
  }

  public void UpdateButtons()
  {
    int selectedIndex = this.tc.SelectedIndex;
    this.prevButton.Enabled = selectedIndex != 0;
    this.nextButton.Enabled = selectedIndex < this.tc.TabCount;
    if (selectedIndex == this.tc.TabCount - 1)
    {
      this.nextButton.DialogResult = DialogResult.OK;
      this.nextButton.Text = this.cStrReady;
    }
    else
    {
      this.nextButton.DialogResult = DialogResult.None;
      this.nextButton.Text = this.cStrNext;
    }
  }

  private void Wizard_KeyDown(object sender, KeyEventArgs e)
  {
    if (!this.tc.Focused || e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
      return;
    e.Handled = true;
  }

  public delegate bool SelectionChangedHandler(ref int NewIndex);
}
