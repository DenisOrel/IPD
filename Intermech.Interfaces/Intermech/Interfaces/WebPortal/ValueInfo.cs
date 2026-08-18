
// Type: Intermech.Interfaces.WebPortal.ValueInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.WebPortal
{
    [Serializable]
    public class ValueInfo
    {
      public string StringValue;
      public long IntValue;
      public double FloatValue;
      public DateTime DateValue;
      public string FileName;
      public FileTypes FileType;
      public ArcMethods ArcMethod;
      public string FileAuthor;
      public int Index;

      public ValueInfo()
      {
      }

      public ValueInfo(
        int index,
        string stringValue,
        long intValue,
        double floatValue,
        DateTime dateValue,
        string fileName,
        FileTypes fileType,
        string fileAuthor,
        ArcMethods arcMethod)
      {
        this.Index = index;
        this.StringValue = stringValue;
        this.IntValue = intValue;
        this.FloatValue = floatValue;
        this.DateValue = dateValue;
        this.FileName = fileName;
        this.FileType = fileType;
        this.FileAuthor = fileAuthor;
        this.ArcMethod = arcMethod;
      }
    }
}
