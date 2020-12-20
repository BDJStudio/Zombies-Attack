using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : Damage
{
    public GameObject bulletPrefab;
    public Transform spawnBulletPoint;
    public TrajectoryShow trajectory;

    public int countPoints = 0;

    public float bulletSpeed = 30;
    public float shootPauza = 0.1f;
    public float reloadSpeed = 2;

    public GameObject gameOverPanel;
    public Text gameScore;
    public Text nowScore;
    public Text bestScore;


    private float timeLastShoot = 0; 
    private MyHP thisHP;

    private void Start()
    {
        thisHP = GetComponent<MyHP>();
        gameOverPanel.SetActive(false);

        if (PlayerPrefs.HasKey("BestScore"))
        {
            bestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
        }

        Time.timeScale = 1;
    }

    private void Update()
    {
        Ray ray = new Ray(new Vector3(spawnBulletPoint.position.x, 3, spawnBulletPoint.position.z), spawnBulletPoint.forward);
        RaycastHit hit;
        
        Debug.DrawRay(new Vector3(spawnBulletPoint.position.x,3, spawnBulletPoint.position.z), spawnBulletPoint.forward * 5f, Color.yellow);

        trajectory.ShowTrajectory(spawnBulletPoint.position, spawnBulletPoint.forward * 10f);

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.collider.tag == "Mob")
            {
                trajectory.ShowTrajectory(spawnBulletPoint.position, spawnBulletPoint.forward /1.5f * hit.distance);
                Shooting();
            }
        }

        gameScore.text = countPoints.ToString();

    }

    public override void SetDamage(float damage)
    {
        if (thisHP.SetDamage(damage))
        {

        }
        else
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            nowScore.text = gameScore.text;

            if (int.Parse(bestScore.text) < int.Parse(nowScore.text))
            {
                bestScore.text = nowScore.text;
                PlayerPrefs.SetInt("BestScore", int.Parse(gameScore.text));
            }
            else
                bestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
        }

    }

    void Shooting()
    {
        if (Time.time > timeLastShoot + shootPauza)
        {
            GameObject newBullet = Instantiate(bulletPrefab, spawnBulletPoint.position, Quaternion.identity);
            Rigidbody newBulletRigidbody = newBullet.GetComponent<Rigidbody>();
            newBulletRigidbody.AddForce(spawnBulletPoint.forward * bulletSpeed, ForceMode.VelocityChange);

            timeLastShoot = Time.time;
        }
    }
}
