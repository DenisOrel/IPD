// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.NotifyOptionsAttribute
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Workflow;
using System;

#nullable disable
namespace Intermech.Interfaces.Workflow;

/// <summary>
/// Атрибут к NotifyOptions с дополнительными значениями для каждой опции
/// </summary>
public class NotifyOptionsAttribute : Attribute
{
  /// <summary>Краткое наименование</summary>
  public string ShortName = "-";
  /// <summary>Имя картинки в INamedImageList</summary>
  public string ImageName = "imgEmpty";
  /// <summary>Название действия</summary>
  public string ActionName = string.Empty;

  public NotifyOptionsAttribute(NotifyOptions option)
  {
    switch (option)
    {
      case NotifyOptions.CheckOut:
        this.ShortName = LocalizationHolder.rm.GetString("Interfaces.Workflow_1");
        this.ImageName = "imgCheckOut";
        break;
      case NotifyOptions.UndoCheckOut:
        this.ShortName = LocalizationHolder.rm.GetString("Interfaces.Workflow_2");
        this.ImageName = "imgUndoCheckOut";
        break;
      case NotifyOptions.Delete:
        this.ShortName = LocalizationHolder.rm.GetString("Interfaces.Workflow_3");
        this.ImageName = "imgDeleteObject";
        break;
      case NotifyOptions.Version:
        this.ShortName = LocalizationHolder.rm.GetString("Interfaces.Workflow_4");
        this.ImageName = "imgNewVersion";
        break;
      case NotifyOptions.Forum:
        this.ShortName = LocalizationHolder.rm.GetString("ShortNotifyName");
        this.ImageName = "imgUser";
        break;
      case NotifyOptions.AttributeValueChanged:
        this.ShortName = LocalizationHolder.rm.GetString("ShortNotifyName");
        this.ImageName = "imgProp";
        break;
    }
  }
}
