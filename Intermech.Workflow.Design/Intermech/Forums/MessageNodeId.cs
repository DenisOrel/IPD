// Decompiled with JetBrains decompiler
// Type: Intermech.Forums.MessageNodeId
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Navigator.Interfaces;

#nullable disable
namespace Intermech.Forums;

/// <summary>
/// Липовый нод, заведенный для того, чтобы окно выбора сообщений из форума не блокировало кнопку Ок.
/// Почему-то не всегда SelectionWindow достаточно разрешения анализатора, работает через раз. </summary>
public class MessageNodeId : INodeID
{
  public int CategoryID { get; }

  public int TypeID { get; }

  public object Cookie { get; set; }

  public MessageNodeId()
  {
    this.CategoryID = 31 /*0x1F*/;
    this.TypeID = -1;
    this.Cookie = (object) null;
  }
}
