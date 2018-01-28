using UnityEngine;
using System.Collections;

//Tags are usefull so that we dont have to hard code things

public class Tags {

	//Tags
	public static string Player_Tag="Player";
	public static string Platform_Tag="Platform";
	public static string MorePlatforms_Tag="MorePlatforms";
	public static string Health_Tag="Health";
	public static string Monster_Tag="Monster";
    public static string MonsterBullet_Tag = "MonsterBullet";
    public static string PlayerBullet_Tag = "PlayerBullet";
    public static string Bounds_Tag = "Bounds";

    //Scene Names
    public static string Gameplay_Scene = "Gameplay";
    public static string MainMenu_Scene = "MainMenu";

    //GameObject Names
    public static string Backgrund_GameObjTag="Background";
	public static string LevelGenerator_ObjTag="Level Generator";
    public static string Score_Text_Obj = "Score Text";
    public static string Health_Text_Obj = "Health Text";
    public static string Level_Text_Obj = "Level Text";
    public static string Pause_Panel_Obj = "Pause Panel";
    public static string Shoot_Button_Obj = "Shoot Button";
    public static string Jump_Button_Obj = "Jump Button";

    //animation Tags
    public static string animation_Idle="idle";
	public static string animation_Run="run";
	public static string animation_Walk="walk";
	public static string animation_Jump="jump";
	public static string animation_Jump_Fall="jumpFall";
	public static string animation_Jump_Land="jumpLand";
	public static string animation_Ledge_Fall="ledgeFall";
}