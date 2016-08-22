/*
 * NoxShared
 * Пользователь: AngryKirC
 * Дата: 01.07.2015
 */
using System;
using System.IO;
using System.Text;
using System.Security.AccessControl;

namespace NoxShared
{
	/// <summary>
	/// Class that provides information logging utility.
	/// </summary>
	public static class Logger
	{
		/// <summary>
		/// logfile default filename
		/// </summary>
		const string LOG_FILE_NAME = "Latest.log";
		
		/// <summary>
		/// if true, output has been initialized successfully
		/// </summary>
		static bool Initialized = false;
		
		/// <summary>
		/// logfile writer
		/// </summary>
		static StreamWriter Output = null;
		
		/// <summary>
		/// Writes a new text entry (along with current time) to the logfile.
		/// </summary>
		public static void Log(string str)
		{
			if (!Initialized) return;
			
			string time = DateTime.UtcNow.ToShortTimeString();
			try
			{
				Output.Write("[{0}] {1}", time, str);
				Output.WriteLine();
			}
			catch (Exception)
			{
				// Weird stuff
				Initialized = false;
			}
		}
		
		/// <summary>
		/// Attempts to create logfile and setup output stream.
		/// </summary>
		public static void Init()
		{
			if (Initialized) return;
			
			try
			{
				// Create new file (replacing existing one)
				Output = File.CreateText(Path.Combine(Environment.CurrentDirectory, LOG_FILE_NAME));
				
				// Configure output stream
				//Output.Encoding = LOG_TEXT_ENCODING;
				Output.AutoFlush = true;
				
				// Output initialized
				Initialized = true;
				
				// Write start string
				Log(String.Format("Logfile opened {0}", DateTime.UtcNow.ToLongDateString()));
			}
			catch (Exception)
			{
				// most probably user does not have write access to working directory
				// for now this is left unhandled
			}
		}
		
		public static void Close()
		{
			if (Initialized)
			{
				Output.Close();
				Initialized = false;
			}
		}
	}
}
