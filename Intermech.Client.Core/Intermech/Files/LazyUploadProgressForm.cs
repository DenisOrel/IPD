
// Type: Intermech.Files.LazyUploadProgressForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Windows.Forms;


namespace Intermech.Files;

internal sealed class LazyUploadProgressForm : IUploadProgressForm, IDisposable
{
  private UploadProgressForm nativeForm;

  void IUploadProgressForm.MakeVisible(long startTime)
  {
    if ((long) Environment.TickCount - startTime <= 1500L || this.nativeForm != null)
      return;
    this.nativeForm = new UploadProgressForm();
    this.nativeForm.Show();
  }

  void IUploadProgressForm.ShowWorkObject(DBObjectState workObject)
  {
    if (this.nativeForm == null)
      return;
    this.nativeForm.ShowWorkObject(workObject);
  }

  void IUploadProgressForm.ShowProgress(double percentComplete)
  {
    if (this.nativeForm == null)
      return;
    this.nativeForm.ShowProgress(percentComplete);
  }

  void IUploadProgressForm.DoEvents()
  {
    if (this.nativeForm == null)
      return;
    Application.DoEvents();
  }

  bool IUploadProgressForm.IsCancelRequested()
  {
    return this.nativeForm != null && this.nativeForm.IsCancelRequested();
  }

  void IDisposable.Dispose()
  {
    if (this.nativeForm == null)
      return;
    this.nativeForm.Dispose();
  }
}
