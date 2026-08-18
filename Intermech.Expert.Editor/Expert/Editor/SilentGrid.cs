// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.SilentGrid
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using SourceGrid3;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>
/// Grid, который НЕ выкидывает идиотских окон при появлении исключения!!!
/// </summary>
public class SilentGrid : Grid
{
  public override void OnUserException(ExceptionEventArgs e)
  {
  }
}
