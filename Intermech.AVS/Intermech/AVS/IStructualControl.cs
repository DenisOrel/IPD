// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.IStructualControl
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;

#nullable disable
namespace Intermech.AVS;

/// <summary>
/// Функции, которые контрол, которому мы помогаем должен переадресовывать обекту типа AutoUpdateControlHelper
/// </summary>
public interface IStructualControl
{
  /// <summary>Блокирование обновления визуальных контролов</summary>
  void LockControls();

  /// <summary>Разблокирование обновления визуальных контролов</summary>
  void UnlockControls();

  /// <summary>Разблокирование обновления визуальных контролов</summary>
  void UnlockControls(bool notUpdate);

  /// <summary>
  /// Проверка, заблокировано обновление визуальных контролов
  /// </summary>
  bool IsControlsLocked();

  /// <summary>Обновление визуального состояния контролов</summary>
  /// <param name="recurce">Обновлять так же все дочерние контролы с "помошниками"</param>
  void UpdateControls(bool recurce);

  /// <summary> Признак того, что контролы в данный момент обновляются </summary>
  bool ControlsAreUpdating { get; }

  /// <summary>Доступно ли редактирование</summary>
  bool ReadOnly { get; set; }

  bool OverrideReadOnly { get; set; }

  /// <summary>
  /// Обновить значение параметра "Доступно ли редактирование"
  /// </summary>
  void RefreshReadOnly();

  /// <summary>
  /// Должен вызываться при каждой попытке редактирования.
  /// Проверяет доступно ли редактирование данных и, если требуется,
  /// запрашивает у пользователя разрешение на их редактирование
  /// (например, на взятие на изменение соотв. объекта)
  /// </summary>
  bool CheckCanEdit(ref bool wasUpdated);

  /// <summary>
  /// Признак того, что данные, связаные с контролом были изменены
  /// </summary>
  bool Changed { get; set; }

  /// <summary>
  /// Событие, вызываемое при изменении данных, связанных с контролом
  /// </summary>
  event EventHandler OnChangedEvent;

  /// <summary>Получить объект-помошник</summary>
  /// <returns>объект-помошник</returns>
  AutoUpdateControlHelper GetHelperObj();
}
