using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;


namespace Intermech.PdfPrintCenter.Connector
{
    /// <summary>Описатель документа PDM-системы.</summary>
    [Serializable]
    public class PDMDocumentInfo : ISerializable
    {
        public PDMDocumentInfo(SerializationInfo info, StreamingContext context)
        {
            this.ObjectName = (string)info.GetValue(nameof(ObjectName), typeof(string));
            this.FilePaths = (List<string>)info.GetValue(nameof(FilePaths), typeof(List<string>));
        }

        /// <summary>
        /// Добавляет новый документ в список документов, доступных для печати.
        /// </summary>
        /// <param name="objectName">Заголовок документа в PDM-системе</param>
        /// <param name="filePaths">Путь к файлу документа на локальном диске</param>
        /// <exception cref="T:System.ArgumentException">параметр <paramref name="objectName" /> не задан или равен null; параметр <paramref name="filePaths" /> не задан или равен null;</exception>
        public PDMDocumentInfo(string objectName, List<string> filePaths)
        {
            if (string.IsNullOrEmpty(objectName))
                throw new ArgumentException("Не задано имя объекта в PDM-системе.", nameof(objectName));
            if (filePaths == null || !filePaths.Any())
                throw new ArgumentException("Не заданы пути к файлам документа на локальном диске.", nameof(filePaths));
            this.ObjectName = objectName;
            this.FilePaths = filePaths;
        }

        /// <summary>Возвращает заголовок документа в PDM-системе.</summary>
        public string ObjectName { get; }

        /// <summary>Возвращает путь к файлу документа на локальном диске.</summary>
        public List<string> FilePaths { get; }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ObjectName", (object)this.ObjectName, this.ObjectName.GetType());
            info.AddValue("FilePaths", (object)this.FilePaths, this.FilePaths.GetType());
        }
    }
}
