using System.Collections.Generic;
using Templates.Singleton;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Board.Pieces
{
    public class TileProvider : MonoBehaviourSingleton<TileProvider>
    {
        [SerializeField] private TileBase blue; 
        [SerializeField] private TileBase cyan; 
        [SerializeField] private TileBase green; 
        [SerializeField] private TileBase orange; 
        [SerializeField] private TileBase purple; 
        [SerializeField] private TileBase red;
        [SerializeField] private TileBase yellow; 
        public Dictionary<ETile, TileBase> Tiles { get; set; }

        protected override void AfterAwake()
        {
            Tiles = new Dictionary<ETile, TileBase>()
            {
                { ETile.B, blue },
                { ETile.C, cyan },
                { ETile.G, green },
                { ETile.O, orange },
                { ETile.P, purple },
                { ETile.Y, yellow },
                { ETile.R, red },
                { ETile.Null, null }
            };
        }

        public TileBase Get(ETile tile)
        {
            return Tiles[tile];
        }
    }
}