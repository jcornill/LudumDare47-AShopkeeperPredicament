using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterMovement : MonoBehaviour
{
    public float speed;

    private Animator anim;
    private static readonly int Right = Animator.StringToHash("Right");
    private static readonly int Left = Animator.StringToHash("Left");
    private static readonly int Up = Animator.StringToHash("Back");
    private static readonly int Down = Animator.StringToHash("Front");

    private bool goRight;
    private bool goingToNPC;
    private bool leavingFromNPC;
    private bool wait;

    public float SendX;
    public float SendY;

    public GameCharacterData gameCharacter;

    private void Awake()
    {
        this.gameCharacter = CharacterManager.Instance.GenerateRandomCharacter();
        this.anim = GetComponent<Animator>();
        this.goRight = Random.Range(0, 2) == 1;
        this.SendY = Random.Range(-3.5f, -2.5f);
        this.SendX = Random.Range(-2f, 2f);
        if (this.goRight)
        {
            this.transform.position = new Vector3(-12f, this.SendY);
        }
        else
        {
            this.transform.position = new Vector3(12f, this.SendY);
        }

        if (DialogManager.CanSendCharacter)
        {
            DialogManager.SendChar(this);
            this.goingToNPC = true;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        ResetAllBool();
        if (this.wait)
        {
            return;
        }
        TestDelete();
        if (this.goingToNPC == false && this.leavingFromNPC == false)
        {
            if (this.goRight)
            {
                MoveToRight();
            }
            else
            {
                MoveToLeft();
            }
        }
        else if (Math.Abs(this.transform.position.x - this.SendX) > 0.1f)
        {
            if (this.goRight)
            {
                MoveToRight();
            }
            else
            {
                MoveToLeft();
            }
        }
        else if (this.goingToNPC)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    public void CanLeave()
    {
        this.leavingFromNPC = true;
        this.wait = false;
    }
    
    private void TestDelete()
    {
        if (Mathf.Abs(this.transform.position.x) >= 15f)
        {
            Destroy(this.gameObject);
        }
    }

    private void ResetAllBool()
    {
        this.anim.SetBool(Right, false);
        this.anim.SetBool(Left, false);
        this.anim.SetBool(Up, false);
        this.anim.SetBool(Down, false);
    }

    private void MoveUp()
    {
        this.anim.SetBool(Up, true);
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Back"))
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.SendX, -0.5f), this.speed * Time.deltaTime);
        }

        if (Math.Abs(this.transform.position.y - (-0.5f)) < 0.1f)
        {
            this.goingToNPC = false;
            this.wait = true;
            DialogManager.CharIsHere();
        }
    }
    
    private void MoveDown()
    {
        this.anim.SetBool(Down, true);
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Front"))
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.SendX, -3f), this.speed * Time.deltaTime);
        }

        if (Math.Abs(this.transform.position.y - (-3f)) < 0.1f)
        {
            this.leavingFromNPC = false;
        }
    }

    private void MoveToRight()
    {
        this.anim.SetBool(Right, true);
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Right"))
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(20, this.transform.position.y), this.speed * Time.deltaTime);
        }
    }

    private void MoveToLeft()
    {
        this.anim.SetBool(Left, true);
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Left"))
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(-20, this.transform.position.y), this.speed * Time.deltaTime);
        }
    }
}
