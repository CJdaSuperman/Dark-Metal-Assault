using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int playerHealth;
    [SerializeField] Text healthText;

    [SerializeField]
    [Tooltip("How many points needed to increase tower limit by 1")] int towerScore;
    [SerializeField] int startingTowers;
    [SerializeField] int towerLimit;

    [HideInInspector] public static int score;
    [SerializeField] Text scoreText;
   
    [SerializeField] AudioClip playerHitSFX;

    void Start() { healthText.text = playerHealth.ToString(); }

    void Update() { scoreText.text = score.ToString(); }

    void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().PlayOneShot(playerHitSFX);

        playerHealth--;

        if (playerHealth < 0)
            healthText.text = "0";
        else
            healthText.text = playerHealth.ToString();
    }

    public int GetPlayerHealth() { return playerHealth; }

    public int GetTowersAvailable()
    {
        int currentAmountOfTowers = startingTowers + (score / towerScore);

        return (currentAmountOfTowers <= towerLimit) ? currentAmountOfTowers : towerLimit;       
    }
}
