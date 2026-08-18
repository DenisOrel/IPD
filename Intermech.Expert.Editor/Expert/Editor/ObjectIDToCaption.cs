// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.ObjectIDToCaption
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Localization;
using Intermech.PropertyEditors;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Класс для отображения</summary>
internal class ObjectIDToCaption : ObjectPropertyClass
{
  /// <summary>Конструктор</summary>
  /// <param name="objectId"></param>
  public ObjectIDToCaption(long objectId)
    : base(objectId)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="objectId"></param>
  /// <param name="caption"></param>
  public ObjectIDToCaption(long objectId, string caption)
    : base(objectId, caption)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override string ToString()
  {
    if (this.ObjectID == -1L)
      return LocalizationHolder.rm.GetString("Expert.Editor_124");
    string str = base.ToString();
    return string.IsNullOrEmpty(str) ? string.Format(LocalizationHolder.rm.GetString("Expert.Editor_125"), (object) this.ObjectID) : str;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    return obj is ObjectIDToCaption objectIdToCaption ? objectIdToCaption.ObjectID.Equals(this.ObjectID) : base.Equals(obj);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode() => this.ObjectID.GetHashCode();
}
