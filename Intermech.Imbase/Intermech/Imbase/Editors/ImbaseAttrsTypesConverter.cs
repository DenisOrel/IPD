// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.ImbaseAttrsTypesConverter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Editors;

internal static class ImbaseAttrsTypesConverter
{
  private const int TYPES_COUNT = 6;
  private static List<FieldTypes> _lstTypes = new List<FieldTypes>((IEnumerable<FieldTypes>) new FieldTypes[6]
  {
    FieldTypes.ftBoolean,
    FieldTypes.ftDateTime,
    FieldTypes.ftString,
    FieldTypes.ftInteger,
    FieldTypes.ftDouble,
    FieldTypes.ftMeasured
  });
  private static ImbaseAttrsTypesConverter.MatrixValue[,] _ReplaceabilityMatrix = new ImbaseAttrsTypesConverter.MatrixValue[6, 6]
  {
    {
      ImbaseAttrsTypesConverter.MatrixValue.Yes,
      ImbaseAttrsTypesConverter.MatrixValue.No,
      ImbaseAttrsTypesConverter.MatrixValue.Part,
      ImbaseAttrsTypesConverter.MatrixValue.No,
      ImbaseAttrsTypesConverter.MatrixValue.No,
      ImbaseAttrsTypesConverter.MatrixValue.No
    },
    {
      ImbaseAttrsTypesConverter.MatrixValue.No,
      ImbaseAttrsTypesConverter.MatrixValue.Yes,
      ImbaseAttrsTypesConverter.MatrixValue.Part,
      ImbaseAttrsTypesConverter.MatrixValue.No,
      ImbaseAttrsTypesConverter.MatrixValue.No,
      ImbaseAttrsTypesConverter.MatrixValue.No
    },
    {
      ImbaseAttrsTypesConverter.MatrixValue.Part,
      ImbaseAttrsTypesConverter.MatrixValue.Part,
      ImbaseAttrsTypesConverter.MatrixValue.Yes,
      ImbaseAttrsTypesConverter.MatrixValue.Part,
      ImbaseAttrsTypesConverter.MatrixValue.Part,
      ImbaseAttrsTypesConverter.MatrixValue.Part
    },
    {
      ImbaseAttrsTypesConverter.MatrixValue.No,
      ImbaseAttrsTypesConverter.MatrixValue.No,
      ImbaseAttrsTypesConverter.MatrixValue.Part,
      ImbaseAttrsTypesConverter.MatrixValue.Yes,
      ImbaseAttrsTypesConverter.MatrixValue.Part,
      ImbaseAttrsTypesConverter.MatrixValue.Part
    },
    {
      ImbaseAttrsTypesConverter.MatrixValue.No,
      ImbaseAttrsTypesConverter.MatrixValue.No,
      ImbaseAttrsTypesConverter.MatrixValue.Part,
      ImbaseAttrsTypesConverter.MatrixValue.Part,
      ImbaseAttrsTypesConverter.MatrixValue.Yes,
      ImbaseAttrsTypesConverter.MatrixValue.Yes
    },
    {
      ImbaseAttrsTypesConverter.MatrixValue.No,
      ImbaseAttrsTypesConverter.MatrixValue.No,
      ImbaseAttrsTypesConverter.MatrixValue.Part,
      ImbaseAttrsTypesConverter.MatrixValue.Part,
      ImbaseAttrsTypesConverter.MatrixValue.Yes,
      ImbaseAttrsTypesConverter.MatrixValue.Yes
    }
  };

  internal static ImbaseAttrsTypesConverter.MatrixValue CompleteReplaceability(
    FieldTypes sourceType,
    FieldTypes targetType)
  {
    int index1 = ImbaseAttrsTypesConverter._lstTypes.IndexOf(sourceType);
    int index2 = ImbaseAttrsTypesConverter._lstTypes.IndexOf(targetType);
    return index1 < 0 || index2 < 0 ? ImbaseAttrsTypesConverter.MatrixValue.No : ImbaseAttrsTypesConverter._ReplaceabilityMatrix[index1, index2];
  }

  internal static bool IsReplaceableType(FieldTypes sourceType)
  {
    return ImbaseAttrsTypesConverter._lstTypes.Contains(sourceType);
  }

  internal static List<FieldTypes> ListReplaceableTypes(FieldTypes sourceType)
  {
    List<FieldTypes> fieldTypesList = new List<FieldTypes>(6);
    int index1 = ImbaseAttrsTypesConverter._lstTypes.IndexOf(sourceType);
    if (index1 < 0)
      return (List<FieldTypes>) null;
    for (int index2 = 0; index2 < 6; ++index2)
    {
      switch (ImbaseAttrsTypesConverter._ReplaceabilityMatrix[index1, index2])
      {
        case ImbaseAttrsTypesConverter.MatrixValue.Part:
        case ImbaseAttrsTypesConverter.MatrixValue.Yes:
          fieldTypesList.Add(ImbaseAttrsTypesConverter._lstTypes[index2]);
          break;
      }
    }
    return fieldTypesList;
  }

  internal enum MatrixValue
  {
    No = -1, // 0xFFFFFFFF
    Part = 0,
    Yes = 1,
  }
}
