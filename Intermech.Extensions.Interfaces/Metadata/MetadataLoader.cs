// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.MetadataLoader
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Intermech.Metadata;

public abstract class MetadataLoader
{
  private const string InitHolderMethodName = "Init";
  [NotNull]
  [ItemNotNull]
  private static readonly IReadOnlyList<Type> _typesToInit = (IReadOnlyList<Type>) new Type[11]
  {
    typeof (Attributes),
    typeof (ObjectTypes),
    typeof (RelationTypes),
    typeof (LCLevel),
    typeof (LCStep),
    typeof (Role),
    typeof (User),
    typeof (UserGroup),
    typeof (SystemObject),
    typeof (PhysicalQuantity),
    typeof (MeasureUnit)
  };
  [NotNull]
  private static readonly InitOnceGuardian _initOnce = new InitOnceGuardian();

  protected internal static void Init([NotNull] IUserSession session)
  {
    MetadataLoader._initOnce.Invoke((Action) (() => MetadataLoader.InitMetadata<MetadataLoader>(session)));
  }

  protected static void InitMetadata<TMetadataLoader>([NotNull] IUserSession session) where TMetadataLoader : MetadataLoader
  {
    Assembly assembly = typeof (TMetadataLoader).Assembly;
    string str = typeof (TMetadataLoader).Namespace;
    object[] objArray = (object[]) null;
    foreach (Type memberInfo in (IEnumerable<Type>) MetadataLoader._typesToInit)
    {
      Type type = assembly.GetType($"{str}.{memberInfo.Name}", false);
      if (type != (Type) null)
      {
        if (memberInfo.HasAttribute<InitFieldsWithSessionAttribute>() || type.HasAttribute<InitFieldsWithSessionAttribute>())
        {
          MetadataLoader.InitMetadataObjects(session, type);
        }
        else
        {
          MethodInfo method = type.GetMethod("Init", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, CallingConventions.Any, new Type[1]
          {
            typeof (IUserSession)
          }, (ParameterModifier[]) null);
          if (method != (MethodInfo) null)
          {
            MethodInfo methodInfo = method;
            object[] parameters = objArray;
            if (parameters == null)
              parameters = objArray = new object[1]
              {
                (object) session
              };
            methodInfo.Invoke((object) null, parameters);
          }
        }
      }
    }
  }

  private static void LoadMetadataEntityTypes<TIpsMetadataEntityType>(
    [NotNull] Type idsHolderClass,
    [NotNull] Action<TIpsMetadataEntityType> initMetadataEntityMethod)
    where TIpsMetadataEntityType : IpsMetadataEntityType
  {
    FieldInfo[] fields = idsHolderClass.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
    if (fields.Length == 0)
      return;
    Type c = typeof (TIpsMetadataEntityType);
    foreach (FieldInfo fieldInfo in fields)
    {
      if (fieldInfo.FieldType.IsSubclassOf(c) && fieldInfo.GetValue((object) null) is TIpsMetadataEntityType metadataEntityType)
        initMetadataEntityMethod(metadataEntityType);
    }
  }

  private static void InitMetadataObjects([NotNull] IUserSession session, [NotNull] Type idsHolderClass)
  {
    FieldInfo[] fields = idsHolderClass.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
    if (fields.Length == 0)
      return;
    foreach (FieldInfo fieldInfo in fields)
    {
      if (fieldInfo.FieldType.GetInterface("IInitWithSession") != (Type) null && fieldInfo.GetValue((object) null) is IInitWithSession initWithSession)
        initWithSession.Init(session);
    }
  }

  [Conditional("DEBUG")]
  private static void CheckClassDontHaveInstanceFieldsAndProperties([NotNull] Type idsHolderClass)
  {
    PropertyInfo[] properties = idsHolderClass.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance);
    if (properties.Length != 0)
    {
      if (properties.Length == 1)
        throw new Exception($"Контейнер идентификаторов {idsHolderClass.FullName} не должен иметь нестатических свойств! {Environment.NewLine}{$"Свойство \"{properties[0]}\" не является статическим!"}");
      throw new Exception($"Контейнер идентификаторов {idsHolderClass.FullName} не должен иметь нестатических свойств! {Environment.NewLine}Свойства \"{string.Join(", ", ((IEnumerable<PropertyInfo>) properties).Select<PropertyInfo, string>((Func<PropertyInfo, string>) (prop => prop.Name)))}\" не являются статическими!");
    }
    FieldInfo[] fields = idsHolderClass.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance);
    if (fields.Length == 0)
      return;
    if (fields.Length == 1)
      throw new Exception($"Контейнер идентификаторов {idsHolderClass.FullName} не должен иметь нестатических полей! {Environment.NewLine}{$"Поле \"{fields[0]}\" не является статическим!"}");
    throw new Exception($"Контейнер идентификаторов {idsHolderClass.FullName} не должен иметь нестатических полей! {Environment.NewLine}Поле \"{string.Join(", ", ((IEnumerable<FieldInfo>) fields).Select<FieldInfo, string>((Func<FieldInfo, string>) (field => field.Name)))}\" не являются статическими!");
  }
}
