
// Type: Intermech.Interfaces.WebPortal.ReadImportedObjectAttributesEventArgs
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Xml;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>
    /// Аргументы для события, возникающего при чтении атрибутов объекта из пришедшего XML.
    /// В нем можно обработать специфические данные и добавить их импортируемому объекту
    /// </summary>
    public sealed class ReadImportedObjectAttributesEventArgs
    {
      /// <summary>
      /// Корневой нод со свойствами и атрибутами импортируемого объекта
      /// </summary>
      public XmlNode RootNode { get; private set; }

      /// <summary>Структура с данными импортиреумого объекта</summary>
      public ImportingObject Object { get; private set; }

      public ReadImportedObjectAttributesEventArgs(XmlNode rootNode, ImportingObject importingObject)
      {
        this.RootNode = rootNode;
        this.Object = importingObject;
      }
    }
}
