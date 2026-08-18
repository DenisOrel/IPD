
// Type: Intermech.Interfaces.SecurityRecord
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Структура , описывающая права доступа (используется при перекачке данных)
    /// </summary>
    [Serializable]
    public struct SecurityRecord(
      long categoryID,
      int categoryType,
      int rightId,
      object userId,
      int rightType,
      object ownerId,
      object beginDate,
      object endDate)
    {
      public long CategoryID = categoryID;
      public int CategoryType = categoryType;
      public int RightId = rightId;
      public object UserId = userId;
      public int RightType = rightType;
      public object OwnerId = ownerId;
      public object BeginDate = beginDate;
      public object EndDate = endDate;
    }
}
