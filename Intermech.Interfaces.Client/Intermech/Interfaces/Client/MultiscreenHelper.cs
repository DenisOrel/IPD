// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.MultiscreenHelper
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Вспомогательный статический класс, помогающий при работе на системе с несколькими рабочими столами
/// </summary>
public static class MultiscreenHelper
{
  /// <summary>
  /// Ссылка на интерфейс, позволяющий обращаться к главной форме
  /// (есть вероятность получить значение null)
  /// </summary>
  private static IMainFormUpdate _mainForm;

  /// <summary>
  /// Ссылка на интерфейс, позволяющий обращаться к главной форме
  /// (есть вероятность получить значение null)
  /// </summary>
  public static IMainFormUpdate MainForm
  {
    get
    {
      if (MultiscreenHelper._mainForm != null)
        return MultiscreenHelper._mainForm;
      MultiscreenHelper._mainForm = ServicesManager.GetService(typeof (IMainFormUpdate)) as IMainFormUpdate;
      return MultiscreenHelper._mainForm;
    }
  }

  /// <summary>Возвращает экран главной формы или null</summary>
  public static Screen MainFormScreen
  {
    get
    {
      return MultiscreenHelper.MainForm == null ? (Screen) null : MultiscreenHelper.MainForm.MainFormScreen;
    }
  }

  /// <summary>
  /// Возвращает основную рабочую область, в которой размещается главное окно приложения.
  /// Если недоступно главное окно, свойство вернёт рабочую область основного рабочего стола
  /// </summary>
  public static Rectangle PrimaryWorkingArea
  {
    get
    {
      return MultiscreenHelper.MainForm == null ? Screen.PrimaryScreen.WorkingArea : MultiscreenHelper.MainForm.PrimaryWorkingArea;
    }
  }
}
