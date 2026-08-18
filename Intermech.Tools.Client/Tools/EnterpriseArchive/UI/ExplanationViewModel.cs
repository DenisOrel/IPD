// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.UI.ExplanationViewModel
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive.UI;

internal class ExplanationViewModel : ViewModel
{
  private string caption;
  private string explanation;
  private string fileListName;

  public ExplanationViewModel(string caption, string explanation)
  {
    if (string.IsNullOrEmpty(caption))
      throw new ArgumentException();
    if (string.IsNullOrEmpty(explanation))
      throw new ArgumentException();
    this.caption = caption;
    this.explanation = explanation;
    this.fileListName = string.Empty;
  }

  public ExplanationViewModel()
  {
    this.caption = string.Empty;
    this.explanation = string.Empty;
    this.fileListName = string.Empty;
  }

  public string Caption
  {
    get => this.caption;
    set
    {
      if (string.Compare(this.caption, value) == 0)
        return;
      this.caption = value;
      this.NotifyPropertyChanged(nameof (Caption));
    }
  }

  public string Explanation
  {
    get => this.explanation;
    set
    {
      if (string.Compare(this.explanation, value) == 0)
        return;
      this.explanation = value;
      this.NotifyPropertyChanged(nameof (Explanation));
    }
  }

  public string FileListName
  {
    get => this.fileListName;
    set
    {
      if (string.Compare(this.fileListName, value) == 0)
        return;
      this.fileListName = value;
      this.NotifyPropertyChanged(nameof (FileListName));
    }
  }
}
