// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.Images
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project.Controls;

/// <summary>Статические методы и кэш изображений для IPS.Project.Controls</summary>
public abstract class Images : Intermech.Client.Images
{
  public static int ResourcesImageIndex = -1;
  public static int ResultsImageIndex = -1;
  public static int ProjectImageIndex = -1;
  public static int TaskImageIndex = -1;
  public static int SyncImageIndex = -1;
  [NotNull]
  private static readonly InitOnceGuardian _initOnce = new InitOnceGuardian();
  [CanBeNull]
  private static Image _projectImage;
  [CanBeNull]
  private static Image _portraitImage;
  [CanBeNull]
  private static Image _landscapeImage;
  [CanBeNull]
  private static Image _exclamationImage;
  [CanBeNull]
  private static Image _syncPendingImage;
  [CanBeNull]
  private static Bitmap _waitingBitmap;
  [CanBeNull]
  private static Bitmap _checkBitmap;
  [CanBeNull]
  private static Bitmap _playBitmap;
  [CanBeNull]
  private static Bitmap _bulletImage;
  [CanBeNull]
  private static Bitmap _constraintImage;
  [CanBeNull]
  private static Bitmap _infoImage;
  [CanBeNull]
  private static Bitmap _notesImage;

  /// <summary>Инициализация</summary>
  internal static void Init([NotNull] IServiceProvider serviceProvider, [CanBeNull] IUserSession session = null)
  {
    Images._initOnce.Invoke((Action) (() =>
    {
      Images.ResourcesImageIndex = Images.LoadToNamedList("resources.ico", "prjResources");
      Images.ResultsImageIndex = Images.LoadToNamedList("results.bmp", "prjResults");
      Images.SyncImageIndex = Images.LoadToNamedList("sync.bmp", "prjSync");
      Images.ProjectImageIndex = Intermech.Client.Images.LoadToNamedList(ObjectTypes.Project.GetIcon(), "ImProject");
      Images.TaskImageIndex = Intermech.Client.Images.LoadToNamedList(ObjectTypes.Task.GetIcon(), "ImProjectTask");
    }));
  }

  [ContractAnnotation("throwExceptionIfNotFound:true => NotNull; throwExceptionIfNotFound:false => CanBeNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected static Image GetFromResources([NotNull, NotWhitespace] string imageName, bool throwExceptionIfNotFound = true)
  {
    return Intermech.Client.Images.GetFromResources<Image>(Library.Assembly, imageName, throwExceptionIfNotFound);
  }

  [ContractAnnotation("throwExceptionIfNotFound:true => NotNull; throwExceptionIfNotFound:false => CanBeNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected static TImage GetFromResources<TImage>([NotNull, NotWhitespace] string imageName, bool throwExceptionIfNotFound = true) where TImage : Image
  {
    return Intermech.Client.Images.GetFromResources<TImage>(Library.Assembly, imageName, throwExceptionIfNotFound);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void LoadToNamedList([NotNull, ItemNotEmpty, ItemNotWhitespace] params (string id, string name)[] nameAndIds)
  {
    Intermech.Client.Images.LoadToNamedList(Library.Assembly, nameAndIds);
  }

  public static int LoadToNamedList([NotNull, NotWhitespace] string id, [NotNull, NotWhitespace] string name, bool throwExceptionIfNotFound = true)
  {
    return Intermech.Client.Images.LoadToNamedList(Library.Assembly, id, name, throwExceptionIfNotFound);
  }

  [NotNull]
  public static Image ProjectImage
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Images._projectImage ?? (Images._projectImage = Intermech.Client.Services.NamedList.ImageList.Images[Images.ProjectImageIndex]);
    }
  }

  [NotNull]
  public static Image PortraitImage
  {
    get
    {
      return Images._portraitImage ?? (Images._portraitImage = Images.GetFromResources(".img.PortraitCombo.png"));
    }
  }

  [NotNull]
  public static Image LandscapeImage
  {
    get
    {
      return Images._landscapeImage ?? (Images._landscapeImage = Images.GetFromResources(".img.LandscapeCombo.png"));
    }
  }

  [NotNull]
  public static Image ExclamationImage
  {
    get
    {
      return Images._exclamationImage ?? (Images._exclamationImage = Images.GetFromResources(".img.exclamation.bmp"));
    }
  }

  [NotNull]
  public static Image SyncPendingImage
  {
    get
    {
      return Images._syncPendingImage ?? (Images._syncPendingImage = Images.GetFromResources(".img.syncpending.bmp"));
    }
  }

  [NotNull]
  public static Bitmap WaitingBitmap
  {
    get
    {
      return Images._waitingBitmap ?? (Images._waitingBitmap = Images.GetFromResources<Bitmap>(".img.hourglass.bmp"));
    }
  }

  [NotNull]
  public static Bitmap CheckBitmap
  {
    get
    {
      return Images._checkBitmap ?? (Images._checkBitmap = Images.GetFromResources<Bitmap>(".img.check.bmp"));
    }
  }

  [NotNull]
  public static Bitmap PlayBitmap
  {
    get
    {
      return Images._playBitmap ?? (Images._playBitmap = Images.GetFromResources<Bitmap>(".img.play.bmp"));
    }
  }

  [NotNull]
  public static Bitmap BulletImage
  {
    get
    {
      return Images._bulletImage ?? (Images._bulletImage = Images.GetFromResources<Bitmap>(".img.bullet.png"));
    }
  }

  [NotNull]
  public static Bitmap ConstraintImage
  {
    get
    {
      return Images._constraintImage ?? (Images._constraintImage = Images.GetFromResources<Bitmap>(".img.constraint.bmp"));
    }
  }

  [NotNull]
  public static Bitmap InfoImage
  {
    get
    {
      return Images._infoImage ?? (Images._infoImage = Images.GetFromResources<Bitmap>(".img.info.png"));
    }
  }

  [NotNull]
  public static Bitmap NotesImage
  {
    get
    {
      return Images._notesImage ?? (Images._notesImage = Images.GetFromResources<Bitmap>(".img.notes.bmp"));
    }
  }

  [CanBeNull]
  public static Image GetStatusImage(TaskStatus status, bool isGrid = false)
  {
    if (status == TaskStatus.Completed)
      return (Image) Images.CheckBitmap;
    if (status == TaskStatus.Executed && !isGrid)
      return (Image) Images.PlayBitmap;
    return status != TaskStatus.Waiting ? (Image) null : (Image) Images.WaitingBitmap;
  }
}
