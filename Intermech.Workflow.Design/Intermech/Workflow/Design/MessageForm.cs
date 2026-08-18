// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.MessageForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinForms.Documents.FormatProviders.Html;
using Telerik.WinForms.Documents.Model;

#nullable disable
namespace Intermech.Workflow.Design;

public class MessageForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel Panel2;
  private Button OkButton;
  private RadRichTextEditor radRichTextEditor;

  public MessageForm()
  {
    this.InitializeComponent();
    this.radRichTextEditor.HyperlinkToolTipFormatString = "Нажмите {1} для перехода по ссылке. (Адрес ссылки: {0})";
  }

  public MessageForm(string text)
    : this()
  {
    text = $"<html>\r\n<head><meta HTTP-EQUIV=\"content-type\" CONTENT=\"text/html; charset=UTF-8\"><style>pre {{  white-space: pre-wrap; white-space: -moz-pre-wrap; white-space: -pre-wrap; white-space: -o-pre-wrap; word-wrap: break-word; }}</style></head>\r\n<body><pre>{text}</pre></body>\r\n</html>";
    string path = Path.GetTempPath() + "wfmsg2.htm";
    using (StreamWriter streamWriter = new StreamWriter(path))
      streamWriter.Write(text);
    using (Stream input = (Stream) File.OpenRead(path))
      this.radRichTextEditor.Document = new HtmlFormatProvider().Import(input);
  }

  public static void Show(string text, string caption)
  {
    MessageForm messageForm = new MessageForm(text);
    messageForm.ShowInTaskbar = true;
    messageForm.Text = caption;
    messageForm.Show();
  }

  private void MessageForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void MessageForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private long ConvertToObjectID(string s) => this.ConvertToObjectID(s, false);

  private long ConvertToObjectID(string s, bool silent)
  {
    long result = 0;
    if (long.TryParse(s, out result))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetObject(result, false) == null)
          result = 0L;
      }
    }
    else
    {
      Guid objectGUID = Guid.Empty;
      try
      {
        objectGUID = new Guid($"{{{s}}}");
      }
      catch
      {
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectGUID, false);
        if (dbObject != null)
          return dbObject.ObjectID;
      }
    }
    if (!silent && result == 0L)
      wfFunx.SayError(string.Format(LocalizationHolder.GetString("ObjectNotFound"), (object) s));
    return result;
  }

  private void OkButton_Click(object sender, EventArgs e) => this.Close();

  protected override void CreateHandle()
  {
    this.KeyPreview = true;
    this.StartPosition = FormStartPosition.CenterParent;
    if (!this.DesignMode && this.FormBorderStyle == FormBorderStyle.Sizable)
      this.MinimumSize = new Size(250, 250);
    base.CreateHandle();
  }

  protected override void OnKeyDown(KeyEventArgs e)
  {
    base.OnKeyDown(e);
    if (!this.Modal || e.KeyCode != Keys.Escape)
      return;
    this.DialogResult = DialogResult.Cancel;
  }

  private void radRichTextEditor_HyperlinkClicked(object sender, HyperlinkClickedEventArgs e)
  {
    string[] strArray = e.URL.Split('=');
    switch (strArray[0])
    {
      case "activity":
        wfFunx.ShowActivityProperties(Convert.ToInt64(strArray[1]));
        this.Close();
        break;
      case "message":
        wfFunx.ShowActivityMessage(Convert.ToInt64(strArray[1]));
        this.Close();
        break;
      case "object":
        long objectId1 = this.ConvertToObjectID(strArray[1]);
        if (objectId1 != 0L)
        {
          ServiceContainer viewServices = new ServiceContainer();
          viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
          Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(objectId1), (System.IServiceProvider) viewServices);
        }
        this.Close();
        break;
      case "process":
        long objectId2 = this.ConvertToObjectID(strArray[1]);
        if (objectId2 != 0L)
          wfFunx.ViewProcess(objectId2);
        this.Close();
        break;
      case "view":
        long objectId3 = this.ConvertToObjectID(strArray[1]);
        if (objectId3 != 0L)
          wfFunx.TryViewByNavigator(objectId3);
        this.Close();
        break;
      case "edit":
        long objectId4 = this.ConvertToObjectID(strArray[1]);
        if (objectId4 != 0L)
          wfFunx.TryEditByNavigator(objectId4);
        this.Close();
        break;
    }
    e.Handled = false;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MessageForm));
    this.Panel2 = new Panel();
    this.OkButton = new Button();
    this.radRichTextEditor = new RadRichTextEditor();
    this.Panel2.SuspendLayout();
    this.radRichTextEditor.BeginInit();
    this.SuspendLayout();
    this.Panel2.Controls.Add((Control) this.OkButton);
    componentResourceManager.ApplyResources((object) this.Panel2, "Panel2");
    this.Panel2.Name = "Panel2";
    componentResourceManager.ApplyResources((object) this.OkButton, "OkButton");
    this.OkButton.DialogResult = DialogResult.OK;
    this.OkButton.Name = "OkButton";
    this.OkButton.Click += new EventHandler(this.OkButton_Click);
    this.radRichTextEditor.BorderColor = Color.FromArgb(172, 172, 172);
    componentResourceManager.ApplyResources((object) this.radRichTextEditor, "radRichTextEditor");
    this.radRichTextEditor.EnableTheming = false;
    this.radRichTextEditor.IsContextMenuEnabled = false;
    this.radRichTextEditor.IsPasteOptionsPopupEnabled = false;
    this.radRichTextEditor.IsReadOnly = true;
    this.radRichTextEditor.IsSelectionMiniToolBarEnabled = false;
    this.radRichTextEditor.LayoutMode = new DocumentLayoutMode?(DocumentLayoutMode.Flow);
    this.radRichTextEditor.Name = "radRichTextEditor";
    this.radRichTextEditor.SelectionFill = Color.FromArgb(128 /*0x80*/, 78, 158, (int) byte.MaxValue);
    this.radRichTextEditor.SelectionStroke = Color.FromArgb(0, 0, 115, (int) byte.MaxValue);
    this.radRichTextEditor.ThemeName = "Windows8";
    this.radRichTextEditor.HyperlinkClicked += new EventHandler<HyperlinkClickedEventArgs>(this.radRichTextEditor_HyperlinkClicked);
    this.AcceptButton = (IButtonControl) this.OkButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.OkButton;
    this.Controls.Add((Control) this.radRichTextEditor);
    this.Controls.Add((Control) this.Panel2);
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (MessageForm);
    this.FormClosed += new FormClosedEventHandler(this.MessageForm_FormClosed);
    this.Load += new EventHandler(this.MessageForm_Load);
    this.Panel2.ResumeLayout(false);
    this.radRichTextEditor.EndInit();
    this.ResumeLayout(false);
  }
}
