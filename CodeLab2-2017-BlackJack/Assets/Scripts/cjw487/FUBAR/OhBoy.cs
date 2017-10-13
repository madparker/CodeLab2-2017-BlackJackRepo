using UnityEngine;

public class OhBoy : MonoBehaviour
{
    //  This is where we shake the camera and fire the events
    //  for the game's logic to check

    [SerializeField] private KeyCode shakeButton;

	void Start ()
    {
        shakeButton = KeyCode.Space;

        //  Assigns the function OnKeyPressed to the KeyPressedEvent
        Services.EventManager.Register<KeyPressedEvent>(OnKeyPressed);
	}

    private void OnDestroy()
    {
        Services.EventManager.Unregister<KeyPressedEvent>(OnKeyPressed);
    }

    private void OnKeyPressed(KeyPressedEvent e)
    {
        if (e.key == shakeButton)
        {
            CameraShake.CameraShakeEffect.Shake(0.2f, 0.5f);
            Services.EventManager.Fire(new ChangeTokenEvent());
            Services.EventManager.Fire(new CheckIfSolvedEvent());
            Services.EventManager.Fire(new ResetTokenLockEvent());
        }
    }
}
