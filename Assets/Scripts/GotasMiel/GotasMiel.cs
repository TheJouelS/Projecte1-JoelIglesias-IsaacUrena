using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotasMiel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != null && collision.tag != "Ground")
        {
            if (collision.tag == "Player")
                collision.GetComponent<PuntosPlayer>().SetScore(false);

            Destroy(this.gameObject);
        }
    }
}
