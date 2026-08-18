
// Type: Intermech.Interfaces.IXmlObjectIPS
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Runtime.Serialization;
using System.Xml;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс классов, которые можно сохранить в XML и извлечь из XML</summary>
    public interface IXmlObjectIPS
    {
      /// <summary>Записать поля в XML</summary>
      /// <param name="elementName">Имя элемента XML, под которым нужно сохранить данные</param>
      /// <param name="xw">XmlWriter</param>
      /// <param name="objectRefId">Генератор идентификаторов</param>
      void WriteToXml(string elementName, XmlWriter xw, ObjectIDGenerator objectRefId);

      /// <summary>Загрузить из XML</summary>
      /// <remarks>
      /// Рекомендуемая реализация метода
      /// public virtual void ReadFromXml(XmlReadArgsIPS readArgs)
      /// {
      /// 	// Код который необходимо выполнить до загрузки объекта
      /// 
      /// 	// Загрузка данных
      /// 	XmlHelperIPS.ReadFromXml(this, readArgs);
      /// 
      /// 	// Код который необходимо выполнить после загрузки объекта
      /// }
      /// </remarks>
      /// <param name="readArgs">Аргументы чтения из XML</param>
      void ReadFromXml(XmlReadArgsIPS readArgs);

      /// <summary>Прочитать одно поле из XML</summary>
      /// <param name="readArgs">Аргументы чтения из XML</param>
      /// <returns>Возвращает true, если поле прочитано</returns>
      bool ReadFieldFromXml(XmlReadArgsIPS readArgs);
    }
}
