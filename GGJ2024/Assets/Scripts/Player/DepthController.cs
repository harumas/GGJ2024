using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Utility;
public class DepthController : MonoBehaviour
{
    [SerializeField] private Volume volume;
    private DepthOfField depthOfField;

    private void Awake()
    {
        Locator.Register(this);
    }

    public void SetDepth(bool isFocus)
    {
        if (volume.profile.TryGet<DepthOfField>(out depthOfField))
        {
            depthOfField.active = isFocus;
        }
    }
}
