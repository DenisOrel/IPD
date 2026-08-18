
// Type: Intermech.Client.Core.SOAPExceptionForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using System.Xml;


namespace Intermech.Client.Core;

/// <summary>Summary description for ExceptionForm.</summary>
public class SOAPExceptionForm : Form
{
  private Button button1;
  private Label label2;
  private TextBox exceptionText;
  private RichTextBox exceptionStack;
  private Button button3;
  private bool _collapsed;
  private int _delta;
  private int _fullHeight;
  private Button button4;
  private SaveFileDialog sd;
  private Exception exc;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  public SOAPExceptionForm()
  {
    this.InitializeComponent();
    this._collapsed = true;
    this._delta = this.Height - this.ClientRectangle.Height - 4;
    this._fullHeight = this.Height;
    this.Height = this.exceptionStack.Top + this._delta;
    this.button3.Select();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1371);
  }

  public DialogResult ShowException(Exception e)
  {
    this.exc = e;
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    SoapExceptionHelper.ParceMessage(e.Message, ref empty1, ref empty2);
    if (empty1 != string.Empty)
    {
      this.exceptionText.Text = empty1;
      this.exceptionStack.Text = this.ShowExtendexStackTrace(empty2, e);
    }
    else
    {
      this.exceptionText.Text = e.Message;
      this.exceptionStack.Text = this.ShowExtendexStackTrace(string.Empty, e);
    }
    return this.ShowDialog();
  }

  private string ShowExtendexStackTrace(string portalStack, Exception e)
  {
    StringBuilder stringBuilder = new StringBuilder(256 /*0x0100*/);
    if (portalStack != string.Empty)
      stringBuilder.AppendLine(portalStack);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SOAPExceptionForm));
    this.exceptionText = new TextBox();
    this.button1 = new Button();
    this.label2 = new Label();
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
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
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
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.button1);
    this.Controls.Add((Control) this.exceptionText);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SOAPExceptionForm);
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

  /// <summary>Нажата кнопка "Сохранить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
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
    XmlNode parentNode1 = xmlSettingsStorage.AddNode((XmlNode) xmlSettingsStorage.document.DocumentElement, "Exception");
    XmlNode xmlNode1 = xmlSettingsStorage.AddNode(parentNode1, "ExceptionText");
    XmlNode xmlNode2 = xmlSettingsStorage.AddNode(parentNode1, "ExceptionStack");
    XmlNode xmlNode3 = xmlSettingsStorage.AddNode(parentNode1, "ExceptionSource");
    xmlNode1.InnerText = this.exceptionText.Text;
    xmlNode2.InnerText = this.exceptionStack.Text;
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
    try
    {
      if (ServicesManager.GetService(typeof (IPluginManager)) is IPluginManager service)
      {
        XmlNode parentNode2 = xmlSettingsStorage.AddNode((XmlNode) xmlSettingsStorage.document.DocumentElement, "Plugins");
        foreach (IPlugin plugin in (IEnumerable<IPlugin>) service.Plugins)
        {
          foreach (IPackage package in (IEnumerable<IPackage>) plugin.Packages)
          {
            XmlNode node = xmlSettingsStorage.AddNode(parentNode2, "Plugin");
            string location = plugin.Location;
            string str = package.GetType().Assembly.GetName().Version.ToString();
            xmlSettingsStorage.SetAttributeValue(node, "name", package.Name);
            xmlSettingsStorage.SetAttributeValue(node, "version", str);
            xmlSettingsStorage.SetAttributeValue(node, "location", location);
          }
        }
      }
    }
    catch
    {
    }
    xmlSettingsStorage.Save(this.sd.FileName);
  }

  public static void OnExceptionHandler(object sender, ExceptionEventArgs e)
  {
    if (SOAPExceptionForm.IsSOAPException(e.Exception))
    {
      e.Handled = true;
    }
    else
    {
      for (Exception innerException = e.Exception != null ? e.Exception.InnerException : (Exception) null; innerException != null; innerException = innerException.InnerException)
      {
        if (SOAPExceptionForm.IsSOAPException(innerException))
        {
          e.Handled = true;
          break;
        }
      }
    }
  }

  private static bool IsSOAPException(Exception e)
  {
    if (!(e is SoapException e1))
      return false;
    int num = (int) new SOAPExceptionForm().ShowException((Exception) e1);
    return true;
  }
}
