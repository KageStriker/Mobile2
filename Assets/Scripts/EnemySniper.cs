using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySniper : MonoBehaviour
{
    private Animator anim;
    private Canvas retCanvas;
    private Image img;
    public Sprite reticle;

    
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(retCanvas != null && img.sprite == null)
        {
            retCanvas = GameObject.Find("ReticleCanvas").GetComponent<Canvas>();

            img.sprite = reticle;
            img = retCanvas.gameObject.AddComponent<Image>();
        }

        if (GameManager.Instance.player != null && img != null)
        {
            if (transform.position.x - GameManager.Instance.player.position.x < 40 && transform.position.x - GameManager.Instance.player.position.x > -30)
            {
                transform.LookAt(GameManager.Instance.player.position, Vector3.up);
                
                img.enabled = true;

                Vector2 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);

                img.rectTransform.position = new Vector2(screenPoint.x, screenPoint.y);
            }
            else
            {
                img.enabled = false;
            } 
        }
    }
}