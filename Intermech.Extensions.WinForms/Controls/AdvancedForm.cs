// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.AdvancedForm
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Controls;

public class AdvancedForm : 
  Form,
  IContainerControl,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IComponent,
  IDisposable
{
  private readonly CancellationToken _ownerCancellationToken = CancellationToken.None;
  [CanBeNull]
  protected readonly SynchronizationContext FormSynchronizationContext;
  private bool _closed;

  [CanBeNull]
  protected internal CancellationTokenSource FormCancellation { get; private set; }

  public CancellationToken FormCancellationToken
  {
    get
    {
      CancellationTokenSource formCancellation = this.FormCancellation;
      return formCancellation == null ? this._ownerCancellationToken : formCancellation.Token;
    }
  }

  public AdvancedForm(CancellationToken cancellationToken)
  {
    this.FormSynchronizationContext = SynchronizationContext.Current;
    this._ownerCancellationToken = cancellationToken;
    CancellationTokenSource cancellationTokenSource;
    if (!(cancellationToken != CancellationToken.None))
      cancellationTokenSource = new CancellationTokenSource();
    else
      cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
    this.FormCancellation = cancellationTokenSource;
    if (!(this._ownerCancellationToken != CancellationToken.None))
      return;
    this.FormCancellation.Token.Register(new Action(this.FormCancellationFired), true);
  }

  private void FormCancellationFired()
  {
    if (!this.Visible || this._closed)
      return;
    this.Close();
  }

  public AdvancedForm()
  {
    this.FormSynchronizationContext = SynchronizationContext.Current;
    this.FormCancellation = new CancellationTokenSource();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      CancellationTokenSource formCancellation = this.FormCancellation;
      this.FormCancellation = (CancellationTokenSource) null;
      formCancellation?.Dispose();
    }
    base.Dispose(disposing);
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected bool InDesignMode
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime;
    }
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected void CheckNotInDesignMode() => Intermech.Diagnostics.Check.Assert(this.InDesignMode, "Only in design-mode");

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected bool IsShown { get; private set; }

  protected override void OnShown([NotNull] EventArgs e)
  {
    base.OnShown(e);
    this.IsShown = true;
  }

  public void CenterOnWindow([CanBeNull] IWin32Window window)
  {
    if (window == null)
      return;
    Rectangle windowRect = window.GetWindowRect();
    this.StartPosition = FormStartPosition.Manual;
    this.Location = new Point(windowRect.X + (windowRect.Width - this.Width) / 2, windowRect.Y + (windowRect.Height - this.Height) / 2);
  }

  public void CenterOnParentForm([CanBeNull] Form parentForm)
  {
    if (parentForm != null)
    {
      this.StartPosition = FormStartPosition.Manual;
      this.Location = new Point(parentForm.Location.X + (parentForm.Width - this.Width) / 2, parentForm.Location.Y + (parentForm.Height - this.Height) / 2);
    }
    else
      this.StartPosition = FormStartPosition.CenterScreen;
  }

  protected override void OnClosed([NotNull] EventArgs e)
  {
    this.FormCancellation?.Cancel();
    base.OnClosed(e);
  }

  public bool HideOnClose { get; set; }

  protected override void OnClosing(CancelEventArgs e)
  {
    base.OnClosing(e);
    if (e.Cancel)
      return;
    this._closed = true;
    if (this.HideOnClose)
    {
      this.Hide();
      e.Cancel = true;
    }
    this.FormCancellation?.Cancel();
  }
}
