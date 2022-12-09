using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class Rogue : MonoBehaviour
{
    private BTBaseNode tree;
    private NavMeshAgent agent;
    private Animator animator;

    private Blackboard blackboard = new Blackboard();
    [SerializeReference] public BaseScriptableObject[] variables;
    [SerializeField] private GameObject[] hidingPlaces;
    [SerializeField] private GameObject bombPrefab;
    private GameObject player;
    public UIElement UI;

    public VariableGameObject Target
    {
        get { return Target = blackboard.GetVariable<VariableGameObject>("VariableGameObject_Rogue_Target"); }
        set { blackboard.dictionary["VariableGameObject_Rogue_Target"] = value; }
    }
    public VariableFloat WalkSpeed
    {
        get { return WalkSpeed = blackboard.GetVariable<VariableFloat>("VariableFloat_Rogue_WalkSpeed"); }
        set { blackboard.dictionary["VariableFloat_Rogue_WalkSpeed"] = value; }
    }
    public VariableFloat StopDistance
    {
        get { return StopDistance = blackboard.GetVariable<VariableFloat>("VariableFloat_Rogue_StoppingDistance"); }
        set { blackboard.dictionary["VariableFloat_Rogue_StoppingDistance"] = value; }
    }
    public VariableFloat SightRange
    {
        get { return StopDistance = blackboard.GetVariable<VariableFloat>("VariableFloat_Rogue_SightRange"); }
        set { blackboard.dictionary["VariableFloat_Rogue_SightRange"] = value; }
    }
    public VariableFloat ViewAngleInDegrees
    {
        get { return StopDistance = blackboard.GetVariable<VariableFloat>("VariableFloat_Rogue_ViewAngleInDegrees"); }
        set { blackboard.dictionary["VariableFloat_Rogue_ViewAngleInDegrees"] = value; }
    }
    public VariableFloat AttackRange
    {
        get { return StopDistance = blackboard.GetVariable<VariableFloat>("VariableFloat_Rogue_AttackRange"); }
        set { blackboard.dictionary["VariableGameObject_Rogue_AttackRange"] = value; }
    }

    BTBaseNode followPlayer;
    BTBaseNode hide;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        player = FindObjectOfType<Player>().gameObject;

        foreach (BaseScriptableObject variable in variables)
        {
            blackboard.AddVariable(variable.name, variable);
        }
    }

    private void Start()
    {
        //follow player sequence
        followPlayer = new BTParallelNode(new BTBaseNode[4]{
                            new BTUpdateUI(UI, "Following Player"),
                            new BTInverterNode(new BTCheckEnemyStatus(FindObjectOfType<Guard>(), player)),
                            new BTPlayAnimation(animator, "Walk Crouch"),
                            new BTFollowPlayer(player, agent)
        });

        //hide sequence
        hide = new BTSequenceNode(
                    new BTUpdateUI(UI, "Hiding"),
                    new BTFindCover(transform, Target, hidingPlaces),
                    new BTPlayAnimation(animator, "Walk Crouch"),
                    new BTHide(FindObjectOfType<Guard>(), Target, WalkSpeed, agent),
                    new BTWaitNode(2),
                    new BTPlayAnimation(animator, "Crouch Idle"),
                    new BTUpdateUI(UI, "Throwing Bomb"),
                    new BTThrowBomb(bombPrefab, player, 2)
               );

        tree = new BTFallbackNode(new BTBaseNode[1]
        {
            //followPlayer,
            hide
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
