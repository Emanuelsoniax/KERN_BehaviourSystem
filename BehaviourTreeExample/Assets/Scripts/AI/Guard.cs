using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    [SerializeField] private Transform viewTransform;

    private NavMeshAgent agent;
    private Animator animator;
    private Blackboard blackboard = new Blackboard();
    [SerializeReference] public BaseScriptableObject[] variables;
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private Transform weaponHoldTransform;

    private GameObject player;
    public UIElement UI;

    public VariableBoolean HasWeapon
    {
        get { return HasWeapon = blackboard.GetVariable<VariableBoolean>("VariableBoolean_Guard_HasWeapon"); }
        set { blackboard.dictionary["VariableBoolean_Guard_HasWeapon"] = value; }
    }
    public VariableGameObject Target
    {
        get { return Target = blackboard.GetVariable<VariableGameObject>("VariableGameObject_Guard_Target"); }
        set { blackboard.dictionary["VariableGameObject_Guard_Target"] = value; }
    }
    public VariableFloat WalkSpeed
    {
        get { return WalkSpeed = blackboard.GetVariable<VariableFloat>("VariableFloat_Guard_WalkSpeed"); }
        set { blackboard.dictionary["VariableFloat_Guard_WalkSpeed"] = value; }
    }
    public VariableFloat StopDistance
    {
        get { return StopDistance = blackboard.GetVariable<VariableFloat>("VariableFloat_Guard_StoppingDistance"); }
        set { blackboard.dictionary["VariableFloat_Guard_StoppingDistance"] = value; }
    }
    public VariableFloat SightRange
    {
        get { return StopDistance = blackboard.GetVariable<VariableFloat>("VariableFloat_Guard_SightRange"); }
        set { blackboard.dictionary["VariableFloat_Guard_SightRange"] = value; }
    }
    public VariableFloat ViewAngleInDegrees
    {
        get { return StopDistance = blackboard.GetVariable<VariableFloat>("VariableFloat_Guard_ViewAngleInDegrees"); }
        set { blackboard.dictionary["VariableFloat_Guard_ViewAngleInDegrees"] = value; }
    }
    public VariableFloat AttackRange
    {
        get { return StopDistance = blackboard.GetVariable<VariableFloat>("VariableFloat_Guard_AttackRange"); }
        set { blackboard.dictionary["VariableFloat_Guard_AttackRange"] = value; }
    }

    //BTNodes
    private BTBaseNode tree;
    private BTBaseNode patrol;
    public BTBaseNode attack;
    private BTBaseNode grabWeapon;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        player = FindObjectOfType<Player>().gameObject;

        foreach (BaseScriptableObject variable in variables)
        {
            blackboard.AddVariable(variable.name, variable);
        }

        Target.Value = waypoints[0];
        
    }

    private void Start()
    {
        #region Patrol Sequence
        //patrol sequence
        patrol = new BTParallelNode(new BTBaseNode[2]{
                    //condition node
                    new BTInverterNode(new BTCheckForPlayer(viewTransform, SightRange, ViewAngleInDegrees, player)),
                    //waypoint sequence
                    new BTSequenceNode(
                        new BTUpdateUI(UI, "Patrolling"),
                        new BTSetTarget(waypoints, Target),
                        new BTPlayAnimation(animator, "Rifle Walk"),
                        new BTMoveToTarget(Target, WalkSpeed, StopDistance, agent),
                        new BTPlayAnimation(animator, "Idle"),
                        new BTWaitNode(1)
                    )
                 }
        );

        #endregion

        #region Attack Sequence

        attack = new BTFallbackNode(new BTBaseNode[2]{
                   //grab weapon sequence
                   new BTParallelNode(new BTBaseNode[2]
                   {
                       //condition node
                       new BTInverterNode(new BTCheckForWeapon(HasWeapon)),

                       new BTSequenceNode(
                            new BTUpdateUI(UI, "Grabbing Weapon"),
                            new BTFindWeapon(weapons, Target, transform),
                            new BTPlayAnimation(animator, "Rifle Walk"),
                            new BTMoveToTarget(Target, WalkSpeed, StopDistance, agent),
                            new BTPlayAnimation(animator, "Crouch Idle"),
                            new BTGrabWeapon(HasWeapon),
                            new BTWaitNode(1)
                       )
                   }
                   ),
                   //attack sequence
                   new BTParallelNode(new BTBaseNode[2]{
                        new BTInverterNode(new BTCheckForPlayer(viewTransform, SightRange, ViewAngleInDegrees, player)),
                        new BTSequenceNode(
                            new BTUpdateUI(UI, "Attacking"),
                            new BTSetTarget(new GameObject[]{player}, Target),
                            new BTPlayAnimation(animator, "Rifle Walk"),
                            new BTMoveToTarget(Target, WalkSpeed, StopDistance, agent),
                            new BTAttackPlayer(player.GetComponent<Player>(), 1, gameObject),
                            new BTPlayAnimation(animator, "Kick")
                       )
                   })
                });
          

        #endregion


        tree = new BTFallbackNode(new BTBaseNode[2]{
            patrol,
            attack
        });

    }

    private void FixedUpdate()
    {
        tree?.Run();
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Handles.color = Color.yellow;
    //    Vector3 endPointLeft = viewTransform.position + (Quaternion.Euler(0, -ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;
    //    Vector3 endPointRight = viewTransform.position + (Quaternion.Euler(0, ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;

    //    Handles.DrawWireArc(viewTransform.position, Vector3.up, Quaternion.Euler(0, -ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward, ViewAngleInDegrees.Value * 2, SightRange.Value);
    //    Gizmos.DrawLine(viewTransform.position, endPointLeft);
    //    Gizmos.DrawLine(viewTransform.position, endPointRight);

    //}
}
