// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.util.JPXParameters
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Collections;
using System.Collections.Generic;


namespace Syncfusion.Pdf.JPEG2000.util
{
    internal class JPXParameters : Dictionary<string, string>
    {
      private JPXParameters defaults;

      public JPXParameters()
      {
      }

      public JPXParameters(JPXParameters def) => this.defaults = def;

      public virtual void checkList(char prfx, string[] plist)
      {
        IEnumerator enumerator = (IEnumerator) this.Keys.GetEnumerator();
        while (enumerator.MoveNext())
        {
          string current = (string) enumerator.Current;
          if (current.Length > 0 && (int) current[0] == (int) prfx)
          {
            bool flag = false;
            if (plist != null)
            {
              for (int index = plist.Length - 1; index >= 0; --index)
              {
                if (current.Equals(plist[index]))
                {
                  flag = true;
                  break;
                }
              }
            }
            if (!flag)
              throw new ArgumentException($"Option '{current}' is not a valid one.");
          }
        }
      }

      public virtual bool getBooleanParameter(string pname)
      {
        switch (this.getParameter(pname))
        {
          case null:
            throw new ArgumentException("No parameter with name " + pname);
          case "on":
            return true;
          case "off":
            return false;
          default:
            throw new Exception();
        }
      }

      public virtual float getFloatParameter(string pname)
      {
        string parameter = this.getParameter(pname);
        if (parameter == null)
          throw new ArgumentException("No parameter with name " + pname);
        try
        {
          return float.Parse(parameter);
        }
        catch (FormatException ex)
        {
          throw new FormatException($"Parameter \"{pname}\" is not floating-point: {ex.Message}");
        }
      }

      public virtual int getIntParameter(string pname)
      {
        string parameter = this.getParameter(pname);
        if (parameter == null)
          throw new ArgumentException("No parameter with name " + pname);
        try
        {
          return int.Parse(parameter);
        }
        catch (FormatException ex)
        {
          throw new FormatException($"Parameter \"{pname}\" is not integer: {ex.Message}");
        }
      }

      public virtual string getParameter(string pname)
      {
        if (this.ContainsKey(pname))
          return this[pname];
        string parameter;
        this.defaults.TryGetValue(pname, out parameter);
        return parameter;
      }

      public static string[] toNameArray(string[][] pinfo)
      {
        if (pinfo == null)
          return (string[]) null;
        string[] nameArray = new string[pinfo.Length];
        for (int index = pinfo.Length - 1; index >= 0; --index)
          nameArray[index] = pinfo[index][0];
        return nameArray;
      }

      public JPXParameters DefaultParameterList => this.defaults;
    }
}
