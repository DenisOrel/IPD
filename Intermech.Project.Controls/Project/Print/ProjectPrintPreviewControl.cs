// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Print.ProjectPrintPreviewControl
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Controls;
using Intermech.Diagnostics;
using Intermech.Navigator.Interfaces;
using Intermech.Project.Controls;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Print;

public class ProjectPrintPreviewControl : 
  PreviewPrintControl,
  IContextAware,
  IClientProjectContext,
  IProjectViewContext
{
  private bool _selectable;
  [CanBeNull]
  private System.IServiceProvider _services;

  [EditorBrowsable(EditorBrowsableState.Never)]
  public override void ResetBackColor()
  {
  }

  protected override void DrawPageBorder([NotNull] Graphics g, Rectangle rect)
  {
    g.DrawRectangle(SystemPens.ControlDark, rect);
  }

  protected override void DrawPageBorder2([NotNull] Graphics g, Rectangle rect)
  {
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(false)]
  public bool Selectable
  {
    get => this._selectable;
    set
    {
      if (this._selectable == value)
        return;
      this._selectable = value;
      if (this._selectable)
      {
        if (!this.TabStop)
          this.TabStop = true;
        this.SetStyle(ControlStyles.Selectable, this._selectable);
      }
      this.Invalidate();
    }
  }

  protected override bool ShowFocusCues => this._selectable;

  protected override void OnPaint([NotNull] PaintEventArgs paintEventArgs)
  {
    base.OnPaint(paintEventArgs);
    if (!this._selectable || !this.Focused)
      return;
    ControlPaint.DrawFocusRectangle(paintEventArgs.Graphics, this.ClientRectangle);
  }

  protected override void OnEnter([NotNull] EventArgs e)
  {
    base.OnEnter(e);
    if (!this._selectable)
      return;
    this.Invalidate();
  }

  protected override void OnLeave([NotNull] EventArgs e)
  {
    base.OnLeave(e);
    if (!this._selectable)
      return;
    this.Invalidate();
  }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public System.IServiceProvider Services
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._services;
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._services = value;
      System.IServiceProvider services = this._services;
      this.ProjectView = services != null ? services.GetService<ProjectView>() : (ProjectView) null;
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [CanBeNull]
  public ProjectView ProjectView { get; private set; }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ClientProject Project => this.ProjectView?.Project;
}
