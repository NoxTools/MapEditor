using System;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;

namespace NoxShared
{
	public class NoxDb
	{
		/// <summary>
		/// Nox installation directory
		/// </summary>
		public static string NoxPath;
		
		/// <summary>
		/// Must be pointing to a valid file in order for GetStream to work
		/// </summary>
		protected static string dbFile;
		
		protected static FileStream GetStream() { return File.OpenRead(NoxPath + dbFile); }
		
		const string REGISTRY_PATH = "SOFTWARE\\Westwood\\Nox";
		
		static readonly RegistryKey installPathKey;
		static readonly string directorySeparator = Path.DirectorySeparatorChar.ToString();
		
		/// <summary>
		/// Used for validating Nox installation path
		/// </summary>
		static bool CheckThingDb()
		{
           
            return File.Exists(NoxPath + directorySeparator + "video.bag");

		}
		
		/// <summary>
		/// Requests user to select Nox installation directory by hand.
		/// </summary>
		static void FindNoxPath()
		{
			var fbd = new FolderBrowserDialog();
			fbd.Description = "Please select your Nox install directory.";
			if (fbd.ShowDialog() == DialogResult.OK)
			{
				NoxPath = fbd.SelectedPath;
				// Look up for thing.db //thing.db???? why not video.bag?
				if (CheckThingDb()) return;
			}
			
			// Failure (fallthrough)
          
			MessageBox.Show("Failed to find or access Nox installation directory.\nEditor requires some important game resources in order to start properly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			Environment.Exit(0);
		}
		
		static NoxDb()
		{
			NoxPath = Settings.Default.NoxInstallDir;
			// Check for Nox installation path
			if (!CheckThingDb())
			{
				// Try to use path from registry, request user if failed
				installPathKey = Registry.LocalMachine.OpenSubKey(REGISTRY_PATH);
			
				if (installPathKey == null) FindNoxPath();
				else
				{
					object val = installPathKey.GetValue("InstallPath");
					if (val == null) FindNoxPath();
            		else NoxPath = (string) val;
                    
				}
              
            	// Get directory part if it's a path to file
            	if (Path.HasExtension(NoxPath))
					NoxPath = Path.GetDirectoryName(NoxPath);
            	
            	if (!NoxPath.EndsWith(directorySeparator, StringComparison.InvariantCultureIgnoreCase)) NoxPath += directorySeparator;
				// Save path for later
				Settings.Default.NoxInstallDir = NoxPath;
				Settings.Default.Save();
			}
		}
	}
}
