
// Type: Intermech.Data.SectionEntities.SectionCollection
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace Intermech.Data.SectionEntities
{
    public class SectionCollection : 
      IEnumerable,
      IEnumerable<KeyValuePair<Type, object>>,
      INotifyCollectionChanged
    {
      private readonly Dictionary<Type, object> sections;

      public SectionCollection() => this.sections = new Dictionary<Type, object>();

      public SectionCollection(IEnumerable<KeyValuePair<Type, object>> sections)
        : this()
      {
        this.CopyFrom(sections);
      }

      public SectionCollection(IEnumerable sections)
        : this()
      {
        this.CopyFrom(sections);
      }

      public void CopyFrom(IEnumerable<KeyValuePair<Type, object>> sections)
      {
        if (sections == null)
          throw new ArgumentNullException(nameof (sections));
        foreach (KeyValuePair<Type, object> section in sections)
          this.Set(section.Value, section.Key);
      }

      public void CopyFrom(IEnumerable sections)
      {
        if (sections == null)
          throw new ArgumentNullException(nameof (sections));
        foreach (object section in sections)
          this.Set(section);
      }

      public void Set(object sectionObject)
      {
        if (sectionObject == null)
          throw new ArgumentNullException(nameof (sectionObject));
        this.Set(sectionObject, sectionObject.GetType());
      }

      public void Set(object sectionObject, Type sectionType)
      {
        if (sectionObject == null)
          throw new ArgumentNullException(nameof (sectionObject));
        if (sectionType == (Type) null)
          throw new ArgumentNullException(nameof (sectionType));
        if (!sectionType.IsAssignableFrom(sectionObject.GetType()))
          throw new ArgumentException("Incompatible section type.", nameof (sectionType));
        object oldSectionObject;
        if (this.sections.TryGetValue(sectionType, out oldSectionObject))
        {
          this.sections[sectionType] = sectionObject;
          this.RaiseReplaceSection(sectionType, oldSectionObject, sectionObject);
        }
        else
        {
          this.sections.Add(sectionType, sectionObject);
          this.RaiseAddSection(sectionType, sectionObject);
        }
      }

      public TSection Get<TSection>()
      {
        Type key = typeof (TSection);
        object obj;
        if (!this.sections.TryGetValue(key, out obj))
          throw new KeyNotFoundException($"Section object of type '{key}' is not found!");
        return (TSection) obj;
      }

      public TSection Get<TSection>(TSection defaultValue)
      {
        object obj;
        return this.sections.TryGetValue(typeof (TSection), out obj) ? (TSection) obj : defaultValue;
      }

      public bool Contains<TSection>() => this.sections.ContainsKey(typeof (TSection));

      public void Remove(Type sectionType)
      {
        if (sectionType == (Type) null)
          throw new ArgumentNullException(nameof (sectionType));
        object sectionObject;
        if (!this.sections.TryGetValue(sectionType, out sectionObject))
          return;
        this.sections.Remove(sectionType);
        this.RaiseRemoveSection(sectionType, sectionObject);
      }

      public void Remove<TSection>() => this.Remove(typeof (TSection));

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.sections.GetEnumerator();

      public IEnumerator<KeyValuePair<Type, object>> GetEnumerator()
      {
        return (IEnumerator<KeyValuePair<Type, object>>) this.sections.GetEnumerator();
      }

      public int Count => this.sections.Count;

      private void RaiseAddSection(Type sectionType, object sectionObject)
      {
        NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
        if (collectionChanged == null)
          return;
        collectionChanged((object) this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (object) Tuple.Create(sectionType, sectionObject)));
      }

      private void RaiseRemoveSection(Type sectionType, object sectionObject)
      {
        NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
        if (collectionChanged == null)
          return;
        collectionChanged((object) this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, (object) Tuple.Create(sectionType, sectionObject)));
      }

      private void RaiseReplaceSection(
        Type sectionType,
        object oldSectionObject,
        object newSectionObject)
      {
        NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
        if (collectionChanged == null)
          return;
        collectionChanged((object) this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, (object) Tuple.Create(sectionType, newSectionObject), (object) Tuple.Create(sectionType, oldSectionObject)));
      }

      public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}
