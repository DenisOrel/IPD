// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.CompositionCopyingImages
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.Client.Properties;
using Intermech.UI.Wpf.WinformsInterop;
using System.Windows.Media.Imaging;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

internal static class CompositionCopyingImages
{
  private static BitmapSource warning48x48 = WpfBitmapSources.FromBitmap(Resources.IR_Warning);
  private static BitmapSource error48x48 = WpfBitmapSources.FromBitmap(Resources.IR_Error);
  private static BitmapSource completed48x48 = WpfBitmapSources.FromBitmap(Resources.IR_Completed);

  public static BitmapSource Warning => CompositionCopyingImages.warning48x48;

  public static BitmapSource Error => CompositionCopyingImages.error48x48;

  public static BitmapSource Completed => CompositionCopyingImages.completed48x48;
}
