
// Type: Intermech.Navigator.SelectionView.SelectionFormMode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.SelectionView;

/// <summary>Режимы инициализации формы</summary>
public enum SelectionFormMode
{
  /// <summary>Cамостоятельная форма</summary>
  IndependentForm,
  /// <summary>На форме-создателе новых объектов</summary>
  InObjectCreator,
  /// <summary>На вьюшке "Навигатора"</summary>
  InView,
  /// <summary>Форма для вложенных условий</summary>
  InnerConditionsForm,
}
