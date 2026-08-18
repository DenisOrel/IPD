using Intermech.Remoting.Ipc;
using System.Collections.Generic;


namespace Intermech.PdfPrintCenter.Connector
{
    /// <summary>
    /// Интерфейс головного объекта центра печати, который используется другими приложениями для
    /// взаимодействия с центром печати. Реализация должна быть thread safe.
    /// </summary>
    public interface IPrintCenterSystem : IReliableIpcObject
    {
        /// <summary>Закрывает центр печати PDF</summary>
        void ClosePrintCenter();

        /// <summary>
        /// Передает центру печати параметры подключения к PDM-системе, работающей в связке с центром печати.
        /// Метод вызывается PDM-системой после установления подключения к центру печати.
        /// </summary>
        /// <param name="connectionInfo">Параметры подключения к PDM-системе</param>
        /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="connectionInfo" /> содержит null</exception>
        void SetPDMSystem(IpcConnectionInfo connectionInfo);

        /// <summary>
        /// Добавляет несколько документов в рабочую область центра печати.
        /// </summary>
        /// <param name="documents">Массив добавляемых документов PDM-системы</param>
        /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="documents" /> содержит null</exception>
        void AddDocuments(List<PDMDocumentInfo> documents);
    }
}
