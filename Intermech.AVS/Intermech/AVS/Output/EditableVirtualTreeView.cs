// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Output.EditableVirtualTreeView
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Output;

/// <summary>
/// Класс-наследник VirtualTreeView, предоставляющий доп. сведения о состоянии дерева в режиме редактирования
/// </summary>
internal class EditableVirtualTreeView : Intermech.VirtualTreeView.VirtualTreeView
{
  private bool isEditModeOn;

  internal bool IsEditModeOn
  {
    get => this.isEditModeOn;
    set
    {
      this.isEditModeOn = value;
      this.IsEditCancelRequested = false;
      this.IsEditCommitRequested = false;
    }
  }

  internal bool IsEditCancelRequested { get; private set; }

  internal bool IsEditCommitRequested { get; private set; }

  protected override bool ProcessEditEscapeCmdKey(Keys modifiers)
  {
    if (this.IsEditModeOn && modifiers == Keys.None)
      this.IsEditCancelRequested = true;
    return base.ProcessEditEscapeCmdKey(modifiers);
  }

  protected override bool ProcessEditEnterCmdKey(Keys modifiers)
  {
    if (this.IsEditModeOn && modifiers == Keys.None)
      this.IsEditCommitRequested = true;
    return base.ProcessEditEnterCmdKey(modifiers);
  }
}
