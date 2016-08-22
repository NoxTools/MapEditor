using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;

namespace NoxShared
{
	public class XML
	{
		public static void exportClassToXml(Object obj, System.IO.Stream stream)
		{
			Type objType = obj.GetType();
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.CloseOutput = false;
			XmlWriter xmlWriter = XmlWriter.Create(stream, settings);

			xmlWriter.WriteWhitespace("\n");
			xmlWriter.WriteStartElement(objType.Name);
			xmlWriter.WriteWhitespace("\n");
			foreach (System.Reflection.FieldInfo mi in objType.GetFields())
			{
				xmlWriter.WriteStartElement(mi.Name);
				switch (mi.FieldType.FullName)
				{
					case "NoxShared.Map+Object+Property":
						xmlWriter.WriteValue((short)mi.GetValue(obj));
						break;
					case "System.Drawing.PointF":
						xmlWriter.WriteStartAttribute("type");
						xmlWriter.WriteString(typeof(System.Drawing.PointF).FullName);
						xmlWriter.WriteEndAttribute();
						xmlWriter.WriteValue(String.Format("{0} {1}", ((System.Drawing.PointF)mi.GetValue(obj)).X, ((System.Drawing.PointF)mi.GetValue(obj)).Y));
						break;
					case "System.Drawing.Point":
						xmlWriter.WriteStartAttribute("type");
						xmlWriter.WriteString(typeof(System.Drawing.Point).FullName);
						xmlWriter.WriteEndAttribute();
						xmlWriter.WriteValue(String.Format("{0} {1}", ((System.Drawing.Point)mi.GetValue(obj)).X, ((System.Drawing.Point)mi.GetValue(obj)).Y));
						break;
					case "System.Byte[]":
						String temp = "";
						foreach (byte b in (Byte[])mi.GetValue(obj))
							temp += String.Format("{0:x2} ", b);
						xmlWriter.WriteStartAttribute("type");
						xmlWriter.WriteString(typeof(System.Byte[]).FullName);
						xmlWriter.WriteEndAttribute();
						xmlWriter.WriteString(temp);
						break;
					case "System.Drawing.Color":
						xmlWriter.WriteValue(((System.Drawing.Color)mi.GetValue(obj)).ToArgb());
						break;
					case "NoxShared.Map+Object":
						break;
					default:
						xmlWriter.WriteValue(mi.GetValue(obj));
						break;
				}
				xmlWriter.WriteEndElement();
				xmlWriter.WriteWhitespace("\n");
			}
			xmlWriter.WriteEndElement();
			xmlWriter.Close();
		}
		public static System.Object importXml(System.IO.Stream stream)
		{
			Type objType = null;
			Object member = null;
			FieldInfo fi;
			String temp = "";
			XmlReader xmlReader = XmlReader.Create(stream);
			while (xmlReader.Read())
			{
				switch (xmlReader.Name)
				{
					case "Object":
						member = new Map.Object();
						objType = member.GetType();
						break;
					default:
						break;
				}
				if (member != null)
					break;
			}
			if (member == null)
			{
				xmlReader.Close();
				return member;
			}
			while (xmlReader.Read())
			{
				if (xmlReader.IsStartElement())
					if (!xmlReader.IsEmptyElement)
					{
						fi = objType.GetField(xmlReader.Name);
						switch (fi.FieldType.FullName)
						{
							case "System.Drawing.PointF":
								temp = xmlReader.ReadElementContentAsString();
								fi.SetValue(member, new System.Drawing.PointF(Convert.ToSingle(temp.Split(' ')[0]), Convert.ToSingle(temp.Split(' ')[1])));
								break;
							case "System.Byte[]":
								temp = xmlReader.ReadElementContentAsString();
								if (temp.Length > 0)
								{
									System.IO.MemoryStream memstream = new System.IO.MemoryStream();
									System.IO.BinaryWriter wtr = new System.IO.BinaryWriter(memstream);
									System.Text.RegularExpressions.Regex bytes = new System.Text.RegularExpressions.Regex("[0-9|a-f|A-F]{2}");
									foreach (System.Text.RegularExpressions.Match match in bytes.Matches(temp))
										wtr.Write(Convert.ToByte(match.Value, 16));
									fi.SetValue(member, memstream.ToArray());
									wtr.Close();
								}
								break;
							case "NoxShared.Map+Object+Property":
								fi.SetValue(member, xmlReader.ReadElementContentAs(typeof(System.Int16), null));
								break;
							case "System.Collections.ArrayList":
								break;
							case "System.Collections.Generic.List<>":
								break;
							default:
								try
								{
									fi.SetValue(member, xmlReader.ReadElementContentAs(fi.FieldType, null));
								}
								catch
								{
								}
								break;
						}

					}
			}
			xmlReader.Close();
			return member;
		}
	}
}
