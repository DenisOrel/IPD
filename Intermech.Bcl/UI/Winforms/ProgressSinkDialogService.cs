
// Type: Intermech.UI.Winforms.ProgressSinkDialogService
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Windows.Forms;


namespace Intermech.UI.Winforms
{
    /// <summary>
    /// Сервис для выполнения процессов с отображением хода выполнения в диалоговом окне.
    /// Для создания диалоговых окон используется технология Windows Forms. Реализация сервиса является thread safe.
    /// </summary>
    public sealed class ProgressSinkDialogService : IProgressSinkDialogService
    {
      /// <summary>
      /// Позволяет выполнить указанный процесс с отображением хода его выполнения в диалоговом окне.
      /// </summary>
      /// <param name="dialogCaption">Заголовок окна</param>
      /// <param name="dialogFlags">Флаги, управляющие поведением окна</param>
      /// <param name="processAction">Выполняемый процесс</param>
      /// <exception cref="T:ArgumentNullException">Параметры <paramref name="dialogCaption" />, <paramref name="processAction" /> не должны быть равны null</exception>
      public void Invoke(
        string dialogCaption,
        ProgressSinkDialogFlags dialogFlags,
        Action<IPercentageProgressSink> processAction)
      {
        if (dialogCaption == null)
          throw new ArgumentNullException(nameof (dialogCaption));
        if (processAction == null)
          throw new ArgumentNullException(nameof (processAction));
        using (PercentageProgressView view = new PercentageProgressView())
        {
          view.Text = dialogCaption;
          this.ShowProgressSinkView((Form) view);
          processAction(view.ProgressSink);
        }
      }

      /// <summary>
      /// Позволяет выполнить указанный процесс с отображением хода его выполнения в диалоговом окне.
      /// </summary>
      /// <typeparam name="TResult">Тип результата выполнения процесса</typeparam>
      /// <param name="dialogCaption">Заголовок окна</param>
      /// <param name="dialogFlags">Флаги, управляющие поведением окна</param>
      /// <param name="processAction">Выполняемый процесс</param>
      /// <returns>Результат выполнения процесса</returns>
      /// <exception cref="T:ArgumentNullException">Параметры <paramref name="dialogCaption" />, <paramref name="processAction" /> не должны быть равны null</exception>
      public TResult Invoke<TResult>(
        string dialogCaption,
        ProgressSinkDialogFlags dialogFlags,
        Func<IPercentageProgressSink, TResult> processAction)
      {
        if (dialogCaption == null)
          throw new ArgumentNullException(nameof (dialogCaption));
        if (processAction == null)
          throw new ArgumentNullException(nameof (processAction));
        using (PercentageProgressView view = new PercentageProgressView())
        {
          view.Text = dialogCaption;
          this.ShowProgressSinkView((Form) view);
          return processAction(view.ProgressSink);
        }
      }

      /// <summary>
      /// Позволяет выполнить указанный процесс с отображением хода его выполнения в диалоговом окне.
      /// </summary>
      /// <param name="dialogCaption">Заголовок окна</param>
      /// <param name="dialogFlags">Флаги, управляющие поведением окна</param>
      /// <param name="processAction">Выполняемый процесс</param>
      /// <exception cref="T:ArgumentNullException">Параметры <paramref name="dialogCaption" />, <paramref name="processAction" /> не должны быть равны null</exception>
      public void Invoke(
        string dialogCaption,
        ProgressSinkDialogFlags dialogFlags,
        Action<IMasterSlaveProgressSink> processAction)
      {
        if (dialogCaption == null)
          throw new ArgumentNullException(nameof (dialogCaption));
        if (processAction == null)
          throw new ArgumentNullException(nameof (processAction));
        using (MasterSlavePercentageProgressView view = new MasterSlavePercentageProgressView())
        {
          view.Text = dialogCaption;
          this.ShowProgressSinkView((Form) view);
          processAction((IMasterSlaveProgressSink) view);
        }
      }

      /// <summary>
      /// Позволяет выполнить указанный процесс с отображением хода его выполнения в диалоговом окне.
      /// </summary>
      /// <typeparam name="TResult">Тип результата выполнения процесса</typeparam>
      /// <param name="dialogCaption">Заголовок окна</param>
      /// <param name="dialogFlags">Флаги, управляющие поведением окна</param>
      /// <param name="processAction">Выполняемый процесс</param>
      /// <returns>Результат выполнения процесса</returns>
      /// <exception cref="T:ArgumentNullException">Параметры <paramref name="dialogCaption" />, <paramref name="processAction" /> не должны быть равны null</exception>
      public TResult Invoke<TResult>(
        string dialogCaption,
        ProgressSinkDialogFlags dialogFlags,
        Func<IMasterSlaveProgressSink, TResult> processAction)
      {
        if (dialogCaption == null)
          throw new ArgumentNullException(nameof (dialogCaption));
        if (processAction == null)
          throw new ArgumentNullException(nameof (processAction));
        using (MasterSlavePercentageProgressView view = new MasterSlavePercentageProgressView())
        {
          view.Text = dialogCaption;
          this.ShowProgressSinkView((Form) view);
          return processAction((IMasterSlaveProgressSink) view);
        }
      }

      private void ShowProgressSinkView(Form view)
      {
        view.Show();
        Application.DoEvents();
      }
    }
}
