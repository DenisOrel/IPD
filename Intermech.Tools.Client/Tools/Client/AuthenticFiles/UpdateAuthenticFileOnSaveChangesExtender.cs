// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.AuthenticFiles.UpdateAuthenticFileOnSaveChangesExtender
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Runtime;
using Intermech.Tools.CommonTasks;
using Intermech.Tools.Integrators.CADInterface;
using Intermech.Tools.Integrators.Notifications;
using Intermech.UI;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Client.AuthenticFiles;

internal sealed class UpdateAuthenticFileOnSaveChangesExtender : ServiceExtender
{
  private INotificationService notificationService;
  private DocumentFilesTaskFactory taskFactory;
  private string authenticFileExtension;
  private Predicate<SaveChangesMode> saveChangesModeFilter;

  public UpdateAuthenticFileOnSaveChangesExtender(
    INotificationService notificationService,
    DocumentFilesTaskFactory taskFactory)
  {
    if (notificationService == null)
      throw new ArgumentNullException(nameof (notificationService));
    if (taskFactory == null)
      throw new ArgumentNullException(nameof (taskFactory));
    this.notificationService = notificationService;
    this.taskFactory = taskFactory;
    this.authenticFileExtension = string.Empty;
  }

  public string AuthenticFileExtension
  {
    [DebuggerStepThrough] get => this.authenticFileExtension;
    set
    {
      this.authenticFileExtension = value != null ? value : throw new ArgumentNullException(nameof (value));
    }
  }

  public Predicate<SaveChangesMode> SaveChangesModeFilter
  {
    [DebuggerStepThrough] get => this.saveChangesModeFilter;
    [DebuggerStepThrough] set => this.saveChangesModeFilter = value;
  }

  protected override void DoEnable()
  {
    if (string.IsNullOrEmpty(this.AuthenticFileExtension))
      throw PropertyExceptions.PropertyNotSetException((object) this, "AuthenticFileExtension");
    base.DoEnable();
    this.notificationService.Subscribe(CaptureChangesEventArgs.CaptureChangesCompleted, new NotificationEventHandler(this.OnCaptureChangesCompleted));
  }

  protected override void DoDisable()
  {
    this.notificationService.Unsubscribe(CaptureChangesEventArgs.CaptureChangesCompleted, new NotificationEventHandler(this.OnCaptureChangesCompleted));
    base.DoDisable();
  }

  private void OnCaptureChangesCompleted(object sender, NotificationEventArgs e)
  {
    CaptureChangesEventArgs captureChangesEventArgs = (CaptureChangesEventArgs) e;
    if (captureChangesEventArgs.Documents.Count == 0 || !this.IsSaveChangesModeSupported(captureChangesEventArgs))
      return;
    CADSettings cadSettings = this.TryGetCADSettings(captureChangesEventArgs);
    if (cadSettings == null)
      return;
    foreach (CaptureChangesDocumentInfo document in captureChangesEventArgs.Documents)
    {
      DocumentGroup byDocumentType = cadSettings.FileDocumentGroups.FindByDocumentType(document.ObjectTypeId, false);
      if (byDocumentType != null && this.IsDocumentGroupSupported(cadSettings, byDocumentType))
      {
        if (UIReport.Enabled)
          UIReport.StartLogicalOperation((object) this);
        try
        {
          if (UIReport.Enabled)
            UIReport.ReportEvent($"Аутентичные файлы: выполняется автоматическое формирование файла типа {this.AuthenticFileExtension}");
          MakeAuthenticFileTask authenticFileTask = this.taskFactory.MakeAuthenticFile();
          authenticFileTask.Initialize(document.ObjectId, document.ObjectTypeId, this.AuthenticFileExtension, (string) null);
          authenticFileTask.Perform();
        }
        catch (Exception ex)
        {
          if (UIReport.Enabled)
            UIReport.ReportEvent($"Аутентичные файлы: при формировании файла типа {this.AuthenticFileExtension} произошла ошибка. {ex.Message}");
        }
        finally
        {
          if (UIReport.Enabled)
            UIReport.StopLogicalOperation((object) this);
        }
      }
    }
  }

  private CADSettings TryGetCADSettings(CaptureChangesEventArgs captureChangesEventArgs)
  {
    try
    {
      return ServiceUtils.GetService<ICADSettingsService>((object) captureChangesEventArgs.Integrator, true).GetCADSettings();
    }
    catch
    {
      return (CADSettings) null;
    }
  }

  private bool IsSaveChangesModeSupported(CaptureChangesEventArgs captureChangesEventArgs)
  {
    return this.saveChangesModeFilter == null || this.saveChangesModeFilter(captureChangesEventArgs.Mode);
  }

  private bool IsDocumentGroupSupported(CADSettings cadSettings, DocumentGroup documentGroup)
  {
    return cadSettings.UpdateModelAuthenticFilesOnCheckin && (documentGroup.Name == "Assembly" || documentGroup.Name == "Part") || cadSettings.UpdateDrawingAuthenticFilesOnCheckin && (documentGroup.Name == "AssemblyDrawing" || documentGroup.Name == "PartDrawing");
  }
}
