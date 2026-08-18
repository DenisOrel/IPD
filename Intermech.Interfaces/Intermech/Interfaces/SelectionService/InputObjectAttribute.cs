
// Type: Intermech.Interfaces.SelectionService.InputObjectAttribute
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.SelectionService
{
    /// <summary>
    /// Класс для представления типа атрибута, привязанного к определенному типу объекта
    /// </summary>
    [Serializable]
    public class InputObjectAttribute
    {
      public Guid ObjectGUID = Guid.Empty;
      public Guid AttributeGUID = Guid.Empty;

      public object GetAttributeValueByObjectID(
        IUserSession userSession,
        long objectID,
        bool firstValueOnly)
      {
        object attributeValueByObjectId = (object) null;
        if (userSession != null && objectID != 0L)
        {
          IDBObject dbObject = userSession.GetObject(objectID);
          if (dbObject != null && (this.ObjectGUID.Equals(Guid.Empty) || userSession.GetObjectType(this.ObjectGUID).ObjectType == dbObject.ObjectType) && !this.AttributeGUID.Equals(Guid.Empty))
          {
            object[] valuesByGuid = dbObject.GetValuesByGuid(this.AttributeGUID, false);
            if (valuesByGuid != null)
            {
              if (!(valuesByGuid.Length == 1 | firstValueOnly))
                return (object) valuesByGuid;
              attributeValueByObjectId = valuesByGuid[0];
            }
          }
        }
        return attributeValueByObjectId;
      }
    }
}
