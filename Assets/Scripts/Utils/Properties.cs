namespace Utils {
    public static class Properties {

        public struct Inputs {
            public const string Horizontal = "Horizontal1";
            public const string Vertical = "Vertical1";
            public const string Rotation = "Rotation";
            public const string ScrollWheel = "MouseScrollWheel";
            public const string MouseX = "MouseX";
            public const string MouseY = "MouseY";
        }

        public struct Scenes {
            public const string Shared = "Shared";
            public const string Menu = "Menu";
            public const string Game = "Game";
        }

        public struct Parameters {
            public const string GameType = "GameType";
        }

        public struct GameTypes {
            public const string TeamFight = "TeamFight";
            public const string Tournament = "Tournament";
        }

        public struct PlayerPrefs {
            public const string ExplosionDamage = "ExplosionDamage";
            public const string ExplosionCreateBustedTank = "ExplosionCreateBustedTank";
            public const string CanonDamage = "CanonDamage";
            public const string CanonPower = "CanonPower";
            public const string TurretSpeed = "TurretSpeed";
            public const string HealthPoints = "MaxHp";
            public const string ReloadTime = "ReloadTime";
            public const string WaypointSeekRadius = "WaypointSeekRadius";
            public const string AlwaysPickBestChoice = "AlwaysPickBestChoice";
            public const string SecondsBetweenRefresh = "TimeBetweenRefresh";
        }
        
        public struct PlayerPrefsDefault {
            public const int ExplosionDamage = 1;
            public const bool ExplosionCreateBustedTank = true;
            public const int CanonDamage = 1;
            public const int CanonPower = 50;
            public const int TurretSpeed = 10;
            public const int HealthPoints = 5;
            public const int ReloadTime = 5;
            public const int WaypointSeekRadius = 15;
            public const bool AlwaysPickBestChoice = true;
            public const int SecondsBetweenRefresh = 1;
        }
        
    }
}