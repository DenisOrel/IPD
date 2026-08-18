// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.UserPropertyClass
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.PropertyEditors;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Класс выбора пользователей для PropertyGrid</summary>
[Editor(typeof (UserEditor), typeof (UITypeEditor))]
public class UserPropertyClass : ObjectPropertyClass
{
  public UserPropertyClass(long aObjectID)
    : base(aObjectID)
  {
  }

  public UserPropertyClass(long aObjectID, string aCaption)
    : base(aObjectID, aCaption)
  {
  }
}
