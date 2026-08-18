// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Client.ProgressNotifier
// Assembly: Intermech.Project.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D968BDD9-29F0-4E24-8F57-6E851EE47258
// Assembly location: D:\IPS\Client\Intermech.Project.Client.dll

using Intermech.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Client;

internal class ProgressNotifier : IProgressNotifier
{
  [NotNull]
  private static readonly object _syncObj = new object();
  [CanBeNull]
  private static ProgressForm _form;
  [CanBeNull]
  private static ProgressNotifier _notifier;
  [CanBeNull]
  private static List<string> _msgStack;
  private static int _startCounter;

  [NotNull]
  public static ProgressNotifier Notifier
  {
    [DebuggerStepThrough] get
    {
      if (ProgressNotifier._notifier == null)
      {
        lock (ProgressNotifier._syncObj)
        {
          if (ProgressNotifier._notifier == null)
          {
            ProgressNotifier progressNotifier = new ProgressNotifier();
            Thread.MemoryBarrier();
            Interlocked.Exchange<ProgressNotifier>(ref ProgressNotifier._notifier, progressNotifier);
          }
        }
      }
      return ProgressNotifier._notifier;
    }
  }

  private static void UpdateText()
  {
    lock (ProgressNotifier._syncObj)
    {
      string empty = string.Empty;
      int count = ProgressNotifier._msgStack.Count;
      for (int index = 0; index < count; ++index)
      {
        string msg = ProgressNotifier._msgStack[index];
        if (msg != string.Empty)
        {
          if (empty != string.Empty)
            empty += "/";
          empty += msg;
        }
      }
      ProgressNotifier._form.CaptionLabel.Text = empty;
    }
  }

  private static void EnableWindows(bool enable)
  {
    foreach (Form openForm in (ReadOnlyCollectionBase) Application.OpenForms)
    {
      if (openForm != null)
      {
        try
        {
          openForm.Enabled = enable;
        }
        catch
        {
        }
      }
    }
    Cursor.Current = enable ? Cursors.Default : Cursors.WaitCursor;
  }

  public void Start(int maximum, string msg)
  {
    lock (ProgressNotifier._syncObj)
    {
      int num = 0;
      ++ProgressNotifier._startCounter;
      if (ProgressNotifier._form == null)
      {
        ProgressNotifier.EnableWindows(false);
        ProgressNotifier._form = new ProgressForm();
        ProgressNotifier._form.FormClosed += new FormClosedEventHandler(ProgressNotifier.ProgressForm_FormClosed);
        ProgressNotifier._form.FormClosing += new FormClosingEventHandler(ProgressNotifier.ProgressForm_FormClosing);
        ProgressNotifier._msgStack = new List<string>();
      }
      else
        num = ProgressNotifier._form.ProgressBar.Maximum;
      if (maximum != 0)
        ProgressNotifier._form.ProgressBar.Maximum = maximum + num;
      ProgressNotifier._msgStack.Add(msg);
      ProgressNotifier.UpdateText();
      if (!ProgressNotifier._form.Visible)
        ProgressNotifier._form.Show();
    }
    Application.DoEvents();
  }

  private static void ProgressForm_FormClosing([CanBeNull] object sender, [NotNull] FormClosingEventArgs e)
  {
    if (e.Cancel)
      return;
    ProgressNotifier.EnableWindows(true);
  }

  public void Inc()
  {
    if (ProgressNotifier._form == null)
      return;
    ProgressNotifier._form.ProgressBar.PerformStep();
  }

  public bool Stop()
  {
    lock (ProgressNotifier._syncObj)
    {
      int count = ProgressNotifier._msgStack.Count;
      if (count > 0)
        ProgressNotifier._msgStack.RemoveAt(count - 1);
      ProgressNotifier.UpdateText();
      --ProgressNotifier._startCounter;
      if (ProgressNotifier._form == null || ProgressNotifier._startCounter != 0)
        return false;
      if (ProgressNotifier._form.ProgressBar.Value != ProgressNotifier._form.ProgressBar.Maximum)
        ProgressNotifier._form.ProgressBar.Value = ProgressNotifier._form.ProgressBar.Maximum;
      ProgressNotifier._form.Close();
      return true;
    }
  }

  [NotNull]
  public string Caption
  {
    get
    {
      return ProgressNotifier._form == null ? string.Empty : ProgressNotifier._form.Text ?? string.Empty;
    }
    set
    {
      if (ProgressNotifier._form == null)
        return;
      ProgressNotifier._form.Text = value;
    }
  }

  private static void ProgressForm_FormClosed([CanBeNull] object sender, [NotNull] FormClosedEventArgs e)
  {
    lock (ProgressNotifier._syncObj)
      Interlocked.Exchange<ProgressForm>(ref ProgressNotifier._form, (ProgressForm) null)?.Dispose();
  }
}
