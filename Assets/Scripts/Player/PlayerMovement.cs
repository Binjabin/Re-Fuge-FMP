using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    float turnDirection;
    Vector3 velocity, additionalForce, inputVector;
    Rigidbody rb;
    [SerializeField] float thrustSpeed = 5f;
    [SerializeField] AudioSource thrustSound;
    [SerializeField] float rotationSpeed = 5f;
    float velocityref;
    bool thrusting;
    bool breaking;
    [SerializeField] float lazerDistance;
    [SerializeField] bool die;
    bool inDialogue;
    [SerializeField] GameObject playerShip;
    TrailRenderer[] trail;
    float[] standardTrailLength = { 0f, 0f, 0f };
    EnergyBar energy;
    public bool miningRock;
    [SerializeField] float energyPerSecondThrusting;
    [SerializeField] float energyPerSecondMining;
    [SerializeField] float damagePerUnitSpeed;
    [SerializeField] float minImpactSpeedForDamage;

    GameObject dialogueFocus;

    [SerializeField] ParticleSystem miningLazer;
    bool dead = false;
    bool win = false;
    public bool inactive;
    float timeSinceLastCollide;

    [SerializeField] AudioSource miningLazerSound;
    [SerializeField] AudioSource rubbleEffectSound;

    [SerializeField] LayerMask deathLayers;
    [SerializeField] List<ParticleSystem> playerDeathParticles;
    [SerializeField] GameObject deathCanvas;
    [SerializeField] AudioSource explosion;
    [SerializeField] LayerMask lazerLayerMask;
    string deathMessage;
    [SerializeField] TextMeshProUGUI causeOfDeath;
    string tipText;
    [SerializeField] private TextMeshProUGUI tip;

    [SerializeField] GameObject winCanvas;
    Inventory inventory;
    [SerializeField] AudioSource collisionSound;
    [SerializeField] AudioClip heavyCollision;
    [SerializeField] AudioClip lightCollision;

    // Start is called before the first frame update
    void Start()
    {

        timeSinceLastCollide = 0f;
        dead = false;
        win = false;
        rb = GetComponent<Rigidbody>();
        energy = gameObject.GetComponent<EnergyBar>();
        inDialogue = false;
        trail = GetComponentsInChildren<TrailRenderer>();
        inventory = FindObjectOfType<Inventory>();
        CheckDeath();
        for (int i = 0; i < trail.Length; i++)
        {
            standardTrailLength[i] = trail[i].time;
            trail[i].time = 0f;
        }
    }

    void CheckDeath()
    {
        if (inventory.currentWater < 0.1f)
        {
            Die("Water");
        }
        else if (inventory.currentFood < 0.1f)
        {
            Die("Food");
        }
    }
    void DetermineRubbleEffect()
    {
        RaycastHit hit;
        Physics.Raycast(miningLazer.transform.position, transform.right, out hit, lazerDistance, lazerLayerMask);
        Debug.DrawRay(miningLazer.transform.position, transform.right * lazerDistance, Color.red);
        Debug.Log(hit.collider);
        if (hit.collider != null)
        {

            if (hit.collider.gameObject.GetComponent<Asteroids>() != null)
            {
                miningRock = true;
            }
            else
            {
                miningRock = false;
            }
        }

        else
        {
            miningRock = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        inactive = dead || win;
        DetermineRubbleEffect();
        GetInput();
        timeSinceLastCollide += Time.deltaTime;
        if (die)
        {
            die = false;
            Die("Command");
        }
        if (!inactive)
        {
            if (inventory.currentEnergy < 0.1f)
            {
                Die("Power");
            }
        }

    }


    void FixedUpdate()
    {

        if (!inactive)
        {
            deathCanvas.SetActive(false);
            if (!inDialogue && !inventory.invOpen)
            {
                ProcessInput();
            }
            else if (inventory.invOpen)
            {
                miningLazer.Stop();
                miningLazerSound.Stop();
                rubbleEffectSound.Stop();
            }
            else if (inDialogue && dialogueFocus != null)
            {
                Vector3 targetDirection = dialogueFocus.transform.position - transform.position;
                targetDirection.y = 0f;
                float rotationStep = 3f * Time.deltaTime;
                Vector3 newDirection = Vector3.RotateTowards(transform.right, targetDirection, rotationStep, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection) * Quaternion.Euler(0f, -90f, 0f);
            }
            if (inDialogue)
            {
                miningLazer.Stop();
                miningLazerSound.Stop();
                rubbleEffectSound.Stop();
            }

        }
        else
        {
            miningLazer.Stop();
            miningLazerSound.Stop();
            rubbleEffectSound.Stop();
            if(dead)
            {
                deathCanvas.SetActive(true);
                
            }
            if(win)
            {
                winCanvas.SetActive(true);
            }
        }
        

    }

    void GetInput()
    {
        thrusting = Input.GetKey(KeyCode.W);
        breaking = Input.GetKey(KeyCode.S) || inDialogue;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turnDirection = 1f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turnDirection = -1f;
        }
        else
        {
            turnDirection = 0f;
        }


        for (int i = 0; i < trail.Length; i++)
        {
            if (!(energy.currentEnergy > 0))
            {
                trail[i].time = Mathf.SmoothDamp(trail[i].time, 0f, ref velocityref, 0.1f);
            }
            else if (thrusting)
            {
                trail[i].time = Mathf.SmoothDamp(trail[i].time, standardTrailLength[i], ref velocityref, 0.1f);
            }
            else
            {
                trail[i].time = Mathf.SmoothDamp(trail[i].time, 0f, ref velocityref, 0.1f);
            }
        }
    }
    void ProcessInput()
    {
        if (inventory.currentEnergy < 0.01f)
        {
            miningLazer.Stop();
            miningLazerSound.Stop();
            rubbleEffectSound.Stop();
        }
        else
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {

                miningLazer.Play();
                if (!miningLazerSound.isPlaying)
                {
                    miningLazerSound.Play();

                }
                if (miningRock)
                {
                    if (!rubbleEffectSound.isPlaying)
                    {
                        rubbleEffectSound.Play();
                    }

                }
                else
                {
                    rubbleEffectSound.Stop();
                }

                inventory.ReduceEnergy(energyPerSecondMining * Time.deltaTime);
            }
            else
            {
                miningLazer.Stop();
                miningLazerSound.Stop();
                rubbleEffectSound.Stop();
            }
        }

        if (thrusting)
        {
            inventory.ReduceEnergy(energyPerSecondThrusting * Time.deltaTime);
            if (energy.currentEnergy > 0f)
            {
                rb.AddForce(transform.right * thrustSpeed);
                if (!thrustSound.isPlaying)
                {
                    thrustSound.Play();
                    thrustSound.volume = rb.velocity.magnitude / 300f + 0.2f;
                    thrustSound.pitch = rb.velocity.magnitude / 100f + 0.5f;
                }
            }
            else
            {
                thrustSound.Stop();
            }


        }
        else
        {
            thrustSound.Stop();
        }
        if (breaking)
        {
            rb.drag = 3f;
        }
        else
        {
            rb.drag = 0.1f;
        }
        if (turnDirection != 0f)
        {
            rb.AddTorque(transform.up * rotationSpeed * -turnDirection);
        }

    }

    public void EnterDialogue(GameObject focus)
    {
        inDialogue = true;
        rb.drag = 3f;
        dialogueFocus = focus;
        FindObjectOfType<CameraStateController>().ToMerchant();
    }

    public void EnterMonologue()
    {
        inDialogue = true;
        rb.drag = 3f;
    }

    public void ExitDialogue()
    {
        inDialogue = false;
        FindObjectOfType<CameraStateController>().ToPlayer();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (timeSinceLastCollide > 1f)
        {
            if (collision.relativeVelocity.magnitude > minImpactSpeedForDamage)
            {
                GetComponent<HealthBar>().Damage(Mathf.Min(collision.relativeVelocity.magnitude * damagePerUnitSpeed, 50f));
                timeSinceLastCollide = 0f;
                collisionSound.Stop();
                collisionSound.pitch = 1.8f;
                collisionSound.clip = heavyCollision;
                collisionSound.Play();
                
            }
            else
            {
                timeSinceLastCollide = 0f;
                collisionSound.Stop();
                collisionSound.pitch = 0.7f;
                collisionSound.clip = lightCollision;
                collisionSound.Play();
            }
        }

    }
    public void Die(string deathCause)
    {
        Debug.Log(deathCause);
        switch (deathCause)
        {
            case "Food":
                tipText = "Keep an eye on your supplies. If you don't have enough food gather some more by mining the darker brown asteroids.";
                deathMessage = "Starvation";
                break;
            case "Water":
                tipText = "Keep an eye on your supplies. If you don't have enough water gather some more by mining the light grey asteroids.";
                deathMessage = "Thirst";
                break;
            case "Power":
                tipText = "Keep an eye on your supplies. If you don't have enough energy gather some more by mining the light brown asteroids.";
                deathMessage = "Power Failure";
                break;
            case "Damage":
                tipText = "Avoid crashing into asteroids! You can slow down using the S key. Make sure to use your stealth to stay well away from enemies. ";
                deathMessage = "Ship Damage";
                break;
            case "Deported":
                tipText = "You didn't have any ID so they sent you away! You might need to find someone who sells false identification.";
                deathMessage = "Deportation";
                break;
        }
        

        
        PlayerStats.isDead = true;
        PlayerStats.SaveStats();
        explosion.Play();
        rb.drag = 100f;
        dead = true;
        inventory.gameObject.SetActive(false);
        Camera.main.cullingMask = deathLayers;
        Camera.main.clearFlags = CameraClearFlags.Color;
        Camera.main.backgroundColor = Color.black;
        playerShip.SetActive(false);
        foreach (ParticleSystem part in playerDeathParticles)
        {
            part.Play();
        }
        playerShip.SetActive(false);
        playerShip.SetActive(false);
        StartCoroutine(Death());

    }

    IEnumerator Death()
    {
        FindObjectOfType<LevelAudio>().Death();

        deathCanvas.GetComponent<CanvasGroup>().alpha = 0f;
        yield return new WaitForSeconds(4f);
        causeOfDeath.text = "Cause Of Death: " + deathMessage;
        tip.text = "Tip: " + tipText;
        float elapsed = 0f;
        while (elapsed < 2f)
        {
            deathCanvas.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0f, 2f, elapsed / 2f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        elapsed = 0f;
        while (elapsed < 1f)
        {
            deathCanvas.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(2f, 0f, elapsed / 1f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        FindObjectOfType<SceneManagment>().LeaveScene();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Menu");

    }

    

    public void Win()
    {
        PlayerStats.levelPassed += 1;
        PlayerStats.SaveStats();
        rb.drag = 100f;
        win = true;
        inventory.gameObject.SetActive(false);
        Camera.main.cullingMask = deathLayers;
        Camera.main.clearFlags = CameraClearFlags.Color;
        Camera.main.backgroundColor = Color.black;
        StartCoroutine(PlayerWin());
    }

    IEnumerator PlayerWin()
    {
        FindObjectOfType<LevelAudio>().Death();

        winCanvas.GetComponent<CanvasGroup>().alpha = 0f;
        yield return new WaitForSeconds(4f);
        float elapsed = 0f;
        while (elapsed < 2f)
        {
            winCanvas.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0f, 2f, elapsed / 2f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        elapsed = 0f;
        while (elapsed < 1f)
        {
            deathCanvas.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(2f, 0f, elapsed / 1f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        FindObjectOfType<SceneManagment>().LeaveScene();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Credits");
    }
}
