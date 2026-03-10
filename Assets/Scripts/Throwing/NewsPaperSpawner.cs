using UnityEngine;

public class NewsPaperSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _newspaperPrefab;
    [SerializeField] private Transform _spawnPosition;

    public void ThrowNewspaper()
    {
        GameObject go = Instantiate(_newspaperPrefab, _spawnPosition.position, Quaternion.identity);
        //add to pool
    }
}
