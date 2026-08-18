
// Type: Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFilterPartType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Search.ComponentModel;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;


namespace Intermech.Search.CompositionByObjectTypesFilters
{
    [Serializable]
    public sealed class CompositionByObjectTypesFilterPartType : INotifyPropertyChanged, ICloneable
    {
      private bool _checked;

      public CompositionByObjectTypesFilterPartType(int partTypeID, bool isAbstract)
      {
        this.PartTypeID = !ObjectTypeHelper.IsUnknownObjectTypeID(partTypeID) ? partTypeID : throw new ArgumentException();
        this.IsAbstract = isAbstract;
        this.Children = new BindingListBase<CompositionByObjectTypesFilterPartType>();
        this.Children.ListChanged += new ListChangedEventHandler(this.Children_ListChanged);
      }

      public int PartTypeID { get; private set; }

      public bool IsAbstract { get; private set; }

      public bool Checked
      {
        get => this._checked;
        set
        {
          if (this._checked == value)
            return;
          this._checked = value;
          this.OnPropertyChanged(nameof (Checked));
          if (!this.IsAbstract || (this.Children.Any<CompositionByObjectTypesFilterPartType>((Func<CompositionByObjectTypesFilterPartType, bool>) (o => o.Checked)) || !this._checked) && this._checked)
            return;
          bool listChangedEvents = this.Children.RaiseListChangedEvents;
          this.Children.RaiseListChangedEvents = false;
          try
          {
            foreach (CompositionByObjectTypesFilterPartType child in (Collection<CompositionByObjectTypesFilterPartType>) this.Children)
              child.Checked = this._checked;
          }
          finally
          {
            this.Children.RaiseListChangedEvents = listChangedEvents;
            this.Children.ResetBindings();
          }
        }
      }

      public BindingListBase<CompositionByObjectTypesFilterPartType> Children { get; private set; }

      public IEnumerable<CompositionByObjectTypesFilterPartType> GetDescendents()
      {
        foreach (CompositionByObjectTypesFilterPartType child in (Collection<CompositionByObjectTypesFilterPartType>) this.Children)
        {
          yield return child;
          foreach (CompositionByObjectTypesFilterPartType descendent in child.GetDescendents())
            yield return descendent;
        }
      }

      public IEnumerable<CompositionByObjectTypesFilterPartType> GetDescendentsAndSelf()
      {
        foreach (CompositionByObjectTypesFilterPartType descendent in this.GetDescendents())
          yield return descendent;
        yield return this;
      }

      public CompositionByObjectTypesFilterPartType Clone()
      {
        CompositionByObjectTypesFilterPartType typesFilterPartType = new CompositionByObjectTypesFilterPartType(this.PartTypeID, this.IsAbstract);
        typesFilterPartType.Checked = this.Checked;
        typesFilterPartType.Children.AddRange(this.Children.Select<CompositionByObjectTypesFilterPartType, CompositionByObjectTypesFilterPartType>((Func<CompositionByObjectTypesFilterPartType, CompositionByObjectTypesFilterPartType>) (o => o.Clone())));
        return typesFilterPartType;
      }

      public event PropertyChangedEventHandler PropertyChanged;

      object ICloneable.Clone() => (object) this.Clone();

      [System.Runtime.Serialization.OnDeserialized]
      private void OnDeserialized(StreamingContext context)
      {
        this.Children.ListChanged += new ListChangedEventHandler(this.Children_ListChanged);
      }

      private void Children_ListChanged(object sender, ListChangedEventArgs e)
      {
        if (this.IsAbstract)
          this.Checked = this.Children.Any<CompositionByObjectTypesFilterPartType>((Func<CompositionByObjectTypesFilterPartType, bool>) (o => o.Checked));
        this.OnPropertyChanged("Children");
      }

      private void OnPropertyChanged(string propertyName)
      {
        PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
        if (propertyChanged == null)
          return;
        propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
      }
    }
}
