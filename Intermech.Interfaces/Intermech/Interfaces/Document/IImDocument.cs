
// Type: Intermech.Interfaces.Document.IImDocument
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Document
{
    /// <summary>Интерфейс документа Интермех</summary>
    public interface IImDocument
    {
      /// <summary>Обозначение документа</summary>
      string Designation { get; }

      /// <summary>Наименование документа</summary>
      string DocumentName { get; }
    }
}
