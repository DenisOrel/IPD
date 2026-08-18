
// Type: Intermech.UI.Winforms.DesignerServices
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.UI.Winforms
{
    /// <summary>
    /// Вспомогательные сервисы и утилиты для дизайнера Winforms.
    /// </summary>
    public static class DesignerServices
    {
      /// <summary>
      /// Позволяет определить, находится ли указанный компонент в DesignTime.
      /// Этот метод призван решить проблему неработоспособности существующего свойства
      /// DesignMode у компонентов, если обращение к этому свойству выполняется из
      /// конструктора компонента.
      /// </summary>
      /// <param name="component">Объект компонента</param>
      /// <param name="calledFromConstructor">Признак, что текущий метод вызван из конструктора компонента</param>
      /// <returns>Признак, что компонент находится в DesignTime</returns>
      public static bool IsInDesignMode(Component component, bool calledFromConstructor)
      {
        if (component == null)
          throw new ArgumentNullException(nameof (component));
        if (calledFromConstructor)
          return LicenseManager.UsageMode == LicenseUsageMode.Designtime;
        return component.Site != null && component.Site.DesignMode;
      }

      /// <summary>
      /// Контрол или его родители находятся в DesignMode
      /// <remarks>
      /// Для вложенных контролов свойство DesignMode не всегда актуально.
      /// Родительская форма может быть в DesignMode, а многоуровневые вложенные дочерние контролы нет.
      /// При этом обращение к сервисам и БД нельзя использовать.
      /// IsDesignerHosted проверяет режим DesignMode в общем, а не только конкретное состояние контрола
      /// </remarks>
      /// </summary>
      public static bool IsDesignerHosted(this Control checkedControl)
      {
        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
          return true;
        for (Control control = checkedControl; control != null; control = control.Parent)
        {
          if (control.Site != null && control.Site.DesignMode)
            return true;
        }
        return false;
      }
    }
}
