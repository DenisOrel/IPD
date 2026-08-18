
// Type: Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFilter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Search.ComponentModel;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;


namespace Intermech.Search.CompositionByObjectTypesFilters
{
    [Serializable]
    public sealed class CompositionByObjectTypesFilter : INotifyPropertyChanged, ICloneable
    {
      private BindingListBase<CompositionByObjectTypesFilterProjectType> _projectTypes = new BindingListBase<CompositionByObjectTypesFilterProjectType>();

      public CompositionByObjectTypesFilter()
      {
        this.ObjectVersionID = 0L;
        this._projectTypes.ListChanged += new ListChangedEventHandler(this.ProjectTypes_ListChanged);
      }

      public CompositionByObjectTypesFilter(long objectVersionID)
        : this()
      {
        this.ObjectVersionID = !ObjectHelper.IsUnknownObjectVersionID(objectVersionID) ? objectVersionID : throw new ArgumentException();
      }

      public long ObjectVersionID { get; private set; }

      public string Name { get; set; }

      public BindingListBase<CompositionByObjectTypesFilterProjectType> ProjectTypes
      {
        get => this._projectTypes;
      }

      public int[] GetCheckedPartTypeIdsForProjectType(int projectTypeID)
      {
        CompositionByObjectTypesFilterProjectType filterProjectType = this.ProjectTypes.FirstOrDefault<CompositionByObjectTypesFilterProjectType>((Func<CompositionByObjectTypesFilterProjectType, bool>) (o => o.ProjectTypeID == projectTypeID));
        if (filterProjectType == null)
        {
          foreach (int num in MetaDataHelper.GetObjectTypeParentsID(projectTypeID))
          {
            int parentType = num;
            if (ObjectTypeHelper.IsAbstract(parentType))
            {
              filterProjectType = this.ProjectTypes.FirstOrDefault<CompositionByObjectTypesFilterProjectType>((Func<CompositionByObjectTypesFilterProjectType, bool>) (o => o.ProjectTypeID == parentType));
              if (filterProjectType != null)
                break;
            }
          }
        }
        return filterProjectType != null ? ((IEnumerable<CompositionByObjectTypesFilterPartType>) filterProjectType.GetCheckedGetPartTypesAndDescendants()).Select<CompositionByObjectTypesFilterPartType, int>((Func<CompositionByObjectTypesFilterPartType, int>) (o => o.PartTypeID)).Distinct<int>().ToArray<int>() : new int[0];
      }

      public CompositionByObjectTypesFilter Clone()
      {
        CompositionByObjectTypesFilter objectTypesFilter = new CompositionByObjectTypesFilter();
        objectTypesFilter.ProjectTypes.AddRange(this.ProjectTypes.Select<CompositionByObjectTypesFilterProjectType, CompositionByObjectTypesFilterProjectType>((Func<CompositionByObjectTypesFilterProjectType, CompositionByObjectTypesFilterProjectType>) (o => o.Clone())));
        return objectTypesFilter;
      }

      public event PropertyChangedEventHandler PropertyChanged;

      object ICloneable.Clone() => (object) this.Clone();

      [System.Runtime.Serialization.OnDeserialized]
      private void OnDeserialized(StreamingContext context)
      {
        this._projectTypes.ListChanged += new ListChangedEventHandler(this.ProjectTypes_ListChanged);
      }

      private void ProjectTypes_ListChanged(object sender, ListChangedEventArgs e)
      {
        this.OnPropertyChanged("ProjectTypes");
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
