
// Type: Intermech.Interfaces.IImportUsersProfile
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с сервером импорта профилей пользователей IImportUsersProfile.
    /// </summary>
    public interface IImportUsersProfile
    {
      /// <summary>
      /// Копировать профили пользователей c идентификаторами sourceUsersIDs
      /// в профили юзеров с идентификаторами destUsersIDs
      /// </summary>
      /// <param name="sourceUserIDs">Идентификаторы юзеров из которых брать профили</param>
      /// <param name="destUserIDs">Идентификаторы юзеров в которые записывать профили</param>
      /// <param name="throwExceptionIfEqual">Генерировать exception, если идентификаторы равны</param>
      void CopyProfile(long[] sourceUserIDs, long[] destUserIDs, bool throwExceptionIfEqual);

      /// <summary>
      /// Копировать профиль пользователя sourceUserID пользователю destUserID
      /// </summary>
      /// <param name="sourceUserID">Идентификатор пользователя из которого брать профиль</param>
      /// <param name="destUserID">Идентификатор пользователя в который писать профиль</param>
      /// <param name="throwExceptionIfEqual">Генерировать exception, если идентификаторы равны</param>
      void CopyProfile(long sourceUserID, long destUserID, bool throwExceptionIfEqual);
    }
}
