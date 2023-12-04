using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

 public partial class Item : MonoBehaviour
{
    private float _dropSpeed = 3f;
    private Rigidbody2D _rb;

    public float GetSpeed => (_dropSpeed);

    [SerializeField] private Items itemType;

    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject balls;
    private ArkanoidBall _mainBall;
    private float _originSpeed;



    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Managers.Game.State == GameState.Pause)
        {
            _rb.velocity = Vector3.zero;
            return;
        }

        _rb.velocity = _dropSpeed * Vector3.down;
        //transform.position += new Vector3(0, -_dropSpeed, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SFX.Instance.PlayOneShot(SFX.Instance.itemPickup);
            ItemSkill(collision.gameObject);

            //Destroy(gameObject);
        }
    }

    private void ItemSkill(GameObject player)
    {
        switch (itemType)
        {
            case Items.Player:
                // 목숨 추가
                break;

            case Items.Lasers:
                // 클릭시 2발씩 발사
                LasersItemUse(player);
                break;

            case Items.Enlarge:
                // 패들이 1.5배 커짐(가로)
                EnalargeItemUse(player);
                break;

            case Items.Catch:
                // 공이 튕기지않고 패들에 달라붙음
                CatchItemUse();
                break;

            case Items.Slow:
                // 공 속도 감소
                SlowItemUse();
                break;

            case Items.Disruption:
                // 공 2개 추가
                DisruptionItemUse();
                break;
            case Items.Power:
                Managers.Skill.PowerUp();
            break;

        }
    }

    private void DisruptionItemUse()
    {
        Instantiate(balls);
    }

    private void SlowItemUse()
    {
        _mainBall = Managers.Game.CurrentBalls[0].GetComponent<ArkanoidBall>();
        _originSpeed = _mainBall.ballMaxSpeed;
        _mainBall.SetMaxSpeed(_originSpeed / 2);
        StartCoroutine(OriginBallSpeed());
    }

    IEnumerator OriginBallSpeed()
    {
        yield return new WaitForSeconds(2f);
        _mainBall.SetMaxSpeed(_originSpeed);
    }



    private void LasersItemUse(GameObject player)
    {
        var bullet1 = Instantiate(bullet, player.transform);
        bullet1.transform.position += new Vector3(-0.5f, 1f, 0f);
        var bullet2 = Instantiate(bullet, player.transform);
        bullet2.transform.position += new Vector3(0.5f, 1f, 0f);
    }

    private void EnalargeItemUse(GameObject player)
    {
        var playerScale = player.transform.localScale;
        player.transform.localScale = new Vector3(playerScale.x * 1.5f, playerScale.y, playerScale.z);
    }


}