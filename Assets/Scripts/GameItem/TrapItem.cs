using Kamgam.BikeAndCharacter25D;
using System.Collections;
using UnityEngine;

public class TrapItem : MonoBehaviour
{
    public GameObject ExplosionParticle;
    public AudioSource ExplosionSFX;

    private BikeAndCharacter bikeController;
    public Rigidbody2D mRigidBody;
    private bool isExploded;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("bike"))
        {
            ExplosionParticle.transform.position = collision.contacts[0].point;
            bikeController = collision.gameObject.GetComponentInParent<BikeAndCharacter>();
            mRigidBody = bikeController.Bike.GetComponent<Rigidbody2D>();
            if (!bikeController.Pause && !isExploded)
            {
                Explode();
                isExploded = true;
            }
        }
    }

    private void Explode()
    {
        bikeController.PauseBike(true);
        bikeController.DisconectCharacter();
        bikeController.AddForce(new Vector2(0, 500f));
        ExplosionParticle.SetActive(true);
        ExplosionParticle.GetComponent<ParticleSystem>().Play();

        //this.mVehicleAudio.enabled = false;

        ExplosionSFX.Play();
        StartCoroutine(ExplosionTimer());
    }

    private IEnumerator ExplosionTimer()
    {
        yield return new WaitForSeconds(3f);

        isExploded = false;
        yield break;
    }
}
