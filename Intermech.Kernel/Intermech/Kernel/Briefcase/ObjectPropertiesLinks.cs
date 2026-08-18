// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ObjectPropertiesLinks
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll


namespace Intermech.Kernel.Briefcase;

internal sealed class ObjectPropertiesLinks
{
  public long ObjectID;
  public int ObjectType;
  public long OldOwnerID;
  public long OldProjectID;
  public long OldCreatorID;

  public ObjectPropertiesLinks(long objectID, int objectType)
    : this(objectID, objectType, 0L, 0L, 0L)
  {
  }

  public ObjectPropertiesLinks(
    long objectID,
    int objectType,
    long ownerObjectID,
    long oldProjectID,
    long oldCreatorID)
  {
    this.ObjectID = objectID;
    this.ObjectType = objectType;
    this.OldOwnerID = ownerObjectID;
    this.OldProjectID = oldProjectID;
    this.OldCreatorID = oldCreatorID;
  }
}
