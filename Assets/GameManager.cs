using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [SerializeField] private AssetReference environement;

    private Object instantiatedPrefab;
    private AsyncOperationHandle handlePrefab;

    private bool loaded = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Debug.Log("Exit");
            Application.Quit();
        }
        if (Input.GetKey("l") && loaded == false)
        {
            loaded = true;
            Debug.Log("Try to load to load");
            //instantiatedPrefab = Instantiate(environment, transform.position, Quaternion.identity);
            environement.LoadAssetAsync<GameObject>().Completed +=  handle => {
                Debug.Log("call back handle.Status "+ handle.Status);
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    handlePrefab = handle;
                    instantiatedPrefab = Instantiate(handle.Result);
                }
                else
                {
                    Debug.Log("Failed to load");
                }
            };
            
        }
        if (Input.GetKey("u") && loaded == true)
        {
            if (handlePrefab.IsValid() && instantiatedPrefab != null)
            {
                Destroy(instantiatedPrefab);
                Addressables.Release(handlePrefab);
                loaded = false;
                instantiatedPrefab = null;
            }
        }
    }
}
