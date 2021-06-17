using UnityEngine;

/// <summary>
/// Used to control the bullet shot from the player gun.
/// If the bullet hits the environment, it will spawn a decal at that position with it's direction parallel to the normal of the contact point.
/// </summary>
public class BulletController : MonoBehaviour
{

    [SerializeField, Tooltip("Decal that is spawned when bullet hits environment.")]
    private GameObject bulletDecal;
    [SerializeField, Tooltip("Speed of the bullet traveling through the air.")]
    private float speed = 50f;
    [SerializeField, Tooltip("How long to destroy the bullet if it has not hit something.")]
    private float timeToDestroy = 3f;

    
    public Vector3 target { get; set; }
    public bool hit { get; set; }

    private void OnEnable() {
        // Destroy the gameobject after x seconds when it is enabled.
        Destroy(gameObject, timeToDestroy);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        // If there is no raycast target, the bullet is destroyed when it reaches a certain distance from the player.
        if (!hit && Vector3.Distance(transform.position, target) < .01f) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {
        // Spawn a bullet hole decal at the point of contact with the environment. The decal is rotated to match the normal of the contact point.
        ContactPoint contact = other.GetContact(0);
        GameObject.Instantiate(bulletDecal, contact.point + contact.normal * .0001f, Quaternion.LookRotation(contact.normal));
        Destroy(gameObject);

        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

    
}
