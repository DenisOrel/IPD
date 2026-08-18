
// Type: Intermech.Mvp.MvpContext
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Mvp.Winforms;
using System.Diagnostics;


namespace Intermech.Mvp
{
    /// <summary>
    /// Предоставляет доступ к глобальным сервисам MVP (ambient context).
    /// Все свойства класса, используемые для доступа к экземплярам сервисов, никогда не возвращают null.
    /// </summary>
    public static class MvpContext
    {
      private static ServiceRef<AbstractViewService> viewServiceRef = new ServiceRef<AbstractViewService>();

      /// <summary>
      /// Возвращает сервис видов MVP, отвечающий за создание видов и отображение их на экране.
      /// Значение свойства по умолчанию содержит ссылку на сервис видов Windows Forms.
      /// </summary>
      public static AbstractViewService ViewService
      {
        [DebuggerStepThrough] get
        {
          ServiceRef<AbstractViewService> viewServiceRef = MvpContext.viewServiceRef;
          if (!viewServiceRef.HasValue)
            viewServiceRef.Value = (AbstractViewService) WinformsViewService.Default;
          return viewServiceRef.Value;
        }
        [DebuggerStepThrough] set => MvpContext.viewServiceRef.Value = value;
      }
    }
}
