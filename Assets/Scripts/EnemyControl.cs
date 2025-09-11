using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    //Enemy Unique ID
    private static int counter = 0;  // Counter shared by all Enemies 
    [SerializeField] private int myOrder; //  Unique ID for each Enemy
    private float orderDelay;
    private bool hasStarted;

    [SerializeField] private GameObject playerObj;
    private Vector3 playerPos;
    private UnityEngine.AI.NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private bool isFreezing; 
 
    [SerializeField] private Material UnFrezMat;
    [SerializeField] private Material FrezMat;

    [SerializeField] private GameManager gameManager;
    void Awake()
    {
        counter++;
        myOrder = counter;
        orderDelay = 5f * (myOrder - 1); 
        hasStarted = false;
        //Debug.Log(gameObject.name+": "+myOrder); 
    }
    void Start()
    {
        isFreezing = false;
        playerPos = playerObj.transform.position;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void Freeze(){
        isFreezing = true;
        agent.isStopped = true;
        agent.ResetPath();
        agent.velocity = Vector3.zero;
        agent.speed = 0;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = FrezMat;
    }

    public void UnFreeze(){
        isFreezing =false;
        agent.isStopped = false;
        agent.speed = 3.5f;

        Renderer renderer = GetComponent<Renderer>();
        renderer.material = UnFrezMat;
    }

    public void TemporyStopOn()
    {
        isFreezing = true;
        agent.isStopped = true;
        agent.ResetPath();
        agent.velocity = Vector3.zero;
        agent.speed = 0;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void TemporyStopOff()
    {
        isFreezing = false;
        agent.isStopped = false;
        agent.speed = 3.5f;
    }


    // Update is called once per frame
    void Update()
    {
        if (hasStarted){

            if(!isFreezing){
                playerPos = playerObj.transform.position;
                agent.SetDestination(playerPos);
            }
            
        }
        else{
            if(orderDelay > 0){
                orderDelay -= Time.deltaTime;
            }
            else{
                hasStarted = true;
            }
            
        }
    }

    
    void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.CompareTag("Player")){
            //Debug.Log("Collided with Player!");
            if(isFreezing){
                gameManager.KillEnemy(this.gameObject);
            }
            else{
                gameManager.OnPlayerDamaged();
            }
        }
    }
}
