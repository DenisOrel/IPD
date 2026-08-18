// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.IIEWrapperEvents
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.Drawing;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Document.RtfEditor;

[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
internal interface IIEWrapperEvents
{
  [DispId(2)]
  void IEAction(object Sender, int ActionType, int ActionId);

  [DispId(9)]
  void IEClosing(object Sender);

  [DispId(6)]
  void IEHypertext(object Sender, ref tc.StrHyperlink link);

  [DispId(7)]
  bool IEMergeData(object Sender, string name, out string data);

  [DispId(1)]
  void IEModified(object Sender);

  [DispId(10)]
  void IEPageCount(object Sender);

  [DispId(8)]
  void IEPageSizeChanging(object Sender, ref int NewPageSize);

  [DispId(11)]
  void IEPostPaint(object Sender, Graphics gr);

  [DispId(3)]
  void IEPreprocess(object Sender, int ActionType, int ActionId);

  [DispId(12)]
  void IESpellWordReplaced(object Sender, int CharPos, string PrevWord, string NewWord);

  [DispId(4)]
  void IEUpdateStatusbar(object Sender);

  [DispId(5)]
  void IEUpdateToolbar(object Sender);
}
