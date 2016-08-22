using System;
using System.IO;
using System.Collections;
using Microsoft.Win32;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Globalization;
using System.Windows.Forms;//for messageboxes only
using System.Collections.Generic;
using System.Drawing;

namespace NoxShared
{
    public class ThingDb : NoxDb
    {
        public enum ThingToken : uint
        {
            FLOR = 0x464C4f52,//Floor Tile entry
            EDGE = 0x45444745,//Edge Tile entry
            WALL = 0x57414C4C,//Wall entry
            END = 0x454E4420,//End of entry
            AUD = 0x41554420,//Audio section
            AVNT = 0x41564E54,//???
            SPEL = 0x5350454C, //Spell section
            ABIL = 0x4142494C,//Ability section
            IMAG = 0x494D4147,//Image section
            THNG = 0x54484E47,//474e4854,//Thing entry
            STAT = 0x53544154,//Animation State entry
            SEQU = 0x53455155,//Sequence within a State entry
        }

        public class Tile : IComparable
        {
            public enum TileType : uint
            {
                Floor = ThingToken.FLOR,
                Edge = ThingToken.EDGE
            }

            public Color color;
            public Color col;
            public bool hascolor = false;
            public TileType Type;
            public string Name;
            public List<uint> Variations = new List<uint>();
            public byte numRows;
            public byte numCols;

            public int Id;//must be set as the entries are read in. sorted in this order. (0-n)

            public Tile(Stream stream)
            {
                Read(stream);
            }

            private Color GetColor(uint a) // added by AngryKirC
            {

                byte[] tmp = BitConverter.GetBytes(a);
                return Color.FromArgb(tmp[0], tmp[1], tmp[2]);
                //return Color.FromArgb(unchecked((int)((uint)tilecolors.DirtRed)));
            }

            public void Read(Stream stream)
            {
                BinaryReader rdr = new BinaryReader(stream);

                Type = (TileType)rdr.ReadInt32();
                int numEntries = rdr.ReadInt32();//number of tile entries in this section
                Name = rdr.ReadString();
                color = GetColor(rdr.ReadUInt32()); hascolor = true;
                //color = GetColor(Name); hascolor = true;
                rdr.ReadInt32();
                rdr.ReadInt32();
                numRows = rdr.ReadByte();
                numCols = rdr.ReadByte();
                if (Type == TileType.Floor) rdr.ReadInt16();
                uint var;
                while ((var = rdr.ReadUInt32()) != (int)ThingToken.END)//Variations
                    Variations.Add(var);
            }

            public int CompareTo(object obj)
            {
                Tile rhs = obj as Tile;
                if (rhs == null || Id == rhs.Id)
                    return 0;
                else
                    return (int)(Id - rhs.Id);
            }

            public override string ToString()
            {
                return Name;
            }
        }

        public class Wall
        {
            public string Name;
            public int Id;
            public byte Variations;
            public int unk1;
            public int unk2;
            public int unk3;
            public WallRenderInfo[][] RenderNormal;
            public WallRenderInfo[][] RenderBreakable;

            public Wall(Stream stream)
            {
                RenderNormal = new WallRenderInfo[15][];
                RenderBreakable = new WallRenderInfo[15][];
                Read(stream);
            }

            public struct WallRenderInfo
            {
                public int unknown1;
                public int unknown2;
                public int SpriteIndex;

                public WallRenderInfo(int doX, int doY, int sprite)
                {
                    unknown1 = doX;
                    unknown2 = doY;
                    SpriteIndex = sprite;
                }
            }

            public void Read(Stream stream)
            {
                BinaryReader rdr = new BinaryReader(stream);

                rdr.ReadInt32();//"WALL"
                uint numEntries = rdr.ReadUInt32();
                Name = rdr.ReadString();
                unk1 = rdr.ReadInt32();
                unk2 = rdr.ReadInt32();
                unk3 = rdr.ReadInt32();
                rdr.ReadInt16();
                rdr.BaseStream.Seek((8 - rdr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
                ulong numDebris = rdr.ReadUInt64();
                while (numDebris-- > 0)
                    rdr.ReadString();//debris names
                rdr.ReadString();//secret open sound
                rdr.ReadString();//secret close sound
                rdr.ReadString();//break sound

                List<WallRenderInfo> spritesNormal;
                List<WallRenderInfo> spritesSecret;
                Variations = rdr.ReadByte();
                // FIXME really there are separate # of variations for each direction
                int direction = 0; WallRenderInfo wri;
                while (direction < 15) // 0x5A9E14 - directions
                {
                    byte some = rdr.ReadByte();
                    if (some == 0)
                    {
                        Variations++;
                        continue;
                    }
                    spritesNormal = new List<WallRenderInfo>(4);
                    spritesSecret = new List<WallRenderInfo>(4);
                    rdr.BaseStream.Seek(7, SeekOrigin.Current);
                    while (some > 0)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            wri = new WallRenderInfo(rdr.ReadInt32(), rdr.ReadInt32(), rdr.ReadInt32());
                            if (i > 1)
                                spritesSecret.Add(wri);
                            else
                                spritesNormal.Add(wri);
                            // parseWALL 0x46A010
                        }
                        some--;
                    }
                    RenderNormal[direction] = spritesNormal.ToArray();
                    RenderBreakable[direction] = spritesSecret.ToArray();
                    direction++;
                }
                ThingToken endToken = (ThingToken)rdr.ReadUInt32();
                Debug.Assert(endToken == ThingToken.END, "Failed to parse Wall entry");
            }

            public int CompareTo(object obj)
            {
                Wall rhs = obj as Wall;
                if (rhs == null || Id == rhs.Id)
                    return 0;
                else
                    return (int)(Id - rhs.Id);
            }

            public override string ToString()
            {
                return Name;
            }
        }

        public class AudioMapping
        {
            public string Name;
            public ArrayList Sounds = new ArrayList();
            public byte Flags;//0x01 and 0x40 are the only flags i've seen but no clue as to what they're for...
            // they might not be flags, cause i've never seen 0x40; just 0x00, 0x01, 0x41
            public int u1;
            public int u2;

            public AudioMapping(Stream stream)
            {
                Read(stream);
            }

            public void Read(Stream stream)
            {
                BinaryReader rdr = new BinaryReader(stream);

                Name = rdr.ReadString();
                Flags = rdr.ReadByte();
                u1 = rdr.ReadInt32();
                u2 = rdr.ReadInt32();
                while (true)
                {
                    byte nextByte = rdr.ReadByte();
                    if (nextByte == 0)
                        break;
                    rdr.BaseStream.Seek(-1, SeekOrigin.Current);
                    Sounds.Add(rdr.ReadString());
                }
            }
        }

        public class Avnt
        {
            //dunno what these are for! (Ambient sounds use these)
            // hence the dumb name
            // they are very similar in structure to audio entries

            public string Name;
            public ArrayList Sounds = new ArrayList();
            public int u1;
            public int u2;

            public Avnt(Stream stream)
            {
                Read(stream);
            }

            public void Read(Stream stream)
            {
                BinaryReader rdr = new BinaryReader(stream);

                rdr.ReadInt32();//"AVNT"
                Name = rdr.ReadString();
                u1 = rdr.ReadInt32();
                u2 = rdr.ReadInt32();
                while (true)
                {
                    byte nextByte = rdr.ReadByte();
                    if (nextByte == 0)
                        break;
                    rdr.BaseStream.Seek(-1, SeekOrigin.Current);
                    Sounds.Add(rdr.ReadString());
                }

                //im not sure what the logic is behind the part after the strings...
                // cause sometimes it's 8bytes and other times its 11, or even 17?
                // the last byte for the 11 or 17 length entries is always null
                rdr.ReadInt32();
                rdr.ReadInt32();
                //FIXME, HACK: wrong logic here...
                while (true)
                {
                    uint next = rdr.ReadUInt32();
                    rdr.BaseStream.Seek(-4, SeekOrigin.Current);
                    if (next != (uint)ThingToken.AVNT && next != (uint)ThingToken.SPEL)
                    {
                        rdr.ReadByte();
                    }
                    else
                        break;
                }
            }
        }

        public class Spell
        {
            public enum Phoneme : byte
            {
                KA = 0x00,
                UN = 0x01,
                IN = 0x02,
                ET = 0x03,
                CHA = 0x05,
                RO = 0x06,
                ZO = 0x07,
                DO = 0x08
            }

            public enum SpellFlags : uint
            {
                ENABLED = 0x02
            }

            public string Name;
            public byte ManaCost;
            public ArrayList Phonemes = new ArrayList();
            public uint Flags;
            public string NameString;
            public string DescriptionString;
            public string SoundCast;
            public string SoundOn;
            public string SoundOff;

            public bool Enabled
            {
                get
                {
                    return (Flags & (uint)SpellFlags.ENABLED) != 0;
                }
                set
                {
                    Flags |= (uint)SpellFlags.ENABLED;
                }
            }

            public int i1, i2;
            public int b1, b2;

            public Spell(Stream stream)
            {
                Read(stream);
            }

            public void Read(Stream stream)
            {
                BinaryReader rdr = new BinaryReader(stream);

                Name = rdr.ReadString();
                ManaCost = rdr.ReadByte();
                b1 = rdr.ReadByte();
                b2 = rdr.ReadByte();
                int numPhonemes = rdr.ReadByte();
                while (numPhonemes-- > 0)
                    Phonemes.Add((Phoneme)rdr.ReadByte());
                i1 = rdr.ReadInt32();
                i2 = rdr.ReadInt32();
                Flags = rdr.ReadUInt32();

                NameString = rdr.ReadString();
                DescriptionString = new string(rdr.ReadChars(rdr.ReadUInt16()));
                SoundCast = rdr.ReadString();
                SoundOn = rdr.ReadString();
                SoundOff = rdr.ReadString();
            }
        }

        public class Ability
        {
            public string Name;
            public string NameString;
            public string DescriptionString;
            public string SoundCast;
            public string SoundOn;
            public string SoundOff;

            public int i1, i2, i3;

            public Ability(Stream stream)
            {
                Read(stream);
            }

            public void Read(Stream stream)
            {
                BinaryReader rdr = new BinaryReader(stream);

                Name = rdr.ReadString();
                rdr.ReadByte();//null byte
                i1 = rdr.ReadInt32();
                i2 = rdr.ReadInt32();
                i3 = rdr.ReadInt32();

                NameString = rdr.ReadString();
                DescriptionString = new string(rdr.ReadChars(rdr.ReadUInt16()));
                SoundCast = rdr.ReadString();
                SoundOn = rdr.ReadString();
                SoundOff = rdr.ReadString();
            }
        }

        public class Sprite
        {
            public class Sequence
            {
                public string Name;
                public int[] Frames;

                internal Sequence(Stream stream)
                {
                    Read(stream);
                }

                internal void Read(Stream stream)
                {
                    BinaryReader rdr = new BinaryReader(stream);
                    uint next = rdr.ReadUInt32();
                    Debug.Assert(next == (uint)ThingToken.SEQU, "Invalid ThingDb.Image.Animation.Sequece entry.");
                    Name = rdr.ReadString();
                    var framez = new List<int>();

                    while (true)
                    {
                        next = rdr.ReadUInt32();
                        rdr.BaseStream.Seek(-4, SeekOrigin.Current);
                        if (next != (uint)ThingToken.SEQU//HACK: this condition is used to detect end of Frame list within a Sequence(FIXME)
                            && next != (uint)ThingToken.STAT
                            && next != (uint)ThingToken.END)
                            framez.Add(rdr.ReadInt32());
                        else
                            break;
                    }

                    Frames = framez.ToArray();
                }
            }

            public class Animation
            {
                public enum AnimationType
                {
                    Loop,
                    OneShot,
                    OneShotRemove,
                    LoopAndFade,
                    Slave,
                    Random
                }

                public AnimationType Type;
                public List<int> Frames = new List<int>();
                public byte MonsterAnimId;

                public List<Sprite.Sequence> Sequences = new List<Sprite.Sequence>();

                public Animation(Stream stream)
                {
                    Read(stream);
                }

                public void Read(Stream stream)
                {
                    BinaryReader rdr = new BinaryReader(stream);
                    byte count = rdr.ReadByte();

                    MonsterAnimId = rdr.ReadByte();
                    Type = (AnimationType)Enum.Parse(typeof(AnimationType), rdr.ReadString());
                    uint next = rdr.ReadUInt32();
                    rdr.BaseStream.Seek(-4, SeekOrigin.Current);
                    if (next == (uint)ThingToken.SEQU)
                        while (true)
                        {
                            next = rdr.ReadUInt32();
                            rdr.BaseStream.Seek(-4, SeekOrigin.Current);
                            if (next == (uint)ThingToken.SEQU)
                                Sequences.Add(new Sequence(stream));
                            else
                                break;
                        }
                    else
                    {
                        while (count-- > 0)
                            Frames.Add(rdr.ReadInt32());
                    }
                }
            }

            public class State
            {
                public string Name;
                public Animation Animation;
                public byte ActionId;

                public State(Stream stream)
                {
                    Read(stream);
                }

                public void Read(Stream stream)
                {
                    BinaryReader rdr = new BinaryReader(stream);
                    uint next = rdr.ReadUInt32();
                    Debug.Assert(next == (uint)ThingToken.STAT, "Invalid ThingDb.Image.State entry."); // Never put a read in an assert
                    //FIXME: special cases beyond this point
                    int type = rdr.ReadInt32();

                    if (type <= 0x08)//in this form, the States usually come in threes, the first has type 2, then 4, then 8
                    {
                        //Note that this format is similar to that of MonsterDraw, except that monster draw has 1 byte preceding this part, not 4
                        Name = rdr.ReadString();
                        short maybeCount = rdr.ReadInt16();
                        Animation = new Animation(rdr.BaseStream);
                    }
                    else
                    {
                        rdr.BaseStream.Seek(-4, SeekOrigin.Current);

                        //TOTAL HACK to differentiate between STATs in PlayerDraw and MonsterDraw
                        ActionId = rdr.ReadByte();
                        if (Char.IsLetter((char)rdr.PeekChar()))//for PlayerDraw
                        {
                            rdr.BaseStream.Seek(-1, SeekOrigin.Current);
                            Name = rdr.ReadString();
                            Animation = new Animation(rdr.BaseStream);
                        }
                        else//for MonsterDraw, MaidenDraw
                        {
                            Name = rdr.ReadString();
                            rdr.ReadInt16();//always 0x0001?
                            //HACK keep tacking on till we see a STAT or END
                            Animation = new Animation(rdr.BaseStream);
                            while (true)
                            {
                                next = rdr.ReadUInt32();
                                rdr.BaseStream.Seek(-4, SeekOrigin.Current);
                                if (next == (uint)ThingToken.STAT
                                    || next == (uint)ThingToken.END)
                                    break;
                                else
                                    Animation.Frames.Add(rdr.ReadInt32());
                            }
                        }
                    }
                }
            }

            public string Name;

            public int type1;
            public Animation type2;

            public Sprite(Stream stream)
            {
                Read(stream);
            }

            public void Read(Stream stream)
            {
                BinaryReader rdr = new BinaryReader(stream);

                Name = rdr.ReadString();
                byte type = rdr.ReadByte();
                if (type == 1)
                    type1 = rdr.ReadInt32();
                else if (type == 2)
                {
                    type2 = new Animation(rdr.BaseStream);
                }
                else
                    Logger.Log(String.Format("Unknown Image type: {0} ({1})", type, Name));
            }
        }

        public class Thing
        {
            //these field names must remain as is!! Read() uses reflection to initialize them
            public string Name;
            public uint Speed;
            public uint Health;
            public uint Worth;
            public int SizeX;
            public int SizeY;
            public string Size;
            public string Extent;
            public string ExtentType;
            public int ExtentX;
            public int ExtentY;
            public int Z;
            public string ZSize;
            public int ZSizeX;
            public int ZSizeY;
            public FlagsFlags Flags;
            public ClassFlags Class;
            public BitArray Subclass = new BitArray(subclassBitCount);
            public uint Weight;
            public MaterialFlags Material;
            public float Mass;
            public string Pickup;
            public string Drop;
            public string Collide;
            public string Xfer;
            public string Create;
            public string Damage;
            public string Die;
            public string Init;
            public string Update;
            public string PrettyName;
            public string Description;
            public string Use;
            public byte WandCharges;
            public string DrawType = "NoDraw";
            public int SpritePrettyImage;
            public int SpriteMenuIcon;
            public List<int> SpriteAnimFrames = new List<int>();
            public List<Sprite.State> SpriteStates = new List<Sprite.State>();

            [Flags]
            public enum FlagsFlags : ulong
            {
                // These are guaranteed to correlate with values used by Nox internally
                NULL = 0,
                BELOW = 0x1,
                NO_UPDATE = 0x2,
                ACTIVE = 0x4,
                ALLOW_OVERLAP = 0x8,
                SHORT = 0x10,
                DESTROYED = 0x20,
                NO_COLLIDE = 0x40,
                MISSILE_HIT = 0x80,
                EQUIPPED = 0x100,
                PARTITIONED = 0x200,
                NO_COLLIDE_OWNER = 0x400,
                OWNER_VISIBLE = 0x800,
                EDIT_VISIBLE = 0x1000,
                NO_PUSH_CHARACTERS = 0x2000,
                AIRBORNE = 0x4000,
                DEAD = 0x8000,
                SHADOW = 0x10000,
                FALLING = 0x20000,
                IN_HOLE = 0x40000,
                RESPAWN = 0x80000,
                ON_OBJECT = 0x100000,
                SIGHT_DESTROY = 0x200000,
                TRANSIENT = 0x400000,
                BOUNCY = 0x800000,
                ENABLED = 0x1000000,
                PENDING = 0x2000000,
                TRANSLUCENT = 0x4000000,
                STILL = 0x8000000,
                NO_AUTO_DROP = 0x10000000,
                FLICKER = 0x20000000,
                SELECTED = 0x40000000,
                MARKED = 0x80000000
            }

            [Flags]
            public enum ClassFlags : ulong
            {
                // These are guaranteed to correlate with values used by Nox internally
                NULL = 0x0,//0
                MISSILE = 0x1,
                MONSTER = 0x2,//1
                PLAYER = 0x4, //2
                OBSTACLE = 0x8,//3
                FOOD = 0x10,//4
                EXIT = 0x20,
                KEY = 0x40,//5
                DOOR = 0x80,
                INFO_BOOK = 0x100,
                TRIGGER = 0x200,
                TRANSPORTER = 0x400,
                HOLE = 0x800,
                WAND = 0x1000,
                FIRE = 0x2000,
                ELEVATOR = 0x4000,
                ELEVATOR_SHAFT = 0x8000,
                DANGEROUS = 0x10000,
                MONSTERGENERATOR = 0x20000,
                READABLE = 0x40000,
                LIGHT = 0x80000,
                SIMPLE = 0x100000,
                COMPLEX = 0x200000,
                IMMOBILE = 0x400000,
                VISIBLE_ENABLE = 0x800000,
                WEAPON = 0x1000000,//6
                ARMOR = 0x2000000,//7
                NOT_STACKABLE = 0x4000000,
                TREASURE = 0x8000000,//8
                FLAG = 0x10000000,
                CLIENT_PERSIST = 0x20000000,
                CLIENT_PREDICT = 0x40000000,
                PICKUP = 0x80000000
            }

            //FIXME: too many! break up into 2 sets? number these 0,1,2,etc. and use as indexes into BitArray?
            //[Flags]
            //public enum SubclassFlags : ulong
            protected const int subclassBitCount = 97;
            public enum SubclassBitIndex : int//index into the bitarray
            {
                //these values are arbitrary and not necessarily what Nox uses internally.
                //assumes that the first enum is 0 then 1,2,3,etc. (verify this in c# specification)
                NULL,
                ABILITY_BOOK,
                APPLE,
                ARM_ARMOR,
                ARROW,
                AXE,
                BACK,
                BOLT,
                BOMBER,
                BOOTS,
                BOW,
                BREASTPLATE,
                CHAKRAM,
                CHEST_NE,
                CHEST_NW,
                CHEST_SE,
                CHEST_SW,
                CROSSBOW,
                CURE_POISON_POTION,
                DAGGER,
                FEMALE_NPC,
                FIELD_GUIDE,
                FIRE_PROTECT_POTION,
                GATE,
                GENERATOR_NE,
                GENERATOR_NW,
                GENERATOR_SE,
                GENERATOR_SW,
                GREAT_SWORD,
                HAMMER,
                HAS_SOUL,
                HASTE_POTION,
                HEALTH_POTION,
                HEAVY,
                HELMET,
                IMMUNE_ELECTRICITY,
                IMMUNE_FEAR,
                IMMUNE_FIRE,
                IMMUNE_POISON,
                INFRAVISION_POTION,
                INVISIBILITY_POTION,
                INVISIBLE_OBELISK,
                INVULNERABILITY_POTION,
                JUG,
                LARGE_MONSTER,
                LAVA,
                LEG_ARMOR,
                LONG_SWORD,
                LOOK_AROUND,
                LOTD,
                MACE,
                MAGIC,
                MANA_POTION,
                MEDIUM_MONSTER,
                MISSILE_COUNTERSPELL,
                MUSHROOM,
                NO_SPELL_TARGET,
                NO_TARGET,
                NPC,
                NPC_WIZARD,
                OGRE_AXE,
                PANTS,
                POISON_PROTECT_POTION,
                POTION,
                QUEST_EXIT,
                QUEST_WARP_EXIT,
                QUIVER,
                SHIELD,
                SHIELD_POTION,
                SHIRT,
                SHOCK_PROTECT_POTION,
                SHOPKEEPER,
                SHURIKEN,
                SIMPLE,
                SMALL_MONSTER,
                SPELL_BOOK,
                STAFF,
                STAFF_DEATH_RAY,
                STAFF_FIREBALL,
                STAFF_FORCE_OF_NATURE,
                STAFF_LIGHTNING,
                STAFF_OBLIVION_HALBERD,
                STAFF_OBLIVION_HEART,
                STAFF_OBLIVION_ORB,
                STAFF_OBLIVION_WIERDLING,
                STAFF_SULPHOROUS_FLARE,
                STAFF_SULPHOROUS_SHOWER,
                STAFF_TRIPLE_FIREBALL,
                STONE_DOOR,
                SWORD,
                TECH,
                UNDEAD,
                USEABLE,
                VAMPIRISM_POTION,
                VISIBLE_OBELISK,
                WARCRY_STUN,
                WOUNDED_NPC,
            }

            [Flags]
            public enum MaterialFlags
            {
                NULL = 0x0,
                ANIMAL_HIDE = 0x1,
                BELOW = 0x2,
                CLOTH = 0x4,
                EARTH = 0x8,
                FLESH = 0x10,
                METAL = 0x20,
                NO_COLLIDE = 0x40,
                NO_PUSH_CHARACTERS = 0x80,
                NONE = 0x100,
                STONE = 0x200,
                WOOD = 0x400,
            }

            public Thing(Stream stream)
            {
                Read(stream);
            }

            public bool HasClassFlag(ClassFlags flag)
            {
                if ((Class & flag) == flag) return true;
                return false;
            }

            public bool HasObjectFlag(FlagsFlags flag)
            {
                if ((Flags & flag) == flag) return true;
                return false;
            }

            public void Read(Stream stream)
            {
                BinaryReader rdr = new BinaryReader(stream);

                rdr.ReadInt32();//"THNG"
                Name = rdr.ReadString();

                while (true)
                {
                    byte nextByte = rdr.ReadByte();
                    if (nextByte == 0)//thing entry is terminated by a null byte
                        break;
                    rdr.BaseStream.Seek(-1, SeekOrigin.Current);

                    string line = rdr.ReadString();
                    if (line.EndsWith("Draw") && line.IndexOf(" ") == -1)
                        DrawType = line;

                    //skip, length, raw frames
                    if (line == "StaticDraw"
                        || line == "ArmorDraw"
                        || line == "WeaponDraw"
                        || line == "SlaveDraw"
                        || line == "BaseDraw")//single frame
                    {
                        rdr.BaseStream.Seek((8 - rdr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
                        long finishPos = rdr.ReadInt64() + rdr.BaseStream.Position;
                        SpriteAnimFrames = new List<int>();
                        if (line == "SlaveDraw")
                        {
                            byte len = rdr.ReadByte();
                            while ((len--) != 0)
                                SpriteAnimFrames.Add(rdr.ReadInt32());
                        }
                        else
                            SpriteAnimFrames.Add(rdr.ReadInt32());
                        rdr.BaseStream.Seek(finishPos, SeekOrigin.Begin);
                    }
                    //skip, length, 1 animation
                    else if (line == "AnimateDraw"
                        || line == "SphericalShieldDraw"
                        || line == "WeaponAnimateDraw"
                        || line == "FlagDraw"
                        || line == "SummonEffectDraw"
                        || line == "ReleasedSoulDraw"
                        || line == "GlyphDraw")
                    {
                        rdr.BaseStream.Seek((8 - rdr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
                        long finishPos = rdr.ReadInt64() + rdr.BaseStream.Position;
                        Sprite.Animation ani = new Sprite.Animation(rdr.BaseStream);
                        while (rdr.BaseStream.Position < finishPos)//HACK
                        {
                            ani.Frames.Add(rdr.ReadInt32());
                        }
                        SpriteAnimFrames = ani.Frames;
                        Debug.Assert(rdr.BaseStream.Position == finishPos);
                    }
                    //skip, length, State entries
                    else if (line == "AnimateStateDraw"
                        || line == "PlayerDraw"
                        || line == "MonsterDraw"
                        || line == "MaidenDraw")
                    {
                        rdr.BaseStream.Seek((8 - rdr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
                        ulong length = rdr.ReadUInt64();
                        while (true)
                        {
                            uint next = rdr.ReadUInt32();
                            if (next == (uint)ThingToken.END)
                                break;
                            else
                            {
                                rdr.BaseStream.Seek(-4, SeekOrigin.Current);
                                while (true)
                                {
                                    next = rdr.ReadUInt32();
                                    rdr.BaseStream.Seek(-4, SeekOrigin.Current);
                                    if (next == (uint)ThingToken.STAT)
                                        SpriteStates.Add(new Sprite.State(rdr.BaseStream));
                                    else
                                        break;
                                }
                            }
                        }
                    }
                    //skip, length, byte(numFrames) prefixed raw frames
                    else if (line == "BoulderDraw"
                        || line == "StaticRandomDraw"
                        || line == "DoorDraw"
                        || line == "ArrowDraw"
                        || line == "HarpoonDraw"
                        || line == "WeakArrowDraw")
                    {
                        rdr.BaseStream.Seek((8 - rdr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
                        ulong length = rdr.ReadUInt64();
                        byte numFrames = rdr.ReadByte();
                        SpriteAnimFrames = new List<int>(numFrames);
                        while (numFrames-- > 0)
                            SpriteAnimFrames.Add(rdr.ReadInt32());
                    }
                    else if (line == "VectorAnimateDraw")
                    {
                        rdr.BaseStream.Seek((8 - rdr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
                        long finishPos = rdr.ReadInt64() + rdr.BaseStream.Position;
                        Sprite.Animation ani = new Sprite.Animation(rdr.BaseStream);
                        //FIXME: this may be a Loop of Loops and should probably be constructed as such
                        //HACK: right now we just read until we reach the given length, tacking on the frames to the existing ones
                        while (rdr.BaseStream.Position < finishPos)
                        {
                            ani.Frames.Add(rdr.ReadInt32());
                        }
                        SpriteAnimFrames = ani.Frames;
                    }
                    //skip, length, number of animations, animations
                    else if (line == "ConditionalAnimateDraw"
                        || line == "MonsterGeneratorDraw")
                    {
                        rdr.BaseStream.Seek((8 - rdr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
                        long finishPos = rdr.ReadInt64() + rdr.BaseStream.Position;
                        byte numAni = rdr.ReadByte();
                        while (numAni-- > 0)
                            new Sprite.Animation(rdr.BaseStream);
                        Debug.Assert(rdr.BaseStream.Position == finishPos);
                    }
                    //2 pretty useless ints, then skip -- this is the most common and simplest, so the default
                    else if (line.EndsWith("Draw") && line.IndexOf(" ") == -1)
                    {
                        rdr.ReadInt32();
                        rdr.ReadInt32();
                        rdr.BaseStream.Seek((8 - rdr.BaseStream.Position % 8) % 8, SeekOrigin.Current);//SkipToNextBoundary
                    }
                    else if (line == "MENUICON")
                        SpriteMenuIcon = rdr.ReadInt32();
                    else if (line == "PRETTYIMAGE")
                        SpritePrettyImage = rdr.ReadInt32();
                    else
                        Parse(line);
                }
            }

            private Regex regex1 = new Regex("(?<field>.*)( = )(?<value>.*)", RegexOptions.IgnoreCase);
            private Regex regex2 = new Regex("(?<flag>\\w+)");
            public void Parse(string line)
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                Regex regex = regex1;
                Match regexmatch = regex.Match(line);
                string fldString = regexmatch.Groups["field"].Value;
                string valString = regexmatch.Groups["value"].Value;
                Type typeClassFlags = typeof(ClassFlags);
                Type typeFlagsFlags = typeof(FlagsFlags);

                FieldInfo field = GetType().GetField(fldString, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (field != null)
                {
                    object val = null; string[] strs = null;
                    ClassFlags cflags = 0; FlagsFlags fflags = 0;

                    //special handling for enumed types
                    switch (field.Name)
                    {
                        case "Flags":
                            regex = regex2;//group "flag" will have whatever's between plus signs

                            foreach (Match match in regex.Matches(valString))
                            {

                                string flag = match.Groups["flag"].Value;
                                // get rid of those pesky exceptions
                                if (Enum.IsDefined(typeFlagsFlags, flag))
                                    fflags |= (FlagsFlags)Enum.Parse(typeof(FlagsFlags), flag);
                            }
                            field.SetValue(this, fflags);
                            break;
                        case "Z":
                            field.SetValue(this, Convert.ToInt32(valString));
                            break;
                        case "ZSize":
                            field.SetValue(this, valString);
                            strs = valString.Split(' ');
                            ZSizeX = Convert.ToInt32(strs[0]);
                            ZSizeY = Convert.ToInt32(strs[1]);
                            break;
                        case "Size":
                            field.SetValue(this, valString);
                            strs = valString.Split(' ');
                            SizeX = Convert.ToInt32(strs[0]);
                            SizeY = Convert.ToInt32(strs[1]);
                            break;
                        case "Extent":
                            field.SetValue(this, valString);
                            if (valString.Contains("CIRCLE"))
                            {
                                strs = valString.Split(' ');
                                ExtentType = strs[0];
                                ExtentX = Convert.ToInt32(strs[1]);

                            }
                            else if (valString.Contains("BOX"))
                            {
                                strs = valString.Split(' ');
                                ExtentType = strs[0];
                                ExtentX = Convert.ToInt32(strs[1]);
                                ExtentY = Convert.ToInt32(strs[2]);
                            }
                            else if (valString.Contains("CENTER"))
                            {
                                ExtentType = valString;
                            }


                            break;
                        case "Class":
                            regex = regex2;//group "flag" will have whatever's between plus signs
                            ArrayList enums = new ArrayList();

                            foreach (Match match in regex.Matches(valString))
                            {
                                string flag = match.Groups["flag"].Value;
                                // get rid of those pesky exceptions
                                if (Enum.IsDefined(typeClassFlags, flag))
                                    cflags |= (ClassFlags)Enum.Parse(typeClassFlags, flag);
                            }

                            field.SetValue(this, cflags);
                            break;
                        case "Subclass":
                            regex = regex2;//group "flag" will have whatever's between plus signs
                            foreach (Match match in regex.Matches(valString))
                                Subclass[(int)(SubclassBitIndex)Enum.Parse(typeof(SubclassBitIndex), match.Groups["flag"].Value)] = true;
                            break;
                        case "Material":
                            regex = regex2;//group "flag" will have whatever's between plus signs
                            MaterialFlags flags = 0;
                            foreach (Match match in regex.Matches(valString))
                                flags |= (MaterialFlags)Enum.Parse(typeof(MaterialFlags), match.Groups["flag"].Value);
                            field.SetValue(this, flags);
                            break;
                        case "Use":
                            val = valString;
                            // Handle WandUse, WandCastUse
                            strs = valString.Split(' ');
                            if (strs[0] == "WandUse" || strs[0] == "WandCastUse")
                            {
                                WandCharges = 0;
                                Byte.TryParse(strs[1], out WandCharges);
                            }
                            break;
                        default:
                            //simply parse any other fields...
                            if (field.FieldType == typeof(String))
                                val = valString;
                            else if (field.FieldType == typeof(Int32))
                                val = Convert.ToInt32(valString, culture);
                            else if (field.FieldType == typeof(UInt32))
                                val = Convert.ToUInt32(valString, culture);
                            else if (field.FieldType == typeof(Byte))
                                val = Convert.ToByte(valString, culture);
                            else if (field.FieldType == typeof(Single))
                                val = Convert.ToSingle(valString, culture);
                            break;
                    }

                    if (val != null) field.SetValue(this, val);
                }
            }
        }

        public static List<Tile> FloorTiles = new List<Tile>();
        public static List<Tile> EdgeTiles = new List<Tile>();
        public static List<Wall> Walls = new List<Wall>();
        public static SortedDictionary<string, Thing> Things = new SortedDictionary<string, Thing>();
        public static SortedDictionary<string, AudioMapping> AudioMappings = new SortedDictionary<string, AudioMapping>();
        public static SortedDictionary<string, Avnt> Avnts = new SortedDictionary<string, Avnt>();
        public static SortedList<string, Spell> Spells = new SortedList<string, Spell>();
        public static SortedDictionary<string, Ability> Abilities = new SortedDictionary<string, Ability>();
        public static List<Sprite> Images = new List<Sprite>();

        public static List<string> FloorTileNames
        {
            get
            {
                List<string> list = new List<string>();
                foreach (Tile tile in FloorTiles)
                    list.Add(tile.Name);
                return list;
            }
        }

        public static List<string> EdgeTileNames
        {
            get
            {
                List<string> list = new List<string>();
                foreach (Tile tile in EdgeTiles)
                    list.Add(tile.Name);
                return list;
            }
        }

        public static List<string> WallNames
        {
            get
            {
                List<string> list = new List<string>();
                foreach (Wall wall in Walls)
                    list.Add(wall.Name);
                return list;
            }
        }

        static ThingDb()
        {
            dbFile = "thing.bin";
            NoxBinaryReader rdr = new NoxBinaryReader(GetStream(), CryptApi.NoxCryptFormat.THING);

            int floorId = 0, edgeId = 0, wallId = 0;
            for (ThingToken token = NextToken(rdr); Enum.IsDefined(typeof(ThingToken), token); token = NextToken(rdr))
            {
                if (token == ThingToken.FLOR)
                {
                    Tile tile = new Tile(rdr.BaseStream);
                    tile.Id = floorId++;
                    FloorTiles.Add(tile);
                }
                else if (token == ThingToken.EDGE)
                {
                    Tile tile = new Tile(rdr.BaseStream);
                    tile.Id = edgeId++;
                    EdgeTiles.Add(tile);
                }
                else if (token == ThingToken.WALL)
                {
                    Wall wall = new Wall(rdr.BaseStream);
                    wall.Id = wallId++;
                    Walls.Add(wall);
                }
                else if (token == ThingToken.AUD)
                    ReadEntries(rdr);
                else if (token == ThingToken.AVNT)
                {
                    Avnt avnt = new Avnt(rdr.BaseStream);
                    Avnts.Add(avnt.Name, avnt);
                }
                else if (token == ThingToken.SPEL)
                    ReadEntries(rdr);
                else if (token == ThingToken.ABIL)
                    ReadEntries(rdr);
                else if (token == ThingToken.IMAG)
                    ReadEntries(rdr);
                else if (token == ThingToken.THNG)
                {
                    Thing thing = new Thing(rdr.BaseStream);

                    if (!Things.ContainsKey(thing.Name))//there are a few duplicates, but they seem to be identical
                        Things.Add(thing.Name, thing);


                    if (thing.Name.Contains("Obelisk"))
                        thing.ZSizeY += 20;
                    else if (thing.Name.Contains("NPC"))
                        thing.ZSizeY = 35;
                    else if (thing.Name.Contains("TraderArmorRack"))
                        thing.ZSizeY += 25;
                    else if (thing.Name.Contains("Tree"))
                    {
                        thing.ExtentX += 7;
                        thing.ZSizeY *= 4;

                        if (thing.Name == "TreeApple")
                            thing.ExtentX += 15;

                        if (thing.Name == "TreeForest05"
                        || thing.Name == "TreeApple"
                        || thing.Name == "TreeForest06"
                        || thing.Name == "TreeForest10"
                        || thing.Name == "TreeForest17"
                        || thing.Name == "TreeGalava07"
                        || thing.Name == "TreeGalava10"
                        || thing.Name == "TreeGalava04"
                        || thing.Name == "TreeGalava05"
                        || thing.Name == "TreePine02"
                        || thing.Name == "TreePine03"
                        || thing.Name == "TreePine04"
                        || thing.Name == "TreePine05"
                        || thing.Name == "TreePine06"
                        || thing.Name == "TreePine07"
                        || thing.Name == "TreePine09"
                        || thing.Name == "TreePine10"
                        || thing.Name == "TreePine11"
                        || thing.Name == "TreePine12"
                        || thing.Name == "TreePine13"
                        || thing.Name == "TreePine14"
                        || thing.Name == "TreeGreen10"
                        || thing.Name == "TreeGreen4"
                        || thing.Name == "TreeGreen5"
                        || thing.Name == "TreeGreen6"
                        || thing.Name == "TreeGreen7"
                        || thing.Name == "TreeGreen8"
                        || thing.Name == "TreeGreen9"
                        || thing.Name == "TreeOgre06"
                        || thing.Name.Contains("TreeMangrove")
                        || thing.Name.Contains("Willow")
                        || thing.Name.Contains("OldTree")
                        || thing.Name.Contains("TreeSnowCovered"))
                            thing.ZSizeY = 55;


                        if (thing.Name == "TreeForest13" || thing.Name == "TreeForest14")
                            thing.ZSizeY += 50;

                        if (thing.Name == "TreePine01" || thing.Name == "TreePine08")
                            thing.ZSizeY = 80;

                    }
                    if (thing.Name.Contains("Aspen"))
                    {
                        thing.ExtentX += 8;
                        if (thing.Name == "Aspen1")
                            thing.ExtentX += 8;
                        else if (thing.Name == "Aspen3")
                            thing.ExtentX += 12;


                        thing.ZSizeY += 35;
                    }
                    else if (thing.Name.Contains("Decidious"))
                    {
                        if (thing.Name == "Decidious2" || thing.Name == "Decidious1" || thing.Name == "Decidious4" || thing.Name == "Decidious3" || thing.Name == "Decidious13")
                        {
                            thing.ExtentX += 15;
                            thing.SizeY = 90;
                        }
                        else
                        {
                            thing.ExtentX += 10;
                            thing.ZSizeY += 7;
                        }
                    }



                    else if (thing.Name.Contains("Conifer"))
                    {
                        thing.ZSizeY += 35;
                        thing.ExtentX += 7;
                    }


                    else if (thing.Name.Contains("Bookcase") || thing.Name.Contains("Shelves"))
                        thing.ZSizeY += 20;
                    else if (thing.Name.Contains("Gargoyle"))
                        thing.ZSizeY = 60;

                    else if (thing.Name == "OgreFirePitUnlit")
                        thing.ExtentX += 25;

                    if (thing.Name == "DockUpCenter1")
                    {
                        thing.ExtentX += 35;
                        thing.ExtentY += 45;
                    }
                    if (thing.Name == "DockUpFarEnd" || thing.Name == "DockUpNearEnd")
                    {
                        thing.ExtentX = 70;
                        thing.ExtentY = 8;
                        thing.ZSizeY = 10;
                    }
                    else if (thing.Name.Contains("DockUp"))
                    {
                        thing.ExtentX += 15;
                        thing.ZSizeY = 15;
                    }
                    if (thing.Name == "DockDownCenter1")
                    {
                        thing.ExtentY += 5;
                        thing.ExtentX += 65;
                    }
                    if (thing.Name == "DockDownFarEnd" || thing.Name == "DockDownNearEnd")
                    {
                        thing.ExtentY = 70;
                        thing.ExtentX = 8;
                        thing.ZSizeY = 10;
                    }
                    else if (thing.Name.Contains("DockDown"))
                    {
                        thing.ExtentY += 15;
                        thing.ZSizeY = 15;
                    }
                    else if (thing.Name.Contains("Fireplace"))
                        thing.ZSizeY += 10;
                    else if (thing.Name.Contains("RopeBridge1"))
                    {
                        thing.ZSizeY = 10;
                        thing.ExtentX = 50;
                        thing.ExtentY = 70;

                        if (thing.Name == "RopeBridge1Center2Front" || thing.Name == "RopeBridge1CenterBrokenFront" || thing.Name == "RopeBridge1CenterFront")
                        {
                            thing.SizeY = 120;
                            thing.SizeX = 80;
                            thing.ExtentY = 10;
                            thing.ExtentX = 60;
                        }
                        else if (thing.Name.Contains("EndBack"))
                        {
                           // thing.ZSizeY = 10;
                            thing.ExtentX = 40;
                            thing.ExtentY = 60;
                        }
                        else if (thing.Name == "RopeBridge1FarEndFront" || thing.Name == "RopeBridge1NearEndFront")
                        {
                            thing.ExtentX = 40;
                            thing.ExtentY = 20;
                            thing.SizeY = 120;
                            thing.SizeX = 80;
                        }
                    }
                    else if (thing.Name.Contains("RopeBridge2"))
                    {
                        thing.ZSizeY = 10;
                        thing.ExtentY = 50;
                        thing.ExtentX = 70;

                        if (thing.Name == "RopeBridge2Center2Front" || thing.Name == "RopeBridge2CenterBrokenFront" || thing.Name == "RopeBridge2CenterFront")
                        {
                            thing.SizeY = 130;
                            thing.SizeX = 180;
                            thing.ExtentX = 10;
                            thing.ExtentY = 60;
                        }
                            /*
                        else if (thing.Name.Contains("EndBack"))
                        {
                            // thing.ZSizeY = 10;
                            

                             


                        }
                        */

                        else if (thing.Name == "RopeBridge2FarEndFront" || thing.Name == "RopeBridge2NearEndFront")
                        {
                            thing.ExtentY = 40;
                            thing.ExtentX = 20;
                            thing.SizeY = 130;
                            thing.SizeX = 180;
                        }
                        
                    }
                    if (thing.Name == "RopeBridgeBroken1FarEndBack" || thing.Name == "RopeBridgeBroken1NearEndBack")
                    {
                        thing.ExtentX = 40;
                        thing.ExtentY = 60;

                    }
                    else if (thing.Name == "RopeBridgeBroken2NearEndBack")
                    {
                        thing.ZSizeY = 10;
                        thing.ExtentY = 50;
                        thing.ExtentX = 70;

                    }
                    else if (thing.Name == "RopeBridgeBroken2FarEndBack")
                    {
                        thing.ZSizeY = 10;
                        thing.ExtentY = 30;
                        thing.ExtentX = 70;

                    }

                    else if (thing.Name == "RopeBridgeBroken1NearEndFront")
                    {
                        thing.ZSizeY = 10;
                        thing.ExtentX = 40;
                        thing.ExtentY = 20;
                        thing.SizeY = 120;
                        thing.SizeX = 80;
                    }
                    else if (thing.Name == "RopeBridgeBroken1FarEndFront")
                    {
                        thing.ZSizeY = 10;
                        thing.ExtentX = 40;
                        thing.ExtentY = 20;
                        thing.SizeY = 100;
                        thing.SizeX = 60;
                    }
                    else if (thing.Name == "RopeBridgeBroken2FarEndFront")
                    {
                        thing.ZSizeY = 10;
                        thing.ExtentY = 40;
                        thing.ExtentX = 20;
                        thing.SizeY = 120;
                        thing.SizeX = 180;
                    }
                    else if (thing.Name == "RopeBridgeBroken2NearEndFront")
                    {
                        thing.ZSizeY = 50;
                        thing.ExtentY = 15;
                        thing.ExtentX = 15;
                        thing.SizeY = 160;
                        thing.SizeX = 170;

                    }
                    else if (thing.Name.StartsWith("Amb"))
                    {
                        thing.ZSizeY = 0;
                        thing.ExtentX = 17;

                    }
                }
                else
                    Debug.Fail("Encountered unkown token while reading thing.bin");
            }

            Debug.Assert(rdr.BaseStream.Length - rdr.BaseStream.Position < 8, "Error reading thing.bin", "Could not parse entire file");
        }

        protected static void ReadEntries(BinaryReader rdr)
        {
            ThingToken type = (ThingToken)rdr.ReadInt32();
            uint numEntries = rdr.ReadUInt32();

            if (type == ThingToken.AUD)
                for (; numEntries > 0; numEntries--)
                {
                    AudioMapping aud = new AudioMapping(rdr.BaseStream);
                    AudioMappings.Add(aud.Name, aud);
                }
            else if (type == ThingToken.SPEL)
                for (; numEntries > 0; numEntries--)
                {
                    Spell spell = new Spell(rdr.BaseStream);
                    Spells.Add(spell.Name, spell);
                }
            else if (type == ThingToken.ABIL)
                for (; numEntries > 0; numEntries--)
                {
                    Ability abil = new Ability(rdr.BaseStream);
                    Abilities.Add(abil.Name, abil);
                }
            else if (type == ThingToken.IMAG)
                for (; numEntries > 0; numEntries--)
                {
                    Sprite img = new Sprite(rdr.BaseStream);
                    Images.Add(img);
                }

            Debug.Assert(numEntries == 0, "Wrong number of entries read.");
        }

        private static ThingToken NextToken(NoxBinaryReader rdr)
        {
            ThingToken result = (ThingToken)0xFFFFFFFF;
            if (rdr.BaseStream.Position < rdr.BaseStream.Length)
            {
                result = (ThingToken)rdr.ReadUInt32();
                rdr.BaseStream.Seek(-4, SeekOrigin.Current);
            }
            return result;
        }

        public static Thing GetThing(string name)
        {
            /*foreach (Thing thing in Things)
                if (thing.Name == name)
                    return thing;
            return null;*/
            return (Thing)Things[name];
        }
    }
}
