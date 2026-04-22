using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;
using UnityStandardAssets.Characters.FirstPerson;

public class WinTrigger : MonoBehaviour 
{
    [Header("Win Requirements")]
    public string nextSceneName; 
    public int requiredFuel = 3;

    [Header("Cutscene Elements")]
    public Image fadeImage;
    public Camera birdsEyeCam;
    public AudioSource heliAudio;
    public Animator heliAnimator; 
    public GameObject cutSceneZombie; 

    [Header("Camera Follow Settings")]
    public Vector3 offset = new Vector3(0, 8, -20); 
    public Vector3 rotationOffset = new Vector3(20, 0, 0); 

    private void OnTriggerEnter(Collider other)
    {
        PlayerManager pm = other.GetComponent<PlayerManager>();
        
        if (pm != null && pm.fuelCount >= requiredFuel)
        {
            StartCoroutine(PlayWinSequence(other.gameObject));
        }
    }

    IEnumerator PlayWinSequence(GameObject player)
    {
        if (cutSceneZombie != null) 
        {
            cutSceneZombie.SetActive(true);
            foreach (var agent in cutSceneZombie.GetComponentsInChildren<UnityEngine.AI.NavMeshAgent>()) agent.enabled = false;
            foreach (var anim in cutSceneZombie.GetComponentsInChildren<Animator>()) anim.enabled = false;
            foreach (var health in cutSceneZombie.GetComponentsInChildren<ZombieHealth>()) health.enabled = false;
        }

     
        player.SetActive(false);
        birdsEyeCam.gameObject.SetActive(true);

        fadeImage.color = new Color(0, 0, 0, 1); 
        fadeImage.DOFade(0, 1.5f);
        yield return new WaitForSeconds(2f);

        if (heliAudio != null) heliAudio.Play();
        if (heliAnimator != null) heliAnimator.SetTrigger("FlyAway");

        birdsEyeCam.transform.SetParent(heliAnimator.transform);

        birdsEyeCam.transform.DOLocalMove(offset, 4f).SetEase(Ease.OutSine);
        birdsEyeCam.transform.DOLocalRotate(rotationOffset, 4f).SetEase(Ease.OutSine);

   
        yield return new WaitForSeconds(10f);

    
        fadeImage.DOFade(1, 2f);
        yield return new WaitForSeconds(2.5f);

       
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(nextSceneName);
    }
} 