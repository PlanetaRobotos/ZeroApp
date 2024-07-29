using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Core;
using UnityEngine;

namespace _Project.Scripts.Data
{
    [Serializable]
    public class SymbolSpriteModel
    {
        public SymbolType SymbolType;
        public Sprite SymbolSprite;
        public Color SymbolColor;
    }
    
    [CreateAssetMenu(fileName = "BoardDatabase", menuName = "Gameplay/BoardDatabase")]
    public class BoardDatabase : ScriptableObject
    {
        [SerializeField] private List<SymbolSpriteModel> symbolSprites;
        
        public List<SymbolSpriteModel> SymbolSprites => symbolSprites;
    }
}