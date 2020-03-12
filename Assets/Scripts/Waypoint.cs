using UnityEngine;

public class Waypoint : MonoBehaviour
{
    const int gridSize = 10;

    public bool isExplored { get; set; } = false;
    public Waypoint exploredFrom { get; set; }
    public bool isPlaceable { get; set; } = true;    

    TowerFactory towerFactory;

    [Header("Waypoint Indicators")]
    [SerializeField] Transform placeTowerIndicator;
    [SerializeField] Transform cantPlaceIndicator;
    [SerializeField] public Transform limitReachedIndicator;

    GameManager gameManager;

    void Awake()
    {
        towerFactory = FindObjectOfType<TowerFactory>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public int GetGridSize() { return gridSize; }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize));
        //Let's say the current transform position at x is 6, divide that by 10 is 0.6
        //Then that will round to 1 and you multiply by 10 to get that snapping of 10 units
        //without you having to hold CTRL to snap objects (if x was 4, then RoundToInt will make
        //it 0 and the object will stay where it is)
    }

    void OnMouseOver()
    {
        if (gameManager.IsGameOver() || gameManager.isGamePaused())
        {
            enabled = false;
            return;
        }

        if (isPlaceable)
        {
            if(towerFactory.IsTowerLimitReached())
            {
                placeTowerIndicator.gameObject.SetActive(false);
                limitReachedIndicator.gameObject.SetActive(true);                
                towerFactory.GetTowerToMove().baseWaypoint.limitReachedIndicator.gameObject.SetActive(true);
            }
            else
            {
                placeTowerIndicator.gameObject.SetActive(true);
            }            
        }             
        else
        {
            cantPlaceIndicator.gameObject.SetActive(true);
        }
    }

    void OnMouseExit()
    {
        placeTowerIndicator.gameObject.SetActive(false);
        cantPlaceIndicator.gameObject.SetActive(false);
        limitReachedIndicator.gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        if (isPlaceable)
            towerFactory.AddTower(this);
        else
            return;
    }
}
