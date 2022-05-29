using UnityEngine;
using Unity.Netcode;

public class WeaponAim : NetworkBehaviour
{

    #region Variables

    [SerializeField] Transform crossHair;
    [SerializeField] Transform weapon;
    [SerializeField] GameObject bullet;
    SpriteRenderer weaponRenderer;
    InputHandler handler;

    #endregion

    #region Unity Event Functions

    private void Awake()
    {
        handler = GetComponent<InputHandler>();
        weaponRenderer = weapon.gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {

        handler.OnMousePosition.AddListener(UpdateCrosshairPosition);
        handler.OnFire.AddListener(Shoot);
    }

    private void OnDisable()
    {
        handler.OnMousePosition.RemoveListener(UpdateCrosshairPosition);
        handler.OnFire.RemoveListener(Shoot);
    }

    #endregion

    #region Methods

    void Shoot()
    {
        Debug.Log("Shoot");
        Vector3 offset = new Vector3(weapon.right.x * 0.3f, weapon.right.y * 0.3f);
        Instantiate(bullet, weapon.position + offset, weapon.rotation);
    }

    void UpdateCrosshairPosition(Vector2 input)
    {

        // https://docs.unity3d.com/2020.3/Documentation/ScriptReference/Camera.ScreenToWorldPoint.html
        var worldMousePosition = Camera.main.ScreenToWorldPoint(input);
        var facingDirection = worldMousePosition - transform.position;
        var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
        if (aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }

        SetCrossHairPosition(aimAngle);

        UpdateWeaponOrientation();

    }

    void UpdateWeaponOrientation()
    {
        weapon.right = crossHair.position - weapon.position;

        if (crossHair.localPosition.x > 0)
        {
            weaponRenderer.flipY = false;
        }
        else
        {
            weaponRenderer.flipY = true;
        }
    }

    void SetCrossHairPosition(float aimAngle)
    {

        var x = transform.position.x + .5f * Mathf.Cos(aimAngle);
        var y = transform.position.y + .5f * Mathf.Sin(aimAngle);

        var crossHairPosition = new Vector3(x, y, 0);
        crossHair.transform.position = crossHairPosition;
    }

    #endregion

}
