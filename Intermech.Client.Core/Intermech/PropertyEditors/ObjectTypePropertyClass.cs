
// Type: Intermech.PropertyEditors.ObjectTypePropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System.ComponentModel;
using System.Drawing.Design;


namespace Intermech.PropertyEditors;

[Editor(typeof (ObjectTypeEditor), typeof (UITypeEditor))]
public class ObjectTypePropertyClass
{
  private int objectType;
  private string caption;

  public int ObjectType => this.objectType;

  public ObjectTypePropertyClass(int aObjectTypeID)
    : this(aObjectTypeID, (string) null)
  {
  }

  public ObjectTypePropertyClass(int aObjectTypeID, string aCaption)
  {
    this.objectType = aObjectTypeID;
    this.caption = aCaption;
  }

  public override string ToString()
  {
    if (this.caption != null)
      return this.caption;
    if (this.objectType == -1)
      return LocalizationHolder.rm.GetString("Client.Core_976");
    IDBObjectTypeInfo objectType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(this.objectType, false);
    return objectType != null ? objectType.ObjectInstanceName : string.Empty;
  }
}
