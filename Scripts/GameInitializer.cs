using System;
using DefaultNamespace;
using UnityEngine;

[Serializable]
public class GameInitializer : MonoBehaviour
{
    [SerializeField] private View _view;
    [SerializeField] private Model _model;
    [SerializeField] private Controller _controller;
    void Start()
    {
        _model = new Model();
        _view.Init(_model);
        _controller = new Controller(_model, _view);
        
    }

    // Update is called once per frame
   
}
