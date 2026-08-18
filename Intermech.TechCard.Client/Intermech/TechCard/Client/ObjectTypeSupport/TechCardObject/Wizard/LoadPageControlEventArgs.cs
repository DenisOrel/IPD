// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Wizard.LoadPageControlEventArgs
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.UI.Winforms;
using System;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Wizard;

/// <summary>Параметры события загрузки содержимого закладки</summary>
public class LoadPageControlEventArgs : EventArgs
{
  /// <summary>Конструктор</summary>
  /// <param name="previousPage"></param>
  public LoadPageControlEventArgs(IWizardPage previousPage) => this.PreviousPage = previousPage;

  /// <summary>Предыдущая закладка</summary>
  public IWizardPage PreviousPage { get; private set; }

  /// <summary>Признак успешной загрузки данных</summary>
  /// <remarks>Если данные загружены в закладке, при следующей активации метод загрузки не будет вызываться</remarks>
  public bool DataLoaded { get; set; }
}
