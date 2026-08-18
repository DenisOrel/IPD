
// Type: Intermech.Security.OrganizationalUnitsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;


namespace Intermech.Security;

/// <summary>
/// Вьюха отображения организационных единиц в виде дерева
/// </summary>
public class OrganizationalUnitsView : ObjectsViewBase
{
  private int _imageIndex;

  public OrganizationalUnitsView() => this._imageIndex = -1;

  public override ContentType ViewContentType => ContentType.Folders;

  public override int ImageIndex
  {
    get
    {
      if (this._imageIndex < 0)
        this._imageIndex = Holder.NamedImageList.ImageIndex("imgContains");
      return this._imageIndex;
    }
  }
}
