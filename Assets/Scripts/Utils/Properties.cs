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
            public static string TeamASettings = "TeamASettings";
            public static string TeamBSettings = "TeamBSettings";
            public static string TeamCSettings = "TeamCSettings";
            public static string TeamDSettings = "TeamDSettings";
            public static string FFASettings = "FFASettings";
        }

        public struct GameTypes {
            public static string TeamFight = "TF";
            public static string FreeForAll = "FFA";
            public static string Tournament = "Tournament";
        }
        
    }
}