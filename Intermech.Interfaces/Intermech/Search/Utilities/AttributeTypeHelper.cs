
// Type: Intermech.Search.Utilities.AttributeTypeHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;


namespace Intermech.Search.Utilities
{
    /// <summary>Хелпер типов атрибутов</summary>
    public static class AttributeTypeHelper
    {
      /// <summary>Проверить идентификатор типа атрибута</summary>
      /// <param name="attributeTypeID">Идентификатор типа атрибута</param>
      /// <returns></returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool IsUnknownAttributeTypeID(int attributeTypeID)
      {
        return attributeTypeID == 0 || attributeTypeID == -10000;
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool IsSystemAttributeTypeID(int attributeTypeID)
      {
        return ObligatoryObjectAttributesHelper.IsObligatoryAttribute(attributeTypeID);
      }

      public static bool IsAnyUnknownAttributeTypeID(IEnumerable<int> attributeTypeIds)
      {
        return attributeTypeIds != null ? attributeTypeIds.Any<int>((Func<int, bool>) (o => AttributeTypeHelper.IsUnknownAttributeTypeID(o))) : throw new ArgumentNullException(nameof (attributeTypeIds));
      }

      public static bool IsFileAttributeType(int attributeTypeID)
      {
        IMSAttributeType imsAttributeType = !AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID) ? MetaDataHelper.GetAttributeType(attributeTypeID) : throw new ArgumentException();
        return imsAttributeType != null && imsAttributeType.RealFieldType == FieldTypes.ftFile;
      }

      public static bool IsStringAttributeType(int attributeTypeID)
      {
        IMSAttributeType imsAttributeType = !AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID) ? MetaDataHelper.GetAttributeType(attributeTypeID) : throw new ArgumentException();
        return imsAttributeType != null && imsAttributeType.RealFieldType == FieldTypes.ftString;
      }

      public static int ConvertToAttributeTypeID(object @object)
      {
        if (@object == null)
          throw new ArgumentNullException("@object");
        if (@object is int)
          return (int) @object;
        if (@object is ObligatoryObjectAttributes)
          return (int) @object;
        if (@object is Guid)
          return MetaDataHelper.GetAttributeTypeID((Guid) @object);
        if (!(@object is string))
          throw new Exception();
        IMSAttributeType imsAttributeType = (MetaDataHelper.GetAttributeTypesList() ?? new List<IMSAttributeType>()).FirstOrDefault<IMSAttributeType>((Func<IMSAttributeType, bool>) (o => o.Name == (string) @object));
        if (imsAttributeType != null)
          return imsAttributeType.AttributeID;
        ObligatoryObjectAttributes result;
        if (Enum.TryParse<ObligatoryObjectAttributes>((string) @object, out result))
          return (int) result;
        throw new Exception();
      }

      public static bool IsSingleValueOrSingleVaueFromList(int attributeTypeID)
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeTypeID);
        return attributeType.MultiValueMode == MultiValueModes.SingleValue || attributeType.MultiValueMode == MultiValueModes.SingleValueFromList;
      }

      public static FieldTypes GetFieldTypeForObligatoryObjectAttribute(
        ObligatoryObjectAttributes obligatoryObjectAttribute)
      {
        if (obligatoryObjectAttribute <= ObligatoryObjectAttributes.F_PROJECT_ID)
        {
          if (obligatoryObjectAttribute == ObligatoryObjectAttributes.F_USER_ID || obligatoryObjectAttribute == ObligatoryObjectAttributes.F_PROJECT_ID)
            return FieldTypes.ftObjectLink;
        }
        else if (obligatoryObjectAttribute == ObligatoryObjectAttributes.F_OWNER_ID || obligatoryObjectAttribute == ObligatoryObjectAttributes.F_CHKOUT_BY)
          return FieldTypes.ftObjectLink;
        return ObligatoryObjectAttributesHelper.GetDataType(obligatoryObjectAttribute);
      }

      public static bool AllowEmpty(int attributeTypeID)
      {
        IMSAttributeType imsAttributeType = !AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID) ? MetaDataHelper.GetAttributeType(attributeTypeID) : throw new ArgumentException();
        return imsAttributeType != null && !imsAttributeType.Options.HasFlag((Enum) AttributeOptions.DisableNulls);
      }

      public static bool IsManualEditingDisabled4ObjectType(int objectTypeID, int attributeTypeID)
      {
        if (ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeID))
          throw new ArgumentException();
        IMSAttribute4ObjectType attribute4ObjectType = !AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID) ? MetaDataHelper.GetAttribute4ObjectType(objectTypeID, attributeTypeID) : throw new ArgumentException();
        return attribute4ObjectType != null && attribute4ObjectType.Options.HasFlag((Enum) AttributeOptions.DisableManualEdit);
      }

      public static bool IsManualEditingDisabled4RelationType(int relationTypeID, int attributeTypeID)
      {
        if (RelationTypeHelper.IsUnknownRelationTypeID(relationTypeID))
          throw new ArgumentException();
        IMSAttribute4RelationType attribute4RelationType = !AttributeTypeHelper.IsUnknownAttributeTypeID(attributeTypeID) ? MetaDataHelper.GetAttribute4RelationType(relationTypeID, attributeTypeID) : throw new ArgumentException();
        return attribute4RelationType != null && attribute4RelationType.Options.HasFlag((Enum) AttributeOptions.DisableManualEdit);
      }
    }
}
