// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.IDBObjectEntityTypeDescriptor
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;

#nullable disable
namespace Experimental.Kernel.Entities;

internal interface IDBObjectEntityTypeDescriptor : IDBEntityTypeDescriptor, IEntityTypeDescriptor
{
  DBObjectTypeMapping DBObjectType { get; }

  DataPropertyMappings DataPropertiesMappings { get; }

  DataPropertyDescriptors DataProperties { get; }

  DataPropertyDescriptor KeyProperty { get; }

  NavigationPropertyDescriptors NavigationProperties { get; }

  DBObjectNavigationPropertyMappings NavigationPropertiesMappings { get; }

  long GetKey(object entity);

  void SetKey(object entity, long newKey);

  object CreateInstance();
}
