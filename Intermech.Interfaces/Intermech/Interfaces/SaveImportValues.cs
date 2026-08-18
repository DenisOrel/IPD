
// Type: Intermech.Interfaces.SaveImportValues
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Структура, хранящая значения у метаданных для их дальнейшего
    /// </summary>
    [Serializable]
    public struct SaveImportValues
    {
      public int ObjectTypeID;
      public int RelationTypeID;
      public int AttributeTypeID;
      public object Value;
      public object MeasuredDefaultVAlue;

      public SaveImportValues(int attributeTypeID, int objectTypeID, int relationTypeID, object value)
      {
        this.AttributeTypeID = attributeTypeID;
        this.ObjectTypeID = objectTypeID;
        this.RelationTypeID = relationTypeID;
        this.Value = value;
        this.MeasuredDefaultVAlue = (object) null;
      }

      public SaveImportValues(
        int attributeTypeID,
        int objectTypeID,
        int relationTypeID,
        object value,
        object measuredDefaultVAlue)
      {
        this.AttributeTypeID = attributeTypeID;
        this.ObjectTypeID = objectTypeID;
        this.RelationTypeID = relationTypeID;
        this.Value = value;
        this.MeasuredDefaultVAlue = measuredDefaultVAlue;
      }

      public SaveImportValues(int attributeTypeID, object value)
      {
        this.AttributeTypeID = attributeTypeID;
        this.ObjectTypeID = -1;
        this.RelationTypeID = -1;
        this.Value = value;
        this.MeasuredDefaultVAlue = (object) null;
      }
    }
}
