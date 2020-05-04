using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Plugins.ReflexityAI.Framework;
using UnityEngine;
using Utils;

namespace Framework {
    public static class DataHandler {

        private static string[] TankDirectories => Directory.GetDirectories(_directoryPath);
        private static readonly string _directoryPath = Application.streamingAssetsPath;

        private static string[] FilesInDirectory(string directory) {
            return Directory.GetFiles(directory);
        }
        
        public static List<TankSetting> GetTankData() {
            return TankDirectories.Select(Load).ToList();
        }

        public static async Task RefreshTankData(List<TankSetting> tankDatas) {
            foreach (TankSetting tankData in tankDatas) {
                await Save(tankData);
            }
        }

        public static TankSetting Load(string tankDirectory) {
            TankSetting tankSetting = ScriptableObject.CreateInstance<TankSetting>();
            List<AIBrainGraph> aiBrainGraphs = new List<AIBrainGraph>();
            foreach (string file in FilesInDirectory(tankDirectory)) {
                switch (Path.GetExtension(file)) {
                    case ".set": {
                        using (StreamReader sr = new StreamReader(file)) {
                            tankSetting = (TankSetting) FullSerializerApi.Deserialize(typeof(TankSetting), sr.ReadToEnd());
                        }
                        break;
                    }
                    case ".bra": {
                        using (StreamReader sr = new StreamReader(file)) {
                            AIBrainGraph aiBrainGraph = (AIBrainGraph) FullSerializerApi.Deserialize(typeof(AIBrainGraph), sr.ReadToEnd());
                            aiBrainGraphs.Add(aiBrainGraph);
                        }
                        break;
                    }
                }
            }
            tankSetting.Brains = aiBrainGraphs;
            return tankSetting;
        }

        public static async Task Save(TankSetting tankSetting) {
            string tankDirectory = _directoryPath + "/" + tankSetting.PlayerName + "_" + tankSetting.TankName;
            if (Directory.Exists(tankDirectory)) Directory.Delete(tankDirectory, true);
            Directory.CreateDirectory(tankDirectory);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(tankDirectory, tankSetting.name + ".set"))) {
                await outputFile.WriteAsync(FullSerializerApi.Serialize(typeof(TankSetting), tankSetting));
            }
            foreach (AIBrainGraph aiBrainGraph in tankSetting.Brains) {
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(tankDirectory, aiBrainGraph.name + ".bra"))) {
                    await outputFile.WriteAsync(FullSerializerApi.Serialize(typeof(AIBrainGraph), aiBrainGraph));
                }
            }
            Debug.Log("Tank settings saved in " + tankDirectory);
        }
        
    }
}