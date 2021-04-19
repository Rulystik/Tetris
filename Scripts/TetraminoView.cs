﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetraminoView : MonoBehaviour
{
    [SerializeField] private bool _isActive;
    [Space]
    [SerializeField] private Image _image;

    public bool IsActive
    {
        get => _isActive;
        set
        {
            if (_isActive != value)
            {
                _isActive = value;
                UpdateView();
            }
        }
    }
    
    [ContextMenu("Update")]
    private void UpdateView()
    {
       _image.enabled = IsActive;
    }

}
