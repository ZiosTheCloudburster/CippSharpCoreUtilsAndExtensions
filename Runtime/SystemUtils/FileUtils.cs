using System;
using System.Diagnostics;
using System.IO;

namespace CippSharp.Core.Utils
{
    using Debug = UnityEngine.Debug;
    using Object = UnityEngine.Object;
    
    public static class FileUtils
    {
        private static readonly string LogName = $"[{nameof(FileUtils)}]: ";
        
        #region → Directories

        #region → EnsureExistsFolder
        
        /// <summary>
        /// Tries to ensure the existence of a folder
        /// </summary>
        /// <param name="directoryPath">full path of the directory</param>
        public static bool EnsureExistsFolder(string directoryPath)
        {
            return EnsureExistsDirectory(directoryPath, null);
        }

        /// <summary>
        /// Tries to ensure the existence of a folder
        /// </summary>
        /// <param name="directoryPath">full path of the directory</param>
        /// <param name="debug"></param>
        public static bool EnsureExistsDirectory(string directoryPath, Object debug = null)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                
                return true;
            }
            catch (Exception e)
            {
                string logName = debug != null ? StringUtils.LogName(debug) : LogName;
                Debug.LogError(logName+$"{nameof(EnsureExistsDirectory)} failed. Caught exception: {e.Message}.", debug);
                return false;
            }
        }
        
        #endregion
        
        /// <summary>
        /// Try to open folder
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="debug"></param>
        public static bool TryOpenFolder(string fullPath, Object debug = null)
        {
            try
            {
                Process.Start(@Path.GetDirectoryName(fullPath));
                return true;
            }
            catch (Exception e)
            {
                string logName = debug != null ? StringUtils.LogName(debug) : LogName;
                Debug.LogError(logName + $"{nameof(TryOpenFolder)} failed. Caught exception: {e.Message}.", debug);
                return false;
            }
        }
        
        #endregion

        #region → Files
        
        /// <summary>
        /// Try to delete a file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="debug"></param>
        public static bool TryDelete(string path, Object debug = null)
        {
            try
            {
                File.Delete(path);
                return true;
            }
            catch (Exception e)
            {
                string logName = debug != null ? StringUtils.LogName(debug) : LogName;
                Debug.LogError(logName + $"{nameof(TryDelete)} failed. Caught exception: {e.Message}.", debug);
                return false;
            }
        }
        
        /// <summary>
        /// Try to move a file. It also work for file rename.
        /// </summary>
        /// <param name="oldPath"></param>
        /// <param name="newPath"></param>
        /// <param name="debug"></param>
        public static bool TryMove(string oldPath, string newPath, Object debug = null)
        {
            try
            {
                File.Move(oldPath, newPath);
                return true;
            }
            catch (Exception e)
            {
                string logName = debug != null ? StringUtils.LogName(debug) : LogName;
                Debug.LogError(logName+$"{nameof(TryMove)} failed. Caught exception: {e.Message}.", debug);
                return false;
            }
        }
        
        #endregion
    }
}
