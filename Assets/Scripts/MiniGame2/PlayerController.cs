namespace MiniGame2 
{
   using UnityEngine;

   public class PlayerController : MonoBehaviour 
   {
       public SPUM_Prefabs spumPrefab;

       void Update() 
       {
           // 마우스 위치를 월드 좌표로 변환
           Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           
           // 캐릭터가 마우스의 x축 위치에 따라 좌우 방향 전환
           if(mousePosition.x > transform.position.x)
           {
               transform.parent.localScale = new Vector3(2f, 2f, 2f); // 오른쪽 보기
           }
           else
           {
               transform.parent.localScale = new Vector3(-2f, 2f, 2f); // 왼쪽 보기
           }

           // 기본 idle 애니메이션 재생
           spumPrefab._anim.SetBool("Run", false);
       }
   }
}