// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.SelectionBlock
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace Intermech.Document.RtfEditor;

/// <summary>Координаты блока текста для выделения</summary>
internal class SelectionBlock
{
  /// <summary>Первый символ</summary>
  internal int StartPos;
  /// <summary>Последний символ</summary>
  internal int EndPos;
  /// <summary>Положение каретки</summary>
  internal int CurPos;
  /// <summary>Тип выделения</summary>
  internal int HilightType;
  /// <summary>Выбрана картинка</summary>
  internal bool PictureClicked;
  /// <summary>Тип координат</summary>
  internal bool InternalEditorPos;

  /// <summary>Конструктор</summary>
  /// <param name="startPos">Первый символ выделения</param>
  /// <param name="endPos">Последний символ выделения</param>
  /// <param name="curPos">Положение каретки</param>
  /// <param name="hilightType">Тип выделения</param>
  internal SelectionBlock(
    int startPos,
    int endPos,
    int curPos,
    int hilightType,
    bool pictureClicked,
    bool internalEditorPos)
  {
    this.StartPos = startPos;
    this.EndPos = endPos;
    this.CurPos = curPos;
    this.HilightType = hilightType;
    this.PictureClicked = pictureClicked;
    this.InternalEditorPos = internalEditorPos;
  }
}
