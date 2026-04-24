using UnityEngine;
using UnityEngine.AI;

public class ChickAutoSitPerfect : MonoBehaviour
{
    public Transform chairTarget;       // 의자 위치
    public float alignSpeed = 5f;       // 위치 보정 속도
    public float rotateSpeed = 10f;     // 회전 보정 속도

    private Animator animator;
    private NavMeshAgent agent;

    private bool isSitting = false;
    private bool hasPositionAligned = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (agent != null && chairTarget != null)
        {
            agent.stoppingDistance = 0.3f;
            agent.isStopped = false;
            agent.SetDestination(chairTarget.position);
        }
    }

    void Update()
    {
        if (animator == null || agent == null) return;

        // 이동 속도 Animator에 전달
        animator.SetFloat("Speed", agent.velocity.magnitude);

        // 목적지 도착 체크
        if (!isSitting && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            isSitting = true;
            agent.isStopped = true;
            animator.SetFloat("Speed", 0);
            animator.SetTrigger("SitDown");
            UnityEngine.Debug.Log("SitDown Trigger Set");
        }

        // 앉기 상태 진행
        if (isSitting && !hasPositionAligned)
        {
            AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

            // 현재 애니메이션이 SitDown 상태인지 확인
            if (state.IsName("SitDown"))
            {
                // 애니메이션 끝까지 기다리기
                if (state.normalizedTime >= 1.0f)
                {
                    // 최종 위치와 회전 정확히 고정
                    transform.position = chairTarget.position;
                    transform.rotation = chairTarget.rotation;
                    hasPositionAligned = true;
                    UnityEngine.Debug.Log("Position Aligned");

                    // NavMeshAgent 완전 비활성화
                    agent.enabled = false;

                   
                }
                else
                {
                    // 애니 진행 중 부드럽게 위치와 회전 조정
                    transform.position = Vector3.Lerp(transform.position, chairTarget.position, alignSpeed * Time.deltaTime);
                    transform.rotation = Quaternion.Slerp(transform.rotation, chairTarget.rotation, rotateSpeed * Time.deltaTime);
                }
            }
            else
            {
                // SitDown 상태가 아닌 경우에도 위치 정렬 시도 (예: 트리거로 들어간 직후)
                transform.position = Vector3.Lerp(transform.position, chairTarget.position, alignSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, chairTarget.rotation, rotateSpeed * Time.deltaTime);
            }
        }
    }
}
