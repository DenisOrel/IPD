// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Images.Metafiles.EmfObjectCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Native;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;


namespace Syncfusion.Pdf.Graphics.Images.Metafiles
{
    internal class EmfObjectCollection
    {
      private ArrayList m_avaibleIndexes;
      private Hashtable m_createdGraphicObjects;
      private static Hashtable s_standartGraphicObjects = new Hashtable();
      private const uint StockFlag = 2147483648 /*0x80000000*/;
      private const int StockModifFlag = 2147483647 /*0x7FFFFFFF*/;

      static EmfObjectCollection()
      {
        EmfObjectCollection.s_standartGraphicObjects.Add((object) 0, (object) (Brushes.White.Clone() as Brush));
        EmfObjectCollection.s_standartGraphicObjects.Add((object) 1, (object) (Brushes.LightGray.Clone() as Brush));
        EmfObjectCollection.s_standartGraphicObjects.Add((object) 2, (object) (Brushes.Gray.Clone() as Brush));
        EmfObjectCollection.s_standartGraphicObjects.Add((object) 3, (object) (Brushes.DarkGray.Clone() as Brush));
        EmfObjectCollection.s_standartGraphicObjects.Add((object) 4, (object) (Brushes.Black.Clone() as Brush));
        EmfObjectCollection.s_standartGraphicObjects.Add((object) 5, (object) (Brushes.Transparent.Clone() as Brush));
        EmfObjectCollection.s_standartGraphicObjects.Add((object) 6, (object) (Pens.White.Clone() as Pen));
        EmfObjectCollection.s_standartGraphicObjects.Add((object) 7, (object) (Pens.Black.Clone() as Pen));
        EmfObjectCollection.s_standartGraphicObjects.Add((object) 8, (object) (Pens.Transparent.Clone() as Pen));
        EmfObjectCollection.s_standartGraphicObjects.Add((object) 10, (object) (Control.DefaultFont.Clone() as Font));
        EmfObjectCollection.s_standartGraphicObjects.Add((object) 11, (object) (Control.DefaultFont.Clone() as Font));
        EmfObjectCollection.s_standartGraphicObjects.Add((object) 12, (object) (Control.DefaultFont.Clone() as Font));
        EmfObjectCollection.s_standartGraphicObjects.Add((object) 13, (object) (Control.DefaultFont.Clone() as Font));
        EmfObjectCollection.s_standartGraphicObjects.Add((object) 14, (object) (Control.DefaultFont.Clone() as Font));
        EmfObjectCollection.s_standartGraphicObjects.Add((object) 15, (object) (Control.DefaultFont.Clone() as Font));
        EmfObjectCollection.s_standartGraphicObjects.Add((object) 16 /*0x10*/, (object) (Control.DefaultFont.Clone() as Font));
        EmfObjectCollection.s_standartGraphicObjects.Add((object) 17, (object) (Control.DefaultFont.Clone() as Font));
        EmfObjectCollection.s_standartGraphicObjects.Add((object) 18, (object) (Brushes.White.Clone() as Brush));
        EmfObjectCollection.s_standartGraphicObjects.Add((object) 19, (object) (Pens.Black.Clone() as Pen));
      }

      public void AddObject(object value)
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        int index;
        if (this.AvaibleIndexes.Count > 0)
        {
          index = (int) this.AvaibleIndexes[0];
          this.AvaibleIndexes.RemoveAt(0);
        }
        else
          index = this.CreatedGraphicObjects.Count;
        this.AddObject(value, index);
      }

      public void AddObject(object value, int index)
      {
        this.CreatedGraphicObjects[(object) index] = value != null ? value : throw new ArgumentNullException(nameof (value));
      }

      public void Clear()
      {
        if (this.m_createdGraphicObjects != null)
          this.m_createdGraphicObjects.Clear();
        if (this.m_avaibleIndexes == null)
          return;
        this.m_avaibleIndexes.Clear();
      }

      public object DeleteObject(int index)
      {
        object createdGraphicObject = this.CreatedGraphicObjects[(object) index];
        this.CreatedGraphicObjects[(object) index] = (object) null;
        if (this.AvaibleIndexes.Contains((object) index))
          return createdGraphicObject;
        this.AvaibleIndexes.Add((object) index);
        return createdGraphicObject;
      }

      public object GetStockObject(STOCK objId)
      {
        return EmfObjectCollection.s_standartGraphicObjects[(object) (int) objId];
      }

      private object GetStockObjectMasked(int objId)
      {
        objId &= int.MaxValue;
        return EmfObjectCollection.s_standartGraphicObjects[(object) objId];
      }

      private bool IsInStock(int objId) => ((ulong) objId & 2147483648UL /*0x80000000*/) > 0UL;

      public bool IsStockObject(object value)
      {
        return value != null ? EmfObjectCollection.s_standartGraphicObjects.ContainsValue(value) : throw new ArgumentNullException(nameof (value));
      }

      public object SelectObject(int index)
      {
        if (this.IsInStock(index))
          return this.GetStockObjectMasked(index);
        if (this.AvaibleIndexes.Contains((object) index))
          this.AvaibleIndexes.Remove((object) index);
        return this.CreatedGraphicObjects[(object) index];
      }

      private ArrayList AvaibleIndexes
      {
        get
        {
          if (this.m_avaibleIndexes == null)
            this.m_avaibleIndexes = new ArrayList();
          return this.m_avaibleIndexes;
        }
      }

      protected internal Hashtable CreatedGraphicObjects
      {
        get
        {
          if (this.m_createdGraphicObjects == null)
            this.m_createdGraphicObjects = new Hashtable();
          return this.m_createdGraphicObjects;
        }
      }
    }
}
