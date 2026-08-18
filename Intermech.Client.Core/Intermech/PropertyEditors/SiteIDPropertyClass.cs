
// Type: Intermech.PropertyEditors.SiteIDPropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.WebPortal;
using System.ComponentModel;
using System.Drawing.Design;


namespace Intermech.PropertyEditors;

[Editor(typeof (SiteIDEditor), typeof (UITypeEditor))]
public class SiteIDPropertyClass
{
  private string _siteID;
  private string _caption;

  public string SiteID => this._siteID;

  public SiteIDPropertyClass(string siteID)
  {
    this._siteID = siteID;
    this._caption = SiteIDPropertyClass.GetCaption(siteID);
  }

  public SiteIDPropertyClass(string siteID, string caption)
  {
    this._siteID = siteID;
    this._caption = caption;
  }

  public override string ToString()
  {
    return this._siteID == string.Empty || this._caption == string.Empty ? this._siteID : this._caption;
  }

  public static string GetCaption(string siteID)
  {
    return CompareValuesHelper.NormalizedValue((object) siteID) == null ? string.Empty : SiteIDHelper.GetCaption((ISitesCacheService) (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (ISitesCacheService)), siteID);
  }
}
