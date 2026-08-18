// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBAttributableType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;


namespace Intermech.Kernel;

internal class DBAttributableType(UserSession uSession) : DBMetadataExtensions(uSession)
{
  public virtual bool AnyAttributes
  {
    get => false;
    set
    {
    }
  }

  public virtual IDBAttribute4TypeCollection Attributes => (IDBAttribute4TypeCollection) null;

  public IDBAttributeType GetAttributeType(int attributeID)
  {
    IDBAttributeType attributeType = (IDBAttributeType) this.Attributes.GetAttributeByID(attributeID, false);
    if (attributeType == null && this.AnyAttributes)
      attributeType = this.UserSession.GetAttributeType(attributeID, false);
    return attributeType;
  }

  public IDBAttributeType GetAttributeType(string attributeName)
  {
    return this.GetAttributeType((this.EventHelper as EventLogHelper).GetAttributeID((object) attributeName));
  }
}
