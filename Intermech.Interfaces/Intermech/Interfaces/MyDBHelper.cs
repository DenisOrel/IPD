
// Type: Intermech.Interfaces.MyDBHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Вспомогательный статический класс для вытягивания разной информации из базы данных
    /// </summary>
    public static class MyDBHelper
    {
      /// <summary>
      /// По идентификатору связи (F_PRJLINK_ID) вернуть тип этой связи (F_RELATION_TYPE)
      /// </summary>
      /// <param name="session">Пользовательская сессия, из которой будет получаться информация</param>
      /// <param name="RelationID">ID связи (F_PRJLINK_ID)</param>
      /// <param name="RelationTypeName">Название типа связи</param>
      /// <returns>Вернёт ID типа связи (F_RELATION_TYPE) или -1, если связь не найдена</returns>
      public static int GetRelationTypeByID(
        IUserSession session,
        long RelationID,
        out string RelationTypeName)
      {
        RelationTypeName = string.Empty;
        if (session == null || RelationID == 0L)
          return -1;
        IDBRelation relation = session.GetRelation(RelationID, false);
        if (relation == null)
          return -1;
        IDBRelationType dbRelationType;
        try
        {
          dbRelationType = session.GetRelationType(relation.RelationType);
        }
        catch
        {
          dbRelationType = (IDBRelationType) null;
        }
        if (dbRelationType != null)
          RelationTypeName = dbRelationType.Description;
        return relation.RelationType;
      }
    }
}
