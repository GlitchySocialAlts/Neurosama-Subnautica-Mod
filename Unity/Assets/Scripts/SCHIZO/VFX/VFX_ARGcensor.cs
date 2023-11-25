using SCHIZO.VFX;
using UnityEngine;

public class VFX_ARGCensor : MonoBehaviour
{
    public T2DArray t2da;
    private Texture2DArray texture2DArray;

    public float fadeOutStartDistance = 35f;
    public float scale = 1.3f;
    public float frameChangeInterval = 0.08f;

    private MatPassID matPassID;

    private float lastUpdate;
    private float lastRnd = 0f;

    public void Awake()
    {
        _ = SchizoVFXStack.VFXStack;

        texture2DArray = t2da.PopulateTexture2DArray();
        texture2DArray.wrapMode = TextureWrapMode.Clamp;

        matPassID = new MatPassID(VFXMaterialHolder.instance.GetMaterialForEffect(Effects.ARGCensor));
        matPassID.SetTexture("_Images", texture2DArray);

        lastUpdate = Time.time;
    }

    public void LateUpdate()
    {
        Vector3 dirToCam = (Camera.main.transform.position - transform.position).normalized;
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position + dirToCam);

        float dot = Vector3.Dot(transform.forward, dirToCam);

        bool isBelowWaterLevel = Camera.main.transform.position.y < 0f;

        if (isBelowWaterLevel && pos.z > 0 && dot > -0.3f)
        {
            if (Time.time - lastUpdate > frameChangeInterval)
            {
                lastRnd = Random.Range(0, texture2DArray.depth) * Time.timeScale;
                lastUpdate = Time.time;
            }

            float opacity = Mathf.Clamp01((1f / pos.z) * fadeOutStartDistance);

            matPassID.SetVector("_ScreenPosition", new Vector4(pos.x, pos.y, pos.z, lastRnd));
            matPassID.SetFloat("_Strength", opacity);
            matPassID.SetFloat("_Scale", scale);

            SchizoVFXStack.RenderEffect(matPassID);
        }
    }
}
