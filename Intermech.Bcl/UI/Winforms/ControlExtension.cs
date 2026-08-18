
// Type: Intermech.UI.Winforms.ControlExtension
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Reflection;
using System.Windows.Forms;


namespace Intermech.UI.Winforms
{
    public static class ControlExtension
    {
      /// <summary>Проверка поддержки контролом стиля</summary>
      /// <param name="control"></param>
      /// <param name="flags"></param>
      /// <returns></returns>
      public static bool GetControlStyle(this Control control, ControlStyles flags)
      {
        MethodInfo method = control.GetType().GetMethod("GetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
        object[] parameters = new object[1]{ (object) flags };
        return (bool) method.Invoke((object) control, parameters);
      }
    }
}
