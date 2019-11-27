namespace Utils {
    public static class Properties {

        public struct Inputs {
            public static string Horizontal = "Horizontal1";
            public static string Vertical = "Vertical1";
            public static string Rotation = "Rotation";
            public static string ScrollWheel = "MouseScrollWheel";
            public static string MouseX = "MouseX";
            public static string MouseY = "MouseY";
        }

        public struct Scenes {
            public static string Shared = "Shared";
            public static string Menu = "Menu";
            public static string Game = "Game";
        }

        public struct Parameters {
            public static string GameType = "GameType";
        }

        public struct GameTypes {
            public static string TeamFight = "TeamFight";
            public static string Tournament = "Tournament";
        }

        public struct PlayerPrefs {
            public static string ExplosionDamage = "ExplosionDamage";
            public static string ExplosionCreateBustedTank = "ExplosionCreateBustedTank";
            public static string CanonDamage = "CanonDamage";
            public static string CanonPower = "CanonPower";
            public static string TurretSpeed = "TurretSpeed";
            public static string HealthPoints = "MaxHp";
            public static string ReloadTime = "ReloadTime";
            public static string WaypointSeekRadius = "WaypointSeekRadius";
            public static string AlwaysPickBestChoice = "AlwaysPickBestChoice";
            public static string SecondsBetweenRefresh = "TimeBetweenRefresh";
        }
        
        public struct PlayerPrefsDefault {
            public static int ExplosionDamage = 1;
            public static bool ExplosionCreateBustedTank = true;
            public static int CanonDamage = 1;
            public static int CanonPower = 50;
            public static int TurretSpeed = 10;
            public static int HealthPoints = 5;
            public static int ReloadTime = 5;
            public static int WaypointSeekRadius = 15;
            public static bool AlwaysPickBestChoice = true;
            public static int SecondsBetweenRefresh = 1;
        }
        
    }
}