using UnityEngine;

public class DeathFall : MonoBehaviour
{
    [SerializeField] private LevelLoader levelLoader;

   void OnCollisionEnter2D(Collision2D collision)
    {
        levelLoader.ReloadLevel();
    }
}
