
// Type: Intermech.Interfaces.AttributeTypePropertiesValidator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Структура для проверки правильности вводимых значений (и заполнения значений
    /// по умолчанию) для атрибута типа FieldType. Заполняется сервером.
    /// </summary>
    [Serializable]
    public struct AttributeTypePropertiesValidator
    {
      public FieldTypes FieldType;
      public string Name;
      public string ShortName;
      public string Alias;
      public string Note;
      public object DefaultValue;
      public MultiValueModes[] MultiValueMode;
      public ComputeValueModes[] Computed;
      public long[] SizeType;
      public object Formula;
      public UniqueValueModes[] Unique;
      public int LevelID;
      public string LanguageID;
      public string AreaID;
      public Guid AttributeGuid;
      public InheritModes[] InheritMode;
      public RequiredModes[] RequiredMode;
      public DataTable PossibleValuesTable;
      public OptimizationModes[] OptimizationMode;
      public bool IsContent;
      public AttributeOptions Options;
      public string Mask;
      public int MasterAttributeID;
      public int SourceAttributeID;
    }
}
