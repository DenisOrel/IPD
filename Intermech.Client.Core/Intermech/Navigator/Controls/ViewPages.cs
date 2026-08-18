
// Type: Intermech.Navigator.Controls.ViewPages
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Коллекция закладок навигатора, отображаемых в менеджере.
/// </summary>
public abstract class ViewPages : IEnumerable, IEnumerable<IViewPage>
{
  /// <summary>Возвращает количество закладок.</summary>
  public abstract int Count { get; }

  /// <summary>Возвращает указанную закладку.</summary>
  /// <param name="index">Индекс закладки</param>
  /// <returns>Закладка навигатора</returns>
  public abstract IViewPage this[int index] { get; }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) new ViewPages.ViewPagesEnumerator(this);

  public IEnumerator<IViewPage> GetEnumerator()
  {
    return (IEnumerator<IViewPage>) new ViewPages.ViewPagesEnumerator(this);
  }

  private class ViewPagesEnumerator : IEnumerator, IEnumerator<IViewPage>, IDisposable
  {
    private ViewPages _viewPages;
    private int _count;
    private int _index;

    public ViewPagesEnumerator(ViewPages viewPages)
    {
      this._viewPages = viewPages;
      this._count = viewPages.Count;
      this._index = -1;
    }

    object IEnumerator.Current => (object) this.Current;

    public bool MoveNext()
    {
      ++this._index;
      return this._index < this._count;
    }

    public void Reset() => this._index = -1;

    public IViewPage Current
    {
      get
      {
        try
        {
          return this._viewPages[this._index];
        }
        catch (IndexOutOfRangeException ex)
        {
          throw new InvalidOperationException();
        }
      }
    }

    public void Dispose() => this._viewPages = (ViewPages) null;
  }
}
