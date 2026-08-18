// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBMetadataInfoService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

#nullable disable
namespace Experimental.Kernel.Entities;

public sealed class DBMetadataInfoService
{
  private DBModelConfiguration modelConfiguration;

  public DBMetadataInfoService(DBModelConfiguration modelConfiguration)
  {
    this.modelConfiguration = modelConfiguration != null ? modelConfiguration : throw new ArgumentNullException(nameof (modelConfiguration));
  }

  public GlobalId<int> GetTypeId<TEntity>()
  {
    DBObjectTypeMapping dbObjectType = this.modelConfiguration.GetEntityTypeDescriptor(typeof (TEntity)).AsDBObjectDescriptor().DBObjectType;
    return new GlobalId<int>(dbObjectType.Guid, dbObjectType.Id, dbObjectType.Name);
  }

  public GlobalId<int> GetAttributeId<TEntity, TProperty>(
    Expression<Func<TEntity, TProperty>> propertySelector)
  {
    string propertyName = NameOf<TEntity>.PropertyName<TProperty>(propertySelector);
    DataPropertyMapping byPropertyName = this.modelConfiguration.GetEntityTypeDescriptor(typeof (TEntity)).AsDBObjectDescriptor().DataPropertiesMappings.GetByPropertyName(propertyName, true);
    return new GlobalId<int>(byPropertyName.Guid, byPropertyName.Id, byPropertyName.Name);
  }

  public List<GlobalId<int>> GetAttributeIdList<TEntity>()
  {
    ICollection<DataPropertyMapping> asCollection = this.modelConfiguration.GetEntityTypeDescriptor(typeof (TEntity)).AsDBObjectDescriptor().DataPropertiesMappings.AsCollection;
    List<GlobalId<int>> attributeIdList = new List<GlobalId<int>>(asCollection.Count);
    foreach (DataPropertyMapping dataPropertyMapping in (IEnumerable<DataPropertyMapping>) asCollection)
      attributeIdList.Add(new GlobalId<int>(dataPropertyMapping.Guid, dataPropertyMapping.Id, dataPropertyMapping.Name));
    return attributeIdList;
  }
}
