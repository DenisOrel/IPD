
// Type: Intermech.Client.Core.AdditionalCommandProviderExceptionForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Plugins;
using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>
/// 
/// </summary>
public class AdditionalCommandProviderExceptionForm : Form
{
  private bool _collapsed;
  private int _delta;
  private int _fullHeight;
  private Exception _exc;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _btnSkip;
  private Button _btnSkipAll;
  private Button _btnBreak;
  private Label _lb;
  private TextBox _txt;
  private RichTextBox _rtb;
  private Button _btnSave;
  private SaveFileDialog sd;
  private Button _btnDetails;
  private Panel _pnlButtons;

  /// <summary>Результат выполнения.</summary>
  public QuestionFormResult QuestionResult { get; private set; }

  /// <summary>Конструктор.</summary>
  public AdditionalCommandProviderExceptionForm()
  {
    this.InitializeComponent();
    this.QuestionResult = QuestionFormResult.Break;
    this._delta = this.Height - this.ClientRectangle.Height - 4;
    this._fullHeight = this.Height;
    this.Height = this._rtb.Top + this._delta;
  }

  /// <summary>Коструктор.</summary>
  /// <param name="ex">Ошибка</param>
  public AdditionalCommandProviderExceptionForm(AdditionalCommandProviderException ex)
    : this()
  {
    if (ex == null)
      return;
    this._exc = (Exception) ex;
    this._btnSkip.Visible = ex.BtnSkipVisible;
    this._btnSkipAll.Visible = ex.BtnSkipAllVisible;
    this._txt.Text = ex.Message;
    StringBuilder stringBuilder = new StringBuilder(ex.StackTrace);
    for (Exception innerException = ex.InnerException; innerException != null; innerException = innerException.InnerException)
    {
      stringBuilder.AppendLine("=");
      stringBuilder.AppendLine(innerException.Message);
      stringBuilder.AppendLine(innerException.StackTrace);
    }
    this._rtb.Text = stringBuilder.ToString();
  }

  /// <summary>Коструктор.</summary>
  /// <param name="ex">Ошибка</param>
  public AdditionalCommandProviderExceptionForm(Exception ex)
    : this()
  {
    if (ex == null)
      return;
    this._exc = ex;
    this._txt.Text = ex.Message;
    this._rtb.Text = ExceptionServices.GetExtendedStackTrace(ex);
  }

  /// <summary>Коструктор.</summary>
  /// <param name="text"></param>
  /// <param name="caption"></param>
  public AdditionalCommandProviderExceptionForm(string text, string caption)
    : this()
  {
    this._txt.Text = text;
    this.Text = caption;
  }

  /// <summary>Прервать.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnBreak_Click(object sender, EventArgs e)
  {
    this.QuestionResult = QuestionFormResult.Break;
    this.Close();
  }

  /// <summary>Отображение стека ошибки.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnDetails_Click(object sender, EventArgs e)
  {
    if (this._collapsed)
    {
      this.Height = this._fullHeight;
    }
    else
    {
      this._fullHeight = this.Height;
      this.Height = this._rtb.Top + this._delta;
    }
    this._collapsed = !this._collapsed;
  }

  /// <summary>Пропустить.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnSkip_Click(object sender, EventArgs e)
  {
    this.QuestionResult = QuestionFormResult.Skip;
    this.Close();
  }

  /// <summary>Пропустить все.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnSkipAll_Click(object sender, EventArgs e)
  {
    this.QuestionResult = QuestionFormResult.SkipAll;
    this.Close();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnSave_Click(object sender, EventArgs e)
  {
    if (this._exc == null)
      return;
    DateTime now = DateTime.Now;
    this.sd.FileName = $"IPS_Error_({now.Year:D4}_{now.Month:D2}_{now.Day:D2})_{now.Hour:D2}-{now.Minute:D2}.xml";
    IXMLSettingsStorage xml = ExceptionHelper.ExceptionToXML(this._exc, ServicesManager.GetService(typeof (IPluginManager)) as IPluginManager);
    if (this.sd.ShowDialog() != DialogResult.OK)
      return;
    xml.Save(this.sd.FileName);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ex"></param>
  /// <returns></returns>
  public static QuestionFormResult Show(AdditionalCommandProviderException ex)
  {
    using (AdditionalCommandProviderExceptionForm providerExceptionForm = new AdditionalCommandProviderExceptionForm(ex))
    {
      int num = (int) providerExceptionForm.ShowDialog();
      return providerExceptionForm.QuestionResult;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ex"></param>
  /// <returns></returns>
  public static QuestionFormResult Show(Exception ex)
  {
    using (AdditionalCommandProviderExceptionForm providerExceptionForm = new AdditionalCommandProviderExceptionForm(ex))
    {
      int num = (int) providerExceptionForm.ShowDialog();
      return providerExceptionForm.QuestionResult;
    }
  }

  /// <summary>Статическое отображение формы с возвратом результата.</summary>
  /// <param name="text">Текст сообщения об ошибке</param>
  /// <param name="caption">Заголовок формы</param>
  /// <param name="btnDetailsVisible"></param>
  /// <param name="btnSaveVisible"></param>
  /// <returns>Результат выполнения</returns>
  public static QuestionFormResult Show(
    string text,
    string caption,
    bool btnDetailsVisible = true,
    bool btnSaveVisible = true)
  {
    using (AdditionalCommandProviderExceptionForm providerExceptionForm = new AdditionalCommandProviderExceptionForm(text, caption))
    {
      providerExceptionForm._btnDetails.Visible = btnDetailsVisible;
      providerExceptionForm._btnSave.Visible = btnSaveVisible;
      if (!providerExceptionForm._btnDetails.Visible)
      {
        providerExceptionForm._rtb.Visible = false;
        providerExceptionForm._pnlButtons.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        providerExceptionForm._txt.Anchor |= AnchorStyles.Bottom;
      }
      int num = (int) providerExceptionForm.ShowDialog();
      providerExceptionForm._btnSave.Visible = false;
      return providerExceptionForm.QuestionResult;
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AdditionalCommandProviderExceptionForm));
    this._btnBreak = new Button();
    this._btnSkipAll = new Button();
    this._btnSkip = new Button();
    this._lb = new Label();
    this._txt = new TextBox();
    this._btnDetails = new Button();
    this._rtb = new RichTextBox();
    this._btnSave = new Button();
    this.sd = new SaveFileDialog();
    this._pnlButtons = new Panel();
    this._pnlButtons.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._btnBreak, "_btnBreak");
    this._btnBreak.DialogResult = DialogResult.Cancel;
    this._btnBreak.Name = "_btnBreak";
    this._btnBreak.Click += new EventHandler(this.On_btnBreak_Click);
    componentResourceManager.ApplyResources((object) this._btnSkipAll, "_btnSkipAll");
    this._btnSkipAll.Name = "_btnSkipAll";
    this._btnSkipAll.Click += new EventHandler(this.On_btnSkipAll_Click);
    componentResourceManager.ApplyResources((object) this._btnSkip, "_btnSkip");
    this._btnSkip.Name = "_btnSkip";
    this._btnSkip.Click += new EventHandler(this.On_btnSkip_Click);
    componentResourceManager.ApplyResources((object) this._lb, "_lb");
    this._lb.Name = "_lb";
    componentResourceManager.ApplyResources((object) this._txt, "_txt");
    this._txt.Name = "_txt";
    this._txt.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this._btnDetails, "_btnDetails");
    this._btnDetails.Name = "_btnDetails";
    this._btnDetails.Click += new EventHandler(this.On_btnDetails_Click);
    componentResourceManager.ApplyResources((object) this._rtb, "_rtb");
    this._rtb.Name = "_rtb";
    this._rtb.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this._btnSave, "_btnSave");
    this._btnSave.Name = "_btnSave";
    this._btnSave.Click += new EventHandler(this.On_btnSave_Click);
    this.sd.CheckPathExists = false;
    this.sd.DefaultExt = "xml";
    componentResourceManager.ApplyResources((object) this.sd, "sd");
    this.sd.RestoreDirectory = true;
    this.sd.SupportMultiDottedExtensions = true;
    this._pnlButtons.Controls.Add((Control) this._btnSave);
    this._pnlButtons.Controls.Add((Control) this._btnDetails);
    this._pnlButtons.Controls.Add((Control) this._btnBreak);
    this._pnlButtons.Controls.Add((Control) this._btnSkip);
    this._pnlButtons.Controls.Add((Control) this._btnSkipAll);
    componentResourceManager.ApplyResources((object) this._pnlButtons, "_pnlButtons");
    this._pnlButtons.Name = "_pnlButtons";
    this.AcceptButton = (IButtonControl) this._btnSkip;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._btnBreak;
    this.Controls.Add((Control) this._pnlButtons);
    this.Controls.Add((Control) this._rtb);
    this.Controls.Add((Control) this._txt);
    this.Controls.Add((Control) this._lb);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AdditionalCommandProviderExceptionForm);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this._pnlButtons.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
