// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.IStructualControlSupport
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

#nullable disable
namespace Intermech.AVS;

/// <summary>
/// Функции, которые котрол, которому  мы помогаем должен рализовать сам
/// </summary>
public interface IStructualControlSupport
{
  /// <summary>
  /// Вызывается при необходимости обновления визуального состояния контролов
  /// </summary>
  void UpdateControls();

  /// <summary>Получить интерфейс с основной функциональностью</summary>
  IStructualControl GetStructualControlIntf();

  /// <summary>
  /// Узнать, мешает ли на данном уровне что-либо редактировать контролы
  /// </summary>
  bool IsReadOnly();

  /// <summary>Проверка, что объект-помошник создан</summary>
  void CheckHelperObjCreated();

  /// <summary>
  /// Вызывается, когда требуется проверка перед попыткой модификации данных
  /// Например, когда у пользователя необходимо запросить разрешение на взятие
  /// на редактирование некоторого объекта
  /// </summary>
  bool CheckCanEdit(ref bool wasUpdated);

  /// <summary>
  /// Событие, вызываемое при перезагрузке данных, связанных с контролом (например, при обновлении их из БД)
  /// </summary>
  event InitDataEventHandler OnInitDataEvent;

  /// <summary> Этот метод трубуется вызывать при изменении данных </summary>
  /// <param name="data"></param>
  void RaiseOnInitDataEvent(object data);
}
