using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    // Monster가 Attack 애니메이션을 실행한 후 실행할 로직
    public void Ate()
    {
        Transform monsterTransform = transform.parent;
        Monster m = monsterTransform.gameObject.GetComponent<Monster>();
        // 낮이면
        if(TimeManager.instance.GetisDay())
            m.OffTransformation();
        // 항상
        m.TransitionCamera(false);
    }
}
