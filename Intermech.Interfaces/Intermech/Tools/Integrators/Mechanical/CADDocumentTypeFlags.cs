
// Type: Intermech.Tools.Integrators.Mechanical.CADDocumentTypeFlags
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Tools.Integrators.Mechanical
{
    /// <summary>
    /// Флаги для типов документов конструкторских 3D-моделей и связанных с ними чертежей. Флаги используются для привязки к типам документов обработчиков команд, событий и т.д.
    /// </summary>
    public static class CADDocumentTypeFlags
    {
      /// <summary>
      /// Флаг добавляется типам документов, предназначенных для хранения 3D-моделей.
      /// Используется обработчиком парного создания версий 3D-моделей и их чертежей.
      /// </summary>
      public const string Model = "model";
      /// <summary>
      /// Флаг добавляется типам документов, предназначенных для хранения чертежей 3D-моделей (чертежи деталей и сборочные чертежи).
      /// Используется обработчиком парного создания версий 3D-моделей и их чертежей.
      /// </summary>
      public const string ModelDrawing = "drawing";
      /// <summary>
      /// Флаг непарного создания версий документов данного типа. Т.е. версия документа не должна создаваться при создании версии изделия.
      /// </summary>
      public const string UnpairedVersionCreation = "unpairedVersionCreation";
      /// <summary>
      /// Флаг добавляется типам документов, предназначенных для хранения неизменяемых 3D-моделей
      /// (например, моделей стандартных CADMECH).
      /// </summary>
      public const string ReadonlyModel = "readonlyModel";
    }
}
