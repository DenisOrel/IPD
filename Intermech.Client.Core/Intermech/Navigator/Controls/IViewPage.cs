
// Type: Intermech.Navigator.Controls.IViewPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Views;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Описывает закладку навигатора, отображаемую в менеджере.
/// </summary>
public interface IViewPage
{
  /// <summary>Возвращает имя закладки.</summary>
  string Name { get; }

  /// <summary>Возвращает элемент управления, реализующий закладку.</summary>
  Control Control { get; }

  /// <summary>
  /// Возвращает интерфейс, представляющий для навигатора наибольший интерес.
  /// </summary>
  IView View { get; }

  ViewDescription ViewDescription { get; }

  string HelpID { get; }

  string HelpPath { get; }

  ViewInfo Info { get; }
}
