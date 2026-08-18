
// Type: Intermech.PropertyEditors.UserPropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.Drawing.Design;


namespace Intermech.PropertyEditors;

/// <summary>Класс юзеров для PropertyGrid</summary>
[Editor(typeof (UserEditor), typeof (UITypeEditor))]
public class UserPropertyClass : ObjectPropertyClass
{
  public UserPropertyClass(long aObjectID)
    : base(aObjectID)
  {
  }

  public UserPropertyClass(long aObjectID, string aCaption)
    : base(aObjectID, aCaption)
  {
  }
}
