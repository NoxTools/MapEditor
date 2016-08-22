using System;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Generic;

using System.Text;

namespace NoxShared
{
	public class Map
	{
		// ----PUBLIC MEMBERS----
		public MapInfo Info = new MapInfo();

		public bool Encrypted = true;//whether map will be encrypted upon saving
		//encrypt file unless this is explicitly set to false
		//  OR we find upon loading that the file was originally unencrypted

		public FloorMap Tiles = new FloorMap();
		public ObjectTable Objects = new ObjectTable();//contains objects, why would we reference them by location? -- TODO: enforce uniqueness of PointF key? <-- rethink this. Should this be a map at all? if so, we need to allow for multiple objects at the same point(?)
		public PolygonList Polygons = new PolygonList();
        public WaypointList Waypoints = new WaypointList();
        public ScriptObject Scripts = new ScriptObject();
		public GroupData Groups = new GroupData();
		public DebugData DebugD = new DebugData();
		public ScriptData ScriptD = new ScriptData();
		public AmbientData Ambient = new AmbientData();
		public MapIntro Intro = new MapIntro();
		public WallMap Walls = new WallMap();

		// ----PROTECTED MEMBERS----
		protected MapHeader Header = new MapHeader();
		public string FileName = "";
		protected Hashtable Headers = new Hashtable();//contains the headers for each section or the complete section

		// ----CONSTRUCTORS----
		public Map() { }

		public Map(string filename) : this()
		{
			Load(filename);
		}
        public Map(NoxBinaryReader rdr)
            : this()
		{
			Load(rdr);
		}
        public void Load(NoxBinaryReader rdr)
        {
            ReadFile(rdr);
        }
		public void Load(string filename)
		{
			FileName = filename;
			ReadFile();
		}

		#region Inner Classes and Enumerations

		public abstract class Section
		{
			protected virtual string SectionName { get { return GetType().Name; } }//return derived class name if not overidden
			protected long finish;

			public Section() {}
			public Section(Stream stream) {Read(stream);}

			protected void Read(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);
				finish = rdr.ReadInt64() + rdr.BaseStream.Position;

				ReadContents(rdr.BaseStream);

				Debug.Assert(rdr.BaseStream.Position == finish, "(Read, " + SectionName + ") Bad read length.");
			}

			protected abstract void ReadContents(Stream stream);

			public void Write(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);
				wtr.Write(SectionName + "\0");
				wtr.BaseStream.Seek((8 - wtr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
				wtr.Write((long)0);//dummy length value
				long startPos = wtr.BaseStream.Position;

				WriteContents(wtr.BaseStream);

				//rewrite the length now that we can find it
				long length = wtr.BaseStream.Position - startPos;
				wtr.Seek((int)startPos - 8, SeekOrigin.Begin);
				wtr.Write((long)length);
				wtr.Seek((int)length, SeekOrigin.Current);
			}

			protected abstract void WriteContents(Stream stream);
		}

		public abstract class SortedDictionarySection<K, V> : Section, IDictionary<K, V>, ICollection<KeyValuePair<K, V>>, IEnumerable<KeyValuePair<K, V>>
		{
			private SortedDictionary<K, V> contents;

			public SortedDictionarySection(IComparer<K> comp)//cant call base...
			{
				contents = new SortedDictionary<K, V>(comp);
			}

			public SortedDictionarySection(IComparer<K> comp, Stream stream)//cant call base...
			{
				contents = new SortedDictionary<K, V>(comp);
				Read(stream);
			}

			#region IDictionary<K,V> Members

			public void Add(K key, V value)
			{
				contents.Add(key, value);
			}

			public bool ContainsKey(K key)
			{
				return contents.ContainsKey(key);
			}

			public bool TryGetValue(K key, out V value)
			{
				return contents.TryGetValue(key, out value);
			}

			public ICollection<K> Keys
			{
				get { return contents.Keys; }
			}


			public bool Remove(K key)
			{
				return contents.Remove(key);
			}
			public ICollection<V> Values
			{
				get { return contents.Values; }
			}

			public V this[K key]
			{
				get
				{
					return contents[key];
				}

				set
				{
					contents[key] = value;
				}
			}


			#endregion

			#region ICollection<KeyValuePair<K,V>> Members

			public void Add(KeyValuePair<K, V> item)
			{
				throw new NotImplementedException();
			}

			public void Clear()
			{
				throw new NotImplementedException();
			}

			public bool Contains(KeyValuePair<K, V> item)
			{
				throw new NotImplementedException();
			}

			public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
			{
				throw new NotImplementedException();
			}
			public int Count
			{
                get { return Keys.Count; }
            }

			public bool IsReadOnly
			{
				get { throw new global::System.NotImplementedException(); }
			}


			public bool Remove(KeyValuePair<K, V> item)
			{
				throw new NotImplementedException();
			}

			#endregion

			#region IEnumerable<KeyValuePair<K,V>> Members

			IEnumerator<KeyValuePair<K, V>> System.Collections.Generic.IEnumerable<KeyValuePair<K, V>>.GetEnumerator()
			{
				throw new NotImplementedException();
			}

			#endregion

			#region IEnumerable Members

			public IEnumerator GetEnumerator()
			{
				throw new NotImplementedException();
			}

			#endregion
		}

		public class WallMap : SortedDictionarySection<Point, Wall>
		{
			public short Prefix = 1;//CHECKED
			public int Var1;
			public int Var2;
			public int Var3;
			public int Var4;

			public WallMap() : base(new LocationComparer()) {}
			public WallMap(Stream stream) : base(new LocationComparer(), stream) {}

			protected override void ReadContents(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);

				Prefix = rdr.ReadInt16();
				Var1 = rdr.ReadInt32();
				Var2 = rdr.ReadInt32();
				Var3 = rdr.ReadInt32();
				Var4 = rdr.ReadInt32();

				byte x, y;
				while ((x = rdr.ReadByte()) != 0xFF && (y = rdr.ReadByte()) != 0xFF)//we'll get an 0xFF for x to signal end of section
				{
					rdr.BaseStream.Seek(-2, SeekOrigin.Current);
					Wall wall = new Wall(rdr.BaseStream);
					//NOTE: not checking for duplicates (there should never be any)
					Add(wall.Location, wall);
				}
			}

			protected override void WriteContents(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);

				wtr.Write((short)Prefix);
				wtr.Write((int)Var1);
				wtr.Write((int)Var2);
				wtr.Write((int)Var3);
				wtr.Write((int)Var4);

				foreach (Wall wall in Values)
					wall.Write(wtr.BaseStream);
				wtr.Write((byte)0xFF);//wallmap terminates with this byte
			}
		}

		public class AmbientData : Section
		{
			//AmbientData seems to always have a short 1 followed by three very small (usually only 1 byte is used) ints
			public short Prefix = 1;//CHECKED
			public int Var1;
			public int Var2;
			public int Var3;

			public AmbientData() : base() {}
			public AmbientData(Stream stream) : base(stream) {}

			protected override void ReadContents(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);

				Prefix = rdr.ReadInt16();
				Var1 = rdr.ReadInt32();
				Var2 = rdr.ReadInt32();
				Var3 = rdr.ReadInt32();

				Debug.WriteLineIf(Prefix != 1, "Prefix not 1: 0x" + Prefix.ToString("x"), "AmbientData.Read");
			}

			protected override void WriteContents(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);

				wtr.Write((short)Prefix);
				wtr.Write((int)Var1);
				wtr.Write((int)Var2);
				wtr.Write((int)Var3);
			}
		}

		public class ScriptData : Section
		{
			public short Prefix = 1;//CHECKED
			public byte Count = 0;//always zero? //CHECKED
			public List<ScriptDataEntry> entries = new List<ScriptDataEntry>();
			private byte[] data;

			public ScriptData() : base() {}
			public ScriptData(Stream stream) : base(stream) {}

			protected override void ReadContents(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);

				Prefix = rdr.ReadInt16();
				Count = rdr.ReadByte();

				// Use then when we understand it totally
				/*for (int i = 0; i < Count; i++)
					entries.Add(new ScriptDataEntry(rdr));*/
				data = rdr.ReadBytes((int)(finish - rdr.BaseStream.Position));

				Debug.WriteLineIf(Prefix != 1, "Prefix not 1: 0x" + Prefix.ToString("x"), "ScriptData.Read");
			}

			protected override void WriteContents(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);

				wtr.Write((short)Prefix);
				wtr.Write((byte)Count);
				//wtr.Write((byte)entries.Count);
				/*foreach (ScriptDataEntry sde in entries)
					sde.Write(wtr);*/
				wtr.Write(data);
			}

			public class ScriptDataEntry
			{
				Int64 Unknown1;
				Int64 Unknown2;
				Int16 Unknown3; //Might be a count
				Int64 Unknown4;

				public ScriptDataEntry()
				{
				}
				public ScriptDataEntry(BinaryReader rdr)
				{
					Read(rdr);
				}
				public void Read(BinaryReader rdr)
				{
					Unknown1 = rdr.ReadInt64();
					Unknown2 = rdr.ReadInt64();
					Unknown3 = rdr.ReadInt16();
					Unknown4 = rdr.ReadInt64();
					Debug.WriteLineIf(Unknown3 != 1, "Unknown3 not 1: 0x" + Unknown3.ToString("x"), "ScriptData.ScriptDataEntry.Read");
				}
				public void Write(BinaryWriter wtr)
				{
					wtr.Write(Unknown1);
					wtr.Write(Unknown2);
					wtr.Write(Unknown3);
					wtr.Write(Unknown4);
				}
			}
		}

		public class MapIntro : Section
		{
			public short numSubSections = 1;//CHECKED
			//public int Var1;//always zero? //CHECKED
            public String text;

            public MapIntro() : base() {}
			public MapIntro(Stream stream) : base(stream) { }

			protected override void ReadContents(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);
				numSubSections = rdr.ReadInt16();
				Debug.WriteLineIf(numSubSections != 1, "SubSections not 1: 0x" + numSubSections.ToString("x"), "MapIntro.Read");
                text = new String(rdr.ReadChars(rdr.ReadInt32())); //entire subSection is ascii text; rdr.ReadInt32 is subSection size
            }

			protected override void WriteContents(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);

				wtr.Write((short)numSubSections);
				wtr.Write((int)text.Length);
                wtr.Write(text.ToCharArray());
            }
		}

		public class DebugData : Section
		{
			public short Unknown = 1;//CHECKED
			public int Unknown2;//probably a count //CHECKED
			protected byte[] data;

			public DebugData() : base() {}
			public DebugData(Stream stream) : base(stream) {}


			protected override void ReadContents(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);

				Unknown = rdr.ReadInt16();
				Unknown2 = rdr.ReadInt32();

				//TODO: skip the rest for now
				data = rdr.ReadBytes((int)(finish - rdr.BaseStream.Position));

				Debug.WriteLineIf(Unknown != 1, "Unknown short is not 1: 0x" + Unknown.ToString("x"), "Map.DebugData.Read");
				Debug.WriteLineIf(Unknown2 != 0, "Unknown long is not 0 (probably a count!): 0x" + Unknown2.ToString("x"), "Map.DebugData.Read");
			}

			protected override void WriteContents(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);

				wtr.Write((short)Unknown);
				wtr.Write((int)Unknown2);
				wtr.Write(data);
			}
		}

        public class Group : ArrayList
        {
            public GroupTypes type;
            public string name;
            public int id;
            public Group(string n, GroupTypes t, int i) : base()
            {
                type = t;
                name = n;
                id = i;
            }
            public enum GroupTypes : byte
            {
                objects = 0,
                waypoint = 1, // This could be used for other stuff; only saw in Con01A
                walls = 2
            };

			public override string ToString()
			{
				return String.Format("{0} {1}", name, Enum.GetName(typeof(GroupTypes),type));
			}
        }

		public class GroupData : SortedDictionarySection<String,Group>
		{
			public short Unknown = 3;//CHECKED
			protected byte[] data;

            public GroupData() : base(StringComparer.CurrentCulture) {}
            public GroupData(Stream stream) : base(StringComparer.CurrentCulture, stream) { }

            protected override void ReadContents(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);

				Unknown = rdr.ReadInt16();
				int count = rdr.ReadInt32();
				//TODO: skip the rest for now
				//data = rdr.ReadBytes((int) (finish - rdr.BaseStream.Position));
                int size = 0;
                for (int i = 0; i < count; i++)
                {
                    Group grp = new Group(rdr.ReadString(), (Group.GroupTypes)rdr.ReadByte(), rdr.ReadInt32());
                    size = rdr.ReadInt32();
                    for (int k = 0; k < size; k++)
                    {
                        switch (grp.type)
                        {
                            case Group.GroupTypes.walls:
                                grp.Add(new Point(rdr.ReadInt32(), rdr.ReadInt32()));
                                break;
                            case Group.GroupTypes.waypoint:
                            case Group.GroupTypes.objects:
                                grp.Add(rdr.ReadInt32());
                                break;
                            default:
                                Debug.WriteLine("Unknown type 0x" + grp.type.ToString("x"), "Map.GroupData.Read");
                                break;
                        }
                    }
                    if(!ContainsKey(grp.name))
                        Add(grp.name,grp);
                }

                Debug.WriteLineIf(Unknown != 3, "Unknown was not 3, it was: 0x" + Unknown.ToString("x"), "Map.GroupData.Read");
			}

            protected override void WriteContents(Stream stream)
            {
				BinaryWriter wtr = new BinaryWriter(stream);

				int index = Count - 1;
				wtr.Write((short)Unknown);
				wtr.Write((int)Count);
				foreach(Group grp in Values)
                {
                    wtr.Write(grp.name);
                    wtr.Write((byte)grp.type);
                    wtr.Write(index);
                    wtr.Write(grp.Count);
                    foreach (System.Object obj in grp)
                    {
                        if (obj.GetType() == typeof(Int32))
                            wtr.Write((Int32)obj);
                        else if (obj.GetType() == typeof(Point))
                        {
                            wtr.Write(((Point)obj).X);
                            wtr.Write(((Point)obj).Y);
                        }
                        else
                            Debug.WriteLine("Unknown type 0x" + grp.type.ToString("x"), "Map.GroupData.Write");
                    }
					index--;
                }
			}
		}

		public class PolygonList : ArrayList
		{
			public short TermCount;//seems to control amount of useless data at the end??? FIXME: set to 3 or 4 by default but which?
			public List<PointF> Points = new List<PointF>();

			public PolygonList() {}
			public PolygonList(Stream stream)
			{
				Read(stream);
			}

			protected void Read(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);
				long finish = rdr.ReadInt64() + rdr.BaseStream.Position;

				TermCount = rdr.ReadInt16();
				
				int numPoints = rdr.ReadInt32();
				while (numPoints-- > 0)
				{
					rdr.ReadInt32();//the point number. NOTE: I'm assuming they are in ascending order!!
					Points.Add(new PointF(rdr.ReadSingle(), rdr.ReadSingle()));
				}

				int numPolygons = rdr.ReadInt32();
				while (numPolygons-- > 0)
					Add(new Polygon(rdr.BaseStream, this));

				Debug.Assert(rdr.BaseStream.Position == finish, "(Map, Polygons) Bad read length.");
			}

			public void Write(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);
				wtr.Write("Polygons\0");
				wtr.BaseStream.Seek((8 - wtr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
				wtr.Write((long) 0);//dummy length value
				long startPos = wtr.BaseStream.Position;
				
				wtr.Write((short) TermCount);

				//rebuild point list
				Points.Clear();
				foreach (Polygon poly in this)
					foreach (PointF pt in poly.Points)
						if (!Points.Contains(pt))
							Points.Add(pt);

				//TODO: sort this before writing?
				wtr.Write((int) Points.Count);
				foreach (PointF pt in Points)
				{
					wtr.Write((int) (Points.IndexOf(pt)+1));
					wtr.Write((float) pt.X);
					wtr.Write((float) pt.Y);
				}

				wtr.Write((int) this.Count);
				foreach (Polygon poly in this)
					poly.Write(wtr.BaseStream, this);

				//rewrite the length now that we can find it
				long length = wtr.BaseStream.Position - startPos;
				wtr.Seek((int) startPos - 8, SeekOrigin.Begin);
				wtr.Write((long) length);
				wtr.Seek((int) length, SeekOrigin.Current);
			}
		}

		public class Polygon
		{
			public string Name;
            public string EnterFunc;
            public Color AmbientLightColor;//the area's ambient light color
			public byte MinimapGroup;//the visible wall group when in this area
			public List<PointF> Points = new List<PointF>();//the unindexed points that define the polygon
			protected byte[] endbuf;
            protected short Unknown1 = 1;

            public Polygon(string name, Color ambient, byte mmGroup, List<PointF> points, string enterfunc)
			{
				Name = name;
				AmbientLightColor = ambient;
				MinimapGroup = mmGroup;
				Points = points;
                EnterFunc = enterfunc;
                //N.B. that endbuf is left null here
			}

			public Polygon(Stream stream, PolygonList list)
			{
				Read(stream, list);
			}

			//FIXME, TODO: figure out what the "termCount" is about
			// and remove dependency on
			// the parent polygon list for reading and writing
			protected void Read(Stream stream, PolygonList list)
			{
				BinaryReader rdr = new BinaryReader(stream);

				Name = rdr.ReadString();
				AmbientLightColor = Color.FromArgb(rdr.ReadByte(), rdr.ReadByte(), rdr.ReadByte());
				MinimapGroup = rdr.ReadByte();

				short ptCount = rdr.ReadInt16();
				while (ptCount-- > 0)
					Points.Add(list.Points[rdr.ReadInt32()-1]);

				//TODO: figure this out?? really weird...
				//  termCount of 0x0004 means we end with the normal unknown endbuf of the last polygon
				//  termCount of 0x0003 means we omit the last 4 (null) bytes.
				//always "01 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00" or 4 shorter?
                System.IO.MemoryStream nStream = new System.IO.MemoryStream();
                System.IO.BinaryWriter wtr = new System.IO.BinaryWriter(nStream);

                Unknown1 = rdr.ReadInt16();
                Debug.WriteLineIf(Unknown1 != 1, String.Format("Unknown1 != 1, it is: {0}", Unknown1), "Map.Polygon.Read");
                EnterFunc = new string(rdr.ReadChars(rdr.ReadInt32()));
				wtr.Write(rdr.ReadInt32());
				wtr.Write(rdr.ReadInt16());
				string temp = new string(rdr.ReadChars(rdr.ReadInt32()));
				wtr.Write((int)temp.Length);
				wtr.Write(temp.ToCharArray());

				if (list.TermCount == 0x0003)
					wtr.Write(rdr.ReadBytes(4));
				else if (list.TermCount == 0x0004)
					wtr.Write(rdr.ReadBytes(8));
				else
					Debug.Assert(false, "(Map, Polygons) Unhandled terminal count.");
				endbuf = nStream.ToArray();
			}

			public void Write(Stream stream, PolygonList list)
			{
				BinaryWriter wtr = new BinaryWriter(stream);
				wtr.Write(Name);
				wtr.Write((byte) AmbientLightColor.R);
				wtr.Write((byte) AmbientLightColor.G);
				wtr.Write((byte) AmbientLightColor.B);
				wtr.Write((byte) MinimapGroup);
				wtr.Write((short) Points.Count);
				foreach (PointF pt in Points)
					wtr.Write((int) (list.Points.IndexOf(pt)+1));
                wtr.Write(Unknown1);
                wtr.Write(EnterFunc.Length);
                wtr.Write(EnterFunc.ToCharArray());
                if (endbuf != null)
					wtr.Write(endbuf);
				else
				{
					if (list.TermCount == 0x0003)
						wtr.Write(new byte[] { 00, 00, 00, 00, 01, 00, 00, 00, 00, 00, 00, 00, 00, 00});
					else if (list.TermCount == 0x0004)
						wtr.Write(new byte[] { 00, 00, 00, 00, 01, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00, 00});
					else
						Debug.Assert(false, "(Map, Polygons) Unhandled terminal count.");
				}
			}
			public override string ToString()
			{
				return String.Format("{0} {1} {2}, {3}", Name, MinimapGroup, Points[0].X,Points[1].Y);
			}
		}
        public class WaypointList : ArrayList
        {
            public short TermCount;//seems to control amount of useless data at the end???
            public Hashtable num_wp = new Hashtable();

            public WaypointList() { }
            public WaypointList(Stream stream)
            {
                Read(stream);
            }

            protected void Read(Stream stream)
            {
                BinaryReader rdr = new BinaryReader(stream);
                long finish = rdr.ReadInt64() + rdr.BaseStream.Position;
                TermCount = rdr.ReadInt16();
                int numWaypoints = rdr.ReadInt32();
                while (numWaypoints-- > 0)
                {
                    Waypoint wp = new Waypoint(rdr.BaseStream, this);
                    Add(wp);
                    num_wp.Add(wp.num, wp);
                }
                foreach (Waypoint wp in this)
                {
                    ArrayList conn = (ArrayList)wp.connections.Clone();
                    foreach (Waypoint.WaypointConnection wpc in conn)
                    {
                        if (num_wp.ContainsKey(wpc.wp_num))
                            wpc.wp = (Waypoint)num_wp[wpc.wp_num];
                        else
                            wp.connections.Remove(wpc);
                    }
                }
             
                Debug.Assert(rdr.BaseStream.Position == finish, "(Map, Waypoints) Bad read length.");
            }
            public void SetNameFromPoint(Point pt, string name)
            {
                //Map.Waypoints
                int mod = (7 * 2);
                foreach (Waypoint wp in this)
                {
                    float lowX = wp.Point.X - mod;
                    float highX = wp.Point.X + mod;
                    float lowY = wp.Point.Y - mod;
                    float highY = wp.Point.Y + mod;

                    if (pt.X >= lowX && pt.X <= highX && pt.Y >= lowY && pt.Y <= highY)
                    {
                        wp.Name = name;
                    }
                }
            }
            public Waypoint GetWPFromPoint(Point pt)
            {
                //Map.Waypoints
                int mod = (7 * 2);
                foreach (Waypoint wp in this)
                {
                    float lowX = wp.Point.X - mod;
                    float highX = wp.Point.X + mod;
                    float lowY = wp.Point.Y - mod;
                    float highY = wp.Point.Y + mod;

                    if (pt.X >= lowX && pt.X <= highX && pt.Y >= lowY && pt.Y <= highY)
                    {
                        return (wp);
                    }
                }
                return (null);
            }
            public override void Remove(object obj)
            {
                foreach (Waypoint wp in this)
                {
                    ArrayList conn = (ArrayList)wp.connections.Clone();
                    foreach (Waypoint.WaypointConnection wc in conn)
                        if (wc.wp == obj)
                            wp.connections.Remove(wc);
                }
                base.Remove(obj);
            }

            public void Write(Stream stream)
            {
                BinaryWriter wtr = new BinaryWriter(stream);
                wtr.Write("WayPoints\0");
                wtr.BaseStream.Seek((8 - wtr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
                wtr.Write((long)0);//dummy length value
                long startPos = wtr.BaseStream.Position;

                wtr.Write(TermCount);
                wtr.Write((int)this.Count);
                int i = this.Count;
                foreach (Waypoint wp in this)
                {
                    wtr.Write(wp.num);
                    wp.Write(wtr.BaseStream, this);
                    foreach (Waypoint.WaypointConnection wpc in wp.connections)
                    {
                        wtr.Write(wpc.wp.num);
                        wtr.Write(wpc.flag);
                    }
                }

                //rewrite the length now that we can find it
                long length = wtr.BaseStream.Position - startPos;
                wtr.Seek((int)startPos - 8, SeekOrigin.Begin);
                wtr.Write((long)length);
                wtr.Seek((int)length, SeekOrigin.Current);
            }
        }

        public class Waypoint
        {
            public string Name;
            public PointF Point;
            public ArrayList connections = new ArrayList();
            public int enabled;
            public int num;

            public Waypoint(string name, PointF point, int number)
            {
                Name = name;
                Point = point;
                num = number;
            }

            public Waypoint(Stream stream, WaypointList list)
            {
                Read(stream, list);
            }
           
            protected void Read(Stream stream, WaypointList list)
            {
                BinaryReader rdr = new BinaryReader(stream);
                num = rdr.ReadInt32(); //Waypoint number
                Point = new PointF(rdr.ReadSingle(), rdr.ReadSingle());
                Name = rdr.ReadString();
                enabled = rdr.ReadInt32();
                for (int i = rdr.ReadByte(); i > 0; i--)
                {
                    connections.Add(new WaypointConnection(rdr.ReadInt32(),rdr.ReadByte()));
                }
            }

            public void Write(Stream stream, WaypointList list)
            {
                BinaryWriter wtr = new BinaryWriter(stream);
                wtr.Write(Point.X);
                wtr.Write(Point.Y);
                wtr.Write(Name);
                wtr.Write(enabled);
                wtr.Write((byte)(connections.Count));
            }
            public class WaypointConnection
            {
                public int wp_num;
                public static byte DefaultFlag = 0x80;
                public Waypoint wp;
                public byte flag;
                public WaypointConnection(int num, byte flg)
                {
                    wp_num = num;
                    flag = flg;
                }
                public WaypointConnection(Waypoint wayp, byte flg)
                {
                    wp = wayp;
                    flag = flg;
                }
				public override string ToString()
				{
					return String.Format("{0} ({1})", wp, flag);
				}
            }
            public void RemoveConnByNum(Waypoint wp)
            {
                foreach (WaypointConnection wc in this.connections)
                {
                    if (wc.wp == wp)
                    {
                        this.connections.Remove(wc);
                        return;
                    }
                }
            }
            public void AddConnByNum(Waypoint wp, byte tag)
            {
                foreach (WaypointConnection wc in this.connections)
                {
                    if (wc.wp_num == num)
                    {
                        return;
                    }
                }
                connections.Add(new WaypointConnection(wp,tag));
            }
			public override string ToString()
			{
				return String.Format("{2}: {3} {0}, {1}", Point.X, Point.Y, num, Name);
			}
        }
        public class Tile
		{
            public bool hascolor;
            public Color col;
			public Point Location;
			public byte graphicId;
			public UInt16 Variation;
			public ArrayList EdgeTiles = new ArrayList();

			public string Graphic
			{
				get
				{
					return ((ThingDb.Tile) ThingDb.FloorTiles[graphicId]).Name;
				}
			}

			public List<uint> Variations
			{
				get
				{
					return ((ThingDb.Tile) ThingDb.FloorTiles[graphicId]).Variations;
				}
			}

			public Tile(Point loc, byte graphic, UInt16 variation, ArrayList edgetiles)
			{
                hascolor = false;
                Location = loc;
				graphicId = graphic; Variation = variation;
				EdgeTiles = edgetiles;
			}

			public Tile(Point loc, byte graphic, UInt16 variation) : this(loc, graphic, variation, new ArrayList()) {}

			public Tile(Stream stream)
			{
				Read(stream);
			}

			public void Read(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);
				graphicId = rdr.ReadByte();
				Variation = rdr.ReadUInt16();
				rdr.ReadBytes(2);//these are always null for first tilePair of a blending group (?)
				for (int numEdgeTiles = rdr.ReadByte(); numEdgeTiles > 0; numEdgeTiles--)
					EdgeTiles.Add(new EdgeTile(rdr.BaseStream));
			}

			public void Write(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);

				wtr.Write((byte )graphicId);
				wtr.Write((UInt16) Variation);
				wtr.Write(new byte[2]);//3 nulls
				wtr.Write((byte) EdgeTiles.Count);
				foreach(EdgeTile edge in EdgeTiles)
					edge.Write(stream);
			}

			public class EdgeTile//maybe derive from tile?
			{
				public byte Graphic;
				public UInt16 Variation;
				public byte unknown1 = 0x00; //Always 00(?)
				public Direction Dir;
				public byte Edge;

				public enum Direction : byte
				{
					SW_Tip,//0x00
					West,
					West_02,
					West_03,
					NW_Tip,//0x04
					South,
					North,
					South_07,
					North_08,//0x08
					South_09,
					North_0A,
					SE_Tip,
					East,//0x0C
					East_D,
					East_E,
					NE_Tip,
					SW_Sides,//0x10
					NW_Sides,
					NE_Sides,
					SE_Sides//0x13
					//TODO: figure out what's up with the different directions
				}

				public EdgeTile(byte graphic, ushort variation, Direction dir, byte edge)
				{
					Graphic = graphic; Variation = variation; Dir = dir; Edge = edge;
				}

				public EdgeTile(Stream stream)
				{
					Read(stream);
				}

				public void Read(Stream stream)
				{
					BinaryReader rdr = new BinaryReader(stream);

					Graphic = rdr.ReadByte();
					Variation = rdr.ReadUInt16();
					Edge = rdr.ReadByte();
					Dir = (Direction) rdr.ReadByte();

					//Debug.WriteLineIf(unknown1 != 0, String.Format("WARNING: tile unknown byte was not 0, it was {0}.", unknown1), "MapRead");
					Debug.WriteLineIf(!Enum.IsDefined(typeof(Direction), (byte) Dir), String.Format("WARNING: edgetile direction {0} is undefined.", (byte) Dir), "MapRead");
				}

				public void Write(Stream stream)
				{
					BinaryWriter wtr = new BinaryWriter(stream);

					wtr.Write((byte) Graphic);
					wtr.Write((UInt16) Variation);
					wtr.Write((byte) Edge);
					wtr.Write((byte) Dir);
				}
			}
			public override string ToString()
			{
				return String.Format("{0}, {1} {2}", Location.X, Location.Y, Graphic);
			}
		}

		public class FloorMap : SortedDictionarySection<Point, Tile>
		{
			public short Prefix;
			public int Var1;
			public int Var2;
			public int Var3;
			public int Var4;

			public FloorMap() : base(new LocationComparer()) {}
			public FloorMap(Stream stream) : base(new LocationComparer(), stream) {}

			protected override void ReadContents(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);

				List<TilePair> tilePairs = new List<TilePair>();

				Prefix = rdr.ReadInt16();
				Var1 = rdr.ReadInt32();
				Var2 = rdr.ReadInt32();
				Var3 = rdr.ReadInt32();
				Var4 = rdr.ReadInt32();

				Debug.WriteLine(String.Format("contents of header: 0x{0:x} 0x{1:x} 0x{2:x} 0x{3:x} 0x{4:x}", Prefix, Var1, Var2, Var3, Var4));

				while (true)//we'll get an 0xFF for both x and y to signal end of section
				{
					byte y = rdr.ReadByte();
					byte x = rdr.ReadByte();

					if (y == 0xFF && x == 0xFF)
						break;

					rdr.BaseStream.Seek(-2, SeekOrigin.Current);//rewind back to beginning of current entry
					TilePair tilePair = new TilePair(rdr.BaseStream);
					if (!tilePairs.Contains(tilePair))//do not add duplicates (there should not be duplicate entries in a file anyway)
						tilePairs.Add(tilePair);
				}

				foreach (TilePair tp in tilePairs)
				{
					if (tp.Left != null) this.Add(tp.Left.Location, tp.Left);
					if (tp.Right != null) this.Add(tp.Right.Location, tp.Right);
				}
			}

			protected override void WriteContents(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);

				wtr.Write((short)Prefix);
				wtr.Write((int)Var1);
				wtr.Write((int)Var2);
				wtr.Write((int)Var3);
				wtr.Write((int)Var4);

				//generate the TilePairs...
				ArrayList tilePairs = new ArrayList();

				//dumb replacement for clone()
				SortedDictionary<Point, Tile> tiles = new SortedDictionary<Point, Tile>(new LocationComparer());
				foreach (Point key in Keys)
					tiles.Add(key, this[key]);

				List<Tile> tileList = new List<Tile>(tiles.Values);
				List<Tile>.Enumerator tEnum = tileList.GetEnumerator();
				while (tEnum.MoveNext())
				{
					Tile left = null, right = null;
					Tile tile1 = tEnum.Current;
					if (tile1.Location.X % 2 == 1)//we got a right tile. the right tile will always come before it's left tile
					{
						right = tile1;
						Point t2p = new Point(tile1.Location.X - 1, tile1.Location.Y + 1);
						if (tiles.ContainsKey(t2p))
							left = tiles[t2p];
						tilePairs.Add(new TilePair((byte) ((right.Location.X-1)/2), (byte) ((right.Location.Y+1)/2), left, right));
					}
					else //assume that this tile is a single since the ordering would have forced the right tile to be handled first
					{
						left = tile1;
						tilePairs.Add(new TilePair((byte) (left.Location.X/2), (byte) (left.Location.Y/2), left, right));
					}
					if (left != null) tiles.Remove(left.Location);
					if (right != null) tiles.Remove(right.Location);
				}

				//... and write them
				foreach (TilePair tilePair in tilePairs)
					tilePair.Write(wtr.BaseStream);

				wtr.Write((ushort) 0xFFFF);//terminating x and y
			}
		}

		protected class MapHeader
		{
			private const int LENGTH = 0x18;
			private const uint FILE_ID = 0xFADEFACE;
			public int CheckSum;//checksum for rest of file. used in determining whether download is necessary.
			public int u1;//UNKNOWN
			public int u2;//UNKNOWN

			public MapHeader() { }
			public MapHeader(Stream stream)
			{
				Read(stream);
			}

			public void Read(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);
				int id = rdr.ReadInt32();//first int is always FADEFACE
				Debug.Assert((uint)id == FILE_ID);

				int check;
				check = rdr.ReadInt32();
				Debug.WriteLineIf(check != 0, "int in header was not null: 0x" + check.ToString("x"), "MapHeader.Read");
				CheckSum = rdr.ReadInt32();
				check = rdr.ReadInt32();
				Debug.WriteLineIf(check != 0, "int in header was not null: 0x" + check.ToString("x"), "MapHeader.Read");
				u1 = rdr.ReadInt32();
				u2 = rdr.ReadInt32();

				Debug.WriteLine("u1 is 0x" + u1.ToString("x"), "MapHeader.Read");
				Debug.WriteLine("u2 is 0x" + u2.ToString("x"), "MapHeader.Read");
			}

			public void Write(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);

				wtr.Write(FILE_ID);
				wtr.Write((int) 0);
				wtr.Write((int) CheckSum);
				wtr.Write((int) 0);
				wtr.Write((int) u1);
				wtr.Write((int) u2);
			}

			public void GenerateChecksum(byte[] data)
			{
				CheckSum = Crc32.Calculate(data);
			}
		}

		public class MapInfo : Section
		{
			public short unknown = 2;//CHECKED
			public string Summary;//the map's brief name
			public string Description;//the map's long description
			public string Author;
			public string Email;
			public string Author2;
			public string Email2;
			public string Version;//the map's current version
			public string Copyright;
			public string Date;
			public MapType Type = MapType.ARENA;
			public byte RecommendedMin;
			public byte RecommendedMax;
			public String QIntroTitle = "";
			public String QIntroGraphic = "";

			protected override string SectionName { get { return "MapInfo"; } }
			public MapInfo() : base() { }
			public MapInfo(Stream stream) : base(stream) { }

			public enum MapType : uint
			{
				SOLO = 0x00000001,
				QUEST = 0x00000002,
				SOLO_T = 0x00000003,
				ARENA = 0x00000034,
				CTF = 0x00000018,
				SOCIAL = 0x80000000,
				FLAGBALL = 0x00000040
			}

			public static SortedList MapTypeNames;

			static MapInfo()
			{
				MapTypeNames = new SortedList();

				MapTypeNames.Add(MapType.ARENA, "Arena");
				MapTypeNames.Add(MapType.CTF, "CTF");
				MapTypeNames.Add(MapType.SOCIAL, "Social");
				MapTypeNames.Add(MapType.QUEST, "Quest");
				MapTypeNames.Add(MapType.SOLO, "Solo");
				MapTypeNames.Add(MapType.SOLO_T, "Solo Template");
				MapTypeNames.Add(MapType.FLAGBALL, "Flagball");
			}
			const int PREFIX = 0x02;
			const int TITLE = 0x40;
			const int DESCRIPTION = 0x200;
			const int VERSION = 0x10;
			const int AUTHOR = 0x40;
			const int EMAIL = 0xC0;
			const int EMPTY = 0x80;
			const int COPYRIGHT = 0x80;//only on very few maps
			const int DATE = 0x20;
			const int TYPE = 0x04;
			const int MINMAX = 0x02;
			const int TOTAL = PREFIX + TITLE + DESCRIPTION + VERSION + 2*(AUTHOR + EMAIL) + EMPTY + COPYRIGHT + DATE + TYPE + MINMAX;

			protected override void ReadContents(Stream stream)
			{
				NoxBinaryReader rdr = new NoxBinaryReader(stream);

				unknown = rdr.ReadInt16();//dont know what this is for
				Summary = rdr.ReadString((int) TITLE);
				Description = rdr.ReadString((int) DESCRIPTION);
				Version = rdr.ReadString((int) VERSION);
				Author = rdr.ReadString((int) AUTHOR);
				Email = rdr.ReadString((int) EMAIL);
				Author2 = rdr.ReadString((int) AUTHOR);
				Email2 = rdr.ReadString((int) EMAIL);
				rdr.ReadBytes((int) EMPTY);
				Copyright = rdr.ReadString((int) COPYRIGHT);
				Date = rdr.ReadString((int) DATE);
				uint temp = rdr.ReadUInt32();
				Type = (MapType)temp;

				if (Type == MapType.QUEST)//quest maps have an extra variable length section
				{
					QIntroTitle = Encoding.ASCII.GetString(rdr.ReadBytes(rdr.ReadByte()));
					QIntroGraphic = Encoding.ASCII.GetString(rdr.ReadBytes(rdr.ReadByte()));
				}
				else
				{
					RecommendedMin = rdr.ReadByte();
					RecommendedMax = rdr.ReadByte();
				}

				Debug.WriteLineIf(unknown != 2, "Unknown in MapInfo is not 2, it is: " + unknown, "Map.MapInfo.Read");
			}

			protected override void WriteContents(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);

				wtr.Write((short) unknown);

				wtr.Write(Summary.ToCharArray());
				wtr.BaseStream.Seek((int) TITLE - Summary.Length, SeekOrigin.Current);

				wtr.Write(Description.ToCharArray());
				wtr.BaseStream.Seek((int) DESCRIPTION - Description.Length, SeekOrigin.Current);

				wtr.Write(Version.ToCharArray());
				wtr.BaseStream.Seek((int) VERSION - Version.Length, SeekOrigin.Current);

				wtr.Write(Author.ToCharArray());
				wtr.BaseStream.Seek((int) AUTHOR - Author.Length, SeekOrigin.Current);

				wtr.Write(Email.ToCharArray());
				wtr.BaseStream.Seek((int) EMAIL - Email.Length, SeekOrigin.Current);

				wtr.Write(Author2.ToCharArray());
				wtr.BaseStream.Seek((int) AUTHOR - Author2.Length, SeekOrigin.Current);

				wtr.Write(Email2.ToCharArray());
				wtr.BaseStream.Seek((int) EMAIL - Email2.Length, SeekOrigin.Current);

				wtr.BaseStream.Seek((int) EMPTY, SeekOrigin.Current);

				wtr.Write(Copyright.ToCharArray());
				wtr.BaseStream.Seek((int) COPYRIGHT - Copyright.Length, SeekOrigin.Current);

				wtr.Write(Date.ToCharArray());
				wtr.BaseStream.Seek((int) DATE - Date.Length, SeekOrigin.Current);

				wtr.Write((int) Type);

				if (Type == MapType.QUEST)
				{
					wtr.Write((byte) QIntroTitle.Length);
					wtr.Write(Encoding.ASCII.GetBytes(QIntroTitle));
					wtr.Write((byte) QIntroGraphic.Length);
					wtr.Write(Encoding.ASCII.GetBytes(QIntroGraphic));

				}
				else
				{
					wtr.Write((byte) RecommendedMin);
					wtr.Write((byte) RecommendedMax);
				}
			}
		}

		protected class LocationComparer : IComparer<Point>
		{
			public int Compare(Point lhs, Point rhs)
			{
				if (lhs.Y != rhs.Y)
					return lhs.Y - rhs.Y;
				else
					return lhs.X - rhs.X;
			}

			public bool Equals(Point lhs, Point rhs)
			{
				return lhs.Equals(rhs);
			}

			public int GetHashCode(Point p)
			{
				return p.GetHashCode();
			}
		}


		public class TilePair : IComparable
		{
			public Point Location;
			public bool OneTileOnly
			{
				get
				{
					return Left == null || Right == null;
				}
			}

			//set one of these to null if you want a single-tile entry
			public Tile Left;
			public Tile Right;

			public TilePair(byte x, byte y, Tile left, Tile right)
			{
				Location = new Point(x, y);
				Left = left;
				Right = right;
			}

			public TilePair(Stream stream)
			{
				Read(stream);
			}

			public void Read(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);

				byte y = rdr.ReadByte(), x = rdr.ReadByte();
				Location = new Point(x & 0x7F, y & 0x7F);//ignore sign bits for coordinates

				if ((x & y & 0x80) == 0)//sign bit signifies whether only the left, right, or both tilePairs are listed in this entry
				{
					if ((y & 0x80) != 0)//if y has sign bit set, then the left tilePair is specified
						Left = new Tile(stream);
					else if ((x & 0x80) != 0)
						Right = new Tile(stream);
					else
						Debug.Assert(false, "invalid x,y for tilepair entry");
				}
				else //otherwise, read right then left
				{
					Right = new Tile(stream);
					Left = new Tile(stream);
				}
				
				if (Left != null) Left.Location = new Point(Location.X * 2, Location.Y * 2);
				if (Right != null) Right.Location = new Point(Location.X * 2 + 1, Location.Y * 2 - 1);
			}

			public void Write(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);
				byte x = (byte) (Location.X | 0x80), y = (byte) (Location.Y | 0x80);

				if (OneTileOnly)
				{
					if (Left == null)
						y &= 0x7F;
					else
						x &= 0x7F;
				}

				wtr.Write((byte) y);
				wtr.Write((byte) x);

				//write the right one first
				if (Right != null)
					Right.Write(stream);
				if (Left != null)
					Left.Write(stream);
			}

			public int CompareTo(object obj)
			{
				TilePair rhs = (TilePair) obj;
				if (Location.Y != rhs.Location.Y)
					return Location.Y - rhs.Location.Y;
				else
					return Location.X - rhs.Location.X;
			}

			public override bool Equals(object obj)
			{
				return obj is TilePair && CompareTo(obj) == 0;
			}

			public override int GetHashCode()
			{
				return Location.GetHashCode();
			}
		}

		public class Wall : IComparable
		{
			public enum WallFacing : byte
			{
				NORTH,//same as SOUTH
				WEST,//same as EAST
				CROSS,
				
				SOUTH_T,
				EAST_T,//4
				NORTH_T,
				WEST_T,

				SW_CORNER,
				NW_CORNER,//8
				NE_CORNER,
				SE_CORNER//10
			}

			public Point Location;
			//TODO: Make a list of all types and have a string
			//perhaps add these in the NoxTypes namespace?
			public WallFacing Facing;
			/*protected*/public byte matId;
			public byte Variation;
			public byte Minimap = 0x64;
			protected byte alwaysNull;
			public bool Destructable;
			public bool Secret;
			public bool Window;

			public byte Variations
			{
				get
				{
					return ThingDb.Walls[matId].Variations;
				}
			}

			//these unknowns follow the wall's entry in the SecretWalls section
			// and usually (always?) have these values
			//    (initialized here so they are the default for new walls)
			public int Secret_u1 = 0x00000003;
			public uint Secret_flags;
			//the lowest bits seem to be flags of some sort, maybe a delay? ranges from 1-6
			[Flags]
			public enum SecretFlags : uint
			{
				AutoOpen = 0x2,
				AutoClose = 0x4,
				Unknown100 = 0x100
			}

			public string Material
			{
				get
				{
					return ((ThingDb.Wall) ThingDb.Walls[matId]).Name;
				}
			}

			public Wall(Stream stream)
			{
				Read(stream);
			}

			public Wall(Point loc, WallFacing facing, byte mat)
			{
				Location = loc;	Facing = facing; matId = mat;
			}

			public Wall(Point loc, WallFacing facing, byte mat, byte mmGroup, byte var)
			{
				Location = loc; Facing = facing; matId = mat; Minimap = mmGroup; Variation = var;
			}

			protected void Read(Stream stream)
			{
				BinaryReader rdr = new BinaryReader(stream);
				Location = new Point(rdr.ReadByte(), rdr.ReadByte());
				Facing = (WallFacing) (rdr.ReadByte() & 0x7F);//I'm almost certain the sign bit is just garbage and does not signify anything about the wall
				matId = rdr.ReadByte();
				Variation = rdr.ReadByte();
				Minimap = rdr.ReadByte();
				alwaysNull = rdr.ReadByte();
				Debug.WriteLineIf(Variation >= Variations, String.Format("Wall at {0} has a Variation that is out of range.", Location), "Map.Wall.Read");
				Debug.WriteLineIf(alwaysNull != 0, String.Format("Wall at {0} has non-null alwaysNull: {1}.", Location, alwaysNull), "Map.Wall.Read");
			}

			public void Write(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);

				wtr.Write((byte) Location.X);
				wtr.Write((byte) Location.Y);
				wtr.Write((byte) Facing);
				wtr.Write((byte) matId);
				wtr.Write((byte) Variation);
				wtr.Write((byte) Minimap);
				wtr.Write((byte) alwaysNull);
			}

			public int CompareTo(object obj)
			{
				Wall rhs = (Wall) obj;
				if (Location.Y != rhs.Location.Y)
					return Location.Y - rhs.Location.Y;
				else
					return Location.X - rhs.Location.X;
			}

			public override string ToString()
			{
				return String.Format("{0}, {1}", Location.X, Location.Y);
			}
		}

		public class ObjectTable : ArrayList
		{
			protected SortedList toc = new SortedList();
			protected short tocUnknown = 1;
			protected short dataUnknown = 1;
			short id = 1;

			public ArrayList extents = new ArrayList();

			/// <summary>
			/// Constructs the Object table using the given streams.
			/// </summary>
			/// <param name="toc">A stream containing the ObjectTOC, without header but with length.</param>
			/// <param name="data">A stream containing the ObjectData, without header but with length.</param>
			public ObjectTable(Stream toc, Stream data)
			{
				Read(toc, data);
			}

			public ObjectTable()
			{
			}

			public override int Add(object obj)

			{
				extents.Add(((Object) obj).Extent);
				return base.Add(obj);
			}

			public void Read(Stream toc, Stream data)
			{
				BinaryReader rdr;
				//first construct the toc
				rdr = new BinaryReader(toc);
				long finish = rdr.ReadInt64() + rdr.BaseStream.Position;

				tocUnknown = rdr.ReadInt16();//0x0001, unknown -- might be useful
				short numEntries = rdr.ReadInt16();

				while (rdr.BaseStream.Position < finish)
					this.toc.Add(rdr.ReadInt16(), rdr.ReadString());//map id to string

				Debug.Assert(rdr.BaseStream.Position == finish, "NoxMap (ObjectTOC) ERROR: bad section length");
				Debug.Assert(this.toc.Count == numEntries, "NoxMap (ObjectTOC) ERROR: wrong number of object entries");

				//now read the table and construct its objects
				rdr = new BinaryReader(data);
				finish = rdr.ReadInt64() + rdr.BaseStream.Position;
				dataUnknown = rdr.ReadInt16();//0x0001 -- useful?
				while (rdr.BaseStream.Position < finish)
				{
					if (rdr.ReadInt16() == 0)//the list is terminated by a null object Name
						break;        //the loop should break on this condition only
					else
						rdr.BaseStream.Seek(-2, SeekOrigin.Current);//roll back the short we just read
					Add(new Object(rdr.BaseStream, this.toc));
				}

				Debug.WriteLineIf(tocUnknown != 1, "tocUnknown was not 1, it was: " + tocUnknown);
				Debug.WriteLineIf(dataUnknown != 1, "dataUnknown was not 1, it was: " + dataUnknown);
				Debug.WriteLine(this.toc.Count + " objects in TOC.");
				Debug.WriteLine(Count + " objects read.");

				//check that length of section matches
				Debug.Assert(rdr.BaseStream.Position == finish, "NoxMap (ObjectDATA) ERROR: bad section length");


			}

			/// <summary>
			/// Writes the ObjectTOC and ObjectData along prefixed by their section names to the given strea.
			/// </summary>
			/// <param name="stream">The stream to write to.</param>
			public void Write(Stream stream)
			{
				BinaryWriter wtr = new BinaryWriter(stream);

				//before we start writing, we need to build a new toc
				Sort();
				toc = new SortedList();
				foreach (Object obj in this)
				{
					if (toc[obj.Name] == null)
						toc.Add(obj.Name, id++);
					foreach (Object o in obj.childObjects)
						AddEmbeddedObjects(o);
				}

				//--write the ObjectTOC--
				wtr.Write("ObjectTOC\0");
				long length = 0;
				wtr.BaseStream.Seek((8 - wtr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
				long startPos = wtr.BaseStream.Position;
				wtr.Write((long) length);//dummy value
				wtr.Write((short) tocUnknown);

				wtr.Write((short) toc.Count);

				//and write them
				foreach (string key in toc.Keys)
				{
					wtr.Write((short) toc[key]);
					wtr.Write(key);
				}

				//rewrite the length
				length = wtr.BaseStream.Position - (startPos+8);
				wtr.Seek((int)startPos, SeekOrigin.Begin);
				wtr.Write(length);
				wtr.Seek(0,SeekOrigin.End);

				//--write the ObjectTable--
				wtr.Write("ObjectData\0");
				wtr.BaseStream.Seek((8 - wtr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
				length = 0;
				startPos = wtr.BaseStream.Position;
				wtr.Write((long) length);//will be rewritten later
				wtr.Write((short) dataUnknown);
				foreach (Object obj in this)
					obj.Write(wtr.BaseStream, toc);

				wtr.Write((short) 0);//write the null Name terminator

				//rewrite the length now that we can find it
				length = wtr.BaseStream.Position - (startPos + 8);
				wtr.Seek((int) startPos,SeekOrigin.Begin);
				wtr.Write((long) length);
				wtr.Seek(0, SeekOrigin.End);
				
				Debug.WriteLine(toc.Count + " objects in TOC.");
				Debug.WriteLine(Count + " objects written.");
			}
			private void AddEmbeddedObjects(Object o)
			{
				if (toc[o.Name] == null) //What if there are embedded inventories? That needs fixed.
					toc.Add(o.Name, id++);
				foreach (Object obj in o.childObjects)
					AddEmbeddedObjects(obj);
			}
		}

		[Serializable]
		public class Object : IComparable, ICloneable
		{
            public int UniqueID;
			public string Name;
			public Property Properties;
			public short Type2;
			public int Extent;//TODO:enforce uniqueness?
			public PointF Location;
			public int Unknown;//always null?
			public byte Terminator;//usually 0x00, sometimes 0xFF (e.g., Flag objects)
			//TODO//public ArrayList Modifiers = new ArrayList();//modifiers this object has (elements are of type 'class Modifier')
			public byte[] modbuf = new byte[0];
			public ArrayList enchants = new ArrayList();
			public byte Team;//Specified in the extra stuff that comes with 0xFF Terminator
			public string Scr_Name = "";//Name used in Script Section
			//public byte inven; //Number of objects in inventory, important for object ordering
			public ArrayList childObjects = new ArrayList(); //Objects in its inventory
			public string pickup_func = ""; //Function to execute when picked-up
			public byte unknown1 = 0;//Temporary buffers for FF term. stuff, unknowns 
			public List<UInt32> unknown2 = new List<uint>();//FF term. unknown (Used by SaveGameLocation)
			public bool AutoEquip = false; //Found by RogueTeddyBear
            public UInt16 temp1 = 0x0100;//Temporary buffers for FF term. stuff, unknowns 
			public UInt16 temp2 = 0x0000;//Temporary buffers for FF term. stuff, unknowns 
			public UInt32 temp3 = 0x00010000;//Temporary buffers for FF term. stuff, unknowns
			public UInt64 temp4 = 0;//Temporary buffers for FF term. stuff, unknowns

            public enum Property : short
			{
				Normal = 0x003C,
				Interact = 0x003D,
				Pickup = 0x003E,
				Quest = 0x003F,
				Enemy = 0x0040
			}

			public Object()
			{
				//default values
				Name = "ExtentShortCylinderSmall";
				Extent = 0;
				Properties = Property.Normal;
				Type2 = 0x0040;//always??
				Location = new PointF(0, 0);
                UniqueID = 0;
			}

			public Object(string name, PointF loc) : this()
			{
				Name = name;
				Location = loc;
			}

			public Object(Stream stream, IDictionary toc)//read an object from the stream, using the provided toc to identify the object
			{
				Read(stream, toc);
			}

			public void Read(Stream stream, IDictionary toc)
			{
				BinaryReader rdr = new BinaryReader(stream);
				Name = (string) toc[rdr.ReadInt16()];
				rdr.BaseStream.Seek((8 - rdr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
				long endOfData = rdr.ReadInt64() + rdr.BaseStream.Position;
				Properties = (Property) rdr.ReadInt16();
				Type2 = rdr.ReadInt16();
				Extent = rdr.ReadInt32();
				Unknown = rdr.ReadInt32();//always null?
				Location = new PointF(rdr.ReadSingle(), rdr.ReadSingle());//x then y
				int inven = 0;

				if(Location.X > 5880 || Location.Y > 5880)
					Location = new PointF(5870,5870);
				Terminator = rdr.ReadByte();
				if(Terminator == 0xFF)
				{
					unknown1 = rdr.ReadByte();
					AutoEquip = rdr.ReadBoolean();
					temp1 = rdr.ReadUInt16();
					Scr_Name = rdr.ReadString();
					Team = rdr.ReadByte();
					inven = rdr.ReadByte();
					for (int i = rdr.ReadInt16(); i > 0; i--)
						unknown2.Add(rdr.ReadUInt32());
					temp2 = rdr.ReadUInt16();
					temp3 = rdr.ReadUInt32();
					if (Type2 == 0x40)
					{
						int strsize = rdr.ReadInt32();
						pickup_func = strsize > 0 ? new string(rdr.ReadChars(strsize)) : "";
					}
					temp4 = rdr.ReadUInt64();
					Debug.WriteLineIf(temp1 != 0x100, String.Format("Object: {0} temp1 was not 0x100, it was: {1:x}", this, temp1));
					Debug.WriteLineIf(temp2 != 0, String.Format("Object: {0} temp2 was not 0, it was: {1:x}", this, temp2));
					Debug.WriteLineIf(temp3 != 0x00010000, String.Format("Object: {0} temp3 was not 0x10000, it was: {1:x}", this, temp2));
                }
				if (rdr.BaseStream.Position < endOfData)
					modbuf = rdr.ReadBytes((int)(endOfData - rdr.BaseStream.Position));
				else if (rdr.BaseStream.Position > endOfData)
					Debug.Fail(String.Format("Object: {0} went beyond endOfData ({1})", this, endOfData));

				Debug.WriteLineIf(Terminator != 0 && Terminator != 0xFF, "Terminator was not 0 or ff, it was: " + Terminator);
				Debug.WriteLineIf(!Enum.IsDefined(typeof(Property), Properties), String.Format("object {0} has undefined properties 0x{1:x}.", Name, (short)Properties));
				Debug.WriteLineIf(Type2 != 0x40 && Type2 != 0x3F, String.Format("object {0} has type2 0x{1:x}.", Name, Type2));
				Debug.WriteLineIf(Unknown != 0, "Unknown in Object '" + Name + "' at " + Location + " was not null, it was: 0x" + Unknown.ToString("x"));

				//check that this entry's length matches
				Debug.Assert(rdr.BaseStream.Position == endOfData, String.Format("NoxMap (ObjectData) ERROR: bad entry length: diff = {0}", endOfData - rdr.BaseStream.Position));
				if(inven > 0)
				{
					childObjects = new ArrayList(inven);
					for(int i = inven; i > 0; i--)
						childObjects.Add(new Object(stream,toc));
				}
			}
			/// <summary>
			/// Writes the object to the stream
			/// </summary>
			/// <param name="stream">The stream to write to</param>
			/// <param name="toc">A Mapping of string to short IDs</param>
			public void Write(Stream stream, IDictionary toc)
			{
				if (pickup_func != null && pickup_func.Length > 0) { Type2 = 0x40; };
				BinaryWriter wtr = new BinaryWriter(stream);
				wtr.Write((short) toc[Name]);
				wtr.BaseStream.Seek((8 - wtr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
				int xtraLength = 0;
				if (Terminator == 0xFF)
					xtraLength = 23 + Scr_Name.Length + pickup_func.Length + (Type2 - 0x3F)*4 + unknown2.Count*4;
				long dataLength = 0x15 + modbuf.Length + xtraLength;//0x15 is the minumum length of an entry
				wtr.Write((long) dataLength);
				//the 0x15 is the length of these entries combined...
				wtr.Write((short) Properties);
				wtr.Write((short) Type2);
				wtr.Write((int) Extent);
				wtr.Write((int) Unknown);
				wtr.Write((float) Location.X);
				wtr.Write((float) Location.Y);
				wtr.Write((byte) Terminator);
				//... these entries make 0x15 bytes
				if (Terminator == 0xFF)
				{
					wtr.Write(unknown1);
					wtr.Write(AutoEquip);
					wtr.Write(temp1);
					wtr.Write(Scr_Name);
					wtr.Write(Team);
					wtr.Write((byte)childObjects.Count);
					wtr.Write((short)unknown2.Count);
					foreach (UInt32 u in unknown2)
						wtr.Write(u);
					wtr.Write(temp2);
					wtr.Write(temp3);
					if (Type2 == 0x40)
					{
						wtr.Write(pickup_func.Length);
						wtr.Write(pickup_func.ToCharArray());
					}
                    wtr.Write(temp4);
                }
				if (modbuf != null)
					wtr.Write(modbuf);
				foreach(Object o in childObjects)
					o.Write(stream,toc);
			}

			public int CompareTo(object obj)
			{
				return (Name.CompareTo(((Object)obj).Name) == 0) ? Extent.CompareTo(((Object)obj).Extent) : Name.CompareTo(((Object)obj).Name);
			}

			public override string ToString()
			{
				return Name + " " + Extent.ToString();
			}

			public object Clone()
			{
				Object copy = (Object) MemberwiseClone();
				copy.AutoEquip = AutoEquip;
				copy.childObjects = new ArrayList(childObjects.Count);
				foreach (Object o in childObjects)
					copy.childObjects.Add(o.Clone());
				copy.Extent = Extent;
				copy.Location = Location;
				copy.modbuf = new byte[modbuf.Length];
				modbuf.CopyTo(copy.modbuf, 0);
				copy.Name = Name;
				copy.pickup_func = pickup_func;
				copy.Properties = Properties;
				copy.Scr_Name = Scr_Name;
				copy.Team = Team;
				copy.temp1 = temp1;
				copy.temp2 = temp2;
				copy.temp3 = temp3;
				copy.Terminator = Terminator;
				copy.Unknown = Unknown;
				copy.unknown1 = unknown1;
				return copy;
			}
		}
		
		public class ScriptObject
		{
            public List<String> SctStr = new List<String>();
            public List<ScriptFunction> Funcs = new List<ScriptFunction>();
            public byte[] rest; // CODE till DONE, hopefully	
            public ScriptObject()
            {
                ScriptFunction sf = new ScriptFunction("GLOBAL");
                sf.code = new Byte[] { 0x48, 0, 0, 0 };
                Funcs.Add(sf);
                sf = new ScriptFunction("GLOBAL");
                sf.vars.Add(1);
                sf.vars.Add(1);
                sf.vars.Add(1);
                sf.vars.Add(1);
                sf.code = new Byte[]{0x48,0,0,0};
                Funcs.Add(sf);
                sf = new ScriptFunction("MapInitialize");
                sf.code = new Byte[] { 0x48, 0, 0, 0 };
                Funcs.Add(sf);
            }
        }
        public class ScriptFunction : IComparable //bug in beta 2 prevents IndexOf from using generic IComparable
        {
            public string name;
            public byte[] code;
            public List<int> vars = new List<int>();

            public ScriptFunction()
            {
            }
            public ScriptFunction(string s)
            {
                name = s;
            }

            public override bool Equals(System.Object other)
            {
				if (other.GetType() == typeof(ScriptFunction))
					return (name.Equals(((ScriptFunction)other).name));
				else
					return false;
            }
            public int CompareTo(System.Object other)
            {
				if (other.GetType() == typeof(ScriptFunction))
					return name.CompareTo(((ScriptFunction)other).name);
				else
					return -1;
            }

            public override int GetHashCode()
            {
                return name.GetHashCode();
            }

			public override string ToString()
			{
				return String.Format("{0}", name);
			}
        }
		public class Modifier
		{
			//TODO: is object to modifier a 1 to n relationship? or is it more like constructor info instead of modifiers?
		}
		#endregion

		#region Reading Methods
        public void ReadFile(NoxBinaryReader rdr)
        {
            Debug.WriteLine("Reading " + FileName + ".", "MapRead");
            Debug.Indent();
            
            //check to see if the file is not encrypted
            if (rdr.ReadUInt32() != 0xFADEFACE)//all unencrypted maps start with this
            {
                rdr = new NoxBinaryReader(File.Open(FileName, FileMode.Open), CryptApi.NoxCryptFormat.NONE);
                Encrypted = false;
            }
            rdr.BaseStream.Seek(0, SeekOrigin.Begin);//reset to start

            Header = new MapHeader(rdr.BaseStream);

            while (rdr.BaseStream.Position < rdr.BaseStream.Length)
            {
                //I don't know if the map format allows sections out of order, but this app supports it...
                string section = rdr.ReadString();
                rdr.SkipToNextBoundary();
                switch (section)
                {
                    case "MapInfo":
                        Info = new MapInfo(rdr.BaseStream);
                        break;
                    case "WallMap":
                        Walls = new WallMap(rdr.BaseStream);
                        break;
                    case "FloorMap":
                        Tiles = new FloorMap(rdr.BaseStream);
                        break;
                    case "SecretWalls":
                        ReadSecretWalls(rdr);
                        break;
                    case "DestructableWalls":
                        ReadDestructableWalls(rdr);
                        break;
                    case "WayPoints":
                        Waypoints = new WaypointList(rdr.BaseStream);
                        break;
                    case "DebugData":
                        DebugD = new DebugData(rdr.BaseStream);
                        break;
                    case "WindowWalls":
                        ReadWindowWalls(rdr);
                        break;
                    case "GroupData":
                        Groups = new GroupData(rdr.BaseStream);
                        break;
                    case "ScriptObject":
                        ReadScriptObject(rdr);
                        break;
                    case "AmbientData":
                        Ambient = new AmbientData(rdr.BaseStream);
                        break;
                    case "Polygons":
                        Polygons = new PolygonList(rdr.BaseStream);
                        break;
                    case "MapIntro":
                        Intro = new MapIntro(rdr.BaseStream);
                        break;
                    case "ScriptData":
                        ScriptD = new ScriptData(rdr.BaseStream);
                        break;
                    case "ObjectTOC":
                        ReadObjectToc(rdr);
                        break;
                    case "ObjectData":
                        ReadObjectData(rdr);
                        break;
                    default:
                        Debug.WriteLineIf(section.Length > 0, "WARNING: Unhandled section: " + section + ".");
                        break;
                }
            }

            rdr.Close();
            Debug.Unindent();
            Debug.WriteLine("Read successful.", "MapRead");
        }
		public void ReadFile()
		{
			Debug.WriteLine("Reading " + FileName + ".", "MapRead");
			Debug.Indent();
			NoxBinaryReader rdr	= new NoxBinaryReader(File.Open(FileName, FileMode.Open), CryptApi.NoxCryptFormat.MAP);

			//check to see if the file is not encrypted
			if (rdr.ReadUInt32() != 0xFADEFACE)//all unencrypted maps start with this
			{
				rdr = new NoxBinaryReader(File.Open(FileName, FileMode.Open), CryptApi.NoxCryptFormat.NONE);
				Encrypted = false;
			}
			rdr.BaseStream.Seek(0, SeekOrigin.Begin);//reset to start

			Header = new MapHeader(rdr.BaseStream);
			
			while (rdr.BaseStream.Position < rdr.BaseStream.Length)
			{
				//I don't know if the map format allows sections out of order, but this app supports it...
				string section = rdr.ReadString();
				rdr.SkipToNextBoundary();
				switch (section)
				{
					case "MapInfo":
						Info = new MapInfo(rdr.BaseStream);
						break;
					case "WallMap":
						Walls = new WallMap(rdr.BaseStream);
						break;
					case "FloorMap":
						Tiles = new FloorMap(rdr.BaseStream);
						break;
					case "SecretWalls":
						ReadSecretWalls(rdr);
						break;
					case "DestructableWalls":
						ReadDestructableWalls(rdr);
						break;
					case "WayPoints":
                        Waypoints = new WaypointList(rdr.BaseStream);
                        break;
					case "DebugData":
						DebugD = new DebugData(rdr.BaseStream);
						break;
					case "WindowWalls":
						ReadWindowWalls(rdr);
						break;
					case "GroupData":
						Groups = new GroupData(rdr.BaseStream);
						break;
					case "ScriptObject":
						ReadScriptObject(rdr);
						break;
					case "AmbientData":
						Ambient = new AmbientData(rdr.BaseStream);
						break;
					case "Polygons":
						Polygons = new PolygonList(rdr.BaseStream);
						break;
					case "MapIntro":
						Intro = new MapIntro(rdr.BaseStream);
						break;
					case "ScriptData":
						ScriptD = new ScriptData(rdr.BaseStream);
						break;
					case "ObjectTOC":
						ReadObjectToc(rdr);
						break;
					case "ObjectData":
						ReadObjectData(rdr);
						break;
					default:
						Debug.WriteLineIf(section.Length > 0, "WARNING: Unhandled section: " + section + ".");
						break;
				}
			}

			rdr.Close();
			Debug.Unindent();
			Debug.WriteLine("Read successful.", "MapRead");
		}

		private Stream tocStream;
		private Stream dataStream;
		protected void ReadObjectToc(NoxBinaryReader rdr)
		{
			tocStream = new MemoryStream();
			ulong length = rdr.ReadUInt64();
			BinaryWriter wtr = new BinaryWriter(tocStream);
			wtr.Write((ulong) length);
			wtr.Write(rdr.ReadBytes((int) length));
			wtr.BaseStream.Seek(0, SeekOrigin.Begin);
			if (dataStream != null)
				Objects = new ObjectTable(tocStream, dataStream);
		}
		protected void ReadObjectData(NoxBinaryReader rdr)
		{
			dataStream = new MemoryStream();
			ulong length = rdr.ReadUInt64();
			BinaryWriter wtr = new BinaryWriter(dataStream);
			wtr.Write((ulong) length);
			wtr.Write(rdr.ReadBytes((int) length));
			wtr.BaseStream.Seek(0, SeekOrigin.Begin);
			if (tocStream != null)
				Objects = new ObjectTable(tocStream, dataStream);
		}

		public class SectHeader
		{
			public string name;
			public byte[] header;
			public SectHeader(string n, byte[] h)
			{
				name = n; header = h;
				Debug.WriteLine("header '" + name + "' has bytes: " + ToString());
			}

			public override string ToString()
			{
				string s = "";
				foreach (byte b in header)
					s += String.Format("{0:x2} ", b);
				return s;
			}
		}

		//TODO: make WallMap class that extends SortedList to clean this wall reading/writing up
		protected void ReadDestructableWalls(NoxBinaryReader rdr)
		{
			long finish = rdr.ReadInt64() + rdr.BaseStream.Position;
			
			SectHeader hed = new SectHeader("DestructableWalls", rdr.ReadBytes(2));
			Headers.Add(hed.name,hed);

			int num = rdr.ReadInt16();
			while (rdr.BaseStream.Position < finish)
			{
				int x = rdr.ReadInt32();
				int y = rdr.ReadInt32();
				Wall wall = Walls[new Point(x,y)];
				wall.Destructable = true;
				num--;
			}
			Debug.Assert(num == 0, "NoxMap (DestructableWalls) ERROR: bad header");
			Debug.Assert(rdr.BaseStream.Position == finish, "NoxMap (DestructableWalls) ERROR: bad section length");
		}

		protected void ReadWindowWalls(NoxBinaryReader rdr)
		{
			long finish = rdr.ReadInt64() + rdr.BaseStream.Position;

			SectHeader hed = new SectHeader("WindowWalls", rdr.ReadBytes(2));
			Headers.Add(hed.name,hed);

			int num = rdr.ReadInt16();//the number of window walls
			
			while (rdr.BaseStream.Position < finish)
			{
				int x = rdr.ReadInt32();
				int y = rdr.ReadInt32();
				Wall wall = Walls[new Point(x,y)];
				wall.Window = true;
				num--;
			}
			Debug.Assert(num == 0, "NoxMap (WindowWalls) ERROR: bad header");
			Debug.Assert(rdr.BaseStream.Position == finish, "NoxMap (WindowWalls) ERROR: bad section length");
		}

		protected void ReadSecretWalls(NoxBinaryReader rdr)
		{
			long finish = rdr.ReadInt64() + rdr.BaseStream.Position;

			SectHeader hed = new SectHeader("SecretWalls", rdr.ReadBytes(2));
			Headers.Add(hed.name,hed);

			int num = rdr.ReadInt16();//the number of window walls
			
			while (rdr.BaseStream.Position < finish)
			{
				int x = rdr.ReadInt32();
				int y = rdr.ReadInt32();
				Wall wall = Walls[new Point(x,y)];
				wall.Secret = true;
				wall.Secret_u1 = rdr.ReadInt32();
				wall.Secret_flags = rdr.ReadUInt32();
				byte[] nulls = rdr.ReadBytes(7);//7 nulls
				num--;
				foreach (byte b in nulls)
					if (b != 0)
						Debug.WriteLine("nulls in SecretWall are not null!!");
				Debug.WriteLineIf(wall.Secret_u1 != 0x3, String.Format("Wall at {0} Secret_u1 != 0x00000003: 0x{1:x}.", wall.Location, wall.Secret_u1));
				Debug.WriteLineIf((wall.Secret_flags & (0xFFFFFEF9)) != 0, String.Format("Wall at {0} Secret_flags != 0x00000106: 0x{1:x}.", wall.Location, wall.Secret_flags));
			}

			Debug.Assert(num == 0, "NoxMap (SecretWalls) ERROR: bad wall count");
			Debug.Assert(rdr.BaseStream.Position == finish, "NoxMap (SecretWalls) ERROR: bad section length");
		}

		protected void ReadScriptObject(NoxBinaryReader rdr)
		{
			long finish = rdr.ReadInt64() + rdr.BaseStream.Position;
			SectHeader hed = new SectHeader("ScriptObject", rdr.ReadBytes(2));//always 0x0001?
			Headers.Add(hed.name,hed);
			short unknown = BitConverter.ToInt16(hed.header, 0);
			Debug.WriteLineIf(unknown != 0x0001, "header for ScriptObject was not 0x0001, it was 0x" + unknown.ToString("x") + ".");
			
			Scripts = new ScriptObject();
            Scripts.SctStr = new List<String>();

            int Sectlen = rdr.ReadInt32();
			while(rdr.BaseStream.Position < finish)
			{
                if (rdr.BaseStream.Position < finish - 12 && new string(rdr.ReadChars(12)) == "SCRIPT03STRG")
                {
                    int numStr = rdr.ReadInt32();
                    for (int i = 0; i < numStr; i++)
                        Scripts.SctStr.Add(new string(rdr.ReadChars(rdr.ReadInt32())));
                    if (rdr.BaseStream.Position < finish - 4 && new string(rdr.ReadChars(4)) == "CODE")
                    {
                        Scripts.Funcs = new List<ScriptFunction>(rdr.ReadInt32());
                        while (new string(rdr.ReadChars(4)) == "FUNC")
                        {
                            ScriptFunction func = new ScriptFunction();
                            Scripts.Funcs.Add(func);
                            func.name = new string(rdr.ReadChars(rdr.ReadInt32()));
                            rdr.ReadInt64();
                            rdr.ReadInt32(); // SYMB
                            for(Int64 i = rdr.ReadInt64(); i > 0 ; i--)
                                func.vars.Add(rdr.ReadInt32());
                            rdr.ReadInt32(); // DATA
                            func.code = rdr.ReadBytes(rdr.ReadInt32());
                        }
                    }
                }
                else
                    rdr.BaseStream.Seek(-12, SeekOrigin.Current);
                Scripts.rest = rdr.ReadBytes((int)(finish - rdr.BaseStream.Position));
			}		

		}
		#endregion

		#region Writing Methods
		protected byte[] mapData;
		protected void WriteMapData()
		{
			Debug.WriteLine("Writing " + FileName + ".", "MapWrite");
			Debug.Indent();
			NoxBinaryWriter wtr = new NoxBinaryWriter(new MemoryStream(), CryptApi.NoxCryptFormat.NONE);//encrypt later

			Header.Write(wtr.BaseStream);
			Info.Write(wtr.BaseStream);
			Walls.Write(wtr.BaseStream);
			Tiles.Write(wtr.BaseStream);
			WriteSecretWalls(wtr);
			WriteDestructableWalls(wtr);
            Waypoints.Write(wtr.BaseStream);
			DebugD.Write(wtr.BaseStream);
			WriteWindowWalls(wtr);
			Groups.Write(wtr.BaseStream);
			WriteScriptObject(wtr);
			Ambient.Write(wtr.BaseStream);
			Polygons.Write(wtr.BaseStream);
			Intro.Write(wtr.BaseStream);
			ScriptD.Write(wtr.BaseStream);
			Objects.Write(wtr.BaseStream);

			//write null bytes to next boundary -- this is needed only because
			// no more data is going to be written,
			// so the null bytes are not written implicitly by 'Seek()'ing
			wtr.Write(new byte[(8 - wtr.BaseStream.Position % 8) % 8]);
			
			//go back and write header again, with a proper checksum
			Header.GenerateChecksum(((MemoryStream) wtr.BaseStream).ToArray());
			wtr.BaseStream.Seek(0, SeekOrigin.Begin);
			Header.Write(wtr.BaseStream);
			wtr.BaseStream.Seek(0, SeekOrigin.End);
			wtr.Close();

			mapData = ((MemoryStream) wtr.BaseStream).ToArray();
			if (Encrypted)
				mapData = CryptApi.NoxEncrypt(mapData, CryptApi.NoxCryptFormat.MAP);

			Debug.Unindent();
			Debug.WriteLine("Write successful.", "MapWrite");
		}

		public void WriteMap()
		{
			WriteMapData();
			BinaryWriter fileWtr = new BinaryWriter(File.Open(FileName, FileMode.Create));
			fileWtr.Write(mapData);
			fileWtr.Close();
		}

		public void WriteNxz()
		{
			//WriteMapData(); Recreating MapData causes checksums to be different and objects to be in different order
			//do a stupid replace of ".map" -- better be named correctly!!!
			BinaryWriter fileWtr = new BinaryWriter(File.Open(FileName.Replace(".map", ".nxz"), FileMode.Create));
			fileWtr.Write((uint) mapData.Length);
			fileWtr.Write(CryptApi.NxzEncrypt(mapData));
			fileWtr.Close();
		}

		private void WriteWindowWalls(NoxBinaryWriter wtr)
		{
			string str = "WindowWalls";
			SectHeader hed = (SectHeader) Headers[str];
			wtr.Write(str + "\0");
			long length = 0;
			long pos;
			wtr.SkipToNextBoundary();
			pos = wtr.BaseStream.Position;
			wtr.Write(length);
			wtr.Write(hed.header);
			wtr.Write((short) 0);//placeholder for count

			//TODO: give these a consistent ordering before writing. the maps do have an ordering...
			//   seems to be based on x, y. figure it out and then enforce it here.
			short count = 0;
			foreach (Wall wall in Walls.Values)
				if(wall.Window)
				{
					wtr.Write((uint) wall.Location.X);
					wtr.Write((uint) wall.Location.Y);
					count++;
				}
		
			//rewrite the length
			length = wtr.BaseStream.Position - (pos + 8);
			wtr.Seek((int) pos, SeekOrigin.Begin);
			wtr.Write(length);
			wtr.Seek((int) hed.header.Length, SeekOrigin.Current);
			//rewrite the windowwall count
			wtr.Write((short) count);
			wtr.Seek(0, SeekOrigin.End);
		}

		private void WriteDestructableWalls(NoxBinaryWriter wtr)
		{
			string str = "DestructableWalls";
			SectHeader hed = (SectHeader) Headers[str];
			wtr.Write(str + "\0");
			long length = 0;
			long pos;
			wtr.SkipToNextBoundary();
			pos = wtr.BaseStream.Position;
			wtr.Write(length);
			wtr.Write(hed.header);
			wtr.Write((Int16)0);

			Int16 count = 0;
			foreach (Wall wall in Walls.Values)
				if(wall.Destructable)
				{
					wtr.Write((uint) wall.Location.X);
					wtr.Write((uint) wall.Location.Y);
					count++;
				}

			//rewrite the length
			length = wtr.BaseStream.Position - (pos+8);
			wtr.Seek((int)pos,SeekOrigin.Begin);
			wtr.Write(length);
			wtr.Seek((int)hed.header.Length,SeekOrigin.Current);
			wtr.Write((Int16)count);
			wtr.Seek(0,SeekOrigin.End);
		}

		private void WriteSecretWalls(NoxBinaryWriter wtr)
		{
			string str = "SecretWalls";
			SectHeader hed = (SectHeader) Headers[str];
			wtr.Write(str + "\0");
			long length = 0;
			long pos;
			wtr.SkipToNextBoundary();
			pos = wtr.BaseStream.Position;
			wtr.Write(length);
			wtr.Write(hed.header);
			wtr.Write((Int16)0);

			Int16 count = 0;
			foreach (Wall wall in Walls.Values)
				if(wall.Secret)
				{
					wtr.Write((uint) wall.Location.X);
					wtr.Write((uint) wall.Location.Y);
					wtr.Write((uint) wall.Secret_u1);
					wtr.Write((uint) wall.Secret_flags);
					wtr.Write(new byte[7]);//7 nulls
					count++;
				}

			//rewrite the length
			length = wtr.BaseStream.Position - (pos+8);
			wtr.Seek((int)pos,SeekOrigin.Begin);
			wtr.Write(length);
			wtr.Seek((int)hed.header.Length,SeekOrigin.Current);
			wtr.Write((Int16)count);
			wtr.Seek(0,SeekOrigin.End);
		}

		private void WriteScriptObject(NoxBinaryWriter wtr)
		{
			string str = "ScriptObject";
			SectHeader hed = (SectHeader) Headers[str];
			wtr.Write(str+"\0");
			long length = 0;
			long pos;
			wtr.SkipToNextBoundary();
			// placeholder for whole section length
			pos = wtr.BaseStream.Position;
			wtr.Write(length);
			wtr.Write(hed.header);
			// placeholder for subsection length
			int sectlen = 0;
			long secpos;
			secpos = wtr.BaseStream.Position;
			wtr.Write(sectlen);
            if (Scripts.SctStr.Count != 0 || Scripts.Funcs.Count != 0)
            {
                // if there is a strings section
                //Eric fixed a bug here: "SCRIPTTO3STRG" should(? - see Bunker) be written even if count is 0
                wtr.Write("SCRIPT03STRG".ToCharArray()); // tokens used to distiguish sections of the section
                wtr.Write(Scripts.SctStr.Count); // write number of strings
                foreach (String s in Scripts.SctStr) // write each string
                {
                    wtr.Write(s.Length);
                    wtr.Write(s.ToCharArray());
                }
                wtr.Write("CODE".ToCharArray());
                wtr.Write(Scripts.Funcs.Count);
                foreach (ScriptFunction sf in Scripts.Funcs)
                {
                    wtr.Write("FUNC".ToCharArray());
                    wtr.Write(sf.name.Length);
                    wtr.Write(sf.name.ToCharArray());
                    wtr.Write((Int64)0);
                    wtr.Write("SYMB".ToCharArray());
                    wtr.Write((Int64)sf.vars.Count);
                    foreach(int var in sf.vars)
                        wtr.Write(var);
                    wtr.Write("DATA".ToCharArray());
                    wtr.Write(sf.code.Length);
                    wtr.Write(sf.code);
                }
                wtr.Write("DONE".ToCharArray());
            }
            /* Shouldn't be anything left over
			if(Scripts.rest != null)
				wtr.Write(Scripts.rest); // write rest of the section*/
			// rewrite section length
			sectlen = (int)(wtr.BaseStream.Position - (secpos + 4));
			wtr.Seek((int)secpos,SeekOrigin.Begin);
			wtr.Write(sectlen);
			wtr.Seek(0, SeekOrigin.End);

			//rewrite the length
			length = wtr.BaseStream.Position - (pos + 8);
			wtr.Seek((int) pos, SeekOrigin.Begin);
			wtr.Write(length);
			wtr.Seek(0, SeekOrigin.End);
		}
		#endregion
	}
}
