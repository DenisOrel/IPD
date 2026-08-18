// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Persistence.BinaryStateFormatter
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.Navigator.Persistence;

/// <summary>Реализует бинарный форматтер объектов.</summary>
public class BinaryStateFormatter : IStateFormatter
{
  private static Dictionary<Type, byte> typeCodes = new Dictionary<Type, byte>();
  private static Dictionary<byte, BinaryStateFormatter.IValueFormatter> valueFormatters = new Dictionary<byte, BinaryStateFormatter.IValueFormatter>();
  private const byte NullValue = 0;

  static BinaryStateFormatter()
  {
    BinaryStateFormatter.RegisterFormatter(typeof (char), (byte) 1, (BinaryStateFormatter.IValueFormatter) new BinaryStateFormatter.CharFormatter());
    BinaryStateFormatter.RegisterFormatter(typeof (string), (byte) 2, (BinaryStateFormatter.IValueFormatter) new BinaryStateFormatter.StringFormatter());
    BinaryStateFormatter.RegisterFormatter(typeof (byte), (byte) 3, (BinaryStateFormatter.IValueFormatter) new BinaryStateFormatter.ByteFormatter());
    BinaryStateFormatter.RegisterFormatter(typeof (short), (byte) 4, (BinaryStateFormatter.IValueFormatter) new BinaryStateFormatter.Int16Formatter());
    BinaryStateFormatter.RegisterFormatter(typeof (int), (byte) 5, (BinaryStateFormatter.IValueFormatter) new BinaryStateFormatter.Int32Formatter());
    BinaryStateFormatter.RegisterFormatter(typeof (long), (byte) 6, (BinaryStateFormatter.IValueFormatter) new BinaryStateFormatter.Int64Formatter());
    BinaryStateFormatter.RegisterFormatter(typeof (float), (byte) 7, (BinaryStateFormatter.IValueFormatter) new BinaryStateFormatter.SingleFormatter());
    BinaryStateFormatter.RegisterFormatter(typeof (double), (byte) 8, (BinaryStateFormatter.IValueFormatter) new BinaryStateFormatter.DoubleFormatter());
    BinaryStateFormatter.RegisterFormatter(typeof (Guid), (byte) 9, (BinaryStateFormatter.IValueFormatter) new BinaryStateFormatter.GuidFormatter());
    BinaryStateFormatter.RegisterFormatter(typeof (PersistentState), (byte) 10, (BinaryStateFormatter.IValueFormatter) new BinaryStateFormatter.PersistentStateFormatter());
  }

  public void Serialize(Stream stream, PersistentState state)
  {
    BinaryStateFormatter.Validate(stream);
    BinaryStateFormatter.WriterProps props = new BinaryStateFormatter.WriterProps(this, stream);
    try
    {
      this.InternalWrite(props, state);
    }
    finally
    {
      props.Stream.Flush();
    }
  }

  public PersistentState Deserialize(Stream stream)
  {
    BinaryStateFormatter.Validate(stream);
    BinaryStateFormatter.ReaderProps props = new BinaryStateFormatter.ReaderProps(this, stream);
    PersistentState state = new PersistentState();
    this.InternalRead(props, state);
    return state;
  }

  private static void Validate(Stream stream)
  {
    if (stream == null)
      throw new ArgumentNullException(nameof (stream), LocalizationHolder.rm.GetString("Interfaces.Client_69"));
  }

  private static void RegisterFormatter(
    Type valueType,
    byte typeCode,
    BinaryStateFormatter.IValueFormatter formatter)
  {
    BinaryStateFormatter.typeCodes.Add(valueType, typeCode);
    BinaryStateFormatter.valueFormatters.Add(typeCode, formatter);
  }

  private void InternalRead(BinaryStateFormatter.ReaderProps props, PersistentState state)
  {
    state.FullTypeName = props.Reader.ReadString();
    if (state.FullTypeName == string.Empty)
      state.FullTypeName = Consts.PersistentStateTypeName;
    int num = (int) props.Reader.ReadInt16();
    for (int index = 0; index < num; ++index)
    {
      string name = props.Reader.ReadString();
      byte key = props.Reader.ReadByte();
      if (key == (byte) 0)
      {
        state.AddValue(name, (object) null);
      }
      else
      {
        if (!BinaryStateFormatter.valueFormatters.ContainsKey(key))
          throw new StateFormatterException(string.Format(LocalizationHolder.rm.GetString("Interfaces.Client_70"), (object) name));
        object obj = BinaryStateFormatter.valueFormatters[key].Read(props);
        state.AddValue(name, obj);
      }
    }
  }

  private void InternalWrite(BinaryStateFormatter.WriterProps props, PersistentState state)
  {
    if (state.FullTypeName == Consts.PersistentStateTypeName)
      props.Writer.Write(string.Empty);
    else
      props.Writer.Write(state.FullTypeName);
    props.Writer.Write((short) state.MemberCount);
    foreach (KeyValuePair<string, object> keyValuePair in state)
    {
      props.Writer.Write(keyValuePair.Key);
      if (keyValuePair.Value == null)
      {
        props.Writer.Write((byte) 0);
      }
      else
      {
        Type type = keyValuePair.Value.GetType();
        byte key = BinaryStateFormatter.typeCodes.ContainsKey(type) ? BinaryStateFormatter.typeCodes[type] : throw new StateFormatterException(string.Format(LocalizationHolder.rm.GetString("Interfaces.Client_71"), (object) keyValuePair.Key));
        BinaryStateFormatter.IValueFormatter valueFormatter = BinaryStateFormatter.valueFormatters[key];
        props.Writer.Write(key);
        BinaryStateFormatter.WriterProps props1 = props;
        object obj = keyValuePair.Value;
        valueFormatter.Write(props1, obj);
      }
    }
  }

  private class FormatterProps
  {
    private BinaryStateFormatter formatter;
    private Stream stream;

    public FormatterProps(BinaryStateFormatter formatter, Stream stream)
    {
      this.formatter = formatter;
      this.stream = stream;
    }

    public BinaryStateFormatter Formatter => this.formatter;

    public Stream Stream => this.stream;
  }

  private class ReaderProps : BinaryStateFormatter.FormatterProps
  {
    private BinaryReader reader;

    public ReaderProps(BinaryStateFormatter formatter, Stream stream)
      : base(formatter, stream)
    {
      this.reader = new BinaryReader(stream, Encoding.UTF8);
    }

    public BinaryReader Reader => this.reader;
  }

  private class WriterProps : BinaryStateFormatter.FormatterProps
  {
    private BinaryWriter writer;

    public WriterProps(BinaryStateFormatter formatter, Stream stream)
      : base(formatter, stream)
    {
      this.writer = new BinaryWriter(stream, Encoding.UTF8);
    }

    public BinaryWriter Writer => this.writer;
  }

  private interface IValueFormatter
  {
    object Read(BinaryStateFormatter.ReaderProps props);

    void Write(BinaryStateFormatter.WriterProps props, object value);
  }

  private class CharFormatter : BinaryStateFormatter.IValueFormatter
  {
    public object Read(BinaryStateFormatter.ReaderProps props) => (object) props.Reader.ReadChar();

    public void Write(BinaryStateFormatter.WriterProps props, object value)
    {
      props.Writer.Write((char) value);
    }
  }

  private class StringFormatter : BinaryStateFormatter.IValueFormatter
  {
    public object Read(BinaryStateFormatter.ReaderProps props)
    {
      return (object) props.Reader.ReadString();
    }

    public void Write(BinaryStateFormatter.WriterProps props, object value)
    {
      props.Writer.Write((string) value);
    }
  }

  private class ByteFormatter : BinaryStateFormatter.IValueFormatter
  {
    public object Read(BinaryStateFormatter.ReaderProps props) => (object) props.Reader.ReadByte();

    public void Write(BinaryStateFormatter.WriterProps props, object value)
    {
      props.Writer.Write((byte) value);
    }
  }

  private class Int16Formatter : BinaryStateFormatter.IValueFormatter
  {
    public object Read(BinaryStateFormatter.ReaderProps props) => (object) props.Reader.ReadInt16();

    public void Write(BinaryStateFormatter.WriterProps props, object value)
    {
      props.Writer.Write((short) value);
    }
  }

  private class Int32Formatter : BinaryStateFormatter.IValueFormatter
  {
    public object Read(BinaryStateFormatter.ReaderProps props) => (object) props.Reader.ReadInt32();

    public void Write(BinaryStateFormatter.WriterProps props, object value)
    {
      props.Writer.Write((int) value);
    }
  }

  private class Int64Formatter : BinaryStateFormatter.IValueFormatter
  {
    public object Read(BinaryStateFormatter.ReaderProps props) => (object) props.Reader.ReadInt64();

    public void Write(BinaryStateFormatter.WriterProps props, object value)
    {
      props.Writer.Write((long) value);
    }
  }

  private class SingleFormatter : BinaryStateFormatter.IValueFormatter
  {
    public object Read(BinaryStateFormatter.ReaderProps props)
    {
      return (object) props.Reader.ReadSingle();
    }

    public void Write(BinaryStateFormatter.WriterProps props, object value)
    {
      props.Writer.Write((float) value);
    }
  }

  private class DoubleFormatter : BinaryStateFormatter.IValueFormatter
  {
    public object Read(BinaryStateFormatter.ReaderProps props)
    {
      return (object) props.Reader.ReadDouble();
    }

    public void Write(BinaryStateFormatter.WriterProps props, object value)
    {
      props.Writer.Write((double) value);
    }
  }

  private class GuidFormatter : BinaryStateFormatter.IValueFormatter
  {
    public object Read(BinaryStateFormatter.ReaderProps props)
    {
      return (object) new Guid(props.Reader.ReadBytes(16 /*0x10*/));
    }

    public void Write(BinaryStateFormatter.WriterProps props, object value)
    {
      props.Writer.Write(((Guid) value).ToByteArray());
    }
  }

  private class PersistentStateFormatter : BinaryStateFormatter.IValueFormatter
  {
    public object Read(BinaryStateFormatter.ReaderProps props)
    {
      PersistentState state = new PersistentState();
      props.Formatter.InternalRead(props, state);
      return (object) state;
    }

    public void Write(BinaryStateFormatter.WriterProps props, object value)
    {
      props.Formatter.InternalWrite(props, (PersistentState) value);
    }
  }
}
