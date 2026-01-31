using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class PlayerAnimatorSetup : MonoBehaviour
{
    [MenuItem("Tools/Create Player Animator Controller")]
    public static void CreatePlayerAnimatorController()
    {
        // Crea la cartella se non esiste
        if (!AssetDatabase.IsValidFolder("Assets/Animations"))
        {
            AssetDatabase.CreateFolder("Assets", "Animations");
        }

        // Crea l'Animator Controller
        AnimatorController controller = AnimatorController.CreateAnimatorControllerAtPath("Assets/Animations/PlayerAnimator.controller");

        // Aggiungi i parametri
        // Triggers per le animazioni
        controller.AddParameter("Idle", AnimatorControllerParameterType.Trigger);
        controller.AddParameter("Block", AnimatorControllerParameterType.Trigger);
        controller.AddParameter("Crouch", AnimatorControllerParameterType.Trigger);
        controller.AddParameter("Jump", AnimatorControllerParameterType.Trigger);
        controller.AddParameter("Hit", AnimatorControllerParameterType.Trigger);
        controller.AddParameter("Down", AnimatorControllerParameterType.Trigger);
        controller.AddParameter("GetUp", AnimatorControllerParameterType.Trigger);
        controller.AddParameter("Punch", AnimatorControllerParameterType.Trigger);
        controller.AddParameter("Kick", AnimatorControllerParameterType.Trigger);
        controller.AddParameter("LowHit", AnimatorControllerParameterType.Trigger);
        controller.AddParameter("Special", AnimatorControllerParameterType.Trigger);

        // Bool per stati continui
        controller.AddParameter("IsGrounded", AnimatorControllerParameterType.Bool);
        controller.AddParameter("IsBlocking", AnimatorControllerParameterType.Bool);
        controller.AddParameter("IsCrouching", AnimatorControllerParameterType.Bool);

        // Ottieni il layer base
        AnimatorControllerLayer baseLayer = controller.layers[0];
        AnimatorStateMachine stateMachine = baseLayer.stateMachine;

        // Crea gli stati
        AnimatorState idleState = stateMachine.AddState("Idle", new Vector3(0, 0, 0));
        AnimatorState blockState = stateMachine.AddState("Block", new Vector3(250, 50, 0));
        AnimatorState crouchState = stateMachine.AddState("Crouch", new Vector3(250, 100, 0));
        AnimatorState jumpState = stateMachine.AddState("Jump", new Vector3(250, 150, 0));
        AnimatorState hitState = stateMachine.AddState("Hit", new Vector3(500, 0, 0));
        AnimatorState downState = stateMachine.AddState("Down", new Vector3(500, 50, 0));
        AnimatorState getUpState = stateMachine.AddState("GetUp", new Vector3(500, 100, 0));
        AnimatorState punchState = stateMachine.AddState("Punch", new Vector3(0, 150, 0));
        AnimatorState kickState = stateMachine.AddState("Kick", new Vector3(0, 200, 0));
        AnimatorState lowHitState = stateMachine.AddState("LowHit", new Vector3(0, 250, 0));
        AnimatorState specialState = stateMachine.AddState("Special", new Vector3(0, 300, 0));

        // Imposta Idle come stato di default
        stateMachine.defaultState = idleState;

        // Crea transizioni da Any State per i trigger
        AddAnyStateTransition(stateMachine, blockState, "Block");
        AddAnyStateTransition(stateMachine, crouchState, "Crouch");
        AddAnyStateTransition(stateMachine, jumpState, "Jump");
        AddAnyStateTransition(stateMachine, hitState, "Hit");
        AddAnyStateTransition(stateMachine, downState, "Down");
        AddAnyStateTransition(stateMachine, getUpState, "GetUp");
        AddAnyStateTransition(stateMachine, punchState, "Punch");
        AddAnyStateTransition(stateMachine, kickState, "Kick");
        AddAnyStateTransition(stateMachine, lowHitState, "LowHit");
        AddAnyStateTransition(stateMachine, specialState, "Special");

        // Transizioni di ritorno a Idle
        AddExitTransition(punchState, idleState);
        AddExitTransition(kickState, idleState);
        AddExitTransition(lowHitState, idleState);
        AddExitTransition(specialState, idleState);
        AddExitTransition(hitState, idleState);
        AddExitTransition(getUpState, idleState);
        AddExitTransition(jumpState, idleState, "IsGrounded", true);

        // Block e Crouch tornano a Idle quando il bool diventa false
        AnimatorStateTransition blockToIdle = blockState.AddTransition(idleState);
        blockToIdle.AddCondition(AnimatorConditionMode.IfNot, 0, "IsBlocking");
        blockToIdle.hasExitTime = false;
        blockToIdle.duration = 0.1f;

        AnimatorStateTransition crouchToIdle = crouchState.AddTransition(idleState);
        crouchToIdle.AddCondition(AnimatorConditionMode.IfNot, 0, "IsCrouching");
        crouchToIdle.hasExitTime = false;
        crouchToIdle.duration = 0.1f;

        // Down va a GetUp
        AnimatorStateTransition downToGetUp = downState.AddTransition(getUpState);
        downToGetUp.AddCondition(AnimatorConditionMode.If, 0, "GetUp");
        downToGetUp.hasExitTime = false;
        downToGetUp.duration = 0.1f;

        // Salva le modifiche
        EditorUtility.SetDirty(controller);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Player Animator Controller creato in Assets/Animations/PlayerAnimator.controller");
        Debug.Log("Ora devi aggiungere le Animation Clips a ogni stato!");
    }

    private static void AddAnyStateTransition(AnimatorStateMachine stateMachine, AnimatorState targetState, string triggerName)
    {
        AnimatorStateTransition transition = stateMachine.AddAnyStateTransition(targetState);
        transition.AddCondition(AnimatorConditionMode.If, 0, triggerName);
        transition.hasExitTime = false;
        transition.duration = 0.05f;
        transition.canTransitionToSelf = false;
    }

    private static void AddExitTransition(AnimatorState fromState, AnimatorState toState)
    {
        AnimatorStateTransition transition = fromState.AddTransition(toState);
        transition.hasExitTime = true;
        transition.exitTime = 1f;
        transition.duration = 0.1f;
    }

    private static void AddExitTransition(AnimatorState fromState, AnimatorState toState, string boolName, bool boolValue)
    {
        AnimatorStateTransition transition = fromState.AddTransition(toState);
        transition.AddCondition(boolValue ? AnimatorConditionMode.If : AnimatorConditionMode.IfNot, 0, boolName);
        transition.hasExitTime = false;
        transition.duration = 0.1f;
    }
}
