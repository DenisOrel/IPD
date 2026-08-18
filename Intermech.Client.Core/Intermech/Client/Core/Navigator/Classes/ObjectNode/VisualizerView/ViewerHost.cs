
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewerHost
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost;
using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.Interfaces;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView;

internal class ViewerHost : UserControl
{
  /// <summary>менеджер настройки просмотра</summary>
  private IExtensionsService _extensionsService;
  /// <summary>Провайдер просмотрщиков данной вкладки</summary>
  private ViewerProvider _viewerProvider;
  /// <summary>Сервис вывода сообщшений</summary>
  private IOutputView _outputView;
  /// <summary>Флаг, писать лог или нет</summary>
  private bool _writeLog;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public IViewer CurrentView { get; private set; }

  /// <summary>конструктор</summary>
  public ViewerHost()
  {
    this.InitializeComponent();
    this._viewerProvider = new ViewerProvider();
  }

  public void InitializeServices()
  {
    this._outputView = ServiceUtils.GetService<IOutputView>((object) ServicesManager.ServiceContainer, true);
    this._extensionsService = ServiceUtils.GetService<IExtensionsService>((object) ServicesManager.ServiceContainer, true);
    this._viewerProvider.InitializeServices();
  }

  /// <summary>открыть вьювер</summary>
  /// <param name="fileItem">имя файла</param>
  /// <param name="serviceProvider"></param>
  public void Open(FileItem fileItem, System.IServiceProvider serviceProvider)
  {
    this._writeLog = this._extensionsService.DebugMode;
    this.WriteLogMsg(Environment.NewLine);
    this.WriteLogMsg(DateTime.Now.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    this.WriteLogMsg(string.Format(LocalizationHolder.rm.GetString("ViewFile"), (object) fileItem.FileName));
    string @extension = Path.GetExtension(fileItem.FileName)?.ToLower() ?? string.Empty;
    IViewer viewer1 = this._viewerProvider.TryFindViewer(@extension);
    if (viewer1 != null)
    {
      this.WriteLogMsg(string.Format(LocalizationHolder.rm.GetString("ViewerFindInCache"), (object) @extension, (object) viewer1.GetType()));
      this.CurrentView = viewer1;
      try
      {
        this.CurrentView.Open(fileItem, serviceProvider);
      }
      catch (Exception ex)
      {
        this.WriteLogMsg(ex.Message);
      }
    }
    else
    {
      IReadOnlyCollection<FileExtensionsInfo> fileExtensionsInfo1 = this._extensionsService.GetFileExtensionsInfo(@extension);
      if (fileExtensionsInfo1 == null || !fileExtensionsInfo1.Any<FileExtensionsInfo>())
      {
        this.WriteLogMsg(string.Format(LocalizationHolder.rm.GetString("ViewMethodNotFindForExtension"), (object) @extension));
        this.CurrentView = (IViewer) null;
      }
      else
      {
        this.WriteLogMsg(string.Format(LocalizationHolder.rm.GetString("ViewMethodFoundedForExtension"), (object) @extension, (object) string.Join<StyleView>(Environment.NewLine, (IEnumerable<StyleView>) fileExtensionsInfo1.Select<FileExtensionsInfo, StyleView>((Func<FileExtensionsInfo, StyleView>) (x => x.Style)).ToArray<StyleView>())));
        foreach (FileExtensionsInfo fileExtensionsInfo2 in (IEnumerable<FileExtensionsInfo>) fileExtensionsInfo1)
        {
          IViewer viewer2 = this._viewerProvider.GetViewer(fileExtensionsInfo2);
          if (viewer2 == null)
          {
            this.WriteLogMsg(string.Format(LocalizationHolder.rm.GetString("CantGetViewerForOpenMethod"), (object) fileExtensionsInfo2.Style));
          }
          else
          {
            this.WriteLogMsg(string.Format(LocalizationHolder.rm.GetString("OpenFileByViewer"), (object) viewer2.GetType()));
            try
            {
              this.WriteLogMsg(LocalizationHolder.rm.GetString("ViewerInitialize"));
              this.SuspendLayout();
              viewer2.Init((Control) this);
              this.WriteLogMsg(LocalizationHolder.rm.GetString("OpenFileViewer"));
              viewer2.Open(fileItem, serviceProvider);
              this.ResumeLayout(false);
            }
            catch (AccessDeniedException ex)
            {
              this.WriteLogMsg(ex.Message);
              throw;
            }
            catch (Exception ex)
            {
              viewer2.Clear();
              this.WriteLogMsg(ex.Message);
              continue;
            }
            this._viewerProvider.AddView(@extension, viewer2);
            this._extensionsService.AddFileExtensionInfoToCache(@extension, fileExtensionsInfo2);
            this.WriteLogMsg(string.Format(LocalizationHolder.rm.GetString("ViewerAddedToCache"), (object) viewer2.GetType()));
            this.CurrentView = viewer2;
            return;
          }
        }
        this.CurrentView = (IViewer) null;
      }
    }
  }

  /// <summary>Очистка кэша провайдера просмотрщиков</summary>
  public void Clear() => this._viewerProvider.ClearViewersCache();

  /// <summary>Очистка от предыдущего просмотра</summary>
  public void CloseCurrentViewer()
  {
    this.CurrentView?.Close();
    this.CurrentView = (IViewer) null;
  }

  /// <summary>Запись строки в лог</summary>
  /// <param name="msg"></param>
  private void WriteLogMsg(string msg)
  {
    if (!this._writeLog)
      return;
    this._outputView.WriteString(LocalizationHolder.rm.GetString("Client.Core_378"), msg);
  }

  private void ViewerHost_Paint(object sender, PaintEventArgs e)
  {
    if (this.CurrentView != null)
      return;
    using (StringFormat format = new StringFormat())
    {
      format.Alignment = StringAlignment.Center;
      format.LineAlignment = StringAlignment.Center;
      e.Graphics.DrawString(LocalizationHolder.rm.GetString("Client.Core_380"), this.Font, SystemBrushes.ControlText, (RectangleF) this.DisplayRectangle, format);
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
    this.SuspendLayout();
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (ViewerHost);
    this.Size = new Size(683, 409);
    this.Paint += new PaintEventHandler(this.ViewerHost_Paint);
    this.ResumeLayout(false);
  }
}
