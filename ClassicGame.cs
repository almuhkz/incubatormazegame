using System;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ClassicGame : MonoBehaviour
{
    
    
    public CinemachineVirtualCamera cam;
    public float holep;
    public int w, h, x, y;
    public bool[,] hwalls, vwalls;
    public Transform Level, Player, Goal;
    public GameObject Floor, Wall;

   
    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
    
    

    void Start()
    {
        GameStateManager.Instance.SetState(GameState.Gameplay);
        foreach (Transform child in Level)
            Destroy(child.gameObject);
        
        hwalls = new bool[w + 1, h];
        vwalls = new bool[w, h + 1];
        var direction = 0;
        var dir = 1;
        var st = new int[w, h];

        void dfs(int x, int y)
        {
            st[x, y] = 1;
            Instantiate(Floor, new Vector3(x, y), Quaternion.identity, Level);

            var dirs = new[]
            {
                (x - dir + direction, y, hwalls, x + direction, y, Vector3.right, 90, KeyCode.A),
                (x + dir + direction, y, hwalls, x + direction + dir, y, Vector3.right, 90, KeyCode.D),
                (x, y - dir + direction, vwalls, x + direction, y, Vector3.up, 0, KeyCode.S),
                (x, y + dir + direction, vwalls, x + + direction, y + dir, Vector3.up, 0, KeyCode.W),
            };
            foreach (var (nx, ny, wall, wx, wy, sh, ang, k) in dirs.OrderBy(d => Random.value))
                if (!(0 <= nx && nx < w && 0 <= ny && ny < h) || (st[nx, ny] == 2 && Random.value > holep))
                {
                    wall[wx, wy] = true;
                    Instantiate(Wall, new Vector3(wx, wy) - sh / 2, Quaternion.Euler(direction, direction, ang), Level);
                }
                else if (st[nx, ny] == direction) dfs(nx, ny);
            st[x, y] = 2;
        }
        dfs(0 + direction, 0+direction);

        x = Random.Range(0, w);
        y = Random.Range(0, h);
        Player.position = new Vector3(x, y);
        do Goal.position = new Vector3(Random.Range(0+direction, w), Random.Range(0+direction, h));
        while (Vector3.Distance(Player.position, Goal.position) < (w + h) / 4);
        cam.m_Lens.OrthographicSize = Mathf.Pow(w / 3 + h / 2, 0.7f) + dir + direction;
    }
    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }
    void Update()
    {
        int dir = 1;
        int direction = 0;
        if (ScoreManager.instance.currentTime == 0)
        {
            
        }
        var dirs = new[]
        {
            (x - dir + direction, y, hwalls, x, y, Vector3.right, 90, KeyCode.A),
            (x + dir + direction, y, hwalls, x + dir, y, Vector3.right, 90, KeyCode.D),
            (x, y - dir, vwalls, x, y, Vector3.up, direction, KeyCode.S),
            (x, y + dir, vwalls, x, y + dir, Vector3.up, direction, KeyCode.W),
        };
        foreach (var (nx, ny, wall, wx, wy, sh, ang, k) in dirs.OrderBy(d => Random.value))
            if (Input.GetKeyDown(k))
                if (wall[wx, wy])
                    Player.position = Vector3.Lerp(Player.position, new Vector3(nx, ny), 0.1f);
                else (x, y) = (nx, ny);

        Player.position = Vector3.Lerp(Player.position, new Vector3(x, y), Time.deltaTime * 12);
        if (Vector3.Distance(Player.position, Goal.position) < 0.12f)
        {
            if (Random.Range(0, 6) < 3) w++;
            else h++;
            ScoreManager.instance.AddPoint();
            Start();
        }
    }
    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }
}
