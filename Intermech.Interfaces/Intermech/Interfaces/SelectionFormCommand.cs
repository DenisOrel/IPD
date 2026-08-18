
// Type: Intermech.Interfaces.SelectionFormCommand
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Drawing;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Команда формы редактирования условия выборок SelectionForm
    /// </summary>
    public class SelectionFormCommand
    {
      public SelectionFormCommand(
        string name,
        string caption,
        int index,
        Image image,
        SelectionFormCommandExecHandler onClickHandler)
      {
        this.Name = name;
        this.Caption = caption;
        this.Index = index;
        this.Image = image;
        this.OnClickHandler = onClickHandler;
      }

      /// <summary>
      /// Название команды. Должно быть уникально в пределах всего IPS
      /// </summary>
      public string Name { get; private set; }

      /// <summary>Заголовок</summary>
      public string Caption { get; private set; }

      /// <summary>Индекс</summary>
      public int Index { get; private set; }

      /// <summary>Изображение для кнопки на тулбаре</summary>
      public Image Image { get; private set; }

      /// <summary>Обработчик нажатия</summary>
      public SelectionFormCommandExecHandler OnClickHandler { get; private set; }
    }
}
