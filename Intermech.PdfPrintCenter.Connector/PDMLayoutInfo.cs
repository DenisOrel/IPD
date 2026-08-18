using System;
using System.Runtime.Serialization;


namespace Intermech.PdfPrintCenter.Connector
{
    /// <summary>Описатель макета центра печати</summary>
    [Serializable]
    public class PDMLayoutInfo : ISerializable
    {
        public PDMLayoutInfo(SerializationInfo info, StreamingContext context)
        {
            this.Name = (string)info.GetValue(nameof(Name), typeof(string));
            this.Content = (string)info.GetValue(nameof(Content), typeof(string));
        }

        /// <summary>
        /// Создаёт экземпляр структуры, хранящей информацию о макете
        /// </summary>
        /// <param name="layoutName">Заголовок макета</param>
        /// <param name="content">Информация о макете в xml-формате</param>
        /// <exception cref="T:System.ArgumentException">параметр <paramref name="layoutName" /> не задан или равен null; параметр <paramref name="content" /> не задан или равен null;</exception>
        public PDMLayoutInfo(string layoutName, string content)
        {
            if (string.IsNullOrEmpty(layoutName))
                throw new ArgumentException("Не задано имя макета.", nameof(layoutName));
            if (string.IsNullOrEmpty(content))
                throw new ArgumentException("Не задана информация о макете.", nameof(content));
            this.Name = layoutName;
            this.Content = content;
        }

        /// <summary>Возвращает имя макета</summary>
        public string Name { get; }

        /// <summary>Возвращает информацию о макете в xml-формате</summary>
        public string Content { get; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", (object)this.Name, this.Name.GetType());
            info.AddValue("Content", (object)this.Content, this.Content.GetType());
        }
    }
}
