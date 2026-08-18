// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.ICanDeactivateView
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Views;

/// <summary>
/// Интерфейс позволяет элементу управления, реализующему закладку IView,
/// запретить или подтвердить возможность деактивации закладки
/// </summary>
public interface ICanDeactivateView
{
  /// <summary>
  /// Выполнить запрос, можно ли деактивировать текущую закладку
  /// </summary>
  /// <param name="sender">Отправитель запроса</param>
  /// <returns>true - закладку можно деактивировать, false - закладку нельзя деактивировать</returns>
  bool CanDeactivate(object sender);
}
