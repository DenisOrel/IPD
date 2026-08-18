
// Type: Intermech.Interfaces.Client.IIMDocumentEditorService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Client
{
    /// <summary>
    /// 
    /// </summary>
    public interface IIMDocumentEditorService
    {
      /// <summary>
      /// Получение расшифрованного значения непечатаемого символа.
      /// </summary>
      /// <param name="formula">Строковое значение формулы</param>
      /// <returns>Результат редактирования. TRUE - формула была отредактирована</returns>
      bool CallDocumentFormulaEditor(ref string formula);
    }
}
