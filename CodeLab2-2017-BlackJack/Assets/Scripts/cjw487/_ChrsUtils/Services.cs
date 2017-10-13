

public class Services
{
    public static Main Main { get; set; }
    public static GameManager GameManager { get; set; }
    public static GameEventsManager EventManager { get; set; }
    public static TaskManager GeneralTaskManager { get; set; }
    public static PrefabDB Prefabs { get; set; }
    
    public static InputManager InputManager { get; set; }
    
    public static GameSceneManager<TransitionData> Scenes { get; set; }
}
