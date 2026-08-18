// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.ObligatoryElementKeys
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;
using System;
using System.Collections.Concurrent;


namespace Intermech.Kernel;

internal static class ObligatoryElementKeys
{
  private static readonly ObligatoryElementKey objectKey = new ObligatoryElementKey(ObligatoryElementKind.Object, (object) "Object");
  private static readonly ConcurrentDictionary<string, ObligatoryElementKey> objectPropertyKeys = new ConcurrentDictionary<string, ObligatoryElementKey>();
  private static readonly ConcurrentDictionary<(string, int), ObligatoryElementKey> objectOptionsFlagKeys = new ConcurrentDictionary<(string, int), ObligatoryElementKey>();
  private static readonly ConcurrentDictionary<string, ObligatoryElementKey> attributeValueKeysByString = new ConcurrentDictionary<string, ObligatoryElementKey>();
  private static readonly ConcurrentDictionary<Guid, ObligatoryElementKey> attributeValueKeysByAttributeGuid = new ConcurrentDictionary<Guid, ObligatoryElementKey>();
  private static readonly ConcurrentDictionary<int, ObligatoryElementKey> attributeValueKeysByAttributeID = new ConcurrentDictionary<int, ObligatoryElementKey>();

  public static ObligatoryElementKey GetKeyForObject() => ObligatoryElementKeys.objectKey;

  public static ObligatoryElementKey GetKeyForObjectProperty(string propertyFieldName)
  {
    if (propertyFieldName == null)
      throw new ArgumentNullException(nameof (propertyFieldName));
    return ObligatoryElementKeys.objectPropertyKeys.GetOrAdd(propertyFieldName, (Func<string, ObligatoryElementKey>) (createArg => propertyFieldName.StartsWith("F_OPTIONS") ? new ObligatoryElementKey(ObligatoryElementKind.ObjectOptionsFlag, (object) Tuple.Create<string, int>("F_OPTIONS", int.Parse(propertyFieldName.Substring("F_OPTIONS".Length)))) : new ObligatoryElementKey(ObligatoryElementKind.ObjectProperty, (object) createArg)));
  }

  public static ObligatoryElementKey GetKeyForObjectOptionsFlag(int optionsFlag)
  {
    (string, int) key = ("F_OPTIONS", optionsFlag);
    return ObligatoryElementKeys.objectOptionsFlagKeys.GetOrAdd(key, (Func<(string, int), ObligatoryElementKey>) (createArg => new ObligatoryElementKey(ObligatoryElementKind.ObjectOptionsFlag, (object) Tuple.Create<string, int>("F_OPTIONS", optionsFlag))));
  }

  public static ObligatoryElementKey GetKeyForAttributePresence(int attributeID)
  {
    return new ObligatoryElementKey(ObligatoryElementKind.AttributeType, (object) attributeID);
  }

  public static ObligatoryElementKey GetKeyForAttributeProperty(
    int attributeID,
    string propertyFieldName)
  {
    if (propertyFieldName == null)
      throw new ArgumentNullException(nameof (propertyFieldName));
    if (!propertyFieldName.StartsWith("F_OPTIONS"))
      return new ObligatoryElementKey(ObligatoryElementKind.AttributeTypeProperty, (object) Tuple.Create<int, string>(attributeID, propertyFieldName));
    int num = int.Parse(propertyFieldName.Substring("F_OPTIONS".Length));
    return new ObligatoryElementKey(ObligatoryElementKind.AttributeTypeOptionsFlag, (object) Tuple.Create<int, string, int>(attributeID, "F_OPTIONS", num));
  }

  public static ObligatoryElementKey GetKeyForAttributeOptionsFlag(int attributeID, int optionsFlag)
  {
    return new ObligatoryElementKey(ObligatoryElementKind.AttributeTypeOptionsFlag, (object) Tuple.Create<int, string, int>(attributeID, "F_OPTIONS", optionsFlag));
  }

  public static ObligatoryElementKey GetKeyForAttributeValue(string fieldNameOrGuid)
  {
    if (fieldNameOrGuid == null)
      throw new ArgumentNullException(nameof (fieldNameOrGuid));
    return ObligatoryElementKeys.attributeValueKeysByString.GetOrAdd(fieldNameOrGuid, (Func<string, ObligatoryElementKey>) (createArg => new ObligatoryElementKey(ObligatoryElementKind.AttributeValue, (object) createArg)));
  }

  public static ObligatoryElementKey GetKeyForAttributeValue(Guid attributeGuid)
  {
    return ObligatoryElementKeys.attributeValueKeysByAttributeGuid.GetOrAdd(attributeGuid, (Func<Guid, ObligatoryElementKey>) (createArg => ObligatoryElementKeys.GetKeyForAttributeValue(UpdateScriptHelper.GetAttributeNodeNameFromGuid(createArg, false))));
  }

  public static ObligatoryElementKey GetKeyForAttributeValue(IDBAttributeType attributeType)
  {
    if (attributeType == null)
      throw new ArgumentNullException(nameof (attributeType));
    return ObligatoryElementKeys.attributeValueKeysByAttributeID.GetOrAdd(attributeType.AttributeID, (Func<int, ObligatoryElementKey>) (createArg => ObligatoryElementKeys.GetKeyForAttributeValue(attributeType.GUID)));
  }
}
