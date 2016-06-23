/*Silent Dream created by Adina Shanholtz. Art and story by Dominic D'Andrea.*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

#if UNITY_WSA_10_0 && NETFX_CORE
using Microsoft.UnityPlugins;
#endif


public class GameController : MonoBehaviour
{
    //Keeps track of sweet dream images and enemies. Initialized in editor. 
    public GameObject[] enemies;
    public Image[] sweetDreamImages;

    public GameObject sweetDream;
    public Vector3 spawnValues;
    public float spawnWait;
    public float startWait;

    public bool over = false;
    public bool done = false;

    //Text elements for score and health
    public Text scoreText;
    private int score;

    public Text healthText;
    private int health;

    //Keep track of which sweet dream # you're updating
    private int sweetDreams;
    private int prevsweetdreams;

    //Variables for adjusting difficulty
    private int enemyCount;
    private int timesHit;

    //joysticks
    public GameObject joystickRight;
    public GameObject joystickLeft;

    private bool calledthenotif = false;


    void Start()
    {
        //Initialization
        score = 0;
        health = 100;
        sweetDreams = 0;
        Reset();
        UpdateScore();
        UpdateHealth();
        UpdateDreams();
        StartCoroutine(SpawnWaves());


    }

    void Update()
    {
        Time.timeScale = 1;

        //Check for gameover
        if(sweetDreams >= 10)
        {
            over = true;
            StartCoroutine(WinGame());

        }

        if (over)
        {
            Time.timeScale = .2f;
            if (done)
            {
                Time.timeScale = 0;
            }
        }

    }


    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {

            Vector3 spawnPosition;

            // this decides where to spawn the next enemy
            if (Random.Range(0, 2) == 1)
            {
                if (Random.Range(0, 2) == 1)
                    spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                else
                {
                    spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), -spawnValues.y, spawnValues.z);
                }
            }
            else
            {
                if (Random.Range(0, 2) == 1)
                    spawnPosition = new Vector3(spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
                else
                {
                    spawnPosition = new Vector3(-spawnValues.x, Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
                }
            }

            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(enemies[Random.Range(0, 3)], spawnPosition, spawnRotation);
            if (enemyCount >= 10)
            {
                enemyCount = 0;
                Instantiate(sweetDream, new Vector3(Random.Range(-7f, 8f), Random.Range(-3f, 4f), 0), spawnRotation);
            }

            //spawn time adjusted to how many times you've been hit
            yield return new WaitForSeconds(spawnWait+((float)timesHit/10));

        }

    }


    IEnumerator WinGame()
    {
        //Pop up a notification of score for Win10 devices
#if UNITY_WSA_10_0 && NETFX_CORE
       if(!calledthenotif){
            Toasts.ShowToast(ToastTemplateType.ToastImageAndText01, new string[] { "Congratulations! Your score was: " + score }, "Assets/StoreLogo.scale-400.png");

        }
#endif

        calledthenotif = true;

        //Enter win scenario
        GameObject light = GameObject.FindGameObjectWithTag("Light");
        Light backgroundLight = light.GetComponent<Light>();
        while (backgroundLight.intensity < 7)
        {
            backgroundLight.intensity+=.04f;
            yield return new WaitForSeconds(.4f);

        }
        over = false;
        Reset();
        SceneManager.LoadScene("Win Screen");

    }



    /**UI functions- update score, health, and sweet dream count**/

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public int getScore()
    {
        return score;
    }

    public void AddSweetDream()
    {
        prevsweetdreams = sweetDreams;
        sweetDreams++;
        UpdateDreams();
        //update health based on how many times hit
        if(timesHit > 5 && health < 100)
        {
            health += 10;
        }
        else
        {
            if(health < 100)
                health += 5;

        }
        UpdateHealth();

    }

    public void EnemyCountIncrease()
    {
        enemyCount++;
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void SubtractHealth()
    {
        //adjust damage takaway depending on how well you're doing in game
        if(timesHit < 3)
        {
            health -= 20;
        }
        else if (timesHit < 7)
        {
            health -= 10;
        }

        else
        {
            health -= 5;
        }
        timesHit++;
        prevsweetdreams = sweetDreams;
        sweetDreams--;
        if (sweetDreams <= 0)
        {
            sweetDreams = 0;
        }

        UpdateHealth();
        UpdateDreams();
    }

    void UpdateHealth()
    {
        healthText.text = "Health: " + health;
    }

    void UpdateDreams()
    {
        //update Sweet Dream UI
        sweetDreamImages[prevsweetdreams].GetComponent<Image>().enabled = false;
        sweetDreamImages[sweetDreams].GetComponent<Image>().enabled = true;
        //sweetDreamText.text = "Sweet Dreams: " + sweetDreams + " % ";
    }

    public void Reset()
    {
        timesHit = 1;
    }
    public int getHealth()
    {
        return health;
    }

    public void ToggleJoysticks()
    {
        if (joystickLeft.GetComponent<Image>().IsActive())
        {
            joystickLeft.GetComponent<Image>().enabled = false;
            joystickRight.GetComponent<Image>().enabled = false;
        }
        else
        {
            joystickLeft.GetComponent<Image>().enabled = true;
            joystickRight.GetComponent<Image>().enabled = true;
        }

    }
}
