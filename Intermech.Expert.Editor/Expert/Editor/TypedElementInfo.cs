// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.TypedElementInfo
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.DataFormats;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>
/// 
/// </summary>
internal class TypedElementInfo : ElementInfo, IDBObjectTypeID
{
  /// <summary>
  /// 
  /// </summary>
  private readonly int _typeId;

  /// <summary>Конструктор.</summary>
  /// <param name="id"></param>
  /// <param name="kind"></param>
  /// <param name="typeId"></param>
  public TypedElementInfo(long id, AttributableElements kind, int typeId)
    : base(id, kind)
  {
    this._typeId = typeId;
  }

  int IDBObjectTypeID.Value => this._typeId;
}
