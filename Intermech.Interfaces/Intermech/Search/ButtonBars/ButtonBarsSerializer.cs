
// Type: Intermech.Search.ButtonBars.ButtonBarsSerializer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Xml.Linq;


namespace Intermech.Search.ButtonBars
{
    public sealed class ButtonBarsSerializer
    {
      public ButtonBar[] Deserialize(Stream stream, out bool onlySettings)
      {
        if (stream == null)
          throw new ArgumentNullException(nameof (stream));
        List<ButtonBar> buttonBarList = new List<ButtonBar>();
        XDocument xdocument = XDocument.Load(stream);
        onlySettings = false;
        XAttribute xattribute = xdocument.Root.Attribute((XName) "OnlySettings");
        if (xattribute != null)
          onlySettings = Convert.ToBoolean(xattribute.Value, (IFormatProvider) CultureInfo.InvariantCulture);
        foreach (XElement element in xdocument.Root.Elements((XName) "TechCardBar"))
        {
          ButtonBar buttonBar = this.DeserializeButtonBar(element);
          buttonBarList.Add(buttonBar);
        }
        return buttonBarList.ToArray();
      }

      public void Serialize(ButtonBar[] buttonBars, Stream stream, bool onlySettings = false)
      {
        if (buttonBars == null)
          throw new ArgumentNullException(nameof (buttonBars));
        if (stream == null)
          throw new ArgumentNullException(nameof (stream));
        XDocument xdocument = new XDocument(new object[1]
        {
          (object) new XElement((XName) "TechCardBarsSettings")
        });
        if (onlySettings)
          xdocument.Root.Add((object) new XAttribute((XName) "OnlySettings", (object) onlySettings.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
        foreach (ButtonBar buttonBar in buttonBars)
        {
          XElement content = this.SerializeButtonBar(buttonBar);
          xdocument.Root.Add((object) content);
        }
        xdocument.Save(stream);
      }

      private ButtonBar DeserializeButtonBar(XElement buttonBarXElement)
      {
        ButtonBar buttonBar = new ButtonBar(Guid.Parse(buttonBarXElement.Attribute((XName) "Guid").Value));
        if (buttonBarXElement.Attribute((XName) "ContainerGuid") != null)
          buttonBar.ContainerGuid = Guid.Parse(buttonBarXElement.Attribute((XName) "ContainerGuid").Value);
        buttonBar.DockLine = Convert.ToInt32(buttonBarXElement.Attribute((XName) "DockLine").Value, (IFormatProvider) CultureInfo.InvariantCulture);
        buttonBar.DockOffset = Convert.ToInt32(buttonBarXElement.Attribute((XName) "DockOffset").Value, (IFormatProvider) CultureInfo.InvariantCulture);
        buttonBar.Name = buttonBarXElement.Attribute((XName) "Text").Value;
        buttonBar.Visible = Convert.ToBoolean(buttonBarXElement.Attribute((XName) "Visible").Value, (IFormatProvider) CultureInfo.InvariantCulture);
        foreach (XElement element in buttonBarXElement.Elements((XName) "TechCardBarButton"))
        {
          ButtonBarButton buttonBarButton = this.DeserializeButtonBarButton(element);
          buttonBar.Buttons.Add(buttonBarButton);
        }
        return buttonBar;
      }

      private ButtonBarButton DeserializeButtonBarButton(XElement buttonBarButtonXElement)
      {
        ButtonBarButton buttonBarButton1 = new ButtonBarButton(buttonBarButtonXElement.Attribute((XName) "CommandName").Value);
        buttonBarButton1.BeginGroup = Convert.ToBoolean(buttonBarButtonXElement.Attribute((XName) "BeginGroup").Value, (IFormatProvider) CultureInfo.InvariantCulture);
        buttonBarButton1.DisplayType = (ButtonBarButtonDisplayType) Convert.ToInt32(buttonBarButtonXElement.Attribute((XName) "ButtonKind").Value);
        buttonBarButton1.Text = buttonBarButtonXElement.Attribute((XName) "Text").Value;
        buttonBarButton1.ToolTipText = buttonBarButtonXElement.Attribute((XName) "ToolTipText").Value;
        buttonBarButton1.Visible = Convert.ToBoolean(buttonBarButtonXElement.Attribute((XName) "Visible").Value, (IFormatProvider) CultureInfo.InvariantCulture);
        foreach (XElement element in buttonBarButtonXElement.Elements((XName) "TechCardBarButton"))
        {
          ButtonBarButton buttonBarButton2 = this.DeserializeButtonBarButton(element);
          buttonBarButton1.Buttons.Add(buttonBarButton2);
        }
        return buttonBarButton1;
      }

      private XElement SerializeButtonBar(ButtonBar buttonBar)
      {
        XElement xelement = new XElement((XName) "TechCardBar", new object[6]
        {
          (object) new XAttribute((XName) "Guid", (object) buttonBar.Guid),
          (object) new XAttribute((XName) "ContainerGuid", (object) buttonBar.ContainerGuid),
          (object) new XAttribute((XName) "DockLine", (object) buttonBar.DockLine.ToString((IFormatProvider) CultureInfo.InvariantCulture)),
          (object) new XAttribute((XName) "DockOffset", (object) buttonBar.DockOffset.ToString((IFormatProvider) CultureInfo.InvariantCulture)),
          (object) new XAttribute((XName) "Text", (object) (buttonBar.Name ?? string.Empty)),
          (object) new XAttribute((XName) "Visible", (object) buttonBar.Visible.ToString((IFormatProvider) CultureInfo.InvariantCulture))
        });
        foreach (ButtonBarButton button in (Collection<ButtonBarButton>) buttonBar.Buttons)
        {
          XElement content = this.SerializableButtonBarButton(button);
          xelement.Add((object) content);
        }
        return xelement;
      }

      private XElement SerializableButtonBarButton(ButtonBarButton buttonBarButton)
      {
        XElement xelement = new XElement((XName) "TechCardBarButton", new object[6]
        {
          (object) new XAttribute((XName) "BeginGroup", (object) buttonBarButton.BeginGroup.ToString((IFormatProvider) CultureInfo.InvariantCulture)),
          (object) new XAttribute((XName) "CommandName", (object) buttonBarButton.CommandName),
          (object) new XAttribute((XName) "ButtonKind", (object) ((int) buttonBarButton.DisplayType).ToString((IFormatProvider) CultureInfo.InvariantCulture)),
          (object) new XAttribute((XName) "Text", (object) buttonBarButton.Text),
          (object) new XAttribute((XName) "ToolTipText", (object) (buttonBarButton.ToolTipText ?? buttonBarButton.Text)),
          (object) new XAttribute((XName) "Visible", (object) buttonBarButton.Visible.ToString((IFormatProvider) CultureInfo.InvariantCulture))
        });
        foreach (ButtonBarButton button in (Collection<ButtonBarButton>) buttonBarButton.Buttons)
        {
          XElement content = this.SerializableButtonBarButton(button);
          xelement.Add((object) content);
        }
        return xelement;
      }
    }
}
