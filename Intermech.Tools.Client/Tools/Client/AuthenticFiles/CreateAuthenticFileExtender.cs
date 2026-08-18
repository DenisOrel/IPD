// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.AuthenticFiles.CreateAuthenticFileExtender
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces.Client;
using Intermech.Tools.CommonTasks;

#nullable disable
namespace Intermech.Tools.Client.AuthenticFiles;

internal sealed class CreateAuthenticFileExtender : ServiceExtender
{
  private readonly IAuthFilesService authService;
  private readonly DocumentFilesTaskFactory documentFilesTaskFactory;

  public CreateAuthenticFileExtender(
    IAuthFilesService authService,
    DocumentFilesTaskFactory documentFilesTaskFactory)
  {
    this.authService = authService;
    this.documentFilesTaskFactory = documentFilesTaskFactory;
  }

  protected override void DoEnable()
  {
    base.DoEnable();
    this.authService.AuthFileAssignEvent += new AuthFileAssignEventHandler(this.OnAssignAuthenticFiles);
  }

  protected override void DoDisable()
  {
    this.authService.AuthFileAssignEvent -= new AuthFileAssignEventHandler(this.OnAssignAuthenticFiles);
    base.DoDisable();
  }

  private void OnAssignAuthenticFiles(object sender, AuthFileAssignEventArgs e)
  {
    if (e.IsHandled)
      return;
    MakeAuthenticFileTask authenticFileTask = this.documentFilesTaskFactory.MakeAuthenticFile();
    authenticFileTask.Initialize(e.ObjectId, e.ObjectType, e.PDFOnly ? ".pdf" : (string) null, (string) null);
    if (!authenticFileTask.CanPerform)
      return;
    authenticFileTask.Perform();
    e.IsHandled = true;
  }
}
