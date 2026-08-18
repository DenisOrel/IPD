// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Editor.ExceptionForm
// Assembly: IMDocumentEditor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 105C08B1-9CA8-4A5F-8603-7439747D5610
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\IMDocumentEditor\IMDocumentEditor.exe

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Document.Editor;

public class ExceptionForm : Form
{
  private Button button1;
  private Button button2;
  private Label labelHint;
  private TextBox exceptionText;
  private RichTextBox exceptionStack;
  private Button button3;
  private bool _collapsed;
  private int _delta;
  private int _fullHeight;
  private Button button4;
  private SaveFileDialog sd;
  private Exception exc;
  private System.ComponentModel.Container components;

  public ExceptionForm()
  {
    this.InitializeComponent();
    this._collapsed = true;
    this._delta = this.Height - this.ClientRectangle.Height - 4;
    this._fullHeight = this.Height;
    this.Height = this.exceptionStack.Top + this._delta;
    this.button3.Select();
    Label labelHint = this.labelHint;
    string format = LocalizationHolder.rm.GetString("Document.Editor_41");
    DateTime now = DateTime.Now;
    string longDateString = now.ToLongDateString();
    now = DateTime.Now;
    string longTimeString = now.ToLongTimeString();
    string str = string.Format(format, (object) longDateString, (object) longTimeString);
    labelHint.Text = str;
  }

  public static DialogResult ShowExceptionDialog(Exception e)
  {
    return new ExceptionForm().ShowException(e);
  }

  public DialogResult ShowException(Exception e)
  {
    this.exc = e;
    this.exceptionText.Text = e.Message;
    this.exceptionStack.Text = this.ShowExtendexStackTrace(e);
    return this.ShowDialog();
  }

  private string ShowExtendexStackTrace(Exception e)
  {
    StringBuilder stringBuilder = new StringBuilder(256 /*0x0100*/);
    stringBuilder.AppendLine(e.StackTrace);
    Exception innerException = e.InnerException;
    string str = new string('=', 32 /*0x20*/);
    for (; innerException != null; innerException = innerException.InnerException)
    {
      stringBuilder.AppendLine(str);
      stringBuilder.AppendLine(innerException.Message);
      stringBuilder.AppendLine(innerException.StackTrace);
    }
    return stringBuilder.ToString();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ExceptionForm));
    this.exceptionText = new TextBox();
    this.button1 = new Button();
    this.button2 = new Button();
    this.labelHint = new Label();
    this.exceptionStack = new RichTextBox();
    this.button3 = new Button();
    this.button4 = new Button();
    this.sd = new SaveFileDialog();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.exceptionText, "exceptionText");
    this.exceptionText.Name = "exceptionText";
    this.exceptionText.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.Ignore;
    this.button1.Name = "button1";
    this.button2.DialogResult = DialogResult.Abort;
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.Name = "button2";
    componentResourceManager.ApplyResources((object) this.labelHint, "labelHint");
    this.labelHint.Name = "labelHint";
    componentResourceManager.ApplyResources((object) this.exceptionStack, "exceptionStack");
    this.exceptionStack.Name = "exceptionStack";
    this.exceptionStack.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.button3, "button3");
    this.button3.Name = "button3";
    this.button3.Click += new EventHandler(this.button3_Click);
    componentResourceManager.ApplyResources((object) this.button4, "button4");
    this.button4.Name = "button4";
    this.button4.Click += new EventHandler(this.button4_Click);
    this.sd.CheckPathExists = false;
    this.sd.DefaultExt = "xml";
    componentResourceManager.ApplyResources((object) this.sd, "sd");
    this.sd.RestoreDirectory = true;
    this.sd.SupportMultiDottedExtensions = true;
    this.AcceptButton = (IButtonControl) this.button1;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.button1;
    this.Controls.Add((Control) this.button4);
    this.Controls.Add((Control) this.button3);
    this.Controls.Add((Control) this.exceptionStack);
    this.Controls.Add((Control) this.labelHint);
    this.Controls.Add((Control) this.button2);
    this.Controls.Add((Control) this.button1);
    this.Controls.Add((Control) this.exceptionText);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ExceptionForm);
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void button3_Click(object sender, EventArgs e)
  {
    if (this._collapsed)
    {
      this.Height = this._fullHeight;
    }
    else
    {
      this._fullHeight = this.Height;
      this.Height = this.exceptionStack.Top + this._delta;
    }
    this._collapsed = !this._collapsed;
  }

  private void button4_Click(object sender, EventArgs e)
  {
    if (this.exc == null)
      return;
    DateTime now = DateTime.Now;
    this.sd.FileName = $"IPS_Error_({now.Year:D4}_{now.Month:D2}_{now.Day:D2})_{now.Hour:D2}-{now.Minute:D2}.xml";
    if (this.sd.ShowDialog() != DialogResult.OK)
      return;
    XMLSettingsStorage xmlSettingsStorage = new XMLSettingsStorage();
    xmlSettingsStorage.document.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><IPS />");
    XmlNode parentNode = xmlSettingsStorage.AddNode((XmlNode) xmlSettingsStorage.document.DocumentElement, "Exception");
    XmlNode xmlNode1 = xmlSettingsStorage.AddNode(parentNode, "ExceptionText");
    XmlNode xmlNode2 = xmlSettingsStorage.AddNode(parentNode, "ExceptionStack");
    XmlNode xmlNode3 = xmlSettingsStorage.AddNode(parentNode, "ExceptionSource");
    xmlNode1.InnerText = this.exc.Message;
    xmlNode2.InnerText = this.ShowExtendexStackTrace(this.exc);
    string source = this.exc.Source;
    xmlNode3.InnerText = source;
    FileInfo fileInfo = new FileInfo(typeof (XMLSettingsStorage).Assembly.Location);
    object[] customAttributes1 = typeof (XMLSettingsStorage).Assembly.GetCustomAttributes(typeof (AssemblyVersionString), true);
    object[] customAttributes2 = typeof (XMLSettingsStorage).Assembly.GetCustomAttributes(typeof (AssemblyBuildDate), true);
    object[] customAttributes3 = typeof (XMLSettingsStorage).Assembly.GetCustomAttributes(typeof (AssemblyBuildTime), true);
    object[] customAttributes4 = typeof (XMLSettingsStorage).Assembly.GetCustomAttributes(typeof (AssemblyBuildGuid), true);
    AssemblyVersionString assemblyVersionString = customAttributes1 == null || customAttributes1.Length == 0 ? (AssemblyVersionString) null : customAttributes1[0] as AssemblyVersionString;
    AssemblyBuildDate assemblyBuildDate = customAttributes2 == null || customAttributes2.Length == 0 ? (AssemblyBuildDate) null : customAttributes2[0] as AssemblyBuildDate;
    AssemblyBuildTime assemblyBuildTime = customAttributes3 == null || customAttributes3.Length == 0 ? (AssemblyBuildTime) null : customAttributes3[0] as AssemblyBuildTime;
    AssemblyBuildGuid assemblyBuildGuid = customAttributes4 == null || customAttributes4.Length == 0 ? (AssemblyBuildGuid) null : customAttributes4[0] as AssemblyBuildGuid;
    if (assemblyVersionString != null)
      xmlSettingsStorage.SetAttributeValue((XmlNode) xmlSettingsStorage.document.DocumentElement, "Build", assemblyVersionString.Description);
    if (assemblyBuildDate != null)
      xmlSettingsStorage.SetAttributeValue((XmlNode) xmlSettingsStorage.document.DocumentElement, "BuildDate", assemblyBuildDate.Description);
    if (assemblyBuildTime != null)
      xmlSettingsStorage.SetAttributeValue((XmlNode) xmlSettingsStorage.document.DocumentElement, "BuildTime", assemblyBuildTime.Description);
    if (assemblyBuildGuid != null)
      xmlSettingsStorage.SetAttributeValue((XmlNode) xmlSettingsStorage.document.DocumentElement, "BuildGuid", assemblyBuildGuid.Description);
    xmlSettingsStorage.Save(this.sd.FileName);
  }
}
