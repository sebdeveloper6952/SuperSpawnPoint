using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Any gameObject that has health can use this script.
/// </summary>
public class CHealth : MonoBehaviour
{
    #region Public Attributes
    [Tooltip("Maximum health, initial health.")]
    public float maxHealth;
    #endregion

    #region Private Attributes
    private float currentHealth;
    #endregion

    #region UnityCallbacks
    private void Start()
    {
        currentHealth = maxHealth;
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Decrease the health of this gameObject by some amount.
    /// If health is less than or equal to 0, death is triggered.
    /// </summary>
    /// <param name="amount"></param>
    public void DecreaseHealth(float amount)
    {
        if (amount <= 0f) return;
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die();
        }
    }

    /// <summary>
    /// Increase the health of this gameObject by some amount;
    /// </summary>
    /// <param name="amount"></param>
    public void IncreaseHealth(float amount)
    {
        if (amount < 0f) return;
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Called when currentHealth is less than or equal to zero.
    /// </summary>
    private void Die()
    {
        // not implemented yet
    }
    #endregion
}
