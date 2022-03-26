using Cinemachine;
using UnityEngine;

namespace Utilities.CinemachineExtensions
{
    public class MainCameraValidator : MonoBehaviour
    {
        public GameObject mainCameraPrefab;

        private void Awake()
        {
            var cam = Camera.main;
            if (cam == null)
            {
                SpawnNewMainCamera();
                return;
            }

            if (!cam.gameObject.TryGetComponent<CinemachineBrain>(out var brain))
            {
                SpawnNewMainCamera();
                return;
            }
        }

        void SpawnNewMainCamera()
        {
            if (Camera.main != null) Destroy(Camera.main.gameObject);
            Instantiate(mainCameraPrefab);
        }
    }
}
