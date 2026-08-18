// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AutoSelection.ObjectCommitEventArgs
// Assembly: Intermech.Interfaces.AutoSelection, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A8A58CF2-90E0-4922-B0EB-2EB55893A867
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AutoSelection.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AutoSelection.xml

using Intermech.Interfaces.Compositions;

#nullable disable
namespace Intermech.Interfaces.AutoSelection;

public class ObjectCommitEventArgs : ObjectEventArgs
{
  /// <summary>
  /// 
  /// </summary>
  public ObjInfoItem NewObject;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aObject"></param>
  /// <param name="newObject"></param>
  public ObjectCommitEventArgs(ObjInfoItem aObject, ObjInfoItem newObject)
    : base(aObject)
  {
    this.NewObject = newObject;
  }
}
