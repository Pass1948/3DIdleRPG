using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("Ž��/���� ����")]
    [SerializeField] private string enemyTag = "Enemy";
    [SerializeField] private float sightRadius = 12f;   // �߰� �ݰ�
    [SerializeField] private float attackRange = 2.2f;  // ���� ��Ÿ�
    [SerializeField] private float attackCooldown = 1.0f;

    [Header("�̵�/ȸ��")]
    [SerializeField] private float rotateSpeed = 10f;   // Ÿ�������� ȸ�� �ӵ�

    Idle idle;
    Chase chase; 
    Attack attack;

    Transform target;
    PlayerStat stat;
    float speed => stat ? stat.Get(StatType.Speed) : 3.5f;
    float atk => stat ? stat.Get(StatType.ATK) : 10f;

    private void Awake()
    {
        stat = GetComponent<PlayerStat>(); // ������ null �� �⺻ġ ���
        // ���� �ν��Ͻ� ����
        idle = new Idle(this);
        chase = new Chase(this);
        attack = new Attack(this);
        GameManager.StateMachine.ChangeState(idle);
    }
    // ====== ��ƿ ======
    public bool HasTarget() => target != null;

    public bool InSight(Transform t)
        => t && (t.position - transform.position).sqrMagnitude <= sightRadius * sightRadius;

    public bool InAttackRange(Transform t)
        => t && (t.position - transform.position).sqrMagnitude <= attackRange * attackRange;

    public void MoveTo(Vector3 worldPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, worldPos, speed * Time.deltaTime);
        FaceTo(worldPos);
    }

    public void FaceTo(Vector3 worldPos)
    {
        var dir = worldPos - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.0001f) return;
        var to = Quaternion.LookRotation(dir.normalized, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, to, rotateSpeed * Time.deltaTime);
    }

    public void ClearTarget() => target = null;

    public bool TryAcquireTarget()
    {
        var enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        Transform best = null;
        float bestSq = float.PositiveInfinity;
        var self = transform.position;

        for (int i = 0; i < enemies.Length; i++)
        {
            var tr = enemies[i].transform;
            float sq = (tr.position - self).sqrMagnitude;
            if (sq < bestSq && sq <= sightRadius * sightRadius)
            {
                bestSq = sq; best = tr;
            }
        }
        target = best;
        return target != null;
    }

    public bool IsTargetAlive()
    {
        if (!target) return false;
        var hp = target.GetComponent<EnemyStat>(); // ������Ʈ�� �°� ����
        return hp ? hp.isAlive : true;
    }

    public void DoAttack()
    {
        if (!target) return;
        FaceTo(target.position);

        var hp = target.GetComponent<EnemyStat>();
       /* if (hp)
        {
            hp.ApplyDamage(Attack);
            if (!hp.IsAlive) ClearTarget();
        }*/
    }

    // ====== ���µ� ======
    class Idle : IState
    {
        readonly PlayerStateMachine o;
        float scanTimer;
        const float ScanInterval = 0.25f;

        public Idle(PlayerStateMachine o) => this.o = o;
        public void OnEnter() { scanTimer = 0f; }
        public void OnExit() { }

        public void OnUpdate()
        {
            scanTimer -= Time.deltaTime;
            if (scanTimer <= 0f)
            {
                scanTimer = ScanInterval;
                if (o.TryAcquireTarget())
                {
                    GameManager.StateMachine.ChangeState(o.chase);
                    return;
                }
            }
            // �ʿ� �� ���/���� ����
        }

    }

    class Chase : IState
    {
        readonly PlayerStateMachine o;
        public Chase(PlayerStateMachine o) => this.o = o;

        public void OnEnter() { }
        public void OnExit() { }

        public void OnUpdate()
        {
            if (!o.HasTarget() || !o.IsTargetAlive())
            {
                o.ClearTarget();
                GameManager.StateMachine.ChangeState(o.idle);
                return;
            }

            if (o.InAttackRange(o.target))
            {
                GameManager.StateMachine.ChangeState(o.attack);
                return;
            }

            o.MoveTo(o.target.position);

            if (!o.InSight(o.target))
            {
                o.ClearTarget();
                GameManager.StateMachine.ChangeState(o.idle);
            }
        }
    }

    class Attack : IState
    {
        readonly PlayerStateMachine o;
        float cd;

        public Attack(PlayerStateMachine o) => this.o = o;
        public void OnEnter() { cd = 0f; }
        public void OnExit() { }

        public void OnUpdate()
        {
            if (!o.HasTarget() || !o.IsTargetAlive())
            {
                o.ClearTarget();
                GameManager.StateMachine.ChangeState(o.idle);
                return;
            }

            if (!o.InAttackRange(o.target))
            {
                GameManager.StateMachine.ChangeState(o.chase);
                return;
            }

            cd -= Time.deltaTime;
            if (cd <= 0f)
            {
                o.DoAttack();
                cd = o.attackCooldown;
            }
        }
    }
}
