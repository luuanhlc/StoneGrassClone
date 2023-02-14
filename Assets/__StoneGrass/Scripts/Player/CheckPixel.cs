using System.Collections;
using UnityEngine;
namespace PT.Garden
{
    public class CheckPixel : MonoBehaviour
    {

        [SerializeField] private Transform target;
        private GrassType grType;
        string gn;

        Texture2D grass;

        private Transform grassPlace;
        Vector2 pixelPos;
        bool _needCheck;
        Renderer ren;
        PoolingObject _pool;
        [SerializeField] private ParticleSystem cutFx;
        public PositionPainter p;

        private void Awake()
        {
            cutFx.Stop();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("GrassPlace"))
                return;
            grassPlace = other.gameObject.transform;
            PlayerController.Ins.grassPlace = grassPlace;
            ren = other.transform.GetComponent<Renderer>();
            PlayerController.Ins._mainMaterial = ren.materials[0];

            _needCheck = true;
            GetGrassTx();
        }
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("GrassPlace"))
                return;
            PlayerController.Ins._mainMaterial = null;
            ren = null;
            _needCheck = false;
        }

        private void GetGrassTx()
        {
            grass = PlayerController.Ins._mainMaterial.GetTexture("_GrassTex") as Texture2D;
        }

        float spamTime;

        float waitTime;
        void FixedUpdate()
        {
            if (Time.frameCount % 2 != 0)
                return;
            if (InGame.Ins != null)
                InGame.Ins.UpdateComplete(); // Cap nhat slider bar Complete;

            if (!PlayerController._isMove || !_needCheck)
            {
                StartCoroutine(turnOff(.15f));
                return;
            }
            if (grassPlace != null)
            {
                pixelPos.x = (((grassPlace.position.x - target.position.x) / grassPlace.localScale.x) + 5);
                pixelPos.y = (((grassPlace.position.z - target.position.z) / grassPlace.localScale.z) + 5);

                if (PlayerController.Ins.tes == null)
                    return;
                var color = PlayerController.Ins.tes.GetPixel((int)(pixelPos.x * 3.2f), (int)(pixelPos.y * 3.2f));
               /* var color2 = PlayerController.Ins.tes.GetPixel((int)((pixelPos.x + 2) * 3.2f), (int)((pixelPos.y  + 2)* 3.2f ));
                var color3 = PlayerController.Ins.tes.GetPixel((int)((pixelPos.x - 2) * 3.2f), (int)((pixelPos.y - 2) * 3.2f));*/

                var icolor = new Color((int)(color.r * 1000), (int)(color.g * 1000), (int)(color.b * 1000), (int)(color.a * 1000));



                if (icolor.r > 2/* && color2.r * 1000 > 2  && color3.r * 1000 > 2*/)
                {
                    //p._isPainting = false;
                    infor.Ins.needcut = false;
                    infor.Ins.speed = infor.Ins.originSpeed;
                    StartCoroutine(turnOff(.15f));
                    return;
                }
                else 
                {
                    if(PlayerDataManager.GetVibraviton())
                        PlayerController.Ins.haptics.PlayFeedbacks();
                    p._isPainting = true;

                    //Debug.Log(this.gameObject.name + " " +  target.position);
                    infor.Ins.needcut = true;
                    
                    RaycastHit hit;
                    if (Physics.Raycast(target.transform.position, Vector3.down, out hit))
                    {
                        GameObject obj = hit.collider.gameObject;

                        if (!obj.CompareTag("GrassPlace"))
                            return;
                        //NiceVibrationsDefineSymbols.Haptic(HapticTypes.RigidImpact);
                        cutFx.Play();
                        grType = obj.GetComponent<GrassType>();
                        int aSpeed = Mathf.Max(PlayerDataManager.GetDamage() - grType.strong, 1);
                        gn = grType.gn;
                        cutFx.startColor = grass.GetPixel((int)(pixelPos.y * 51.2f), (int)(pixelPos.y * 51.2f));

                        //infor.Ins.speed = Mathf.Min(aSpeed, infor.Ins.originSpeed);

                        if (spamTime < Time.time)
                        {
                            InGame.Ins.UpdateComplete(); // Cap nhat slider bar Complete;
                            LobbyManager.Ins._pool.spawmpool("Product" + gn, target, Quaternion.identity, this.transform);
                            spamTime = Time.time + .1f /*4f / infor.Ins.speed*/;
                        }
                    }
                }
            }
        }
        IEnumerator turnOff(float time)
        {
            yield return Yielders.Get(time);
            cutFx.Stop();
        }
    }

    
}