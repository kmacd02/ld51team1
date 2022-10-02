using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public UnityEvent tenSecondsPassed;
    [NonSerialized] public float timer = 0f;

    [SerializeField] private Image fade; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 10f)
        {
            tenSecondsPassed.Invoke();
            timer = 0;
        }

        fade.rectTransform.offsetMax = new Vector2(fade.rectTransform.offsetMax.x, -timer / 10 * 150);
    }

    bool SaveSetting<T> (string s, T value)
    {
        if (typeof(T) == typeof(string))
        {
            PlayerPrefs.SetString(s, (string)(object)value);
            return true;
        }
        if (typeof(T) == typeof(int))
        {
            PlayerPrefs.SetInt(s, (int)(object)value);
            return true;
        }
        if (typeof(T) == typeof(float))
        {
            PlayerPrefs.SetFloat(s, (float)(object)value);
            return true;
        }

        return false;
    }

    T LoadSetting<T>(string s)
    {
        if (PlayerPrefs.HasKey(s)) return default;
        
        if (typeof(T) == typeof(string))
        {
            return (T) Convert.ChangeType(PlayerPrefs.GetString(s), typeof(T));
        }
        if (typeof(T) == typeof(int))
        {
            return (T) Convert.ChangeType(PlayerPrefs.GetInt(s), typeof(T));
        }
        if (typeof(T) == typeof(float))
        {
            return (T) Convert.ChangeType(PlayerPrefs.GetFloat(s), typeof(T));
        }

        return default;
    }
}
