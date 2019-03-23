using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;








namespace Game1Test.Code
{
    public enum GameState { Menu, InGame, Quit };
    public enum MenuState { MainMenu, PlayMenu, MissionMenu, OptionMenu}
    //public enum GameType { Normal, TimeAttack, ScoreAttack, Mission };
    public enum InGameState { Going, Paused, Finished };
    public enum PauseReason { Menu, Upgrade}
    //public enum VictoryState { Mission, Everykill };
    //public enum DefeatState { Notime, Destroyed, Noscore };
    //public enum Pause { going, paused }
    //public enum AllowGenerate { Yes, No };
    //public enum AllowGenerate { No, Yes };
    public enum Loading { False, True }
    //public enum DrawLoading { False, True };

    static class GameData
    {
        public static GameState gameState = new GameState();
        public static MenuState menuState = new MenuState();
        //public static GameType gameType = new GameType();
        public static InGameState inGameState = new InGameState();
        public static PauseReason pauseReason = new PauseReason();
        //public static VictoryState victoryState = new VictoryState();
        //public static DefeatState defeatState = new DefeatState();
        //public static Pause pause = new Pause();
        //public static AllowGenerate allowGenerate = new AllowGenerate();
        public static Loading loading = new Loading();
        //public static DrawLoading drawLoading = new DrawLoading();
    }
}