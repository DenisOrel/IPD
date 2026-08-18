
// Type: Intermech.Interfaces.WebPortal.PublishOptions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Опции публикации</summary>
    [Serializable]
    public class PublishOptions : IComparable, IComparable<PublishOptions>, ICloneable
    {
      /// <summary>Опции получения публикуемого состава</summary>
      public PublishCompositionOptions CompositionOptions { get; set; }

      /// <summary>Количество уровней, полный состав -1</summary>
      public int CountLevels { get; set; }

      /// <summary>
      /// Типы связей по которым раскручивается состав по запросу
      /// </summary>
      public List<int> EnableRelationTypes { get; set; }

      /// <summary>Типы искомых объектов</summary>
      public List<int> EnableTypes { get; set; }

      /// <summary>Правило подбора версий при поиске состава</summary>
      public FiltrationSettings Filtration { get; set; }

      /// <summary>Разрешенные узлы</summary>
      public string EnableSites { get; set; }

      /// <summary>
      /// Флаг, указывающий, что объекты необходимо автореплицировать в дальнейшем
      /// </summary>
      public bool AutoReplication { get; set; }

      /// <summary>Код узла, которому передаются права владения</summary>
      public char? OwnerSite { get; set; }

      /// <summary>Код узла, которому передаются права владения составом</summary>
      public char? CompositionOwnerSite { get; set; }

      /// <summary>Уровень доступа</summary>
      public int AccessLevel { get; set; }

      /// <summary>Конструктор</summary>
      /// <param name="options">Опции получения публикуемого состава</param>
      /// <param name="countLevels">Количество уровней, полный состав -1</param>
      /// <param name="enableRelationTypes">Типы связей по которым раскручивается состав</param>
      /// <param name="enableTypes">Типы искомых объектов</param>
      /// <param name="filtrationSettings">Фильтрация при поиске состава</param>
      /// <param name="enableSites">Разрешенные узлы</param>
      /// <param name="owner"></param>
      /// <param name="autoReplication"></param>
      /// <param name="accessLevel"></param>
      public PublishOptions(
        PublishCompositionOptions options,
        int countLevels,
        List<int> enableRelationTypes,
        List<int> enableTypes,
        FiltrationSettings filtrationSettings,
        string enableSites,
        bool autoReplication,
        char? owner,
        char? compositionOwner,
        int accessLevel = -1)
      {
        this.CompositionOptions = options;
        this.CountLevels = countLevels;
        this.EnableRelationTypes = enableRelationTypes;
        this.EnableTypes = enableTypes;
        this.Filtration = filtrationSettings;
        this.EnableSites = enableSites;
        this.AutoReplication = autoReplication;
        this.OwnerSite = owner;
        this.CompositionOwnerSite = compositionOwner;
        this.AccessLevel = accessLevel;
      }

      public virtual object Clone()
      {
        return (object) new PublishOptions(this.CompositionOptions, this.CountLevels, this.EnableRelationTypes, this.EnableTypes, this.Filtration, this.EnableSites, this.AutoReplication, this.OwnerSite, this.CompositionOwnerSite, this.AccessLevel);
      }

      public virtual int CompareTo(object obj)
      {
        return !(obj is PublishOptions other) ? -1 : this.CompareTo(other);
      }

      public virtual int CompareTo(PublishOptions other)
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
            if (nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue && other.AutoReplication == this.AutoReplication && other.AccessLevel == this.AccessLevel && object.Equals((object) other.EnableRelationTypes, (object) this.EnableRelationTypes) && object.Equals((object) other.EnableTypes, (object) this.EnableTypes))
              return 0;
          }
        }
        return 1;
      }
    }
}
