using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool isSwitch { get; set; }
    Color _color;
    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        _color = _spriteRenderer.color;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Box") && isSwitch == false)
        {
            isSwitch = true;
            _spriteRenderer.color = Color.red;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")|| collision.CompareTag("Box")|| collision.CompareTag("Laser"))
        {
            isSwitch = true;
            _spriteRenderer.color = Color.red;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Box") || collision.CompareTag("Laser"))
        {
            isSwitch = false;
            _spriteRenderer.color = _color;
        }
    }

    private void OnDisable()
    {
        Debug.Log("disable");
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, 1.0f); 

        foreach (Collider2D collision in collisions)
        {
            if (collision.CompareTag("Box")) // 다른 Collider2D가 "Player" 태그를 가진 경우
            {
                isSwitch = true;
                break;
            }
        }
    }

}
