// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.UINotificationAction
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Описатель действия, связанного с <see cref="T:Intermech.Interfaces.Client.UINotification" />.
/// Реализация является immutable.
/// </summary>
[DataContract(Namespace = "")]
[Serializable]
public class UINotificationAction
{
  public const string OpenAction = "UI.Notifications.Open";
  public const string RecoverErrorAction = "UI.Notifications.RecoverError";
  public const string ShowErrorAction = "UI.Notifications.ShowError";

  /// <summary>
  /// Создает объект.
  /// Вместо прямого вызова конструктора следует использовать класс <see cref="T:Intermech.Interfaces.Client.UINotificationActionBuilder" />
  /// </summary>
  /// <param name="name">Имя действия</param>
  /// <param name="data">Данные действия (параметр может быть не задан)</param>
  /// <param name="anchorText">Текст для привязки к сообщению уведомления</param>
  internal UINotificationAction(string name, Uri data, string anchorText)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    if (anchorText == null)
      throw new ArgumentNullException(nameof (anchorText));
    this.Name = name;
    this.Data = data;
    this.AnchorText = anchorText;
  }

  /// <summary>Возвращает имя действия.</summary>
  [DataMember(IsRequired = true)]
  public string Name { get; private set; }

  /// <summary>
  /// Возвращает данные действия.
  /// Значение может быть не задано и равно null.
  /// </summary>
  [DataMember(IsRequired = false)]
  public Uri Data { get; private set; }

  /// <summary>
  /// Возвращает текст привязки действия к сообщению уведомления.
  /// Значение может быть не задано и равно пустой строке.
  /// </summary>
  [DataMember(IsRequired = false)]
  public string AnchorText { get; private set; }
}
