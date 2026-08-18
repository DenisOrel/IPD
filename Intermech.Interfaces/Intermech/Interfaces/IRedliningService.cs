
// Type: Intermech.Interfaces.IRedliningService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>служба для работы с настройками файлов замечаний</summary>
    public interface IRedliningService
    {
      /// <summary>
      /// Поколение настроек сервиса (используется для синхронизации серверных настроек с клиентскими кэшами)
      /// </summary>
      long Generation { get; }

      /// <summary>
      /// ID уровня продвижения, на котором нужно удалять файлы редлайнинга
      /// Intermech.Consts.UnknownLevelId -  если удалять не нужно
      /// </summary>
      int LevelID { get; }

      /// <summary>
      ///  Удалять ли файлы редлайнинга при переходе на уровень продвижения LevelID
      /// </summary>
      bool DeleteFiles { get; }

      /// <summary>Описания существующих файлов замечаний</summary>
      List<RedliningFiles> RedliningFilesSettings { get; }

      /// <summary>проверить, является ли файл редлайнингом</summary>
      /// <param name="mainFilePath">относительный путь основного файла</param>
      /// <param name="verifiableFilePath">относительный путь проверяемого файла</param>
      /// <returns></returns>
      bool IsRedliningFile(string mainFilePath, string verifiableFilePath);

      /// <summary>Изменить настройки для файлов замечаний</summary>
      /// <param name="settings">новые настройки </param>
      /// <param name="delete">новое значение флага Удалить файлы на уровне продвижения?</param>
      /// <param name="levelID">новое значние уровня продвижения на котором удалять файлы</param>
      /// <param name="sessionID">сесcия если вызов с сервера, и id сессии, если вызов с клиента</param>
      void ChangeRedliningSettings(
        List<RedliningFiles> settings,
        bool delete,
        int levelID,
        object sessionID);

      /// <summary>Ид. атрибута "Графические замечания к документам"</summary>
      int RedliningAttributeID { get; }
    }
}
