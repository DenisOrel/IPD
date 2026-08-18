// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.ImportSourcePresenter
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Mvp;
using System;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal sealed class ImportSourcePresenter : Presenter<IImportSourceView>
{
  private ImportSource selectedSource;

  public ImportSource SelectedSource => this.selectedSource;

  protected override void OnAttachView()
  {
    base.OnAttachView();
    this.View.OperationConfirmed += new EventHandler(this.OnSelectedSuccessfully);
  }

  protected override void OnDetachView()
  {
    base.OnDetachView();
    this.View.OperationConfirmed -= new EventHandler(this.OnSelectedSuccessfully);
  }

  private void OnSelectedSuccessfully(object sender, EventArgs e)
  {
    this.selectedSource = this.View.SelectedSource;
  }
}
