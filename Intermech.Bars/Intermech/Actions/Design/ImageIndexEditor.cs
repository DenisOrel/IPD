
// Type: Intermech.Actions.Design.ImageIndexEditor
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;


namespace Intermech.Actions.Design
{
    public class ImageIndexEditor : ImageIndexEditorBase
    {
      protected override ImageList GetImageList(ITypeDescriptorContext context)
      {
        ImageList imageList = (ImageList) null;
        foreach (PropertyInfo property in context.Instance.GetType().GetProperties())
        {
          if (property.PropertyType == typeof (ImageList) && property.CanRead)
          {
            imageList = (ImageList) property.GetValue(context.Instance, (object[]) null);
            break;
          }
        }
        if (imageList == null)
        {
          PropertyInfo property1 = context.Instance.GetType().GetProperty("Parent");
          if (property1 != (PropertyInfo) null)
          {
            object obj = property1.GetValue(context.Instance, (object[]) null);
            if (obj != null)
            {
              foreach (PropertyInfo property2 in obj.GetType().GetProperties())
              {
                if (property2.PropertyType == typeof (ImageList) && property2.CanRead)
                {
                  imageList = (ImageList) property2.GetValue(obj, (object[]) null);
                  break;
                }
              }
            }
          }
        }
        return imageList;
      }
    }
}
