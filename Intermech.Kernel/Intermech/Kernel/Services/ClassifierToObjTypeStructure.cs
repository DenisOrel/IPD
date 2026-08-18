// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ClassifierToObjTypeStructure
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll


namespace Intermech.Kernel.Services;

internal sealed class ClassifierToObjTypeStructure
{
  public long ClassifierID { get; private set; }

  public int ObjectTypeID { get; private set; }

  public long UserID { get; private set; }

  public ClassifierToObjTypeStructure(long classifierID, int objTypeID, long userID)
  {
    this.ClassifierID = classifierID;
    this.ObjectTypeID = objTypeID;
    this.UserID = userID;
  }
}
