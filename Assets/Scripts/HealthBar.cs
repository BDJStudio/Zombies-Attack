using UnityEngine;

public class HealthBar : MonoBehaviour {

    MaterialPropertyBlock matBlock;
    MeshRenderer meshRenderer;
    Camera mainCamera;
    EnemyHP enemyHP;
    PlayerHP playerHP;

    public bool isPlayer;

    private void Awake() 
    {
        meshRenderer = GetComponent<MeshRenderer>();
        matBlock = new MaterialPropertyBlock();
        enemyHP = GetComponentInParent<EnemyHP>();
        playerHP = GetComponentInParent<PlayerHP>();
    }

    private void Start() 
    {
        mainCamera = Camera.main;
    }

    private void Update() 
    {
        VerifyDamage(isPlayer);
    }

    public void VerifyDamage(bool x)
    {
        if (!x)
        {
            if (enemyHP.currentHP < enemyHP.originalHP)
            {
                meshRenderer.enabled = true;
                AlignCamera();
                UpdateParams();
            }
            else
            {
                meshRenderer.enabled = false;
            }
        }
        else
        {
            if (playerHP.currentHP < playerHP.originalHP)
            {
                meshRenderer.enabled = true;
                AlignCamera();
                UpdateParams();
            }
            else
            {
                meshRenderer.enabled = false;
            }
        }
    }

    private void UpdateParams() 
    {
        meshRenderer.GetPropertyBlock(matBlock);

        if (!isPlayer)
            matBlock.SetFloat("_Fill", enemyHP.currentHP / (float)enemyHP.originalHP);
        else
            matBlock.SetFloat("_Fill", playerHP.currentHP / (float)playerHP.originalHP);

        meshRenderer.SetPropertyBlock(matBlock);
    }

    // функция слежения healthBar за объектом или осью координат
    private void AlignCamera() 
    {
        if (mainCamera != null) 
        {
            var camXform = mainCamera.transform;
            var forward = transform.position - new Vector3(0, 90, 0);
            forward.Normalize();
            var up = Vector3.Cross(forward, camXform.right);
            transform.rotation = Quaternion.LookRotation(forward, up);
        }
    }

}