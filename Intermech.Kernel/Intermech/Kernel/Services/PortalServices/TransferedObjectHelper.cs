// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.TransferedObjectHelper
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;


namespace Intermech.Kernel.Services.PortalServices;

internal static class TransferedObjectHelper
{
  private static List<Intermech.Kernel.Services.PortalServices.TransferedObjectTypeIDs> _transferedObjectTypeIDs;

  public static void WriteTo(BinaryWriter writer, ITransferedObject transferedObject)
  {
    Intermech.Kernel.Services.PortalServices.TransferedObjectTypeIDs transferedObjectTypeIds = TransferedObjectHelper.TransferedObjectTypeIDs.Find((Predicate<Intermech.Kernel.Services.PortalServices.TransferedObjectTypeIDs>) (x => x.Type.Equals(transferedObject.GetType())));
    writer.Write(transferedObjectTypeIds.ID);
    transferedObject.Save(writer);
  }

  public static TransferedObject LoadFor(BinaryReader reader, bool extendedDefault)
  {
    TransferedObject transferedObject = TransferedObjectHelper.LoadFor(reader);
    if (transferedObject == null)
    {
      transferedObject = extendedDefault ? (TransferedObject) new ExtendedTransferedObject() : new TransferedObject();
      transferedObject.Load(reader);
    }
    return transferedObject;
  }

  private static TransferedObject LoadFor(BinaryReader reader)
  {
    int id = reader.ReadInt32();
    Intermech.Kernel.Services.PortalServices.TransferedObjectTypeIDs transferedObjectTypeIds = TransferedObjectHelper.TransferedObjectTypeIDs.Find((Predicate<Intermech.Kernel.Services.PortalServices.TransferedObjectTypeIDs>) (x => x.ID.Equals(id)));
    if (transferedObjectTypeIds != null)
    {
      TransferedObject instance = (TransferedObject) Activator.CreateInstance(transferedObjectTypeIds.Type);
      instance.Load(reader);
      return instance;
    }
    reader.BaseStream.Position -= 4L;
    return (TransferedObject) null;
  }

  public static List<Intermech.Kernel.Services.PortalServices.TransferedObjectTypeIDs> TransferedObjectTypeIDs
  {
    get
    {
      if (TransferedObjectHelper._transferedObjectTypeIDs == null)
        TransferedObjectHelper._transferedObjectTypeIDs = Enum.GetValues(typeof (TransferedObjectTypes)).Cast<TransferedObjectTypes>().Select<TransferedObjectTypes, Intermech.Kernel.Services.PortalServices.TransferedObjectTypeIDs>((Func<TransferedObjectTypes, Intermech.Kernel.Services.PortalServices.TransferedObjectTypeIDs>) (value => new Intermech.Kernel.Services.PortalServices.TransferedObjectTypeIDs((Attribute.GetCustomAttribute((MemberInfo) value.GetType().GetField(value.ToString()), typeof (TypeAttribute)) as TypeAttribute).Type, (int) value))).ToList<Intermech.Kernel.Services.PortalServices.TransferedObjectTypeIDs>();
      return TransferedObjectHelper._transferedObjectTypeIDs;
    }
  }
}
