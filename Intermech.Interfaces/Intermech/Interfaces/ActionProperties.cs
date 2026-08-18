
// Type: Intermech.Interfaces.ActionProperties
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Структура, описывающая действие, которое можно применить к данному объекту или категории
    /// </summary>
    [Serializable]
    /// <summary>Создать экземпляр структуры</summary>
    /// <param name="name">Описание действия</param>
    /// <param name="actionID">Идентификатор действия</param>
    /// <param name="defaultAccess">Дано ли для него право по умолчанию всем юзерам</param>
    /// <param name="category">Категория действия (чтение, изменение, администрирование)</param>
    public struct ActionProperties(
      string name,
      ActionType actionID,
      bool defaultAccess,
      ActionCategory category)
    {
      /// <summary>Описание действия</summary>
      public string Name = name;
      /// <summary>Идентификатор действия</summary>
      public ActionType ActionID = actionID;
      /// <summary>Дано ли для него право по умолчанию всем юзерам</summary>
      public bool DefaultAccess = defaultAccess;
      /// <summary>
      /// Категория действия (чтение, изменение, администрирование)
      /// </summary>
      public ActionCategory Category = category;
      /// <summary>
      /// Задает список связанных действий, права доступа для которых нужно снимать при снятии
      /// прав для опции ActionID. Если таковых нет, ConnectedActions == null
      /// </summary>
      public ActionType[] ConnectedActions = (ActionType[]) null;
    }
}
