using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public Animator targetAnim;
    public Rigidbody2D targetRigid;

    // Start is called before the first frame update
    void Start()
    {
        targetAnim.SetBool("isRunning", true);
    }

    // Update is called once per frame
    void Update()
    {
        CheckKeyboard();
    }

    void CheckKeyboard()
    {
        if(Input.GetKeyDown(KeyCode.Space) == true)
        {
            targetRigid.AddForce(Vector2.up * 1000);
        }
    }
}
