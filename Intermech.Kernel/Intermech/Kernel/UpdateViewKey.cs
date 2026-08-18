// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.UpdateViewKey
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll


namespace Intermech.Kernel;

internal class UpdateViewKey
{
  public string ViewName;
  public long ObjID;
  public string KeyField;

  public UpdateViewKey(string viewName, long objID, string keyFld)
  {
    this.ViewName = viewName;
    this.ObjID = objID;
    this.KeyField = keyFld;
  }

  public override int GetHashCode() => this.ViewName.GetHashCode() ^ this.ObjID.GetHashCode();

  public override bool Equals(object obj)
  {
    if (!(obj is UpdateViewKey))
      return false;
    UpdateViewKey updateViewKey = obj as UpdateViewKey;
    return updateViewKey.ObjID == this.ObjID && updateViewKey.ViewName == this.ViewName;
  }
}
