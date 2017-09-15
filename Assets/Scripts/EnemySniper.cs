using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySniper : MonoBehaviour
{
    private Animator anim;

    public static Button[] img;
    private static int index;
    public int thisIndex;
    private bool shoot;

    private Vector3 offset;
    
    private void Start()
    {
        shoot = true;
        offset = new Vector3(0, 2, 0);
        anim = GetComponent<Animator>();
        if (img == null)
        {
            img = new Button[6]; 
        }

        if (index == 0)
        {
            thisIndex = index;
            index++;
        }
        else
        {
            thisIndex = index;
            index++;
        }

        img[thisIndex] = GameObject.Find("ReticleImage0" + index.ToString()).GetComponent<Button>();
    }

    private void Update()
    {
        if (GameManager.Instance.player != null)
        {
            if (transform.position.x - GameManager.Instance.player.position.x < 60 && transform.position.x - GameManager.Instance.player.position.x > -40)
            {
                transform.LookAt(GameManager.Instance.player.position, Vector3.up);

                Vector2 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position + offset);

                if (!img[thisIndex].enabled)
                {
                    img[thisIndex].enabled = true;
                    img[thisIndex].image.enabled = true;
                }

                img[thisIndex].image.rectTransform.position = new Vector2(screenPoint.x, screenPoint.y);
            }
            else
            {
                img[thisIndex].enabled = false;
                img[thisIndex].image.enabled = false;
            }

            if (transform.position.x - GameManager.Instance.player.position.x < 30 && transform.position.x - GameManager.Instance.player.position.x > -40 && shoot)
            {
                anim.SetTrigger("Shoot");
                shoot = false;
                StartCoroutine(WaitToShoot(3.0f));
            }
        }
    }

    IEnumerator WaitToShoot(float _time)
    {
        yield return new WaitForSeconds(_time);
        shoot = true;
    }
}