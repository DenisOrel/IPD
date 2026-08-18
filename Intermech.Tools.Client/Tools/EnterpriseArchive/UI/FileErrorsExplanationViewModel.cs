// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.UI.FileErrorsExplanationViewModel
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive.UI;

internal sealed class FileErrorsExplanationViewModel : ExplanationViewModel
{
  private readonly ObservableCollection<FileError> fileList;

  public FileErrorsExplanationViewModel(string caption, string description)
    : base(caption, description)
  {
    this.fileList = new ObservableCollection<FileError>();
  }

  public FileErrorsExplanationViewModel() => this.fileList = new ObservableCollection<FileError>();

  public IList<FileError> FileList => (IList<FileError>) this.fileList;
}
