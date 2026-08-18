
// Type: Intermech.UI.Wpf.ViewModels.WizardPageErrorsVM`1
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;


namespace Intermech.UI.Wpf.ViewModels;

/// <summary>Класс модели вида для ошибок на странице мастера.</summary>
public class WizardPageErrorsVM<T> : ViewModel
{
  private bool isEmpty;
  private readonly ObservableCollection<T> items;

  /// <summary>Создает объект.</summary>
  public WizardPageErrorsVM()
  {
    this.items = new ObservableCollection<T>();
    this.items.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnItemsChanged);
    this.isEmpty = true;
  }

  /// <summary>Возвращает коллекцию ошибок</summary>
  public ObservableCollection<T> Items
  {
    [DebuggerStepThrough] get => this.items;
  }

  /// <summary>Возвращает признак, что коллекция ошибок пуста.</summary>
  public bool IsEmpty
  {
    [DebuggerStepThrough] get => this.isEmpty;
  }

  /// <summary>Возвращает признак, что коллекция ошибок не пуста.</summary>
  public bool IsNotEmpty
  {
    [DebuggerStepThrough] get => !this.isEmpty;
  }

  private void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
  {
    this.UpdateIsEmpty();
  }

  private void UpdateIsEmpty()
  {
    bool flag = this.items.Count == 0;
    if (this.isEmpty == flag)
      return;
    this.isEmpty = flag;
    this.RaisePropertyChanged("IsEmpty");
    this.RaisePropertyChanged("IsNotEmpty");
  }
}
