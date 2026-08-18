
// Type: Intermech.Bars.BarLanguage
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Bars
{
    public class BarLanguage
    {
      private static string _addRemoveButtons = LocalizationHolder.rm.GetString("Bars_1");
      private static string _toolbarOptions = LocalizationHolder.rm.GetString("Bars_2");
      private static string _minimizeWindow = LocalizationHolder.rm.GetString("Bars_3");
      private static string _restoreWindow = LocalizationHolder.rm.GetString("Bars_4");
      private static string _closeWindow = LocalizationHolder.rm.GetString("Bars_5");
      private static string _restore = LocalizationHolder.rm.GetString("Bars_6");
      private static string _move = LocalizationHolder.rm.GetString("Bars_7");
      private static string _size = LocalizationHolder.rm.GetString("Bars_8");
      private static string _minimize = LocalizationHolder.rm.GetString("Bars_9");
      private static string _maximize = LocalizationHolder.rm.GetString("Bars_10");
      private static string _close = LocalizationHolder.rm.GetString("Bars_11");

      private BarLanguage()
      {
      }

      [Localizable(true)]
      public static string AddRemoveButtonsText
      {
        get => BarLanguage._addRemoveButtons;
        set => BarLanguage._addRemoveButtons = value;
      }

      [Localizable(true)]
      public static string CloseMenuText
      {
        get => BarLanguage._close;
        set => BarLanguage._close = value;
      }

      [Localizable(true)]
      public static string CloseWindowText
      {
        get => BarLanguage._closeWindow;
        set => BarLanguage._closeWindow = value;
      }

      [Localizable(true)]
      public static string MaximizeMenuText
      {
        get => BarLanguage._maximize;
        set => BarLanguage._maximize = value;
      }

      [Localizable(true)]
      public static string MinimizeMenuText
      {
        get => BarLanguage._minimize;
        set => BarLanguage._minimize = value;
      }

      [Localizable(true)]
      public static string MinimizeWindowText
      {
        get => BarLanguage._minimizeWindow;
        set => BarLanguage._minimizeWindow = value;
      }

      [Localizable(true)]
      public static string MoveMenuText
      {
        get => BarLanguage._move;
        set => BarLanguage._move = value;
      }

      [Localizable(true)]
      public static string RestoreMenuText
      {
        get => BarLanguage._restore;
        set => BarLanguage._restore = value;
      }

      [Localizable(true)]
      public static string RestoreWindowText
      {
        get => BarLanguage._restoreWindow;
        set => BarLanguage._restoreWindow = value;
      }

      [Localizable(true)]
      public static string SizeMenuText
      {
        get => BarLanguage._size;
        set => BarLanguage._size = value;
      }

      [Localizable(true)]
      public static string ToolbarOptionsText
      {
        get => BarLanguage._toolbarOptions;
        set => BarLanguage._toolbarOptions = value;
      }
    }
}
