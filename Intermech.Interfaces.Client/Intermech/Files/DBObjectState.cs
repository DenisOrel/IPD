// Decompiled with JetBrains decompiler
// Type: Intermech.Files.DBObjectState
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Text;

#nullable disable
namespace Intermech.Files;

[Serializable]
public sealed class DBObjectState
{
  private readonly long id;
  private readonly long objectId;
  private readonly ObjectModifyModes modifyMode;
  private readonly string caption;

  public DBObjectState(long id, long objectId, ObjectModifyModes modifyMode, string caption)
  {
    if (id == -1L)
      throw new ArgumentException();
    if (objectId == 0L)
      throw new ArgumentException();
    this.id = id;
    this.objectId = objectId;
    this.modifyMode = modifyMode;
    this.caption = caption;
    if (this.caption != null)
      return;
    this.caption = string.Empty;
  }

  private static bool IsEditable(long objectId, ObjectModifyModes modifyMode)
  {
    if (objectId == 0L)
      throw new ArgumentException();
    switch (modifyMode)
    {
      case ObjectModifyModes.InBase:
        return true;
      case ObjectModifyModes.Checkout:
        if (objectId < 0L)
          return true;
        break;
    }
    return false;
  }

  public bool IsEditableState => DBObjectState.IsEditable(this.objectId, this.modifyMode);

  public long Id => this.id;

  public long ObjectId => this.objectId;

  public ObjectModifyModes ModifyMode => this.modifyMode;

  public string Caption => this.caption;

  public override string ToString()
  {
    string str1 = this.caption;
    if (string.IsNullOrEmpty(str1))
      str1 = "Безымянный объект";
    StringBuilder stringBuilder = new StringBuilder(str1.Length + 16 /*0x10*/);
    stringBuilder.Append(str1);
    string str2 = this.objectId.ToString();
    if (!this.caption.Contains(str2))
    {
      stringBuilder.Append(", ");
      stringBuilder.AppendFormat("ид. версии {0}", (object) str2);
    }
    return stringBuilder.ToString();
  }
}
