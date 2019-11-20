using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using UnityEngine;

namespace Utils {
    public static class DataHandler {
        
        private static readonly string _directoryPath = Application.streamingAssetsPath;

        private static string[] FilesInDirectory(string directory) {
            return Directory.GetFiles(directory);
        }
        
        public static List<TankData> GetTankData() {
            return FilesInDirectory(_directoryPath).Select(Load).ToList();
        }

        public static async Task RefreshTankData(List<TankData> tankData) {
            foreach (TankData tank in tankData) {
                await Save(tank);
            }
        }

        public static TankData Load(string fileName) {
            using (StreamReader sr = new StreamReader(fileName)) {
                return JsonUtility.FromJson<TankData>(sr.ReadToEnd());
            }
        }

        public static async Task Save(TankData tankData) {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(_directoryPath, tankData.PlayerName + ".tank"))) {
                await outputFile.WriteAsync(JsonUtility.ToJson(tankData));
            }
        }
        
    }
}