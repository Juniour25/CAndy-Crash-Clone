using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Level : MonoBehaviour
{   public enum LevelType{
    TIMER,
    OBSTACLE,
    MOVES,
    };
    public Grids grid;
    public HUD hud;
    public int score1Star,score2Star,score3Star;
    protected LevelType type;
    public LevelType Type{
        get{return type;}
    }
    protected int currentScore;
    protected bool didWin;
    // Start is called before the first frame update
    void Start()
    {
        hud.SetScore(currentScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void GameWin(){
       
        grid.GameOver();
        didWin=true;
        StartCoroutine(WaitForGridFill());
    }
    public virtual void GameLose(){
        
        grid.GameOver();
        didWin=false;
        StartCoroutine(WaitForGridFill());
    }
    public virtual void OnMove(){
        
    }
    public virtual void OnPieceCleared(GamePiece piece){
        currentScore+=piece.score;
        hud.SetScore(currentScore);
    }
    protected virtual IEnumerator WaitForGridFill(){
        while(grid.IsFilling){
            yield return 0;
        }
        if(didWin){
            hud.OnGameWin(currentScore);
        }
        else{
            hud.OnGameLose();
        }
    }
}
