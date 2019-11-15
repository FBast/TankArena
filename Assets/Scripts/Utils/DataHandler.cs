using System.IO;
using UnityEngine;

namespace Utils {
    public class DataHandler {

        public string[] TankDirectories => Directory.GetDirectories(Application.streamingAssetsPath);

        public string[] FilesInDirectory(string directory) {
            return Directory.GetFiles(directory);
        }

    }
}