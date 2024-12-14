using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerHP : MonoBehaviour
{
    public int maxHP = 10; // Player's maximum HP
    [SerializeField] int currentHP, starCount, oldStarCount;
    [SerializeField] Animator animOnHit, animOnStar;
    [SerializeField] VisualEffect starVfx;
    [SerializeField] ParticleSystem starOnCollect;
    [SerializeField] float currentStarSpeed, targetStarSpeed, tweeningStrength;

    public void TakeDamage(int amount)
    {
        if (currentHP > 0)
        {
            currentHP -= amount;
            if (currentHP < 0) { currentHP = 0; }
        }
        else currentHP = 0;
        starCount = currentHP / 3;
        if(targetStarSpeed<=0){
          targetStarSpeed=-targetStarSpeed-0.15f;
        }
        else targetStarSpeed=-targetStarSpeed+0.15f;
        animOnHit.ResetTrigger("isHit");
        animOnHit.SetTrigger("isHit");
    }

    public void GainHP(int amount)
    {
        if (currentHP < maxHP)
        {
            currentHP += amount;
            if (currentHP > maxHP) { currentHP = maxHP; }
        }
        else currentHP = maxHP;
        starCount = currentHP / 3;
        if(targetStarSpeed<=0){
          targetStarSpeed=-targetStarSpeed+0.15f;
        }
        else targetStarSpeed=-targetStarSpeed-0.15f;
        animOnStar.ResetTrigger("isStar");
        animOnStar.SetTrigger("isStar");
        starOnCollect.Play();
    }

    private void Start()
    {
        currentStarSpeed = 0.5f;
        starVfx.SetFloat("RotationSpeed", currentStarSpeed); 
        targetStarSpeed = currentStarSpeed;
        starVfx.SetInt("StarCount", 3);
        currentHP = 9;
        oldStarCount = 3;
        starCount = oldStarCount;
    }

    private void Update()
    {
        if (starCount != oldStarCount)
        {   starVfx.SetInt("StarCount", starCount);
            starVfx.Stop();
            starVfx.gameObject.SetActive(false);
            starVfx.gameObject.SetActive(true);
            starVfx.Play();
            oldStarCount = starCount;
        }
        currentStarSpeed = currentStarSpeed + (targetStarSpeed - currentStarSpeed) * tweeningStrength;
        starVfx.SetFloat("RotationSpeed", currentStarSpeed);
    }
}
