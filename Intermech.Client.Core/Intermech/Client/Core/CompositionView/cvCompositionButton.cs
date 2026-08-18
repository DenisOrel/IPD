
// Type: Intermech.Client.Core.CompositionView.cvCompositionButton
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.Xml;


namespace Intermech.Client.Core.CompositionView;

/// <summary>
/// 
/// </summary>
[Serializable]
internal class cvCompositionButton : CVButtonBase
{
  /// <summary>
  /// 
  /// </summary>
  private Guid _objectGuid = Guid.Empty;

  /// <summary>
  /// 
  /// </summary>
  public cvCompositionButton() => this.ImageName = "imgFolder";

  /// <summary>
  /// 
  /// </summary>
  /// <param name="button"></param>
  public override void ApplyParams(CVButtonBase button)
  {
    if (!(button is cvCompositionButton compositionButton))
      return;
    base.ApplyParams(button);
    this._objectGuid = compositionButton._objectGuid;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override CVButtonBase Clone()
  {
    cvCompositionButton compositionButton = new cvCompositionButton();
    compositionButton.ApplyParams((CVButtonBase) this);
    return (CVButtonBase) compositionButton;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override bool Select()
  {
    long[] numArray = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Client.Core_22"), LocalizationHolder.rm.GetString("Client.Core_23"), SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0)
      return base.Select();
    QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(numArray[0]);
    if (objectInfo.Empty)
      return base.Select();
    this._objectGuid = objectInfo.VersionGuid;
    this._hint = string.Format(LocalizationHolder.rm.GetString("Client.Core_24"), (object) objectInfo.Caption);
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override IDescriptor BuildTree()
  {
    QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(this._objectGuid);
    return !objectInfo.Empty ? (IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(objectInfo.ObjectID) : base.BuildTree();
  }

  /// <summary>Проверка на доступность действия</summary>
  /// <param name="args"></param>
  public override CVButtonEnabled Check(CVLocalButton.CVButtonArgs args)
  {
    return CVLocalButton.Check((CVButtonBase) this, args);
  }

  /// <summary>Выполнение действия</summary>
  /// <param name="args"></param>
  public override void Click(CVLocalButton.CVButtonClickArgs args)
  {
    CVLocalButton.Click((CVButtonBase) this, args);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="xmlNodeType"></param>
  public override void Save(XmlNode xmlNodeType)
  {
    XmlNode xmlNode = this.SaveInternal(xmlNodeType);
    XmlAttribute attribute = xmlNodeType.OwnerDocument.CreateAttribute("Guid");
    attribute.Value = this._objectGuid.ToString();
    xmlNode.Attributes.Append(attribute);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="xmlNodeButton"></param>
  /// <returns></returns>
  public static cvCompositionButton Load(XmlNode xmlNodeButton)
  {
    if (!xmlNodeButton.Name.Equals(typeof (cvCompositionButton).FullName))
      return (cvCompositionButton) null;
    cvCompositionButton compositionButton = new cvCompositionButton();
    XmlAttribute attribute = xmlNodeButton.Attributes["Guid"];
    if (attribute != null)
    {
      try
      {
        compositionButton._objectGuid = new Guid(attribute.Value);
      }
      catch
      {
        compositionButton._objectGuid = Guid.Empty;
      }
    }
    return compositionButton;
  }
}
