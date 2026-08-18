// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.Processors.GtcItem
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using System;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Intermech.GTC.Server.Processors;

internal class GtcItem
{
  public GtcItem(XElement node)
  {
    this.ProductId = !node.Descendants((XName) "product_id").Any<XElement>() || node.Descendants((XName) "product_id").First<XElement>() == null ? string.Empty : node.Descendants((XName) "product_id").First<XElement>().Value;
    this.GtcGenericClassId = !node.Descendants((XName) "gtc_generic_class_id").Any<XElement>() || node.Descendants((XName) "gtc_generic_class_id").First<XElement>() == null ? string.Empty : node.Descendants((XName) "gtc_generic_class_id").First<XElement>().Value;
    this.GtcVendorClassId = !node.Descendants((XName) "gtc_vendor_class_id").Any<XElement>() || node.Descendants((XName) "gtc_vendor_class_id").First<XElement>() == null ? string.Empty : node.Descendants((XName) "gtc_vendor_class_id").First<XElement>().Value;
    this.P21ValueChangeTimestamp = !node.Descendants((XName) "p21_value_change_timestamp").Any<XElement>() || node.Descendants((XName) "p21_value_change_timestamp").First<XElement>() == null ? new DateTime?() : new DateTime?(Convert.ToDateTime(node.Descendants((XName) "p21_value_change_timestamp").First<XElement>().Value));
    this.P21StructureChangeTimestamp = !node.Descendants((XName) "p21_structure_change_timestamp").Any<XElement>() || node.Descendants((XName) "p21_structure_change_timestamp").First<XElement>() == null ? new DateTime?() : new DateTime?(Convert.ToDateTime(node.Descendants((XName) "p21_structure_change_timestamp").First<XElement>().Value));
    this.P21FileName = !node.Descendants((XName) "p21_file_name").Any<XElement>() || node.Descendants((XName) "p21_file_name").First<XElement>() == null ? string.Empty : node.Descendants((XName) "p21_file_name").First<XElement>().Value;
    this.P21FileUrl = !node.Descendants((XName) "p21_file_url").Any<XElement>() || node.Descendants((XName) "p21_file_url").First<XElement>() == null ? string.Empty : node.Descendants((XName) "p21_file_url").First<XElement>().Value;
    this.EffectivityActiveStartDate = !node.Descendants((XName) "effectivity_active_start_date").Any<XElement>() || node.Descendants((XName) "effectivity_active_start_date").First<XElement>() == null ? new DateTime?() : new DateTime?(Convert.ToDateTime(node.Descendants((XName) "effectivity_active_start_date").First<XElement>().Value));
    this.EffectivityActiveEndDate = !node.Descendants((XName) "effectivity_active_end_date").Any<XElement>() || node.Descendants((XName) "effectivity_active_end_date").First<XElement>() == null ? new DateTime?() : new DateTime?(Convert.ToDateTime(node.Descendants((XName) "effectivity_active_end_date").First<XElement>().Value));
    this.ReplacementProductId = !node.Descendants((XName) "replacement_product_id").Any<XElement>() || node.Descendants((XName) "replacement_product_id").First<XElement>() == null ? string.Empty : node.Descendants((XName) "replacement_product_id").First<XElement>().Value;
    this.GtcGenericVersion = !node.Descendants((XName) "gtc_generic_version").Any<XElement>() || node.Descendants((XName) "gtc_generic_version").First<XElement>() == null ? string.Empty : node.Descendants((XName) "gtc_generic_version").First<XElement>().Value;
    this.UnitSystem = !node.Descendants((XName) "unit_system").Any<XElement>() || node.Descendants((XName) "unit_system").First<XElement>() == null ? string.Empty : node.Descendants((XName) "unit_system").First<XElement>().Value;
  }

  public string ProductId { get; private set; }

  public string GtcGenericClassId { get; private set; }

  public string GtcVendorClassId { get; private set; }

  public DateTime? P21ValueChangeTimestamp { get; private set; }

  public DateTime? P21StructureChangeTimestamp { get; private set; }

  public string P21FileName { get; private set; }

  public string P21FileUrl { get; private set; }

  public DateTime? EffectivityActiveStartDate { get; private set; }

  public DateTime? EffectivityActiveEndDate { get; private set; }

  public string ReplacementProductId { get; private set; }

  public string GtcGenericVersion { get; private set; }

  public string UnitSystem { get; private set; }
}
