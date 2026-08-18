
// Type: Intermech.UI.Winforms.IWizardPage
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.UI.Winforms
{
    /// <summary>Позволяет реализовать страницу мастера.</summary>
    public interface IWizardPage
    {
      /// <summary>Вызвается при переходе на страницу мастера.</summary>
      /// <param name="prevPage">Предыдущая страница мастера. Может быть null, если эта страница - первая</param>
      /// <param name="rollback">True, если передвижение осуществляется по кнопке "Назад"</param>
      void Activate(IWizardPage prevPage, bool rollback);

      /// <summary>Вызывает при переходе на другую страницу мастера.</summary>
      /// <param name="nextPage">Следующая страница мастера. Может быть null, если эта страница - последняя</param>
      /// <param name="rollback">True, если передвижение осуществляется по кнопке "Назад"</param>
      void Deactivate(IWizardPage nextPage, bool rollback);

      /// <summary>
      /// Возвращает true, если работа пользователя с этой страницей действительно может быть закончена.
      /// Вызывается при нажатии пользователем кнопки "Вперед/Готово".
      /// </summary>
      bool ReallyComplete { get; }

      /// <summary>
      /// Позволяет сохранить/обработать результаты работы страницы мастера. Вызывается при нажатии
      /// пользователем кнопки "Вперед/Готово" до смены страниц мастера.
      /// </summary>
      void DoMagic();

      /// <summary>
      /// Возвращает визуальный элемент управления, реализующий страницу мастера.
      /// </summary>
      Control Control { get; }

      /// <summary>
      /// Возвращает или устанавливает мастер, к которому относится эта страница.
      /// </summary>
      IWizard Wizard { get; set; }

      /// <summary>Возвращает имя страницы.</summary>
      string Name { get; }

      /// <summary>Возвращает название страницы мастера.</summary>
      string Caption { get; }

      /// <summary>Возвращает описание страницы мастера.</summary>
      string Description { get; }

      /// <summary>Возвращает иконку страницы мастера.</summary>
      Image Image { get; }

      /// <summary>
      /// Событие, когда пользователь ввел все необходимые данные на этой странице и может
      /// перейти к следующей странице мастера. По этому событию мастер включает и выключает
      /// кнопку "Далее/Готово".
      /// </summary>
      event EventHandler<PageCompleteEventArgs> PageComplete;
    }
}
