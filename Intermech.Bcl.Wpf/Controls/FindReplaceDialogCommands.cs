
// Type: Intermech.UI.Wpf.Controls.FindReplaceDialogCommands
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System.Windows.Input;


namespace Intermech.UI.Wpf.Controls;

/// <summary>Внутренние команды для FindReplaceDialog</summary>
internal static class FindReplaceDialogCommands
{
  public static readonly RoutedCommand FindNext = new RoutedCommand(nameof (FindNext), typeof (FindReplaceDialogCommands));
  public static readonly RoutedCommand Replace = new RoutedCommand(nameof (Replace), typeof (FindReplaceDialogCommands));
  public static readonly RoutedCommand ReplaceAll = new RoutedCommand(nameof (ReplaceAll), typeof (FindReplaceDialogCommands));
}
