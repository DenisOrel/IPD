// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.Holder
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Interfaces.Plugins;
using System;
using System.Drawing;
using System.Reflection;

#nullable disable
namespace Intermech.Project.Controls;

[Obsolete("Class will be removed in future releases!")]
public class Holder : Intermech.Workflow.Design.Holder
{
  [Obsolete("Use Images.ResourcesImageIndex")]
  public static int ResourcesImageIndex => Images.ResourcesImageIndex;

  [Obsolete("Use Images.ResultsImageIndex")]
  public static int ResultsImageIndex => Images.ResultsImageIndex;

  [Obsolete("Use Images.ProjectImageIndex")]
  public static int ProjectImageIndex => Images.ProjectImageIndex;

  [Obsolete("Use Images.TaskImageIndex")]
  public static int TaskImageIndex => Images.TaskImageIndex;

  [Obsolete("Use Images.SyncImageIndex")]
  public static int SyncImageIndex => Images.SyncImageIndex;

  [NotNull]
  [Obsolete("Use Library.Assembly")]
  protected new static Assembly MyAssembly => Library.Assembly;

  [NotNull]
  [Obsolete("Use Images.CheckBitmap")]
  public static Bitmap CheckBitmap => Images.CheckBitmap;

  [NotNull]
  [Obsolete("Use Images.PlayBitmap")]
  public static Bitmap PlayBitmap => Images.PlayBitmap;

  [NotNull]
  [Obsolete("Use Images.BulletImage")]
  public static Bitmap BulletImage => Images.BulletImage;

  [NotNull]
  [Obsolete("Use Images.ConstraintImage")]
  public static Bitmap ConstraintImage => Images.ConstraintImage;

  [NotNull]
  [Obsolete("Use Images.InfoImage")]
  public static Bitmap InfoImage => Images.InfoImage;

  [NotNull]
  [Obsolete("Use Images.NotesImage")]
  public static Bitmap NotesImage => Images.NotesImage;

  [Obsolete("Use IMProject.DefaultTaskColor")]
  public static Color DefaultTaskColor => IMProject.DefaultTaskColor;

  [NotNull]
  [Obsolete("Use IMProject.DefaultTaskBrush")]
  public static Brush DefaultTaskBrush => IMProject.DefaultTaskBrush;

  [NotNull]
  [Obsolete("Use Images.ProjectImage")]
  public static Image ProjectImage => Images.ProjectImage;

  [NotNull]
  [Obsolete("Use Images.PortraitImage")]
  public static Image PortraitImage => Images.PortraitImage;

  [NotNull]
  [Obsolete("Use Images.LandscapeImage")]
  public static Image LandscapeImage => Images.LandscapeImage;

  [NotNull]
  [Obsolete("Use Images.ExclamationImage")]
  public static Image ExclamationImage => Images.ExclamationImage;

  [NotNull]
  [Obsolete("Use Images.SyncPendingImage")]
  public static Image SyncPendingImage => Images.SyncPendingImage;

  [NotNull]
  [Obsolete("Use Images.WaitingBitmap")]
  public static Bitmap WaitingBitmap => Images.WaitingBitmap;

  [CanBeNull]
  [Obsolete("Use Images.GetStatusImage(TaskStatus, bool)")]
  public static Image GetStatusImage(TaskStatus status, bool isGrid = false)
  {
    return Images.GetStatusImage(status, isGrid);
  }

  [Obsolete("Call IMProject.Controls.Init(IPackage, IServiceProvider)")]
  public new static void Init([NotNull] IPackage plugin, [NotNull] IServiceProvider serviceProvider)
  {
    Library.Init(plugin, serviceProvider);
  }
}
