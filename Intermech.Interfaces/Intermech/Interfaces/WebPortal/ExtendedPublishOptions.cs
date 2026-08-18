
// Type: Intermech.Interfaces.WebPortal.ExtendedPublishOptions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.WebPortal
{
    [Serializable]
    public class ExtendedPublishOptions : PublishOptions, IComparable<ExtendedPublishOptions>
    {
      public TaskPriority TaskPriority { get; set; }

      public ExtendedPublishOptions(
        string enableSites,
        char? owner,
        char? compositionOwner,
        TaskPriority priority = TaskPriority.Normal)
        : this(PublishCompositionOptions.None, -1, (List<int>) null, (List<int>) null, (FiltrationSettings) null, enableSites, false, owner, compositionOwner, priority)
      {
      }

      public ExtendedPublishOptions(
        PublishCompositionOptions options,
        int countLevels,
        List<int> enableRelationTypes,
        List<int> enableTypes,
        FiltrationSettings filtrationSettings,
        TaskPriority priority = TaskPriority.Normal,
        int accessLevel = -1)
        : this(options, countLevels, enableRelationTypes, enableTypes, filtrationSettings, string.Empty, false, new char?(), new char?(), priority, accessLevel)
      {
      }

      public ExtendedPublishOptions(
        PublishCompositionOptions options,
        int countLevels,
        List<int> enableRelationTypes,
        List<int> enableTypes,
        FiltrationSettings filtrationSettings,
        string enableSites,
        bool autoReplication,
        char? owner,
        char? compositionOwner,
        TaskPriority priority = TaskPriority.Normal,
        int accessLevel = -1)
        : base(options, countLevels, enableRelationTypes, enableTypes, filtrationSettings, enableSites, autoReplication, owner, compositionOwner, accessLevel)
      {
        this.TaskPriority = priority;
      }

      public override object Clone()
      {
        return (object) new ExtendedPublishOptions(this.CompositionOptions, this.CountLevels, this.EnableRelationTypes, this.EnableTypes, this.Filtration, this.EnableSites, this.AutoReplication, this.OwnerSite, this.CompositionOwnerSite, this.TaskPriority, this.AccessLevel);
      }

      public override int CompareTo(object obj)
      {
        return !(obj is ExtendedPublishOptions other) ? -1 : this.CompareTo(other);
      }

      public int CompareTo(ExtendedPublishOptions other)
      {
        if (other.CompositionOptions == this.CompositionOptions && other.CountLevels == this.CountLevels && other.Filtration.OwnerID == this.Filtration.OwnerID && other.EnableSites == this.EnableSites)
        {
          char? nullable1 = other.OwnerSite;
          int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
          nullable1 = this.OwnerSite;
          int? nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
          if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
          {
            nullable1 = other.CompositionOwnerSite;
            int? nullable4 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
            nullable1 = this.CompositionOwnerSite;
            int? nullable5 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
            if (nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue && other.AutoReplication == this.AutoReplication && other.AccessLevel == this.AccessLevel && object.Equals((object) other.EnableRelationTypes, (object) this.EnableRelationTypes) && object.Equals((object) other.EnableTypes, (object) this.EnableTypes) && other.TaskPriority == this.TaskPriority)
              return 0;
          }
        }
        return 1;
      }

      public static ExtendedPublishOptions Create(PublishOptions options)
      {
        return new ExtendedPublishOptions(options.CompositionOptions, options.CountLevels, options.EnableRelationTypes, options.EnableTypes, options.Filtration, options.EnableSites, options.AutoReplication, options.OwnerSite, options.CompositionOwnerSite, accessLevel: options.AccessLevel);
      }
    }
}
