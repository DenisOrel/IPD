// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.UI.FileListExplainationPresenter
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Mvp;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive.UI;

internal sealed class FileListExplainationPresenter : Presenter<IFileListExplainationView>
{
  private FileListExplanationViewModel viewModel;

  public FileListExplainationPresenter()
  {
  }

  public FileListExplainationPresenter(FileListExplanationViewModel viewModel)
  {
    this.ViewModel = viewModel;
  }

  public FileListExplanationViewModel ViewModel
  {
    get => this.viewModel;
    set
    {
      this.CheckAllowPropertyChange();
      this.viewModel = value;
    }
  }

  protected override void OnAttachView()
  {
    base.OnAttachView();
    this.View.ViewModel = this.viewModel;
  }

  protected override void OnDetachView()
  {
    base.OnDetachView();
    this.View.ViewModel = (FileListExplanationViewModel) null;
  }
}
