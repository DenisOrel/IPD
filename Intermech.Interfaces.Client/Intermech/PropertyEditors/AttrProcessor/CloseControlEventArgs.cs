// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.AttrProcessor.CloseControlEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.PropertyEditors.AttrProcessor;

public class CloseControlEventArgs : CancelEventArgs
{
  private DialogResult dialogResult;

  /// <summary>результат закрытия формы</summary>
  public DialogResult DialogResult
  {
    get => this.dialogResult;
    set => this.dialogResult = value;
  }

  public CloseControlEventArgs()
  {
  }

  public CloseControlEventArgs(bool cancel)
    : base(cancel)
  {
  }

  public CloseControlEventArgs(DialogResult dialogResult) => this.dialogResult = dialogResult;

  public CloseControlEventArgs(bool cancel, DialogResult dialogResult)
    : base(cancel)
  {
    this.dialogResult = dialogResult;
  }
}
