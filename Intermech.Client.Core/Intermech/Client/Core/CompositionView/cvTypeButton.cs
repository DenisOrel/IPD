
// Type: Intermech.Client.Core.CompositionView.cvTypeButton
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;


namespace Intermech.Client.Core.CompositionView;

/// <summary>
/// 
/// </summary>
[Serializable]
internal class cvTypeButton : CVButtonBase
{
  /// <summary>
  /// 
  /// </summary>
  private Guid _objectTypeGuid = Guid.Empty;

  /// <summary>
  /// 
  /// </summary>
  public cvTypeButton() => this.ImageName = "imgServerObjects";

  /// <summary>
  /// 
  /// </summary>
  /// <param name="button"></param>
  public override void ApplyParams(CVButtonBase button)
  {
    if (!(button is cvTypeButton cvTypeButton))
      return;
    base.ApplyParams(button);
    this._objectTypeGuid = cvTypeButton._objectTypeGuid;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override CVButtonBase Clone()
  {
    cvTypeButton cvTypeButton = new cvTypeButton();
    cvTypeButton.ApplyParams((CVButtonBase) this);
    return (CVButtonBase) cvTypeButton;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override bool Select()
  {
    int num = MetaDataHelper.GetObjectTypeID(this._objectTypeGuid);
    using (AdvSelectorForm advSelectorForm = new AdvSelectorForm(AdvSelector.AttributableType, AttributableElements.Object, -1, num))
    {
      if (advSelectorForm.ShowDialog() != DialogResult.OK)
        return base.Select();
      num = advSelectorForm.ObjectType;
    }
    if (num == -1)
      return base.Select();
    IMSObjectType objectType = MetaDataHelper.GetObjectType(num);
    if (objectType == null)
      return base.Select();
    this._objectTypeGuid = objectType.Guid;
    this._hint = string.Format(LocalizationHolder.rm.GetString("Client.Core_25"), (object) objectType.ObjectTypeName);
    if (Statics.IconSrv != null)
    {
      using (Icon icon = new Icon(Statics.IconSrv.GetIcon(4, num), new Size(CVButtonBase.Consts.IconSize, CVButtonBase.Consts.IconSize)))
        this.Image = (Image) icon.ToBitmap();
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override IDescriptor BuildTree()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectType objectType = sessionKeeper.Session.GetObjectType(this._objectTypeGuid, false);
      if (objectType != null)
        return (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(objectType.ObjectType);
    }
    return base.BuildTree();
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
    attribute.Value = this._objectTypeGuid.ToString();
    xmlNode.Attributes.Append(attribute);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="xmlNodeButton"></param>
  /// <returns></returns>
  public static cvTypeButton Load(XmlNode xmlNodeButton)
  {
    if (!xmlNodeButton.Name.Equals(typeof (cvTypeButton).FullName))
      return (cvTypeButton) null;
    cvTypeButton cvTypeButton = new cvTypeButton();
    XmlAttribute attribute = xmlNodeButton.Attributes["Guid"];
    if (attribute != null)
    {
      try
      {
        cvTypeButton._objectTypeGuid = new Guid(attribute.Value);
      }
      catch
      {
        cvTypeButton._objectTypeGuid = Guid.Empty;
      }
    }
    return cvTypeButton;
  }
}
