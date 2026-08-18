// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.MsoSavedState
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.IO;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.MSOffice;

internal sealed class MsoSavedState
{
  private PathCollection openDocuments;
  private string activeDocument;

  public MsoSavedState() => this.openDocuments = new PathCollection();

  public ICollection<string> OpenDocuments => (ICollection<string>) this.openDocuments;

  public string ActiveDocument
  {
    get => this.activeDocument;
    set => this.activeDocument = value;
  }
}
