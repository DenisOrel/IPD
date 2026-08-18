
// Type: Intermech.Client.Core.CompositionView.CommonButtonService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.CompositionView;

/// <summary>Сервис для "общих" кнопок</summary>
internal class CommonButtonService : ButtonsServiceBase
{
  /// <summary>Конструктор</summary>
  public CommonButtonService() => this.LoadFromBase();

  /// <summary>Загрузка информации сервиса</summary>
  public override void LoadFromBase() => this.LoadFromBase(true);

  /// <summary>Сохранение информации сервиса</summary>
  public override void SaveToBase() => this.SaveToBase(true);
}
