
// Type: Intermech.Interfaces.ObjectsVisibility
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Search;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Interfaces
{
    /// <summary>
    /// В классе хранятся идентификаторы ролей, групп и пользователей, а также их права на видимость объекта
    /// </summary>
    [Serializable]
    public sealed class ObjectsVisibility : IAssignable, ICloneable, IDatabaseSync
    {
      /// <summary>
      /// Коллекция с правами видимости к объекту
      /// [(Int64)Идентификатор роли, группы, пользователя] = [Права видимости]
      /// </summary>
      private Dictionary<long, ObjectsVisibilityFlags> _rights = new Dictionary<long, ObjectsVisibilityFlags>();

      public static bool IsAllowableObjectTypeID(int objectTypeID)
      {
        return objectTypeID == Constants.UserObjectTypeID || MetaDataHelper.GetObjectTypeChildrenIDRecursive(Constants.UserObjectTypeID).Contains(objectTypeID) || objectTypeID == Constants.UserGroupObjectTypeID || MetaDataHelper.GetObjectTypeChildrenIDRecursive(Constants.UserGroupObjectTypeID).Contains(objectTypeID) || objectTypeID == Constants.RoleObjectTypeID || MetaDataHelper.GetObjectTypeChildrenIDRecursive(Constants.RoleObjectTypeID).Contains(objectTypeID);
      }

      /// <summary>
      /// Коллекция с правами видимости к объекту
      /// [(Int64)Идентификатор роли, группы, пользователя] = [Права видимости]
      /// </summary>
      public Dictionary<long, ObjectsVisibilityFlags> Rights
      {
        [DebuggerStepThrough] get => this._rights;
      }

      /// <summary>Создать пустой экземпляр класса</summary>
      public ObjectsVisibility()
      {
      }

      /// <summary>
      /// Создать экземпляр класса, заполнить его поля значениями, извлечёнными из указанной строки
      /// </summary>
      /// <param name="data">Строка в кодированном виде</param>
      public ObjectsVisibility(string data) => ObjectsVisibilityHelper.ParseString(this._rights, data);

      public bool IsHidden(long id)
      {
        return this._rights.ContainsKey(id) && this._rights[id].HasFlag((Enum) ObjectsVisibilityFlags.Hidden);
      }

      public bool IsVisible(long id)
      {
        return this._rights.ContainsKey(id) && this._rights[id].HasFlag((Enum) ObjectsVisibilityFlags.Visible);
      }

      /// <summary>Сравнить с указанным объектом</summary>
      /// <param name="obj">Объект для сравнения</param>
      /// <returns>true, если равны</returns>
      public override bool Equals(object obj)
      {
        return obj is ObjectsVisibility objectsVisibility && objectsVisibility._rights.Count == this._rights.Count && this.ToString().Equals(objectsVisibility.ToString());
      }

      /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
      /// <returns>32-битный хэш-код экземпляра класса</returns>
      public override int GetHashCode() => this.ToString().GetHashCode();

      /// <summary>Получить строковое представление экземпляра класса</summary>
      /// <returns>Строковое представление экземпляра класса</returns>
      public override string ToString() => ObjectsVisibilityHelper.ToString(this._rights);

      /// <summary>Очистить поля класса</summary>
      public void Clear() => this._rights.Clear();

      /// <summary>
      /// Скопировать в текущий объект поля из другого объекта.
      /// Допускается подавать в качестве source объекты типов ObjectsVisibility, String и
      /// Dictionary[Int64, ObjectsVisibilityFlags]
      /// </summary>
      /// <param name="source">Объект-источник. Допускается подавать в качестве source объекты
      /// типов ObjectsVisibility, String и Dictionary[Int64, ObjectsVisibilityFlags]</param>
      public void Assign(object source)
      {
        if (this == source)
          return;
        this.Clear();
        ObjectsVisibility objectsVisibility = source as ObjectsVisibility;
        Dictionary<long, ObjectsVisibilityFlags> dictionary = source as Dictionary<long, ObjectsVisibilityFlags>;
        if (objectsVisibility != null)
          ObjectsVisibilityHelper.ParseString(this._rights, objectsVisibility.ToString());
        else if (dictionary != null)
        {
          foreach (KeyValuePair<long, ObjectsVisibilityFlags> keyValuePair in dictionary)
            this._rights.Add(keyValuePair.Key, keyValuePair.Value);
        }
        else
        {
          if (!(source is string))
            return;
          ObjectsVisibilityHelper.ParseString(this._rights, (string) source);
        }
      }

      /// <summary>Создать точную копию экземпляра класса</summary>
      /// <returns>Точная копия экземпляра класса</returns>
      public object Clone() => (object) new ObjectsVisibility(this.ToString());

      /// <summary>
      /// Выполнить синхронизацию внутренних коллекций с базой данных
      /// </summary>
      /// <param name="session">Ссылка на сессию, в рамках которой выполняется работа с базой данных и сервером приложений</param>
      public void SyncObjectsData(IUserSession session)
      {
        if (this._rights.Count == 0 || session == null)
          return;
        List<long> longList = new List<long>();
        foreach (KeyValuePair<long, ObjectsVisibilityFlags> right in this._rights)
        {
          IDBObject dbObject = session.GetObject(right.Key, false);
          if (dbObject == null)
            longList.Add(right.Key);
          if (!ObjectsVisibility.IsAllowableObjectTypeID(dbObject.ObjectType))
            longList.Add(right.Key);
        }
        for (int index = 0; index < longList.Count; ++index)
          this._rights.Remove(longList[index]);
      }

      /// <summary>Вернуть список объектов из коллекции Rights</summary>
      /// <param name="session">Сессия</param>
      /// <returns>Список объектов из коллекции Rights</returns>
      public List<MyObjectElement> GetObjects(IUserSession session)
      {
        List<MyObjectElement> objects = new List<MyObjectElement>(this._rights.Count);
        if (this._rights.Count == 0 || session == null)
          return objects;
        foreach (KeyValuePair<long, ObjectsVisibilityFlags> right in this._rights)
        {
          MyObjectElement objectInfo = ObjectsVisibilityHelper.GetObjectInfo(session, right.Key);
          if (objectInfo != null && ObjectsVisibility.IsAllowableObjectTypeID(objectInfo.ObjectType))
          {
            objectInfo.Tag = (object) right.Value;
            objects.Add(objectInfo);
          }
        }
        objects.Sort();
        return objects;
      }

      /// <summary>
      /// Заполнить внутреннюю коллекцию данными из указанного списка объектов
      /// </summary>
      /// <param name="data">Список описаний объектов, а также их настройки видимости</param>
      public void SetObjects(List<MyObjectElement> data)
      {
        this.Clear();
        if (data == null || data.Count == 0)
          return;
        for (int index = 0; index < data.Count; ++index)
        {
          MyObjectElement myObjectElement = data[index];
          if (myObjectElement != null && myObjectElement.ObjectID != 0L && !this._rights.ContainsKey(myObjectElement.ObjectID) && myObjectElement.Tag is ObjectsVisibilityFlags)
            this._rights.Add(myObjectElement.ObjectID, (ObjectsVisibilityFlags) myObjectElement.Tag);
        }
      }
    }
}
