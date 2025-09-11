using UnityEngine;

public class Character : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private float speed = 5f;

    [SerializeField] private GameManager gameManager;
    private float init_y_pos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        init_y_pos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));

        if(characterController.enabled == true)
        {
            characterController.Move(move*Time.deltaTime*speed);

            Vector3 p = transform.position;

            //Y-axis correction        
            float yDelta = init_y_pos - transform.position.y;
            if (Mathf.Abs(yDelta) > 0.0001f)
            {
                characterController.Move(new Vector3(0f, yDelta, 0f));
            }
        }
            
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Reward"))
        {
            Debug.Log("collect Reward");
            gameManager.HandleReward();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("BigReward"))
        {
            Debug.Log("collect Big Reward");
            gameManager.HandleBigReward();
            Destroy(other.gameObject);
        }
    }

}
 