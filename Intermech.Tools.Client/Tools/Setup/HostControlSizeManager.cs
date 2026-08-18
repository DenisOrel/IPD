// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Setup.HostControlSizeManager
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Setup;

internal sealed class HostControlSizeManager
{
  private Control hostControl;
  private Size savedHostMinimumSize;
  private bool isHostAdjusted;
  private Control contentControl;

  public Control ContentControl
  {
    [DebuggerStepThrough] get => this.contentControl;
    set
    {
      if (this.contentControl == value)
        return;
      if (value != null)
      {
        if (value.Parent == null)
          throw new ArgumentException("Визуальный элемент должен иметь родителя.", "СontentControl");
        if (value.Dock == DockStyle.None)
          throw new ArgumentException("Визуальный элемент должен иметь значение свойства Dock != None.", "СontentControl");
      }
      this.ChangeContentControl(value);
    }
  }

  public void UpdateSizeAndConstraints()
  {
    if (this.ContentControl == null)
      return;
    if (this.IsHostAdjusted)
      this.RestoreHostSizeAndConstraints();
    this.AdjustHostSizeAndConstraints();
  }

  private void ChangeContentControl(Control newValue)
  {
    if (this.IsHostAdjusted)
      this.RestoreHostSizeAndConstraints();
    this.contentControl = newValue;
    if (this.contentControl == null)
      return;
    this.LazyInitializeHostControl();
    this.AdjustHostSizeAndConstraints();
  }

  private void LazyInitializeHostControl()
  {
    if (this.hostControl != null)
      return;
    this.hostControl = this.ContentControl.TopLevelControl;
    this.savedHostMinimumSize = this.hostControl.MinimumSize;
  }

  private Control HostControl
  {
    [DebuggerStepThrough] get => this.hostControl;
  }

  private bool IsHostAdjusted
  {
    [DebuggerStepThrough] get => this.isHostAdjusted;
  }

  private void AdjustHostSizeAndConstraints()
  {
    try
    {
      this.AdjustHostSizeAndConstraintsCore();
    }
    catch
    {
      this.RestoreHostSizeAndConstraintsCore();
      throw;
    }
    this.isHostAdjusted = true;
  }

  private void AdjustHostSizeAndConstraintsCore()
  {
    this.AdjustHostSize();
    this.AdjustHostMinimumSize();
  }

  private void AdjustHostSize()
  {
    Size hostSizeDelta = this.CalculateHostSizeDelta();
    if (hostSizeDelta.IsEmpty)
      return;
    AnchorStyles anchor = this.ContentControl.Anchor;
    try
    {
      AnchorStyles anchorStyles = anchor & ~(AnchorStyles.Bottom | AnchorStyles.Right);
      if (anchorStyles != anchor)
        this.ContentControl.Anchor = anchorStyles;
      this.HostControl.ClientSize += hostSizeDelta;
    }
    finally
    {
      this.ContentControl.Anchor = anchor;
    }
  }

  private Size CalculateHostSizeDelta()
  {
    Size size = this.ContentControl.Size;
    Control parent = this.ContentControl.Parent;
    Size clientSize = parent.ClientSize;
    int width = Math.Max(0, this.ContentControl.Left + size.Width + parent.Padding.Right - clientSize.Width);
    int height = Math.Max(0, this.ContentControl.Top + size.Height + this.ContentControl.Padding.Bottom - clientSize.Height);
    return width == 0 && height == 0 ? Size.Empty : new Size(width, height);
  }

  private void AdjustHostMinimumSize()
  {
    Size minimumSize = this.ContentControl.MinimumSize;
    Size size = this.ContentControl.Size;
    int val1_1 = 0;
    if (minimumSize.Width != 0)
      val1_1 = this.HostControl.Width - Math.Max(size.Width - minimumSize.Width, 0);
    int val1_2 = 0;
    if (minimumSize.Height != 0)
      val1_2 = this.HostControl.Height - Math.Max(size.Height - minimumSize.Height, 0);
    this.HostControl.MinimumSize = new Size(Math.Max(val1_1, this.HostControl.MinimumSize.Width), Math.Max(val1_2, this.HostControl.MinimumSize.Height));
  }

  private void RestoreHostSizeAndConstraints()
  {
    try
    {
      this.RestoreHostSizeAndConstraintsCore();
    }
    finally
    {
      this.isHostAdjusted = false;
    }
  }

  private void RestoreHostSizeAndConstraintsCore()
  {
    if (!(this.HostControl.MinimumSize != this.savedHostMinimumSize))
      return;
    this.HostControl.MinimumSize = this.savedHostMinimumSize;
  }
}
