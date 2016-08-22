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

        public static bool SaveProtected = false;
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
        public int progress = new int();
        public static uint[] tilecolors = new uint[145] {
          0xFF5E250C,
          0xFFC0C0C0,
          0xFF664431,
          0xFF69574D,
          0xFFA27861,
          0xFF59481E,
          0xFFEE0012,
          0xFF2A591E,
          0xFF356F26,
          0xFF8AD0CE,
          0xFF85806E,
          0xFF403D33,
          0xFFC77E56,
          0xFF45596B,
          0xFF115E7A,
          0xFF476069,
          0xFF555274,
          0xFF878185,
          0xFFC8911C,
          0xFF7A2F0A,
          0xFF5B86AA,
          0xFF898879,
          0xFFEDDCA9,
          0xFF8AA7B5,
          0xFFEDDCA9,
          0xFF141414,
          0xFF4A5F69,
          0xFF542209,
          0xFF426FE0,
          0xFF109432,
          0xFFAE1212,
          0xFF7C9775,
          0xFFA5937C,
          0xFFBABAB9,
          0xFFDA8146,
          0xFFB18669,
          0xFFEC6C17,
          0xFF7B91A5,
          0xFFA37985,
          0xFF9B4D27,
          0xFF6B6B6B,
          0xFF6B5840,
          0xFF8A3405,
          0xFFA76A1C,
          0xFFA7491C,
          0xFFC8BBB4,
          0xFFD8A96C,
          0xFF9F6033,
          0xFF8E8B79,
          0xFF4750FF,
          0xFFD35900,
          0xFF645C44,
          0xFFD9DA8A,
          0xFFC6940D,
          0xFF743506,
          0xFF9F8D7F,
          0xFF708145,
          0xFF626142,
          0xFF6F9843,
          0xFF8D8359,
          0xFF8D8359,
          0xFFB7AD85,
          0xFF615136,
          0xFF774628,
          0xFF5B8576,
          0xFFD0C36D,
          0xFF816543,
          0xFF796752,
          0xFF9D8D7B,
          0xFF817669,
          0xFFC1B2A1,
          0xFF899E56,
          0xFF8D8869,
          0xFFB9C67A,
          0xFFADA677,
          0xFF735B3C,
          0xFF452700,
          0xFFC3F0F2,
          0xFFC5A949,
          0xFF9F8B43,
          0xFFC6B062,
          0xFFA49563,
          0xFFA6D5DE,
          0xFF93BBC2,
          0xFFD9F5FA,
          0xFF846825,
          0xFF755C20,
          0xFF8B7D5A,
          0xFF756B52,
          0xFFA58E56,
          0xFF8E7E59,
          0xFF59788E,
          0xFF164668,
          0xFFE1DA9F,
          0xFFC5BC6E,
          0xFF9C927C,
          0xFFB8B09E,
          0xFF677987,
          0xFFA5A793,
          0xFF8CA685,
          0xFFA6B2A3,
          0xFF888274,
          0xFF808874,
          0xFF7A8766,
          0xFF999285,
          0xFF838A73,
          0xFFA59D8E,
          0xFF6D5F53,
          0xFF465466,
          0xFFFFFFFF,
          0xFFCF9139,
          0xFFCD7E29,
          0xFF637446,
          0xFF053E65,
          0xFF714B28,
          0xFF4F3721,
          0xFF67A899,
          0xFF50520D,
          0xFF3B7366,
          0xFF1A6265,
          0xFF536470,
          0xFF554959,
          0xFF68596E,
          0xFF606060,
          0xFF918486,
          0xFF938C8D,
          0xFF564C41,
          0xFF43597E,
          0xFFB2A175,
          0xFF968863,
          0xFF695B42,
          0xFF504727,
          0xFF516672,
          0xFF523D27,
          0xFF7B6248,
          0xFF215549,
          0xFF6B978D,
          0xFF53A794,
          0xFF3B7366,
          0xFF1A6265,
          0xFF928777,
          0xFFB4A690,
          0xFF7A746B,
          0xFFBDB4A6,
          0xFF868076
        };


        // ----PROTECTED MEMBERS----
        protected MapHeader Header = new MapHeader();
        public string FileName = "";
        protected Hashtable Headers = new Hashtable();//contains the headers for each section or the complete section

        // ----CONSTRUCTORS----
        public Map() { }

        public Map(NoxBinaryReader rdr)
            : this()
        {
            Load(rdr);
        }

        public void Load(NoxBinaryReader rdr)
        {
            ReadFile(rdr);
        }

        #region Inner Classes and Enumerations

        public abstract class Section
        {
            protected virtual string SectionName { get { return GetType().Name; } }//return derived class name if not overidden
            protected long finish;

            public Section() { }
            public Section(Stream stream) { Read(stream); }

            protected void Read(Stream stream)
            {
                BinaryReader rdr = new BinaryReader(stream);
                finish = rdr.ReadInt64() + rdr.BaseStream.Position;

                ReadContents(rdr.BaseStream);

                long compensate = (finish - rdr.BaseStream.Position);
                stream.Seek(compensate, SeekOrigin.Current);
                if (compensate != 0)
                    System.Windows.Forms.MessageBox.Show("Forced to activate compensation mode! Skipping " + compensate.ToString() + " bytes...", "WARNING: " + SectionName + " looks corrupted");
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
                contents.Clear();
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

            public WallMap() : base(new LocationComparer()) { }
            public WallMap(Stream stream) : base(new LocationComparer(), stream) { }

            protected override void ReadContents(Stream stream)
            {
                BinaryReader rdr = new BinaryReader(stream);

                Prefix = rdr.ReadInt16();
                Var1 = rdr.ReadInt32();
                Var2 = rdr.ReadInt32();
                Var3 = rdr.ReadInt32();
                Var4 = rdr.ReadInt32();

                byte x, y;
            repeat:
                while ((x = rdr.ReadByte()) != 0xFF && (y = rdr.ReadByte()) != 0xFF)//we'll get an 0xFF for x to signal end of section
                {
                    rdr.BaseStream.Seek(-2, SeekOrigin.Current);
                    Wall wall = new Wall(rdr.BaseStream);

                    Add(wall.Location, wall);
                }
                if (finish != rdr.BaseStream.Position)
                {
                    rdr.ReadInt16(); // skip two bytes.. ugly hack for an ugly bug
                    Logger.Log("Detected Fake-Terminator bug, ignoring two bytes.");
                    goto repeat;
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
            private int Var1;
            private int Var2;
            private int Var3;
            public Color AmbientColor
            {
                get
                {
                    return Color.FromArgb(Var1, Var2, Var3);
                }
                set
                {
                    Var1 = (int)value.R;
                    Var2 = (int)value.G;
                    Var3 = (int)value.B;
                }
            }

            public AmbientData() : base() { }
            public AmbientData(Stream stream) : base(stream) { }

            protected override void ReadContents(Stream stream)
            {
                BinaryReader rdr = new BinaryReader(stream);

                Prefix = rdr.ReadInt16();
                Var1 = rdr.ReadInt32();
                Var2 = rdr.ReadInt32();
                Var3 = rdr.ReadInt32();
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
            // This section contains script timer related stuff
            public short Prefix = 1;
            public byte Count = 0;
            public List<ScriptDataEntry> entries = new List<ScriptDataEntry>();
            private byte[] data;

            public ScriptData() : base() { }
            public ScriptData(Stream stream) : base(stream) { }

            protected override void ReadContents(Stream stream)
            {
                BinaryReader rdr = new BinaryReader(stream);

                Prefix = rdr.ReadInt16();
                Count = rdr.ReadByte();

                // Count must be zero normally, else this is a saved map
                data = rdr.ReadBytes((int)(finish - rdr.BaseStream.Position));
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
            public short sectionVersion = 1;//CHECKED
            public String Text;

            public MapIntro() : base() { }
            public MapIntro(Stream stream) : base(stream) { }

            protected override void ReadContents(Stream stream)
            {
                BinaryReader rdr = new BinaryReader(stream);
                sectionVersion = rdr.ReadInt16();
                Text = new String(rdr.ReadChars(rdr.ReadInt32()));
                if (Text.Length > 0)
                    Logger.Log(string.Format("MapIntro section text: {0}", Text));
            }

            protected override void WriteContents(Stream stream)
            {
                BinaryWriter wtr = new BinaryWriter(stream);

                wtr.Write((short)sectionVersion);
                if (SaveProtected)
                {
                    // editor should be unable to load map, but not the game
                    wtr.Write((int)-1);
                }
                else
                {
                    wtr.Write((int)Text.Length);
                    wtr.Write(Text.ToCharArray());
                }
            }
        }

        public class DebugData : Section
        {
            public short sectionVersion = 1;//CHECKED
            public int Unknown2;//probably a count //CHECKED
            protected byte[] data;

            public DebugData() : base() { }
            public DebugData(Stream stream) : base(stream) { }


            protected override void ReadContents(Stream stream)
            {
                BinaryReader rdr = new BinaryReader(stream);

                sectionVersion = rdr.ReadInt16();
                Unknown2 = rdr.ReadInt32();

                //TODO: skip the rest for now
                data = rdr.ReadBytes((int)(finish - rdr.BaseStream.Position));
            }

            protected override void WriteContents(Stream stream)
            {
                BinaryWriter wtr = new BinaryWriter(stream);

                wtr.Write((short)sectionVersion);
                wtr.Write((int)Unknown2);
                wtr.Write(data);
            }
        }

        public class Group : ArrayList
        {
            public GroupTypes type;
            public string name;
            public int id;
            public Group(string n, GroupTypes t, int i)
                : base()
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
                return String.Format("{0} {1}", name, Enum.GetName(typeof(GroupTypes), type));
            }
        }

        public class GroupData : SortedDictionarySection<String, Group>
        {
            public short Unknown = 3;//CHECKED
            protected byte[] data;

            public GroupData() : base(StringComparer.CurrentCulture) { }
            public GroupData(Stream stream) : base(StringComparer.CurrentCulture, stream) { }

            protected override void ReadContents(Stream stream)
            {
                BinaryReader rdr = new BinaryReader(stream);

                Unknown = rdr.ReadInt16();
                int count = rdr.ReadInt32();

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
                                Logger.Log("Unknown group type 0x" + grp.type.ToString("x"));
                                break;
                        }
                    }
                    if (!ContainsKey(grp.name)) Add(grp.name, grp);
                }
            }

            protected override void WriteContents(Stream stream)
            {
                BinaryWriter wtr = new BinaryWriter(stream);

                int index = Count;
                wtr.Write((short)Unknown);
                wtr.Write((int)Count);
                foreach (Group grp in Values)
                {
                    wtr.Write(grp.name);
                    wtr.Write((byte)grp.type);
                    wtr.Write(index--);
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
                            Logger.Log("Unknown type 0x" + grp.type.ToString("x"));
                    }
                }
            }
        }

        public class PolygonList : ArrayList
        {
            public short EntryVersion;
            public List<PointF> Points = new List<PointF>();

            public PolygonList() { }
            public PolygonList(Stream stream)
            {
                Read(stream);
            }

            protected void Read(Stream stream)
            {
                BinaryReader rdr = new BinaryReader(stream);
                long finish = rdr.ReadInt64() + rdr.BaseStream.Position;

                EntryVersion = rdr.ReadInt16();

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
                wtr.Write((long)0);//dummy length value
                long startPos = wtr.BaseStream.Position;

                EntryVersion = 4; // enforce using latest version if we want to save quest maps
                wtr.Write((short)EntryVersion);

                //rebuild point list
                Points.Clear();
                foreach (Polygon poly in this)
                    foreach (PointF pt in poly.Points)
                        if (!Points.Contains(pt))
                            Points.Add(pt);

                //TODO: sort this before writing?
                wtr.Write((int)Points.Count);
                foreach (PointF pt in Points)
                {
                    wtr.Write((int)(Points.IndexOf(pt) + 1));
                    wtr.Write((float)pt.X);
                    wtr.Write((float)pt.Y);
                }

                wtr.Write((int)this.Count);
                foreach (Polygon poly in this)
                    poly.Write(wtr.BaseStream, this);

                //rewrite the length now that we can find it
                long length = wtr.BaseStream.Position - startPos;
                wtr.Seek((int)startPos - 8, SeekOrigin.Begin);
                wtr.Write((long)length);
                wtr.Seek((int)length, SeekOrigin.Current);
            }
        }

        public class Polygon
        {
            public string Name;
            public string EnterFuncPlayer; // script
            public string EnterFuncMonster; // script
            public bool IsQuestSecret; // displays message upon entering
            public Color AmbientLightColor;//the area's ambient light color
            public byte MinimapGroup;//the visible wall group when in this area
            public List<PointF> Points = new List<PointF>();//the unindexed points that define the polygon

            public Polygon(string name, Color ambient, byte mmGroup, List<PointF> points, string enterfunc, string unknownfunc, bool secretarea)
            {
                Name = name;
                AmbientLightColor = ambient;
                MinimapGroup = mmGroup;
                Points = points;
                EnterFuncPlayer = enterfunc;
                EnterFuncMonster = unknownfunc;
                IsQuestSecret = secretarea;
            }

            internal Polygon(Stream stream, PolygonList list)
            {
                Read(stream, list);
            }

            public bool IsPointInside(PointF p)
            {
                PointF p1, p2;

                bool inside = false;

                if (Points.Count < 3)
                {
                    return inside;
                }

                var oldPoint = new PointF(
                    Points[Points.Count - 1].X, Points[Points.Count - 1].Y);

                for (int i = 0; i < Points.Count; i++)
                {
                    var newPoint = new PointF(Points[i].X, Points[i].Y);

                    if (newPoint.X > oldPoint.X)
                    {
                        p1 = oldPoint;
                        p2 = newPoint;
                    }
                    else
                    {
                        p1 = newPoint;
                        p2 = oldPoint;
                    }

                    if ((newPoint.X < p.X) == (p.X <= oldPoint.X)
                        && (p.Y - (long)p1.Y) * (p2.X - p1.X)
                        < (p2.Y - (long)p1.Y) * (p.X - p1.X))
                    {
                        inside = !inside;
                    }

                    oldPoint = newPoint;
                }

                return inside;
            }

            protected void Read(Stream stream, PolygonList list)
            {
                BinaryReader rdr = new BinaryReader(stream);

                Name = rdr.ReadString();
                AmbientLightColor = Color.FromArgb(rdr.ReadByte(), rdr.ReadByte(), rdr.ReadByte());
                MinimapGroup = rdr.ReadByte();

                // Points forming a polygon
                short ptCount = rdr.ReadInt16();
                while (ptCount-- > 0)
                    Points.Add(list.Points[rdr.ReadInt32() - 1]);

                if (list.EntryVersion >= 2)
                {
                    // Typical script handler entries
                    short s1 = rdr.ReadInt16();
                    if (s1 <= 1)
                    {
                        EnterFuncPlayer = Encoding.ASCII.GetString(rdr.ReadBytes(rdr.ReadInt32()));
                        rdr.ReadInt32();
                    }
                    short s2 = rdr.ReadInt16();
                    if (s2 <= 1)
                    {
                        EnterFuncMonster = Encoding.ASCII.GetString(rdr.ReadBytes(rdr.ReadInt32()));
                        rdr.ReadInt32();
                    }
                }
                if (list.EntryVersion >= 4)
                {
                    int polyFlags = rdr.ReadInt32();
                    // Validated.
                    if ((polyFlags & 1) == 1)
                        IsQuestSecret = true;
                }
            }

            internal void Write(Stream stream, PolygonList list)
            {
                BinaryWriter wtr = new BinaryWriter(stream);
                wtr.Write(Name);
                wtr.Write((byte)AmbientLightColor.R);
                wtr.Write((byte)AmbientLightColor.G);
                wtr.Write((byte)AmbientLightColor.B);
                wtr.Write((byte)MinimapGroup);
                wtr.Write((short)Points.Count);
                foreach (PointF pt in Points)
                    wtr.Write((int)(list.Points.IndexOf(pt) + 1));

                if (list.EntryVersion >= 2)
                {
                    wtr.Write((short)1);
                    wtr.Write(EnterFuncPlayer.Length);
                    wtr.Write(Encoding.ASCII.GetBytes(EnterFuncPlayer));
                    wtr.Write((int)0);
                    wtr.Write((short)1);
                    wtr.Write(EnterFuncMonster.Length);
                    wtr.Write(Encoding.ASCII.GetBytes(EnterFuncMonster));
                    wtr.Write((int)0);
                }
                if (list.EntryVersion >= 4)
                {
                    int flags = 0;
                    if (IsQuestSecret) flags |= 1;
                    wtr.Write(flags);
                }
            }

            public override string ToString()
            {
                return String.Format("{0} {1} {2}, {3}", Name, MinimapGroup, Points[0].X, Points[1].Y);
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
                    num_wp.Add(wp.Number, wp);
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

            public override void Remove(object obj)
            {
                foreach (Waypoint wp in this)
                {
                    ArrayList conn = (ArrayList)wp.connections.Clone();
                    foreach (Waypoint.WaypointConnection wc in conn)
                        if (wc.wp == obj)
                            wp.connections.Remove(wc);
                }
                num_wp.Remove(((Map.Waypoint)obj).Number);
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
                    wtr.Write(wp.Number);
                    wp.Write(wtr.BaseStream, this);
                    foreach (Waypoint.WaypointConnection wpc in wp.connections)
                    {
                        wtr.Write(wpc.wp.Number);
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
            public string ShortName
            {
                get
                {
                    string result = Name;
                    int ptIndex = result.LastIndexOf(':');
                    // убирае?название карт??двоеточи?
                    if (ptIndex >= 0) result = result.Substring(ptIndex + 1);

                    return result;
                }
            }
            public PointF Point;
            public ArrayList connections = new ArrayList();
            public int Flags; // 1=enabled
            public int Number;

            public Waypoint(string name, PointF point, int number)
            {
                Name = name;
                Point = point;
                Number = number;
            }

            public Waypoint(Stream stream, WaypointList list)
            {
                Read(stream, list);
            }

            protected void Read(Stream stream, WaypointList list)
            {
                BinaryReader rdr = new BinaryReader(stream);
                Number = rdr.ReadInt32(); //Waypoint number
                Point = new PointF(rdr.ReadSingle(), rdr.ReadSingle());
                Name = rdr.ReadString();
                Flags = rdr.ReadInt32();
                for (int i = rdr.ReadByte(); i > 0; i--)
                {
                    connections.Add(new WaypointConnection(rdr.ReadInt32(), rdr.ReadByte()));
                }
            }

            public void Write(Stream stream, WaypointList list)
            {
                BinaryWriter wtr = new BinaryWriter(stream);
                wtr.Write(Point.X);
                wtr.Write(Point.Y);
                wtr.Write(Name);
                wtr.Write(Flags);
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
                    wp_num = wayp.Number;
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
                // make sure there is no duplicate connection
                foreach (WaypointConnection wc in this.connections)
                {
                    if (wc.wp_num == wp.Number)
                    {
                        return;
                    }
                }
                connections.Add(new WaypointConnection(wp, tag));
            }
            public override string ToString()
            {
                return String.Format("{2}: {3} {0}, {1}", Point.X, Point.Y, Number, Name);
            }
        }



        private static Color GetColor(uint a)
        {
            byte[] tmp = BitConverter.GetBytes(a);
            return Color.FromArgb(unchecked((int)((uint)a)));
        }

        public class Tile
        {

            public Color col
            {
                get
                {
                    return GetColor(tilecolors[graphicId]);
                    //return ThingDb.FloorTiles[graphicId].color;
                }
            }
            public Point Location;
            public byte graphicId;
            public UInt16 Variation;
            public ArrayList EdgeTiles = new ArrayList();

            public string Graphic
            {
                get
                {
                    return ((ThingDb.Tile)ThingDb.FloorTiles[graphicId]).Name;
                }
            }

            public List<uint> Variations
            {
                get
                {
                    return ((ThingDb.Tile)ThingDb.FloorTiles[graphicId]).Variations;
                }
            }

            public Tile(Point loc, byte graphic, UInt16 variation, ArrayList edgetiles)
            {
                Location = loc;
                graphicId = graphic; Variation = variation;
                EdgeTiles = edgetiles;
            }

            public Tile(Point loc, byte graphic, UInt16 variation) : this(loc, graphic, variation, new ArrayList()) { }

            internal Tile(Stream stream)
            {
                Read(stream);
            }

            internal void Read(Stream stream)
            {
                BinaryReader rdr = new BinaryReader(stream);
                graphicId = rdr.ReadByte();
                Variation = rdr.ReadUInt16();
                rdr.ReadBytes(2);//these are always null for first tilePair of a blending group (?)
                for (int numEdgeTiles = rdr.ReadByte(); numEdgeTiles > 0; numEdgeTiles--)
                    EdgeTiles.Add(new EdgeTile(rdr.BaseStream));
            }

            internal void Write(Stream stream)
            {
                BinaryWriter wtr = new BinaryWriter(stream);

                wtr.Write((byte)graphicId);
                wtr.Write((UInt16)Variation);
                wtr.Write(new byte[2]);//3 nulls
                wtr.Write((byte)EdgeTiles.Count);
                foreach (EdgeTile edge in EdgeTiles)
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

                internal EdgeTile(Stream stream)
                {
                    Read(stream);
                }

                internal void Read(Stream stream)
                {
                    BinaryReader rdr = new BinaryReader(stream);

                    Graphic = rdr.ReadByte();
                    Variation = rdr.ReadUInt16();
                    Edge = rdr.ReadByte();
                    Dir = (Direction)rdr.ReadByte();

                    Debug.WriteLineIf(!Enum.IsDefined(typeof(Direction), (byte)Dir), String.Format("WARNING: edgetile direction {0} is undefined.", (byte)Dir), "MapRead");
                }

                internal void Write(Stream stream)
                {
                    BinaryWriter wtr = new BinaryWriter(stream);

                    wtr.Write((byte)Graphic);
                    wtr.Write((UInt16)Variation);
                    wtr.Write((byte)Edge);
                    wtr.Write((byte)Dir);
                }
            }
            public override string ToString()
            {
                return String.Format("{0}, {1} {2}", Location.X, Location.Y, Graphic);
            }
        }

        public class FloorMap : SortedDictionarySection<Point, Tile>
        {
            public short Prefix = 4;
            public int Var1 = 24;
            public int Var2 = 7;
            public int Var3 = 103;
            public int Var4 = 110;

            public FloorMap() : base(new LocationComparer()) { }
            public FloorMap(Stream stream) : base(new LocationComparer(), stream) { }

            protected override void ReadContents(Stream stream)
            {
                BinaryReader rdr = new BinaryReader(stream);

                List<TilePair> tilePairs = new List<TilePair>();

                Prefix = rdr.ReadInt16();
                Var1 = rdr.ReadInt32();
                Var2 = rdr.ReadInt32();
                Var3 = rdr.ReadInt32();
                Var4 = rdr.ReadInt32();

                if (Prefix <= 3)
                {
                    throw new NotImplementedException("Unsupported subtile entry detected");
                }

                //Debug.WriteLine(String.Format("contents of header: 0x{0:x} 0x{1:x} 0x{2:x} 0x{3:x} 0x{4:x}", Prefix, Var1, Var2, Var3, Var4));

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
                        tilePairs.Add(new TilePair((byte)((right.Location.X - 1) / 2), (byte)((right.Location.Y + 1) / 2), left, right));
                    }
                    else //assume that this tile is a single since the ordering would have forced the right tile to be handled first
                    {
                        left = tile1;
                        tilePairs.Add(new TilePair((byte)(left.Location.X / 2), (byte)(left.Location.Y / 2), left, right));
                    }
                    if (left != null) tiles.Remove(left.Location);
                    if (right != null) tiles.Remove(right.Location);
                }

                //... and write them
                foreach (TilePair tilePair in tilePairs)
                    tilePair.Write(wtr.BaseStream);

                wtr.Write((ushort)0xFFFF);//terminating x and y
            }
        }

        protected class MapHeader
        {
            private const int LENGTH = 0x18;
            private const uint FILE_ID = 0xFADEFACE;
            public int CheckSum;//checksum for rest of file. used in determining whether download is necessary.
            public int wallOffsetX;
            public int wallOffsetY;

            public MapHeader() { }
            public MapHeader(Stream stream)
            {
                Read(stream);
            }

            public void Read(Stream stream)
            {
                BinaryReader rdr = new BinaryReader(stream);
                int id = rdr.ReadInt32(); // 0xFADEFACE or 0xFADEBEEF
                Debug.Assert((uint)id == FILE_ID);

                int check;
                check = rdr.ReadInt32();
                Debug.WriteLineIf(check != 0, "int in header was not null: 0x" + check.ToString("x"), "MapHeader.Read");
                CheckSum = rdr.ReadInt32();
                check = rdr.ReadInt32();
                Debug.WriteLineIf(check != 0, "int in header was not null: 0x" + check.ToString("x"), "MapHeader.Read");
                wallOffsetX = rdr.ReadInt32();
                wallOffsetY = rdr.ReadInt32();
            }

            public void Write(Stream stream)
            {
                BinaryWriter wtr = new BinaryWriter(stream);

                wtr.Write(FILE_ID);
                wtr.Write((int)0);
                wtr.Write((int)CheckSum);
                wtr.Write((int)0);
                wtr.Write((int)wallOffsetX);
                wtr.Write((int)wallOffsetY);
            }

            public void GenerateChecksum(byte[] data)
            {
                CheckSum = Crc32.Calculate(data);
                Logger.Log(String.Format("CRC32: 0x{0:X}", CheckSum));
            }
        }

        public class MapInfo : Section
        {
            public short EntryVer = 2; // если 3 - игнорируем лими?игроко?
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

            // ?я дума?чт?эт?флаг?
            // 1 - solo, 2 - quest, 0x10 - kotr, 0x20 - capflag, 0x40 - flagball, 0x80000000 - chat
            public enum MapType : uint
            {
                SOLO = 0x00000001,
                QUEST = 0x00000002,
                SOLO_T = 0x00000003,
                ARENA = 0x00000034,
                CTF = 0x00000018,
                CTF_8 = 8, // conflict.map got this
                SOCIAL = 0x80000000,
                FLAGBALL = 0x00000040
            }

            public static SortedList MapTypeNames;

            static MapInfo()
            {
                MapTypeNames = new SortedList();

                MapTypeNames.Add(MapType.ARENA, "Arena");
                MapTypeNames.Add(MapType.CTF, "CTF (normal)");
                MapTypeNames.Add(MapType.CTF_8, "CTF (conflict.map)");
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
            const int TOTAL = PREFIX + TITLE + DESCRIPTION + VERSION + 2 * (AUTHOR + EMAIL) + EMPTY + COPYRIGHT + DATE + TYPE + MINMAX;

            protected override void ReadContents(Stream stream)
            {
                NoxBinaryReader rdr = new NoxBinaryReader(stream);

                EntryVer = rdr.ReadInt16();
                Summary = rdr.ReadString((int)TITLE);
                Description = rdr.ReadString((int)DESCRIPTION);
                Version = rdr.ReadString((int)VERSION);
                Author = rdr.ReadString((int)AUTHOR);
                Email = rdr.ReadString((int)EMAIL);
                Author2 = rdr.ReadString((int)AUTHOR);
                Email2 = rdr.ReadString((int)EMAIL);
                rdr.ReadBytes((int)EMPTY);
                Copyright = rdr.ReadString((int)COPYRIGHT);
                Date = rdr.ReadString((int)DATE);
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
            }

            protected override void WriteContents(Stream stream)
            {
                BinaryWriter wtr = new BinaryWriter(stream);

                if (Type == MapType.QUEST) wtr.Write((short)3);
                else wtr.Write((short)2);

                wtr.Write(Summary.ToCharArray());
                wtr.BaseStream.Seek((int)TITLE - Summary.Length, SeekOrigin.Current);

                wtr.Write(Description.ToCharArray());
                wtr.BaseStream.Seek((int)DESCRIPTION - Description.Length, SeekOrigin.Current);

                wtr.Write(Version.ToCharArray());
                wtr.BaseStream.Seek((int)VERSION - Version.Length, SeekOrigin.Current);

                wtr.Write(Author.ToCharArray());
                wtr.BaseStream.Seek((int)AUTHOR - Author.Length, SeekOrigin.Current);

                wtr.Write(Email.ToCharArray());
                wtr.BaseStream.Seek((int)EMAIL - Email.Length, SeekOrigin.Current);

                wtr.Write(Author2.ToCharArray());
                wtr.BaseStream.Seek((int)AUTHOR - Author2.Length, SeekOrigin.Current);

                wtr.Write(Email2.ToCharArray());
                wtr.BaseStream.Seek((int)EMAIL - Email2.Length, SeekOrigin.Current);

                wtr.BaseStream.Seek((int)EMPTY, SeekOrigin.Current);

                wtr.Write(Copyright.ToCharArray());
                wtr.BaseStream.Seek((int)COPYRIGHT - Copyright.Length, SeekOrigin.Current);

                wtr.Write(Date.ToCharArray());
                wtr.BaseStream.Seek((int)DATE - Date.Length, SeekOrigin.Current);

                wtr.Write((int)Type);

                if (Type == MapType.QUEST)
                {
                    wtr.Write((byte)QIntroTitle.Length);
                    wtr.Write(Encoding.ASCII.GetBytes(QIntroTitle));
                    wtr.Write((byte)QIntroGraphic.Length);
                    wtr.Write(Encoding.ASCII.GetBytes(QIntroGraphic));
                }
                else
                {
                    wtr.Write((byte)RecommendedMin);
                    wtr.Write((byte)RecommendedMax);
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

        public struct TilePair : IComparable
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
                Left = null;
                Right = null;
                Location = Point.Empty;
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
                        Logger.Log("invalid x,y for tilepair entry");
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
                byte x = (byte)(Location.X | 0x80), y = (byte)(Location.Y | 0x80);

                if (OneTileOnly)
                {
                    if (Left == null)
                        y &= 0x7F;
                    else
                        x &= 0x7F;
                }

                wtr.Write((byte)y);
                wtr.Write((byte)x);

                //write the right one first
                if (Right != null)
                    Right.Write(stream);
                if (Left != null)
                    Left.Write(stream);
            }

            public int CompareTo(object obj)
            {
                TilePair rhs = (TilePair)obj;
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
            public byte matId;
            public byte Variation;
            public byte Minimap = 0x64;
            protected byte wallModifiedMB;
            public bool Destructable;
            public bool Secret
            {
                get
                {
                    return Secret_ScanFlags != 0;
                }
            }
            public bool Window;

            public byte Variations
            {
                get
                {
                    return ThingDb.Walls[matId].Variations;
                }
            }

            /*** ДЛ?СЕКРЕТНЫ?СТЕН ***/
            public int Secret_OpenWaitSeconds = 3; // Скольк?_секунд_ стен?ждет прежде че?снов?поднять?/опустить?
            public byte Secret_ScanFlags = 0; // 4 - стен?сама опускает?, 8 - стен?сама поднимается
            public byte Secret_WallState = 0; // 4 - стен?опускает?, 3 - стен?готовить? опустить?, 2 - стен?поднимается, 1 - стен?поднята
            public byte Secret_OpenDelayFrames = 0; // Скольк?времен?(фреймо? стен?открывается/закрывается
            public int Secret_LastOpenTime = 0;
            public uint Secret_r2 = 0;

            [Flags]
            public enum SecretScanFlags : byte
            {
                Scripted = 1,
                AutoOpen = 2,
                AutoClose = 4,
                Unknown8 = 8 // помоем?ставит? игро?когд?игро??до?
            }

            public string Material
            {
                get
                {
                    return ((ThingDb.Wall)ThingDb.Walls[matId]).Name;
                }
            }

            internal Wall(Stream stream)
            {
                Read(stream);
            }

            public Wall(Point loc, WallFacing facing, byte mat)
            {
                Location = loc; Facing = facing; matId = mat;
            }

            public Wall(Point loc, WallFacing facing, byte mat, byte mmGroup, byte var)
            {
                Location = loc; Facing = facing; matId = mat; Minimap = mmGroup; Variation = var;
            }

            internal void Read(Stream stream)
            {
                BinaryReader rdr = new BinaryReader(stream);
                Location = new Point(rdr.ReadByte(), rdr.ReadByte());
                Facing = (WallFacing)(rdr.ReadByte() & 0x7F);//I'm almost certain the sign bit is just garbage and does not signify anything about the wall
                matId = rdr.ReadByte();
                Variation = rdr.ReadByte();
                Minimap = rdr.ReadByte();
                wallModifiedMB = rdr.ReadByte(); // may be 1 in saved maps
                //Debug.WriteLineIf(alwaysNull != 0, String.Format("Wall at {0} has non-null alwaysNull: {1}.", Location, alwaysNull), "Map.Wall.Read");
            }

            internal void Write(Stream stream)
            {
                BinaryWriter wtr = new BinaryWriter(stream);

                wtr.Write((byte)Location.X);
                wtr.Write((byte)Location.Y);
                wtr.Write((byte)Facing);
                wtr.Write((byte)matId);
                wtr.Write((byte)Variation);
                wtr.Write((byte)Minimap);
                wtr.Write((byte)wallModifiedMB);
            }

            public int CompareTo(object obj)
            {
                Wall rhs = (Wall)obj;
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

            /// <summary>
            /// Constructs the Object table using the given streams.
            /// </summary>
            /// <param name="toc">A stream containing the ObjectTOC, without header but with length.</param>
            /// <param name="data">A stream containing the ObjectData, without header but with length.</param>
            internal ObjectTable(Stream toc, Stream data)
            {
                Read(toc, data);
            }

            public ObjectTable()
            {
            }

            public override int Add(object obj)
            {
                return base.Add(obj);
            }

            internal void Read(Stream toc, Stream data)
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
                Object currentObj;
                while (rdr.BaseStream.Position < finish)
                {
                    if (rdr.ReadInt16() == 0)//the list is terminated by a null object Name
                        break;        //the loop should break on this condition only
                    else
                        rdr.BaseStream.Seek(-2, SeekOrigin.Current);//roll back the short we just read

                    currentObj = new Object(rdr.BaseStream, this.toc);
                    if (currentObj.Extent >= 0)
                        Add(currentObj);
                }

                Debug.WriteLineIf(tocUnknown != 1, "tocUnknown was not 1, it was: " + tocUnknown);
                Debug.WriteLineIf(dataUnknown != 1, "dataUnknown was not 1, it was: " + dataUnknown);
                Logger.Log(this.toc.Count + " objects in TOC.");
                Logger.Log(Count + " objects read.");

                //check that length of section matches
                Debug.Assert(rdr.BaseStream.Position == finish, "NoxMap (ObjectDATA) ERROR: bad section length");
            }

            /// <summary>
            /// Writes the ObjectTOC and ObjectData along prefixed by their section names to the given strea.
            /// </summary>
            /// <param name="stream">The stream to write to.</param>
            internal void Write(Stream stream)
            {
                BinaryWriter wtr = new BinaryWriter(stream);

                //before we start writing, we need to build a new toc
                Sort();
                toc = new SortedList();
                id = 1;
                foreach (Object obj in this)
                {
                    if (toc[obj.Name] == null)
                        toc.Add(obj.Name, id++);
                    foreach (Object o in obj.InventoryList)
                        AddEmbeddedObjects(o);
                }

                //--write the ObjectTOC--
                wtr.Write("ObjectTOC\0");
                long length = 0;
                wtr.BaseStream.Seek((8 - wtr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
                long startPos = wtr.BaseStream.Position;
                wtr.Write((long)length);//dummy value
                wtr.Write((short)tocUnknown);

                wtr.Write((short)toc.Count);

                //and write them
                foreach (string key in toc.Keys)
                {
                    wtr.Write((short)toc[key]);
                    wtr.Write(key);
                }

                //rewrite the length
                length = wtr.BaseStream.Position - (startPos + 8);
                wtr.Seek((int)startPos, SeekOrigin.Begin);
                wtr.Write(length);
                wtr.Seek(0, SeekOrigin.End);

                //--write the ObjectTable--
                wtr.Write("ObjectData\0");
                wtr.BaseStream.Seek((8 - wtr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary

                length = 0;
                MemoryStream ms = new MemoryStream();
                ms.Write(BitConverter.GetBytes(length), 0, 8);
                ms.Write(BitConverter.GetBytes(dataUnknown), 0, 2);

                // write objects
                foreach (Object obj in this)
                    obj.Write(ms, toc);

                ms.Write(BitConverter.GetBytes((short)0), 0, 2);
                // rewrite length
                ms.Seek(0, SeekOrigin.Begin);
                ms.Write(BitConverter.GetBytes(ms.Length - 8), 0, 8);
                // copy to main
                wtr.Write(ms.ToArray());
                ms.Close();

                Logger.Log(toc.Count + " objects in TOC.");
                Logger.Log(Count + " objects written.");
            }
            private void AddEmbeddedObjects(Object o)
            {
                if (toc[o.Name] == null) //What if there are embedded inventories? That needs fixed.
                    toc.Add(o.Name, id++);
                foreach (Object obj in o.InventoryList)
                    AddEmbeddedObjects(obj);
            }
        }

        [Serializable]
        public class Object : IComparable, ICloneable
        {
            public string Name;
            public short ReadRule1; // AngryKirC - XFer parsing rule
            public short ReadRule2; // AngryKirC - Object entry parsing rule
            // ReadRule1 and ReadRule2 in newest Westwood maps nearly always appear to be 0x40
            public int Extent;
            public PointF Location;
            public int IngameID; // Global ID used by Nox itself
            public byte Terminator;
            public byte Team; // Team ID (0 = unassigned, 1 = Red, 2 = Blue etc)
            public string Scr_Name = ""; //Name used in Script Section
            public string ScrNameShort
            {
                get
                {
                    string result = Scr_Name;
                    int ptIndex = result.LastIndexOf(':');
                    // убирае?название карт??двоеточи?
                    if (ptIndex >= 0) result = result.Substring(ptIndex + 1);

                    return result;
                }
            }
            public List<Map.Object> InventoryList = new List<Map.Object>(); //Objects in its inventory
            public string pickup_func = ""; //Function to execute when picked-up
            public List<UInt32> SlaveGIDList = new List<UInt32>(); // list of objects ref'd by GlobalID
            public uint CreateFlags = 0; // added to normal object flags. research by AngryKirC
            public uint AnimFlags = 0x00; // max 0xA1
            public uint DestroyFrame = 0xFFFFFFFB;
            protected ObjDataXfer.DefaultXfer ExtraData = new ObjDataXfer.DefaultXfer();

            /// <summary>
            /// Initializes new ExtraData container with default values
            /// </summary>
            public void NewDefaultExtraData()
            {
                ExtraData = ObjectXferProvider.Get(ThingDb.Things[Name].Xfer);
            }

            public T GetExtraData<T>() where T : ObjDataXfer.DefaultXfer
            {
                return (T)ExtraData;
            }

            public Object()
            {
                //default values
                Name = "ExtentShortCylinderSmall";
                Extent = -1;
                ReadRule1 = 0x3C; // DefaultXfer
                ReadRule2 = 0x40;
                CreateFlags = 0x1000000; // ENABLED
                Location = new PointF(0, 0);
            }

            public Object(string name, PointF loc)
                : this()
            {
                Name = name;
                Location = loc;
                NewDefaultExtraData();
            }

            internal Object(Stream stream, IDictionary toc)
            {
                Extent = -1;
                Read(stream, toc);
            }

            public bool HasFlag(ThingDb.Thing.FlagsFlags flag)
            {
                if ((ThingDb.Things[Name].Flags & flag) == flag) return true;
                if ((CreateFlags & (uint)flag) == (uint)flag) return true;
                return false;
            }

            /// <summary>
            /// Возвращает true если обьект можн?поворачивать (использует? ?редактор?
            /// </summary>
            public bool CanBeRotated()
            {
                ThingDb.Thing tt = ThingDb.Things[Name];
                if (tt.HasClassFlag(ThingDb.Thing.ClassFlags.DOOR)
                    || tt.HasClassFlag(ThingDb.Thing.ClassFlags.MONSTER)
                    || tt.Xfer == "SentryXfer"
                   ) return true;
                return false;
            }

            /// <summary>
            /// Logs warning message including this object's name and extent
            /// </summary>
            /// <param name="msg"></param>
            internal void LogObjectWarning(string msg)
            {
                Logger.Log(String.Format("Warning: ({0}) {1}", this, msg));
            }

            /// <summary>
            /// read an object from the stream, using the provided toc to identify the object
            /// </summary>
            internal void Read(Stream stream, IDictionary toc)
            {
                BinaryReader rdr = new BinaryReader(stream);
                Name = (string)toc[rdr.ReadInt16()];
                rdr.BaseStream.Seek((8 - rdr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
                long endOfData = rdr.ReadInt64() + rdr.BaseStream.Position;

                NewDefaultExtraData();
                ReadRule1 = rdr.ReadInt16();
                ReadRule2 = rdr.ReadInt16(); // entry structure sign

                if (ReadRule2 < 0x3D || ReadRule2 > 0x40)
                {
                    Logger.Log(string.Format("({0}: RR 0x{1:X}) Unsupported entry structure.\n Ignoring this object...", Name, ReadRule2));
                    rdr.BaseStream.Seek(endOfData, SeekOrigin.Begin);
                    return;
                }

                Extent = rdr.ReadInt32();
                IngameID = rdr.ReadInt32();
                Location = new PointF(rdr.ReadSingle(), rdr.ReadSingle());//x then y

                if (Location.X > 5880 || Location.Y > 5880)
                    Location = new PointF(5870, 5870);

                byte inven = 0;
                Terminator = rdr.ReadByte();
                if (Terminator != 0)
                {
                    CreateFlags = rdr.ReadUInt32();
                    Scr_Name = rdr.ReadString();
                    Team = rdr.ReadByte();
                    inven = rdr.ReadByte();
                    for (int i = rdr.ReadInt16(); i > 0; i--)
                        SlaveGIDList.Add(rdr.ReadUInt32());

                    AnimFlags = rdr.ReadUInt32();
                    if (ReadRule2 >= 0x3F)
                    {
                        if (rdr.ReadInt16() <= 1)
                        {
                            int len = rdr.ReadInt32();
                            byte[] temp = rdr.ReadBytes(len);
                            rdr.ReadUInt32();
                            pickup_func = Encoding.ASCII.GetString(temp);
                        }

                        if (ReadRule2 >= 0x40) DestroyFrame = rdr.ReadUInt32();
                    }
                }

                ThingDb.Thing tt = ThingDb.Things[Name];
                if (ReadRule1 > ExtraData.MaxVersion)
                {
                    // temporarily allowed for WeaponXfer and ElevatorXfer (see implementation)
                    // for other types this signals data corruption
                    LogObjectWarning(String.Format("{0} version {1} is greater than max supported {2}", tt.Xfer, ReadRule1, ExtraData.MaxVersion));
                }

                long pos = rdr.BaseStream.Position;
                if (pos <= endOfData)
                {
                    bool result = ExtraData.FromStream(rdr.BaseStream, ReadRule1, tt);
                    if (!result && tt.Xfer != null && tt.Xfer != "DefaultXfer")
                    {
                        // Unable to fully parse Xfer data
                        LogObjectWarning(String.Format("Failed to fully parse {0} data", tt.Xfer));
                    }
                }
                else
                {
                    // Corrupted header
                    LogObjectWarning("Corrupted header structure!");
                }

                pos = rdr.BaseStream.Position;
                if (pos != endOfData)
                {
                    // Corrupted header OR Xfer data. It would be better to ignore this object
                    LogObjectWarning(String.Format("Object entry out of bounds (diff {0})", pos - endOfData));
                }
                rdr.BaseStream.Seek(endOfData, SeekOrigin.Begin); // Ensure correct position

                // Read subitems (inventory)
                if (inven > 0)
                {
                    InventoryList.Clear();
                    for (int i = inven; i > 0; i--)
                        InventoryList.Add(new Object(stream, toc));
                }
            }
            /// <summary>
            /// Writes the object to the stream
            /// </summary>
            /// <param name="stream">The stream to write to</param>
            /// <param name="toc">A Mapping of string to short IDs</param>
            internal void Write(Stream stream, IDictionary toc)
            {
                if (pickup_func != null && pickup_func.Length > 0 && ReadRule2 < 0x3F) { ReadRule2 = 0x40; };
                BinaryWriter wtr = new BinaryWriter(stream);
                wtr.Write((short)toc[Name]);
                wtr.BaseStream.Seek((8 - wtr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
                long lengthRecordPos = wtr.BaseStream.Position;

                wtr.Write((long)0);
                ReadRule1 = ExtraData.MaxVersion;
                wtr.Write((short)ReadRule1);
                wtr.Write((short)ReadRule2);
                wtr.Write((int)Extent);
                wtr.Write((int)IngameID);
                wtr.Write((float)Location.X);
                wtr.Write((float)Location.Y);
                wtr.Write((byte)Terminator);

                if (Terminator != 0)
                {
                    wtr.Write(CreateFlags);
                    wtr.Write(Scr_Name);
                    wtr.Write(Team);
                    wtr.Write((byte)InventoryList.Count);
                    wtr.Write((short)SlaveGIDList.Count);
                    foreach (UInt32 u in SlaveGIDList)
                        wtr.Write(u);
                    // AngryKirC: использует? только ?сохранен??одиночно?игры
                    wtr.Write(AnimFlags);
                    if (ReadRule2 >= 0x3F)
                    {
                        wtr.Write((short)1);
                        wtr.Write(pickup_func.Length);
                        wtr.Write(pickup_func.ToCharArray());
                        wtr.Write((uint)0);

                        if (ReadRule2 >= 0x40) wtr.Write(DestroyFrame);
                    }
                }

                //Debug.WriteLine(String.Format("Writing ExtraData for object: {0}", this));
                ExtraData.WriteToStream(wtr.BaseStream, ReadRule1, ThingDb.Things[Name]);

                long currentPos = wtr.BaseStream.Position;
                long entryLength = currentPos - (lengthRecordPos + 8);
                wtr.BaseStream.Seek(lengthRecordPos, SeekOrigin.Begin);
                wtr.Write(entryLength);
                wtr.BaseStream.Seek(currentPos, SeekOrigin.Begin);

                if (Terminator != 0)
                {
                    foreach (Object o in InventoryList) o.Write(stream, toc);
                }
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
                Object copy = (Object)MemberwiseClone();
                // Clone inventory, not copy the reference
                copy.InventoryList = new List<Map.Object>();
                foreach (Map.Object o in InventoryList) copy.InventoryList.Add((Map.Object)o.Clone());
                copy.SlaveGIDList = new List<UInt32>();
                foreach (uint i in SlaveGIDList) copy.SlaveGIDList.Add(i);
                // Clone transferdata, not copy the reference
                copy.ExtraData = (NoxShared.ObjDataXfer.DefaultXfer)ExtraData.Clone();
                // fields are already copied by MemberwiseClone
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
                sf.code = new Byte[] { 0x48, 0, 0, 0 };
                Funcs.Add(sf);
                sf = new ScriptFunction("MapInitialize");
                sf.code = new Byte[] { 0x48, 0, 0, 0 };
                Funcs.Add(sf);
            }
        }
        public class ScriptFunction : IComparable //bug in beta 2 prevents IndexOf from using generic IComparable
        {
            public string name;
            public bool retval;
            public int args;
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
        #endregion

        #region Reading Methods
        protected void ReadFile(NoxBinaryReader rdr)
        {
            Logger.Log("Reading " + FileName + ".");

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
                if (section.Length > 0) Logger.Log("Reading section " + section);
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
                        ReadScriptObjectSection(rdr);
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

            //Caller method should take care of the stream.
            //rdr.Close();
            Logger.Log("Map reading finished.");
        }

        private Stream tocStream;
        private Stream dataStream;
        protected void ReadObjectToc(NoxBinaryReader rdr)
        {
            tocStream = new MemoryStream();
            ulong length = rdr.ReadUInt64();
            BinaryWriter wtr = new BinaryWriter(tocStream);
            wtr.Write((ulong)length);
            wtr.Write(rdr.ReadBytes((int)length));
            wtr.BaseStream.Seek(0, SeekOrigin.Begin);
            if (dataStream != null)
                Objects = new ObjectTable(tocStream, dataStream);
        }
        protected void ReadObjectData(NoxBinaryReader rdr)
        {
            dataStream = new MemoryStream();
            ulong length = rdr.ReadUInt64();
            BinaryWriter wtr = new BinaryWriter(dataStream);
            wtr.Write((ulong)length);
            wtr.Write(rdr.ReadBytes((int)length));
            wtr.BaseStream.Seek(0, SeekOrigin.Begin);
            if (tocStream != null)
                Objects = new ObjectTable(tocStream, dataStream);
        }
        public class OldSectHeader
        {
            public string name;
            public byte[] header;
            public OldSectHeader(string n, byte[] h)
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
        public class SectHeader
        {
            public string Name;
            public short Version;

            public SectHeader(string n, short version)
            {
                Name = n; Version = version;
            }

            public override string ToString()
            {
                string s = Version.ToString("x");
                return s;
            }
        }

        //TODO: make WallMap class that extends SortedList to clean this wall reading/writing up
        protected void ReadDestructableWalls(NoxBinaryReader rdr)
        {
            long finish = rdr.ReadInt64() + rdr.BaseStream.Position;

            SectHeader hed = new SectHeader("DestructableWalls", rdr.ReadInt16());
            Headers.Add(hed.Name, hed);

            int num = rdr.ReadInt16();
            while (rdr.BaseStream.Position < finish)
            {
                int x = rdr.ReadInt32();
                int y = rdr.ReadInt32();
                try
                {
                    Wall wall = Walls[new Point(x, y)];
                    wall.Destructable = true;
                }
                catch (Exception) { Logger.Log("DestructableWalls read fail @" + String.Format("X:{0}, Y:{1}", x, y)); }
                num--;
            }
            Debug.Assert(num == 0, "NoxMap (DestructableWalls) ERROR: bad header");
            Debug.Assert(rdr.BaseStream.Position == finish, "NoxMap (DestructableWalls) ERROR: bad section length");
        }

        protected void ReadWindowWalls(NoxBinaryReader rdr)
        {
            long finish = rdr.ReadInt64() + rdr.BaseStream.Position;

            SectHeader hed = new SectHeader("WindowWalls", rdr.ReadInt16());
            Headers.Add(hed.Name, hed);

            int num = rdr.ReadInt16();//the number of window walls

            while (rdr.BaseStream.Position < finish)
            {
                int x = rdr.ReadInt32();
                int y = rdr.ReadInt32();
                try
                {
                    Wall wall = Walls[new Point(x, y)];
                    wall.Window = true;
                }
                catch (Exception) { Debug.WriteLine("WindowWalls read fail @" + String.Format("X:{0}, Y:{1}", x, y)); }
                num--;
            }
            Debug.Assert(num == 0, "NoxMap (WindowWalls) ERROR: bad header");
            Debug.Assert(rdr.BaseStream.Position == finish, "NoxMap (WindowWalls) ERROR: bad section length");
        }

        protected void ReadSecretWalls(NoxBinaryReader rdr)
        {
            long finish = rdr.ReadInt64() + rdr.BaseStream.Position;

            SectHeader hed = new SectHeader("SecretWalls", rdr.ReadInt16());
            Headers.Add(hed.Name, hed);

            int num = rdr.ReadInt16();//the number of window walls

            while (rdr.BaseStream.Position < finish)
            {
                int x = rdr.ReadInt32();
                int y = rdr.ReadInt32();
                Wall wall = Walls[new Point(x, y)];
                // PE 0x4297C0
                wall.Secret_OpenWaitSeconds = rdr.ReadInt32();
                wall.Secret_ScanFlags = rdr.ReadByte();
                wall.Secret_WallState = rdr.ReadByte();
                wall.Secret_OpenDelayFrames = rdr.ReadByte();
                wall.Secret_LastOpenTime = rdr.ReadInt32();
                wall.Secret_r2 = rdr.ReadUInt32();
                // BugFix: Wall will not be treated as secret if scanflags == 0
                // This is wrong since it is in secretwalls section anyway
                if (wall.Secret_ScanFlags == 0) { wall.Secret_ScanFlags = 1; }
                num--;
            }

            Debug.Assert(num == 0, "NoxMap (SecretWalls) ERROR: bad wall count");
            Debug.Assert(rdr.BaseStream.Position == finish, "NoxMap (SecretWalls) ERROR: bad section length");
        }

        public void ReadScriptObject(NoxBinaryReader rdr)
        {
            Scripts = new ScriptObject();
            Scripts.SctStr = new List<String>();

            if (new string(rdr.ReadChars(12)) == "SCRIPT03STRG")
            {
                int numStr = rdr.ReadInt32();
                for (int i = 0; i < numStr; i++)
                    Scripts.SctStr.Add(new string(rdr.ReadChars(rdr.ReadInt32())));
                if (new string(rdr.ReadChars(4)) == "CODE")
                {
                    Scripts.Funcs = new List<ScriptFunction>(rdr.ReadInt32());
                    while (new string(rdr.ReadChars(4)) == "FUNC")
                    {
                        ScriptFunction func = new ScriptFunction();
                        Scripts.Funcs.Add(func);
                        func.name = new string(rdr.ReadChars(rdr.ReadInt32()));
                        func.retval = rdr.ReadInt32() == 1;
                        func.args = rdr.ReadInt32();
                        rdr.ReadInt32(); // SYMB
                        for (Int64 i = rdr.ReadInt64(); i > 0; i--)
                            func.vars.Add(rdr.ReadInt32());
                        rdr.ReadInt32(); // DATA
                        func.code = rdr.ReadBytes(rdr.ReadInt32());
                    }
                }
            }
            else
                rdr.BaseStream.Seek(-12, SeekOrigin.Current);
        }

        protected void ReadScriptObjectSection(NoxBinaryReader rdr)
        {
            long finish = rdr.ReadInt64() + rdr.BaseStream.Position;
            OldSectHeader hed = new OldSectHeader("ScriptObject", rdr.ReadBytes(2));//always 0x0001?
            Headers.Add(hed.name, hed);
            short unknown = BitConverter.ToInt16(hed.header, 0);
            Debug.WriteLineIf(unknown != 0x0001, "header for ScriptObject was not 0x0001, it was 0x" + unknown.ToString("x") + ".");

            int Sectlen = rdr.ReadInt32();

            ReadScriptObject(rdr);

            Scripts.rest = rdr.ReadBytes((int)(finish - rdr.BaseStream.Position));
        }
        #endregion

        #region Writing Methods
        protected byte[] mapData;
        protected void WriteMapData()
        {


            Logger.Log("Writing " + FileName + ".");
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
            Header.GenerateChecksum(((MemoryStream)wtr.BaseStream).ToArray());
            wtr.BaseStream.Seek(0, SeekOrigin.Begin);
            Header.Write(wtr.BaseStream);
            wtr.BaseStream.Seek(0, SeekOrigin.End);

            wtr.Close();

            mapData = ((MemoryStream)wtr.BaseStream).ToArray();
            if (true)
                mapData = CryptApi.NoxEncrypt(mapData, CryptApi.NoxCryptFormat.MAP);
            Debug.Unindent();
            Logger.Log("Map has been written successfully.");
        }

        public void WriteMap()
        {
            WriteMapData();

            //System.Windows.Forms.MessageBox.Show(FileName+ "  map.cs");
            try
            {
                if (File.Exists(FileName)) File.Delete(FileName);

                //System.Windows.Forms.MessageBox.Show(FileName);
                BinaryWriter fileWtr = new BinaryWriter(File.Create(FileName));
                fileWtr.Write(mapData);
                fileWtr.Close();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("No permission!"+" "+e);
                return;
            }

        }

        /// <summary>
        /// Generates compressed version of the map file used for network transmission
        /// </summary>
        public void WriteNxz()
        {
            string nxzName = Path.GetDirectoryName(FileName) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(FileName) + ".nxz";
            if (File.Exists(nxzName)) File.Delete(nxzName);
            BinaryWriter fileWtr = new BinaryWriter(File.Create(nxzName));
            fileWtr.Write((uint)mapData.Length);
            fileWtr.Write(NoxLzCompression.Compress(mapData));
            fileWtr.Close();
        }

        private void WriteWindowWalls(NoxBinaryWriter wtr)
        {
            string str = "WindowWalls";
            SectHeader hed = (SectHeader)Headers[str];
            wtr.Write(str + "\0");
            long length = 0;
            long pos;
            wtr.SkipToNextBoundary();
            pos = wtr.BaseStream.Position;
            wtr.Write(length);
            wtr.Write(hed.Version);
            wtr.Write((short)0);//placeholder for count

            //TODO: give these a consistent ordering before writing. the maps do have an ordering...
            //   seems to be based on x, y. figure it out and then enforce it here.
            short count = 0;
            foreach (Wall wall in Walls.Values)
                if (wall.Window)
                {
                    wtr.Write((uint)wall.Location.X);
                    wtr.Write((uint)wall.Location.Y);
                    count++;
                }

            //rewrite the length
            length = wtr.BaseStream.Position - (pos + 8);
            wtr.Seek((int)pos, SeekOrigin.Begin);
            wtr.Write(length);
            wtr.Seek((int)2, SeekOrigin.Current);
            //rewrite the windowwall count
            wtr.Write((short)count);
            wtr.Seek(0, SeekOrigin.End);
        }

        private void WriteDestructableWalls(NoxBinaryWriter wtr)
        {
            string str = "DestructableWalls";
            SectHeader hed = (SectHeader)Headers[str];
            wtr.Write(str + "\0");
            long length = 0;
            long pos;
            wtr.SkipToNextBoundary();
            pos = wtr.BaseStream.Position;
            wtr.Write(length);
            wtr.Write(hed.Version);
            wtr.Write((Int16)0);

            Int16 count = 0;
            foreach (Wall wall in Walls.Values)
                if (wall.Destructable)
                {
                    wtr.Write((uint)wall.Location.X);
                    wtr.Write((uint)wall.Location.Y);
                    count++;
                }

            //rewrite the length
            length = wtr.BaseStream.Position - (pos + 8);
            wtr.Seek((int)pos, SeekOrigin.Begin);
            wtr.Write(length);
            wtr.Seek((int)2, SeekOrigin.Current);
            wtr.Write((Int16)count);
            wtr.Seek(0, SeekOrigin.End);
        }

        private void WriteSecretWalls(NoxBinaryWriter wtr)
        {
            string str = "SecretWalls";
            SectHeader hed = (SectHeader)Headers[str];
            wtr.Write(str + "\0");
            long length = 0;
            long pos;
            wtr.SkipToNextBoundary();
            pos = wtr.BaseStream.Position;
            wtr.Write(length);
            wtr.Write(hed.Version);
            wtr.Write((Int16)0);

            Int16 count = 0;
            foreach (Wall wall in Walls.Values)
                if (wall.Secret)
                {
                    wtr.Write((uint)wall.Location.X);
                    wtr.Write((uint)wall.Location.Y);
                    wtr.Write((uint)wall.Secret_OpenWaitSeconds);
                    wtr.Write((byte)wall.Secret_ScanFlags);
                    wtr.Write((byte)wall.Secret_WallState);
                    wtr.Write((byte)wall.Secret_OpenDelayFrames);
                    wtr.Write((uint)wall.Secret_LastOpenTime);
                    wtr.Write((uint)wall.Secret_r2);
                    count++;
                }

            //rewrite the length
            length = wtr.BaseStream.Position - (pos + 8);
            wtr.Seek((int)pos, SeekOrigin.Begin);
            wtr.Write(length);
            wtr.Seek((int)2, SeekOrigin.Current);
            wtr.Write((Int16)count);
            wtr.Seek(0, SeekOrigin.End);
        }

        private void WriteScriptObject(NoxBinaryWriter wtr)
        {
            string str = "ScriptObject";
            OldSectHeader hed = (OldSectHeader)Headers[str];
            wtr.Write(str + "\0");
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
                    wtr.Write(sf.retval ? 1 : 0);
                    wtr.Write(sf.args);
                    wtr.Write("SYMB".ToCharArray());
                    wtr.Write((Int64)sf.vars.Count);
                    foreach (int var in sf.vars)
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
            wtr.Seek((int)secpos, SeekOrigin.Begin);
            wtr.Write(sectlen);
            wtr.Seek(0, SeekOrigin.End);

            //rewrite the length
            length = wtr.BaseStream.Position - (pos + 8);
            wtr.Seek((int)pos, SeekOrigin.Begin);
            wtr.Write(length);
            wtr.Seek(0, SeekOrigin.End);
        }
        #endregion
    }
}
