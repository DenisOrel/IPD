
// Type: Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFilterXmlConverter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;


namespace Intermech.Search.CompositionByObjectTypesFilters
{
    public sealed class CompositionByObjectTypesFilterXmlConverter : 
      ICompositionByObjectTypesFilterXmlConverter
    {
      private const string RootXmlElementName = "IPS.UserSettings";
      private const string FiltersXmlElementName = "OT_Filters";
      private const string FilterXmlElementName = "OT_Filter";
      private const string FilterNameXmlAttributeName = "name";
      private const string GuidXmlAttributeName = "guid";
      private const string ProjectTypeXmlElementName = "OT_ParentType";
      private const string PartTypeXmlElementName = "OT_ChildrenType";

      public CompositionByObjectTypesFilter[] ConvertFromXml(string xml)
      {
        if (string.IsNullOrEmpty(xml))
          throw new ArgumentException();
        return XDocument.Parse(xml).Root.Element((XName) "OT_Filters").Elements((XName) "OT_Filter").Select<XElement, CompositionByObjectTypesFilter>((Func<XElement, CompositionByObjectTypesFilter>) (o => this.CreateFilterFromXElement(o))).ToArray<CompositionByObjectTypesFilter>();
      }

      public string ConvertToXml(CompositionByObjectTypesFilter[] filters)
      {
        if (filters == null)
          throw new ArgumentNullException(nameof (filters));
        XElement content = new XElement((XName) "OT_Filters");
        foreach (CompositionByObjectTypesFilter filter in filters)
          content.Add((object) this.CreateXElementForFilter(filter));
        return new XDocument(new object[1]
        {
          (object) new XElement((XName) "IPS.UserSettings", (object) content)
        }).ToString();
      }

      private CompositionByObjectTypesFilter CreateFilterFromXElement(XElement xElement)
      {
        CompositionByObjectTypesFilter filterFromXelement = new CompositionByObjectTypesFilter();
        filterFromXelement.Name = xElement.Attribute((XName) "name").Value;
        filterFromXelement.ProjectTypes.AddRange(xElement.Elements((XName) "OT_ParentType").Select<XElement, CompositionByObjectTypesFilterProjectType>((Func<XElement, CompositionByObjectTypesFilterProjectType>) (o => this.CreateProjectTypeFromXElement(o))));
        return filterFromXelement;
      }

      private CompositionByObjectTypesFilterProjectType CreateProjectTypeFromXElement(XElement xElement)
      {
        CompositionByObjectTypesFilterProjectType projectType = CompositionByObjectTypesFiltersHelper.CreateProjectType(MetaDataHelper.GetObjectTypeID(this.GetGuidForXElement(xElement)));
        projectType.CheckPartTypesAndDescendants(xElement.Elements((XName) "OT_ChildrenType").Select<XElement, int>((Func<XElement, int>) (o => this.GetPartTypeIDForPartTypeXElement(o))).ToArray<int>());
        return projectType;
      }

      private Guid GetGuidForXElement(XElement xElement)
      {
        return Guid.Parse(xElement.Attribute((XName) "guid").Value);
      }

      private int GetPartTypeIDForPartTypeXElement(XElement xElement)
      {
        return MetaDataHelper.GetObjectTypeID(this.GetGuidForXElement(xElement));
      }

      private XElement CreateXElementForFilter(CompositionByObjectTypesFilter filter)
      {
        XElement xelementForFilter = new XElement((XName) "OT_Filter");
        xelementForFilter.Add((object) new XAttribute((XName) "name", (object) filter.Name));
        xelementForFilter.Add((object) new XAttribute((XName) "guid", (object) Guid.NewGuid().ToString()));
        foreach (CompositionByObjectTypesFilterProjectType projectType in (Collection<CompositionByObjectTypesFilterProjectType>) filter.ProjectTypes)
          xelementForFilter.Add((object) this.CreateXElementForProjectType(projectType));
        return xelementForFilter;
      }

      private XElement CreateXElementForProjectType(
        CompositionByObjectTypesFilterProjectType projectType)
      {
        XElement xelementForProjectType = new XElement((XName) "OT_ParentType");
        Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(projectType.ProjectTypeID);
        xelementForProjectType.Add((object) new XAttribute((XName) "guid", (object) objectTypeGuid));
        foreach (CompositionByObjectTypesFilterPartType partType1 in (Collection<CompositionByObjectTypesFilterPartType>) projectType.PartTypes)
        {
          foreach (CompositionByObjectTypesFilterPartType partType2 in partType1.GetDescendentsAndSelf().Where<CompositionByObjectTypesFilterPartType>((Func<CompositionByObjectTypesFilterPartType, bool>) (o => o.Checked)))
            xelementForProjectType.Add((object) this.CreateXElementForPartType(partType2));
        }
        return xelementForProjectType;
      }

      private XElement CreateXElementForPartType(CompositionByObjectTypesFilterPartType partType)
      {
        XElement xelementForPartType = new XElement((XName) "OT_ChildrenType");
        xelementForPartType.Add((object) new XAttribute((XName) "guid", (object) MetaDataHelper.GetObjectTypeGuid(partType.PartTypeID)));
        return xelementForPartType;
      }
    }
}
