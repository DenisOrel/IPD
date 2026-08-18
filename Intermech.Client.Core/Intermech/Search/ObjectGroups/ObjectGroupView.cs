
// Type: Intermech.Search.ObjectGroups.ObjectGroupView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;


namespace Intermech.Search.ObjectGroups;

public sealed class ObjectGroupView : ChildrenView
{
  public override string Caption
  {
    get
    {
      return this._nodeID is ObjectGroupNodeID ? MetaDataHelper.GetObjectTypeName(((ObjectGroupNodeID) this._nodeID).PartTypeID) : string.Empty;
    }
  }

  public override int ImageIndex => -1;

  public override ContentType ViewContentType
  {
    get => ContentType.NonFolders;
    set
    {
    }
  }
}
