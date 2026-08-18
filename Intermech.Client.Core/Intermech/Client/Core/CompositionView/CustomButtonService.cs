
// Type: Intermech.Client.Core.CompositionView.CustomButtonService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.CompositionView;

/// <summary>Сервис для "кастом" кнопок</summary>
internal class CustomButtonService : ButtonsServiceBase
{
  /// <summary>Конструктор</summary>
  public CustomButtonService() => this.LoadFromBase();

  /// <summary>Загрузка информации сервиса</summary>
  public override void LoadFromBase() => this.LoadFromBase(false);

  /// <summary>Сохранение информации сервиса</summary>
  public override void SaveToBase() => this.SaveToBase(false);
}
