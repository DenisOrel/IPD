
// Type: Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFilterProjectType
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
    public sealed class CompositionByObjectTypesFilterProjectType : INotifyPropertyChanged, ICloneable
    {
      private int _projectTypeID = -1;
      private BindingListBase<CompositionByObjectTypesFilterPartType> _partTypes = new BindingListBase<CompositionByObjectTypesFilterPartType>();

      public CompositionByObjectTypesFilterProjectType(int projectTypeID)
      {
        this._projectTypeID = !ObjectTypeHelper.IsUnknownObjectTypeID(projectTypeID) ? projectTypeID : throw new ArgumentException();
        this._partTypes.ListChanged += new ListChangedEventHandler(this.PartTypes_ListChanged);
      }

      public int ProjectTypeID => this._projectTypeID;

      public BindingListBase<CompositionByObjectTypesFilterPartType> PartTypes => this._partTypes;

      public IEnumerable<CompositionByObjectTypesFilterPartType> GetPartTypesAndDescendants()
      {
        foreach (CompositionByObjectTypesFilterPartType partType in (Collection<CompositionByObjectTypesFilterPartType>) this._partTypes)
        {
          yield return partType;
          foreach (CompositionByObjectTypesFilterPartType descendent in partType.GetDescendents())
            yield return descendent;
        }
      }

      public CompositionByObjectTypesFilterPartType[] GetCheckedGetPartTypesAndDescendants()
      {
        return this.GetPartTypesAndDescendants().Where<CompositionByObjectTypesFilterPartType>((Func<CompositionByObjectTypesFilterPartType, bool>) (o => !o.IsAbstract && o.Checked)).ToArray<CompositionByObjectTypesFilterPartType>();
      }

      public void CheckPartTypesAndDescendants()
      {
        bool listChangedEvents = this._partTypes.RaiseListChangedEvents;
        this._partTypes.RaiseListChangedEvents = false;
        try
        {
          foreach (CompositionByObjectTypesFilterPartType typesAndDescendant in this.GetPartTypesAndDescendants())
            typesAndDescendant.Checked = true;
        }
        finally
        {
          this._partTypes.RaiseListChangedEvents = listChangedEvents;
          this._partTypes.ResetBindings();
        }
      }

      public void UncheckPartTypesAndDescendants()
      {
        bool listChangedEvents = this._partTypes.RaiseListChangedEvents;
        this._partTypes.RaiseListChangedEvents = false;
        try
        {
          foreach (CompositionByObjectTypesFilterPartType typesAndDescendant in this.GetPartTypesAndDescendants())
            typesAndDescendant.Checked = false;
        }
        finally
        {
          this._partTypes.RaiseListChangedEvents = listChangedEvents;
          this._partTypes.ResetBindings();
        }
      }

      public void CheckPartTypesAndDescendants(int[] partTypeIds)
      {
        if (partTypeIds == null)
          throw new ArgumentNullException(nameof (partTypeIds));
        bool listChangedEvents = this._partTypes.RaiseListChangedEvents;
        this._partTypes.RaiseListChangedEvents = false;
        try
        {
          foreach (CompositionByObjectTypesFilterPartType typesFilterPartType in this.GetPartTypesAndDescendants().Where<CompositionByObjectTypesFilterPartType>((Func<CompositionByObjectTypesFilterPartType, bool>) (o => !o.IsAbstract)))
          {
            if (((IEnumerable<int>) partTypeIds).Contains<int>(typesFilterPartType.PartTypeID))
              typesFilterPartType.Checked = true;
          }
        }
        finally
        {
          this._partTypes.RaiseListChangedEvents = listChangedEvents;
          this._partTypes.ResetBindings();
        }
      }

      public CompositionByObjectTypesFilterProjectType Clone()
      {
        CompositionByObjectTypesFilterProjectType filterProjectType = new CompositionByObjectTypesFilterProjectType(this.ProjectTypeID);
        filterProjectType.PartTypes.AddRange(this.PartTypes.Select<CompositionByObjectTypesFilterPartType, CompositionByObjectTypesFilterPartType>((Func<CompositionByObjectTypesFilterPartType, CompositionByObjectTypesFilterPartType>) (o => o.Clone())));
        return filterProjectType;
      }

      public event PropertyChangedEventHandler PropertyChanged;

      object ICloneable.Clone() => (object) this.Clone();

      [System.Runtime.Serialization.OnDeserialized]
      private void OnDeserialized(StreamingContext context)
      {
        this._partTypes.ListChanged += new ListChangedEventHandler(this.PartTypes_ListChanged);
      }

      private void PartTypes_ListChanged(object sender, ListChangedEventArgs e)
      {
        this.OnPropertyChanged("PartTypes");
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
