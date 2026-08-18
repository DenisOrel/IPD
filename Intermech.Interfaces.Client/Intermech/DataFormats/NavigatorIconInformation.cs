// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.NavigatorIconInformation
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Класс какой-то информации для определения значков узлам в "Навигаторе"
/// </summary>
public class NavigatorIconInformation : INavigatorIconInformation
{
  /// <summary>
  /// Какие-то данные, на основании которых может выполняться изменение стандартного значка
  /// для узла "Навигатора"
  /// </summary>
  protected object _data;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="data">Какие-то данные, на основании которых может выполняться изменение стандартного значка
  /// для узла "Навигатора"</param>
  public NavigatorIconInformation(object data) => this._data = data;

  /// <summary>
  /// Какие-то данные, на основании которых может выполняться изменение стандартного значка
  /// для узла "Навигатора"
  /// </summary>
  public object data => this._data;

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    return obj is NavigatorIconInformation navigatorIconInformation && this.data != null ? this.data.Equals(navigatorIconInformation.data) : base.Equals(obj);
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode()
  {
    return this.data != null ? this.data.GetHashCode() : base.GetHashCode();
  }
}
