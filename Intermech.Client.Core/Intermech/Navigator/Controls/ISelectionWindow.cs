
// Type: Intermech.Navigator.Controls.ISelectionWindow
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Интерфейс для Коли Кожемякина - получать доступ к кнопке "ОК".
/// Вернее не для Коли Кожемякина, а для нашего общего дела!
/// </summary>
public interface ISelectionWindow
{
  /// <summary>Контейнер сервисов окна</summary>
  System.IServiceProvider Services { get; }

  /// <summary>Дерево "Навигатора"</summary>
  NavigatorTreeView Tree { get; }

  /// <summary>Кнопка "ОК"</summary>
  Button OkButton { get; }
}
