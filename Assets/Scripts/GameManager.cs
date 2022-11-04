using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager InstanGameManager;
    // Start is called before the first frame update
    public GameStates ActualState { get; set; }
    public enum GameStates
    {
        Start,
        Runing,
        GameOver
    }



    private void Awake()
    {
        InstanGameManager = this;
    }
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState(GameStates.Runing);
        }

        if (ActualState == GameStates.Start || ActualState == GameStates.GameOver)
        {
            return;
        }

    }
    public void ChangeState(GameStates newStates)
    {
        if (ActualState != newStates)
        {
            ActualState = newStates;
        }
    }
}


