using Intermech.Remoting.Ipc;
using System.Collections.Generic;


namespace Intermech.PdfPrintCenter.Connector
{
    /// <summary>
    /// Интерфейс головного объекта PDM-системы, который используется центром печати для
    /// взаимодействия с PDM-системой. Реализация должна быть thread safe.
    /// </summary>
    public interface IPDMSystem : IReliableIpcObject
    {
        /// <summary>
        /// Позволяет выбрать макет из списка сохраненных в базе данных
        /// </summary>
        /// <returns>id выбранного макета (Int64) либо null, если элемент не был выбран</returns>
        object ChooseLayout();

        /// <summary>Возвращает список id всех макетов в базе данных</summary>
        /// <returns>Список id макетов</returns>
        List<object> GetLayoutsId();

        /// <summary>
        /// Загружает информацию о макете из базы данных по его id
        /// </summary>
        /// <param name="layoutId">id макета</param>
        /// <returns>Имя макета и информация о нем в xml-формате либо null, если объект не найден</returns>
        PDMLayoutInfo LoadLayout(object layoutId);

        /// <summary>
        /// Сохраняет в базу данных информацию о макете <paramref name="layoutInfo" />
        /// </summary>
        /// <param name="layoutInfo">Структура, содержащая имя макета и информацию о нем в формате</param>
        /// <param name="layoutId">id макета либо null, если требуется создать новый макет</param>
        /// <returns>id сохраненного макета</returns>
        object SaveLayout(PDMLayoutInfo layoutInfo, object layoutId = null);

        /// <summary>
        /// Получает из базы данных настройки принтеров в виде xml-документа
        /// </summary>
        /// <returns>Настройки принтеров в виде xml-документа</returns>
        string GetPrintersSettings();

        /// <summary>
        /// Заносит в базу данных настройки принтеров в виде xml-документа
        /// </summary>
        /// <param name="xmlPrintersSettings">Настройки принтеров в виде xml-документа </param>
        void PutPrintersSettings(string xmlPrintersSettings);

        /// <summary>
        /// Получает из базы данных настройки водяного знака в виде xml-документа
        /// </summary>
        /// <returns>Настройки водяного знака в виде xml-документа</returns>
        string GetWatermakSettings();

        /// <summary>
        /// Заносит в базу данных настройки водяного знака в виде xml-документа
        /// </summary>
        /// <param name="xmlWatermarkSettings">Настройки водяного знака в виде xml-документа </param>
        void PutWatermarkSettings(string xmlWatermarkSettings);

        /// <summary>
        /// Возвращает имя пользователя, выводящего на печать документ
        /// </summary>
        /// <returns>Имя пользователя, выводящего на печать документ</returns>
        string GetCurrentUserName();

        /// <summary>
        /// Получает из базы данных настройки основного окна в виде xml-документа
        /// </summary>
        /// <returns>Настройки основного окна в виде xml-документа</returns>
        string GetWindowSettings();

        /// <summary>
        /// Заносит в базу данных настройки основного окна в виде xml-документа
        /// </summary>
        /// <param name="xmlWindowSettings">Настройки основного окна в виде xml-документа </param>
        void PutWindowSettings(string xmlWindowSettings);
    }
}
