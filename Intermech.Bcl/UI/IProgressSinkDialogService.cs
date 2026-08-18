
// Type: Intermech.UI.IProgressSinkDialogService
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.UI
{
    /// <summary>
    /// Интерфейс сервиса для выполнения процессов с отображением хода выполнения в диалоговом окне.
    /// </summary>
    public interface IProgressSinkDialogService
    {
      /// <summary>
      /// Позволяет выполнить указанный процесс с отображением хода его выполнения в диалоговом окне.
      /// </summary>
      /// <param name="dialogCaption">Заголовок окна</param>
      /// <param name="dialogFlags">Флаги, управляющие поведением окна</param>
      /// <param name="processAction">Выполняемый процесс</param>
      /// <exception cref="T:ArgumentNullException">Параметры <paramref name="dialogCaption" />, <paramref name="processAction" /> не должны быть равны null</exception>
      void Invoke(
        string dialogCaption,
        ProgressSinkDialogFlags dialogFlags,
        Action<IPercentageProgressSink> processAction);

      /// <summary>
      /// Позволяет выполнить указанный процесс с отображением хода его выполнения в диалоговом окне.
      /// </summary>
      /// <typeparam name="TResult">Тип результата выполнения процесса</typeparam>
      /// <param name="dialogCaption">Заголовок окна</param>
      /// <param name="dialogFlags">Флаги, управляющие поведением окна</param>
      /// <param name="processAction">Выполняемый процесс</param>
      /// <returns>Результат выполнения процесса</returns>
      /// <exception cref="T:ArgumentNullException">Параметры <paramref name="dialogCaption" />, <paramref name="processAction" /> не должны быть равны null</exception>
      TResult Invoke<TResult>(
        string dialogCaption,
        ProgressSinkDialogFlags dialogFlags,
        Func<IPercentageProgressSink, TResult> processAction);

      /// <summary>
      /// Позволяет выполнить указанный процесс с отображением хода его выполнения в диалоговом окне.
      /// </summary>
      /// <param name="dialogCaption">Заголовок окна</param>
      /// <param name="dialogFlags">Флаги, управляющие поведением окна</param>
      /// <param name="processAction">Выполняемый процесс</param>
      /// <exception cref="T:ArgumentNullException">Параметры <paramref name="dialogCaption" />, <paramref name="processAction" /> не должны быть равны null</exception>
      void Invoke(
        string dialogCaption,
        ProgressSinkDialogFlags dialogFlags,
        Action<IMasterSlaveProgressSink> processAction);

      /// <summary>
      /// Позволяет выполнить указанный процесс с отображением хода его выполнения в диалоговом окне.
      /// </summary>
      /// <typeparam name="TResult">Тип результата выполнения процесса</typeparam>
      /// <param name="dialogCaption">Заголовок окна</param>
      /// <param name="dialogFlags">Флаги, управляющие поведением окна</param>
      /// <param name="processAction">Выполняемый процесс</param>
      /// <returns>Результат выполнения процесса</returns>
      /// <exception cref="T:ArgumentNullException">Параметры <paramref name="dialogCaption" />, <paramref name="processAction" /> не должны быть равны null</exception>
      TResult Invoke<TResult>(
        string dialogCaption,
        ProgressSinkDialogFlags dialogFlags,
        Func<IMasterSlaveProgressSink, TResult> processAction);
    }
}
