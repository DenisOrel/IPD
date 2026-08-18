// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.SelTypeEventArgs
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Navigator.SelectionView;

#nullable disable
namespace Intermech.Expert.Editor;

public class SelTypeEventArgs
{
  public SelFormResult result;
  public string currObjType;
  public string currAttrType;

  public SelTypeEventArgs(SelFormResult result, string cObjType, string cAttrType)
  {
    this.result = result;
    this.currObjType = cObjType;
    this.currAttrType = cAttrType;
  }
}
