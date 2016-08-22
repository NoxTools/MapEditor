using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Threading;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;

namespace NoxShared
{
	public class NoxMemoryHack
	{
		public static ProcessMemory Process = new ProcessMemory("GAME");
		//public static ProcessMemory Process = ProcessMemory.OpenProcessByWindowName("Nox Game Window", "Nox");
		public static readonly NoxMemoryHack Instance = new NoxMemoryHack();
		
		public TeamMemory Teams = new TeamMemory();
		public PlayerMemory Players = new PlayerMemory();

		private ArrayList threads = new ArrayList();
		private NoxMemoryHack()
		{
			threads.Add(new Thread(new ThreadStart(RefreshThreadStart)));
			foreach (Thread thread in threads)
				thread.Start();

			//TODO: make this optional
			Fixes.ApplyFixes();
		}

		~NoxMemoryHack()
		{
			foreach (Thread thread in threads)
				thread.Abort(); 
		}

		protected void RefreshThreadStart()
		{
			while (true)
			{
				Teams.Read();
				Players.Read();

				Thread.Sleep(100);
			}
		}

		//TODO: use values from xml file for addresses
		public class PlayerMemory
		{
			protected internal const int PLAYER_TABLE_ADDRESS = 0x62F9A4;
			protected internal const int MAX_PLAYERS = 32;
			protected internal const int PLAYER_ENTRY_LENGTH = 0x12DC;
			protected internal const int PLAYER_TABLE_LENGTH = MAX_PLAYERS * PLAYER_ENTRY_LENGTH;

			public ArrayList PlayerList = new ArrayList();

			public class PlayerEventArgs : EventArgs
			{
				public Player Player;
				public PlayerEventArgs(Player player)
				{
					Player = player;
				}
			}
			public delegate void PlayerEvent(object sender, PlayerEventArgs e);
			
			public event PlayerEvent PlayerJoined;
			public event PlayerEvent PlayerLeft;

			public void Read()
			{
				ArrayList oldList = PlayerList;
				PlayerList = new ArrayList();

				if (NoxMemoryHack.Process.Available)
				{
					BinaryReader rdr = new BinaryReader(new MemoryStream(NoxMemoryHack.Process.Read(PLAYER_TABLE_ADDRESS, PLAYER_TABLE_LENGTH)));

					for (int player = 0; player < MAX_PLAYERS; player++)
						PlayerList.Add(Player.ReadFromMemory(rdr.BaseStream));
				}

				foreach (Player player in oldList)
					if (!PlayerList.Contains(player) && player.Connected)
						if (PlayerLeft != null) PlayerLeft(this, new PlayerEventArgs((Player) player.Clone()));//FIXME: clone necessary? (tryin to fix crashes)

				foreach (Player player in PlayerList)
					if (!oldList.Contains(player) && player.Connected)
						if (PlayerJoined != null) PlayerJoined(this, new PlayerEventArgs((Player) player.Clone()));//FIXME: clone necessary? (tryin to fix crashes)
			}
		}

		public class TeamMemory
		{
			public ArrayList TeamList = new ArrayList();

			public class TeamEventArgs : EventArgs
			{
				public Team Team;
				public TeamEventArgs(Team team)
				{
					Team = team;
				}
			}
			public delegate void TeamEvent(object sender, TeamEventArgs e);
			public event TeamEvent TeamChanged;
			public delegate void Event(object sender, EventArgs e);
			public event Event Refreshed;

			public int TeamCount
			{
				get
				{
					int count = 0;
					foreach (Team team in TeamList)
						if (team.Enabled)
							count++;
					return count;
				}
			}

			public enum TeamFlags
			{
				TEAM_DAMAGE = 0x01,
				AUTO_ASSIGN = 0x02,
				USE_TEAMS = 0x04
			}
			private int flags;

			public bool UseTeams
			{
				get {return (flags & (int) TeamFlags.USE_TEAMS) != 0;}
				set {SetTeamFlagsBit(TeamFlags.USE_TEAMS, value);}
			}

			public bool AutoAssign
			{
				get {return (flags & (int) TeamFlags.AUTO_ASSIGN) != 0;}
				set {SetTeamFlagsBit(TeamFlags.AUTO_ASSIGN, value);}
			}

			public bool TeamDamage
			{
				get {return (flags & (int) TeamFlags.TEAM_DAMAGE) != 0;}
				set {SetTeamFlagsBit(TeamFlags.TEAM_DAMAGE, value);}
			}

			private void SetTeamFlagsBit(TeamFlags bit, bool enable)
			{
				if (enable)
					flags |= (int) bit;
				else
					flags &= ~((int) bit);
			}

			private const int MAX_TEAMS = 0x10;
			private const int TEAM_TABLE_ADDRESS = 0x654D5C;
			private const int TEAM_HEADER_LENGTH = 0x5C;
			private const int TEAM_ENTRY_LENGTH = 0x50;
			private const int TEAM_NAME_LENGTH = 0x2C;
			private const int TEAM_TABLE_LENGTH = TEAM_HEADER_LENGTH + MAX_TEAMS * TEAM_ENTRY_LENGTH;

			public void Read()
			{
				lock (NoxMemoryHack.Process)
				{
					ArrayList old = TeamList;
					TeamList =  new ArrayList();

					BinaryReader rdr = new BinaryReader(new MemoryStream(NoxMemoryHack.Process.Available ? NoxMemoryHack.Process.Read(TEAM_TABLE_ADDRESS, TEAM_TABLE_LENGTH) : new byte[TEAM_TABLE_LENGTH]));

					int numTeams = rdr.ReadInt32();
					flags = rdr.ReadInt32();

					rdr.BaseStream.Seek(TEAM_HEADER_LENGTH, SeekOrigin.Begin);//skip to end of header

					//read the teams
					for (int ndx = 0; ndx < MAX_TEAMS; ndx++)
					{
						TeamList.Add(Team.FromBytes(rdr.ReadBytes(TEAM_ENTRY_LENGTH)));
						((Team) TeamList[ndx]).TeamNumber = ndx + 1;//TODO? find a better way of keeping track of the team number/index
						if (ndx >= old.Count || !TeamList[ndx].Equals(old[ndx]))
							if (TeamChanged != null) TeamChanged(this, new TeamEventArgs((Team) TeamList[ndx]));
					}
				}

				if (Refreshed != null) Refreshed(this, new EventArgs());
			}

			public void Write()
			{
				if (NoxMemoryHack.Process.Available)
					lock (NoxMemoryHack.Process)
					{
						BinaryWriter wtr = new BinaryWriter(new MemoryStream(NoxMemoryHack.Process.Read(TEAM_TABLE_ADDRESS, TEAM_TABLE_LENGTH)));

						wtr.Write((int) TeamCount);
						wtr.Write((int) flags);

						wtr.BaseStream.Seek(TEAM_HEADER_LENGTH, SeekOrigin.Begin);//skip to end of header

						//write the teams
						foreach (Team team in TeamList)
							team.Write(wtr.BaseStream);

						NoxMemoryHack.Process.Write(TEAM_TABLE_ADDRESS, ((MemoryStream) wtr.BaseStream).ToArray());
					}
			}

			public void AddTeam()
			{
				TeamList.Add(new Team(TeamList.Count + 1));
			}

			public class Team
			{
				public string Name;
				public int TeamNumber;
				public int MemberCount;
				public Color Color;
				public bool Enabled;

				private int unknownAddress;

				public static Color[] TeamColor = {
													  Color.LightGray,//FIXME: no team? may be white also
													  Color.Red,
													  Color.Blue,
													  Color.Green,
													  Color.DarkBlue,
													  Color.Yellow,
													  Color.DarkRed,
													  Color.Black,
													  Color.White,
													  Color.Orange
												  };
				
				public Team(int teamNumber) : this()
				{
					TeamNumber = teamNumber;
					Color = TeamColor[teamNumber - 1];
				}

				protected Team()
				{
					//default values in case not all details are filled in
					Name = "Unnamed Team";
					MemberCount = 0;
					Enabled = true;
					Color = TeamColor[0];
				}

				public static Team FromBytes(byte[] data)
				{
					Team team = new Team();
					BinaryReader rdr = new BinaryReader(new MemoryStream(data));
					team.Name = Encoding.Unicode.GetString(rdr.ReadBytes(TEAM_NAME_LENGTH), 0, TEAM_NAME_LENGTH).Split('\0')[0];
					
					team.unknownAddress = rdr.ReadInt32();//some kind of address
					team.MemberCount = rdr.ReadInt32();
					rdr.ReadBytes(4);//always null?

					team.Color = TeamColor[Math.Max(0, rdr.ReadByte() - 1)];
					rdr.ReadByte();//UNKOWN -- doesnt seem to do anything but tends to match teamnumber byte and is zeroed out when team is disabled
					team.TeamNumber = rdr.ReadByte();//team number is the index in our array, so this will be ignored and overwritten

					rdr.ReadBytes(5);//null bytes
					team.Enabled = rdr.ReadBoolean();
					//rdr.ReadBytes(7);//last null bytes are meaningless?

					return team;
				}

				public void Write(Stream stream)
				{
					BinaryWriter wtr = new BinaryWriter(stream);

					wtr.Write(Encoding.Unicode.GetBytes(Name));
					wtr.Write(new byte[TEAM_NAME_LENGTH - Encoding.Unicode.GetByteCount(Name)]);//write the rest of the space used for the name with nulls
					wtr.BaseStream.Seek(4, SeekOrigin.Current);
					wtr.Write((int) MemberCount);
					wtr.BaseStream.Seek(4, SeekOrigin.Current);
					wtr.Write((byte) (new ArrayList(TeamColor).IndexOf(Color) + 1));//assumes that color is valid and will be found in list
					//wtr.BaseStream.Seek(1, SeekOrigin.Current);//cant be zero!
					wtr.Write((byte) TeamNumber);//dont know what this is, but it should not be 0, otherwise, teams after will not show
					wtr.Write((byte) TeamNumber);
					wtr.BaseStream.Seek(5, SeekOrigin.Current);
					wtr.Write((bool) Enabled);
					wtr.BaseStream.Seek(15, SeekOrigin.Current);//advance to end of this entry
				}

				public override bool Equals(object obj)
				{
					Team rhs = obj as Team;
					return rhs != null && TeamNumber == rhs.TeamNumber && Name == rhs.Name && Color == rhs.Color;
				}

				public override int GetHashCode() {return base.GetHashCode ();}
			}
		}

		public enum ConsoleColor
		{
			BLACK = 0x00,
			DARK_GRAY = 0x01,
			GRAY = 0x02,
			LIGHT_GRAY = 0x03,
			WHITE = 0x04,
			DARK_RED = 0x05,
			RED = 0x06,
			PINK = 0x07,
			DARK_GREEN = 0x08,
			GREEN = 0x09,
			LIGHT_GREEN = 0x0A,
			DARK_BLUE = 0x0B,
			BLUE = 0x0C,
			LIGHT_BLUE = 0x0D,
			DARK_YELLOW = 0x0E,
			YELLOW = 0x0F,
			LIGHT_YELLOW = 0x10
			//anything higher goes back to GRAY
		}

		static readonly IntPtr FUNC_PRINT_TO_CONSOLE = (IntPtr) 0x450B90;
		static readonly IntPtr FUNC_KICK_PLAYER = (IntPtr) 0x4DEAB0;
		static readonly IntPtr FUNC_ADD_TO_BAN_LIST = (IntPtr) 0x416770;
		static readonly IntPtr FUNC_CONSOLE_COMMAND = (IntPtr) 0x443C80;

		public static void PrintToConsole(string text, ConsoleColor color)
		{
			Process.CallFunction(FUNC_PRINT_TO_CONSOLE, (byte) color, text);
		}

		/*/doesnt work (useless anyway)
		static readonly IntPtr FUNC_PRINT_TO_SCREEN = (IntPtr) 0x565CA3;
		public static void PrintToScreen(string text1, string text2)
		{
			Process.CallFunction(FUNC_PRINT_TO_SCREEN, text1, text2);
		}
		//*/
		
		//TODO: find out where function is that says "<name> was kicked" -- this happens when the name is typed into the console,
		// but not when a player is kicked from the admin menu
		public static void KickPlayer(int playerId)
		{
			if (playerId >= 31) return;//can't do host player
			Process.CallFunction(FUNC_KICK_PLAYER, playerId, 4);//4 is needed to send "You have been kicked" message
		}

		public static void BanPlayer(int playerId)
		{
			if (playerId >= 31) return;//can't do host player
			Player player = (Player) Instance.Players.PlayerList[playerId];
			KickPlayer(player.Number);
			Process.CallFunction(FUNC_ADD_TO_BAN_LIST, 0, player.Login, player.Serial.ToCharArray());
		}

		public static void ConsoleCommand(string commandLine)
		{
			Process.CallFunction(FUNC_CONSOLE_COMMAND, commandLine);
		}

		public class Fixes
		{
			public static bool ScreenshotNumbering;// = true;
			public static void ApplyFixes()
			{
				//FIXME/TODO: rename the file to the current date instead of "nox", prefix string is at 0x6dcbc8
				if (ScreenshotNumbering)
				{
					Microsoft.Win32.RegistryKey noxReg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Westwood\\Nox");
					if (noxReg != null)
					{
						string[] files = Directory.GetFiles(((string) noxReg.GetValue("InstallPath")).Replace("Nox.EXE", ""), "nox*.bmp");
						Regex exp = new Regex(".*nox(?<num>[0-9]+)\\..*");
						int highest = 0;
						foreach (string file in files)
						{
							int num = Convert.ToInt32(exp.Match(file).Groups["num"].Value);
							if (num > highest)
								highest = num;
						}
						//Process.Write(0x6DCBC4, );
					}
				}
			}

			public static void FixUnkickable(int playerId)
			{
				if (playerId <= 31)
					Process.Write(PlayerMemory.PLAYER_TABLE_ADDRESS + PlayerMemory.PLAYER_ENTRY_LENGTH * playerId + 0xED, new byte[2]);
			}
		}
	}
}