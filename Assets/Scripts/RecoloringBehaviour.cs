using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RecoloringBehaviour : MonoBehaviour
{
    [SerializeField] 
    private float _colorChangeTime;

    [SerializeField] 
    private float _colorChangeDelay;
    
    private Color _startColor;
    private Color _nextColor;
    private Color _delayColor;
    private Renderer _renderer;
    
    private float _currentTime;
    
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        GenerateNextColor();
    }

    private void GenerateNextColor()
    {
        _startColor = Random.ColorHSV(0f, 1f, 0.8f, 1f, 1f, 1f);
        _nextColor = Random.ColorHSV(0f, 1f, 0.8f, 1f, 1f, 1f);
    }

    private void StartColorChangeDelay()
    {
        StartCoroutine(ColorChangeDelay());
    }

    private IEnumerator ColorChangeDelay()
    {
        yield return new WaitForSeconds(_colorChangeDelay);
        _currentTime = 0f;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime; 
        var progress = Mathf.PingPong(_currentTime, _colorChangeTime) / _colorChangeTime;
           
       if (_currentTime > _colorChangeTime)
       {
           StartColorChangeDelay();
           GenerateNextColor();
       }
       else
       {
           _renderer.material.color = Color.Lerp(_startColor, _nextColor, progress);
       }
    }
}
