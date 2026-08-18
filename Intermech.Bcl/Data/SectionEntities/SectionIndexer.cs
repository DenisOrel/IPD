
// Type: Intermech.Data.SectionEntities.SectionIndexer
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Data.EntityDb;
using Intermech.Data.EntityDb.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;


namespace Intermech.Data.SectionEntities
{
    public sealed class SectionIndexer : EntityIndexerBase
    {
      private Dictionary<Type, SectionMetadata> metadataStore;
      private Dictionary<long, IIndex<object>> indexStore;
      private Dictionary<long, SectionEntityData> entityDataStore;
      private IIndex<Type> sectionTypeIndex;

      public override void Initialize(EntityDatabase database)
      {
        base.Initialize(database);
        this.metadataStore = new Dictionary<Type, SectionMetadata>();
        this.indexStore = new Dictionary<long, IIndex<object>>();
        this.entityDataStore = new Dictionary<long, SectionEntityData>();
        this.sectionTypeIndex = (IIndex<Type>) new UniversalIndex<Type, Type>((IIndexKeyProvider<Type, Type>) new EmptyIndexKeyProvider<Type>(), (IDirectIndex<Type>) new NonUniqueEqualityDirectIndex<Type>((IEqualityComparer<Type>) EqualityComparer<Type>.Default), (IInverseIndex<Type>) new NonUniqueInverseIndex<Type>());
      }

      protected override bool IsEntitySupported(IEntity entity) => entity is SectionEntity;

      protected override void DoAddToIndex(IEntity entity)
      {
        SectionEntity entity1 = (SectionEntity) entity;
        SectionEntityData entityData = new SectionEntityData(entity1);
        this.entityDataStore.Add(entity1.UniqueId, entityData);
        foreach (KeyValuePair<Type, object> section in entity1.Sections)
          this.AddSectionToIndex(entityData, section.Key, section.Value);
        entityData.MetadataWatcher = (NotifyCollectionChangedEventHandler) ((sender, e) => this.OnEntityMetadataChanged(entityData, e));
        entity1.Sections.CollectionChanged += entityData.MetadataWatcher;
      }

      private void AddSectionToIndex(
        SectionEntityData entityData,
        Type sectionType,
        object sectionObject)
      {
        Tuple<SectionMetadata, bool> orCreateMetadata = this.FindOrCreateMetadata(sectionType);
        if (orCreateMetadata.Item2)
        {
          foreach (SectionProperty enumProperty in orCreateMetadata.Item1.EnumProperties())
            this.indexStore.Add(enumProperty.UniqueId, enumProperty.CreateIndex());
        }
        foreach (SectionProperty enumProperty in orCreateMetadata.Item1.EnumProperties())
        {
          SectionProperty sectionProperty = enumProperty;
          this.AddPropertyToIndex(entityData, sectionType, sectionProperty, sectionProperty.GetValue(sectionObject));
          if (!sectionProperty.IsReadOnly)
          {
            EventHandler handler = (EventHandler) ((sender, e) => this.OnSectionPropertyChanged(entityData, sectionType, sectionObject, sectionProperty));
            sectionProperty.Descriptor.AddValueChanged(sectionObject, handler);
            entityData[sectionProperty].PropertyWatcher = handler;
          }
        }
        this.sectionTypeIndex.AddValue((IEntity) entityData.Entity, sectionType);
      }

      private void AddPropertyToIndex(
        SectionEntityData entityData,
        Type sectionType,
        SectionProperty sectionProperty,
        object propertyValue)
      {
        IIndex<object> index = this.indexStore[sectionProperty.UniqueId];
        switch (sectionProperty.Kind)
        {
          case PropertyKind.Scalar:
            index.AddValue((IEntity) entityData.Entity, propertyValue);
            break;
          case PropertyKind.Vector:
            if (propertyValue is IEnumerable enumerable)
            {
              foreach (object propertyValue1 in enumerable)
                index.AddValue((IEntity) entityData.Entity, propertyValue1);
            }
            if (!(propertyValue is INotifyCollectionChanged collectionChanged))
              break;
            NotifyCollectionChangedEventHandler changedEventHandler = (NotifyCollectionChangedEventHandler) ((sender, e) => this.OnCollectionContentChanged(entityData, sectionType, sectionProperty, sender, e));
            collectionChanged.CollectionChanged += changedEventHandler;
            entityData[sectionProperty].Collection = collectionChanged;
            entityData[sectionProperty].CollectionWatcher = changedEventHandler;
            break;
          default:
            throw new NotImplementedException();
        }
      }

      protected override void DoDeleteFromIndex(IEntity entity)
      {
        SectionEntity sectionEntity = (SectionEntity) entity;
        SectionEntityData entityData = this.entityDataStore[entity.UniqueId];
        sectionEntity.Sections.CollectionChanged -= entityData.MetadataWatcher;
        entityData.MetadataWatcher = (NotifyCollectionChangedEventHandler) null;
        foreach (KeyValuePair<Type, object> section in sectionEntity.Sections)
          this.DeleteSectionFromIndex(entityData, section.Key, section.Value);
        this.entityDataStore.Remove(entity.UniqueId);
      }

      private void DeleteSectionFromIndex(
        SectionEntityData entityData,
        Type sectionType,
        object sectionObject)
      {
        SectionMetadata metadata = this.FindMetadata(sectionType);
        if (metadata == null)
          return;
        this.sectionTypeIndex.RemoveValue((IEntity) entityData.Entity, sectionType);
        foreach (SectionProperty enumProperty in metadata.EnumProperties())
        {
          this.DeletePropertyFromIndex(entityData, enumProperty);
          if (!enumProperty.IsReadOnly)
          {
            SectionEntityPropertyData entityPropertyData = entityData[enumProperty];
            if (entityPropertyData.PropertyWatcher != null)
            {
              enumProperty.Descriptor.RemoveValueChanged(sectionObject, entityPropertyData.PropertyWatcher);
              entityPropertyData.PropertyWatcher = (EventHandler) null;
            }
          }
        }
      }

      private void DeletePropertyFromIndex(
        SectionEntityData entityData,
        SectionProperty sectionProperty)
      {
        IIndex<object> index = this.indexStore[sectionProperty.UniqueId];
        switch (sectionProperty.Kind)
        {
          case PropertyKind.Scalar:
            index.RemoveAllValues((IEntity) entityData.Entity);
            break;
          case PropertyKind.Vector:
            SectionEntityPropertyData entityPropertyData = entityData[sectionProperty];
            if (entityPropertyData.Collection != null && entityPropertyData.CollectionWatcher != null)
            {
              entityPropertyData.Collection.CollectionChanged -= entityPropertyData.CollectionWatcher;
              entityPropertyData.Collection = (INotifyCollectionChanged) null;
              entityPropertyData.CollectionWatcher = (NotifyCollectionChangedEventHandler) null;
            }
            index.RemoveAllValues((IEntity) entityData.Entity);
            break;
          default:
            throw new NotImplementedException();
        }
      }

      private void OnSectionPropertyChanged(
        SectionEntityData entityData,
        Type sectionType,
        object sectionObject,
        SectionProperty sectionProperty)
      {
        this.DeletePropertyFromIndex(entityData, sectionProperty);
        object propertyValue = sectionProperty.GetValue(sectionObject);
        this.AddPropertyToIndex(entityData, sectionType, sectionProperty, propertyValue);
      }

      private void OnCollectionContentChanged(
        SectionEntityData entityData,
        Type sectionType,
        SectionProperty sectionProperty,
        object propertyValue,
        NotifyCollectionChangedEventArgs e)
      {
        IIndex<object> index = this.indexStore[sectionProperty.UniqueId];
        switch (e.Action)
        {
          case NotifyCollectionChangedAction.Add:
            IEnumerator enumerator1 = e.NewItems.GetEnumerator();
            try
            {
              while (enumerator1.MoveNext())
              {
                object current = enumerator1.Current;
                index.AddValue((IEntity) entityData.Entity, current);
              }
              break;
            }
            finally
            {
              if (enumerator1 is IDisposable disposable)
                disposable.Dispose();
            }
          case NotifyCollectionChangedAction.Remove:
            IEnumerator enumerator2 = e.OldItems.GetEnumerator();
            try
            {
              while (enumerator2.MoveNext())
              {
                object current = enumerator2.Current;
                index.RemoveValue((IEntity) entityData.Entity, current);
              }
              break;
            }
            finally
            {
              if (enumerator2 is IDisposable disposable)
                disposable.Dispose();
            }
          case NotifyCollectionChangedAction.Replace:
            foreach (object oldItem in (IEnumerable) e.OldItems)
              index.RemoveValue((IEntity) entityData.Entity, oldItem);
            IEnumerator enumerator3 = e.NewItems.GetEnumerator();
            try
            {
              while (enumerator3.MoveNext())
              {
                object current = enumerator3.Current;
                index.AddValue((IEntity) entityData.Entity, current);
              }
              break;
            }
            finally
            {
              if (enumerator3 is IDisposable disposable)
                disposable.Dispose();
            }
          case NotifyCollectionChangedAction.Reset:
            index.RemoveAllValues((IEntity) entityData.Entity);
            if (!(propertyValue is IEnumerable enumerable))
              break;
            IEnumerator enumerator4 = enumerable.GetEnumerator();
            try
            {
              while (enumerator4.MoveNext())
              {
                object current = enumerator4.Current;
                index.AddValue((IEntity) entityData.Entity, current);
              }
              break;
            }
            finally
            {
              if (enumerator4 is IDisposable disposable)
                disposable.Dispose();
            }
        }
      }

      /// <summary>
      /// Срабатывает, когда изменяется список секций у объекта.
      /// </summary>
      /// <remarks>
      /// Реализация этого обработчика тесно связана с классом <see cref="T:Intermech.Data.EntityDb.Sections.SectionCollection" />.
      /// </remarks>
      /// <param name="entityData">Объект базы данных, у которого изменился список секций</param>
      /// <param name="e">Аргументы события</param>
      private void OnEntityMetadataChanged(
        SectionEntityData entityData,
        NotifyCollectionChangedEventArgs e)
      {
        switch (e.Action)
        {
          case NotifyCollectionChangedAction.Add:
            IEnumerator enumerator1 = e.NewItems.GetEnumerator();
            try
            {
              while (enumerator1.MoveNext())
              {
                Tuple<Type, object> current = (Tuple<Type, object>) enumerator1.Current;
                this.AddSectionToIndex(entityData, current.Item1, current.Item2);
              }
              break;
            }
            finally
            {
              if (enumerator1 is IDisposable disposable)
                disposable.Dispose();
            }
          case NotifyCollectionChangedAction.Remove:
            IEnumerator enumerator2 = e.OldItems.GetEnumerator();
            try
            {
              while (enumerator2.MoveNext())
              {
                Tuple<Type, object> current = (Tuple<Type, object>) enumerator2.Current;
                this.DeleteSectionFromIndex(entityData, current.Item1, current.Item2);
              }
              break;
            }
            finally
            {
              if (enumerator2 is IDisposable disposable)
                disposable.Dispose();
            }
          case NotifyCollectionChangedAction.Replace:
            foreach (Tuple<Type, object> oldItem in (IEnumerable) e.OldItems)
              this.DeleteSectionFromIndex(entityData, oldItem.Item1, oldItem.Item2);
            IEnumerator enumerator3 = e.NewItems.GetEnumerator();
            try
            {
              while (enumerator3.MoveNext())
              {
                Tuple<Type, object> current = (Tuple<Type, object>) enumerator3.Current;
                this.AddSectionToIndex(entityData, current.Item1, current.Item2);
              }
              break;
            }
            finally
            {
              if (enumerator3 is IDisposable disposable)
                disposable.Dispose();
            }
          default:
            throw new NotImplementedException();
        }
      }

      protected override EntitySet DoQuery(EntityQuery query, IQueryCondition condition)
      {
        if (condition is PropertyValueCondition condition1)
        {
          if (condition1.PropertyReference.Equals(SectionVirtualProperties.SectionTypeRef))
            return this.QuerySectionTypeCondition(query, condition1);
          if (condition1.PropertyReference is SectionPropertyReference)
            return this.QuerySectionPropertyCondition(query, condition1);
        }
        return base.DoQuery(query, condition);
      }

      private EntitySet QuerySectionTypeCondition(EntityQuery query, PropertyValueCondition condition)
      {
        return this.sectionTypeIndex.Query(query, (IQueryCondition) condition);
      }

      private EntitySet QuerySectionPropertyCondition(
        EntityQuery query,
        PropertyValueCondition condition)
      {
        SectionPropertyReference propertyReference = (SectionPropertyReference) condition.PropertyReference;
        SectionMetadata metadata = this.FindMetadata(propertyReference.SectionType);
        if (metadata != null)
        {
          SectionProperty sectionProperty = metadata.PropertyByName(propertyReference.PropertyName);
          IIndex<object> index;
          if (sectionProperty != null && this.indexStore.TryGetValue(sectionProperty.UniqueId, out index))
            return index.Query(query, (IQueryCondition) condition);
        }
        return (EntitySet) null;
      }

      private SectionMetadata FindMetadata(Type sectionType)
      {
        SectionMetadata metadata;
        this.metadataStore.TryGetValue(sectionType, out metadata);
        return metadata;
      }

      private Tuple<SectionMetadata, bool> FindOrCreateMetadata(Type sectionType)
      {
        bool flag = false;
        SectionMetadata metadata;
        if (!this.metadataStore.TryGetValue(sectionType, out metadata))
        {
          metadata = this.CreateMetadata(sectionType);
          this.metadataStore.Add(sectionType, metadata);
          flag = true;
        }
        return Tuple.Create(metadata, flag);
      }

      private SectionMetadata CreateMetadata(Type sectionType)
      {
        LinkedList<SectionProperty> linkedList = new LinkedList<SectionProperty>();
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(sectionType))
        {
          IndexableAttribute attribute = (IndexableAttribute) property.Attributes[typeof (IndexableAttribute)];
          if (attribute != null)
          {
            SectionProperty sectionProperty = new SectionProperty(property, attribute);
            if (sectionProperty.CompareInfo.CompareKind == PropertyCompareKind.None)
              throw new Exception("Can't detect comparer");
            linkedList.AddLast(sectionProperty);
          }
        }
        return new SectionMetadata(sectionType, (ICollection<SectionProperty>) linkedList);
      }
    }
}
