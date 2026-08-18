
// Type: Intermech.Search.BaseModel
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.ComponentModel;
using System.Linq.Expressions;


namespace Intermech.Search
{
    [Serializable]
    public abstract class BaseModel : INotifyPropertyChanged
    {
      protected void OnPropertyChanged<T>(Expression<Func<T>> expression)
      {
        PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
        if (propertyChanged == null)
          return;
        propertyChanged((object) this, new PropertyChangedEventArgs(((MemberExpression) expression.Body).Member.Name));
      }

      public event PropertyChangedEventHandler PropertyChanged;
    }
}
