
// Type: Intermech.Calendars.Editor.MaskedTextBoxExtensions
// Assembly: Intermech.Calendars.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0D5478F2-D4B6-4EDD-A444-F5E197647782
:\IPS\Client\Intermech.Calendars.Editor.dll

using Intermech.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace Intermech.Calendars.Editor;

internal static class MaskedTextBoxExtensions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Clear([NotNull] this MaskedTextBox edit)
  {
    if (!(edit.Text != string.Empty))
      return;
    edit.Text = string.Empty;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Disable([NotNull] this MaskedTextBox edit)
  {
    if (edit.ReadOnly)
      return;
    if (edit.Text != string.Empty)
      edit.Text = string.Empty;
    edit.ReadOnly = true;
    edit.BackColor = SystemColors.Control;
    edit.Cursor = Cursors.Arrow;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Enable([NotNull] this MaskedTextBox edit)
  {
    if (!edit.ReadOnly)
      return;
    edit.ReadOnly = false;
    edit.BackColor = SystemColors.Window;
    edit.Cursor = Cursors.IBeam;
  }
}
