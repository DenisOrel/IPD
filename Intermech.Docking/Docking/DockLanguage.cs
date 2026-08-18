
// Type: Intermech.Docking.DockLanguage
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;


namespace Intermech.Docking;

public class DockLanguage
{
  private static string _closeText = "Закрыть";
  private static string _autoHideText = "Автоматически убирать с экрана";
  private static string _scrollLeftText = "Прокрутка влево";
  private static string _scrollRightText = "Прокрутка вправо";
  private static string _documentListText = "Список окон";

  public static void ShowCachedAssemblyError(Assembly componentAssembly, Assembly designerAssembly)
  {
    int num = (int) MessageBox.Show("Error", "Visual Studio Error Detected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
  }

  [Localizable(true)]
  public static string AutoHideText
  {
    get => DockLanguage._autoHideText;
    set => DockLanguage._autoHideText = value;
  }

  [Localizable(true)]
  public static string CloseText
  {
    get => DockLanguage._closeText;
    set => DockLanguage._closeText = value;
  }

  [Localizable(true)]
  public static string DocumentListText
  {
    get => DockLanguage._documentListText;
    set => DockLanguage._documentListText = value;
  }

  [Localizable(true)]
  public static string ScrollLeftText
  {
    get => DockLanguage._scrollLeftText;
    set => DockLanguage._scrollLeftText = value;
  }

  [Localizable(true)]
  public static string ScrollRightText
  {
    get => DockLanguage._scrollRightText;
    set => DockLanguage._scrollRightText = value;
  }
}
