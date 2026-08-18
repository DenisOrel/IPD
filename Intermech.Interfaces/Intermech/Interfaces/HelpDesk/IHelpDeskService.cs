
// Type: Intermech.Interfaces.HelpDesk.IHelpDeskService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces.HelpDesk
{
    public interface IHelpDeskService
    {
      /// <summary>Проверка наличия заявки в HelpDesk</summary>
      /// <param name="workOrderId">Идентификатор заявки</param>
      /// <returns></returns>
      bool ExistWorkOrder(long workOrderId);

      /// <summary>
      /// Проверка наличия прикрепленных файлов для заявки HelpDesk
      /// </summary>
      /// <param name="workOrderId">Идентификатор заявки</param>
      /// <returns></returns>
      bool ExistAttachment(long workOrderId);

      /// <summary>Получение данных заявки с HelpDesk</summary>
      /// <param name="workOrderId">Идентификатор заявки</param>
      /// <param name="withAttachment"></param>
      /// <returns></returns>
      DataTable HelpDeskDataTable(long workOrderId, bool withAttachment);

      /// <summary>Получаем прикрепленный файл</summary>
      /// <param name="attachmentId">Идентификатор прикрепленного файла</param>
      /// <param name="key">Ключ</param>
      /// <param name="userName">Логин для входа в HelpDesk</param>
      /// <param name="userPassword">Пароль для входа в HelpDesk</param>
      /// <returns></returns>
      byte[] GetFile(int attachmentId, string key, string userName, string userPassword);

      /// <summary>Проверяем аутентификацию для HelpDesk</summary>
      /// <param name="userName">Имя пользователя в системе HelpDesk</param>
      /// <param name="userPassword">Пароль в системе HelpDesk</param>
      /// <returns>Словарь "статус проверки":"строка с ошибкой"</returns>
      Dictionary<bool, string> AuthenticationHelpDesk(string userName, string userPassword);
    }
}
