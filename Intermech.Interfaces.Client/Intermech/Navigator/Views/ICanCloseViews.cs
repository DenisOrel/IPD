// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.ICanCloseViews
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Views;

/// <summary>
/// Интерфейс позволяет элементу управления, реализующему закладку IView,
/// запретить или подтвердить возможность закрытия формы, на которой
/// расположен менеджер закладок
/// </summary>
public interface ICanCloseViews
{
  /// <summary>
  /// Выполнить запрос, можно ли закрывать форму, на которой расположены закладки
  /// </summary>
  /// <param name="sender">Отправитель запроса</param>
  /// <returns>true - закладка разрешает закрытие формы, false - закладка запрещает закрытие формы</returns>
  bool CanClose(object sender);
}
