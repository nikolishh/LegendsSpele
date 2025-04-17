using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseControl : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private Collider[] weapons;


    private CharacterController characterController;
    private Vector3 targetPosition;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        ToggleWeapons(false);
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        targetPosition = transform.position;
    }

    public void BeginAttack()
    {
        ToggleWeapons(true);
    }

    public void EndAttack()
    {
        ToggleWeapons(false);
    }
    public void ToggleWeapons(bool enable)
    {
        foreach (Collider weapon in weapons)
        {
            weapon.enabled = enable;
        }
    }
    // Update is called once per frame
    void Update()
    {
        float distToTarget = Vector3.Distance(transform.position, targetPosition);
        if (distToTarget > 1f && PlayerHealth.isAlive)
        {
            Vector3 targetDirection = Vector3.Normalize(targetPosition - transform.position);
            characterController.Move(targetDirection * moveSpeed * Time.deltaTime);
            transform.LookAt(targetPosition);
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 500, layerMask))
            {
                //Debug.Log("hit: " + hit.collider.name);
                targetPosition = hit.point;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("attack");
        }
        }
}
