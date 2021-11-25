using UnityEngine;

public class ExecuteAction : MonoBehaviour
{
    private Animator anim;
    private bool closed=true;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Execute()
    {
        closed = !closed;
        anim.SetBool("Closed", closed);
    }
}
