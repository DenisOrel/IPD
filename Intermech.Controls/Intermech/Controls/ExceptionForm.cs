
// Type: Intermech.Controls.ExceptionForm
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Diagnostics;
using Intermech.Extensions.WinForms;
using Intermech.Interfaces.Client;
using Intermech.UI.ExceptionHandling;
using Intermech.UI.Winforms.CodeBehaviors;
using Intermech.UI.Wpf.WinformsInterop;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;


namespace Intermech.Controls;

/// <summary>
/// Форма, предназначенная для отображения информации о возникшей исключительной ситуации (Exception)
/// </summary>
/// <remarks>
/// Внимание! Не следует непосредственно создавать эту форму или наследоваться от нее. Вместо этого следует использовать
/// сервис <see cref="T:Intermech.ApplicationModel.IExceptionDisplayService" />,
/// сервис <see cref="T:Intermech.Interfaces.IExceptionHandlerService" /> или
/// вспомогательный класс <see cref="T:Intermech.ExceptionHelper" />.
/// </remarks>
public sealed class ExceptionForm : Form
{
  private SaveFileDialog saveFileDialog;
  private ExceptionVM exceptionViewModel;
  private WpfElementHost wpfViewerHost;
  private ExceptionViewerControl wpfViewerControl;
  private AutoCloseBehavior formAutoCloseBehavior;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  /// <summary>Создать пустой экземпляр формы</summary>
  public ExceptionForm()
  {
    this.InitializeComponent();
    this.exceptionViewModel = new ExceptionVM();
    this.exceptionViewModel.FontSize = 11.0;
    this.exceptionViewModel.SaveToFile += new EventHandler(this.btnSave_Click);
    this.exceptionViewModel.EmailReport += new EventHandler(this.btnSendReport_Click);
    this.exceptionViewModel.PropertyChanged += new PropertyChangedEventHandler(this.OnViewModelPropertyChanged);
    this.formAutoCloseBehavior = new AutoCloseBehavior((Form) this, (INotifyPropertyChanged) this.exceptionViewModel);
    this.wpfViewerControl.DataContext = (object) this.exceptionViewModel;
    this.wpfViewerControl.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.OnPreviewKeyDown);
    HelpProvidersClass.SetHelpOptionForControl((System.Windows.Forms.Control) this, 1371);
  }

  /// <summary>Освободить ресурсы</summary>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ExceptionForm));
    this.saveFileDialog = new SaveFileDialog();
    this.wpfViewerHost = new WpfElementHost();
    this.wpfViewerControl = new ExceptionViewerControl();
    this.SuspendLayout();
    this.saveFileDialog.CheckPathExists = false;
    this.saveFileDialog.DefaultExt = "zip";
    componentResourceManager.ApplyResources((object) this.saveFileDialog, "saveFileDialog");
    this.saveFileDialog.RestoreDirectory = true;
    this.saveFileDialog.SupportMultiDottedExtensions = true;
    componentResourceManager.ApplyResources((object) this.wpfViewerHost, "wpfViewerHost");
    this.wpfViewerHost.Name = "wpfViewerHost";
    this.wpfViewerHost.Child = (UIElement) this.wpfViewerControl;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((System.Windows.Forms.Control) this.wpfViewerHost);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ExceptionForm);
    this.ShowIcon = false;
    this.FormClosed += new FormClosedEventHandler(this.ExceptionForm_FormClosed);
    this.Shown += new EventHandler(this.ExceptionForm_Shown);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary>
  /// Возвращает модель вида для отображения исключения (WPF MVVM ViewModel).
  /// </summary>
  public ExceptionVM ViewModel
  {
    [DebuggerStepThrough] get => this.exceptionViewModel;
  }

  /// <summary>
  /// Отобразить информацию о возникшей исключительной ситуации (Exception)
  /// </summary>
  /// <returns>Тип нажатой в окне кнопки</returns>
  public DialogResult ShowDialogWithOwner() => this.ShowTopDialog();

  private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
  {
    if (!(e.PropertyName == "Exception"))
      return;
    Exception exception = this.ViewModel.Exception;
    if (exception != null)
    {
      this.ViewModel.DateTime = DateTime.Now;
      this.ViewModel.Message = this.ExtractExceptionMessage(exception);
      this.ViewModel.TechnicalInfo = exception.GetOriginalStackTrace() ?? ExceptionServices.GetExtendedStackTrace(exception);
    }
    else
    {
      this.ViewModel.Message = string.Empty;
      this.ViewModel.TechnicalInfo = string.Empty;
    }
  }

  private void OnPreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
  {
    if (e.Key != Key.F4 || e.KeyboardDevice.Modifiers != (System.Windows.Input.ModifierKeys.Alt | System.Windows.Input.ModifierKeys.Control))
      return;
    this.ViewModel.Abort();
  }

  /// <summary>Отображение формы</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void ExceptionForm_Shown(object sender, EventArgs e)
  {
    this.ViewModel.IsClosed = false;
    this.AdjustFormAutoSizeAndLocation();
  }

  private void ExceptionForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    this.DialogResult = this.ViewModel.IsAborted ? DialogResult.Abort : DialogResult.Ignore;
  }

  private void AdjustFormAutoSizeAndLocation()
  {
    Rectangle rectangle = this.Owner != null ? Screen.FromControl((System.Windows.Forms.Control) this.Owner).WorkingArea : Screen.PrimaryScreen.WorkingArea;
    System.Windows.Size dipSize1 = this.DevicePixelsToDIPSize(rectangle.Width, rectangle.Height, (System.Windows.Controls.Control) this.wpfViewerControl);
    System.Windows.Size dipSize2 = this.DevicePixelsToDIPSize(960, 700, (System.Windows.Controls.Control) this.wpfViewerControl);
    this.wpfViewerControl.Width = Math.Min(dipSize2.Width, dipSize1.Width * 0.8);
    this.wpfViewerControl.MaxHeight = Math.Min(dipSize2.Height, dipSize1.Height * 0.8);
    this.wpfViewerControl.VerticalAlignment = VerticalAlignment.Top;
    this.wpfViewerHost.MaximumSize = System.Drawing.Size.Empty;
    SizeF devicePixels = this.DIPSizeToDevicePixels(this.wpfViewerControl.Width, this.wpfViewerControl.MaxHeight, (System.Windows.Controls.Control) this.wpfViewerControl);
    this.Location = System.Drawing.Point.Round(new PointF((float) (((double) rectangle.Width - (double) devicePixels.Width) / 2.0), (float) (((double) rectangle.Height - (double) devicePixels.Height) / 2.0)));
  }

  private SizeF DIPSizeToDevicePixels(double width, double height, System.Windows.Controls.Control wpfControl)
  {
    System.Windows.Point point = PresentationSource.FromVisual((Visual) wpfControl).CompositionTarget.TransformToDevice.Transform(new System.Windows.Point(width, height));
    return new SizeF((float) point.X, (float) point.Y);
  }

  private System.Windows.Size DevicePixelsToDIPSize(int width, int height, System.Windows.Controls.Control wpfControl)
  {
    System.Windows.Point point = PresentationSource.FromVisual((Visual) wpfControl).CompositionTarget.TransformFromDevice.Transform(new System.Windows.Point((double) width, (double) height));
    return new System.Windows.Size(point.X, point.Y);
  }

  /// <summary>Получение текста сообщения из Исключения</summary>
  /// <param name="e">Исключение</param>
  /// <returns>текст сообщения</returns>
  private string ExtractExceptionMessage(Exception e)
  {
    string message = e.Message;
    while (e is TargetInvocationException)
      e = e.InnerException;
    return e != null ? e.Message : message;
  }

  /// <summary>Нажата кнопка "Сохранить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btnSave_Click(object sender, EventArgs e)
  {
    if (this.ViewModel.Exception == null || this.ViewModel.SaveHandler == null)
      return;
    DateTime dateTime = this.ViewModel.DateTime;
    string str = $"IPS_Error_({dateTime.Year:D4}_{dateTime.Month:D2}_{dateTime.Day:D2})_{dateTime.Hour:D2}-{dateTime.Minute:D2}.zip";
    string tempFileName = Path.GetTempFileName();
    try
    {
      this.ViewModel.SaveHandler.SaveToFile(this.ViewModel.Exception, tempFileName);
      this.saveFileDialog.FileName = str;
      if (this.saveFileDialog.ShowDialog() != DialogResult.OK)
        return;
      File.Copy(tempFileName, this.saveFileDialog.FileName, true);
    }
    finally
    {
      File.Delete(tempFileName);
    }
  }

  private void btnSendReport_Click(object sender, EventArgs e)
  {
    if (this.ViewModel.Exception == null || this.ViewModel.SaveHandler == null)
      return;
    using (ReportTopicForm reportTopicForm = new ReportTopicForm())
    {
      if (reportTopicForm.ShowDialog() != DialogResult.OK)
        return;
      for (int index = 0; index < 10; ++index)
      {
        System.Windows.Forms.Application.DoEvents();
        Thread.Sleep(50);
      }
      this.ViewModel.SaveHandler.SendByEmail(this.ViewModel.Exception, reportTopicForm.ReportTopic, reportTopicForm.ReportText);
    }
  }
}
