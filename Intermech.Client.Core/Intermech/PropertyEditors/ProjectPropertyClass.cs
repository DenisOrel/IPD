
// Type: Intermech.PropertyEditors.ProjectPropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.Drawing.Design;


namespace Intermech.PropertyEditors;

/// <summary>Класс юзеров для PropertyGrid.</summary>
[Editor(typeof (ProjectEditor), typeof (UITypeEditor))]
public class ProjectPropertyClass : ObjectPropertyClass
{
  /// <summary>Конструктор.</summary>
  /// <param name="aObjectID"></param>
  public ProjectPropertyClass(long aObjectID)
    : base(aObjectID)
  {
  }

  /// <summary>Конструктор.</summary>
  /// <param name="aObjectID"></param>
  /// <param name="aCaption"></param>
  public ProjectPropertyClass(long aObjectID, string aCaption)
    : base(aObjectID, aCaption)
  {
  }
}
