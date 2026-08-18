
// Type: Intermech.Navigator.Views.OrderIDComparer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;


namespace Intermech.Navigator.Views;

/// <summary>
/// Реализует алгоритм сортировки коллекции закладок по возрастанию значений
/// индекса расположения (IView.OrderID).
/// </summary>
internal class OrderIDComparer : IComparer<IView>
{
  private int _factor;

  public OrderIDComparer(bool ascendingSort) => this._factor = ascendingSort ? 1 : -1;

  public int Compare(IView x, IView y) => this._factor * x.OrderID.CompareTo(y.OrderID);
}
