
// Type: Intermech.Client.Core.IFindController
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Configuration;
using System.ComponentModel;


namespace Intermech.Client.Core;

/// <summary> Интерфейс, который должен поддерживать формы поиска чего-либо </summary>
public interface IFindController
{
  /// <summary>
  /// Получение ссылки на объект, который реализует всю функциональность по настройке поиска
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  object InterfaceObject { get; }

  /// <summary> Вызывается когда форма настройки поиска "присоединяется" к окну, в содержимом которого должен производиться поиск </summary>
  /// <param name="iWindowWithFind"> Окно, в содержимом которого должен производиться поиск </param>
  void AttachToWindow(IWindowWithFind iWindowWithFind);

  /// <summary> Показать пользователю форму настройки поиска </summary>
  void Show();

  /// <summary> Скрыть форму настройки поиска </summary>
  void Hide();

  /// <summary> Сохранить выбранные пользователем настройки поиска для последующего востановления </summary>
  /// <param name="iConfiguration"> Интерфейс позволяющий сохранять / читать конфигурацию </param>
  void SaveConfiguration(IConfiguration iConfiguration);

  /// <summary> Востановление настроек поиска из ранее сохнанённых </summary>
  /// <param name="iConfiguration"> Интерфейс позволяющий сохранять / читать конфигурацию </param>
  void LoadConfiguration(IConfiguration iConfiguration);

  /// <summary> Признак того, что форма настройки поиска видна пользователю </summary>
  bool IsVisible { get; }
}
