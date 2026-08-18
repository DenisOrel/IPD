
// Type: Intermech.Client.Core.AccessDeniedExceptionForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Summary description for ExceptionForm.</summary>
public class AccessDeniedExceptionForm : Form
{
  public string[] MessageText;
  public string[] MessageFullText;
  private Button button1;
  private Label label2;
  private RichTextBox exceptionStack;
  private Button button3;
  private bool _collapsed;
  private bool _fullReport;
  private int _delta;
  private int _fullHeight;
  private Button buttonFullReport;
  private PictureBox pictureBox1;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  public AccessDeniedExceptionForm()
  {
    this.InitializeComponent();
    if (ServicesManager.GetService(typeof (IBigImageList)) is IBigImageList service)
      this.pictureBox1.Image = service.ImageList.Images[service.ImageIndex("imgLocked")];
    this._collapsed = true;
    this._delta = this.Height - this.ClientRectangle.Height;
    this._fullHeight = this.Height;
    this.Height = this.exceptionStack.Top + this._delta;
    this.button3.Select();
    this.SetMessageText();
  }

  private void SetMessageText()
  {
    if (!this._collapsed)
    {
      this.buttonFullReport.Visible = true;
      this.exceptionStack.Lines = this._fullReport ? this.MessageFullText : this.MessageText;
      this.buttonFullReport.Text = this._fullReport ? LocalizationHolder.rm.GetString("Client.Core_1023") : LocalizationHolder.rm.GetString("Client.Core_1024");
      if (!this._fullReport)
        return;
      this.exceptionStack.SelectionStart = this.exceptionStack.Text.Length;
      this.exceptionStack.SelectionLength = 0;
      this.exceptionStack.ScrollToCaret();
    }
    else
      this.buttonFullReport.Visible = false;
  }

  private static AccessDeniedException SearchInnerException(Exception ex)
  {
    if (ex == null || ex.InnerException == null)
      return (AccessDeniedException) null;
    return ex.InnerException is AccessDeniedException innerException ? innerException : AccessDeniedExceptionForm.SearchInnerException(ex.InnerException);
  }

  public static void OnExceptionHandler(object sender, ExceptionEventArgs e)
  {
    if (!(e.Exception is AccessDeniedException accessDeniedException))
      accessDeniedException = AccessDeniedExceptionForm.SearchInnerException(e.Exception);
    if (accessDeniedException == null)
      return;
    AccessDeniedExceptionForm deniedExceptionForm = new AccessDeniedExceptionForm();
    List<string> stringList = new List<string>();
    for (int index = 0; index < accessDeniedException.LogList.Length && accessDeniedException.LogList[accessDeniedException.LogList.Length - index - 1] != "------------------------------------"; ++index)
      stringList.Add(accessDeniedException.LogList[accessDeniedException.LogList.Length - index - 1]);
    deniedExceptionForm.MessageText = new string[stringList.Count];
    for (int index = 0; index < stringList.Count; ++index)
      deniedExceptionForm.MessageText[index] = stringList[stringList.Count - index - 1];
    deniedExceptionForm.MessageFullText = accessDeniedException.LogList;
    Form openForm = Application.OpenForms[Application.OpenForms.Count - 1];
    if (openForm.TopMost)
      openForm.TopMost = false;
    if (openForm.Modal)
    {
      int num1 = (int) deniedExceptionForm.ShowDialog((IWin32Window) openForm);
    }
    else
    {
      int num2 = (int) deniedExceptionForm.ShowDialog();
    }
    e.Handled = true;
  }

  private void buttonFullReport_Click(object sender, EventArgs e)
  {
    this._fullReport = !this._fullReport;
    this.SetMessageText();
  }

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AccessDeniedExceptionForm));
    this.button1 = new Button();
    this.label2 = new Label();
    this.exceptionStack = new RichTextBox();
    this.button3 = new Button();
    this.buttonFullReport = new Button();
    this.pictureBox1 = new PictureBox();
    ((ISupportInitialize) this.pictureBox1).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.OK;
    this.button1.Name = "button1";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.exceptionStack, "exceptionStack");
    this.exceptionStack.Name = "exceptionStack";
    this.exceptionStack.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.button3, "button3");
    this.button3.Name = "button3";
    this.button3.Click += new EventHandler(this.button3_Click);
    componentResourceManager.ApplyResources((object) this.buttonFullReport, "buttonFullReport");
    this.buttonFullReport.Name = "buttonFullReport";
    this.buttonFullReport.Click += new EventHandler(this.buttonFullReport_Click);
    componentResourceManager.ApplyResources((object) this.pictureBox1, "pictureBox1");
    this.pictureBox1.Name = "pictureBox1";
    this.pictureBox1.TabStop = false;
    this.AcceptButton = (IButtonControl) this.button1;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.button1;
    this.Controls.Add((Control) this.pictureBox1);
    this.Controls.Add((Control) this.buttonFullReport);
    this.Controls.Add((Control) this.button3);
    this.Controls.Add((Control) this.exceptionStack);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.button1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AccessDeniedExceptionForm);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    ((ISupportInitialize) this.pictureBox1).EndInit();
    this.ResumeLayout(false);
  }

  private void button3_Click(object sender, EventArgs e)
  {
    if (this._collapsed)
      this.Height = this._fullHeight;
    else
      this.Height = this.exceptionStack.Top + this._delta;
    this._collapsed = !this._collapsed;
    this.SetMessageText();
  }
}
