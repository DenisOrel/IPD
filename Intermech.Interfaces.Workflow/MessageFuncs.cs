// Decompiled with JetBrains decompiler
// Type: Intermech.MessageFuncs
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces.Workflow;
using System.Windows.Forms;

#nullable disable
namespace Intermech;

public class MessageFuncs
{
  private static IWin32Window MainForm
  {
    get => (IWin32Window) Application.OpenForms[0] ?? (IWin32Window) null;
  }

  public static DialogResult SayError(string s, MessageBoxButtons buttons)
  {
    return MessageBox.Show(MessageFuncs.MainForm, s, (string) null, buttons, MessageBoxIcon.Hand);
  }

  public static DialogResult SayError(string s) => MessageFuncs.SayError(s, MessageBoxButtons.OK);

  public static DialogResult SayOK(string s)
  {
    return MessageBox.Show(MessageFuncs.MainForm, s, "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  public static DialogResult Ask(string s) => MessageFuncs.Ask(s, MessageBoxButtons.YesNo);

  public static DialogResult Ask(string s, MessageBoxButtons buttons)
  {
    return MessageBox.Show((IWin32Window) null, s, LocalizationHolder.rm.GetString("Confirmation"), buttons, MessageBoxIcon.Question);
  }

  public static DialogResult Confirm(string s)
  {
    return MessageBox.Show((IWin32Window) null, s, LocalizationHolder.rm.GetString("Confirmation"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
  }
}
