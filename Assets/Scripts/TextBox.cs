using UnityEngine;

public class TextBox : MonoBehaviour
{
    private InstructionManager instructionManagerScript;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Component textBoxTrigger = GetComponent<BoxCollider2D>();
        
        GameObject instructionManager = GameObject.Find("Instruction Manager");
        instructionManagerScript = instructionManager.GetComponent<InstructionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
      
    }
}
