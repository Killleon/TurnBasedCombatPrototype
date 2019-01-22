using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// A class for running animation coroutines because the combat states cannot inherit from Monobehaviour (because I am using "new" keyword to change states).
/// </summary>
public class CombatAnimations : MonoBehaviour {

    public void AttackAnimationCaller(CombatManager combatManager, BaseUnit target)
    {
        StartCoroutine(AttackAnimation(combatManager, target));
    }

    public IEnumerator AttackAnimation(CombatManager combatManager, BaseUnit target)
    {
        Transform currentPos = combatManager.CurrentUnit.transform;
        Vector3 startinPos = currentPos.position;
        Sequence attackSequence = DOTween.Sequence();
        Tween strike = currentPos.DOMove(new Vector3(target.transform.position.x - 0.5f, target.transform.position.y, target.transform.position.z), 0.3f).SetEase(Ease.OutCubic);
        Tween fallback = currentPos.DOMove(new Vector3(startinPos.x, startinPos.y, startinPos.z), 0.3f).SetEase(Ease.OutCubic);
        
        attackSequence.Append(strike)
            .AppendInterval(1f)
            .Append(fallback);

        Debug.Log("BOOM");

        yield return new WaitForSeconds(1f);
        
    }

    void Start () {
		
	}
	
	void Update () {
		
	}
}
