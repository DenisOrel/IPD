
// Type: Intermech.Tools.Data.Sync.AppToDBAttributeSyncTaskIDCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Data.Metadata;
using System;


namespace Intermech.Tools.Data.Sync;

internal sealed class AppToDBAttributeSyncTaskIDCache
{
  public AppToDBAttributeSyncTaskIDCache(MetadataResolverFactory metadataResolvers)
  {
    this.CheckoutById = metadataResolvers.AttributeTypeResolver(new Guid("CAD0002D-306C-11D8-B4E9-00304F19F545"));
    this.ObjectType = metadataResolvers.AttributeTypeResolver(new Guid("CAD0002E-306C-11D8-B4E9-00304F19F545"));
    this.OwnerId = metadataResolvers.AttributeTypeResolver(new Guid("CAD0002F-306C-11D8-B4E9-00304F19F545"));
    this.ProjectId = metadataResolvers.AttributeTypeResolver(new Guid("CAD00811-306C-11D8-B4E9-00304F19F545"));
    this.ContentModifyDate = metadataResolvers.AttributeTypeResolver(new Guid("CAD0013A-306C-11D8-B4E9-00304F19F545"));
    this.ChangeNumber = metadataResolvers.AttributeTypeResolver(new Guid("CAD00770-306C-11D8-B4E9-00304F19F545"));
  }

  /// <summary>Кем взят на редактирование</summary>
  public AttributeTypeResolver CheckoutById { get; private set; }

  /// <summary>Тип объекта</summary>
  public AttributeTypeResolver ObjectType { get; private set; }

  /// <summary>Владелец объекта</summary>
  public AttributeTypeResolver OwnerId { get; private set; }

  /// <summary>Принадлежность проекту</summary>
  public AttributeTypeResolver ProjectId { get; private set; }

  /// <summary>Дата модификации содержимого объекта</summary>
  public AttributeTypeResolver ContentModifyDate { get; private set; }

  /// <summary>Номер изменения (поддержка извещений)</summary>
  public AttributeTypeResolver ChangeNumber { get; private set; }
}
