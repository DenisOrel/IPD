// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.ObjectAttributesWriter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class ObjectAttributesWriter : AttributesWriter<IDBObject>
{
  private bool _needCheckIn;

  protected override IDBAttributeType4 GetAttributeType4(
    IUserSession session,
    IDBObject attributable,
    int attributeID)
  {
    return session.GetObjectType(attributable.ObjectType).Attributes.GetAttributeByID(attributeID, false);
  }

  public IDBObject CheckOut(IDBObject attributable)
  {
    if (attributable.CheckoutBy != 0L)
      return attributable;
    this._needCheckIn = true;
    return attributable.ObjectModifyMode == ObjectModifyModes.CantModify ? (IDBObject) null : attributable.CheckOut(false);
  }

  public void CheckIn(IDBObject attributable)
  {
    if (!this._needCheckIn)
      return;
    attributable.CheckIn();
  }
}
