
// Type: Intermech.Interfaces.IOutputView
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Служба позволяет отобразить текстовую информацию в окне вывода Навигатора
    /// </summary>
    public interface IOutputView
    {
      /// <summary>Выводит текст в окно вывода для заданной категории</summary>
      /// <param name="category">Категория</param>
      /// <param name="text">Выводимый текст</param>
      void WriteString(string category, string text);

      /// <summary>Очищает окно вывода для указанной категории</summary>
      /// <param name="category">Категория</param>
      void ClearText(string category);

      /// <summary>Переключает окно вывода для заданной категории</summary>
      /// <param name="category">Категория</param>
      void Activate(string category);

      /// <summary>Показывает окно вывода</summary>
      void ShowView();
    }
}
