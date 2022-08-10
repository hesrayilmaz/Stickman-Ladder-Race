using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject _floor;
    [SerializeField] private GameObject[] _floors;
    [SerializeField] private SpawnItems _items;
    [SerializeField] private int _numOfFloor;
    private int _numOfBrick;
    private GameObject _currentLevel;
    private int _currentNum = 0, _minNum=40, _maxNum=50;
    private float _yDiff = 480f;
    private float _zDiff = 793f;

    // Start is called before the first frame update
    void Start()
    {
        _floor.transform.position = new Vector3(-16f, 411f, 1263f);
        _floors = new GameObject[_numOfFloor];
        _currentLevel = _floor;
        _floors[0] = _currentLevel;
        //Instantiate(_floor);
        //GenerateLevel();
        _numOfBrick = Random.Range(_minNum, _maxNum);
        for (int i = 0; i < _numOfBrick; i++)
        {
            _items.Init(_currentLevel);
        }
       
    }

    // Update is called once per frame
    public void GenerateLevel()
    {
        _currentNum += 1;
        if (_currentNum < _numOfFloor)
        {
            _floor.transform.position = _floor.transform.position + new Vector3(0f, _yDiff, _zDiff);
            _currentLevel = Instantiate(_floor);
            _floors[_currentNum] = _currentLevel;
            _numOfBrick = Random.Range(_minNum, _maxNum);
            for(int i = 0; i < _numOfBrick; i++)
            {
                _items.Init(_currentLevel);
            }
        }
        else
            _currentLevel = null;
        
    }

    public GameObject GetCurrentLevel()
    {
        return _currentLevel;
    }
    public GameObject GetLevel(int index)
    {
        return _floors[index];
    }

}
