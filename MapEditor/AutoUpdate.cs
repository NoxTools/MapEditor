using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Windows.Forms;

/// <summary>
/// Clase para actualización de aplicaciones a través del web
/// </summary>
/// <author>Eduardo Oliveira</author>
/// <Modified_by>Danny (dbembibre@virtualdesk.es)</Modified_by>
/// <history Danny>
/// Changed: C# translated and retouched
/// Changed: Added method IsUpdatable() to verify if exist a new update
/// Changed: Now you can update any file without neccesity of exe file
/// Changed: If anything was wrong, you have a collection to rollback all files to original state
/// Changed: The ? operator not is available

///</history Danny>
namespace MapEditor
{
    public class AutoUpdate
    {
        #region Variables
        private static string m_UpdateFileName = "Update.txt";
        private static string m_ErrorMessage = "Ocurrió un problema mientras se intentaba Actualizar.";
        #endregion

        #region Propiedades

        public static string UpdateFileName
        {
            get
            {
                return m_UpdateFileName;
            }
            set
            {
                m_UpdateFileName = value;
            }
        }

        public static string ErrorMessage
        {
            get
            {
                return m_ErrorMessage;
            }
            set
            {
                m_ErrorMessage = value;
            }
        }
        #endregion

        #region Comprobar si hay una actualizacion
        public static bool IsUpdatable(string RemotePath,string localpath)
        {
            if (RemotePath == string.Empty)
                return false;

            string AssemblyName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            string RemoteUri = RemotePath + /*AssemblyName +*/ "/";
            WebClient MyWebClient = new WebClient();
            bool isDLL = false;

            try
            {
                string Contents = MyWebClient.DownloadString(RemoteUri + UpdateFileName);
                //Contents = Contents.Replace("\n", "");
                string[] FileList = Contents.Split(Convert.ToChar("\n"));

                Contents = string.Empty;

                foreach (string F in FileList)
                {
                    string Aux = string.Empty;
                    if ((F.IndexOf("\'") + 1 != 0))
                    {
                        Aux = F.Substring(0, ((F.IndexOf("\'") + 1) - 1));
                    }
                    if (Aux.Trim() != string.Empty)
                    {
                        if (!string.IsNullOrEmpty(Contents))
                            Contents = Contents + (char)(Keys.Return);
                        Contents = Contents + Aux.Trim();
                    }
                }

                    FileList = Contents.Split((char)(Keys.Return));
  
                string[] Info = FileList[0].Split(Convert.ToChar(";"));
               
                //Version Version1, Version2;
                //FileVersionInfo fv = null;
                DateTime date;
                DateTime date2;

                //if (!ignoreVersion)
               // {
                 //  date = DateTime.Parse(Info[1].Trim());
                  // date2 = File.GetCreationTime(Application.StartupPath + "\\" + localpath + Info[0].Trim());
                   //isToUpgrade = Convert.ToBoolean(date > date2);
                //}

                foreach (string sFile in FileList)
                {
                    isDLL = false;
                    Info = sFile.Split(Convert.ToChar(";"));
                    bool isToUpgrade = false;
                    string FileName;
                    if (!Directory.Exists(Application.StartupPath + "\\" + localpath))
                    {
                        Directory.CreateDirectory(Application.StartupPath + "\\" + localpath);
                    }
                    FileName = Application.StartupPath + "\\" + localpath + Info[0].Trim();
                    bool FileExists = File.Exists(FileName);

                    //if ((Info[1].Trim() == "delete"))
                    //{
                   //     if (FileExists)
                    //        return true;
                    //}
                    /*else*/ if (FileExists)
                    {
                        //if ()
                       //{
                            isDLL = Convert.ToBoolean(Info[2]);
                        //}
                        //  Verificar la version de los archivos web y local
                       // if (!ignoreVersion && !isDLL)
                        //{
                          //  fv = FileVersionInfo.GetVersionInfo(FileName);
                            //Version1 = new Version(Info[1].Trim());
                            //Version2 = new Version(fv.FileVersion);
                            //isToUpgrade = Convert.ToBoolean(Version1 > Version2);
                        //}
                        //else
                        //{
                            date = DateTime.Parse(Info[1].Trim(), CultureInfo.InvariantCulture);
                            date2 = File.GetCreationTime(Application.StartupPath + "\\" + localpath + Info[0].Trim());
                            isToUpgrade = Convert.ToBoolean(date > date2);
                        //}
                    }
                    else
                    {
                        isToUpgrade = true;
                       // return isToUpgrade;
                    }

                    if (isToUpgrade)
                        return true;
                }

                return false;


            }
            catch { return false; }
        }
        #endregion

        #region Actualizar Archivos
        public static bool UpdateFiles(string RemotePath, string localpath)
        {
            if (RemotePath == string.Empty)
                return false;

            /*if (!Directory.Exists(Application.StartupPath + "\\" + localpath))
            {
                //MessageBox.Show("YO recreating the directory");
                Directory.CreateDirectory(Application.StartupPath + "\\" + localpath);
            }*/
            //System.Collections.Generic.List<AutoUpdateRollback> mList = new System.Collections.Generic.List<AutoUpdateRollback>();

            string AssemblyName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            //string ToDeleteExtension = ".ToDelete";
            string RemoteUri = RemotePath /*+ AssemblyName*/ + "/";
            WebClient MyWebClient = new WebClient();
            bool isDLL = false;

            try
            {
                //  Borramos primero la aplicación antes de actualizar

                //string[] Archivos = System.IO.Directory.GetFiles(Application.StartupPath, "*");

                //foreach (string Archivo in Archivos)
                 //   File.Delete(Application.StartupPath + "\\" + Archivo);

                //  Obtenemos el archivo de acutalización remoto
                string Contents = MyWebClient.DownloadString(RemoteUri + UpdateFileName);

                //Contents = Contents.Replace("\n", "");
                string[] FileList = Contents.Split(Convert.ToChar("\n"));

                Contents = string.Empty;

                foreach (string F in FileList)
                {
                    string Aux = string.Empty;
                    if ((F.IndexOf("\'") + 1 != 0))
                    {
                        Aux = F.Substring(0, ((F.IndexOf("\'") + 1) - 1));
                    }
                    if (Aux.Trim() != string.Empty)
                    {
                        if (!string.IsNullOrEmpty(Contents))
                            Contents = Contents + (char)(Keys.Return);
                        Contents = Contents + Aux.Trim();
                    }
                }

                //Rehacer la lista de archivos

                FileList = Contents.Split((char)(Keys.Return));
                string[] Info = FileList[0].Split(Convert.ToChar(";"));

                DateTime date;
                DateTime date2;
                foreach (string sFile in FileList)
                {
                    isDLL = false;
                    Info = sFile.Split(Convert.ToChar(";"));

                    string FileName;
                        FileName = Application.StartupPath + "\\" + localpath + Info[0].Trim();

                    bool isToDelete = false; bool isToUpgrade = false;
                    //string FileName = Application.StartupPath + "\\" + Info[0].Trim();
                    string TempFileName = Application.StartupPath + "\\" + "Temp.fil";
                    bool FileExists = File.Exists(FileName);

                    if (FileExists)
                    {
                            isDLL = Convert.ToBoolean(Info[2]);
                            date = DateTime.Parse(Info[1].Trim(), CultureInfo.InvariantCulture);
                            date2 = File.GetCreationTime(Application.StartupPath + "\\" + localpath + Info[0].Trim());
                            isToUpgrade = Convert.ToBoolean(date > date2);
                            isToDelete = isToUpgrade;
                    }
                    else
                    {
                        isToUpgrade = true;
                    }
                    //MessageBox.Show("TempFileName: " + TempFileName + "\r" + "FileName: " + FileName + "\r");
                    if (isToUpgrade)
                        MyWebClient.DownloadFile(RemoteUri + Info[0], TempFileName);
                    //  Renombrar el archivo para eliminar en el futuro
                    if (!Directory.Exists(Application.StartupPath + "\\updated"))
                    {
                        Directory.CreateDirectory(Application.StartupPath + "\\updated");
                    }
                    if (isToDelete && (isDLL))
                        File.Move(FileName, Application.StartupPath + "\\updated\\" + Info[0] + ".tst");
                    else if(isToDelete)
                    {
                        File.Delete(FileName);
                    }
                    //  Renombrar el archivo temporal al nombre de archivo real
                    if (isToUpgrade)
                    {
                            date = DateTime.Parse(Info[1].Trim(), CultureInfo.InvariantCulture);
                            File.Move(TempFileName, FileName);
                            File.SetCreationTime(FileName, date);
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(m_ErrorMessage + "\r" + ex.Message + "\r" + "Assembly: " + AssemblyName);
                return false;
            }
            finally
            {
                if (MyWebClient != null)
                    MyWebClient.Dispose();
            }
        }
        #endregion

        #region Util
        private static string GetVersion(string Version)
        {
            string[] x = Version.Split(Convert.ToChar("."));
            return string.Format("{0:00000}{1:00000}{2:00000}", Convert.ToInt16(x[0]), Convert.ToInt16(x[1]), Convert.ToInt16(x[2]));
        }
        #endregion
    }

    public class AutoUpdateRollback
    {
        #region Variables y Propiedades
        private string _Renamed, _Original, _Operacion;

        public string Operacion
        {
            get { return _Operacion; }
            set { _Operacion = value; }
        }

        public string Original
        {
            get { return _Original; }
            set { _Original = value; }
        }

        public string Renamed
        {
            get { return _Renamed; }
            set { _Renamed = value; }
        }
        #endregion

        #region Constructor
        public AutoUpdateRollback(string Original, string Renombrado, string Operacion)
        {
            this._Original = Original;
            this._Renamed = Renombrado;
            this._Operacion = Operacion;
        }
        #endregion
    }

}
