// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.AssemblyExtensions
// Assembly: Intermech.Extensions.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A1017829-B851-420B-83EC-75723A20702A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Extensions.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class AssemblyExtensions
{
  public const string CreatorNestedClassName = "Creator";
  public const string TypeIdPropertyName = "TypeID";
  public const string TypeGuidPropertyName = "TypeGuid";
  public const string CreatorKnownTypesFieldName = "KnownTypes";

  public static void RegisterDbCreators(
    [NotNull] this Assembly assembly,
    bool overrideIdExist = true,
    bool recursiveChildTypes = true)
  {
    assembly.RegisterDBEntityCreators<DBObject, IDBObjectService, IDBObjectCreator>(AssemblyExtensions.EntityType.Object, overrideIdExist, recursiveChildTypes, false);
    assembly.RegisterDBEntityCreators<DBObjectCollection, IDBObjectCollectionService, IDBObjectCollectionCreator>(AssemblyExtensions.EntityType.Object, overrideIdExist, recursiveChildTypes, true);
    assembly.RegisterDBEntityCreators<DBRelation, IDBRelationService, IDBRelationCreator>(AssemblyExtensions.EntityType.Relation, overrideIdExist, false, false);
    assembly.RegisterDBEntityCreators<DBRelationCollection, IDBRelationCollectionService, IDBRelationCollectionCreator>(AssemblyExtensions.EntityType.Relation, overrideIdExist, false, true);
  }

  private static void RegisterDBEntityCreators<TBaseType, TServiceInterface, TCreatorInterface>(
    [NotNull] this Assembly assembly,
    AssemblyExtensions.EntityType entity,
    bool overrideIdExist,
    bool recursiveChildTypes,
    bool collection)
    where TBaseType : class
    where TServiceInterface : class
    where TCreatorInterface : class
  {
    int num = recursiveChildTypes ? 1 : 0;
    Dictionary<(int, Guid), Type> typeCustomCreators = new Dictionary<(int, Guid), Type>();
    Dictionary<(int, Guid), (Type, bool)> defaultTypeCreators = new Dictionary<(int, Guid), (Type, bool)>();
    HashSet<int> typeIDsToRegister = new HashSet<int>();
    string creatorInterfaceName = typeof (TCreatorInterface).Name;
    Type baseType = typeof (TBaseType);
    Type[] types = assembly.GetTypes();
    foreach (Type type in ((IEnumerable<Type>) types).Where<Type>((Func<Type, bool>) (type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(baseType))).ToList<Type>(types.Length))
    {
      bool flag = false;
      Type customCreatorType = ((IEnumerable<Type>) type.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic)).FirstOrDefault<Type>((Func<Type, bool>) (nestedType => string.Equals(nestedType.Name, "Creator", StringComparison.Ordinal)));
      if (customCreatorType != (Type) null && customCreatorType.GetField("KnownTypes", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty) == (FieldInfo) null)
      {
        RememberTypeCustomCreator(type, customCreatorType);
        flag = true;
      }
      if (!flag)
      {
        switch (entity)
        {
          case AssemblyExtensions.EntityType.Object:
            DBObjectTypeHandlerAttribute customAttribute1 = type.GetCustomAttribute<DBObjectTypeHandlerAttribute>();
            if (customAttribute1 != null)
            {
              defaultTypeCreators.Add((customAttribute1.ObjectTypeID, customAttribute1.ObjectTypeGuid), (type, customAttribute1.RecursiveHandle));
              typeIDsToRegister.Add(customAttribute1.ObjectTypeID);
              continue;
            }
            if (type.IsDefined(typeof (DBRelationTypeHandlerAttribute)))
              throw new Exception($"Object type {type} has relation attribute {typeof (DBRelationTypeHandlerAttribute)}");
            continue;
          case AssemblyExtensions.EntityType.Relation:
            DBRelationTypeHandlerAttribute customAttribute2 = type.GetCustomAttribute<DBRelationTypeHandlerAttribute>();
            if (customAttribute2 != null)
            {
              defaultTypeCreators.Add((customAttribute2.RelationTypeID, customAttribute2.RelationTypeGuid), (type, false));
              typeIDsToRegister.Add(customAttribute2.RelationTypeID);
              continue;
            }
            if (type.IsDefined(typeof (DBObjectTypeHandlerAttribute)))
              throw new Exception($"Relation type {type} has relation attribute {typeof (DBObjectTypeHandlerAttribute)}");
            continue;
          default:
            throw new ArgumentOutOfRangeException(nameof (entity), (object) entity, (string) null);
        }
      }
    }
    foreach (Type customCreatorType in ((IEnumerable<Type>) types).Where<Type>((Func<Type, bool>) (type => type.IsClass && !type.IsAbstract && type.GetInterface(creatorInterfaceName) != (Type) null)).ToList<Type>(types.Length))
    {
      customCreatorType.GetConstructor(Type.EmptyTypes);
      FieldInfo field = customCreatorType.GetField("KnownTypes", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty);
      if (field != (FieldInfo) null)
      {
        foreach (Type entityType in (Type[]) field.GetValue((object) null))
          RememberTypeCustomCreator(entityType, customCreatorType);
      }
    }
    ICreatorContainer creators = (ICreatorContainer) null;
    (int, Guid) key;
    if (defaultTypeCreators.Count > 0)
    {
      foreach (KeyValuePair<(int ID, Guid Guid), (Type EnityType, bool Recursive)> keyValuePair in defaultTypeCreators)
      {
        (Type EnityType, bool Recursive) tuple1;
        keyValuePair.Deconstruct<(int, Guid), (Type, bool)>(out key, out tuple1);
        (int, Guid) typeIdentity = key;
        (Type EnityType, bool Recursive) tuple2 = tuple1;
        TCreatorInterface instance;
        if (collection)
        {
          instance = DefaultDBEntityCollectionCreator.Instance as TCreatorInterface;
          AssemblyExtensions.RegisterEntityCollectionType(entity, typeIdentity.Item2, tuple2.EnityType);
        }
        else
        {
          instance = DefaultDBEntityCreator.Instance as TCreatorInterface;
          AssemblyExtensions.RegisterEntityType(entity, typeIdentity.Item2, tuple2.EnityType);
        }
        AddCreator(typeIdentity, instance, tuple2.Recursive);
      }
    }
    if (typeCustomCreators.Count <= 0)
      return;
    foreach (KeyValuePair<(int TypeID, Guid TypeGuid), Type> keyValuePair in typeCustomCreators)
    {
      Type type;
      keyValuePair.Deconstruct<(int, Guid), Type>(out key, out type);
      AddCreator(key, type.GetConstructor(Type.EmptyTypes).Invoke(Array.Empty<object>()) as TCreatorInterface, recursiveChildTypes);
    }

    void RememberTypeCustomCreator(Type entityType, Type customCreatorType)
    {
      PropertyInfo property1 = entityType.GetProperty("TypeID", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, (Binder) null, typeof (int), Type.EmptyTypes, (ParameterModifier[]) null);
      PropertyInfo property2 = entityType.GetProperty("TypeGuid", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty, (Binder) null, typeof (Guid), Type.EmptyTypes, (ParameterModifier[]) null);
      FieldInfo field = property2 != (PropertyInfo) null ? (FieldInfo) null : entityType.GetField("TypeGuid", BindingFlags.Static | BindingFlags.Public);
      int num1 = field != (FieldInfo) null ? 1 : 0;
      if (property1 == (PropertyInfo) null && property2 == (PropertyInfo) null && field == (FieldInfo) null)
        throw new InvalidOperationException($"Type {entityType} does not implement public static property or field " + "int TypeID  or Guid TypeGuid");
      int? nullable1 = new int?();
      Guid? nullable2 = new Guid?();
      if (property1 != (PropertyInfo) null)
        nullable1 = new int?((int) property1.GetMethod.Invoke((object) null, (object[]) null));
      if (property2 != (PropertyInfo) null || field != (FieldInfo) null)
      {
        int num2 = property2 != (PropertyInfo) null ? 1 : 0;
        nullable2 = new Guid?((Guid) (property2 != (PropertyInfo) null ? property2.GetMethod.Invoke((object) null, (object[]) null) : field.GetValue((object) null)));
      }
      if (!nullable2.HasValue)
      {
        nullable2 = new Guid?(AssemblyExtensions.GetEntityTypeGuid(entity, nullable1.Value));
      }
      else
      {
        int num3 = nullable1.HasValue ? 1 : 0;
      }
      if (!nullable1.HasValue)
        nullable1 = new int?(AssemblyExtensions.GetEntityTypeID(entity, nullable2.Value));
      int num4 = nullable1.Value;
      Guid guid = nullable2.Value;
      if (typeIDsToRegister.Contains(num4))
      {
        if (!defaultTypeCreators.ContainsKey((num4, guid)))
          throw new Exception((entity == AssemblyExtensions.EntityType.Object ? "Object" : "Relation") + $" type with ID={num4} has multiple custom creators in assembly!");
        defaultTypeCreators.Remove((num4, guid));
      }
      else
        typeIDsToRegister.Add(num4);
      typeCustomCreators.Add((num4, guid), customCreatorType);
    }

    void AddCreator((int ID, Guid Guid) typeIdentity, TCreatorInterface creator, bool recursive)
    {
      creators = creators ?? ApplicationServices.Container.GetService<TServiceInterface>().CastInterfaceToOtherInterface<TServiceInterface, ICreatorContainer>();
      creators.AddCreator((object) typeIdentity.ID, (object) creator, overrideIdExist);
      creators.AddCreator((object) typeIdentity.Guid, (object) creator, overrideIdExist);
      if (!(entity == AssemblyExtensions.EntityType.Object & recursive))
        return;
      RegisterChildTypes(typeIdentity.ID);

      void RegisterChildTypes(int parentTypeID)
      {
        foreach (int num in MetaDataHelperService.Instance.GetObjectTypeChildrenID(parentTypeID))
        {
          if (!typeIDsToRegister.Contains(num))
          {
            Guid objectTypeGuid = MetaDataHelperService.Instance.GetObjectTypeGuid(num);
            creators.AddCreator((object) num, (object) creator, overrideIdExist);
            creators.AddCreator((object) objectTypeGuid, (object) creator, overrideIdExist);
            RegisterChildTypes(num);
          }
        }
      }
    }
  }

  [NotEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static Guid GetEntityTypeGuid(AssemblyExtensions.EntityType entity, int id)
  {
    if (entity == AssemblyExtensions.EntityType.Object)
      return MetaDataHelperService.Instance.GetObjectTypeGuid(id);
    if (entity == AssemblyExtensions.EntityType.Relation)
      return MetaDataHelperService.Instance.GetRelationTypeGuid(id);
    throw new ArgumentOutOfRangeException(nameof (entity), (object) entity, (string) null);
  }

  [NotEmpty]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static int GetEntityTypeID(AssemblyExtensions.EntityType entity, Guid guid)
  {
    if (entity == AssemblyExtensions.EntityType.Object)
      return MetaDataHelperService.Instance.GetObjectTypeID(guid);
    if (entity == AssemblyExtensions.EntityType.Relation)
      return MetaDataHelperService.Instance.GetRelationTypeID(guid);
    throw new ArgumentOutOfRangeException(nameof (entity), (object) entity, (string) null);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static void RegisterEntityType(
    AssemblyExtensions.EntityType entity,
    [NotEmpty] Guid guid,
    [NotNull] Type type)
  {
    if (entity != AssemblyExtensions.EntityType.Object)
    {
      if (entity != AssemblyExtensions.EntityType.Relation)
        throw new ArgumentOutOfRangeException(nameof (entity), (object) entity, (string) null);
      DefaultDBEntityCreator.RegisterRelationType(guid, type, true);
    }
    else
      DefaultDBEntityCreator.RegisterObjectType(guid, type, true);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static void RegisterEntityCollectionType(
    AssemblyExtensions.EntityType entity,
    [NotEmpty] Guid guid,
    [NotNull] Type type)
  {
    if (entity != AssemblyExtensions.EntityType.Object)
    {
      if (entity != AssemblyExtensions.EntityType.Relation)
        throw new ArgumentOutOfRangeException(nameof (entity), (object) entity, (string) null);
      DefaultDBEntityCollectionCreator.RegisterRelationType(guid, type, true);
    }
    else
      DefaultDBEntityCollectionCreator.RegisterObjectType(guid, type, true);
  }

  private enum EntityType
  {
    Object,
    Relation,
  }

  private static class Debug
  {
    [Conditional("DEBUG")]
    [AssertionMethod]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CheckEntityTypeIdNotEmpty(
      AssemblyExtensions.EntityType entity,
      int id,
      [NotNull, NotWhitespace] string name)
    {
      if (entity != AssemblyExtensions.EntityType.Object && entity != AssemblyExtensions.EntityType.Relation)
        throw new ArgumentOutOfRangeException(nameof (entity), (object) entity, (string) null);
    }

    [Conditional("DEBUG")]
    [AssertionMethod]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CheckEntityTypeIdNotEmpty(
      AssemblyExtensions.EntityType entity,
      int? id,
      [NotNull, NotWhitespace] string name)
    {
      if (entity != AssemblyExtensions.EntityType.Object && entity != AssemblyExtensions.EntityType.Relation)
        throw new ArgumentOutOfRangeException(nameof (entity), (object) entity, (string) null);
    }
  }
}
