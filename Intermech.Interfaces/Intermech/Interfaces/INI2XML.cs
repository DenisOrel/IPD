
// Type: Intermech.Interfaces.INI2XML
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.IO;
using System.Text;
using System.Xml;


namespace Intermech.Interfaces
{
    /// <summary>Convert an INI file into an XML file</summary>
    public class INI2XML
    {
      /// <summary>
      /// Initial size of the buffer used when calling the Win32 API functions
      /// </summary>
      private const int INITIAL_BUFFER_SIZE = 1024 /*0x0400*/;

      /// <summary>
      /// Converts an INI file into an XML file.
      /// Output XML file has the following structure...
      ///   (?xml version="1.0"?)
      ///   (configuration)
      ///       (section name="Main")
      ///           (setting name="Timeout" value="90"/)
      ///           (setting name="Mode" value="Live"/)
      ///      (/section)
      ///   (/configuration)
      /// Example:
      /// if (Intermech.Interfaces.INI2XML.Convert( txtIniFileName.Text ))
      /// 	Console.WriteLine( "Successfully converted \"" + txtIniFileName.Text + "\" to xml" );
      /// else
      /// 	Console.WriteLine( "Problem converting \"" + txtIniFileName.Text + "\" to xml" );
      /// If an exception is raised, it is passed on to the caller.
      /// </summary>
      /// <param name="strINIFileName">File name of the INI file to convert</param>
      /// <returns>True if successfuly, or False if a problem</returns>
      public static bool Convert(string strINIFileName) => INI2XML.Convert(strINIFileName, "");

      /// <summary>
      /// Converts an INI file into an XML file.
      /// Output XML file has the following structure...
      ///   (?xml version="1.0"?)
      ///   (configuration)
      ///       (section name="Main")
      ///           (setting name="Timeout" value="90"/)
      ///           (setting name="Mode" value="Live"/)
      ///      (/section)
      ///   (/configuration)
      /// Example:
      /// if (Intermech.Interfaces.INI2XML.Convert( txtIniFileName.Text, txtXMLFileName.Text ))
      /// 	Console.WriteLine( "Successfully converted \"" + txtIniFileName.Text + "\" to \"" + txtXMLFileName.Text + "\"" );
      /// else
      /// 	Console.WriteLine( "Problem converting \"" + txtIniFileName.Text + "\" to \"" + txtXMLFileName.Text + "\"" );
      /// If an exception is raised, it is passed on to the caller.
      /// </summary>
      /// <param name="strINIFileName">File name of the INI file to convert</param>
      /// <param name="strXMLFileName">File name of the XML file that is created</param>
      /// <returns>True if successfuly, or False if a problem</returns>
      public static bool Convert(string strINIFileName, string strXMLFileName)
      {
        char[] delimiter1 = new char[1]{ '=' };
        byte[] numArray1 = new byte[1];
        XmlWriter xmlWriter = (XmlWriter) null;
        try
        {
          if (strXMLFileName.Length == 0)
            strXMLFileName = Path.Combine(Path.GetDirectoryName(strINIFileName), $"{Path.GetFileNameWithoutExtension(strINIFileName)}.xml");
          int nSize1 = 1024 /*0x0400*/;
          byte[] numArray2;
          while (true)
          {
            numArray2 = new byte[nSize1];
            int profileSectionNames = WIN32Wrapper.GetPrivateProfileSectionNames(numArray2, nSize1, strINIFileName);
            if (profileSectionNames != 0 && profileSectionNames == nSize1 - 2)
              nSize1 *= 2;
            else
              break;
          }
          string strText1 = Encoding.ASCII.GetString(numArray2);
          xmlWriter = (XmlWriter) new XmlTextWriter(strXMLFileName, Encoding.UTF8);
          xmlWriter.WriteStartDocument();
          xmlWriter.WriteStartElement("configuration");
          char[] delimiter2 = new char[1];
          int intIndex1 = 0;
          for (string token1 = INI2XML.GetToken(strText1, delimiter2, intIndex1); token1.Length > 0; token1 = INI2XML.GetToken(strText1, delimiter2, ++intIndex1))
          {
            xmlWriter.WriteStartElement("section");
            xmlWriter.WriteAttributeString("name", token1);
            int nSize2 = 1024 /*0x0400*/;
            for (int index = nSize2; index != 0 && index >= nSize2 - 2; nSize2 *= 2)
            {
              numArray2 = new byte[nSize2];
              index = WIN32Wrapper.GetPrivateProfileSection(token1, numArray2, nSize2, strINIFileName);
            }
            string strText2 = Encoding.ASCII.GetString(numArray2);
            int intIndex2 = 0;
            for (string token2 = INI2XML.GetToken(strText2, delimiter2, intIndex2); token2.Length > 0; token2 = INI2XML.GetToken(strText2, delimiter2, ++intIndex2))
            {
              string token3 = INI2XML.GetToken(token2, delimiter1, 0);
              string str = token2.Length <= token3.Length + 1 ? "" : token2.Substring(token3.Length + 1);
              xmlWriter.WriteStartElement("setting");
              xmlWriter.WriteAttributeString("name", token3);
              xmlWriter.WriteAttributeString("value", str);
              xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
          }
          xmlWriter.WriteEndElement();
          xmlWriter.WriteEndDocument();
          return true;
        }
        finally
        {
          xmlWriter?.Close();
        }
      }

      /// <summary>
      /// Get a token from a delimited string, eg.
      ///   intSection = 0
      ///   strSection = GetToken(lpSections, charNull, intSection)
      /// </summary>
      /// <param name="strText">Text that is delimited</param>
      /// <param name="delimiter">The delimiter, eg. ","</param>
      /// <param name="intIndex">The index of the token to return, NB. first token is index 0.</param>
      /// <returns>Returns the nth token from a string.</returns>
      private static string GetToken(string strText, char[] delimiter, int intIndex)
      {
        string token = "";
        string[] strArray = strText.Split(delimiter);
        if (strArray.GetUpperBound(0) >= intIndex)
          token = strArray[intIndex];
        return token;
      }
    }
}
